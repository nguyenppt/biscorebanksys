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
    public partial class OutwardTransferByCash_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReperenceNo");
            dt.Columns.Add("SendingName");
            dt.Columns.Add("ReceivingName");
            dt.Columns.Add("AmtLCY");
            dt.Columns.Add("ChargeAmtLCY");
            dt.Columns.Add("ChargeVatAmt");
            dt.Columns.Add("Status");
            dt.Columns.Add("LCCode");
            

            DataRow dr = dt.NewRow();
            dr["ReperenceNo"] = "TT/09164/00393";
            dr["SendingName"] = "Đỗ Bảo Lộc";
            dr["ReceivingName"] = "Nguyễn Văn Trung";
            dr["AmtLCY"] = "40,000,000";
            dr["ChargeAmtLCY"] = "20,000";
            dr["ChargeVatAmt"] = "2,000";
            dr["Status"] = "UNA";
            dr["LCCode"] = "1";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["ReperenceNo"] = "TT/09164/00394";
            dr1["SendingName"] = "Trẩn Bửu Thạch";
            dr1["ReceivingName"] = "Tô Văn Hoa";
            dr1["AmtLCY"] = "5,000,000";
            dr1["ChargeAmtLCY"] = "4,500";
            dr1["ChargeVatAmt"] = "450";
            dr1["Status"] = "UNA";
            dr1["LCCode"] = "2";
            dt.Rows.Add(dr1); 

            DataRow dr2 = dt.NewRow();
            dr2["ReperenceNo"] = "TT/09164/00395";
            dr2["SendingName"] = "Trần Minh Tâm";
            dr2["ReceivingName"] = "Lý Thánh Tông";
            dr2["AmtLCY"] = "10,000,0000";
            dr2["ChargeAmtLCY"] = "11,000";
            dr2["ChargeVatAmt"] = "1,100";
            dr2["Status"] = "UNA";
            dr2["LCCode"] = "3";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["ReperenceNo"] = "TT/09164/00396";
            dr3["SendingName"] = "Nguyễn Vĩ Minh";
            dr3["ReceivingName"] = "Nguyễn Thị Hoa";
            dr3["AmtLCY"] = "15,000,0000";
            dr3["ChargeAmtLCY"] = "15,000";
            dr3["ChargeVatAmt"] = "1,500";
            dr3["Status"] = "UNA";
            dr3["LCCode"] = "4";
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4["ReperenceNo"] = "TT/09164/00397";
            dr4["SendingName"] = "Lưu Ái Nhi";
            dr4["ReceivingName"] = "Đỗ Tường Vy";
            dr4["AmtLCY"] = "2,000,000";
            dr4["ChargeAmtLCY"] = "3,300";
            dr4["ChargeVatAmt"] = "330";
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