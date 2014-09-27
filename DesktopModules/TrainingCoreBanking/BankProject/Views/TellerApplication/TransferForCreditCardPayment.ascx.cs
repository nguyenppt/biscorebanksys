using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BankProject.DataProvider;
using System.Data;

namespace BankProject.Views.TellerApplication
{
    public partial class TransferForCreditCardPayment : PortalModuleBase
    {
        private  string refix_BMACODE()
        {
            return "TT";
        }
        public static int update_flag = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            FirstLoad();    
                if (Request.QueryString["ID"] != null)
                {
                    LoadToolBar(false);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    Load_data_detail(Request.QueryString["ID"]);
                }
                else
                {
                    LoadToolBar(true);
                    txtID.Text = TriTT.B_BMACODE_GetNewID_3par("TRS_CRED_CARD_PAYM", refix_BMACODE(), ".");
                    rdpValueDate.SelectedDate = DateTime.Now;
                    rdpValueDate2.SelectedDate = DateTime.Now;
                }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolbarbutton = e.Item as RadToolBarButton;
            var CommandName = toolbarbutton.CommandName;
            if (CommandName == "commit")
            {
                if (txtDebitAmtLCY.Value.Value > tbOldBalance.Value.Value)
                { ShowMsgBox("Can not OverDraf due to Debit Amount Value is grater than Old Balance Value.Please Check again !"); return; }
                DataSet ds = TriTT.BTRANSFER_4_CRE_CARD_PAYMENT_Load_Acct_Info(tbDebitAccount.Text, cmbDebitCurrency.SelectedValue);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    TriTT.BTRANSFER_4_CRE_CARD_PAYMENT_Insert(txtID.Text, "UNA",lblCustomerID.Text, lblCustomerName.Text, this.UserInfo.Username, cmbDebitCurrency.SelectedValue
                        , tbDebitAccount.Text, txtDebitAmtLCY.Value.HasValue ? txtDebitAmtLCY.Value.Value : 0, 0, tbOldBalance.Value.HasValue ? tbOldBalance.Value.Value : 0
                        , tbNewBalance.Value.HasValue ? tbNewBalance.Value.Value : 0, rdpValueDate.SelectedDate.ToString(), cmbCreditAccount.SelectedValue
                        , cmbCreditCurrency.SelectedValue, tbAmtCredit.Value.HasValue ? tbAmtCredit.Value.Value : 0, rdpValueDate2.SelectedDate.ToString(), tbCreditCardNum.Text
                        , cmbWaiveCharges.SelectedValue, txtNarrative.Text, txtNarrative2.Text);
                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                }
                else { ShowMsgBox("Debit Account does not exists, Please check again !"); return; }
            }
            if (CommandName == "Preview")
            {
                Response.Redirect(EditUrl("TransferForCreditCardPayment_PL"));
            }
            if (CommandName == "authorize" )
            {
                // check so du truoc khi tru tien vao field working amount
                var ds = TriTT.BTRANSFER_4_CRE_CARD_PAYMENT_Load_Acct_Info(tbDebitAccount.Text, cmbDebitCurrency.SelectedValue);
                if (ds.Tables == null || ds.Tables.Count ==0 || ds.Tables[0].Rows.Count == 0)
                    { ShowMsgBox("Your Account does not exist, Can not Authorize this Transaction !"); return; }
                else
                {
                    if ((txtDebitAmtLCY.Value.HasValue ? txtDebitAmtLCY.Value.Value : 0) > Convert.ToDouble(ds.Tables[0].Rows[0]["WorkingAmount"].ToString()))
                    { 
                        ShowMsgBox("Your Balance Amount Is "+ string.Format("{0:C}",Convert.ToDouble(ds.Tables[0].Rows[0]["WorkingAmount"].ToString())).Replace("$","") +" "+cmbDebitCurrency.SelectedValue
                            +" .Less than Debit Amount, can not Authorize this transaction !");
                        update_flag = 1;
                        return;
                    }
                }
                TriTT.BTRANSFER_4_CRE_CARD_PAYMENT_UpdateStatus(txtID.Text, "AUT", tbDebitAccount.Text, txtDebitAmtLCY.Value.HasValue ? txtDebitAmtLCY.Value.Value : 0
                    , cmbDebitCurrency.SelectedValue);
                Response.Redirect("Default.aspx?tabid=" + this.TabId);

            }
            if (CommandName == "reverse")
            {
                TriTT.BTRANSFER_4_CRE_CARD_PAYMENT_UpdateStatus(txtID.Text, "REV", tbDebitAccount.Text, txtDebitAmtLCY.Value.HasValue ? txtDebitAmtLCY.Value.Value : 0
                    , cmbDebitCurrency.SelectedValue);
                LoadToolBar(true);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                cmbCreditCurrency.Enabled = false;
                // update lai
                if (update_flag == 1)
                {
                    var ds = TriTT.BTRANSFER_4_CRE_CARD_PAYMENT_Load_Acct_Info(tbDebitAccount.Text, cmbDebitCurrency.SelectedValue);
                    tbOldBalance.Text = ds.Tables[0].Rows[0]["WorkingAmount"].ToString();
                    tbNewBalance.Text = "";
                    update_flag = 0;
                }
            }
        }
        protected void FirstLoad()
        {
            LoadCurrency();
        }
        #region Properties
        protected void Load_data_detail(string ID)
        {
            DataSet ds = TriTT.BTRANSFER_4_CRE_CARD_PAYMENT_Load_detail_data(ID);
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtID.Text = dr["ID"].ToString();
                lblCustomerID.Text = dr["DebitCustomerID"].ToString();
                lblCustomerName.Text = dr["DebitCustomerName"].ToString();
                cmbCreditCurrency.SelectedValue= cmbDebitCurrency.SelectedValue = dr["DebitCurrency"].ToString();
                tbDebitAccount.Text = dr["DebitAccount"].ToString();
                txtDebitAmtLCY.Text = dr["DebitAmt"].ToString();
                tbOldBalance.Text = dr["OldBalance"].ToString();
                tbNewBalance.Text = dr["NewBalance"].ToString();
                if (dr["ValueDate"].ToString() != "")
                {
                    rdpValueDate.SelectedDate = Convert.ToDateTime(dr["ValueDate"].ToString());
                }
                if (dr["ValueDate2"].ToString() != "")
                {
                    rdpValueDate2.SelectedDate = Convert.ToDateTime(dr["ValueDate2"].ToString());
                }
                LoadCreditAcct(cmbDebitCurrency.SelectedValue);
                tbAmtCredit.Text = dr["CreditAmt"].ToString();
                tbCreditCardNum.Text = dr["CreditCardNumber"].ToString();
                cmbWaiveCharges.SelectedValue = dr["WaiveCharges"].ToString();
                txtNarrative.Text = dr["Narrative"].ToString();
                txtNarrative2.Text = dr["Narrative2"].ToString();
                if (dr["Status"].ToString() == "AUT")
                {
                    LoadToolBar_AllFalse();
                }
            }
        }
        protected void LoadCurrency()
        {
            var Currency = TriTT.B_LoadCurrency("", "");
            cmbCreditCurrency.Items.Clear();
            cmbCreditCurrency.Items.Add(new RadComboBoxItem(""));
            cmbCreditCurrency.AppendDataBoundItems = true;
            cmbCreditCurrency.DataValueField = "Code";
            cmbCreditCurrency.DataTextField = "Code";
            cmbCreditCurrency.DataSource = Currency;
            cmbCreditCurrency.DataBind();

            cmbDebitCurrency.Items.Clear();
            cmbDebitCurrency.Items.Add(new RadComboBoxItem(""));
            cmbDebitCurrency.AppendDataBoundItems = true;
            cmbDebitCurrency.DataValueField = "Code";
            cmbDebitCurrency.DataTextField = "Code";
            cmbDebitCurrency.DataSource = Currency;
            cmbDebitCurrency.DataBind();
        }
        protected void LoadToolBar(bool isauthorize)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = !isauthorize;
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
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        #endregion

        protected void btSearch_Click(object sender, EventArgs e)
        {
            Load_Acct_Info(tbDebitAccount.Text, cmbDebitCurrency.SelectedValue);
        }
        protected void Load_Acct_Info(string AccountID, string Currency)
        {
            lblCustomerID.Text = ""; lblCustomerName.Text = ""; lblAccountTitle.Text = ""; tbOldBalance.Text = "";
             var ds = TriTT.BTRANSFER_4_CRE_CARD_PAYMENT_Load_Acct_Info(AccountID, Currency);
             if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
             {
                 DataRow dr = ds.Tables[0].Rows[0];
                 lblCustomerID.Text = dr["CustomerId"].ToString();
                 lblCustomerName.Text = dr["CustomerNAME"].ToString();
                 lblAccountTitle.Text = dr["AccountTitle"].ToString();
                 tbOldBalance.Text = dr["WorkingAmount"].ToString();
             }
             else lblAccountTitle.Text = "Account does not exist !";
            
        }
        protected void cmbDebitCurrency_OnselectedIndexChanged(object sender, EventArgs e)
        {
            LoadCreditAcct(cmbDebitCurrency.SelectedValue);
        }
        protected void LoadCreditAcct(string DebitCurrency)
        {
            cmbCreditCurrency.SelectedValue = cmbDebitCurrency.SelectedValue;
            var Account = TriTT.COLLECTION_4_CRE_CARD_PAYMENT_LoadAcct(DebitCurrency);
            cmbCreditAccount.Items.Clear();
            cmbCreditAccount.Items.Add(new RadComboBoxItem("", ""));
            cmbCreditAccount.AppendDataBoundItems = true;
            cmbCreditAccount.DataTextField = "ThuChiHoHasName";
            cmbCreditAccount.DataValueField = "ThuChiHoAccount";
            cmbCreditAccount.DataSource = Account;
            cmbCreditAccount.DataBind();
            cmbCreditAccount.SelectedIndex = 1;
        }

    }
}