using System;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close.PreClose
{
    public partial class PreClosureFinal : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = ThanhLP.B_BPRECLOSURE_AUTHORISE();
        }

        public string geturlReview(string id,string typeCode)

        {
            string[] param = new string[2];
            param[0] = "CodeID=" + id;
            param[1] = "TypeCode=" + typeCode;
            //return "Default.aspx?tabid=" + this.TabId.ToString() + "&ctl=INPUT&CodeID=" + id + "&TypeCode=" + typeCode;
            return EditUrl("", "", "INPUT", param);
        }
    }
}