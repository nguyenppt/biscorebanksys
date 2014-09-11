using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BankProject.DataProvider;

namespace BankProject.Views.TellerApplication
{
    public partial class TransferForCreditCardPayment : PortalModuleBase
    {
        private  string refix_BMACODE()
        {
            return "TT";
        }
        private void LoadToolBar(bool isauthorize)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            rcbDebitAccount.Focus();
                txtID.Text = TriTT.B_BMACODE_GetNewID_3par("TRS_CRED_CARD_PAYM", refix_BMACODE(), "/");
                //cmbDebitCurrency.SelectedValue = "VND";
                rdpValueDate.SelectedDate = DateTime.Now;
               // cmbCreditCurrency.SelectedValue = "VND";
                rdpValueDate2.SelectedDate = DateTime.Now;
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

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolbarbutton = e.Item as RadToolBarButton;
            var CommandName = toolbarbutton.CommandName;
            if (CommandName == "commit")
            {
                rcbDebitAccount.Focus();
                txtID.Text = TriTT.B_BMACODE_GetNewID_3par("TRS_CRED_CARD_PAYM", refix_BMACODE(), "/");
                txtDebitAmtLCY.Text = "";
                txtDealRate.Text = "";
                tbCreditCardNum.Text = "";
                txtNarrative.Text = "";
                cmbDebitCurrency.SelectedValue = "";
                rdpValueDate.SelectedDate = DateTime.Now;
                cmbCreditCurrency.SelectedValue = "";
                rdpValueDate2.SelectedDate = DateTime.Now;
                cmbCreditAccount.SelectedValue = "";
                rcbDebitAccount.SelectedValue = "";

            }
            if (CommandName == "Preview")
            {
                Response.Redirect(EditUrl("TransferForCreditCardPayment_PL"));
            }
            if (CommandName == "authorize" || CommandName == "reverse")
            {
               
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                LoadToolBar(false);
                AfterProc();
            }

        }

