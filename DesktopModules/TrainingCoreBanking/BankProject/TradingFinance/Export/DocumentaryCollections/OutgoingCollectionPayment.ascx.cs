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

namespace BankProject.TradingFinance.Export.DocumentaryCollections
{
    public partial class OutgoingCollectionPayment : DotNetNuke.Entities.Modules.PortalModuleBase
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
            LoadPayment();
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

        private bool CheckStatusExportDocumentCollection(BEXPORT_DOCUMETARYCOLLECTION expDoc)
        {
            if (expDoc.Status != "AUT" || expDoc.Cancel_Status == "UNA" || (expDoc.Amend_Status != null && expDoc.Amend_Status != "AUT"))
            {
                lblError.Text = "Document was not authorized";
                return false;
            }
            if (expDoc.Cancel_Status == "AUT")
            {
                lblError.Text = "Document was canceled";
                return false;
            }
            if ((expDoc.PaymentFullFlag??0) == 1)
            {
                lblError.Text = "This document was paid full";
                return false;
            }
            return true;
        }

        private string GetNextPaymentCode(string docCode)
        {
            if (_entities.BOUTGOINGCOLLECTIONPAYMENTs.Any(q => q.CollectionPaymentCode == docCode))
            {
                var lst = _entities.BOUTGOINGCOLLECTIONPAYMENTs.Where(q => q.CollectionPaymentCode == docCode).ToList();
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
            if (_entities.BOUTGOINGCOLLECTIONPAYMENTs.Any(q => q.CollectionPaymentCode == docCode && q.Status == "AUT"))
            {
                var closestPayment =
                    _entities.BOUTGOINGCOLLECTIONPAYMENTs.Where(q => q.CollectionPaymentCode == docCode && q.Status == "AUT")
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

            var payment = _entities.BOUTGOINGCOLLECTIONPAYMENTs.FirstOrDefault(q => q.PaymentId == txtCode.Text);
            
            if (payment != null)
            {
                var doc = _entities.BEXPORT_DOCUMETARYCOLLECTION.FirstOrDefault(q => q.DocCollectCode == payment.CollectionPaymentCode);
                var drFromAccount = _entities.BDRFROMACCOUNTs.FirstOrDefault(q => q.Id == comboCreditAcct.SelectedValue);
                if (drFromAccount != null)
                {
                    drFromAccount.Amount = drFromAccount.Amount; /*+ (decimal)(numExchangeRate.Value ?? 0) * (decimal)(numDrawingAmount.Value ?? 0);*/ //fixed bug 66
                }
                if (numDrawingAmount.Value == doc.Amount - double.Parse(lblCreditAmount.Text))
                {
                    doc.PaymentFullFlag = 1;
                }
                payment.Status = "AUT";
                _entities.SaveChanges();
            }
            Response.Redirect(Globals.NavigateURL(TabId));
        }

        protected void Revert()
        {
            var payment = _entities.BOUTGOINGCOLLECTIONPAYMENTs.FirstOrDefault(q => q.PaymentId == txtCode.Text);
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
                _entities.BEXPORT_DOCUMETARYCOLLECTION.FirstOrDefault(q => q.DocCollectCode == CodeId.Substring(0, 14) && (q.ActiveRecordFlag == "YES" || q.ActiveRecordFlag == null));
            if ((doc.PaymentFullFlag ?? 0) == 1)
            {
                lblError.Text = "This document was paid full";
                return false;
            }
            double amount = doc.AmountNew != null ? double.Parse(doc.AmountNew.Value.ToString()) : doc.Amount.Value;
           
            if (numDrawingAmount.Value > amount - double.Parse(lblCreditAmount.Text)) 
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
                       _entities.BOUTGOINGCOLLECTIONPAYMENTs.FirstOrDefault(q => q.PaymentId == txtCode.Text);
            if (outCoPayment == null) // insert
            {
                outCoPayment = new BOUTGOINGCOLLECTIONPAYMENT();
                outCoPayment.Id = Guid.NewGuid();
                outCoPayment.PaymentId = txtCode.Text;
                outCoPayment.PaymentNo = long.Parse(txtCode.Text.Substring(15));
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
                outCoPayment.PresentorCusNo = cbNostroAccount.SelectedValue;
                _entities.BOUTGOINGCOLLECTIONPAYMENTs.Add(outCoPayment);
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
                outCoPayment.PresentorCusNo = cbNostroAccount.SelectedValue;
            }
            _entities.SaveChanges();
            SaveCharges();
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
            var mt910 = _entities.BOUTGOINGCOLLECTIONPAYMENTMT910.FirstOrDefault(q => q.PaymentId == CodeId);
            comboCurrencyMt910.SelectedValue = comboCreditCurrency.SelectedValue;
            if (mt910 != null)
            {
                txtTransactionRefNumber.Text = mt910.TransactionReferenceNumber;
                txtRelatedRef.Text = mt910.RelatedReference;
                txtAccountIndentification.Text = mt910.AccountIndentification;
                dtValueDateMt910.SelectedDate = mt910.ValueDate;
                //fixed bug 46 get value of currency from tab Main
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
            var mt910 = _entities.BOUTGOINGCOLLECTIONPAYMENTMT910.FirstOrDefault(q => q.PaymentId == txtCode.Text);
            if (mt910 == null)
            {
                mt910 = new BOUTGOINGCOLLECTIONPAYMENTMT910();
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
                _entities.BOUTGOINGCOLLECTIONPAYMENTMT910.Add(mt910);
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
                _entities.BOUTGOINGCOLLECTIONPAYMENTCHARGES.FirstOrDefault(
                    q => q.CollectionPaymentCode == txtCode.Text && q.Rowchages == rowCharge);
            if (charge == null)
            {
                charge = new BOUTGOINGCOLLECTIONPAYMENTCHARGE
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
                _entities.BOUTGOINGCOLLECTIONPAYMENTCHARGES.Add(charge);
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
     

        private void SaveCharges()
        {
            if (!string.IsNullOrWhiteSpace(tbChargeAmt.Text))
            {
                UpsertCharge(comboWaiveCharges.SelectedValue, tbChargeCode.SelectedValue, rcbChargeAcct.SelectedValue,rcbChargeCcy.SelectedValue,
                    tbChargeAmt.Text,rcbPartyCharged.SelectedValue,rcbOmortCharge.SelectedValue,rcbChargeStatus.SelectedValue,
                    tbChargeRemarks.Text,tbVatNo.Text,lblTaxCode.Text,lblTaxAmt.Text, "4");
            }
            if (!string.IsNullOrWhiteSpace(tbChargeAmt2.Text))
            {
                UpsertCharge(comboWaiveCharges.SelectedValue, tbChargeCode2.SelectedValue, 
                    rcbChargeAcct2.SelectedValue, rcbChargeCcy2.SelectedValue,
                    tbChargeAmt2.Text, rcbPartyCharged2.SelectedValue, 
                    rcbOmortCharge2.SelectedValue, rcbChargeStatus2.SelectedValue,
                    tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode2.Text, lblTaxAmt2.Text, "2");
            }
            if (!string.IsNullOrWhiteSpace(tbChargeAmt3.Text))
            {
                UpsertCharge(comboWaiveCharges.SelectedValue, tbChargeCode3.SelectedValue,
                    rcbChargeAcct3.SelectedValue, rcbChargeCcy3.SelectedValue,
                    tbChargeAmt3.Text, rcbPartyCharged3.SelectedValue,
                    rcbOmortCharge3.SelectedValue, rcbChargeStatus3.SelectedValue,
                    tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode3.Text, lblTaxAmt3.Text, "3");
            }
            if (!string.IsNullOrWhiteSpace(tbChargeAmt4.Text))
            {
                UpsertCharge(comboWaiveCharges.SelectedValue, tbChargeCode4.SelectedValue,
                    rcbChargeAcct4.SelectedValue, rcbChargeCcy4.SelectedValue,
                    tbChargeAmt4.Text, rcbPartyCharged4.SelectedValue,
                    rcbOmortCharge4.SelectedValue, rcbChargeStatus4.SelectedValue,
                    tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode4.Text, lblTaxAmt4.Text, "1");
            } 

            //Comment code to fix bug 47 start
            //if (!string.IsNullOrWhiteSpace(tbChargeAmt5.Text))
            //{
            //    UpsertCharge(comboWaiveCharges.SelectedValue, tbChargeCode5.SelectedValue,
            //        rcbChargeAcct5.SelectedValue, rcbChargeCcy5.SelectedValue,
            //        tbChargeAmt5.Text, rcbPartyCharged5.SelectedValue,
            //        rcbOmortCharge5.SelectedValue, rcbChargeStatus5.SelectedValue,
            //        tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode5.Text, lblTaxAmt5.Text, "5");
            //}
            //if (!string.IsNullOrWhiteSpace(tbChargeAmt6.Text))
            //{
            //    UpsertCharge(comboWaiveCharges.SelectedValue, tbChargeCode6.SelectedValue,
            //        rcbChargeAcct6.SelectedValue, rcbChargeCcy6.SelectedValue,
            //        tbChargeAmt6.Text, rcbPartyCharged6.SelectedValue,
            //        rcbOmortCharge6.SelectedValue, rcbChargeStatus6.SelectedValue,
            //        tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode6.Text, lblTaxAmt4.Text, "6");
            //}
            //comment code to fix bug 47 ends
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 03, 2015    Hien Nguyen       Fix bug 65 _ remove Payment Method 
         * 0.3            Oct 03, 2015    Hien Nguyen       Fix bug 66 _ remove Exchange Rate 
         */
        void LoadPaymentDetail(BOUTGOINGCOLLECTIONPAYMENT outColPayment)
        {
            var exportDoc = _entities.BEXPORT_DOCUMETARYCOLLECTION.FirstOrDefault(q => q.DocCollectCode == outColPayment.CollectionPaymentCode);
            if (exportDoc.CollectionType.Equals("DP"))
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
            cbNostroAccount.SelectedValue = outColPayment.PresentorCusNo;
            //numExchangeRate.Value = outColPayment.ExchRate; //fixed bug 66
            txtPaymentRemarks1.Text = outColPayment.PaymentRemarks1;
            txtPaymentRemarks2.Text = outColPayment.PaymentRemarks2;
            LoadCharges();
            //bc.Commont.initRadComboBox(ref cboNostroAcct, "Code", "AccountNo", _entities.BSWIFTCODEs.Where(q => q.Currency.Equals(outColPayment.Currency)).ToList());
        }
        private void LoadData(string strtxtCode)
        {
            var outCoPayment =
                        _entities.BOUTGOINGCOLLECTIONPAYMENTs.FirstOrDefault(q => q.PaymentId == strtxtCode);
            if (outCoPayment == null)
            {
                lblError.Text = "Not found payment";
            }
            else
            {
                //txtCode.Text = CodeId;
                var expDocCode = CodeId.Substring(0, 14);
                lblCreditAmount.Text = GetAmountCredited(expDocCode).ToString("#,##0.00");
                var expDoc = _entities.BEXPORT_DOCUMETARYCOLLECTION.FirstOrDefault(q => q.DocCollectCode == expDocCode && (q.ActiveRecordFlag == "YES" || q.ActiveRecordFlag == null));
                if (expDoc == null)
                {
                    lblError.Text = "Document does not exists";
                    return;
                }
                if (CheckStatusExportDocumentCollection(expDoc))
                {
                    LoadExpDoc(expDoc);
                    LoadPaymentDetail(outCoPayment);
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
        private void LoadPayment()
        {
            mainToolbar.FindItemByValue("btReview").Enabled = true;
            if (string.IsNullOrWhiteSpace(CodeId)) return;
            lblCreditAmount.Text = GetAmountCredited(CodeId).ToString("");
            if (CodeId.Length == 14)
            {
                lblCreditAmount.Text = GetAmountCredited(CodeId).ToString("#,##0.00");

                var expDoc = _entities.BEXPORT_DOCUMETARYCOLLECTION.FirstOrDefault(q => q.DocCollectCode == CodeId & (q.ActiveRecordFlag == "YES" || q.ActiveRecordFlag == null));
                if (expDoc == null)
                {
                    lblError.Text = "Document does not exists";
                    return;
                }
                if (CheckStatusExportDocumentCollection(expDoc))
                {
                    var lstDoc = _entities.BOUTGOINGCOLLECTIONPAYMENTs.Where(x => x.CollectionPaymentCode == CodeId).ToList();
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
            else if (CodeId.Length > 14)
            {
                var outCoPayment =
                        _entities.BOUTGOINGCOLLECTIONPAYMENTs.FirstOrDefault(q => q.PaymentId == CodeId);
                if (outCoPayment == null)
                {
                    lblError.Text = "Not found payment";
                }
                else
                {
                    txtCode.Text = CodeId;
                    var expDocCode = CodeId.Substring(0, 14);
                    lblCreditAmount.Text = GetAmountCredited(expDocCode).ToString("#,##0.00");
                    var expDoc = _entities.BEXPORT_DOCUMETARYCOLLECTION.FirstOrDefault(q => q.DocCollectCode == expDocCode & (q.ActiveRecordFlag == "YES" || q.ActiveRecordFlag == null));
                    if (expDoc == null)
                    {
                        lblError.Text = "Document does not exists";
                        return;
                    }
                    if (CheckStatusExportDocumentCollection(expDoc))
                    {
                        LoadExpDoc(expDoc);
                        LoadPaymentDetail(outCoPayment);
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

        private void LoadCharges()
        {
            var lstCharges =
                _entities.BOUTGOINGCOLLECTIONPAYMENTCHARGES.Where(q => q.CollectionPaymentCode == txtCode.Text);
            #region tab Charge

            

            if (lstCharges.Any(q=>q.Rowchages == "4"))
            {
                var charge = lstCharges.FirstOrDefault(q => q.Rowchages == "4");

                comboWaiveCharges.SelectedValue = charge.WaiveCharges;
                rcbChargeAcct.SelectedValue = charge.ChargeAcct;

                //tbChargePeriod.Text = drow1["ChargePeriod"].ToString();
                rcbChargeCcy.SelectedValue = charge.ChargeCcy;
                if (!string.IsNullOrEmpty(rcbChargeCcy.SelectedValue))
                {
                    LoadChargeAcct();
                }

                //tbExcheRate.Text = drow1["ExchRate"].ToString();
                tbChargeAmt.Text = charge.ChargeAmt;
                rcbPartyCharged.SelectedValue = charge.PartyCharged;
                //lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];
                rcbOmortCharge.SelectedValue = charge.OmortCharges;
                rcbChargeStatus.SelectedValue = charge.ChargeStatus;
                lblChargeStatus.Text = charge.ChargeStatus;

                tbChargeRemarks.Text = charge.ChargeRemarks;
                //tbVatNo.Text = charge.VATNo; //VAT No is harded code
                lblTaxCode.Text = charge.TaxCode;
                //lblTaxCcy.Text = drow1["TaxCcy"].ToString();
                lblTaxAmt.Text = charge.TaxAmt;

                tbChargeCode.SelectedValue = charge.Chargecode;
            }
            else
            {
                comboWaiveCharges.SelectedValue = "NO";
                rcbChargeAcct.SelectedValue = string.Empty;
                //tbChargePeriod.Text = "1";
                rcbChargeCcy.SelectedValue = string.Empty;
                //tbExcheRate.Text = string.Empty;
                tbChargeAmt.Text = string.Empty;
                rcbPartyCharged.SelectedValue = "A";
                
                //lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];
                rcbOmortCharge.SelectedValue = string.Empty;
                rcbChargeStatus.SelectedValue = string.Empty;
                lblChargeStatus.Text = string.Empty;

                tbChargeRemarks.Text = string.Empty;
                tbVatNo.Text = "154";
                lblTaxCode.Text = string.Empty;
                //lblTaxCcy.Text = string.Empty;
                lblTaxAmt.Text = string.Empty;

                //tbChargeCode.SelectedValue = string.Empty;

                //lblChargeAcct.Text = string.Empty;
                //lblPartyCharged.Text = string.Empty;
                lblChargeStatus.Text = string.Empty;
            }

            if (lstCharges.Any(q => q.Rowchages == "2"))
            {
                var charge = lstCharges.FirstOrDefault(q => q.Rowchages == "2");

                //divChargeInfo2.Visible = true;

                rcbChargeAcct2.SelectedValue = charge.ChargeAcct;

                rcbChargeCcy2.SelectedValue = charge.ChargeCcy;
                if (!string.IsNullOrEmpty(rcbChargeCcy2.SelectedValue))
                {
                    LoadChargeAcct2();
                }

                tbChargeAmt2.Text = charge.ChargeAmt;
                rcbPartyCharged2.SelectedValue = charge.PartyCharged;
                //lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];
                rcbChargeStatus2.SelectedValue = charge.ChargeStatus;
                lblChargeStatus2.Text = charge.ChargeStatus;

                lblTaxCode2.Text = charge.TaxCode;
                lblTaxAmt2.Text = charge.TaxAmt;

                tbChargeCode2.SelectedValue = charge.Chargecode;
            }
            else
            {
                rcbChargeAcct2.SelectedValue = string.Empty;
                rcbChargeCcy2.SelectedValue = string.Empty;
                tbChargeAmt2.Text = string.Empty;
                rcbPartyCharged2.SelectedValue = "A";
                //lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];
                rcbChargeStatus2.SelectedValue = string.Empty;
                lblChargeStatus2.Text = string.Empty;

                lblTaxCode2.Text = string.Empty;
                lblTaxAmt2.Text = string.Empty;

                //tbChargeCode2.SelectedValue = string.Empty;

                //lblChargeAcct2.Text = string.Empty;
                //lblPartyCharged2.Text = string.Empty;
                lblChargeStatus2.Text = string.Empty;
            }
            if (lstCharges.Any(q => q.Rowchages == "3"))
            {
                var charge = lstCharges.FirstOrDefault(q => q.Rowchages == "3");

                //divChargeInfo3.Visible = true;

                rcbChargeAcct3.SelectedValue = charge.ChargeAcct;

                rcbChargeCcy3.SelectedValue = charge.ChargeCcy;
                if (!string.IsNullOrEmpty(rcbChargeCcy3.SelectedValue))
                {
                    LoadChargeAcct3();
                }

                tbChargeAmt3.Text = charge.ChargeAmt;
                rcbPartyCharged3.SelectedValue = charge.PartyCharged;
                //lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];
                rcbChargeStatus3.SelectedValue = charge.ChargeStatus;
                //lblChargeStatus3.Text = charge.ChargeStatus;

                lblTaxCode3.Text = charge.TaxCode;
                lblTaxAmt3.Text = charge.TaxAmt;

                tbChargeCode3.SelectedValue = charge.Chargecode;
            }
            else
            {
                rcbChargeAcct3.SelectedValue = string.Empty;
                rcbChargeCcy3.SelectedValue = string.Empty;
                tbChargeAmt3.Text = string.Empty;
                rcbPartyCharged3.SelectedValue = "A";
                //lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];
                rcbChargeStatus3.SelectedValue = string.Empty;
                //lblChargeStatus3.Text = string.Empty;

                lblTaxCode3.Text = string.Empty;
                lblTaxAmt3.Text = string.Empty;

                //tbChargeCode3.SelectedValue = string.Empty;

                //lblChargeAcct3.Text = string.Empty;
                //lblPartyCharged3.Text = string.Empty;
                //lblChargeStatus3.Text = string.Empty;
            }
            if (lstCharges.Any(q => q.Rowchages == "1")) //1 is "Other Tab"
            {
                var charge = lstCharges.FirstOrDefault(q => q.Rowchages == "1");

                //divChargeInfo4.Visible = true;

                rcbChargeAcct4.SelectedValue = charge.ChargeAcct;

                rcbChargeCcy4.SelectedValue = charge.ChargeCcy;
                if (!string.IsNullOrEmpty(rcbChargeCcy4.SelectedValue))
                {
                    LoadChargeAcct4();
                }

                tbChargeAmt4.Text = charge.ChargeAmt;
                rcbPartyCharged4.SelectedValue = charge.PartyCharged;
                //lblPartyCharged4.Text = rcbPartyCharged4.SelectedItem.Attributes["Description"];
                rcbChargeStatus4.SelectedValue = charge.ChargeStatus;
                //lblChargeStatus4.Text = charge.ChargeStatus;

                lblTaxCode4.Text = charge.TaxCode;
                lblTaxAmt4.Text = charge.TaxAmt;

                tbChargeCode4.SelectedValue = charge.Chargecode;
            }
            else
            {
                rcbChargeAcct4.SelectedValue = string.Empty;
                rcbChargeCcy4.SelectedValue = string.Empty;
                tbChargeAmt4.Text = string.Empty;
                rcbPartyCharged4.SelectedValue = "A";
                //lblPartyCharged4.Text = rcbPartyCharged4.SelectedItem.Attributes["Description"];
                rcbChargeStatus4.SelectedValue = string.Empty;
                //lblChargeStatus4.Text = string.Empty;

                lblTaxCode4.Text = string.Empty;
                lblTaxAmt4.Text = string.Empty;

                //tbChargeCode4.SelectedValue = string.Empty;

                //lblChargeAcct4.Text = string.Empty;
                //lblPartyCharged4.Text = string.Empty;
                //lblChargeStatus4.Text = string.Empty;
            }
            //comment code to fix bug 47 start
            /*
            if (lstCharges.Any(q => q.Rowchages == "5"))
            {
                var charge = lstCharges.FirstOrDefault(q => q.Rowchages == "5");

                //divChargeInfo4.Visible = true;

                rcbChargeAcct5.SelectedValue = charge.ChargeAcct;

                rcbChargeCcy5.SelectedValue = charge.ChargeCcy;
                if (!string.IsNullOrEmpty(rcbChargeCcy5.SelectedValue))
                {
                    LoadChargeAcct5();
                }

                tbChargeAmt5.Text = charge.ChargeAmt;
                rcbPartyCharged5.SelectedValue = charge.PartyCharged;
                lblPartyCharged5.Text = rcbPartyCharged5.SelectedItem.Attributes["Description"];
                rcbChargeStatus5.SelectedValue = charge.ChargeStatus;
                //lblChargeStatus4.Text = charge.ChargeStatus;

                lblTaxCode5.Text = charge.TaxCode;
                lblTaxAmt5.Text = charge.TaxAmt;

                tbChargeCode5.SelectedValue = charge.Chargecode;
            }
            else
            {
                rcbChargeAcct5.SelectedValue = string.Empty;
                rcbChargeCcy5.SelectedValue = string.Empty;
                tbChargeAmt5.Text = string.Empty;
                rcbPartyCharged5.SelectedValue = "BC";
                lblPartyCharged5.Text = rcbPartyCharged5.SelectedItem.Attributes["Description"];
                rcbChargeStatus5.SelectedValue = string.Empty;
                //lblChargeStatus5.Text = string.Empty;

                lblTaxCode5.Text = string.Empty;
                lblTaxAmt5.Text = string.Empty;

                //tbChargeCode5.SelectedValue = string.Empty;

                //lblChargeAcct5.Text = string.Empty;
                lblPartyCharged5.Text = string.Empty;
                //lblChargeStatus5.Text = string.Empty;
            }

            if (lstCharges.Any(q => q.Rowchages == "6"))
            {
                var charge = lstCharges.FirstOrDefault(q => q.Rowchages == "6");

                //divChargeInfo6.Visible = true;

                rcbChargeAcct6.SelectedValue = charge.ChargeAcct;

                rcbChargeCcy6.SelectedValue = charge.ChargeCcy;
                if (!string.IsNullOrEmpty(rcbChargeCcy6.SelectedValue))
                {
                    LoadChargeAcct6();
                }

                tbChargeAmt6.Text = charge.ChargeAmt;
                rcbPartyCharged6.SelectedValue = charge.PartyCharged;
                lblPartyCharged6.Text = rcbPartyCharged6.SelectedItem.Attributes["Description"];
                rcbChargeStatus6.SelectedValue = charge.ChargeStatus;
                //lblChargeStatus6.Text = charge.ChargeStatus;

                lblTaxCode6.Text = charge.TaxCode;
                lblTaxAmt6.Text = charge.TaxAmt;

                tbChargeCode6.SelectedValue = charge.Chargecode;
            }
            else
            {
                rcbChargeAcct6.SelectedValue = string.Empty;
                rcbChargeCcy6.SelectedValue = string.Empty;
                tbChargeAmt6.Text = string.Empty;
                rcbPartyCharged6.SelectedValue = "BB";
                lblPartyCharged6.Text = rcbPartyCharged6.SelectedItem.Attributes["Description"];
                rcbChargeStatus6.SelectedValue = string.Empty;
                //lblChargeStatus6.Text = string.Empty;

                lblTaxCode6.Text = string.Empty;
                lblTaxAmt6.Text = string.Empty;

                //tbChargeCode6.SelectedValue = string.Empty;

                //lblChargeAcct6.Text = string.Empty;
                lblPartyCharged6.Text = string.Empty;
                //lblChargeStatus6.Text = string.Empty;
            }
            */
            //comment code to fix bug 47 end

            #endregion
        }
        protected void LoadExpDoc(BEXPORT_DOCUMETARYCOLLECTION expDoc)
        {
            
            if (expDoc != null)
            {
                //mainToolbar.FindItemByValue("btReview").Enabled = false;

                //var drow = dsDoc.Tables[0].Rows[0];

                #region Load Export Collection
                comboCollectionType.SelectedValue = expDoc.CollectionType;
                lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Attributes["Description"];

                comboDrawerCusNo.SelectedValue = expDoc.DrawerCusNo;
                txtDrawerCusName.Text = expDoc.DrawerCusName;
                txtDrawerAddr1.Text = expDoc.DrawerAddr1;
                txtDrawerAddr2.Text = expDoc.DrawerAddr2;
                txtDrawerAddr3.Text = expDoc.DrawerAddr3;
                txtDrawerRefNo.Text = expDoc.DrawerRefNo;
                comboCollectingBankNo.SelectedValue = expDoc.CollectingBankNo;
                txtCollectingBankName.Text = expDoc.CollectingBankName;
                txtCollectingBankAddr1.Text = expDoc.CollectingBankAddr1;
                txtCollectingBankAddr2.Text = expDoc.CollectingBankAddr2;
                comboCollectingBankAcct.SelectedValue = expDoc.CollectingBankAcct;
                txtDraweeCusNo.Text = expDoc.DraweeCusNo;
                txtDraweeCusName.Text = expDoc.DraweeCusName;
                txtDraweeAddr1.Text = expDoc.DraweeAddr1;
                txtDraweeAddr2.Text = expDoc.DraweeAddr2;
                txtDraweeAddr3.Text = expDoc.DraweeAddr3;
                comboNostroCusNo.SelectedValue = expDoc.NostroCusNo;
                lblNostroCusName.Text = comboNostroCusNo.SelectedItem.Attributes["Description"];
                comboCreditCurrency.SelectedValue = expDoc.Currency;
                comboCurrency.SelectedValue = expDoc.Currency;
                loadNostroAccount();
                LoadCreditAccount();
                numAmount.Value = expDoc.Amount;
                txtTenor.Text = expDoc.Tenor;
                numReminderDays.Text = (expDoc.ReminderDays??0).ToString();

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

                txtOtherDocs.Text = expDoc.OtherDocs;
                txtRemarks.Text = expDoc.Remarks;

                if (expDoc.DocsReceivedDate.HasValue && expDoc.DocsReceivedDate.Value != new DateTime(1900,1,1))
                {
                    dteDocsReceivedDate.SelectedDate = expDoc.DocsReceivedDate.Value;
                }
                if (expDoc.MaturityDate.HasValue && expDoc.MaturityDate.Value != new DateTime(1900, 1, 1))
                {
                    dteMaturityDate.SelectedDate = expDoc.MaturityDate.Value;
                }
                if (expDoc.TracerDate.HasValue && expDoc.TracerDate.Value != new DateTime(1900, 1, 1))
                {
                    dteTracerDate.SelectedDate = expDoc.TracerDate.Value;
                }

                comboCurrencyMt910.SelectedValue = comboCreditCurrency.SelectedValue;
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
        protected void LoadDataSourceComboPartyCharged()
        {
            var dtSource = bd.SQLData.CreateGenerateDatas("PartyCharged");

            rcbPartyCharged.Items.Clear();
            rcbPartyCharged.DataValueField = "Id";
            rcbPartyCharged.DataTextField = "Id";
            rcbPartyCharged.DataSource = dtSource;
            rcbPartyCharged.DataBind();
            rcbPartyCharged.SelectedValue = "A";
            //lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];

            rcbPartyCharged2.Items.Clear();
            rcbPartyCharged2.DataValueField = "Id";
            rcbPartyCharged2.DataTextField = "Id";
            rcbPartyCharged2.DataSource = dtSource;
            rcbPartyCharged2.DataBind();
            rcbPartyCharged2.SelectedValue = "A";
            //lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];

            rcbPartyCharged3.Items.Clear();
            rcbPartyCharged3.DataValueField = "Id";
            rcbPartyCharged3.DataTextField = "Id";
            rcbPartyCharged3.DataSource = dtSource;
            rcbPartyCharged3.DataBind();
            rcbPartyCharged3.SelectedValue = "A";
            //lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];

            rcbPartyCharged4.Items.Clear();
            rcbPartyCharged4.DataValueField = "Id";
            rcbPartyCharged4.DataTextField = "Id";
            rcbPartyCharged4.DataSource = dtSource;
            rcbPartyCharged4.DataBind();
            rcbPartyCharged4.SelectedValue = "A";
            //lblPartyCharged4.Text = rcbPartyCharged4.SelectedItem.Attributes["Description"];

            //comment code to fix bug 47 start
            /*
            rcbPartyCharged5.Items.Clear();
            rcbPartyCharged5.DataValueField = "Id";
            rcbPartyCharged5.DataTextField = "Id";
            rcbPartyCharged5.DataSource = dtSource;
            rcbPartyCharged5.DataBind();
            rcbPartyCharged5.SelectedValue = "BC";
            lblPartyCharged5.Text = rcbPartyCharged5.SelectedItem.Attributes["Description"];


            rcbPartyCharged6.Items.Clear();
            rcbPartyCharged6.DataValueField = "Id";
            rcbPartyCharged6.DataTextField = "Id";
            rcbPartyCharged6.DataSource = dtSource;
            rcbPartyCharged6.DataBind();
            rcbPartyCharged6.SelectedValue = "BB";
            lblPartyCharged6.Text = rcbPartyCharged6.SelectedItem.Attributes["Description"];
            */
            //comment code to fix bug 47 ends
        }
        protected void LoadChargeCode()
        {
            var datasource = _entities.BCHARGECODEs.Where(q => q.PaymentEC == "x").ToList();

            tbChargeCode.Items.Clear();
            tbChargeCode.Items.Add(new RadComboBoxItem(""));
            tbChargeCode.DataValueField = "Code";
            tbChargeCode.DataTextField = "Code";
            tbChargeCode.DataSource = datasource;
            tbChargeCode.DataBind();

            tbChargeCode2.Items.Clear();
            tbChargeCode2.Items.Add(new RadComboBoxItem(""));
            tbChargeCode2.DataValueField = "Code";
            tbChargeCode2.DataTextField = "Code";
            tbChargeCode2.DataSource = datasource;
            tbChargeCode2.DataBind();

            tbChargeCode3.Items.Clear();
            tbChargeCode3.Items.Add(new RadComboBoxItem(""));
            tbChargeCode3.DataValueField = "Code";
            tbChargeCode3.DataTextField = "Code";
            tbChargeCode3.DataSource = datasource;
            tbChargeCode3.DataBind();

            tbChargeCode4.Items.Clear();
            tbChargeCode4.Items.Add(new RadComboBoxItem(""));
            tbChargeCode4.DataValueField = "Code";
            tbChargeCode4.DataTextField = "Code";
            tbChargeCode4.DataSource = datasource;
            tbChargeCode4.DataBind();

            //comment code to fix bug 47 start
            /*
            tbChargeCode5.Items.Clear();
            tbChargeCode5.Items.Add(new RadComboBoxItem(""));
            tbChargeCode5.DataValueField = "Code";
            tbChargeCode5.DataTextField = "Code";
            tbChargeCode5.DataSource = datasource;
            tbChargeCode5.DataBind();

            tbChargeCode6.Items.Clear();
            tbChargeCode6.Items.Add(new RadComboBoxItem(""));
            tbChargeCode6.DataValueField = "Code";
            tbChargeCode6.DataTextField = "Code";
            tbChargeCode6.DataSource = datasource;
            tbChargeCode6.DataBind();
             */
            //comment code to fix bug 47 end
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

            LoadDataSourceComboPartyCharged();
            LoadChargeCode();
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
            comboCollectionType.Items.Clear();
            comboCollectionType.DataValueField = "ID";
            comboCollectionType.DataTextField = "ID";
            comboCollectionType.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_CollectionType");
            comboCollectionType.DataBind();
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

            tbChargeCode.SelectedValue = "EC.PAYMENT";
            tbChargeCode.Enabled = false;
            tbChargeCode2.SelectedValue = "EC.COURIER";
            tbChargeCode2.Enabled = false;
            tbChargeCode3.SelectedValue = "EC.HANDLING";
            tbChargeCode3.Enabled = false;
            tbChargeCode4.SelectedValue = "EC.OTHER";
            tbChargeCode4.Enabled = false;

            //comment code to fix bug 47
            //tbChargeCode5.SelectedValue = "EC.OVERSEASPLUS";
            //tbChargeCode5.Enabled = false;
            //tbChargeCode6.SelectedValue = "EC.OVERSEASMINUS";
            //tbChargeCode6.Enabled = false;

            rcbPartyCharged.Enabled = false;
            rcbPartyCharged2.Enabled = false;
            rcbPartyCharged3.Enabled = false;
            rcbPartyCharged4.Enabled = false;

            //coment code to fix bug 47
            //rcbPartyCharged5.Enabled = false;
            //rcbPartyCharged6.Enabled = false;

            //rcbChargeAcct5.Enabled = false;
            //rcbChargeAcct6.Enabled = false;

            var curList = _entities.BCURRENCies.ToList();
            bc.Commont.initRadComboBox(ref comboCreditCurrency, "Code", "Code", curList);
            bc.Commont.initRadComboBox(ref comboCurrency, "Code", "Code", curList);
            bc.Commont.initRadComboBox(ref comboCurrencyMt910, "Code", "Code", curList);

            bc.Commont.initRadComboBox(ref rcbChargeCcy, "Code", "Code", curList);
            bc.Commont.initRadComboBox(ref rcbChargeCcy2, "Code", "Code", curList);
            bc.Commont.initRadComboBox(ref rcbChargeCcy3, "Code", "Code", curList);
            bc.Commont.initRadComboBox(ref rcbChargeCcy4, "Code", "Code", curList);

            //remove "GOLD" from currency List
            bc.Commont.removeCurrencyItem(comboCreditCurrency, "GOLD");
            bc.Commont.removeCurrencyItem(comboCurrency, "GOLD");
            bc.Commont.removeCurrencyItem(comboCurrencyMt910, "GOLD");
            bc.Commont.removeCurrencyItem(rcbChargeCcy, "GOLD");
            bc.Commont.removeCurrencyItem(rcbChargeCcy2, "GOLD");
            bc.Commont.removeCurrencyItem(rcbChargeCcy3, "GOLD");
            bc.Commont.removeCurrencyItem(rcbChargeCcy4, "GOLD");

            tbVatNo.Text = "154"; //harded code fixed VAT no

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

        protected void comboWaiveCharges_OnSelectedIndexChanged(object sender,
                                                                RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (comboWaiveCharges.SelectedValue == "NO")
            {
                divReceiveCharge.Visible = true;
                divCourierCharge.Visible = true;
                divOtherCharge.Visible = true;
                divPaymentCharge.Visible = true;
            }
            else if (comboWaiveCharges.SelectedValue == "YES")
            {
                divReceiveCharge.Visible = true;
                divCourierCharge.Visible = true;
                divOtherCharge.Visible = true;
                divPaymentCharge.Visible = true;
            }
        }
        protected void rcbPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged.Text = rcbPartyCharged.SelectedValue;
            //lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];
            CalcTax();
        }

        protected void rcbChargeStatus_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus.Text = rcbChargeStatus.SelectedValue;
        }

        protected void rcbChargeAcct2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
             //lblPartyCharged2.Text = rcbPartyCharged2.SelectedValue;

        }

        protected void rcbPartyCharged2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];
            CalcTax2();
        }
        protected void rcbPartyCharged3_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];
            CalcTax3();
        }
        protected void rcbPartyCharged4_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged4.Text = rcbPartyCharged4.SelectedItem.Attributes["Description"];
            CalcTax4();
        }

        //coment code to fix bug 47 start
        /*
        protected void rcbPartyCharged5_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged5.Text = rcbPartyCharged5.SelectedItem.Attributes["Description"];
            CalcTax5();
        }
        protected void rcbPartyCharged6_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged5.Text = rcbPartyCharged6.SelectedItem.Attributes["Description"];
            CalcTax6();
        }
         * */
        //coment code to fix bug 47 end

        protected void rcbChargeStatus2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus2.Text = rcbChargeStatus2.SelectedValue.ToString();
        }
        protected void rcbChargeCcy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct();
        }

        protected void rcbChargeCcy2_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct2();
        }

        protected void rcbChargeCcy3_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct3();
        }

        protected void rcbChargeCcy4_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct4();
        }

        protected void rcbChargeCcy5_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //LoadChargeAcct5();
        }

        protected void rcbChargeCcy6_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //LoadChargeAcct6();
        }

        protected void rcbChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void rcbChargeAcct2_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }
        protected void rcbChargeAcct3_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }
        protected void rcbChargeAcct4_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void rcbChargeAcct5_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void rcbChargeAcct6_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void tbChargeAmt_TextChanged(object sender, EventArgs e)
        {
            CalcTax();
        }

        protected void tbChargeAmt2_TextChanged(object sender, EventArgs e)
        {
            CalcTax2();
        }
        protected void tbChargeAmt3_TextChanged(object sender, EventArgs e)
        {
            CalcTax3();
        }
        protected void tbChargeAmt4_TextChanged(object sender, EventArgs e)
        {
            CalcTax4();
        }
        
        //comment code to fix bug 47 start
        /*
        protected void tbChargeAmt5_TextChanged(object sender, EventArgs e)
        {
            CalcTax5();
        }
        protected void tbChargeAmt6_TextChanged(object sender, EventArgs e)
        {
            CalcTax6();
        }
        */
        //comment code to fix bug 47 end
 
        protected void rcbPartyCharged_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void rcbPartyCharged2_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void rcbPartyCharged3_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void rcbPartyCharged4_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }

        protected void rcbPartyCharged5_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }

