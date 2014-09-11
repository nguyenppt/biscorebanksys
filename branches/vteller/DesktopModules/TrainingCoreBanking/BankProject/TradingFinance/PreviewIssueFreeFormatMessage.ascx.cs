using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TradingFinance
{
    public partial class PreviewIssueFreeFormatMessage : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = SQLData.B_BFREETEXTMESSAGE_GetByStatus(UserId);
        }
        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=282&CodeID=" + id + "&disable=1";
        }
    }
}