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
    public partial class AdvNegLC : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private ExportLC dbEntities = new ExportLC();
        //
        private void setDefaultControls()
        {
            //rcbWaiveCharges.SelectedValue = bd.YesNo.NO;
            rcbWaiveCharges_OnSelectedIndexChanged(null, null);
            tbVatNo.Enabled = false;
            txtChargeCode1.Enabled = false;
            txtChargeCode2.Enabled = false;
            txtChargeCode3.Enabled = false;
            //
            divConfirmLC.Style.Remove("Display");
            divConfirmLC.Style.Add("Display", (TabId == ExportLC.Actions.Confirm ? "" : "none"));            
            //
            divCancelLC.Style.Remove("Display");
            divCancelLC.Style.Add("Display", (TabId == ExportLC.Actions.Cancel ? "" : "none"));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtChargeCode1.Text = ExportLC.Charges.Advising;
            txtChargeCode2.Text = ExportLC.Charges.Courier;
            txtChargeCode3.Text = ExportLC.Charges.Other;
            //
            if (IsPostBack) return;
            //
            RadToolBar1.FindItemByValue("btCommit").Enabled = false;
            RadToolBar1.FindItemByValue("btHoldData").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            //
            var dsCurrency = bd.SQLData.B_BCURRENCY_GetAll();
            bc.Commont.initRadComboBox(ref rcbCurrency, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy1, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy2, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy3, "Code", "Code", dsCurrency);

            bc.Commont.initRadComboBox(ref rcbCommodity, "Name", "ID", bd.SQLData.B_BCOMMODITY_GetByTransactionType("OTC"));
            bc.Commont.initRadComboBox(ref rcbBeneficiaryNumber, "CustomerName", "CustomerID", bd.SQLData.B_BCUSTOMERS_OnlyBusiness());
            //
            if (!string.IsNullOrEmpty(Request.QueryString["Code"]))
            {
                tbLCCode.Text = Request.QueryString["Code"];
                var ExLC = dbEntities.findExportLC(tbLCCode.Text);
                if (ExLC == null)
                {
                    lblLCCodeMessage.Text = "Can not find this Code !";
                    bc.Commont.SetTatusFormControls(this.Controls, false);
                    return;
                }
                loadLC(ExLC);
                //
                #region Register
                if (TabId == ExportLC.Actions.Register)
                {
                    if (ExLC.Status.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "This LC is authorized !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        return;
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]) && Request.QueryString["lst"].Equals("4appr"))
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        if (!ExLC.Status.Equals(bd.TransactionStatus.UNA))
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
                        RadToolBar1.FindItemByValue("btHoldData").Enabled = true;
                        RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    }
                }
                #endregion
                #region Confirm
                if (TabId == ExportLC.Actions.Confirm)
                {
                    bc.Commont.SetTatusFormControls(this.Controls, false);
                    if (!ExLC.Status.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "This LC is not authorized !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.ConfirmStatus) && ExLC.ConfirmStatus.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "This LC is confirm !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.AmendStatus) && ExLC.AmendStatus.Equals(bd.TransactionStatus.UNA))
                    {
                        lblLCCodeMessage.Text = "This LC Amend is not authorized !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.CancelStatus) && !ExLC.CancelStatus.Equals(bd.TransactionStatus.REV))
                    {
                        lblLCCodeMessage.Text = "This LC Close is canceled !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.ClosedStatus) && !ExLC.ClosedStatus.Equals(bd.TransactionStatus.REV))
                    {
                        lblLCCodeMessage.Text = "This LC Close is closed !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]) && Request.QueryString["lst"].Equals("4appr"))
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        if (!ExLC.ConfirmStatus.Equals(bd.TransactionStatus.UNA))
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
                        //
                        rcbGenerateDelivery.Enabled = true;
                        txtDateConfirm.Enabled = true;
                        rcbConfirmInstr.Enabled = true;
                    }
                }
                #endregion
                #region Cancel
                if (TabId == ExportLC.Actions.Cancel)
                {
                    bc.Commont.SetTatusFormControls(this.Controls, false);
                    if (!ExLC.Status.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "This LC is not authorized !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.CancelStatus) && ExLC.CancelStatus.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "This LC is canceled !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.AmendStatus) && ExLC.AmendStatus.Equals(bd.TransactionStatus.UNA))
                    {
                        lblLCCodeMessage.Text = "This LC Amend is not authorized !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.ClosedStatus) && !ExLC.ClosedStatus.Equals(bd.TransactionStatus.REV))
                    {
                        lblLCCodeMessage.Text = "This LC Close is closed !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]) && Request.QueryString["lst"].Equals("4appr"))
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        if (!ExLC.CancelStatus.Equals(bd.TransactionStatus.UNA))
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
                        //
                        txtCancelDate.Enabled = true;
                        txtContingentExpiryDate.Enabled = true;
                        txtCancelRemark.Enabled = true;
                        txtImportLCNo.Enabled = true;
                    }
                }
                #endregion
                #region Close
                if (TabId == ExportLC.Actions.Close)
                {
                    bc.Commont.SetTatusFormControls(this.Controls, false);
                    if (!ExLC.Status.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "This LC is not authorized !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.ClosedStatus) && ExLC.ClosedStatus.Equals(bd.TransactionStatus.AUT))
                    {
                        lblLCCodeMessage.Text = "This LC is Closed !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.AmendStatus) && ExLC.AmendStatus.Equals(bd.TransactionStatus.UNA))
                    {
                        lblLCCodeMessage.Text = "This LC Amend is not authorized !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(ExLC.CancelStatus) && !ExLC.CancelStatus.Equals(bd.TransactionStatus.REV))
                    {
                        lblLCCodeMessage.Text = "This LC Close is canceled !";
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]) && Request.QueryString["lst"].Equals("4appr"))
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        if (!ExLC.ClosedStatus.Equals(bd.TransactionStatus.UNA))
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
                #endregion
            }
            else
            {
                if (TabId == ExportLC.Actions.Register)
                {
                    RadToolBar1.FindItemByValue("btCommit").Enabled = true;
                    RadToolBar1.FindItemByValue("btHoldData").Enabled = true;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    //
                    tbVatNo.Text = dbEntities.getVATNo();
                    tbLCCode.Text = dbEntities.getNewId();
                    //
                    rcbAdvisingBankType_OnSelectedIndexChanged(null, null);
                    rcbIssuingBankType_OnSelectedIndexChanged(null, null);
                    rcbAvailableWithType_OnSelectedIndexChanged(null, null);
                    rcbReimbBankType_OnSelectedIndexChanged(null, null);
                }
                else
                {
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = true;
                }
            }
            //
            setDefaultControls();                     
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var ExLC = dbEntities.findExportLC(tbLCCode.Text);
            var commandName = toolBarButton.CommandName.ToLower();
            #region Register
            if (TabId == ExportLC.Actions.Register)
            {                
                switch (commandName)
                {
                    case bc.Commands.Commit:
                    case bc.Commands.Hold:
                        if (ExLC == null)
                        {
                            ExLC = new BEXPORT_LC();
                            ExLC.ExportLCCode = tbLCCode.Text.Trim();
                            ExLC.Status = (commandName.Equals(bc.Commands.Commit) ? bd.TransactionStatus.UNA : bd.TransactionStatus.HLD);
                            ExLC.CreateDate = DateTime.Now;
                            ExLC.CreateBy = this.UserInfo.Username;
                            saveLC(ref ExLC);
                            dbEntities.BEXPORT_LC.Add(ExLC);
                        }
                        else
                        {
                            ExLC.Status = (commandName.Equals(bc.Commands.Commit) ? bd.TransactionStatus.UNA : bd.TransactionStatus.HLD);
                            ExLC.UpdateDate = DateTime.Now;
                            ExLC.UpdatedBy = this.UserInfo.Username;
                            saveLC(ref ExLC);
                            //Xoa di insert lai
                            var ExLCCharge = dbEntities.BEXPORT_LC_CHARGES.Where(p => p.ExportLCCode.Trim().ToLower().Equals(tbLCCode.Text.Trim().ToLower()));
                            if (ExLCCharge != null)
                            {
                                foreach (BEXPORT_LC_CHARGES ch in ExLCCharge)
                                {
                                    dbEntities.BEXPORT_LC_CHARGES.Remove(ch);
                                }
                            }
                        }
                        if (ExLC.WaiveCharges.Equals(bd.YesNo.NO))
                        {
                            BEXPORT_LC_CHARGES ExLCCharge;
                            if (tbChargeAmt1.Value.HasValue)
                            {
                                ExLCCharge = new BEXPORT_LC_CHARGES();
                                saveCharge(txtChargeCode1, rcbChargeCcy1, rcbChargeAcct1, tbChargeAmt1, rcbPartyCharged1, rcbAmortCharge1, rcbChargeStatus1, lblTaxCode1, lblTaxAmt1, ref ExLCCharge);
                                dbEntities.BEXPORT_LC_CHARGES.Add(ExLCCharge);
                            }
                            if (tbChargeAmt2.Value.HasValue)
                            {
                                ExLCCharge = new BEXPORT_LC_CHARGES();
                                saveCharge(txtChargeCode2, rcbChargeCcy2, rcbChargeAcct2, tbChargeAmt2, rcbPartyCharged2, rcbAmortCharge2, rcbChargeStatus2, lblTaxCode2, lblTaxAmt2, ref ExLCCharge);
                                dbEntities.BEXPORT_LC_CHARGES.Add(ExLCCharge);
                            }
                            if (tbChargeAmt3.Value.HasValue)
                            {
                                ExLCCharge = new BEXPORT_LC_CHARGES();
                                saveCharge(txtChargeCode3, rcbChargeCcy3, rcbChargeAcct3, tbChargeAmt3, rcbPartyCharged3, rcbAmortCharge3, rcbChargeStatus3, lblTaxCode3, lblTaxAmt3, ref ExLCCharge);
                                dbEntities.BEXPORT_LC_CHARGES.Add(ExLCCharge);
                            }
                        }
                        //
                        dbEntities.SaveChanges();
                        //
                        Response.Redirect("Default.aspx?tabid=" + this.TabId + (commandName.Equals(bc.Commands.Commit) ? "" : "&code=" + tbLCCode.Text));
                        break;
                    case bc.Commands.Authorize:
                    case bc.Commands.Reverse:
                        if (ExLC != null)
                        {
                            if (commandName.Equals(bc.Commands.Authorize))
                            {
                                ExLC.Status = bd.TransactionStatus.AUT;
                                ExLC.AuthorizedBy = this.UserInfo.Username;
                                ExLC.AuthorizedDate = DateTime.Now;
                                //
                                dbEntities.SaveChanges();
                                Response.Redirect("Default.aspx?tabid=" + this.TabId);
                                return;
                            }
                            //
                            ExLC.Status = bd.TransactionStatus.REV;
                            dbEntities.SaveChanges();
                            Response.Redirect("Default.aspx?tabid=" + this.TabId + "&code=" + tbLCCode.Text);
                        }
                        break;
                }

                return;
            }
            #endregion
            #region Cancel
            if (TabId == ExportLC.Actions.Cancel)
            {
                switch (commandName)
                {
                    case bc.Commands.Commit:
                        if (ExLC != null)
                        {
                            ExLC.CancelStatus = bd.TransactionStatus.UNA;
                            ExLC.CancelDate = DateTime.Now;
                            ExLC.CancelDay = txtCancelDate.SelectedDate;
                            ExLC.CancelContingentExpiryDate = txtContingentExpiryDate.SelectedDate;
                            ExLC.CancelRemark = txtCancelRemark.Text;
                            ExLC.ImportLCCode = txtImportLCNo.Text;
                            //
                            dbEntities.SaveChanges();
                        }                        
                        //
                        Response.Redirect("Default.aspx?tabid=" + this.TabId);
                        break;
                    case bc.Commands.Authorize:
                    case bc.Commands.Reverse:
                        if (ExLC != null)
                        {
                            if (commandName.Equals(bc.Commands.Authorize))
                            {
                                ExLC.CancelStatus = bd.TransactionStatus.AUT;
                                //
                                dbEntities.SaveChanges();
                                Response.Redirect("Default.aspx?tabid=" + this.TabId);
                                return;
                            }
                            //
                            ExLC.CancelStatus = bd.TransactionStatus.REV;
                            dbEntities.SaveChanges();
                            Response.Redirect("Default.aspx?tabid=" + this.TabId + "&code=" + tbLCCode.Text);
                        }
                        break;
                }

                return;
            }
            #endregion
            #region Close
            if (TabId == ExportLC.Actions.Close)
            {
                switch (commandName)
                {
                    case bc.Commands.Commit:
                        if (ExLC != null)
                        {                            
                            ExLC.ClosedStatus = bd.TransactionStatus.UNA;
                            ExLC.ClosedDate = DateTime.Now;
                            //
                            dbEntities.SaveChanges();
                        }
                        //
                        Response.Redirect("Default.aspx?tabid=" + this.TabId);
                        break;
                    case bc.Commands.Authorize:
                    case bc.Commands.Reverse:
                        if (ExLC != null)
                        {
                            if (commandName.Equals(bc.Commands.Authorize))
                            {
                                ExLC.ClosedStatus = bd.TransactionStatus.AUT;
                                //
                                dbEntities.SaveChanges();
                                Response.Redirect("Default.aspx?tabid=" + this.TabId);
                                return;
                            }
                            //
                            ExLC.ClosedStatus = bd.TransactionStatus.REV;
                            dbEntities.SaveChanges();
                            Response.Redirect("Default.aspx?tabid=" + this.TabId + "&code=" + tbLCCode.Text);
                        }
                        break;
                }

                return;
            }
            #endregion
        }
        private void saveLC(ref BEXPORT_LC ExLC)
        {
            ExLC.ReceivingLCVia = rcbReceivingLCVia.SelectedValue.Trim();
            ExLC.ImportLCCode = txtImportLCNo.Text.Trim();
            //
            ExLC.IssuingBankType = rcbIssuingBankType.SelectedValue.Trim();
            ExLC.IssuingBankNo = txtIssuingBankNo.Text.Trim();
            ExLC.IssuingBankName = txtIssuingBankName.Text.Trim();
            ExLC.IssuingBankAddr1 = txtIssuingBankAddr1.Text.Trim();
            ExLC.IssuingBankAddr2 = txtIssuingBankAddr2.Text.Trim();
            ExLC.IssuingBankAddr3 = txtIssuingBankAddr3.Text.Trim();
            //
            ExLC.AdvisingBankType = rcbAdvisingBankType.SelectedValue.Trim();
            ExLC.AdvisingBankNo = txtAdvisingBankNo.Text.Trim();
            ExLC.AdvisingBankName = txtAdvisingBankName.Text.Trim();
            ExLC.AdvisingBankAddr1 = txtAdvisingBankAddr1.Text.Trim();
            ExLC.AdvisingBankAddr2 = txtAdvisingBankAddr2.Text.Trim();
            ExLC.AdvisingBankAddr3 = txtAdvisingBankAddr3.Text.Trim();
            //
            ExLC.FormOfDocumentaryCredit = rcbFormOfDocumentaryCredit.SelectedValue.Trim();
            ExLC.DateOfIssue = txtDateOfIssue.SelectedDate;
            ExLC.DateOfExpiry = txtDateOfExpiry.SelectedDate;
            ExLC.PlaceOfExpiry = txtPlaceOfExpiry.Text.Trim();            
            //
            ExLC.ApplicableRule = rcbApplicableRule.SelectedValue.Trim();
            ExLC.ApplicantName = txtApplicantName.Text.Trim();
            ExLC.ApplicantAddr1 = tbApplicantAddr1.Text.Trim();
            ExLC.ApplicantAddr2 = tbApplicantAddr2.Text.Trim();
            ExLC.ApplicantAddr3 = tbApplicantAddr3.Text.Trim();
            //
            ExLC.BeneficiaryNo = rcbBeneficiaryNumber.SelectedValue;
            ExLC.BeneficiaryName = txtBeneficiaryName.Text.Trim();
            ExLC.BeneficiaryAddr1 = txtBeneficiaryAddr1.Text.Trim();
            ExLC.BeneficiaryAddr2 = txtBeneficiaryAddr2.Text.Trim();
            ExLC.BeneficiaryAddr3 = txtBeneficiaryAddr3.Text.Trim();
            //
            ExLC.Currency = rcbCurrency.SelectedValue.Trim();
            ExLC.Amount = txtAmount.Value;
            ExLC.PercentageCreditAmountTolerance1 = txtPercentCreditAmountTolerance1.Value;
            ExLC.PercentageCreditAmountTolerance2 = txtPercentCreditAmountTolerance2.Value;
            ExLC.MaximunCreditAmount = rcbMaximumCreditAmount.SelectedValue.Trim();
            //
            ExLC.AvailableWithType = rcbAvailableWithType.SelectedValue.Trim();
            ExLC.AvailableWithNo = txtAvailableWithNo.Text.Trim();
            ExLC.AvailableWithName = tbAvailableWithName.Text.Trim();
            ExLC.AvailableWithAddr1 = tbAvailableWithAddr1.Text.Trim();
            ExLC.AvailableWithAddr2 = tbAvailableWithAddr2.Text.Trim();
            ExLC.AvailableWithAddr3 = tbAvailableWithAddr3.Text.Trim();
            ExLC.AvailableWithBy = rcbAvailableWithBy.SelectedValue;
            //
            ExLC.DraftsAt = txtDraftsAt1.Text.Trim() + bc.Commont.breakLine + txtDraftsAt2.Text.Trim();
            ExLC.Tenor = rcbTenor.SelectedValue;
            ExLC.MixedPaymentDetails = txtMixedPaymentDetails1.Text.Trim() + bc.Commont.breakLine + txtMixedPaymentDetails2.Text.Trim() + bc.Commont.breakLine + txtMixedPaymentDetails3.Text.Trim() + bc.Commont.breakLine + txtMixedPaymentDetails4.Text.Trim();
            ExLC.DeferedPaymentDetails = txtDeferredPaymentDetails1.Text.Trim() + bc.Commont.breakLine + txtDeferredPaymentDetails2.Text.Trim() + bc.Commont.breakLine + txtDeferredPaymentDetails3.Text.Trim() + bc.Commont.breakLine + txtDeferredPaymentDetails4.Text.Trim();
            ExLC.PartialShipment = rcbPartialShipment.SelectedValue;
            ExLC.TranShipment = rcbTranshipment.SelectedValue;
            ExLC.PlaceOfTakingInCharge = txtPlaceOfTakingInCharge.Text.Trim();
            ExLC.PortOfLoading = txtPortOfLoading.Text.Trim();
            ExLC.PortOfDischarge = txtPortOfDischarge.Text.Trim();
            ExLC.PlaceOfFinalDestination = txtPlaceOfFinalDestination.Text.Trim();
            ExLC.LatesDateOfShipment = txtLatesDateOfShipment.SelectedDate;
            ExLC.ShipmentPeriod = txtShipmentPeriod1.Text.Trim() + bc.Commont.breakLine + txtShipmentPeriod2.Text.Trim() + bc.Commont.breakLine + txtShipmentPeriod3.Text.Trim() + bc.Commont.breakLine + txtShipmentPeriod4.Text.Trim() + bc.Commont.breakLine + txtShipmentPeriod5.Text.Trim() + bc.Commont.breakLine + txtShipmentPeriod6.Text.Trim();
            //
            ExLC.DescriptionOfGoodsServices = txtDescriptionOfGoodsServices.Text.Trim();
            ExLC.Commodity = rcbCommodity.SelectedValue;
            ExLC.DocsRequired = txtDocsRequired.Text.Trim();
            ExLC.AdditionalConditions = txtAdditionalConditions.Text.Trim();
            ExLC.Charges = txtCharges.Text.Trim();
            ExLC.PeriodForPresentation = txtPeriodForPresentation.Text.Trim();
            ExLC.ConfirmationInstructions = rcbConfimationInstructions.SelectedValue;
            //
            ExLC.ReimbBankType = rcbReimbBankType.SelectedValue.Trim();
            ExLC.ReimbBankNo = txtReimbBankNo.Text.Trim();
            ExLC.ReimbBankName = tbReimbBankName.Text.Trim();
            ExLC.ReimbBankAddr1 = tbReimbBankAddr1.Text.Trim();
            ExLC.ReimbBankAddr2 = tbReimbBankAddr2.Text.Trim();
            ExLC.ReimbBankAddr3 = tbReimbBankAddr3.Text.Trim();
            //
            ExLC.InstrToPaygAccptgNegotgBank = txtInstrToPaygAccptgNegotgBank.Text.Trim();
            //
            ExLC.AdviseThroughNo = txtAdviseThroughBankNo.Text.Trim();
            ExLC.AdviseThroughName = txtAdviseThroughBankName.Text.Trim();
            ExLC.AdviseThroughAddr1 = txtAdviseThroughBankAddr1.Text.Trim();
            ExLC.AdviseThroughAddr2 = txtAdviseThroughBankAddr2.Text.Trim();
            ExLC.AdviseThroughAddr3 = txtAdviseThroughBankAddr3.Text.Trim();
            //
            ExLC.SenderToReceiverInformation = txtSenderToReceiverInformation.Text.Trim();
            //
            ExLC.WaiveCharges = rcbWaiveCharges.SelectedValue;
            ExLC.ChargeRemarks = tbChargeRemarks.Text.Trim();
            ExLC.VATNo = tbVatNo.Text.Trim();
        }
        private void saveCharge(RadTextBox txtChargeCode, RadComboBox cbChargeCcy, RadComboBox cbChargeAcc, RadNumericTextBox txtChargeAmt, RadComboBox cbChargeParty, RadComboBox cbChargeAmort,
            RadComboBox cbChargeStatus, Label lblTaxCode, Label lblTaxAmt, ref BEXPORT_LC_CHARGES ExLCCharge)
        {
            ExLCCharge.ExportLCCode = tbLCCode.Text;
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

        private void loadLC(BEXPORT_LC ExLC)
        {
            switch (TabId)
            {
                case ExportLC.Actions.Confirm:
                    rcbGenerateDelivery.SelectedValue = ExLC.ConfirmGenerateDelivery;
                    txtDateConfirm.SelectedDate = ExLC.ConfirmDay;
                    //rcbConfirmInstr
                    break;
                case ExportLC.Actions.Cancel:
                    txtCancelDate.SelectedDate = ExLC.CancelDay;
                    txtContingentExpiryDate.SelectedDate = ExLC.CancelContingentExpiryDate;
                    txtCancelRemark.Text = ExLC.CancelRemark;
                    break;
                case ExportLC.Actions.Close:
                    break;
            }
            //
            rcbReceivingLCVia.SelectedValue = ExLC.ReceivingLCVia;
            txtImportLCNo.Text = ExLC.ImportLCCode;
            txtImportLCNo_TextChanged(null, null);
            //
            rcbIssuingBankType.SelectedValue = ExLC.IssuingBankType;
            rcbIssuingBankType_OnSelectedIndexChanged(null, null);
            txtIssuingBankNo.Text = ExLC.IssuingBankNo;
            txtIssuingBankName.Text = ExLC.IssuingBankName;
            txtIssuingBankAddr1.Text = ExLC.IssuingBankAddr1;
            txtIssuingBankAddr2.Text = ExLC.IssuingBankAddr2;
            txtIssuingBankAddr3.Text = ExLC.IssuingBankAddr3;
            //
            rcbAdvisingBankType.SelectedValue = ExLC.AdvisingBankType;
            rcbAdvisingBankType_OnSelectedIndexChanged(null, null);
            txtAdvisingBankNo.Text = ExLC.AdvisingBankNo;
            txtAdvisingBankName.Text = ExLC.AdvisingBankName;
            txtAdvisingBankAddr1.Text = ExLC.AdvisingBankAddr1;
            txtAdvisingBankAddr2.Text = ExLC.AdvisingBankAddr2;
            txtAdvisingBankAddr3.Text = ExLC.AdvisingBankAddr3;
            //
            rcbFormOfDocumentaryCredit.SelectedValue = ExLC.FormOfDocumentaryCredit;
            txtDateOfIssue.SelectedDate = ExLC.DateOfIssue;
            txtDateOfExpiry.SelectedDate = ExLC.DateOfExpiry;
            txtPlaceOfExpiry.Text = ExLC.PlaceOfExpiry;            
            //
            rcbApplicableRule.SelectedValue = ExLC.ApplicableRule;
            txtApplicantName.Text = ExLC.ApplicantName;
            tbApplicantAddr1.Text = ExLC.ApplicantAddr1;
            tbApplicantAddr2.Text = ExLC.ApplicantAddr2;
            tbApplicantAddr3.Text = ExLC.ApplicantAddr3;
            //
            rcbBeneficiaryNumber.SelectedValue = ExLC.BeneficiaryNo;
            txtBeneficiaryName.Text = ExLC.BeneficiaryName;
            txtBeneficiaryAddr1.Text = ExLC.BeneficiaryAddr1;
            txtBeneficiaryAddr2.Text = ExLC.BeneficiaryAddr2;
            txtBeneficiaryAddr3.Text = ExLC.BeneficiaryAddr3;
            //
            rcbCurrency.SelectedValue = ExLC.Currency;
            txtAmount.Value = ExLC.Amount;
            if (ExLC.PaymentAmount.HasValue) lblPaymentAmount.Text = "Payment Amount : " + String.Format("{0:C}", ExLC.PaymentAmount.Value).Replace("$", "");
            txtPercentCreditAmountTolerance1.Value = ExLC.PercentageCreditAmountTolerance1;
            txtPercentCreditAmountTolerance2.Value = ExLC.PercentageCreditAmountTolerance2;
            rcbMaximumCreditAmount.SelectedValue = ExLC.MaximunCreditAmount;
            //
            rcbAvailableWithType.SelectedValue = ExLC.AvailableWithType;
            rcbAvailableWithType_OnSelectedIndexChanged(null, null);
            txtAvailableWithNo.Text = ExLC.AvailableWithNo;
            tbAvailableWithName.Text = ExLC.AvailableWithName;
            tbAvailableWithAddr1.Text = ExLC.AvailableWithAddr1;
            tbAvailableWithAddr2.Text = ExLC.AvailableWithAddr2;
            tbAvailableWithAddr3.Text = ExLC.AvailableWithAddr3;
            rcbAvailableWithBy.SelectedValue = ExLC.AvailableWithBy;
            //
            if (!string.IsNullOrEmpty(ExLC.DraftsAt))
            {
                string[] DraftsAt = ExLC.DraftsAt.Split(new string[] { bc.Commont.breakLine }, StringSplitOptions.None);
                txtDraftsAt1.Text = DraftsAt[0];
                if (DraftsAt.Length > 1) txtDraftsAt2.Text = DraftsAt[1];
            }
            rcbTenor.SelectedValue = ExLC.Tenor;
            if (!string.IsNullOrEmpty(ExLC.MixedPaymentDetails))
            {
                string[] MixedPaymentDetails = ExLC.MixedPaymentDetails.Split(new string[] { bc.Commont.breakLine }, StringSplitOptions.None);
                txtMixedPaymentDetails1.Text = MixedPaymentDetails[0];
                if (MixedPaymentDetails.Length > 1)
                {
                    txtMixedPaymentDetails2.Text = MixedPaymentDetails[1];
                    if (MixedPaymentDetails.Length > 2)
                    {
                        txtMixedPaymentDetails3.Text = MixedPaymentDetails[2];
                        if (MixedPaymentDetails.Length > 3)
                            txtMixedPaymentDetails4.Text = MixedPaymentDetails[3];
                    }
                }
            }
            if (!string.IsNullOrEmpty(ExLC.DeferedPaymentDetails))
            {
                string[] DeferedPaymentDetails = ExLC.DeferedPaymentDetails.Split(new string[] { bc.Commont.breakLine }, StringSplitOptions.None);
                txtDeferredPaymentDetails1.Text = DeferedPaymentDetails[0];
                if (DeferedPaymentDetails.Length > 1)
                {
                    txtDeferredPaymentDetails2.Text = DeferedPaymentDetails[1];
                    if (DeferedPaymentDetails.Length > 2)
                    {
                        txtDeferredPaymentDetails3.Text = DeferedPaymentDetails[2];
                        if (DeferedPaymentDetails.Length > 3)
                            txtDeferredPaymentDetails4.Text = DeferedPaymentDetails[3];
                    }
                }
            }
            rcbPartialShipment.SelectedValue = ExLC.PartialShipment;
            rcbTranshipment.SelectedValue = ExLC.TranShipment;
            txtPlaceOfTakingInCharge.Text = ExLC.PlaceOfTakingInCharge;
            txtPortOfLoading.Text = ExLC.PortOfLoading;
            txtPortOfDischarge.Text = ExLC.PortOfDischarge;
            txtPlaceOfFinalDestination.Text = ExLC.PlaceOfFinalDestination;
            txtLatesDateOfShipment.SelectedDate = ExLC.LatesDateOfShipment;
            if (!string.IsNullOrEmpty(ExLC.ShipmentPeriod))
            {
                string[] ShipmentPeriod = ExLC.ShipmentPeriod.Split(new string[] { bc.Commont.breakLine }, StringSplitOptions.None);
                txtShipmentPeriod1.Text = ShipmentPeriod[0];
                if (ShipmentPeriod.Length > 1)
                {
                    txtShipmentPeriod2.Text = ShipmentPeriod[1];
                    if (ShipmentPeriod.Length > 2)
                    {
                        txtShipmentPeriod3.Text = ShipmentPeriod[2];
                        if (ShipmentPeriod.Length > 3)
                        {
                            txtShipmentPeriod4.Text = ShipmentPeriod[3];
                            if (ShipmentPeriod.Length > 4)
                            {
                                txtShipmentPeriod5.Text = ShipmentPeriod[4];
                                if (ShipmentPeriod.Length > 4)
                                    txtShipmentPeriod6.Text = ShipmentPeriod[4];
                            }
                        }
                    }
                }
            }
            //
            txtDescriptionOfGoodsServices.Text = ExLC.DescriptionOfGoodsServices;
            rcbCommodity.SelectedValue = ExLC.Commodity;
            txtDocsRequired.Text = ExLC.DocsRequired;
            txtAdditionalConditions.Text = ExLC.AdditionalConditions;
            txtCharges.Text = ExLC.Charges;
            txtPeriodForPresentation.Text = ExLC.PeriodForPresentation;
            rcbConfimationInstructions.SelectedValue = ExLC.ConfirmationInstructions;
            //
            rcbReimbBankType.SelectedValue = ExLC.ReimbBankType;
            rcbReimbBankType_OnSelectedIndexChanged(null, null);
            txtReimbBankNo.Text = ExLC.ReimbBankNo;
            tbReimbBankName.Text = ExLC.ReimbBankName;
            tbReimbBankAddr1.Text = ExLC.ReimbBankAddr1;
            tbReimbBankAddr2.Text = ExLC.ReimbBankAddr2;
            tbReimbBankAddr3.Text = ExLC.ReimbBankAddr3;
            //
            txtInstrToPaygAccptgNegotgBank.Text = ExLC.InstrToPaygAccptgNegotgBank;
            //
            txtAdviseThroughBankNo.Text = ExLC.AdviseThroughNo;
            txtAdviseThroughBankName.Text = ExLC.AdviseThroughName;
            txtAdviseThroughBankAddr1.Text = ExLC.AdviseThroughAddr1;
            txtAdviseThroughBankAddr2.Text = ExLC.AdviseThroughAddr2;
            txtAdviseThroughBankAddr3.Text = ExLC.AdviseThroughAddr3;
            //
            txtSenderToReceiverInformation.Text = ExLC.SenderToReceiverInformation;
            //
            rcbWaiveCharges.SelectedValue = ExLC.WaiveCharges;
            tbChargeRemarks.Text = ExLC.ChargeRemarks;
            tbVatNo.Text = ExLC.VATNo;
            //
            if (ExLC.WaiveCharges.Equals(bd.YesNo.NO)) loadCharges();
        }
        private void loadCharges()
        {
            var lstCharges = dbEntities.BEXPORT_LC_CHARGES.Where(p => p.ExportLCCode.Equals(tbLCCode.Text));
            if (lstCharges == null || lstCharges.Count() <= 0) return;
            //
            foreach (BEXPORT_LC_CHARGES ch in lstCharges)
            {
                switch(ch.ChargeCode)
                {
                    case ExportLC.Charges.Advising:
                        loadCharge(ch, ref txtChargeCode1, ref rcbChargeCcy1, ref rcbChargeAcct1, ref tbChargeAmt1, ref rcbPartyCharged1, ref rcbAmortCharge1, ref rcbChargeStatus1, ref lblTaxCode1, ref lblTaxAmt1);
                        break;
                    case ExportLC.Charges.Courier:
                        loadCharge(ch, ref txtChargeCode2, ref rcbChargeCcy2, ref rcbChargeAcct2, ref tbChargeAmt2, ref rcbPartyCharged2, ref rcbAmortCharge2, ref rcbChargeStatus2, ref lblTaxCode2, ref lblTaxAmt2);
                        break;
                    case ExportLC.Charges.Other:
                        loadCharge(ch, ref txtChargeCode3, ref rcbChargeCcy3, ref rcbChargeAcct3, ref tbChargeAmt3, ref rcbPartyCharged3, ref rcbAmortCharge3, ref rcbChargeStatus3, ref lblTaxCode3, ref lblTaxAmt3);
                        break;
                }
            }
        }
        private void loadCharge(BEXPORT_LC_CHARGES ExLCCharge, ref RadTextBox txtChargeCode, ref RadComboBox cbChargeCcy, ref RadComboBox cbChargeAcc, ref RadNumericTextBox txtChargeAmt,
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

        protected void rcbAdvisingBankType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bc.Commont.BankTypeChange(rcbAdvisingBankType.SelectedValue, ref lblAdvisingBankMessage, ref txtAdvisingBankNo, ref txtAdvisingBankName, ref txtAdvisingBankAddr1, ref txtAdvisingBankAddr2, ref txtAdvisingBankAddr3);
        }
        protected void rcbIssuingBankType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bc.Commont.BankTypeChange(rcbIssuingBankType.SelectedValue, ref lblIssuingBankMessage, ref txtIssuingBankNo, ref txtIssuingBankName, ref txtIssuingBankAddr1, ref txtIssuingBankAddr2, ref txtIssuingBankAddr3);
        }
        protected void rcbAvailableWithType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bc.Commont.BankTypeChange(rcbAvailableWithType.SelectedValue, ref lblAvailableWithMessage, ref txtAvailableWithNo, ref tbAvailableWithName, ref tbAvailableWithAddr1, ref tbAvailableWithAddr2, ref tbAvailableWithAddr3);
        }
        protected void rcbReimbBankType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bc.Commont.BankTypeChange(rcbReimbBankType.SelectedValue, ref lblReimbBankMessage, ref txtReimbBankNo, ref tbReimbBankName, ref tbReimbBankAddr1, ref tbReimbBankAddr2, ref tbReimbBankAddr3);
        }

        //ABBKVNVX : AN BINH COMMERCIAL JOINT STOCK BANK
        protected void txtAdvisingBankNo_TextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtAdvisingBankNo.Text, ref lblAdvisingBankMessage, ref txtAdvisingBankName, ref txtAdvisingBankAddr1, ref txtAdvisingBankAddr2, ref txtAdvisingBankAddr3);
        }
        protected void txtIssuingBankNo_TextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtIssuingBankNo.Text, ref lblIssuingBankMessage, ref txtIssuingBankName, ref txtIssuingBankAddr1, ref txtIssuingBankAddr2, ref txtIssuingBankAddr3);
        }
        protected void rcbBeneficiaryNumber_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblBeneficiaryMessage.Text = "";
            txtBeneficiaryName.Text = "";
            txtBeneficiaryAddr1.Text = "";
            txtBeneficiaryAddr2.Text = "";
            txtBeneficiaryAddr3.Text = "";
            if (!string.IsNullOrEmpty(rcbBeneficiaryNumber.SelectedValue))
            {
                var ds = bd.DataTam.B_BCUSTOMERS_GetbyID(rcbBeneficiaryNumber.SelectedValue);
                if (ds == null || ds.Tables.Count <= 0)
                {
                    lblBeneficiaryMessage.Text = "Customer not found !";
                    return;
                }
                DataRow dr = ds.Tables[0].Rows[0];
                txtBeneficiaryName.Text = dr["CustomerName"].ToString();
                txtBeneficiaryAddr1.Text = dr["Address"].ToString();
                txtBeneficiaryAddr2.Text = dr["City"].ToString();
                txtBeneficiaryAddr3.Text = dr["Country"].ToString();
            }
            LoadChargeAcct(ref rcbChargeAcct1, rcbChargeCcy1.SelectedValue);
            LoadChargeAcct(ref rcbChargeAcct2, rcbChargeCcy2.SelectedValue);
            LoadChargeAcct(ref rcbChargeAcct3, rcbChargeCcy3.SelectedValue);
        }
        protected void txtAvailableWithNo_TextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtAvailableWithNo.Text, ref lblAvailableWithMessage, ref tbAvailableWithName, ref tbAvailableWithAddr1, ref tbAvailableWithAddr2, ref tbAvailableWithAddr3);
        }
        protected void txtReimbBankNo_TextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtReimbBankNo.Text, ref lblReimbBankMessage, ref tbReimbBankName, ref tbReimbBankAddr1, ref tbReimbBankAddr2, ref tbReimbBankAddr3);
        }
        protected void txtAdviseThroughBankNo_TextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtAdviseThroughBankNo.Text, ref lblAdviseThroughBankMessage, ref txtAdviseThroughBankName, ref txtAdviseThroughBankAddr1, ref txtAdviseThroughBankAddr2, ref txtAdviseThroughBankAddr3);
        }

        protected void rcbWaiveCharges_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string WaiveCharges = rcbWaiveCharges.SelectedValue;
            RadTabStrip3.Visible = WaiveCharges.Equals(bd.YesNo.NO);
            RadMultiPage1.Visible = WaiveCharges.Equals(bd.YesNo.NO);
        }

        private void LoadChargeAcct(ref RadComboBox cboChargeAcct, string ChargeCcy)
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
        protected void btnReportMauBiaHsLc_Click(object sender, EventArgs e)
        {
            showReport("BiaHs");
        }
        protected void btnReportMauThongBaoLc_Click(object sender, EventArgs e)
        {
            showReport("ThuThongBao");
        }
        private void showReport(string reportType)
        {
            var ExLC = dbEntities.findExportLC(tbLCCode.Text);
            if (ExLC == null)
            {
                lblLCCodeMessage.Text = "Can not find this LC.";
                return;
            }
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
                        reportTemplate = Context.Server.MapPath(reportTemplate + "Mau Thong bao LC.doc");
                        reportSaveName = "ThuThongBao" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        //
                        var dataThuThongBao = new Model.Reports.MauThongBaoVaTuChinhLc()
                        {
                            DateCreate = (ExLC.CreateDate.HasValue ? ExLC.CreateDate.Value.ToString("dd/MM/yyyy") : ""),
                            Ref = ExLC.ExportLCCode,
                            Beneficiary = ExLC.BeneficiaryName,
                            LCCode = ExLC.ImportLCCode,
                            DateOfIssue = (ExLC.DateOfIssue.HasValue ? ExLC.DateOfIssue.Value.ToString("dd/MM/yyyy") : ""),
                            DateOfExpiry = (ExLC.DateOfExpiry.HasValue ? ExLC.DateOfExpiry.Value.ToString("dd/MM/yyyy") : ""),
                            IssuingBank = ExLC.IssuingBankName,
                            Amount = ExLC.Amount + " " + ExLC.Currency,
                            Applicant = ExLC.ApplicantName
                        };
                        if (!string.IsNullOrEmpty(ExLC.BeneficiaryAddr1)) dataThuThongBao.Beneficiary += ", " + ExLC.BeneficiaryAddr1;
                        if (!string.IsNullOrEmpty(ExLC.BeneficiaryAddr2)) dataThuThongBao.Beneficiary += ", " + ExLC.BeneficiaryAddr2;
                        if (!string.IsNullOrEmpty(ExLC.BeneficiaryAddr3)) dataThuThongBao.Beneficiary += ", " + ExLC.BeneficiaryAddr3;

                        if (!string.IsNullOrEmpty(ExLC.IssuingBankAddr1)) dataThuThongBao.IssuingBank += ", " + ExLC.IssuingBankAddr1;
                        if (!string.IsNullOrEmpty(ExLC.IssuingBankAddr2)) dataThuThongBao.IssuingBank += ", " + ExLC.IssuingBankAddr2;
                        if (!string.IsNullOrEmpty(ExLC.IssuingBankAddr3)) dataThuThongBao.IssuingBank += ", " + ExLC.IssuingBankAddr3;

                        if (!string.IsNullOrEmpty(ExLC.ApplicantAddr1)) dataThuThongBao.Applicant += ", " + ExLC.ApplicantAddr1;
                        if (!string.IsNullOrEmpty(ExLC.ApplicantAddr2)) dataThuThongBao.Applicant += ", " + ExLC.ApplicantAddr2;
                        if (!string.IsNullOrEmpty(ExLC.ApplicantAddr3)) dataThuThongBao.Applicant += ", " + ExLC.ApplicantAddr3;
                        //
                        var lstData = new List<Model.Reports.MauThongBaoVaTuChinhLc>();
                        lstData.Add(dataThuThongBao);
                        tbl1 = Utils.CreateDataTable<Model.Reports.MauThongBaoVaTuChinhLc>(lstData);
                        reportData.Tables.Add(tbl1);
                        break;
                    case "BiaHs":
                        reportTemplate = Context.Server.MapPath(reportTemplate + "Mau Bia hs lc.doc");
                        reportSaveName = "BiaHs" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        //
                        var dataBiaHs = new Model.Reports.MauBiaHsLc()
                        {
                            Ref = ExLC.ExportLCCode,
                            LCCode = ExLC.ImportLCCode,
                            DateOfIssue = (ExLC.DateOfIssue.HasValue ? ExLC.DateOfIssue.Value.ToString("dd/MM/yyyy") : ""),
                            Beneficiary = ExLC.BeneficiaryName,
                            Applicant = ExLC.ApplicantName,
                            IssuingBank = ExLC.IssuingBankName,
                            Tenor = ExLC.Tenor,
                            AdvisingBank = ExLC.AdvisingBankName,
                            Amount = ExLC.Amount + " " + ExLC.Currency,
                            LatestDateOfShipment = (ExLC.LatesDateOfShipment.HasValue ? ExLC.LatesDateOfShipment.Value.ToString("dd/MM/yyyy") : ""),
                            DateOfExpiry = (ExLC.DateOfExpiry.HasValue ? ExLC.DateOfExpiry.Value.ToString("dd/MM/yyyy") : ""),
                            Transhipment = ExLC.TranShipment,
                            PartialShipment = ExLC.PartialShipment,
                            Commodity = ExLC.Commodity,
                            PortOfLoading = ExLC.PortOfLoading,
                            PeriodForPresentation = ExLC.PeriodForPresentation,
                            PortOfDischarge = ExLC.PortOfDischarge
                        };
                        if (!string.IsNullOrEmpty(ExLC.Commodity))
                        {
                            var bc = dbEntities.BCOMMODITies.FirstOrDefault(p => p.ID.Equals(ExLC.Commodity));
                            if (bc != null) dataBiaHs.Commodity = bc.Name;
                        }

                        if (!string.IsNullOrEmpty(ExLC.BeneficiaryAddr1)) dataBiaHs.Beneficiary += ", " + ExLC.BeneficiaryAddr1;
                        if (!string.IsNullOrEmpty(ExLC.BeneficiaryAddr2)) dataBiaHs.Beneficiary += ", " + ExLC.BeneficiaryAddr2;
                        if (!string.IsNullOrEmpty(ExLC.BeneficiaryAddr3)) dataBiaHs.Beneficiary += ", " + ExLC.BeneficiaryAddr3;

                        if (!string.IsNullOrEmpty(ExLC.IssuingBankAddr1)) dataBiaHs.IssuingBank += ", " + ExLC.IssuingBankAddr1;
                        if (!string.IsNullOrEmpty(ExLC.IssuingBankAddr2)) dataBiaHs.IssuingBank += ", " + ExLC.IssuingBankAddr2;
                        if (!string.IsNullOrEmpty(ExLC.IssuingBankAddr3)) dataBiaHs.IssuingBank += ", " + ExLC.IssuingBankAddr3;

                        if (!string.IsNullOrEmpty(ExLC.ApplicantAddr1)) dataBiaHs.Applicant += ", " + ExLC.ApplicantAddr1;
                        if (!string.IsNullOrEmpty(ExLC.ApplicantAddr2)) dataBiaHs.Applicant += ", " + ExLC.ApplicantAddr2;
                        if (!string.IsNullOrEmpty(ExLC.ApplicantAddr3)) dataBiaHs.Applicant += ", " + ExLC.ApplicantAddr3;

                        if (!string.IsNullOrEmpty(ExLC.AdvisingBankAddr1)) dataBiaHs.AdvisingBank += ", " + ExLC.AdvisingBankAddr1;
                        if (!string.IsNullOrEmpty(ExLC.AdvisingBankAddr2)) dataBiaHs.AdvisingBank += ", " + ExLC.AdvisingBankAddr2;
                        if (!string.IsNullOrEmpty(ExLC.AdvisingBankAddr3)) dataBiaHs.AdvisingBank += ", " + ExLC.AdvisingBankAddr3;
                        //
                        var lstData1 = new List<Model.Reports.MauBiaHsLc>();
                        lstData1.Add(dataBiaHs);
                        tbl1 = Utils.CreateDataTable<Model.Reports.MauBiaHsLc>(lstData1);
                        reportData.Tables.Add(tbl1);
                        break;
                    case "VAT":
                        reportTemplate = Context.Server.MapPath(reportTemplate + "VAT.doc");
                        reportSaveName = "VAT" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        //
                        var dataVAT = new Model.Reports.VAT()
                        {
                            UserName = ExLC.CreateBy,
                            VATNo = ExLC.VATNo,
                            TransCode = ExLC.ExportLCCode,
                            //
                            CustomerID = "",
                            CustomerName = "",
                            CustomerAddress = "",
                            IdentityNo = "",
                            //
                            DebitAccount = "",
                            ChargeRemarks = ExLC.ChargeRemarks
                        };
                        //
                        var ExLCCharges = dbEntities.BEXPORT_LC_CHARGES.Where(p => p.ExportLCCode.Equals(tbLCCode.Text));
                        if (ExLCCharges != null)
                        {
                            double TotalTaxAmount = 0, TotalChargeAmount = 0;
                            foreach (BEXPORT_LC_CHARGES ch in ExLCCharges)
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
                                dataVAT.TotalChargeAmount = TotalChargeAmount + ExLC.Currency;
                                dataVAT.TotalChargeAmountWord = Utils.ReadNumber(ExLC.Currency, TotalChargeAmount);
                                if (TotalTaxAmount != 0)
                                {
                                    dataVAT.TotalTaxAmount = TotalTaxAmount + ExLC.Currency + " PL90304";
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

        protected void txtImportLCNo_TextChanged(object sender, EventArgs e)
        {
            /*lblImportLCNoMessage.Text = "";
            txtCustomerName.Text = "";
            var lc = dbEntities.findImportLC(txtImportLCNo.Text);
            if (lc == null)
            {
                lblImportLCNoMessage.Text = "Can not found this LC !";
                return;
            }
            txtCustomerName.Text = lc.ApplicantName;
            lblImportLCNoMessage.Text = lc.ApplicantName;*/
        }
    }
}