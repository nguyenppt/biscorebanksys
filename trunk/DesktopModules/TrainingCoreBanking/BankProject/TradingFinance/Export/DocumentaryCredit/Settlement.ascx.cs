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
{
    public partial class Settlement : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private ExportLCDocSettlement dbEntities = new ExportLCDocSettlement();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtChargeCode1.Text = ExportLCDocSettlement.Charges.Commission;
            txtChargeCode2.Text = ExportLCDocSettlement.Charges.Courier;
            txtChargeCode3.Text = ExportLCDocSettlement.Charges.Other;
            if (IsPostBack) return;
            //
            RadToolBar1.FindItemByValue("btCommit").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            //
            setDefaultControls();
            //
            string lst = Request.QueryString["lst"];
            tbLCCode.Text = Request.QueryString["code"];
            if (!string.IsNullOrEmpty(tbLCCode.Text))
            {
                BEXPORT_LC_DOCS_SETTLEMENT ExLCDocSettlement;
                int i = tbLCCode.Text.IndexOf("."), j = tbLCCode.Text.LastIndexOf(".");
                if (i < 0)
                {
                    lblLCCodeMessage.Text = "Invalid Document Code !";
                    return;
                }
                if (i == j)//Document Code
                {
                    BEXPORT_LC_DOCS_PROCESSING ExLCDoc = dbEntities.findExportLCDoc(tbLCCode.Text);
                    if (ExLCDoc == null)
                    {
                        lblLCCodeMessage.Text = "Document Code not found !";
                        return;
                    }
                    if (!ExLCDoc.Status.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "Document Code not authorize !";
                        return;
                    }
                    if (ExLCDoc.PaymentFull.HasValue && ExLCDoc.PaymentFull.Value == true)
                    {
                        lblLCCodeMessage.Text = "Document Code payment full !";
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLCDoc.AmendStatus) && ExLCDoc.Status.Equals(bd.TransactionStatus.UNA))
                    {
                        lblLCCodeMessage.Text = "Document Code is under amend !";
                        return;
                    }
                    if (!(string.IsNullOrEmpty(ExLCDoc.RejectStatus) || ExLCDoc.RejectStatus.Equals(bd.TransactionStatus.REV)))
                    {
                        lblLCCodeMessage.Text = "Document Code is rejected !";
                        return;
                    }
                    //Tim Settlement
                    ExLCDocSettlement = dbEntities.findExportLCDocSettlementUNA(tbLCCode.Text);
                    if (ExLCDocSettlement != null)
                    {
                        lblLCCodeMessage.Text = "Document Code has payment unautorize !";
                        return;
                    }
                    ExLCDocSettlement = dbEntities.findExportLCDocSettlementLastest(tbLCCode.Text);
                    if (ExLCDocSettlement != null){
                        i = ExLCDocSettlement.PaymentCode.LastIndexOf(".") + 1;
                        tbLCCode.Text += "." + (Convert.ToInt32(ExLCDocSettlement.PaymentCode.Substring(i, ExLCDocSettlement.PaymentCode.Length - i)) + 1);
                    }
                    else{
                        tbLCCode.Text += ".1";
                    }
                    loadLCDoc(ExLCDoc);
                    txtVATNo.Text = dbEntities.getVATNo();
                    //Cho phép thanh toán
                    RadToolBar1.FindItemByValue("btCommit").Enabled = true;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = true;
                    return;
                }
                //Settlement
                ExLCDocSettlement = dbEntities.findExportLCDocSettlement(tbLCCode.Text);
                if (ExLCDocSettlement == null)
                {
                    lblLCCodeMessage.Text = "This PaymentCode not exists !";
                    return;
                }
                loadLCSettlement(ExLCDocSettlement);
                //chờ duyệt
                if (!string.IsNullOrEmpty(lst) && lst.Equals("4appr"))
                {
                    bc.Commont.SetTatusFormControls(this.Controls, false);
                    //register approve
                    if (ExLCDocSettlement.Status.Equals(bd.TransactionStatus.UNA))
                    {
                        //Cho phép duyệt
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    return;
                }
                if (!ExLCDocSettlement.Status.Equals(bd.TransactionStatus.AUT))
                {
                    ExLCDocSettlement = dbEntities.findExportLCDocSettlementLastest(ExLCDocSettlement.DocsCode);
                    if (ExLCDocSettlement.PaymentCode.Equals(tbLCCode.Text))
                    {
                        //Cho phép edit
                        RadToolBar1.FindItemByValue("btCommit").Enabled = true;
                        RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                        RadToolBar1.FindItemByValue("btSearch").Enabled = true;
                        return;
                    }
                }
                bc.Commont.SetTatusFormControls(this.Controls, false);
                return;
            }
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            RadToolBar1.FindItemByValue("btSearch").Enabled = true;
        }
        private void setDefaultControls()
        {
            txtBeneficiaryNumber.Enabled = false;
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
            txtNostroAgentBankNo.Enabled = false;
            txtNostroAgentBankName.Enabled = false;
            txtNostroAgentBankAddr1.Enabled = false;
            txtNostroAgentBankAddr2.Enabled = false;
            txtNostroAgentBankAddr3.Enabled = false;
            //
            txtReceivingBankName.Enabled = false;
            txtReceivingBankAddr1.Enabled = false;
            txtReceivingBankAddr2.Enabled = false;
            txtReceivingBankAddr3.Enabled = false;
            //
            txtDocumentaryCreditNo.Enabled = false;
            //
            txtChargeCode1.Enabled = false;
            txtChargeCode2.Enabled = false;
            txtChargeCode3.Enabled = false;
            //
            txtVATNo.Enabled = false;
            //
            var dsCurrency = bd.SQLData.B_BCURRENCY_GetAll();
            bc.Commont.initRadComboBox(ref rcbCurrency, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy1, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy2, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy3, "Code", "Code", dsCurrency);
        }
        
        private void loadLCDoc(BEXPORT_LC_DOCS_PROCESSING ExLCDoc)
        {
            if (ExLCDoc == null) return;
            //
            txtBeneficiaryNumber.Text = ExLCDoc.BeneficiaryNo;
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
            rcbCurrency.SelectedValue = ExLCDoc.Currency;
            txtInvoiceNo.Text = ExLCDoc.InvoiceNo;
        }
        private void loadLCSettlement(BEXPORT_LC_DOCS_SETTLEMENT ExLCDocSettlement)
        {
            loadLCDoc(dbEntities.findExportLCDoc(ExLCDocSettlement.DocsCode));
            //
            txtInvoiceAmount.Value = ExLCDocSettlement.InvoiceAmount;
            txtReceiveAmount.Value = ExLCDocSettlement.ReceiveAmount;
            if (ExLCDocSettlement.DeductedAmount.HasValue)
                lblDeductedAmount.Text = ExLCDocSettlement.DeductedAmount.ToString();
            txtValueDate.SelectedDate = ExLCDocSettlement.ValueDate;
            //
            //
            rcbWaiveCharges.SelectedValue = ExLCDocSettlement.WaiveCharges;
            rcbWaiveCharges_OnSelectedIndexChanged(null, null);
            txtChargeRemarks.Text = ExLCDocSettlement.ChargeRemarks;
            txtVATNo.Text = ExLCDocSettlement.VATNo;
            if (ExLCDocSettlement.WaiveCharges.Equals(bd.YesNo.NO)) loadCharges();
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
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName.ToLower();
            //
            string paymentCode = tbLCCode.Text.Trim();
            var ExLCDocSettlement = dbEntities.findExportLCDocSettlement(paymentCode);
            //
            switch (commandName)
            {
                case bc.Commands.Commit:
                    if (ExLCDocSettlement == null)
                    {
                        ExLCDocSettlement = new BEXPORT_LC_DOCS_SETTLEMENT();
                        ExLCDocSettlement.Status = bd.TransactionStatus.UNA;
                        ExLCDocSettlement.CreateDate = DateTime.Now;
                        ExLCDocSettlement.CreateBy = this.UserInfo.Username;
                        saveSettlement(ref ExLCDocSettlement);
                        dbEntities.BEXPORT_LC_DOCS_SETTLEMENT.Add(ExLCDocSettlement);
                    }
                    else
                    {
                        ExLCDocSettlement.Status = bd.TransactionStatus.UNA;
                        ExLCDocSettlement.UpdateDate = DateTime.Now;
                        ExLCDocSettlement.UpdatedBy = this.UserInfo.Username;
                        saveSettlement(ref ExLCDocSettlement);
                        //Xoa di insert lai
                        var ExLCDocCharge = dbEntities.BEXPORT_LC_DOCS_SETTLEMENT_CHARGES.Where(p => p.PaymentCode.Trim().ToLower().Equals(paymentCode.ToLower()));
                        if (ExLCDocCharge != null)
                        {
                            foreach (BEXPORT_LC_DOCS_SETTLEMENT_CHARGES ch in ExLCDocCharge)
                                dbEntities.BEXPORT_LC_DOCS_SETTLEMENT_CHARGES.Remove(ch);
                        }
                    }
                    if (ExLCDocSettlement.WaiveCharges.Equals(bd.YesNo.NO))
                    {
                        BEXPORT_LC_DOCS_SETTLEMENT_CHARGES ExLCCharge;
                        if (tbChargeAmt1.Value.HasValue)
                        {
                            ExLCCharge = new BEXPORT_LC_DOCS_SETTLEMENT_CHARGES();
                            saveCharge(txtChargeCode1, rcbChargeCcy1, rcbChargeAcct1, tbChargeAmt1, rcbPartyCharged1, rcbAmortCharge1, rcbChargeStatus1, lblTaxCode1, lblTaxAmt1, ref ExLCCharge);
                            dbEntities.BEXPORT_LC_DOCS_SETTLEMENT_CHARGES.Add(ExLCCharge);
                        }
                        if (tbChargeAmt2.Value.HasValue)
                        {
                            ExLCCharge = new BEXPORT_LC_DOCS_SETTLEMENT_CHARGES();
                            saveCharge(txtChargeCode2, rcbChargeCcy2, rcbChargeAcct2, tbChargeAmt2, rcbPartyCharged2, rcbAmortCharge2, rcbChargeStatus2, lblTaxCode2, lblTaxAmt2, ref ExLCCharge);
                            dbEntities.BEXPORT_LC_DOCS_SETTLEMENT_CHARGES.Add(ExLCCharge);
                        }
                        if (tbChargeAmt3.Value.HasValue)
                        {
                            ExLCCharge = new BEXPORT_LC_DOCS_SETTLEMENT_CHARGES();
                            saveCharge(txtChargeCode3, rcbChargeCcy3, rcbChargeAcct3, tbChargeAmt3, rcbPartyCharged3, rcbAmortCharge3, rcbChargeStatus3, lblTaxCode3, lblTaxAmt3, ref ExLCCharge);
                            dbEntities.BEXPORT_LC_DOCS_SETTLEMENT_CHARGES.Add(ExLCCharge);
                        }
                    }
                    //
                    dbEntities.SaveChanges();
                    //
                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
                case bc.Commands.Authorize:
                case bc.Commands.Reverse:
                    if (ExLCDocSettlement != null)
                    {
                        if (commandName.Equals(bc.Commands.Authorize))
                        {
                            ExLCDocSettlement.Status = bd.TransactionStatus.AUT;
                            ExLCDocSettlement.AuthorizedBy = this.UserInfo.Username;
                            ExLCDocSettlement.AuthorizedDate = DateTime.Now;
                            /*
                            if (ExLCDoc.PaymentAmount.HasValue)
                                ExLCDoc.PaymentAmount += ExLCDoc.Amount;
                            else
                                ExLCDoc.PaymentAmount = ExLCDoc.Amount;
                            ExLCDoc.PaymentFull = (ExLC.PaymentAmount == ExLC.Amount);
                            */
                            dbEntities.SaveChanges();
                            Response.Redirect("Default.aspx?tabid=" + this.TabId);
                        }
                        else
                        {
                            ExLCDocSettlement.Status = bd.TransactionStatus.REV;
                            dbEntities.SaveChanges();
                            Response.Redirect("Default.aspx?tabid=" + this.TabId + "&code=" + tbLCCode.Text);
                        }
                    }
                    break;
            }
        }
        private void saveSettlement(ref BEXPORT_LC_DOCS_SETTLEMENT ExLCDocSettlement)
        {
            ExLCDocSettlement.PaymentCode = tbLCCode.Text.Trim();
            int i = ExLCDocSettlement.PaymentCode.LastIndexOf(".");
            ExLCDocSettlement.DocsCode = ExLCDocSettlement.PaymentCode.Substring(0, i);
            ExLCDocSettlement.InvoiceAmount = txtInvoiceAmount.Value;
            ExLCDocSettlement.ReceiveAmount = txtReceiveAmount.Value;
            if (txtInvoiceAmount.Value.HasValue || txtReceiveAmount.Value.HasValue)
            {
                if (txtInvoiceAmount.Value.HasValue)
                {
                    ExLCDocSettlement.DeductedAmount = txtInvoiceAmount.Value;
                    if (txtReceiveAmount.Value.HasValue) ExLCDocSettlement.DeductedAmount = txtInvoiceAmount.Value - txtReceiveAmount.Value;
                }
                else ExLCDocSettlement.DeductedAmount = -txtReceiveAmount.Value;
            }
            ExLCDocSettlement.ValueDate = txtValueDate.SelectedDate;
            ExLCDocSettlement.WaiveCharges = rcbWaiveCharges.SelectedValue;
            ExLCDocSettlement.ChargeRemarks = txtChargeRemarks.Text;
            ExLCDocSettlement.VATNo = txtVATNo.Text;
        }
        private void saveCharge(RadTextBox txtChargeCode, RadComboBox cbChargeCcy, RadComboBox cbChargeAcc, RadNumericTextBox txtChargeAmt, RadComboBox cbChargeParty, RadComboBox cbChargeAmort,
            RadComboBox cbChargeStatus, Label lblTaxCode, Label lblTaxAmt, ref BEXPORT_LC_DOCS_SETTLEMENT_CHARGES ExLCCharge)
        {
            ExLCCharge.PaymentCode = tbLCCode.Text;
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
            bc.Commont.initRadComboBox(ref cboChargeAcct, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtBeneficiaryName.Text, ChargeCcy));
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

        protected void btnVAT_Click(object sender, EventArgs e)
        {
            showReport("VAT");
        }
        private void showReport(string reportType)
        {
            var ExLCDocSettlement = dbEntities.findExportLCDocSettlement(tbLCCode.Text);
            if (ExLCDocSettlement == null)
            {
                lblLCCodeMessage.Text = "Can not find this Code.";
                return;
            }
            var ExLCDoc = dbEntities.findExportLCDoc(ExLCDocSettlement.DocsCode);
            //
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/Export/DocumentProcessing/Settlement/";
            string reportSaveName = "";
            DataSet reportData = new DataSet();
            DataTable tbl1 = new DataTable();
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {
                switch (reportType)
                {
                    case "VAT":
                        reportTemplate = Context.Server.MapPath(reportTemplate + "VAT.doc");
                        reportSaveName = "VAT" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        //
                        var dataVAT = new Model.Reports.VAT()
                        {
                            UserName = ExLCDocSettlement.CreateBy,
                            VATNo = ExLCDocSettlement.VATNo,
                            TransCode = ExLCDocSettlement.PaymentCode,
                            //
                            CustomerID = "",
                            CustomerName = "",
                            CustomerAddress = "",
                            IdentityNo = "",
                            //
                            DebitAccount = "",
                            ChargeRemarks = ExLCDocSettlement.ChargeRemarks
                        };
                        //
                        var ExLCDocCharges = dbEntities.BEXPORT_LC_DOCS_SETTLEMENT_CHARGES.Where(p => p.PaymentCode.Equals(tbLCCode.Text));
                        if (ExLCDocCharges != null)
                        {
                            double TotalTaxAmount = 0, TotalChargeAmount = 0;
                            foreach (BEXPORT_LC_DOCS_SETTLEMENT_CHARGES ch in ExLCDocCharges)
                            {
                                if (ch.ChargeAmt.HasValue && ch.ChargeAmt.Value != 0)
                                {
                                    if (string.IsNullOrEmpty(dataVAT.ChargeType1))
                                    {
                                        dataVAT.ChargeType1 = dbEntities.getChargeTypeInfo(ch.ChargeCode, 1);
                                        dataVAT.ChargeAmount1 = ch.ChargeAmt.Value + ch.ChargeCcy + " " + dbEntities.getChargeTypeInfo(ch.ChargeCode, 2);
                                        if (ch.TaxAmt.HasValue) TotalTaxAmount += ch.TaxAmt.Value;
                                        TotalChargeAmount += ch.ChargeAmt.Value;
                                    }
                                    else if (string.IsNullOrEmpty(dataVAT.ChargeType2))
                                    {
                                        dataVAT.ChargeType2 = dbEntities.getChargeTypeInfo(ch.ChargeCode, 1);
                                        dataVAT.ChargeAmount2 = ch.ChargeAmt.Value + ch.ChargeCcy + " " + dbEntities.getChargeTypeInfo(ch.ChargeCode, 2);
                                        if (ch.TaxAmt.HasValue) TotalTaxAmount += ch.TaxAmt.Value;
                                        TotalChargeAmount += ch.ChargeAmt.Value;
                                    }
                                    else if (string.IsNullOrEmpty(dataVAT.ChargeType3))
                                    {
                                        dataVAT.ChargeType3 = dbEntities.getChargeTypeInfo(ch.ChargeCode, 1);
                                        dataVAT.ChargeAmount3 = ch.ChargeAmt.Value + ch.ChargeCcy + " " + dbEntities.getChargeTypeInfo(ch.ChargeCode, 2);
                                        if (ch.TaxAmt.HasValue) TotalTaxAmount += ch.TaxAmt.Value;
                                        TotalChargeAmount += ch.ChargeAmt.Value;
                                    }
                                }
                            }
                            TotalChargeAmount += TotalTaxAmount;
                            if (TotalChargeAmount != 0)
                            {
                                dataVAT.TotalChargeAmount = TotalChargeAmount + ExLCDoc.Currency;
                                dataVAT.TotalChargeAmountWord = Utils.ReadNumber(ExLCDoc.Currency, TotalChargeAmount);
                                if (TotalTaxAmount != 0)
                                {
                                    dataVAT.TotalTaxAmount = TotalTaxAmount + ExLCDoc.Currency + " PL90304";
                                    dataVAT.TotalTaxText = "VAT";
                                }
                            }
                        }
                        //
                        var lstData2 = new List<Model.Reports.VAT>();
                        lstData2.Add(dataVAT);
                        tbl1 = Utils.CreateDataTable<Model.Reports.VAT>(lstData2);
                        reportData.Tables.Add(tbl1);
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
                        lblLCCodeMessage.Text = reportData.Tables[0].TableName + "#" + err.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                lblLCCodeMessage.Text = ex.Message;
            }
        }
    }
}