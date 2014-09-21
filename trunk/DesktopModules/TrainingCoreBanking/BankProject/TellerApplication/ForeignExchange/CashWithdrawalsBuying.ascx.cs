using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;

namespace BankProject.Views.TellerApplication.ForeignExchange
{
    public partial class CashWithdrawalsBuying : PortalModuleBase
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //
            bc.Commont.initRadComboBox(ref cmbCustomerAccount, "Display", "AccountNo", bd.Teller.AccountForBuyingTC());
            bc.Commont.initRadComboBox(ref cmbCurrencyPaid, "Title", "Value", bd.Teller.ExchangeRate());
            //
            if (!String.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                bc.Commont.SetTatusFormControls(this.Controls, false);
                txtId.Text = Request.QueryString["tid"];
                string TTNo = txtId.Text;
                DataTable dt = bd.Teller.CashWithrawalForBuyingTCDetail(TTNo);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    lblMessage.Text = "This TT does not exist.";                    
                    return;
                }
                DataRow dr = dt.Rows[0];
                txtId.Text = dr["TransID"].ToString();
                cmbCustomerAccount.SelectedValue = dr["Account"].ToString();
                lbCustomer.Text = dr["CustomerID"].ToString();
                lbCustomerName.Text = dr["CustomerName"].ToString();
                lbCurrency.Text = dr["Currency"].ToString();                
                txtAmtFCY.Value = Convert.ToDouble(dr["AmtFCY"]);
                if (dr["AmtFCY"] != DBNull.Value)
                    txtAmtLCY.Value = Convert.ToDouble(dr["AmtLCY"]);
                for (int i = 0; i < cmbCurrencyPaid.Items.Count; i++)
                {
                    if (cmbCurrencyPaid.Items[i].Text.Equals(dr["CurrencyPaid"].ToString()))
                    {
                        cmbCurrencyPaid.SelectedIndex = i;
                        break;
                    }
                }
                txtExchangeRate.Value = Convert.ToDouble(dr["ExchangeRate"]);
                if (dr["DealRate"] != DBNull.Value)
                    txtDealRate.Value = Convert.ToDouble(dr["DealRate"]);
                txtTellerId.Text = dr["TellerId"].ToString();
                cmbWaiveCharges.SelectedValue = dr["WaiveCharges"].ToString();
                txtNarrative.Text = dr["Narrative"].ToString();
                lblAmtPaidToCust.Value = Convert.ToDouble(dr["AmtPaidToCust"]);
                //
                if (!String.IsNullOrEmpty(Request.QueryString["lst"]))
                    loadToolBar(null);
                else
                    loadToolBar(dr["Status"].ToString());
            }
            else
            {
                this.txtId.Text = bd.Teller.GenerateTTId();
                txtTellerId.Text = this.UserInfo.UserID + "";
                //
                bc.Commont.SetTatusFormControls(this.Controls, true);
                loadToolBar(null);
            }
        }

        private void loadToolBar(string Status)
        {
            if (Status == null) Status = "";
            RadToolBar1.FindItemByValue("btCommitData").Enabled = String.IsNullOrEmpty(Status);
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = Status.Equals(bd.TransactionStatus.UNA);
            RadToolBar1.FindItemByValue("btReverse").Enabled = Status.Equals(bd.TransactionStatus.UNA);
            RadToolBar1.FindItemByValue("btSearch").Enabled = true;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            dvAudit.Visible = Status.Equals(bd.TransactionStatus.AUT);
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case bc.Commands.Commit:
                    try
                    {
                        bd.Teller.InsertCashWithrawalForBuyingTC(txtId.Text, cmbCustomerAccount.SelectedItem.Text.Split('-')[0].Trim(), lbCurrency.Text, txtExchangeRate.Value, txtAmtLCY.Value, txtAmtFCY.Value, cmbCurrencyPaid.SelectedItem.Text, txtDealRate.Value, lblAmtPaidToCust.Value, txtTellerId.Text, cmbWaiveCharges.SelectedItem.Value, txtNarrative.Text, this.UserInfo.Username);
                        bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Save data success !");
                        bc.Commont.SetEmptyFormControls(this.Controls);
                        this.txtId.Text = bd.Teller.GenerateTTId();
                    }
                    catch (Exception err)
                    {
                        lblMessage.Text = err.StackTrace;
                        bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Error : " + err.Message);
                    }
                    break;
                case bc.Commands.Preview:
                    
                    break;
                case bc.Commands.Authorize:
                case bc.Commands.Reverse:
                    try
                    {
                        if (commandName.Equals(bc.Commands.Authorize))
                        {
                            bd.Teller.UpdateCashWithrawalForBuyingTC(txtId.Text, bd.TransactionStatus.AUT);
                            bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Authozize complete !");
                        }
                        else
                        {
                            bd.Teller.UpdateCashWithrawalForBuyingTC(txtId.Text, bd.TransactionStatus.REV);
                            bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Reverse complete !");
                        }
                        bc.Commont.SetEmptyFormControls(this.Controls);
                        bc.Commont.SetTatusFormControls(this.Controls, true);
                        this.txtId.Text = bd.Teller.GenerateTTId();
                        loadToolBar(null);                        
                    }
                    catch (Exception err)
                    {
                        lblMessage.Text = err.StackTrace;
                        bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Error : " + err.Message);
                    }
                    break;
            }
        }

        protected void RadAjaxPanelAccount_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (cmbCustomerAccount.SelectedIndex < 0) return;
            DataTable tDetail = bd.Teller.AccountForBuyingTC(cmbCustomerAccount.SelectedValue);
            if (tDetail == null || tDetail.Rows.Count <= 0) return;
            DataRow dr = tDetail.Rows[0];
            switch (e.Argument)
            {
                case "loadTopInfo":
                    lbCustomer.Text = dr["CustomerID"].ToString();
                    lbCustomerName.Text = dr["CustomerName"].ToString();
                    lbCurrency.Text = dr["Currency"].ToString();
                    txtExchangeRate.Value = Convert.ToDouble(dr["CurrencyRate"].ToString());
                    break;
            }
        }

        protected void cmbCustomerAccount_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblCustomerAccount.Text = "";
            DataTable tDetail = bd.Teller.AccountForBuyingTC(cmbCustomerAccount.SelectedValue);
            if (tDetail == null || tDetail.Rows.Count <= 0)
            {
                lblCustomerAccount.Text = "Account not found !";
                return;
            }
            DataRow dr = tDetail.Rows[0];
            //
            lbCustomer.Text = dr["CustomerID"].ToString();
            lbCustomerName.Text = dr["CustomerName"].ToString();
            lbCurrency.Text = dr["Currency"].ToString();
            txtExchangeRate.Value = Convert.ToDouble(dr["CurrencyRate"].ToString());
        }
    }
}