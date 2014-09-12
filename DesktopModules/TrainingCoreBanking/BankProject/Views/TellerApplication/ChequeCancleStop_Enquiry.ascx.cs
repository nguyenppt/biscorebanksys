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
    public partial class ChequeCancleStop_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
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
                if (IsPostBack)
                {
                    RadGridDataPreview.DataSource = TriTT.CHEQUE_CANCLE_STOP_Enquiry(tbWorkingAcct.Text, tbChequeNo.Value.HasValue ? tbChequeNo.Value.Value : 0,
                        tbDocID.Text, tbCustomerID.Text, tbCustomerName.Text, rdpActiveDate.SelectedDate, rcbChequeType.SelectedValue);
                    RadGridDataPreview.DataBind();
                }
            }
        }
        protected string getUrlPreview(string AccountID, string SerialNo)
        {
            return "Default.aspx?tabid=135"+ "&AccountID=" + AccountID + "&SerialNo=" + SerialNo;
        }
        protected void DataPreview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                RadGridDataPreview.DataSource = TriTT.CHEQUE_CANCLE_STOP_Enquiry(tbWorkingAcct.Text, tbChequeNo.Value.HasValue ? tbChequeNo.Value.Value : 0,
                    tbDocID.Text, tbCustomerID.Text, tbCustomerName.Text, rdpActiveDate.SelectedDate, rcbChequeType.SelectedValue);
            }
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