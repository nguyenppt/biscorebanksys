using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Data;

namespace BankProject.Views.TellerApplication.ForeignExchange
{
    public partial class ForeignExchange : PortalModuleBase
    {
        private static int Id = 7890;
        private void LoadToolBar(bool isauthorise)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.txtId.Text = "TT/09161/0" + Id.ToString();
            txtTellerId.Text = this.UserInfo.Username;
            tbTellerIDCR.Text = this.UserInfo.Username;
            rcbCurrencyPaid.SelectedValue = "VND";
            dvAudit.Visible = false;

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
                //dvAudit.Visible = false;
            }

        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                Id++;
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                txtTellerId.Text = this.UserInfo.Username;
                tbTellerIDCR.Text = this.UserInfo.Username;
                rcbCurrencyPaid.SelectedValue = "VND";
                this.txtId.Text = "TT/09161/0" + Id.ToString();
                RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            }

            if (commandName == "Preview")
            {
                Response.Redirect(EditUrl("chitiet"));
            }

            if (commandName == "authorize" || commandName == "reverse")
            {
                Id++;
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                LoadToolBar(false);
                txtTellerId.Text = this.UserInfo.Username.ToString();
                this.txtId.Text = "TT/09161/0" + Id.ToString();
            }
        }

        void loaddataPreview()
        {
            if (Request.QueryString["LCCode"] != null)
            {
                string LCCode = Request.QueryString["LCCode"].ToString();
                switch (LCCode)
                { 
                    case "1":
                        txtId.Text = "TT/09161/078911";
                        tbCustomerName.Text = "Phan Van Han";
                        tbAddress.Text = "100 Phan Van Han, Phuong 17, Quan Binh Thanh";
                        txtPhoneNo.Text = "0909888999";
                        txtTellerId.Text = "140001";
                        tbTellerIDCR.Text = "140001";
                        cmbDebitCurrency.SelectedValue = "USD";
                        loadDebitAcc(tbCustomerName.Text);
                        rcbDebitAccount.SelectedIndex = 1;
                        rcbCrAccount.SelectedValue = "VND";
                        rcbCurrencyPaid.SelectedValue = "VND";
                        lblDebitAmtLCY.Text = lblAmountPaidToCust.Text = "17,791,000";
                        tbDebitAmtFCY.Text = "1,000.00";
                        tbDealRate.Text = "17,791";
                        txtNarrative.Text = "NOP TM DE MUA SEC TRANG";
                        break;

                    case "2":
                        txtId.Text = "TT/09161/078912";
                        tbCustomerName.Text = "Pham Ngoc Thach";
                        tbAddress.Text = "180 Pham Ngoc Thach, Phuong 1, Quan 1";
                        txtPhoneNo.Text = "09091234567";
                        txtTellerId.Text = "140002";
                        tbTellerIDCR.Text = "140002";
                        cmbDebitCurrency.SelectedValue = "USD";
                        loadDebitAcc(tbCustomerName.Text);
                        rcbDebitAccount.SelectedIndex = 1;
                        rcbCrAccount.SelectedValue = "VND";
                        rcbCurrencyPaid.SelectedValue = "VND";
                        lblDebitAmtLCY.Text = lblAmountPaidToCust.Text = "35,582,000";
                        tbDebitAmtFCY.Text = "2,000.00";
                        tbDealRate.Text = "17,791";
                        txtNarrative.Text = "NOP TM DE MUA SEC TRANG";
                        break;
                }
            }
        }

        void loadDebitAcc(string Custname)
        {
            rcbDebitAccount.Items.Clear();
            if (cmbDebitCurrency.SelectedValue != "")
            {
                DataSet ds = BankProject.DataProvider.Database.B_BDRFROMACCOUNT_OtherCustomer(Custname, cmbDebitCurrency.SelectedValue);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].NewRow();
                    dr["Display"] = "";
                    dr["Id"] = "";
                    dr["CustomerID"] = "";
                    dr["Name"] = "";
                    ds.Tables[0].Rows.InsertAt(dr, 0);

                    rcbDebitAccount.DataTextField = "Display";
                    rcbDebitAccount.DataValueField = "Id";
                    rcbDebitAccount.DataSource = ds;
                    rcbDebitAccount.DataBind();
                }
            }
        }

        protected void cmbDebitCurrency_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            loadDebitAcc("");
        }
    }
}