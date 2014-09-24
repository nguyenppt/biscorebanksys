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
    public partial class CashRepayment_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }
        protected void RadGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid.DataSource = TriTT.B_CASHREPAYMENT_PreviewList();
        }
        protected string getUrlPreview(string ID)
        { 
            return "Default.aspx?tabid=" +this.TabId.ToString() +"&ID=" + ID ;
        }
    }
}