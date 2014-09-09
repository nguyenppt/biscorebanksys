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
    public partial class CollateralContingentEntry_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RadGridPreview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CollContEntry");
            dt.Columns.Add("TransCode");
            dt.Columns.Add("AmtLCY");
            dt.Columns.Add("DC");
            dt.Columns.Add("LCCode");

            DataRow dr = dt.NewRow();
            dr["CollContEntry"] = "DC-14190-001-963-063";
            dr["TransCode"] = "901";
            dr["AmtLCY"] = "1,500,000,000";
            dr["DC"] = "D";
            dr["LCCode"] = "0";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["CollContEntry"] = "DC-14170-001-963-053";
            dr1["TransCode"] = "901";
            dr1["AmtLCY"] = "500,000,000";
            dr1["DC"] = "D";
            dr1["LCCode"] = "1";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["CollContEntry"] = "DC-14171-001-963-054";
            dr2["TransCode"] = "901";
            dr2["AmtLCY"] = "600,000,000";
            dr2["DC"] = "D";
            dr2["LCCode"] = "2";
            dt.Rows.Add(dr2);
            DataRow dr3 = dt.NewRow();
            dr3["CollContEntry"] = "DC-14172-001-963-056";
            dr3["TransCode"] = "901";
            dr3["AmtLCY"] = "700,000,000";
            dr3["DC"] = "D";
            dr3["LCCode"] = "3";
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4["CollContEntry"] = "DC-14173-001-963-057";
            dr4["TransCode"] = "901";
            dr4["AmtLCY"] = "770,000,000";
            dr4["DC"] = "D";
            dr4["LCCode"] = "4";
            dt.Rows.Add(dr4);
            DataRow dr5 = dt.NewRow();
            dr5["CollContEntry"] = "DC-14174-001-963-058";
            dr5["TransCode"] = "901";
            dr5["AmtLCY"] = "990,000,000";
            dr5["DC"] = "D";
            dr5["LCCode"] = "5";
            dt.Rows.Add(dr5);

            DataRow dr6 = dt.NewRow();
            dr6["CollContEntry"] = "DC-14190-001-963-059";
            dr6["TransCode"] = "901";
            dr6["AmtLCY"] = "1,000,000,000";
            dr6["DC"] = "D";
            dr6["LCCode"] = "6";
            dt.Rows.Add(dr6);

            DataRow dr7 = dt.NewRow();
            dr7["CollContEntry"] = "DC-14175-001-963-060";
            dr7["TransCode"] = "901";
            dr7["AmtLCY"] = "1,100,000,000";
            dr7["DC"] = "D";
            dr7["LCCode"] = "7";
            dt.Rows.Add(dr7);

            DataRow dr8 = dt.NewRow();
            dr8["CollContEntry"] = "DC-14176-001-963-061";
            dr8["TransCode"] = "901";
            dr8["AmtLCY"] = "1,200,000,000";
            dr8["DC"] = "D";
            dr8["LCCode"] = "8";
            dt.Rows.Add(dr8);

            DataRow dr9 = dt.NewRow();
            dr9["CollContEntry"] = "DC-14177-001-963-062";
            dr9["TransCode"] = "901";
            dr9["AmtLCY"] = "1,300,000,000";
            dr9["DC"] = "D";
            dr9["LCCode"] = "9";
            dt.Rows.Add(dr9);
            RadGridPreview.DataSource = dt;
        }
        public string getUrlPreview(string id)
        {
            return "Default.aspx?tabid=" + this.TabId.ToString() + "&LCCode=" + id + "&IsAuthorize=1";
        }
    }
}