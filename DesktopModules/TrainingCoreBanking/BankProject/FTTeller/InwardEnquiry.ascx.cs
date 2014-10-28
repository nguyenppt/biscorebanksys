using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BankProject.DataProvider;

namespace BankProject.FTTeller
{
    public partial class InwardEnquiry : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            FirstLoad();
        }
        protected void radtoolbar2_onbuttonclick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            string CommandName = ToolBarButton.CommandName;
            if (CommandName == "search")
            {
                if (IsPostBack)
                {
                    radGridReview.DataSource = TriTT.BINWARD_CASH_WITHDRAW_Enquiry("CashWithdraw",tbBOName.Text,tbFOName.Text,tbLegalID.Text,
                        tbID.Text, rcbCurrency.SelectedValue,tbFromAmt.Value.HasValue ? tbFromAmt.Value.Value : 0,
                        tbToAmt.Value.HasValue ? tbToAmt.Value.Value : 0);
                    radGridReview.DataBind();
                }
            }
        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                radGridReview.DataSource = TriTT.BINWARD_CASH_WITHDRAW_Enquiry(rcbTransactionType.SelectedValue, tbBOName.Text, tbFOName.Text, tbLegalID.Text,
                       tbID.Text, rcbCurrency.SelectedValue, tbFromAmt.Value.HasValue ? tbFromAmt.Value.Value : 0,
                       tbToAmt.Value.HasValue ? tbToAmt.Value.Value : 0);
            }
        }
        protected string geturlReview(string ID, string Status, string tu)
        {
            switch (tu)
            { 
                case "CashWithdraw":
                    return "Default.aspx?tabid=171&RefID=" + ID + "&Status=" + Status;
                    break;
                default:
                    return "Default.aspx?tabid=172&RefID=" + ID + "&Status=" + Status;
                    break;
            }
            
        }
       
        protected void FirstLoad()
        {
            
            LoadCurrency();
            LoadToolBar();
        }
        protected void LoadCurrency()
        {
            var Currency = TriTT.B_LoadCurrency("", "");
            rcbCurrency.Items.Clear();
            rcbCurrency.Items.Add(new RadComboBoxItem(""));
            rcbCurrency.AppendDataBoundItems = true;
            rcbCurrency.DataValueField = "Code";
            rcbCurrency.DataTextField = "Code";
            rcbCurrency.DataSource = Currency;
            rcbCurrency.DataBind();
        }
        protected void LoadToolBar()
        {
            RadToolBar2.FindItemByValue("btCommit").Enabled = false;
            RadToolBar2.FindItemByValue("btPreview").Enabled = false;
            RadToolBar2.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar2.FindItemByValue("btReverse").Enabled = false;
            RadToolBar2.FindItemByValue("btSearch").Enabled = true;
            RadToolBar2.FindItemByValue("btPrint").Enabled = false;
        }
    
    }
}