using BankProject.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close
{
    public partial class PreclosureWithDraw : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        #region Property

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
        public string TotalAmt
        {
            get
            {
                return Request.QueryString["TotalAmt"]??0.ToString();
            }
        }
        #endregion
        double totalAmt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();
            LoadDataForDropdowns();
            BindDataToControl();
                      
            hdfTotalAmt.Value = TotalAmt;
            tbTeller.Text = UserInfo.Username;
            lblTellerName.Text = UserInfo.DisplayName;
            
        }

        #region private methods
        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
        }

        private void LoadDataForDropdowns()
        {
            var currentcys = SavingAccountDAO.GetAllCurrency();
            cmbCreditCCY.DataValueField = "Code";
            cmbCreditCCY.DataTextField = "Code";
            cmbCreditCCY.DataSource = currentcys;
            cmbCreditCCY.DataBind();
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
            tbDepositCode.Text = RefIdToReview;
            lblTTPaymentNo.Text = arrearLoadAccount.TTNo;
            lblCustomer.Text = string.Format("{0} - {1}", arrearLoadAccount.CustomerId, arrearLoadAccount.CustomerName);;
            lblCurrency.Text = arrearLoadAccount.Currency;
            cmbCreditCCY.SelectedValue = arrearLoadAccount.Currency;
            cmbCreditAcc.SelectedValue = arrearLoadAccount.TTDebitAccount;
            cmbDrAccount.Items.Clear();
            cmbDrAccount.DataValueField = "AccountCode";
            cmbDrAccount.DataTextField = "AccountCode";
            //cmbDrAccount.DataSource = SavingAccountDAO.GetWorkingBankAccountByCustomerId(arrearLoadAccount.CustomerId);
            cmbDrAccount.DataBind();
            cmbDrAccount.SelectedValue = arrearLoadAccount.AZWorkingAccount;
            totalAmt += 50000000;           
            numAcmountLcy.Value = totalAmt;   


        }
        private void BindDataToControlFromPeriodic()
        {
            var periodicLoadAccount = SavingAccountDAO.GetPeriodicSavingAccountById(RefIdToReview);
            if (periodicLoadAccount == null)
            {
                return;
            }
            tbDepositCode.Text = RefIdToReview;
            lblTTPaymentNo.Text = periodicLoadAccount.TTNo;
            lblCustomer.Text = string.Format("{0} - {1}", periodicLoadAccount.CustomerId, periodicLoadAccount.CustomerName);;
            lblCurrency.Text = periodicLoadAccount.Currency;
            cmbCreditCCY.SelectedValue = periodicLoadAccount.Currency;
            cmbCreditAcc.SelectedValue = periodicLoadAccount.TTDebitAccount;
            cmbDrAccount.Items.Clear();
            cmbDrAccount.DataValueField = "AccountCode";
            cmbDrAccount.DataTextField = "AccountCode";
            //cmbDrAccount.DataSource = SavingAccountDAO.GetWorkingBankAccountByCustomerId(periodicLoadAccount.CustomerId);
            cmbDrAccount.DataBind();
            cmbDrAccount.SelectedValue = periodicLoadAccount.AZWorkingAccount;
            totalAmt += 50000000;
            numAcmountLcy.Value = totalAmt;   
        }


        #endregion

      
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "hold")
            {
                string[] param = new string[1];
                param[0] = "CodeID=" + tbDepositCode.Text;
                Response.Redirect(EditUrl("", "", "HOLD", param));
            }
        }
        
    }
}