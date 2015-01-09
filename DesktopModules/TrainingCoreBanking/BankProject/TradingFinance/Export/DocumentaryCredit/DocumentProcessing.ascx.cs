using System;
using System.Data;
using System.Web.UI;
using DotNetNuke.Common;
using Telerik.Web.UI;
using BankProject.DBContext;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using BankProject.DataProvider;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;
using BankProject.Helper;
using BankProject.Model;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Data.Entity.Validation;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{//B_ExportDocCreditProcessing
    public partial class DocumentProcessing : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private ExportLCDocProcessing dbEntities = new ExportLCDocProcessing();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtChargeCode1.Text = (TabId == ExportLCDocProcessing.Actions.Amend ? ExportLCDocProcessing.Charges.Service : ExportLCDocProcessing.Charges.Commission);
            txtChargeCode2.Text = ExportLCDocProcessing.Charges.Courier;
            txtChargeCode3.Text = ExportLCDocProcessing.Charges.Other;
            if (IsPostBack) return;
            setDefaultControls();
            //
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                //Code có dạng TFxxx hoặc TFxxx.No hoặc TFxxx.No.AmendNo
                tbLCCode.Text = Request.QueryString["code"].Trim();
                var ExLCDoc = dbEntities.findExportLCDoc(tbLCCode.Text);
                int i = tbLCCode.Text.IndexOf(".");
                if (i < 0)//TFxxx
                {
                    if (TabId != ExportLCDocProcessing.Actions.Register && TabId != ExportLCDocProcessing.Actions.Register1)
                    {
                        lblLCCodeMessage.Text = "Invalid Code !";
                        return;
                    }
                    var ExLC = dbEntities.findExportLC(tbLCCode.Text);
                    if (ExLC == null)
                    {
                        lblLCCodeMessage.Text = "Can not find this LC Code !";
                        return;
                    }
                    if (!ExLC.Status.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "This LC Code not authorized !";
                        return;
                    }
                    /*if (!string.IsNullOrEmpty(ExLC.AmendStatus) && ExLC.AmendStatus.Equals(bd.TransactionStatus.UNA))
                    {
                        lblLCCodeMessage.Text = "This LC Code is under amend !";
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.CancelStatus) && !ExLC.CancelStatus.Equals(bd.TransactionStatus.REV))
                    {
                        lblLCCodeMessage.Text = "This LC Code is canceled !";
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.ClosedStatus) && !ExLC.ClosedStatus.Equals(bd.TransactionStatus.REV))
                    {
                        lblLCCodeMessage.Text = "This LC Code is closed !";
                        return;
                    }
                    if (ExLC.PaymentFull.HasValue && ExLC.PaymentFull.Value)
                    {
                        lblLCCodeMessage.Text = "This LC Code is PaymentFull !";
                        return;
                    }*/
                    if (ExLCDoc == null)
                        tbLCCode.Text += ".1";
                    else
                    {
                        string[] s = ExLCDoc.DocCode.Split('.');
                        tbLCCode.Text += "." + (Convert.ToInt32(s[1]) + 1);
                    }
                    loadLC(ExLC);
                    RadToolBar1.FindItemByValue("btCommit").Enabled = true;
                    return;
                }
                //                
                i = tbLCCode.Text.IndexOf(".", i + 1);
                if (i > 0)//AmendNo
                {
                    if (TabId != ExportLCDocProcessing.Actions.Amend)
                    {
                        lblLCCodeMessage.Text = "Invalid AmendNo !";
                        return;
                    }
                    loadLCDoc(ExLCDoc);
                    if (!string.IsNullOrEmpty(ExLCDoc.AmendStatus) && ExLCDoc.AmendStatus.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "This doc already authorized !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]) && Request.QueryString["lst"].Equals("4appr"))
                    {
                        if (ExLCDoc.AmendStatus.Equals(bd.TransactionStatus.UNA))
                        {
                            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                            RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                            RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                            return;
                        }
                        lblLCCodeMessage.Text = "This doc already reversed !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    RadToolBar1.FindItemByValue("btCommit").Enabled = true;
                    return;
                }
                //TFxx.No
                loadLCDoc(ExLCDoc);
                if (TabId == ExportLCDocProcessing.Actions.Register || TabId == ExportLCDocProcessing.Actions.Register1)
                {
                    if (ExLCDoc.Status.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "This doc already authorized !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]) && Request.QueryString["lst"].Equals("4appr"))
                    {
                        if (ExLCDoc.Status.Equals(bd.TransactionStatus.UNA))
                        {
                            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                            RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                            RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                            return;
                        }
                        lblLCCodeMessage.Text = "This doc already reversed !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    RadToolBar1.FindItemByValue("btCommit").Enabled = true;
                    return;
                }
                if (!ExLCDoc.Status.Equals(bd.TransactionStatus.AUT))
                {
                    lblLCCodeMessage.Text = "This doc not authorize !";
                    RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                    bc.Commont.SetTatusFormControls(this.Controls, false);
                    return;
                }
                if (TabId == ExportLCDocProcessing.Actions.Amend)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]) && Request.QueryString["lst"].Equals("4appr"))
                    {
                        lblLCCodeMessage.Text = "Invalid code !";
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLCDoc.AmendStatus) && ExLCDoc.AmendStatus.Equals(bd.TransactionStatus.UNA))
                    {
                        lblLCCodeMessage.Text = "This docs is waiting for amend authorize !";
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    var ExLcDocAmend = dbEntities.findExportLCDocLastestAmend(tbLCCode.Text);
                    if (tbLCCode.Text.Equals(ExLcDocAmend.AmendNo))
                        tbLCCode.Text += ".1";
                    else
                    {
                        string[] s = ExLCDoc.DocCode.Split('.');
                        tbLCCode.Text += "." + (Convert.ToInt32(s[2]) + 1);
                    }
                    RadToolBar1.FindItemByValue("btCommit").Enabled = true;
                    return;
                }
            }
        }
        private void setDefaultControls()
        {
            txtBeneficiaryName.Enabled = false;
            txtBeneficiaryAddr1.Enabled = false;
            txtBeneficiaryAddr2.Enabled = false;
            txtBeneficiaryAddr3.Enabled = false;
            //
            txtApplicantName.Enabled = false;
            txtApplicantAddr1.Enabled = false;
            txtApplicantAddr2.Enabled = false;
            txtApplicantAddr3.Enabled = false;
            //
            txtIssuingBankNo.Enabled = false;
            txtIssuingBankName.Enabled = false;
            txtIssuingBankAddr1.Enabled = false;
            txtIssuingBankAddr2.Enabled = false;
            txtIssuingBankAddr3.Enabled = false;
            //
            txtDocumentaryCreditNo.Enabled = false;
            txtCommodity.Enabled = false;
            txtOriginalAmount.Enabled = false;
            txtTenor.Enabled = false;
            txtOriginalTenor.Enabled = false;
            //
            txtChargeCode1.Enabled = false;
            txtChargeCode2.Enabled = false;
            txtChargeCode3.Enabled = false;
            //
            divAmountRegister.Visible = ((TabId == ExportLCDocProcessing.Actions.Register) || (TabId == ExportLCDocProcessing.Actions.Register1));
            divAmountAmend.Visible = (TabId == ExportLCDocProcessing.Actions.Amend);
            //
            divTenorRegister.Visible = ((TabId == ExportLCDocProcessing.Actions.Register) || (TabId == ExportLCDocProcessing.Actions.Register1));
            divTenorAmend.Visible = (TabId == ExportLCDocProcessing.Actions.Amend);
            //
            if (TabId == ExportLCDocProcessing.Actions.Amend)
                RadTabStrip3.Tabs[0].Text = "Service Charge";
            //
            var dsCurrency = bd.SQLData.B_BCURRENCY_GetAll();
            bc.Commont.initRadComboBox(ref rcbCurrency, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy1, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy2, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy3, "Code", "Code", dsCurrency);
            //
            var tblList = bd.SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_DocsCode");
            bc.Commont.initRadComboBox(ref rcbDocsCode1, "Description", "Id", tblList);
            bc.Commont.initRadComboBox(ref rcbDocsCode2, "Description", "Id", tblList);
            bc.Commont.initRadComboBox(ref rcbDocsCode3, "Description", "Id", tblList);
        }
        private void loadLC(BEXPORT_LC ExLC)
        {
            txtBeneficiaryName.Text = ExLC.BeneficiaryName;
            txtBeneficiaryAddr1.Text = ExLC.BeneficiaryAddr1;
            txtBeneficiaryAddr2.Text = ExLC.BeneficiaryAddr2;
            txtBeneficiaryAddr3.Text = ExLC.BeneficiaryAddr3;
            //
            txtApplicantName.Text = ExLC.ApplicantName;
            txtApplicantAddr1.Text = ExLC.ApplicantAddr1;
            txtApplicantAddr2.Text = ExLC.ApplicantAddr2;
            txtApplicantAddr3.Text = ExLC.ApplicantAddr3;
            //
            txtIssuingBankNo.Text = ExLC.IssuingBankNo;
            txtIssuingBankName.Text = ExLC.IssuingBankName;
            txtIssuingBankAddr1.Text = ExLC.IssuingBankAddr1;
            txtIssuingBankAddr2.Text = ExLC.IssuingBankAddr2;
            txtIssuingBankAddr3.Text = ExLC.IssuingBankAddr3;
            //
            txtDocumentaryCreditNo.Text = ExLC.ImportLCCode;
            txtCommodity.Text = ExLC.Commodity;
            rcbCurrency.SelectedValue = ExLC.Currency;
            txtAmount.Value = ExLC.Amount;
            txtTenor.Text = ExLC.Tenor;
        }
        private void loadLCDoc(BEXPORT_LC_DOCS_PROCESSING ExLCDoc)
        {
            txtBeneficiaryName.Text = ExLCDoc.BeneficiaryName;
            txtBeneficiaryAddr1.Text = ExLCDoc.BeneficiaryAddr1;
            txtBeneficiaryAddr2.Text = ExLCDoc.BeneficiaryAddr2;
            txtBeneficiaryAddr3.Text = ExLCDoc.BeneficiaryAddr3;
            //
            txtApplicantName.Text = ExLCDoc.ApplicantName;
            txtApplicantAddr1.Text = ExLCDoc.ApplicantAddr1;
            txtApplicantAddr2.Text = ExLCDoc.ApplicantAddr2;
            txtApplicantAddr3.Text = ExLCDoc.ApplicantAddr3;
            //
            txtIssuingBankNo.Text = ExLCDoc.IssuingBankNo;
            txtIssuingBankName.Text = ExLCDoc.IssuingBankName;
            txtIssuingBankAddr1.Text = ExLCDoc.IssuingBankAddr1;
            txtIssuingBankAddr2.Text = ExLCDoc.IssuingBankAddr2;
            txtIssuingBankAddr3.Text = ExLCDoc.IssuingBankAddr3;
            //
            txtNostroAgentBankNo.Text = ExLCDoc.NostroAgentBankNo;
            txtNostroAgentBankName.Text = ExLCDoc.NostroAgentBankName;
            txtNostroAgentBankAddr1.Text = ExLCDoc.NostroAgentBankAddr1;
            txtNostroAgentBankAddr2.Text = ExLCDoc.NostroAgentBankAddr2;
            txtNostroAgentBankAddr3.Text = ExLCDoc.NostroAgentBankAddr3;
            //
            txtReceivingBankName.Text = ExLCDoc.ReceivingBankName;
            txtReceivingBankAddr1.Text = ExLCDoc.ReceivingBankAddr1;
            txtReceivingBankAddr2.Text = ExLCDoc.ReceivingBankAddr2;
            txtReceivingBankAddr3.Text = ExLCDoc.ReceivingBankAddr3;
            //
            txtDocumentaryCreditNo.Text = ExLCDoc.DocumentaryCreditNo;
            txtCommodity.Text = ExLCDoc.Commodity;
            rcbCurrency.SelectedValue = ExLCDoc.Currency;
            txtAmount.Value = ExLCDoc.Amount;
            txtDocumentReceivedDate.SelectedDate = ExLCDoc.DocumentReceivedDate;
            txtProccessingDate.SelectedDate = ExLCDoc.ProccessingDate;
            txtTenor.Text = ExLCDoc.Tenor;
            txtInvoiceNo.Text = ExLCDoc.InvoiceNo;
            //
            if (!string.IsNullOrEmpty(ExLCDoc.DocsCode1))
            {
                loadLCDocsCode(ExLCDoc.DocsCode1, ExLCDoc.NoOfOriginals1, ExLCDoc.NoOfCopies1, ref rcbDocsCode1, ref txtNoOfOriginals1, ref txtNoOfCopies1);
                if (!string.IsNullOrEmpty(ExLCDoc.DocsCode2))
                {
                    divDocs2.Attributes.CssStyle.Remove("Display");
                    loadLCDocsCode(ExLCDoc.DocsCode2, ExLCDoc.NoOfOriginals2, ExLCDoc.NoOfCopies2, ref rcbDocsCode2, ref txtNoOfOriginals2, ref txtNoOfCopies2);
                    if (!string.IsNullOrEmpty(ExLCDoc.DocsCode3))
                    {
                        divDocs3.Attributes.CssStyle.Remove("Display");
                        loadLCDocsCode(ExLCDoc.DocsCode3, ExLCDoc.NoOfOriginals3, ExLCDoc.NoOfCopies3, ref rcbDocsCode3, ref txtNoOfOriginals3, ref txtNoOfCopies3);
                    }
                }
            }
            //
            txtRemark.Text = ExLCDoc.Remark;
            txtSettlementInstruction.Text = ExLCDoc.SettlementInstruction;
            //
            rcbWaiveCharges.SelectedValue = ExLCDoc.WaiveCharges;
            rcbWaiveCharges_OnSelectedIndexChanged(null, null);
            txtChargeRemarks.Text = ExLCDoc.ChargeRemarks;
            txtVATNo.Text = ExLCDoc.VATNo;
            if (ExLCDoc.WaiveCharges.Equals(bd.YesNo.NO)) loadCharges();
        }
        private void loadLCDocsCode(string DocsCode, int? NoOfOriginals, int? NoOfCopies, ref RadComboBox rcbDocsCode, ref RadNumericTextBox txtNoOfOriginals, ref RadNumericTextBox txtNoOfCopies)
        {
            rcbDocsCode.SelectedValue = DocsCode;
            txtNoOfOriginals.Value = NoOfOriginals;
            txtNoOfCopies.Value = NoOfCopies;
        }
        private void loadCharges()
        {
            var lstCharges = dbEntities.BEXPORT_LC_DOCS_PROCESSING_CHARGES.Where(p => p.DocsCode.Equals(tbLCCode.Text));
            if (lstCharges == null || lstCharges.Count() <= 0) return;
            //
            foreach (BEXPORT_LC_DOCS_PROCESSING_CHARGES ch in lstCharges)
            {
                switch (ch.ChargeCode)
                {
                    case ExportLCDocProcessing.Charges.Commission:
                    //case ExportLCDocProcessing.Charges.Service:
                        loadCharge(ch, ref txtChargeCode1, ref rcbChargeCcy1, ref rcbChargeAcct1, ref tbChargeAmt1, ref rcbPartyCharged1, ref rcbAmortCharge1, ref rcbChargeStatus1, ref lblTaxCode1, ref lblTaxAmt1);
                        break;
                    case ExportLCDocProcessing.Charges.Courier:
                        loadCharge(ch, ref txtChargeCode2, ref rcbChargeCcy2, ref rcbChargeAcct2, ref tbChargeAmt2, ref rcbPartyCharged2, ref rcbAmortCharge2, ref rcbChargeStatus2, ref lblTaxCode2, ref lblTaxAmt2);
                        break;
                    case ExportLCDocProcessing.Charges.Other:
                        loadCharge(ch, ref txtChargeCode3, ref rcbChargeCcy3, ref rcbChargeAcct3, ref tbChargeAmt3, ref rcbPartyCharged3, ref rcbAmortCharge3, ref rcbChargeStatus3, ref lblTaxCode3, ref lblTaxAmt3);
                        break;
                }
            }
        }
        private void loadCharge(BEXPORT_LC_DOCS_PROCESSING_CHARGES ExLCCharge, ref RadTextBox txtChargeCode, ref RadComboBox cbChargeCcy, ref RadComboBox cbChargeAcc, ref RadNumericTextBox txtChargeAmt,
            ref RadComboBox cbChargeParty, ref RadComboBox cbChargeAmort, ref RadComboBox cbChargeStatus, ref Label lblTaxCode, ref Label lblTaxAmt)
        {
            txtChargeCode.Text = ExLCCharge.ChargeCode;
            cbChargeCcy.SelectedValue = ExLCCharge.ChargeCcy;
            cbChargeAcc.SelectedValue = ExLCCharge.ChargeAcc;
            txtChargeAmt.Value = ExLCCharge.ChargeAmt;
            cbChargeParty.SelectedValue = ExLCCharge.PartyCharged;
            cbChargeAmort.SelectedValue = ExLCCharge.AmortCharge;
            cbChargeStatus.SelectedValue = ExLCCharge.ChargeStatus;
            lblTaxCode.Text = ExLCCharge.TaxCode;
            if (ExLCCharge.TaxAmt.HasValue)
                lblTaxAmt.Text = ExLCCharge.TaxAmt.ToString();
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var ExLCDoc = dbEntities.findExportLCDoc(tbLCCode.Text);
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName.ToLower();
            #region Register
            if (TabId == ExportLCDocProcessing.Actions.Register || TabId == ExportLCDocProcessing.Actions.Register1)
            {
                switch (commandName)
                {
                    case bc.Commands.Commit:
                        if (ExLCDoc == null)
                        {
                            ExLCDoc = new BEXPORT_LC_DOCS_PROCESSING();
                            ExLCDoc.DocCode = tbLCCode.Text.Trim();
                            ExLCDoc.AmendNo = tbLCCode.Text.Trim();
                            ExLCDoc.ActiveRecordFlag = "Yes";
                            ExLCDoc.Status = bd.TransactionStatus.UNA;
                            ExLCDoc.CreateDate = DateTime.Now;
                            ExLCDoc.CreateBy = this.UserInfo.Username;
                            saveLCDoc(ref ExLCDoc);
                            dbEntities.BEXPORT_LC_DOCS_PROCESSING.Add(ExLCDoc);
                        }
                        else
                        {
                            ExLCDoc.Status = bd.TransactionStatus.UNA;
                            ExLCDoc.ActiveRecordFlag = "Yes";
                            ExLCDoc.UpdateDate = DateTime.Now;
                            ExLCDoc.UpdatedBy = this.UserInfo.Username;
                            saveLCDoc(ref ExLCDoc);
                            //Xoa di insert lai
                            var ExLCCharge = dbEntities.BEXPORT_LC_CHARGES.Where(p => p.ExportLCCode.Trim().ToLower().Equals(tbLCCode.Text.Trim().ToLower())).FirstOrDefault();
                            while (ExLCCharge != null)
                            {
                                dbEntities.BEXPORT_LC_CHARGES.Remove(ExLCCharge);
                                ExLCCharge = dbEntities.BEXPORT_LC_CHARGES.Where(p => p.ExportLCCode.Trim().ToLower().Equals(tbLCCode.Text.Trim().ToLower())).FirstOrDefault();
                            }
                        }
                        if (ExLCDoc.WaiveCharges.Equals(bd.YesNo.NO))
                        {
                            BEXPORT_LC_DOCS_PROCESSING_CHARGES ExLCCharge;
                            if (tbChargeAmt1.Value.HasValue)
                            {
                                ExLCCharge = new BEXPORT_LC_DOCS_PROCESSING_CHARGES();
                                saveCharge(txtChargeCode1, rcbChargeCcy1, rcbChargeAcct1, tbChargeAmt1, rcbPartyCharged1, rcbAmortCharge1, rcbChargeStatus1, lblTaxCode1, lblTaxAmt1, ref ExLCCharge);
                                dbEntities.BEXPORT_LC_DOCS_PROCESSING_CHARGES.Add(ExLCCharge);
                            }
                            if (tbChargeAmt2.Value.HasValue)
                            {
                                ExLCCharge = new BEXPORT_LC_DOCS_PROCESSING_CHARGES();
                                saveCharge(txtChargeCode2, rcbChargeCcy2, rcbChargeAcct2, tbChargeAmt2, rcbPartyCharged2, rcbAmortCharge2, rcbChargeStatus2, lblTaxCode2, lblTaxAmt2, ref ExLCCharge);
                                dbEntities.BEXPORT_LC_DOCS_PROCESSING_CHARGES.Add(ExLCCharge);
                            }
                            if (tbChargeAmt3.Value.HasValue)
                            {
                                ExLCCharge = new BEXPORT_LC_DOCS_PROCESSING_CHARGES();
                                saveCharge(txtChargeCode3, rcbChargeCcy3, rcbChargeAcct3, tbChargeAmt3, rcbPartyCharged3, rcbAmortCharge3, rcbChargeStatus3, lblTaxCode3, lblTaxAmt3, ref ExLCCharge);
                                dbEntities.BEXPORT_LC_DOCS_PROCESSING_CHARGES.Add(ExLCCharge);
                            }
                        }
                        //
                        dbEntities.SaveChanges();
                        //
                        Response.Redirect("Default.aspx?tabid=" + this.TabId);
                        break;
                    case bc.Commands.Authorize:
                    case bc.Commands.Reverse:
                        if (ExLCDoc != null)
                        {
                            if (commandName.Equals(bc.Commands.Authorize))
                            {
                                ExLCDoc.Status = bd.TransactionStatus.AUT;
                                ExLCDoc.AuthorizedBy = this.UserInfo.Username;
                                ExLCDoc.AuthorizedDate = DateTime.Now;
                                //
                                dbEntities.SaveChanges();
                                Response.Redirect("Default.aspx?tabid=" + this.TabId);
                                return;
                            }
                            //
                            ExLCDoc.Status = bd.TransactionStatus.REV;
                            dbEntities.SaveChanges();
                            Response.Redirect("Default.aspx?tabid=" + this.TabId + "&code=" + tbLCCode.Text);
                        }
                        break;
                }
            }
            #endregion Register
            #region Amend
            if (TabId == ExportLCDocProcessing.Actions.Amend)
            {
                switch (commandName)
                {
                    case bc.Commands.Commit:
                        if (ExLCDoc == null)
                        {
                            string docCode = tbLCCode.Text.Substring(0, tbLCCode.Text.LastIndexOf("."));
                            var ExLCDocOld = dbEntities.findExportLCDoc(docCode);
                            ExLCDocOld.ActiveRecordFlag = "No";
                            ExLCDoc = new BEXPORT_LC_DOCS_PROCESSING()
                            {
                                AmendNo = tbLCCode.Text.Trim(),
                                AmendNoOriginal = ExLCDocOld.AmendNo,
                                DocCode = docCode,
                                ActiveRecordFlag = "Yes",
                                Status = bd.TransactionStatus.UNA,
                                CreateDate = DateTime.Now,
                                CreateBy = this.UserInfo.Username,
                            };
                            saveLCDoc(ref ExLCDoc);
                            dbEntities.BEXPORT_LC_DOCS_PROCESSING.Add(ExLCDoc);
                        }
                        else
                        {
                            var ExLCDocOld = dbEntities.findExportLCDoc(ExLCDoc.AmendNoOriginal);
                            ExLCDocOld.ActiveRecordFlag = "No";
                            //
                            ExLCDoc.AmendStatus = bd.TransactionStatus.UNA;
                            ExLCDoc.ActiveRecordFlag = "Yes";
                            ExLCDoc.UpdateDate = DateTime.Now;
                            ExLCDoc.UpdatedBy = this.UserInfo.Username;
                            saveLCDoc(ref ExLCDoc);
                            //Xoa di insert lai
                            var ExLCCharge = dbEntities.BEXPORT_LC_CHARGES.Where(p => p.ExportLCCode.Trim().ToLower().Equals(tbLCCode.Text.Trim().ToLower())).FirstOrDefault();
                            while (ExLCCharge != null)
                            {
                                dbEntities.BEXPORT_LC_CHARGES.Remove(ExLCCharge);
                                ExLCCharge = dbEntities.BEXPORT_LC_CHARGES.Where(p => p.ExportLCCode.Trim().ToLower().Equals(tbLCCode.Text.Trim().ToLower())).FirstOrDefault();
                            }
                        }
                        if (ExLCDoc.WaiveCharges.Equals(bd.YesNo.NO))
                        {
                            BEXPORT_LC_DOCS_PROCESSING_CHARGES ExLCCharge;
                            if (tbChargeAmt1.Value.HasValue)
                            {
                                ExLCCharge = new BEXPORT_LC_DOCS_PROCESSING_CHARGES();
                                saveCharge(txtChargeCode1, rcbChargeCcy1, rcbChargeAcct1, tbChargeAmt1, rcbPartyCharged1, rcbAmortCharge1, rcbChargeStatus1, lblTaxCode1, lblTaxAmt1, ref ExLCCharge);
                                dbEntities.BEXPORT_LC_DOCS_PROCESSING_CHARGES.Add(ExLCCharge);
                            }
                            if (tbChargeAmt2.Value.HasValue)
                            {
                                ExLCCharge = new BEXPORT_LC_DOCS_PROCESSING_CHARGES();
                                saveCharge(txtChargeCode2, rcbChargeCcy2, rcbChargeAcct2, tbChargeAmt2, rcbPartyCharged2, rcbAmortCharge2, rcbChargeStatus2, lblTaxCode2, lblTaxAmt2, ref ExLCCharge);
                                dbEntities.BEXPORT_LC_DOCS_PROCESSING_CHARGES.Add(ExLCCharge);
                            }
                            if (tbChargeAmt3.Value.HasValue)
                            {
                                ExLCCharge = new BEXPORT_LC_DOCS_PROCESSING_CHARGES();
                                saveCharge(txtChargeCode3, rcbChargeCcy3, rcbChargeAcct3, tbChargeAmt3, rcbPartyCharged3, rcbAmortCharge3, rcbChargeStatus3, lblTaxCode3, lblTaxAmt3, ref ExLCCharge);
                                dbEntities.BEXPORT_LC_DOCS_PROCESSING_CHARGES.Add(ExLCCharge);
                            }
                        }
                        //
                        dbEntities.SaveChanges();
                        //
                        Response.Redirect("Default.aspx?tabid=" + this.TabId);
                        break;
                    case bc.Commands.Authorize:
                    case bc.Commands.Reverse:
                        if (ExLCDoc != null)
                        {
                            if (commandName.Equals(bc.Commands.Authorize))
                            {
                                ExLCDoc.AmendStatus = bd.TransactionStatus.AUT;
                                //
                                dbEntities.SaveChanges();
                                Response.Redirect("Default.aspx?tabid=" + this.TabId);
                                return;
                            }
                            //
                            var ExLCDocOld = dbEntities.findExportLCDoc(ExLCDoc.AmendNoOriginal);
                            ExLCDocOld.ActiveRecordFlag = "Yes";
                            //
                            ExLCDoc.AmendStatus = bd.TransactionStatus.REV;
                            ExLCDoc.ActiveRecordFlag = "No";
                            //
                            dbEntities.SaveChanges();
                            Response.Redirect("Default.aspx?tabid=" + this.TabId + "&code=" + tbLCCode.Text);
                        }
                        break;
                }
            }
            #endregion Amend
        }
        private void saveLCDoc(ref BEXPORT_LC_DOCS_PROCESSING ExLCDoc)
        {
            ExLCDoc.BeneficiaryName = txtBeneficiaryName.Text;
            ExLCDoc.BeneficiaryAddr1 = txtBeneficiaryAddr1.Text;
            ExLCDoc.BeneficiaryAddr2 = txtBeneficiaryAddr2.Text;
            ExLCDoc.BeneficiaryAddr3 = txtBeneficiaryAddr3.Text;
            //
            ExLCDoc.ApplicantName = txtApplicantName.Text;
            ExLCDoc.ApplicantAddr1 = txtApplicantAddr1.Text;
            ExLCDoc.ApplicantAddr2 = txtApplicantAddr2.Text;
            ExLCDoc.ApplicantAddr3 = txtApplicantAddr3.Text;
            //
            ExLCDoc.IssuingBankNo = txtIssuingBankNo.Text;
            ExLCDoc.IssuingBankName = txtIssuingBankName.Text;
            ExLCDoc.IssuingBankAddr1 = txtIssuingBankAddr1.Text;
            ExLCDoc.IssuingBankAddr2 = txtIssuingBankAddr2.Text;
            ExLCDoc.IssuingBankAddr3 = txtIssuingBankAddr3.Text;
            //
            ExLCDoc.NostroAgentBankNo = txtNostroAgentBankNo.Text;
            ExLCDoc.NostroAgentBankName = txtNostroAgentBankName.Text;
            ExLCDoc.NostroAgentBankAddr1 = txtNostroAgentBankAddr1.Text;
            ExLCDoc.NostroAgentBankAddr2 = txtNostroAgentBankAddr2.Text;
            ExLCDoc.NostroAgentBankAddr3 = txtNostroAgentBankAddr3.Text;
            //
            ExLCDoc.ReceivingBankName = txtReceivingBankName.Text;
            ExLCDoc.ReceivingBankAddr1 = txtReceivingBankAddr1.Text;
            ExLCDoc.ReceivingBankAddr2 = txtReceivingBankAddr2.Text;
            ExLCDoc.ReceivingBankAddr3 = txtReceivingBankAddr3.Text;
            //
            ExLCDoc.DocumentaryCreditNo = txtDocumentaryCreditNo.Text;
            ExLCDoc.Commodity = txtCommodity.Text;
            ExLCDoc.Currency = rcbCurrency.SelectedValue;
            ExLCDoc.Amount = txtAmount.Value;
            ExLCDoc.DocumentReceivedDate = txtDocumentReceivedDate.SelectedDate;
            ExLCDoc.ProccessingDate = txtProccessingDate.SelectedDate;
            ExLCDoc.Tenor = txtTenor.Text;
            ExLCDoc.InvoiceNo = txtInvoiceNo.Text;
            //
            ExLCDoc.DocsCode1 = rcbDocsCode1.SelectedValue;
            if (txtNoOfOriginals1.Value.HasValue)
                ExLCDoc.NoOfOriginals1 = Convert.ToInt32(txtNoOfOriginals1.Value.Value);
            if (txtNoOfCopies1.Value.HasValue)
                ExLCDoc.NoOfCopies1 = Convert.ToInt32(txtNoOfCopies1.Value.Value);
            //
            ExLCDoc.DocsCode2 = rcbDocsCode2.SelectedValue;
            if (txtNoOfOriginals2.Value.HasValue)
                ExLCDoc.NoOfOriginals2 = Convert.ToInt32(txtNoOfOriginals2.Value.Value);
            if (txtNoOfCopies2.Value.HasValue)
                ExLCDoc.NoOfCopies2 = Convert.ToInt32(txtNoOfCopies2.Value.Value);
            //
            ExLCDoc.DocsCode3 = rcbDocsCode3.SelectedValue;
            if (txtNoOfOriginals3.Value.HasValue)
                ExLCDoc.NoOfOriginals3 = Convert.ToInt32(txtNoOfOriginals3.Value.Value);
            if (txtNoOfCopies3.Value.HasValue)
                ExLCDoc.NoOfCopies3 = Convert.ToInt32(txtNoOfCopies3.Value.Value);
            //
            ExLCDoc.Remark = txtRemark.Text;
            ExLCDoc.SettlementInstruction = txtSettlementInstruction.Text;
            //
            ExLCDoc.WaiveCharges = rcbWaiveCharges.SelectedValue;
            ExLCDoc.ChargeRemarks = txtChargeRemarks.Text;
            ExLCDoc.VATNo = txtVATNo.Text;
        }
        private void saveCharge(RadTextBox txtChargeCode, RadComboBox cbChargeCcy, RadComboBox cbChargeAcc, RadNumericTextBox txtChargeAmt, RadComboBox cbChargeParty, RadComboBox cbChargeAmort,
            RadComboBox cbChargeStatus, Label lblTaxCode, Label lblTaxAmt, ref BEXPORT_LC_DOCS_PROCESSING_CHARGES ExLCCharge)
        {
            ExLCCharge.DocsCode = tbLCCode.Text;
            ExLCCharge.ChargeCode = txtChargeCode.Text;
            ExLCCharge.ChargeCcy = cbChargeCcy.SelectedValue;
            ExLCCharge.ChargeAcc = cbChargeAcc.SelectedValue;
            ExLCCharge.ChargeAmt = txtChargeAmt.Value;
            ExLCCharge.PartyCharged = cbChargeParty.SelectedValue;
            ExLCCharge.AmortCharge = cbChargeAmort.SelectedValue;
            ExLCCharge.ChargeStatus = cbChargeStatus.SelectedValue;
            ExLCCharge.TaxCode = lblTaxCode.Text;
            if (!string.IsNullOrEmpty(lblTaxAmt.Text))
                ExLCCharge.TaxAmt = Convert.ToDouble(lblTaxAmt.Text);
        }

        //ABBKVNVX : AN BINH COMMERCIAL JOINT STOCK BANK
        protected void txtNostroAgentBankNo_TextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtNostroAgentBankNo.Text, ref lblNostroAgentBankMessage, ref txtNostroAgentBankName, ref txtNostroAgentBankAddr1, ref txtNostroAgentBankAddr2, ref txtNostroAgentBankAddr3);
        }

        protected void rcbWaiveCharges_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string WaiveCharges = rcbWaiveCharges.SelectedValue;
            RadTabStrip3.Visible = WaiveCharges.Equals(bd.YesNo.NO);
            RadMultiPage1.Visible = WaiveCharges.Equals(bd.YesNo.NO);
        }

        protected void LoadChargeAcct(ref RadComboBox cboChargeAcct, string ChargeCcy)
        {
            //bc.Commont.initRadComboBox(ref cboChargeAcct, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Text, ChargeCcy));
        }
        protected void rcbChargeCcy1_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct1, rcbChargeCcy1.SelectedValue);
        }
        protected void rcbChargeCcy2_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct2, rcbChargeCcy2.SelectedValue);
        }
        protected void rcbChargeCcy3_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct3, rcbChargeCcy3.SelectedValue);
        }
    }
}