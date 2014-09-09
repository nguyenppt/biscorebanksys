using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class CashWithdrawalsBuyingPreviewList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            /*DataTable dt = new DataTable();
            dt.Columns.Add("ReperenceNo");
            dt.Columns.Add("CustomerAccount");
            dt.Columns.Add("AmtLCY");
            dt.Columns.Add("AmtFCY");
            dt.Columns.Add("Status");
            dt.Columns.Add("LCCode");
            

            DataRow dr = dt.NewRow();
            dr["ReperenceNo"] = "TT/09161/078911";
            dr["CustomerAccount"] = "BANK OF SHANGHAI";
            dr["AmtLCY"] = "2,000,0000";
            dr["AmtFCY"] = "100.00";
            dr["Status"] = "UNA";
            dr["LCCode"] = "1";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["ReperenceNo"] = "TT/09161/078912";
            dr1["CustomerAccount"] = "CITI BANK NEWYORK";
            dr1["AmtLCY"] = "4,000,0000";
            dr1["AmtFCY"] = "200.00";
            dr1["Status"] = "UNA";
            dr1["LCCode"] = "2";
            dt.Rows.Add(dr1);*/

            radGridReview.DataSource = BankProject.DataProvider.Teller.CashWithrawalForBuyingTCList();
        }
        public string geturlReview(string id)
        {

            return "Default.aspx?tabid=" + this.TabId.ToString() + "&LCCode=" + id + "&IsAuthorize=1";
        }
    }
}