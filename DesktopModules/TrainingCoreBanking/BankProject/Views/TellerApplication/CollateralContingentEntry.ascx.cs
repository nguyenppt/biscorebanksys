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
    public partial class CollateralContingentEntry : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        public static int AutoID = 1;
        public string pageid = "392";
        private string Refix_BMACODE()
        {
            return "DC";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["tabid"] != null)
            {
                pageid = Request.QueryString["tabid"].ToString();
                
            }
            
            if (IsPostBack) return;

            

            if (Request.QueryString["ID"] != null)
            {
                Load_Contingent_Account(Request.QueryString["ID"].ToString());
                LoadToolBar(false);
            }
            else
            {
                LoadToolBar(true);
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolbarbutton = e.Item as RadToolBarButton;
            var commandname = toolbarbutton.CommandName;
            if (commandname == "commit")
            {
                var Rate = (tbDealRate.Text == "" ? 0 : tbDealRate.Value.Value);
                if (Convert.ToDecimal(tbAmount.Text.Replace(",", "")) <= 0)
                {
                    ShowMsgBox("Amount Value must be greater than 0 . Please check again !"); return;
                }
                TriTT_Credit.B_CONTINGENT_ENTRY_Insert_Update(tbID.Text, tbContingentEntryID.Text, tbCustomerIDName_Cont.Text.Substring(0, 7), tbAddress_cont.Text, tbIDTaxCode.Text
                        , tbDateOfIssue.Text == "" ? "" : tbDateOfIssue.Text, rcbTransactionCode.SelectedValue, rcbTransactionCode.Text.Replace(rcbTransactionCode.SelectedValue + " - ", "")
                        , rcbDebitOrCredit.SelectedValue, rcbDebitOrCredit.Text.Replace(rcbDebitOrCredit.SelectedValue + " - ", ""), rcbCurrency.SelectedValue,
                        rcbAccountNo.SelectedValue, rcbAccountNo.Text,tbAmount.Text ==""? 0: Convert.ToDecimal(tbAmount.Value),Convert.ToDecimal( Rate), rdpValuedate_cont.SelectedDate, tbNarrative.Text
                        , UserInfo.Username.ToString(), tbCollateralType.Text);
                Response.Redirect("Default.aspx?tabid=" + pageid);
            }
            if (commandname == "search")
            {
                Load_Contingent_Account(tbID.Text.Trim());
            }
            if (commandname == "edit")
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                rcbCurrency.Enabled = true;
                rcbDebitOrCredit.Enabled =  false;
                LoadToolBar(true);
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            Load_Contingent_Account(tbID.Text.Trim());
        }
        protected void Load_Contingent_Account(string ContingentID)
        {
            string CustomerID = ContingentID.Substring(0, 7);
            LoadCurrencies(CustomerID);
            //LoadGlobalLimitID(CustomerID); //Load dua vao CUstomerID , trong table [BCUSTOMER_LIMIT_SUB]
            if (ContingentID.Length == 10 && ContingentID.Substring(7, 1) == ".")// check lenght, hop le thi di tiep
            {
                if (TriTT.B_CUSTOMER_LIMIT_LoadCustomerName(ContingentID.Substring(0, 7)) == null)
                {
                    //tbCollInfoID.Text = CollIndoID;
                    lblCheckCustomer.Text = "";
                    lblCheckCustomer.Text = "Customer ID does not exists !"; return;
                }
                DataSet ds = TriTT_Credit.Load_Contingent_Account(ContingentID);
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null && ds.Tables.Count>0)// neu Collateral Info exist thi` load len, neu khong thi chekc de tao moi
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    tbID.Text = ContingentID;
                    //Load thong tin cho tab Contingent Entry Info//
                    tbContingentEntryID.Text = dr["ContingentEntryID"].ToString();
                    tbCustomerIDName_Cont.Text = dr["CustomerID"].ToString();
                    tbAddress_cont.Text = dr["Address_cont"].ToString();
                    tbIDTaxCode.Text = dr["DocID"].ToString();
                    if (dr["DocIssueDate"].ToString() != "")
                    {
                        tbDateOfIssue.Text = (Convert.ToDateTime(dr["DocIssueDate"].ToString())).ToShortDateString();
                    }
                    tbReferenceNo.Text = ContingentID;
                    rcbTransactionCode.SelectedValue = dr["TransactionCode"].ToString();
                    rcbDebitOrCredit.SelectedValue = dr["DCTypeCode"].ToString();
                    rcbCurrency.SelectedValue = dr["Currency"].ToString();
                    tbCollateralType.Text = dr["CollateralType_Code"].ToString();
                    LoadContingetnAcct(tbCollateralType.Text, rcbCurrency.SelectedValue);

                    tbAmount.Text = dr["Amount"].ToString();
                    if (dr["DealRate"].ToString() == "0.000000")
                    {
                        tbDealRate.Text = "";
                    }
                    else tbDealRate.Text = dr["DealRate"].ToString();
                    if (dr["ValueDateCont"].ToString() != "")
                    {
                        rdpValuedate_cont.DbSelectedDate = Convert.ToDateTime(dr["ValueDateCont"].ToString());
                    }
                    tbNarrative.Text = dr["Narrative"].ToString();
                    //////////////////////
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    LoadToolBar(false);
                    return;
                }
                // load thong tin can thiet de tao form moi cho contingeent Entry
                DataSet ds1 = TriTT_Credit.B_COLLATERAL_INFO_LoadExistColl_InfoExists_2(ContingentID);
                int countRow = ds1.Tables[0].Rows.Count;
                if (countRow > 0) 
                {
                    DataRow dr = ds1.Tables[0].Rows[0];
                    tbContingentEntryID.Text = TriTT.B_BMACODE_GetNewID_3part_new("B_BMACODE_CONTINGENT_ENTRY_ID", "COLL_CONTIN_ENTRY", "DC", ".");

                    tbCustomerIDName_Cont.Text = dr["CustomerID"].ToString();
                    lblCheckCustomer.Text = ""; // xoa trang thai not exist neu ton tai
                    tbCustomerIDName_Cont.Enabled = false;
                    tbCustomerIDName_Cont.Text = CustomerID;
                    tbAddress_cont.Text = dr["Address_cont"].ToString();
                    tbIDTaxCode.Text = dr["DocID"].ToString();
                    if (dr["DocIssueDate"].ToString() != "")
                    {
                        tbDateOfIssue.Text = (Convert.ToDateTime(dr["DocIssueDate"].ToString())).ToShortDateString();
                    }
                    LoadCurrencies(ContingentID.Substring(0, 7));
                    tbCollateralType.Text = dr["CollateralTypeCode"].ToString();
                    LoadContingetnAcct(tbCollateralType.Text, rcbCurrency.SelectedValue);
                    tbReferenceNo.Text = ContingentID;
                }
                else
                { ShowMsgBox("Your Collateral Information ID has not been Created, You'd create it first !"); return; }
            }
            else { ShowMsgBox("Contingent ID is Incorrect Format. Please check again ! "); return; }
        }
        protected void First_Load()
        {
            
        }
        protected void LoadCurrencies(string CustomerID)
        {
            DataSet ds = TriTT.B_COLLATERAL_INFO_LoadCurrency_forEach_Customer(CustomerID);
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["CurrencyCode"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbCurrency.Items.Clear();
            rcbCurrency.DataSource = ds;
            rcbCurrency.DataValueField = "CurrencyCode";
            rcbCurrency.DataTextField = "CurrencyCode";
            rcbCurrency.DataBind();
        }
        protected void rcbCurrency_OnClientSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadContingetnAcct(tbCollateralType.Text, rcbCurrency.SelectedValue);
        }
        protected void LoadContingetnAcct(string CollateralTypeCode, string Currency)
        {
            DataSet ds = TriTT.LoaContAcctFromDB(CollateralTypeCode, Currency);
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["ContingentAcctID"] = "";
                dr["AccountHasName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }

            rcbAccountNo.Items.Clear();
            rcbAccountNo.DataSource = ds;
            rcbAccountNo.DataValueField = "ContingentAcctID";
            rcbAccountNo.DataTextField = "AccountHasName";
            rcbAccountNo.DataBind();
            rcbAccountNo.SelectedIndex = 1;
        }
        private void LoadToolBar(bool isauthorize)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            RadToolBar1.FindItemByValue("btEdit").Enabled = !isauthorize;
        }
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void rcbTransactionCode_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (rcbTransactionCode.SelectedIndex)
            {
                case 2:
                    rcbDebitOrCredit.SelectedIndex = 2;
                    break;
                case 1:
                    rcbDebitOrCredit.SelectedIndex = 1;
                    break;
                default:
                    rcbDebitOrCredit.SelectedValue = "";
                    break;
            }

        }
        
        
    }
}