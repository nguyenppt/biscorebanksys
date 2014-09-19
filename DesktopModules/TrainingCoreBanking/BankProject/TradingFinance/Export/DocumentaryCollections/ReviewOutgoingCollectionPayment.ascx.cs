using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;
using BankProject.DBContext;

namespace BankProject.TradingFinance.Export.DocumentaryCollections
{
    public partial class ReviewOutgoingCollectionPayment : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private VietVictoryCoreBankingEntities _entities = new VietVictoryCoreBankingEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }
        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=" + TabId.ToString() + "&CodeId=" + id + "&disable=1";
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var userId = UserId.ToString();
            radGridReview.DataSource = _entities.BOUTGOINGCOLLECTIONPAYMENTs.Where(q => q.CreateBy == userId && q.Status != "AUT").ToList();
        }
    }
}