        protected void rcbPartyCharged6_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void CalcTax()
        {
            double sotien = 0;
            if (rcbPartyCharged.SelectedValue != "AC" && tbChargeAmt.Value > 0)
            {
                sotien = double.Parse(tbChargeAmt.Value.ToString());
                sotien = sotien * 0.1;
                lblTaxAmt.Text = String.Format("{0:C}", sotien).Replace("$", "");
                lblTaxCode.Text = "81      10% VAT on Charge";
            }
            else
            {
                lblTaxAmt.Text = "";
                lblTaxCode.Text = "";
            }
        }

        protected void CalcTax2()
        {
            double sotien = 0;
            if (rcbPartyCharged2.SelectedValue != "AC" && tbChargeAmt2.Value > 0)
            {
                sotien = double.Parse(tbChargeAmt2.Value.ToString());
                sotien = sotien * 0.1;
                lblTaxAmt2.Text = String.Format("{0:C}", sotien).Replace("$", "");
                lblTaxCode2.Text = "81      10% VAT on Charge";
            }
            else
            {
                lblTaxAmt2.Text = "";
                lblTaxCode2.Text = "";
            }
        }

        protected void CalcTax3()
        {

            double sotien = 0;
            if (rcbPartyCharged3.SelectedValue != "AC" && tbChargeAmt3.Value > 0)
            {
                sotien = Double.Parse(tbChargeAmt3.Value.ToString());
                sotien = sotien * 0.1;
                lblTaxAmt3.Text = String.Format("{0:C}", sotien).Replace("$", "");
                lblTaxCode3.Text = "81      10% VAT on Charge";
            }
            else
            {
                lblTaxAmt3.Text = "";
                lblTaxCode3.Text = "";
            }
        }
        protected void CalcTax4()
        {

            double sotien = 0;
            if (rcbPartyCharged4.SelectedValue != "AC" && tbChargeAmt4.Value > 0)
            {
                sotien = Double.Parse(tbChargeAmt4.Value.ToString());
                sotien = sotien * 0.1;
                lblTaxAmt4.Text = String.Format("{0:C}", sotien).Replace("$", "");
                lblTaxCode4.Text = "81      10% VAT on Charge";
            }
            else
            {
                lblTaxAmt4.Text = "";
                lblTaxCode4.Text = "";
            }
        }

