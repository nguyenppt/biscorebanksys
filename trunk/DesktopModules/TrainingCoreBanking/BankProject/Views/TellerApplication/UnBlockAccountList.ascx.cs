using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class UnBlockAccountList : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            if (Request.QueryString["preview"] == null)
            {
                Repository.ListItemRepository data = new Repository.ListItemRepository();
                radGridReview.DataSource = data.GetBlockedAccountList();
            }
            else
            {
                Repository.ListItemRepository data = new Repository.ListItemRepository();
                radGridReview.DataSource = data.GetPreviewBlockedAccountList();
            }
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.LoadData();
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            string urlFTAccountClose = this.EditUrl("UnBlockAccount");
            this.Response.Redirect(urlFTAccountClose);
        }
    }
}