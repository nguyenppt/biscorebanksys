using System;
using BankProject.DataProvider;
using System.Data;
using Telerik.Web.UI;
using System.Linq;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close
{
    public partial class Preclosure : DotNetNuke.Entities.Modules.PortalModuleBase
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
            if (IsPostBack) return;
            LoadDataForDropdowns();
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = new DataTable();// SavingAccountDAO.GetPeriodicArrearByStatus(Entity.AuthoriseStatus.AUT); 
        }
        #region private methods
        private void LoadDataForDropdowns()
        {
            var customers = SavingAccountDAO.GetAllAuthorisedCustomer();
            rcbCustomerID.DataSource = customers;
            rcbCustomerID.DataTextField = "CustomerName";
            rcbCustomerID.DataValueField = "CustomerID";
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
            rcbCategory.SelectedIndex=0;
            rcbCurrentcy.SelectedIndex = 0;
            rcbProductLine.SelectedIndex = 0;
            tbPrincipalFrom.Value = null;
            tbPrincipalTo.Value = null;
            rcbCustomerID.SelectedIndex = 0;
        }
        #endregion
        #region helper
        public string GeturlReview(string refId,string from)
        {
            string[] param = new string[2];
            param[0] = "RefId=" + refId;
            param[1] = "from=" + from;
            return EditUrl("", "", "input", param);           
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
                    radGridReview.DataSource = SavingAccountDAO.GetPeriodicArrearByStatus(Entity.AuthoriseStatus.AUT.ToString(),
                                                tbRefId.Text,rcbCategory.SelectedValue,rcbCurrentcy.SelectedValue,rcbProductLine.SelectedValue,
                                                tbPrincipalFrom.Value.HasValue?tbPrincipalFrom.Value.Value:0,
                                                tbPrincipalTo.Value.HasValue ? tbPrincipalTo.Value.Value : 0, rcbCustomerID.SelectedValue,
                                                rcbtype.SelectedValue);
                    radGridReview.Rebind();
                    break;
                case "preview":
                    if (rcbtype.SelectedValue == "Arrear")
                    {
                        Response.Redirect("Default.aspx?tabid=148&ctl=CloseSavingAccReviewList&mid=808&From=Arrear");
                    }
                    else
                    {
                        Response.Redirect("Default.aspx?tabid=148&ctl=CloseSavingAccReviewList&mid=808&From=Periodic");
                    }
                    
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