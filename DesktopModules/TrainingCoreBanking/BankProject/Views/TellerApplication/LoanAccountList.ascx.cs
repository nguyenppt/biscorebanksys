using BankProject.Business;
using BankProject.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class LoanAccountList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        public string page = "196";
        INewNormalLoanBusiness<BNEWNORMALLOAN> loanBusiness;
        List<BNEWNORMALLOAN> listOfLoan = new List<BNEWNORMALLOAN>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["tabid"] != null)
            {
                if (Request.Params["tabid"] == "202")
                {
                    page = "202";
                    loanBusiness = new NewNormalLoanAmendBusiness();
                }
                else
                {
                    loanBusiness = new NewNormalLoanBusiness();
                }
            }
        }

        private void LoadData()
        {
            loanBusiness.loadEntrities(ref listOfLoan);
            radGridReview.DataSource = listOfLoan;
            
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.LoadData();
        }
       
    }
}