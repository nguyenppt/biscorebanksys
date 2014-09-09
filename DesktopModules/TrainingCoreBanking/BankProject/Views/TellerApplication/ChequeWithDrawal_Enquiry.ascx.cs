using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using System.Data;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class ChequeWithDrawal_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
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
            LoadChequeType();
        }
        protected void radtoolbar2_onbuttonclick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            string CommandName = ToolBarButton.CommandName;
            if (CommandName == "search")
            {
                RadGridView.DataSource = TriTT.CHEQUE_WITHDRAWAL_Enquiry(tbWithdrawalID.Text, tbWorkingAcct.Text,tbChequeNo.Value.HasValue? tbChequeNo.Value.Value:0, tbCustomerID.Text
                    ,tbCustomerName.Text, rdpIssuedDate.SelectedDate, rcbChequeType.SelectedValue, tbLegalID.Text,tbFromAmountLCY.Value.HasValue? tbFromAmountLCY.Value.Value :0,
                    tbToAmountLCY.Value.HasValue? tbToAmountLCY.Value.Value:0);
                RadGridView.DataBind();
            }
        }
        public string geturlReview(string ID)
        {
            return string.Format("Default.aspx?tabid=132&ID={0}", ID);
        }
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TriTT.CHEQUE_WITHDRAWAL_Enquiry("!", "!",0, "0", "0",new DateTime(1900,12,12),"0","0",0,0);
        }
        protected void LoadChequeType()
        {
            rcbChequeType.Items.Clear();
            rcbChequeType.Items.Add(new RadComboBoxItem("",""));
            rcbChequeType.DataSource = TriTT.CHEQUE_WITHDRAWAL_LoadChequeType();
            rcbChequeType.DataTextField = "Description";
            rcbChequeType.DataValueField = "Code";
            rcbChequeType.DataBind();
        }
    }
}