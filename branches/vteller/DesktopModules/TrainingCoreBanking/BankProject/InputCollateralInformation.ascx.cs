using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BankProject.DataProvider;

namespace BankProject
{
    public partial class InputCollateralInformation : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            FirstLoad();
            rcbCollateralType.Focus();
            tbExeValue.Value = tbNominalValue.Value * 0.7;
            if (Request.QueryString["CollInfoID"] != null)
            {
                LoadData_forCollINFO(Request.QueryString["CollInfoID"].ToString());
            }
            else
            {
                LoadToolBar(true);
            }
        }

        protected void FirstLoad()
        {
            LoadCountries();
            LoadCollateralStatus();
            //LoadCurrencies();
            LoadCompanyStorage();
            LoadCollateralType();
            rdpValueDate.SelectedDate = DateTime.Now;
            rcbCountry.SelectedValue = "VN";
            tbContingentEntryID.Text = TriTT.B_BMACODE_GetNewID_3part_new("B_BMACODE_CONTINGENT_ENTRY_ID", "COLL_CONTIN_ENTRY", "DC", ".");
        }
      
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolbarbutton = e.Item as RadToolBarButton;
            var commandname = toolbarbutton.CommandName;
            if (commandname == "commit")
            {
                var CollInfoID = tbCollInfoID.Text.Trim();
                if (CollInfoID.Length == 10 && CollInfoID.Substring(7, 1) == ".")
                {
                    string RightID = CollInfoID.Substring(0,10); 
                    var ProvisionValue = decimal.Parse(lblProvisionValue.Text  == "" ? "0" : lblProvisionValue.Text);
                    var AllocatedAmt = decimal.Parse(lblAllocatedAmt.Text == "" ? "0" : lblAllocatedAmt.Text);
                    TriTT.B_COLLATERAL_INFO_Insert_Update(RightID, CollInfoID, rcbCollateralType.SelectedValue, rcbCollateralType.Text.Replace(rcbCollateralType.SelectedValue + " - ", "")
                        , rcbCollateralCode.SelectedValue, rcbCollateralCode.Text.Replace(rcbCollateralCode.SelectedValue+" - ", ""),rcbContingentAcct.SelectedValue
                        ,rcbContingentAcct.SelectedItem.Text.Replace(rcbContingentAcct.SelectedValue+" - ",""),tbDescription.Text
                        , tbAddress.Text, rcbCollateralStatus.SelectedValue, rcbCollateralStatus.Text.Replace(rcbCollateralStatus.SelectedValue+" - ", ""),
                        tbCustomerIDName.Text.Trim().Substring(0, 7), tbCustomerIDName.Text, tbNotes.Text, rcbCompanyStorage.SelectedValue, rcbCompanyStorage.SelectedItem.Text.Replace(rcbCompanyStorage.SelectedValue+" - ", "")
                        , rcbCurrency.SelectedValue, rcbCountry.SelectedValue, rcbCountry.SelectedItem.Text.Replace(rcbCountry.SelectedValue+" - ",""),
                        Convert.ToDecimal(tbNominalValue.Value.HasValue? tbNominalValue.Value : 0),Convert.ToDecimal( tbMaxValue.Value.HasValue? tbMaxValue.Value : 0),ProvisionValue ,
                        Convert.ToDecimal( tbExeValue.Value.HasValue? tbExeValue.Value:0),
                        AllocatedAmt, rdpValueDate.SelectedDate, rdpExpiryDate.SelectedDate, rdpReviewDate.SelectedDate, UserInfo.Username.ToString());
                    TriTT.B_CONTINGENT_ENTRY_Insert_Update(CollInfoID, tbContingentEntryID.Text, tbCustomerIDName_Cont.Text.Substring(0, 7), tbAddress_cont.Text, tbIDTaxCode.Text
                        , tbDateOfIssue.Text == "" ? "" : tbDateOfIssue.Text, rcbTransactionCode.SelectedValue, rcbTransactionCode.Text.Replace(rcbTransactionCode.SelectedValue + " - ", "")
                        , rcbDebitOrCredit.SelectedValue, rcbDebitOrCredit.Text.Replace(rcbDebitOrCredit.SelectedValue + " - ", ""), rcbCurrency.SelectedValue,
                        rcbAccountNo.SelectedValue, rcbAccountNo.Text, Convert.ToDecimal(tbAmount.Value), Convert.ToDecimal(tbDealRate.Value), rdpValueDate.SelectedDate, tbNarrative.Text
                        , UserInfo.Username.ToString());
                    Response.Redirect("Default.aspx?tabid=194");
                }
                else
                {
                    ShowMsgBox("Collateral Information ID is incorrect format, check it again !"); return;
                }
            }
            if (commandname == "search")
            {
                LoadData_forCollINFO(tbCollInfoID.Text.Trim());
            }
            if (commandname == "edit")
            { 
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                rcbCollateralType.Enabled = false;
                rcbCollateralCode.Enabled = false;
                tbCustomerIDName.Enabled = false;
                rcbCurrency.Enabled = true;
                rcbFreignCcy.Enabled = true;
                rcbDebitOrCredit.Enabled= tbCollInfoID.Enabled = false;
                LoadToolBar(true);
            }
        }
        protected void btSearch_Click1(object sender, EventArgs e)
        {
            LoadData_forCollINFO(tbCollInfoID.Text.Trim());
        }
        protected void LoadData_forCollINFO(string CollIndoID)
        {
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);// f5 form de search tiep data
            FirstLoad();
            LoadCurrencies(CollIndoID.Substring(0, 7));
            if (CollIndoID.Length == 10 && CollIndoID.Substring(7,1)==".")// check lenght, hop le thi di tiep
            {
                if (TriTT.B_CUSTOMER_LIMIT_LoadCustomerName(CollIndoID.Substring(0, 7)) == null)
                {
                    tbCollInfoID.Text = CollIndoID;
                    lblCheckCustomer.Text = "Customer ID does not exists !"; return;
                }

                if (TriTT.B_COLLATERAL_INFO_LoadExistColl_InfoExists(CollIndoID).Tables[0].Rows.Count > 0)// neu Collateral Info exist thi` load len, neu khong thi chekc de tao moi
                {
                    DataRow dr = TriTT.B_COLLATERAL_INFO_LoadExistColl_InfoExists(CollIndoID).Tables[0].Rows[0];
                    tbCollInfoID.Text = CollIndoID;
                    rcbCollateralType.SelectedValue = dr["CollateralTypeCode"].ToString();
                    LoadCollateralCode(rcbCollateralType.SelectedValue);
                    rcbCollateralCode.SelectedValue = dr["CollateralCode"].ToString();
                    tbCustomerIDName.Text = dr["CustomreIDName"].ToString();
                    rcbCurrency.SelectedValue = dr["Currency"].ToString();
                    LoadContingetnAcct(rcbCollateralType.SelectedValue, rcbCurrency.SelectedValue);
                   
                    //rcbContingentAcct.SelectedValue = dr["ContingentAcctID"].ToString();
                    tbDescription.Text = dr["Description"].ToString();
                    tbAddress.Text = dr["Address"].ToString();
                    rcbCollateralStatus.SelectedValue = dr["CollateralStatusID"].ToString();
                    tbCustomerIDName.Text = dr["CustomreIDName"].ToString();
                    tbNotes.Text = dr["Note"].ToString();
                    rcbCompanyStorage.SelectedValue = dr["CompanyStorageID"].ToString();
                    rcbCurrency.SelectedValue = dr["Currency"].ToString();
                    rcbCountry.SelectedValue = dr["CountryCode"].ToString();
                    rcbCountry.SelectedValue = dr["CountryCode"].ToString();
                    tbNominalValue.Text = (dr["NominalValue"].ToString());
                    tbMaxValue.Text = (dr["MaxValue"].ToString());
                    if (dr["ProvisionValue"].ToString() == "0.00")
                    { lblProvisionValue.Text = ""; }
                    tbExeValue.Text = (dr["ExecutionValue"].ToString());
                    if(dr["AllocatedAmt"].ToString() =="0.00")
                    {lblAllocatedAmt.Text ="" ;}
                    if (dr["ValueDate"].ToString() != "")
                    {
                        rdpValueDate.DbSelectedDate = Convert.ToDateTime(dr["ValueDate"].ToString());
                    }
                    if (dr["ExpiryDate"].ToString() != "")
                    {
                        rdpExpiryDate.DbSelectedDate = Convert.ToDateTime(dr["ExpiryDate"].ToString());
                    }
                    if (dr["ReviewDateFreq"].ToString() != "")
                    {
                        rdpReviewDate.DbSelectedDate = Convert.ToDateTime(dr["ReviewDateFreq"].ToString());
                    }
                    //Load thong tin cho tab Contingent Entry Info//
                    tbContingentEntryID.Text = dr["ContingentEntryID"].ToString();
                    tbCustomerIDName_Cont.Text = tbCustomerIDName.Text;
                    tbAddress_cont.Text = dr["Address_cont"].ToString();
                    tbIDTaxCode.Text = dr["DocID"].ToString();
                    if (dr["DocIssueDate"].ToString() != "")
                    {
                        tbDateOfIssue.Text = (Convert.ToDateTime(dr["DocIssueDate"].ToString())).ToShortDateString();
                    }
                    tbReferenceNo.Text = CollIndoID;
                    rcbTransactionCode.SelectedValue = dr["TransactionCode"].ToString();
                    rcbDebitOrCredit.SelectedValue = dr["DCTypeCode"].ToString();
                    rcbFreignCcy.SelectedValue = dr["Currency"].ToString();
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
                var CustomerID = CollIndoID.Substring(0, 7);
                DataSet ds = TriTT.B_COLLATERAL_INFO_LoadCustomer_Info(CustomerID);
                int countRow = ds.Tables[0].Rows.Count;
                if (countRow  > 0) // neu CustomerID exist va Authorized trong BCustomer_INFO thi load len
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    tbCollInfoID.Text = CollIndoID;
                    tbCustomerIDName.Text = dr["CustomerID_Name"].ToString();
                    lblCheckCustomer.Text = ""; // xoa trang thai not exist neu ton tai
                    tbCustomerIDName.Enabled = tbCustomerIDName_Cont.Enabled = false;
                    tbCustomerIDName_Cont.Text = CustomerID;
                    tbAddress_cont.Text = dr["Address"].ToString();
                    tbIDTaxCode.Text = dr["DocID"].ToString();
                    if (dr["DocIssueDate"].ToString() != "")
                    {
                        tbDateOfIssue.Text = (Convert.ToDateTime(dr["DocIssueDate"].ToString())).ToShortDateString();
                    }
                    tbReferenceNo.Text = tbCollInfoID.Text;
                }
                else
                { ShowMsgBox("Your Customer ID has not been Created, You'd create it first !"); return; }
            }
            else { ShowMsgBox("Collateral Information ID is Incorrect Format. Please check again ! "); return; }
        }
        #region Properties
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

        protected void LoadCountries()
        {
            rcbCountry.Items.Clear();
            rcbCountry.DataSource = DataProvider.TriTT.B_BCOUNTRY_GetAll();
            rcbCountry.DataTextField = "TenTA";
            rcbCountry.DataValueField = "MaQuocGia";
            rcbCountry.DataBind();
        }
        protected void LoadCollateralStatus()
        {
            rcbCollateralStatus.Items.Clear();
            rcbCollateralStatus.DataSource = TriTT.B_COLLATERAL_INFO_GetCollateralStatus();
            rcbCollateralStatus.DataTextField = "Description";
            rcbCollateralStatus.DataValueField = "StatusID";
            rcbCollateralStatus.DataBind();
        }
        protected void LoadCompanyStorage()
        {
            rcbCompanyStorage.Items.Clear();
            DataSet ds = TriTT.B_COLLATERAL_INFO_GetBankBranch();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["BranchID"] = "";
                dr["Description"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbCompanyStorage.DataSource = ds;
            rcbCompanyStorage.DataValueField = "BranchID";
            rcbCompanyStorage.DataTextField = "Description";
            rcbCompanyStorage.DataBind();
        }
        protected void LoadCollateralType()
        {
            rcbCollateralType.Items.Clear();
            DataSet ds = TriTT.B_CUSTOMER_LIMIT_Load_CollateralType();
            if (ds.Tables != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["CollateralTypeCode"] = "";
                dr["CollateralTypeHasName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbCollateralType.DataSource = ds;
            rcbCollateralType.DataValueField = "CollateralTypeCode";
            rcbCollateralType.DataTextField = "CollateralTypeHasName";
            rcbCollateralType.DataBind();
        }
        protected void LoadCollateralCode(string CollateralTypeCode)
        {
            rcbCollateralCode.Items.Clear();
            rcbCollateralCode.Text = "";
            DataSet ds = TriTT.B_CUSTOMER_LIMIT_Load_CollateralCode(CollateralTypeCode);
            if (ds.Tables != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["CollateralCode"] = "";
                dr["CollateralHasName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbCollateralCode.DataSource = ds;
            rcbCollateralCode.DataValueField = "CollateralCode";
            rcbCollateralCode.DataTextField = "CollateralHasName";
            rcbCollateralCode.DataBind();
        }
        protected void rcbCollateralType_OnselectedIndexchanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadCollateralCode(rcbCollateralType.SelectedValue);
            LoadContingetnAcct(rcbCollateralType.SelectedValue, rcbCurrency.SelectedValue);
        }
        protected void LoadCurrencies(string CustomerID)
        {
            rcbCurrency.Items.Clear();
            DataSet ds = TriTT.B_COLLATERAL_INFO_LoadCurrency_forEach_Customer(CustomerID);
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["CurrencyCode"] = "";
                //dr["CurrencyCode"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbCurrency.DataSource = ds;
            rcbCurrency.DataValueField = "CurrencyCode";
            rcbCurrency.DataTextField = "CurrencyCode";
            rcbCurrency.DataBind();

            rcbFreignCcy.Items.Clear();
            rcbFreignCcy.DataSource = ds;
            rcbFreignCcy.DataValueField = "CurrencyCode";
            rcbFreignCcy.DataTextField = "CurrencyCode";
            rcbFreignCcy.DataBind();
        }
        protected void rcbCurrency_OnClientSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadContingetnAcct(rcbCollateralType.SelectedValue, rcbCurrency.SelectedValue);
        }
        protected void LoadContingetnAcct(string CollateralTypeCode, string Currency)
        {
            rcbContingentAcct.Items.Clear();
            rcbContingentAcct.Text = rcbAccountNo.Text = "";
            DataSet ds = TriTT.LoaContAcctFromDB(CollateralTypeCode, Currency);
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["ContingentAcctID"] = "";
                dr["AccountHasName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbContingentAcct.DataSource = ds;
            rcbContingentAcct.DataValueField = "ContingentAcctID";
            rcbContingentAcct.DataTextField = "AccountHasName";
            rcbContingentAcct.DataBind();

            rcbAccountNo.Items.Clear();
            rcbAccountNo.DataSource = ds;
            rcbAccountNo.DataValueField = "ContingentAcctID";
            rcbAccountNo.DataTextField = "AccountHasName";
            rcbAccountNo.DataBind();
            rcbContingentAcct.SelectedIndex = 1;
            rcbAccountNo.SelectedIndex = 1;
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
           switch(rcbTransactionCode.SelectedIndex )
           {
               case 2:
                   rcbDebitOrCredit.SelectedIndex = 2;
                   break;
               case 1:
                   rcbDebitOrCredit.SelectedIndex = 1;
                   break;
               default:
                   rcbDebitOrCredit.SelectedValue= "";
                   break;
           }
                
        }
        #endregion
        
        
    }
}