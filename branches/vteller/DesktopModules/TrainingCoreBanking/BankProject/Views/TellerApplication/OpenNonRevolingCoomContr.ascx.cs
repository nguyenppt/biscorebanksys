using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BankProject.DataProvider;

namespace BankProject.Views.TellerApplication
{
    public partial class OpenNonRevolingCoomContr : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private string Refix_BMACODE()
        {
            return "LD";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            rcbCustomerID.Focus();
            tbID.Text = SQLData.B_BMACODE_GetNewID("CRED_REVOLVING_CONTRACT", Refix_BMACODE(), "/");
            dtpStartDate.SelectedDate = DateTime.Now;
            dtpDDStartDate.SelectedDate = DateTime.Now;

            rcbCustomerID.DataSource = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            rcbCustomerID.DataTextField = "CustomerName";
            rcbCustomerID.DataValueField = "CustomerID";
            rcbCustomerID.DataBind();

            rcbAcctOfficer.Items.Add(new RadComboBoxItem(UserInfo.Username, UserInfo.UserID.ToString()));
            rcbAcctOfficer.SelectedValue = UserInfo.UserID.ToString();

            if (Request.QueryString["IsAuthorize"] != null)
            {
                LoadToolBar(true);
                LoadDataPreview();
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }
            else
            {
                LoadToolBar(false);
            }
        }

