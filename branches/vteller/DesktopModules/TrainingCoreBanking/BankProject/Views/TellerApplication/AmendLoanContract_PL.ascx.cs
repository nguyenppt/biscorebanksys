using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BankProject.DataProvider;

namespace BankProject.Views.TellerApplication
{
    public partial class AmendLoanContract_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //else
            //{
            //    if (Request.QueryString["IsAuthorize"] == "2")
            //        Response.Redirect(EditUrl("LoanContractReference", Request.QueryString["LoanContractReference"], "AmendLoanContract_Item"));
            //}
        }
        protected void RadGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = TriTT.B_BPastDueLoanRepayment_FindItem("", "", "");
        }
        public string geturlReview(string LoanContractReference)
        {
            
            //return "Default.aspx?tabid=" + this.TabId.ToString() + "&&LoanContractReference=" + LoanContractReference.ToString() + "&&IsAuthorize=2";
            return "Default.aspx?tabid=202&ctl=AmendLoanContract_Item&mid=821&" + "&&LoanContractReference=" + LoanContractReference.ToString();
            // cau lenh o tren chuyen thang qua Url khac, khong can phai xet dieu kien if cua IsAuthorize
        }
    }
}