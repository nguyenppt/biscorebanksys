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
    public partial class Discounted : DotNetNuke.Entities.Modules.PortalModuleBase
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

                radGridReview.DataSource = SavingAccountDAO.SearchDiscountedAccountByStatus(rcbStatus.SelectedValue, tbRefId.Text, tbWrokingAccid.Text,
                    tbWorkingAccName.Text, (decimal?)tbPrincipalFrom.Value, (decimal?)tbPrincipalTo.Value, rcbCurrentcy.SelectedValue, LDid.Text);
                radGridReview.Rebind();
                return;
            }
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

                    radGridReview.DataSource = SavingAccountDAO.SearchDiscountedAccountByStatus(rcbStatus.SelectedValue, tbRefId.Text, tbWrokingAccid.Text,
                        tbWorkingAccName.Text, (decimal?)tbPrincipalFrom.Value, (decimal?)tbPrincipalTo.Value, rcbCurrentcy.SelectedValue, LDid.Text);
                    radGridReview.Rebind();
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
            return string.Format("Default.aspx?tabid={0}&RefId={1}", "147", id);
        }
    }
}