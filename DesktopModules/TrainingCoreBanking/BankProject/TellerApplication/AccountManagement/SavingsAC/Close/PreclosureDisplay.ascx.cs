using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close
{
    public partial class PreclosureDisplay : DotNetNuke.Entities.Modules.PortalModuleBase
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();
            BindDataToControl();
        }

        #region private methods
        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
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
            
            lblCustomer.Text = string.Format("{0} - {1}", arrearLoadAccount.CustomerId, arrearLoadAccount.CustomerName);
            lblCategory.Text = arrearLoadAccount.AccCategory;
            lblCurrency.Text = arrearLoadAccount.Currency;
            lblProductCode.Text = arrearLoadAccount.AZProductCode;
            lblPrincipal.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:0,0.0}", arrearLoadAccount.AZPrincipal);
            lblOpenDate.Text = arrearLoadAccount.AZValueDate.HasValue ? arrearLoadAccount.AZValueDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            lblMaturityDate.Text = arrearLoadAccount.AZMaturityDate.HasValue ? arrearLoadAccount.AZMaturityDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            lblInterestedRate.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:0,0.0}", arrearLoadAccount.AZInterestRate);
            lblOrgPrincipal.Text = string.Empty; //
            dtpValueDate.SelectedDate = DateTime.Now;
           

        }
        private void BindDataToControlFromPeriodic()
        {
            var periodicLoadAccount = SavingAccountDAO.GetPeriodicSavingAccountById(RefIdToReview);
            if (periodicLoadAccount == null)
            {
                return;
            }
            tbDepositCode.Text = RefIdToReview;
            lblCustomer.Text = string.Format("{0} - {1}", periodicLoadAccount.CustomerId, periodicLoadAccount.CustomerName);
            lblCategory.Text = periodicLoadAccount.AccCategory;
            lblCurrency.Text = periodicLoadAccount.Currency;
            lblProductCode.Text = periodicLoadAccount.AZProductCode;
            lblPrincipal.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:0,0.0}", periodicLoadAccount.AZPrincipal);
            lblOpenDate.Text = periodicLoadAccount.AZValueDate.HasValue ? periodicLoadAccount.AZValueDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            lblMaturityDate.Text = periodicLoadAccount.AZMaturityDate.HasValue ? periodicLoadAccount.AZMaturityDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            lblInterestedRate.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:0,0.0}", periodicLoadAccount.AZInterestRate);
            dtpValueDate.SelectedDate = DateTime.Now;
            lblOrgPrincipal.Text = string.Empty; //
           


        }


        #endregion

       
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
               
                string[] param = new string[4];
                param[0] = "RefId=" + RefIdToReview;
                param[1] = "from=" + From;
                Response.Redirect(EditUrl("", "", "withdraw", param));
            }

        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            
        }
    }
}