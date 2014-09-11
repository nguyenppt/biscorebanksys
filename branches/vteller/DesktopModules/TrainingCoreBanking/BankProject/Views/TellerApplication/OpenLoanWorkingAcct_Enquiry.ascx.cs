using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class OpenLoanWorkingAcct_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            RadToolBar2.FindItemByValue("btCommit").Enabled = false;
            RadToolBar2.FindItemByValue("btPreview").Enabled = false;
            RadToolBar2.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar2.FindItemByValue("btReverse").Enabled = false;
            RadToolBar2.FindItemByValue("btSearch").Enabled = true;
            RadToolBar2.FindItemByValue("btPrint").Enabled = false;
            LoadCategory();
            LoadCurrency();
            LoadProductLine();
        }
        protected void radtoolbar2_onbuttonclick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            string CommandName = ToolBarButton.CommandName;
            if (CommandName == "search")
            {
                if (IsPostBack)
                {
                    RadGrid.DataSource = TriTT.B_OPEN_LOANWORK_ACCT_Enquiry_Customer(tbAcctRef.Text, tbCustomerID.Text, tbFullName.Text, rcbCurrency.SelectedValue,
                        rcbProductLine.SelectedValue, rcbCategory.SelectedValue, tbDocId.Text);
                    RadGrid.DataBind();
                }
                else
                {
                    RadGrid.DataSource = TriTT.B_OPEN_LOANWORK_ACCT_Enquiry_Customer("!!", "!", tbFullName.Text, rcbCurrency.SelectedValue,
                         rcbProductLine.SelectedValue, rcbCategory.SelectedValue, "!");
                }
            }
        }
        protected string geturlReview(string AccountID, string status)
        {
            return string.Format("Default.aspx?tabid=184&mid={0}&AcctID={1}&Status={2}&mode=Load_Full", 774, AccountID, status);
        }
        #region Load Properties
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid.DataSource = TriTT.B_OPEN_LOANWORK_ACCT_Enquiry_Customer("!", "!", "", "", "", "", "");
        }
        private void LoadCurrency()
        {
            rcbCurrency.DataSource = TriTT.B_LoadCurrency("USD","VND");
            rcbCurrency.DataValueField = "Code";
            rcbCurrency.DataTextField = "Code";
            rcbCurrency.DataBind();
        }
        private void LoadCategory()
        {
            rcbCategory.DataSource = DataProvider.TriTT.B_OPEN_LOANWORK_ACCT_Get_ALLCategory("B_OPEN_LOANWORK_ACCT_Get_ALLCategory", "4");
            rcbCategory.DataValueField = "Code";
            rcbCategory.DataTextField = "CodeHasName";
            rcbCategory.DataBind();
        }
        private void LoadProductLine()
        {
            rcbProductLine.DataSource = DataProvider.TriTT.B_OPEN_LOANWORK_ACCT_Get_ALLCategory("B_OPEN_LOANWORK_ACCT_Get_ALLProductLine", "4");
            rcbProductLine.DataValueField = "ProductID";
            rcbProductLine.DataTextField = "ProductHasName";
            rcbProductLine.DataBind();
        }
        #endregion
    }
}