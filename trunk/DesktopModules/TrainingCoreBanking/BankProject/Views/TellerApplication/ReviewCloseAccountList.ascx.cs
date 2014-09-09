using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class ReviewCloseAccountList : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ///this.LoadData();
            }
        }

        private void LoadData()
        {
              radGridReview.DataSource = BankProject.DataProvider.Database.BOPENACCOUNT_CLOSE_GetbyStatus("UNA",this.UserId.ToString());
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.LoadData();
        }
    }
}

