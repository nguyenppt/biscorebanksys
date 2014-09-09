using DotNetNuke.Entities.Modules;
using System;
using System.Data;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class EnquiryAccountTransaction : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            LoadToolBar();
        }

        protected void LoadToolBar()
        {
            RadToolBar2.FindItemByValue("btCommit").Enabled = false;
            RadToolBar2.FindItemByValue("btReview").Enabled = false;
            RadToolBar2.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar2.FindItemByValue("btRevert").Enabled = false;
            RadToolBar2.FindItemByValue("btPrint").Enabled = false;
            RadToolBar2.FindItemByValue("btSearch").Enabled = true;
        }

        protected void radtoolbar2_onbuttonclick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            var commandName = ToolBarButton.CommandName;
            switch (commandName)
            {
                case "search":
                    LoadData();
                    break;
            }
        }

        private void LoadData()
        {
            radGridReview.DataSource = BankProject.DataProvider.Database.BOPENACCOUNT_EnquiryTransaction(rcbAccountType.SelectedValue, rcbTransactionType.SelectedValue, txtRefID.Text, rcbCurrency.SelectedValue, rcbCustomerType.SelectedValue, tbCustomerID.Text,
                                                                                              tbGBFullName.Text, tbCustomerAccount.Text, txtFrom.Value, txtTo.Value, txtDate.SelectedDate);
            radGridReview.DataBind();
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = BankProject.DataProvider.Database.BOPENACCOUNT_EnquiryTransaction("1", "0", "NOTdata", "NODATa", "NOTdata", "NOTdata", "NOTdata", "NOTdata", 0, 0, new DateTime(1900, 1, 1));
        }

    }
}