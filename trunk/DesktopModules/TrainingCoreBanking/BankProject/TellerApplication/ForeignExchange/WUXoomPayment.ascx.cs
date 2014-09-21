using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication.ForeignExchange
{
    public partial class WUXoomPayment : PortalModuleBase
    {
        private static int Id = 7890;
        private void LoadToolBar(bool isauthorise)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.txtId.Text = "TT/09161/0" + Id.ToString();
            txtTellerIDCR.Text = this.UserInfo.Username;
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
                txtTellerIDCR.Text = this.UserInfo.Username;
                this.txtId.Text = "TT/09161/0" + Id.ToString();
                //this.cmbCustomerAccount.SelectedIndex = 0;
                //this.txtAmtFCY.Text = string.Empty;
                //this.txtAmtLCY.Text = string.Empty;
                //this.txtNarrative.Text = string.Empty;

                //this.cmbCurrencyPaid.SelectedIndex = 0;
                //this.txtDealRate.Text = string.Empty;
                //this.cmbWaiveCharges.SelectedIndex = 0;
            }

            if(commandName=="preview")
            {
                Response.Redirect(EditUrl("chitiet"));
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
                        //cmbCustomerAccount.SelectedValue = "0";
                        //txtAmtFCY.Value = 100;
                        //cmbTCCurrency.SelectedValue = "USD";
                        //txtDealRate.Value = 7;
                        //txtTellerId.Text = "140001";
                        //cmbWaiveCharges.SelectedValue = "YES";
                        txtNarrative.Text = "NOP TM DE MUA SEC TRANG";
                        break;

                    case "2":
                        txtId.Text = "TT/09161/078912";
                        //cmbCustomerAccount.SelectedValue = "1";
                        //txtAmtFCY.Value = 200;
                        //cmbTCCurrency.SelectedValue = "USD";
                        //txtDealRate.Value = 5.8;
                        //txtTellerId.Text = "140002";
                        //cmbWaiveCharges.SelectedValue = "YES";
                        txtNarrative.Text = "NOP TM DE MUA SEC TRANG";
                        break;
                }
            }
        }
    }
}