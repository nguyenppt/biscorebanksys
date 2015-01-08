﻿using System;
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
    public partial class AdvisingAndNegotiationLCAmend : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private ExportLC dbEntities = new ExportLC();
        //
        private void setDefaultControls()
        {            
            rcbWaiveCharges_OnSelectedIndexChanged(null, null);
            tbVatNo.Enabled = false;
            rcbChargeCode.Enabled = false;
            rcbChargeCode2.Enabled = false;
            rcbChargeCode3.Enabled = false;
            txtNumberOfAmendment.Enabled = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //
            var dsCurrency = bd.SQLData.B_BCURRENCY_GetAll();
            bc.Commont.initRadComboBox(ref rcbChargeCcy, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy2, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy3, "Code", "Code", dsCurrency);
            //
            if (!string.IsNullOrEmpty(Request.QueryString["Code"]))
            {
                RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                //
                tbLCCode.Text = Request.QueryString["Code"];
                if (tbLCCode.Text.IndexOf(".") < 0)
                {
                    var ExLC = dbEntities.findExportLC(tbLCCode.Text);
                    if (ExLC == null)
                    {
                        lblLCCodeMessage.Text = "Can not find this Code !";
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    if (!ExLC.Status.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "This Code not authorized !";
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.AmendStatus) && (!ExLC.AmendStatus.Equals(bd.TransactionStatus.AUT) && !ExLC.AmendStatus.Equals(bd.TransactionStatus.REV)))
                    {
                        lblLCCodeMessage.Text = "This Code is under amend !";
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    if (ExLC.PaymentFull.HasValue && ExLC.PaymentFull.Value)
                    {
                        lblLCCodeMessage.Text = "This Code Payment Full !";
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    //Tìm xem có amend nào k ?
                    var ExLCAmend = dbEntities.findExportLCAmend(tbLCCode.Text);
                    if (ExLCAmend != null)
                    {
                        if (ExLCAmend.AmendStatus.Equals(bd.TransactionStatus.UNA))
                        {
                            lblLCCodeMessage.Text = "This Code is under amend !";
                            bc.Commont.SetTatusFormControls(this.Controls, false);
                            return;
                        }
                        txtNumberOfAmendment.Value = ExLCAmend.NumberOfAmendment + 1;
                    }
                    txtNumberOfAmendment.Value = 1;
                    tbLCCode.Text += "." + txtNumberOfAmendment.Value;
                    loadLC(ExLC);
                    RadToolBar1.FindItemByValue("btCommit").Enabled = true;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = true;
                }
                else//Load thông tin theo amendCode
                {
                    var ExLCAmend = dbEntities.findExportLCAmend(tbLCCode.Text);
                    if (ExLCAmend == null)
                    {
                        lblLCCodeMessage.Text = "This Code can not found !";
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    loadLCAmend(ExLCAmend);
                    if (ExLCAmend.AmendStatus.Equals(bd.TransactionStatus.AUT))
                    {                        
                        lblLCCodeMessage.Text = "This Code is authorized !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]) && Request.QueryString["lst"].Equals("4appr"))
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        if (!ExLCAmend.AmendStatus.Equals(bd.TransactionStatus.UNA))
                        {
                            lblLCCodeMessage.Text = "This Code is reversed !";                            
                            return;
                        }
                        //cho phép duyệt
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                    }
                    else//cho phép edit
                    {
                        RadToolBar1.FindItemByValue("btCommit").Enabled = true;
                        RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                        RadToolBar1.FindItemByValue("btSearch").Enabled = true;
                    }
                }
            }
            //else rcbWaiveCharges.SelectedValue = "NO";
            //
            setDefaultControls();
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var ExLC = dbEntities.findExportLC(tbLCCode.Text.Substring(0, tbLCCode.Text.IndexOf(".")));
            var ExLCAmend = dbEntities.findExportLCAmend(tbLCCode.Text);
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName.ToLower();
            switch (commandName)
            {
                case bc.Commands.Commit:
                    if (ExLCAmend == null)
                    {
                        ExLCAmend = new BEXPORT_LC_AMEND();
                        ExLCAmend.AmendNo = tbLCCode.Text.Trim();
                        ExLCAmend.AmendStatus = bd.TransactionStatus.UNA;
                        ExLCAmend.AmendBy = this.UserInfo.Username;
                        ExLCAmend.AmendDate = DateTime.Now;
                        saveLCAmend(ref ExLCAmend);
                        dbEntities.BEXPORT_LC_AMEND.Add(ExLCAmend);
                    }
                    else
                    {
                        ExLCAmend.AmendStatus = bd.TransactionStatus.UNA;
                        saveLCAmend(ref ExLCAmend);
                        //Xoa di insert lai
                        var ExLCCharge = dbEntities.BEXPORT_LC_CHARGES.Where(p => p.ExportLCCode.Equals(ExLCAmend.AmendNo)).FirstOrDefault();
                        while (ExLCCharge != null)
                        {
                            dbEntities.BEXPORT_LC_CHARGES.Remove(ExLCCharge);
                            ExLCCharge = dbEntities.BEXPORT_LC_CHARGES.Where(p => p.ExportLCCode.Equals(ExLCAmend.AmendNo)).FirstOrDefault();
                        }
                    }
                    if (ExLCAmend.WaiveCharges.Equals(bd.YesNo.NO))
                    {
                        BEXPORT_LC_CHARGES ExLCCharge;
                        if (tbChargeAmt.Value.HasValue)
                        {
                            ExLCCharge = new BEXPORT_LC_CHARGES();
                            saveCharge(rcbChargeCode, rcbChargeCcy, rcbChargeAcct, tbChargeAmt, rcbPartyCharged, rcbAmortCharge, rcbChargeStatus, lblTaxCode, lblTaxAmt, ref ExLCCharge);
                            dbEntities.BEXPORT_LC_CHARGES.Add(ExLCCharge);
                        }
                        if (tbChargeAmt2.Value.HasValue)
                        {
                            ExLCCharge = new BEXPORT_LC_CHARGES();
                            saveCharge(rcbChargeCode2, rcbChargeCcy2, rcbChargeAcct2, tbChargeAmt2, rcbPartyCharged2, rcbAmortCharge2, rcbChargeStatus2, lblTaxCode2, lblTaxAmt2, ref ExLCCharge);
                            dbEntities.BEXPORT_LC_CHARGES.Add(ExLCCharge);
                        }
                        if (tbChargeAmt3.Value.HasValue)
                        {
                            ExLCCharge = new BEXPORT_LC_CHARGES();
                            saveCharge(rcbChargeCode3, rcbChargeCcy3, rcbChargeAcct3, tbChargeAmt3, rcbPartyCharged3, rcbAmortCharge3, rcbChargeStatus3, lblTaxCode3, lblTaxAmt3, ref ExLCCharge);
                            dbEntities.BEXPORT_LC_CHARGES.Add(ExLCCharge);
                        }
                    }
                    //
                    ExLC.AmendStatus = bd.TransactionStatus.UNA;
                    ExLC.AmendBy = this.UserInfo.Username;
                    ExLC.AmendDate = DateTime.Now;
                    //
                    try
                    {
                        dbEntities.SaveChanges();
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                System.Diagnostics.Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                                    validationErrors.Entry.Entity.GetType().FullName,
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                        throw;  // You can also choose to handle the exception here...
                    }
                    //
                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
                case bc.Commands.Authorize:
                case bc.Commands.Reverse:
                    if (commandName.Equals(bc.Commands.Authorize))
                    {
                        ExLC.AmendStatus = bd.TransactionStatus.AUT;
                        ExLCAmend.AmendStatus = bd.TransactionStatus.AUT;
                        //
                        dbEntities.SaveChanges();
                        Response.Redirect("Default.aspx?tabid=" + this.TabId);
                        return;
                    }
                    //
                    ExLC.AmendStatus = bd.TransactionStatus.REV;
                    ExLCAmend.AmendStatus = bd.TransactionStatus.REV;
                    dbEntities.SaveChanges();
                    Response.Redirect("Default.aspx?tabid=" + this.TabId + "&code=" + tbLCCode.Text);
                    break;
            }
        }

        private void saveLCAmend(ref BEXPORT_LC_AMEND ExLCAmend)
        {
            ExLCAmend.ImportLCCode = txtImportLCNo.Text.Trim();
            //
            ExLCAmend.SenderReference = txtSenderReference.Text.Trim();
            ExLCAmend.ReceiverReference = txtReceiverReference.Text.Trim();
            ExLCAmend.IssuingBankReference = txtIssuingBankReference.Text.Trim();
            //
            ExLCAmend.IssuingBankType = rcbIssuingBankType.SelectedValue.Trim();
            ExLCAmend.IssuingBankNo = txtIssuingBankNo.Text.Trim();
            ExLCAmend.IssuingBankName = txtIssuingBankName.Text.Trim();
            ExLCAmend.IssuingBankAddr1 = txtIssuingBankAddr1.Text.Trim();
            ExLCAmend.IssuingBankAddr2 = txtIssuingBankAddr2.Text.Trim();
            ExLCAmend.IssuingBankAddr3 = txtIssuingBankAddr3.Text.Trim();
            //
            ExLCAmend.DateOfIssue = txtDateOfIssue.SelectedDate;
            ExLCAmend.DateOfAmendment = txtDateOfAmendment.SelectedDate;
            ExLCAmend.NumberOfAmendment = Convert.ToInt32(txtNumberOfAmendment.Value);
            //
            ExLCAmend.BeneficiaryNo = txtBeneficiaryNo.Text.Trim();
            ExLCAmend.BeneficiaryName = txtBeneficiaryName.Text.Trim();
            ExLCAmend.BeneficiaryAddr1 = txtBeneficiaryAddr1.Text.Trim();
            ExLCAmend.BeneficiaryAddr2 = txtBeneficiaryAddr2.Text.Trim();
            ExLCAmend.BeneficiaryAddr3 = txtBeneficiaryAddr3.Text.Trim();
            //
            ExLCAmend.NewDateOfExpiry = txtNewDateOfExpiry.SelectedDate;
            ExLCAmend.IncreaseOfDocumentaryCreditAmount = txtIncreaseOfDocumentaryCreditAmount.Value;
            ExLCAmend.DecreaseOfDocumentaryCreditAmount = txtDecreaseOfDocumentaryCreditAmount.Value;
            ExLCAmend.NewDocumentaryCreditAmountAfterAmendment = txtNewDocumentaryCreditAmountAfterAmendment.Value;
            ExLCAmend.PercentageCreditAmountTolerance1 = txtPercentCreditAmountTolerance1.Value;
            ExLCAmend.PercentageCreditAmountTolerance2 = txtPercentCreditAmountTolerance2.Value;
            //
            ExLCAmend.PlaceOfTakingInCharge = txtPlaceOfTakingInCharge.Text.Trim();
            ExLCAmend.PortOfLoading = txtPortOfLoading.Text.Trim();
            ExLCAmend.PortOfDischarge = txtPortOfDischarge.Text.Trim();
            ExLCAmend.PlaceOfFinalDestination = txtPlaceOfFinalDestination.Text.Trim();
            ExLCAmend.LatesDateOfShipment = txtLatesDateOfShipment.SelectedDate;
            //
            ExLCAmend.Narrative = txtNarrative.Text;
            ExLCAmend.SenderToReceiverInformation = txtSenderToReceiverInformation.Text.Trim();
            //
            ExLCAmend.WaiveCharges = rcbWaiveCharges.SelectedValue;
            ExLCAmend.ChargeRemarks = tbChargeRemarks.Text.Trim();
            ExLCAmend.VATNo = tbVatNo.Text.Trim();
        }
        private void saveCharge(RadComboBox cbChargeCode, RadComboBox cbChargeCcy, RadComboBox cbChargeAcc, RadNumericTextBox txtChargeAmt, RadComboBox cbChargeParty, RadComboBox cbChargeAmort,
            RadComboBox cbChargeStatus, Label lblTaxCode, Label lblTaxAmt, ref BEXPORT_LC_CHARGES ExLCCharge)
        {
            ExLCCharge.ChargeCode = cbChargeCode.SelectedValue;
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
        private void loadLC(BEXPORT_LC ExLC)
        {
            txtImportLCNo.Text = ExLC.ImportLCCode;
            txtImportLCNo_TextChanged(null, null);
            //
            txtSenderReference.Text = "";
            txtReceiverReference.Text = "";
            txtIssuingBankReference.Text = "";
            //
            rcbIssuingBankType.SelectedValue = ExLC.IssuingBankType;
            rcbIssuingBankType_OnSelectedIndexChanged(null, null);
            txtIssuingBankNo.Text = ExLC.IssuingBankNo;
            txtIssuingBankName.Text = ExLC.IssuingBankName;
            txtIssuingBankAddr1.Text = ExLC.IssuingBankAddr1;
            txtIssuingBankAddr2.Text = ExLC.IssuingBankAddr2;
            txtIssuingBankAddr3.Text = ExLC.IssuingBankAddr3;
            //
            txtDateOfIssue.SelectedDate = ExLC.DateOfIssue;
            //txtDateOfAmendment.SelectedDate = DateTime.Now;
            //txtNumberOfAmendment.Value = 1;
            //
            txtBeneficiaryNo.Text = ExLC.BeneficiaryNo;
            txtBeneficiaryName.Text = ExLC.BeneficiaryName;
            txtBeneficiaryAddr1.Text = ExLC.BeneficiaryAddr1;
            txtBeneficiaryAddr2.Text = ExLC.BeneficiaryAddr2;
            txtBeneficiaryAddr3.Text = ExLC.BeneficiaryAddr3;
            //
            //txtNewDateOfExpiry.SelectedDate
            //txtIncreaseOfDocumentaryCreditAmount.Value
            //txtDecreaseOfDocumentaryCreditAmount.Value
            //txtNewDocumentaryCreditAmountAfterAmendment.Text
            txtPercentCreditAmountTolerance1.Value = ExLC.PercentageCreditAmountTolerance1;
            txtPercentCreditAmountTolerance2.Value = ExLC.PercentageCreditAmountTolerance2;
            //
            txtPlaceOfTakingInCharge.Text = ExLC.PlaceOfTakingInCharge;
            txtPortOfLoading.Text = ExLC.PortOfLoading;
            txtPortOfDischarge.Text = ExLC.PortOfDischarge;
            txtPlaceOfFinalDestination.Text = ExLC.PlaceOfFinalDestination;
            txtLatesDateOfShipment.SelectedDate = ExLC.LatesDateOfShipment;
            //txtNarrative.Text
            txtSenderToReceiverInformation.Text = ExLC.SenderToReceiverInformation;
            //
            rcbWaiveCharges.SelectedValue = ExLC.WaiveCharges;
            tbChargeRemarks.Text = ExLC.ChargeRemarks;
            tbVatNo.Text = ExLC.VATNo;
        }
        private void loadLCAmend(BEXPORT_LC_AMEND ExLCAmend)
        {
            txtImportLCNo.Text = ExLCAmend.ImportLCCode;
            txtImportLCNo_TextChanged(null, null);
            //
            txtSenderReference.Text = "";
            txtReceiverReference.Text = "";
            txtIssuingBankReference.Text = "";
            //
            rcbIssuingBankType.SelectedValue = ExLCAmend.IssuingBankType;
            rcbIssuingBankType_OnSelectedIndexChanged(null, null);
            txtIssuingBankNo.Text = ExLCAmend.IssuingBankNo;
            txtIssuingBankName.Text = ExLCAmend.IssuingBankName;
            txtIssuingBankAddr1.Text = ExLCAmend.IssuingBankAddr1;
            txtIssuingBankAddr2.Text = ExLCAmend.IssuingBankAddr2;
            txtIssuingBankAddr3.Text = ExLCAmend.IssuingBankAddr3;
            //
            txtDateOfIssue.SelectedDate = ExLCAmend.DateOfIssue;
            txtDateOfAmendment.SelectedDate = ExLCAmend.DateOfAmendment;
            txtNumberOfAmendment.Value = ExLCAmend.NumberOfAmendment;
            //
            txtBeneficiaryNo.Text = ExLCAmend.BeneficiaryNo;
            txtBeneficiaryName.Text = ExLCAmend.BeneficiaryName;
            txtBeneficiaryAddr1.Text = ExLCAmend.BeneficiaryAddr1;
            txtBeneficiaryAddr2.Text = ExLCAmend.BeneficiaryAddr2;
            txtBeneficiaryAddr3.Text = ExLCAmend.BeneficiaryAddr3;
            //
            txtNewDateOfExpiry.SelectedDate = ExLCAmend.NewDateOfExpiry;
            txtIncreaseOfDocumentaryCreditAmount.Value = ExLCAmend.IncreaseOfDocumentaryCreditAmount;
            txtDecreaseOfDocumentaryCreditAmount.Value = ExLCAmend.DecreaseOfDocumentaryCreditAmount;
            txtNewDocumentaryCreditAmountAfterAmendment.Value = ExLCAmend.NewDocumentaryCreditAmountAfterAmendment;
            txtPercentCreditAmountTolerance1.Value = ExLCAmend.PercentageCreditAmountTolerance1;
            txtPercentCreditAmountTolerance2.Value = ExLCAmend.PercentageCreditAmountTolerance2;
            //
            txtPlaceOfTakingInCharge.Text = ExLCAmend.PlaceOfTakingInCharge;
            txtPortOfLoading.Text = ExLCAmend.PortOfLoading;
            txtPortOfDischarge.Text = ExLCAmend.PortOfDischarge;
            txtPlaceOfFinalDestination.Text = ExLCAmend.PlaceOfFinalDestination;
            txtLatesDateOfShipment.SelectedDate = ExLCAmend.LatesDateOfShipment;
            txtNarrative.Text = ExLCAmend.Narrative;
            txtSenderToReceiverInformation.Text = ExLCAmend.SenderToReceiverInformation;
            //
            rcbWaiveCharges.SelectedValue = ExLCAmend.WaiveCharges;
            tbChargeRemarks.Text = ExLCAmend.ChargeRemarks;
            tbVatNo.Text = ExLCAmend.VATNo;
            if (ExLCAmend.WaiveCharges.Equals(bd.YesNo.NO)) loadCharges();
        }
        private void loadCharges()
        {
            var lstCharges = dbEntities.BEXPORT_LC_CHARGES.Where(p => p.ExportLCCode.Equals(tbLCCode.Text));
            if (lstCharges == null || lstCharges.Count() <= 0) return;
            //
            foreach (BEXPORT_LC_CHARGES ch in lstCharges)
            {
                switch (ch.ChargeCode)
                {
                    case "ELC.ADVISE":
                        loadCharge(ch, ref rcbChargeCode, ref rcbChargeCcy, ref rcbChargeAcct, ref tbChargeAmt, ref rcbPartyCharged, ref rcbAmortCharge, ref rcbChargeStatus, ref lblTaxCode, ref lblTaxAmt);
                        break;
                    case "ELC.CONFIRM":
                        loadCharge(ch, ref rcbChargeCode2, ref rcbChargeCcy2, ref rcbChargeAcct2, ref tbChargeAmt2, ref rcbPartyCharged2, ref rcbAmortCharge2, ref rcbChargeStatus2, ref lblTaxCode2, ref lblTaxAmt2);
                        break;
                    case "ELC.OTHER":
                        loadCharge(ch, ref rcbChargeCode3, ref rcbChargeCcy3, ref rcbChargeAcct3, ref tbChargeAmt3, ref rcbPartyCharged3, ref rcbAmortCharge3, ref rcbChargeStatus3, ref lblTaxCode3, ref lblTaxAmt3);
                        break;
                }
            }
        }
        private void loadCharge(BEXPORT_LC_CHARGES ExLCCharge, ref RadComboBox cbChargeCode, ref RadComboBox cbChargeCcy, ref RadComboBox cbChargeAcc, ref RadNumericTextBox txtChargeAmt,
            ref RadComboBox cbChargeParty, ref RadComboBox cbChargeAmort, ref RadComboBox cbChargeStatus, ref Label lblTaxCode, ref Label lblTaxAmt)
        {
            cbChargeCode.SelectedValue = ExLCCharge.ChargeCode;
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

        protected void rcbIssuingBankType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bc.Commont.BankTypeChange(rcbIssuingBankType.SelectedValue, ref lblIssuingBankMessage, ref txtIssuingBankNo, ref txtIssuingBankName, ref txtIssuingBankAddr1, ref txtIssuingBankAddr2, ref txtIssuingBankAddr3);
        }

        //ABBKVNVX : AN BINH COMMERCIAL JOINT STOCK BANK
        protected void txtIssuingBankNo_TextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtIssuingBankNo.Text, ref lblIssuingBankMessage, ref txtIssuingBankName, ref txtIssuingBankAddr1, ref txtIssuingBankAddr2, ref txtIssuingBankAddr3);
        }
        protected void txtBeneficiaryNo_TextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtBeneficiaryNo.Text, ref lblBeneficiaryMessage, ref txtBeneficiaryName, ref txtBeneficiaryAddr1, ref txtBeneficiaryAddr2, ref txtBeneficiaryAddr3);
        }

        protected void rcbWaiveCharges_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string WaiveCharges = rcbWaiveCharges.SelectedValue;
            divACCPTCHG.Visible = WaiveCharges.Equals("NO");
            divCABLECHG.Visible = WaiveCharges.Equals("NO");
            divPAYMENTCHG.Visible = WaiveCharges.Equals("NO");
        }

        protected void LoadChargeAcct(ref RadComboBox cboChargeAcct, string ChargeCcy)
        {
            bc.Commont.initRadComboBox(ref cboChargeAcct, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Text, ChargeCcy));
        }
        protected void rcbChargeCcy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct, rcbChargeCcy.SelectedValue);
        }
        protected void rcbChargeCcy2_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct2, rcbChargeCcy2.SelectedValue);
        }
        protected void rcbChargeCcy3_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct3, rcbChargeCcy3.SelectedValue);
        }
        
        protected void txtImportLCNo_TextChanged(object sender, EventArgs e)
        {
            lblImportLCNoMessage.Text = "";
            txtCustomerName.Text = "";
            var lc = dbEntities.BIMPORT_NORMAILLC.Where(p => p.NormalLCCode.ToLower().Trim().Equals(txtImportLCNo.Text.ToLower().Trim())).FirstOrDefault();
            if (lc == null)
            {
                lblImportLCNoMessage.Text = "Can not found this LC !";
                return;
            }
            txtCustomerName.Text = lc.ApplicantName;
            lblImportLCNoMessage.Text = lc.ApplicantName;
        }

        protected void btnReportMauThongBaoLc_Click(object sender, EventArgs e)
        {
            showReport("ThuThongBao");
        }
        private void showReport(string reportType)
        {
            var ExLCAmend = dbEntities.findExportLCAmend(tbLCCode.Text);
            if (ExLCAmend == null)
            {
                lblLCCodeMessage.Text = "Can not find this LC amend.";
                return;
            }
            var ExLC = dbEntities.findExportLC(tbLCCode.Text.Substring(0, tbLCCode.Text.IndexOf(".")));
            //
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/Export/";
            string reportSaveName = "";
            DataSet reportData = new DataSet();
            DataTable tbl1 = new DataTable();
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {
                switch (reportType)
                {
                    case "ThuThongBao":
                        reportTemplate = Context.Server.MapPath(reportTemplate + "Mau Thong bao Tu chinh LC.doc");
                        reportSaveName = "ThuThongBao" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        //
                        var dataThuThongBao = new MauThongBaoVaTuChinhLc()
                        {
                            Ref = ExLCAmend.AmendNo,
                            Beneficiary = ExLCAmend.BeneficiaryName,
                            LCCode = ExLCAmend.SenderReference,
                            DateOfIssue = (ExLCAmend.DateOfIssue.HasValue ? ExLCAmend.DateOfIssue.Value.ToString("dd/MM/yyyy") : ""),
                            DateOfExpiry = (ExLC.DateOfExpiry.HasValue ? ExLC.DateOfExpiry.Value.ToString("dd/MM/yyyy") : ""),
                            IssuingBank = ExLCAmend.IssuingBankName,
                            Applicant = ExLC.ApplicantName
                        };
                        if (ExLCAmend.IncreaseOfDocumentaryCreditAmount.HasValue || ExLCAmend.DecreaseOfDocumentaryCreditAmount.HasValue)
                        {
                            if (ExLCAmend.IncreaseOfDocumentaryCreditAmount.HasValue)
                                dataThuThongBao.Amount = ExLCAmend.IncreaseOfDocumentaryCreditAmount.Value + " " + ExLC.Currency;
                            else
                                dataThuThongBao.Amount = ExLCAmend.DecreaseOfDocumentaryCreditAmount.Value + " " + ExLC.Currency;
                        }

                        if (!string.IsNullOrEmpty(ExLCAmend.BeneficiaryAddr1)) dataThuThongBao.Beneficiary += ", " + ExLCAmend.BeneficiaryAddr1;
                        if (!string.IsNullOrEmpty(ExLCAmend.BeneficiaryAddr2)) dataThuThongBao.Beneficiary += ", " + ExLCAmend.BeneficiaryAddr2;
                        if (!string.IsNullOrEmpty(ExLCAmend.BeneficiaryAddr3)) dataThuThongBao.Beneficiary += ", " + ExLCAmend.BeneficiaryAddr3;

                        if (!string.IsNullOrEmpty(ExLCAmend.IssuingBankAddr1)) dataThuThongBao.IssuingBank += ", " + ExLCAmend.IssuingBankAddr1;
                        if (!string.IsNullOrEmpty(ExLCAmend.IssuingBankAddr2)) dataThuThongBao.IssuingBank += ", " + ExLCAmend.IssuingBankAddr2;
                        if (!string.IsNullOrEmpty(ExLCAmend.IssuingBankAddr3)) dataThuThongBao.IssuingBank += ", " + ExLCAmend.IssuingBankAddr3;

                        if (!string.IsNullOrEmpty(ExLC.ApplicantAddr1)) dataThuThongBao.Applicant += ", " + ExLC.ApplicantAddr1;
                        if (!string.IsNullOrEmpty(ExLC.ApplicantAddr2)) dataThuThongBao.Applicant += ", " + ExLC.ApplicantAddr2;
                        if (!string.IsNullOrEmpty(ExLC.ApplicantAddr3)) dataThuThongBao.Applicant += ", " + ExLC.ApplicantAddr3;
                        //
                        var lstData = new List<MauThongBaoVaTuChinhLc>();
                        lstData.Add(dataThuThongBao);
                        tbl1 = Utils.CreateDataTable<MauThongBaoVaTuChinhLc>(lstData);
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