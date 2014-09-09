using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;
using System.Data;

namespace BankProject.Views.TellerApplication
{
    public partial class TransferByAccount : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            rcbProvince.Items.Clear();
            rcbProvince.Items.Add(new RadComboBoxItem(""));
            rcbProvince.DataSource = BankProject.DataProvider.Database.BPROVINCE_GetAll();
            rcbProvince.DataTextField = "TenTinhThanh";
            rcbProvince.DataValueField = "MaTinhThanh";
            rcbProvince.DataBind();

            rcbBenCom.Items.Clear();
            rcbBenCom.Items.Add(new RadComboBoxItem("")); ;
            rcbBenCom.DataSource = BankProject.DataProvider.Database.BENCOM_GetALL();
            rcbBenCom.DataTextField = "BENCOMNAME";
            rcbBenCom.DataValueField = "BENCOMID";
            rcbBenCom.DataBind();
            tbSendingAddress.SetTextDefault("");
            //tbReceivingName.SetTextDefault("");
            txtChargeAmtLCY.ReadOnly = true;
            txtChargeVatAmt.ReadOnly = true;
            if (Request.QueryString["IsAuthorize"] != null)
            {
                LoadToolBar(true);
                LoadDataPreview();
                //dvAudit.Visible = true;
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }
            else
            {
                LoadToolBar(false);
                Default_Setting();
            }

        }
        
        protected void rcbCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadDebitAcct();
        }

        protected void rcbProductID_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbCurrency.Items.Clear();
            //rcbCashAccount.Items.Clear();
            rcbCurrency.Items.Add(new RadComboBoxItem(""));
            rcbCurrency.Items.Add(new RadComboBoxItem("EUR", "EUR"));
            rcbCurrency.Items.Add(new RadComboBoxItem("USD", "USD"));
            rcbCurrency.Items.Add(new RadComboBoxItem("GBP", "GBP"));
            rcbCurrency.Items.Add(new RadComboBoxItem("JPY", "JPY"));
            rcbCurrency.Items.Add(new RadComboBoxItem("VND", "VND"));

            //rcbCashAccount.Items.Add(new RadComboBoxItem(""));
            //rcbCashAccount.Items.Add(new RadComboBoxItem("EUR-10001-2054-2861", "EUR"));
            //rcbCashAccount.Items.Add(new RadComboBoxItem("USD-10001-2054-2861", "USD"));
            //rcbCashAccount.Items.Add(new RadComboBoxItem("GBP-10001-2054-2861", "GBP"));
            //rcbCashAccount.Items.Add(new RadComboBoxItem("JPY-10001-2054-2861", "JPY"));
            //rcbCashAccount.Items.Add(new RadComboBoxItem("VND-10001-2054-2861", "VND"));

            switch (rcbProductID.SelectedValue)
            {
                case "3000":
                    tbBenAccount.ReadOnly = false;
                    rcbProvince.Enabled = true;
                    rcbBankCode.Enabled = true;

                    break;
                case "1000":
                    tbBenAccount.ReadOnly = true;
                    rcbProvince.Enabled = false;
                    rcbBankCode.Enabled = false;
                    break;

                case "":
                    rcbCurrency.Items.Add(new RadComboBoxItem(""));
                    //rcbCashAccount.Items.Add(new RadComboBoxItem(""));
                    break;

            }
        }

        protected void rcbProvince_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataSet ds = BankProject.DataProvider.Database.BBANKCODE_GetByProvince(rcbProvince.SelectedValue);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["BANKNAME"] = "";
                dr["BANKCODE"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);

                rcbBankCode.DataSource = ds;
                rcbBankCode.DataTextField = "BANKNAME";
                rcbBankCode.DataValueField = "BANKCODE";
                rcbBankCode.DataBind();
            }
        }
        protected void rcbWaiveCharge_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (rcbWaiveCharge.SelectedValue)
            {
                case "YES":
                    txtChargeAmtLCY.ReadOnly = true;
                    txtChargeVatAmt.ReadOnly = true;
                    txtChargeAmtLCY.Text = "";
                    txtChargeVatAmt.Text = "";
                    break;

                case "NO":
                    txtChargeAmtLCY.ReadOnly = false;
                    txtChargeVatAmt.ReadOnly = false;

                    break;

            }
        }
        
        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            if (hdfDisable.Value == "0") return;
            var ToolBarButton = e.Item as RadToolBarButton;
            var commandname = ToolBarButton.CommandName;
            switch (commandname)
            { 
                case "commit":
                    BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                    Default_Setting();
                    rcbDebitAccount.Items.Clear();
                    rcbDebitAccount.Text = "";
                    break;
                case "Preview":
                    LoadToolBar(true);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    Response.Redirect(EditUrl("TransferByAccount_PL"));
                    break;
                case "reverse":
                case "authorize":
                    Default_Setting();
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    LoadToolBar(false);
                    
                    break;
            }

        }
        #endregion

        #region function_load

        protected void Default_Setting()
        {
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
            tbID.Text = TriTT.B_BMACODE_3part_varMaCode_varSP("B_BMACODE_3part_varMaCode_varSP", "TRS_BY_ACCOUNT_ID", "TT");
            tbTellerID.Text = this.UserInfo.Username;
            rcbDebitAccount.Items.Clear();
            rcbDebitAccount.Text = "";
        }
        protected void LoadDebitAcct()
        {
            string currency = rcbCurrency.SelectedValue;
            //string bencom = rcbBenCom.SelectedValue;
            if (currency != "")
            {
                rcbDebitAccount.Items.Clear();
                rcbDebitAccount.AppendDataBoundItems = true;
                rcbDebitAccount.Items.Add(new RadComboBoxItem("", ""));
                rcbDebitAccount.DataSource = BankProject.DataProvider.Database.B_BDRFROMACCOUNT_GetByCustomer("", currency);
                rcbDebitAccount.DataTextField = "Display";
                rcbDebitAccount.DataValueField = "ID";
                rcbDebitAccount.DataBind();
            }
        }

      
        protected void Load_BankCode(string Province)
        {
            DataSet ds = BankProject.DataProvider.Database.BBANKCODE_GetByProvince(Province);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["BANKNAME"] = "";
                dr["BANKCODE"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);

                rcbBankCode.DataSource = ds;
                rcbBankCode.DataTextField = "BANKNAME";
                rcbBankCode.DataValueField = "BANKCODE";
                rcbBankCode.DataBind();
            }
        }

        protected void LoadToolBar(bool flag)
        {

            RadToolBar1.FindItemByValue("btCommitData").Enabled = !flag;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !flag;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar1.FindItemByValue("btReverse").Enabled = flag;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        void LoadDataPreview()
        {
            if (Request.QueryString["LCCode"] != null)
            {
                string LCCode = Request.QueryString["LCCode"].ToString();
                switch (LCCode)
                {
                    case "1":
                        this.tbID.Text = "TT/14164/00393";
                        
                        tbTellerID.Text = this.UserInfo.Username;
                        tbReceivingName.Text = "Nguyễn Văn Trung";
                        //tbReceivingName.SetTextDefault("Nguyễn Văn Trung");
                        tbSendingName.Text = "Đỗ Bảo Lộc";
                        tbSendingAddress.SetTextDefault("151 Nguyễn Văn Trung");
                        tbTaxCode.Text = "723647621";
                        tbNarrative.Text = "CHUYEN TIEN NHAN CMND NGUYEN VAN TRUNG";

                        rcbProductID.SelectedIndex = 2;
                        rcbProductID_OnSelectedIndexChanged(null, null);

                        rcbBenCom.SelectedValue = "VN00101"; 
                        rcbCurrency.SelectedValue = "VND";

                         LoadDebitAcct();
                         rcbDebitAccount.SelectedIndex = 1;
                         rcbProvince.SelectedValue = "60";
                         Load_BankCode("60");
                         rcbBankCode.SelectedIndex =1;
                         //rcbBankCode.SelectedValue= "60701001";
                        tbBenAccount.Text = "VND54856";
                        //rcbCashAccount.SelectedValue = "VND";
                        //lblVAT.Text = "BA12345";

                        tbAmount.Value = 40000000;
                        //lblChargeAmt.Text = "20000";
                        //txtChargeVatAmt.Value = 2000;
                        //lblCustomer.Text = "Dinh Tien Hoang";
                        //lblNewCust.Text ="10,000,000" ;
                        //lblCreditAccount.Text = "07.000259583.7";
                        //lblCurrency.Text = "VND";
                        //lblCreditAmt.Text=Convert.ToString(tbAmount.Value);
                        rcbWaiveCharge.SelectedValue=rcbSaveTemplate.SelectedValue = "NO";

                        tbIDCard.Text = "025984158";
                        rdpIssueDate.SelectedDate = new DateTime(2001, 1, 2);
                        tbIssuePlace.Text = "TP.HCM";

                        break;

                    case "2":
                        this.tbID.Text = "TT/14164/00394";
                        tbTellerID.Text = this.UserInfo.Username;
                        //tbReceivingName.SetTextDefault("Tô Văn Hoa");
                        tbReceivingName.Text = "Tô Văn Hoa";

                        tbSendingName.Text = "Trẩn Bửu Thạch";
                        tbSendingAddress.SetTextDefault("23 Lũy Bán Bích");

                        rcbProductID.SelectedIndex = 1;
                        rcbProductID_OnSelectedIndexChanged(null, null);
                        tbTaxCode.Text = "711647621";
                        rcbBenCom.SelectedValue = "VN00105";
                        rcbCurrency.SelectedValue = "VND";
                        tbBenAccount.Text = "VND54857";
                         LoadDebitAcct();
                         rcbDebitAccount.SelectedIndex = 1;
                         rcbProvince.SelectedValue = "82";
                         Load_BankCode("82");
                         rcbBankCode.SelectedIndex = 4;
                        tbNarrative.Text = "CHUYEN TIEN NHAN CMND TO VAN HOA";
                        //rcbBankCode.SelectedValue = "79305003";
                        //rcbCashAccount.SelectedValue = "VND";
                        //lblVAT.Text = "BA12345";

                        tbAmount.Value = 5000000;
                        //lblChargeAmt.Text = "4500";
                        //txtChargeVatAmt.Value = 450;
                        //lblCustomer.Text = "Dinh Tien Hoang";
                        //lblNewCust.Text ="10,000,000" ;
                        //lblCreditAccount.Text = "07.000259583.7";
                        //lblCurrency.Text = "VND";
                        //lblCreditAmt.Text=Convert.ToString(tbAmount.Value);
                        rcbWaiveCharge.SelectedValue=rcbSaveTemplate.SelectedValue = "NO";

                        tbIDCard.Text = "362584157";
                        rdpIssueDate.SelectedDate = new DateTime(1999, 5, 6);
                        tbIssuePlace.Text = "TP.HCM";
                        break;

                    case "3":
                        this.tbID.Text = "TT/14164/00395";
                        tbTellerID.Text = this.UserInfo.Username;
                        tbReceivingName.Text = "Lý Thánh Tông";
                        //tbReceivingName.SetTextDefault("Lý Thánh Tông");
                        tbSendingName.Text = "Trần Minh Tâm";
                        tbSendingAddress.SetTextDefault("632 Trần Hưng Đạo");
                        rcbProductID.SelectedIndex = 1;
                        rcbProductID_OnSelectedIndexChanged(null, null);

                        rcbCurrency.SelectedValue = "VND";
                        rcbBenCom.SelectedValue = "VN00112";
                        tbBenAccount.Text = "VND54854";
                         LoadDebitAcct();
                         rcbDebitAccount.SelectedIndex = 1;
                         rcbProvince.SelectedValue = "83";
                         Load_BankCode("83");
                         rcbBankCode.SelectedIndex = 5;
                        tbNarrative.Text = "CHUYEN TIEN NHAN CMND LY THANH TONG";
                        //rcbBankCode.SelectedValue = "22201004";
                        //rcbCashAccount.SelectedValue = "VND";
                        //lblVAT.Text = "BA12345";
                        tbTaxCode.Text = "093647621";
                        tbAmount.Value = 10000000;
                        //lblChargeAmt.Text = "11000";
                        //txtChargeVatAmt.Value = 1100;
                        //lblCustomer.Text = "Dinh Tien Hoang";
                        //lblNewCust.Text ="10,000,000" ;
                        //lblCreditAccount.Text = "07.000259583.7";
                        //lblCurrency.Text = "VND";
                        //lblCreditAmt.Text=Convert.ToString(tbAmount.Value);
                        rcbWaiveCharge.SelectedValue=rcbSaveTemplate.SelectedValue = "NO";

                        tbIDCard.Text = "025639587";
                        rdpIssueDate.SelectedDate = new DateTime(1990, 7, 21);
                        tbIssuePlace.Text = "TP.HCM";
                        break;

                    case "4":
                        this.tbID.Text = "TT/14164/00396";
                        tbTellerID.Text = this.UserInfo.Username;
                        tbReceivingName.Text  = "Nguyễn Thị Hoa";
                        //tbReceivingName.SetTextDefault("Nguyễn Thị Hoa");
                        tbSendingName.Text = "Nguyễn Vĩ Minh";
                        tbSendingAddress.SetTextDefault("63 Bùi Thị Xuân");
                        rcbBenCom.SelectedValue = "VN0043123";
                        rcbProductID.SelectedIndex = 1;
                        rcbProductID_OnSelectedIndexChanged(null, null);
                        tbTaxCode.Text = "723237621";
                        rcbCurrency.SelectedValue = "VND";
                        rcbBenCom.SelectedValue = "VN00126";
                        tbBenAccount.Text = "VND54896";
                         LoadDebitAcct();
                         rcbDebitAccount.SelectedIndex = 1;
                         rcbProvince.SelectedValue = "84";
                         Load_BankCode("84");
                         rcbBankCode.SelectedIndex = 3;
                        tbNarrative.Text = "CHUYEN TIEN NHAN CMND NGUYEN THI HOA";
                        //rcbCashAccount.SelectedValue = "VND";
                        //lblVAT.Text = "BA12345";
                        tbAmount.Value = 15000000;
                        //lblChargeAmt.Text = "15000";
                        //txtChargeVatAmt.Value = 1500;
                        //lblCustomer.Text = "Dinh Tien Hoang";
                        //lblNewCust.Text ="10,000,000" ;
                        //lblCreditAccount.Text = "07.000259583.7";
                        //lblCurrency.Text = "VND";
                        //lblCreditAmt.Text=Convert.ToString(tbAmount.Value);
                        rcbWaiveCharge.SelectedValue=rcbSaveTemplate.SelectedValue = "NO";

                        tbIDCard.Text = "012569987";
                        rdpIssueDate.SelectedDate = new DateTime(1990, 12, 16);
                        tbIssuePlace.Text = "TP.HCM";
                        break;

                    case "5":
                        this.tbID.Text = "TT/14164/00397";
                        tbTellerID.Text = this.UserInfo.Username;
                        tbReceivingName.Text = "Đỗ Tường Vy";
                        //tbReceivingName.SetTextDefault("Đỗ Tường Vy");
                        tbSendingName.Text = "Lưu Ái Nhi";
                        tbSendingAddress.SetTextDefault("1632/5 CMT8");
                        rcbProductID.SelectedIndex = 2;
                        rcbProductID_OnSelectedIndexChanged(null, null);
                        rcbBenCom.SelectedValue = "VN00138";
                        rcbCurrency.SelectedValue = "VND";
                        tbTaxCode.Text = "723647624";
                         LoadDebitAcct();
                         rcbDebitAccount.SelectedIndex = 1;
                         rcbProvince.SelectedValue = "86";
                         Load_BankCode("86");
                         rcbBankCode.SelectedIndex = 1;
                        tbBenAccount.Text = "VND125487";
                        tbNarrative.Text = "CHUYEN TIEN NHAN CMND DO TUONG VY";

                        //rcbCashAccount.SelectedValue = "VND";
                        //lblVAT.Text = "BA12345";

                        tbAmount.Value = 2000000;
                        //lblChargeAmt.Text = "3300";
                        //txtChargeVatAmt.Value = 330;
                        //lblCustomer.Text = "Dinh Tien Hoang";
                        //lblNewCust.Text ="10,000,000" ;
                        //lblCreditAccount.Text = "07.000259583.7";
                        //lblCurrency.Text = "VND";
                        //lblCreditAmt.Text=Convert.ToString(tbAmount.Value);
                        rcbWaiveCharge.SelectedValue=rcbSaveTemplate.SelectedValue = "NO";

                        tbIDCard.Text = "075984236";
                        rdpIssueDate.SelectedDate = new DateTime(1995, 8, 30);
                        tbIssuePlace.Text = "TP.HCM";
                        break;
                }
            }
        }
        #endregion
    }
}