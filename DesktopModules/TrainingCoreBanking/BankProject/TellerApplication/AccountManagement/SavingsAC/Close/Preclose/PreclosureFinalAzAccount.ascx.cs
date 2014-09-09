using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close.PreClose
{
    public partial class PreclosureFinalAzAccount : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        string TypeCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();

            //dtpValueDate.SelectedDate = DateTime.Now;

            if (Request.QueryString["CodeID"] != null)
                tbDepositCode.Text = Request.QueryString["CodeID"].ToString();
            
            if (Request.QueryString["TypeCode"] != null)
                TypeCode = Request.QueryString["TypeCode"].ToString();

            hdfPreclosureType.Value = TypeCode;

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
               
                Response.Redirect(EditUrl("", "", "VIEW", param));
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
            if (TypeCode == "1")
            {
                lblCustomer.Text = "134494 - NGUYEN VAN HAI";
                lblCategory.Text = "8-100 - Traditional Savings INT in Arrears";
                lblCurrency.Text = "VND";
                lblProductCode.Text = "100 - Traditional FD - Arrears Ind";
                lblPrincipal.Text = "50,000,000";
                lblValueDate.Text = DateTime.Now.AddMonths(-11).AddDays(2).ToString("dd/MM/yyyy");
                lblMaturityDate.Text = DateTime.Now.AddMonths(-1).AddDays(2).ToString("dd/MM/yyyy");
                lblInterestedRate.Text = "18.06";
                rcbWorkingAcc.Text = "07.000168836.8";
                rcbWorkingAcc.SelectedValue = "1";

                lblOverrides.Text = "Charges , Red In Amt 0 , Total amt due " + lblPrincipal.Text;
                lblOverrides2.Text = " ";
                lblRecordStatus.Text = "INAU";
                lblRecordStatus2.Text = "";
                lblCurrNumber.Text = "2";
                lblCurrNumber2.Text = "1";
                lblInputter.Text = "114_" + UserInfo.Username + "_I_INAU";
                lblInputter2.Text = "1049_DMUSER_I_INAU_OFS _DM.OFS.SRC.VAL";
                lblAuthoriser.Text = "";
                lblAuthoriser2.Text = "1049_DMUSER_OFS_DM.OFS.SRC.VAL";
                lblCoCode.Text = "VN-001-1311";
                lblDeptCode.Text = "1";

            }
            else if (TypeCode == "2")
            {
                lblCustomer.Text = "136336 - VO THI HOA";
                lblCategory.Text = "8-150 - Traditional Savings INT Periodic";
                lblCurrency.Text = "VND";
                lblProductCode.Text = "200 - Tradi FD - Periodic Monthly";
                lblPrincipal.Text = "250,000,000";
                lblValueDate.Text = DateTime.Now.AddMonths(-1).AddDays(2).ToString("dd/MM/yyyy");
                lblMaturityDate.Text = DateTime.Now.AddMonths(3).AddDays(2).ToString("dd/MM/yyyy");
                lblInterestedRate.Text = "16.152";
                rcbWorkingAcc.Text = "07.000164412.7";
                rcbWorkingAcc.SelectedValue = "2";

                lblOverrides.Text = "Charges , Red In Amt 0 , Total amt due " + lblPrincipal.Text;
                lblOverrides2.Text = "VALUE IN INTERESTE.RATE IS DIFFERENT FROM DEFAULTED VALUE(14.808)";
                lblRecordStatus.Text = "INAU";
                lblRecordStatus2.Text = "";
                lblCurrNumber.Text = "2";
                lblCurrNumber2.Text = "1";
                lblInputter.Text = "114_" + UserInfo.Username + "_I_INAU";
                lblInputter2.Text = "1049_DMUSER_I_INAU_OFS _DM.OFS.SRC.VAL";
                lblAuthoriser.Text = "";
                lblAuthoriser2.Text = "1049_DMUSER_OFS_DM.OFS.SRC.VAL";
                lblCoCode.Text = "VN-001-1311";
                lblDeptCode.Text = "1";
            }
            else
            {
                lblCustomer.Text = "136336 - VO THI HOA";
                lblCategory.Text = "8-150 - Traditional Savings INT Periodic";
                lblCurrency.Text = "VND";
                lblProductCode.Text = "300 - Tradi - FD-Periodic Quaterly";
                lblPrincipal.Text = "250,000,000";
                lblValueDate.Text = DateTime.Now.AddMonths(-1).AddDays(2).ToString("dd/MM/yyyy");
                lblMaturityDate.Text = DateTime.Now.AddMonths(3).AddDays(2).ToString("dd/MM/yyyy");
                lblInterestedRate.Text = "16.152";
                rcbWorkingAcc.Text = "07.000164412.7";
                rcbWorkingAcc.SelectedValue = "2";

                lblOverrides.Text = "Charges , Red In Amt 0 , Total amt due " + lblPrincipal.Text;
                lblOverrides2.Text = "VALUE IN INTERESTE.RATE IS DIFFERENT FROM DEFAULTED VALUE(14.808)";
                lblRecordStatus.Text = "INAU";
                lblRecordStatus2.Text = "";
                lblCurrNumber.Text = "2";
                lblCurrNumber2.Text = "1";
                lblInputter.Text = "114_" + UserInfo.Username + "_I_INAU";
                lblInputter2.Text = "1049_DMUSER_I_INAU_OFS _DM.OFS.SRC.VAL";
                lblAuthoriser.Text = "";
                lblAuthoriser2.Text = "1049_DMUSER_OFS_DM.OFS.SRC.VAL";
                lblCoCode.Text = "VN-001-1311";
                lblDeptCode.Text = "1";
            }
        }
    }
}