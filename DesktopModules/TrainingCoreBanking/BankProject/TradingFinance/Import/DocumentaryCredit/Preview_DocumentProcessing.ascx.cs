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
    public partial class Preview_DocumentProcessing : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=" + TabId.ToString() + "&paycode=" + id + "&disable=1";
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = SQLData.B_BIMPORT_DOCUMENTPROCESSING_GetByReview(TabId ,UserId);
        }
    }
}