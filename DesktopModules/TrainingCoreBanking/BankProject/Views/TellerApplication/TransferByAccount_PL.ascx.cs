using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

namespace BankProject.Views.TellerApplication
{
    public partial class TransferByAccount_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReferenceNo");
            dt.Columns.Add("SendingName");
            dt.Columns.Add("ReceivingName");
            dt.Columns.Add("Amt");
            dt.Columns.Add("ChargeAmt");
            
            dt.Columns.Add("Status");
            dt.Columns.Add("LCCode");


            DataRow dr = dt.NewRow();
            dr["ReferenceNo"] = "TT/09164/00393";
            dr["SendingName"] = "Đỗ Bảo Lộc";
            dr["ReceivingName"] = "Nguyễn Văn Trung";
            dr["Amt"] = "40,000,000";
            dr["ChargeAmt"] = "20,000";
            
            dr["Status"] = "UNA";
            dr["LCCode"] = "1";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["ReferenceNo"] = "TT/09164/00394";
            dr1["SendingName"] = "Trẩn Bửu Thạch";
            dr1["ReceivingName"] = "Tô Văn Hoa";
            dr1["Amt"] = "5,000,000";
            dr1["ChargeAmt"] = "4,500";
           
            dr1["Status"] = "UNA";
            dr1["LCCode"] = "2";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["ReferenceNo"] = "TT/09164/00395";
            dr2["SendingName"] = "Trần Minh Tâm";
            dr2["ReceivingName"] = "Lý Thánh Tông";
            dr2["Amt"] = "10,000,0000";
            dr2["ChargeAmt"] = "11,000";
          
            dr2["Status"] = "UNA";
            dr2["LCCode"] = "3";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["ReferenceNo"] = "TT/09164/00396";
            dr3["SendingName"] = "Nguyễn Vĩ Minh";
            dr3["ReceivingName"] = "Nguyễn Thị Hoa";
            dr3["Amt"] = "15,000,0000";
            dr3["ChargeAmt"] = "15,000";
            
            dr3["Status"] = "UNA";
            dr3["LCCode"] = "4";
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4["ReferenceNo"] = "TT/09164/00397";
            dr4["SendingName"] = "Lưu Ái Nhi";
            dr4["ReceivingName"] = "Đỗ Tường Vy";
            dr4["Amt"] = "2,000,000";
            dr4["ChargeAmt"] = "3,300";
           
            dr4["Status"] = "UNA";
            dr4["LCCode"] = "5";
            dt.Rows.Add(dr4);

            //DataRow dr5 = dt.NewRow();
            //dr5["ReferenceNo"] = "TT/09164/00393";
            //dr5["SendingName"] = "Đỗ Bảo Lộc";
            //dr5["ReceivingName"] = "Nguyễn Văn Trung";
            //dr5["Amt"] = "40,000,000";
            //dr5["ChargeAmt"] = "20,000";

            //dr5["Status"] = "UNA";
            //dr5["LCCode"] = "1";
            //dt.Rows.Add(dr5);

            //DataRow dr6 = dt.NewRow();
            //dr["ReferenceNo"] = "TT/09164/00394";
            //dr6["SendingName"] = "Trẩn Bửu Thạch";
            //dr6["ReceivingName"] = "Tô Văn Hoa";
            //dr6["Amt"] = "5,000,000";
            //dr6["ChargeAmt"] = "4,500";

            //dr6["Status"] = "UNA";
            //dr6["LCCode"] = "2";
            //dt.Rows.Add(dr6);

            //DataRow dr7 = dt.NewRow();
            //dr7["ReferenceNo"] = "TT/09164/00395";
            //dr7["SendingName"] = "Trần Minh Tâm";
            //dr7["ReceivingName"] = "Lý Thánh Tông";
            //dr7["Amt"] = "10,000,0000";
            //dr7["ChargeAmt"] = "11,000";

            //dr7["Status"] = "UNA";
            //dr7["LCCode"] = "3";
            //dt.Rows.Add(dr7);

            //DataRow dr8 = dt.NewRow();
            //dr8["ReferenceNo"] = "TT/09164/00396";
            //dr8["SendingName"] = "Nguyễn Vĩ Minh";
            //dr8["ReceivingName"] = "Nguyễn Thị Hoa";
            //dr8["Amt"] = "15,000,0000";
            //dr8["ChargeAmt"] = "15,000";

            //dr8["Status"] = "UNA";
            //dr8["LCCode"] = "4";
            //dt.Rows.Add(dr8);

            //DataRow dr9 = dt.NewRow();
            //dr9["ReferenceNo"] = "TT/09164/00397";
            //dr9["SendingName"] = "Lưu Ái Nhi";
            //dr9["ReceivingName"] = "Đỗ Tường Vy";
            //dr9["Amt"] = "2,000,000";
            //dr9["ChargeAmt"] = "3,300";

            //dr4["Status"] = "UNA";
            //dr9["LCCode"] = "5";
            //dt.Rows.Add(dr9);

            radGridReview.DataSource = dt;
        }
        public string geturlReview(string id)
        {

            return "Default.aspx?tabid=" + this.TabId.ToString() + "&LCCode=" + id + "&IsAuthorize=1";
        }
    }
}