        //comment code to fix bug 47 start
        /*
        protected void CalcTax5()
        {

            double sotien = 0;
            if (rcbPartyCharged5.SelectedValue != "AC" && tbChargeAmt5.Value > 0)
            {
                sotien = Double.Parse(tbChargeAmt5.Value.ToString());
                sotien = sotien * 0.1;
                lblTaxAmt5.Text = String.Format("{0:C}", sotien).Replace("$", "");
                lblTaxCode5.Text = "81      10% VAT on Charge";
            }
            else
            {
                lblTaxAmt5.Text = "";
                lblTaxCode5.Text = "";
            }
        }
        protected void CalcTax6()
        {

            double sotien = 0;
            if (rcbPartyCharged6.SelectedValue != "AC" && tbChargeAmt6.Value > 0)
            {
                sotien = Double.Parse(tbChargeAmt6.Value.ToString());
                sotien = sotien * 0.1;
                lblTaxAmt6.Text = String.Format("{0:C}", sotien).Replace("$", "");
                lblTaxCode6.Text = "81      10% VAT on Charge";
            }
            else
            {
                lblTaxAmt6.Text = "";
                lblTaxCode6.Text = "";
            }
        }
        */
        //comment code to fix bug 47 end

        protected void LoadChargeAcct()
        {
            rcbChargeAcct.Items.Clear();
            rcbChargeAcct.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct.DataValueField = "Id";
            rcbChargeAcct.DataTextField = "Id";
            rcbChargeAcct.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(comboDrawerCusNo.SelectedItem != null ? comboDrawerCusNo.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy.SelectedValue);
            rcbChargeAcct.DataBind();
        }

