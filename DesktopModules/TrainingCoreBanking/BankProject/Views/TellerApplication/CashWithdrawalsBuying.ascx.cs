using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace BankProject.Views.TellerApplication
{
    public partial class CashWithdrawalsBuying : PortalModuleBase
    {
        private void LoadToolBar(bool isauthorise)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            dvAudit.Visible = isauthorise;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.txtId.Text = GenerateTTId();
            txtExchangeRate.Value = 0;
            loaddataPreview();
            bool IsAuthorize = (Request.QueryString["IsAuthorize"] != null);
            LoadToolBar(IsAuthorize);
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, !IsAuthorize);
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case BankProject.Controls.Commands.Commit:
                    try
                    {
                        BankProject.DataProvider.Teller.InsertCashWithrawalForBuyingTC(txtId.Text, cmbCustomerAccount.SelectedItem.Text.Split('-')[0].Trim(), lbCurrency.Text, txtExchangeRate.Value, txtAmtLCY.Value, txtAmtFCY.Value, cmbCurrencyPaid.SelectedItem.Text, txtDealRate.Value, lblAmtPaidToCust.Value, txtTellerId.Text, cmbWaiveCharges.SelectedItem.Value, txtNarrative.Text, this.UserInfo.Username);
                        BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                        this.txtId.Text = GenerateTTId();
                        RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    }
                    catch (Exception err)
                    {
                        ShowMsgBox("This Loan Working Account was already created by Officer, Please try with another Currency !");
                    }
                    break;
                case BankProject.Controls.Commands.Preview:
                    Response.Redirect(EditUrl("chitiet"));
                    break;
                case BankProject.Controls.Commands.Authozize:
                case BankProject.Controls.Commands.Reverse:
                    try
                    {
                        if (commandName.Equals(BankProject.Controls.Commands.Authozize))
                            BankProject.DataProvider.Teller.UpdateCashWithrawalForBuyingTC(txtId.Text, BankProject.DataProvider.TransactionStatus.AUT);
                        else
                            BankProject.DataProvider.Teller.UpdateCashWithrawalForBuyingTC(txtId.Text, BankProject.DataProvider.TransactionStatus.REV);
                        BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                        BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                        LoadToolBar(false);
                        //txtTellerId.Text = this.UserInfo.Username.ToString();
                        this.txtId.Text = GenerateTTId();
                    }
                    catch (Exception err)
                    {
                        ShowMsgBox("Error : " + err.Message);
                    }
                    break;
            }
        }

        void loaddataPreview()
        {
            cmbCustomerAccount.DataSource = BankProject.DataProvider.Teller.AccountForBuyingTC();
            cmbCustomerAccount.DataValueField = "Value";
            cmbCustomerAccount.DataTextField = "Title";
            cmbCustomerAccount.DataBind();
            //
            cmbCurrencyPaid.DataSource = BankProject.DataProvider.Teller.ExchangeRate();
            cmbCurrencyPaid.DataValueField = "Value";
            cmbCurrencyPaid.DataTextField = "Title";
            cmbCurrencyPaid.DataBind();
            //
            if (Request.QueryString["LCCode"] != null)
            {
                string LCCode = Request.QueryString["LCCode"].ToString();                
                /*switch (LCCode)
                { 
                    case "1":
                        txtId.Text = "TT/09161/078911";
                        cmbCustomerAccount.SelectedValue = "0";
                        lbCustomer.Text = "16123";
                        lbCustomerName.Text = "BANK OF SHANGHAI";
                        lbCurrency.Text = "USD";
                        txtAmtFCY.Value = 100;
                        cmbCurrencyPaid.SelectedValue = "USD";
                        txtDealRate.Value = 7;
                        txtTellerId.Text = "140001";
                        cmbWaiveCharges.SelectedValue = "YES";
                        txtNarrative.Text = "NOP TM DE MUA SEC TRANG";
                        break;

                    case "2":
                        txtId.Text = "TT/09161/078912";
                        cmbCustomerAccount.SelectedValue = "1";
                        lbCustomer.Text = "16548";
                        lbCustomerName.Text = "CITI BANK NEWYORK";
                        lbCurrency.Text = "USD";
                        txtAmtFCY.Value = 200;
                        cmbCurrencyPaid.SelectedValue = "USD";
                        txtDealRate.Value = 5.8;
                        txtTellerId.Text = "140002";
                        cmbWaiveCharges.SelectedValue = "YES";
                        txtNarrative.Text = "NOP TM DE MUA SEC TRANG";
                        break;
                }*/
                DataTable dt = BankProject.DataProvider.Teller.CashWithrawalForBuyingTCDetail(LCCode);
                DataRow dr = dt.Rows[0];
                txtId.Text = dr["TransID"].ToString();
                //cmbCustomerAccount.SelectedValue = dr["Account"] + " - " + dr["AccountTitle"];
                cmbCustomerAccount.SelectedIndex = 1;
                lbCustomer.Text = dr["CustomerID"].ToString();
                lbCustomerName.Text = dr["AccountTitle"].ToString();
                lbCurrency.Text = dr["Currency"].ToString();
                txtAmtFCY.Value = Convert.ToDouble(dr["AmtFCY"]);
                txtAmtLCY.Value = Convert.ToDouble(dr["AmtLCY"]);
                for (int i = 0; i < cmbCurrencyPaid.Items.Count; i++)
                {
                    if (cmbCurrencyPaid.Items[i].Text.Equals(dr["CurrencyPaid"].ToString())){
                        cmbCurrencyPaid.SelectedIndex = i;
                        break;
                    }
                }
                txtExchangeRate.Value = Convert.ToDouble(dr["ExchangeRate"]);
                txtDealRate.Value = Convert.ToDouble(dr["DealRate"]);
                txtTellerId.Text = dr["TellerId"].ToString();
                cmbWaiveCharges.SelectedValue = dr["WaiveCharges"].ToString();
                txtNarrative.Text = dr["Narrative"].ToString();
                lblAmtPaidToCust.Value = Convert.ToDouble(dr["AmtPaidToCust"]);
            }
        }

        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }

        private string GenerateTTId()
        {
            return BankProject.DataProvider.SQLData.B_BMACODE_GetNewID("FOREIGNEXCHANGE", "TT", ".");
        }
    }
}