        private void LoadToolBar(bool isauthorise)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btReverse").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                rcbCustomerID.Focus();
                tbID.Text = SQLData.B_BMACODE_GetNewID("CRED_REVOLVING_CONTRACT", Refix_BMACODE(), "/");
                this.rcbCustomerID.SelectedValue = "";
                this.rcbCurrency.SelectedValue = "";
                this.tbCommAmt.Text = "";
                this.dtpStartDate.SelectedDate = null;
                this.dtpEndDate.SelectedDate = null;
                this.tbFeeStart.Text = "";
                this.tbFeeEnd.Text = "";
                this.tbTrancheAmount.Text = "";
                this.dtpDDStartDate.SelectedDate =
                this.dtpDDEndDate.SelectedDate = null;
                this.rcbIntRepayAcct.SelectedValue = "";
                this.RcbChargeCode.SelectedValue = "";
                this.tbChargeAmount.Text = "";
                this.dtpChargeDate.SelectedDate = null;
                this.rcbChargeAccount.SelectedValue = "";
                this.rcbSecured.SelectedValue = null;
                this.tbCustRemarks.Text = "";
                this.rcbAcctOfficer.Text = "";
              
            }
            if (commandName == "Preview")
            {
                Response.Redirect(EditUrl("B_OpenNonRevolvingCommCont_PL"));
            }
            if (commandName == "authorize" || commandName == "reverse")
            {
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                LoadToolBar(false);
                AfterProc();
            }
        }
        public void AfterProc()
        {
            rcbCustomerID.Focus();
            tbID.Text = SQLData.B_BMACODE_GetNewID("CRED_REVOLVING_CONTRACT", Refix_BMACODE(), "/");
            this.dtpStartDate.SelectedDate = DateTime.Now;
            this.tbBusDayDefine.Text = "VN";
            this.dtpDDStartDate.SelectedDate = DateTime.Now;
            this.rcbAcctOfficer.Text = UserInfo.UserID.ToString();
        }
        void LoadDataPreview()
        {
            if (Request.QueryString["LCCode"] != null)
            {
                string LCCode = Request.QueryString["LCCode"].ToString();
                switch (LCCode)
                {
                    case "1":
                         rcbCustomerID.SelectedValue = "1100001";
                        tbID.Text = "LD/14189/00089";
                        tbCommAmt.Text="200000000";
                         rcbCurrency.SelectedValue = "VND";
                        tbFeeStart.Text = "340000";
                        tbFeeEnd.Text = "600000";
                        rcbIntRepayAcct.SelectedValue = "060002650471";
                        rcbSecured.SelectedValue = "Y";
                        tbCustRemarks.Text = "Dong tien hang thang";
                        dtpStartDate.SelectedDate =Convert.ToDateTime("9/7/2014");
                        dtpDDStartDate.SelectedDate = Convert.ToDateTime("9/7/2014");
                        dtpEndDate.SelectedDate = Convert.ToDateTime("10/7/2015");
                        dtpDDEndDate.SelectedDate = Convert.ToDateTime("10/7/2015");
                        tbTrancheAmount.Text = "10000";
                        tbChargeAmount.Text = "10000";
                        dtpChargeDate.SelectedDate = Convert.ToDateTime("9/7/2014");
                        rcbIntRepayAcct.SelectedValue = "070002650471";
                        break;
                    case "2":
                         rcbCustomerID.SelectedValue = "1100002";
                        tbID.Text = "LD/14189/00080";
                        tbCommAmt.Text="200000000000";
                        rcbCurrency.SelectedValue = "VND";
                        tbFeeStart.Text = "50000";
                        tbFeeEnd.Text = "60000";
                        rcbIntRepayAcct.SelectedValue = "060002650471";
                        rcbSecured.SelectedValue = "Y";
                        tbCustRemarks.Text = "Dong tien hang thang";

                        dtpStartDate.SelectedDate = Convert.ToDateTime("5/6/2014");
                        dtpDDStartDate.SelectedDate = Convert.ToDateTime("5/6/2014");
                        dtpEndDate.SelectedDate = Convert.ToDateTime("6/6/2015");
                        dtpDDEndDate.SelectedDate = Convert.ToDateTime("6/6/2015");
                        tbTrancheAmount.Text = "10000";
                        tbChargeAmount.Text = "10000";
                        dtpChargeDate.SelectedDate = Convert.ToDateTime("6/6/2014");
                        rcbIntRepayAcct.SelectedValue = "070002650041";
                        break;
                    case "3":
                        rcbCustomerID.SelectedValue = "1100003";
                        tbID.Text = "LD/14159/00066";
                        tbCommAmt.Text = "950000000";
                        rcbCurrency.SelectedValue = "VND";
                        tbFeeStart.Text = "760000";
                        tbFeeEnd.Text = "900000";
                        rcbIntRepayAcct.SelectedValue = "060002650471";
                        rcbSecured.SelectedValue = "Y";
                        tbCustRemarks.Text = "Dong tien hang thang";
                        dtpStartDate.SelectedDate = Convert.ToDateTime("4/5/2014");
                        dtpDDStartDate.SelectedDate = Convert.ToDateTime("4/5/2014");
                        dtpEndDate.SelectedDate = Convert.ToDateTime("5/5/2015");
                        dtpDDEndDate.SelectedDate = Convert.ToDateTime("5/5/2015");
                        tbTrancheAmount.Text = "10000";
                        tbChargeAmount.Text = "10000";
                        dtpChargeDate.SelectedDate = Convert.ToDateTime("5/5/2014");
                        rcbIntRepayAcct.SelectedValue = "070002650174";
                        break;

                    case "4":
                        rcbCustomerID.SelectedValue = "1100004";
                        tbID.Text = "LD/14159/00067";
                        tbCommAmt.Text = "95000000";
                        rcbCurrency.SelectedValue = "VND";
                        tbFeeStart.Text = "76000";
                        tbFeeEnd.Text = "4300000";
                        rcbIntRepayAcct.SelectedValue = "060002650471";
                        rcbSecured.SelectedValue = "Y";
                        tbCustRemarks.Text = "Dong tien hang thang";
                        dtpStartDate.SelectedDate = Convert.ToDateTime("5/5/2014");
                        dtpDDStartDate.SelectedDate = Convert.ToDateTime("5/5/2014");
                        dtpEndDate.SelectedDate = Convert.ToDateTime("6/5/2015");
                        dtpDDEndDate.SelectedDate = Convert.ToDateTime("6/5/2015");
                        tbTrancheAmount.Text = "12000";
                        tbChargeAmount.Text = "12000";
                        dtpChargeDate.SelectedDate = Convert.ToDateTime("5/5/2014");
                        rcbIntRepayAcct.SelectedValue = "070002665366";
                        break;
                    case "5":
                        rcbCustomerID.SelectedValue = "1100005";
                        tbID.Text = "LD/14159/00068";
                        tbCommAmt.Text = "450000000";
                        rcbCurrency.SelectedValue = "VND";
                        tbFeeStart.Text = "770000";
                        tbFeeEnd.Text = "700000";
                        rcbIntRepayAcct.SelectedValue = "060002650471";
                        rcbSecured.SelectedValue = "Y";
                        tbCustRemarks.Text = "Dong tien hang thang";
                        dtpStartDate.SelectedDate = Convert.ToDateTime("6/5/2014");
                        dtpDDStartDate.SelectedDate = Convert.ToDateTime("6/5/2014");
                        dtpEndDate.SelectedDate = Convert.ToDateTime("7/5/2015");
                        dtpDDEndDate.SelectedDate = Convert.ToDateTime("7/5/2015");
                        tbTrancheAmount.Text = "20000";
                        tbChargeAmount.Text = "20000";
                        dtpChargeDate.SelectedDate = Convert.ToDateTime("6/5/2014");
                        rcbIntRepayAcct.SelectedValue = "070002665321";
                        break;

                    case "6":
                        rcbCustomerID.SelectedValue = "2102925";
                        tbID.Text = "LD/14159/00069";
                        tbCommAmt.Text = "150000000";
                        rcbCurrency.SelectedValue = "VND";
                        tbFeeStart.Text = "460000";
                        tbFeeEnd.Text = "910000";
                        rcbIntRepayAcct.SelectedValue = "060002665321";
                        rcbSecured.SelectedValue = "N";
                        tbCustRemarks.Text = "Dong tien hang thang";
                        dtpStartDate.SelectedDate = Convert.ToDateTime("9/5/2014");
                        dtpDDStartDate.SelectedDate = Convert.ToDateTime("9/5/2014");
                        dtpEndDate.SelectedDate = Convert.ToDateTime("10/5/2015");
                        dtpDDEndDate.SelectedDate = Convert.ToDateTime("10/5/2015");
                        tbTrancheAmount.Text = "11000";
                        tbChargeAmount.Text = "11000";
                        dtpChargeDate.SelectedDate = Convert.ToDateTime("9/5/2014");
                        rcbIntRepayAcct.SelectedValue = "070002677321";
                        break;

                    case "7":
                        rcbCustomerID.SelectedValue = "2102926";
                        tbID.Text = "LD/14159/00070";
                        tbCommAmt.Text = "250000000";
                        rcbCurrency.SelectedValue = "VND";
                        tbFeeStart.Text = "760000";
                        tbFeeEnd.Text = "900000";
                        rcbIntRepayAcct.SelectedValue = "060002665321";
                        rcbSecured.SelectedValue = "N";
                        tbCustRemarks.Text = "Dong tien hang thang";
                        dtpStartDate.SelectedDate = Convert.ToDateTime("11/5/2014");
                        dtpDDStartDate.SelectedDate = Convert.ToDateTime("11/5/2014");
                        dtpEndDate.SelectedDate = Convert.ToDateTime("12/5/2015");
                        dtpDDEndDate.SelectedDate = Convert.ToDateTime("12/5/2015");
                        tbTrancheAmount.Text = "12000";
                        tbChargeAmount.Text = "12000";
                        dtpChargeDate.SelectedDate = Convert.ToDateTime("11/5/2014");
                        rcbIntRepayAcct.SelectedValue = "070006755767";
                        break;
                    case "8":
                        rcbCustomerID.SelectedValue = "2102927";
                        tbID.Text = "LD/14159/00071";
                        tbCommAmt.Text = "300000000";
                        rcbCurrency.SelectedValue = "VND";
                        tbFeeStart.Text = "760000";
                        tbFeeEnd.Text = "900000";
                        rcbIntRepayAcct.SelectedValue = "060002665321";
                        rcbSecured.SelectedValue = "N";
                        tbCustRemarks.Text = "Dong tien hang thang";
                        dtpStartDate.SelectedDate = Convert.ToDateTime("12/5/2014");
                        dtpDDStartDate.SelectedDate = Convert.ToDateTime("12/5/2014");
                        dtpEndDate.SelectedDate = Convert.ToDateTime("12/6/2015");
                        dtpDDEndDate.SelectedDate = Convert.ToDateTime("12/6/2015");
                        tbTrancheAmount.Text = "12000";
                        tbChargeAmount.Text = "11000";
                        dtpChargeDate.SelectedDate = Convert.ToDateTime("12/5/2014");
                        rcbIntRepayAcct.SelectedValue = "070006785527";
                        break;
                    case "9":
                        rcbCustomerID.SelectedValue = "2102928";
                        tbID.Text = "LD/14159/00072";
                        tbCommAmt.Text = "950000000";
                        rcbCurrency.SelectedValue = "VND";
                        tbFeeStart.Text = "760000";
                        tbFeeEnd.Text = "900000";
                        rcbIntRepayAcct.SelectedValue = "060002665321";
                        rcbSecured.SelectedValue = "N";
                        tbCustRemarks.Text = "Dong tien hang thang";
                        dtpStartDate.SelectedDate = Convert.ToDateTime("12/5/2014");
                        dtpDDStartDate.SelectedDate = Convert.ToDateTime("12/5/2014");
                        dtpEndDate.SelectedDate = Convert.ToDateTime("12/7/2015");
                        dtpDDEndDate.SelectedDate = Convert.ToDateTime("12/7/2015");
                        tbTrancheAmount.Text = "10000";
                        tbChargeAmount.Text = "10000";
                        dtpChargeDate.SelectedDate = Convert.ToDateTime("12/5/2014");
                        rcbIntRepayAcct.SelectedValue = "070006785565";
                        break;
                    case "10":
                        rcbCustomerID.SelectedValue = "2102929";
                        tbID.Text = "LD/14159/00066";
                        tbCommAmt.Text = "550000000";
                        rcbCurrency.SelectedValue = "VND";
                        tbFeeStart.Text = "760000";
                        tbFeeEnd.Text = "900000";
                        rcbIntRepayAcct.SelectedValue = "060002665321";
                        rcbSecured.SelectedValue = "Y";
                        tbCustRemarks.Text = "Dong tien hang thang";
                        dtpStartDate.SelectedDate = Convert.ToDateTime("12/10/2014");
                        dtpDDStartDate.SelectedDate = Convert.ToDateTime("12/10/2014");
                        dtpEndDate.SelectedDate = Convert.ToDateTime("12/11/2015");
                        dtpDDEndDate.SelectedDate = Convert.ToDateTime("12/11/2015");
                        tbTrancheAmount.Text = "55000";
                        tbChargeAmount.Text = "15000";
                        dtpChargeDate.SelectedDate = Convert.ToDateTime("12/10/2014");
                        rcbIntRepayAcct.SelectedValue = "070006785567";
                        break;

                }
            }
        }
    }
}