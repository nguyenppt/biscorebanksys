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
    public partial class TransferByAccount_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource= TriTT.BOUTWARD_TRANS_BY_ACCT_PreviewList();
        }
        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=" + this.TabId.ToString() + "&ID=" + id ;
        }
    }
}