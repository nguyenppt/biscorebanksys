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
    public partial class ArrearInput : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();

            dtpValueDate.SelectedDate = DateTime.Now;
            
            if (Request.QueryString["codeid"] != null)
                tbDepositCode.Text = Request.QueryString["codeid"].ToString();
            else
            {
                DataSet ds = DataProvider.DataTam.B_BDEPOSITACCTS_GetArrearID();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tbDepositCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                }
            }
            if (Request.QueryString["CustomerID"] != null) { 
                lblCustomer.Text = Request.QueryString["CustomerID"].ToString();
                lblWokingAccName.Text = Request.QueryString["CustomerID"].ToString();
            }
            if (Request.QueryString["Category"] != null)
                lblCategory.Text = Request.QueryString["Category"].ToString();
            if (Request.QueryString["Currency"] != null)
                lblCurrency.Text = Request.QueryString["Currency"].ToString();
            //if (Request.QueryString["disable"] != null)
            //    disableForm();

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
                param[0] = "CustomerID=" + lblCustomer.Text;
                param[1] = "Category=" + lblCategory.Text;
                param[2] = "Currency=" + lblCurrency.Text;
                param[3] = "Principal=" + radNumPrincipal.Text;
                Response.Redirect(EditUrl("", "", "NEW", param));
            }

        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            DataSet ds = DataProvider.Database.B_BACCOUNTS_GetbyID(tbDepositCode.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {

            }
        }

        //private void disableForm()
        //{
        //    RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
        //    RadToolBar1.FindItemByValue("btPreview").Enabled = false;
        //    RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
        //    RadToolBar1.FindItemByValue("btReverse").Enabled = true;
        //    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
        //    RadToolBar1.FindItemByValue("btPrint").Enabled = false;

        //    setHardValueForm();

        //    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
        //}

        //private void setHardValueForm()
        //{
        //    lblCustomer.Text = "000001 - CTY TNHH SONG HONG";
        //    lblCategory.Text = "8-150 - Traditional Savings INT in Periodic";
        //    lblCurrency.Text = "VND";
        //    rcbProduct.SelectedIndex = 1;
        //    radNumPrincipal.Text = "1000000000";
        //    dtpValueDate.SelectedDate = DateTime.Now;
        //    rcbTerm.SelectedValue = "15";
        //    dtpMaturityDate.SelectedDate = DateTime.Now.AddMonths(15);
        //    tbInterestRate.Text = "7.872";
        //    rcbWorkingAcc.SelectedValue = "1";
        //    rcbSchedules.SelectedValue = "1";
        //    rcbSchType.SelectedValue = "1";
        //    DateTime freq = DateTime.Now.AddMonths(15).AddDays(-1);
        //    tbFrequency.Text = freq.ToString("yyyyMMdd") + "M0310";

        //    lblOverrides.Text = "";
        //    lblRecordStatus.Text = "INAU";
        //    lblCurrNumber.Text = "1";
        //    lblInputter.Text = "50_ID0296_I_INAU";
        //    lblAuthoriser.Text = "";
        //    lblDateTime.Text = "";
        //    lblDateTime2.Text = "";
        //    lblCoCode.Text = "VN-001-2691";
        //    lblDeptCode.Text = "1";
        //    lblAuditorCode.Text = "";
        //}
    }
}