using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using BankProject.DataProvider;
using System.Data;

namespace BankProject.FTTeller
{
    public partial class OutwardTransferByCash : PortalModuleBase
    {
        private static int Id = 392;
        
        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            FirstLoad();
            if (Request.QueryString["ID"] != null)
            {
                loaddataPreview(Request.QueryString["ID"].ToString());
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
                TriTT.OUT_TRANS_BY_CASH_Insert_Update(txtId.Text, "UNA", rcbProductID.SelectedValue, rcbProductID.Text.Replace(rcbProductID.SelectedValue + " - ", ""),
                    rcbCurrency.SelectedValue, rcbBenCom.SelectedValue, rcbBenCom.Text.Replace(rcbBenCom.SelectedValue + " - ", ""), rcbCreditAccount.SelectedValue,
                    rcbCreditAccount.Text !=""? rcbCreditAccount.Text.Replace(rcbCreditAccount.SelectedValue + " - ", ""): "",
                    rcbCashAccount.SelectedValue, txtAmountLCY.Value.HasValue? txtAmountLCY.Value.Value:0, txtSendingName.Text,
                    tbAddress.Text, tbAddress2.Text, tbPhone.Text, tbReceivingName.Text, txtBenAccount.Text, rcbProvince.SelectedValue, rcbProvince.Text.Replace(rcbProvince.SelectedValue + " - ", "")
                    , rcbBankCode.SelectedValue,rcbBankCode.Text !=""? rcbBankCode.Text.Replace(rcbBankCode.SelectedValue + " - ", "") : "", txtIdentityCard.Text, txtIsssueDate.SelectedDate,
                    txtIsssuePlace.Text, txtTeller.Text, txtNarrative.Text, txtNarrative2.Text, txtNarrative3.Text, cmbWaiveCharges.SelectedValue, txtVatSerial.Text,
                    txtChargeAmtLCY.Value.HasValue? txtChargeAmtLCY.Value.Value : 0,txtChargeVatAmt.Value.HasValue? txtChargeVatAmt.Value.Value : 0);
                Response.Redirect("Default.aspx?tabid=158");
            }
            if (commandName == "Preview")
            {
                Response.Redirect(EditUrl("chitiet"));
            }
            if (commandName == "authorize") 
            {
                if (TriTT.BINWARD_CASH_WITHDRAW_Load_Status(txtId.Text, "trans_by_cash") == "AUT")
                {
                    ShowMsgBox("this transaction is authorized, you can not authorize it again !"); return;
                }
                DataSet ds = TriTT.OUT_TRANS_BY_CASH_Update_Status("AUT", txtId.Text, txtBenAccount.Text,txtAmountLCY.Value.HasValue? txtAmountLCY.Value.Value:0, rcbCurrency.SelectedValue);
                string BenAccount_Currency = "";
                if(ds.Tables != null && ds.Tables.Count >0 && ds.Tables[0].Rows.Count>0) BenAccount_Currency = ds.Tables[0].Rows[0]["Currency"].ToString();
                if (BenAccount_Currency != "")
                {
                    ShowMsgBox("Selected Ben Account is not "+ rcbCurrency.SelectedValue+" account. Please select the right account! The current Ben Account is "+
                        BenAccount_Currency +" account"); return;
                }
                Response.Redirect("Default.aspx?tabid=158");
            }
            if (commandName == "reverse")
            {
                TriTT.OUT_TRANS_BY_CASH_Update_Status("REV", txtId.Text,"",0,"");
                LoadToolBar(true);
            }
        }
        #endregion