        protected void LoadChargeAcct2()
        {
            rcbChargeAcct2.Items.Clear();
            rcbChargeAcct2.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct2.DataValueField = "Id";
            rcbChargeAcct2.DataTextField = "Id";
            rcbChargeAcct2.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(comboDrawerCusNo.SelectedItem != null ? comboDrawerCusNo.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy2.SelectedValue);
            rcbChargeAcct2.DataBind();
        }

        protected void LoadChargeAcct3()
        {
            rcbChargeAcct3.Items.Clear();
            rcbChargeAcct3.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct3.DataValueField = "Id";
            rcbChargeAcct3.DataTextField = "Id";
            rcbChargeAcct3.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(comboDrawerCusNo.SelectedItem != null ? comboDrawerCusNo.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy3.SelectedValue);
            rcbChargeAcct3.DataBind();
        }
        protected void LoadChargeAcct4()
        {
            rcbChargeAcct4.Items.Clear();
            rcbChargeAcct4.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct4.DataValueField = "Id";
            rcbChargeAcct4.DataTextField = "Id";
            rcbChargeAcct4.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(comboDrawerCusNo.SelectedItem != null ? comboDrawerCusNo.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy4.SelectedValue);
            rcbChargeAcct4.DataBind();
        }

        //Comment code to fix bug 47 start
        /*
        protected void LoadChargeAcct5()
        {
            rcbChargeAcct5.Items.Clear();
            rcbChargeAcct5.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct5.DataValueField = "Id";
            rcbChargeAcct5.DataTextField = "Id";
            rcbChargeAcct5.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(comboDrawerCusNo.SelectedItem != null ? comboDrawerCusNo.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy5.SelectedValue);
            rcbChargeAcct5.DataBind();
        }

        protected void LoadChargeAcct6()
        {
            rcbChargeAcct6.Items.Clear();
            rcbChargeAcct6.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct6.DataValueField = "Id";
            rcbChargeAcct6.DataTextField = "Id";
            rcbChargeAcct6.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(comboDrawerCusNo.SelectedItem != null ? comboDrawerCusNo.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy6.SelectedValue);
            rcbChargeAcct6.DataBind();
        }
        */
        //comment code to fix bug 47 end

