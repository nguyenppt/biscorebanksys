using System;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.OverseasTransfers
{
    public partial class ListReview : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = SQLData.B_BOVERSEASTRANSFER_GetByReview(UserId.ToString());
        }
    }
}