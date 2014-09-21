using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication.ForeignExchange
{
    public partial class ForeignExchangePreviewList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReperenceNo");
            dt.Columns.Add("CustomerAccount");
            dt.Columns.Add("AmtLCY");
            dt.Columns.Add("AmtFCY");
            dt.Columns.Add("Status");
            dt.Columns.Add("LCCode");
            

            DataRow dr = dt.NewRow();
            dr["ReperenceNo"] = "TT/09161/078911";
            dr["CustomerAccount"] = "Phan Van Han";
            dr["AmtLCY"] = "17,791,0000";
            dr["AmtFCY"] = "1,000.00";
            dr["Status"] = "UNA";
            dr["LCCode"] = "1";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["ReperenceNo"] = "TT/09161/078912";
            dr1["CustomerAccount"] = "Pham Ngoc Thach";
            dr1["AmtLCY"] = "35,582,0000";
            dr1["AmtFCY"] = "2,000.00";
            dr1["Status"] = "UNA";
            dr1["LCCode"] = "2";
            dt.Rows.Add(dr1);

            radGridReview.DataSource = dt;
        }
        public string geturlReview(string id)
        {

            return "Default.aspx?tabid=" + this.TabId.ToString() + "&LCCode=" + id + "&IsAuthorize=1";
        }
    }
}