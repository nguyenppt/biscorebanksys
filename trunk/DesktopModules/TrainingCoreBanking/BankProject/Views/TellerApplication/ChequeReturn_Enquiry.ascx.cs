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
    public partial class ChequeReturn_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
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
                    RadGrid.DataSource = TriTT.B_CHEQUE_RETURN_Enquiry(tbRefCheque.Text, tbWorkingAcct.Text, tbDocID.Text, tbCustomerID.Text, tbCustomerName.Text,
                        rdpActiveDate.SelectedDate.HasValue ? rdpActiveDate.SelectedDate : null, rcbChequeType.SelectedValue, tbReturnedCheque.Value.HasValue ? tbReturnedCheque.Value.Value : 0);
                    RadGrid.DataBind();
                }
            }
        }
        protected string getUrlPreview(string RefCheque, string ReturnedCheque)
        {
            return "Default.aspx?tabid=136&&ctl=ChequeReturned2&mid=837" + "&RefCheque=" + RefCheque + "&ReturnedCheque=" + ReturnedCheque;
        }
        protected void RadGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                RadGrid.DataSource = TriTT.B_CHEQUE_RETURN_Enquiry(tbRefCheque.Text, tbWorkingAcct.Text, tbDocID.Text, tbCustomerID.Text, tbCustomerName.Text,
                    rdpActiveDate.SelectedDate.HasValue ? rdpActiveDate.SelectedDate : null, rcbChequeType.SelectedValue, tbReturnedCheque.Value.HasValue ? tbReturnedCheque.Value.Value : 0);
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