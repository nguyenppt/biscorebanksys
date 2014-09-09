using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

namespace BankProject
{
    public partial class InputCollateralInformation_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RadGridPreview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CollInfoNo");
            dt.Columns.Add("CollType");
            dt.Columns.Add("CollStatus");
            dt.Columns.Add("ExecValue");
            dt.Columns.Add("LCCode");

            DataRow dr = dt.NewRow();
            dr["CollInfoNo"] = "900756.1.1";
            dr["CollType"] = "351";
            dr["CollStatus"] = "00 - NORMAL";
            dr["ExecValue"] = "100,000,000";
            dr["LCCode"] = "0";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["CollInfoNo"] = "900757.1.1";
            dr1["CollType"] = "352";
            dr1["CollStatus"] = "00 - NORMAL";
            dr1["ExecValue"] = "200,000,000";
            dr1["LCCode"] = "1";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["CollInfoNo"] = "900759.1.1";
            dr2["CollType"] = "353";
            dr2["CollStatus"] = "01 - RELEASED";
            dr2["ExecValue"] = "300,000,000";
            dr2["LCCode"] = "2";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["CollInfoNo"] = "900760.1.1";
            dr3["CollType"] = "354";
            dr3["CollStatus"] = "01 - RELEASED";
            dr3["ExecValue"] = "400,000,000";
            dr3["LCCode"] = "3";
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4["CollInfoNo"] = "900761.1.1";
            dr4["CollType"] = "355";
            dr4["CollStatus"] = "02 - CUSTODY";
            dr4["ExecValue"] = "500,000,000";
            dr4["LCCode"] = "4";
            dt.Rows.Add(dr4);

            DataRow dr5 = dt.NewRow();
            dr5["CollInfoNo"] = "900762.1.1";
            dr5["CollType"] = "356";
            dr5["CollStatus"] = "02 - CUSTODY";
            dr5["ExecValue"] = "600,000,000";
            dr5["LCCode"] = "5";
            dt.Rows.Add(dr5);

            DataRow dr6 = dt.NewRow();
            dr6["CollInfoNo"] = "900763.1.1";
            dr6["CollType"] = "357";
            dr6["CollStatus"] = "00 - NORMAL";
            dr6["ExecValue"] = "700,000,000";
            dr6["LCCode"] = "6";
            dt.Rows.Add(dr6);

            DataRow dr7 = dt.NewRow();
            dr7["CollInfoNo"] = "900764.1.1";
            dr7["CollType"] = "358";
            dr7["CollStatus"] = "00 - NORMAL";
            dr7["ExecValue"] = "700,000,000";
            dr7["LCCode"] = "7";
            dt.Rows.Add(dr7);

            DataRow dr8 = dt.NewRow();
            dr8["CollInfoNo"] = "900765.1.1";
            dr8["CollType"] = "359";
            dr8["CollStatus"] = "01 - RELEASED";
            dr8["ExecValue"] = "900,000,000";
            dr8["LCCode"] = "8";
            dt.Rows.Add(dr8);

            DataRow dr9 = dt.NewRow();
            dr9["CollInfoNo"] = "900766.1.1";
            dr9["CollType"] = "359";
            dr9["CollStatus"] = "00 - NORMAL";
            dr9["ExecValue"] = "200,000,000";
            dr9["LCCode"] = "0";
            dt.Rows.Add(dr9);

            RadGridPreview.DataSource = dt;


        }
        public string getUrlPreview(string id)
        {
            return "Default.aspx?tabid=" + this.TabId.ToString() + "&LCCode=" + id + "&IsAuthorize=1";
        }
    }
}