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
    public partial class ExchangeBanknotesManyDeno_PL : DotNetNuke.Entities.Modules.PortalModuleBase
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
            dr["ReperenceNo"] = "TT/09224/00567";
            dr["CustomerAccount"] = "Nguyễn Văn Trung";
            dr["AmtLCY"] = "40,000,0000";
            dr["AmtFCY"] = "2,000.00";
            dr["Status"] = "UNA";
            dr["LCCode"] = "1";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["ReperenceNo"] = "TT/09224/00568";
            dr1["CustomerAccount"] = "Tô Văn Hoa";
            dr1["AmtLCY"] = "5,000,0000";
            dr1["AmtFCY"] = "250.00";
            dr1["Status"] = "UNA";
            dr1["LCCode"] = "2";
            dt.Rows.Add(dr1); 

            DataRow dr2 = dt.NewRow();
            dr2["ReperenceNo"] = "TT/09224/00569";
            dr2["CustomerAccount"] = "Lý Thánh Tông";
            dr2["AmtLCY"] = "10,000,0000";
            dr2["AmtFCY"] = "500.00";
            dr2["Status"] = "UNA";
            dr2["LCCode"] = "3";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["ReperenceNo"] = "TT/09224/00570";
            dr3["CustomerAccount"] = "Nguyễn Thị Hoa";
            dr3["AmtLCY"] = "15,000,0000";
            dr3["AmtFCY"] = "750.00";
            dr3["Status"] = "UNA";
            dr3["LCCode"] = "4";
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4["ReperenceNo"] = "TT/09224/00571";
            dr4["CustomerAccount"] = "Đỗ Tường Vy";
            dr4["AmtLCY"] = "2,000,0000";
            dr4["AmtFCY"] = "100.00";
            dr4["Status"] = "UNA";
            dr4["LCCode"] = "5";
            dt.Rows.Add(dr4);
            radGridReview.DataSource = dt;
        }
        public string geturlReview(string id)
        {

            return "Default.aspx?tabid=" + this.TabId.ToString() + "&LCCode=" + id + "&IsAuthorize=1";
        }
    }
}