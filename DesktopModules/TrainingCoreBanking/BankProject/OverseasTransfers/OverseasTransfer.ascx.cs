using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.OverseasFundsTransfer
{
    public partial class OverseasTransfer : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private string Refix_BMACODE()
        {
            return "FT";
        }

        public string WaiveCharges = string.Empty;

        protected double PercentOverseasTransfer()
        {
            return 0.15;
        }
        public string CheckAccount()
        {
            return "Hello" + hf_AccountId.Value;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            lblIntermediaryInstitutionNoError.Text = "";
            lblAccountWithInstitutionNoError.Text = "";
            txtVATNo.Enabled = false;

            numDebitAmount.Value = 0;
            numCreditAmount.Value = 0;
            numCommissionAmount.Value = 0;
            numChargeAmount.Value = 0;

            GeneralVATNo();
            SetChargeVisibilityByCommissionCode();

            // default no use
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            // default no use

            txtCommissionCurrency.Text = string.Empty;
            txtChargeCurrency.Text = string.Empty;
            txtDeitCurrency.Text = string.Empty;

            //VVTextBox1.SetTextDefault("");

            comboDetailOfCharges.SelectedValue = "SHA";
            
            InitData();
            InitToolBar(false);

            if (!string.IsNullOrEmpty(Request.QueryString["CodeID"]))
            {
                txtCode.Text = Request.QueryString["CodeID"];
                LoadData();
                RadToolBar1.FindItemByValue("btprint").Enabled = true;
            }
            else
            {
                SetDisableByReview(true);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["disable"]))
            {
                InitToolBar(true);
                SetDisableByReview(false);
                RadToolBar1.FindItemByValue("btSave").Enabled = false;
                RadToolBar1.FindItemByValue("btReview").Enabled = false;
            }
            Session["DataKey"] = txtCode.Text;
            dteValueDate.Enabled = false;
            dteProcessingDate.Enabled = false;
            SetChargeAcct();
            LoadCreditAccountByDebitCurrency();

            comboIntermediaryType_OnSelectedIndexChanged(null, null);
            comboAccountType_OnSelectedIndexChanged(null, null);
        }
        
        protected void comboTPKT_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblTPKTName.Text = comboTPKT.SelectedValue;
        }

        protected void comboCreditAccount_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblSenderCorrespondent.Text = string.Empty;
            lblCreditAccount.Text = string.Empty;
            txtCreditCurrency.Text = string.Empty;
            comboCurrency.SelectedValue = string.Empty;
            //
            var item = comboCreditAccount.SelectedItem;
            if (item != null && !string.IsNullOrEmpty(item.Text))
            {
                lblSenderCorrespondent.Text = item.Attributes["Code"].ToString();
                lblCreditAccount.Text = item.Attributes["Description"].ToString();
                txtCreditCurrency.Text = item.Attributes["Currency"].ToString();
                comboCurrency.SelectedValue = txtCreditCurrency.Text;
            }
        }

        protected void comboCountryCode_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblCountryCodeName.Text = comboCountryCode.SelectedValue;
        }

        protected void comboTransactionType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lbTransactionTypeName.Text = comboTransactionType.SelectedValue;
            LoadCommoditySerByTransactionType();

            var transactionType = comboTransactionType.SelectedItem != null ? comboTransactionType.SelectedItem.Text : "";
            switch (transactionType)
            {
                case "OTC1":
                case "OTS1":
                    comboCommissionType.SelectedValue = "TTCHRGADV";
                    break;
                case "OTC2":
                case "OTS2":
                    comboCommissionType.SelectedValue = "TTCHRGREC";
                    break;
                case "OTP1":
                    comboCommissionType.SelectedValue = "TTCHRGIND";
                    break;
                default:
                    comboCommissionType.SelectedValue = string.Empty;
                    break;
            }
        }

        protected void LoadCommoditySerByTransactionType()
        {
            var TransactionType = "";
            if (comboTransactionType.SelectedItem != null && !string.IsNullOrEmpty(comboTransactionType.SelectedItem.Text)) TransactionType = comboTransactionType.SelectedItem.Text.Substring(0, 3);
            bc.Commont.initRadComboBox(ref comboCommoditySer, "Name", "Id", bd.SQLData.B_BCOMMODITY_GetByTransactionType(TransactionType));
        }
        private string GetCommissionTypeByTransactionType(string p)
        {
            switch (p.ToUpper())
            {
                case "OTC1":
                case "OTS1":
                    return "OTCHRGADV";
                case "OTC2":
                case "OTS2":
                    return "OTCHRGREC";
                case "OTP1":
                    return "OTCHRGIND";
                default:
                    return "";
            }
        }
        
        protected void comboAccountWithInstitution_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
           // lblAccountWithInstitutionName.Text = comboAccountWithInstitution.SelectedItem.Attributes["Description"];
        }

        protected void comboChargeAcct_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtChargeAcctName.Text = comboChargeAcct.SelectedItem.Attributes["Name"];
            comboCommissionCurrency.SelectedValue = comboChargeAcct.SelectedItem.Attributes["Currency"]; 
            comboChargeCurrency.SelectedValue = comboChargeAcct.SelectedItem.Attributes["Currency"];
            txtChargeCurrency.Text = comboChargeAcct.SelectedItem.Attributes["Currency"];
            txtCommissionCurrency.Text = comboChargeAcct.SelectedItem.Attributes["Currency"];
        }
        
        protected void comboOtherBy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetOtherByName();
            LoadChargeAcc();

            var customerName = comboOtherBy.SelectedItem != null
                                   ? comboOtherBy.SelectedItem.Attributes["CustomerName2"]
                                   : "";
            LoadDebitAcctNo(customerName);
        }
        
        protected void LoadCreditAccountByDebitCurrency()
        {
            bc.Commont.initRadComboBox(ref comboCreditAccount, "AccountNo", "AccountNo", bd.SQLData.B_BSWIFTCODE_GetByCurrency(txtDeitCurrency.Text));
        }
        
        protected void SetDebitCurrencyName()
        {
            //if (comboDebitAcctNo.SelectedItem != null && !string.IsNullOrEmpty(comboDebitAcctNo.SelectedItem.Text))
            //{
            //    //lblDebitCurrency.Text = comboDebitAcctNo.SelectedItem.Text.Substring(0, 3);
            //}
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            LoadData();
            comboIntermediaryType_OnSelectedIndexChanged(null, null);
            comboAccountType_OnSelectedIndexChanged(null, null);
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case bc.Commands.Commit:
                    SaveData();
                    Response.Redirect("Default.aspx?tabid=" + TabId.ToString());
                    break;

                case bc.Commands.Preview:
                    Response.Redirect(EditUrl("otreview"));
                    break;

                case bc.Commands.Authorize:
                    Authorize();
                    Response.Redirect("Default.aspx?tabid=" + TabId.ToString());
                    break;

                case bc.Commands.Reverse:
                    Revert();
                    break;
            }
        }

        protected void InitData()
        {
            txtCode.Text = bd.SQLData.B_BMACODE_GetNewID("OVERSEASTRANSFER", Refix_BMACODE());

            dteCreditDate.SelectedDate = DateTime.Now;
            dteDebitDate.SelectedDate = DateTime.Now;
            dteProcessingDate.SelectedDate = DateTime.Now;
            dteValueDate.SelectedDate = DateTime.Now;

            bc.Commont.initRadComboBox(ref comboOtherBy, "CustomerID", "CustomerID", bd.DataTam.B_BCUSTOMERS_GetAll());
            
            bc.Commont.initRadComboBox(ref comboTransactionType, "Id", "Description", bd.SQLData.CreateGenerateDatas("TabAccountTransfer_TransactionType"));

            bc.Commont.initRadComboBox(ref comboCountryCode, "TenTA", "TenTA", bd.SQLData.B_BCOUNTRY_GetAll());

            bc.Commont.initRadComboBox(ref comboCurrency, "Code", "Code", bd.SQLData.B_BCURRENCY_GetAll());

            bc.Commont.initRadComboBox(ref comboAccountOfficer, "Description", "Code", bd.SQLData.B_BACCOUNTOFFICER_GetAll());

            lblCommoditySerName.Text = comboCommoditySer.SelectedValue;

            SetDebitCurrencyName();
            lbTransactionTypeName.Text = comboTransactionType.SelectedValue;

            GeneralVATNo();

            LoadChargeAcc();
        }
        protected void InitToolBar(bool flag)
        {
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar1.FindItemByValue("btRevert").Enabled = flag;
            if (Request.QueryString["disable"] != null)
                RadToolBar1.FindItemByValue("btprint").Enabled = true;
            else
                RadToolBar1.FindItemByValue("btprint").Enabled = false;
        }
        protected void SaveData()
        {
            double AmtCredited = 0, DebitAmount = 0, AmountDebited = 0, TreasuryRate = 0, CreditAmount = 0;
            if (!string.IsNullOrEmpty(lblAmtCredited.Text))
            {
                AmtCredited = double.Parse(lblAmtCredited.Text);
            }
            
            if (numDebitAmount.Value > 0)
            {
                DebitAmount = double.Parse(numDebitAmount.Value.ToString());
            }

            if (!string.IsNullOrEmpty(lblAmountDebited.Text))
            {
                AmountDebited = double.Parse(lblAmountDebited.Text);
            }

            if (numTreasuryRate.Value > 0)
            {
                TreasuryRate = double.Parse(numTreasuryRate.Value.ToString());
            }

            if (numCreditAmount.Value > 0)
            {
                CreditAmount = double.Parse(numCreditAmount.Value.ToString());
            }

            bd.SQLData.B_BOVERSEASTRANSFER_Insert(txtCode.Text.Trim()
                    , comboTransactionType.SelectedValue
                    , comboProductLine.SelectedValue
                    , comboCountryCode.SelectedValue
                    , comboCommoditySer.SelectedValue
                    , txtOtherInfo.Text.Trim()
                    , comboOtherBy.SelectedValue
                    , txtDebitRef.Text.Trim()
                    , comboDebitAcctNo.SelectedValue
                    , txtDeitCurrency.Text
                    , DebitAmount
                    , dteDebitDate.SelectedDate.ToString()
                    , AmountDebited
                    , comboTPKT.SelectedValue
                    , comboCreditAccount.SelectedValue
                    , txtCreditCurrency.Text
                    , TreasuryRate
                    , CreditAmount
                    , dteCreditDate.SelectedDate.ToString()
                    , dteProcessingDate.SelectedDate.ToString()
                    , AmtCredited
                    , txtVATSend.Text.Trim()
                    , txtAddRemarks.Text.Trim()
                    , UserId.ToString()
                    , txtOtherBy2.Text.Trim()
                    , txtOtherBy3.Text.Trim()
                    , txtOtherBy4.Text.Trim()
                    , txtOtherBy5.Text.Trim());

            double InterBankSettleAmount = 0, InstancedAmount = 0;
            double? SenderCharges = null;
            double? ReceiverCharges = null;

            if (!string.IsNullOrEmpty(lblInterBankSettleAmount.Text))
            {
                InterBankSettleAmount = double.Parse(lblInterBankSettleAmount.Text);
            }
            if (!string.IsNullOrEmpty(lblInstancedAmount.Text))
            {
                InstancedAmount = double.Parse(lblInstancedAmount.Text);
            }

            if (!string.IsNullOrEmpty(lblSenderCharges.Text) && lblSenderCharges.Text != "0")
            {
                SenderCharges = double.Parse(lblSenderCharges.Text);
            }

            if (!string.IsNullOrEmpty(lblReceiverCharges.Text) && lblReceiverCharges.Text !=  "0")
            {
                ReceiverCharges = double.Parse(lblReceiverCharges.Text);
            }
            bd.SQLData.B_BOVERSEASTRANSFERMT103_Insert(txtCode.Text.Trim()
                , ""
                , lblSenderReference.Text
                , ""
                , lblBankOperationCode.Text.Trim()
                , ""
                , dteValueDate.SelectedDate.ToString()
                , comboCurrency.SelectedValue
                , InterBankSettleAmount
                , InstancedAmount
                , comboOrderingCustAcc.SelectedValue
                , txtOrderingInstitution.Text.Trim()
                , lblSenderCorrespondent.Text
                , txtReceiverCorrespondent.Text
                , txtReceiverCorrBankAct.Text.Trim()
                , txtIntermediaryInstitutionNo.Text.Trim()
                , txtIntermediaryBankAcct.Text.Trim()
                , txtAccountWithInstitutionNo.Text.Trim()
                , txtAccountWithBankAcct.Text.Trim()
                , txtRemittanceInformation.Text.Trim()
                , comboDetailOfCharges.SelectedValue
                , SenderCharges
                , ReceiverCharges
                , txtSenderToReceiverInfo.Text.Trim()
                , UserId.ToString()
                , txtBeneficiaryCustomer1.Text.Trim()
                , txtBeneficiaryCustomer2.Text.Trim()
                , txtBeneficiaryCustomer3.Text.Trim()
                , comboAccountType.SelectedValue
                , txtAccountWithBankAcct2.Text.Trim()
                , txtBeneficiaryCustomer4.Text.Trim()
                , txtBeneficiaryCustomer5.Text.Trim()
                , comboIntermediaryType.SelectedValue
                , txtIntermediaryInstruction1.Text.Trim()
                , txtIntermediaryInstruction2.Text.Trim()
                , txtOrderingCustomer1.Text
                , txtOrderingCustomer2.Text
                , txtOrderingCustomer3.Text
                , txtOrderingCustomer4.Text
                ,txtPartyIdentifyForInter.Text.Trim()
                ,txtPartyIdentifyForInsti.Text.Trim()
                );

            double CommissionAmount = 0, ChargeAmount = 0, TotalChargeAmount = 0, TotalTaxAmount = 0;
            if (numCommissionAmount.Value > 0)
            {
                CommissionAmount = double.Parse(numCommissionAmount.Value.ToString());
            }

            if (numChargeAmount.Value > 0)
            {
                ChargeAmount = double.Parse(numChargeAmount.Value.ToString());
            }

            if (!string.IsNullOrEmpty(lblTotalChargeAmount.Text))
            {
                TotalChargeAmount = double.Parse(lblTotalChargeAmount.Text);
            }

            if (!string.IsNullOrEmpty(lblTotalTaxAmount.Text))
            {
                TotalTaxAmount = double.Parse(lblTotalTaxAmount.Text);
            }
            bd.SQLData.B_BOVERSEASTRANSFERCHARGECOMMISSION_Insert(
                txtCode.Text.Trim()
                , comboChargeAcct.SelectedValue
                , comboDisplayChargesCom.SelectedValue
                , comboCommissionCode.SelectedValue
                , comboCommissionType.SelectedValue
                , CommissionAmount
                , comboCommissionFor.SelectedValue
                , comboChargeCode.SelectedValue
                , comboChargeType.SelectedValue
                , ChargeAmount
                , comboChargeFor.SelectedValue
                , comboDetailOfCharges_TabChargeInfo.SelectedValue
                , txtVATNo.Text.Trim()
                , txtAddRemarks1.Text.Trim()
                , txtAddRemarks2.Text.Trim()
                , comboAccountOfficer.SelectedValue
                , TotalChargeAmount
                , TotalTaxAmount,
                UserId.ToString(),
                txtCommissionCurrency.Text,
                txtChargeCurrency.Text);
        }
        protected void LoadData()
        {
            var dsOT = bd.SQLData.B_BOVERSEASTRANSFER_GetByOverseasTransferCode(txtCode.Text.Trim());

            // truong hop Edit, thi` ko cho click Preview
            RadToolBar1.FindItemByValue("btReview").Enabled = true;

            if (dsOT != null && dsOT.Tables.Count > 0)
            {
                #region tab Main
                if (dsOT.Tables[0].Rows.Count > 0)
                {
                    RadToolBar1.FindItemByValue("btReview").Enabled = false;

                    var drow = dsOT.Tables[0].Rows[0];
                    //Cho phep Edit sau khi commit giao dich nhung chua chuyen sang cap kiem soat
                    //Neu status UNA/REV thi dc edit, nguoc lai AUT = view only
                    switch (drow["Status"].ToString())
                    {
                        case "UNA":
                        case "REV":
                            if (string.IsNullOrEmpty(Request.QueryString["CodeID"]))
                            {
                                SetDisableByReview(true);
                                RadToolBar1.FindItemByValue("btSave").Enabled = true;
                            }
                            break;
                        case "AUT":
                            SetDisableByReview(false);
                            txtCode.Enabled = true;
                            RadToolBar1.FindItemByValue("btprint").Enabled = true;
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            break;
                    }


                    comboTransactionType.SelectedValue = drow["TransactionType"].ToString();
                    lbTransactionTypeName.Text = comboTransactionType.SelectedValue;

                    comboProductLine.SelectedValue = drow["ProductLine"].ToString();

                    comboCountryCode.SelectedValue = drow["CountryCode"].ToString();

                    LoadCommoditySerByTransactionType();
                    comboCommoditySer.SelectedValue = drow["CommoditySer"].ToString();
                    lblCommoditySerName.Text = comboCommoditySer.SelectedValue;

                    txtOtherInfo.Text = drow["OtherInfo"].ToString();

                    comboOtherBy.SelectedValue = drow["OtherBy"].ToString();
                    txtOtherBy2.Text = drow["OtherBy2"].ToString();
                    txtOtherBy3.Text = drow["OtherBy3"].ToString();
                    txtOtherBy4.Text = drow["OtherBy4"].ToString();
                    txtOtherBy5.Text = drow["OtherBy5"].ToString();

                    SetOtherByName();
                    var cusName = comboOtherBy.SelectedItem != null
                                      ? comboOtherBy.SelectedItem.Attributes["CustomerName2"]
                                      : "";
                    LoadDebitAcctNo(cusName);


                    txtDebitRef.Text = drow["DebitRef"].ToString();

                    comboDebitAcctNo.SelectedValue = drow["DebitAcctNo"].ToString();
                    txtDeitCurrency.Text = drow["DebitCurrency"].ToString();
                    SetDebitCurrencyName();

                    comboDebitCurrency.SelectedValue = drow["DebitCurrency"].ToString();
                    numDebitAmount.Text = drow["DebitAmount"].ToString();

                    if (drow["DebitDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteDebitDate.SelectedDate = DateTime.Parse(drow["DebitDate"].ToString());
                    }
                    lblAmountDebited.Text = drow["AmountDebited"].ToString();

                    comboTPKT.SelectedValue = drow["TPKT"].ToString();
                    lblTPKTName.Text = comboTPKT.SelectedValue;

                    txtCreditCurrency.Text = drow["CreditCurrency"].ToString();
                    numTreasuryRate.Text = drow["TreasuryRate"].ToString();

                    numCreditAmount.Text = drow["CreditAmount"].ToString();
                    if (drow["ProcessingDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteProcessingDate.SelectedDate = DateTime.Parse(drow["ProcessingDate"].ToString());
                    }
                    if (drow["CreditDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteCreditDate.SelectedDate = DateTime.Parse(drow["CreditDate"].ToString());
                    }
                    lblAmtCredited.Text = drow["AmountCredited"].ToString();
                    txtVATSend.Text = drow["VATSend"].ToString();
                    txtAddRemarks.Text = drow["AddRemarks"].ToString();

                    LoadCreditAccountByDebitCurrency();
                    comboCreditAccount.SelectedValue = drow["CreditAccount"].ToString();
                    lblCreditAccount.Text = comboCreditAccount.SelectedItem != null ? comboCreditAccount.SelectedItem.Attributes["Description"] : "";
                    
                }
                else
                {
                    comboTransactionType.SelectedValue = string.Empty;
                    lbTransactionTypeName.Text = comboTransactionType.SelectedValue;

                    comboProductLine.SelectedValue = string.Empty;

                    comboCountryCode.SelectedValue = string.Empty;

                    comboCommoditySer.SelectedValue = string.Empty;
                    lblCommoditySerName.Text = comboCommoditySer.SelectedValue;

                    txtOtherInfo.Text = string.Empty;

                    comboOtherBy.SelectedValue = string.Empty;
                    lblOtherByName.Text = string.Empty;

                    txtDebitRef.Text = string.Empty;

                    comboDebitAcctNo.SelectedValue = string.Empty;
                    SetDebitCurrencyName();

                    comboDebitCurrency.SelectedValue = string.Empty;
                    numDebitAmount.Value = 0;

                    dteDebitDate.SelectedDate = null;
                    lblAmountDebited.Text = string.Empty;

                    comboTPKT.SelectedValue = string.Empty;
                    lblTPKTName.Text = comboTPKT.SelectedValue;

                    comboCreditAccount.SelectedValue = string.Empty;
                    lblCreditAccount.Text = comboCreditAccount.SelectedItem != null ? comboCreditAccount.SelectedItem.Attributes["Description"] : "";

                    txtCreditCurrency.Text = string.Empty;

                    numTreasuryRate.Value = 0;

                    numCreditAmount.Value = 0;
                    dteCreditDate.SelectedDate = null;
                    dteProcessingDate.SelectedDate = null;
                    lblAmtCredited.Text = string.Empty;
                    txtVATSend.Text = string.Empty;
                    txtAddRemarks.Text = string.Empty;

                    txtOtherBy2.Text = string.Empty;
                    txtOtherBy3.Text = string.Empty;
                    txtOtherBy4.Text = string.Empty;
                    txtOtherBy5.Text = string.Empty;

                    //SetDisableByReview(true);
                }
                #endregion

                #region MT  103
                if (dsOT.Tables[1].Rows.Count > 0)
                {
                    var drow103 = dsOT.Tables[1].Rows[0];

                    lblSenderReference.Text = drow103["SenderReference"].ToString();
     
                    lblBankOperationCode.Text = drow103["BankOperationCode"].ToString();
                    if (drow103["ValueDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteValueDate.SelectedDate = DateTime.Parse(drow103["ValueDate"].ToString());
                    }

                    comboCurrency.SelectedValue = drow103["Currency"].ToString();

                    lblInterBankSettleAmount.Text = String.Format("{0:C}", drow103["InterBankSettleAmount"]).Replace("$", "");
                    lblInstancedAmount.Text = String.Format("{0:C}", drow103["InstancedAmount"]).Replace("$", "");

                    comboOrderingCustAcc.SelectedValue = drow103["OrderingCustAcc"].ToString();
                    txtOrderingInstitution.Text = drow103["OrderingInstitution"].ToString();
                    lblSenderCorrespondent.Text = drow103["SenderCorrespondent"].ToString();
                    
                    txtReceiverCorrBankAct.Text = drow103["ReceiverCorrBankAct"].ToString();

                    txtIntermediaryInstitutionNo.Text = drow103["IntermediaryInstruction"].ToString();
                    lblIntermediaryInstitutionName.Text = drow103["IntermediaryInstructionName"].ToString();

                    txtIntermediaryBankAcct.Text = drow103["IntermediaryBankAcct"].ToString();
                    
                    txtAccountWithBankAcct.Text = drow103["AccountWithBankAcct"].ToString();
                    txtRemittanceInformation.Text = drow103["RemittanceInformation"].ToString();
                    comboDetailOfCharges.SelectedValue = drow103["DetailOfCharges"].ToString();

                    if (!string.IsNullOrEmpty(drow103["SenderCharges"].ToString()) && drow103["SenderCharges"].ToString() != "0")
                    {
                        lblSenderCharges.Text = String.Format("{0:C}", drow103["SenderCharges"]).Replace("$", "");
                    }

                    if (!string.IsNullOrEmpty(drow103["ReceiverCharges"].ToString()) && drow103["ReceiverCharges"].ToString() != "0")
                    {
                        lblReceiverCharges.Text = String.Format("{0:C}", drow103["ReceiverCharges"]).Replace("$", "");
                    }

                    txtSenderToReceiverInfo.Text = drow103["SenderToReceiveInfo"].ToString();

                    txtAccountWithInstitutionNo.Text = drow103["AccountWithInstitution"].ToString();
                    lblAccountWithInstitutionName.Text = drow103["AccountWithInstitutionName"].ToString();

                    txtReceiverCorrespondent.Text = drow103["ReceiverCorrespondent"].ToString();
                    //lblReceiverCorrespondentName.Text = comboReceiverCorrespondent.SelectedItem.Attributes["Description"];

                    comboAccountType.SelectedValue = drow103["AccountType"].ToString();
                    txtAccountWithBankAcct2.Text = drow103["AccountWithBankAcct2"].ToString();

                    txtBeneficiaryCustomer1.Text = drow103["BeneficiaryCustomer1"].ToString();
                    txtBeneficiaryCustomer2.Text = drow103["BeneficiaryCustomer2"].ToString();
                    txtBeneficiaryCustomer3.Text = drow103["BeneficiaryCustomer3"].ToString();

                    txtBeneficiaryCustomer4.Text = drow103["BeneficiaryCustomer4"].ToString();
                    txtBeneficiaryCustomer5.Text = drow103["BeneficiaryCustomer5"].ToString();
                    comboIntermediaryType.SelectedValue = drow103["IntermediaryType"].ToString();
                    txtIntermediaryInstruction1.Text = drow103["IntermediaryInstruction1"].ToString();
                    txtIntermediaryInstruction2.Text = drow103["IntermediaryInstruction2"].ToString();

                    txtPartyIdentifyForInter.Text = drow103["PartyIdentifyForInter"].ToString();
                    txtPartyIdentifyForInsti.Text = drow103["PartyIdentifyForInsti"].ToString();

                    SetRelation_AccountWithInstitution();
                    SetRelation_IntermediaryInstruction();

                    if (!string.IsNullOrEmpty(Request.QueryString["CodeID"]))
                    {
                        txtIntermediaryInstitutionNo.Enabled = false;
                        txtAccountWithInstitutionNo.Enabled = false;
                    }

                    txtOrderingCustomer1.Text = drow103["OrderingCustAccName"].ToString();
                    txtOrderingCustomer2.Text = drow103["OrderingCustAccAddr1"].ToString();
                    txtOrderingCustomer3.Text = drow103["OrderingCustAccAddr2"].ToString();
                    txtOrderingCustomer4.Text = drow103["OrderingCustAccAddr3"].ToString();
                }
                else
                {
                    lblSenderReference.Text = string.Empty;
                    lblBankOperationCode.Text = string.Empty;
                    dteValueDate.SelectedDate = DateTime.Now;
                    comboCurrency.SelectedValue = string.Empty;
                    lblInterBankSettleAmount.Text = "0";
                    lblInstancedAmount.Text = "0";
                    comboOrderingCustAcc.SelectedValue = string.Empty;
                    txtOrderingInstitution.Text = string.Empty;
                    lblSenderCorrespondent.Text = string.Empty;

                    txtReceiverCorrespondent.Text = string.Empty;
                    lblReceiverCorrespondentName.Text = string.Empty;

                    txtReceiverCorrBankAct.Text = string.Empty;
                    txtIntermediaryInstitutionNo.Text = string.Empty;
                    lblIntermediaryInstitutionName.Text = string.Empty;

                    txtAccountWithInstitutionNo.Text = string.Empty;
                    lblAccountWithInstitutionName.Text = string.Empty;
                    txtIntermediaryBankAcct.Text = string.Empty;

                    txtAccountWithBankAcct.Text = string.Empty;
                    txtRemittanceInformation.Text = string.Empty;
                    comboDetailOfCharges.SelectedValue = string.Empty;
                    lblSenderCharges.Text = "";
                    lblReceiverCharges.Text = "";
                    txtSenderToReceiverInfo.Text = string.Empty;

                    txtOrderingCustomer1.Text = string.Empty;
                    txtOrderingCustomer2.Text = string.Empty;
                    txtOrderingCustomer3.Text = string.Empty;
                    txtOrderingCustomer4.Text = string.Empty;

                    comboAccountType.SelectedValue = string.Empty;
                    txtAccountWithBankAcct2.Text = string.Empty;

                    txtBeneficiaryCustomer1.Text = string.Empty;
                    txtBeneficiaryCustomer2.Text = string.Empty;
                    txtBeneficiaryCustomer3.Text = string.Empty;

                    txtBeneficiaryCustomer4.Text = string.Empty;
                    txtBeneficiaryCustomer5.Text = string.Empty;
                    comboIntermediaryType.SelectedValue = string.Empty;
                    txtIntermediaryInstruction1.Text = string.Empty;
                    txtIntermediaryInstruction2.Text = string.Empty;
                    txtPartyIdentifyForInter.Text = string.Empty;
                    txtPartyIdentifyForInsti.Text = string.Empty;
                }
                #endregion

                #region Charge Commission
                if (dsOT.Tables[2].Rows.Count > 0)
                {
                    var drowCharge = dsOT.Tables[2].Rows[0];

                    WaiveCharges = drowCharge["CommissionCode"].ToString();

                    LoadChargeAcc();
                    comboChargeAcct.SelectedValue = drowCharge["ChargeAcct"].ToString();
                    if (comboChargeAcct.SelectedItem != null)
                    {
                        txtChargeAcctName.Text = comboChargeAcct.SelectedItem.Attributes["Name"];
                        txtCommissionCurrency.Text = comboChargeAcct.SelectedItem.Attributes["Currency"];
                        txtChargeCurrency.Text = comboChargeAcct.SelectedItem.Attributes["Currency"];
                    }

                    comboDisplayChargesCom.SelectedValue = drowCharge["DisplayChargesCom"].ToString();
                    comboCommissionCode.SelectedValue = drowCharge["CommissionCode"].ToString();

                    comboCommissionType.SelectedValue = drowCharge["CommissionType"].ToString();

                    numCommissionAmount.Text = drowCharge["CommissionAmount"].ToString();
                    comboCommissionCurrency.SelectedValue = drowCharge["CommissionCurrency"].ToString();

                    comboCommissionFor.SelectedValue = drowCharge["CommissionFor"].ToString();
                    comboChargeCode.SelectedValue = drowCharge["ChargeCode"].ToString();
                    comboChargeType.SelectedValue = drowCharge["ChargeType"].ToString();

                    numChargeAmount.Text = drowCharge["ChargeAmount"].ToString();
                    comboChargeCurrency.SelectedValue = drowCharge["ChargeCurrency"].ToString();

                    comboChargeFor.SelectedValue = drowCharge["ChargeFor"].ToString();
                    comboDetailOfCharges_TabChargeInfo.SelectedValue = drowCharge["DetailOfCharges"].ToString();
                    txtVATNo.Text = drowCharge["VATNo"].ToString();
                    txtAddRemarks1.Text = drowCharge["AddRemarks1"].ToString();
                    txtAddRemarks2.Text = drowCharge["AddRemarks2"].ToString();

                    comboAccountOfficer.SelectedValue = drowCharge["ProfitCenteCust"].ToString();

                    lblTotalChargeAmount.Text = String.Format("{0:C}", drowCharge["TotalChargeAmount"]).Replace("$", "");
                    lblTotalTaxAmount.Text = String.Format("{0:C}", drowCharge["TotalTaxAmount"]).Replace("$", "");
                }
                else
                {
                    comboChargeAcct.SelectedValue = string.Empty;
                    txtChargeAcctName.Text = string.Empty;
                    txtCommissionCurrency.Text = string.Empty;
                    txtChargeCurrency.Text = string.Empty;

                    comboDisplayChargesCom.SelectedValue = string.Empty;
                    comboCommissionCode.SelectedValue = string.Empty;
                    comboCommissionType.SelectedValue = string.Empty;

                    numCommissionAmount.Text = string.Empty;
                    comboCommissionCurrency.SelectedValue = string.Empty;

                    comboCommissionFor.SelectedValue = string.Empty;
                    comboChargeCode.SelectedValue = string.Empty;
                    comboChargeType.SelectedValue = string.Empty;

                    numChargeAmount.Text = string.Empty;
                    comboChargeCurrency.SelectedValue = string.Empty;

                    comboChargeFor.SelectedValue = string.Empty;
                    comboDetailOfCharges_TabChargeInfo.SelectedValue = string.Empty;
                    txtVATNo.Text = string.Empty;
                    txtAddRemarks1.Text = string.Empty;
                    txtAddRemarks2.Text = string.Empty;

                    comboAccountOfficer.SelectedValue = string.Empty;

                    lblTotalChargeAmount.Text = "0";
                    lblTotalTaxAmount.Text = "0";
                }
                #endregion

                SetChargeAcct();
                SetChargeVisibilityByCommissionCode();

                if (!string.IsNullOrEmpty(Request.QueryString["CodeID"]))
                {
                    comboChargeAcct.Enabled = false;
                }
            }
        }
        protected void Authorize()
        {
            // Update status
            bd.SQLData.B_BOVERSEASTRANSFER_UpdateStatus(txtCode.Text.Trim(), "AUT", UserId.ToString());

            // Generate Code
            txtCode.Text = bd.SQLData.B_BMACODE_GetNewID("OVERSEASTRANSFER", Refix_BMACODE());

            // Active control
            SetDisableByReview(true);

            // Reset Data
            LoadData();

            //  InitToolBar
            InitToolBar(false);
            RadToolBar1.FindItemByValue("btSave").Enabled = true;

            dteCreditDate.SelectedDate = DateTime.Now;
            dteDebitDate.SelectedDate = DateTime.Now;
            dteProcessingDate.SelectedDate = DateTime.Now;

            comboDetailOfCharges.SelectedValue = "SHA";
            txtCommissionCurrency.Text = string.Empty;
            txtChargeCurrency.Text = string.Empty;
            txtDeitCurrency.Text = string.Empty;
            //VVTextBox1.SetTextDefault("");

            SetChargeAcct();
            LoadCreditAccountByDebitCurrency();

            lblIntermediaryInstitutionNoError.Text = "";
            lblAccountWithInstitutionNoError.Text = "";

        }
        protected void Revert()
        {
            // Update status REV
            bd.SQLData.B_BOVERSEASTRANSFER_UpdateStatus(txtCode.Text.Trim(), "REV", UserId.ToString());

            // Active control
            SetDisableByReview(true);

            // ko cho Authorize
            InitToolBar(false);
            RadToolBar1.FindItemByValue("btSave").Enabled = true;
        }

        protected void SetOtherByName()
        {
            var dsCus = bd.DataTam.B_BCUSTOMERS_GetbyID(comboOtherBy.SelectedItem.Text);
            if (dsCus != null && dsCus.Tables.Count > 0 && dsCus.Tables[0].Rows.Count > 0)
            {
                var drow = dsCus.Tables[0].Rows[0];

                //lblOtherByName.Text = drow["CustomerName"].ToString();

                // tab MT 103
                txtOrderingCustomer1.Text = drow["CustomerName"].ToString();
                txtOrderingCustomer2.Text = drow["Address"].ToString();
                txtOrderingCustomer3.Text = drow["City"].ToString();
                txtOrderingCustomer4.Text = drow["Country"].ToString();

                txtOtherBy2.Text = drow["CustomerName"].ToString();
                txtOtherBy3.Text = drow["Address"].ToString();
                txtOtherBy4.Text = drow["City"].ToString();
                txtOtherBy5.Text = drow["Country"].ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(txtOtherBy2.Text))
                {
                    txtOrderingCustomer1.Text = txtOtherBy2.Text;
                }

                if (!string.IsNullOrEmpty(txtOtherBy3.Text))
                {
                    txtOrderingCustomer2.Text = txtOtherBy3.Text;
                }

                if (!string.IsNullOrEmpty(txtOtherBy4.Text))
                {
                    txtOrderingCustomer3.Text = txtOtherBy4.Text;
                }

                if (!string.IsNullOrEmpty(txtOtherBy5.Text))
                {
                    txtOrderingCustomer4.Text = txtOtherBy5.Text;
                }
            }
        }

        protected void comboOtherBy_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
            e.Item.Attributes["CustomerName2"] = row["CustomerName2"].ToString();
        }

        protected void commom_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }

        protected void comboProfitCenteCust_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
            e.Item.Attributes["CustomerName2"] = row["CustomerName2"].ToString();
        }
        
        protected void SetDisableByReview(bool flag)
        {
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, flag);
            txtVATNo.Enabled = false;
        }

        protected void numDebitAmount_OnTextChanged(object sender, EventArgs e)
        {
            numCreditAmount.Value = numDebitAmount.Value;

            CalculatorInstructedAmount();
        }

        protected void txtOtherBy2_OnTextChanged(object sender, EventArgs e)
        {
            txtOrderingCustomer1.Text = txtOtherBy2.Text;

            LoadDebitAcctNo("");
        }

        protected void txtOtherBy3_OnTextChanged(object sender, EventArgs e)
        {
            txtOrderingCustomer2.Text = txtOtherBy3.Text;
        }

        protected void txtOtherBy4_OnTextChanged(object sender, EventArgs e)
        {
            txtOrderingCustomer3.Text = txtOtherBy4.Text;
        }

        protected void txtOtherBy5_OnTextChanged(object sender, EventArgs e)
        {
            txtOrderingCustomer4.Text = txtOtherBy5.Text;
        }

        protected void comboCommoditySer_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["ID"] = row["ID"].ToString();
            e.Item.Attributes["Name2"] = row["Name2"].ToString();
        }

        protected void comboReceiverCorrespondent_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblReceiverCorrespondentName.Text = comboReceiverCorrespondent.SelectedItem.Attributes["Description"];
        }

        protected void comboDetailOfCharges_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CalculatorInstructedAmount();

            //LoadChargeAcc();

            SetChargeAcct();

            SetChargeAccByDebitAcctNo();

            //---------------------
            var type = comboDetailOfCharges.SelectedValue;
            var totalAmount = numCommissionAmount.Value + numChargeAmount.Value;
            var totalCharges = ((totalAmount) * (110)) / 100;

            switch (type)
            {
                case "SHA":
                case "OUR":
                    lblSenderCharges.Text = string.Empty;
                    break;
                case "BEN":
                    if (totalCharges > 0)
                    {
                        lblSenderCharges.Text = String.Format("{0:C}", totalCharges).Replace("$", "");
                    }
                    break;
            }
        }

        protected void CalculatorInstructedAmount()
        {
            var type = comboDetailOfCharges.SelectedValue;
            switch (type)
            {
                case "SHA":
                case "OUR":
                    //BEN/OUR =  0.15% * Amount
                    //lblInterBankSettleAmount.Text = numCreditAmount.Text;
                    if (numCreditAmount.Value > 0)
                    {
                        lblInstancedAmount.Text = String.Format("{0:C}", numCreditAmount.Value).Replace("$", "");  
                        lblInterBankSettleAmount.Text = String.Format("{0:C}", numCreditAmount.Value).Replace("$", "");  
                    }
                    
                    break;

                case "BEN":
                    // SHA = (Amount * 0.15%) / 2
                    //lblInstancedAmount.Text = ((PercentOverseasTransfer() / 100) * numDebitAmount.Value).ToString();
                    //lblInstancedAmount.Text = (((PercentOverseasTransfer() / 100) * numDebitAmount.Value) / 2).ToString();

                    //lblInterBankSettleAmount.Text = (numCreditAmount.Value - (numCreditAmount.Value * (PercentOverseasTransfer() / 100))).ToString();
                    if (numCreditAmount.Value > 0)
                    {
                        lblInstancedAmount.Text = String.Format("{0:C}", numCreditAmount.Value).Replace("$", "");
                        lblInterBankSettleAmount.Text = String.Format("{0:C}", numCreditAmount.Value).Replace("$", "");  
                    }
                    
                    break;
            }
        }

        protected void numCreditAmount_OnTextChanged(object sender, EventArgs e)
        {
            numDebitAmount.Value = numCreditAmount.Value;

            CalculatorInstructedAmount();
        }

        protected void LoadChargeAcc()
        {
            switch (comboDetailOfCharges.SelectedValue)
            {
                case "OUR":
                case "SHA":
                    break;
                case "":
                    break;
            }
        }

        protected void SetChargeAcct()
        {
             //Charge Acct: Neu la phi BEN thi gan = NULL va khong cho edit
            if (string.IsNullOrEmpty(Request.QueryString["disable"]))
            {
                if (comboDetailOfCharges.SelectedValue == "BEN")
                {
                    //comboChargeAcct.SelectedValue = string.Empty;
                    //txtChargeAcctName.Text = string.Empty;
                    comboChargeAcct.Enabled = false;
                }
                else
                {
                    comboChargeAcct.Enabled = true;
                }
            }
        }
        
        protected void SetChargeAccByDebitAcctNo()
        {
            if (comboDetailOfCharges.SelectedValue == "BEN")
            {
                //LoadChargeAccByDebitAcctNo();
                comboChargeAcct.SelectedValue = comboDebitAcctNo.SelectedValue;

                if (comboDebitAcctNo.SelectedItem != null)
                {
                    comboChargeAcct.SelectedItem.Attributes["Currency"] =
                        comboDebitAcctNo.SelectedItem.Attributes["Currency"];
                }

                txtChargeAcctName.Text = comboChargeAcct.SelectedItem.Attributes["Name"];
                txtCommissionCurrency.Text = comboChargeAcct.SelectedItem.Attributes["Currency"];
                txtChargeCurrency.Text = comboChargeAcct.SelectedItem.Attributes["Currency"];
            }
            else
            {
                txtChargeAcctName.Text = string.Empty;
                txtCommissionCurrency.Text = string.Empty;
                txtChargeCurrency.Text = string.Empty;

                //LoadChargeAcc();
            }
        }

        protected void numCommissionAmount_OnTextChanged(object sender, EventArgs e)
        {
            CalculatorCommissionAmountChargeAmount();
        }

        protected void numChargeAmount_OnTextChanged(object sender, EventArgs e)
        {
            CalculatorCommissionAmountChargeAmount();
        }

        protected void CalculatorCommissionAmountChargeAmount()
        {
            var type = comboDetailOfCharges.SelectedValue;
            var totalAmount = numCommissionAmount.Value + numChargeAmount.Value;
            var totalCharges = ((totalAmount) * (110)) / 100;
            
            switch (type)
            {
                case "SHA":
                case "OUR":
                    if (numCreditAmount.Value > 0)
                    {
                        lblInterBankSettleAmount.Text = String.Format("{0:C}", numCreditAmount.Value).Replace("$", "");
                    }
                    break;

                case "BEN":
                    //(Commission Amount + Charge Amount) + ((Commission Amount + Charge Amount) * 1.1%)
                    totalAmount = numDebitAmount.Value - (totalCharges);

                    if (totalAmount > 0)
                    {
                        lblInterBankSettleAmount.Text = String.Format("{0:C}", totalAmount).Replace("$", "");
                    }
                    break;
            }

            totalAmount = numCommissionAmount.Value + numChargeAmount.Value;
            totalCharges = ((totalAmount) * (110)) / 100;
            if (totalCharges > 0)
            {
                lblTotalChargeAmount.Text = String.Format("{0:C}", totalCharges).Replace("$", "");                
            }

            switch (type)
            {
                case "SHA":
                case "OUR":
                    lblSenderCharges.Text = string.Empty;
                    break;
                case "BEN":
                    if (totalCharges > 0)
                    {
                        lblSenderCharges.Text = String.Format("{0:C}", totalCharges).Replace("$", "");
                    }                    
                    break;
            }
            
            lblTotalTaxAmount.Text = String.Format("{0:C}", (totalAmount * 0.1)).Replace("$", "");
        }

        protected void comboAccountType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_AccountWithInstitution();
        }

        protected void SetRelation_AccountWithInstitution()
        {
            lblAccountWithInstitutionName.Text = "";
            switch (comboAccountType.SelectedValue)
            {
                case "A":
                    //txtPartyIdentifyForInsti.Enabled = true;
                    txtAccountWithInstitutionNo.Enabled = true;                    
                    txtAccountWithBankAcct.Enabled = false;
                    txtAccountWithBankAcct.Text = "";
                    txtAccountWithBankAcct2.Enabled = false;
                    txtAccountWithBankAcct2.Text = "";
                    break;
                case "B":
                case "D":
                    //txtPartyIdentifyForInsti.Enabled = false;
                    //txtPartyIdentifyForInsti.Text = "";
                    txtAccountWithInstitutionNo.Enabled = false;
                    txtAccountWithInstitutionNo.Text = "";
                    txtAccountWithBankAcct.Enabled = true;
                    txtAccountWithBankAcct2.Enabled = true;
                    break;
            }
        }

        protected void comboIntermediaryInstruction_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //txtIntermediaryInstructionName.Text = comboIntermediaryInstruction.SelectedItem.Attributes["Description"];
        }

        protected void comboIntermediaryType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_IntermediaryInstruction();
        }

        protected void SetRelation_IntermediaryInstruction()
        {
            lblIntermediaryInstitutionName.Text = "";
            switch (comboIntermediaryType.SelectedValue)
            {
                case "A":
                    //txtPartyIdentifyForInter.Enabled = true;
                    txtIntermediaryInstitutionNo.Enabled = true;                    
                    txtIntermediaryInstruction1.Enabled = false;
                    txtIntermediaryInstruction1.Text = "";
                    txtIntermediaryInstruction2.Enabled = false;
                    txtIntermediaryInstruction2.Text = "";
                    break;
                case "B":
                case "D":
                    //txtPartyIdentifyForInter.Enabled = false;
                    //txtPartyIdentifyForInter.Text = "";
                    txtIntermediaryInstitutionNo.Enabled = false;
                    txtIntermediaryInstitutionNo.Text = "";
                    txtIntermediaryInstruction1.Enabled = true;
                    txtIntermediaryInstruction2.Enabled = true;
                    break;
            }
        }

        protected void comboCommissionCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            comboChargeCurrency.SelectedValue = comboCommissionCurrency.SelectedValue;
        }

        protected void comboChargeCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            comboCommissionCurrency.SelectedValue = comboChargeCurrency.SelectedValue;
        }

        protected void comboChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
            e.Item.Attributes["Currency"] = row["Currency"].ToString();
        }

        protected void commomSwiftCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Code"] = row["Code"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
            e.Item.Attributes["Currency"] = row["Currency"].ToString();
        }

        protected void btnMT103Report_Click(object sender, EventArgs e)
        {
            DataSet ds = bd.SQLData.B_BOVERSEASTRANSFER_Report(txtCode.Text);
            bc.Reports.createFileDownload(Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/OverseasTransfer/OverseasTransferMT103.doc"),
                ds, "OverseasTransferMT103_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf", Aspose.Words.SaveFormat.Pdf, Aspose.Words.SaveType.OpenInApplication, Response);
        }

        protected void btnPhieuCKReport_Click(object sender, EventArgs e)
        {
            bc.Reports.createFileDownload(Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/OverseasTransfer/PHIEUCHUYENKHOAN.doc"),
                bd.SQLData.B_BOVERSEASTRANSFER_PHIEUCHUYENKHOAN(txtCode.Text, UserInfo.Username), "PHIEUCHUYENKHOAN_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInApplication, Response);
        }

        protected void LoadDebitAcctNo(string customerName)
        {
            var dtAcc = bd.SQLData.B_BDRFROMACCOUNT_GetByNameWithoutVND(customerName);
            bc.Commont.initRadComboBox(ref comboDebitAcctNo, "Display", "Id", dtAcc);
            bc.Commont.initRadComboBox(ref comboOrderingCustAcc, "Display", "Id", dtAcc);
            bc.Commont.initRadComboBox(ref comboChargeAcct, "Display", "Id", dtAcc);

            if (dtAcc != null && dtAcc.Rows.Count > 0)
            {
                SetChargeAccByDebitAcctNo();
            }
            else
            {
                txtDeitCurrency.Text = string.Empty;
                txtCreditCurrency.Text = string.Empty;
            }
        }

        protected void btnVATReport_Click(object sender, EventArgs e)
        {
            bc.Reports.createFileDownload(Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/OverseasTransfer/OverseasTransferVAT.doc"), 
                bd.SQLData.B_BOVERSEASTRANSFER_VAT_REPORT(txtCode.Text, UserInfo.Username), "OverseasTransferVAT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInApplication, Response);
        }
        
        protected void txtIntermediaryInstitutionNo_OnTextChanged(object sender, EventArgs e)
        {
            lblIntermediaryInstitutionNoError.Text = string.Empty;
            lblIntermediaryInstitutionName.Text = string.Empty;

            var dt = CheckExistBankSwiftCode(txtIntermediaryInstitutionNo.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                lblIntermediaryInstitutionName.Text = dt.Rows[0]["BankName"].ToString();
            }
            else
            {
                lblIntermediaryInstitutionNoError.Text = "No found Intermediary Institution";
            }
        }

        protected DataTable CheckExistBankSwiftCode(string bankSwiftCode)
        {
            return bd.SQLData.B_BBANKSWIFTCODE_GetByCode(bankSwiftCode);
        }

        protected void txtAccountWithInstitutionNo_OnTextChanged(object sender, EventArgs e)
        {
            lblAccountWithInstitutionNoError.Text = string.Empty;
            lblAccountWithInstitutionName.Text = string.Empty;

            var dt = CheckExistBankSwiftCode(txtAccountWithInstitutionNo.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                lblAccountWithInstitutionName.Text = dt.Rows[0]["BankName"].ToString();
            }
            else
            {
                lblAccountWithInstitutionNoError.Text = "No found Account With Institution";
            }
        }

        protected void ResetControlAferAction()
        {
            comboCommissionCode.SelectedValue = "DEBIT PLUS CHARGES";
            comboChargeCode.SelectedValue = "DEBIT PLUS CHARGES";
            comboChargeFor.SelectedValue = "SENDER";
            comboCommissionFor.SelectedValue = "SENDER";
            GeneralVATNo();
        }

        protected void comboCommissionCode_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetChargeVisibilityByCommissionCode();
        }

        protected void SetChargeVisibilityByCommissionCode()
        {
            switch (comboCommissionCode.SelectedValue)
            {
                case "NO":
                    comboCommissionType.Enabled = true;
                    numCommissionAmount.Enabled = true;
                    comboCommissionFor.Enabled = true;
                    comboChargeCode.Enabled = true;
                    comboChargeType.Enabled = true;
                    numChargeAmount.Enabled = true;
                    comboChargeFor.Enabled = true;
                    comboDetailOfCharges_TabChargeInfo.Enabled = true;
                    txtVATNo.Enabled = false;
                    txtAddRemarks1.Enabled = true;
                    txtAddRemarks2.Enabled = true;
                    comboAccountOfficer.Enabled = true;
                    comboChargeAcct.Enabled = true;
                    break;

                case "YES":
                    comboCommissionType.Enabled = false;
                    numCommissionAmount.Enabled = false;
                    comboCommissionFor.Enabled = false;
                    comboChargeCode.Enabled = false;
                    comboChargeType.Enabled = false;
                    numChargeAmount.Enabled = false;
                    comboChargeFor.Enabled = false;
                    comboDetailOfCharges_TabChargeInfo.Enabled = false;
                    txtVATNo.Enabled = false;
                    txtAddRemarks1.Enabled = false;
                    txtAddRemarks2.Enabled = false;
                    comboAccountOfficer.Enabled = false;
                    comboChargeAcct.Enabled = false;
                    break;
            }
        }

        protected void GeneralVATNo()
        {
            txtVATNo.Text = bd.Database.B_BMACODE_GetNewSoTT("VATNO").Tables[0].Rows[0]["SoTT"].ToString();
        }

        protected void comboDebitAcctNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
            e.Item.Attributes["Currency"] = row["Currency"].ToString();
        }

        protected void comboDebitAcctNo_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var item = comboDebitAcctNo.SelectedItem;
            if (item != null)
            {
                txtDeitCurrency.Text = item.Attributes["Currency"];
                txtCreditCurrency.Text = item.Attributes["Currency"];
                SetChargeAccByDebitAcctNo();
            }
            comboOrderingCustAcc.SelectedValue = comboDebitAcctNo.SelectedValue;
            comboChargeAcct.SelectedValue = comboDebitAcctNo.SelectedValue;

            // hung

            LoadCreditAccountByDebitCurrency();
        }

        protected void comboOrderingCustAcc_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["ACCOUNT"] = row["ACCOUNT"].ToString();
            e.Item.Attributes["Currency"] = row["Currency"].ToString();
        }
    }
}