using DotNetNuke.Entities.Modules;
using System;
using Telerik.Web.UI;
using BankProject.DataProvider;
using System.Data;

namespace BankProject.Views.TellerApplication
{
    public partial class DiscountedCloseList : PortalModuleBase
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
            LoadDataForDropdowns();
        }

        private void LoadData()
        {
            radGridReview.DataSource = new DataTable();
            
        }
        private void LoadDataForDropdowns()
        {

            var currentcys = SavingAccountDAO.GetAllCurrency();
            rcbCurrentcy.DataValueField = "Code";
            rcbCurrentcy.DataTextField = "Code";
            rcbCurrentcy.DataSource = currentcys;
            rcbCurrentcy.DataBind();
        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.LoadData();
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "search":
                    
                    radGridReview.DataSource = SavingAccountDAO.SearchDiscountedAccountByStatus(Entity.AuthoriseStatus.AUT.ToString(),tbRefId.Text,tbWrokingAccid.Text,
                        tbWorkingAccName.Text, (decimal?)tbPrincipalFrom.Value, (decimal?)tbPrincipalTo.Value, rcbCurrentcy.SelectedValue, LDid.Text);                 
                    radGridReview.Rebind();
                    break;
                case "preview":
                    Response.Redirect("Default.aspx?tabid=149&ctl=ListReview&mid=861");
                    break;
                default:
                    //ClearControl();
                    radGridReview.DataSource = new DataTable();
                    radGridReview.Rebind();
                    break;
            }


        }        

        public string geturlReview(string id)
        {
            return EditUrl("chitiet") + "&refId=" + id;
        }
    }
}