using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Data;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using System.Globalization;

namespace BankProject.Views.TellerApplication.ForeignExchange
{
    public partial class ExchangeBanknotesManyDeno : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            litDenominations.Text = "";
            //
            DataTable tList = bd.Teller.ExchangeRate();
            bc.Commont.initRadComboBox(ref cboDebitCurrency, "Title", "Value", tList);
            //
            tList = bd.Teller.InternalBankAccount();
            bc.Commont.initRadComboBox(ref cboDebitAccount, "Display", "Account", tList);
            bc.Commont.initRadComboBox(ref cboCreditAccount, "Display", "Account", tList);
            //
            if (Request.QueryString["tid"] != null)
            {
                txtId.Text = Request.QueryString["tid"];
                string TTNo = txtId.Text;
                DataTable dt = bd.Teller.ExchangeBanknotesManyDenoDetailOrList(TTNo, null);
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
                if (dr["DebitAmount"] != DBNull.Value)
                    txtDebitAmount.Value = Convert.ToDouble(dr["DebitAmount"]);
                txtNarrative.Text = dr["Narrative"].ToString();
                if (dr["ValueDate"] != DBNull.Value)
                    txtValueDate.SelectedDate = Convert.ToDateTime(dr["ValueDate"]);
                //
                txtCrTellerId.Text = dr["CrTellerId"].ToString();
                cboCreditAccount.SelectedValue = dr["CreditAccount"].ToString();
                if (dr["ExchangeRate"] != DBNull.Value)
                    txtExchangeRate.Value = Convert.ToDouble(dr["ExchangeRate"]);
                if (dr["AmountPaid"] != DBNull.Value)
                    txtAmountPaid.Value = Convert.ToDouble(dr["AmountPaid"]);
                //
                string DenominationsNum = dr["Denomination_Num"].ToString();
                string DenominationsUnit = dr["Denomination_Unit"].ToString();
                string DenominationsRate = dr["Denomination_Rate"].ToString();
                if (!string.IsNullOrEmpty(DenominationsNum))
                {
                    trDenominations.Visible = false;
                    litDenominations.Text = "";
                    var arrNum = DenominationsNum.Split(',');
                    var arrUnit = DenominationsUnit.Split(',');
                    var arrRate = DenominationsRate.Split(',');
                    for (int i = 0; i < arrNum.Length; i++)
                    {
                        litDenominations.Text += "<tr>"
                                                    + "<td class=\"labelDisabled\" style=\"font-weight:bold; text-align:right; padding-right:5px; padding-bottom:5px;\">" + dr["DebitCurrency"] + "</td>"
                                                    + "<td style=\"padding-bottom:5px;\"><input type=\"text\" style=\"width:80px;\" value=\"" + arrNum[i].Trim() + "\" disabled=\"disabled\"></td>"
                                                    + "<td style=\"padding-bottom:5px;\"><input type=\"text\" style=\"width:80px;\" value=\"" + arrUnit[i].Trim() + "\" disabled=\"disabled\"></td>"
                                                    + "<td style=\"padding-bottom:5px;\"><input type=\"text\" style=\"width:80px;\" value=\"" + arrRate[i].Trim() + "\" disabled=\"disabled\"></td>"
                                                    + "<td style=\"padding-bottom:5px;\">" + (Convert.ToInt64(arrNum[i]) * Convert.ToInt64(arrUnit[i]) * Convert.ToInt64(arrRate[i])) + "</td>"
                                                    + "<td style=\"padding-bottom:5px;\"></td>"
                                                + "</tr>";
                    }
                }
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
                        bd.Teller.ExchangeBanknotesManyDenoUpdate("new", txtId.Text, txtCustomerName.Text, txtAddress.Text, txtPassportNo.Text, DateOfIsssue, txtPlaceOfIss.Text, txtPhoneNo.Text,
                            txtTellerId.Text, cboDebitCurrency.SelectedValue, cboDebitAccount.SelectedValue, txtDebitAmount.Value, txtNarrative.Text, txtValueDate.SelectedDate,
                            txtCrTellerId.Text, cboCreditAccount.SelectedValue, txtExchangeRate.Value, txtAmountPaid.Value, txtDenomination_Num.Text, txtDenomination_Unit.Text, txtDenomination_ExchangeRate.Text, this.UserInfo.Username);
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
                            bd.Teller.ExchangeBanknotesManyDenoUpdateStatus(txtId.Text, bd.TransactionStatus.AUT, this.UserInfo.Username);
                            bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Authozize complete !", "Default.aspx?TabId=" + TabId);
                        }
                        else
                        {
                            bd.Teller.ExchangeBanknotesManyDenoUpdateStatus(txtId.Text, bd.TransactionStatus.REV, this.UserInfo.Username);
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