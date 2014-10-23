using BankProject.Repository;
using DotNetNuke.Entities.Modules;
using System;
using System.Data;
using Telerik.Web.UI;
using BankProject.Common;
using BankProject.DataProvider;

namespace BankProject.Views.TellerApplication
{
    public partial class TransferWithdrawal : PortalModuleBase
    {
        private void LoadToolBar(bool IsAuthorize)
        {
            RadToolBar1.FindItemByValue("btCommit").Enabled = !IsAuthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !IsAuthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = IsAuthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = IsAuthorize;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = IsAuthorize;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack) return;

            if (Request.QueryString["preview"] == null)
            {
                this.ShowInputView();
                LoadToolBar(false);
            }
            else
            {
                this.ShowPreviewView();
                LoadToolBar(true);
            }

            //Session["DataKey"] = txtId.Text;
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;

            switch (commandName)
            {
                case "Commit":
                    if (hdfCheckOverdraft.Value == "0" || hdfCheckCredit.Value == "0" || hdfCheckDebit.Value == "0") return;

                    BankProject.DataProvider.Database.BTRANSFERWITHDRAWAL_Insert(rcbAccountType.SelectedValue, txtId.Text, lbDebitAccountId.Text, txtDebitAmt.Value.HasValue ? txtDebitAmt.Value.Value : 0, 
                        lblCustBal.Value.HasValue ? lblCustBal.Value.Value : 0, lblNewCustBal.Value.HasValue ? lblNewCustBal.Value.Value : 0,
                        rdpValueDate.SelectedDate, lbCreditAccountId.Text, lblAmtCreditForCust.Value.HasValue ? lblAmtCreditForCust.Value.Value : 0, txtDealRate.Value.HasValue ? txtDealRate.Value.Value : 0,
                        rdpCreditValueDate.SelectedDate, cmbWaiveCharges.SelectedValue, txtNarrative.Text, this.UserId);

                    if (cmbWaiveCharges.SelectedValue == "NO") Response.Redirect(EditUrl("waivecharges"));

                    Response.Redirect(string.Format("Default.aspx?tabid={0}", this.TabId.ToString()));
                    //this.EnableControls(true);
                    //this.SetDefaultValues();
                    //LoadToolBar(false);
                    break;

                case "Preview":
                    string unBlockAccountPreviewList = this.EditUrl("TransferWithdrawalPreviewList");
                    this.Response.Redirect(unBlockAccountPreviewList);
                    break;

                case "Authorize":
                    string Status = Database.BCASHDEPOSIT_LoadStatus(txtId.Text, "TransferWithdrawal");
                     if (Status != "AUT")
                     {
                         DataProvider.Database.BTRANSFERWITHDRAWAL_UpdateStatus(rcbAccountType.SelectedValue, "AUT", txtId.Text, this.UserId.ToString());
                         Response.Redirect(string.Format("Default.aspx?tabid={0}", this.TabId.ToString()));
                     }
                     else
                     {
                         ShowMsgBox("This transaction already authorized . You can not authorize it again !"); return;
                     }
                    break;

                case "Reverse":
                    DataProvider.Database.BTRANSFERWITHDRAWAL_UpdateStatus(rcbAccountType.SelectedValue, "REV", txtId.Text, this.UserId.ToString());
                    Response.Redirect(string.Format("Default.aspx?tabid={0}", this.TabId.ToString()));
                    //LoadToolBar(false);
                    //this.SetDefaultValues();
                    //this.EnableControls(true);
                    break;
            }
        }

