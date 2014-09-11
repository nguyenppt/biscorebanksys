using BankProject.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Open
{
    public partial class ArrearNewDeposit : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();
            tbDepositCode.Text = SQLData.B_BMACODE_GetNewID("PERIODIC_LOAN","TT","/");// palment number >> ttnumber
            radAcctNo.Text = "070001688380*" + DateTime.Now.ToString("yyyyMMdd") + "*BO";// refid bo dau
            if (Request.QueryString["Currency"] != null)
            {
                string strCurrency = Request.QueryString["Currency"].ToString();
                hdfCurrency.Value = strCurrency;
                hdfCurrencyText.Value = "Currency = " + strCurrency;
            }
            if (Request.QueryString["CustomerID"] != null)
            {
                hdfCustomerID.Value = Request.QueryString["CustomerID"].ToString();
            }
            if (Request.QueryString["Principal"] != null)
            {
                long Principal = Int64.Parse(Request.QueryString["Principal"].ToString());
                hdfAccount.Value = string.Format("{0:##,#}", Principal);
                hdfAccountText.Value = "Ammount = " + Request.QueryString["Principal"].ToString();
            }
            hdfDateText.Value = "Date   = " + DateTime.Now.ToString("yyyyMMdd");
        }
        
        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;            
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "Preview")
            {
                Response.Redirect(EditUrl("PREVIEW"));
            }
            else if (commandName == "commit")
            {
                Response.Redirect("Default.aspx?tabid=145&Preview=1 ");
            }
        }
        protected void btSearch_Click(object sender, EventArgs e)
        {
            
        }
        
    }
}