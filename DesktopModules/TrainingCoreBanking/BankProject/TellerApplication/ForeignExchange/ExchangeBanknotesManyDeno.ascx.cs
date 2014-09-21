using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication.ForeignExchange
{
    public partial class ExchangeBanknotesManyDeno : PortalModuleBase
    {
        private static int Id = 567;
        
        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

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
            
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
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
                        this.txtId.Text = "TT/09224/00567";
                        txtTeller1.Text = this.UserInfo.Username;
                        txtTeller2.Text = this.UserInfo.Username;
                        txtCustomerName.SetTextDefault("Nguyễn Văn Trung");
                        txtAddress.SetTextDefault("151 Nguyễn Văn Trung");
                        txtPhoneNo.Text = "0909256821";
                        rcbCurrencyBought.SelectedValue = "USD";
                        rcbDebitAccount.SelectedValue = "USD";
                        txtAmount.Value = 2000;
                        txtNarrative.Text = "THU DOI USD";

                        rcbCreditAccount.SelectedValue = "VND";

                        txtExchangeRate.Value = 20000;
                        txtAmtPayCust.Text = "40,000,000";

                        txtRate100.Value = 20000;
                        txtUnit100.Value = 10;
                        lbAmtLcy100.Text = "20,000,000";

                        txtRate50.Value = 20000;
                        txtUnit50.Value = 20;
                        lbAmtLcy50.Text = "20,000,000";
                        break;

                    case "2":
                        this.txtId.Text = "TT/09224/00568";
                        txtTeller1.Text = this.UserInfo.Username;
                        txtTeller2.Text = this.UserInfo.Username;
                        txtCustomerName.SetTextDefault("Tô Văn Hoa");
                        txtAddress.SetTextDefault("23 Lũy Bán Bích");
                        txtPhoneNo.Text = "01223651874";
                        rcbCurrencyBought.SelectedValue = "USD";
                        rcbDebitAccount.SelectedValue = "USD";
                        txtAmount.Value = 250;
                        txtNarrative.Text = "THU DOI USD";

                        rcbCreditAccount.SelectedValue = "VND";

                        txtExchangeRate.Value = 20000;
                        txtAmtPayCust.Text = "5,000,000";

                        txtRate100.Value = 20000;
                        txtUnit100.Value = 2;
                        lbAmtLcy100.Text = "4,000,000";

                        txtRate50.Value = 20000;
                        txtUnit50.Value = 1;
                        lbAmtLcy50.Text = "1,000,000";
                        break;

                    case "3":
                        this.txtId.Text = "TT/09224/00569";
                        txtTeller1.Text = this.UserInfo.Username;
                        txtTeller2.Text = this.UserInfo.Username;
                        txtCustomerName.SetTextDefault("Lý Thánh Tông");
                        txtAddress.SetTextDefault("632 Trần Hưng Đạo");
                        txtPhoneNo.Text = "0919995586";
                        rcbCurrencyBought.SelectedValue = "USD";
                        rcbDebitAccount.SelectedValue = "USD";
                        txtAmount.Value = 500;
                        txtNarrative.Text = "THU DOI USD";

                        rcbCreditAccount.SelectedValue = "VND";

                        txtExchangeRate.Value = 20000;
                        txtAmtPayCust.Text = "10,000,000";

                        txtRate100.Value = 20000;
                        txtUnit100.Value = 4;
                        lbAmtLcy100.Text = "8,000,000";

                        txtRate50.Value = 20000;
                        txtUnit50.Value = 2;
                        lbAmtLcy50.Text = "2,000,000";
                        break;

                    case "4":
                        this.txtId.Text = "TT/09224/00570";
                        txtTeller1.Text = this.UserInfo.Username;
                        txtTeller2.Text = this.UserInfo.Username;
                        txtCustomerName.SetTextDefault("Nguyễn Thị Hoa");
                        txtAddress.SetTextDefault("63 Bùi Thị Xuân");
                        txtPhoneNo.Text = "0989922561";
                        rcbCurrencyBought.SelectedValue = "USD";
                        rcbDebitAccount.SelectedValue = "USD";
                        txtAmount.Value = 750;
                        txtNarrative.Text = "THU DOI USD";

                        rcbCreditAccount.SelectedValue = "VND";

                        txtExchangeRate.Value = 20000;
                        txtAmtPayCust.Text = "15,000,000";

                        txtRate100.Value = 20000;
                        txtUnit100.Value = 6;
                        lbAmtLcy100.Text = "12,000,000";

                        txtRate50.Value = 20000;
                        txtUnit50.Value = 3;
                        lbAmtLcy50.Text = "3,000,000";
                        break;

                    case "5":
                        this.txtId.Text = "TT/09224/00510";
                        txtTeller1.Text = this.UserInfo.Username;
                        txtTeller2.Text = this.UserInfo.Username;
                        txtCustomerName.SetTextDefault("Đỗ Tường Vy");
                        txtAddress.SetTextDefault("1632/5 CMT8");
                        txtPhoneNo.Text = "09071112244";
                        rcbCurrencyBought.SelectedValue = "USD";
                        rcbDebitAccount.SelectedValue = "USD";
                        txtAmount.Value = 100;
                        txtNarrative.Text = "THU DOI USD";

                        rcbCreditAccount.SelectedValue = "VND";

                        txtExchangeRate.Value = 20000;
                        txtAmtPayCust.Text = "2,000,000";

                        txtRate100.Value = 20000;
                        txtUnit100.Value = 1;
                        lbAmtLcy100.Text = "2,000,000";
                     
                        break;
                }
            }
        }
        private void FirstLoad()
        {
            Id++;
            txtCustomerName.SetTextDefault("");
            txtAddress.SetTextDefault("");
            this.txtId.Text = "TT/09224/00" + Id.ToString();
            txtTeller1.Text = this.UserInfo.Username;
            txtTeller2.Text = this.UserInfo.Username;
            txtValueDate.SelectedDate = DateTime.Today;
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