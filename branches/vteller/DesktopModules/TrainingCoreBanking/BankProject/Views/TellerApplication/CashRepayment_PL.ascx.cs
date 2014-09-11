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
    public partial class CashRepayment_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RadGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        { 
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerAccount");
            dt.Columns.Add("CashAccount");
            dt.Columns.Add("AmtDeposited");
            dt.Columns.Add("PLCode");

            DataRow dr = dt.NewRow();
            dr["CustomerAccount"] = "VND - 080002595838";
            dr["CashAccount"] = "VND - 060002595838";
            dr["AmtDeposited"] = "9,000,000";
            dr["PLCode"] = "0";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["CustomerAccount"] = "VND - 080002595928";
            dr1["CashAccount"] = "VND - 060002595928";
            dr1["AmtDeposited"] = "10,000,000";
            dr1["PLCode"] = "1";
            dt.Rows.Add(dr1);
            DataRow dr2 = dt.NewRow();
            dr2["CustomerAccount"] = "VND - 080002595948";
            dr2["CashAccount"] = "VND - 060002595948";
            dr2["AmtDeposited"] = "20,000,000";
            dr2["PLCode"] = "2";
            dt.Rows.Add(dr2);
            DataRow dr3 = dt.NewRow();
            dr3["CustomerAccount"] = "VND - 080002595968";
            dr3["CashAccount"] = "VND - 060002595968";
            dr3["AmtDeposited"] = "30,000,000";
            dr3["PLCode"] = "3";
            dt.Rows.Add(dr3);
            DataRow dr4 = dt.NewRow();
            dr4["CustomerAccount"] = "VND - 080002596268";
            dr4["CashAccount"] = "VND - 060002596268";
            dr4["AmtDeposited"] = "40,000,000";
            dr4["PLCode"] = "4";
            dt.Rows.Add(dr4);
            DataRow dr5 = dt.NewRow();
            dr5["CustomerAccount"] = "VND - 080002596358";
            dr5["CashAccount"] = "VND - 060002596358";
            dr5["AmtDeposited"] = "50,000,000";
            dr5["PLCode"] = "5";
            dt.Rows.Add(dr5);

            DataRow dr6 = dt.NewRow();
            dr6["CustomerAccount"] = "VND - 080002596448";
            dr6["CashAccount"] = "VND - 060002596448";
            dr6["AmtDeposited"] = "60,000,000";
            dr6["PLCode"] = "6";
            dt.Rows.Add(dr6);
            DataRow dr7 = dt.NewRow();
            dr7["CustomerAccount"] = "VND - 080002596538";
            dr7["CashAccount"] = "VND - 060002596538";
            dr7["AmtDeposited"] = "70,000,000";
            dr7["PLCode"] = "7";
            dt.Rows.Add(dr7);

            DataRow dr8 = dt.NewRow();
            dr8["CustomerAccount"] = "VND - 080002596628";
            dr8["CashAccount"] = "VND - 060002596628";
            dr8["AmtDeposited"] = "80,000,000";
            dr8["PLCode"] = "8";
            dt.Rows.Add(dr8);
            RadGrid.DataSource = dt;
        }
        protected string getUrlPreview(string PRCode)
        { 
            return "Default.aspx?tabid=" +this.TabId.ToString() +"&PLCode=" + PRCode + "&IsAuthorize=1";
        }
    }
}