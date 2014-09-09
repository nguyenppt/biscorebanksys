using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using BankProject.DataProvider;
using System.Data;

namespace BankProject.FTTeller
{
    public partial class OutwardTransferByCash : PortalModuleBase
    {
        private static int Id = 392;
        
        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            rcbCurrency.Items.Add(new RadComboBoxItem(""));
            rcbCurrency.Items.Add(new RadComboBoxItem("USD", "USD"));
            rcbCurrency.Items.Add(new RadComboBoxItem("EUR", "EUR"));
            rcbCurrency.Items.Add(new RadComboBoxItem("GBP", "GBP"));
            rcbCurrency.Items.Add(new RadComboBoxItem("JPY", "JPY"));
            rcbCurrency.Items.Add(new RadComboBoxItem("VND", "VND"));

            rcbProvince.Items.Clear();
            rcbProvince.Items.Add(new RadComboBoxItem("")); ;
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

            if (Request.QueryString["IsAuthorize"] != null)
            {
                LoadToolBar(true);
                loaddataPreview();
                //dvAudit.Visible = true;
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }
            else 
            {
                LoadToolBar(false);
                FirstLoad();
                //dvAudit.Visible = false;
            }

        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            //if (hdfDisable.Value == "0") return;
            
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                rcbCreditAccount.Items.Clear();
                rcbCreditAccount.Text = "";
                FirstLoad();
            }

            if(commandName=="Preview")
            {
                Response.Redirect(EditUrl("chitiet"));
            }

