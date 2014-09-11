using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class LoanAccountList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        string key = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect(EditUrl("NormalLoan"));
            if (Request.QueryString["key"] != null)
            {
                key = Request.QueryString["key"];
            }
            key = "";
        }

        private void LoadData()
        {
            radGridReview.DataSource = BankProject.DataProvider.Database.BNEWNORMALLOAN_GetbyStatus("UNA",this.UserId.ToString());
            //radGridReview.DataBind();
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.LoadData();
        }
       
    }
}