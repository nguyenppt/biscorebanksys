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
    public partial class TransferForCreditCardPayment_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGridPreview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerID");
            dt.Columns.Add("CustomerName");
            dt.Columns.Add("DebitAmountLCY");
            dt.Columns.Add("ValueDate");
            dt.Columns.Add("LCCode");

            DataRow dr = dt.NewRow();
            dr["CustomerID"] = "1100001";
            dr["CustomerName"] = "VU THI MY HANH";
            dr["DebitAmountLCY"] = "200,000,000";
            dr["ValueDate"] = "15/03/2014";
            dr["LCCode"] = "0";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["CustomerID"] = "1100002";
            dr1["CustomerName"] = "DINH TIEN HOANG";
            dr1["DebitAmountLCY"] = "300,000,000";
            dr1["ValueDate"] = "10/07/2014";
            dr1["LCCode"] = "1";
            dt.Rows.Add(dr1);
            DataRow dr2 = dt.NewRow();
            dr2["CustomerID"] = "1100003";
            dr2["CustomerName"] = "PHAM NGOC THACH";
            dr2["DebitAmountLCY"] = "400,000,000";
            dr2["ValueDate"] = "19/01/2014";
            dr2["LCCode"] = "2";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["CustomerID"] = "1100004";
            dr3["CustomerName"] = "VO THI SAU";
            dr3["DebitAmountLCY"] = "500,000,000";
            dr3["ValueDate"] = "09/02/2014";
            dr3["LCCode"] = "3";
            dt.Rows.Add(dr3);
            DataRow dr4 = dt.NewRow();
            dr4["CustomerID"] = "1100005";
            dr4["CustomerName"] = "TRUONG CONG DINH";
            dr4["DebitAmountLCY"] = "600,000,000";
            dr4["ValueDate"] = "03/03/2014";
            dr4["LCCode"] = "4";
            dt.Rows.Add(dr4);

            DataRow dr5 = dt.NewRow();
            dr5["CustomerID"] = "2102925";
            dr5["CustomerName"] = "CTY TNHH SONG HONG";
            dr5["DebitAmountLCY"] = "500,000,000";
            dr5["ValueDate"] = "11/01/2014";
            dr5["LCCode"] = "5";
            dt.Rows.Add(dr5);
            DataRow dr6 = dt.NewRow();
            dr6["CustomerID"] = "2102926";
            dr6["CustomerName"] = "CTY TNHH BAS";
            dr6["DebitAmountLCY"] = "200,000,000";
            dr6["ValueDate"] = "12/12/2014";
            dr6["LCCode"] = "6";
            dt.Rows.Add(dr6);

            DataRow dr7 = dt.NewRow();
            dr7["CustomerID"] = "2102927";
            dr7["CustomerName"] = "TRADE CORP";
            dr7["DebitAmountLCY"] = "900,000,000";
            dr7["ValueDate"] = "01/03/2014";
            dr7["LCCode"] = "7";
            dt.Rows.Add(dr7);
            DataRow dr8 = dt.NewRow();
            dr8["CustomerID"] = "2102928";
            dr8["CustomerName"] = "CTY MINH ANH";
            dr8["DebitAmountLCY"] = "100,000,000";
            dr8["ValueDate"] = "07/09/2014";
            dr8["LCCode"] = "8";
            dt.Rows.Add(dr8);

            DataRow dr9 = dt.NewRow();
            dr9["CustomerID"] = "2102929";
            dr9["CustomerName"] = "CTY HOANG VU";
            dr9["DebitAmountLCY"] = "220,000,000";
            dr9["ValueDate"] = "09/07/2014";
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