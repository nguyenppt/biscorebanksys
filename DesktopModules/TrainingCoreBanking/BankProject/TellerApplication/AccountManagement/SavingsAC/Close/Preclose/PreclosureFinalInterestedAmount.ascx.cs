using BankProject.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close.Preclose
{
    public partial class PreclosureFinalInterestedAmount : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        string TypeCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();

            if (Request.QueryString["CodeID"] != null)
                tbDepositCode.Text = Request.QueryString["CodeID"].ToString();

            if (Request.QueryString["TypeCode"] != null)
                TypeCode = Request.QueryString["TypeCode"].ToString();

            hdfPreclosureType.Value = TypeCode;

            dtpValueDate.SelectedDate = DateTime.Now;

            setHardValueForm();

            disableForm();
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
            if (commandName == "commit")
            {
                string[] param = new string[4];
                param[0] = "CodeID=" + tbDepositCode.Text;
                param[1] = "TypeCode=" + hdfPreclosureType.Value;
                param[2] = "TotalAmt=" + hdfTotalAmt.Value;
                param[3] = "TellerTrans=" + lblNextTeller.Text;
                Response.Redirect(EditUrl("", "", "WITHDRAW", param));
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            
        }

        private void disableForm()
        {
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            tbDepositCode.Enabled = true;
            tbDepositCode.Focus();
        }

        private void setHardValueForm()
        {
            string nextTellerTrans = SQLData.B_BMACODE_GetNewID("PERIODIC_LOAN", "TT", "/");
            lblNextTeller.Text = nextTellerTrans;
            if (TypeCode == "1")
            {
                lblCustomer.Text = "134494 - NGUYEN VAN HAI";
                lblCategory.Text = "8100 - Traditional Savings INT in Arrears";
                lblCurrency.Text = "VND";
                lblProductCode.Text = "100 - Traditional FD - Arrears Ind";
                lblOpenDate.Text = DateTime.Now.AddMonths(-11).AddDays(2).ToString("dd/MM/yyyy");
                lblMaturityDate.Text = DateTime.Now.AddMonths(1).AddDays(2).ToString("dd/MM/yyyy");
                lblPrincipal.Text = "0";
                lblOrgPrincipal.Text = "50,000,000";
                lblInterestedRate.Text = "18.06";
            }
            else if (TypeCode == "2")
            {
                lblCustomer.Text = "136336 - VO THI HOA";
                lblCategory.Text = "8-150 - Traditional Savings INT Periodic";
                lblCurrency.Text = "VND";
                lblProductCode.Text = "200 - Tradi FD - Periodic Monthly";
                lblOpenDate.Text = DateTime.Now.AddMonths(-1).AddDays(2).ToString("dd/MM/yyyy");
                lblMaturityDate.Text = DateTime.Now.AddMonths(3).AddDays(2).ToString("dd/MM/yyyy");
                lblPrincipal.Text = "0";
                lblOrgPrincipal.Text = "250,000,000";
                lblInterestedRate.Text = "16.152";
            }
            else
            {
                lblCustomer.Text = "136336 - VO THI HOA";
                lblCategory.Text = "8-150 - Traditional Savings INT Periodic";
                lblCurrency.Text = "VND";
                lblProductCode.Text = "300 - Tradi - FD-Periodic Quaterly";
                lblOpenDate.Text = DateTime.Now.AddMonths(-1).AddDays(2).ToString("dd/MM/yyyy");
                lblMaturityDate.Text = DateTime.Now.AddMonths(3).AddDays(2).ToString("dd/MM/yyyy");
                lblPrincipal.Text = "0";
                lblOrgPrincipal.Text = "250,000,000";
                lblInterestedRate.Text = "16.152";
            }
        }
    }
}