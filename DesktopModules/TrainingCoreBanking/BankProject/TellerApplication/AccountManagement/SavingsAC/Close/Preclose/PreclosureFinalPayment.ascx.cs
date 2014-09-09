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
    public partial class PreclosureFinalPayment : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        string TypeCode = "";
        double totalAmt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();
            //tbDepositCode.Text = SQLData.B_BMACODE_GetNewID("PERIODIC_LOAN", "TT", "/");
            if (Request.QueryString["TellerTrans"] != null)
                tbDepositCode.Text = Request.QueryString["TellerTrans"].ToString();
            if (Request.QueryString["TypeCode"] != null)
                TypeCode = Request.QueryString["TypeCode"].ToString();
            if (Request.QueryString["TotalAmt"] != null)
                totalAmt = Double.Parse(Request.QueryString["TotalAmt"].ToString());

            hdfPreclosureType.Value = TypeCode;
            hdfTotalAmt.Value = totalAmt.ToString();
           
            setHardValueForm();

            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            tbDepositCode.Enabled = true;
            tbDepositCode.Focus();
        }

        private void setHardValueForm()
        {
            tbTeller.Text = UserInfo.Username;
            lblTellerName.Text = UserInfo.DisplayName;
            tbPrintLnNoOf.Text = "1";

            if (TypeCode == "1")
            {
                lblCustomer.Text = "134494 - NGUYEN VAN HAI";
                lblCurrency.Text = "VND";
                cmbDrAccount.Text = "07.000168836.8";
                tbNarative.Text = "TAT TOAN TAI KHOAN NGUYEN VAN HAI";
                cmbDrAccount.SelectedValue = "1";
                cmbCreditCCY.SelectedValue = "5";
                cmbCreditAcc.SelectedValue = "5";
                totalAmt += 50000000;
                numAcmountLcy.Value = totalAmt;
                lblCreditAccName.Text = "RECORD.AUTOMATICALLY.OF";

                lblOverrides.Text = "CREDIT TILL CLOSING BALANCE ";
                lblOverrides2.Text = "Unauthorised overdraft of VND " + totalAmt.ToString() + " on account " + cmbDrAccount.Text;
                lblRecordStatus.Text = "INAU";
                lblCurrNumber.Text = "1";
                lblInputter.Text = "126_" + UserInfo.Username + "_I_INAU";
                lblInputter2.Text = "114_" + UserInfo.Username + "_I_IHLD";
                lblAuthoriser.Text = "";
                lblAuthoriser2.Text = "";
                lblDateTime.Text = DateTime.Now.ToString("dd/MM HH:mm:ss");
                lblDateTime2.Text = DateTime.Now.ToString("dd/MM HH:mm:ss");
                lblCoCode.Text = "VN-001-1311";
                lblDeptCode.Text = "1";

            }
            else
            {
                lblCustomer.Text = "136336 - VO THI HOA";
                lblCurrency.Text = "VND";
                cmbDrAccount.Text = "07.000164412.7";
                tbNarative.Text = "TAT TOAN TAI KHOAN VO THI HOA";
                cmbDrAccount.SelectedValue = "2";
                cmbCreditCCY.SelectedValue = "5";
                cmbCreditAcc.SelectedValue = "5";
                totalAmt += 250000000;
                numAcmountLcy.Value = totalAmt;
                lblCreditAccName.Text = "RECORD.AUTOMATICALLY.OF";

                lblOverrides.Text = "CREDIT TILL CLOSING BALANCE ";
                lblOverrides2.Text = "Unauthorised overdraft of VND " + totalAmt.ToString() + " on account " + cmbDrAccount.Text;
                lblRecordStatus.Text = "INAU";
                lblCurrNumber.Text = "1";
                lblInputter.Text = "126_" + UserInfo.Username + "_I_INAU";
                lblInputter2.Text = "114_" + UserInfo.Username + "_I_IHLD";
                lblAuthoriser.Text = "";
                lblAuthoriser2.Text = "";
                lblDateTime.Text = DateTime.Now.ToString("dd/MM HH:mm:ss");
                lblDateTime2.Text = DateTime.Now.ToString("dd/MM HH:mm:ss");
                lblCoCode.Text = "VN-001-1311";
                lblDeptCode.Text = "1";
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
            
        }
        
    }
}