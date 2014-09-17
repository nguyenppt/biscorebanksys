using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Import.DocumentaryCredit
{
    public partial class Preview_DocumentProcessing : PortalModuleBase
    {
        protected string lstType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lstType = Request.QueryString["lst"];
        }

        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=" + TabId.ToString() + "&tid=" + id + "&lst=" + lstType;
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string Status = null;
            if (!string.IsNullOrEmpty(lstType)) Status = bd.TransactionStatus.UNA;
            radGridReview.DataSource = bd.IssueLC.ImportLCDocsList(Status, this.TabId);
        }
    }
}