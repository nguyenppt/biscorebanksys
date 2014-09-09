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
    public partial class CollectionForCreditCardPayment_PL : DotNetNuke.Entities.Modules.PortalModuleBase
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
            dr["ReperenceNo"] = "TT/09161/07929";
            dr["CustomerAccount"] = "Phan Van Han";
            dr["AmtLCY"] = "213,600,000";
            dr["AmtFCY"] = "10,000.00";
            dr["Status"] = "UNA";
            dr["LCCode"] = "1";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["ReperenceNo"] = "TT/09161/07930";
            dr1["CustomerAccount"] = "Pham Ngoc Thach";
            dr1["AmtLCY"] = "427,400,000";
            dr1["AmtFCY"] = "20,000.00";
            dr1["Status"] = "UNA";
            dr1["LCCode"] = "2";
            dt.Rows.Add(dr1);

            DataRow dr4 = dt.NewRow();
            dr4["ReperenceNo"] = "TT/09161/07931";
            dr4["CustomerAccount"] = "Truong Cong Dinh";
            dr4["AmtLCY"] = "42,740,000";
            dr4["AmtFCY"] = "2,000.00";
            dr4["Status"] = "UNA";
            dr4["LCCode"] = "3";
            dt.Rows.Add(dr4);

            DataRow dr2 = dt.NewRow();
            dr2["ReperenceNo"] = "TT/09161/07932";
            dr2["CustomerAccount"] = "Vo Thi Sau";
            dr2["AmtLCY"] = "320,400,000";
            dr2["AmtFCY"] = "15,000.00";
            dr2["Status"] = "UNA";
            dr2["LCCode"] = "4";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["ReperenceNo"] = "TT/09161/07933";
            dr3["CustomerAccount"] = "Dinh Tien Hoang";
            dr3["AmtLCY"] = "192,240,000";
            dr3["AmtFCY"] = "9,000.00";
            dr3["Status"] = "UNA";
            dr3["LCCode"] = "5";
            dt.Rows.Add(dr3);
            radGridReview.DataSource = dt;
        }
        public string geturlReview(string id)
        {

            return "Default.aspx?tabid=" + this.TabId.ToString() + "&LCCode=" + id + "&IsAuthorize=1";
        }
    }
}