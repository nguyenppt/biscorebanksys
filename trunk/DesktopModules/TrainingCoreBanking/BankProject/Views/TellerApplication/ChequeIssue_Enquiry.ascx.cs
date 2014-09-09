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
    public partial class ChequeIssue_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
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
                RadGridView.DataSource = TriTT.B_CHEQUEISSUE_Enquiry_Cheque(tbChequeRef.Text, tbWorkingAcct.Text,rcbChequeType.SelectedValue,
                    rdpIssuedDate.SelectedDate, tbChequeNo.Text.Length == 0? 0 : tbChequeNo.Value.Value);
                RadGridView.DataBind();
            }
        }
        public string geturlReview(string ChequeID)
        {
            return string.Format("Default.aspx?tabid=131&ChequeID={0}", ChequeID);
        }
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TriTT.B_CHEQUEISSUE_Enquiry_Cheque("!", "!", "",rdpIssuedDate.SelectedDate , 0);
        }
        protected void LoadChequeType()
        {
            rcbChequeType.DataSource = TriTT.CHEQUE_WITHDRAWAL_LoadChequeType();
            rcbChequeType.DataTextField = "Description";
            rcbChequeType.DataValueField = "Code";
            rcbChequeType.DataBind();
        }
       
    }
}