        protected void GenerateVatNo()
        {
            var vatno = bd.Database.B_BMACODE_GetNewSoTT("VATNO");
            tbVatNo.Text = vatno.Tables[0].Rows[0]["SoTT"].ToString();
            tbVatNo.Text = "154"; //hard code fixed VAT no
        }
        protected void comboDrawerCusNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as BCUSTOMER;
            e.Item.Attributes["CustomerID"] = row.CustomerID;
            e.Item.Attributes["CustomerName2"] = row.CustomerName;
        }

        private void showReport(int reportType)
        {
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Export/";
            string reportSaveName = "";
            DataSet reportData = null;
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {
                reportData = bd.IssueLC.ExportLCPaymentReport(reportType, txtCode.Text, this.UserInfo.Username);
                switch (reportType)
                {
                    case 1://PhieuChuyenKhoan
                        reportTemplate = Context.Server.MapPath(reportTemplate + "PaymentPhieuChuyenKhoan.doc");
                        reportSaveName = "PhieuChuyenKhoan" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        break;
                    case 2://VAT B
                        reportTemplate = Context.Server.MapPath(reportTemplate + "PaymentVAT.doc");
                        reportSaveName = "VAT" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        break;
                    case 3://Phieu xaut ngoai bang
                        reportTemplate = Context.Server.MapPath(reportTemplate + "PaymentPHIEUXUATNGOAIBANG.doc");
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
        protected void btnReportVATb_Click(object sender, EventArgs e)
        {
            showReport(2);
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