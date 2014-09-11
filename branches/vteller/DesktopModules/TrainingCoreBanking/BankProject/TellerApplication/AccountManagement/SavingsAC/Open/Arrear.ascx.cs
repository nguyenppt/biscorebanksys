using BankProject.DataProvider;
using BankProject.Entity;
using BankProject.Entity.SavingAcc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Open
{
    public partial class Arrear : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            LoadToolBar();
            LoadDataForDropdowns();
            LoadOrGenerateDefaultData();
            if (DisableForm)
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }
        }

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

        #region Events

        protected void rcbProductAZ_SelectIndexChange(object sender, EventArgs e)
        {
            var productType = SavingAccountDAO.GetProductTypeByCode(rcbProductAZ.SelectedValue);
            lblMaturityInstrAZ.Text = productType == null ? string.Empty : productType.MaturityInstr;
        }

        protected void rcbCustomerID_SelectIndexChange(object sender, EventArgs e)
        {
            lblCustomer.Text = rcbCustomerID.SelectedValue.ToString();
            CustomerIdAZ.Text = rcbCustomerID.Text;
            lblWokingAccNameAZ.Text = rcbCustomerID.Text;
            lblCategoryAZ.Text = rcbCategoryCode.SelectedValue;
            var workingAcc = SavingAccountDAO.GetWorkingBankAccountByCustomerIdForOpen(rcbCustomerID.SelectedValue, rcbCurrentcy.SelectedValue);
            rcbWorkingAccAZ.Items.Clear();
            rcbWorkingAccAZ.Items.Add(new RadComboBoxItem(""));
            rcbWorkingAccAZ.AppendDataBoundItems = true;
            rcbWorkingAccAZ.DataValueField = "AccountCode";
            rcbWorkingAccAZ.DataTextField = "AccountCode";
            rcbWorkingAccAZ.DataSource = workingAcc;
            rcbWorkingAccAZ.DataBind();

            //TTrcbDebitAmmount.Items.Clear();
            //TTrcbDebitAmmount.DataValueField = "BankAccount";
            //TTrcbDebitAmmount.DataTextField = "BankAccount";
            //TTrcbDebitAmmount.DataSource = workingAcc;
            //TTrcbDebitAmmount.DataBind();
        }

        protected void rcbCurrentcy_SelectIndexChange(object sender, EventArgs e)
        {
            var debitAccount = SavingAccountDAO.GetInternalBankAccountByCurrency(rcbCurrentcy.SelectedValue, rcbCustomerID.SelectedValue);

            TTrcbDebitAmmount.Items.Clear();
            TTrcbDebitAmmount.Items.Add(new RadComboBoxItem(""));
            TTrcbDebitAmmount.DataValueField = "Code";
            TTrcbDebitAmmount.DataTextField = "AccountTitle";
            TTrcbDebitAmmount.DataSource = debitAccount;
            TTrcbDebitAmmount.DataBind();

            lblCurrencyAZ.Text = rcbCurrentcy.SelectedValue;
            TTrcbPaymentCcy.SelectedValue = rcbCurrentcy.SelectedValue;
            CompareCurrencyVSPaymentCCY.Text = "1";

            var workingAcc = SavingAccountDAO.GetWorkingBankAccountByCustomerIdForOpen(rcbCustomerID.SelectedValue, rcbCurrentcy.SelectedValue);
            rcbWorkingAccAZ.Items.Clear();
            rcbWorkingAccAZ.Items.Add(new RadComboBoxItem(""));
            rcbWorkingAccAZ.AppendDataBoundItems = true;
            rcbWorkingAccAZ.DataValueField = "AccountCode";
            rcbWorkingAccAZ.DataTextField = "AccountCode";
            rcbWorkingAccAZ.DataSource = workingAcc;
            rcbWorkingAccAZ.DataBind();
        }
       
        //protected void rcbCategoryCode_SelectIndexChange(object sender, EventArgs e)
        //{
        //    lblCategoryAZ.Text = rcbCategoryCode.Text;
        //}

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "commit":
                    if (CommitArrearSavingAccount())
                    {
                        Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                    }                  
                    break;
                case "preview":
                    string[] param = new string[1];
                    param[0] = "From=Arrear";
                    Response.Redirect(EditUrl("", "", "SavingAccReviewList", param));
                    break;
                case "authorize":
                    SavingAccountDAO.ApproveArrearSavingAccount(tbDepositCode.Text, UserInfo.Username);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                    break;
                case "reverse":
                    SavingAccountDAO.ReverseArrearSavingAccount(tbDepositCode.Text, UserInfo.Username);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                    break;
                case "print":
                    PrintSavingAccDocument();
                    break;
            }

        }        

        protected void btSearch_Click(object sender, EventArgs e)
        {
            BindDataToControl(tbDepositCode.Text);
        }

        #endregion

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
                    if (!string.IsNullOrEmpty(RefIdToReview))
                         BindDataToControl(RefIdToReview);
                    break;
            }
        }

        private void GenerateDefaultData()
        {
            dtpValueDateAZ.SelectedDate = DateTime.Now;
            TTtbPaymentNo.Text = SQLData.B_BMACODE_GetNewID("PERIODIC_LOAN", "TT", "/");
            TTlblAcctNo.Text = string.Format("{0}*{1}*BO", tbDepositCode.Text.Replace(".", ""), DateTime.Now.ToString("yyyyMMdd"));
            TTtbTeller.Text = UserInfo.Username;
        }

        private void GenerateDepositeCode()
        {
            tbDepositCode.Text = SavingAccountDAO.GenerateDepositeCode(SavingAccFunc.ARREAR);            
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
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    //RadToolBar1.FindItemByValue("btSearch").Enabled = true;
                    break;
            }
        }

        private void LoadDataForDropdowns()
        {
            var customers = SavingAccountDAO.GetAllAuthorisedCustomer();
            rcbCustomerID.DataSource = customers;
            rcbCustomerID.DataTextField = "CustomerName";
            rcbCustomerID.DataValueField = "CustomerID";
            rcbCustomerID.AppendDataBoundItems = true;
            foreach (DataRow r in customers.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem(r["CustomerName"].ToString(),r["CustomerID"].ToString());
                item.Attributes.Add("GBStreet", r["GBStreet"].ToString());
                item.Attributes.Add("DocID", r["DocID"].ToString());
                item.Attributes.Add("DocIssueDate", (r["DocIssueDate"].ToString() == "") ? string.Empty : ((DateTime)r["DocIssueDate"]).ToString("dd/MM/yyyy"));
                rcbCustomerID.Items.Add(item);
            }
            //rcbCustomerID.DataBind();

            rcbJointHolderID.Items.Clear();
            rcbJointHolderID.Items.Add(new RadComboBoxItem(""));
            rcbJointHolderID.DataSource = customers;
            rcbJointHolderID.DataTextField = "CustomerName";
            rcbJointHolderID.DataValueField = "CustomerID";
            rcbJointHolderID.DataBind();

            rcbRelationCode.Items.Clear();
            rcbRelationCode.Items.Add(new RadComboBoxItem(""));
            rcbRelationCode.DataSource = DataProvider.DataTam.BRELATIONCODE_GetAll(); 
            rcbRelationCode.DataTextField = "NAME";
            rcbRelationCode.DataValueField = "CODE";
            rcbRelationCode.DataBind();

            rcbProductLine.Items.Clear();
            rcbProductLine.Items.Add(new RadComboBoxItem(""));
            rcbProductLine.DataSource = SavingAccountDAO.GetAllProductLinesForSavingAcc();
            rcbProductLine.DataTextField = "Description";
            rcbProductLine.DataValueField = "ProductID";
            rcbProductLine.DataBind();

            rcbAccOfficer.Items.Clear();
            rcbAccOfficer.Items.Add(new RadComboBoxItem(""));
            rcbAccOfficer.DataSource = SQLData.B_BACCOUNTOFFICER_GetAll();
            rcbAccOfficer.DataValueField = "Code";
            rcbAccOfficer.DataTextField = "Description";
            rcbAccOfficer.DataBind();

            rcbCategoryCode.DataSource = SavingAccountDAO.GetAllCategoryForSavingAcc();
            rcbCategoryCode.DataValueField = "Code";
            rcbCategoryCode.DataTextField = "FormatedName";
            rcbCategoryCode.DataBind();

            var currentcys = SavingAccountDAO.GetAllCurrency();
            rcbCurrentcy.DataValueField = "Code";
            rcbCurrentcy.DataTextField = "Code";
            rcbCurrentcy.DataSource = currentcys;
            rcbCurrentcy.DataBind();

            rcbProductAZ.DataValueField = "ProductCode";
            rcbProductAZ.DataTextField = "Description";
            rcbProductAZ.DataSource = SavingAccountDAO.GetAllProductType("arrear");
            rcbProductAZ.DataBind();

            TTrcbPaymentCcy.DataValueField = "Code";
            TTrcbPaymentCcy.DataTextField = "Code";
            TTrcbPaymentCcy.DataSource = currentcys;
            TTrcbPaymentCcy.DataBind();

            radTermAZ.DataValueField = "InterestTerm";
            radTermAZ.DataTextField = "Description";
            radTermAZ.DataSource = SavingAccountDAO.GetAllTerm();
            radTermAZ.DataBind();       
        }

        private bool CommitArrearSavingAccount()
        {
            var arrearSavingAccount = new ArrearSavingAccount();
            BuildArrearSavingAccount(arrearSavingAccount);
            if (SavingAccountDAO.CheckArrearSavingAccountExist(arrearSavingAccount.RefId))
            {
                arrearSavingAccount.UpdatedBy = this.UserInfo.Username;
                return SavingAccountDAO.UpdateArrearSavingAccount(arrearSavingAccount);
            }
            else
            {
                arrearSavingAccount.CreatedBy = this.UserInfo.Username;
                return SavingAccountDAO.CreateNewArrearSavingAccount(arrearSavingAccount);
            }
            //Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
            //AutoLoadExtraInformationForTT(arrearSavingAccount);
            //StatusCommit.Value = 1.ToString();
        }

        private void BuildArrearSavingAccount(ArrearSavingAccount arrearSavingAccount)
        {
            arrearSavingAccount.RefId = tbDepositCode.Text;
            arrearSavingAccount.Status = AuthoriseStatus.UNA.ToString();
            arrearSavingAccount.CustomerId = rcbCustomerID.SelectedValue;
            arrearSavingAccount.CustomerName = rcbCustomerID.SelectedItem.Text.Replace(arrearSavingAccount.CustomerId + " - ", "");
            arrearSavingAccount.AccCategory = rcbCategoryCode.SelectedValue;
            arrearSavingAccount.AccTitle = tbAccountName.Text;
            arrearSavingAccount.ShortTitle = tbShortName.Text;
            arrearSavingAccount.Currency = rcbCurrentcy.SelectedValue;
            arrearSavingAccount.ProductLineId = rcbProductLine.SelectedValue;
            arrearSavingAccount.JointACHolderId = rcbJointHolderID.SelectedValue;
            arrearSavingAccount.JointACHolderName = rcbJointHolderID.SelectedItem.Text.Replace(arrearSavingAccount.JointACHolderId + " - ", "");
            arrearSavingAccount.RelationshipId = rcbRelationCode.SelectedValue;
            arrearSavingAccount.RelationshipName = rcbRelationCode.SelectedItem.Text.Replace(arrearSavingAccount.RelationshipId + " - ", "");
            arrearSavingAccount.Note = tbNotes.Text;
            arrearSavingAccount.AccountOfferCode = rcbAccOfficer.SelectedValue;
            arrearSavingAccount.AZProductCode = rcbProductAZ.SelectedValue;
            arrearSavingAccount.AZPrincipal = radNumPrincipalAZ.Value.HasValue ? (decimal)radNumPrincipalAZ.Value : 0;
            arrearSavingAccount.AZValueDate = dtpValueDateAZ.SelectedDate;
            arrearSavingAccount.AZTerm = radTermAZ.SelectedValue;
            arrearSavingAccount.AZOriginalMaturityDate = dtpMaturityDateAZ.SelectedDate;
            arrearSavingAccount.AZInterestRate = tbInterestRateAZ.Value.HasValue ? (decimal)tbInterestRateAZ.Value : 0;
            arrearSavingAccount.AZWorkingAccount = rcbWorkingAccAZ.SelectedValue;
            arrearSavingAccount.AZMaturityInstr = lblMaturityInstrAZ.Text;
            arrearSavingAccount.AZRolloverPR = rcbRollOverPROnlyAZ.SelectedValue;
            arrearSavingAccount.TTNo = TTtbPaymentNo.Text;
            arrearSavingAccount.TTAccNo = TTlblAcctNo.Text;
            arrearSavingAccount.TTCurrency = TTrcbPaymentCcy.SelectedValue;
            arrearSavingAccount.TTForTeller = TTtbTeller.Text;
            arrearSavingAccount.TTDebitAccount = TTrcbDebitAmmount.SelectedValue;
            arrearSavingAccount.TTNarative = TTtbNarative.Text;
            arrearSavingAccount.TTDealRate = tbDealRate.Value.HasValue? (decimal?)tbDealRate.Value:null;
        }

        private void BindDataToControl(string refId)
        {
            var arrearLoadAccount = SavingAccountDAO.GetArrearSavingAccountById(refId);
            if (arrearLoadAccount == null)
            {
                return;
            }
            tbDepositCode.Text = arrearLoadAccount.RefId;
            rcbCustomerID.SelectedValue = arrearLoadAccount.CustomerId;
            rcbCustomerID.SelectedItem.Text = arrearLoadAccount.CustomerName;
            rcbCategoryCode.SelectedValue = arrearLoadAccount.AccCategory;
            tbAccountName.Text = arrearLoadAccount.AccTitle;
            tbShortName.Text = arrearLoadAccount.ShortTitle;
            rcbCurrentcy.SelectedValue = arrearLoadAccount.Currency;
            rcbProductLine.SelectedValue = arrearLoadAccount.ProductLineId;
            rcbJointHolderID.SelectedValue = arrearLoadAccount.JointACHolderId;
            rcbJointHolderID.SelectedItem.Text = arrearLoadAccount.JointACHolderName;
            rcbRelationCode.SelectedValue = arrearLoadAccount.RelationshipId;
            rcbRelationCode.SelectedItem.Text = arrearLoadAccount.RelationshipName;
            tbNotes.Text = arrearLoadAccount.Note;
            rcbAccOfficer.SelectedValue = arrearLoadAccount.AccountOfferCode;
            rcbProductAZ.SelectedValue = arrearLoadAccount.AZProductCode;
            radNumPrincipalAZ.Value = (double)arrearLoadAccount.AZPrincipal;
            dtpValueDateAZ.SelectedDate = arrearLoadAccount.AZValueDate;
            radTermAZ.SelectedValue = arrearLoadAccount.AZTerm;
            dtpMaturityDateAZ.SelectedDate = arrearLoadAccount.AZOriginalMaturityDate;
            tbInterestRateAZ.Value = (double)arrearLoadAccount.AZInterestRate;

            lblMaturityInstrAZ.Text = arrearLoadAccount.AZMaturityInstr;
            rcbRollOverPROnlyAZ.SelectedValue = arrearLoadAccount.AZRolloverPR;
            TTtbPaymentNo.Text = arrearLoadAccount.TTNo;
            TTrcbPaymentCcy.SelectedValue = arrearLoadAccount.TTCurrency;
            TTtbTeller.Text = arrearLoadAccount.TTForTeller;
            
            TTtbNarative.Text = arrearLoadAccount.TTNarative;

            var productType = SavingAccountDAO.GetProductTypeByCode(rcbProductAZ.SelectedValue);
            lblMaturityInstrAZ.Text = productType == null ? string.Empty : productType.MaturityInstr;

            rcbWorkingAccAZ.Items.Clear();
            rcbWorkingAccAZ.Items.Add(new RadComboBoxItem(""));
            rcbWorkingAccAZ.AppendDataBoundItems = true;
            rcbWorkingAccAZ.DataValueField = "AccountCode";
            rcbWorkingAccAZ.DataTextField = "AccountCode";
            rcbWorkingAccAZ.DataSource = SavingAccountDAO.GetWorkingBankAccountByCustomerIdForOpen(rcbCustomerID.SelectedValue,rcbCurrentcy.SelectedValue);
            rcbWorkingAccAZ.DataBind();
            rcbWorkingAccAZ.SelectedValue = arrearLoadAccount.AZWorkingAccount;

            lblCustomer.Text = rcbCustomerID.SelectedValue.ToString();
            CustomerIdAZ.Text = rcbCustomerID.Text;
            lblWokingAccNameAZ.Text = rcbCustomerID.Text;
            lblCategoryAZ.Text = rcbCategoryCode.Text;

            lblCurrencyAZ.Text = rcbCurrentcy.SelectedValue.ToString();
            TTlblAcctNo.Text = arrearLoadAccount.TTAccNo;
            TTtbPaymentNo.Text = arrearLoadAccount.TTNo;
            tbDealRate.Value = arrearLoadAccount.TTDealRate.HasValue ? (double?)arrearLoadAccount.TTDealRate : null;
            ////Audit Information
            //// TT            
            AutoLoadExtraInformationForTT(arrearLoadAccount);

            var debitAccount = SavingAccountDAO.GetInternalBankAccountByCurrency(arrearLoadAccount.Currency, arrearLoadAccount.CustomerId);

            TTrcbDebitAmmount.Items.Clear();
            TTrcbDebitAmmount.Items.Add(new RadComboBoxItem(""));
            TTrcbDebitAmmount.DataValueField = "Code";
            TTrcbDebitAmmount.DataTextField = "AccountTitle";
            TTrcbDebitAmmount.DataSource = debitAccount;
            TTrcbDebitAmmount.DataBind();
            TTrcbDebitAmmount.SelectedValue = arrearLoadAccount.TTDebitAccount;

            if (arrearLoadAccount.Status == AuthoriseStatus.AUT.ToString())
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            }
        }

        private void AutoLoadExtraInformationForTT(ArrearSavingAccount arrearLoadAccount)
        {
            //TTtbTeller.Text = arrearLoadAccount.TTForTeller;
            //TTrcbDebitAmmount.Text = arrearLoadAccount.TTDebitAccount;
            //lblAccountLcy.Text = arrearLoadAccount.Currency;
            //lblCurrency.Text = arrearLoadAccount.Currency;
            //TTlblCustomerID.Text = arrearLoadAccount.CustomerId;
            //lblAccountNo.Text = arrearLoadAccount.RefId;
            //lblAccountLcy.Text = string.Format("{0:##,#}", arrearLoadAccount.AZPrincipal);
            //lblAmountInLCY.Text = string.Format("{0:##,#}", arrearLoadAccount.AZPrincipal);
            //lblAmmount.Text = "Ammount = " + arrearLoadAccount.AZPrincipal;
            //lblDate.Text = "Date   = " + (arrearLoadAccount.AZValueDate.HasValue ? arrearLoadAccount.AZValueDate.Value.ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd"));
        }

        private void PrintSavingAccDocument()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");
            //Open template
            string docPath =Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/SavingAcc/SavingAccount.docx");
            //Open the template document
            Aspose.Words.Document document = new Aspose.Words.Document(docPath);
            //Execute the mail merge.
            var ds = PrepareData2Print();
            // Fill the fields in the document with user data.
            document.MailMerge.ExecuteWithRegions(ds.Tables["Info"]);
            document.MailMerge.ExecuteWithRegions(ds.Tables["Items"]); 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            document.Save("SavingAccount_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc,Aspose.Words.SaveType.OpenInBrowser, Response);
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

            var row = infoTb.NewRow();
            row["SaveingAccountType"] = "Tiết kiệm trả lãi cuối kỳ (Arrear)";
            row["Currency"] = rcbCurrentcy.SelectedValue;
            row["CustomerFullName"] = rcbCustomerID.SelectedItem.Text;
            row["CustomerAddress"] = rcbCustomerID.SelectedItem.Attributes["GBStreet"];
            row["CustomerDocId"] = rcbCustomerID.SelectedItem.Attributes["DocID"];
            row["IssuedDate"] = rcbCustomerID.SelectedItem.Attributes["DocIssueDate"];
            row["Term"] = radTermAZ.SelectedItem.Text;            
            row["InterestRate"] = tbInterestRateAZ.Text;
            row["AccIssuedDate"] = dtpValueDateAZ.SelectedDate.HasValue ? dtpValueDateAZ.SelectedDate.Value.ToString("dd/MM/yyyy") : ""; 
            row["CustomerId"] = rcbCustomerID.SelectedValue;
            row["AccDueDate"] = dtpValueDateAZ.SelectedDate.HasValue ? dtpMaturityDateAZ.SelectedDate.Value.ToString("dd/MM/yyyy") : ""; 
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
            row["Date"] = dtpValueDateAZ.SelectedDate.HasValue? dtpValueDateAZ.SelectedDate.Value.ToString("dd/MM/yyyy"):""; 
            row["TransactionCode"] = "GTV";
            row["Principle"] = radNumPrincipalAZ.DisplayText;
            row["Balance"] = radNumPrincipalAZ.DisplayText;
            row["InterestRate"] = tbInterestRateAZ.DisplayText;
            row["MaturityDate"] = dtpMaturityDateAZ.SelectedDate.HasValue? dtpMaturityDateAZ.SelectedDate.Value.ToString("dd/MM/yyyy"):"";
            itemTb.Rows.Add(row);
            //row = itemTb.NewRow();
            //row["Date"] = DateTime.Now.ToShortDateString();
            //row["TransactionCode"] = "GTV";
            //row["Principle"] = radNumPrincipalAZ.Text;
            //row["Balance"] = radNumPrincipalAZ.Value;
            //row["InterestRate"] = tbInterestRateAZ.Text;
            //row["MaturityDate"] = dtpMaturityDateAZ.SelectedDate.Value.ToShortDateString();

            //itemTb.Rows.Add(row);

            ds.Tables.Add(itemTb);

            return ds;
        }

        #endregion
    }
}