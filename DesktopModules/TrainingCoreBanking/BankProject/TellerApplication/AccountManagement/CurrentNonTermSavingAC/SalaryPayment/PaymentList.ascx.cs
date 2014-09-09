using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.CurrentNonTermSavingAC.SalaryPayment
{
    public partial class PaymentList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private SavingAccountDAO SavingAccountDAO
        {
            get
            {
                return new SavingAccountDAO();
            }
        }
        private string NonFrequency
        {
            get
            {
                if (Request.QueryString["tabid"] == "138")
                    return "NO";
                return "YES";
            }
        }
        public string TabId
        {
            get
            {
                if (Request.QueryString["id"] != null)
                {
                    return Request.QueryString["id"];
                }
                return "138";
            }
            set
            {
                TabId = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            radGridReview.DataSource = SavingAccountDAO.GetSalaryPaymentFrequency(NonFrequency);
            //radGridReview.DataBind();
        }
    }
}