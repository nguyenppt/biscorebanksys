using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using BankProject.DataProvider;
using System.Data;
namespace BankProject.FTTeller
{
    public partial class InwardProcessCreditAcc : PortalModuleBase
    {
        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            FirstLoad();

            if (Request.QueryString["ID"] != null)
            {
                LoadToolBar(false);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                Load_Data_2Aut(Request.QueryString["ID"].ToString());
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
                    if (lblDebitCurrency.Text != rcbCreditCurrency.SelectedValue && txtDealRate.Text == "")
                    { ShowMsgBox("Deal Rate value is not inputted. Please check again !"); return; }
                double dealrate = (txtDealRate.Value.HasValue ? txtDealRate.Value.Value : 0);
                if (lblDebitCurrency.Text == rcbCreditCurrency.SelectedValue) dealrate = 1;
                TriTT.BINWARD_PROC_CRE_ACCT_Insert(tbId.Text,"UNA", tbClearingID.Text, lblDebitCurrency.Text, lbDebitAccount.Text,
                    tbDebitAmtLCY.Value.HasValue? tbDebitAmtLCY.Value.Value: 0, tbDebitAmtFCY.Value.HasValue ? tbDebitAmtFCY.Value.Value : 0 ,
                    dealrate, tbDebitAmt.Value.HasValue ? tbDebitAmt.Value.Value : 0,txtBOName.Text, rcbCreditCurrency.SelectedValue, rcbCreditAccount.SelectedValue
                    , rcbCreditAccount.Text, tbCrAmtLCY.Value.HasValue ? tbCrAmtLCY.Value.Value : 0, tbCrAmtFCY.Value.HasValue? tbCrAmtFCY.Value.Value : 0,
                    tbFOName.Text, tbFOName2.Text, tbIdentityCard.Text, tbIsssueDate.SelectedDate != null? tbIsssueDate.SelectedDate : null, tbPhone.Text
                    , tbIsssuePlace.Text, tbNarrative.Text, tbNarrative2.Text);
                Response.Redirect("Default.aspx?tabid="+this.TabId.ToString());
            }

            if(commandName=="Preview")
            {
                Response.Redirect(EditUrl("chitiet"));
            }

            if (commandName == "authorize") 
            {
                if (TriTT.BINWARD_CASH_WITHDRAW_Load_Status(tbId.Text, "CreditAcct") == "AUT")
                {
                    ShowMsgBox("This transation was authorized , You can not authorized it again!"); return;
                }
                TriTT.BINWARD_CASH_WITHDRAW_Update_Status(tbId.Text, "AUT", "CreditAcct");
                Response.Redirect("Default.aspx?tabid=" + this.TabId.ToString());

            }
            if (commandName == "reverse")
            {
                TriTT.BINWARD_CASH_WITHDRAW_Update_Status(tbId.Text, "REV", "CreditAcct");
                LoadToolBar(true);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
            }
        }
        
        #endregion

        #region Properties
        protected void Load_Data_2Aut(string ID)
        {
            DataSet ds = TriTT.BINWARD_CASH_WITHDRAW_Load_Preview_Data(ID, "CreditAcct");
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                tbId.Text = dr["ID"].ToString();
                tbClearingID.Text = dr["ClearingID"].ToString();
                lblDebitCurrency.Text = dr["DebitCurrency"].ToString();
                lbDebitAccount.Text = dr["DebitAccount"].ToString();
                tbDebitAmtLCY.Text = dr["DebitAmtLCY"].ToString();
                tbDebitAmtFCY.Text = dr["DebitAmtFCY"].ToString();
                txtDealRate.Text = dr["DealRate"].ToString();
                txtBOName.Text = dr["BOName"].ToString();
                tbDebitAmt.Text = dr["TempAmt"].ToString();
                rcbCreditCurrency.SelectedValue = dr["CreditCurrency"].ToString();
                rcbCreditAccount.SelectedValue = dr["CreditAcctID"].ToString();
                tbCrAmtLCY.Text = dr["CreditAmtLCY"].ToString();
                tbCrAmtFCY.Text = dr["CreditAmtFCY"].ToString();
                tbFOName.Text = dr["FOName"].ToString();
                tbFOName2.Text = dr["FOName2"].ToString();
                tbIdentityCard.Text = dr["LegalID"].ToString();
                if (dr["IssueDate"].ToString() != "") tbIsssueDate.SelectedDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                tbPhone.Text = dr["Telephone"].ToString();
                tbIsssuePlace.Text = dr["IssuePlace"].ToString();
                tbNarrative.Text = dr["Narrative"].ToString();
                tbNarrative2.Text = dr["Narrative2"].ToString();
                if (dr["Status"].ToString() == "AUT")
                {
                    LoadToolBar_AllFalse();
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                }
            }
        }
        private void FirstLoad()
        {
            tbId.Text = TriTT.B_BMACODE_3part_varMaCode_varSP("B_BMACODE_3part_varMaCode_varSP", "INWARD_ACCT_ID", "TT");
            LoadCurrency();
            LoadCreditAccount();
        }
        protected void tbClearing_TextChanged(object sender, EventArgs e)
        {
            Load_Detail_ID();
        }
        protected void Load_Detail_ID()
        {
            DataSet ds = TriTT.BINWARD_PROC_CRE_ACCT_Load_Detail_ID(tbClearingID.Text);
            DataRow dr = ds.Tables[0].Rows[0];
            lblDebitCurrency.Text = dr["Currency"].ToString();
            lbDebitAccount.Text = dr["DebitAcctID"].ToString();
            tbDebitAmt.Text = dr["DebitAmount"].ToString();
            txtBOName.Text = dr["SendingName"].ToString();
            tbFOName.Text = dr["ReceivingName"].ToString();
            tbFOName2.Text = dr["ReceivingName2"].ToString();
            tbIdentityCard.Text = dr["LegalID"].ToString();
            if (dr["IssueDate"].ToString() != "") tbIsssueDate.SelectedDate = Convert.ToDateTime(dr["IssueDate"].ToString());
            tbPhone.Text = dr["Phone"].ToString();
            tbIsssuePlace.Text = dr["IssuePlace"].ToString();
            //tbNarrative.Text = dr["Narrative"].ToString();
            //tbNarrative2.Text = dr["Narrative2"].ToString();
        }
        //protected void Load_ClearingID()
        //{
        //    var ClearingID = TriTT.BINWARD_PROC_CRE_ACCT_Load_ClearingID();
        //    rcbClearingID.Items.Clear();
        //    rcbClearingID.Items.Add(new RadComboBoxItem(""));
        //    rcbClearingID.AppendDataBoundItems = true;
        //    rcbClearingID.DataValueField = "ID";
        //    rcbClearingID.DataTextField = "ID";
        //    rcbClearingID.DataSource = ClearingID;
        //    rcbClearingID.DataBind();
        //}
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
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
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