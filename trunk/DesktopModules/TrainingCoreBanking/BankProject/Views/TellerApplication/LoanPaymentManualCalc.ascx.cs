using BankProject.DBRespository;
using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankProject.Views.TellerApplication
{
    public partial class LoanPaymentManualCalculated : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btCalcu_Click(object sender, EventArgs e)
        {
            StoreProRepository facade = new StoreProRepository();
            facade.StoreProcessor().B_Normal_Loan_Process_Payment(dtpDate.SelectedDate);
        }
    }
}