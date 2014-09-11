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
    public partial class OpenRevolvingCommContract_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RadGridPreview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RevComContNo");
            dt.Columns.Add("CustomerID");
            dt.Columns.Add("CommtAmt");
            dt.Columns.Add("StartDate");
            dt.Columns.Add("LCCode");

            DataRow dr = dt.NewRow();
            dr["RevComContNo"] = "LD/14189/00089";
            dr["CustomerID"] = "1100001";
            dr["CommtAmt"] = "200,000,000";
            dr["StartDate"] = "09/07/2014";
            dr["LCCode"]="1";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["RevComContNo"] = "LD/14169/00080";
            dr1["CustomerID"] = "1100002";
            dr1["CommtAmt"] = "2,000,000,000";
            dr1["StartDate"] = "05/06/2014";
            dr1["LCCode"] = "2";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["RevComContNo"] = "LD/14159/00066";
            dr2["CustomerID"] = "1100003";
            dr2["CommtAmt"] = "950,000,000";
            dr2["StartDate"] = "04/05/2014";
            dr2["LCCode"] = "3";
            dt.Rows.Add(dr2);
            RadGridPreview.DataSource = dt;

            DataRow dr3 = dt.NewRow();
            dr3["RevComContNo"] = "LD/14160/00067";
            dr3["CustomerID"] = "1100004";
            dr3["CommtAmt"] = "95,000,000";
            dr3["StartDate"] = "05/05/2014";
            dr3["LCCode"] = "4";
            dt.Rows.Add(dr3);
            RadGridPreview.DataSource = dt;

            DataRow dr4 = dt.NewRow();
            dr4["RevComContNo"] = "LD/14161/00068";
            dr4["CustomerID"] = "1100005";
            dr4["CommtAmt"] = "450,000,000";
            dr4["StartDate"] = "06/05/2014";
            dr4["LCCode"] = "5";
            dt.Rows.Add(dr4);
            RadGridPreview.DataSource = dt;

            DataRow dr5 = dt.NewRow();
            dr5["RevComContNo"] = "LD/14162/00069";
            dr5["CustomerID"] = "2102925";
            dr5["CommtAmt"] = "150,000,000";
            dr5["StartDate"] = "09/05/2014";
            dr5["LCCode"] = "6";
            dt.Rows.Add(dr5);
            RadGridPreview.DataSource = dt;

            DataRow dr6 = dt.NewRow();
            dr6["RevComContNo"] = "LD/14163/00070";
            dr6["CustomerID"] = "2102926";
            dr6["CommtAmt"] = "250,000,000";
            dr6["StartDate"] = "11/05/2014";
            dr6["LCCode"] = "7";
            dt.Rows.Add(dr6);
            RadGridPreview.DataSource = dt;

            DataRow dr7 = dt.NewRow();
            dr7["RevComContNo"] = "LD/14164/00071";
            dr7["CustomerID"] = "2102927";
            dr7["CommtAmt"] = "300,000,000";
            dr7["StartDate"] = "12/05/2014";
            dr7["LCCode"] = "8";
            dt.Rows.Add(dr7);
            RadGridPreview.DataSource = dt;

            DataRow dr8 = dt.NewRow();
            dr8["RevComContNo"] = "LD/14165/00072";
            dr8["CustomerID"] = "2102928";
            dr8["CommtAmt"] = "950,000,000";
            dr8["StartDate"] = "12/05/2014";
            dr8["LCCode"] = "9";
            dt.Rows.Add(dr8);
            RadGridPreview.DataSource = dt;

            DataRow dr9 = dt.NewRow();
            dr9["RevComContNo"] = "LD/14166/00066";
            dr9["CustomerID"] = "2102929";
            dr9["CommtAmt"] = "550,000,000";
            dr9["StartDate"] = "12/10/2014";
            dr9["LCCode"] = "10";
            dt.Rows.Add(dr9);
            RadGridPreview.DataSource = dt;

        }
        public string geturlPreview(string id)
        {
            return "Default.aspx?tabid=" + this.TabId.ToString() + "&LCCode=" + id + "&IsAuthorize=1";
        }
    }
}