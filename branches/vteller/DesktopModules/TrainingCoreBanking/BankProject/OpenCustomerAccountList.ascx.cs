using DotNetNuke.Entities.Modules;
using System;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class OpenCustomerAccountList : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        private void LoadData()
        {
            radGridReview.DataSource = BankProject.DataProvider.Database.BOPENACCOUNT_GetbyStatus("UNA", "OPEN", this.UserId.ToString());
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.LoadData();
        }
    }
}