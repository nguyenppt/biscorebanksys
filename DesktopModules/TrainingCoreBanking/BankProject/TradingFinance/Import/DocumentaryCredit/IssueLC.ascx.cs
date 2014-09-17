using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using System.Globalization;

namespace BankProject.TradingFinance.Import.DocumentaryCredit
{
    public partial class IssueLC : PortalModuleBase
    {
        protected const int TabIssueLCAddNew = 92;
        protected const int TabIssueLCAmend = 204;
        protected const int TabIssueLCCancel = 205;
        protected const int TabIssueLCClose = 264;
        //
        protected double Amount = 0;
        protected double Amount_Old = 0;
        protected double B4_AUT_Amount = 0;
        protected double TotalChargeAmt = 0;
        protected string Generate740 = string.Empty;
        protected string ReceivingBank_700 = string.Empty;
        protected string ReceivingBank_740 = string.Empty;

        protected string WaiveCharges = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            // default no use
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            // default no use

            divAmount.Visible = false;
            DataRow dataRow = null;

            LoadToolBar(false);
            InitDataSource();
            SetDefaultValue();

            if (Request.QueryString["CodeID"] != null)
            {
                txtCode.Text = Request.QueryString["CodeID"];
                LoadData(ref dataRow);
                SetVisibilityByStatus(ref dataRow);
                RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            }
            else
            {
                GenerateVAT();
                if (TabId == TabIssueLCAddNew)
                {
                    GenerateLCCode();
                }
            }

            if (TabId == TabIssueLCCancel) // Cancel LC
            {
                divCancelLC.Visible = true;
            }

            if (Request.QueryString["disable"] != null)
            {
                SetDisableByReview(false);
                LoadToolBar(true);
                RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            }
            if (TabId == TabIssueLCClose)
            {
                
            }

            Session["DataKey"] = txtCode.Text;
        }
        
        #region Text/Amount Change
        protected void ntSoTien_TextChanged(object sender, EventArgs e)
        {
            numAmount700.Value = ntSoTien.Value;
            tbcrTolerance.Focus();
        }
        
        protected void tbAvailableWithNo740_OnTextChanged(object sender, EventArgs e)
        {
            lblAvailableWithNoError740.Text = "";
            tbAvailableWithName740.Text = "";
            if (!string.IsNullOrEmpty(tbAvailableWithNo740.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(tbAvailableWithNo740.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    tbAvailableWithName740.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblAvailableWithNoError740.Text = "No found swiftcode";
                }
            }
        }
        
        protected void txtReceivingBankNo_OnTextChanged(object sender, EventArgs e)
        {
            CheckSwiftCodeExist();
        }

        protected void tbBeneficiaryNo740_OnTextChanged(object sender, EventArgs e)
        {
            tbBeneficiaryName740.Text = "";
            lblBeneficiaryError740.Text = "";
            if (!string.IsNullOrEmpty(tbBeneficiaryNo740.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(tbBeneficiaryNo740.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    tbBeneficiaryName740.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblBeneficiaryError740.Text = "No found swiftcode";
                }
            }
        }

        protected void txtBeneficiaryNo700_OnTextChanged(object sender, EventArgs e)
        {
            lblBeneficiaryNo700Error.Text = "";
            txtBeneficiaryName700.Text = "";
            if (!string.IsNullOrEmpty(txtBeneficiaryNo700.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtBeneficiaryNo700.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtBeneficiaryName700.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblBeneficiaryNo700Error.Text = "No found swiftcode";
                }
            }
        }

        protected void tbReimbBankName_tbReimbBankName(object sender, EventArgs e)
        {
            txtAdviseThroughBankName700.Text = tbReimbBankName.Text;
        }

        protected void tbReimbBankAddr1_OnTextChanged(object sender, EventArgs e)
        {
            tbReimbBankAddr700_1.Text = tbReimbBankAddr1.Text;
        }

        protected void tbReimbBankAddr2_OnTextChanged(object sender, EventArgs e)
        {
            tbReimbBankAddr700_2.Text = tbReimbBankAddr2.Text;
        }

        protected void tbReimbBankAddr3_OnTextChanged(object sender, EventArgs e)
        {
            tbReimbBankAddr700_3.Text = tbReimbBankAddr3.Text;
        }
        #endregion

        #region Combo SelectedIndexChanged/ ItemDataBound combo
        protected void comboDraweeCusNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
            e.Item.Attributes["CustomerName2"] = row["CustomerName2"].ToString();
        }

        protected void comboDrawee42D_MT740_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //tbDraweeName42D_MT740.Text = comboDrawee42D_MT740.SelectedValue;
        }

