using BankProject.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using BankProject.Entity;
using BankProject.Entity.SavingAcc;
using System.Globalization;

namespace BankProject.TellerApplication.AccountManagement.CurrentNonTermSavingAC.SalaryPayment
{
    public partial class PaymentFrequency : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        #region Property

        static private  Dictionary<string,DataTable> DataInfor = new Dictionary<string, DataTable>();


        private SavingAccountDAO SavingAccountDAO
        {
            get
            {
                return new SavingAccountDAO();
            }
        }

        private string NonFrequency
        {
            get
            {
                if (Request.QueryString["tabid"] == "138")
                    return "NO";                
                return "YES";
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

        #region private methode
        private void GenerateDepositeCode()
        {
            tbDepositCode.Text = SavingAccountDAO.B_BMACODE_SalaryPayment(); 
        }
        private void LoadToolBar()
        {
            switch (Mode)
            {
                case "preview":
                    RadToolBar1.FindItemByValue("btnAuthorize").Enabled = true;
                    RadToolBar1.FindItemByValue("btnReverse").Enabled = true;
                    RadToolBar1.FindItemByValue("btnPrint").Enabled = true;
                    break;
                default:
                    RadToolBar1.FindItemByValue("btnCommit").Enabled = true;
                    RadToolBar1.FindItemByValue("btnPreview").Enabled = true;
                    //RadToolBar1.FindItemByValue("btSearch").Enabled = true;
                    break;
            }
        }
        private void LoadDataForDropdowns()
        {
            rcbAccountPayment.Items.Clear();
            rcbAccountPayment.Items.Add(new RadComboBoxItem(""));
            rcbAccountPayment.AppendDataBoundItems = true;
            rcbAccountPayment.DataValueField = "ID";
            rcbAccountPayment.DataTextField = "Title";
            var listAcc = new SavingAccountDAO().GetAccountOpenByType("C");
            foreach (AccountOpen acc in listAcc)
            {
                RadComboBoxItem item = new RadComboBoxItem(acc.AccountCode + " - "+ acc.CustomerName,acc.ID);                
                item.Attributes.Add("CustomerName", acc.CustomerName);
                item.Attributes.Add("Currency", acc.Currency);
                item.Attributes.Add("CustomerID", acc.CustomerID);
                item.Attributes.Add("ActualBallance", acc.ActualBallance.ToString());
                rcbAccountPayment.Items.Add(item);
            }

            rcbTerm.Items.Clear();
            rcbTerm.Items.Add(new RadComboBoxItem(""));
            rcbTerm.AppendDataBoundItems = true;
            rcbTerm.DataValueField = "Code";
            rcbTerm.DataTextField = "Code";
            rcbTerm.DataSource = SavingAccountDAO.GetSalaryPaymentFrequencyTerm();
            rcbTerm.DataBind();

            rcbCreditAccount.Items.Clear();
            rcbCreditAccount.Items.Add(new RadComboBoxItem(""));
            rcbCreditAccount.AppendDataBoundItems = true;
            rcbCreditAccount.DataValueField = "AccountCode";
            rcbCreditAccount.DataTextField = "Title";
            rcbCreditAccount.DataSource = SavingAccountDAO.GetAccountOpenByType("P");
            rcbCreditAccount.DataBind();


            rcbPaymentMethod.Items.Clear();
            rcbPaymentMethod.Items.Add(new RadComboBoxItem(""));
            rcbPaymentMethod.AppendDataBoundItems = true;
            rcbPaymentMethod.DataValueField = "Code";
            rcbPaymentMethod.DataTextField = "Code";
            rcbPaymentMethod.DataSource = new SavingAccountDAO().GetSalaryPaymentMethod();
            rcbPaymentMethod.DataBind();    
            
            //rcbAccountPayment.DataBind();
        }
        private DataTable BuildNewtable()
        {
            var infoTb = new DataTable("Info");            
            infoTb.Columns.Add("WorkingAccId");
            infoTb.Columns.Add("PaymentMethod");
            infoTb.Columns.Add("CreditAmount");
            infoTb.Columns.Add("CreditAccount");
            infoTb.Columns.Add("Exist");
            return infoTb;
        }
        private void UpdateDataInfo(DataTable infoTb)
        {
            if (DataInfor.ContainsKey(tbDepositCode.Text))
            {
                DataInfor.Remove(tbDepositCode.Text);
            }
            DataInfor.Add(tbDepositCode.Text, infoTb);
        }
        private DataTable GetDataInfo()
        {
            if (DataInfor.ContainsKey(tbDepositCode.Text))
            {
                return DataInfor[tbDepositCode.Text];
            }
            return BuildNewtable();
        }
        private DataTable BuildDataTableFromListObj(IList<SalaryPaymentFrequencyDetail> lobj)
        {

            var infoTb = BuildNewtable();
            try
            {
                foreach (SalaryPaymentFrequencyDetail item in lobj)
                {                   
                    var row = infoTb.NewRow();
                    
                    row["WorkingAccId"] = item.WorkingAccId;
                    row["PaymentMethod"] = item.PaymentMethod;
                    row["CreditAmount"] = String.Format(CultureInfo.InvariantCulture,
                                            "{0:0,0}", item.CreditAmount);
                    row["CreditAccount"] = item.CreditAccount;
                    row["Exist"] = string.Empty;
                    infoTb.Rows.Add(row);
                }
                UpdateDataInfo(infoTb);
            }
            catch (Exception ex)
            {
                labelNoResults.Text = "Wrong Selected Company Payment file";
            }
            return infoTb;
        }
        private DataTable BuildDataTableFromFileExcel(System.IO.Stream dataStream)
        {

            var infoTb = BuildNewtable();
            try
            {
                Aspose.Cells.License license = new Aspose.Cells.License();
                license.SetLicense("Aspose.Cells.lic");
                Aspose.Cells.Workbook document = new Aspose.Cells.Workbook();
                document.Open(dataStream);
                if (document.Worksheets[0].Cells.Rows.Count <= 1) return infoTb;

                for (var i = 1; i < document.Worksheets[0].Cells.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(document.Worksheets[0].Cells[i, 0].Value.ToString())) continue;
                    var row = infoTb.NewRow();
                    
                    row["WorkingAccId"] = document.Worksheets[0].Cells[i, 3].Value;
                    row["PaymentMethod"] = document.Worksheets[0].Cells[i, 0].Value;
                    row["CreditAmount"] = String.Format(CultureInfo.InvariantCulture,
                                            "{0:0,0}", document.Worksheets[0].Cells[i, 5].Value);
                    row["CreditAccount"] = string.Format("{0}-{1}-{2}", document.Worksheets[0].Cells[i, 4].Value, document.Worksheets[0].Cells[i, 3].Value, document.Worksheets[0].Cells[i, 2].Value);
                    row["Exist"] = string.Empty;
                    infoTb.Rows.Add(row);
                }
                UpdateDataInfo(infoTb);
            }
            catch (Exception ex)
            {
                labelNoResults.Text = "Wrong Selected Company Payment file"; 
            }
            return infoTb;
        }
        private void CheckExistDpAccount()
        {
            CheckExist.Text = "ok";
            var tb = GetDataInfo();
            var cAcc = new SavingAccountDAO().GetAccountOpenByType("P");
            foreach (DataRow row in tb.Rows)
            {
                var test = cAcc.Where(r => r.AccountCode == row["WorkingAccId"].ToString()).FirstOrDefault();
                if (test == null)
                {
                    row["Exist"] = "Not exist";
                    CheckExist.Text = string.Empty;
                }
            }
            grdReviewList.DataSource = tb;
            grdReviewList.Rebind();
        }

        private void rebindData()
        {
            var tb = GetDataInfo();
            if (tb.Rows.Count > 0)
            {
                var cAcc = new SavingAccountDAO().GetAccountOpenByType("P");
                double sumAmount = 0;
                foreach(DataRow row in tb.Rows)
                {
                    string workingAccId = row["WorkingAccId"].ToString();
                    cAcc = cAcc.Where(r => r.AccountCode != workingAccId).ToList();
                    sumAmount = sumAmount + Convert.ToDouble(row["CreditAmount"].ToString());                    
                }
                rcbCreditAccount.Items.Clear();
                rcbCreditAccount.Items.Add(new RadComboBoxItem(""));
                rcbCreditAccount.AppendDataBoundItems = true;
                rcbCreditAccount.DataValueField = "AccountCode";
                rcbCreditAccount.DataTextField = "Title";
                rcbCreditAccount.DataSource = cAcc;
                rcbCreditAccount.DataBind();
                tbTotalDebitAmtShow.Value = sumAmount;
                if (string.IsNullOrEmpty(rcbAccountPayment.SelectedItem.Attributes["ActualBallance"]))
                {
                    tbTotalDebitAmt.Value = -1 * sumAmount;
                }
                else
                {
                    tbTotalDebitAmt.Value = Convert.ToDouble(rcbAccountPayment.SelectedItem.Attributes["ActualBallance"]) - sumAmount;
                }
                CheckExistDpAccount();
                if (!tbTotalDebitAmt.Value.HasValue || tbTotalDebitAmt.Value <= 0)
                    NotTotalDebitAmt.Text = string.Empty;
                else NotTotalDebitAmt.Text = "ok";
            }
        }

        private void BuildPaymentFrequency(SalaryPaymentFrequency paymentFrequency)
        {
            var accountPayment = rcbAccountPayment.SelectedItem;
            paymentFrequency.RefId = tbDepositCode.Text;
            paymentFrequency.OpenAccounId = accountPayment.Value;
            paymentFrequency.CustomerId = accountPayment.Attributes["CustomerId"];
            paymentFrequency.CustomerName = accountPayment.Attributes["CustomerName"];
            paymentFrequency.Currency = lblCurrency.Text;
            paymentFrequency.TotalDebitAmt = (decimal?)tbTotalDebitAmtShow.Value;
            if (NonFrequency != "YES")
            {
                paymentFrequency.Fequency = rdpFrequency.SelectedDate;
                paymentFrequency.Term = rcbTerm.SelectedValue;
            }            
            paymentFrequency.EndDate = rdpEndDate.SelectedDate;
            paymentFrequency.OrderingCust = tbOrderingCust.Text;
            paymentFrequency.NonFrequency = NonFrequency;

        }

        private List<SalaryPaymentFrequencyDetail> BuildListDetail()
        {
            var result = new List<SalaryPaymentFrequencyDetail>();
            var tb = GetDataInfo();
            foreach (DataRow row in tb.Rows)
            {
                result.Add(new SalaryPaymentFrequencyDetail
                {
                    WorkingAccId = row["WorkingAccId"].ToString(),
                    CreditAmount = string.IsNullOrEmpty(row["CreditAmount"].ToString()) ? null : (decimal?)Convert.ToDecimal(row["CreditAmount"].ToString()),
                    PaymentMethod = row["PaymentMethod"].ToString(),
                    CreditAccount = row["CreditAccount"].ToString()
                    
                });
            }
            return result;
        }

        private bool CommitPaymentFrequency()
        {
            var paymentFrequency = new SalaryPaymentFrequency();
            BuildPaymentFrequency(paymentFrequency);
            if (SavingAccountDAO.CheckSalaryPaymentFrequencyExist(paymentFrequency.RefId))
            {
                paymentFrequency.UpdatedBy = this.UserInfo.Username;
                return SavingAccountDAO.UpdateSalaryPaymentFrequency(paymentFrequency, BuildListDetail());
            }
            else
            {
                paymentFrequency.CreatedBy = this.UserInfo.Username;
                return SavingAccountDAO.CreateNewSalaryPaymentFrequency(paymentFrequency, BuildListDetail());
            }
        }

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
                    if (!string.IsNullOrEmpty(RefIdToReview))
                        BindDataToControl(RefIdToReview);
                    break;
            }
        }
        private void BindDataToControl(string refId)
        {
            var salaryPaymentFrequency = SavingAccountDAO.GetSalaryPaymentFrequencyId(refId);
            if (salaryPaymentFrequency == null)
            {
                return;
            }
            var salaryPaymentFrequencyDetail = SavingAccountDAO.GetSalaryPaymentDetailFrequencyId(refId);
            tbDepositCode.Text = salaryPaymentFrequency.RefId;
            rcbAccountPayment.SelectedValue = salaryPaymentFrequency.OpenAccounId;
            lblCurrency.Text = salaryPaymentFrequency.Currency;
            tbTotalDebitAmtShow.Value = (double?)salaryPaymentFrequency.TotalDebitAmt;
            rdpFrequency.SelectedDate = salaryPaymentFrequency.Fequency;
            rcbTerm.SelectedValue = salaryPaymentFrequency.Term;
            rdpEndDate.SelectedDate = salaryPaymentFrequency.EndDate;
            tbOrderingCust.Text = salaryPaymentFrequency.OrderingCust;
            if (salaryPaymentFrequencyDetail.Count > 0)
            {
                BuildDataTableFromListObj(salaryPaymentFrequencyDetail);
                rebindData();
            }
            if (salaryPaymentFrequency.Status == AuthoriseStatus.AUT.ToString())
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            }
            if (string.IsNullOrEmpty(rcbAccountPayment.SelectedItem.Attributes["ActualBallance"]))
            {
                tbTotalDebitAmt.Value = (double?)(-1 * salaryPaymentFrequency.TotalDebitAmt);
            }
            else
            {
                tbTotalDebitAmt.Value = Convert.ToDouble(rcbAccountPayment.SelectedItem.Attributes["ActualBallance"]) - (double?)salaryPaymentFrequency.TotalDebitAmt;
            }
            
        }
        #endregion

        #region Event

        protected void btSearch_Click(object sender, EventArgs e)
        {
            BindDataToControl(tbDepositCode.Text);
        }

        protected void UpdateRow_Click(object sender, EventArgs e)
        {            
            var tb = GetDataInfo();
            DataRow rowUpdate = tb.AsEnumerable().Where(r => r.Field<string>("WorkingAccId") == CreditAccountForUpdate.Text).FirstOrDefault();
            if (rowUpdate != null)
            {

                //var rowteam = rowUpdate;
                rowUpdate["PaymentMethod"] = rcbPaymentMethod.SelectedValue;
                rowUpdate["CreditAmount"] = tbCreditAmount.DisplayText;                
                rowUpdate["WorkingAccId"] = rcbCreditAccount.SelectedValue;
                rowUpdate["CreditAccount"] = rcbCreditAccount.SelectedItem.Text;
                rowUpdate["Exist"] = "";
                //tb.Rows.Add(rowteam);
                // tb.Rows.Remove(rowUpdate);
                UpdateDataInfo(tb);
                rebindData();
                rcbPaymentMethod.SelectedValue = "";
                tbCreditAmount.Value = null;
                rcbCreditAccount.SelectedValue = "";
                CreditAccountForUpdate.Text = "";
            }
            else
            {
                var row = tb.NewRow();
                row["WorkingAccId"] = rcbCreditAccount.SelectedValue;
                row["PaymentMethod"] = rcbPaymentMethod.SelectedValue;
                row["CreditAmount"] = tbCreditAmount.DisplayText;
                row["CreditAccount"] = rcbCreditAccount.SelectedItem.Text;
                row["Exist"] = string.Empty;
                tb.Rows.Add(row);
                UpdateDataInfo(tb);
                rebindData();
                rcbPaymentMethod.SelectedValue = "";
                tbCreditAmount.Value = null;
                rcbCreditAccount.SelectedValue = "";
                CreditAccountForUpdate.Text = "";
            }
                   
        }
        
        protected void grdReviewList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            DataTable a = GetDataInfo();     
            switch (e.CommandName)
            {
                case "DeleteRow":
                             
                    a.Rows.RemoveAt((e.Item).ItemIndex);
                    rebindData();
                    break;
                case "EditRow":                   
                    var row  = a.Rows[(e.Item).ItemIndex];
                    if (CreditAccountForUpdate.Text != "" && CreditAccountForUpdate.Text == row["WorkingAccId"].ToString())
                    {
                        return;
                    }
                    var accopen = new SavingAccountDAO().GetAccountOpenByTypeAndCustomerID("P", row["WorkingAccId"].ToString());
                    if (accopen!=null){
                        rcbCreditAccount.Items.Add(new RadComboBoxItem(accopen.Title,accopen.AccountCode) );
                    }
                    rcbPaymentMethod.SelectedValue = row["PaymentMethod"].ToString();
                    tbCreditAmount.Value = Convert.ToDouble(row["CreditAmount"].ToString());
                    rcbCreditAccount.SelectedValue = row["WorkingAccId"].ToString();
                    CreditAccountForUpdate.Text = row["WorkingAccId"].ToString();
                    break;              
                default:
                        grdReviewList.DataSource = a;
                        grdReviewList.Rebind();
                        break;
            }          

        }
        
        protected void rcbAccountPayment_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            
            lblCurrency.Text = rcbAccountPayment.SelectedItem.Attributes["Currency"];
            tbTotalDebitAmt.Value = string.IsNullOrEmpty(rcbAccountPayment.SelectedItem.Attributes["ActualBallance"])?0:Convert.ToDouble(rcbAccountPayment.SelectedItem.Attributes["ActualBallance"]);

            rebindData();
            

        }

        protected void grdReviewList_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdReviewList.DataSource = BuildNewtable();
            
        }
      
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (ImportFileUpload.UploadedFiles.Count > 0 )
            {
                UploadedFile file = ImportFileUpload.UploadedFiles[0];
                if (file.GetExtension().ToLower() != ".xls") {
                    labelNoResults.Text = "Wrong Selected Company Payment file"; 
                    return;
                }
                if (file.GetName().ToLower().Replace(file.GetExtension().ToLower(),"") != rcbAccountPayment.SelectedItem.Text.ToLower())
                {
                    labelNoResults.Text = "Wrong Selected Company Payment file";
                    return;
                }
                labelNoResults.Text = "";
                var data  = BuildDataTableFromFileExcel(file.InputStream);
                rebindData();
            }           
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            //string normalLoan = tbNewNormalLoan.Text;
            var ToolBarButton = e.Item as RadToolBarButton;
            string commandName = ToolBarButton.CommandName;
            switch (commandName)
            {
                case "commit":
                    if (CommitPaymentFrequency())
                    {
                        Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                    }                  
                    break;
                case "Preview":             
                    Response.Redirect(EditUrl("", "", "list", new string[0]));
                    break;
                case "authorize":
                    SavingAccountDAO.ApproveSalaryPaymentFrequency(tbDepositCode.Text, UserInfo.Username);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                    break;
                case "reverse":
                    SavingAccountDAO.ReverseSalaryPaymentFrequency(tbDepositCode.Text, UserInfo.Username);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                    break;
                //case "search":
                //    break;
                default:                    
                    break;
            }

            //string[] param = new string[4];
            //param[0] = "NewNormalLoan=" + normalLoan;
            //Response.Redirect(EditUrl("", "", "fullview", param));
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (NonFrequency == "YES")
            {
                NonFequencyCheck.Visible = false;
                TermCheck.Visible = false;
            }
            if (IsPostBack) return;
            GenerateDepositeCode();
            LoadToolBar();
            LoadDataForDropdowns();
            rdpFrequency.SelectedDate = DateTime.Now;
            CheckExist.Text = "ok";
            LoadOrGenerateDefaultData();
            if (DisableForm)
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }
        }
        
       
       

        
    }
}