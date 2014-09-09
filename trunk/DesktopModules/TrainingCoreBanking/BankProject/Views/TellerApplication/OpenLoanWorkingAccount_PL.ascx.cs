using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class OpenLoanWorkingAccount_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }
        public void RadGridPreview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if(IsPostBack)
            RadGridPreview.DataSource = TriTT.B_OPEN_LOANWORK_ACCT_List_Preview("AUT");
            else RadGridPreview.DataSource = TriTT.B_OPEN_LOANWORK_ACCT_List_Preview("AUT");
        }
        public string GeturlReview(string ID)
        {
            return string.Format("Default.aspx?tabid=184&mid={0}&AcctID={1}&mode=re_2commit", this.ModuleId,ID);
        }
        
    }
}