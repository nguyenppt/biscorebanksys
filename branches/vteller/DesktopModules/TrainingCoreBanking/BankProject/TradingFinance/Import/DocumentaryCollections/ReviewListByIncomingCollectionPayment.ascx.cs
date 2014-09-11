using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Import.DocumentaryCollections
{
    public partial class ReviewListByIncomingCollectionPayment : DotNetNuke.Entities.Modules.PortalModuleBase
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
            radGridReview.DataSource = SQLData.B_INCOMINGCOLLECTIONPAYMENT_GetByReview(UserId.ToString());
        }
    }
}