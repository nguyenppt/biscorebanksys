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
        public bool isAmendPage = false;
        public string page = "196";
        string key = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect(EditUrl("NormalLoan"));
            if (Request.QueryString["key"] != null)
            {
                key = Request.QueryString["key"];
            }
            key = "";
            if (Request.Params["tabid"] != null)
            {
                if (Request.Params["tabid"] == "202")
                {
                    isAmendPage = true;
                    page = "202";
                }
            }
        }

        private void LoadData()
        {
            if (isAmendPage)
            {
                radGridReview.DataSource = BankProject.DataProvider.Database.BNEWNORMALLOAN_GetbyStatus("AUT", this.UserId.ToString());
            }
            else
            {
                radGridReview.DataSource = BankProject.DataProvider.Database.BNEWNORMALLOAN_GetbyStatus("UNA", this.UserId.ToString());
                //radGridReview.DataBind();
            }
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.LoadData();
        }
       
    }
}