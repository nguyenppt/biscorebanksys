using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Import.DocumentaryCredit
{
    public partial class PreviewAmendLC : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = SQLData.B_BIMPORT_NORMAILLC_GetbyStatus("amendlc", UserId);
        }

        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=204&CodeID=" + id + "&key=amendlc" + "&disable=1";
        }
    }
}