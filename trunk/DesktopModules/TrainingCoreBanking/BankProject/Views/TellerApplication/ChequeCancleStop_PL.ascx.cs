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
    public partial class ChequeCancleStop_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack) return;
        }
        protected void RadGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid.DataSource = TriTT.CHEQUE_CANCLE_STOP_Preview_List();
        }
        protected string getUrlPreview(string AccountID, string SerialNo)
        {
            return "Default.aspx?tabid=" + this.TabId.ToString() + "&AccountID=" + AccountID + "&SerialNo="+ SerialNo ;
        }
    }
}