            if (commandName == "authorize" || commandName == "reverse")
            {
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                FirstLoad();
                LoadToolBar(false);
            }
        }

        void loadcreditacc()
        {
            string currency = rcbCurrency.SelectedValue;
            string bencom = rcbBenCom.SelectedValue;
            string product = rcbProductID.SelectedValue;
            if (currency != "" && bencom != "" && product != "")
            {
                rcbCreditAccount.DataSource = BankProject.DataProvider.Database.BENCOM_SetCreditAccount_ByProduct(currency, bencom, product);
                rcbCreditAccount.DataTextField = "CREDITACCOUNT";
                rcbCreditAccount.DataValueField = "CREDITACCOUNT";
                rcbCreditAccount.DataBind();

                rcbCreditAccount.SelectedIndex = 0;
                rcbCreditAccount.Enabled = false;
            }
        }

        protected void rcbCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            loadcreditacc();
        }

        protected void rcbProductID_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbCashAccount.Items.Clear();
           
            rcbCashAccount.Items.Add(new RadComboBoxItem(""));
            rcbCashAccount.Items.Add(new RadComboBoxItem("USD-10001-2054-2861", "USD"));
            rcbCashAccount.Items.Add(new RadComboBoxItem("EUR-10001-2054-2861", "EUR"));
            rcbCashAccount.Items.Add(new RadComboBoxItem("GBP-10001-2054-2861", "GBP"));
            rcbCashAccount.Items.Add(new RadComboBoxItem("JPY-10001-2054-2861", "JPY"));
            rcbCashAccount.Items.Add(new RadComboBoxItem("VND-10001-2054-2861", "VND"));

            switch (rcbProductID.SelectedValue)
            {
                case "3000":
                    txtBenAccount.ReadOnly = false;
                    rcbProvince.Enabled = true;
                    rcbBenCode.Enabled = true;
                    break;

                case "1000":
                    txtBenAccount.ReadOnly = true;
                    rcbProvince.Enabled = false;
                    rcbBenCode.Enabled = false;
                    break;

            }
            loadcreditacc();
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

                rcbBenCode.DataSource = ds;
                rcbBenCode.DataTextField = "BANKNAME";
                rcbBenCode.DataValueField = "BANKCODE";
                rcbBenCode.DataBind();
            }
        }
        protected void cmbWaiveCharges_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (cmbWaiveCharges.SelectedValue)
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
        #endregion

        #region method
        void loaddataPreview()
        {
            if (Request.QueryString["LCCode"] != null)
            {
                string LCCode = Request.QueryString["LCCode"].ToString();
                switch (LCCode)
                {
                    case "1":
                        this.txtId.Text = "TT/09164/00393";

                        txtTeller.Text = this.UserInfo.Username;
                        txtReceivingName.SetTextDefault("Nguyễn Văn Trung");
                        txtSendingName.Text = "Đỗ Bảo Lộc";
                        txtSendingAddress.SetTextDefault("151 Nguyễn Văn Trung");
                        
                        txtNarrative.Text = "CHUYEN TIEN NHAN CMND NGUYEN VAN TRUNG";

                        rcbProductID.SelectedIndex = 2;
                        rcbProductID_OnSelectedIndexChanged(null, null);

                        rcbBenCom.SelectedValue = "VN00107";
                        rcbCurrency.SelectedValue = "VND";

                        loadcreditacc();

                        rcbProvince.SelectedValue = "01";
                        rcbProvince_OnSelectedIndexChanged(null, null);
                        rcbBenCode.SelectedIndex = 2;

                        txtBenAccount.Text = "VND54856";
                        //rcbBenCode.SelectedValue = "79306001";
                        rcbCashAccount.SelectedValue = "VND";
                        txtVatSerial.Text = "BA12345";

                        txtAmountLCY.Value = 40000000;
                        txtChargeAmtLCY.Value = 20000;
                        txtChargeVatAmt.Value = 2000;

                        txtIdentityCard.Text = "025984158";
                        txtIsssueDate.SelectedDate = new DateTime(2001, 1, 2);
                        txtIsssuePlace.Text = "TP.HCM";
                       
                        break;

                    case "2":
                        this.txtId.Text = "TT/09164/00394";
                        txtTeller.Text = this.UserInfo.Username;
                        txtReceivingName.SetTextDefault("Tô Văn Hoa");
                        txtSendingName.Text = "Trẩn Bửu Thạch";
                        txtSendingAddress.SetTextDefault("23 Lũy Bán Bích");

                        rcbProductID.SelectedIndex = 1;
                        rcbProductID_OnSelectedIndexChanged(null, null);

                        rcbBenCom.SelectedValue = "VN00121";
                        rcbCurrency.SelectedValue = "VND";

                        loadcreditacc();

                        txtNarrative.Text = "CHUYEN TIEN NHAN CMND TO VAN HOA";

                        rcbCashAccount.SelectedValue = "VND";
                        txtVatSerial.Text = "BA12345";

                        txtAmountLCY.Value = 5000000;
                        txtChargeAmtLCY.Value = 4500;
                        txtChargeVatAmt.Value = 450;
                       
                        txtIdentityCard.Text = "362584157";
                        txtIsssueDate.SelectedDate = new DateTime(1999, 5, 6);
                        txtIsssuePlace.Text = "TP.HCM";
                        break;

                    case "3":
                        this.txtId.Text = "TT/09164/00395";
                        txtTeller.Text = this.UserInfo.Username;
                        txtReceivingName.SetTextDefault("Lý Thánh Tông");
                        txtSendingName.Text = "Trần Minh Tâm";
                        txtSendingAddress.SetTextDefault("632 Trần Hưng Đạo");

                        rcbProductID.SelectedIndex = 1;
                        rcbProductID_OnSelectedIndexChanged(null, null);

                        rcbCurrency.SelectedValue = "VND";
                        rcbBenCom.SelectedValue = "VN00133";

                        loadcreditacc();

                        txtNarrative.Text = "CHUYEN TIEN NHAN CMND LY THANH TONG";

                        rcbCashAccount.SelectedValue = "VND";
                        txtVatSerial.Text = "BA12345";

                        txtAmountLCY.Value = 10000000;
                        txtChargeAmtLCY.Value = 11000;
                        txtChargeVatAmt.Value = 1100;

                        txtIdentityCard.Text = "025639587";
                        txtIsssueDate.SelectedDate = new DateTime(1990, 7, 21);
                        txtIsssuePlace.Text = "TP.HCM";
                        break;

                    case "4":
                        this.txtId.Text = "TT/09164/00396";
                        txtTeller.Text = this.UserInfo.Username;
                        txtReceivingName.SetTextDefault("Nguyễn Thị Hoa");
                        txtSendingName.Text = "Nguyễn Vĩ Minh";
                        txtSendingAddress.SetTextDefault("63 Bùi Thị Xuân");

                        rcbProductID.SelectedIndex = 1;
                        rcbProductID_OnSelectedIndexChanged(null, null);

                        rcbCurrency.SelectedValue = "VND";
                        rcbBenCom.SelectedValue = "VN00148";

                        loadcreditacc();

                        txtNarrative.Text = "CHUYEN TIEN NHAN CMND NGUYEN THI HOA";

                        rcbCashAccount.SelectedValue = "VND";
                        txtVatSerial.Text = "BA12345";

                       
                        txtAmountLCY.Value = 15000000;
                        txtChargeAmtLCY.Value = 15000;
                        txtChargeVatAmt.Value = 1500;

                        txtIdentityCard.Text = "012569987";
                        txtIsssueDate.SelectedDate = new DateTime(1990, 12, 16);
                        txtIsssuePlace.Text = "TP.HCM";
                        break;

                    case "5":
                        this.txtId.Text = "TT/09164/00397";
                        txtTeller.Text = this.UserInfo.Username;
                        txtReceivingName.SetTextDefault("Đỗ Tường Vy");
                        txtSendingName.Text = "Lưu Ái Nhi";
                        txtSendingAddress.SetTextDefault("1632/5 CMT8");

                        rcbProductID.SelectedIndex = 2;
                        rcbProductID_OnSelectedIndexChanged(null, null);

                        rcbBenCom.SelectedValue = "VN00102";
                        rcbCurrency.SelectedValue = "VND";

                        loadcreditacc();

                        rcbProvince.SelectedValue = "79";
                        rcbProvince_OnSelectedIndexChanged(null, null);
                        rcbBenCode.SelectedIndex = 3;

                        txtBenAccount.Text = "VND125487";
                        //rcbBenCode.SelectedValue = "82305001";

                        txtNarrative.Text = "CHUYEN TIEN NHAN CMND DO TUONG VY";

                        rcbCashAccount.SelectedValue = "VND";
                        txtVatSerial.Text = "BA12345";

                        txtAmountLCY.Value = 2000000;
                        txtChargeAmtLCY.Value = 3300;
                        txtChargeVatAmt.Value = 330;

                        txtIdentityCard.Text = "075984236";
                        txtIsssueDate.SelectedDate = new DateTime(1995, 8, 30);
                        txtIsssuePlace.Text = "TP.HCM";
                        break;
                }
            }
        }
        private void FirstLoad()
        {
            Id++;
            txtSendingAddress.SetTextDefault("");
            txtReceivingName.SetTextDefault("");
            this.txtId.Text = "TT/09164/00" + Id.ToString();
            txtTeller.Text = this.UserInfo.Username;
            txtChargeVatAmt.ReadOnly = true;
            txtChargeAmtLCY.ReadOnly = true;
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
        #endregion
    }
}