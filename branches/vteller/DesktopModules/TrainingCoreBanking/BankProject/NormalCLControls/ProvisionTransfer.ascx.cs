using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.NormalCLControls
{
    public partial class ProvisionTransfer : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();
            DataSet dsc = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            rcbApplicantID.DataSource = dsc;
            rcbApplicantID.DataTextField = "CustomerName";
            rcbApplicantID.DataValueField = "CustomerID";
            rcbApplicantID.DataBind();
        }
        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            //RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (tbLCNo.Text != "" || rcbApplicantID.SelectedValue != "")
            {
                radGridReview.DataSource = DataProvider.KhanhND.B_BNORMAILLC_Search(tbLCNo.Text, rcbApplicantID.SelectedValue);
            }
        }
        public string geturlReview(string id)
        {

            return "Default.aspx?tabid=" + this.TabId.ToString() + "&ctl=update&mid=" + this.ModuleId.ToString() + "&CodeID=" + id + "&lid=" + id;
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            radGridReview.Rebind();
        }
    }
}