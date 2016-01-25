using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using Telerik.Web.UI;
using BankProject.DBContext;
using bc = BankProject.Controls;
using BankProject.DBRespository;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class DocumentProcessingSettlement : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private VietVictoryCoreBankingEntities _entities = new VietVictoryCoreBankingEntities();
        private string CodeId
        {
            get { return Request.QueryString["CodeID"]; }
        }
        private bool Disable
        {
            get { return Request.QueryString["disable"] == "1"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Disable)
            {
                SetDisableByReview(false);
            }


            InitDefaultData();
            LoadSettlement();
        }
        protected void MainToolbar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
          var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "save":
                    if (CheckBeforeSaveOrAuthorizePayment())
                    {
                        SavePayment();
                        SaveMT910();
                        Response.Redirect(Globals.NavigateURL(TabId));
                    }
                    break;
                case "review":
                    Response.Redirect(EditUrl("review"));
                    break;
                case "authorize":
                    if (CheckBeforeSaveOrAuthorizePayment())
                    {
                        Authorize();
                    }
                   
                    break;

                case "revert":
                    Revert();
                    break;

                case "print":
                    //Aspose.Words.License license = new Aspose.Words.License();
                    //license.SetLicense("Aspose.Words.lic");

                    ////Open template
                    //string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/RegisterDocumentaryCollection.doc");
                    ////Open the template document
                    //Aspose.Words.Document doc = new Aspose.Words.Document(path);
                    ////Execute the mail merge.
                    //DataSet ds = new DataSet();
                    //ds = SQLData.B_BDOCUMETARYCOLLECTION_Report(txtCode.Text);

                    //// Fill the fields in the document with user data.
                    //doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
                    //// Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
                    //doc.Save("RegisterDocumentaryCollection_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
                    break;
            }
        }
        protected void SetDisableByReview(bool flag)
        {
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, flag);
            if (Request.QueryString["disable"] != null)
                mainToolbar.FindItemByValue("btPrint").Enabled = true;
            else
                mainToolbar.FindItemByValue("btPrint").Enabled = false;
        }

        private bool CheckStatusExportDocumentCollection(BEXPORT_LC_DOCS_PROCESSING expDoc)
        {
            if (expDoc.Status != "AUT" || expDoc.RejectStatus == "UNA" || (expDoc.AmendStatus != null && expDoc.AmendStatus != "AUT"))
            {
                lblError.Text = "Document was not authorized";
                return false;
            }
            if (expDoc.RejectStatus == "AUT")
            {
                lblError.Text = "Document was rejected";
                return false;
            }
            if (expDoc.PaymentFull == true)
            {
                lblError.Text = "This document was paid full";
                return false;
            }
            return true;
        }

        private string GetNextPaymentCode(string docCode)
        {
            if (_entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT.Any(q => q.CollectionPaymentCode == docCode))
            {
                var lst = _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT.Where(q => q.CollectionPaymentCode == docCode).ToList();
                var Id = lst.Max(q => q.PaymentNo);
                if (Id == null)
                    return docCode + ".1";
                else
                {
                    Id++;
                    return docCode + "." + Id;
                }
            }
            return docCode + ".1";
        }

        private double GetAmountCredited(string docCode)
        {
            if (_entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT.Any(q => q.CollectionPaymentCode == docCode && q.Status == "AUT"))
            {
                var closestPayment =
                    _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT.Where(q => q.CollectionPaymentCode == docCode && q.Status == "AUT")
                        .OrderByDescending(q => q.PaymentNo)
                        .FirstOrDefault();
                if (closestPayment != null)
                {
                    return closestPayment.AmtCredited + closestPayment.DrawingAmount??0;
                }
            }
            return 0;
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Sep 12, 2015    Hien Nguyen       Fix bug 66 _ remove Exchange Rate
         */
        protected void Authorize()
        {

            var payment = _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT.FirstOrDefault(q => q.PaymentId == txtCode.Text);
            
            if (payment != null)
            {
                var doc = _entities.BEXPORT_LC_DOCS_PROCESSING.FirstOrDefault(q => q.DocCode == payment.CollectionPaymentCode);
                var drFromAccount = _entities.BDRFROMACCOUNTs.FirstOrDefault(q => q.Id == comboCreditAcct.SelectedValue);
                if (drFromAccount != null)
                {
                    drFromAccount.Amount = drFromAccount.Amount; /*+ (decimal)(numExchangeRate.Value ?? 0) * (decimal)(numDrawingAmount.Value ?? 0);*/ //fixed bug 66
                }
                if (numDrawingAmount.Value == doc.Amount - double.Parse(lblCreditAmount.Text))
                {
                    doc.PaymentFull = true;
                }
                payment.Status = "AUT";
                _entities.SaveChanges();
            }
            Response.Redirect(Globals.NavigateURL(TabId));
        }

        protected void Revert()
        {
            var payment = _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT.FirstOrDefault(q => q.PaymentId == txtCode.Text);
            if (payment != null)
            {
                payment.Status = "REV";
                _entities.SaveChanges();
            }

            Response.Redirect(Globals.NavigateURL(TabId, "", "CodeID=" + txtCode.Text));
        }

        private bool CheckBeforeSaveOrAuthorizePayment()
        {
            var doc =
                _entities.BEXPORT_LC_DOCS_PROCESSING.FirstOrDefault(q => q.DocCode == CodeId.Substring(0, 16));
            if (doc.PaymentFull == true)
            {
                lblError.Text = "This document was paid full";
                return false;
            }
            if (numDrawingAmount.Value > doc.Amount - double.Parse(lblCreditAmount.Text)) 
            {
                lblError.Text = "Drawing amount must be less then or equal remain amount";
                return false;
            }
            return true;
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Sep 12, 2015    Hien Nguyen       Fix bug 65 _ remove Payment Method 
         * 0.3            Sep 12, 2015    Hien Nguyen       Fix bug 66 _ remove Exchange Rate 
         */
        private void SavePayment()
        {

            var outCoPayment =
                       _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT.FirstOrDefault(q => q.PaymentId == txtCode.Text);
            if (outCoPayment == null) // insert
            {
                outCoPayment = new BEXPORT_DOCS_PROCESSING_SETTLEMENT();
                outCoPayment.Id = Guid.NewGuid();
                outCoPayment.PaymentId = txtCode.Text;
                outCoPayment.PaymentNo = long.Parse(txtCode.Text.Substring(17));
                outCoPayment.ValueDate = dtValueDate.SelectedDate;
                outCoPayment.AmtCredited = double.Parse(lblCreditAmount.Text);
                outCoPayment.Status = "UNA";
                outCoPayment.CreateBy = UserId.ToString();
                //outCoPayment.PaymentMethod = comboPaymentMethod.SelectedValue; //fixed bug 65
                outCoPayment.PaymentRemarks1 = txtPaymentRemarks1.Text;
                outCoPayment.PaymentRemarks2 = txtPaymentRemarks2.Text;
                outCoPayment.CollectionPaymentCode = txtCode.Text.Substring(0, 14);
                outCoPayment.CreateDate = DateTime.Now.Date;
                outCoPayment.Currency = comboCreditCurrency.SelectedValue;
                outCoPayment.DrawType = comboDrawType.SelectedValue;
                outCoPayment.DrawingAmount = numDrawingAmount.Value;
                //outCoPayment.ExchRate = numExchangeRate.Value; //fixed bug 66
                outCoPayment.IncreaseMental = 0;
                outCoPayment.Currency = comboCurrency.SelectedValue;
                outCoPayment.CreditAccount = comboCreditAcct.SelectedValue;
                outCoPayment.CountryCode = comboCountryCode.SelectedValue;
                outCoPayment.LCType = comboCollectionType.SelectedValue;
                _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT.Add(outCoPayment);
            }
            else
            {
                outCoPayment.PaymentId = txtCode.Text;
                outCoPayment.PaymentNo = long.Parse(txtCode.Text.Substring(15));
                outCoPayment.ValueDate = dtValueDate.SelectedDate;
                outCoPayment.AmtCredited = double.Parse(lblCreditAmount.Text);
                outCoPayment.UpdatedBy = UserId.ToString();
                //outCoPayment.PaymentMethod = comboPaymentMethod.SelectedValue; //fixed bug 65
                outCoPayment.PaymentRemarks1 = txtPaymentRemarks1.Text;
                outCoPayment.PaymentRemarks2 = txtPaymentRemarks2.Text;
                outCoPayment.CollectionPaymentCode = txtCode.Text.Substring(0, 14);
                outCoPayment.UpdatedDate = DateTime.Now.Date;
                outCoPayment.Currency = comboCreditCurrency.SelectedValue;
                outCoPayment.DrawType = comboDrawType.SelectedValue;
                outCoPayment.DrawingAmount = numDrawingAmount.Value;
                outCoPayment.CountryCode = comboCountryCode.SelectedValue;
                //outCoPayment.ExchRate = numExchangeRate.Value; //fixed bug 66
                outCoPayment.IncreaseMental = 0;
                outCoPayment.CreditAccount = comboCreditAcct.SelectedValue;
                outCoPayment.LCType = comboCollectionType.SelectedValue;
            }
            _entities.SaveChanges();
            //SaveCharges();
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Sep 12, 2015    Hien Nguyen       Fix bug 46 _ remove nostro Account
         * 0.3            Oct 22, 2015    Hien Nguyen       Fixed bug 86
        */
        private void LoadMT910()
        {
            var mt910 = _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT_MT910.FirstOrDefault(q => q.PaymentId == CodeId);
            if (mt910 != null)
            {
                txtTransactionRefNumber.Text = mt910.TransactionReferenceNumber;
                txtRelatedRef.Text = mt910.RelatedReference;
                txtAccountIndentification.Text = mt910.AccountIndentification;
                dtValueDateMt910.SelectedDate = mt910.ValueDate;
                //fixed bug 46 get value of currency from tab Main
                comboCurrencyMt910.SelectedValue = comboCreditCurrency.SelectedValue;
                numAmountMt910.Value = (double)(mt910.Amount??0);
                txtOrderingInstitutionName.Text = mt910.OrderingInstitutionName;
                txtOrderingInstitutionAddress1.Text = mt910.OrderingInstitutionAddress1;
                txtOrderingInstitutionAddress2.Text = mt910.OrderingInstitutionAddress2;
                txtOrderingInstitutionAddress3.Text = mt910.OrderingInstitutionAddress3;
                txtIntermediaryName.Text = mt910.IntermediaryName;
                txtIntermediaryAddress1.Text = mt910.IntermediaryAddress1;
                txtIntermediaryAddress2.Text = mt910.IntermediaryAddress2;
                txtIntermediaryAddress3.Text = mt910.IntermediaryAddress3;
                txtSendMessage.Text = mt910.SendMessage;
                //cboNostroAcct.SelectedValue = mt910.NostroAccount;
                //Fixed bug 46
                //cbNostroAccount.SelectedValue = mt910.NostroAccount;
                //lblNostro.Text = cbNostroAccount.SelectedItem.Attributes["Code"] + " - " + cbNostroAccount.SelectedItem.Attributes["Description"];
            }
        }

        /*
        * Method Revision History:
        * Version        Date            Author            Comment
        * ----------------------------------------------------------
        * 0.1            NA
        * 0.2            Sep 12, 2015    Hien Nguyen       Fix bug 46 _ remove Nostro Account
        */
        private void SaveMT910()
        {
            var mt910 = _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT_MT910.FirstOrDefault(q => q.PaymentId == txtCode.Text);
            if (mt910 == null)
            {
                mt910 = new BEXPORT_DOCS_PROCESSING_SETTLEMENT_MT910();
                mt910.Id = Guid.NewGuid();
                mt910.PaymentId = txtCode.Text;
                mt910.TransactionReferenceNumber = txtTransactionRefNumber.Text;
                mt910.RelatedReference = txtRelatedRef.Text;
                mt910.AccountIndentification = txtAccountIndentification.Text;
                mt910.ValueDate = dtValueDateMt910.SelectedDate;
                mt910.Currency = comboCurrencyMt910.SelectedValue;
                mt910.Amount = (decimal) (numAmountMt910.Value ?? 0);
                mt910.OrderingInstitutionName = txtOrderingInstitutionName.Text;
                mt910.OrderingInstitutionAddress1 = txtOrderingInstitutionAddress1.Text;
                mt910.OrderingInstitutionAddress2 = txtOrderingInstitutionAddress2.Text;
                mt910.OrderingInstitutionAddress3 = txtOrderingInstitutionAddress3.Text;
                mt910.IntermediaryName = txtIntermediaryName.Text;
                mt910.IntermediaryAddress1 = txtIntermediaryAddress1.Text;
                mt910.IntermediaryAddress2 = txtIntermediaryAddress2.Text;
                mt910.IntermediaryAddress3 = txtIntermediaryAddress3.Text;
                mt910.SendMessage = txtSendMessage.Text;
                //mt910.NostroAccount = cboNostroAcct.SelectedValue;
                //mt910.NostroAccount = cbNostroAccount.SelectedValue;  //fixed bug 46
                _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT_MT910.Add(mt910);
            }
            else
            {
                mt910.TransactionReferenceNumber = txtTransactionRefNumber.Text;
                mt910.RelatedReference = txtRelatedRef.Text;
                mt910.AccountIndentification = txtAccountIndentification.Text;
                mt910.ValueDate = dtValueDateMt910.SelectedDate;
                mt910.Currency = comboCurrencyMt910.SelectedValue;
                mt910.Amount = (decimal)(numAmountMt910.Value ?? 0);
                mt910.OrderingInstitutionName = txtOrderingInstitutionName.Text;
                mt910.OrderingInstitutionAddress1 = txtOrderingInstitutionAddress1.Text;
                mt910.OrderingInstitutionAddress2 = txtOrderingInstitutionAddress2.Text;
                mt910.OrderingInstitutionAddress3 = txtOrderingInstitutionAddress3.Text;
                mt910.IntermediaryName = txtIntermediaryName.Text;
                mt910.IntermediaryAddress1 = txtIntermediaryAddress1.Text;
                mt910.IntermediaryAddress2 = txtIntermediaryAddress2.Text;
                mt910.IntermediaryAddress3 = txtIntermediaryAddress3.Text;
                //mt910.NostroAccount = cbNostroAccount.SelectedValue; //fixed bug 46
                mt910.SendMessage = txtSendMessage.Text;
            }
            _entities.SaveChanges();
        }
        private void UpsertCharge(string weiveCharges, string chargeCode,string chargeAcct, string chargeCcy,string chargeAmt,
            string partyCharged, string omortCharges, string chargeStatus, string chargeRemarks, string vatNo, string taxCode,
            string taxAmt, string rowCharge)
        {
            var charge =
                _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT_CHARGES.FirstOrDefault(
                    q => q.CollectionPaymentCode == txtCode.Text && q.Rowchages == rowCharge);
            if (charge == null)
            {
                charge = new BEXPORT_DOCS_PROCESSING_SETTLEMENT_CHARGES
                {
                    Id = Guid.NewGuid(),
                    CollectionPaymentCode = txtCode.Text,
                    WaiveCharges = weiveCharges,
                    Chargecode = chargeCode,
                    ChargeAcct = chargeAcct,
                    ChargeCcy = chargeCcy,
                    ChargeAmt = chargeAmt,
                    PartyCharged = partyCharged,
                    OmortCharges = omortCharges,
                    ChargeStatus = chargeStatus,
                    ChargeRemarks = chargeRemarks,
                    VATNo = vatNo,
                    TaxCode = taxCode,
                    TaxAmt = taxAmt,
                    Rowchages = rowCharge
                };
                _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT_CHARGES.Add(charge);
            }
            else
            {
                charge.WaiveCharges = weiveCharges;
                charge.Chargecode = chargeCode;
                charge.ChargeAcct = chargeAcct;
                charge.ChargeCcy = chargeCcy;
                charge.ChargeAmt = chargeAmt;
                charge.PartyCharged = partyCharged;
                charge.OmortCharges = omortCharges;
                charge.ChargeStatus = chargeStatus;
                charge.ChargeRemarks = chargeRemarks;
                charge.VATNo = vatNo;
                charge.TaxCode = taxCode;
                charge.TaxAmt = taxAmt;

            }
            _entities.SaveChanges();
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 03, 2015    Hien Nguyen       Fix bug 65 _ remove Payment Method 
         * 0.3            Oct 03, 2015    Hien Nguyen       Fix bug 66 _ remove Exchange Rate 
         */
        void LoadSettlementDetail(BEXPORT_DOCS_PROCESSING_SETTLEMENT outColPayment)
        {
            var exportDoc = _entities.BEXPORT_LC_DOCS_PROCESSING.FirstOrDefault(q => q.DocCode == outColPayment.CollectionPaymentCode);
            if (outColPayment.LCType.Equals("ELCM") || outColPayment.LCType.Equals("ELCP") || outColPayment.LCType.Equals("ELC"))
            {
                comboDrawType.SelectedValue = "SP";
            }
            else
            {
                comboDrawType.SelectedValue = "MA";
            }
            //comboDrawType.SelectedValue = outColPayment.DrawType;
            lblDrawType.Text = comboDrawType.SelectedItem.Attributes["Description"];
            dtValueDate.SelectedDate = outColPayment.ValueDate;
            numDrawingAmount.Value = outColPayment.DrawingAmount;
            comboCountryCode.SelectedValue = outColPayment.CountryCode;
            lblCreditAmount.Text = (outColPayment.AmtCredited??0).ToString("#,##0.00");
            //comboPaymentMethod.SelectedValue = outColPayment.PaymentMethod; //fixed bug 65
            comboCreditCurrency.SelectedValue = outColPayment.Currency;
            LoadCreditAccount();
            comboCreditAcct.SelectedValue = outColPayment.CreditAccount;
            loadNostroAccount();
            //numExchangeRate.Value = outColPayment.ExchRate; //fixed bug 66
            txtPaymentRemarks1.Text = outColPayment.PaymentRemarks1;
            txtPaymentRemarks2.Text = outColPayment.PaymentRemarks2;
            //LoadCharges();
            //bc.Commont.initRadComboBox(ref cboNostroAcct, "Code", "AccountNo", _entities.BSWIFTCODEs.Where(q => q.Currency.Equals(outColPayment.Currency)).ToList());
        }
        private void LoadData(string strtxtCode)
        {
            var outCoPayment =
                        _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT.FirstOrDefault(q => q.PaymentId == strtxtCode);
            if (outCoPayment == null)
            {
                lblError.Text = "Not found settlement";
            }
            else
            {
                //txtCode.Text = CodeId;
                var expDocCode = CodeId.Substring(0, 14);
                lblCreditAmount.Text = GetAmountCredited(expDocCode).ToString("#,##0.00");
                var expDoc = _entities.BEXPORT_LC_DOCS_PROCESSING.FirstOrDefault(q => q.DocCode == expDocCode && q.ActiveRecordFlag == "YES");
                if (expDoc == null)
                {
                    lblError.Text = "Document does not exists";
                    return;
                }
                if (CheckStatusExportDocumentCollection(expDoc))
                {
                    LoadExpDoc(expDoc);
                    LoadSettlementDetail(outCoPayment);
                    LoadMT910();
                }

                if (Disable) // authorize
                {
                    if (outCoPayment.Status == "AUT")
                    {
                        lblError.Text = "Payment was authorized";
                        mainToolbar.FindItemByValue("btPrint").Enabled = true;
                    }
                    else // Not yet authorize
                    {
                        mainToolbar.FindItemByValue("btAuthorize").Enabled = true;
                        mainToolbar.FindItemByValue("btRevert").Enabled = true;
                        mainToolbar.FindItemByValue("btPrint").Enabled = true;
                    }
                    SetDisableByReview(false);
                }
                else // Editing
                {
                    if (outCoPayment.Status == "AUT") // Authorized
                    {
                        mainToolbar.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "Payment was authorized";
                        SetDisableByReview(false);
                    }
                    else // Not yet authorize
                    {
                        mainToolbar.FindItemByValue("btSave").Enabled = true;
                    }

                }
            }
        }
        private void LoadSettlement()
        {
            mainToolbar.FindItemByValue("btReview").Enabled = true;
            if (string.IsNullOrWhiteSpace(CodeId)) return;
            lblCreditAmount.Text = GetAmountCredited(CodeId).ToString("");
            if (CodeId.Length == 16)
            {
                lblCreditAmount.Text = GetAmountCredited(CodeId).ToString("#,##0.00");

                var expDoc = _entities.BEXPORT_LC_DOCS_PROCESSING.FirstOrDefault(q => q.DocCode == CodeId && q.ActiveRecordFlag == "YES");
                if (expDoc == null)
                {
                    lblError.Text = "Document does not exists";
                    return;
                }
                if (CheckStatusExportDocumentCollection(expDoc))
                {
                    var lstDoc = _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT.Where(x => x.CollectionPaymentCode == CodeId).ToList();
                    if (lstDoc != null && lstDoc.Count > 0)
                    {
                        var DetailDoc = lstDoc.Where(x => x.Status == "UNA"||x.Status=="REV").FirstOrDefault();
                        if (DetailDoc != null)
                        {
                            txtCode.Text = DetailDoc.PaymentId;
                            LoadData(txtCode.Text);
                            return;
                        }
                    }
                    txtCode.Text = GetNextPaymentCode(CodeId);
                    GenerateVatNo();
                    LoadExpDoc(expDoc);
                    mainToolbar.FindItemByValue("btSave").Enabled = true;
                }
                else
                {
                    txtCode.Text = CodeId;
                }
            }
            else if (CodeId.Length > 16)
            {
                var outCoPayment =
                        _entities.BEXPORT_DOCS_PROCESSING_SETTLEMENT.FirstOrDefault(q => q.PaymentId == CodeId);
                if (outCoPayment == null)
                {
                    lblError.Text = "Not found settlement";
                }
                else
                {
                    txtCode.Text = CodeId;
                    var expDocCode = CodeId.Substring(0, 16);
                    lblCreditAmount.Text = GetAmountCredited(expDocCode).ToString("#,##0.00");
                    var expDoc = _entities.BEXPORT_LC_DOCS_PROCESSING.FirstOrDefault(q => q.DocCode == expDocCode && q.ActiveRecordFlag == "YES");
                    if (expDoc == null)
                    {
                        lblError.Text = "Document does not exists";
                        return;
                    }
                    if (CheckStatusExportDocumentCollection(expDoc))
                    {
                        LoadExpDoc(expDoc);
                        LoadSettlementDetail(outCoPayment);
                        LoadMT910();
                    }

                    if (Disable) // authorize
                    {
                        if (outCoPayment.Status == "AUT")
                        {
                            lblError.Text = "Payment was authorized";
                            mainToolbar.FindItemByValue("btPrint").Enabled = true;
                        }
                        else // Not yet authorize
                        {
                            mainToolbar.FindItemByValue("btAuthorize").Enabled = true;
                            mainToolbar.FindItemByValue("btRevert").Enabled = true;
                            mainToolbar.FindItemByValue("btPrint").Enabled = true;
                        }
                        SetDisableByReview(false);
                    }
                    else // Editing
                    {
                        if (outCoPayment.Status == "AUT") // Authorized
                        {
                            mainToolbar.FindItemByValue("btPrint").Enabled = true;
                            lblError.Text = "Payment was authorized";
                            SetDisableByReview(false);
                        }
                        else // Not yet authorize
                        {
                            mainToolbar.FindItemByValue("btSave").Enabled = true;
                        }

                    }
                }
            }


        }

        protected void LoadExpDoc(BEXPORT_LC_DOCS_PROCESSING expDoc)
        {
            
            if (expDoc != null)
            {
                //mainToolbar.FindItemByValue("btReview").Enabled = false;

                //var drow = dsDoc.Tables[0].Rows[0];

                #region Load Export Collection
               // comboCollectionType.SelectedValue = expDoc.CollectionType;
                lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Attributes["Description"];

                //comboDrawerCusNo.SelectedValue = expDoc.DrawerCusNo;
                //txtDrawerCusName.Text = expDoc.DrawerCusName;
                //txtDrawerAddr1.Text = expDoc.DrawerAddr1;
                //txtDrawerAddr2.Text = expDoc.DrawerAddr2;
                //txtDrawerAddr3.Text = expDoc.DrawerAddr3;
                //txtDrawerRefNo.Text = expDoc.DrawerRefNo;
                comboCollectingBankNo.SelectedValue = expDoc.IssuingBankNo;
                txtCollectingBankName.Text = expDoc.IssuingBankName;
                txtCollectingBankAddr1.Text = expDoc.IssuingBankAddr1;
                txtCollectingBankAddr2.Text = expDoc.IssuingBankAddr2;
                comboCollectingBankAcct.SelectedValue = expDoc.IssuingBankAddr3;
                //txtDraweeCusNo.Text = expDoc.DraweeCusNo;
                //txtDraweeCusName.Text = expDoc.DraweeCusName;
                //txtDraweeAddr1.Text = expDoc.DraweeAddr1;
                //txtDraweeAddr2.Text = expDoc.DraweeAddr2;
                //txtDraweeAddr3.Text = expDoc.DraweeAddr3;
                //comboNostroCusNo.SelectedValue = expDoc.NostroCusNo;
                lblNostroCusName.Text = comboNostroCusNo.SelectedItem.Attributes["Description"];
                comboCreditCurrency.SelectedValue = expDoc.Currency;
                comboCurrency.SelectedValue = expDoc.Currency;
                loadNostroAccount();
                numAmount.Value = expDoc.Amount;
                txtTenor.Text = expDoc.Tenor;
                //numReminderDays.Text = (expDoc.ReminderDays??0).ToString();

                comboCommodity.SelectedValue = expDoc.Commodity;
                txtCommodityName.Text = comboCommodity.SelectedItem.Attributes["Name2"];

                comboDocsCode1.SelectedValue = expDoc.DocsCode1;
                numNoOfOriginals1.Text = (expDoc.NoOfOriginals1??0).ToString();
                numNoOfCopies1.Text = (expDoc.NoOfCopies1??0).ToString();


                comboDocsCode2.SelectedValue = expDoc.DocsCode2;
                numNoOfOriginals2.Text = (expDoc.NoOfOriginals2 ?? 0).ToString();
                numNoOfCopies2.Text = (expDoc.NoOfCopies2 ?? 0).ToString();

                comboDocsCode3.SelectedValue = expDoc.DocsCode3;
                numNoOfOriginals3.Text = (expDoc.NoOfOriginals3 ?? 0).ToString();
                numNoOfCopies3.Text = (expDoc.NoOfCopies3 ?? 0).ToString();

                divDocsCode2.Visible = (expDoc.NoOfCopies2 ?? 0) > 0 || (expDoc.NoOfOriginals2 ?? 0) > 0;
                divDocsCode3.Visible = (expDoc.NoOfCopies3 ?? 0) > 0 || (expDoc.NoOfOriginals3 ?? 0) > 0;

                txtOtherDocs.Text = expDoc.OtherDocs1;
                txtRemarks.Text = expDoc.Remark;

                if (expDoc.DocumentReceivedDate.HasValue && expDoc.DocumentReceivedDate.Value != new DateTime(1900, 1, 1))
                {
                    dteDocsReceivedDate.SelectedDate = expDoc.DocumentReceivedDate.Value;
                }
                if (expDoc.MaturityDate.HasValue && expDoc.MaturityDate.Value != new DateTime(1900, 1, 1))
                {
                    dteMaturityDate.SelectedDate = expDoc.MaturityDate.Value;
                }
                //if (expDoc.TracerDate.HasValue && expDoc.TracerDate.Value != new DateTime(1900, 1, 1))
                //{
                //    dteTracerDate.SelectedDate = expDoc.TracerDate.Value;
                //}
                //bc.Commont.initRadComboBox(ref cboNostroAcct, "Code", "AccountNo", _entities.BSWIFTCODEs.Where(q => q.Currency.Equals(expDoc.Currency)).ToList());//
                //cboNostroAcct
                #endregion
            }
            else
            {
                comboCollectionType.SelectedValue = string.Empty;
                lblCollectionTypeName.Text = string.Empty;


                comboNostroCusNo.SelectedValue = string.Empty;
                txtDrawerCusName.Text = string.Empty;
                txtDrawerAddr1.Text = string.Empty;
                txtDrawerAddr2.Text = string.Empty;
                txtDrawerAddr3.Text = string.Empty;
                txtDrawerRefNo.Text = string.Empty;
                comboCollectingBankNo.SelectedValue = string.Empty;
                txtCollectingBankName.Text = string.Empty;
                txtCollectingBankAddr1.Text = string.Empty;
                txtCollectingBankAddr2.Text = string.Empty;
                comboCollectingBankAcct.SelectedValue = string.Empty;
                txtDraweeCusName.Text = string.Empty;
                txtDraweeAddr1.Text = string.Empty;
                txtDraweeAddr2.Text = string.Empty;
                txtDraweeAddr3.Text = string.Empty;
                comboNostroCusNo.SelectedValue = string.Empty;
                comboCurrency.SelectedValue = string.Empty;
                numAmount.Text = string.Empty;
                txtTenor.Text = "AT SIGHT";
                numReminderDays.Text = string.Empty;

                comboCommodity.SelectedValue = string.Empty;
                comboDocsCode1.SelectedValue = string.Empty;
                numNoOfOriginals1.Text = string.Empty;
                numNoOfCopies1.Text = string.Empty;

                comboDocsCode2.SelectedValue = string.Empty;
                numNoOfOriginals2.Text = string.Empty;
                numNoOfCopies2.Text = string.Empty;

                txtOtherDocs.Text = string.Empty;
                txtRemarks.Text = string.Empty;

                dteDocsReceivedDate.SelectedDate = null;
                dteMaturityDate.SelectedDate = null;
                dteTracerDate.SelectedDate = null;

            }


            

            //SetVisibilityByStatus(dsDoc);
        }
        
        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 03, 2015    Hien Nguyen       Fix bug 65 _ remove Payment Method 
         */
        protected void InitDefaultData()
        {
            foreach (RadToolBarItem item in mainToolbar.Items)
            {
                item.Enabled = false;
            }

            bc.Commont.initRadComboBox(ref comboCountryCode, "TenTA", "TenTA", bd.SQLData.B_BCOUNTRY_GetAll());

            dteDocsReceivedDate.SelectedDate = DateTime.Now;
            dteTracerDate.SelectedDate = DateTime.Now.AddDays(30);

            divDocsCode2.Visible = false;
            divDocsCode3.Visible = false;

            //fixed bug 65 start
            // bind payment method
            //comboPaymentMethod.Items.Clear();
            //comboPaymentMethod.DataValueField = "Code";
            //comboPaymentMethod.DataTextField = "Description";
            //comboPaymentMethod.DataSource = _entities.BPAYMENTMETHODs.ToList();
            //comboPaymentMethod.DataBind();
            //lblPaymentMethod.Text = comboPaymentMethod.SelectedItem.Attributes["Description"];
            //fixed bug 65 ends
            

            //bind draw type
            comboDrawType.Items.Clear();
            comboDrawType.DataValueField = "ID";
            comboDrawType.DataTextField = "ID";
            comboDrawType.DataSource = bd.SQLData.CreateGenerateDatas("OutgoingPayment_TabMain_DrawType");
            comboDrawType.DataBind();
            lblDrawType.Text = comboDrawType.SelectedItem.Attributes["Description"];

            dtValueDate.SelectedDate = DateTime.Now;

            // bind value collection type
            //comboCollectionType.Items.Clear();
            //comboCollectionType.DataValueField = "ID";
            //comboCollectionType.DataTextField = "ID";
            //comboCollectionType.DataSource = bd.DataTam.B_BLCTYPES_GetAll("Export");
            //comboCollectionType.DataBind();

            bc.Commont.initRadComboBox(ref comboCollectionType, "LCTYPE", "LCTYPE", bd.DataTam.B_BLCTYPES_GetAll("Export"));
            lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Attributes["Description"];

            // bind drawer
            comboDrawerCusNo.Items.Clear();
            comboDrawerCusNo.Items.Add(new RadComboBoxItem(""));
            comboDrawerCusNo.DataValueField = "CustomerID";
            comboDrawerCusNo.DataTextField = "CustomerID";
            comboDrawerCusNo.DataSource = _entities.BCUSTOMERS.Where(q=>q.CustomerID.StartsWith("2")).ToList();
            comboDrawerCusNo.DataBind();

            // bind collecting bank no
            comboCollectingBankNo.Items.Clear();
            comboCollectingBankNo.Items.Add(new RadComboBoxItem(""));
            comboCollectingBankNo.DataValueField = "Code";
            comboCollectingBankNo.DataTextField = "Code";
            comboCollectingBankNo.DataSource = _entities.BSWIFTCODEs.ToList();
            comboCollectingBankNo.DataBind();

            // bind nostro cus no
            comboNostroCusNo.Items.Clear();
            comboNostroCusNo.Items.Add(new RadComboBoxItem(""));
            comboNostroCusNo.DataValueField = "AccountNo";
            comboNostroCusNo.DataTextField = "Code";
            comboNostroCusNo.DataSource = _entities.BSWIFTCODEs.ToList(); 
            comboNostroCusNo.DataBind();

            comboDocsCode1.Items.Clear();
            comboDocsCode1.Items.Add(new RadComboBoxItem(""));
            comboDocsCode1.DataValueField = "Id";
            comboDocsCode1.DataTextField = "Description";
            comboDocsCode1.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_DocsCode");
            comboDocsCode1.DataBind();

            comboDocsCode2.Items.Clear();
            comboDocsCode2.Items.Add(new RadComboBoxItem(""));
            comboDocsCode2.DataValueField = "Id";
            comboDocsCode2.DataTextField = "Description";
            comboDocsCode2.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_DocsCode");
            comboDocsCode2.DataBind();

            comboDocsCode3.Items.Clear();
            comboDocsCode3.Items.Add(new RadComboBoxItem(""));
            comboDocsCode3.DataValueField = "Id";
            comboDocsCode3.DataTextField = "Description";
            comboDocsCode3.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_DocsCode");
            comboDocsCode3.DataBind();

            comboCommodity.Items.Clear();
            comboCommodity.Items.Add(new RadComboBoxItem(""));
            comboCommodity.DataValueField = "ID";
            comboCommodity.DataTextField = "ID";
            comboCommodity.DataSource = bd.DataTam.B_BCOMMODITY_GetAll();
            comboCommodity.DataBind();

            var curList = _entities.BCURRENCies.ToList();
            bc.Commont.initRadComboBox(ref comboCreditCurrency, "Code", "Code", curList);
            bc.Commont.initRadComboBox(ref comboCurrency, "Code", "Code", curList);
            bc.Commont.initRadComboBox(ref comboCurrencyMt910, "Code", "Code", curList);

            //remove "GOLD" from list of currency
            bc.Commont.removeCurrencyItem(comboCreditCurrency, "GOLD");
            bc.Commont.removeCurrencyItem(comboCurrency, "GOLD");
            bc.Commont.removeCurrencyItem(comboCurrencyMt910, "GOLD");

            #region MT910

            dtValueDateMt910.SelectedDate = DateTime.Now.Date;            

            #endregion
        }

        protected void LoadCreditAccount()
        {
            StoreProRepository facade = new StoreProRepository();

            comboCreditAcct.Items.Clear();
            comboCreditAcct.Items.Add(new RadComboBoxItem(""));
            comboCreditAcct.DataValueField = "Id";
            comboCreditAcct.DataTextField = "Id";
            comboCreditAcct.DataSource = bd.SQLData.B_BCRFROMACCOUNT_GetByCurrency_Name(txtDrawerCusName.Text, comboCreditCurrency.SelectedValue);
            //comboCreditAcct.DataSource = facade.StoreProcessor().B_BCRFROMACCOUNT_GetByCurrency_Name(txtDrawerCusName.Text, comboCreditCurrency.SelectedValue).ToList();
            comboCreditAcct.DataBind();
        }

        protected void commom_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }

        protected void rcbLCType_SelectIndexChange(object sender, EventArgs e)
        {
            lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Attributes["Description"].ToString();
        }

        protected void rcbLCType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["LCTYPE"] = row["LCTYPE"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
            e.Item.Attributes["Category"] = row["Category"].ToString();
        }
        protected void commomSwiftCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as BSWIFTCODE;
            e.Item.Attributes["Code"] = row.Code;
            e.Item.Attributes["Description"] = row.Description;
        }
        protected void comboCommodity_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["ID"] = row["ID"].ToString();
            e.Item.Attributes["Name2"] = row["Name2"].ToString();
        }
        protected void comboDrawType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblDrawType.Text = comboDrawType.SelectedItem.Attributes["Description"];
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 03, 2015    Hien Nguyen       Fix bug 65 _ remove Payment Method 
         */
        /*protected void comboPaymentMethod_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as BPAYMENTMETHOD;
            e.Item.Attributes["Code"] = row.Code;
            e.Item.Attributes["Description"] = row.Description;
        }*/

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 03, 2015    Hien Nguyen       Fix bug 65 _ remove Payment Method 
         */
       /* protected void comboPaymentMethod_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            
            lblPaymentMethod.Text = comboPaymentMethod.SelectedItem.Attributes["Description"];
        }*/

        protected void cbNostroAccount_DataBound(object sender, EventArgs e)
        {
            var combo = (RadComboBox)sender;
            combo.Items.Insert(0, new RadComboBoxItem("", string.Empty));
        }
        protected void cbNostroAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as BSWIFTCODE;
            e.Item.Attributes["Code"] = row.Code;
            e.Item.Attributes["AccountNo"] = row.AccountNo;
            e.Item.Attributes["Description"] = row.Description;
        }
        protected void cbNostroAccount_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblNostro.Text = cbNostroAccount.SelectedItem.Attributes["Code"] + " - " + cbNostroAccount.SelectedItem.Attributes["Description"];
        }

        protected void comboCreditAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 07, 2015    Hien Nguyen       Fix bug 86
         */
        protected void comboCreditCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadCreditAccount();
            loadNostroAccount();
            comboCurrencyMt910.SelectedValue = e.Value; //fixed bug 86
        }

        protected void GenerateVatNo()
        {
            var vatno = bd.Database.B_BMACODE_GetNewSoTT("VATNO");
            
        }
        protected void comboDrawerCusNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as BCUSTOMER;
            e.Item.Attributes["CustomerID"] = row.CustomerID;
            e.Item.Attributes["CustomerName2"] = row.CustomerName;
        }

        private void showReport(int reportType)
        {
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Credit/";
            string reportSaveName = "";
            DataSet reportData = null;
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {
                reportData = bd.IssueLC.ExportLCSettlementReport(reportType, txtCode.Text, this.UserInfo.Username);
                switch (reportType)
                {
                    case 1://PhieuChuyenKhoan
                        reportTemplate = Context.Server.MapPath(reportTemplate + "SettlementPhieuChuyenKhoan.doc");
                        reportSaveName = "PhieuChuyenKhoan" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        break;
                    case 2://VAT B
                        reportTemplate = Context.Server.MapPath(reportTemplate + "SettlementVAT.doc");
                        reportSaveName = "VAT" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        break;
                    case 3://Phieu xaut ngoai bang
                        reportTemplate = Context.Server.MapPath(reportTemplate + "SettlementPHIEUXUATNGOAIBANG.doc");
                        reportSaveName = "PhieuXuatNgoaiBang" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        break;
                }
                if (reportData != null)
                {
                    try
                    {
                        reportData.Tables[0].TableName = "Table1";
                        bc.Reports.createFileDownload(reportTemplate, reportData, reportSaveName, saveFormat, saveType, Response);
                    }
                    catch (Exception err)
                    {
                        lblError.Text = reportData.Tables[0].TableName + "#" + err.Message;
                    }
                }
            }
            catch (Exception err)
            {
                lblError.Text = err.Message;
            }
        }
        protected void btnReportPhieuChuyenKhoan_Click(object sender, EventArgs e)
        {
            showReport(1);
        }
     
        /*
        * Method Revision History:
        * Version        Date            Author            Comment
        * ----------------------------------------------------------
        * 0.1            Sep 30, 2015    Hien Nguyen       Fix bug 50 _ report phieu xuat ngoai bang
        */
        protected void btnReportPhieuXuatNgoaiBang_Click(object sender, EventArgs e)
        {
            showReport(3);
        }

        /*
        * Method Revision History:
        * Version        Date            Author            Comment
        * ----------------------------------------------------------
        * 0.1            NA
        * 0.2            Sep 30, 2015    Hien Nguyen       Fix bug 46 _ remove Nostro Account field
        */
        protected void cboNostroAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //var row = e.Item.DataItem as DataRowView;
            //if (row == null) return;
            //e.Item.Attributes["Code"] = row["Code"].ToString();
            //e.Item.Attributes["Description"] = row["Description"].ToString();
            //e.Item.Attributes["Account"] = row["AccountNo"].ToString();
        }

        private void loadNostroAccount(){
            //Bind Nostro
            SwiftCodeRepository facade = new SwiftCodeRepository();
            String currency = comboCreditCurrency.SelectedValue;


            cbNostroAccount.Items.Clear();
            cbNostroAccount.DataValueField = "AccountNo";
            cbNostroAccount.DataTextField = "AccountNo";
            cbNostroAccount.DataSource = facade.FindSwiftCodeAssociateWithCurrency(currency).ToList();
            cbNostroAccount.DataBind();
            lblNostro.Text = "";
        }

        protected void comboCountryCode_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblCountryCodeName.Text = comboCountryCode.SelectedValue;
        }
        
    }
}