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
    public partial class BlockAccountList : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {

            radGridReview.DataSource = BankProject.DataProvider.Database.BOPENACCOUNT_BLOCK_GetbyStatus("UNA", this.UserId.ToString());
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {            
            this.LoadData();
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {           
        }
    }
}