using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Enquiry
{
    public partial class ArrearandPeriodic : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        #region Property

        private SavingAccountDAO SavingAccountDAO
        {
            get
            {
                return new SavingAccountDAO();
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                radGridReview.DataSource = SavingAccountDAO.GetPeriodicArrearByStatus(rcbStatus.SelectedValue,
                                            tbRefId.Text, rcbCategory.SelectedValue, rcbCurrentcy.SelectedValue, rcbProductLine.SelectedValue,
                                            tbPrincipalFrom.Value.HasValue ? tbPrincipalFrom.Value.Value : 0,
                                            tbPrincipalTo.Value.HasValue ? tbPrincipalTo.Value.Value : 0, rcbCustomerID.SelectedValue,
                                            rcbtype.SelectedValue);
                return; 
            }
            LoadDataForDropdowns();
         
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = new DataTable();// SavingAccountDAO.GetPeriodicArrearByStatus(Entity.AuthoriseStatus.AUT); 
        }
        #region private methods
        private void LoadDataForDropdowns()
        {
            //var customers = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            //rcbCustomerID.DataSource = customers;
            //rcbCustomerID.DataTextField = "CustomerName";
            //rcbCustomerID.DataValueField = "CustomerID";
            //rcbCustomerID.DataBind();

            var customers = SavingAccountDAO.GetAllAuthorisedCustomer();
            rcbCustomerID.DataSource = customers;
            rcbCustomerID.DataTextField = "CustomerName";
            rcbCustomerID.DataValueField = "CustomerID";
            rcbCustomerID.AppendDataBoundItems = true;
            rcbCustomerID.DataBind();

            rcbCategory.DataSource = SavingAccountDAO.GetAllCategoryForSavingAcc();
            rcbCategory.DataValueField = "Code";
            rcbCategory.DataTextField = "FormatedName";
            rcbCategory.DataBind();

            rcbProductLine.DataSource = SavingAccountDAO.GetAllProductLinesForSavingAcc();
            rcbProductLine.DataTextField = "Description";
            rcbProductLine.DataValueField = "ProductID";
            rcbProductLine.DataBind();

            var currentcys = SavingAccountDAO.GetAllCurrency();
            rcbCurrentcy.DataValueField = "Code";
            rcbCurrentcy.DataTextField = "Code";
            rcbCurrentcy.DataSource = currentcys;
            rcbCurrentcy.DataBind();
        }
        private void ClearControl()
        {
            tbRefId.Text = string.Empty;
            rcbCategory.SelectedIndex = 0;
            rcbCurrentcy.SelectedIndex = 0;
            rcbProductLine.SelectedIndex = 0;
            tbPrincipalFrom.Value = null;
            tbPrincipalTo.Value = null;
            rcbCustomerID.SelectedIndex = 0;
        }
        #endregion
        #region helper
        public string GeturlReview(string refId, string from)
        {
           if (from=="arrear")
           {
               from = "145";
           }
           else { from = "146"; }
           return string.Format("Default.aspx?tabid={0}&RefId={1}", from, refId);
        }
        #endregion
        #region Event

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "search":
                    radGridReview.DataSource = SavingAccountDAO.GetPeriodicArrearByStatus(rcbStatus.SelectedValue,
                                                tbRefId.Text, rcbCategory.SelectedValue, rcbCurrentcy.SelectedValue, rcbProductLine.SelectedValue,
                                                tbPrincipalFrom.Value.HasValue ? tbPrincipalFrom.Value.Value : 0,
                                                tbPrincipalTo.Value.HasValue ? tbPrincipalTo.Value.Value : 0, rcbCustomerID.SelectedValue,
                                                rcbtype.SelectedValue);
                    radGridReview.Rebind();
                    break;
                default:
                    ClearControl();
                    radGridReview.DataSource = new DataTable();
                    radGridReview.Rebind();
                    break;
            }


        }

        #endregion
    }
}