using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Data;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using System.Globalization;

namespace BankProject.Views.TellerApplication.ForeignExchange
{
    public partial class ForeignExchange : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //
            DataTable tList = bd.Teller.ExchangeRate();
            bc.Commont.initRadComboBox(ref cboDebitCurrency, "Title", "Value", tList);
            bc.Commont.initRadComboBox(ref cboCurrencyPaid, "Title", "Value", tList);
            cboCurrencyPaid.SelectedValue = "VND";
            //
            tList = bd.Teller.InternalBankAccount();
            bc.Commont.initRadComboBox(ref cboDebitAccount, "Display", "Account", tList);
            bc.Commont.initRadComboBox(ref cboCreditAccount, "Display", "Account", tList);
            //
            if (Request.QueryString["tid"] != null)
            {
                txtId.Text = Request.QueryString["tid"];
                string TTNo = txtId.Text;
                DataTable dt = bd.Teller.ForeignExchangeDetailOrList(TTNo, null);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    bc.Commont.SetEmptyFormControls(this.Controls);
                    return;
                }
                DataRow dr = dt.Rows[0];
                //
                txtCustomerName.Text = dr["CustomerName"].ToString();
                txtAddress.Text = dr["CustomerAddress"].ToString();
                txtPassportNo.Text = dr["CustomerPassportNo"].ToString();
                if (dr["CustomerPassportDateOfIssue"] != DBNull.Value && !String.IsNullOrEmpty(dr["CustomerPassportDateOfIssue"].ToString()))
                    txtDateOfIsssue.SelectedDate = DateTime.ParseExact(dr["CustomerPassportDateOfIssue"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                txtPlaceOfIss.Text = dr["CustomerPassportPlaceOfIssue"].ToString();
                txtPhoneNo.Text = dr["CustomerPhoneNo"].ToString();
                //
                txtTellerId.Text = dr["TellerID"].ToString();
                for (int i = 0; i < cboDebitCurrency.Items.Count; i++)
                {
                    if (cboDebitCurrency.Items[i].Text.Equals(dr["DebitCurrency"].ToString()))
                    {
                        cboDebitCurrency.SelectedIndex = i;
                        break;
                    }
                }
                cboDebitAccount.SelectedValue = dr["DebitAccount"].ToString();
                if (dr["DebitAmtLCY"] != DBNull.Value)
                    txtDebitAmtLCY.Value = Convert.ToDouble(dr["DebitAmtLCY"]);
                if (dr["DebitAmtFCY"] != DBNull.Value)
                    txtDebitAmtFCY.Value = Convert.ToDouble(dr["DebitAmtFCY"]);
                //
                for (int i = 0; i < cboCurrencyPaid.Items.Count; i++)
                {
                    if (cboCurrencyPaid.Items[i].Text.Equals(dr["CurrencyPaid"].ToString()))
                    {
                        cboCurrencyPaid.SelectedIndex = i;
                        break;
                    }
                }
                txtCrTellerId.Text = dr["CrTellerId"].ToString();
                cboCreditAccount.SelectedValue = dr["CreditAccount"].ToString();
                if (dr["DealRate"] != DBNull.Value)
                    txtDealRate.Value = Convert.ToDouble(dr["DealRate"]);                
                if (dr["AmountPaid"] != DBNull.Value)
                    txtAmountPaid.Value = Convert.ToDouble(dr["AmountPaid"]);
                //
                txtNarrative.Text = dr["Narrative"].ToString();
                //
                bc.Commont.SetTatusFormControls(this.Controls, false);
                //
                if (!String.IsNullOrEmpty(Request.QueryString["lst"]))
                    loadToolBar(dr["Status"].ToString());
                else
                    loadToolBar(null);
            }
            else
            {
                this.txtId.Text = bd.Teller.GenerateTTId();
                txtTellerId.Text = this.UserInfo.Username;
                txtCrTellerId.Text = this.UserInfo.Username;
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
                        string DateOfIsssue = "";
                        if (txtDateOfIsssue.SelectedDate != null)
                            DateOfIsssue = txtDateOfIsssue.SelectedDate.Value.ToString("yyyyMMdd");
                        //                        
                        bd.Teller.ForeignExchangeUpdate("new", txtId.Text, txtCustomerName.Text, txtAddress.Text, txtPassportNo.Text, DateOfIsssue, txtPlaceOfIss.Text, txtPhoneNo.Text,
                            txtTellerId.Text, cboDebitCurrency.SelectedValue, cboDebitAccount.SelectedValue, txtDebitAmtLCY.Value, txtDebitAmtFCY.Value, cboCurrencyPaid.SelectedValue, 
                            txtCrTellerId.Text, cboCreditAccount.SelectedValue, txtDealRate.Value, txtAmountPaid.Value, txtNarrative.Text, this.UserInfo.Username);
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Save data success !", "Default.aspx?TabId=" + TabId);
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
                            bd.Teller.ForeignExchangeUpdateStatus(txtId.Text, bd.TransactionStatus.AUT, this.UserInfo.Username);
                            bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Authozize complete !", "Default.aspx?TabId=" + TabId);
                        }
                        else
                        {
                            bd.Teller.ForeignExchangeUpdateStatus(txtId.Text, bd.TransactionStatus.REV, this.UserInfo.Username);
                            bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Reverse complete !", "Default.aspx?TabId=" + TabId);
                        }
                    }
                    catch (Exception err)
                    {
                        lblMessage.Text = err.StackTrace;
                        bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Error : " + err.Message);
                    }
                    break;
            }
        }
    }
}