        private void EnableControls(bool enable)
        {
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, enable);
        }

        private void ShowPreviewView()
        {
            if (Request.QueryString["preview"] == null)
                return;

            this.SetPreviewValues("");
            this.EnableControls(false);
        }

        private void ShowInputView()
        {
            this.SetDefaultValues();
        }

        private void SetPreviewValues(string code)
        {
            DataSet ds;
            if (code != "")
                ds = DataProvider.Database.BTRANSFERWITHDRAWAL_GetByCode(code);
            else
                ds = DataProvider.Database.BTRANSFERWITHDRAWAL_GetByID(int.Parse(Request.QueryString["codeid"].ToString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtId.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                this.cmbDebitAccount.Text = ds.Tables[0].Rows[0]["DebitAccountCode"].ToString();
                rcbAccountType.SelectedValue = ds.Tables[0].Rows[0]["AccountType"].ToString();
                cmbDebitAccount_TextChanged(cmbDebitAccount, null);
                this.txtDebitAmt.Value = ds.Tables[0].Rows[0]["DebitAmount"] != null && ds.Tables[0].Rows[0]["DebitAmount"] != DBNull.Value ?
                                                double.Parse(ds.Tables[0].Rows[0]["DebitAmount"].ToString()) : 0;

                this.lblCustBal.Value = ds.Tables[0].Rows[0]["CustBallance"] != null && ds.Tables[0].Rows[0]["CustBallance"] != DBNull.Value ?
                                                double.Parse(ds.Tables[0].Rows[0]["CustBallance"].ToString()) : 0;

                this.lblNewCustBal.Value = ds.Tables[0].Rows[0]["NewCustBallance"] != null && ds.Tables[0].Rows[0]["NewCustBallance"] != DBNull.Value
                                            ? double.Parse(ds.Tables[0].Rows[0]["NewCustBallance"].ToString()) : 0;

                if (ds.Tables[0].Rows[0]["DebitValueDate"] != null && ds.Tables[0].Rows[0]["DebitValueDate"] != DBNull.Value)
                    this.rdpValueDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["DebitValueDate"].ToString());

                this.cmbCreditAccount.Text = ds.Tables[0].Rows[0]["CreditAccountCode"].ToString();
                cmbCreditAccount_TextChanged(cmbCreditAccount, null);

                this.lblAmtCreditForCust.Value = ds.Tables[0].Rows[0]["AmountCreditForCustomer"] != null && ds.Tables[0].Rows[0]["AmountCreditForCustomer"] != DBNull.Value ?
                                            double.Parse(ds.Tables[0].Rows[0]["AmountCreditForCustomer"].ToString()) : 0;

                this.txtDealRate.Value = ds.Tables[0].Rows[0]["DealRate"] != null && ds.Tables[0].Rows[0]["DealRate"] != DBNull.Value ?
                                        double.Parse(ds.Tables[0].Rows[0]["DealRate"].ToString()) : 0;

                if (ds.Tables[0].Rows[0]["CreditValueDate"] != null && ds.Tables[0].Rows[0]["CreditValueDate"] != DBNull.Value)
                    this.rdpCreditValueDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreditValueDate"].ToString());

                this.cmbWaiveCharges.SelectedValue = ds.Tables[0].Rows[0]["WaiveCharges"].ToString();
                this.txtNarrative.Text = ds.Tables[0].Rows[0]["Narrative"].ToString();

                bool isautho = ds.Tables[0].Rows[0]["Status"].ToString() == "AUT";
                this.EnableControls(Request.QueryString["codeid"] == null && !isautho);
                LoadToolBar(Request.QueryString["codeid"] != null);

                if (isautho)
                {
                    RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                }
            }
        }

        private void SetDefaultValues()
        {
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
            string SoTT = BankProject.DataProvider.Database.B_BMACODE_GetNewSoTT("TRANSFERWITHDRAWAL").Tables[0].Rows[0]["SoTT"].ToString();
            this.txtId.Text = "TT." + Util.getIDDate() + "." + SoTT.PadLeft(5, '0');

            rdpValueDate.SelectedDate = DateTime.Today;
            rdpCreditValueDate.SelectedDate = DateTime.Today;
        }

        protected void cmbDebitAccount_TextChanged(object sender, EventArgs e)
        {
            if (cmbDebitAccount.Text != "" && rcbAccountType.SelectedValue != "")
            {
                DataSet ds = BankProject.DataProvider.Database.BOPENACCOUNT_GetByCode_OPEN(cmbDebitAccount.Text, rcbAccountType.SelectedValue);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblCustomerId.Text = ds.Tables[0].Rows[0]["CustomerId"].ToString();
                    lblCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    lbDebitAccountId.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                    cmbDebitCurrency.Text = ds.Tables[0].Rows[0]["Currency"].ToString();
                    lbDebitAccountTitle.Text = ds.Tables[0].Rows[0]["AccountTitle"].ToString();

                    if (ds.Tables[0].Rows[0]["WorkingAmount"] != null && ds.Tables[0].Rows[0]["WorkingAmount"] != DBNull.Value)
                        lblCustBal.Value = double.Parse(ds.Tables[0].Rows[0]["WorkingAmount"].ToString());
                    lbErrorDebitAccount.Visible = false;
                    hdfCheckDebit.Value = "1";
                }
                else
                {
                    hdfCheckDebit.Value = "0";
                    lbErrorDebitAccount.Visible = true;
                    lbErrorDebitAccount.Text = "Debit account does not exist";
                    cmbDebitAccount.Text = "";
                    lblCustomerId.Text = "";
                    lblCustomerName.Text = "";
                    lbDebitAccountTitle.Text = "";
                    cmbDebitCurrency.Text = "";
                    lblCustBal.Text = "";
                }
            }
            else
            {
                hdfCheckDebit.Value = "0";
                lbErrorDebitAccount.Text = "";
                cmbDebitAccount.Text = "";
                lblCustomerId.Text = "";
                lblCustomerName.Text = "";
                lbDebitAccountTitle.Text = "";
                cmbDebitCurrency.Text = "";
                lblCustBal.Text = "";
            }
        }

        protected void cmbCreditAccount_TextChanged(object sender, EventArgs e)
        {
            if (cmbCreditAccount.Text != "" && rcbAccountType.SelectedValue != "")
            {
                DataSet ds = BankProject.DataProvider.Database.BOPENACCOUNT_GetByCode_OPEN(cmbCreditAccount.Text, rcbAccountType.SelectedValue);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lbCustomerID_CR.Text = ds.Tables[0].Rows[0]["CustomerId"].ToString();
                    lbCustomerName_CR.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    lbCreditAccountId.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                    cmbCreditCurrency.Text = ds.Tables[0].Rows[0]["Currency"].ToString();
                    lbCreditAccountTitle.Text = ds.Tables[0].Rows[0]["AccountTitle"].ToString();
                    hdfCheckCredit.Value = "1";
                    lbErrorCreditAccount.Visible = false;
                }
                else
                {
                    hdfCheckCredit.Value = "0";
                    lbErrorCreditAccount.Visible = true;
                    lbErrorCreditAccount.Text = "Credit account does not exist";
                    cmbCreditAccount.Text = "";
                    lbCustomerID_CR.Text = "";
                    lbCustomerName_CR.Text = "";
                    lbCreditAccountTitle.Text = "";
                    cmbCreditCurrency.Text = "";
                }
            }
            else
            {
                hdfCheckCredit.Value = "0";
                lbErrorCreditAccount.Visible = false;
                cmbCreditAccount.Text = "";
                lbCustomerID_CR.Text = "";
                lbCustomerName_CR.Text = "";
                lbCreditAccountTitle.Text = "";
                cmbCreditCurrency.Text = "";
            }
        }

        protected void rcbAccountType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbDebitAccount_TextChanged(cmbDebitAccount, null);
            cmbCreditAccount_TextChanged(cmbCreditAccount, null);
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            SetPreviewValues(txtId.Text);//
        }
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
    }
}