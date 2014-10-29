using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.FTTeller
{
    public partial class OutwardTransferByCash_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
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
                    RadGridView.DataSource = TriTT.OUT_TRANS_BY_CASH_Enquiry(rcbProductID.SelectedValue, tbSendingName.Text, tbLegalID.Text, tbReceivingName.Text
                        , tbBenAccount.Text, tbID.Text, rcbBenCom.SelectedValue, rcbCurrency.SelectedValue,tbFromAmt.Value.HasValue ? tbFromAmt.Value.Value : 0,
                        tbToAmt.Value.HasValue ? tbToAmt.Value.Value : 0, rcbTransactionType.SelectedValue);
                    RadGridView.DataBind();
                }
            }
        }
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                RadGridView.DataSource = TriTT.OUT_TRANS_BY_CASH_Enquiry(rcbProductID.SelectedValue, tbSendingName.Text, tbLegalID.Text, tbReceivingName.Text
                       , tbBenAccount.Text, tbID.Text, rcbBenCom.SelectedValue, rcbCurrency.SelectedValue, tbFromAmt.Value.HasValue ? tbFromAmt.Value.Value : 0,
                       tbToAmt.Value.HasValue ? tbToAmt.Value.Value : 0, rcbTransactionType.SelectedValue);
            }
        }
        protected string geturlReview(string ID, string TransactionType)
        {
            switch(TransactionType)
            {
                case "Cash":
                return "Default.aspx?tabid=158&ID="+ID;
                break;
                case "Account":
                return "Default.aspx?tabid=159&ID=" + ID;
                break;
                default:
                return "";
        }
        }
        protected void rcbPaymentType_onSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (rcbProductID.SelectedValue == "3000")
            {
                tbBenAccount.Enabled = true;
                tbReceivingName.Enabled = true;
            }
            else
            {
                tbBenAccount.Enabled = false;
                tbReceivingName.Enabled = false;
            }
        }
        protected void FirstLoad()
        {
            rcbBenCom.Items.Clear();
            rcbBenCom.Items.Add(new RadComboBoxItem("")); ;
            rcbBenCom.DataSource = BankProject.DataProvider.Database.BENCOM_GetALL();
            rcbBenCom.DataTextField = "BENCOMNAME";
            rcbBenCom.DataValueField = "BENCOMID";
            rcbBenCom.DataBind();
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