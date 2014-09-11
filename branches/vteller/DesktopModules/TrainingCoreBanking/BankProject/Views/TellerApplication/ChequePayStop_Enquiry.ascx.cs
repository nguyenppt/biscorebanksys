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
    public partial class ChequePayStop_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
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
                RadGridDataPreview.DataSource = TriTT.CHEQUE_STOP_Enquiry(tbDocID.Text, tbWorkingAcct.Text, tbChequeNo.Value.HasValue? tbChequeNo.Value.Value:0,
                    tbCustomerID.Text, tbCustomerName.Text, rdpActiveDate.SelectedDate.HasValue? rdpActiveDate.SelectedDate:null ,rcbChequeType.SelectedValue,
                    rcbStopReason.SelectedValue);
                RadGridDataPreview.DataBind();
            }
        }
        protected void DataPreview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //TriTT.CHEQUE_TRANSFER_Enquiry("!", "!", 0, "0", "0", new DateTime(1900, 12, 12), "0", "0", 0, 0);
        }
        protected string getUrlPreview(string ID)
        {
            return string.Format("Default.aspx?tabid=134&AccountID={0}", ID);
        }
        protected void LoadChequeType()
        {
            rcbChequeType.Items.Clear();
            rcbChequeType.Items.Add(new RadComboBoxItem("", ""));
            rcbChequeType.DataSource = TriTT.CHEQUE_WITHDRAWAL_LoadChequeType();
            rcbChequeType.DataTextField = "Description";
            rcbChequeType.DataValueField = "Code";
            rcbChequeType.DataBind();
        }
    }
}