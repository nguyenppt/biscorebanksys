using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using Telerik.Web.UI.Calendar;

namespace BankProject
{
    public partial class AdvisingAndNegotiationLC : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        //Gen tam cac tab chi de lai Main can thi bat len va file note pad +: Advising

        protected void Page_Load(object sender, EventArgs e)
        {
            //BankProject.Controls.Commont.SetTatusFormControls(this.divCharge2.Controls, false);
            if (IsPostBack) return;
            LoadToolBar();
            tbAppAddr.Visible = false;
            divCharge2.Visible = false;
            divChargeInfo2.Visible = false;
            if (Request.QueryString["IsAuthorize"] != null && Request.QueryString["LCCode"] != null)
            {
                tbEssurLCCode.Text = Request.QueryString["LCCode"].ToString();
                fAmendment.Visible = false;
                LoadTabMt700();
                LoadTabMt740();
                LoadLC();
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }
            else if (Request.QueryString["IsAmendment"] != null)
            {
                tbEssurLCCode.Text = Request.QueryString["LCCode"].ToString();
                tbEssurLCCode.Enabled = false;
                fAmendment.Visible = true;
                LoadTabMt700();
                LoadTabMt740();
                LoadLC();
            }
            else
            {
                fAmendment.Visible = false;
                DataSet ds = DataProvider.DataTam.B_ISSURLC_GetNewID();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tbEssurLCCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                }

                DataSet vatno = DataProvider.Database.B_BMACODE_GetNewSoTT("VATNO");
                //tbVatNo.Text = vatno.Tables[0].Rows[0]["SoTT"].ToString();
                //TextBox4.Text = DateTime.Now.ToString("");
            }

            rcbLCType.Items.Clear();
            rcbLCType.Items.Add(new RadComboBoxItem(""));
            rcbLCType.DataTextField = "LCTYPE";
            rcbLCType.DataValueField = "LCTYPE"; 
            rcbLCType.DataSource = DataProvider.DataTam.B_BLCTYPES_GetAll();
            rcbLCType.DataBind();

            rcbCommodity.Items.Clear();
            rcbCommodity.Items.Add(new RadComboBoxItem(""));
            rcbCommodity.DataTextField = "Name";
            rcbCommodity.DataValueField = "ID";
            rcbCommodity.DataSource = DataProvider.DataTam.B_BCOMMODITY_GetAll();
            rcbCommodity.DataBind();


            DataSet dsc = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            rcbBeneficiaryCustNo.DataSource = dsc;
            rcbBeneficiaryCustNo.DataTextField = "CustomerName";
            rcbBeneficiaryCustNo.DataValueField = "CustomerID";
            rcbBeneficiaryCustNo.DataBind();

