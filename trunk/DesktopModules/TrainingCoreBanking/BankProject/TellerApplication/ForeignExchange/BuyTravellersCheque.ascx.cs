using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Data;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using System.Globalization;

namespace BankProject.Views.TellerApplication.ForeignExchange
{
    public partial class BuyTravellersCheque : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //
            DataTable tList = bd.Teller.ExchangeRate();
            bc.Commont.initRadComboBox(ref cmbTCCurrency, "Title", "Value", tList);
            bc.Commont.initRadComboBox(ref rcbCurrencyPaid, "Title", "Value", tList);
            //
            bc.Commont.initRadComboBox(ref rcbDrAccount, "Display", "Account", bd.SQLData.B_BINTERNALBANKPAYMENTACCOUNT_GetAll().Tables[0]);
            bc.Commont.initRadComboBox(ref rcbCrAccount, "Display", "Account", bd.Teller.CashAccount());
            //
            bc.Commont.initRadComboBox(ref rcbTCIssuer, "Text", "Value", bd.Teller.TellerForeignExchangeIssuer(TabId));
            //
            if (Request.QueryString["tid"] != null)
            {
                txtId.Text = Request.QueryString["tid"];
                string TTNo = txtId.Text;
                DataTable dt = bd.Teller.BuyTravellersChequeDetailOrList(TTNo, null);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    bc.Commont.SetEmptyFormControls(this.Controls);
                    return;
                }
                DataRow dr = dt.Rows[0];
                //
                tbCustomerName.Text = dr["CustomerName"].ToString();
                tbAddress.Text = dr["CustomerAddress"].ToString();
                tbPassportNo.Text = dr["CustomerPassportNo"].ToString();
                if (dr["CustomerPassportDateOfIssue"] != DBNull.Value && !String.IsNullOrEmpty(dr["CustomerPassportDateOfIssue"].ToString()))
                    tbDateOfIsssue.SelectedDate = DateTime.ParseExact(dr["CustomerPassportDateOfIssue"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                tbPlaceOfIss.Text = dr["CustomerPassportPlaceOfIssue"].ToString();
                txtPhoneNo.Text = dr["CustomerPhoneNo"].ToString();
                //
                txtTellerId.Text = dr["TellerID"].ToString();
                for (int i = 0; i < cmbTCCurrency.Items.Count; i++)
                {
                    if (cmbTCCurrency.Items[i].Text.Equals(dr["TCCurrency"].ToString()))
                    {
                        cmbTCCurrency.SelectedIndex = i;
                        break;
                    }
                }
                rcbDrAccount.SelectedValue = dr["DrAccount"].ToString();
                tbTCAmount.Value = Convert.ToDouble(dr["TCAmount"]);
                //
                for (int i = 0; i < rcbCurrencyPaid.Items.Count; i++)
                {
                    if (rcbCurrencyPaid.Items[i].Text.Equals(dr["CurrencyPaid"].ToString()))
                    {
                        rcbCurrencyPaid.SelectedIndex = i;
                        break;
                    }
                }
                tbTellerIDCR.Text = dr["CrTellerID"].ToString();
                rcbCrAccount.SelectedValue = dr["CrAccount"].ToString();
                if (dr["ExchangeRate"] != DBNull.Value)
                    tbExchangeRate.Value = Convert.ToDouble(dr["ExchangeRate"]);
                if (dr["ChargeAmtLCY"] != DBNull.Value)
                    tbChargeAmtLCY.Value = Convert.ToDouble(dr["ChargeAmtLCY"]);
                if (dr["ChargeAmtFCY"] != DBNull.Value)
                    tbChargeAmtFCY.Value = Convert.ToDouble(dr["ChargeAmtFCY"]);
                if (dr["AmountPaid"] != DBNull.Value)
                    tbAmountPaid.Value = Convert.ToDouble(dr["AmountPaid"]);
                txtNarrative.Text = dr["Narrative"].ToString();
                //
                rcbTCIssuer.SelectedValue = dr["TCIssuer"].ToString();
                rcbDenomination.SelectedValue = dr["Denomination"].ToString();
                rcbUnit.Text = dr["Unit"].ToString();
                rcbSerialNo.Text = dr["SerialNo"].ToString();
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
                txtTellerId.Text = this.UserInfo.UserID + "";
                //
                bc.Commont.SetTatusFormControls(this.Controls, true);
                //
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
                        if (tbDateOfIsssue.SelectedDate != null)
                            DateOfIsssue = tbDateOfIsssue.SelectedDate.Value.ToString("yyyyMMdd");
                        //                        
                        bd.Teller.BuyTravellersChequeUpdate("new", txtId.Text, tbCustomerName.Text, tbAddress.Text, tbPassportNo.Text, DateOfIsssue, tbPlaceOfIss.Text, txtPhoneNo.Text,
                            txtTellerId.Text, cmbTCCurrency.SelectedValue, rcbDrAccount.SelectedValue, tbTCAmount.Value, rcbCurrencyPaid.SelectedValue, tbTellerIDCR.Text, rcbCrAccount.SelectedValue,
                            tbExchangeRate.Value, tbChargeAmtLCY.Value, tbChargeAmtFCY.Value, tbAmountPaid.Value, txtNarrative.Text, 
                            rcbTCIssuer.SelectedValue, rcbDenomination.SelectedValue, rcbUnit.Text, rcbSerialNo.Text, this.UserInfo.Username);
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
                            bd.Teller.BuyTravellersChequeUpdateStatus(txtId.Text, bd.TransactionStatus.AUT, this.UserInfo.Username);
                            bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Authozize complete !", "Default.aspx?TabId=" + TabId);
                        }
                        else
                        {
                            bd.Teller.BuyTravellersChequeUpdateStatus(txtId.Text, bd.TransactionStatus.REV, this.UserInfo.Username);
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