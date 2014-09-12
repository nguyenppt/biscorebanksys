using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using BankProject.DataProvider;

namespace BankProject.Views.TellerApplication
{
    public partial class ChequeReturned : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["redi"] != null && Request.QueryString["ChequeID"] != null)
            {
                Response.Redirect(EditUrl("ChequeID", Request.QueryString["ChequeID"].ToString(), "ChequeReturned2"));
                return;
            }
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
                    RadGridView.DataSource = TriTT.B_CHEQUEISSUE_Enquiry_Cheque(tbChequeRef.Text, tbWorkingAcct.Text, rcbChequeType.SelectedValue,
                        rdpIssuedDate.SelectedDate, tbChequeNo.Text.Length == 0 ? 0 : tbChequeNo.Value.Value);
                    RadGridView.DataBind();
                }
            }
        }
        public string geturlReview(string ChequeID)
        {
            return string.Format("Default.aspx?tabid=136&ChequeID={0}&redi={1}", ChequeID, "rediect");
            //Response.Redirect(EditUrl("ChequeID", ChequeID, "ChequeReturned2"));
        }
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                RadGridView.DataSource = TriTT.B_CHEQUEISSUE_Enquiry_Cheque(tbChequeRef.Text, tbWorkingAcct.Text, rcbChequeType.SelectedValue,
                    rdpIssuedDate.SelectedDate, tbChequeNo.Text.Length == 0 ? 0 : tbChequeNo.Value.Value);
            }
        }
        protected void btSearch_Click(object sender, EventArgs e)
        {
            //Search();
        }
       
        protected void RadGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //RadGrid1.DataSource = TriTT.B_BCHEQUERETURN_findItem("", "", "", "");
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