using DotNetNuke.Entities.Modules;
using System;
using System.Data;
using Telerik.Web.UI;
using BankProject.DataProvider;

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
            if (IsPostBack)
            {
                radGridReview.DataSource = Database.BOPENACCOUNT_EnquiryTransaction(rcbAccountType.SelectedValue, rcbTransactionType.SelectedValue, txtRefID.Text, rcbCurrency.SelectedValue, rcbCustomerType.SelectedValue, tbCustomerID.Text,
                                                                                                  tbGBFullName.Text, tbCustomerAccount.Text, txtFrom.Value, txtTo.Value, txtDate.SelectedDate);
                radGridReview.DataBind();
            }
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                radGridReview.DataSource = Database.BOPENACCOUNT_EnquiryTransaction(rcbAccountType.SelectedValue, rcbTransactionType.SelectedValue, txtRefID.Text, rcbCurrency.SelectedValue, rcbCustomerType.SelectedValue, tbCustomerID.Text,
                                                                                                 tbGBFullName.Text, tbCustomerAccount.Text, txtFrom.Value, txtTo.Value, txtDate.SelectedDate);
            }
        }
        public string getUrlPreview(string Id, string get_for)
        {
            switch (get_for)
            {
                case "Cash_deposit":
                    return "Default.aspx?tabid=124&preview=1&codeid="+Id;
                    break;
                case "Cash_withraw":
                    return "Default.aspx?tabid=125&preview=1&codeid="+Id;
                    break;
                default:
                    return "Default.aspx?tabid=126&preview=1&codeid=" + Id;
                    break;
                
            }
        }
    }
}