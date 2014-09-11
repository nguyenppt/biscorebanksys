using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using DotNetNuke.Entities.Modules;
using System.Data;
using BankProject.DataProvider;


namespace BankProject.Views.TellerApplication
{
    public partial class ChequePaytmentStop_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        public void DataPreview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGridDataPreview.DataSource = TriTT.CHEQUE_STOP_Preview_List();
        }
        protected string getUrlPreview(string id)
        {
            return "Default.aspx?tabid=" + this.TabId.ToString() + "&AccountID=" + id + "&IsAuthorize=1";
        }

    }
}