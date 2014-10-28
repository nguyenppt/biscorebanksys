using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using BankProject.DataProvider;
using System.Data;
namespace BankProject.FTTeller
{
    public partial class InwardCashWithdraw : PortalModuleBase
    {
        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            FirstLoad();
            if (Request.QueryString["RefID"] != null)
            {
                LoadToolBar(false);
                loaddataPreview(Request.QueryString["RefID"].ToString());
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }
            else 
            {
                LoadToolBar(true);
            }

        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                string CreditAcct_Curency = rcbCreditAccount.Text.Substring(0, 3);
                if (CreditAcct_Curency != rcbCreditCurrency.SelectedValue)
                { ShowMsgBox("Credit Account and Credit Currency are not matched, Please check again !"); return; }
                else
                    if (lbDebitCurrency.Text != rcbCreditCurrency.SelectedValue && txtDealRate.Text =="")
                    { ShowMsgBox("Deal Rate value is not inputted. Please check again !"); return; }
                double dealrate = (txtDealRate.Value.HasValue ? txtDealRate.Value.Value : 0);
                if (lbDebitCurrency.Text == rcbCreditCurrency.SelectedValue) dealrate = 1;
                TriTT.BINWARD_CASH_WITHDRAW_Insert(txtId.Text, "UNA", rcbClearingID.SelectedValue, lbDebitCurrency.Text, lbDebitAccount.Text, tbDebitAmtLCY.Value.HasValue ? tbDebitAmtLCY.Value.Value : 0,
                    tbDebitAmtFCY.Value.HasValue ? tbDebitAmtFCY.Value.Value : 0, dealrate, rcbCreditAccount.SelectedValue,
                    rcbCreditAccount.Text, rcbCreditCurrency.SelectedValue, tbCreditAmtLCY.Value.HasValue ? tbCreditAmtLCY.Value.Value : 0,
                    tbCreditAmtFCY.Value.HasValue ? tbCreditAmtFCY.Value.Value : 0, txtBOName.Text, txtFOName.Text, txtIdentityCard.Text,txtIsssueDate.SelectedDate.HasValue? txtIsssueDate.SelectedDate : null
                    , txtIsssuePlace.Text, tbPhone.Text, txtNarrative.Text, txtNarrative2.Text, UserInfo.Username);
                Response.Redirect( "Default.aspx?tabid=171");
            }
            if(commandName=="Preview")
            {
                Response.Redirect(EditUrl("chitiet"));
            }

