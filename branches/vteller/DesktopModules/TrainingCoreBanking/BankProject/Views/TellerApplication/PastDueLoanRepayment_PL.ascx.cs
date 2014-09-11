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
    public partial class PastDueLoanRepayment_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            else 
            {
                if (Request.QueryString["IsAuthorize"] == "2")
                    Response.Redirect(EditUrl("LoanContractReference", Request.QueryString["LoanContractReference"], "PastDueLoanRepayment"));
            }
        }
        protected void RadGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = TriTT.B_BPastDueLoanRepayment_FindItem("", "", "");
        }
        public string geturlReview(string LoanContractReference)
        {
            //return "Default.aspx?tabid=" + this.TabId.ToString() + "&&LoanContractReference=" + LoanContractReference.ToString() + "&&IsAuthorize=2";
            return "Default.aspx?tabid=201&ctl=PastDueLoanRepayment&mid=853" + "&&LoanContractReference=" + LoanContractReference.ToString();
        }
    }
    

    

}