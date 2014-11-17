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
    public partial class ChequeWithrawalDrawOnUs : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected string refix_MACODE()
        {
            return "TT";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            FirstLoad();
            if (Request.QueryString["ID"] != null)
            {
                LoadChequeID(Request.QueryString["ID"].ToString());
            }
           
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        { 
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                if (TriTT.B_CHEQUE_RETURN_check_cheque_in_used(rcbChequeType.SelectedValue, Convert.ToDouble(tbChequeNo.Value.Value)).Tables[0].Rows[0]["InUsed"].ToString() == "NO") // KIEM TRa xem chequeNo co duoc su dung chua
                {
                    if (TriTT.B_CHEQUE_RETURN_check_cheque_in_Returned(rcbChequeType.SelectedValue ,Convert.ToDouble(tbChequeNo.Value.Value)).Tables[0].Rows.Count == 0)// check xem chequeNo co bi Returned chua ?
                    {
                        decimal dealrate = Convert.ToDecimal(tbDealRate.Value.HasValue? tbDealRate.Value.Value : 1);
                        if (rcbCurrency.SelectedValue == rcbCurrencyPaid.SelectedValue) { dealrate = 1; }
                        TriTT.CHEQUE_WITHDRAWAL_Insert_Update(tbID.Text, tbCustomerID.Text, tbCustomerName.Text, rcbCurrency.SelectedValue, tbAccountCustomer.Text,
                            "", Convert.ToDecimal(tbAmountLocal.Value.HasValue ? tbAmountLocal.Value : 0), Convert.ToDecimal(tbOldCustBal.Text != "" ? tbOldCustBal.Text : "0"),
                            Convert.ToDecimal(tbNewCustBal.Text != "" ? tbNewCustBal.Text : "0"), rcbChequeType.SelectedValue, rcbChequeType.Text.Replace(rcbChequeType.SelectedValue + " - ", "")
                            , Convert.ToDecimal(tbChequeNo.Value), tbTellerID.Text, rcbCurrencyPaid.SelectedValue, rcbAccountPaid.SelectedValue, rcbAccountPaid.Text, dealrate
                            , Convert.ToDecimal(tbAmtPaidToCust.Text != "" ? tbAmtPaidToCust.Text : "0"), rcbWaiveCharges.SelectedValue, tbNarrative.Text, tbBeneName.Text
                            , tbAddress.Text, tbLegalID.Text, rdpIssDate.SelectedDate, tbPlaceOfIssue.Text, "UNA");
                        Response.Redirect("Default.aspx?tabid=132");
                    }
                    else { ShowMsgBox("ChequeNo was Returned back to the Bank, please choose another ChequeNo !"); return; }
                }
                else { ShowMsgBox("ChequeNo was used, please choose another ChequeNo !"); return; }
            }
            if (commandName == "Preview")
            {
                Response.Redirect(EditUrl("ChequeWithrawalDrawOnUs_PL"));
            }
            if (commandName == "authorize") 
            {
                TriTT.CHEQUE_WITHDRAWAL_Update_Status(tbID.Text, "AUT", tbAccountCustomer.Text, rcbCurrency.SelectedValue, tbNewCustBal.Value.Value);
                Response.Redirect("Default.aspx?tabid=132");
            }
            if (commandName == "reverse")
            {
                TriTT.CHEQUE_WITHDRAWAL_Update_Status(tbID.Text, "REV", tbAccountCustomer.Text, rcbCurrency.SelectedValue, tbNewCustBal.Value.Value);
                LoadToolBar(true);
                LoadCustomerInfo();
            }
            if (commandName == "print")
            {
                Print_Deal_Slip();
            }
        }
        protected void FirstLoad()
        {
            tbID.Text = DataProvider.TriTT.B_BMACODE_GetNewID_3par("CHEQUE_WITHRAWAL", refix_MACODE(), ".");
            tbTellerID.Text = UserInfo.Username;
            LoadToolBar(true);
            LoadCurrency();
            LoadChequeType();
            LoadCurrencyPaid();
        }
        protected void LoadChequeID(string ID)
        {
            LoadCurrency();
            LoadChequeType();
            LoadCurrencyPaid();
            DataRow dr = TriTT.CHEQUE_WITHDRAWAL_LoadChequeID(ID).Tables[0].Rows[0];
            tbID.Text = dr["ID"].ToString();
            rcbCurrency.SelectedValue = dr["Currency"].ToString();
            tbCustomerID.Text = dr["CustomerID"].ToString();
            tbCustomerName.Text = dr["CustomerName"].ToString();
            //rcbCurrency_OnSelectedIndexChanged(rcbCurrency, null);
            tbAccountCustomer.Text = dr["AccountCode"].ToString();
            tbAmountLocal.Text = dr["AmountLCY"].ToString();
            tbOldCustBal.Text = dr["OldBalance"].ToString();
            tbNewCustBal.Text = dr["NewBalance"].ToString();
            rcbChequeType.SelectedValue = dr["ChequeType"].ToString();
            tbChequeNo.Text = dr["ChequeNo"].ToString();
            tbTellerID.Text = dr["TellerID"].ToString();
            rcbCurrencyPaid.SelectedValue = dr["CurrencyPaid"].ToString();
            rcbCurrencyPaid_OnSelectedIndexChanged(rcbCurrencyPaid, null);
            rcbAccountPaid.SelectedValue = dr["AccountPaidCode"].ToString();
            tbDealRate.Text = dr["DealRate"].ToString();
            tbAmtPaidToCust.Text = dr["AmountPaid"].ToString();
            rcbWaiveCharges.SelectedValue = dr["WaiveCharge"].ToString();
            tbNarrative.Text = dr["Narrative"].ToString();
            tbBeneName.Text = dr["BeneficialName"].ToString();
            tbAddress.Text = dr["Address"].ToString();
            tbLegalID.Text = dr["LegalID"].ToString();
            tbPlaceOfIssue.Text = dr["PlaceOfIssue"].ToString();
            if (dr["IssuedDate"].ToString() != "")
            {
                rdpIssDate.SelectedDate = Convert.ToDateTime(dr["IssuedDate"].ToString());
            }
            if (dr["Status"].ToString() == "AUT")
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                LoadToolBar_AllFalse();
                return;
            }
            LoadToolBar(false);

        }
        #region Properties
        protected void Print_Deal_Slip()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");
            //Open template
            string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/OutWardTransactions/cheque_withdrawl.doc");
            //Open the template document
            Aspose.Words.Document document = new Aspose.Words.Document(docPath);
            //Execute the mail merge.
            var ds = BankProject.DataProvider.TriTT.Print_Deal_slip("Cheque", "withdrwal", tbID.Text.Trim());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].TableName = "Info";
                document.MailMerge.ExecuteWithRegions(ds.Tables["Info"]);
                document.Save("Cheque_Withdrwal_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
            }
        }
        protected void LoadCurrency()
        {
            rcbCurrency.Items.Clear();
            rcbCurrency.Items.Add(new RadComboBoxItem("",""));
            rcbCurrency.DataSource = TriTT.B_LoadCurrency("", "");
            rcbCurrency.DataTextField = "Code";
            rcbCurrency.DataValueField = "Code";
            rcbCurrency.DataBind();
        }
        protected void tbAccountCustomer_onvalueChanged(object sender, EventArgs e)
        {
            LoadCustomerInfo();
        }
        protected void LoadCustomerInfo()
        {
            DataSet ds = TriTT.CHEQUE_WITHDRAWAL_LoadCustomerAcct(rcbCurrency.SelectedValue, tbAccountCustomer.Text);
            if (ds.Tables != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblNote.Text = "";
                DataRow Data = ds.Tables[0].Rows[0];
                tbCustomerID.Text = Data["CustomerID"].ToString();
                tbCustomerName.Text = Data["CustomerName"].ToString();
                tbBeneName.Text = tbCustomerName.Text;
                tbAddress.Text = Data["Address"].ToString();
                tbOldCustBal.Text = Data["WorkingAmount"].ToString();
                tbLegalID.Text = Data["DocID"].ToString();
                tbPlaceOfIssue.Text = Data["DocIssuePlace"].ToString();
                if (Data["DocIssueDate"].ToString() != "")
                {
                    rdpIssDate.SelectedDate = Convert.ToDateTime(Data["DocIssueDate"].ToString());
                }
                tbChequeEnd.Text = Data["ChequeNoEnd"].ToString();
                tbChequeStart.Text = Data["ChequeNoStart"].ToString();
            }
            else
            {
                lblNote.Text = "Account does not exists !";
                tbCustomerID.Text = "";
                tbCustomerName.Text = "";
                tbBeneName.Text = "";
                tbAddress.Text = "";
                tbOldCustBal.Text = "";
                tbLegalID.Text = "";
                tbPlaceOfIssue.Text = "";
                rdpIssDate.SelectedDate = null;
                tbChequeEnd.Text = ""; tbChequeStart.Text = "";
            }
        }
       
        protected void LoadChequeType()
        {
            rcbChequeType.Items.Clear();
            rcbChequeType.Items.Add(new RadComboBoxItem("", ""));
            rcbChequeType.DataSource = TriTT.CHEQUE_WITHDRAWAL_LoadChequeType();
            rcbChequeType.DataTextField = "Description";
            rcbChequeType.DataValueField = "Code";
            rcbChequeType.DataBind();
        }
        protected void LoadCurrencyPaid()
        {
            rcbCurrencyPaid.Items.Clear();
            rcbCurrencyPaid.Items.Add(new RadComboBoxItem("", ""));
            rcbCurrencyPaid.DataSource = TriTT.B_LoadCurrency("", "");
            rcbCurrencyPaid.DataTextField = "Code";
            rcbCurrencyPaid.DataValueField = "Code";
            rcbCurrencyPaid.DataBind();
        }
        protected void rcbCurrencyPaid_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbAccountPaid.Items.Clear();
            rcbAccountPaid.AppendDataBoundItems = true;
            rcbAccountPaid.Items.Add(new RadComboBoxItem("", ""));
            rcbAccountPaid.DataSource = TriTT.CHEQUE_WITHDRAWAL_LoadAcctPaid(rcbCurrencyPaid.SelectedValue);
            rcbAccountPaid.DataValueField = "AccountCode";
            rcbAccountPaid.DataTextField = "AcctHasName";
            rcbAccountPaid.DataBind();
        }
        protected void LoadToolBar_AllFalse()
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
        }
        private void LoadToolBar(bool isauthorize)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
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
            LoadCustomerInfo();
        }
        //protected void rcbCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    rcbAccCustomer.Items.Clear();
        //    rcbAccCustomer.AppendDataBoundItems = true;
        //    rcbAccCustomer.Items.Add(new RadComboBoxItem("", ""));
        //    rcbAccCustomer.DataSource = TriTT.CHEQUE_WITHDRAWAL_LoadCustomerAcct(rcbCurrency.SelectedValue);
        //    rcbAccCustomer.DataValueField = "AccountCode";
        //    rcbAccCustomer.DataTextField = "AcctHasName";
        //    rcbAccCustomer.DataBind();
        //}
        //protected void rcbAccCustomer_onItemDataBOund(object sender, RadComboBoxItemEventArgs e)
        //{
        //    DataRowView Data = e.Item.DataItem as DataRowView;
        //    e.Item.Attributes["CustomerID"] = Data["CustomerID"].ToString();
        //    e.Item.Attributes["CustomerName"] = Data["CustomerName"].ToString();
        //    e.Item.Attributes["Address"] = Data["Address"].ToString();
        //    e.Item.Attributes["WorkingAmount"] = Data["WorkingAmount"].ToString();
        //    e.Item.Attributes["DocID"] = Data["DocID"].ToString();
        //    e.Item.Attributes["DocIssuePlace"] = Data["DocIssuePlace"].ToString();
        //    e.Item.Attributes["DocIssueDate"] = Data["DocIssueDate"].ToString();
        //    e.Item.Attributes["ChequeNoStart"] = Data["ChequeNoStart"].ToString();
        //    e.Item.Attributes["ChequeNoEnd"] = Data["ChequeNoEnd"].ToString();
        //}
    }
}