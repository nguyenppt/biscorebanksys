using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.CurrentNonTermSavingAC.SalaryPayment
{
    public partial class SalaryPaymentEnquiry : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private SavingAccountDAO SavingAccountDAO
        {
            get
            {
                return new SavingAccountDAO();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            radGridReview.DataSource = new DataTable();
            //radGridReview.DataBind();
        }
        private void LoadDataForDropdowns()
        {
            var customers = SavingAccountDAO.GetAllAuthorisedCustomer();
            rcbCustomerID.DataSource = customers;
            rcbCustomerID.DataTextField = "CustomerName";
            rcbCustomerID.DataValueField = "CustomerID";
            rcbCustomerID.AppendDataBoundItems = true;
            rcbCustomerID.DataBind();

            var currentcys = SavingAccountDAO.GetAllCurrency();
            rcbCurrentcy.DataValueField = "Code";
            rcbCurrentcy.DataTextField = "Code";
            rcbCurrentcy.DataSource = currentcys;
            rcbCurrentcy.DataBind();
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            radGridReview.DataSource = SavingAccountDAO.SearchSalaryPaymentFrequency(rcbStatus.SelectedValue, rcbCustomerID.SelectedValue, (decimal?)tbFrom.Value, (decimal?)tbTo.Value, rcbCurrentcy.SelectedValue, rcbtype.SelectedValue);
            radGridReview.DataBind();
            

        }
        public string GeturlReview(string refId)
        {
            var tabid = "139";
            if (rcbtype.SelectedValue == "Frequency")
            {
                tabid = "138";
            }

            return string.Format("Default.aspx?tabid={0}&RefId={1}", tabid, refId);
        }
    }
}