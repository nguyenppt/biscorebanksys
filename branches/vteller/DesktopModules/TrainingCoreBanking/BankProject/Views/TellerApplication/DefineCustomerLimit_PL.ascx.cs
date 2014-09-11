using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class DefineCustomerLimit_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RadGridPreview_onNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        { 
            DataTable dt = new DataTable();
            dt.Columns.Add("ReferenceNo");
            dt.Columns.Add("ApprovedDate");
            dt.Columns.Add("LimitAmt");
            dt.Columns.Add("FV");
            dt.Columns.Add("LCCode");

            DataRow dr = dt.NewRow();
            dr["ReferenceNo"] = "100026.0010000.01";
            dr["ApprovedDate"] = "01/01/2014";
            dr["LimitAmt"] = "1,000,000,000";
            dr["FV"] = "Fixed";
            dr["LCCode"] = "0";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["ReferenceNo"] = "100027.0010000.01";
            dr1["ApprovedDate"] = "02/02/2014";
            dr1["LimitAmt"] = "2,000,000,000";
            dr1["FV"] = "Fixed";
            dr1["LCCode"] = "1";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["ReferenceNo"] = "100028.0010000.01";
            dr2["ApprovedDate"] = "03/03/2014";
            dr2["LimitAmt"] = "3,000,000,000";
            dr2["FV"] = "Fixed";
            dr2["LCCode"] = "2";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["ReferenceNo"] = "100029.0010000.01";
            dr3["ApprovedDate"] = "04/04/2014";
            dr3["LimitAmt"] = "4,000,000,000";
            dr3["FV"] = "Fixed";
            dr3["LCCode"] = "3";
            dt.Rows.Add(dr3);
            DataRow dr4 = dt.NewRow();
            dr4["ReferenceNo"] = "100030.0010000.01";
            dr4["ApprovedDate"] = "05/05/2014";
            dr4["LimitAmt"] = "4,000,000,000";
            dr4["FV"] = "Fixed";
            dr4["LCCode"] = "4";
            dt.Rows.Add(dr4);
            DataRow dr5 = dt.NewRow();
            dr5["ReferenceNo"] = "100031.0010000.01";
            dr5["ApprovedDate"] = "03/03/2014";
            dr5["LimitAmt"] = "5,000,000,000";
            dr5["FV"] = "Fixed";
            dr5["LCCode"] = "5";
            dt.Rows.Add(dr5);
            DataRow dr6 = dt.NewRow();
            dr6["ReferenceNo"] = "100032.0020000.01";
            dr6["ApprovedDate"] = "30/03/2014";
            dr6["LimitAmt"] = "3,500,000,000";
            dr6["FV"] = "Variable";
            dr6["LCCode"] = "6";
            dt.Rows.Add(dr6);
            DataRow dr7 = dt.NewRow();
            dr7["ReferenceNo"] = "100033.0020000.01";
            dr7["ApprovedDate"] = "03/04/2014";
            dr7["LimitAmt"] = "6,000,000,000";
            dr7["FV"] = "Variable";
            dr7["LCCode"] = "7";
            dt.Rows.Add(dr7);

            DataRow dr8 = dt.NewRow();
            dr8["ReferenceNo"] = "100034.0020000.01";
            dr8["ApprovedDate"] = "03/03/2014";
            dr8["LimitAmt"] = "3,300,000,000";
            dr8["FV"] = "Variable";
            dr8["LCCode"] = "8";
            dt.Rows.Add(dr8);

            DataRow dr9 = dt.NewRow();
            dr9["ReferenceNo"] = "100035.0010000.01";
            dr9["ApprovedDate"] = "05/05/2014";
            dr9["LimitAmt"] = "7,000,000,000";
            dr9["FV"] = "Fixed";
            dr9["LCCode"] = "9";
            dt.Rows.Add(dr9);
            RadGridPreview.DataSource = dt;

        }
        public string geturlPreview(string id)
        {
            return "Default.aspx?tabid=" + this.TabId.ToString() + "&LCCode=" + id + "&IsAuthorize=1";
        }
    }
}