using BankProject.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close
{
    public partial class PreclosureHoldings : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        string TypeCode = "";
        double totalAmt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();

            if (Request.QueryString["CodeID"] != null)
                tbDepositCode.Text = Request.QueryString["CodeID"].ToString();
            
            tbDepositCode.Focus();
        }
        
        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btHold").Visible = false; 
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            
        }
    }
}