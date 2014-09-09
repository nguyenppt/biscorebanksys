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
    public partial class PreviewCancelLC : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = SQLData.B_BIMPORT_NORMAILLC_GetbyStatus("cancellc", UserId);
        }
        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=205&CodeID=" + id + "&key=cancellc" + "&disable=1";
        }
    }
}