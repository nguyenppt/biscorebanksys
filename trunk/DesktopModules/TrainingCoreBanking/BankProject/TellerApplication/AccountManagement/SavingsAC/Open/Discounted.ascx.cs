using BankProject.DataProvider;
using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BankProject.Entity.SavingAcc;
using System.Data;
using System.Configuration;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Open
{
    public partial class Discounted : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        #region Property

        private SavingAccountDAO SavingAccountDAO
        {
            get
            {
                return new SavingAccountDAO();
            }
        }

        public string Mode
        {
            get
            {
                string mode = string.IsNullOrEmpty(Request.QueryString["mode"]) ? "normal" : Request.QueryString["mode"].ToLower();
                return mode;
            }
        }

        public bool DisableForm
        {
            get
            {
                bool disable = false;
                return Boolean.TryParse(Request.QueryString["disable"], out disable) || Mode == "preview";
            }
        }

        public string RefIdToReview
        {
            get
            {
                return Request.QueryString["RefId"];
            }
        }

        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
            LoadToolBar();           
            LoadDataForDropdowns();
            LoadOrGenerateDefaultData();
            if (DisableForm)
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }
        }

        protected void rcbCurrentcy_SelectIndexChange(object sender, EventArgs e)
        {
            var debitAccount = SavingAccountDAO.GetInternalBankAccountByCurrency(rcbPaymentCcy2.SelectedValue, tbcustomId.Text);

            rcbCreditAccount.Items.Clear();
            rcbCreditAccount.Items.Add(new RadComboBoxItem(""));
            rcbCreditAccount.DataValueField = "Code";
            rcbCreditAccount.DataTextField = "AccountTitle";
            rcbCreditAccount.DataSource = debitAccount;
            rcbCreditAccount.DataBind();
        }

        #region private methods
        private void LoadOrGenerateDefaultData()
        {
            switch (Mode)
            {
                case "preview":
                    tbDepositCode.Text = RefIdToReview;
                    BindDataToControl(RefIdToReview);
                    break;
                default:
                    GenerateDepositeCode();
                    GenerateDefaultData();
                    if(!string.IsNullOrEmpty(RefIdToReview))
                        BindDataToControl(RefIdToReview);
                    break;
            }
        }

        private void GenerateDefaultData()
        {
            rdpValueDate.SelectedDate = DateTime.Now;           
            txtForTeller.Text = UserInfo.Username;
            rtbForTeller.Text = UserInfo.Username;
        }

        private void GenerateDepositeCode()
        {
            tbDepositCode.Text = SavingAccountDAO.GenerateDepositeCode(SavingAccFunc.DISCOUNTED);
            LDid.Text = SQLData.B_BMACODE_GetNewID("PERIODIC_LOAN", "LD", "/");
        }
        private void BindDataToControl(string id)
        {
            var discountedAccount = SavingAccountDAO.GetDiscountedAccountById(id);
            if (discountedAccount == null) return;
           
            tbDepositCode.Text = discountedAccount.RefId;
            LDid.Text = discountedAccount.LDId;
            rcbWorkingAccount.SelectedValue = discountedAccount.WorkingAccId;
            tbcustomId.Text = discountedAccount.CustomerId;
            var accountOpen = SavingAccountDAO.GetAccountOpenById(discountedAccount.WorkingAccId);

            if (accountOpen == null) return;
            BindDataforWorkingAccount_SelectedChanged(discountedAccount.CustomerId);
           
            rtbAccountFCY.Value = discountedAccount.AmountFCY.HasValue ? (double?)discountedAccount.AmountFCY.Value : null;
            rtbNarrative.Text = discountedAccount.Narrative;
            rnbDealRate.Value = discountedAccount.DealRate.HasValue ? (double?)discountedAccount.DealRate.Value : null;
            rcbPaymentCCY.SelectedValue = discountedAccount.PaymentCCY;
            rtbForTeller.Text = discountedAccount.ForTeller;          
            lblCustomerNo.Text = discountedAccount.TDCustomerId;
            rcbJoinHolder.SelectedValue = discountedAccount.TDJoinHolderId;
            rcbProductLine.SelectedValue = discountedAccount.TDProductLineId;
            rcbCurrencyAmount.SelectedValue = discountedAccount.TDCurrency;
            rnbAmount.Value = discountedAccount.TDAmmount.HasValue ? (double?)discountedAccount.TDAmmount.Value : null;
            rdpValueDate.SelectedDate = discountedAccount.TDValueDate.HasValue ? (DateTime?)discountedAccount.TDValueDate.Value.Date : null;

            rtbTerm.SelectedValue = discountedAccount.TDTerm;
            rdpFinalMatDate.SelectedDate=discountedAccount.TDFinalMatDate.HasValue ? (DateTime?)discountedAccount.TDFinalMatDate.Value.Date : null;
            rtbInterestRate.Value = discountedAccount.TDInterestRate.HasValue ? (double?)discountedAccount.TDInterestRate.Value : null;
            lblTotalInt.Text = discountedAccount.TDTotalIntamt.ToString();
            rtbWorkingAccountId.Text = discountedAccount.TDWorkingAccountId;
            rtbWorkingAccountName.Text = discountedAccount.TDWorkingAccountName;
            rtbWorkingAccount.Text = discountedAccount.TDWorkingAccountId + " - " + discountedAccount.TDWorkingAccountName;
            rcbAccountOfficer.SelectedValue =discountedAccount.TDAccountOfficerId;

            rcbDrAccount.Text = discountedAccount.DPDrAccountId + " - " + discountedAccount.DPDrAccountName;
            rcbDrAccountId.Text = discountedAccount.DPDrAccountId;
            rcbDrAccountName.Text = discountedAccount.DPDrAccountName;
            txtAmountLCY.Value = discountedAccount.DPAmountLCY.HasValue ? (double?)discountedAccount.DPAmountLCY : null;
            txtAmountFCY.Value = discountedAccount.DPAmountFCY.HasValue ? (double?)discountedAccount.DPAmountFCY : null;
            ctxtAmountLCY.Text = discountedAccount.DPAmountLCY.HasValue ? discountedAccount.DPAmountLCY.ToString() : string.Empty;
            ctxtAmountFCY.Text = discountedAccount.DPAmountFCY.HasValue ? discountedAccount.DPAmountFCY.ToString() : string.Empty;
            lbNarrative.Text = discountedAccount.DPNarrative;
       
            txtForTeller.Text=discountedAccount.DPForTeller;          
            txtExchRate.Value=discountedAccount.DPExchRate.HasValue ? (double?)discountedAccount.DPExchRate.Value : null;

            var debitAccount = SavingAccountDAO.GetInternalBankAccountByCurrency(discountedAccount.PaymentCCY, discountedAccount.CustomerId);
            rcbDebitAccount.Items.Clear();    
            rcbDebitAccount.DataValueField = "Code";
            rcbDebitAccount.DataTextField = "AccountTitle";
            rcbDebitAccount.DataSource = debitAccount;
            rcbDebitAccount.DataBind();
            rcbDebitAccount.SelectedValue = discountedAccount.DebitAccount;

            rcbCreditAccount.Items.Clear();          
            rcbCreditAccount.DataValueField = "Code";
            rcbCreditAccount.DataTextField = "AccountTitle";
            rcbCreditAccount.DataSource = debitAccount;
            rcbCreditAccount.DataBind();
            rcbCreditAccount.SelectedValue = discountedAccount.DPCreditAccount;
            lblAmtCreditForCust.Text = discountedAccount.AmmountLCY.ToString();
            lblNewCustBal.Text = (discountedAccount.AmmountLCY + (accountOpen.ActualBallance.HasValue ? accountOpen.ActualBallance.Value : 0)).ToString();

            var valueAmount = discountedAccount.DPAmountLCY;
            if (discountedAccount.DPPaymentCcy != "VND")
            {
                valueAmount = discountedAccount.DPAmountFCY;
            }
            if (valueAmount != null)
            {
                lbNewCustBal.Text = "-" + valueAmount.ToString();
                lbAmtPaid.Text = valueAmount.ToString();
            }
            var currentcys = new List<Currency>();
            currentcys.Add(new Currency());
            currentcys.Add(new Currency { Code = "VND" });
            rcbPaymentCcy2.DataValueField = "Code";
            rcbPaymentCcy2.DataTextField = "Code";
            if (discountedAccount.PaymentCCY == "VND")
            {
                rcbPaymentCcy2.Items.Clear();
                rcbPaymentCcy2.DataSource = currentcys;
                rcbPaymentCcy2.DataBind();
                rcbPaymentCcy2.SelectedValue = "VND";
                rcbPaymentCcy2.Enabled = false;
                var debitAccount1 = SavingAccountDAO.GetInternalBankAccountByCurrency(rcbPaymentCcy2.SelectedValue, tbcustomId.Text);

                rcbCreditAccount.Items.Clear();
                rcbCreditAccount.Items.Add(new RadComboBoxItem(""));
                rcbCreditAccount.DataValueField = "Code";
                rcbCreditAccount.DataTextField = "AccountTitle";
                rcbCreditAccount.DataSource = debitAccount1;
                rcbCreditAccount.DataBind();
            }
            else
            {
                currentcys.Add(new Currency { Code = rcbPaymentCCY.SelectedValue });
                rcbPaymentCcy2.Items.Clear();
                rcbPaymentCcy2.DataSource = currentcys;
                rcbPaymentCcy2.DataBind();
                //rcbPaymentCcy2.SelectedValue = "VND";
                rcbPaymentCcy2.Enabled = true;
                var debitAccount2 = SavingAccountDAO.GetInternalBankAccountByCurrency(discountedAccount.DPPaymentCcy, tbcustomId.Text);

                rcbCreditAccount.Items.Clear();
                rcbCreditAccount.Items.Add(new RadComboBoxItem(""));
                rcbCreditAccount.DataValueField = "Code";
                rcbCreditAccount.DataTextField = "AccountTitle";
                rcbCreditAccount.DataSource = debitAccount2;
                rcbCreditAccount.DataBind();
            }
            rcbPaymentCcy2.SelectedValue = discountedAccount.DPPaymentCcy;
            FormatAmmountControl(rcbPaymentCCY.SelectedValue);



           
            hidbal.Text = accountOpen.ActualBallance.HasValue ? accountOpen.ActualBallance.Value.ToString() : string.Empty;
            //decimal value = 0;
            //if (discountedAccount.DPPaymentCcy != "VND")
            //{
            //    value = discountedAccount.AmountFCY.Value;
            //    lbNewCustBal.Value = (-1) * (double)discountedAccount.DPAmountFCY.Value;
            //}
            //else
            //{
            //    value = discountedAccount.AmmountLCY.Value;
            //    lbNewCustBal.Value = (-1) * (double)discountedAccount.DPAmountLCY.Value;
            //}
            //if (accountOpen.ActualBallance.HasValue)
            //{
            //    value = value + accountOpen.ActualBallance.Value;
            //    lbAmtPaid.Value = (double)value;
            //}
            
           

            tbcustomId.Text = accountOpen.CustomerID;
            lblCurrency.Text = accountOpen.Currency;
            lblCurrency2.Text = accountOpen.Currency;

            rcbCurrencyAmount.SelectedValue = accountOpen.Currency;
            lblCustomerName.Text = accountOpen.CustomerName;
            lblCustomerName2.Text = accountOpen.CustomerName;
            rcbPaymentCCY.SelectedValue = accountOpen.Currency;

            rtbWorkingAccount.Text = accountOpen.AccountCode + " - " + accountOpen.CustomerName;
            rtbWorkingAccountId.Text = accountOpen.AccountCode;
            rtbWorkingAccountName.Text = accountOpen.CustomerName;
            rcbDrAccount.Text = accountOpen.AccountCode + " - " + accountOpen.CustomerName;
            rcbDrAccountId.Text = accountOpen.AccountCode;
            rcbDrAccountName.Text = accountOpen.CustomerName;
            rcbAccountOfficer.SelectedValue = accountOpen.AccountOfficerID;
            lblCustomerNo.Text = accountOpen.CustomerID;
            lblCustomerNoTitle.Text = string.Format("{0} - {1}", accountOpen.AccountCode, accountOpen.CustomerName);

            rtbAccountLCY.Value = discountedAccount.AmmountLCY.HasValue ? (double?)discountedAccount.AmmountLCY.Value : null;

            if (discountedAccount.Status == AuthoriseStatus.AUT.ToString())
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            }

            if (discountedAccount.Status != AuthoriseStatus.AUT.ToString())
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                ///reset lai
                RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                RadToolBar1.FindItemByValue("btPrint").Enabled = false;

                //set lai
                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
            }
        }

        private void LoadDataForDropdowns()
        {
            var allWorkingAccount = SavingAccountDAO.GetAllWorkingAccount();
            rcbWorkingAccount.Items.Clear();
            rcbWorkingAccount.Items.Add(new RadComboBoxItem(""));
            rcbWorkingAccount.AppendDataBoundItems = true;
            rcbWorkingAccount.DataValueField = "Id";
            rcbWorkingAccount.DataTextField = "Title";
            rcbWorkingAccount.DataSource = allWorkingAccount;
            rcbWorkingAccount.DataBind();

            var currentcys = SavingAccountDAO.GetAllCurrency();
            rcbPaymentCCY.DataValueField = "Code";
            rcbPaymentCCY.DataTextField = "Code";
            rcbPaymentCCY.DataSource = currentcys;
            rcbPaymentCCY.DataBind();

            var customers = SavingAccountDAO.GetAllAuthorisedCustomer();
            rcbJoinHolder.Items.Clear();
            rcbJoinHolder.Items.Add(new RadComboBoxItem(""));
            rcbJoinHolder.DataSource = customers;
            rcbJoinHolder.DataTextField = "CustomerName";
            rcbJoinHolder.DataValueField = "CustomerID";
            rcbJoinHolder.DataBind();

            rcbProductLine.Items.Clear();
            rcbProductLine.Items.Add(new RadComboBoxItem(""));
            rcbProductLine.DataSource = SavingAccountDAO.GetAllProductLinesForDiscounted();
            rcbProductLine.DataTextField = "Description";
            rcbProductLine.DataValueField = "ProductID";
            rcbProductLine.DataBind();

            rcbCurrencyAmount.DataValueField = "Code";
            rcbCurrencyAmount.DataTextField = "Code";
            rcbCurrencyAmount.DataSource = currentcys;
            rcbCurrencyAmount.DataBind();

            rcbAccountOfficer.Items.Clear();
            rcbAccountOfficer.Items.Add(new RadComboBoxItem(""));
            rcbAccountOfficer.DataSource = SQLData.B_BACCOUNTOFFICER_GetAll();
            rcbAccountOfficer.DataValueField = "Code";
            rcbAccountOfficer.DataTextField = "Description";
            rcbAccountOfficer.DataBind();

          

            rtbTerm.DataValueField = "InterestTerm";
            rtbTerm.DataTextField = "Description";
            rtbTerm.DataSource = SavingAccountDAO.GetAllTerm();
            rtbTerm.DataBind();       

          
        }

        private decimal? BindDataforWorkingAccount_SelectedChanged(string workingAccountId)
        {
            var accountOpen = SavingAccountDAO.GetAccountOpenById(workingAccountId);

            if (accountOpen == null) return null;
            hidbal.Text = accountOpen.ActualBallance.HasValue ? accountOpen.ActualBallance.Value.ToString() : string.Empty;
            tbcustomId.Text = accountOpen.CustomerID;
            lblCurrency.Text = accountOpen.Currency;
            lblCurrency2.Text = accountOpen.Currency;

            rcbCurrencyAmount.SelectedValue = accountOpen.Currency;
            lblCustomerName.Text = accountOpen.CustomerName;
            lblCustomerName2.Text = accountOpen.CustomerName;
            rcbPaymentCCY.SelectedValue = accountOpen.Currency;

            rtbWorkingAccount.Text = accountOpen.AccountCode + " - " + accountOpen.CustomerName;
            rtbWorkingAccountId.Text = accountOpen.AccountCode;
            rtbWorkingAccountName.Text = accountOpen.CustomerName;
            rcbDrAccount.Text = accountOpen.AccountCode + " - " + accountOpen.CustomerName;
            rcbDrAccountId.Text = accountOpen.AccountCode ;
            rcbDrAccountName.Text = accountOpen.CustomerName;
            rcbProductLine.SelectedValue = accountOpen.ProductLineID;
            rcbAccountOfficer.SelectedValue = accountOpen.AccountOfficerID;
            rcbJoinHolder.SelectedValue = accountOpen.JoinHolderID;


            lblCustomerNo.Text = accountOpen.CustomerID;
            lblCustomerNoTitle.Text = string.Format("{0} - {1}", accountOpen.AccountCode, accountOpen.CustomerName);

            var debitAccount = SavingAccountDAO.GetInternalBankAccountByCurrency(accountOpen.Currency, accountOpen.CustomerID); ;
            rcbDebitAccount.Items.Clear();
            rcbDebitAccount.Items.Add(new RadComboBoxItem(""));
            rcbDebitAccount.DataValueField = "Code";
            rcbDebitAccount.DataTextField = "AccountTitle";
            rcbDebitAccount.DataSource = debitAccount;
            rcbDebitAccount.DataBind();

            //rcbCreditAccount.Items.Clear();
            //rcbCreditAccount.Items.Add(new RadComboBoxItem(""));
            //rcbCreditAccount.DataValueField = "Code";
            //rcbCreditAccount.DataTextField = "AccountTitle";
            //rcbCreditAccount.DataSource = debitAccount;
            //rcbCreditAccount.DataBind();
            var currentcys = new List<Currency>();
            currentcys.Add(new Currency{Code="VND"});
            rcbPaymentCcy2.DataValueField = "Code";
            rcbPaymentCcy2.DataTextField = "Code";
            if (accountOpen.Currency == "VND")
            {               
                rcbPaymentCcy2.DataSource = currentcys;
                rcbPaymentCcy2.DataBind();
                rcbPaymentCcy2.SelectedValue = "VND";
                rcbPaymentCcy2.Enabled = false;
                var debitAccount1 = SavingAccountDAO.GetInternalBankAccountByCurrency(rcbPaymentCcy2.SelectedValue, tbcustomId.Text);

                rcbCreditAccount.Items.Clear();
                rcbCreditAccount.Items.Add(new RadComboBoxItem(""));
                rcbCreditAccount.DataValueField = "Code";
                rcbCreditAccount.DataTextField = "AccountTitle";
                rcbCreditAccount.DataSource = debitAccount1;
                rcbCreditAccount.DataBind();
            }else
            {
                currentcys.Add(new Currency { Code = accountOpen.Currency });             
                rcbPaymentCcy2.DataSource = currentcys;
                rcbPaymentCcy2.DataBind();
                //rcbPaymentCcy2.SelectedValue = "VND";
                rcbPaymentCcy2.Enabled = true;
                var debitAccount2 = SavingAccountDAO.GetInternalBankAccountByCurrency(rcbPaymentCcy2.SelectedValue, tbcustomId.Text);

                rcbCreditAccount.Items.Clear();
                rcbCreditAccount.Items.Add(new RadComboBoxItem(""));
                rcbCreditAccount.DataValueField = "Code";
                rcbCreditAccount.DataTextField = "AccountTitle";
                rcbCreditAccount.DataSource = debitAccount2;
                rcbCreditAccount.DataBind();
            }
            FormatAmmountControl(accountOpen.Currency);
            return accountOpen.ActualBallance;
        }
        #endregion

        #region event
        //protected void rcbCustomerID_SelectIndexChange(object sender, EventArgs e)
        //{
        //    //lblCustomer.Text = rcbCustomerID.SelectedValue.ToString();
        //}
       
        private void LoadToolBar()
        {
            switch (Mode)
            {
                case "preview":
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                    break;
                default:
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    //RadToolBar1.FindItemByValue("btSearch").Enabled = true;
                    break;
            }
        }
        
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
                      
            switch (commandName)
            {
                case "commit":
                    if (CommitDiscountedAccount())
                    {
                        Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                    }
                    break;
                case "preview":
                    Response.Redirect(EditUrl("", "", "SavingAccReviewList"));
                    break;
                case "authorize":
                    SavingAccountDAO.ApproveDiscountedAccount(tbDepositCode.Text, UserInfo.Username);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                    break;
                case "reverse":
                    SavingAccountDAO.ReverseDiscountedAccount(tbDepositCode.Text, UserInfo.Username);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                    break;
                case "print":
                    PrintSavingAccDocument();
                    break;
            }
           
        }

        private bool CommitDiscountedAccount()
        {
            var discountedAccount = new DiscountedAccount();
            BuilddiscountedAccount(discountedAccount);
            if (SavingAccountDAO.CheckDiscountedAccountExist(discountedAccount.RefId))
            {
                discountedAccount.UpdatedBy = this.UserInfo.Username;
                return SavingAccountDAO.UpdateDiscountedAccount(discountedAccount);
            }
            else
            {
                discountedAccount.CreatedBy = this.UserInfo.Username;
                return SavingAccountDAO.CreateNewDiscountedAccount(discountedAccount);
            }
           
        }

        private void BuilddiscountedAccount(DiscountedAccount discountedAccount)
        {
            discountedAccount.RefId = tbDepositCode.Text;
            discountedAccount.LDId = LDid.Text;
            discountedAccount.Status = AuthoriseStatus.UNA.ToString();
            discountedAccount.WorkingAccId = rcbWorkingAccount.SelectedValue;
            discountedAccount.CustomerId = tbcustomId.Text;
            discountedAccount.AmmountLCY = rtbAccountLCY.Value.HasValue ? (decimal?)rtbAccountLCY.Value : null;
            discountedAccount.AmountFCY = rtbAccountFCY.Value.HasValue ? (decimal?)rtbAccountFCY.Value : null;
            discountedAccount.Narrative = rtbNarrative.Text;
            discountedAccount.DealRate = rnbDealRate.Value.HasValue ? (decimal?)rnbDealRate.Value : null;
            discountedAccount.PaymentCCY = rcbPaymentCCY.SelectedValue;
            discountedAccount.ForTeller = rtbForTeller.Text;
            discountedAccount.DebitAccount = rcbDebitAccount.SelectedValue;
            discountedAccount.DPAmountLCY = !string.IsNullOrEmpty(ctxtAmountLCY.Text) ? (decimal?)Convert.ToDecimal(ctxtAmountLCY.Text) : null;
            discountedAccount.TDCustomerId = lblCustomerNo.Text;
            discountedAccount.TDJoinHolderId = rcbJoinHolder.SelectedValue;
            discountedAccount.TDProductLineId = rcbProductLine.SelectedValue;
            discountedAccount.TDCurrency = rcbCurrencyAmount.SelectedValue;
            
            discountedAccount.TDValueDate = rdpValueDate.SelectedDate.HasValue? (DateTime?)rdpValueDate.SelectedDate.Value.Date:null;
            //discountedAccount.TDBusDayDate = tbBusDayDef.Text;
            discountedAccount.TDTerm = rtbTerm.SelectedValue;
            discountedAccount.TDFinalMatDate = rdpFinalMatDate.SelectedDate.HasValue ? (DateTime?)rdpFinalMatDate.SelectedDate.Value.Date : null;
            discountedAccount.TDInterestRate = rtbInterestRate.Value.HasValue ? (decimal?)rtbInterestRate.Value : null;
            discountedAccount.TDTotalIntamt = string.IsNullOrEmpty(lblTotalInt.Text) ? null : (decimal?)Convert.ToDecimal(lblTotalInt.Text);
            discountedAccount.TDWorkingAccountId = rtbWorkingAccountId.Text;
            discountedAccount.TDWorkingAccountName = rtbWorkingAccountName.Text;
            discountedAccount.TDAccountOfficerId = rcbAccountOfficer.SelectedValue;

            discountedAccount.DPDrAccountId = rcbDrAccountId.Text;
            discountedAccount.DPDrAccountName = rcbDrAccountName.Text;
            discountedAccount.DPAmountFCY = !string.IsNullOrEmpty(ctxtAmountFCY.Text) ? (decimal?)Convert.ToDecimal(ctxtAmountFCY.Text):null;
           
            discountedAccount.DPNarrative = "ID: [" + LDid.Text + "]-P: [" + rnbAmount.Value + "]-R: [" + rtbInterestRate.Value + "]-T: [" + rtbTerm.SelectedValue + "]";//lbNarrative.Text;
            discountedAccount.DPPaymentCcy = rcbPaymentCcy2.SelectedValue;
            discountedAccount.DPForTeller = txtForTeller.Text;
            discountedAccount.DPCreditAccount = rcbCreditAccount.SelectedValue;
            discountedAccount.DPExchRate = txtExchRate.Value.HasValue ? (decimal?)txtExchRate.Value : null;
            if (discountedAccount.TDCurrency == "VND")
            {
                rtbAccountLCY.Enabled = true;
            }
            else
            {
                rtbAccountFCY.Enabled = true;
                rnbDealRate.Enabled = true;
            }
            if (discountedAccount.TDCurrency == "VND")
            {
                discountedAccount.TDAmmount = discountedAccount.AmmountLCY;
            }
            else
            {
                discountedAccount.TDAmmount = discountedAccount.AmountFCY;
                if (discountedAccount.AmountFCY.HasValue && discountedAccount.DealRate.HasValue)
                {
                    discountedAccount.AmmountLCY = discountedAccount.AmountFCY * discountedAccount.DealRate;
                }
                if (discountedAccount.DPAmountFCY.HasValue && discountedAccount.DPExchRate.HasValue)
                {
                    discountedAccount.DPAmountLCY = discountedAccount.DPAmountFCY.Value * discountedAccount.DPExchRate.Value;
                }
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            BindDataToControl(tbDepositCode.Text);
        }
        //protected void cmbCustomerAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        //{
        //    System.Data.DataRowView row = e.Item.DataItem as System.Data.DataRowView;
        //    e.Item.Attributes["Name"] = row["Name"].ToString();
        //    e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
        //}
        protected void rtbAccountLCY_TextChanged(object sender, EventArgs e)
        {
            var accountOpen = SavingAccountDAO.GetAccountOpenById(rcbWorkingAccount.SelectedValue);

            if (accountOpen == null) return;
            if (rtbAccountLCY.Value.HasValue && lblCurrency.Text=="VND")
            {
                lblAmtCreditForCust.Text = rtbAccountLCY.Value.ToString();
                lblNewCustBal.Text = (rtbAccountLCY.Value + (double)(accountOpen.ActualBallance.HasValue ? accountOpen.ActualBallance.Value : 0)).ToString();
                rnbAmount.Value = rtbAccountLCY.Value;
            }
            else
            {
                if (rtbAccountFCY.Value.HasValue && rnbDealRate.Value.HasValue)
                {
                    var value = rtbAccountFCY.Value * rnbDealRate.Value;
                    rtbAccountLCY.Value = value;
                    lblAmtCreditForCust.Text = value.ToString();
                    lblNewCustBal.Text = (value + (double)(accountOpen.ActualBallance.HasValue ? accountOpen.ActualBallance.Value : 0)).ToString();
                    rnbAmount.Value = rtbAccountFCY.Value;

                }
                else {
                    lblAmtCreditForCust.Text = string.Empty;
                    lblNewCustBal.Text = string.Empty;
                    
                }
            }
        }
        
        protected void rcbWorkingAccount_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindDataforWorkingAccount_SelectedChanged(e.Value);
        }
        private void FormatAmmountControl(string currency)
        {
            if (currency == "VND")
            {
                rtbAccountFCY.Enabled = false;
                rnbDealRate.Enabled = false;
                rtbAccountLCY.Enabled = true;
                rtbAccountFCY.Value =null;
                rnbDealRate.Value = null;
            }
            else
            {
                rtbAccountFCY.Enabled = true;
                rnbDealRate.Enabled = true;
                rtbAccountLCY.Enabled = false;
                rtbAccountLCY.Value = null;
            }
        }
        #endregion

        #region Print
        private void PrintSavingAccDocument()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");
            //Open template
            string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/SavingAcc/SavingAccount.docx");
            //Open the template document
            Aspose.Words.Document document = new Aspose.Words.Document(docPath);
            //Execute the mail merge.
            var ds = PrepareData2Print();
            // Fill the fields in the document with user data.
            document.MailMerge.ExecuteWithRegions(ds.Tables["Info"]);
            document.MailMerge.ExecuteWithRegions(ds.Tables["Items"]);
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            document.Save("SavingAccount_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }

        private DataSet PrepareData2Print()
        {
            var ds = new DataSet();
            var infoTb = new DataTable("Info");
            infoTb.Columns.Add("SaveingAccountType");
            infoTb.Columns.Add("Currency");
            infoTb.Columns.Add("CustomerFullName");
            infoTb.Columns.Add("CustomerAddress");
            infoTb.Columns.Add("CustomerDocId");
            infoTb.Columns.Add("IssuedDate");
            infoTb.Columns.Add("Term");
            infoTb.Columns.Add("InterestRate");
            infoTb.Columns.Add("AccIssuedDate");
            infoTb.Columns.Add("CustomerId");
            infoTb.Columns.Add("AccDueDate");
            infoTb.Columns.Add("RefId");
            infoTb.Columns.Add("BranchName");
            infoTb.Columns.Add("BranchAdd");
            infoTb.Columns.Add("BranchTel");

            
            var custom = SavingAccountDAO.GetAuthorisedCustomerById(tbcustomId.Text);
            if (custom.Rows.Count<=0) return ds;
            var row = infoTb.NewRow();
            row["SaveingAccountType"] = "Tiết kiệm trả lãi trước (Discounted)";
            row["Currency"] = lblCurrency.Text;
            row["CustomerFullName"] = custom.Rows[0]["GBFullName"];
            row["CustomerAddress"] = custom.Rows[0]["GBStreet"];
            row["CustomerDocId"] = custom.Rows[0]["DocID"];
            row["IssuedDate"] = string.IsNullOrEmpty(custom.Rows[0]["DocIssueDate"].ToString()) 
                ? "" 
                : Convert.ToDateTime(custom.Rows[0]["DocIssueDate"]).ToString("dd/MM/yyyy");
            row["Term"] = rtbTerm.SelectedItem.Text;
            row["IssuedDate"] = string.IsNullOrEmpty(custom.Rows[0]["DocIssueDate"].ToString()) 
                ? "" 
                : Convert.ToDateTime(custom.Rows[0]["DocIssueDate"]).ToString("dd/MM/yyyy");
            row["InterestRate"] = rtbInterestRate.Text;
            row["AccIssuedDate"] = rdpValueDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            row["CustomerId"] = tbcustomId.Text;
            row["AccDueDate"] = rdpFinalMatDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            row["RefId"] = tbDepositCode.Text;
            row["BranchName"] = ConfigurationManager.AppSettings["ChiNhanh"];
            row["BranchAdd"] = ConfigurationManager.AppSettings["BranchAddress"];
            row["BranchTel"] = ConfigurationManager.AppSettings["BranchTel"];

            infoTb.Rows.Add(row);

            ds.Tables.Add(infoTb);

            var itemTb = new DataTable("Items");
            itemTb.Columns.Add("Date");
            itemTb.Columns.Add("TransactionCode");
            itemTb.Columns.Add("Principle");
            itemTb.Columns.Add("Balance");
            itemTb.Columns.Add("InterestRate");
            itemTb.Columns.Add("MaturityDate");

            row = itemTb.NewRow();
            row["Date"] = rdpValueDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            row["TransactionCode"] = "GTV";
            row["Principle"] = rnbAmount.DisplayText;
            row["Balance"] = rnbAmount.DisplayText;
            row["InterestRate"] = rtbInterestRate.DisplayText;
            row["MaturityDate"] = rdpFinalMatDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            itemTb.Rows.Add(row);
            row = itemTb.NewRow();
            row["Date"] = rdpValueDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            row["TransactionCode"] = "RLT";
            if (rcbPaymentCcy2.SelectedValue == "VND")
            {
                row["Principle"] = txtAmountLCY.DisplayText;
                row["Balance"] = txtAmountLCY.DisplayText;
            }
            else
            {
                row["Principle"] = txtAmountFCY.DisplayText;
                row["Balance"] = txtAmountFCY.DisplayText;
            }
            row["InterestRate"] = rtbInterestRate.DisplayText;
            row["MaturityDate"] = rdpFinalMatDate.SelectedDate.Value.ToString("dd/MM/yyyy");

            itemTb.Rows.Add(row);

            ds.Tables.Add(itemTb);

            return ds;
        }
        #endregion
    }
        
}