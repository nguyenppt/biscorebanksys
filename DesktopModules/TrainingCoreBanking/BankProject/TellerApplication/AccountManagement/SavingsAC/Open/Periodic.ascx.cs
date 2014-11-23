using BankProject.DataProvider;
using BankProject.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BankProject.Entity.SavingAcc;
using BankProject.Entity;
using System.Configuration;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Open
{
    public partial class Periodic : DotNetNuke.Entities.Modules.PortalModuleBase
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
            tbDepositCode.Text = SavingAccountDAO.GenerateDepositeCode(SavingAccFunc.PERIODIC);
        }

        private void LoadDataForDropdowns()
        {
            //Customer ID
            var customers = SavingAccountDAO.GetAllAuthorisedCustomer();
            rcbCustomerID.DataSource = customers;
            rcbCustomerID.DataTextField = "CustomerName";
            rcbCustomerID.DataValueField = "CustomerID";
            foreach (DataRow r in customers.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem(r["CustomerName"].ToString(), r["CustomerID"].ToString());
                item.Attributes.Add("GBStreet", r["GBStreet"].ToString());
                item.Attributes.Add("DocID", r["DocID"].ToString());
                item.Attributes.Add("DocIssueDate", (r["DocIssueDate"].ToString() == "") ? string.Empty : ((DateTime)r["DocIssueDate"]).ToString("dd/MM/yyyy"));
                rcbCustomerID.Items.Add(item);
            }
            //Category
            rcbCategoryCode.DataSource = SavingAccountDAO.GetAllCategoryForSavingAcc();
            rcbCategoryCode.DataValueField = "Code";
            rcbCategoryCode.DataTextField = "FormatedName";
            rcbCategoryCode.DataBind();
            //Currency 
            var currentcys = SavingAccountDAO.GetAllCurrency();
            rcbCurrentcy.DataValueField = "Code";
            rcbCurrentcy.DataTextField = "Code";
            rcbCurrentcy.DataSource = currentcys;
            rcbCurrentcy.DataBind();


            //Account Officer
            rcbAccOfficer.Items.Clear();
            rcbAccOfficer.Items.Add(new RadComboBoxItem(""));
            rcbAccOfficer.DataSource = SQLData.B_BACCOUNTOFFICER_GetAll();
            rcbAccOfficer.DataValueField = "Code";
            rcbAccOfficer.DataTextField = "Description";
            rcbAccOfficer.DataBind();
            //rcbAccOfficer.Text = UserInfo.DisplayName

            //Product Line 
            rcbProductLine.DataSource = SavingAccountDAO.GetAllProductLinesForSavingAcc();
            rcbProductLine.DataTextField = "Description";
            rcbProductLine.DataValueField = "ProductID";
            rcbProductLine.DataBind();
            //Joint A/c Holder
            rcbJointHolderID.Items.Clear();
            rcbJointHolderID.Items.Add(new RadComboBoxItem(""));
            rcbJointHolderID.DataSource = customers;
            rcbJointHolderID.DataTextField = "CustomerName";
            rcbJointHolderID.DataValueField = "CustomerID";
            rcbJointHolderID.DataBind();
            //Relationship
            rcbRelationCode.Items.Clear();
            rcbRelationCode.Items.Add(new RadComboBoxItem(""));
            rcbRelationCode.DataSource = DataProvider.DataTam.BRELATIONCODE_GetAll();
            rcbRelationCode.DataTextField = "NAME";
            rcbRelationCode.DataValueField = "CODE";
            rcbRelationCode.DataBind();

            //Product 
            rcbProductAZ.DataValueField = "ProductCode";
            rcbProductAZ.DataTextField = "Description";
            rcbProductAZ.DataSource = SavingAccountDAO.GetAllProductType("periodic");
            rcbProductAZ.DataBind();

            //PaymentCc
            TTrcbPaymentCcy.DataValueField = "Code";
            TTrcbPaymentCcy.DataTextField = "Code";
            TTrcbPaymentCcy.DataSource = currentcys;
            TTrcbPaymentCcy.DataBind();

          
        }
        private bool CommitPeriodicSavingAccount()
        {
            var periodicSavingAccount = new PeriodicSavingAccount();
            BuildPeriodicSavingAccount(periodicSavingAccount);
            if (SavingAccountDAO.CheckPeriodicSavingAccountExist(periodicSavingAccount.RefId))
            {
                periodicSavingAccount.UpdatedBy = this.UserInfo.Username;
                return SavingAccountDAO.UpdatePeriodicSavingAccount(periodicSavingAccount);
            }
            else
            {
                periodicSavingAccount.CreatedBy = this.UserInfo.Username;
                return SavingAccountDAO.CreateNewPeriodicSavingAccount(periodicSavingAccount);
            }           
        }

        private void BuildPeriodicSavingAccount(PeriodicSavingAccount periodicSavingAccount)
        {
            periodicSavingAccount.RefId = tbDepositCode.Text;
            periodicSavingAccount.Status = AuthoriseStatus.UNA.ToString();
            periodicSavingAccount.CustomerId = rcbCustomerID.SelectedValue;
            periodicSavingAccount.CustomerName = rcbCustomerID.SelectedItem.Text.Replace(periodicSavingAccount.CustomerId + " - ", "");
            periodicSavingAccount.AccCategory = rcbCategoryCode.SelectedValue;
            periodicSavingAccount.AccTitle = tbAccountName.Text;
            periodicSavingAccount.ShortTitle = tbShortName.Text;
            periodicSavingAccount.Currency = rcbCurrentcy.SelectedValue;
            periodicSavingAccount.ProductLineId = rcbProductLine.SelectedValue;
            periodicSavingAccount.JointACHolderId = rcbJointHolderID.SelectedValue;
            periodicSavingAccount.JointACHolderName = rcbJointHolderID.SelectedItem.Text.Replace(periodicSavingAccount.JointACHolderId + " - ", "");
            periodicSavingAccount.RelationshipId = rcbRelationCode.SelectedValue;
            periodicSavingAccount.RelationshipName = rcbRelationCode.SelectedItem.Text.Replace(periodicSavingAccount.RelationshipId + " - ", "");
            periodicSavingAccount.Note = tbNotes.Text;
            periodicSavingAccount.AccountOfferCode = rcbAccOfficer.SelectedValue;
            periodicSavingAccount.AZProductCode = rcbProductAZ.SelectedValue;
            periodicSavingAccount.AZPrincipal = radNumPrincipalAZ.Value.HasValue ? (decimal)radNumPrincipalAZ.Value : 0;
            periodicSavingAccount.AZValueDate = dtpValueDateAZ.SelectedDate;
            periodicSavingAccount.AZTerm = radTermAZ.SelectedValue;
            periodicSavingAccount.AZOriginalMaturityDate = dtpMaturityDateAZ.SelectedDate;
            periodicSavingAccount.AZInterestRate = tbInterestRateAZ.Value.HasValue ? (decimal)tbInterestRateAZ.Value : 0;
            periodicSavingAccount.AZWorkingAccount = rcbWorkingAccAZ.SelectedValue;
            periodicSavingAccount.AZMaturityInstr = lblMaturityInstrAZ.Text;
            periodicSavingAccount.AZIsSchedule = rcbSchedules.SelectedValue;
            periodicSavingAccount.AZScheduleType = rcbSchTypeAZ.SelectedValue;
            periodicSavingAccount.AZFrequency = tbFrequencyAZ.Text;
            periodicSavingAccount.TTNo = TTtbPaymentNo.Text;
            periodicSavingAccount.TTAccNo = TTlblAcctNo.Text;
            periodicSavingAccount.TTCurrency = TTrcbPaymentCcy.SelectedValue;
            periodicSavingAccount.TTForTeller = TTtbTeller.Text;
            periodicSavingAccount.TTDebitAccount = TTrcbDebitAmmount.SelectedValue;
            periodicSavingAccount.TTNarative = TTtbNarative.Text;
            periodicSavingAccount.TTDealRate = (decimal?)tbDealRate.Value;
        }

        private void BindDataToControl(string refId)
        {
            var periodicLoadAccount = SavingAccountDAO.GetPeriodicSavingAccountById(refId);
            if (periodicLoadAccount == null)
            {
                return;
            }

            tbDepositCode.Text = periodicLoadAccount.RefId;
            rcbCustomerID.SelectedValue = periodicLoadAccount.CustomerId;
            rcbCustomerID.SelectedItem.Text = periodicLoadAccount.CustomerName;
            rcbCategoryCode.SelectedValue = periodicLoadAccount.AccCategory;
            tbAccountName.Text = periodicLoadAccount.AccTitle;
            tbShortName.Text = periodicLoadAccount.ShortTitle;
            rcbCurrentcy.SelectedValue = periodicLoadAccount.Currency;
            rcbProductLine.SelectedValue = periodicLoadAccount.ProductLineId;
            rcbJointHolderID.SelectedValue = periodicLoadAccount.JointACHolderId;
            rcbJointHolderID.SelectedItem.Text = periodicLoadAccount.JointACHolderName;
            rcbRelationCode.SelectedValue = periodicLoadAccount.RelationshipId;
            rcbRelationCode.SelectedItem.Text = periodicLoadAccount.RelationshipName;
            tbNotes.Text = periodicLoadAccount.Note;
            rcbAccOfficer.SelectedValue = periodicLoadAccount.AccountOfferCode;
            rcbProductAZ.SelectedValue = periodicLoadAccount.AZProductCode;
            radNumPrincipalAZ.Value = (double)periodicLoadAccount.AZPrincipal;
            dtpValueDateAZ.SelectedDate = periodicLoadAccount.AZValueDate;
         
            dtpMaturityDateAZ.SelectedDate = periodicLoadAccount.AZOriginalMaturityDate;
            tbInterestRateAZ.Value = (double)periodicLoadAccount.AZInterestRate;

            lblMaturityInstrAZ.Text = periodicLoadAccount.AZMaturityInstr;
            //rcbRollOverPROnlyAZ.SelectedValue = periodicLoadAccount.AZRolloverPR;
            TTtbPaymentNo.Text = periodicLoadAccount.TTNo;
            TTrcbPaymentCcy.SelectedValue = periodicLoadAccount.TTCurrency;
            TTtbTeller.Text = periodicLoadAccount.TTForTeller;
            
            TTtbNarative.Text = periodicLoadAccount.TTNarative;

            var productType = SavingAccountDAO.GetProductTypeByCode(rcbProductAZ.SelectedValue);
            lblMaturityInstrAZ.Text = productType == null ? string.Empty : productType.MaturityInstr;
         
            rcbWorkingAccAZ.Items.Clear();
            rcbWorkingAccAZ.Items.Add(new RadComboBoxItem(""));
            rcbWorkingAccAZ.AppendDataBoundItems = true;
            rcbWorkingAccAZ.DataValueField = "AccountCode";
            rcbWorkingAccAZ.DataTextField = "AccountCode";
            rcbWorkingAccAZ.DataSource = SavingAccountDAO.GetWorkingBankAccountByCustomerIdForOpen(rcbCustomerID.SelectedValue,rcbCurrentcy.SelectedValue); ;
            rcbWorkingAccAZ.DataBind();
            rcbWorkingAccAZ.SelectedValue = periodicLoadAccount.AZWorkingAccount;

            lblCustomer.Text = rcbCustomerID.SelectedValue.ToString();
            CustomerIdAZ.Text = rcbCustomerID.Text;
            lblWokingAccNameAZ.Text = rcbCustomerID.Text;
            lblCategoryAZ.Text = rcbCategoryCode.Text;

            lblCurrencyAZ.Text = rcbCurrentcy.SelectedValue.ToString();
            TTlblAcctNo.Text = periodicLoadAccount.TTAccNo;
            TTtbPaymentNo.Text = periodicLoadAccount.TTNo;
            tbFrequencyAZ.Text = periodicLoadAccount.AZFrequency;
            tbDealRate.Value = periodicLoadAccount.TTDealRate.HasValue ? (double?)periodicLoadAccount.TTDealRate : null;
            ////Audit Information
            var debitAccount = SavingAccountDAO.GetInternalBankAccountByCurrency(periodicLoadAccount.Currency, periodicLoadAccount.CustomerId);

            TTrcbDebitAmmount.Items.Clear();
            TTrcbDebitAmmount.Items.Add(new RadComboBoxItem(""));
            TTrcbDebitAmmount.DataValueField = "Code";
            TTrcbDebitAmmount.DataTextField = "AccountTitle";
            TTrcbDebitAmmount.DataSource = debitAccount;
            TTrcbDebitAmmount.DataBind();
            TTrcbDebitAmmount.SelectedValue = periodicLoadAccount.TTDebitAccount;

            var terms = SavingAccountDAO.GetAllTerm();
            terms = terms.Where(r => r.InterestTerm.ToUpper().IndexOf("M") > 0).ToList();
            if (rcbProductAZ.SelectedValue == "300")
            {
                terms = terms.Where(r => Convert.ToInt32(r.InterestTerm.ToUpper().Replace("M", "")) % 3 == 0).ToList();

            }
            radTermAZ.Items.Clear();
            radTermAZ.Items.Add(new RadComboBoxItem(""));
            radTermAZ.DataValueField = "InterestTerm";
            radTermAZ.DataTextField = "Description";
            radTermAZ.DataSource = terms;
            radTermAZ.DataBind();

            radTermAZ.SelectedValue = periodicLoadAccount.AZTerm;


            if (periodicLoadAccount.Status == AuthoriseStatus.AUT.ToString())
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            }
            if (periodicLoadAccount.Status != AuthoriseStatus.AUT.ToString())
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
        private void ComFrequency()
        {
            var product = rcbProductAZ.SelectedValue;
            var frequency = string.Empty;
            if (dtpValueDateAZ.SelectedDate.HasValue)
            {
                var date = dtpValueDateAZ.SelectedDate.Value;
                if (product == "200")
                {
                    date = date.AddMonths(1);
                    frequency = string.Format("{0}{1}{2}M01{3}", date.Year, date.Month.ToString("D2"), date.Day.ToString("D2"), date.Day.ToString("D2"));
                }
                else if (product == "300")
                {
                    date = date.AddMonths(3);
                    frequency = string.Format("{0}{1}{2}M03{3}", date.Year, date.Month.ToString("D2"), date.Day.ToString("D2"), date.Day.ToString("D2"));
                }

            }
            tbFrequencyAZ.Text = frequency;
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
        #endregion

        #region Event 
        protected void rcbCustomerID_SelectIndexChange(object sender, EventArgs e)
        {
            lblCustomer.Text = rcbCustomerID.SelectedValue.ToString();
            CustomerIdAZ.Text = rcbCustomerID.Text;
            lblWokingAccNameAZ.Text = rcbCustomerID.Text;

            var workingAcc = SavingAccountDAO.GetWorkingBankAccountByCustomerIdForOpen(rcbCustomerID.SelectedValue,rcbCurrentcy.SelectedValue);
            rcbWorkingAccAZ.Items.Clear();
            rcbWorkingAccAZ.Items.Add(new RadComboBoxItem(""));
            rcbWorkingAccAZ.AppendDataBoundItems = true;
            rcbWorkingAccAZ.DataValueField = "AccountCode";
            rcbWorkingAccAZ.DataTextField = "AccountCode";
            rcbWorkingAccAZ.DataSource = workingAcc;
            rcbWorkingAccAZ.DataBind();

         

        }
        
        protected void rcbProductAZ_SelectIndexChange(object sender, EventArgs e)
        {
            var productType = SavingAccountDAO.GetProductTypeByCode(rcbProductAZ.SelectedValue);
            lblMaturityInstrAZ.Text = productType == null ? string.Empty : productType.MaturityInstr;

            var terms = SavingAccountDAO.GetAllTerm();
            terms = terms.Where(r => r.InterestTerm.ToUpper().IndexOf("M") > 0).ToList();
            if (rcbProductAZ.SelectedValue == "300")
            {
                terms = terms.Where(r => Convert.ToInt32(r.InterestTerm.ToUpper().Replace("M", "")) % 3 == 0).ToList();
               
            }            
            radTermAZ.Items.Clear();
            radTermAZ.Items.Add(new RadComboBoxItem(""));
            radTermAZ.DataValueField = "InterestTerm";
            radTermAZ.DataTextField = "Description";
            radTermAZ.DataSource = terms;
            radTermAZ.DataBind();
            ComFrequency();
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

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "commit":
                    if (CommitPeriodicSavingAccount())
                    {
                        Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                    }                   
                    break;
                case "preview":
                    string[] param = new string[1];
                    param[0] = "From=Periodic";
                    Response.Redirect(EditUrl("", "", "SavingAccReviewList", param));
                    break;
                case "authorize":
                    SavingAccountDAO.ApprovePeriodicSavingAccount(tbDepositCode.Text, UserInfo.Username);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                    break;
                case "reverse":
                    SavingAccountDAO.ReversePeriodicSavingAccount(tbDepositCode.Text, UserInfo.Username);
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

            var row = infoTb.NewRow();
            if (rcbProductLine.SelectedValue == "200")
            {
                row["SaveingAccountType"] = "Tiết kiệm trả lãi hàng tháng (Periodic - Monthly)";
            }
            else
            {
                row["SaveingAccountType"] = "Tiết kiệm trả lãi hàng tháng (Periodic - Quarterly)";
            }
            row["Currency"] = rcbCurrentcy.SelectedValue;
            row["CustomerFullName"] = rcbCustomerID.SelectedItem.Text;
            row["CustomerAddress"] = rcbCustomerID.SelectedItem.Attributes["GBStreet"];
            row["CustomerDocId"] = rcbCustomerID.SelectedItem.Attributes["DocID"];
            row["IssuedDate"] = rcbCustomerID.SelectedItem.Attributes["DocIssueDate"];
            row["Term"] = radTermAZ.SelectedItem.Text;            
            row["InterestRate"] = tbInterestRateAZ.Text;
            row["AccIssuedDate"] = dtpValueDateAZ.SelectedDate.HasValue? dtpValueDateAZ.SelectedDate.Value.ToString("dd/MM/yyyy") : ""; 
            row["CustomerId"] = rcbCustomerID.SelectedValue;
            row["AccDueDate"] = dtpValueDateAZ.SelectedDate.HasValue? dtpMaturityDateAZ.SelectedDate.Value.ToString("dd/MM/yyyy"):""; 
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