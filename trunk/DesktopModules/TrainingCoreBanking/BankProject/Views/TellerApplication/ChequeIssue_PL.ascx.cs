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
    public partial class ChequeIssue_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }
        protected void RadGridPreview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGridPreview.DataSource = TriTT.B_CHEQUEISSUE_Preview_2_Authorize();
        }
        protected string getUrlPreview(string id)
        {
            return "Default.aspx?tabid=" + this.TabId.ToString() + "&ChequeID=" + id + "&IsAuthorize=1";
        }
    }
}