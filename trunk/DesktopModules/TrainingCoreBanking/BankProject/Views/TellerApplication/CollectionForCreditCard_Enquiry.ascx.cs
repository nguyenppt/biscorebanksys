using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class CollectionForCreditCard_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
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
                    RadGridView.DataSource = TriTT.COLLECTION_4_CRE_CARD_PAYMENT_Enquiry(rcbPaymentType.SelectedValue,tbID.Text, tbDebitAcct.Text, rcbDebitCurrency.SelectedValue
                        , tbCustomerID.Text, tbCustomerName.Text, tbLegalID.Text, tbFromAmt.Value.HasValue ? tbFromAmt.Value.Value : 0,
                        tbToAmt.Value.HasValue ? tbToAmt.Value.Value : 0);
                    RadGridView.DataBind();
                }
            }
        }
        protected string getUrlPreview(string ID, string Type)
        {
            switch (Type)
            { 
                case "Collection":
                return string.Format("Default.aspx?tabid=128&ID={0}", ID);
                default :
                return string.Format("Default.aspx?tabid=129&ID={0}", ID);
            }
        }
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                RadGridView.DataSource = TriTT.COLLECTION_4_CRE_CARD_PAYMENT_Enquiry(rcbPaymentType.SelectedValue, tbID.Text, tbDebitAcct.Text, rcbDebitCurrency.SelectedValue
                       , tbCustomerID.Text, tbCustomerName.Text, tbLegalID.Text, tbFromAmt.Value.HasValue ? tbFromAmt.Value.Value : 0,
                       tbToAmt.Value.HasValue ? tbToAmt.Value.Value : 0);
            }
        }
        protected void LoadCurrency()
        {
            var Currency = TriTT.B_LoadCurrency("", "");
            rcbDebitCurrency.Items.Clear();
            rcbDebitCurrency.Items.Add(new RadComboBoxItem(""));
            rcbDebitCurrency.AppendDataBoundItems = true;
            rcbDebitCurrency.DataValueField = "Code";
            rcbDebitCurrency.DataTextField = "Code";
            rcbDebitCurrency.DataSource = Currency;
            rcbDebitCurrency.DataBind();
        }
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
    }
}