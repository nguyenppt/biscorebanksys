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
    public partial class OpenLoanWorkingAccount : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadDataForCommboBox();
            if (Request.QueryString["mode"] != null)
            {
                string mode = Request.QueryString["mode"];
                switch (mode)
                { 
                    case "re_2commit":
                        LoadDataToReview(Request.QueryString["AcctID"]);
                        BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                        LoadToolBar(false);
                        break;
                    case "Load_Full":
                        LoadDataToReview(Request.QueryString["AcctID"]);
                        BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                        LoadToolBar_AllFalse();
                        break;
                }
            }
            else
            {
                LoadToolBar(true);
                this.tbID.Text = "07." + TriTT.B_BCUSTOMER_GetID_Corporate("B_OPEN_LOANWORK_ACCT_Get_RefID", "LOAN_WORKING_ACCT").PadLeft(9, '0') + ".6";
                tbCustomerID.Focus();
            }
            
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        { 
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                DataSet ds = TriTT.B_OPEN_LOANWORK_ACCT_Check_Acct_Exist(tbCustomerID.Text, rcbCurrency.SelectedValue);
                if (ds.Tables != null && ds.Tables[0].Rows.Count == 0) // tai khoan chua ton tai o DB , co the tao new dc
                {
                    if (tbCustomerName.Text == "Customer ID does not exist.")
                    {
                        ShowMsgBox("Customer ID does not exist, Please check again !"); return;
                    }
                    TriTT_Credit.B_OPEN_LOANWORK_ACCT_Insert_Update_Acct(tbID.Text, tbCustomerID.Text, "UNA", tbCustomerName.Text, tbDocType.Text,
                        tbDocID.Text, tbDocIssuePlace.Text, rdpIssueDate.SelectedDate.HasValue ? Convert.ToString(rdpIssueDate.SelectedDate): "",
                         "" , rcbCategory.SelectedValue, rcbCategory.Text.Replace(rcbCategory.SelectedValue + " - ", ""), txtAccountName.Text, tbShortTitle.Text, tbMnemonic.Text,
                         rcbCurrency.SelectedValue, rcbCurrency.SelectedValue, rcbProductLine.SelectedValue, rcbProductLine.Text.Replace(rcbProductLine.SelectedValue + " - ", ""),
                         tbAlternateAcct.Text, UserInfo.Username.ToString());
                    Response.Redirect("Default.aspx?tabid=184");
                    tbCustomerID.Focus();
                }
                else
                {
                    ShowMsgBox("This Loan Working Account was already created by Officer, Please try with another Currency !");
                    return;
                }
            }
            if (commandName == "Preview")
            {
                Response.Redirect(EditUrl("OpenLoanWorkingAccount_PL"));
            }
            if (commandName == "authozize") 
            {
                TriTT.B_OPEN_LOANWORK_ACCT_Update_Status(tbID.Text , tbCustomerID.Text,"AUT");
                Response.Redirect("Default.aspx?tabid=184");
                tbCustomerID.Focus();
            }
            if (commandName == "reverse")
            {
                TriTT.B_OPEN_LOANWORK_ACCT_Update_Status(tbID.Text, tbCustomerID.Text, "REV");
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                LoadToolBar(true);
                tbID.Enabled= tbCustomerID.Enabled = false; //khong cho hieu chinh thong tin ID 
            }
            if (commandName == "search")
            {
                LoadDataToReview(tbID.Text);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                LoadToolBar_AllFalse();
            }
            if (commandName == "edit")
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                RadToolBar1.FindItemByValue("btEdit").Enabled = false;
            }
        }
        #region Method
        private void LoadDataForCommboBox()
        {
            LoadCurrency();
            LoadCategory();
            LoadProductLine();
        }
        private void LoadDataToReview(string ID)
        { 
            DataSet ds = TriTT.B_OPEN_LOANWORK_ACCT_Load_Account(ID);
            if(ds.Tables != null && ds.Tables.Count >0 && ds.Tables[0].Rows.Count >0)
            {
                tbID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                tbCustomerID.Text = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                //tbCustomerName.Text = ds.Tables[0].Rows[0]["CustomerID"].ToString() + " - " + ds.Tables[0].Rows[0]["GBFullName"].ToString();
                LoadCustomerName(tbCustomerID.Text);
                rcbCategory.SelectedValue = ds.Tables[0].Rows[0]["CategoryCode"].ToString();
                rcbCategory.Text = ds.Tables[0].Rows[0]["CategoryCode"].ToString() + " - " + ds.Tables[0].Rows[0]["Categoryname"].ToString();
                rcbCurrency.SelectedValue = ds.Tables[0].Rows[0]["CurrencyCode"].ToString();
                txtAccountName.Text = ds.Tables[0].Rows[0]["AccountName"].ToString();
                tbShortTitle.Text = ds.Tables[0].Rows[0]["ShortTittle"].ToString();
                tbMnemonic.Text = ds.Tables[0].Rows[0]["Mnemonic"].ToString();
                rcbProductLine.SelectedValue = ds.Tables[0].Rows[0]["ProductLineCode"].ToString();
                rcbProductLine.Text = ds.Tables[0].Rows[0]["ProductLineCode"].ToString() +" - " + ds.Tables[0].Rows[0]["ProductLineDescription"].ToString();
                tbAlternateAcct.Text = ds.Tables[0].Rows[0]["AlternateAccount"].ToString();
                bientoancuc.StatusAccount_from_Search_action= ds.Tables[0].Rows[0]["Status"].ToString();
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                LoadToolBar_AllFalse();
            }
        }
        #endregion

        #region Proverty
        protected class bientoancuc
        {
            public static string StatusAccount_from_Search_action;
        }

        private void LoadToolBar(bool enable )
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = enable;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = !enable;
            RadToolBar1.FindItemByValue("btReverse").Enabled = !enable;
            RadToolBar1.FindItemByValue("btSearch").Enabled = true;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            RadToolBar1.FindItemByValue("btEdit").Enabled = false;

        }
        private void LoadToolBar_AllFalse()
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            RadToolBar1.FindItemByValue("btEdit").Enabled = true;
        }
        private void LoadCurrency()
        {
            rcbCurrency.DataSource = TriTT.B_LoadCurrency("USD","VND");
            rcbCurrency.DataValueField = "Code";
            rcbCurrency.DataTextField = "Code";
            rcbCurrency.DataBind();
        }
        protected void LoadCustomerName(string CustomerID)
        {
            DataSet ds = TriTT_Credit.Load_Customer_Info_From_BCUSTOMER_INFO(CustomerID);
            tbCustomerName.Text = "";
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                tbCustomerName.Text = dr["GBFullName"].ToString();
                tbDocType.Text = dr["DocType"].ToString();
                tbDocID.Text = dr["DocID"].ToString();
                tbDocIssuePlace.Text = dr["DocIssuePlace"].ToString();
                if (dr["DocIssueDate"].ToString() !="") rdpIssueDate.SelectedDate = Convert.ToDateTime(dr["DocIssueDate"].ToString());
            }
            else
            {
                tbCustomerName.Text = "Customer ID does not exist.";
                tbDocType.Text = "";
                tbDocID.Text = "";
                tbDocIssuePlace.Text = "";
                rdpIssueDate.SelectedDate = null;
            }
            Set_Account_name();
        }
        protected void tbCustomerID_ontextChanged(object sender, EventArgs e)
        {
            LoadCustomerName(tbCustomerID.Text);
        }
        protected void rcbCurrency_OnValueChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Set_Account_name();
        }
        protected void Set_Account_name()
        {
            if (tbCustomerID.Text != "" && tbCustomerName.Text != "Customer ID does not exist." && rcbCurrency.SelectedValue != "")
            {
                txtAccountName.Text = "TKTV-" + tbCustomerName.Text + "-" + rcbCurrency.SelectedValue;
                tbShortTitle.Text = "TKTV-" + tbCustomerName.Text + "-" + rcbCurrency.SelectedValue;
            }
        }
        private void LoadCategory()
        {
            rcbCategory.DataSource = DataProvider.TriTT.B_OPEN_LOANWORK_ACCT_Get_ALLCategory("B_OPEN_LOANWORK_ACCT_Get_ALLCategory","4");
            rcbCategory.DataValueField="Code";
            rcbCategory.DataTextField = "CodeHasName";
            rcbCategory.DataBind();
        }
        private void LoadProductLine()
        {
            rcbProductLine.DataSource = DataProvider.TriTT.B_OPEN_LOANWORK_ACCT_Get_ALLCategory("B_OPEN_LOANWORK_ACCT_Get_ALLProductLine", "4");
            rcbProductLine.DataValueField = "ProductID";
            rcbProductLine.DataTextField = "ProductHasName";
            rcbProductLine.DataBind();
        }
        
        #endregion

        protected void btSearch_Click1(object sender, EventArgs e)
        {
            LoadDataToReview(tbID.Text);
            LoadToolBar_AllFalse();
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
       
 