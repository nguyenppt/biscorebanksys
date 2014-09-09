using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using BankProject.DataProvider;

namespace BankProject.Views.TellerApplication
{
    public partial class ChequeReturned_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        
        protected void RadGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid.DataSource = TriTT.B_CHEQUE_RETURN_Preview_List(null, null);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            else
            {
                if (Request.QueryString["RefCheque"] != null)
                { Response.Redirect(EditUrl("RefCheque", Request.QueryString["RefCheque"], "ChequeReturned2")); }
            }
        }
        public string getUrlPreview(string RefCheque, string ReturnedCheque)
        {
            return "Default.aspx?tabid=136&&ctl=ChequeReturned2&mid=837" + "&RefCheque=" + RefCheque + "&ReturnedCheque=" + ReturnedCheque;
        }


    }
}