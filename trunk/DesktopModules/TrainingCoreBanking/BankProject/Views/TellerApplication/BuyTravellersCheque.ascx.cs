using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Data;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using System.Globalization;

namespace BankProject.Views.TellerApplication
{
    public partial class BuyTravellersCheque : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //
            try
            {
                cmbTCCurrency.DataSource = bd.Teller.ExchangeRate();
                cmbTCCurrency.DataValueField = "Value";
                cmbTCCurrency.DataTextField = "Title";
                cmbTCCurrency.DataBind();
                //
                rcbCurrencyPaid.DataSource = bd.Teller.ExchangeRate();
                rcbCurrencyPaid.DataValueField = "Value";
                rcbCurrencyPaid.DataTextField = "Title";
                rcbCurrencyPaid.DataBind();
                //
                rcbCrAccount.DataSource = bd.SQLData.B_BINTERNALBANKPAYMENTACCOUNT_GetAll();
                rcbCrAccount.DataValueField = "Account";
                rcbCrAccount.DataTextField = "Display";
                rcbCrAccount.DataBind();
                //
            }
            catch (Exception err)
            {
                lblMessage.Text = err.StackTrace;
            }
            //
            if (Request.QueryString["tid"] != null)
            {
                txtId.Text = Request.QueryString["tid"];
                string TTNo = txtId.Text;
                DataTable dt = bd.Teller.SellTravellersChequeDetailOrList(TTNo, null);
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
                /*rcbDebitAccount.SelectedValue = dr["DebitAccount"].ToString();
                tbTCAmount.Value = Convert.ToDouble(dr["TCAmount"]);
                for (int i = 0; i < rcbDrCurrency.Items.Count; i++)
                {
                    if (rcbDrCurrency.Items[i].Text.Equals(dr["DrCurrency"].ToString()))
                    {
                        rcbDrCurrency.SelectedIndex = i;
                        break;
                    }
                }
                rcbCrAccount.SelectedValue = dr["CrAccount"].ToString();
                tbAmountDebited.Text = dr["AmountDebited"].ToString();
                tbExchangeRate.Value = null;
                if (dr["ExchangeRate"] != DBNull.Value)
                    tbExchangeRate.Value = Convert.ToDouble(dr["ExchangeRate"]);*/
                txtNarrative.Text = dr["Narrative"].ToString();
                //
                bc.Commont.SetTatusFormControls(this.Controls, false);
                //
                loadToolBar(dr["Status"].ToString());
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
            RadToolBar1.FindItemByValue("btReverse").Enabled = Status.Equals(bd.TransactionStatus.AUT);
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
                        /*bd.Teller.SellTravellersChequeUpdate("new", txtId.Text, tbCustomerName.Text, tbAddress.Text, tbPassportNo.Text, DateOfIsssue, tbPlaceOfIss.Text, txtPhoneNo.Text,
                            txtTellerId.Text, cmbTCCurrency.SelectedValue, rcbDebitAccount.SelectedValue, tbTCAmount.Value, rcbDrCurrency.SelectedValue, rcbCrAccount.SelectedValue, tbAmountDebited.Value, tbExchangeRate.Value, txtNarrative.Text, this.UserInfo.Username);*/
                        bc.Commont.SetEmptyFormControls(this.Controls);
                        this.txtId.Text = bd.Teller.GenerateTTId();
                        bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Save data success !");
                    }
                    catch (Exception err)
                    {
                        lblMessage.Text = err.StackTrace;
                        bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Error : " + err.Message);
                    }
                    break;
                case bc.Commands.Preview:

                    break;
                case bc.Commands.Authozize:
                case bc.Commands.Reverse:
                    try
                    {
                        if (commandName.Equals(bc.Commands.Authozize))
                        {
                            bd.Teller.SellTravellersChequeUpdateStatus(txtId.Text, bd.TransactionStatus.AUT, this.UserInfo.Username);
                            bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Authozize complete !");
                        }
                        else
                        {
                            bd.Teller.SellTravellersChequeUpdateStatus(txtId.Text, bd.TransactionStatus.REV, this.UserInfo.Username);
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

        protected void RadAjaxPanelDebitAcc_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            switch (e.Argument)
            {
                case "loadDebitAcc":
                    string Currency = "";
                    if (cmbTCCurrency.SelectedIndex >= 0)
                        Currency = cmbTCCurrency.Items[cmbTCCurrency.SelectedIndex].Text;
                    /*rcbDebitAccount.DataSource = bd.Database.B_BDRFROMACCOUNT_GetByCustomer(tbCustomerName.Text, Currency);
                    rcbDebitAccount.DataValueField = "Id";
                    rcbDebitAccount.DataTextField = "DisplayHasCurrency";
                    rcbDebitAccount.DataBind();*/
                    break;
                default:
                    break;
            }
        }
    }
}