        #region method
        void loaddataPreview(string ID)
        {
            DataSet ds = TriTT.OUT_TRANS_BY_CASH_Load_Preview(ID);
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            { 
                DataRow dr = ds.Tables[0].Rows[0];
                txtId.Text = dr["ID"].ToString();
                rcbProductID.SelectedValue = dr["ProductID"].ToString();
                rcbCurrency.SelectedValue = dr["Currency"].ToString(); 
                rcbBenCom.SelectedValue = dr["BenComID"].ToString();
                LoadCashAccount(rcbCurrency.SelectedValue);
                loadcreditacc();
                if (rcbProductID.SelectedValue == "1000")
                {
                    txtBenAccount.ReadOnly = true;
                    rcbProvince.Enabled = false;
                    rcbBankCode.Enabled = false; 
                }
                txtAmountLCY.Text = dr["Amount"].ToString();
                txtSendingName.Text = dr["SendingName"].ToString();
                tbAddress.Text = dr["SendingAddress1"].ToString();
                tbAddress2.Text = dr["SendingAddress2"].ToString();
                tbPhone.Text = dr["SendingPhone"].ToString();
                tbReceivingName.Text = dr["ReceivingName"].ToString();
                txtBenAccount.Text = dr["BenAccountID"].ToString();
                rcbProvince.SelectedValue = dr["ProvinceID"].ToString();
                LoadBankCode(rcbProvince.SelectedValue);
                rcbBankCode.SelectedValue = dr["BankCodeID"].ToString();
                txtIdentityCard.Text = dr["IdentityCard"].ToString();
                if (dr["IssueDate"].ToString() != "")
                {
                    txtIsssueDate.SelectedDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                }
                txtIsssuePlace.Text = dr["IssuePlace"].ToString();
                txtTeller.Text = dr["Teller"].ToString();
                txtNarrative.Text = dr["Narrative1"].ToString();
                txtNarrative2.Text = dr["Narrative2"].ToString();
                txtNarrative3.Text = dr["Narrative3"].ToString();
                cmbWaiveCharges.SelectedValue = dr["WaiveCharge"].ToString();
                if (dr["ChargeAmount"].ToString() != "0.0000")
                {
                    txtChargeAmtLCY.Text = dr["ChargeAmount"].ToString();
                }
                txtVatSerial.Text = dr["VatSerial"].ToString();
                if (dr["VATChargeAmount"].ToString() != "0.0000")
                {
                    txtChargeVatAmt.Text = dr["VATChargeAmount"].ToString();
                }
                LoadToolBar(false);
                if (dr["Status"].ToString() == "AUT")
                {
                    LoadToolBar_AllFalse();
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                }
            }
        }
        protected void txtBenAccount_TextChanged(object sender, EventArgs e)
        {
            Load_BenAccount_Info(txtBenAccount.Text);
        }
        protected void Load_BenAccount_Info(string BenAcctID)
        { 
            if (rcbProductID.SelectedValue == "3000")
            {
                DataSet ds = TriTT.OUTWARD_TRANFER_BY_ACCT_LoadBenAcct(BenAcctID);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    tbReceivingName.Text = dr["GBFullName"].ToString();
                    txtIdentityCard.Text = dr["DocID"].ToString();
                    txtIsssuePlace.Text = dr["DocIssuePlace"].ToString();
                    if (dr["DocIssueDate"].ToString() != "")
                        txtIsssueDate.SelectedDate = Convert.ToDateTime(dr["DocIssueDate"].ToString());
                    //tbPhone.Text = dr["MobilePhone"].ToString();
                }
            }
        }
        protected void LoadCashAccount(string Currency)
        {
            var Account = TriTT.COLLECTION_4_CRE_CARD_PAYMENT_LoadAcct(Currency);
            rcbCashAccount.Items.Clear();
            rcbCashAccount.Items.Add(new RadComboBoxItem("", ""));
            rcbCashAccount.AppendDataBoundItems = true;
            rcbCashAccount.DataTextField = "AccountHasName";
            rcbCashAccount.DataValueField = "ACCOUNT";
            rcbCashAccount.DataSource = Account;
            rcbCashAccount.DataBind();
            rcbCashAccount.SelectedIndex = 1;
        }
        private void FirstLoad()
        {
            Id++;
            this.txtId.Text = TriTT.B_BMACODE_GetNewID_3part_new("B_BMACODE_GetNewID_3part_new", "OUTWARD_CASH_ID", "TT", ".");
            txtTeller.Text = this.UserInfo.Username;
            txtChargeVatAmt.ReadOnly = true;
            txtChargeAmtLCY.ReadOnly = true;
            LoadCurrency();
            rcbProvince.Items.Clear();
            rcbProvince.Items.Add(new RadComboBoxItem("")); ;
            rcbProvince.DataSource = BankProject.DataProvider.Database.BPROVINCE_GetAll();
            rcbProvince.DataTextField = "TenTinhThanh";
            rcbProvince.DataValueField = "MaTinhThanh";
            rcbProvince.DataBind();

            rcbBenCom.Items.Clear();
            rcbBenCom.Items.Add(new RadComboBoxItem("")); ;
            rcbBenCom.DataSource = BankProject.DataProvider.Database.BENCOM_GetALL();
            rcbBenCom.DataTextField = "BENCOMNAME";
            rcbBenCom.DataValueField = "BENCOMID";
            rcbBenCom.DataBind();

        }

        void loadcreditacc()
        {
            string currency = rcbCurrency.SelectedValue;
            string bencom = rcbBenCom.SelectedValue;
            string product = rcbProductID.SelectedValue;
            rcbCreditAccount.Items.Clear();
            if (currency != "" && bencom != "" && product != "")
            {
                rcbCreditAccount.DataSource = BankProject.DataProvider.Database.BENCOM_SetCreditAccount_ByProduct(currency, bencom, product);
                rcbCreditAccount.DataTextField = "CREDITACCOUNT";
                rcbCreditAccount.DataValueField = "CREDITACCOUNT";
                rcbCreditAccount.DataBind();

                rcbCreditAccount.SelectedIndex = 0;
                rcbCreditAccount.Enabled = false;
            }
        }

        protected void rcbCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            loadcreditacc();
            LoadCashAccount(rcbCurrency.SelectedValue);
        }
        protected void rcbProductID_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (rcbProductID.SelectedValue)
            {
                case "3000":
                    txtBenAccount.ReadOnly = false;
                    rcbProvince.Enabled = true;
                    rcbBankCode.Enabled = true;
                    break;

                case "1000":
                    txtBenAccount.ReadOnly = true;
                    rcbProvince.Enabled = false;
                    rcbBankCode.Enabled = false;
                    break;

            }
            loadcreditacc();
        }
        protected void rcbProvince_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadBankCode(rcbProvince.SelectedValue);
        }
        protected void LoadBankCode(string ProvinceID)
        {
            DataSet ds = BankProject.DataProvider.Database.BBANKCODE_GetByProvince(rcbProvince.SelectedValue);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["BANKNAME"] = "";
                dr["BANKCODE"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);

                rcbBankCode.DataSource = ds;
                rcbBankCode.DataTextField = "BANKNAME";
                rcbBankCode.DataValueField = "BANKCODE";
                rcbBankCode.DataBind();
            }
        }
        protected void cmbWaiveCharges_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (cmbWaiveCharges.SelectedValue)
            {
                case "YES":
                    txtChargeAmtLCY.ReadOnly = true;
                    txtChargeVatAmt.ReadOnly = true;
                    txtChargeAmtLCY.Text = "";
                    txtChargeVatAmt.Text = "";
                    break;

                case "NO":
                    txtChargeAmtLCY.ReadOnly = false;
                    txtChargeVatAmt.ReadOnly = false;
                    break;
            }
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
            rcbCurrency.Items.Clear();
            rcbCurrency.Items.Add(new RadComboBoxItem(""));
            rcbCurrency.AppendDataBoundItems = true;
            rcbCurrency.DataValueField = "Code";
            rcbCurrency.DataTextField = "Code";
            rcbCurrency.DataSource = Currency;
            rcbCurrency.DataBind();
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
            loaddataPreview(txtId.Text);
        }
    }
}