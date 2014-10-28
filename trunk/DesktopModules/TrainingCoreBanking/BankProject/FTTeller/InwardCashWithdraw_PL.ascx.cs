using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BankProject.DataProvider;

namespace BankProject.FTTeller
{
    public partial class InwardCashWithdraw_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = TriTT.BINWARD_CASH_WITHDRAW_PreviewList();
        }
        public string geturlReview(string ID,string Status)
        {
            return "Default.aspx?tabid=" + this.TabId.ToString() + "&RefID=" + ID +"&Status="+Status ;
        }
    }
}