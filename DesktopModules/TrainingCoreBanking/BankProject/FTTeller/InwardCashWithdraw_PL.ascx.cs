using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.FTTeller
{
    public partial class InwardCashWithdraw_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReperenceNo");
            dt.Columns.Add("BOName");
            dt.Columns.Add("FOName");
            dt.Columns.Add("DebitAmtLCY");
            dt.Columns.Add("CrbitAmtLCY");
            dt.Columns.Add("Status");
            dt.Columns.Add("LCCode");
            

            DataRow dr = dt.NewRow();
            dr["ReperenceNo"] = "TT/09164/00393";
            dr["BOName"] = "Đỗ Bảo Lộc";
            dr["FOName"] = "Nguyễn Văn Trung";
            dr["DebitAmtLCY"] = "9,000,000";
            dr["CrbitAmtLCY"] = "9,000,000";
            dr["Status"] = "UNA";
            dr["LCCode"] = "1";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["ReperenceNo"] = "TT/09164/00394";
            dr1["BOName"] = "Trẩn Bửu Thạch";
            dr1["FOName"] = "Tô Văn Hoa";
            dr1["DebitAmtLCY"] = "10,000,000";
            dr1["CrbitAmtLCY"] = "10,000,000";
            dr1["Status"] = "UNA";
            dr1["LCCode"] = "2";
            dt.Rows.Add(dr1); 

            DataRow dr2 = dt.NewRow();
            dr2["ReperenceNo"] = "TT/09164/00395";
            dr2["BOName"] = "Trần Minh Tâm";
            dr2["FOName"] = "Lý Thánh Tông";
            dr2["DebitAmtLCY"] = "15,000,0000";
            dr2["CrbitAmtLCY"] = "15,000,0000";
            dr2["Status"] = "UNA";
            dr2["LCCode"] = "3";
            dt.Rows.Add(dr2);

            radGridReview.DataSource = dt;
        }
        public string geturlReview(string id)
        {

            return "Default.aspx?tabid=" + this.TabId.ToString() + "&LCCode=" + id + "&IsAuthorize=1";
        }
    }
}