         void LoadDataPreview()
        {
            Random CardNum = new Random();
            if (Request.QueryString["LCCode"] != null)
            {
                string LCCode = Request.QueryString["LCCode"].ToString();
                switch (LCCode)
                { 
                    case "0":
                        txtID.Text = "TT/14192/08015";
                        lblCustomerID.Text = "1100001";
                        lblCustomerName.Text = "VU THI MY HANH";
                        cmbDebitCurrency.SelectedValue = "VND";
                        rcbDebitAccount.SelectedValue = "0";
                        txtDebitAmtLCY.Text = "200000000";
                        rdpValueDate.SelectedDate = rdpValueDate2.SelectedDate = Convert.ToDateTime("03/15/2014");
                        cmbCreditAccount.SelectedValue = "VND";
                        cmbCreditCurrency.SelectedValue = "VND";
                        txtDealRate.Text = "";
                        lblAmForCust.Text = "200,000,000";
                        tbCreditCardNum.Text = Convert.ToString(CardNum.Next(600000000, 900000000));
                        txtNarrative.Text = "CHUYEN TIEN HANG HOA";
                        break;
                    case "1":
                        txtID.Text = "TT/14193/08016";
                        lblCustomerID.Text = "1100002";
                        lblCustomerName.Text = "DINH TIEN HOANG";
                        cmbDebitCurrency.SelectedValue = "VND";
                        rcbDebitAccount.SelectedValue = "0";
                        txtDebitAmtLCY.Text = "300000000";
                        rdpValueDate.SelectedDate = rdpValueDate2.SelectedDate = Convert.ToDateTime("10/07/2014");
                        cmbCreditAccount.SelectedValue = "VND";
                        cmbCreditCurrency.SelectedValue = "VND";
                        txtDealRate.Text = "";
                        lblAmForCust.Text = "300,000,000";
                        tbCreditCardNum.Text = Convert.ToString(CardNum.Next(600000000, 900000000));
                        txtNarrative.Text = "CHUYEN TIEN HANG HOA";
                        break;

                    case "2":
                        txtID.Text = "TT/14190/08013";
                        lblCustomerID.Text = "1100003";
                        lblCustomerName.Text = "PHAM NGOC THACH";
                        cmbDebitCurrency.SelectedValue = "VND";
                        rcbDebitAccount.SelectedValue = "0";
                        txtDebitAmtLCY.Text = "400000000";
                        rdpValueDate.SelectedDate = rdpValueDate2.SelectedDate = Convert.ToDateTime("1/01/2014");
                        cmbCreditAccount.SelectedValue = "VND";
                        cmbCreditCurrency.SelectedValue = "VND";
                        txtDealRate.Text = "";
                        lblAmForCust.Text = "400,000,000";
                        tbCreditCardNum.Text = Convert.ToString(CardNum.Next(600000000, 900000000));
                        txtNarrative.Text = "CHUYEN TIEN HANG HOA";
                        break;

                    case "3":
                        txtID.Text = "TT/14189/08012";
                        lblCustomerID.Text = "1100004";
                        lblCustomerName.Text = "VO THI SAU";
                        cmbDebitCurrency.SelectedValue = "VND";
                        rcbDebitAccount.SelectedValue = "0";
                        txtDebitAmtLCY.Text = "500000000";
                        rdpValueDate.SelectedDate = rdpValueDate2.SelectedDate = Convert.ToDateTime("09/02/2014");
                        cmbCreditAccount.SelectedValue = "VND";
                        cmbCreditCurrency.SelectedValue = "VND";
                        txtDealRate.Text = "";
                        lblAmForCust.Text = "500,000,000";
                        tbCreditCardNum.Text = Convert.ToString(CardNum.Next(600000000, 900000000));
                        txtNarrative.Text = "CHUYEN TIEN HANG HOA";
                        break;

                    case "4":
                        txtID.Text = "TT/14188/08011";
                        lblCustomerID.Text = "1100005";
                        lblCustomerName.Text = "TRUONG CONG DINH";
                        cmbDebitCurrency.SelectedValue = "VND";
                        rcbDebitAccount.SelectedValue = "0";
                        txtDebitAmtLCY.Text = "600000000";
                        rdpValueDate.SelectedDate = rdpValueDate2.SelectedDate = Convert.ToDateTime("03/07/2014");
                        cmbCreditAccount.SelectedValue = "USD";
                        cmbCreditCurrency.SelectedValue = "USD";
                        txtDealRate.Text = "0.00005";
                        lblAmForCust.Text = "30,000";
                        tbCreditCardNum.Text = Convert.ToString(CardNum.Next(600000000, 900000000));
                        txtNarrative.Text = "CHUYEN TIEN HANG HOA";
                        break;

                    case "5":
                        txtID.Text = "TT/14187/08010";
                        lblCustomerID.Text = "2102925";
                        lblCustomerName.Text = "CTY TNHH SONG HONG";
                        cmbDebitCurrency.SelectedValue = "VND";
                        rcbDebitAccount.SelectedValue = "0";
                        txtDebitAmtLCY.Text = "500000000";
                        rdpValueDate.SelectedDate = rdpValueDate2.SelectedDate = Convert.ToDateTime("11/01/2014");
                        cmbCreditAccount.SelectedValue = "USD";
                        cmbCreditCurrency.SelectedValue = "USD";
                        txtDealRate.Text = "0.00005";
                        lblAmForCust.Text = "25,000";
                        tbCreditCardNum.Text = Convert.ToString(CardNum.Next(600000000, 900000000));
                        txtNarrative.Text = "CHUYEN TIEN HANG HOA";
                        break;

                    case "6":
                        txtID.Text = "TT/14186/08009";
                        lblCustomerID.Text = "2102926";
                        lblCustomerName.Text = "CTY TNHH BAS";
                        cmbDebitCurrency.SelectedValue = "VND";
                        rcbDebitAccount.SelectedValue = "0";
                        txtDebitAmtLCY.Text = "200000000";
                        rdpValueDate.SelectedDate = rdpValueDate2.SelectedDate = Convert.ToDateTime("2/2/2014");
                        cmbCreditAccount.SelectedValue = "USD";
                        cmbCreditCurrency.SelectedValue = "USD";
                        txtDealRate.Text = "0.00005";
                        lblAmForCust.Text = "10,000";
                        tbCreditCardNum.Text = Convert.ToString(CardNum.Next(600000000, 900000000));
                        txtNarrative.Text = "CHUYEN TIEN HANG HOA";
                        break;

                    case "7":
                        txtID.Text = "TT/14185/08008";
                        lblCustomerID.Text = "2102927";
                        lblCustomerName.Text = "TRADE CORP";
                        cmbDebitCurrency.SelectedValue = "VND";
                        rcbDebitAccount.SelectedValue = "0";
                        txtDebitAmtLCY.Text = "900000000";
                        rdpValueDate.SelectedDate = rdpValueDate2.SelectedDate = Convert.ToDateTime("01/03/2014");
                        cmbCreditAccount.SelectedValue = "USD";
                        cmbCreditCurrency.SelectedValue = "USD";
                        txtDealRate.Text = "0.00005";
                        lblAmForCust.Text = "45,000";
                        tbCreditCardNum.Text = Convert.ToString(CardNum.Next(600000000, 900000000));
                        txtNarrative.Text = "CHUYEN TIEN HANG HOA";
                        break;

                    case "8":
                        txtID.Text = "TT/14184/08007";
                        lblCustomerID.Text = "2102928";
                        lblCustomerName.Text = "CTY MINH ANH";
                        cmbDebitCurrency.SelectedValue = "VND";
                        rcbDebitAccount.SelectedValue = "0";
                        txtDebitAmtLCY.Text = "100000000";
                        rdpValueDate.SelectedDate = rdpValueDate2.SelectedDate = Convert.ToDateTime("07/09/2014");
                        cmbCreditAccount.SelectedValue = "USD";
                        cmbCreditCurrency.SelectedValue = "USD";
                        txtDealRate.Text = "0.00005";
                        lblAmForCust.Text = "5,000";
                        tbCreditCardNum.Text = Convert.ToString(CardNum.Next(600000000, 900000000));
                        txtNarrative.Text = "CHUYEN TIEN HANG HOA";
                        break;

                    case "9":
                        txtID.Text = "TT/14183/08006";
                        lblCustomerID.Text = "2102929";
                        lblCustomerName.Text = "CTY HOANG VU";
                        cmbDebitCurrency.SelectedValue = "VND";
                        rcbDebitAccount.SelectedValue = "0";
                        txtDebitAmtLCY.Text = "220,000,000";
                        rdpValueDate.SelectedDate = rdpValueDate2.SelectedDate = Convert.ToDateTime("09/07/2014");
                        cmbCreditAccount.SelectedValue = "USD";
                        cmbCreditCurrency.SelectedValue = "USD";
                        txtDealRate.Text = "0.00005";
                        lblAmForCust.Text = "11000";
                        tbCreditCardNum.Text = Convert.ToString(CardNum.Next(600000000, 900000000));
                        txtNarrative.Text = "CHUYEN TIEN HANG HOA";
                        break;
                }
            }
        }

        public void AfterProc()
        {
            rcbDebitAccount.Focus();
            txtID.Text = TriTT.B_BMACODE_GetNewID_3par("TRS_CRED_CARD_PAYM", refix_BMACODE(), "/");
            cmbDebitCurrency.SelectedValue = "";
            rdpValueDate.SelectedDate = DateTime.Now;
            cmbCreditCurrency.SelectedValue = "";
            rdpValueDate2.SelectedDate = DateTime.Now;
            rcbDebitAccount.SelectedValue = "";
            txtDebitAmtLCY.Text = "";
            cmbCreditAccount.SelectedValue = "";
            txtDealRate.Text = "";
            lblAmForCust.Text = "";
            tbCreditCardNum.Text = "";
            txtNarrative.Text = "";
            lblCustomerID.Text = "";
            lblCustomerName.Text = "";
        }

    }
}