            //rcChargeAcct.DataSource = dsc;
            //rcChargeAcct.DataTextField = "BankCode";
            //rcChargeAcct.DataValueField = "ID";
            //rcChargeAcct.DataBind();

        }
        protected void rcChargeAcct_SelectIndexChange(object sender, EventArgs e)
        {
            //lblCommodity.Text = rcCommodity.SelectedItem.Attributes["Name"].ToString();
        }
        protected void rcChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["ID"] = row["ID"].ToString();
            e.Item.Attributes["CBank"] = row["CBank"].ToString();
        }
        protected void rcbBeneficiaryCustNo_SelectIndexChange(object sender, EventArgs e)
        {
            //lblCustomer.Text = rcbBeneficiaryCustNo.SelectedValue.ToString();
            tbAppAddr.Visible = true;
            //tbApplicant50.Text = rcbBeneficiaryCustNo.SelectedItem.Text.ToString();
            //lblApplicant50.Text = rcbBeneficiaryCustNo.SelectedValue.ToString();
            tbBeneficiaryAddr.Text = rcbBeneficiaryCustNo.SelectedValue.ToString();
            tbBeneficiaryAddr2.Text = rcbBeneficiaryCustNo.SelectedValue == "000001" ? "AP TRUNG BINH II, VINH TRACH" : "QUAN 5, TP. HCM";
            tbBeneficiaryAddr3.Text = rcbBeneficiaryCustNo.SelectedValue == "000001" ? "THOAI SON" : "TINH DONG NAI";
        }
        //protected void rcCommodity_SelectIndexChange(object sender, EventArgs e)
        //{
        //    lblCommodity.Text = rcCommodity.SelectedItem.Attributes["Name"].ToString();
        //}
        protected void rcbLCType_SelectIndexChange(object sender, EventArgs e)
        {
            lblLCType.Text = rcbLCType.SelectedItem.Attributes["Description"].ToString();
        }
        protected void rcCommodity_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["ID"] = row["ID"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }
        protected void rcbLCType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["LCTYPE"] = row["LCTYPE"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
            e.Item.Attributes["Category"] = row["Category"].ToString();
        } 
        private void LoadToolBar()
        {

            RadToolBar1.FindItemByValue("btCommitData").Enabled = Request.QueryString["IsAuthorize"] == null;
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = Request.QueryString["IsAuthorize"] != null;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                DataProvider.Database.B_ADVISING_Insert(tbEssurLCCode.Text, rcbLCType.SelectedValue, tbLCNumber.Text, rcbBeneficiaryCustNo.SelectedValue,
                        tbBeneficiaryAddr.Text, rcbBeneficiaryAcct.SelectedValue, rcbIssuingBankNo.SelectedValue, tbIssuingBankAddr.Text, rcbIssBankAcct.SelectedValue,
                        rcbApplicantNo.SelectedValue, tbApplicantAddr.Text, tbApplicantBank.Text, tbReimbBankRef.Text, rcbReimbBankNo.SelectedValue,
                        tbReimbBankAddr.Text, tbAdvThruAddr.Text, tbAdvThruAddr.Text, rcbAvailableWithCustno.SelectedValue, tbAvailableWithAddr.Text,
                        rcbCurrency.SelectedValue, tbAmount.Text, tbToleranceIncrease.Text, tbToleranceDecrease.Text, tbIssuingDate.SelectedDate.ToString(), tbExpiryDate.SelectedDate.ToString(),
                        tbExpiryDate.SelectedDate.ToString(), tbContingentExpiryDate.SelectedDate.ToString(), rcbCommodity.SelectedValue, "", tbGenerateDelivery.Text, this.UserId.ToString());

				// insert tab MT700 - 740
	            InsertTabMt700();
	            InsertTabMt740();
                if (tbChargeCode.Text != "")
                {
                    DataProvider.Database.B_ADVISING_CHARGES_Insert(tbEssurLCCode.Text, cmbWaiveCharges.SelectedValue, tbChargeCode.SelectedValue, rcbChargeAcct.SelectedItem.Text, tbChargePeriod.Text,
                        rcbChargeCcy.SelectedValue, tbExcheRate.Text, tbChargeAmt.Text, rcbPartyCharged.SelectedValue, rcbOmortCharge.SelectedValue, "", "",
                        rcbChargeStatus.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode.Text, lblTaxCcy.Text, lblTaxAmt.Text, "", "", "1");
                }
                
                //Response.Redirect(EditUrl("reviewlist"));
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
            }

            if (commandName == "Preview")
            {
                Response.Redirect(EditUrl("reviewlist"));
            }

            if (commandName == "authorize")
            {
                DataProvider.Database.B_ADVISING_Authorize(tbEssurLCCode.Text);
                Response.Redirect(EditUrl("reviewlist"));
            }
        }

        protected void tbContingentExpiry_TextChanged(object sender, EventArgs e)
        {
            //tbContingentExpiry.Text = DateTime.ParseExact(tbContingentExpiry.Text, "dd MMM yyyy", null).AddDays(30).ToString();
        }

        protected void rcbChargeStatus_SelectIndexChange(object sender, EventArgs e)
        {
            lblChargeStatus.Text = rcbChargeStatus.SelectedValue.ToString();
        }
       
        protected void rcbPartyCharged_SelectIndexChange(object sender, EventArgs e)
        {
            lblPartyCharged.Text = rcbPartyCharged.SelectedValue.ToString();
        }
        protected void rcbChargeAcct_SelectIndexChange(object sender, EventArgs e)
        {
            lblChargeAcct.Text = "TKTT VND " + rcbChargeAcct.SelectedValue.ToString();
        }
      
        protected void tbWaiveCharges_TextChanged(object sender, EventArgs e)
        {
            if (cmbWaiveCharges.SelectedValue.ToUpper() == "NO")
            {
                tbChargeAmt.Visible = true;
                tbChargeCode.Visible = true;
                tbChargePeriod.Visible = true;
                tbChargeRemarks.Visible = true;
                tbExcheRate.Visible = true;
                tbVatNo.Visible = true;
                rcbChargeAcct.Visible = true;
                rcbChargeStatus.Visible = true;
                rcbOmortCharge.Visible = true;
                rcbPartyCharged.Visible = true;
                btThem.Visible = true;
                rcbChargeCcy.Visible = true;
                tbChargeCode.Focus();
            }
            else if (cmbWaiveCharges.SelectedValue.ToUpper() == "YES")
            {
                btThem.Visible = false;
                tbChargeAmt.Visible = false;
                tbChargeCode.Visible = false;
                tbChargePeriod.Visible = false;
                tbChargeRemarks.Visible = false;
                tbExcheRate.Visible = false;
                tbVatNo.Visible = false;
                rcbChargeAcct.Visible = false;
                rcbChargeStatus.Visible = false;
                rcbOmortCharge.Visible = false;
                rcbPartyCharged.Visible = false;
                rcbChargeCcy.Visible = false;
            }
        }
        protected void rcbChargeStatus2_SelectIndexChange(object sender, EventArgs e)
        {
            lblChargeStatus2.Text = rcbChargeStatus2.SelectedValue.ToString();
        }
        protected void rcbPartyCharged2_SelectIndexChange(object sender, EventArgs e)
        {
            lblPartyCharged2.Text = rcbPartyCharged2.SelectedValue.ToString();
        }
        protected void rcbChargeAcct2_SelectIndexChange(object sender, EventArgs e)
        {
            lblChargeAcct2.Text = "TKTT VND " + rcbChargeAcct2.SelectedValue.ToString();
        }
       
        protected void tbChargeAmt_TextChanged(object sender, EventArgs e)
        {
            double tigia = double.Parse(tbExcheRate.Text);
            double sotien = double.Parse(tbChargeAmt.Text);

            sotien = sotien * tigia;
            //sotien = sotien / 1.1;
            float kq = (float)(sotien / 1.1);
            tbChargeAmt.Text = kq.ToString();
            lblTaxAmt.Text = String.Format("{0:#,##0}", (float)(sotien - kq));

            lblTaxCode.Text = "81      10% VAT on Charge";
            lblTaxCcy.Text = rcbChargeCcy.SelectedValue;
        }
        protected void tbChargeAmt2_TextChanged(object sender, EventArgs e)
        {
            double tigia = double.Parse(tbExcheRate2.Text);
            double sotien = double.Parse(tbChargeAmt2.Text);

            sotien = sotien * tigia;
            //sotien = sotien / 1.1;
            float kq = (float)(sotien / 1.1);
            tbChargeAmt2.Text = kq.ToString();
            lblTaxAmt2.Text = String.Format("{0:#,##0}", (float)(sotien - kq));

            lblTaxCode2.Text = "81      10% VAT on Charge";
            lblTaxCcy2.Text = rcbChargeCcy2.SelectedValue;
        }
        
        protected void tbAmount_TextChanged(object sender, EventArgs e)
        {
            //string s = tbAmount.Text.ToString().re
            //string numtext = sender.ToString().Substring(sender.ToString().Length - 0, 1);
        }
        protected void btThem_Click(object sender, ImageClickEventArgs e)
        {
            divCharge2.Visible = true;
            divChargeInfo2.Visible = true;
        }

		#region tab MT700
        //protected void comboRevivingBank_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    tbRevivingBankName.Text = comboRevivingBank.SelectedValue;
        //}
        //protected void comboFormOfDocumentaryCredit_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    tbFormOfDocumentaryCreditName.Text = comboFormOfDocumentaryCredit.SelectedValue;
        //}
        ////protected void comboAvailableRule_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        ////{
        ////    tbAvailableRuleName.Text = comboAvailableRule.SelectedValue;
        ////}
        //protected void combo42DDrawee_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    tb42DDraweeName.Text = combo42DDrawee.SelectedValue;
        //}
        //protected void comboAvailableWithBy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    tbAvailableWithByName.Text = comboAvailableWithBy.SelectedValue;
        //}
        protected void LoadTabMt700()
        {
            //var dsMT700 = DataProvider.SQLData.B_BNORMAILLCMT700_GetByNormalLCCode(tbEssurLCCode.Text.Trim());
            //if (dsMT700 != null && dsMT700.Tables.Count > 0 && dsMT700.Tables[0].Rows.Count > 0)
            //{
            //    var drMT700 = dsMT700.Tables[0].Rows[0];

            //    comboRevivingBank.SelectedValue = drMT700["RevevingBank"].ToString();
            //    tbRevivingBankName.Text = drMT700["RevevingBank"].ToString();

            //    tbBaquenceOfTotal.Text = drMT700["BounceOfTotal"].ToString();
            //    comboFormOfDocumentaryCredit.SelectedValue = drMT700["FormOfDocumentaryCredit"].ToString();
            //    tbFormOfDocumentaryCreditName.Text = comboFormOfDocumentaryCredit.SelectedValue;

            //    if (drMT700["DateOfIssue"].ToString().IndexOf("1/1/1900") == -1)
            //    {
            //        dteDateOfIssue.SelectedDate = Convert.ToDateTime(drMT700["DateOfIssue"].ToString());
            //    }
            //    if (drMT700["Date31D"].ToString().IndexOf("1/1/1900") == -1)
            //    {
            //        dteMT700DateAndPlaceOfExpiry.SelectedDate = Convert.ToDateTime(drMT700["Date31D"].ToString());
            //    }
            //    tbPlaceOfExpiry.Text = drMT700["PlaceOfExpiry31D"].ToString();

            //    tbApplicantBank.Text = drMT700["ApplicantBank51"].ToString();
            //    tbApplicant50.Text = drMT700["Applicant50"].ToString();

            //    comBeneficiary59.SelectedValue = drMT700["DocumentaryCusNo"].ToString();
            //    tbDocumentary_NameAndAddress.Text = drMT700["DocumentaryNameAddress"].ToString();

            //    comboCurrencyCode32B.SelectedValue = drMT700["CurrencyCode32B"].ToString();
            //    if (!string.IsNullOrEmpty(drMT700["Amount32B"].ToString()))
            //    {
            //        numAmount.Value = Convert.ToDouble(drMT700["Amount32B"].ToString());
            //    }
            //    else
            //    {
            //        numAmount.Value = 0;
            //    }


            //    if (!string.IsNullOrEmpty(drMT700["PercentCreditAmount39A1"].ToString()))
            //    {
            //        numPercentCreditAmount1.Value = Convert.ToDouble(drMT700["PercentCreditAmount39A1"].ToString());
            //    }
            //    else
            //    {
            //        numPercentCreditAmount1.Value = 0;
            //    }

            //    if (!string.IsNullOrEmpty(drMT700["PercentCreditAmount39A2"].ToString()))
            //    {
            //        numPercentCreditAmount2.Value = Convert.ToDouble(drMT700["PercentCreditAmount39A2"].ToString());
            //    }
            //    else
            //    {
            //        numPercentCreditAmount2.Value = 0;
            //    }

            //    comboMaximumCreditAmount39B.SelectedValue = drMT700["MaximumCreditAmount39B"].ToString();
            //    tbAdditionalAmountComment.Text = drMT700["AdditionalAmountComment"].ToString();

            //    comboAvailableRule.SelectedValue = drMT700["AvailableRule40E"].ToString();
            //    tbAvailableRuleName.Text = comboAvailableRule.SelectedValue;

            //    comboAvailableWith.SelectedValue = drMT700["AvailableWith41A"].ToString();

            //    tbAvailableWithNameAddress.Text = drMT700["AvailableWithNameAddress"].ToString();

            //    tb42CDraftsAt.Text = drMT700["C42"].ToString();

            //    combo42DDrawee.SelectedValue = drMT700["D42"].ToString();
            //    tb42DDraweeName.Text = combo42DDrawee.SelectedValue;

            //    tbMixedPaymentDetails.Text = drMT700["MixedPaymentDetails"].ToString();

            //    tbDeferredPaymentDetails.Text = drMT700["DeferredPaymentDetails"].ToString();
            //    rcbPatialShipment.SelectedValue = drMT700["PatialShipment"].ToString();
            //    rcbTranshipment.SelectedValue = drMT700["Transhipment"].ToString();
            //    tbPlaceoftakingincharge.Text = drMT700["Placeoftakingincharge"].ToString();
            //    tbPortofloading.Text = drMT700["Portofloading"].ToString();
            //    tbPortofDischarge.Text = drMT700["PortofDischarge"].ToString();
            //    tbPlaceoffinalindistination.Text = drMT700["Placeoffinalindistination"].ToString();
            //    //tbLatesDateofShipment.SelectedDate = DateTime.Parse(drMT700["LatesDateofShipment"].ToString());
            //    tbShipmentPeriod.Text = drMT700["ShipmentPeriod"].ToString();
            //    tbDescrpofGoods.Text = drMT700["DescrpofGoods"].ToString();
            //    rcbDocsRequired.SelectedValue = drMT700["DocsRequired"].ToString();
            //    tbOrderDocs.Text = drMT700["OrderDocs"].ToString();
            //    tbAdditionalConditions.Text = drMT700["AdditionalConditions"].ToString();
            //    //,[AdditionalConditions]

            //    //LCType
            //    comboAvailableWithBy.SelectedValue = drMT700["AvailableWithBy"].ToString();
            //    tbAvailableWithByName.Text = comboAvailableWithBy.SelectedValue;
            //    if (tbAvailableWithByName.Text == "")
            //    {
            //        switch (drMT700["LCType"].ToString())
            //        {
            //            case "ILCP":
            //                tbAvailableWithByName.Text = "BY PAYMENT";
            //                comboAvailableWithBy.SelectedValue = "BY PAYMENT";
            //                break;
            //            case "ILCS":
            //                tbAvailableWithByName.Text = "BY NEGPTIATION";
            //                comboAvailableWithBy.SelectedValue = "BY NEGPTIATION";
            //                break;
            //        }
            //    }
            //}
            //else
            //{
            //    comboRevivingBank.SelectedValue = string.Empty;
            //    tbAvailableRuleName.Text = string.Empty;

            //    tbBaquenceOfTotal.Text = string.Empty;
            //    comboFormOfDocumentaryCredit.SelectedValue = string.Empty;
            //    tbFormOfDocumentaryCreditName.Text = string.Empty;
            //    dteDateOfIssue.SelectedDate = null;
            //    dteMT700DateAndPlaceOfExpiry.SelectedDate = null;
            //    tbPlaceOfExpiry.Text = string.Empty;

            //    tbApplicantBank.Text = string.Empty;
            //    tbApplicant50.Text = string.Empty;

            //    comBeneficiary59.SelectedValue = string.Empty;
            //    tbDocumentary_NameAndAddress.Text = string.Empty;

            //    comboCurrencyCode32B.SelectedValue = string.Empty;
            //    numAmount.Value = 0;
            //    numPercentCreditAmount1.Value = 0;
            //    numPercentCreditAmount2.Value = 0;

            //    comboMaximumCreditAmount39B.SelectedValue = string.Empty;
            //    tbAdditionalAmountComment.Text = string.Empty;

            //    comboAvailableRule.SelectedValue = string.Empty;
            //    tbAvailableRuleName.Text = string.Empty;

            //    comboAvailableWith.SelectedValue = string.Empty;

            //    tbAvailableWithNameAddress.Text = string.Empty;

            //    tb42CDraftsAt.Text = string.Empty;

            //    combo42DDrawee.SelectedValue = string.Empty;
            //    tb42DDraweeName.Text = string.Empty;

            //    tbMixedPaymentDetails.Text = string.Empty;

            //    //LCType
            //    comboAvailableWithBy.SelectedValue = string.Empty;
            //    tbAvailableWithByName.Text = string.Empty;
            //}
        }
        protected void InsertTabMt700()
        {
        //    DataProvider.SQLData.B_BNORMAILLCMT700_Insert(tbEssurLCCode.Text.Trim(), comboRevivingBank.SelectedValue, tbBaquenceOfTotal.Text.Trim(), comboFormOfDocumentaryCredit.SelectedValue,
        //        dteDateOfIssue.SelectedDate.ToString(), dteMT700DateAndPlaceOfExpiry.SelectedDate.ToString(), tbPlaceOfExpiry.Text.Trim(), tbApplicantBank.Text.Trim(), tbApplicant50.Text.Trim(),
        //        comBeneficiary59.SelectedValue, tbDocumentary_NameAndAddress.Text.Trim(), comboCurrencyCode32B.SelectedValue, numAmount.Value.ToString(),
        //        numPercentCreditAmount1.Value.ToString(), numPercentCreditAmount2.Value.ToString(), tbAdditionalAmountComment.Text, comboAvailableRule.Text,
        //        comboAvailableWith.SelectedValue, tbAvailableWithNameAddress.Text.Trim(), tb42CDraftsAt.Text.Trim(),
        //        combo42DDrawee.SelectedValue, comboMaximumCreditAmount39B.SelectedValue, tbMixedPaymentDetails.Text.Trim(), comboAvailableWithBy.SelectedValue
        //        ,tbDeferredPaymentDetails.Text,rcbPatialShipment.SelectedValue,rcbTranshipment.SelectedValue,tbPlaceoftakingincharge.Text,tbPortofloading.Text,tbPortofDischarge.Text
        //        ,tbPlaceoffinalindistination.Text,tbLatesDateofShipment.SelectedDate.ToString(),tbShipmentPeriod.Text,tbDescrpofGoods.Text,rcbDocsRequired.SelectedValue,
        //        tbOrderDocs.Text,tbAdditionalConditions.Text);
        }
		#endregion

		#region MT740
        //protected void comboReceivingBankMT740_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    tbReceivingBankMT740Name.Text = comboReceivingBankMT740.SelectedValue;
        //}
        //protected void comboDrawee42D_MT740_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    tbDraweeName42D_MT740.Text = comboDrawee42D_MT740.SelectedValue;
        //}
        protected void LoadTabMt740 ()
        {
        //    var dsMT740 = DataProvider.SQLData.B_BNORMAILLCMT740_GetByNormalLCCode(tbEssurLCCode.Text.Trim());
        //    if (dsMT740 != null && dsMT740.Tables.Count > 0 && dsMT740.Tables[0].Rows.Count > 0)
        //    {
        //        var drRow = dsMT740.Tables[0].Rows[0];

        //        comGenerate.SelectedValue = drRow["Generate"].ToString();

        //        comboReceivingBankMT740.SelectedValue = drRow["ReceivingBank"].ToString();
        //        tbReceivingBankMT740Name.Text = comboReceivingBankMT740.SelectedValue;

        //        tbDocumentaryCreditNumber.Text = drRow["DocumentaryCreditNumber"].ToString();

        //        if (drRow["Date31D"].ToString().IndexOf("1/1/1900") == -1)
        //        {
        //            dte31DDate.SelectedDate = Convert.ToDateTime(drRow["Date31D"].ToString());
        //        }
        //        tb31DPlaceOfExpiry.Text = drRow["PlaceOfExpiry"].ToString();
        //        comboBeneficial.SelectedValue = drRow["Beneficial"].ToString();
        //        tbBeneficialNameAddress.Text = drRow["BeneficialNameAndAddress"].ToString();
        //        comboCredit32USD.SelectedValue = drRow["CreditMoneyType32"].ToString();

        //        if (!string.IsNullOrEmpty(drRow["CreditAmount32"].ToString()))
        //        {
        //            numUSDAmount.Value = Convert.ToDouble(drRow["CreditAmount32"].ToString());
        //        }
        //        else
        //        {
        //            numUSDAmount.Value = 0;
        //        }

        //        comboAvailableWith_MT740.SelectedValue = drRow["AvailableWith41A"].ToString();
        //        tbAvailableNameAddr_MT740.Text = drRow["AvailableNameAndAddress"].ToString();
        //        tb42CDraff.Text = drRow["Draffy42C"].ToString();

        //        comboDrawee42D_MT740.SelectedValue = drRow["Drawee42D"].ToString();
        //        tbDraweeName42D_MT740.Text = comboDrawee42D_MT740.SelectedValue;

        //        tbNameAddress.Text = drRow["NameAddress"].ToString(); // chua co trong store
        //        comboBankChange.SelectedValue = drRow["BankChanges"].ToString();
        //        tbSenderReceiverInformation.Text = drRow["SenderToReceiverIinformation"].ToString();
        //    }
        //    else
        //    {
        //        comGenerate.SelectedValue = string.Empty;

        //        comboReceivingBankMT740.SelectedValue = string.Empty;
        //        tbReceivingBankMT740Name.Text = string.Empty;

        //        tbDocumentaryCreditNumber.Text = string.Empty;
        //        dte31DDate.SelectedDate = null;
        //        tb31DPlaceOfExpiry.Text = string.Empty;
        //        comboBeneficial.SelectedValue = string.Empty;
        //        tbBeneficialNameAddress.Text = string.Empty;
        //        comboCredit32USD.SelectedValue = string.Empty;

        //        numUSDAmount.Value = 0;

        //        comboAvailableWith_MT740.SelectedValue = string.Empty;
        //        tbAvailableNameAddr_MT740.Text = string.Empty;
        //        tb42CDraff.Text = string.Empty;

        //        comboDrawee42D_MT740.SelectedValue = string.Empty;
        //        tbDraweeName42D_MT740.Text = string.Empty;

        //        tbNameAddress.Text = string.Empty;
        //        comboBankChange.SelectedValue = string.Empty;
        //        tbSenderReceiverInformation.Text = string.Empty;
        //    }
        }
        protected void InsertTabMt740()
        {
        //    DataProvider.SQLData.B_BNORMAILLCMT740_Insert(tbEssurLCCode.Text.Trim(), comGenerate.SelectedValue, comboReceivingBankMT740.SelectedValue,
        //        tbDocumentaryCreditNumber.Text.Trim(), dte31DDate.SelectedDate.ToString(), tb31DPlaceOfExpiry.Text.Trim(), comboBeneficial.SelectedValue, tbBeneficialNameAddress.Text.Trim(),
        //        comboCredit32USD.SelectedValue, numUSDAmount.Value.ToString(), comboAvailableWith_MT740.SelectedValue, tbAvailableNameAddr_MT740.Text.Trim(), tb42CDraff.Text.Trim(),
        //        comboDrawee42D_MT740.SelectedValue, comboBankChange.SelectedValue, tbSenderReceiverInformation.Text.Trim(), tbNameAddress.Text.Trim());
        }
		#endregion
        //protected void tbEssurLCCode_OnTextChanged(object sender, EventArgs e)
        //{
        //    //OnTextChanged="tbEssurLCCode_OnTextChanged" AutoPostBack="True"
        //    LoadTabMt700();
        //    LoadTabMt740();
        //}
        protected void btSearch_Click(object sender, EventArgs e)
        {
            LoadTabMt700();
            LoadTabMt740();
            LoadLC();
        }

        private void LoadLC()
        {
            DataSet ds = DataProvider.Database.B_ADVISING_GetbyLCCode(tbEssurLCCode.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rcbLCType.SelectedValue = ds.Tables[0].Rows[0]["LCType"].ToString();
                lblLCType.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                tbLCNumber.Text = ds.Tables[0].Rows[0]["LCNumber"].ToString();
                rcbBeneficiaryCustNo.SelectedValue = ds.Tables[0].Rows[0]["BeneficiaryCustNo"].ToString();
                tbBeneficiaryAddr.Text = ds.Tables[0].Rows[0]["BeneficiaryAddr"].ToString();
                rcbBeneficiaryAcct.SelectedValue = ds.Tables[0].Rows[0]["BeneficiaryAcct"].ToString();
                rcbIssuingBankNo.SelectedValue = ds.Tables[0].Rows[0]["IssuingBankNo"].ToString();
                tbIssuingBankAddr.Text = ds.Tables[0].Rows[0]["IssBankAddr"].ToString();
                rcbIssBankAcct.SelectedValue = ds.Tables[0].Rows[0]["IssBankAcct"].ToString();
                rcbApplicantNo.SelectedValue = ds.Tables[0].Rows[0]["ApplicantNo"].ToString();
                tbApplicantAddr.Text = ds.Tables[0].Rows[0]["ApplicantAddr"].ToString();
                tbApplicantBank.Text = ds.Tables[0].Rows[0]["ApplicantBank"].ToString();
                tbReimbBankRef.Text = ds.Tables[0].Rows[0]["ReimbBankRef"].ToString();
                rcbReimbBankNo.SelectedValue = ds.Tables[0].Rows[0]["ReimbBankNo"].ToString();
                tbReimbBankAddr.Text = ds.Tables[0].Rows[0]["ReimbBankAddr"].ToString();
                rcbAdviceThruBank.SelectedValue = ds.Tables[0].Rows[0]["AdviceThruBank"].ToString();
                tbAdvThruAddr.Text = ds.Tables[0].Rows[0]["AdvThruAddr"].ToString();
                rcbAvailableWithCustno.SelectedValue = ds.Tables[0].Rows[0]["AvailablewithCustno"].ToString();
                tbAvailableWithAddr.Text = ds.Tables[0].Rows[0]["AvailablewithAddr"].ToString();
                rcbCurrency.SelectedValue = ds.Tables[0].Rows[0]["Currency"].ToString();
                tbAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                tbToleranceIncrease.Text = ds.Tables[0].Rows[0]["ToleranceIncr"].ToString();
                tbToleranceDecrease.Text = ds.Tables[0].Rows[0]["ToleranceDecr"].ToString();
                if (ds.Tables[0].Rows[0]["IssuingDate"] != null && ds.Tables[0].Rows[0]["IssuingDate"] != DBNull.Value)
                     tbIssuingDate.SelectedDate =DateTime.Parse(ds.Tables[0].Rows[0]["IssuingDate"].ToString());

                if (ds.Tables[0].Rows[0]["ExpiryDate"] != null && ds.Tables[0].Rows[0]["ExpiryDate"] != DBNull.Value)
                     tbExpiryDate.SelectedDate =DateTime.Parse(ds.Tables[0].Rows[0]["ExpiryDate"].ToString());

                rcbExpiryPlace.SelectedValue = ds.Tables[0].Rows[0]["ExpiryPlace"].ToString();

                if (ds.Tables[0].Rows[0]["ContingentExpiryDate"] != null && ds.Tables[0].Rows[0]["ContingentExpiryDate"] != DBNull.Value)
                     tbContingentExpiryDate.SelectedDate =DateTime.Parse(ds.Tables[0].Rows[0]["ContingentExpiryDate"].ToString());

                rcbCommodity.SelectedValue = ds.Tables[0].Rows[0]["Commodity"].ToString();
                //rcbAdvisedBySacombank.Text = ds.Tables[0].Rows[0]["AdvisedBySacombank"].ToString();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                cmbWaiveCharges.SelectedValue = ds.Tables[1].Rows[0]["WaiveCharges"].ToString();
                rcbChargeAcct.SelectedValue = ds.Tables[1].Rows[0]["CustomerName"].ToString();
                lblChargeAcct.Text = ds.Tables[1].Rows[0]["CustomerName"].ToString();
                tbChargePeriod.Text = ds.Tables[1].Rows[0]["ChargePeriod"].ToString();
                rcbChargeCcy.SelectedValue = ds.Tables[1].Rows[0]["ChargeCcy"].ToString();
                tbExcheRate.Text = ds.Tables[1].Rows[0]["ExchRate"].ToString();
                tbChargeAmt.Text = ds.Tables[1].Rows[0]["ChargeAmt"].ToString();
                rcbPartyCharged.SelectedValue = ds.Tables[1].Rows[0]["PartyCharged"].ToString();
                lblPartyCharged.Text = ds.Tables[1].Rows[0]["PartyCharged"].ToString();
                rcbOmortCharge.SelectedValue = ds.Tables[1].Rows[0]["OmortCharges"].ToString();
                rcbChargeStatus.SelectedValue = ds.Tables[1].Rows[0]["ChargeStatus"].ToString();
                lblChargeStatus.Text = ds.Tables[1].Rows[0]["ChargeStatus"].ToString();

                tbChargeRemarks.Text = ds.Tables[1].Rows[0]["ChargeRemarks"].ToString();
                tbVatNo.Text = ds.Tables[1].Rows[0]["VATNo"].ToString();
                lblTaxCode.Text = ds.Tables[1].Rows[0]["TaxCode"].ToString();
                lblTaxCcy.Text = ds.Tables[1].Rows[0]["TaxCcy"].ToString();
                lblTaxAmt.Text = ds.Tables[1].Rows[0]["TaxAmt"].ToString();

                tbChargeCode.SelectedValue = ds.Tables[1].Rows[0]["Chargecode"].ToString();
            }

            //if (ds.Tables[2].Rows.Count > 0)
            //{
                //divCharge2.Visible = true;
                //divChargeInfo2.Visible = true;
                //rcbChargeAcct2.SelectedValue = ds.Tables[2].Rows[0]["CustomerName"].ToString();
                //lblChargeAcct2.Text = ds.Tables[2].Rows[0]["CustomerName"].ToString();
                //tbChargePeriod2.Text = ds.Tables[2].Rows[0]["ChargePeriod"].ToString();
                //rcbChargeCcy2.SelectedValue = ds.Tables[2].Rows[0]["ChargeCcy"].ToString();
                //tbExcheRate2.Text = ds.Tables[2].Rows[0]["ExchRate"].ToString();
                //tbChargeAmt2.Text = ds.Tables[2].Rows[0]["ChargeAmt"].ToString();
                //rcbPartyCharged2.SelectedValue = ds.Tables[2].Rows[0]["PartyCharged"].ToString();
                //lblPartyCharged2.Text = ds.Tables[2].Rows[0]["PartyCharged"].ToString();
                //rcbOmortCharges2.SelectedValue = ds.Tables[2].Rows[0]["OmortCharges"].ToString();
                //rcbChargeStatus2.SelectedValue = ds.Tables[2].Rows[0]["ChargeStatus"].ToString();
                //lblChargeStatus2.Text = ds.Tables[2].Rows[0]["ChargeStatus"].ToString();

                //lblTaxCode2.Text = ds.Tables[2].Rows[0]["TaxCode"].ToString();
                //lblTaxCcy2.Text = ds.Tables[2].Rows[0]["TaxCcy"].ToString();
                //lblTaxAmt2.Text = ds.Tables[2].Rows[0]["TaxAmt"].ToString();

                //tbChargecode2.SelectedValue = ds.Tables[2].Rows[0]["Chargecode"].ToString();
            //}
        }
	    
    }
}