using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Import.DocumentaryCredit
{
    public partial class PreviewIssueLC : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        string uiType = "issuelc";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.TabId == IssueLC.TabIssueLCClose) uiType = "closelc";
            if (IsPostBack) return;
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = SQLData.B_BIMPORT_NORMAILLC_GetbyStatus(uiType, UserId);
        }

        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=" + this.TabId + "&CodeID=" + id + "&disable=1" + "&key=" + uiType;
        }
    }
}