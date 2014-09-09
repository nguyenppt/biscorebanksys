using System;
using Telerik.Web.UI;
using System.Data;
using System.Globalization;

namespace BankProject.Views.TellerApplication
{
    public partial class SellTravellersCheque : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //
            try
            {
                cmbTCCurrency.DataSource = BankProject.DataProvider.Teller.ExchangeRate();
                cmbTCCurrency.DataValueField = "Value";
                cmbTCCurrency.DataTextField = "Title";
                cmbTCCurrency.DataBind();
                //
                rcbDrCurrency.DataSource = BankProject.DataProvider.Teller.ExchangeRate();
                rcbDrCurrency.DataValueField = "Value";
                rcbDrCurrency.DataTextField = "Title";
                rcbDrCurrency.DataBind();
                /*
                rcbDebitAccount.DataSource = BankProject.DataProvider.SQLData.B_BDRFROMACCOUNT_GetAll();//BankProject.DataProvider.Database.B_BDRFROMACCOUNT_GetByCustomer
                rcbDebitAccount.DataValueField = "Id";
                rcbDebitAccount.DataTextField = "Display";//DisplayHasCurrency
                rcbDebitAccount.DataBind();
                */
                rcbCrAccount.DataSource = BankProject.DataProvider.SQLData.B_BINTERNALBANKPAYMENTACCOUNT_GetAll();
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
            if (Request.QueryString["TTNo"] != null)
            {
                txtId.Text = Request.QueryString["TTNo"];
                string TTNo = txtId.Text;
                DataTable dt = BankProject.DataProvider.Teller.SellTravellersChequeDetailOrList(TTNo, null);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
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
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                //
                loadToolBar(dr["Status"].ToString());
            }
            else
            {
                this.txtId.Text = BankProject.DataProvider.Teller.GenerateTTId();
                txtTellerId.Text = this.UserInfo.UserID + "";
                //
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                //
                loadToolBar(null);
            }
        }

        private void loadToolBar(string Status)
        {
            if (Status == null) Status = "";
            RadToolBar1.FindItemByValue("btCommitData").Enabled = String.IsNullOrEmpty(Status);
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = Status.Equals(BankProject.DataProvider.TransactionStatus.NAU);
            RadToolBar1.FindItemByValue("btReverse").Enabled = Status.Equals(BankProject.DataProvider.TransactionStatus.AUT);
            RadToolBar1.FindItemByValue("btSearch").Enabled = true;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            dvAudit.Visible = Status.Equals(BankProject.DataProvider.TransactionStatus.AUT);
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
                        string DateOfIsssue = "";
                        if (tbDateOfIsssue.SelectedDate != null)
                            DateOfIsssue = tbDateOfIsssue.SelectedDate.Value.ToString("yyyyMMdd");
                        //                        
                        BankProject.DataProvider.Teller.SellTravellersChequeUpdate("new", txtId.Text, tbCustomerName.Text, tbAddress.Text, tbPassportNo.Text, DateOfIsssue, tbPlaceOfIss.Text, txtPhoneNo.Text,
                            txtTellerId.Text, cmbTCCurrency.SelectedValue, rcbDebitAccount.SelectedValue, tbTCAmount.Value, rcbDrCurrency.SelectedValue, rcbCrAccount.SelectedValue, tbAmountDebited.Value, tbExchangeRate.Value, txtNarrative.Text, this.UserInfo.Username);
                        BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                        this.txtId.Text = BankProject.DataProvider.Teller.GenerateTTId();                        
                    }
                    catch (Exception err)
                    {
                        lblMessage.Text = err.StackTrace;
                        ShowMsgBox("Error : " + err.Message);
                    }
                    break;
                case BankProject.Controls.Commands.Preview:
                    
                    break;
                case BankProject.Controls.Commands.Authozize:
                case BankProject.Controls.Commands.Reverse:
                    try
                    {
                        if (commandName.Equals(BankProject.Controls.Commands.Authozize))
                            BankProject.DataProvider.Teller.SellTravellersChequeUpdateStatus(txtId.Text, BankProject.DataProvider.TransactionStatus.AUT, this.UserInfo.Username);
                        else
                            BankProject.DataProvider.Teller.SellTravellersChequeUpdateStatus(txtId.Text, BankProject.DataProvider.TransactionStatus.REV, this.UserInfo.Username);
                        BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                        BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                        this.txtId.Text = BankProject.DataProvider.Teller.GenerateTTId();
                        loadToolBar(null);
                    }
                    catch (Exception err)
                    {
                        lblMessage.Text = err.StackTrace;
                        ShowMsgBox("Error : " + err.Message);
                    }
                    break;
            }
        }

        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }

        protected void RadAjaxPanelDebitAcc_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            switch (e.Argument)
            {
                case "loadDebitAcc":
                    string Currency = "";
                    if (cmbTCCurrency.SelectedIndex >= 0)
                        Currency = cmbTCCurrency.Items[cmbTCCurrency.SelectedIndex].Text;
                    rcbDebitAccount.DataSource = BankProject.DataProvider.Database.B_BDRFROMACCOUNT_GetByCustomer(tbCustomerName.Text, Currency);
                    rcbDebitAccount.DataValueField = "Id";
                    rcbDebitAccount.DataTextField = "DisplayHasCurrency";
                    rcbDebitAccount.DataBind();
                    break;
                default:
                    break;
            }
        }
    }
}