            if (commandName == "authorize") 
            {
                if (TriTT.BINWARD_CASH_WITHDRAW_Load_Status(txtId.Text, "CashWithdraw") == "AUT")
                {
                    ShowMsgBox("This transation was authorized , You can not authorized it again!"); return;
                }
                TriTT.BINWARD_CASH_WITHDRAW_Update_Status(txtId.Text, "AUT", "CashWithdraw");
                Response.Redirect("Default.aspx?tabid=171");
            }
            if (commandName == "reverse")
            {
                TriTT.BINWARD_CASH_WITHDRAW_Update_Status(txtId.Text, "REV", "CashWithdraw");
                LoadToolBar(true);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                LoadInfo_forREV();
            }
        }
        protected void rcbClearingID_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataSet ds = TriTT.B_BINWARD_CASH_WITHDRAW_Load_ID_Info(rcbClearingID.SelectedValue);
            DataRow dr = ds.Tables[0].Rows[0];
            lbDebitCurrency.Text = dr["Currency"].ToString();
            lbDebitAccount.Text = dr["CreditAcctID"].ToString();
            tbAmount.Text = dr["Amount"].ToString();
            txtBOName.Text = dr["SendingName"].ToString();
            txtFOName.Text = dr["ReceivingName"].ToString();
            txtIdentityCard.Text = dr["IdentityCard"].ToString();
            txtIsssuePlace.Text = dr["IssuePlace"].ToString();
            if (dr["IssueDate"].ToString() != "")
            {
                txtIsssueDate.SelectedDate = Convert.ToDateTime(dr["IssueDate"].ToString());
            }
        }
        protected void LoadInfo_forREV()
        {
            DataSet ds = TriTT.B_BINWARD_CASH_WITHDRAW_Load_ID_Info(rcbClearingID.SelectedValue);
            DataRow dr = ds.Tables[0].Rows[0];
            tbAmount.Text = dr["Amount"].ToString();
        }
    
        #endregion

        #region method
        void loaddataPreview(string ID)
        {
            DataSet ds = TriTT.BINWARD_CASH_WITHDRAW_Load_Preview_Data(ID, "CashWithdraw");
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtId.Text = dr["ID"].ToString();
                lbDebitCurrency.Text = dr["DebitCurrency"].ToString();
                rcbClearingID.SelectedValue = dr["ClearingID"].ToString();
                lbDebitAccount.Text = dr["DebitAccount"].ToString();
                if (dr["DebitAmtLCY"].ToString() != "0.00") tbDebitAmtLCY.Text = dr["DebitAmtLCY"].ToString();
                if (dr["DebitAmtFCY"].ToString() != "0.00") tbDebitAmtFCY.Text = dr["DebitAmtFCY"].ToString();
                txtDealRate.Text = dr["DealRate"].ToString();
                rcbCreditAccount.SelectedValue = dr["CreditAcctID"].ToString();
                rcbCreditCurrency.SelectedValue = dr["CreditCurrency"].ToString();
                if (dr["CreditAmtLCY"].ToString() != "0.00") tbCreditAmtLCY.Text = dr["CreditAmtLCY"].ToString();
                if (dr["CreditAmtFCY"].ToString() != "0.00") tbCreditAmtFCY.Text = dr["CreditAmtFCY"].ToString();
                txtBOName.Text = dr["BOName"].ToString();
                txtFOName.Text = dr["FOName"].ToString();
                txtIdentityCard.Text = dr["IdentityCard"].ToString();
                if (dr["IssueDate"].ToString() != "") txtIsssueDate.SelectedDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                tbPhone.Text = dr["Telephone"].ToString();
                txtIsssuePlace.Text = dr["IssuePlace"].ToString();
                txtNarrative.Text = dr["Narrative1"].ToString();
                txtNarrative2.Text = dr["Narrative2"].ToString();
                if (dr["Status"].ToString() == "AUT")
                {
                    LoadToolBar_AllFalse();
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                }
            }
        }
        private void FirstLoad()
        {
            this.txtId.Text = TriTT.B_BMACODE_GetNewID_3part_new("B_BMACODE_GetNewID_3part_new", "INWARD_CASH_ID", "TT", ".");
            LoadClearingID();
            LoadCurrency();
            LoadCreditAccount();
        }
        protected void LoadClearingID()
        {
            rcbClearingID.Items.Clear();
            rcbClearingID.Items.Add(new RadComboBoxItem(""));
            rcbClearingID.AppendDataBoundItems = true;
            rcbClearingID.DataTextField = "ID";
            rcbClearingID.DataValueField = "ID";
            rcbClearingID.DataSource = BankProject.DataProvider.TriTT.Load_ClearingID();
            rcbClearingID.DataBind();
        }
        protected void LoadCreditAccount()
        {
            var InternalBankAccount = Teller.InternalBankAccount();
            rcbCreditAccount.Items.Clear();
            rcbCreditAccount.Items.Add(new RadComboBoxItem(""));
            rcbCreditAccount.DataValueField = "ACCOUNT";
            rcbCreditAccount.DataTextField = "Display";
            rcbCreditAccount.DataSource = InternalBankAccount;
            rcbCreditAccount.DataBind();
        }
        protected void LoadCurrency()
        {
            var Currency = TriTT.B_LoadCurrency("", "");
            rcbCreditCurrency.Items.Clear();
            rcbCreditCurrency.Items.Add(new RadComboBoxItem(""));
            rcbCreditCurrency.AppendDataBoundItems = true;
            rcbCreditCurrency.DataValueField = "Code";
            rcbCreditCurrency.DataTextField = "Code";
            rcbCreditCurrency.DataSource = Currency;
            rcbCreditCurrency.DataBind();
        }
        private void LoadToolBar(bool isauthorise)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btPreview").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btReverse").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void LoadToolBar_AllFalse()
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled =false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        #endregion
    }
}