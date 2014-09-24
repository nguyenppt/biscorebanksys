using System;
using Telerik.Web.UI;
using System.Data;
using System.Globalization;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;

namespace BankProject.Views.TellerApplication.ForeignExchange
{
    public partial class SellTravellersCheque : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //
            try
            {
                DataTable tList = bd.Teller.ExchangeRate();
                bc.Commont.initRadComboBox(ref cmbTCCurrency, "Title", "Value", tList);
                bc.Commont.initRadComboBox(ref rcbDrCurrency, "Title", "Value", tList);
                //
                bc.Commont.initRadComboBox(ref rcbDebitAccount, "Display", "Account", bd.Teller.CashAccount());
                bc.Commont.initRadComboBox(ref rcbCrAccount, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetAll());
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
                rcbDebitAccount.SelectedValue = dr["DebitAccount"].ToString();
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
                    tbExchangeRate.Value = Convert.ToDouble(dr["ExchangeRate"]);
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
                        bd.Teller.SellTravellersChequeUpdate("new", txtId.Text, tbCustomerName.Text, tbAddress.Text, tbPassportNo.Text, DateOfIsssue, tbPlaceOfIss.Text, txtPhoneNo.Text,
                            txtTellerId.Text, cmbTCCurrency.SelectedValue, rcbDebitAccount.SelectedValue, tbTCAmount.Value, rcbDrCurrency.SelectedValue, rcbCrAccount.SelectedValue, tbAmountDebited.Value, tbExchangeRate.Value, txtNarrative.Text, this.UserInfo.Username);
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
                            bd.Teller.SellTravellersChequeUpdateStatus(txtId.Text, bd.TransactionStatus.AUT, this.UserInfo.Username);
                            bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Authozize complete !", "Default.aspx?TabId=" + TabId);
                        }
                        else
                        {
                            bd.Teller.SellTravellersChequeUpdateStatus(txtId.Text, bd.TransactionStatus.REV, this.UserInfo.Username);
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
        
        protected void btLoadDebitAccount_Click(object sender, EventArgs e)
        {
            /*string Currency = "";
            if (cmbTCCurrency.SelectedIndex >= 0)
                Currency = cmbTCCurrency.Items[cmbTCCurrency.SelectedIndex].Text;
            rcbDebitAccount.DataSource = bd.Database.B_BDRFROMACCOUNT_GetByCustomer(tbCustomerName.Text, Currency);
            rcbDebitAccount.DataValueField = "Id";
            rcbDebitAccount.DataTextField = "DisplayHasCurrency";
            rcbDebitAccount.DataBind();*/
        }
    }
}