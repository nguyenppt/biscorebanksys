using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BankProject.DataProvider;


namespace BankProject.Views.TellerApplication
{
    public partial class CashRepayment_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
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
            LoadCurrency();
        }
        protected void radtoolbar2_onbuttonclick(object sender, RadToolBarEventArgs e)
        { 
            var ToolBarButton = e.Item as RadToolBarButton;
            string CommandName = ToolBarButton.CommandName;
            if (CommandName == "search")
            {
                if (IsPostBack)
                {
                    RadGridView.DataSource = TriTT.B_CASHREPAYMENT_Enquiry(tbID.Text, tbCustomerAcct.Text, rcbCurrency.SelectedValue, tbCustomerID.Text,
                        tbCustomerName.Text, tbLegalID.Text, tbFromDepositedAmt.Value.HasValue? tbFromDepositedAmt.Value.Value:0, 
                        tbTODepositedAmt.Value.HasValue? tbTODepositedAmt.Value.Value:0);
                    RadGridView.DataBind();
                }
            }
        }
        protected string getUrlPreview(string ID)
        {
            return string.Format("Default.aspx?tabid=200&ID={0}", ID);
        }
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                RadGridView.DataSource = TriTT.B_CASHREPAYMENT_Enquiry(tbID.Text, tbCustomerAcct.Text, rcbCurrency.SelectedValue, tbCustomerID.Text,
                       tbCustomerName.Text, tbLegalID.Text, tbFromDepositedAmt.Value.HasValue ? tbFromDepositedAmt.Value.Value : 0,
                       tbTODepositedAmt.Value.HasValue ? tbTODepositedAmt.Value.Value : 0);
            }
        }
        protected void LoadCurrency()
        {
            var Currency = TriTT.B_LoadCurrency("USD", "VND");
            rcbCurrency.Items.Clear();
            rcbCurrency.Items.Add(new RadComboBoxItem(""));
            rcbCurrency.AppendDataBoundItems = true;
            rcbCurrency.DataValueField = "Code";
            rcbCurrency.DataTextField = "Code";
            rcbCurrency.DataSource = Currency;
            rcbCurrency.DataBind();
        }
    }
}