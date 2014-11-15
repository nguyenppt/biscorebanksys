using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BankProject.DataProvider;

namespace BankProject.Views.TellerApplication
{
    public partial class CashRepayment : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            tbID.Text = TriTT.B_BMACODE_NewID_3par_CashRepayment("CASH_REPAYMENT", "TT");
            FirstLoad(); //// da load data cho cac combo Box
            if (Request.QueryString["ID"] != null)
            {
                LoadToolBar(false);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                LoadDetail(Request.QueryString["ID"].ToString());
            }
        }
        
        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            var commandName = ToolBarButton.CommandName;
            

            switch (commandName)
            { 
                case "commit":
                    if (lblNote.Text == "Account Customer ID is not exist !")
                    {
                        ShowMsgBox("Your Customer Account does not exist. Please check again"); return;
                    }
                    if (tbBalanceAmt.Value.Value < tbAmtLCYDeposited.Value.Value)
                    {
                        ShowMsgBox("Can not OverDraf. Maximum Balance Amount is " + string.Format("{0:C}",tbBalanceAmt.Value.Value).Replace("$","") + " " + rcbCurrency.SelectedValue + " .Please check again");
                        return;
                    }
                    decimal DealRate = 1;
                    if (rcbCurrency.SelectedValue != rcbCurrencyDeposited.SelectedValue)
                    {
                        if (tbDealRate.Text != "")
                        { DealRate = Convert.ToDecimal(tbDealRate.Value.Value); }
                        else { ShowMsgBox("DealRate value has no value, You must input DealRate value !"); return; }
                    }
                    decimal BalanceAmt =( tbBalanceAmt.Text ==""? 0: Convert.ToDecimal( tbBalanceAmt.Value.Value));
                    decimal NewBalanceAmt =( tbNewBalanceAmt.Text ==""? 0: Convert.ToDecimal( tbNewBalanceAmt.Value.Value));
                    TriTT.B_CASHREPAYMENT_Insert_Update(tbID.Text, "UNA", lblCustomerID.Text, lblCustomerName.Text, rcbCurrency.SelectedValue, tbCusomerAcct.Text,
                        BalanceAmt, NewBalanceAmt, tbTellerID.Text, rcbCurrencyDeposited.SelectedValue, rcbCashAccount.SelectedValue, rcbCashAccount.Text, Convert.ToDecimal(tbAmtLCYDeposited.Value.Value)
                        ,0 , DealRate, rcbWaiveCharge.SelectedValue, tbNarrative.Text, tbNarrative2.Text, Convert.ToDecimal(tbPrint.Value.HasValue? tbPrint.Value.Value:0));
                    Response.Redirect(string.Format("Default.aspx?tabid={0}",this.TabId));
                    break;
                case "Preview":
                    Response.Redirect(EditUrl("CashRepayment_PL"));
                    break;
                case "authorize":
                    // check so du cua tai khoan truoc khi gan new value
                     DataRow dr = TriTT.B_CASHREPAYMENT_LoadCustomerInfo(tbCusomerAcct.Text, rcbCurrency.SelectedValue).Tables[0].Rows[0];
                    if (tbAmtLCYDeposited.Value.Value <= Convert.ToDouble(dr["WorkingAmount"].ToString()))// check so tien rut' va so du con lai
                    {
                        TriTT.B_CASHREPAYMENT_UpdateStatus(tbID.Text, "AUT", tbCusomerAcct.Text, rcbCurrency.SelectedValue, tbAmtLCYDeposited.Value.Value);
                        Response.Redirect(string.Format("Default.aspx?tabid={0}", this.TabId));
                    }
                    else { ShowMsgBox("You can not execute this action, your Balance Amount is " +string.Format("{0:C}", Convert.ToDouble(dr["WorkingAmount"].ToString())).Replace("$","") +" "+ rcbCurrency.SelectedValue); return; }
                    break;
                case "reverse":
                    TriTT.B_CASHREPAYMENT_UpdateStatus(tbID.Text, "REV", tbCusomerAcct.Text, rcbCurrency.SelectedValue, 0);
                    LoadToolBar(true);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    DataSet ds = TriTT.B_CASHREPAYMENT_LoadCustomerInfo(tbCusomerAcct.Text, rcbCurrency.SelectedValue);
                    if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr1 = ds.Tables[0].Rows[0];
                        tbBalanceAmt.Text = dr1["WorkingAmount"].ToString(); // cap nhat lai so du cua tai khoan trong truong hop cac giao dich khac da AUT rut tien
                    }
                        break;
            }
        }
        protected void FirstLoad()
        {
            LoadCurrency();
            LoadCurrencyDeposited();
            LoadToolBar(true);
            tbTellerID.Text = UserInfo.Username;
        }
        
        #region Properties
        protected void LoadCurrency()
        {
            var Currency = TriTT.B_LoadCurrency("USD","VND");
            rcbCurrency.Items.Clear();
            rcbCurrency.Items.Add(new RadComboBoxItem(""));
            rcbCurrency.AppendDataBoundItems = true;
            rcbCurrency.DataValueField = "Code";
            rcbCurrency.DataTextField = "Code";
            rcbCurrency.DataSource = Currency;
            rcbCurrency.DataBind();
        }
        protected void LoadCurrencyDeposited()
        {
            var Currency = TriTT.B_LoadCurrency("", "");
            rcbCurrencyDeposited.Items.Clear();
            rcbCurrencyDeposited.Items.Add(new RadComboBoxItem(""));
            rcbCurrencyDeposited.AppendDataBoundItems = true;
            rcbCurrencyDeposited.DataValueField = "Code";
            rcbCurrencyDeposited.DataTextField = "Code";
            rcbCurrencyDeposited.DataSource = Currency;
            rcbCurrencyDeposited.DataBind();
        }
        protected void LoadToolBar(bool flag)
        {
            RadToolBar.FindItemByValue("btCommitData").Enabled = flag;
            RadToolBar.FindItemByValue("btPreview").Enabled = flag;
            RadToolBar.FindItemByValue("btAuthorize").Enabled = !flag;
            RadToolBar.FindItemByValue("btReverse").Enabled = !flag;
            RadToolBar.FindItemByValue("btSearch").Enabled = false;
            RadToolBar.FindItemByValue("btPrint").Enabled = false;
        }
        protected void LoadAll_False()
        {
            RadToolBar.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar.FindItemByValue("btPreview").Enabled = false;
            RadToolBar.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar.FindItemByValue("btReverse").Enabled = false;
            RadToolBar.FindItemByValue("btSearch").Enabled = false;
            RadToolBar.FindItemByValue("btPrint").Enabled = false;
        }
        protected void LoadDetail(string ID)
        {
            tbID.Text = ID;
            DataSet ds = TriTT.B_CASHREPAYMENT_LoadDetail(ID);
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                lblCustomerID.Text = dr["CustomerID"].ToString();
                lblCustomerName.Text = dr["CustomerName"].ToString();
                rcbCurrency.SelectedValue = dr["Currency"].ToString();
                tbCusomerAcct.Text = dr["CustomerAccountID"].ToString();
                tbBalanceAmt.Text = dr["BalanceAmount"].ToString();
                tbNewBalanceAmt.Text = dr["NewBalanceAmount"].ToString();
                tbTellerID.Text = dr["TellerID"].ToString();
                rcbCurrencyDeposited.SelectedValue = dr["CurrencyDeposited"].ToString();
                loadCashAccount(rcbCurrencyDeposited.SelectedValue);
                rcbCashAccount.SelectedValue = dr["CashAccountID"].ToString();
                tbAmtLCYDeposited.Text = dr["AmountDeposited"].ToString();
                if (Convert.ToDecimal(dr["DealRate"].ToString()) != 1)
                {
                    tbDealRate.Text = dr["DealRate"].ToString();
                }
                rcbWaiveCharge.SelectedValue = dr["WaiveCharges"].ToString();
                tbNarrative.Text = dr["Narrative"].ToString();
                tbNarrative2.Text = dr["Narrative2"].ToString();
                if (dr["PrintLnNoOfPS"].ToString() != "0")
                {
                    tbPrint.Text = dr["PrintLnNoOfPS"].ToString();
                }
                if (dr["Status"].ToString() == "AUT")
                {
                    LoadAll_False();
                }
            }

        }
        protected void rcbCurrencyDeposited_rcbCurrencyDeposited(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            loadCashAccount(rcbCurrencyDeposited.SelectedValue);
        }

        void loadCashAccount(string CurrencyDeposited)
        {
            DataSet ds = TriTT.B_CASHREPAYMENT_LoadCashAcct(rcbCurrencyDeposited.SelectedValue);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rcbCashAccount.Items.Clear();
                DataRow dr = ds.Tables[0].NewRow();
                dr["Account"] = "";
                dr["AccountHasName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
                rcbCashAccount.DataTextField = "AccountHasName";
                rcbCashAccount.DataValueField = "Account";
                rcbCashAccount.DataSource = ds;
                rcbCashAccount.DataBind();
            }
        }
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }

        protected void Load_CustomerAcct()
        {
            string AccountCustomerID = tbCusomerAcct.Text.Trim();
            string Currency = rcbCurrency.SelectedValue;
            if (Currency != "" || AccountCustomerID != "")
            {
                DataSet ds = TriTT.B_CASHREPAYMENT_LoadCustomerInfo(AccountCustomerID, Currency);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    lblNote.Text = "";
                    lblCustomerID.Text = dr["CustomerID"].ToString();
                    lblCustomerName.Text = dr["CustomerName"].ToString();
                    tbBalanceAmt.Text = dr["WorkingAmount"].ToString();
                }
                else
                {
                    lblNote.Text = "Account Customer ID is not exist !";
                    lblCustomerID.Text = "";
                    lblCustomerName.Text = "";
                    tbBalanceAmt.Text = "";
                }
            }
           
        }
        protected void tbCusomerAcct_TextChanged(object sender, EventArgs e)
        {
            Load_CustomerAcct();
        }
        protected void btSearch_Click(object sender, EventArgs e)
        {
            LoadToolBar(false);
            LoadDetail(tbID.Text);
        }
        //void loadLOANACCOUNT(string customername)
        //{
        //    DataSet ds = BankProject.DataProvider.TriTT.B_BLOANACCOUNT_getbyCurrency(customername, rcbCurrency.SelectedValue);
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        rcbCustAccount.Items.Clear();
        //        DataRow dr = ds.Tables[0].NewRow();
        //        dr["DisplayHasCurrency"] = "";
        //        dr["AccountID"] = "";
        //        dr["CustomerID"] = "";
        //        dr["CustomerName"] = "";  //CustomerName
        //        ds.Tables[0].Rows.InsertAt(dr, 0);
        //        rcbCustAccount.DataTextField = "DisplayHasCurrency";
        //        rcbCustAccount.DataValueField = "AccountID";
        //        rcbCustAccount.DataSource = ds;
        //        rcbCustAccount.DataBind();
        //    }
        //}
        //protected void rcbCustAccount_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    
        //}

        //// gan cac thuoc tinh Name, ID cho cac khach hang khi duoc chon , su kien xay ra khi co du lieu do vao combobox
        //protected void rcbCustAccount_OnItemDataBound(object sender, RadComboBoxItemEventArgs e)
        //{
        //    DataRowView row = e.Item.DataItem as DataRowView;
        //    e.Item.Attributes["CustomerName"] = row["CustomerName"].ToString();  //CustomerName
        //    e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
        //}
        //protected void LoadCustomerAccount()
        //{
        //    rcbCustAccount.Items.Clear();
        //    if (rcbCurrency.SelectedValue != null && rcbCustAccount.SelectedValue != null)
        //    {
        //        DataSet ds = Database.B_BCRFROMACCOUNT_OtherCustomer(rcbCustAccount.SelectedItem.Attributes["Name"].ToString(), rcbCurrency.SelectedValue);
        //        if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            DataRow dr = ds.Tables[0].NewRow();
        //            dr["Display"] = "";
        //            dr["ID"] = "";
        //            dr["CustomerID"] = "";
        //            dr["Name"] = "";
        //            ds.Tables[0].Rows.InsertAt(dr, 0);
        //            rcbCustAccount.DataTextField = "Display";
        //            rcbCustAccount.DataValueField = "ID";
        //            rcbCustAccount.DataSource = ds;
        //            rcbCustAccount.DataBind();
        //        }
        //    }
        //}
        #endregion
    }
}