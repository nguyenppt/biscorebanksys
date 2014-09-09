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
    public partial class OpenRevoling_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
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
        }
        protected void radtoolbar2_onbuttonclick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            string commandName = ToolBarButton.CommandName;
            if (commandName == "search")
            {
                RadGrid.DataSource= TriTT.B_OPEN_COMMITMENT_CONT_Enquiry_Account(tbAcctRef.Text, tbCustomerID.Text, tbFullName.Text, rcbCurrency.SelectedValue, rcbCategory.SelectedValue,
                    tbDocId.Text, tbIntRepayAcct.Text);
                RadGrid.DataBind();
            }
        }
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid.DataSource = TriTT.B_OPEN_COMMITMENT_CONT_Enquiry_Account("!", "!", "", "", "", "", "");
        }

        protected string geturlReview(string ContractID, string status)
        {
            return string.Format("Default.aspx?tabid=187&ContID={0}&Status={1}&mode=Load_Full", ContractID, status);
            //return string.Format("Default.aspx?tabid=187&mid={0}&ContID={1}&Status={2}&mode=Load_Full", "885", ContractID, status);
        }

        private void LoadCurrency()
        {
            rcbCurrency.DataSource = TriTT.B_LoadCurrency("USD","VND");
            rcbCurrency.DataValueField = "Code";
            rcbCurrency.DataTextField = "Code";
            rcbCurrency.DataBind();
        }
        protected void LoadCategory()
        {
            rcbCategory.DataSource = DataProvider.TriTT.B_OPEN_LOANWORK_ACCT_Get_ALLCategory("B_OPEN_LOANWORK_ACCT_Get_ALLCategory", "5");
            rcbCategory.DataValueField = "Code";
            rcbCategory.DataTextField = "CodeHasName";
            rcbCategory.DataBind();
        }
        
    }
}