using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close.TellerTransactions
{
    public partial class TellerTransactionsDisplay : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        string TypeCode = "";
        string AmountLcy = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();

            if (Request.QueryString["CodeID"] != null)
                tbDepositCode.Text = Request.QueryString["CodeID"].ToString();
            //if (Request.QueryString["AmountLcy"] != null)
            //    AmountLcy = Request.QueryString["AmountLcy"].ToString();
            if (Request.QueryString["TypeCode"] != null)
                TypeCode = Request.QueryString["TypeCode"].ToString();

            hdfPreclosureType.Value = TypeCode;

            if (TypeCode == "1") {
                lblTellerID.Text = UserInfo.Username + "\t   " + UserInfo.DisplayName;
                lblCustomer.Text = "134494 - NGUYEN VAN HAI";
                lblDebitCurrency.Text = "VND";
                lblDebitAccount.Text = "07.000168836.8";
                lblDebitAmtLCY.Text = "57,266,667";
                hdfTotalAmt.Value = "57266667";
                lblDebitAmtFCY.Text = "";
                lblNarrative.Text = "TAT TOAN TAI KHOAN NGUYEN VAN HAI";
                lblValueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblNewCustBal.Text = "-57,266,667";

                lblTellerID2.Text = UserInfo.Username + "\t   " + UserInfo.DisplayName;
                lblCrebitCurrency.Text = "VND";
                lblCreditAccount.Text = "VND-10001-1980-1311";
                lblCreditAmountLCY.Text = "57,266,667";
                lblDebitAmtFCY.Text = "";
                lblAmountPaid   .Text = "57,266,667";
                lblValueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                lblTellerID.Text = UserInfo.Username + "\t    " + UserInfo.DisplayName;
                lblCustomer.Text = "136336 - VO THI HOA";
                lblDebitCurrency.Text = "VND";
                lblDebitAccount.Text = "07.000168836.7";
                lblDebitAmtLCY.Text = "250,583,333";
                hdfTotalAmt.Value = "250583333.33";
                lblDebitAmtFCY.Text = "";
                lblNarrative.Text = "TAT TOAN TAI KHOAN VO THI HOA";
                lblValueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblNewCustBal.Text = "-250,583,333";

                lblTellerID2.Text = UserInfo.Username + "\t   " + UserInfo.DisplayName;
                lblCrebitCurrency.Text = "VND";
                lblCreditAccount.Text = "VND-10001-1980-1311";
                lblCreditAmountLCY.Text = "250,583,333";
                lblDebitAmtFCY.Text = "";
                lblAmountPaid.Text = "250,583,333";
                lblValueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
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
                string[] param = new string[1];
                param[0] = "CodeID=" + tbDepositCode.Text;
                Response.Redirect(EditUrl("", "", "OK", param));
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