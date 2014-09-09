using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using BankProject.DataProvider;


namespace BankProject.Views.TellerApplication
{
    public partial class OpenRevolvingCommContract : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        public static string Account_Status = "";
        public static Boolean Enable_toAudit = false; 
        private string Refix_BMACODE()
        {
            return "LD";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            FirstLoad();
            if (Request.QueryString["mode"] != null)
            {
                string mode = Request.QueryString["mode"].ToString();
                switch (mode)
                {
                    case "Load_Full":
                        LoadDataToReview(Request.QueryString["ContID"]);
                        break;
                }
            }
            else
            {
                rdpDDStartDate.SelectedDate= rdpStartDate.SelectedDate = DateTime.Now;
                LoadToolBar(true);
                rcbCustomerID.Focus();
                tbID.Text = TriTT.B_BMACODE_GetNewID_3part_new("B_BMACODE_GetNewID_3part_new", "CRED_REVOLVING_CONTRACT", Refix_BMACODE(), "-");
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                DataSet ds =  TriTT.B_OPEN_COMMITMENT_CONT_Check_Acct_Exist(rcbCategory.SelectedValue, rcbCustomerID.SelectedValue, rcbCurrency.SelectedValue);
                if (ds.Tables != null & ds.Tables[0].Rows.Count > 0 )
                {
                    switch (Enable_toAudit) // kiem tra flag enable, neu = true thi cho phep cap nhat
                    { 
                        case false:
                            ShowMsgBox("This Revolving Commitment Contract was already created by Officer, Please try with another Currency !");
                            break;
                        case true:
                             TriTT.B_OPEN_COMMITMENT_CONT_Insert_Update_Acct(tbID.Text, "UNA", rcbCategory.SelectedValue, rcbCategory.Text.Replace(rcbCategory.SelectedValue + " - ", "")
                    , rcbCustomerID.SelectedValue, rcbCustomerID.SelectedItem.Attributes["GBFullName"], rcbCustomerID.SelectedItem.Attributes["DocType"], rcbCustomerID.SelectedItem.Attributes["DocID"],
                    rcbCustomerID.SelectedItem.Attributes["DocIssuePlace"], DateTime.Parse(rcbCustomerID.SelectedItem.Attributes["DocIssueDate"]),
                    rcbCurrency.SelectedValue, tbCommitmentAmt.Text, rdpStartDate.SelectedDate, rdpEndDate.SelectedDate,
                    tbFeeStart.Text, tbFeeEnd.Text, lblAvailableAmt.Text, tbTrancheAmount.Text, rdpDDStartDate.SelectedDate, rdpDDEndDate.SelectedDate, rcbIntRepayAcct.SelectedValue, rcbIntRepayAcct.Text.Replace(rcbIntRepayAcct.SelectedValue + " - ", "")
                    , rcbSecured.SelectedValue, tbCustRemarks.Text, rcbAccountOfficer.SelectedValue, rcbAccountOfficer.Text, UserInfo.Username.ToString());
                    Enable_toAudit = false; // set flag = false lai
                    Response.Redirect("Default.aspx?tabid=187");
                            break;
                    }
                }
                else
                {
                    TriTT.B_OPEN_COMMITMENT_CONT_Insert_Update_Acct(tbID.Text, "UNA", rcbCategory.SelectedValue, rcbCategory.Text.Replace(rcbCategory.SelectedValue + " - ", "")
                    , rcbCustomerID.SelectedValue, rcbCustomerID.SelectedItem.Attributes["GBFullName"], rcbCustomerID.SelectedItem.Attributes["DocType"], rcbCustomerID.SelectedItem.Attributes["DocID"],
                    rcbCustomerID.SelectedItem.Attributes["DocIssuePlace"], DateTime.Parse(rcbCustomerID.SelectedItem.Attributes["DocIssueDate"]),
                     rcbCurrency.SelectedValue, tbCommitmentAmt.Text, rdpStartDate.SelectedDate, rdpEndDate.SelectedDate,
                    tbFeeStart.Text, tbFeeEnd.Text, lblAvailableAmt.Text, tbTrancheAmount.Text, rdpDDStartDate.SelectedDate, rdpDDEndDate.SelectedDate, rcbIntRepayAcct.SelectedValue, rcbIntRepayAcct.Text.Replace(rcbIntRepayAcct.SelectedValue + " - ", "")
                    , rcbSecured.SelectedValue, tbCustRemarks.Text, rcbAccountOfficer.SelectedValue, rcbAccountOfficer.Text, UserInfo.Username.ToString());
                    Enable_toAudit = false;
                    Response.Redirect("Default.aspx?tabid=187");
                }
            }
            if (commandName == "authorize") 
            {
                TriTT.B_OPEN_COMMITMENT_CONT_Update_Status(tbID.Text, rcbCustomerID.SelectedValue, "AUT");
                Response.Redirect("Default.aspx?tabid=187");
            }
            if (commandName == "reverse")
            {
                TriTT.B_OPEN_COMMITMENT_CONT_Update_Status(tbID.Text, rcbCustomerID.SelectedValue, "REV");
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                tbID.Enabled = rcbCustomerID.Enabled = false;
                LoadToolBar(true);
            }
            if (commandName == "search")
            {
                LoadDataToReview(tbID.Text);
            }
            if (commandName == "edit")
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                RadToolBar1.FindItemByValue("btEdit").Enabled = false;
            }
        }

        protected void AfterProc()
        {
            rcbCustomerID.Focus();
            tbID.Text = SQLData.B_BMACODE_GetNewID("CRED_REVOLVING_CONTRACT", Refix_BMACODE(), "/");
            this.rdpStartDate.SelectedDate = DateTime.Now;
        }
       
        #region Properties
        protected void FirstLoad()
        {
            LoadCategory();
            LoadCustomerID();
            LoadCurrency();
            LoadAccountOfficers();
        }
        private void LoadAccountOfficers()
        {
            this.rcbAccountOfficer.DataSource = BankProject.DataProvider.SQLData.B_BACCOUNTOFFICER_GetAll();
            this.rcbAccountOfficer.DataValueField = "Code";
            this.rcbAccountOfficer.DataTextField = "description";
            this.rcbAccountOfficer.DataBind();
        }
        protected void rcbCustomerID_OnSelectedIndexChanged_forRepayAcct(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadForRcbIntRepayAcct(rcbCustomerID.SelectedValue, rcbCurrency.SelectedValue, "2");
        }
        protected void LoadForRcbIntRepayAcct(string CustomerID, string Currency, string CategoryType)
        {
            rcbIntRepayAcct.Items.Clear();
            DataSet ds  = TriTT.B_OPEN_COMMITMENT_CONT_Load_ALLRepayAcct(CustomerID, Currency, CategoryType);
            if( ds.Tables != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count >0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["AccountCode"] = "";
                dr["AccountHasName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            this.rcbIntRepayAcct.DataSource = ds;
            rcbIntRepayAcct.DataValueField = "AccountCode";
            rcbIntRepayAcct.DataTextField = "AccountHasName";
            rcbIntRepayAcct.DataBind();
        }
        private void LoadToolBar(bool isauthorise)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btReverse").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            RadToolBar1.FindItemByValue("btEdit").Enabled = false;
        }
        protected void LoadToolBar_AllFalse()
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            RadToolBar1.FindItemByValue("btEdit").Enabled = true;
        }
        protected void LoadCategory()
        {
            rcbCategory.DataSource = DataProvider.TriTT.B_OPEN_LOANWORK_ACCT_Get_ALLCategory("B_OPEN_LOANWORK_ACCT_Get_ALLCategory","5");
            rcbCategory.DataValueField="Code";
            rcbCategory.DataTextField = "CodeHasName";
            rcbCategory.DataBind();
        }
        private void LoadCustomerID()
            {
                rcbCustomerID.DataSource = DataProvider.TriTT.B_OPEN_LOANWORK_ACCT_Get_ALLCustomerID();
                rcbCustomerID.DataTextField = "CustomerHasName";
                rcbCustomerID.DataValueField = "CustomerID";
                rcbCustomerID.DataBind();
            }
        protected void rcbCustomerID_OnItemDataBound(object sender, RadComboBoxItemEventArgs e)
            {
                DataRowView Data = e.Item.DataItem as DataRowView;
                e.Item.Attributes["DocType"] = Data["DocType"].ToString();
                e.Item.Attributes["DocID"] = Data["DocID"].ToString();
                e.Item.Attributes["DocIssuePlace"] = Data["DocIssuePlace"].ToString();
                e.Item.Attributes["DocIssueDate"] = Data["DocIssueDate"].ToString();
                e.Item.Attributes["DocExpiryDate"] = Data["DocExpiryDate"].ToString();
                //if (Data["DocExpiryDate"].ToString() != null)
                //{
                //    e.Item.Attributes["DocExpiryDate"] = 
                //}
                e.Item.Attributes["GBFullName"] = Data["GBFullName"].ToString();
            }
        private void LoadCurrency()
            {
                rcbCurrency.DataSource = TriTT.B_LoadCurrency("USD","VND");
                rcbCurrency.DataValueField = "Code";
                rcbCurrency.DataTextField = "Code";
                rcbCurrency.DataBind();
            }
        private void LoadDataToReview(string ID)
        {
           DataSet ds = TriTT.B_OPEN_COMMITMENT_CONT_Load_Acct(ID);
           if (ds.Tables != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
           {
               tbID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
               rcbCategory.SelectedValue = ds.Tables[0].Rows[0]["CategoryCode"].ToString();
               rcbCategory.Text = rcbCategory.SelectedValue + " - " + ds.Tables[0].Rows[0]["CategoryName"].ToString();
               rcbCustomerID.SelectedValue = ds.Tables[0].Rows[0]["CustomerID"].ToString();
               rcbCustomerID.Text = rcbCustomerID.SelectedValue + " - " + ds.Tables[0].Rows[0]["GBFullName"].ToString();
               rcbCurrency.SelectedValue = ds.Tables[0].Rows[0]["CurrencyCode"].ToString();
               tbCommitmentAmt.Text = ds.Tables[0].Rows[0]["CommitmentAmt"].ToString();
               if (ds.Tables[0].Rows[0]["StartDate"].ToString() != "")
               {
                   rdpStartDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"].ToString());
               }
               if (ds.Tables[0].Rows[0]["EndDate"].ToString() != "")
               {
                   rdpEndDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"].ToString());
               }
               tbFeeStart.Text = ds.Tables[0].Rows[0]["CommitmentFeeStart"].ToString();
               tbFeeEnd.Text = ds.Tables[0].Rows[0]["CommitmentFeeEnd"].ToString();
               lblAvailableAmt.Text = ds.Tables[0].Rows[0]["AvailableAmt"].ToString();
               tbTrancheAmount.Text = ds.Tables[0].Rows[0]["TrancheAmt"].ToString();
               if (ds.Tables[0].Rows[0]["DDStartDate"].ToString() != "")
               {
                   rdpDDStartDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DDStartDate"].ToString());
               }
               if (ds.Tables[0].Rows[0]["DDEndDate"].ToString() != "")
               {
                   rdpDDEndDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DDEndDate"].ToString());
               }
               LoadForRcbIntRepayAcct(rcbCustomerID.SelectedValue, rcbCurrency.SelectedValue, "2");
               rcbIntRepayAcct.SelectedValue = ds.Tables[0].Rows[0]["InterestedRepayAcct"].ToString();
               rcbIntRepayAcct.Text = rcbIntRepayAcct.SelectedValue + " - " + ds.Tables[0].Rows[0]["InterestedRepayAcctName"].ToString();
               rcbSecured.SelectedValue = ds.Tables[0].Rows[0]["Secured"].ToString();
               tbCustRemarks.Text = ds.Tables[0].Rows[0]["CustomerRemark"].ToString();
               rcbAccountOfficer.SelectedValue = ds.Tables[0].Rows[0]["AccountOfficerID"].ToString();
               Account_Status = ds.Tables[0].Rows[0]["Status"].ToString();
               Enable_toAudit = true; // flag cho phep audit thong tin , Acct exists trong DB roi
               BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
               LoadToolBar_AllFalse();
           }
        }
        protected void btSearch_Click1(object sender, EventArgs e)
        {
            LoadDataToReview(tbID.Text);
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