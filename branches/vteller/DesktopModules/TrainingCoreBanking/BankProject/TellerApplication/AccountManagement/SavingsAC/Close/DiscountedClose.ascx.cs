using BankProject.DataProvider;
using BankProject.Entity;
using BankProject.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close
{
    public partial class DiscountedClose : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        #region Property

        public bool DisableForm
        {
            get
            {
                bool disable = false;
                return Boolean.TryParse(Request.QueryString["disable"], out disable) || Mode == "preview";
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
        private SavingAccountDAO SavingAccountDAO
        {
            get
            {
                return new SavingAccountDAO();
            }
        }

        public string RefIdToReview
        {
            get
            {
                return Request.QueryString["RefId"];
            }
        }

        public string From
        {
            get
            {
                return Request.QueryString["from"];
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();
            LoadDataForDropdowns();
            BindDataToControl();
            if (DisableForm)
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }

          
        }
        //DateTime? startDate = null;
        //DateTime? endDate = null;
        //decimal amount = 0;
        //decimal amountPri = 0;
        //decimal interestRate = 0;
        //string accountCurrency = string.Empty;
        #region private methods
        private void BindDataToControl()
        {
            var discountedAccount = SavingAccountDAO.GetDiscountedAccountById(RefIdToReview);
            if (discountedAccount == null) return;
            var accountOpen = SavingAccountDAO.GetAccountOpenById(discountedAccount.WorkingAccId);
            if (accountOpen == null) return;
            tbcustomId.Text = accountOpen.CustomerID;
            rtbTerm.Text = discountedAccount.TDTerm;
            rdpFinalMatDate.Text = discountedAccount.TDFinalMatDate.Value.ToString("dd/MM/yyy");
            startDate.SelectedDate = discountedAccount.TDValueDate;
            endDate.SelectedDate = discountedAccount.TDFinalMatDate;
            interestRate.Value = (double?)discountedAccount.TDInterestRate.Value;
            tbDepositCode.Text = discountedAccount.RefId;
            accountCurrency.Text = accountOpen.Currency;
            lblCustomerName.Text = string.Format("{0} - {1}", accountOpen.AccountCode, accountOpen.CustomerName);
            lblCurrencyAmount.Text = accountOpen.Currency;

            if (accountOpen.Currency == "VND")
            {
                amount.Value = (double?)discountedAccount.DPAmountLCY.Value;
                amountPri.Value = (double?)discountedAccount.AmmountLCY.Value;
                lblAmount.Text = discountedAccount.AmmountLCY.ToString();
                lbTotalIntAmt.Text = discountedAccount.DPAmountLCY.ToString();
            }
            else {
                amount.Value = (double?)discountedAccount.DPAmountFCY.Value;
                amountPri.Value = (double?)discountedAccount.AmountFCY.Value;
                lblAmount.Text = discountedAccount.AmountFCY.ToString();
                lbTotalIntAmt.Text = discountedAccount.DPAmountFCY.ToString();
            }
            lbValueDate.Text = discountedAccount.TDValueDate.Value.ToString("dd/MM/yyyy");
           
            lbInterestRate.Text = discountedAccount.TDInterestRate.ToString();
            
            lblCrAccforPrincipal.Text = string.Format("{0} - {1}", discountedAccount.TDWorkingAccountId, discountedAccount.TDWorkingAccountName);
            txtCrAccforInterest.Text = lblCrAccforPrincipal.Text;

            DItbDepositNo.Text = discountedAccount.LDId;
            DIlbCustomer.Text = lblCrAccforPrincipal.Text;
            DIlblCurrency.Text = discountedAccount.TDCurrency;
            DILblDrAccount.Text = string.Format("{0} - {1}", discountedAccount.DPDrAccountId, discountedAccount.DPDrAccountName);
                       
            DIlbNarrative.Text = discountedAccount.DPNarrative;
            BuildCurrency(accountOpen.Currency);
            txtForTeller.Text = UserInfo.Username;

            var debitAccount = SavingAccountDAO.GetInternalBankAccountByCurrency(discountedAccount.PaymentCCY, discountedAccount.CustomerId);
            BuildCreditAccount(debitAccount);
            DIrcbCreditAccount.SelectedValue = discountedAccount.DebitAccount;

            lblDebitAccount.Text = debitAccount.Where(r => r.Code == discountedAccount.DebitAccount).Select(r=>r.AccountTitle).FirstOrDefault();

            if (accountOpen.Currency == "VND")
            {
                DIlbNewCustBal.Value = -1 * (discountedAccount.DPAmountLCY.HasValue ? (double?)discountedAccount.DPAmountLCY.Value : 0);
                DIlbAmtPaid.Value = (discountedAccount.DPAmountLCY.HasValue ? (double?)discountedAccount.DPAmountLCY.Value : 0);

            }
            else
            {
                DIlbNewCustBal.Value = -1 * (discountedAccount.DPAmountFCY.HasValue ? (double?)discountedAccount.DPAmountFCY.Value : 0);
                DIlbAmtPaid.Value = (discountedAccount.DPAmountFCY.HasValue ? (double?)discountedAccount.DPAmountFCY.Value : 0);
            }

             ComAmount(DateTime.Now, interestRate.Value);

            //review
            rdpNewMatDate.SelectedDate = discountedAccount.CloseDate.HasValue ? (DateTime?)discountedAccount.CloseDate.Value : DateTime.Now;
            rtbEligibleInterest.Value = discountedAccount.CloseInterest.HasValue ? (double?)discountedAccount.CloseInterest.Value : null;
            RdpIntRateVDate.SelectedDate = discountedAccount.CloseRateVDate.HasValue ? (DateTime?)discountedAccount.CloseRateVDate.Value : null;
            if (!string.IsNullOrEmpty(discountedAccount.CloseTeller))
            {
                txtForTeller.Text = discountedAccount.CloseTeller;
            }
            DItxtExchRate.Value = discountedAccount.CloseDealRate.HasValue ? (double?)discountedAccount.CloseDealRate.Value : null;
            DItxtNarrative.Text = discountedAccount.CloseNarrative;
            if (discountedAccount.CloseAmountLCY.HasValue)
            {
                DItxtAmountLCY.Value = (double?)discountedAccount.CloseAmountLCY;
            }
            if (discountedAccount.CloseAmountFCY.HasValue)
            {
                DItxtAmountFCY.Value = (double?)discountedAccount.CloseAmountFCY;
            }

            if (!rdpNewMatDate.SelectedDate.HasValue)
                rdpNewMatDate.SelectedDate = DateTime.Now;
        }

        private void ComAmount(DateTime? matDate, double? locInterestRate)
        {
            if (!locInterestRate.HasValue) return;
            var dayTerm = endDate.SelectedDate.Value.Subtract(startDate.SelectedDate.Value).Days;
            var totalCloseDate = matDate.Value.Subtract(startDate.SelectedDate.Value).Days;
            var n = totalCloseDate / dayTerm;
            var d = totalCloseDate - (dayTerm * n);
            var interestRateDay = locInterestRate.Value / 100 / 360;
            var amountPaid = d * interestRateDay * amountPri.Value;

            var totalAmount = amountPri.Value + (amount.Value * (n - 1)) + amountPaid;
            if (accountCurrency.Text == "VND")
            {
                DItxtAmountLCY.Value = (double?)totalAmount;
            }
            else
            {
                DItxtAmountFCY.Value = (double?)totalAmount;
            }
        }
        protected void ServerComAmount(object sender, EventArgs e)
        {
            ComAmount(rdpNewMatDate.SelectedDate, interestRate.Value);
            //lblCustomer.Text = rcbCustomerID.SelectedValue.ToString();
        }
        protected void ServerComAmountNewInterestRate(object sender, EventArgs e)
        {
            ComAmount(RdpIntRateVDate.SelectedDate, rtbEligibleInterest.Value);
           
        }
        private void BuildCreditAccount(IList<InternalBankAccount> debitAccount)
        {
            //var debitAccount = SavingAccountDAO.GetInternalBankAccountByCurrency(currency, customerId);
            DIrcbCreditAccount.Items.Clear();
            DIrcbCreditAccount.Items.Add(new RadComboBoxItem(""));
            DIrcbCreditAccount.AppendDataBoundItems = true;
            DIrcbCreditAccount.DataValueField = "Code";
            DIrcbCreditAccount.DataTextField = "AccountTitle";
            DIrcbCreditAccount.DataSource = debitAccount;
            DIrcbCreditAccount.DataBind();
            
        }
        
        private void BuildCurrency(string currency)
        {
            var currentcys = new List<Currency>();
            currentcys.Add(new Currency { Code = "VND" });
            DIrcbCurrencyPaid.DataValueField = "Code";
            DIrcbCurrencyPaid.DataTextField = "Code";
            if (currency == "VND")
            {
                DIrcbCurrencyPaid.DataSource = currentcys;
                DIrcbCurrencyPaid.DataBind();
                DIrcbCurrencyPaid.SelectedValue = "VND";
                DIrcbCurrencyPaid.Enabled = false;
            }
            else
            {
                currentcys.Add(new Currency { Code = currency });
                DIrcbCurrencyPaid.DataSource = currentcys;
                DIrcbCurrencyPaid.DataBind();
                DIrcbCurrencyPaid.SelectedValue = "VND";
                DIrcbCurrencyPaid.Enabled = true;
            }
        }
        
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
                    break;
            }
        }
        private void LoadDataForDropdowns()
        {
          
        }
        #endregion

        protected void rcbCustomerID_SelectIndexChange(object sender, EventArgs e)
        {
            //lblCustomer.Text = rcbCustomerID.SelectedValue.ToString();
        }
       
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "commit":
                    SavingAccountDAO.DiscountedCommitClose(RefIdToReview, rdpNewMatDate.SelectedDate, RdpIntRateVDate.SelectedDate,
                        (decimal?)rtbEligibleInterest.Value, (decimal?)DItxtExchRate.Value, txtForTeller.Text, DItxtNarrative.Text,
                        (decimal?)DItxtAmountLCY.Value, (decimal?)DItxtAmountFCY.Value, DIrcbCurrencyPaid.SelectedValue);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}", TabId));
                    break;
                case "Preview":
                    string[] param = new string[0];
                    Response.Redirect(EditUrl("", "", "ListReview", param));
                    break;
                case "authorize":
                    SavingAccountDAO.ApproveDiscountedAccount(tbDepositCode.Text, UserInfo.Username, From);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&ctl={1}&mid={2}", TabId, "ListReview", ModuleId));
                    break;
                case "reverse":
                    SavingAccountDAO.ReverseDiscountedAccount(tbDepositCode.Text, UserInfo.Username, From);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&ctl={1}&mid={2}", TabId, "ListReview", ModuleId));
                    break;

                case "hold":
                    if (From == "arrear")
                    {
                        SavingAccountDAO.HoldArrearSavingAccount(tbDepositCode.Text, UserInfo.Username);
                    }
                    else if (From == "periodic")
                    {
                        SavingAccountDAO.HoldPeriodicSavingAccount(tbDepositCode.Text, UserInfo.Username);
                    }
                    Response.Redirect(string.Format("Default.aspx?tabid={0}", TabId));
                    break;
                case "print":
                    PrintSavingAccDocument();
                    break;
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {

        }
        protected void cmbCustomerAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //System.Data.DataRowView row = e.Item.DataItem as System.Data.DataRowView;
            //e.Item.Attributes["Name"] = row["Name"].ToString();
            //e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
        }

        protected void rcbWaiveCharge_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //switch (rcbWaiveCharge.SelectedValue)
            //{
            //    case "YES":
            //        txtVatSerial.ReadOnly = true;
            //        txtChargeAmtLCY.ReadOnly = true;
            //        txtChargeVatAmt.ReadOnly = true;
            //        txtVatSerial.Text = "";
            //        txtChargeAmtLCY.Text = "";
            //        txtChargeVatAmt.Text = "";
            //        break;

            //    case "NO":
            //        txtVatSerial.ReadOnly = false;
            //        txtChargeAmtLCY.ReadOnly = false;
            //        txtChargeVatAmt.ReadOnly = false;
            //        break;

            //}
        }
        protected void rcbCurrency_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //rcbDrAccount.DataSource = BankProject.DataProvider.Database.B_BDRFROMACCOUNT_GetByCustomer(lbCustomer2.Text, rcbCurrency.SelectedValue);
            //rcbDrAccount.DataTextField = "Display";
            //rcbDrAccount.DataValueField = "Id";
            //rcbDrAccount.DataBind();

            //if (rcbCurrency.SelectedValue == "VND")
            //{
            //    txtAmountFCY.ReadOnly = true;
            //    txtAmountFCY.Value = 0;
            //    txtAmountLCY.ReadOnly = false;
            //}
            //else
            //{
            //    txtAmountFCY.ReadOnly = false;
            //    txtAmountLCY.ReadOnly = true;
            //    txtAmountLCY.Value = 0;
            //}

            //fillamount();
        }

        void fillamount()
        {
            //ListItemRepository repository = new ListItemRepository();
            //Entity.ListItem listItem = repository.GetDiscountedCloseByCode(tbDepositNo.Text);

            //double amount = 0;
            //if (listItem != null)
            //    if (rcbCurrency.SelectedValue == "VND")
            //    {
            //        txtAmountLCY.Value = double.Parse(listItem.OpenActual.Replace(",", ""));
            //        amount = txtAmountLCY.Value.Value;
            //    }
            //    else
            //    {
            //        txtAmountFCY.Value = double.Parse(listItem.OpenActual.Replace(",", ""));
            //        amount = txtAmountLCY.Value.Value;
            //    }

            //int songay = rdpNewMatDate.SelectedDate.HasValue?(rdpNewMatDate.SelectedDate.Value - new DateTime(2014,8,3)).Days : 1;
            //double tienlai = amount * songay * (rtbEligibleInterest.Value.HasValue ? rtbEligibleInterest.Value.Value : 1) / 360;
            //lbNewCustBal.Text = (-1 * tienlai).ToString("#,##0");
            //lbAmtPaid.Text = tienlai.ToString("#,##0");

            //lbNarrative.Text = tbDepositNo.Text;
        }

        protected void tbDepositNo_TextChanged(object sender, EventArgs e)
        {
            //rcbCurrency.SelectedValue = "VND";
            //rcbCurrency_SelectedIndexChanged(rcbCurrency, null);
            //fillamount();
        }

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
            if (custom == null) return ds;
            var row = infoTb.NewRow();
            row["SaveingAccountType"] = "Tiết kiệm trả lãi trước (Discounted)";
            row["Currency"] = lblCurrencyAmount.Text;
            row["CustomerFullName"] = custom.Rows[0]["GBFullName"];
            row["CustomerAddress"] = custom.Rows[0]["GBStreet"];
            row["CustomerDocId"] = custom.Rows[0]["DocID"];
            row["IssuedDate"] = string.IsNullOrEmpty(custom.Rows[0]["DocIssueDate"].ToString()) ? "" : Convert.ToDateTime(custom.Rows[0]["DocIssueDate"]).ToString("dd/MM/yyyy"); ;
            row["Term"] = rtbTerm.Text;
            row["IssuedDate"] = string.IsNullOrEmpty(custom.Rows[0]["DocIssueDate"].ToString()) ? "" : Convert.ToDateTime(custom.Rows[0]["DocIssueDate"]).ToString("dd/MM/yyyy"); ;
            row["InterestRate"] = lbInterestRate.Text;
            row["AccIssuedDate"] = lbValueDate.Text;
            row["CustomerId"] = tbcustomId.Text;
            row["AccDueDate"] = rdpFinalMatDate.Text;
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
            row["Date"] = lbValueDate.Text;
            row["TransactionCode"] = "GTV";
            row["Principle"] = lblAmount.Text;
            row["Balance"] = lblAmount.Text;
            row["InterestRate"] = lbInterestRate.Text;
            row["MaturityDate"] = rdpFinalMatDate.Text;
            itemTb.Rows.Add(row);
            row = itemTb.NewRow();
            row["Date"] = lbValueDate.Text;
            row["TransactionCode"] = "RTV";
            if (lblCurrencyAmount.Text == "VND")
            {
                row["Principle"] = lblAmount.Text;
                row["Balance"] = DItxtAmountLCY.Text;
            }
            else
            {
                row["Principle"] = lblAmount.Text;
                row["Balance"] = DItxtAmountFCY.Text;
            }
            if (rtbEligibleInterest.Value.HasValue)
            {
                row["InterestRate"] = rtbEligibleInterest.Text;
            }
            else
            {
                row["InterestRate"] = lbInterestRate.Text;
            }

            row["MaturityDate"] = rdpNewMatDate.SelectedDate.Value.ToString("dd/MM/yyyy");

            itemTb.Rows.Add(row);

            ds.Tables.Add(itemTb);

            return ds;
        }
        #endregion
    }
}