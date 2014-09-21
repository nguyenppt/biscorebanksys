using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using BankProject.Entity;
using Telerik.Web.UI;
using System.Configuration;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close
{
    public partial class PreclosureInput : DotNetNuke.Entities.Modules.PortalModuleBase
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
        private string CurrencyCode = string.Empty;
        private decimal PrincipalValue = 0;
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

        #region private methods
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
            var currentcys = SavingAccountDAO.GetAllCurrency();
            WDcmbCreditCCY.DataValueField = "Code";
            WDcmbCreditCCY.DataTextField = "Code";
            WDcmbCreditCCY.DataSource = currentcys;
            WDcmbCreditCCY.DataBind();

            var customers = SavingAccountDAO.GetAllAuthorisedCustomer();         
            rcbCustomerID.AppendDataBoundItems = true;
            rcbCustomerID.Items.Add(new RadComboBoxItem());
            foreach (DataRow r in customers.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem(r["CustomerName"].ToString(), r["CustomerID"].ToString());
                item.Attributes.Add("GBStreet", r["GBStreet"].ToString());
                item.Attributes.Add("DocID", r["DocID"].ToString());
                item.Attributes.Add("DocIssueDate", (r["DocIssueDate"].ToString() == "") ? string.Empty : ((DateTime)r["DocIssueDate"]).ToString("dd/MM/yyyy"));
                rcbCustomerID.Items.Add(item);
            }

            radTermAZ.DataValueField = "InterestTerm";
            radTermAZ.DataTextField = "Description";
            radTermAZ.DataSource = SavingAccountDAO.GetAllTerm();
            radTermAZ.DataBind();      
        }

        private void BindDataToControl()
        {
            if (From == "arrear")
            {
                BindDataToControlFromArrear();
            }
            else if (From == "periodic")
            {
                BindDataToControlFromPeriodic();
            }
        }
        private void BindDataToControlFromArrear()
        {
            var arrearLoadAccount = SavingAccountDAO.GetArrearSavingAccountById(RefIdToReview);
            if (arrearLoadAccount == null)
            {
                return;
            }
            CurrencyCode = arrearLoadAccount.Currency;
            PrincipalValue = arrearLoadAccount.AZPrincipal;
            tbDepositCode.Text = RefIdToReview;
            lblCustomer.Text = string.Format("{0} - {1}", arrearLoadAccount.CustomerId, arrearLoadAccount.CustomerName);
            lblCategory.Text = arrearLoadAccount.AccCategory;
            lblCurrency.Text = arrearLoadAccount.Currency;
            lblProductCode.Text = arrearLoadAccount.AZProductCode;
            lblPrincipal.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:N}", arrearLoadAccount.AZPrincipal);        
            lblValueDate.Text = arrearLoadAccount.AZValueDate.HasValue? arrearLoadAccount.AZValueDate.Value.ToString("dd/MM/yyyy"): string.Empty;
            lblMaturityDate.Text = arrearLoadAccount.AZOriginalMaturityDate.HasValue ? arrearLoadAccount.AZOriginalMaturityDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            lblInterestedRate.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:N}", arrearLoadAccount.AZInterestRate);

            rcbWorkingAcc.Items.Clear();
            rcbWorkingAcc.DataValueField = "AccountCode";
            rcbWorkingAcc.DataTextField = "AccountCode";
            rcbWorkingAcc.DataSource = SavingAccountDAO.GetWorkingBankAccountByCustomerId(arrearLoadAccount.CustomerId,arrearLoadAccount.Currency);
            rcbWorkingAcc.DataBind();
            rcbWorkingAcc.SelectedValue = arrearLoadAccount.AZWorkingAccount;

            //View
            VWlblCustomer.Text = string.Format("{0} - {1}", arrearLoadAccount.CustomerId, arrearLoadAccount.CustomerName);
            VWlblCategory.Text = arrearLoadAccount.AccCategory;
            VWlblCurrency.Text = arrearLoadAccount.Currency;
            VWlblProductCode.Text = arrearLoadAccount.AZProductCode;
            
            VWlblOrgPrincipal.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:N}", arrearLoadAccount.AZPrincipal);
            VWlblOpenDate.Text = arrearLoadAccount.AZValueDate.HasValue ? arrearLoadAccount.AZValueDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            VWlblMaturityDate.Text = arrearLoadAccount.AZOriginalMaturityDate.HasValue ? arrearLoadAccount.AZOriginalMaturityDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            VWlblInterestedRate.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:N}", arrearLoadAccount.AZInterestRate);
        
            VWdtpValueDate.SelectedDate = arrearLoadAccount.CloseValueDate.HasValue ? arrearLoadAccount.CloseValueDate.Value :  DateTime.Now;

            //WidthDraw
            WDtbTeller.Text = UserInfo.Username;
            WDlblTellerName.Text = UserInfo.DisplayName;
        
            WDlblTTPaymentNo.Text = arrearLoadAccount.TTNo;
            WDlblCustomer.Text = string.Format("{0} - {1}", arrearLoadAccount.CustomerId, arrearLoadAccount.CustomerName); ;
            WDlblCurrency.Text = arrearLoadAccount.Currency;
            WDcmbCreditCCY.SelectedValue = arrearLoadAccount.Currency;
            WDcmbCreditAcc.SelectedValue = arrearLoadAccount.TTDebitAccount;
            WDcmbDrAccount.Items.Clear();
            WDcmbDrAccount.DataValueField = "AccountCode";
            WDcmbDrAccount.DataTextField = "AccountCode";
            WDcmbDrAccount.DataSource = SavingAccountDAO.GetWorkingBankAccountByCustomerId(arrearLoadAccount.CustomerId,arrearLoadAccount.Currency);
            WDcmbDrAccount.DataBind();
            WDcmbDrAccount.SelectedValue = arrearLoadAccount.AZWorkingAccount;
            WDtbDealRate.Value = arrearLoadAccount.TTDealRate.HasValue ? (double?)arrearLoadAccount.TTDealRate : null;
            WDnumAcmountLcy.Value = 0;


            // for print
            rcbCustomerID.SelectedValue = arrearLoadAccount.CustomerId;
            radTermAZ.SelectedValue = arrearLoadAccount.AZTerm;
            tbInterestRateAZ.Value = (double?)arrearLoadAccount.AZInterestRate;
            dtpValueDateAZ.SelectedDate = arrearLoadAccount.AZValueDate;
            dtpMaturityDateAZ.SelectedDate = arrearLoadAccount.AZOriginalMaturityDate;
            radNumPrincipalAZ.Value = (double?)arrearLoadAccount.AZPrincipal;
            if (arrearLoadAccount.CloseStatus == AuthoriseStatus.AUT.ToString())
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                RadToolBar1.FindItemByValue("btCommitData").Enabled = false;           
                RadToolBar1.FindItemByValue("btHold").Enabled = true;
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            }
        }
        private void BindDataToControlFromPeriodic()
        {
            var periodicLoadAccount = SavingAccountDAO.GetPeriodicSavingAccountById(RefIdToReview);
            if (periodicLoadAccount == null)
            {
                return;
            }
            CurrencyCode = periodicLoadAccount.Currency;
            PrincipalValue = periodicLoadAccount.AZPrincipal;
            tbDepositCode.Text = RefIdToReview;
            lblCustomer.Text = string.Format("{0} - {1}", periodicLoadAccount.CustomerId, periodicLoadAccount.CustomerName);
            lblCategory.Text = periodicLoadAccount.AccCategory;
            lblCurrency.Text = periodicLoadAccount.Currency;
            lblProductCode.Text = periodicLoadAccount.AZProductCode;
            lblPrincipal.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:N}", periodicLoadAccount.AZPrincipal);           
            lblValueDate.Text = periodicLoadAccount.AZValueDate.HasValue ? periodicLoadAccount.AZValueDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            lblMaturityDate.Text = periodicLoadAccount.AZOriginalMaturityDate.HasValue ? periodicLoadAccount.AZOriginalMaturityDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            lblInterestedRate.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:N}", periodicLoadAccount.AZInterestRate);


            rcbWorkingAcc.Items.Clear();
            rcbWorkingAcc.DataValueField = "AccountCode";
            rcbWorkingAcc.DataTextField = "AccountCode";
            rcbWorkingAcc.DataSource = SavingAccountDAO.GetWorkingBankAccountByCustomerId(periodicLoadAccount.CustomerId, periodicLoadAccount.Currency);
            rcbWorkingAcc.DataBind();
            rcbWorkingAcc.SelectedValue = periodicLoadAccount.AZWorkingAccount;

            // VIEW
            VWlblCustomer.Text = string.Format("{0} - {1}", periodicLoadAccount.CustomerId, periodicLoadAccount.CustomerName);
            VWlblCategory.Text = periodicLoadAccount.AccCategory;
            VWlblCurrency.Text = periodicLoadAccount.Currency;
            VWlblProductCode.Text = periodicLoadAccount.AZProductCode;
            VWlblOrgPrincipal.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:N}", periodicLoadAccount.AZPrincipal);
            VWlblOpenDate.Text = periodicLoadAccount.AZValueDate.HasValue ? periodicLoadAccount.AZValueDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            VWlblMaturityDate.Text = periodicLoadAccount.AZOriginalMaturityDate.HasValue ? periodicLoadAccount.AZOriginalMaturityDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            VWlblInterestedRate.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:N}", periodicLoadAccount.AZInterestRate);

            VWdtpValueDate.SelectedDate = periodicLoadAccount.CloseValueDate.HasValue ? periodicLoadAccount.CloseValueDate.Value : DateTime.Now;
            
            //WidthDraw
            WDtbTeller.Text = UserInfo.Username;
            WDlblTellerName.Text = UserInfo.DisplayName;
           
            WDlblTTPaymentNo.Text = periodicLoadAccount.TTNo;
            WDlblCustomer.Text = string.Format("{0} - {1}", periodicLoadAccount.CustomerId, periodicLoadAccount.CustomerName); ;
            WDlblCurrency.Text = periodicLoadAccount.Currency;
            WDcmbCreditCCY.SelectedValue = periodicLoadAccount.Currency;
            WDcmbCreditAcc.SelectedValue = periodicLoadAccount.TTDebitAccount;
            WDcmbDrAccount.Items.Clear();
            WDcmbDrAccount.DataValueField = "AccountCode";
            WDcmbDrAccount.DataTextField = "AccountCode";
            WDcmbDrAccount.DataSource = SavingAccountDAO.GetWorkingBankAccountByCustomerId(periodicLoadAccount.CustomerId,periodicLoadAccount.Currency);
            WDcmbDrAccount.DataBind();
            WDcmbDrAccount.SelectedValue = periodicLoadAccount.AZWorkingAccount;
            WDtbDealRate.Value = periodicLoadAccount.TTDealRate.HasValue? (double?)periodicLoadAccount.TTDealRate : null;
            WDnumAcmountLcy.Value = 0;

            // for print
            rcbCustomerID.SelectedValue = periodicLoadAccount.CustomerId;
            radTermAZ.SelectedValue = periodicLoadAccount.AZTerm;
            tbInterestRateAZ.Value = (double?)periodicLoadAccount.AZInterestRate;
            dtpValueDateAZ.SelectedDate = periodicLoadAccount.AZValueDate;
            dtpMaturityDateAZ.SelectedDate = periodicLoadAccount.AZOriginalMaturityDate;
            radNumPrincipalAZ.Value = (double?)periodicLoadAccount.AZPrincipal;

            if (periodicLoadAccount.CloseStatus == AuthoriseStatus.AUT.ToString())
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                RadToolBar1.FindItemByValue("btCommitData").Enabled = false;              
                RadToolBar1.FindItemByValue("btHold").Enabled = true;
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            }
        }

      
        #endregion
       
        #region Event

        protected void grdSavingAccInterestList_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var data = SavingAccountDAO.GetInteresAccountById(RefIdToReview);
            var total = data.AsEnumerable().Sum(r => r.Field<decimal?>("InterestAmt") ?? 0);
            VWlblTotalInAmt.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:N}", total);
            VWlblPrincipal.Text = String.Format(CultureInfo.InvariantCulture,
                              "{0:N}", Convert.ToDecimal(VWlblOrgPrincipal.Text) + total);

          
            if (CurrencyCode == "VND")
            {
                WDnumAcmountLcy.Value =(double?)(total + PrincipalValue);
                WDlblNewCustBal.Text = string.Format("- {0}",(total + PrincipalValue).ToString()) ;
                WDlblAmtPaidToCust.Text = (total + PrincipalValue).ToString();
                WDnumAcmountFcy.Value = null;
            }
            else
            {
                WDnumAcmountLcy.Value = null;
                WDnumAcmountFcy.Value = (double?)(total + PrincipalValue);
                WDlblNewCustBal.Text = string.Format("- {0}",(total + PrincipalValue).ToString()) ;
                WDlblAmtPaidToCust.Text = (total + PrincipalValue).ToString();
            }
            grdSavingAccInterestList.DataSource = data;
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "commit":
                    SavingAccountDAO.PeriodicArrearCommitClose(From, RefIdToReview, VWdtpValueDate.SelectedDate, WDnumAcmountLcy.Value, 
                        WDnumAcmountFcy.Value, WDtbNarative.Text, WDtbTeller.Text, WDcmbCreditAcc.Text);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}", TabId));
                    break;
                case "Preview":
                    string[] param = new string[1];
                    param[0] = "From=" + From;
                    Response.Redirect(EditUrl("", "", "CloseSavingAccReviewList", param));
                    break;
                case "authorize":
                    SavingAccountDAO.ApproveSavingAccount(tbDepositCode.Text, UserInfo.Username,From);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&ctl={1}&mid={2}", TabId, "CloseSavingAccReviewList", ModuleId));
                    break;
                case "reverse":
                    SavingAccountDAO.ReverseSavingAccount(tbDepositCode.Text, UserInfo.Username, From);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&ctl={1}&mid={2}", TabId, "CloseSavingAccReviewList", ModuleId));
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
        
        #endregion

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
            infoTb.Columns.Add("CustomerFullName");
            infoTb.Columns.Add("Currency");            
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
            infoTb.Columns.Add("SaveingAccountType");        /// added 20/9/2014   
            var row = infoTb.NewRow();
            if (From == "arrear")
            {
                row["SaveingAccountType"] = "Tiết kiệm trả lãi cuối kỳ (Arrear)";
            }
            else
            {
                if (lblProductCode.Text == "200")
                {
                    row["SaveingAccountType"] = "Tiết kiệm trả lãi hàng tháng (Periodic - Monthly)";
                }
                else
                {
                    row["SaveingAccountType"] = "Tiết kiệm trả lãi hàng tháng (Periodic - Quarterly)";
                }
            }
            row["Currency"] = lblCurrency.Text;
            row["CustomerFullName"] = rcbCustomerID.SelectedItem.Text;
            row["CustomerAddress"] = rcbCustomerID.SelectedItem.Attributes["GBStreet"];
            row["CustomerDocId"] = rcbCustomerID.SelectedItem.Attributes["DocID"];
            row["IssuedDate"] = rcbCustomerID.SelectedItem.Attributes["DocIssueDate"];
            row["Term"] = radTermAZ.SelectedItem.Text;
            row["IssuedDate"] = rcbCustomerID.SelectedItem.Attributes["DocIssueDate"];
            row["InterestRate"] = tbInterestRateAZ.Text;
            row["AccIssuedDate"] = dtpValueDateAZ.SelectedDate.Value.ToString("dd/MM/yyyy");
            row["CustomerId"] = rcbCustomerID.SelectedValue;
            row["AccDueDate"] = dtpMaturityDateAZ.SelectedDate.Value.ToShortDateString();
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
            row["Date"] = dtpValueDateAZ.SelectedDate.Value.ToString("dd/MM/yyyy"); 
            row["TransactionCode"] = "GTV";
            row["Principle"] = radNumPrincipalAZ.DisplayText;
            row["Balance"] = radNumPrincipalAZ.DisplayText;
            row["InterestRate"] = tbInterestRateAZ.DisplayText;
            row["MaturityDate"] = dtpMaturityDateAZ.SelectedDate.Value.ToShortDateString();
            itemTb.Rows.Add(row);
            row = itemTb.NewRow();
            row["Date"] = dtpValueDateAZ.SelectedDate.Value.ToShortDateString();
            row["TransactionCode"] = "RTV";
            row["Principle"] = radNumPrincipalAZ.DisplayText;
            if (lblCurrency.Text == "VND")
            {
                row["Balance"] = WDnumAcmountLcy.DisplayText;
            }
            else               
            {
                row["Balance"] = WDnumAcmountFcy.DisplayText;
            }
            row["InterestRate"] = tbInterestRateAZ.DisplayText;
            row["MaturityDate"] = VWdtpValueDate.SelectedDate.Value.ToShortDateString();

            itemTb.Rows.Add(row);

            ds.Tables.Add(itemTb);

            return ds;
        }
    }
}