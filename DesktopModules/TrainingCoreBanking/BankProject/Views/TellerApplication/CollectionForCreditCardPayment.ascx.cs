using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Data;
using BankProject.DataProvider;
namespace BankProject.Views.TellerApplication
{
    public partial class CollectionForCreditCardPayment : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
            //this.rdpIssueDate.SelectedDate = null;
            FirstLoad();
            if (Request.QueryString["ID"] != null)
            {
                LoadToolBar(false);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                LoadData_View(Request.QueryString["ID"]);
            }
            else
            {
                LoadToolBar(true);
                txtId.Text = TriTT.B_BMACODE_ID_3par_CHEQUE_TRANS("TT", ".", "COLLECTIONID");
                txtTellerId1.Text = this.UserInfo.Username;
            }
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "doclines")
            {
                double DealRate = 1;
                if (cmbCreditCurrency.SelectedValue != cmbDebitCurrency.SelectedValue)
                {
                    if (txtDealRate.Text != "")
                    { DealRate = Convert.ToDouble(txtDealRate.Value.Value); }
                    else { ShowMsgBox("DealRate value has no value, You must input DealRate value !"); return; }
                }
                TriTT.COLLECTION_4_CRE_CARD_PAYMENT_Insert_Update(txtId.Text, "UNA", cmbCustomerId.SelectedValue, txtFullName.Text, txtAddress.Text, txtLegalID.Text
                    , tbIssueDate.Text, txtTel.Text, txtPlaceOfIs.Text, txtTellerId1.Text, cmbDebitCurrency.SelectedValue, cmbDebitAccount.SelectedValue, txtDebitAmtLCY.Value.Value,
                     cmbCreditCurrency.SelectedValue, cmbCreditAccount.SelectedValue, DealRate, tbCreditAmt.Value.Value, txtCreditCardNumber.Text, cmbWaiveCharges.SelectedValue,
                    txtNarrative.Text, txtNarrative2.Text);
                Response.Redirect("Default.aspx?tabid="+ this.TabId.ToString());
            }

            if (commandName == "docnew")
            {
                Response.Redirect(EditUrl("chitiet"));
            }

            if (commandName == "draghand" ) 
            {
                TriTT.COLLECTION_4_CRE_CARD_PAYMENT_Update_Status(txtId.Text, "AUT");
                Response.Redirect("Default.aspx?tabid=" + this.TabId.ToString());
            }
            if (commandName == "reverse")
            {
                TriTT.COLLECTION_4_CRE_CARD_PAYMENT_Update_Status(txtId.Text, "REV");
                LoadToolBar(true);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
            }
        }

        protected void FirstLoad()
        {
            LoadCustomer();
            LoadCurrency();
        }
        #region properties
        protected void LoadData_View(string ID)
        {
            var ds = TriTT.COLLECTION_4_CRE_CARD_PAYMENT_Load_dataView(ID);
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var dr = ds.Tables[0].Rows[0];
                txtId.Text = ID;
                cmbCustomerId.SelectedValue = dr["CustomerID"].ToString();
                txtFullName.Text = dr["CustomerName"].ToString();
                txtAddress.Text = dr["Address"].ToString();
                txtLegalID.Text = dr["LegalID"].ToString();
                tbIssueDate.Text = dr["IssueDate"].ToString();
                txtTel.Text = dr["Telephone"].ToString();
                txtPlaceOfIs.Text = dr["PlaceOfIssue"].ToString();
                txtTellerId1.Text = dr["TellerID"].ToString();
                cmbDebitCurrency.SelectedValue = dr["DebitCurrency"].ToString();
                Load_Debit_Credit_Acct(cmbDebitCurrency.SelectedValue);
                txtDebitAmtLCY.Text = dr["DebitAmt"].ToString();
                cmbDebitAccount.SelectedValue = dr["DebitAccount"].ToString();
                cmbCreditAccount.SelectedValue = dr["CreditAccount"].ToString();
                cmbCreditCurrency.SelectedValue = dr["CreditCurrency"].ToString();
                if (dr["DealRate"].ToString() != "1.00000")
                {
                    txtDealRate.Text = dr["DealRate"].ToString();
                }
                tbCreditAmt.Text = dr["CreditAmt"].ToString();
                txtCreditCardNumber.Text = dr["CreditCardNumber"].ToString();
                cmbWaiveCharges.SelectedValue = dr["WaiveCharges"].ToString();
                txtNarrative.Text = dr["Narrative"].ToString();
                txtNarrative2.Text = dr["Narrative2"].ToString();
                if (dr["Status"].ToString() == "AUT")
                {
                    LoadToolBar_AllFlase();
                }
            }
        }
        protected void cmbDebitCurrency_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Debit_Credit_Acct(cmbDebitCurrency.SelectedValue);
        }
        protected void Load_Debit_Credit_Acct(string DebitCurrency)
        {
            var Account = TriTT.COLLECTION_4_CRE_CARD_PAYMENT_LoadAcct(DebitCurrency);
            cmbDebitAccount.Items.Clear();
            cmbDebitAccount.Items.Add(new RadComboBoxItem("", ""));
            cmbDebitAccount.AppendDataBoundItems = true;
            cmbDebitAccount.DataTextField = "AccountHasName";
            cmbDebitAccount.DataValueField = "ACCOUNT";
            cmbDebitAccount.DataSource = Account;
            cmbDebitAccount.DataBind();
            cmbDebitAccount.SelectedIndex = 1;

            cmbCreditAccount.Items.Clear();
            cmbCreditAccount.Items.Add(new RadComboBoxItem("", ""));
            cmbCreditAccount.AppendDataBoundItems = true;
            cmbCreditAccount.DataTextField = "ThuChiHoHasName";
            cmbCreditAccount.DataValueField = "ThuChiHoAccount";
            cmbCreditAccount.DataSource = Account;
            cmbCreditAccount.DataBind();
            cmbCreditAccount.SelectedIndex = 1;

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
        protected void LoadCustomer()
        {
            DataSet ds = TriTT.B_OPEN_LOANWORK_ACCT_Get_ALLCustomerID();
            if (ds != null && ds.Tables[0] != null)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["CustomerID"] = "";
                dr["CustomerHasName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            cmbCustomerId.DataTextField = "CustomerHasName";
            cmbCustomerId.DataValueField = "CustomerID";
            cmbCustomerId.DataSource = ds;
            cmbCustomerId.DataBind();
        }
        protected void cmbCustomerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerName"] = row["GBFullName"].ToString();
            e.Item.Attributes["Address"] = row["Address"].ToString();
            e.Item.Attributes["IdentityNo"] = row["DocID"].ToString();
            e.Item.Attributes["IssueDate"] = row["DocIssueDate2"].ToString();
            e.Item.Attributes["IssuePlace"] = row["DocIssuePlace"].ToString();
            e.Item.Attributes["Telephone"] = row["Telephone"].ToString();
        }
        private void LoadToolBar(bool isauthorise)
        {

            RadToolBar1.FindItemByValue("btdoclines").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btdocnew").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btdraghand").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btreverse").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("searchNew").Enabled = false;
            RadToolBar1.FindItemByValue("print").Enabled = false;
        }
        protected void LoadToolBar_AllFlase()
        {

            RadToolBar1.FindItemByValue("btdoclines").Enabled = false;
            RadToolBar1.FindItemByValue("btdocnew").Enabled = false;
            RadToolBar1.FindItemByValue("btdraghand").Enabled = false;
            RadToolBar1.FindItemByValue("btreverse").Enabled = false;
            RadToolBar1.FindItemByValue("searchNew").Enabled = false;
            RadToolBar1.FindItemByValue("print").Enabled = false;
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