        protected void comboAvailableWithBy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tbAvailableWithByName.Text = comboAvailableWithBy.SelectedValue;
        }

        protected void tbAdviseThruNo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (e.Value == "")
            //    //tbAdviseThruAddr.SetEnable(true);
            //else
            //{
            //    //tbAdviseThruAddr.SetEnable(false);
            //    //tbAdviseThruAddr.Clear();
            //    //tbAdviseThruAddr.ReLoadControl();
            //}
        }

        protected void rcbLCType_SelectIndexChange(object sender, EventArgs e)
        {
            lblLCType.Text = rcbLCType.SelectedItem.Attributes["Description"].ToString();
        }

        protected void rcCommodity_SelectIndexChange(object sender, EventArgs e)
        {
            lblCommodity.Text = rcCommodity.SelectedItem.Attributes["Name"].ToString();
        }

        protected void rcbLCType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["LCTYPE"] = row["LCTYPE"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
            e.Item.Attributes["Category"] = row["Category"].ToString();
        }

        protected void rcbApplicantID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerName"] = row["CustomerName2"].ToString();
            e.Item.Attributes["Address"] = row["Address"].ToString();
            e.Item.Attributes["IdentityNo"] = row["IdentityNo"].ToString();
            e.Item.Attributes["IssueDate"] = row["IssueDate"].ToString();
            e.Item.Attributes["IssuePlace"] = row["IssuePlace"].ToString();
            e.Item.Attributes["City"] = row["City"].ToString();
            e.Item.Attributes["Country"] = row["Country"].ToString();
        }

        protected void rcCommodity_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["ID"] = row["ID"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void comboBeneficiaryBankType_OnSelectedIndexChanged(object sender,
                                                                       RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_BeneficiaryBank();
        }
        
        // tab charge
        protected void comboWaiveCharges_OnSelectedIndexChanged(object sender,
                                                                RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (comboWaiveCharges.SelectedValue == "NO")
            {
                divACCPTCHG.Visible = true;
                divCABLECHG.Visible = true;
                divPAYMENTCHG.Visible = true;
            }
            else if (comboWaiveCharges.SelectedValue == "YES")
            {
                divACCPTCHG.Visible = false;
                divCABLECHG.Visible = false;
                divPAYMENTCHG.Visible = false;
            }
        }

        protected void rcbChargeStatus_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus.Text = rcbChargeStatus.SelectedValue.ToString();
        }
        protected void rcbChargeStatus2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus2.Text = rcbChargeStatus2.SelectedValue.ToString();
        }
        protected void tbChargeAmt_TextChanged(object sender, EventArgs e)
        {
            chargeAmt_Changed();
        }
        protected void tbChargeAmt2_TextChanged(object sender, EventArgs e)
        {
            chargeAmt2_Changed();
        }
        protected void tbChargeAmt3_TextChanged(object sender, EventArgs e)
        {
            chargeAmt3_Changed();
        }
        
        protected void chargeAmt_Changed()
        {
            //double sotien = 0;

            //if (tbChargeAmt.Value > 0)
            //{
            //    sotien = double.Parse(tbChargeAmt.Value.ToString());
            //    sotien = sotien * 0.1;
            //}

            //lblTaxAmt.Text = String.Format("{0:C}", sotien).Replace("$", "") ;
            //lblTaxCode.Text = "81      10% VAT on Charge";
        }
        protected void chargeAmt2_Changed()
        {
            //double sotien = 0;

            //if (tbChargeAmt2.Value > 0)
            //{
            //    sotien = Double.Parse(tbChargeAmt2.Value.ToString());
            //    sotien = sotien * 0.1;
            //}
            //lblTaxAmt2.Text = String.Format("{0:C}", sotien).Replace("$", "");
            //lblTaxCode2.Text = "81      10% VAT on Charge";
        }
        protected void chargeAmt3_Changed()
        {
            //double sotien = 0;
            //if (tbChargeAmt3.Value > 0)
            //{
            //    sotien = Double.Parse(tbChargeAmt3.Value.ToString());
            //    sotien = sotien * 0.1;
            //}
            //lblTaxAmt3.Text = String.Format("{0:C}", sotien).Replace("$", "");
            //lblTaxCode3.Text = "81      10% VAT on Charge";
        }
        
        protected void rcbPartyCharged_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void rcbPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];
        }
        protected void rcbPartyCharged2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];
        }
        protected void rcbPartyCharged3_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];
        }

        protected void SwiftCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["BankName"] = row["BankName"].ToString();
            e.Item.Attributes["City"] = row["City"].ToString();
            e.Item.Attributes["Country"] = row["Country"].ToString();
            e.Item.Attributes["Continent"] = row["Continent"].ToString();
            e.Item.Attributes["SwiftCode"] = row["SwiftCode"].ToString();
        }

        //protected void rcbAdviseBankNo_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    tbAdviseBankName.Text = rcbAdviseBankNo.SelectedItem.Attributes["BankName"];
            
        //}
        
        protected void rcbApplicantBankType700_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_ApplicantBankType700();
        }
          
        protected void rcbAvailableWithType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_AvailableWithType();

            rcbAvailableWithType740.SelectedValue = rcbAvailableWithType.SelectedValue;
        }

        protected void rcbAvailableWithType740_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_AvailableWithType740();
        }

       
        protected void rcbBeneficiaryType740_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_Beneficiary740();
        }

        protected void comGenerate_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GenerateMT740();
        }
        
        protected void rcbChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void rcbChargeCcy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct);
        }

        protected void rcbChargeCcy2_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct2);
        }

        protected void rcbChargeCcy3_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct3);
        }

        protected void comboReimbBankType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_ReimbBankType();        
        }
        
        protected void comboAvailableWithNo_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tbAvailableWithName.Text = comboAvailableWithNo.SelectedItem != null ? comboAvailableWithNo.SelectedItem.Attributes["BankName"] : "";

            tbAvailableWithNo740.Text = comboAvailableWithNo.SelectedValue;
            tbAvailableWithName740.Text = tbAvailableWithName.Text;
        }

        protected void rcbAdviseThruType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_AdviseThruType();
        }

        protected void comboDraweeCusType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_DraweeCusType700();
        }

        protected void comboAvailableRule_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblApplicableRule740.Text = comboAvailableRule.SelectedValue;
        }

        protected void comboDraweeCusNo700_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtDraweeCusName.Text = comboDraweeCusNo700.SelectedItem != null
                                        ? comboDraweeCusNo700.SelectedItem.Attributes["BankName"]
                                        : "";
        }

        protected void comboGenerateMT747_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GenerateMT747();
        }
        #endregion
        
        protected void InitDataSource()
        {
            LoadPartyCharged();
            LoadChargeCode();

            var dsc = bd.SQLData.B_BCUSTOMERS_OnlyBusiness();
            bc.Commont.initRadComboBox(ref rcbApplicantID, "CustomerName", "CustomerID", dsc);
            //bc.Commont.initRadComboBox(ref rcbApplicant700, "CustomerName", "CustomerID", dsc);
            bc.Commont.initRadComboBox(ref rcbAccountOfficer, "Description", "Code", bd.SQLData.B_BACCOUNTOFFICER_GetAll());

            var dsAdviseBankNo = bd.SQLData.B_BBANKSWIFTCODE_GetByType("RMA");
            bc.Commont.initRadComboBox(ref rcbAdviseBankNo, "SwiftCode", "SwiftCode", dsAdviseBankNo);
                        
            var dsNostro = bd.SQLData.B_BBANKSWIFTCODE_GetByType("Nostro");
            bc.Commont.initRadComboBox(ref rcbReimbBankNo, "SwiftCode", "SwiftCode", dsNostro);
            bc.Commont.initRadComboBox(ref rcbReimbBankNo700, "SwiftCode", "SwiftCode", dsNostro);
            //bc.Commont.initRadComboBox(ref comboRevivingBank, "SwiftCode", "SwiftCode", dsNostro);
            bc.Commont.initRadComboBox(ref comboReimbBankNo_747, "SwiftCode", "SwiftCode", dsNostro);
            var dsall = bd.SQLData.B_BBANKSWIFTCODE_GetByType("all");

            bc.Commont.initRadComboBox(ref rcbAdviseThruNo, "SwiftCode", "SwiftCode", dsall);
            bc.Commont.initRadComboBox(ref comboAdviseThroughBankNo700, "SwiftCode", "SwiftCode", dsall);
            bc.Commont.initRadComboBox(ref comboAvailableWithNo, "SwiftCode", "SwiftCode", dsall);
            bc.Commont.initRadComboBox(ref comboDraweeCusNo700, "SwiftCode", "SwiftCode", dsall);

            bc.Commont.initRadComboBox(ref rcbLCType, "LCTYPE", "LCTYPE", bd.DataTam.B_BLCTYPES_GetAll());
            bc.Commont.initRadComboBox(ref rcCommodity, "ID", "ID", bd.SQLData.B_BCOMMODITY_GetByTransactionType("OTC"));
            var dsCurrency = bd.SQLData.B_BCURRENCY_GetAll();
            bc.Commont.initRadComboBox(ref rcbChargeCcy, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy2, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy3, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbCcyAmount, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref comboCurrency700, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref comboCreditCurrency, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref comboCurrency_747, "Code", "Code", dsCurrency);
        }

        protected void SetDefaultValue()
        {
            SetRelation_BeneficiaryBank();
            SetRelation_ReimbBankType();
            SetRelation_AvailableWithType();
            SetRelation_AdviseThruType();

            SetRelation_ApplicantBankType700();
            SetRelation_DraweeCusType700();
            
            //SetRelation_AvailableWithType740();
            SetRelation_Beneficiary740();

            // not use
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            // not use

            ntSoTien.Value = 0;
            tbcrTolerance.Value = 0;
            tbdrTolerance.Value = 0;

            txtRevivingBank700.Enabled = false;

            tbVatNo.Enabled = false;
            tbChargeCode.Enabled = false;
            tbChargeCode2.Enabled = false;
            tbChargeCode3.Enabled = false;

            rcbPartyCharged.SelectedValue = "A";
            rcbPartyCharged2.SelectedValue = "A";
            rcbPartyCharged3.SelectedValue = "A";

            rcbOmortCharge.SelectedValue = "NO";
            rcbOmortCharges2.SelectedValue = "NO";
            rcbOmortCharges3.SelectedValue = "NO";

            tbChargeAmt.Value = 0;
            tbChargeAmt2.Value = 0;
            tbChargeAmt3.Value = 0;
            
            var now = DateTime.Now;
            tbIssuingDate.SelectedDate = now;
            dteDateOfIssue.SelectedDate = now;

            now = DateTime.Now;
            dteMT700DateAndPlaceOfExpiry.SelectedDate = now.AddDays(15);

            now = DateTime.Now;
            tbContingentExpiry.SelectedDate = now.AddDays(30);

            dteDateOfIssue.Enabled = false;
            dteMT700DateAndPlaceOfExpiry.Enabled = false;
            tbPlaceOfExpiry.Enabled = false;
            comboCurrency700.Enabled = false;
            numAmount700.Enabled = false;
            numPercentCreditAmount1.Enabled = false;
            numPercentCreditAmount2.Enabled = false;

            numAmount700.Value = 0;
            numPercentCreditAmount1.Value = 0;
            numPercentCreditAmount2.Value = 0;

            numPro.Value = 0;
            numLcAmountUnSecured.Value = 0;
            numLcAmountSecured.Value = 0;
            numLoanPrincipal.Value = 0;

            numCreditAmount.Value = 0;

            comboAvailableRule.SelectedValue = "UCP LATEST VERSION";
            lblApplicableRule740.Text = comboAvailableRule.SelectedValue;

            numPercentageCreditAmountTolerance740_1.Value = 0;
            numPercentageCreditAmountTolerance740_2.Value = 0;

            txtSenderToReceiverInformation740_1.Text = "PLS NOTIFY US 03 BANKING DAYS";
            txtSenderToReceiverInformation740_2.Text = "BEFORE DEBITING OUR A/C AND";
            txtSenderToReceiverInformation740_3.Text = "HONORING THE REIMBURSEMENT CLAIM SO";
            txtSenderToReceiverInformation740_4.Text = "THAT WE CAN ARRANGE FOR THIS PMT";

            divCancelLC.Visible = false;
            dteCancelDate.SelectedDate = DateTime.Now;
            dteContingentExpiryDate.SelectedDate = DateTime.Now;

            tbAdviseBankName.Enabled = false;
            tbAdviseBankAddr1.Enabled = false;
            tbAdviseBankAddr2.Enabled = false;
            tbAdviseBankAddr3.Enabled = false;

            tbAdviseThruName.Enabled = false;
            tbAdviseThruAddr1.Enabled = false;
            tbAdviseThruAddr2.Enabled = false;
            tbAdviseThruAddr3.Enabled = false;
            
            comboBeneficiaryType700.SelectedValue = "D";
            comboBeneficiaryType700.Enabled = false;

            comboAvailableWithBy.SelectedValue = "BY NEGOTIATION";

            rcbConfimationInstructions.SelectedValue = "Without";

            comboAdviseThroughBankType700.Enabled = false;
            comboAdviseThroughBankNo700.Enabled = false;
            txtAdviseThroughBankName700.Enabled = false;
            txtAdviseThroughBankAddr700_1.Enabled = false;
            txtAdviseThroughBankAddr700_2.Enabled = false;
            txtAdviseThroughBankAddr700_3.Enabled = false;
            
            comboReimbBankType700.Enabled = false;
            rcbReimbBankNo700.Enabled = false;
            tbReimbBankName700.Enabled = false;
            tbReimbBankAddr700_1.Enabled = false;
            tbReimbBankAddr700_2.Enabled = false;
            tbReimbBankAddr700_3.Enabled = false;

            tbApplicantNo700.Enabled = false;
            tbApplicantName700.Enabled = false;
            tbApplicantAddr700_1.Enabled = false;
            tbApplicantAddr700_2.Enabled = false;
            tbApplicantAddr700_3.Enabled = false;

            tbDraweeCusNo740.Enabled = false;
            tbDraweeCusName740.Enabled = false;
            tbDraweeAddr740_1.Enabled = false;
            tbDraweeAddr740_2.Enabled = false;
            tbDraweeAddr740_3.Enabled = false;

            rcbBeneficiaryType740.SelectedValue = "D";
            rcbBeneficiaryType740.Enabled = false;
            tbBeneficiaryNo740.Enabled = false;

            rcbAvailableWithType740.Enabled = false;
            tbAvailableWithNo740.Enabled = false;
            tbAvailableWithName740.Enabled = false;
            tbAvailableWithAddr740_1.Enabled = false;
            tbAvailableWithAddr740_2.Enabled = false;
            tbAvailableWithAddr740_3.Enabled = false;

            // tab 747
            comboReimbBankType_747.Enabled = false;
            comboReimbBankNo_747.Enabled = false;
            txtReimbBankName_747.Enabled = false;
            txtReimbBankAddr_747_1.Enabled = false;
            txtReimbBankAddr_747_2.Enabled = false;
            txtReimbBankAddr_747_3.Enabled = false;

            dteDateOfOriginalAuthorization_747.Enabled = false;
            dteDateOfOriginalAuthorization_747.SelectedDate = DateTime.Now;
            comboCurrency_747.Enabled = false;

            numAmount_747.Value = 0;
            numPercentageCreditTolerance_747_1.Value = 0;
            numPercentageCreditTolerance_747_2.Value = 0;

            divMT747.Visible = false;
            divMT707.Visible = false;

            divMT740.Visible = false;
            divMT700.Visible = false;
            switch (TabId)
            {
                case TabIssueLCAddNew: //Issue LC
                    divMT740.Visible = true;
                    divMT700.Visible = true;

                    tbChargeCode.SelectedValue = "ILC.CABLE";
                    tbChargeCode2.SelectedValue = "ILC.OPEN";
                    tbChargeCode3.SelectedValue = "ILC.OPENAMORT";
                    break;
                case TabIssueLCAmend: //Amend LC
                    divMT747.Visible = true;
                    divMT707.Visible = true;
                    tbIssuingDate.Enabled = false;

                    RadTabStrip3.Tabs[1].Text = "Amendment Charge";
                    RadTabStrip3.Tabs[2].Visible = false;

                    tbChargeCode.SelectedValue = "ILC.CABLE";
                    tbChargeCode2.SelectedValue = "IC.AMEND";
                    break;
                case TabIssueLCCancel: //Cancel LC

                    tbChargeCode.SelectedValue = "ILC.CABLE";
                    tbChargeCode2.SelectedValue = "ILC.CANCEL";
                    tbChargeCode3.SelectedValue = "ILC.OTHER";

                    RadTabStrip3.Tabs[1].Text = "Cancel Charge";
                    RadTabStrip3.Tabs[2].Text = "Other Charge";
                    break;
            }

            txtReceivingBank_747.Enabled = false;

            // tab 707
            txtReceivingBankId_707.Enabled = false;
            dteDateOfIssue_707.Enabled = false;
            dteDateOfIssue_707.SelectedDate = DateTime.Now;
            comboAvailableRule_707.SelectedValue = "UCP LATEST VERSION";
            dteDateOfAmendment_707.Enabled = false;
            dteDateOfAmendment_707.SelectedDate = DateTime.Now;

            comboBeneficiaryType_707.Enabled = false;
            txtBeneficiaryNo_707.Enabled = false;
            txtBeneficiaryName_707.Enabled = false;
            txtBeneficiaryAddr_707_1.Enabled = false;
            txtBeneficiaryAddr_707_2.Enabled = false;
            txtBeneficiaryAddr_707_3.Enabled = false;

            numPercentageCreditAmountTolerance_707_1.Value = 0;
            numPercentageCreditAmountTolerance_707_2.Value = 0;

            txtSenderToReceiverInformation_707_1.Text = "PLEASE ADVISE THIS AMENDMENT TO THE BENEFICIARY THROUGH";
        }
        
        protected void LoadPartyCharged()
        {
            var dtSource = bd.SQLData.CreateGenerateDatas("PartyCharged_IssueLC");
            bc.Commont.initRadComboBox(ref rcbPartyCharged, "Id", "Id", dtSource);
            bc.Commont.initRadComboBox(ref rcbPartyCharged2, "Id", "Id", dtSource);
            bc.Commont.initRadComboBox(ref rcbPartyCharged3, "Id", "Id", dtSource);
        }

        protected void LoadChargeCode()
        {
            var datasource = bd.SQLData.B_BCHARGECODE_GetByViewType(TabId);
            bc.Commont.initRadComboBox(ref tbChargeCode, "Code", "Code", datasource);
            bc.Commont.initRadComboBox(ref tbChargeCode2, "Code", "Code", datasource);
            bc.Commont.initRadComboBox(ref tbChargeCode3, "Code", "Code", datasource);
        }
        
        private void LoadToolBar(bool flag)
        {
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar1.FindItemByValue("btReverse").Enabled = flag;
            if (Request.QueryString["disable"] != null)
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            else
                RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected void SetDisableByReview(bool flag)
        {
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, flag);

            txtRevivingBank700.Enabled = false;

            tbVatNo.Enabled = false;
            tbChargeCode.Enabled = false;
            tbChargeCode2.Enabled = false;
            tbChargeCode3.Enabled = false;

            dteDateOfIssue.Enabled = false;
            dteMT700DateAndPlaceOfExpiry.Enabled = false;
            tbPlaceOfExpiry.Enabled = false;
            comboCurrency700.Enabled = false;
            numAmount700.Enabled = false;
            numPercentCreditAmount1.Enabled = false;
            numPercentCreditAmount2.Enabled = false;

            tbAdviseBankName.Enabled = false;
            tbAdviseBankAddr1.Enabled = false;
            tbAdviseBankAddr2.Enabled = false;
            tbAdviseBankAddr3.Enabled = false;

            tbAdviseThruName.Enabled = false;
            tbAdviseThruAddr1.Enabled = false;
            tbAdviseThruAddr2.Enabled = false;
            tbAdviseThruAddr3.Enabled = false;
            

            comboBeneficiaryType700.Enabled = false;

            comboAdviseThroughBankType700.Enabled = false;
            comboAdviseThroughBankNo700.Enabled = false;
            txtAdviseThroughBankName700.Enabled = false;
            txtAdviseThroughBankAddr700_1.Enabled = false;
            txtAdviseThroughBankAddr700_2.Enabled = false;
            txtAdviseThroughBankAddr700_3.Enabled = false;

            comboReimbBankType700.Enabled = false;
            rcbReimbBankNo700.Enabled = false;
            tbReimbBankName700.Enabled = false;
            tbReimbBankAddr700_1.Enabled = false;
            tbReimbBankAddr700_2.Enabled = false;
            tbReimbBankAddr700_3.Enabled = false;

            tbApplicantNo700.Enabled = false;
            tbApplicantName700.Enabled = false;
            tbApplicantAddr700_1.Enabled = false;
            tbApplicantAddr700_2.Enabled = false;
            tbApplicantAddr700_3.Enabled = false;
            
            tbDraweeCusNo740.Enabled = false;
            tbDraweeCusName740.Enabled = false;
            tbDraweeAddr740_1.Enabled = false;
            tbDraweeAddr740_2.Enabled = false;
            tbDraweeAddr740_3.Enabled = false;

            rcbBeneficiaryType740.Enabled = false;
            tbBeneficiaryNo740.Enabled = false;

            rcbAvailableWithType740.Enabled = false;
            tbAvailableWithNo740.Enabled = false;
            tbAvailableWithName740.Enabled = false;
            tbAvailableWithAddr740_1.Enabled = false;
            tbAvailableWithAddr740_2.Enabled = false;
            tbAvailableWithAddr740_3.Enabled = false;

            // tab 747
            comboReimbBankType_747.Enabled = false;
            comboReimbBankNo_747.Enabled = false;
            txtReimbBankName_747.Enabled = false;
            txtReimbBankAddr_747_1.Enabled = false;
            txtReimbBankAddr_747_2.Enabled = false;
            txtReimbBankAddr_747_3.Enabled = false;

            dteDateOfOriginalAuthorization_747.Enabled = false;
            comboCurrency_747.Enabled = false;

            txtReceivingBank_747.Enabled = false;

            // 707
            txtReceivingBankId_707.Enabled = false;
            dteDateOfIssue_707.Enabled = false;
            dteDateOfAmendment_707.Enabled = false;

            comboBeneficiaryType_707.Enabled = false;
            txtBeneficiaryNo_707.Enabled = false;
            txtBeneficiaryName_707.Enabled = false;
            txtBeneficiaryAddr_707_1.Enabled = false;
            txtBeneficiaryAddr_707_2.Enabled = false;
            txtBeneficiaryAddr_707_3.Enabled = false;

            switch (TabId)
            {
                case TabIssueLCAddNew: //Issue LC
                    break;
                case TabIssueLCAmend: //Amend LC
                    tbIssuingDate.Enabled = false;
                    break;
                case TabIssueLCCancel: //Cancel LC
                    break;
            }
        }
        
        protected void GenerateVAT()
        {
            var vatno = bd.Database.B_BMACODE_GetNewSoTT("VATNO");
            tbVatNo.Text = vatno.Tables[0].Rows[0]["SoTT"].ToString();
        }

        protected void GenerateLCCode()
        {
            var ds = bd.DataTam.B_ISSURLC_GetNewID();
            txtCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();

            GetDocumentaryCreditNumber();
        }
        
        protected void SetRelation_BeneficiaryBank()
        {
            //switch (comboBeneficiaryBankType.SelectedValue)
            //{
            //    case "A":
            //        txtBeneficiaryBank.Enabled = true;
            //        txtBeneficiaryBankName.Enabled = false;
            //        txtBeneficiaryBankAddr1.Enabled = false;
            //        txtBeneficiaryBankAddr2.Enabled = false;
            //        txtBeneficiaryBankAddr3.Enabled = false;
            //        break;
            //    case "B":
            //    case "D":
            //        txtBeneficiaryBank.Enabled = false;
            //        txtBeneficiaryBankName.Enabled = true;
            //        txtBeneficiaryBankAddr1.Enabled = true;
            //        txtBeneficiaryBankAddr2.Enabled = true;
            //        txtBeneficiaryBankAddr3.Enabled = true;
            //        break;
            //}
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;

            switch (commandName)
            {
                case "commit":
                    SaveData();

                    Response.Redirect("Default.aspx?tabid=" + TabId.ToString());
                    break;

                case "Preview":
                    switch (TabId)
                    {
                        case TabIssueLCAddNew: // Issue LC
                            Response.Redirect(EditUrl("preview_issuelc"));
                            break;

                        case TabIssueLCAmend: //Amend LC
                            Response.Redirect(EditUrl("preview_amendlc"));
                            break;

                        case TabIssueLCCancel: //Cancel LC
                            Response.Redirect(EditUrl("preview_cancellc"));
                            break;
                    }
                    break;

                case "authorize":
                    bd.SQLData.B_BIMPORT_NORMAILLC_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.AUT, UserId.ToString(), TabId);

                    Response.Redirect("Default.aspx?tabid=" + TabId.ToString());
                    break;

                case "reverse":
                    bd.SQLData.B_BIMPORT_NORMAILLC_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.REV, UserId.ToString(), TabId);

                    // Active control
                    SetDisableByReview(true);

                    // ko cho Authorize/Preview
                    LoadToolBar(false);
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                    break;
            }
        }
        
        protected void GetDocumentaryCreditNumber()
        {
            lblDocumentaryCreditNumber.Text = txtCode.Text + "I";
            lblDocumentaryCreditNumber740.Text = lblDocumentaryCreditNumber.Text;
            lblDocumentaryCreditNumber_747.Text = txtCode.Text;
            lblSenderReference_707.Text = txtCode.Text;
        }

        protected void SetRelation_ApplicantBankType700()
        {
            //switch (rcbApplicantBankType700.SelectedValue)
            //{
            //    case "A":
            //        rcbApplicant700.Enabled = true;
            //        tbApplicantName700.Enabled = false;
            //        tbApplicantAddr700_1.Enabled = false;
            //        tbApplicantAddr700_2.Enabled = false;
            //        tbApplicantAddr700_3.Enabled = false;
            //        break;
            //    case "B":
            //    case "D":
            //        rcbApplicant700.Enabled = false;
            //        tbApplicantName700.Enabled = true;
            //        tbApplicantAddr700_1.Enabled = true;
            //        tbApplicantAddr700_2.Enabled = true;
            //        tbApplicantAddr700_3.Enabled = true;
            //        break;
            //}
        }

        protected void SetRelation_AvailableWithType()
        {
            switch (rcbAvailableWithType.SelectedValue)
            {
                case "A":
                    comboAvailableWithNo.Enabled = true;
                    tbAvailableWithName.Enabled = false;
                    tbAvailableWithAddr1.Enabled = false;
                    tbAvailableWithAddr2.Enabled = false;
                    tbAvailableWithAddr3.Enabled = false;
                    break;
                case "B":
                case "D":
                    comboAvailableWithNo.Enabled = false;
                    tbAvailableWithName.Enabled = true;
                    tbAvailableWithAddr1.Enabled = true;
                    tbAvailableWithAddr2.Enabled = true;
                    tbAvailableWithAddr3.Enabled = true;
                    break;
            }
        }

        protected void CheckSwiftCodeExist()
        {
            lblReceivingBankNoError.Text = "";
            lblReceivingBankName.Text = "";
            if (!string.IsNullOrEmpty(txtRemittingBankNo.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtRemittingBankNo.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    lblReceivingBankName.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblReceivingBankNoError.Text = "No found swiftcode";
                }
            }

        }
        
        protected void SetRelation_AvailableWithType740()
        {
            switch (rcbAvailableWithType740.SelectedValue)
            {
                case "A":
                    tbAvailableWithNo740.Enabled = true;
                    tbAvailableWithName740.Enabled = false;
                    tbAvailableWithAddr740_1.Enabled = false;
                    tbAvailableWithAddr740_2.Enabled = false;
                    tbAvailableWithAddr740_3.Enabled = false;
                    break;
                case "B":
                case "D":
                    tbAvailableWithNo740.Enabled = false;
                    tbAvailableWithName740.Enabled = true;
                    tbAvailableWithAddr740_1.Enabled = true;
                    tbAvailableWithAddr740_2.Enabled = true;
                    tbAvailableWithAddr740_3.Enabled = true;
                    break;
            }
        }
        
        protected void SetRelation_Beneficiary740()
        {
            switch (rcbBeneficiaryType740.SelectedValue)
            {
                case "A":
                    tbBeneficiaryNo740.Enabled = true;
                    tbBeneficiaryName740.Enabled = false;
                    tbBeneficiaryAddr740_1.Enabled = false;
                    tbBeneficiaryAddr740_2.Enabled = false;
                    tbBeneficiaryAddr740_3.Enabled = false;
                    break;
                case "B":
                case "D":
                    tbBeneficiaryNo740.Enabled = false;
                    tbBeneficiaryName740.Enabled = true;
                    tbBeneficiaryAddr740_1.Enabled = true;
                    tbBeneficiaryAddr740_2.Enabled = true;
                    tbBeneficiaryAddr740_3.Enabled = true;
                    break;
            }
        }

        protected void SaveData()
        {
            double amount_main = 0;
            double amount_Old = 0;
            double crTolerance = 0, drTolerance = 0;
            double pro = 0, lcAmountSecured = 0, lcAmountUnSecured = 0, loanPrincipal = 0;

            if (!string.IsNullOrEmpty(lblAmount_Old.Text))
            {
                amount_Old = double.Parse(lblAmount_Old.Text);
            }

            if (tbcrTolerance.Value > 0)
            {
                crTolerance = double.Parse(tbcrTolerance.Value.ToString());
            }

            if (tbdrTolerance.Value > 0)
            {
                drTolerance = double.Parse(tbdrTolerance.Value.ToString());
            }

            if (ntSoTien.Value > 0)
            {
                amount_main = double.Parse(ntSoTien.Value.ToString());
            }

            if (numPro.Value > 0)
            {
                pro = double.Parse(numPro.Value.ToString());
            }

            if (numLcAmountSecured.Value > 0)
            {
                lcAmountSecured = double.Parse(numLcAmountSecured.Value.ToString());
            }
            if (numLcAmountUnSecured.Value > 0)
            {
                lcAmountUnSecured = double.Parse(numLcAmountUnSecured.Value.ToString());
            }
            
            if (numLoanPrincipal.Value > 0)
            {
                loanPrincipal = double.Parse(numLoanPrincipal.Value.ToString());
            }

            bd.SQLData.B_BIMPORT_NORMAILLC_Insert(txtCode.Text.Trim()
                , rcbLCType.SelectedValue
                , rcbApplicantID.SelectedValue
                , tbApplicantName.Text.Trim()
                , tbApplicantAddr1.Text.Trim()
                , tbApplicantAddr2.Text.Trim()
                , tbApplicantAddr3.Text.Trim()
                , rcbCcyAmount.SelectedValue
                , amount_main
                , amount_Old
                , crTolerance
                , drTolerance
                , tbIssuingDate.SelectedDate
                , tbExpiryDate.SelectedDate
                , tbExpiryPlace.Text.Trim()
                , tbContingentExpiry.SelectedDate
                , rcbAccountOfficer.SelectedValue
                , tbContactNo.Text.Trim()
                , comboBeneficiaryBankType.SelectedValue
                , txtBeneficiaryNo.Text
                , txtBeneficiaryBankName.Text.Trim()
                , txtBeneficiaryBankAddr1.Text.Trim()
                , txtBeneficiaryBankAddr2.Text.Trim()
                , txtBeneficiaryBankAddr3.Text.Trim()
                , "A"
                , rcbAdviseBankNo.SelectedValue
                , tbAdviseBankName.Text.Trim()
                , tbAdviseBankAddr1.Text.Trim()
                , tbAdviseBankAddr2.Text.Trim()
                , tbAdviseBankAddr3.Text.Trim()
                , comboReimbBankType.SelectedValue
                , rcbReimbBankNo.SelectedValue
                , tbReimbBankName.Text.Trim()
                , tbReimbBankAddr1.Text.Trim()
                , tbReimbBankAddr2.Text.Trim()
                , tbReimbBankAddr3.Text.Trim()
                , "A"
                , rcbAdviseThruNo.SelectedValue
                , tbAdviseThruName.Text.Trim()
                , tbAdviseThruAddr1.Text.Trim()
                , tbAdviseThruAddr2.Text.Trim()
                , tbAdviseThruAddr3.Text.Trim()
                , "A"
                , ""
                , ""
                , ""
                , ""
                , ""
                , rcCommodity.SelectedValue
                , pro
                , lcAmountSecured
                , lcAmountUnSecured
                , loanPrincipal
                , UserId
                , TabId
                , dteCancelDate.SelectedDate
                , dteCancelDate.SelectedDate
                , txtCancelRemark.Text.Trim()
                );

            // Insert tab Dien ======================
            switch (TabId)
            {
                case TabIssueLCAddNew: //Issue LC
                    SaveData_IssueLC_TabDien();
                    break;
                case TabIssueLCAmend://Amend LC
                    SaveData_AmendLC_TabDien();
                    break;
            }
            //=======================================

            double chargeAmt = 0, chargeAmt2 = 0, chargeAmt3 = 0;
            if (tbChargeAmt.Value > 0)
            {
                chargeAmt = (double)tbChargeAmt.Value;
            }

            if (tbChargeAmt2.Value > 0)
            {
                chargeAmt2 = (double)tbChargeAmt2.Value;
            }

            if (tbChargeAmt3.Value > 0)
            {
                chargeAmt3 = (double)tbChargeAmt3.Value;
            }

            bd.SQLData.B_BIMPORT_NORMAILLC_CHARGE_Insert(txtCode.Text.Trim(),
                                                    comboWaiveCharges.SelectedValue,
                                                    tbChargeCode.SelectedValue,
                                                    rcbChargeAcct.SelectedValue,
                                                    tbChargePeriod.Text,
                                                    rcbChargeCcy.SelectedValue,
                                                    chargeAmt,
                                                    rcbPartyCharged.SelectedValue,
                                                    rcbOmortCharge.SelectedValue,
                                                    rcbChargeStatus.SelectedValue,
                                                    tbChargeRemarks.Text,
                                                    tbVatNo.Text,
                                                    lblTaxCode.Text,
                                                    string.IsNullOrEmpty(lblTaxAmt.Text) ? 0 : double.Parse(lblTaxAmt.Text),
                                                    1,
                                                    TabId);

            bd.SQLData.B_BIMPORT_NORMAILLC_CHARGE_Insert(txtCode.Text.Trim(),
                                                    comboWaiveCharges.SelectedValue,
                                                    tbChargeCode2.SelectedValue,
                                                    rcbChargeAcct2.SelectedValue,
                                                    tbChargePeriod2.Text,
                                                    rcbChargeCcy2.SelectedValue,
                                                    chargeAmt2,
                                                    rcbPartyCharged2.SelectedValue,
                                                    rcbOmortCharges2.SelectedValue,
                                                    rcbChargeStatus2.SelectedValue,
                                                    tbChargeRemarks.Text,
                                                    tbVatNo.Text,
                                                    lblTaxCode2.Text,
                                                    string.IsNullOrEmpty(lblTaxAmt2.Text) ? 0 : double.Parse(lblTaxAmt2.Text),
                                                    2,
                                                    TabId);
            bd.SQLData.B_BIMPORT_NORMAILLC_CHARGE_Insert(txtCode.Text.Trim(),
                                                    comboWaiveCharges.SelectedValue,
                                                    tbChargeCode3.SelectedValue,
                                                    rcbChargeAcct3.SelectedValue,
                                                    tbChargePeriod3.Text,
                                                    rcbChargeCcy3.SelectedValue,
                                                    chargeAmt3,
                                                    rcbPartyCharged3.SelectedValue,
                                                    rcbOmortCharges3.SelectedValue,
                                                    rcbChargeStatus3.SelectedValue,
                                                    tbChargeRemarks.Text,
                                                    tbVatNo.Text,
                                                    lblTaxCode3.Text,
                                                    string.IsNullOrEmpty(lblTaxAmt3.Text) ? 0 : double.Parse(lblTaxAmt3.Text),
                                                    3,
                                                    TabId);
        }

        protected void SaveData_IssueLC_TabDien()
        {
            double amount700 = 0;
            double percentCreditAmount1 = 0, percentCreditAmount2 = 0;
            if (numAmount700.Value > 0)
            {
                amount700 = double.Parse(numAmount700.Value.ToString());
            }

            if (numPercentCreditAmount1.Value > 0)
            {
                percentCreditAmount1 = double.Parse(numPercentCreditAmount1.Value.ToString());
            }

            if (numPercentCreditAmount2.Value > 0)
            {
                percentCreditAmount2 = double.Parse(numPercentCreditAmount2.Value.ToString());
            }

            bd.SQLData.B_BIMPORT_NORMAILLC_MT700_Insert(
                txtCode.Text.Trim()
                , rcbAdviseBankNo.SelectedValue
                , tbBaquenceOfTotal.Text
                , comboFormOfDocumentaryCredit.SelectedValue
                , lblDocumentaryCreditNumber.Text
                , dteDateOfIssue.SelectedDate
                , dteMT700DateAndPlaceOfExpiry.SelectedDate
                , tbPlaceOfExpiry.Text.Trim()
                , comboAvailableRule.SelectedValue
                , rcbApplicantBankType700.SelectedValue
                , tbApplicantNo700.Text
                , tbApplicantName700.Text.Trim()
                , tbApplicantAddr700_1.Text.Trim()
                , tbApplicantAddr700_2.Text.Trim()
                , tbApplicantAddr700_3.Text.Trim()
                , comboCurrency700.SelectedValue
                , amount700
                , percentCreditAmount1
                , percentCreditAmount2
                , comboMaximumCreditAmount700.SelectedValue
                , rcbAvailableWithType.SelectedValue
                , comboAvailableWithNo.SelectedValue
                , tbAvailableWithName.Text.Trim()
                , tbAvailableWithAddr1.Text.Trim()
                , tbAvailableWithAddr2.Text.Trim()
                , tbAvailableWithAddr3.Text.Trim()
                , comboAvailableWithBy.SelectedValue
                , comboDraweeCusType.SelectedValue
                , comboDraweeCusNo700.SelectedValue
                , txtDraweeCusName.Text.Trim()
                , txtDraweeAddr1.Text.Trim()
                , txtDraweeAddr2.Text.Trim()
                , txtDraweeAddr3.Text.Trim()
                , ""
                , ""
                , rcbPatialShipment.SelectedValue
                , rcbTranshipment.SelectedValue
                , tbPlaceoftakingincharge.Text.Trim()
                , tbPortofloading.Text.Trim()
                , tbPortofDischarge.Text.Trim()
                , tbPlaceoffinalindistination.Text.Trim()
                , tbLatesDateofShipment.SelectedDate
                , ""
                , tbDescrpofGoods.Text
                , tbOrderDocs.Text.Trim()//rcbDocsRequired.SelectedValue
                , tbAdditionalConditions.Text.Trim()// 
                , tbCharges.Text.Trim()
                , tbPeriodforPresentation.Text.Trim()
                , rcbConfimationInstructions.SelectedValue
                , tbNegotgBank.Text.Trim()
                , tbSendertoReceiverInfomation.Text.Trim()
                , comboBeneficiaryType700.SelectedValue
                , txtBeneficiaryNo700.Text.Trim()
                , txtBeneficiaryName700.Text.Trim()
                , txtBeneficiaryAddr700_1.Text.Trim()
                , txtBeneficiaryAddr700_2.Text.Trim()
                , txtBeneficiaryAddr700_3.Text.Trim()

                , comboAdviseThroughBankType700.SelectedValue
                , comboAdviseThroughBankNo700.SelectedValue
                , txtAdviseThroughBankName700.Text
                , txtAdviseThroughBankAddr700_1.Text
                , txtAdviseThroughBankAddr700_2.Text
                , txtAdviseThroughBankAddr700_3.Text

                , comboReimbBankType700.SelectedValue
                , rcbReimbBankNo700.SelectedValue
                , tbReimbBankName700.Text.Trim()
                , tbReimbBankAddr700_1.Text.Trim()
                , tbReimbBankAddr700_2.Text.Trim()
                , tbReimbBankAddr700_3.Text.Trim()

                , txtAdditionalAmountsCovered700_1.Text
                , txtAdditionalAmountsCovered700_2.Text
                , txtDraftsAt700_1.Text
                , txtDraftsAt700_2.Text
                , txtMixedPaymentDetails700_1.Text
                , txtMixedPaymentDetails700_2.Text
                , txtDeferredPaymentDetails700_1.Text
                , txtDeferredPaymentDetails700_2.Text
                , txtShipmentPeriod700_1.Text
                , txtShipmentPeriod700_2.Text
                , txtMixedPaymentDetails700_3.Text
                , txtMixedPaymentDetails700_4.Text
                , txtDeferredPaymentDetails700_3.Text
                , txtDeferredPaymentDetails700_4.Text
                , txtShipmentPeriod700_3.Text
                , txtShipmentPeriod700_4.Text
                , txtShipmentPeriod700_5.Text
                , txtShipmentPeriod700_6.Text
                );

            double creditAmount = 0;
            if (numCreditAmount.Value > 0)
            {
                creditAmount = (double)numCreditAmount.Value;
            }

            bd.SQLData.B_BIMPORT_NORMAILLC_MT740_Insert(
                txtCode.Text.Trim(),
                comGenerate.SelectedValue,
                txtRemittingBankNo.Text.Trim(),
                lblDocumentaryCreditNumber740.Text,
                tbExpiryDate740.SelectedDate,
                tb31DPlaceOfExpiry.Text.Trim(),
                rcbBeneficiaryType740.SelectedValue,
                tbBeneficiaryNo740.Text.Trim(),
                tbBeneficiaryName740.Text.Trim(),
                tbBeneficiaryAddr740_1.Text.Trim(),
                tbBeneficiaryAddr740_2.Text.Trim(),
                tbBeneficiaryAddr740_3.Text.Trim(),
                creditAmount,
                comboCreditCurrency.SelectedValue,
                rcbAvailableWithType740.SelectedValue,
                tbAvailableWithNo740.Text.Trim(),
                tbAvailableWithName740.Text.Trim(),
                tbAvailableWithAddr740_1.Text.Trim(),
                tbAvailableWithAddr740_2.Text.Trim(),
                tbAvailableWithAddr740_3.Text.Trim(),
                tbDraweeCusNo740.Text,
                tbDraweeCusName740.Text.Trim(),
                tbDraweeAddr740_1.Text.Trim(),
                tbDraweeAddr740_2.Text.Trim(),
                tbDraweeAddr740_3.Text.Trim(),
                comboReimbursingBankChange.SelectedValue,
                lblApplicableRule740.Text,
                (double)numPercentageCreditAmountTolerance740_1.Value,
                (double)numPercentageCreditAmountTolerance740_2.Value,
                txtSenderToReceiverInformation740_1.Text,
                txtSenderToReceiverInformation740_2.Text,
                txtSenderToReceiverInformation740_3.Text,
                txtSenderToReceiverInformation740_4.Text,
                txtDraftsAt740_1.Text,
                txtDraftsAt740_2.Text
            );
        }

        protected void SaveData_AmendLC_TabDien()
        {
            bd.SQLData.B_BIMPORT_NORMAILLC_MT707_Insert(txtCode.Text
                , txtReceivingBankId_707.Text
                , lblSenderReference_707.Text
                , txtReceiverReference_707.Text
                , txtReferenceToPreAdvice_707.Text
                , ""
                , txtIssuingBankReferenceNo_707.Text
                , txtIssuingBankReferenceName_707.Text
                , txtIssuingBankReferenceAddr_707_1.Text
                , txtIssuingBankReferenceAddr_707_2.Text
                , txtIssuingBankReferenceAddr_707_3.Text
                , dteDateOfIssue_707.SelectedDate
                , comboAvailableRule_707.SelectedValue
                , dteDateOfAmendment_707.SelectedDate
                , comboBeneficiaryType_707.SelectedValue
                , txtBeneficiaryNo_707.Text
                , txtBeneficiaryName_707.Text
                , txtBeneficiaryAddr_707_1.Text
                , txtBeneficiaryAddr_707_2.Text
                , txtBeneficiaryAddr_707_3.Text
                , dteNewDateOfExpiry_707.SelectedDate
                , numPercentageCreditAmountTolerance_707_1.Value
                , numPercentageCreditAmountTolerance_707_2.Value
                , comboMaximumCreditAmount_707.SelectedValue
                , txtAdditionalAmountsCovered_707_1.Text
                , txtAdditionalAmountsCovered_707_2.Text
                , txtPlaceoftakingincharge_707.Text
                , txtPlaceoffinalindistination_707.Text
                , dteLatesDateofShipment_707.SelectedDate
                , txtShipmentPeriod_707_1.Text
                , txtShipmentPeriod_707_2.Text
                , txtShipmentPeriod_707_3.Text
                , txtShipmentPeriod_707_4.Text
                , txtPortofloading_707.Text
                , txtPortofDischarge_707.Text
                , txtNarrative_707.Text
                , txtSenderToReceiverInformation_707_1.Text
                , txtSenderToReceiverInformation_707_2.Text
                , txtSenderToReceiverInformation_707_3.Text
                , txtSenderToReceiverInformation_707_4.Text
                , txtShipmentPeriod_707_5.Text
                , txtShipmentPeriod_707_6.Text
                , txtSenderToReceiverInformation_707_5.Text
                , txtSenderToReceiverInformation_707_6.Text
                );


            bd.SQLData.B_BIMPORT_NORMAILLC_MT747_Insert(txtCode.Text
                , comboGenerateMT747.SelectedValue
                , txtReceivingBank_747.Text
                , lblDocumentaryCreditNumber_747.Text
                , comboReimbBankType_747.SelectedValue
                , comboReimbBankNo_747.SelectedValue
                , txtReimbBankName_747.Text
                , txtReimbBankAddr_747_1.Text
                , txtReimbBankAddr_747_2.Text
                , txtReimbBankAddr_747_3.Text
                , dteDateOfOriginalAuthorization_747.SelectedDate
                , dteNewDateOfExpiry_747.SelectedDate
                , comboCurrency_747.SelectedValue
                , numAmount_747.Value
                , numPercentageCreditTolerance_747_1.Value
                , numPercentageCreditTolerance_747_2.Value
                , comboMaximumCreditAmount_747.SelectedValue
                , txtAdditionalCovered_747_1.Text
                , txtAdditionalCovered_747_2.Text
                , txtAdditionalCovered_747_3.Text
                , txtAdditionalCovered_747_4.Text
                , txtSenderToReceiverInfomation_747_1.Text
                , txtSenderToReceiverInfomation_747_2.Text
                , txtSenderToReceiverInfomation_747_3.Text
                , txtSenderToReceiverInfomation_747_4.Text
            );
        }

        protected void LoadData(ref DataRow drowLC)
        {
            var dsDoc = bd.SQLData.B_BIMPORT_NORMAILLC_GetByNormalLCCode(txtCode.Text.Trim(), TabId);
            if (dsDoc == null || dsDoc.Tables.Count <= 0)
            {
                return;
            }

            // truong hop Edit, thi` ko cho click Preview
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;

            #region tab main
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                RadToolBar1.FindItemByValue("btPreview").Enabled = false;

                var drow = dsDoc.Tables[0].Rows[0];

                Amount_Old = double.Parse(drow["Amount_Old"].ToString());
                Amount = double.Parse(drow["Amount"].ToString());
                B4_AUT_Amount = double.Parse(drow["B4_AUT_Amount"].ToString());
                TotalChargeAmt = double.Parse(drow["TotalChargeAmt"].ToString());

                drowLC = drow;
                SetVisibilityByStatus(ref drow);

                rcbLCType.SelectedValue = drow["LCType"].ToString();
                rcbApplicantID.SelectedValue = drow["ApplicantId"].ToString();
                tbApplicantName.Text = drow["ApplicantName"].ToString();
                tbApplicantAddr1.Text = drow["ApplicantAddr1"].ToString();
                tbApplicantAddr2.Text = drow["ApplicantAddr2"].ToString();
                tbApplicantAddr3.Text = drow["ApplicantAddr3"].ToString();
                rcbCcyAmount.SelectedValue = drow["Currency"].ToString();
                ntSoTien.Text = drow["B4_AUT_Amount"].ToString();
                tbcrTolerance.Text = drow["CrTolerance"].ToString();
                tbdrTolerance.Text = drow["DrTolerance"].ToString();

                tbExpiryPlace.Text = drow["ExpiryPlace"].ToString();
                rcbAccountOfficer.SelectedValue = drow["AccountOfficer"].ToString();
                tbContactNo.Text = drow["ContactNo"].ToString();
                comboBeneficiaryBankType.SelectedValue = drow["BeneficiaryType"].ToString();
                txtBeneficiaryNo.Text = drow["BeneficiaryNo"].ToString();
                txtBeneficiaryBankName.Text = drow["BeneficiaryName"].ToString();
                txtBeneficiaryBankAddr1.Text = drow["BeneficiaryAddr1"].ToString();
                txtBeneficiaryBankAddr2.Text = drow["BeneficiaryAddr2"].ToString();
                txtBeneficiaryBankAddr3.Text = drow["BeneficiaryAddr3"].ToString();
                rcbAdviseBankNo.SelectedValue = drow["AdviseBankNo"].ToString();
                tbAdviseBankName.Text = drow["AdviseBankName"].ToString();
                tbAdviseBankAddr1.Text = drow["AdviseBankAddr1"].ToString();
                tbAdviseBankAddr2.Text = drow["AdviseBankAddr2"].ToString();
                tbAdviseBankAddr3.Text = drow["AdviseBankAddr3"].ToString();

                comboReimbBankType.SelectedValue = drow["ReimbBankType"].ToString();
                rcbReimbBankNo.SelectedValue = drow["ReimbBankNo"].ToString();
                tbReimbBankName.Text = drow["ReimbBankName"].ToString();
                tbReimbBankAddr1.Text = drow["ReimbBankAddr1"].ToString();
                tbReimbBankAddr2.Text = drow["ReimbBankAddr2"].ToString();
                tbReimbBankAddr3.Text = drow["ReimbBankAddr3"].ToString();

                rcbAdviseThruNo.SelectedValue = drow["AdviseThruNo"].ToString();
                tbAdviseThruName.Text = drow["AdviseThruName"].ToString();
                tbAdviseThruAddr1.Text = drow["AdviseThruAddr1"].ToString();
                tbAdviseThruAddr2.Text = drow["AdviseThruAddr2"].ToString();
                tbAdviseThruAddr3.Text = drow["AdviseThruAddr3"].ToString();

                //rcbAvailWithNo.SelectedValue = drow["AvailWithNo"].ToString();
                //tbAvailWithName.Text = drow["AvailWithName"].ToString();
                //tbAvailWithAddr1.Text = drow["AvailWithAddr1"].ToString();
                //tbAvailWithAddr2.Text = drow["AvailWithAddr2"].ToString();
                //tbAvailWithAddr3.Text = drow["AvailWithAddr3"].ToString();

                rcCommodity.SelectedValue = drow["Commodity"].ToString();
                numPro.Text = drow["Prov"].ToString();
                numLcAmountSecured.Text = drow["LCAmountSecured"].ToString();
                numLcAmountUnSecured.Text = drow["LCAmountUnSecured"].ToString();
                numLoanPrincipal.Text = drow["LoanPrincipal"].ToString();

                if (!string.IsNullOrEmpty(drow["IssuingDate"].ToString()) && drow["IssuingDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    tbIssuingDate.SelectedDate = DateTime.Parse(drow["IssuingDate"].ToString());
                }

                if (!string.IsNullOrEmpty(drow["ExpiryDate"].ToString()) && drow["ExpiryDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    tbExpiryDate.SelectedDate = DateTime.Parse(drow["ExpiryDate"].ToString());
                }

                if (!string.IsNullOrEmpty(drow["ContingentExpiry"].ToString()) &&  drow["ContingentExpiry"].ToString().IndexOf("1/1/1900") == -1)
                {
                    tbContingentExpiry.SelectedDate = DateTime.Parse(drow["ContingentExpiry"].ToString());
                }

                // ===Amount_Old
                // Amend LC
                if (TabId == TabIssueLCAmend)
                {
                    lblAmount_Old.Text = Double.Parse(drow["Amount_Old"].ToString()).ToString("C", CultureInfo.CurrentCulture).Replace("$", "");
                    divAmount.Visible = true;
                }

                // DocumentaryCollectionCancel
                if (!string.IsNullOrEmpty(drow["CancelLCDate"].ToString()) && drow["CancelLCDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteCancelDate.SelectedDate = DateTime.Parse(drow["CancelLCDate"].ToString());
                }

                if (!string.IsNullOrEmpty(drow["ContingentExpiryDate"].ToString()) && drow["ContingentExpiryDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteContingentExpiryDate.SelectedDate = DateTime.Parse(drow["ContingentExpiryDate"].ToString());
                }
                txtCancelRemark.Text = drow["CancelRemark"].ToString();
                // DocumentaryCollectionCancel
            }
            else
            {
                rcbLCType.SelectedValue = string.Empty;
                rcbApplicantID.SelectedValue = string.Empty;
                tbApplicantName.Text = string.Empty;
                tbApplicantAddr1.Text = string.Empty;
                tbApplicantAddr2.Text = string.Empty;
                tbApplicantAddr3.Text = string.Empty;
                rcbCcyAmount.SelectedValue = string.Empty;
                ntSoTien.Text = string.Empty;
                tbcrTolerance.Text = string.Empty;
                tbdrTolerance.Text = string.Empty;

                tbExpiryPlace.Text = string.Empty;
                rcbAccountOfficer.SelectedValue = string.Empty;
                tbContactNo.Text = string.Empty;

                comboBeneficiaryBankType.SelectedValue = string.Empty;
                txtBeneficiaryNo.Text = string.Empty;
                txtBeneficiaryBankName.Text = string.Empty;
                txtBeneficiaryBankAddr1.Text = string.Empty;
                txtBeneficiaryBankAddr2.Text = string.Empty;
                txtBeneficiaryBankAddr3.Text = string.Empty;
                rcbAdviseBankNo.SelectedValue = string.Empty;

                tbAdviseBankName.Text = string.Empty;
                tbAdviseBankAddr1.Text = string.Empty;
                tbAdviseBankAddr2.Text = string.Empty;
                tbAdviseBankAddr3.Text = string.Empty;

                rcbReimbBankNo.SelectedValue = string.Empty;
                tbReimbBankName.Text = string.Empty;
                tbReimbBankAddr1.Text = string.Empty;
                tbReimbBankAddr2.Text = string.Empty;
                tbReimbBankAddr3.Text = string.Empty;

                rcbAdviseThruNo.SelectedValue = string.Empty;
                tbAdviseThruName.Text = string.Empty;
                tbAdviseThruAddr1.Text = string.Empty;
                tbAdviseThruAddr2.Text = string.Empty;
                tbAdviseThruAddr3.Text = string.Empty;

                rcCommodity.SelectedValue = string.Empty;
                numPro.Value = 0;
                numLcAmountSecured.Value = 0;
                numLcAmountUnSecured.Value = 0;
                numLoanPrincipal.Value = 0;

                var now = DateTime.Now;
                tbIssuingDate.SelectedDate = now;
                //tbContingentExpiry.SelectedDate = now.AddDays(30);
                //tbExpiryDate.SelectedDate = now.AddDays(15);

                dteCancelDate.SelectedDate = DateTime.Now;
                dteContingentExpiryDate.SelectedDate = DateTime.Now;
                txtCancelRemark.Text = "";
            }
            #endregion

            #region tab MT700
            if (dsDoc.Tables[1].Rows.Count > 0)
            {
                var drow = dsDoc.Tables[1].Rows[0];
                ReceivingBank_700 = drow["ReceivingBank"].ToString();

                txtRevivingBank700.Text = drow["ReceivingBank"].ToString();
                tbBaquenceOfTotal.Text = drow["SequenceOfTotal"].ToString();
                comboFormOfDocumentaryCredit.SelectedValue = drow["FormDocumentaryCredit"].ToString();
                lblDocumentaryCreditNumber.Text = drow["DocumentaryCreditNumber"].ToString();
                tbPlaceOfExpiry.Text = drow["PlaceOfExpiry"].ToString();
                comboAvailableRule.SelectedValue = drow["ApplicationRule"].ToString();

                rcbApplicantBankType700.SelectedValue = drow["ApplicantType"].ToString();
                tbApplicantNo700.Text = drow["ApplicantNo"].ToString();
                tbApplicantName700.Text = drow["ApplicantName"].ToString();
                tbApplicantAddr700_1.Text = drow["ApplicantAddr1"].ToString();
                tbApplicantAddr700_2.Text = drow["ApplicantAddr2"].ToString();
                tbApplicantAddr700_3.Text = drow["ApplicantAddr3"].ToString();

                comboBeneficiaryType700.SelectedValue = drow["BeneficiaryType"].ToString();
                txtBeneficiaryNo700.Text = drow["BeneficiaryNo"].ToString();
                txtBeneficiaryName700.Text = drow["BeneficiaryName"].ToString();
                txtBeneficiaryAddr700_1.Text = drow["BeneficiaryAddr1"].ToString();
                txtBeneficiaryAddr700_2.Text = drow["BeneficiaryAddr2"].ToString();
                txtBeneficiaryAddr700_3.Text = drow["BeneficiaryAddr3"].ToString();

                comboCurrency700.SelectedValue = drow["Currency"].ToString();
                numAmount700.Value = (double?)drow["Amount"];
                numPercentCreditAmount1.Value = (double?) drow["PercentageCredit"];
                numPercentCreditAmount2.Value = (double?) drow["AmountTolerance"];
                comboMaximumCreditAmount700.SelectedValue = drow["MaximumCreditAmount"].ToString();

                rcbAvailableWithType.SelectedValue = drow["AvailableWithType"].ToString();
                comboAvailableWithNo.SelectedValue = drow["AvailableWithNo"].ToString();
                tbAvailableWithName.Text = drow["AvailableWithName"].ToString();
                tbAvailableWithAddr1.Text = drow["AvailableWithAddr1"].ToString();
                tbAvailableWithAddr2.Text = drow["AvailableWithAddr2"].ToString();
                tbAvailableWithAddr3.Text = drow["AvailableWithAddr3"].ToString();

                comboAvailableWithBy.SelectedValue = drow["Available_By"].ToString();

                rcbPatialShipment.SelectedValue = drow["PatialShipment"].ToString();
                rcbTranshipment.SelectedValue = drow["Transhipment"].ToString();
                tbPlaceoftakingincharge.Text = drow["PlaceOfTakingInCharge"].ToString();
                tbPortofloading.Text = drow["PortOfLoading"].ToString();
                tbPortofDischarge.Text = drow["PortOfDischarge"].ToString();
                tbPlaceoffinalindistination.Text = drow["PlaceOfFinalInDistination"].ToString();

                tbDescrpofGoods.Text = drow["DescrpGoodsBervices"].ToString();
                tbOrderDocs.Text = drow["DocsRequired"].ToString();
                tbAdditionalConditions.Text = drow["AdditionalConditions"].ToString();
                tbCharges.Text = drow["Charges"].ToString();
                tbPeriodforPresentation.Text = drow["PeriodForPresentation"].ToString();
                rcbConfimationInstructions.SelectedValue = drow["ConfimationInstructions"].ToString();
                tbNegotgBank.Text = drow["InstrToPaygAccptgNegotgBank"].ToString();
                tbSendertoReceiverInfomation.Text = drow["SenderReceiverInfomation"].ToString();

                if (!string.IsNullOrEmpty(drow["LatesDateOfShipment"].ToString())  && drow["LatesDateOfShipment"].ToString().IndexOf("1/1/1900") == -1)
                {
                    tbLatesDateofShipment.SelectedDate = DateTime.Parse(drow["LatesDateOfShipment"].ToString());
                }
                if (!string.IsNullOrEmpty(drow["DateExpiry"].ToString())  && drow["DateExpiry"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteMT700DateAndPlaceOfExpiry.SelectedDate = DateTime.Parse(drow["DateExpiry"].ToString());
                }
                if (!string.IsNullOrEmpty(drow["DateOfIssue"].ToString())  && drow["DateOfIssue"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteDateOfIssue.SelectedDate = DateTime.Parse(drow["DateOfIssue"].ToString());
                }

                comboAdviseThroughBankType700.SelectedValue = drow["AdviseThroughBankType"].ToString();
                comboAdviseThroughBankNo700.SelectedValue = drow["AdviseThroughBankNo"].ToString();
                txtAdviseThroughBankName700.Text = drow["AdviseThroughBankName"].ToString();
                txtAdviseThroughBankAddr700_1.Text = drow["AdviseThroughBankAddr1"].ToString();
                txtAdviseThroughBankAddr700_2.Text = drow["AdviseThroughBankAddr2"].ToString();
                txtAdviseThroughBankAddr700_3.Text = drow["AdviseThroughBankAddr3"].ToString();

                comboReimbBankType700.SelectedValue = drow["ReimbBankType"].ToString();
                rcbReimbBankNo700.SelectedValue = drow["ReimbBankNo"].ToString();
                tbReimbBankName700.Text = drow["ReimbBankName"].ToString();
                tbReimbBankAddr700_1.Text = drow["ReimbBankAddr1"].ToString();
                tbReimbBankAddr700_2.Text = drow["ReimbBankAddr2"].ToString();
                tbReimbBankAddr700_3.Text = drow["ReimbBankAddr3"].ToString();

                txtAdditionalAmountsCovered700_1.Text = drow["AdditionalAmountsCovered1"].ToString();
                txtAdditionalAmountsCovered700_2.Text = drow["AdditionalAmountsCovered2"].ToString();
                txtDraftsAt700_1.Text = drow["DraftsAt1"].ToString();
                txtDraftsAt700_2.Text = drow["DraftsAt2"].ToString();

                txtMixedPaymentDetails700_1.Text = drow["MixedPaymentDetails1"].ToString();
                txtMixedPaymentDetails700_2.Text = drow["MixedPaymentDetails2"].ToString();
                txtMixedPaymentDetails700_3.Text = drow["MixedPaymentDetails3"].ToString();
                txtMixedPaymentDetails700_4.Text = drow["MixedPaymentDetails4"].ToString();

                txtDeferredPaymentDetails700_1.Text = drow["DeferredPaymentDetails1"].ToString();
                txtDeferredPaymentDetails700_2.Text = drow["DeferredPaymentDetails2"].ToString();
                txtDeferredPaymentDetails700_3.Text = drow["DeferredPaymentDetails3"].ToString();
                txtDeferredPaymentDetails700_4.Text = drow["DeferredPaymentDetails4"].ToString();

                txtShipmentPeriod700_1.Text = drow["ShipmentPeriod1"].ToString();
                txtShipmentPeriod700_2.Text = drow["ShipmentPeriod2"].ToString();
                txtShipmentPeriod700_3.Text = drow["ShipmentPeriod3"].ToString();
                txtShipmentPeriod700_4.Text = drow["ShipmentPeriod4"].ToString();
                txtShipmentPeriod700_5.Text = drow["ShipmentPeriod3"].ToString();
                txtShipmentPeriod700_6.Text = drow["ShipmentPeriod4"].ToString();

                comboDraweeCusType.SelectedValue = drow["DraweeType"].ToString();
                comboDraweeCusNo700.SelectedValue = drow["DraweeNo"].ToString();
                txtDraweeCusName.Text = drow["DraweeName"].ToString();
                txtDraweeAddr1.Text = drow["DraweeAddr1"].ToString();
                txtDraweeAddr2.Text = drow["DraweeAddr2"].ToString();
                txtDraweeAddr3.Text = drow["DraweeAddr3"].ToString();
            }
            else
            {
                txtRevivingBank700.Text = string.Empty;
                tbBaquenceOfTotal.Text = string.Empty;
                comboFormOfDocumentaryCredit.SelectedValue = string.Empty;
                lblDocumentaryCreditNumber.Text = string.Empty;
                tbPlaceOfExpiry.Text = string.Empty;
                comboAvailableRule.SelectedValue = "EUCP LASTED VERSION";

                rcbApplicantBankType700.SelectedValue = string.Empty;
                tbApplicantNo700.Text = string.Empty;
                tbApplicantName700.Text = string.Empty;
                tbApplicantAddr700_1.Text = string.Empty;
                tbApplicantAddr700_2.Text = string.Empty;
                tbApplicantAddr700_3.Text = string.Empty;

                comboBeneficiaryType700.SelectedValue = "D";
                txtBeneficiaryNo700.Text = string.Empty;
                txtBeneficiaryName700.Text = string.Empty;
                txtBeneficiaryAddr700_1.Text = string.Empty;
                txtBeneficiaryAddr700_2.Text = string.Empty;
                txtBeneficiaryAddr700_3.Text = string.Empty;

                comboCurrency700.SelectedValue = string.Empty;
                numAmount700.Value = 0;
                numPercentCreditAmount1.Value = 0;
                numPercentCreditAmount2.Value = 0;
                comboMaximumCreditAmount700.SelectedValue = string.Empty;

                rcbAvailableWithType.SelectedValue = string.Empty;
                rcbAvailableWithType.SelectedValue = string.Empty;
                tbAvailableWithName.Text = string.Empty;
                tbAvailableWithAddr1.Text = string.Empty;
                tbAvailableWithAddr2.Text = string.Empty;
                tbAvailableWithAddr3.Text = string.Empty;

                comboAvailableWithBy.SelectedValue = string.Empty;
                
                rcbPatialShipment.SelectedValue = string.Empty;
                rcbTranshipment.SelectedValue = string.Empty;
                tbPlaceoftakingincharge.Text = string.Empty;
                tbPortofloading.Text = string.Empty;
                tbPortofDischarge.Text = string.Empty;
                tbPlaceoffinalindistination.Text = string.Empty;

                comboAdviseThroughBankType700.SelectedValue = string.Empty;
                comboAdviseThroughBankNo700.SelectedValue = string.Empty;
                txtAdviseThroughBankName700.Text = string.Empty;
                txtAdviseThroughBankAddr700_1.Text = string.Empty;
                txtAdviseThroughBankAddr700_2.Text = string.Empty;
                txtAdviseThroughBankAddr700_3.Text = string.Empty;

                comboReimbBankType700.SelectedValue = string.Empty;
                rcbReimbBankNo700.SelectedValue = string.Empty;
                tbReimbBankName700.Text = string.Empty;
                tbReimbBankAddr700_1.Text = string.Empty;
                tbReimbBankAddr700_2.Text = string.Empty;
                tbReimbBankAddr700_3.Text = string.Empty;

                txtAdditionalAmountsCovered700_1.Text = string.Empty;
                txtAdditionalAmountsCovered700_2.Text = string.Empty;
                txtDraftsAt700_1.Text = string.Empty;
                txtDraftsAt700_2.Text = string.Empty;

                txtMixedPaymentDetails700_1.Text = string.Empty;
                txtMixedPaymentDetails700_2.Text = string.Empty;
                txtMixedPaymentDetails700_3.Text = string.Empty;
                txtMixedPaymentDetails700_4.Text = string.Empty;

                txtDeferredPaymentDetails700_1.Text = string.Empty;
                txtDeferredPaymentDetails700_2.Text = string.Empty;

                txtShipmentPeriod700_1.Text = string.Empty;
                txtShipmentPeriod700_2.Text = string.Empty;
                txtShipmentPeriod700_3.Text = string.Empty;
                txtShipmentPeriod700_4.Text = string.Empty;
                txtShipmentPeriod700_5.Text = string.Empty;
                txtShipmentPeriod700_6.Text = string.Empty;

                comboDraweeCusType.SelectedValue = string.Empty;
                comboDraweeCusNo700.SelectedValue = string.Empty;
                txtDraweeCusName.Text = string.Empty;
                txtDraweeAddr1.Text = string.Empty;
                txtDraweeAddr2.Text = string.Empty;
                txtDraweeAddr3.Text = string.Empty;
            }
            #endregion

            #region tab MT740
            if (dsDoc.Tables[2].Rows.Count > 0)
            {
                var drow = dsDoc.Tables[2].Rows[0];

                comGenerate.SelectedValue = drow["GenerateMT740"].ToString();
                Generate740 = comGenerate.SelectedValue;
                ReceivingBank_740 = drow["ReceivingBank"].ToString();

                txtRemittingBankNo.Text = drow["ReceivingBank"].ToString();
                lblDocumentaryCreditNumber740.Text = drow["DocumentaryCreditNumber"].ToString();
                tb31DPlaceOfExpiry.Text = drow["PlaceExpiry"].ToString();

                rcbBeneficiaryType740.SelectedValue = drow["BeneficiaryType"].ToString();
                tbBeneficiaryNo740.Text = drow["BeneficiaryNo"].ToString();
                tbBeneficiaryName740.Text = drow["BeneficiaryName"].ToString();
                tbBeneficiaryAddr740_1.Text = drow["BeneficiaryAddr1"].ToString();
                tbBeneficiaryAddr740_2.Text = drow["BeneficiaryAddr2"].ToString();
                tbBeneficiaryAddr740_3.Text = drow["BeneficiaryAddr3"].ToString();

                numCreditAmount.Value = (double?) drow["CreditAmount"];
                comboCreditCurrency.SelectedValue = drow["CreditCurrency"].ToString();
                lblApplicableRule740.Text = drow["ApplicableRule"].ToString();
                numPercentageCreditAmountTolerance740_1.Value = (double?)drow["PercentageCreditAmountTolerance1"];
                numPercentageCreditAmountTolerance740_2.Value = (double?)drow["PercentageCreditAmountTolerance2"];

                rcbAvailableWithType740.SelectedValue = drow["AvailableWithType"].ToString();
                tbAvailableWithNo740.Text = drow["AvailableWithNo"].ToString();
                tbAvailableWithName740.Text = drow["AvailableWithName"].ToString();
                tbAvailableWithAddr740_1.Text = drow["AvailableWithAddr1"].ToString();
                tbAvailableWithAddr740_2.Text = drow["AvailableWithAddr2"].ToString();
                tbAvailableWithAddr740_3.Text = drow["AvailableWithAddr3"].ToString();
                
                comboReimbursingBankChange.SelectedValue = drow["ReimbursingBankChanges"].ToString();

                if (!string.IsNullOrEmpty(drow["DateExpiry"].ToString()) && drow["DateExpiry"].ToString().IndexOf("1/1/1900", StringComparison.Ordinal) == -1)
                {
                    tbExpiryDate740.SelectedDate = DateTime.Parse(drow["DateExpiry"].ToString());
                }

                txtSenderToReceiverInformation740_1.Text = drow["SenderToReceiverInformation1"].ToString();
                txtSenderToReceiverInformation740_2.Text = drow["SenderToReceiverInformation2"].ToString();
                txtSenderToReceiverInformation740_3.Text = drow["SenderToReceiverInformation3"].ToString();
                txtSenderToReceiverInformation740_4.Text = drow["SenderToReceiverInformation4"].ToString();

                txtDraftsAt740_1.Text = drow["DraftsAt1"].ToString();
                txtDraftsAt740_2.Text = drow["DraftsAt2"].ToString();
            }
            else
            {
                comGenerate.SelectedValue = string.Empty;
                txtRemittingBankNo.Text = string.Empty;
                lblDocumentaryCreditNumber740.Text = string.Empty;
                tb31DPlaceOfExpiry.Text = string.Empty;

                rcbBeneficiaryType740.SelectedValue = "D";
                tbBeneficiaryNo740.Text = string.Empty;
                tbBeneficiaryName740.Text = string.Empty;
                tbBeneficiaryAddr740_1.Text = string.Empty;
                tbBeneficiaryAddr740_2.Text = string.Empty;
                tbBeneficiaryAddr740_3.Text = string.Empty;

                numCreditAmount.Value = 0;
                comboCreditCurrency.SelectedValue = string.Empty;

                rcbAvailableWithType740.SelectedValue = string.Empty;
                tbAvailableWithNo740.Text = string.Empty;
                tbAvailableWithName740.Text = string.Empty;
                tbAvailableWithAddr740_1.Text = string.Empty;
                tbAvailableWithAddr740_2.Text = string.Empty;
                tbAvailableWithAddr740_3.Text = string.Empty;
                
                comboReimbursingBankChange.SelectedValue = string.Empty;

                lblApplicableRule740.Text = comboAvailableRule.SelectedValue;
                numPercentageCreditAmountTolerance740_1.Value = 0;
                numPercentageCreditAmountTolerance740_2.Value = 0;

                txtDraftsAt740_1.Text = string.Empty;
                txtDraftsAt740_2.Text = string.Empty;

                
            }
            #endregion

            #region Tab Charges
            if (dsDoc.Tables[3].Rows.Count > 0)
            {
                var drow1 = dsDoc.Tables[3].Rows[0];
                WaiveCharges = drow1["WaiveCharges"].ToString();

                comboWaiveCharges.SelectedValue = WaiveCharges;

                tbChargeRemarks.Text = drow1["ChargeRemarks"].ToString();
                tbVatNo.Text = drow1["VATNo"].ToString();
                

                rcbChargeCcy.SelectedValue = drow1["ChargeCcy"].ToString();
                if (!string.IsNullOrEmpty(rcbChargeCcy.SelectedValue))
                {
                    LoadChargeAcct(ref rcbChargeAcct);
                    rcbChargeAcct.SelectedValue = drow1["ChargeAcct"].ToString();    
                }
                
                tbChargeAmt.Value = (double?) drow1["ChargeAmt"];
                rcbPartyCharged.SelectedValue = drow1["PartyCharged"].ToString();
                rcbOmortCharge.SelectedValue = drow1["OmortCharges"].ToString();
                rcbChargeStatus.SelectedValue = drow1["ChargeStatus"].ToString();
                lblTaxCode.Text = drow1["TaxCode"].ToString();
                lblTaxAmt.Text = String.Format("{0:C}", drow1["TaxAmt"]).Replace("$", "");
            }
            else
            {
                rcbChargeAcct.SelectedValue = string.Empty;
                rcbChargeCcy.SelectedValue = string.Empty;
                tbChargeAmt.Value = 0;
                rcbPartyCharged.SelectedValue = "A";
                rcbChargeStatus.SelectedValue = string.Empty;
                lblTaxCode.Text = string.Empty;
                lblTaxAmt.Text = string.Empty;
            }

            if (dsDoc.Tables[4].Rows.Count > 0)
            {
                var drow1 = dsDoc.Tables[4].Rows[0];

                rcbChargeCcy2.SelectedValue = drow1["ChargeCcy"].ToString();
                if (!string.IsNullOrEmpty(rcbChargeCcy2.SelectedValue))
                {
                    LoadChargeAcct(ref rcbChargeAcct2);
                    rcbChargeAcct2.SelectedValue = drow1["ChargeAcct"].ToString();    
                }
                
                tbChargeAmt2.Value = (double?) drow1["ChargeAmt"];
                rcbPartyCharged2.SelectedValue = drow1["PartyCharged"].ToString();
                rcbOmortCharges2.SelectedValue = drow1["OmortCharges"].ToString();
                rcbChargeStatus2.SelectedValue = drow1["ChargeStatus"].ToString();
                lblTaxCode2.Text = drow1["TaxCode"].ToString();
                lblTaxAmt2.Text = String.Format("{0:C}", drow1["TaxAmt"]).Replace("$", "");
            }
            else
            {
                rcbChargeAcct2.SelectedValue = string.Empty;
                rcbChargeCcy2.SelectedValue = string.Empty;
                tbChargeAmt2.Value = 0;
                rcbPartyCharged2.SelectedValue = string.Empty;
                rcbOmortCharges2.SelectedValue = string.Empty;
                rcbChargeStatus2.SelectedValue = string.Empty;
                lblTaxCode2.Text = string.Empty;
                lblTaxAmt2.Text = string.Empty;
            }

            if (dsDoc.Tables[5].Rows.Count > 0)
            {
                var drow1 = dsDoc.Tables[5].Rows[0];

                rcbChargeCcy3.SelectedValue = drow1["ChargeCcy"].ToString();
                if (!string.IsNullOrEmpty(rcbChargeCcy3.SelectedValue))
                {
                    LoadChargeAcct(ref rcbChargeAcct3);
                    rcbChargeAcct3.SelectedValue = drow1["ChargeAcct"].ToString();    
                }
                
                tbChargeAmt3.Value = (double?) drow1["ChargeAmt"];
                rcbPartyCharged3.SelectedValue = drow1["PartyCharged"].ToString();
                rcbOmortCharges3.SelectedValue = drow1["OmortCharges"].ToString();
                rcbChargeStatus3.SelectedValue = drow1["ChargeStatus"].ToString();
                lblTaxCode3.Text = drow1["TaxCode"].ToString();
                lblTaxAmt3.Text = String.Format("{0:C}", drow1["TaxAmt"]).Replace("$", "");
            }
            else
            {
                rcbChargeAcct3.SelectedValue = string.Empty;
                rcbChargeCcy3.SelectedValue = string.Empty;
                tbChargeAmt3.Value = 0;
                rcbPartyCharged3.SelectedValue = "A";
                rcbOmortCharges3.SelectedValue = string.Empty;
                rcbChargeStatus3.SelectedValue = string.Empty;
                lblTaxCode3.Text = string.Empty;
                lblTaxAmt3.Text = string.Empty;
            }
            #endregion

            #region tab MT747

            if (dsDoc.Tables[6].Rows.Count > 0)
            {
                var drow = dsDoc.Tables[6].Rows[0];
                comboGenerateMT747.SelectedValue = drow["GenerateMT747"].ToString();
                txtReceivingBank_747.Text = drow["ReceivingBank"].ToString();
                lblDocumentaryCreditNumber_747.Text = drow["DocumentaryCreditNumber"].ToString();
                comboReimbBankType_747.SelectedValue = drow["ReimbBankType"].ToString();
                comboReimbBankNo_747.SelectedValue = drow["ReimbBankNo"].ToString();
                txtReimbBankName_747.Text = drow["ReimbBankName"].ToString();
                txtReimbBankAddr_747_1.Text = drow["ReimbBankAddr1"].ToString();
                txtReimbBankAddr_747_2.Text = drow["ReimbBankAddr2"].ToString();
                txtReimbBankAddr_747_3.Text = drow["ReimbBankAddr3"].ToString();

                if (!string.IsNullOrEmpty(drow["DateOriginalAuthorization"].ToString()) &&
                    drow["DateOriginalAuthorization"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteDateOfOriginalAuthorization_747.SelectedDate =
                        DateTime.Parse(drow["DateOriginalAuthorization"].ToString());
                }

                if (!string.IsNullOrEmpty(drow["DateOfExpiry"].ToString()) &&
                    drow["DateOfExpiry"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteNewDateOfExpiry_747.SelectedDate = DateTime.Parse(drow["DateOfExpiry"].ToString());
                }

                comboCurrency_747.SelectedValue = drow["Currency"].ToString();
                numAmount_747.Text = drow["Amount"].ToString();
                numPercentageCreditTolerance_747_1.Text = drow["PercentageCreditAmountTolerance1"].ToString();
                numPercentageCreditTolerance_747_2.Text = drow["PercentageCreditAmountTolerance2"].ToString();
                comboMaximumCreditAmount_747.SelectedValue = drow["MaximumCreditAmount"].ToString();

                txtAdditionalCovered_747_1.Text = drow["AdditionalCovered1"].ToString();
                txtAdditionalCovered_747_2.Text = drow["AdditionalCovered2"].ToString();
                txtAdditionalCovered_747_3.Text = drow["AdditionalCovered3"].ToString();
                txtAdditionalCovered_747_4.Text = drow["AdditionalCovered4"].ToString();

                txtSenderToReceiverInfomation_747_1.Text = drow["SenderToReceiverInformation1"].ToString();
                txtSenderToReceiverInfomation_747_2.Text = drow["SenderToReceiverInformation2"].ToString();
                txtSenderToReceiverInfomation_747_3.Text = drow["SenderToReceiverInformation3"].ToString();
                txtSenderToReceiverInfomation_747_4.Text = drow["SenderToReceiverInformation4"].ToString();

                GenerateMT747();
            }
            else
            {
                lblDocumentaryCreditNumber_747.Text = string.Empty;
                txtReceivingBank_747.Text = string.Empty;

                comboReimbBankType_747.SelectedValue = string.Empty;
                comboReimbBankNo_747.SelectedValue = string.Empty;
                txtReimbBankName_747.Text = string.Empty;
                txtReimbBankAddr_747_1.Text = string.Empty;
                txtReimbBankAddr_747_2.Text = string.Empty;
                txtReimbBankAddr_747_3.Text = string.Empty;

                numPercentageCreditTolerance_747_1.Value = 0;
                numPercentageCreditTolerance_747_1.Value = 0;

                comboGenerateMT747.SelectedValue = string.Empty;

                dteDateOfOriginalAuthorization_747.SelectedDate = DateTime.Now;
                dteNewDateOfExpiry_747.SelectedDate = null;

                comboCurrency_747.SelectedValue = string.Empty;
                numAmount_747.Text = string.Empty;
                comboMaximumCreditAmount_747.SelectedValue = string.Empty;

                txtAdditionalCovered_747_1.Text = string.Empty;
                txtAdditionalCovered_747_2.Text = string.Empty;
                txtAdditionalCovered_747_3.Text = string.Empty;
                txtAdditionalCovered_747_4.Text = string.Empty;

                txtSenderToReceiverInfomation_747_1.Text = string.Empty;
                txtSenderToReceiverInfomation_747_2.Text = string.Empty;
                txtSenderToReceiverInfomation_747_3.Text = string.Empty;
                txtSenderToReceiverInfomation_747_4.Text = string.Empty;

                // set default values
                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    var drow = dsDoc.Tables[0].Rows[0];

                    lblDocumentaryCreditNumber_747.Text = drow["NormalLCCode"].ToString();
                    txtReceivingBank_747.Text = drow["ReimbBankNo"].ToString();

                    comboReimbBankType_747.SelectedValue = drow["ReimbBankType"].ToString();
                    comboReimbBankNo_747.SelectedValue = drow["ReimbBankNo"].ToString();
                    txtReimbBankName_747.Text = drow["ReimbBankName"].ToString();
                    txtReimbBankAddr_747_1.Text = drow["ReimbBankAddr1"].ToString();
                    txtReimbBankAddr_747_2.Text = drow["ReimbBankAddr2"].ToString();
                    txtReimbBankAddr_747_3.Text = drow["ReimbBankAddr3"].ToString();

                    numPercentageCreditTolerance_747_1.Text = drow["CrTolerance"].ToString();
                    numPercentageCreditTolerance_747_1.Text = drow["DrTolerance"].ToString();

                    comboCurrency_747.SelectedValue = drow["Currency"].ToString();
                    numAmount_747.Text = drow["Amount"].ToString();
                }

                if (dsDoc.Tables[2].Rows.Count > 0)
                {
                    var drow740 = dsDoc.Tables[2].Rows[0];

                    txtSenderToReceiverInfomation_747_1.Text = drow740["SenderToReceiverInformation1"].ToString();
                    txtSenderToReceiverInfomation_747_2.Text = drow740["SenderToReceiverInformation2"].ToString();
                    txtSenderToReceiverInfomation_747_3.Text = drow740["SenderToReceiverInformation3"].ToString();
                    txtSenderToReceiverInfomation_747_4.Text = drow740["SenderToReceiverInformation4"].ToString();
                }

                GenerateMT747();
            }

            #endregion

            #region tab MT707

            if (dsDoc.Tables[7].Rows.Count > 0)
            {
                var drow = dsDoc.Tables[7].Rows[0];

                txtReceivingBankId_707.Text = drow["ReceivingBank"].ToString();
                lblSenderReference_707.Text = drow["SenderReference"].ToString();
                txtReceiverReference_707.Text = drow["ReceiverReference"].ToString();
                txtReferenceToPreAdvice_707.Text = drow["ReferenceToPreAdvice"].ToString();

                txtIssuingBankReferenceNo_707.Text = drow["IssuingBankNo"].ToString();
                txtIssuingBankReferenceName_707.Text = drow["IssuingBankName"].ToString();
                txtIssuingBankReferenceAddr_707_1.Text = drow["IssuingBankAddr1"].ToString();
                txtIssuingBankReferenceAddr_707_2.Text = drow["IssuingBankAddr2"].ToString();
                txtIssuingBankReferenceAddr_707_3.Text = drow["IssuingBankAddr3"].ToString();

                if (!string.IsNullOrEmpty(drow["DateOfIssue"].ToString()) &&
                    drow["DateOfIssue"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteDateOfIssue_707.SelectedDate = DateTime.Parse(drow["DateOfIssue"].ToString());
                }

                comboAvailableRule_707.SelectedValue = drow["ApplicableRule"].ToString();

                if (!string.IsNullOrEmpty(drow["DateOfAmendment"].ToString()) &&
                    drow["DateOfAmendment"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteDateOfAmendment_707.SelectedDate = DateTime.Parse(drow["DateOfAmendment"].ToString());
                }

                comboBeneficiaryType_707.SelectedValue = drow["BeneficiaryType"].ToString();
                txtBeneficiaryNo_707.Text = drow["BeneficiaryNo"].ToString();
                txtBeneficiaryName_707.Text = drow["BeneficiaryName"].ToString();
                txtBeneficiaryAddr_707_1.Text = drow["BeneficiaryAddr1"].ToString();
                txtBeneficiaryAddr_707_2.Text = drow["BeneficiaryAddr2"].ToString();
                txtBeneficiaryAddr_707_3.Text = drow["BeneficiaryAddr3"].ToString();

                if (!string.IsNullOrEmpty(drow["NewDateOfExpiry"].ToString()) &&
                    drow["NewDateOfExpiry"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteNewDateOfExpiry_707.SelectedDate = DateTime.Parse(drow["NewDateOfExpiry"].ToString());
                }

                numPercentageCreditAmountTolerance_707_1.Text = drow["PercentageCreditAmountTolerance1"].ToString();
                numPercentageCreditAmountTolerance_707_2.Text = drow["PercentageCreditAmountTolerance2"].ToString();

                comboMaximumCreditAmount_707.SelectedValue = drow["MaximumCreditAmount"].ToString();

                txtAdditionalAmountsCovered_707_1.Text = drow["AdditionalAmountsCovered1"].ToString();
                txtAdditionalAmountsCovered_707_2.Text = drow["AdditionalAmountsCovered2"].ToString();

                txtPlaceoftakingincharge_707.Text = drow["PlaceOfTakingInCharge"].ToString();
                txtPlaceoffinalindistination_707.Text = drow["PlaceOfFinalInDistination"].ToString();

                if (!string.IsNullOrEmpty(drow["LatesDateOfShipment"].ToString()) &&
                    drow["LatesDateOfShipment"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteLatesDateofShipment_707.SelectedDate = DateTime.Parse(drow["LatesDateOfShipment"].ToString());
                }


                txtShipmentPeriod_707_1.Text = drow["ShipmentPeriod1"].ToString();
                txtShipmentPeriod_707_2.Text = drow["ShipmentPeriod2"].ToString();
                txtShipmentPeriod_707_3.Text = drow["ShipmentPeriod3"].ToString();
                txtShipmentPeriod_707_4.Text = drow["ShipmentPeriod4"].ToString();
                txtShipmentPeriod_707_5.Text = drow["ShipmentPeriod5"].ToString();
                txtShipmentPeriod_707_6.Text = drow["ShipmentPeriod6"].ToString();

                txtPortofloading_707.Text = drow["PortOfLoading"].ToString();
                txtPortofDischarge_707.Text = drow["PortOfDischarge"].ToString();
                txtNarrative_707.Text = drow["Narrative"].ToString();

                txtSenderToReceiverInformation_707_1.Text = drow["SenderReceiverInfomation1"].ToString();
                txtSenderToReceiverInformation_707_2.Text = drow["SenderReceiverInfomation2"].ToString();
                txtSenderToReceiverInformation_707_3.Text = drow["SenderReceiverInfomation3"].ToString();
                txtSenderToReceiverInformation_707_4.Text = drow["SenderReceiverInfomation4"].ToString();
                txtSenderToReceiverInformation_707_5.Text = drow["SenderReceiverInfomation5"].ToString();
                txtSenderToReceiverInformation_707_6.Text = drow["SenderReceiverInfomation6"].ToString();
            }
            else
            {
                txtReceivingBankId_707.Text = string.Empty;
                lblSenderReference_707.Text = string.Empty;
                numPercentageCreditAmountTolerance_707_1.Value = 0;
                numPercentageCreditAmountTolerance_707_2.Value = 0;

                comboBeneficiaryType_707.SelectedValue = string.Empty;
                txtBeneficiaryNo_707.Text = string.Empty;
                txtBeneficiaryName_707.Text = string.Empty;
                txtBeneficiaryAddr_707_1.Text = string.Empty;
                txtBeneficiaryAddr_707_2.Text = string.Empty;
                txtBeneficiaryAddr_707_3.Text = string.Empty;

                txtReferenceToPreAdvice_707.Text = string.Empty;
                txtIssuingBankReferenceNo_707.Text = string.Empty;
                txtIssuingBankReferenceName_707.Text = string.Empty;
                txtIssuingBankReferenceAddr_707_1.Text = string.Empty;
                txtIssuingBankReferenceAddr_707_2.Text = string.Empty;
                txtIssuingBankReferenceAddr_707_3.Text = string.Empty;

                txtReceiverReference_707.Text = string.Empty;

                dteDateOfIssue_707.SelectedDate = DateTime.Now;
                comboAvailableRule_707.SelectedValue = string.Empty;
                dteDateOfAmendment_707.SelectedDate = DateTime.Now;

                dteNewDateOfExpiry_707.SelectedDate = null;
                comboMaximumCreditAmount_707.SelectedValue = string.Empty;
                txtAdditionalAmountsCovered_707_1.Text = string.Empty;
                txtAdditionalAmountsCovered_707_2.Text = string.Empty;
                txtPlaceoftakingincharge_707.Text = string.Empty;
                txtPlaceoffinalindistination_707.Text = string.Empty;
                dteLatesDateofShipment_707.SelectedDate = null;

                txtShipmentPeriod_707_1.Text = string.Empty;
                txtShipmentPeriod_707_2.Text = string.Empty;
                txtShipmentPeriod_707_3.Text = string.Empty;
                txtShipmentPeriod_707_4.Text = string.Empty;
                txtShipmentPeriod_707_5.Text = string.Empty;
                txtShipmentPeriod_707_6.Text = string.Empty;

                txtPortofloading_707.Text = string.Empty;
                txtPortofDischarge_707.Text = string.Empty;
                txtNarrative_707.Text = string.Empty;

                txtSenderToReceiverInformation_707_1.Text = "PLEASE ADVISE THIS AMENDMENT TO THE BENEFICIARY THROUGH";
                txtSenderToReceiverInformation_707_2.Text = string.Empty;
                txtSenderToReceiverInformation_707_3.Text = string.Empty;
                txtSenderToReceiverInformation_707_4.Text = string.Empty;
                txtSenderToReceiverInformation_707_5.Text = string.Empty;
                txtSenderToReceiverInformation_707_6.Text = string.Empty;

                // set default values
                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    var drow = dsDoc.Tables[0].Rows[0];

                    txtReceivingBankId_707.Text = drow["ReimbBankNo"].ToString();
                    lblSenderReference_707.Text = drow["NormalLCCode"].ToString();
                    numPercentageCreditAmountTolerance_707_1.Text = drow["CrTolerance"].ToString();
                    numPercentageCreditAmountTolerance_707_2.Text = drow["DrTolerance"].ToString();

                    comboBeneficiaryType_707.SelectedValue = drow["BeneficiaryType"].ToString();
                    txtBeneficiaryNo_707.Text = drow["BeneficiaryNo"].ToString();
                    txtBeneficiaryName_707.Text = drow["BeneficiaryName"].ToString();
                    txtBeneficiaryAddr_707_1.Text = drow["BeneficiaryAddr1"].ToString();
                    txtBeneficiaryAddr_707_2.Text = drow["BeneficiaryAddr2"].ToString();
                    txtBeneficiaryAddr_707_3.Text = drow["BeneficiaryAddr3"].ToString();
                }                

                if (dsDoc.Tables[1].Rows.Count > 0)
                {
                    var drow700 = dsDoc.Tables[1].Rows[0];

                    txtIssuingBankReferenceNo_707.Text = drow700["DraweeNo"].ToString();
                    txtIssuingBankReferenceName_707.Text = drow700["DraweeName"].ToString();
                    txtIssuingBankReferenceAddr_707_1.Text = drow700["DraweeAddr1"].ToString();
                    txtIssuingBankReferenceAddr_707_2.Text = drow700["DraweeAddr2"].ToString();
                    txtIssuingBankReferenceAddr_707_3.Text = drow700["DraweeAddr3"].ToString();

                    comboMaximumCreditAmount_707.SelectedValue = drow700["MaximumCreditAmount"].ToString();

                    txtAdditionalAmountsCovered_707_1.Text = drow700["AdditionalAmountsCovered1"].ToString();
                    txtAdditionalAmountsCovered_707_2.Text = drow700["AdditionalAmountsCovered2"].ToString();

                    txtPlaceoftakingincharge_707.Text = drow700["PlaceOfTakingInCharge"].ToString();
                    txtPlaceoffinalindistination_707.Text = drow700["PlaceOfFinalInDistination"].ToString();

                    if (!string.IsNullOrEmpty(drow700["LatesDateOfShipment"].ToString()) &&
                    drow700["LatesDateOfShipment"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteLatesDateofShipment_707.SelectedDate = DateTime.Parse(drow700["LatesDateOfShipment"].ToString());
                    }

                    if (!string.IsNullOrEmpty(drow700["DateExpiry"].ToString()) &&
                    drow700["DateExpiry"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteNewDateOfExpiry_707.SelectedDate = DateTime.Parse(drow700["DateExpiry"].ToString());
                    }                    

                    txtShipmentPeriod_707_1.Text = drow700["ShipmentPeriod1"].ToString();
                    txtShipmentPeriod_707_2.Text = drow700["ShipmentPeriod2"].ToString();
                    txtShipmentPeriod_707_3.Text = drow700["ShipmentPeriod3"].ToString();
                    txtShipmentPeriod_707_4.Text = drow700["ShipmentPeriod4"].ToString();
                    txtShipmentPeriod_707_5.Text = drow700["ShipmentPeriod5"].ToString();
                    txtShipmentPeriod_707_6.Text = drow700["ShipmentPeriod6"].ToString();

                    txtPortofloading_707.Text = drow700["PortOfLoading"].ToString();
                    txtPortofDischarge_707.Text = drow700["PortOfDischarge"].ToString();
                }                
            }

            #endregion

            if (string.IsNullOrEmpty(tbVatNo.Text))
            {
                GenerateVAT();
            }

            SetRelation_BeneficiaryBank();
            SetRelation_ReimbBankType();
            SetRelation_AvailableWithType();
            SetRelation_AdviseThruType();

            SetRelation_ApplicantBankType700();
            SetRelation_DraweeCusType700();

            SetRelation_Beneficiary740();

            GenerateMT740();

            if (TabId == TabIssueLCCancel && Request.QueryString["disable"] == null) //Cancel LC
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);

                txtCode.Enabled = true;
                dteCancelDate.Enabled = true;
                dteContingentExpiryDate.Enabled = true;
                txtCancelRemark.Enabled = true;


                //===========================
                //if (drowLC != null && drowLC["Cancel_Status"].ToString() != bd.TransactionStatus.AUT)
               // {
                    comboWaiveCharges.Enabled = true;
                    tbChargeRemarks.Enabled = true;

                    rcbChargeAcct.Enabled = true;
                    tbChargeAmt.Enabled = true;
                    rcbPartyCharged.Enabled = true;
                    rcbChargeCcy.Enabled = true;
                    rcbOmortCharge.Enabled = true;

                    rcbChargeCcy2.Enabled = true;
                    rcbChargeAcct2.Enabled = true;
                    tbChargeAmt2.Enabled = true;
                    rcbPartyCharged2.Enabled = true;
                    rcbOmortCharges2.Enabled = true;

                    rcbChargeCcy3.Enabled = true;
                    rcbChargeAcct3.Enabled = true;
                    tbChargeAmt3.Enabled = true;
                    rcbPartyCharged3.Enabled = true;
                    rcbOmortCharges3.Enabled = true;
               // }
            }
        }

        protected void SetVisibilityByStatus(ref DataRow drow)
        {
            // Issue LC => tabid=92
            // Amend LC => tabid=204
            // Cancel LC => tabid=205
            if (drow == null)
            {
                return;
            }
            lblError.Text = "";
            const string errorUn_AUT = "This Issue LC has Not Authorized yet. Do not allow to process Payment Acceptance!";
            switch (TabId)
            {
                case TabIssueLCAddNew: // Issue LC
                    if (Request.QueryString["key"] == null)
                    {
                        if (drow["Status"].ToString() == bd.TransactionStatus.AUT && drow["PaymentFullFlag"].ToString() == "1")
                        {
                            lblError.Text = "This LC has payment full";
                            LoadToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                        }
                        else if (drow["Cancel_Status"].ToString() == bd.TransactionStatus.AUT)
                        {
                            lblError.Text = "This LC was canceled";
                            LoadToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                        }
                        else if (drow["Status"].ToString() == bd.TransactionStatus.AUT)
                        {
                            lblError.Text = "This LC was authorized";
                            LoadToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                        }
                    }
                    break;
                case TabIssueLCAmend: //Amend LC
                    if (Request.QueryString["key"] == null)
                    {
                        //Chỉ cho phép tu chỉnh đối với BCT:
                        //1. Đã authorised màn hình nhập nhờ thu (Reg)
                        //2, Chưa thực hiện cancel
                        //3, Chưa thực hiện thanh toán full 
                        if (drow["Status"].ToString() != bd.TransactionStatus.AUT)
                        {
                            lblError.Text = errorUn_AUT;
                            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                        }
                        else if (drow["Amend_Status"].ToString() == bd.TransactionStatus.AUT)
                        {
                            lblError.Text = "This LC was authorized";

                            LoadToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;

                        }
                        else if (drow["Cancel_Status"].ToString() == bd.TransactionStatus.AUT)
                        {
                            lblError.Text = "This LC is cancel";

                            LoadToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;

                        }
                        else if (drow["PaymentFullFlag"].ToString() == "1")
                        {
                            lblError.Text = "This LC has payment full";

                            LoadToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                        }
                    }
                    break;
                case TabIssueLCCancel: //  Cancel LC
                    if (Request.QueryString["key"] == null)
                    {
                        //Chỉ cho cancel khi BCT:
                        //1, Nhập BCT đã được authorised
                        //2, Chưa thực hiện thanh toán full
                        if (drow["Status"].ToString() != bd.TransactionStatus.AUT)
                        {
                            lblError.Text = errorUn_AUT;
                            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                        }
                        else if (drow["Cancel_Status"].ToString() == bd.TransactionStatus.AUT)
                        {
                            lblError.Text = "This LC is cancel";

                            LoadToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                        }
                        else if (drow["PaymentFullFlag"].ToString() == "1")
                        {
                            lblError.Text = "This LC has payment full";

                            LoadToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                        }
                    }
                    break;
                
            }
        }              
        
        #region Module Report
        protected void btnIssueLC_MT700Report_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/IssueLC_MT700.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = bd.SQLData.B_BIMPORT_NORMAILLC_MT700_Report(txtCode.Text);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("IssueLC_MT700_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
        protected void btnIssueLC_MT740Report_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/IssueLC_MT740.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = bd.SQLData.B_BIMPORT_NORMAILLC_MT740_Report(txtCode.Text);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("IssueLC_MT740_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
        protected void btnIssueLC_VATReport_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/IssueLC_VAT.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = bd.SQLData.B_BIMPORT_NORMAILLC_VAT_Report(txtCode.Text, UserInfo.Username, TabId);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("IssueLC_VAT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
        protected void btnIssueLC_NHapNgoaiBangReport_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/IssueLC_PHIEUNHAPNGOAIBANG.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = bd.SQLData.B_BIMPORT_NORMAILLC_PHIEUNHAPNGOAIBANG_Report(txtCode.Text, UserInfo.Username, TabId);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("IssueLC_PHIEUNHAPNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }

        protected void btnAmentLCReport_XuatNgoaiBang_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/AmendLC_PHIEUXUATNGOAIBANG.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = bd.SQLData.B_BIMPORT_NORMAILLC_AMEND_PHIEUXUATNGOAIBANG_REPORT(txtCode.Text, UserInfo.Username);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("AmendLC_PHIEUXUATNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
        protected void btnAmentLCReport_NhapNgoaiBang_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/AmendLC_PHIEUNHAPNGOAIBANG.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = bd.SQLData.B_BIMPORT_NORMAILLC_AMEND_PHIEUNHAPNGOAIBANG_REPORT(txtCode.Text, UserInfo.Username);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("AmendLC_PHIEUNHAPNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
        protected void btnAmentLCReport_VAT_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/AmendLC_VAT.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = bd.SQLData.B_BIMPORT_NORMAILLC_AMEND_VAT_REPORT(txtCode.Text, UserInfo.Username, TabId);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("IncomingCollectionAmendmentsVAT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }

        protected void btnCancelLC_XUATNGOAIBANG_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/CancelLC_PHIEUXUATNGOAIBANG.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = bd.SQLData.B_BIMPORT_NORMAILLC_CANCEL_PHIEUXUATNGOAIBANG_REPORT(txtCode.Text, UserInfo.Username);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("CancelLC_PHIEUXUATNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
        protected void btnCancelLC_VAT_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/CancelLC_VAT.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = bd.SQLData.B_BIMPORT_NORMAILLC_CANCEL_VAT_REPORT(txtCode.Text, UserInfo.Username, TabId);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("CancelLC_VAT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
        #endregion

        protected void btSearch_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            DataRow dataRow = null;
            Session["DataKey"] = txtCode.Text;

            LoadData(ref dataRow);
        }

        protected void GenerateMT740()
        {
            if (comGenerate.SelectedValue.ToLower() == "yes")
            {
                txtRemittingBankNo.Enabled = true;
                tb31DPlaceOfExpiry.Enabled = true;

                rcbBeneficiaryType740.Enabled = true;
                tbBeneficiaryNo740.Enabled = true;
                tbBeneficiaryName740.Enabled = true;
                tbBeneficiaryAddr740_1.Enabled = true;
                tbBeneficiaryAddr740_2.Enabled = true;
                tbBeneficiaryAddr740_3.Enabled = true;

                numCreditAmount.Enabled = true;
                comboCreditCurrency.Enabled = true;

                rcbAvailableWithType740.Enabled = true;
                tbAvailableWithNo740.Enabled = true;

               
                comboReimbursingBankChange.Enabled = true;

                tbExpiryDate740.Enabled = true;

                numPercentageCreditAmountTolerance740_1.Enabled = true;
                numPercentageCreditAmountTolerance740_2.Enabled = true;

                txtSenderToReceiverInformation740_1.Enabled = true;
                txtSenderToReceiverInformation740_2.Enabled = true;
                txtSenderToReceiverInformation740_3.Enabled = true;
                txtSenderToReceiverInformation740_4.Enabled = true;

                txtDraftsAt740_1.Enabled = true;
                txtDraftsAt740_2.Enabled = true;
            }
            else
            {
                txtRemittingBankNo.Enabled = false;
                tb31DPlaceOfExpiry.Enabled = false;

                rcbBeneficiaryType740.Enabled = false;
                tbBeneficiaryNo740.Enabled = false;
                tbBeneficiaryName740.Enabled = false;
                tbBeneficiaryAddr740_1.Enabled = false;
                tbBeneficiaryAddr740_2.Enabled = false;
                tbBeneficiaryAddr740_3.Enabled = false;

                numCreditAmount.Enabled = false;
                comboCreditCurrency.Enabled = false;

                rcbAvailableWithType740.Enabled = false;
                tbAvailableWithNo740.Enabled = false;
  
                comboReimbursingBankChange.Enabled = false;

                tbExpiryDate740.Enabled = false;

                numPercentageCreditAmountTolerance740_1.Enabled = false;
                numPercentageCreditAmountTolerance740_2.Enabled = false;

                txtSenderToReceiverInformation740_1.Enabled = false;
                txtSenderToReceiverInformation740_2.Enabled = false;
                txtSenderToReceiverInformation740_3.Enabled = false;
                txtSenderToReceiverInformation740_4.Enabled = false;

                txtDraftsAt740_1.Enabled = false;
                txtDraftsAt740_2.Enabled = false;
            }
        }

        protected void LoadChargeAcct(ref RadComboBox cboChargeAcct)
        {
            bc.Commont.initRadComboBox(ref cboChargeAcct, "Id", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(rcbApplicantID.SelectedItem != null ? rcbApplicantID.SelectedItem.Attributes["CustomerName"] : "XXXXX", rcbChargeCcy.SelectedValue));
        }

        /*protected void LoadChargeAcct2()
        {
            rcbChargeAcct2.Items.Clear();
            rcbChargeAcct2.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct2.DataValueField = "Id";
            rcbChargeAcct2.DataTextField = "Id";
            rcbChargeAcct2.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(rcbApplicantID.SelectedItem != null ? rcbApplicantID.SelectedItem.Attributes["CustomerName"] : "XXXXX", rcbChargeCcy2.SelectedValue);
            rcbChargeAcct2.DataBind();
        }

        protected void LoadChargeAcct3()
        {
            rcbChargeAcct3.Items.Clear();
            rcbChargeAcct3.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct3.DataValueField = "Id";
            rcbChargeAcct3.DataTextField = "Id";
            rcbChargeAcct3.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(rcbApplicantID.SelectedItem != null ? rcbApplicantID.SelectedItem.Attributes["CustomerName"] : "XXXXX", rcbChargeCcy3.SelectedValue);
            rcbChargeAcct3.DataBind();
        }*/
        
        protected void SetRelation_ReimbBankType()
        {
            switch (comboReimbBankType.SelectedValue)
            {
                case "A":
                    rcbReimbBankNo.Enabled = true;
                    tbReimbBankName.Enabled = false;
                    tbReimbBankAddr1.Enabled = false;
                    tbReimbBankAddr2.Enabled = false;
                    tbReimbBankAddr3.Enabled = false;

                    tbReimbBankAddr1.Text = string.Empty;
                    tbReimbBankAddr2.Text = string.Empty;
                    tbReimbBankAddr3.Text = string.Empty;
                    break;
                case "B":
                case "D":
                    rcbReimbBankNo.Enabled = false;
                    tbReimbBankName.Enabled = true;
                    tbReimbBankAddr1.Enabled = true;
                    tbReimbBankAddr2.Enabled = true;
                    tbReimbBankAddr3.Enabled = true;
                    break;
            }
        }

        protected void SetRelation_AdviseThruType()
        {
            switch (rcbAdviseThruType.SelectedValue)
            {
                case "A":
                    rcbAdviseThruNo.Enabled = true;
                    tbAdviseThruName.Enabled = false;
                    tbAdviseThruAddr1.Enabled = false;
                    tbAdviseThruAddr2.Enabled = false;
                    tbAdviseThruAddr3.Enabled = false;
                    break;
                case "B":
                case "D":
                    rcbAdviseThruNo.Enabled = false;
                    tbAdviseThruName.Enabled = true;
                    tbAdviseThruAddr1.Enabled = true;
                    tbAdviseThruAddr2.Enabled = true;
                    tbAdviseThruAddr3.Enabled = true;
                    break;
            }
        }

        protected void SetRelation_DraweeCusType700()
        {
            switch (comboDraweeCusType.SelectedValue)
            {
                case "A":
                    comboDraweeCusNo700.Enabled = true;
                    txtDraweeCusName.Enabled = false;
                    txtDraweeAddr1.Enabled = false;
                    txtDraweeAddr2.Enabled = false;
                    txtDraweeAddr3.Enabled = false;
                    break;
                case "B":
                case "D":
                    comboDraweeCusNo700.Enabled = false;
                    txtDraweeCusName.Enabled = true;
                    txtDraweeAddr1.Enabled = true;
                    txtDraweeAddr2.Enabled = true;
                    txtDraweeAddr3.Enabled = true;
                    break;
            }
        }
        
        protected void GenerateMT747()
        {
            if (comboGenerateMT747.SelectedValue.ToLower() == "yes")
            {
                dteNewDateOfExpiry_747.Enabled = true;
                numAmount_747.Enabled = true;
                numPercentageCreditTolerance_747_1.Enabled = true;
                numPercentageCreditTolerance_747_2.Enabled = true;
                comboMaximumCreditAmount_747.Enabled = true;
                txtAdditionalCovered_747_1.Enabled = true;
                txtAdditionalCovered_747_2.Enabled = true;
                txtAdditionalCovered_747_3.Enabled = true;
                txtAdditionalCovered_747_4.Enabled = true;
                txtSenderToReceiverInfomation_747_1.Enabled = true;
                txtSenderToReceiverInfomation_747_2.Enabled = true;
                txtSenderToReceiverInfomation_747_3.Enabled = true;
                txtSenderToReceiverInfomation_747_4.Enabled = true;
                txtNarrative_747.Enabled = true;
            }
            else
            {
                dteNewDateOfExpiry_747.Enabled = false;
                numAmount_747.Enabled = false;
                numPercentageCreditTolerance_747_1.Enabled = false;
                numPercentageCreditTolerance_747_2.Enabled = false;
                comboMaximumCreditAmount_747.Enabled = false;
                txtAdditionalCovered_747_1.Enabled = false;
                txtAdditionalCovered_747_2.Enabled = false;
                txtAdditionalCovered_747_3.Enabled = false;
                txtAdditionalCovered_747_4.Enabled = false;
                txtSenderToReceiverInfomation_747_1.Enabled = false;
                txtSenderToReceiverInfomation_747_2.Enabled = false;
                txtSenderToReceiverInfomation_747_3.Enabled = false;
                txtSenderToReceiverInfomation_747_4.Enabled = false;
                txtNarrative_747.Enabled = false;
            } 
        }        
    }
}