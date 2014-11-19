using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;
using System.Data;
using System.Configuration;

namespace BankProject.Views.TellerApplication
{
    public partial class TransferByAccount : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            rcbProvince.Items.Clear();
            rcbProvince.Items.Add(new RadComboBoxItem(""));
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
            txtChargeAmtLCY.ReadOnly = true;
            txtChargeVatAmt.ReadOnly = true;
            LoadCurrency();

            if (Request.QueryString["ID"] != null)
            {
                LoadToolBar(false);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                LoadDetail_Data(Request.QueryString["ID"].ToString());
            }
            else
            {
                LoadToolBar(true);
                Default_Setting();
            }

        }
        
        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            if (hdfDisable.Value == "0") return;
            var ToolBarButton = e.Item as RadToolBarButton;
            var commandname = ToolBarButton.CommandName;
            switch (commandname)
            { 
                case "commit":
                    TriTT.BOUTWARD_TRANS_BY_ACCT_Insert(tbID.Text, "UNA", rcbProductID.SelectedValue, rcbProductID.Text.Replace(rcbProductID.SelectedValue + " - ", ""), rcbBenCom.SelectedValue,
                        rcbBenCom.Text.Replace(rcbBenCom.SelectedValue + " - ", ""), rcbCurrency.SelectedValue, tbDebitAccount.Text, tbDebitAccount.Text,
                        tbAmount.Value.HasValue ? tbAmount.Value.Value : 0, tbSendingName.Text, tbSendingAddress.Text, tbTaxCode.Text, tbReceivingName.Text, tbReceivingName2.Text,
                        tbBenAccount.Text, tbIDCard.Text, rdpIssueDate.SelectedDate.HasValue? rdpIssueDate.SelectedDate: null, tbIssuePlace.Text, rcbProvince.SelectedValue,rcbProvince.Text !=""? rcbProvince.Text.Replace(rcbProvince.SelectedValue + " - ", ""):"",
                        tbPhone.Text, rcbBankCode.SelectedValue,rcbBankCode.Text !=""? rcbBankCode.Text.Replace(rcbBankCode.SelectedValue + " - ", ""):"", tbBankName.Text, tbPayNumber.Text,
                        UserInfo.Username.ToString(), tbNarrative.Text, tbNarrative2.Text, rcbWaiveCharge.SelectedValue, rcbSaveTemplate.SelectedValue, txtVatSerial.Text, txtChargeAmtLCY.Value.HasValue ? txtChargeAmtLCY.Value.Value : 0,
                        txtChargeVatAmt.Value.HasValue ? txtChargeVatAmt.Value.Value : 0, rcbCreditAccount.SelectedValue, tbOldBalance.Value.HasValue? tbOldBalance.Value.Value : 0
                        , tbNewBalance.Value.HasValue? tbNewBalance.Value.Value : 0);
                    switch (rcbWaiveCharge.SelectedValue)
                    {
                        case "NO":
                            Response.Redirect("Default.aspx?tabid=141");
                            break;
                        default:
                            Response.Redirect("Default.aspx?tabid="+this.TabId);
                            break;
                    }
                    break;
                case "Preview":
                    //LoadToolBar(true);
                    //BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    Response.Redirect(EditUrl("TransferByAccount_PL"));
                    break;
                case "reverse":
                    TriTT.BOUTWARD_TRANS_BY_ACCT_Update_Status(tbID.Text,"REV","",0,0,0);
                    LoadToolBar(true);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    if (rcbProductID.SelectedValue == "1000")
                    {
                        rcbProvince.Enabled = false;
                        rcbBankCode.Enabled = false;
                        tbBankName.ReadOnly = true;
                    }
                    break;
                case "authorize":
                    if (TriTT.BINWARD_CASH_WITHDRAW_Load_Status(tbID.Text, "trans_by_acct") == "AUT")
                    {
                        ShowMsgBox("this transaction is authorized, you can not authorize it again !"); return;
                    }
                    double debitAmt = tbAmount.Value.HasValue ? tbAmount.Value.Value : 0;
                    double chargeAmt = txtChargeAmtLCY.Value.HasValue ? txtChargeAmtLCY.Value.Value : 0;
                    double chargeVATAmt = txtChargeVatAmt.Value.HasValue ? txtChargeVatAmt.Value.Value : 0;
                    if (TriTT.BOUTWARD_TRANS_BY_ACCT_Update_Status(tbID.Text, "AUT", tbDebitAccount.Text, debitAmt, chargeAmt, chargeVATAmt).Tables[0].Rows[0]["check_amt"].ToString() == "not_enough")
                    {
                        ShowMsgBox("Your Debit Account does not have enough money to make this transaction !"); return;
                    }
                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
                case "print":
                    Print_Deal_Slip();
                    break;
            }
        }
        #endregion

        #region function_load
        protected void LoadDetail_Data(string ID)
        {
            DataSet ds = TriTT.BOUTWARD_TRANS_BY_ACCT_Load_Detail_Data(ID);
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                tbID.Text = dr["ID"].ToString();
                rcbProductID.SelectedValue = dr["ProductID"].ToString();
                rcbBenCom.SelectedValue = dr["BenComID"].ToString();
                rcbCurrency.SelectedValue = dr["Currency"].ToString();
                loadcreditacc();
                tbDebitAccount.Text = dr["DebitAcctID"].ToString();
                tbAmount.Text = dr["DebitAmount"].ToString();
                tbSendingName.Text = dr["SendingName"].ToString();
                tbSendingAddress.Text = dr["SendingAddress"].ToString();
                tbTaxCode.Text = dr["IDTaxCode"].ToString();
                tbReceivingName.Text = dr["ReceivingName"].ToString();
                tbReceivingName2.Text = dr["ReceivingName2"].ToString();
                tbBenAccount.Text = dr["BenAcctID"].ToString();
                tbIDCard.Text = dr["LegalID"].ToString();
                if (dr["IssueDate"].ToString() != "") rdpIssueDate.SelectedDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                tbIssuePlace.Text = dr["IssuePlace"].ToString();
                rcbProvince.SelectedValue = dr["ProvinceCode"].ToString();
                LoadBankCode();
                rcbBankCode.SelectedValue = dr["BankCode"].ToString();
                tbPhone.Text = dr["Phone"].ToString();
                tbBankName.Text = dr["BankName"].ToString();
                tbPayNumber.Text = dr["PayNumber"].ToString();
                tbTellerID.Text = dr["TellerID"].ToString();
                tbNarrative.Text = dr["Narrative"].ToString();
                tbNarrative2.Text = dr["Narrative2"].ToString();
                rcbWaiveCharge.SelectedValue = dr["WaiveCharge"].ToString();
                rcbSaveTemplate.SelectedValue = dr["SaveTemplate"].ToString();
                txtVatSerial.Text = dr["VATSerial"].ToString();
                txtChargeAmtLCY.Text = dr["ChargeAmtLCY"].ToString();
                txtChargeVatAmt.Text = dr["ChargeVATAmt"].ToString();
                tbOldBalance.Text = dr["OldBalance"].ToString();
                tbNewBalance.Text = dr["NewBalance"].ToString();
                if (dr["Status"].ToString() == "AUT") LoadToolBar_AllFalse();
            }
        }
        protected void Default_Setting()
        {
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
            tbID.Text = TriTT.B_BMACODE_3part_varMaCode_varSP("B_BMACODE_3part_varMaCode_varSP", "TRS_BY_ACCOUNT_ID", "TT");
            tbTellerID.Text = this.UserInfo.Username;
            tbDebitAccount.Text = "";
        }
        protected void tbDebitAccount_onTextChanged(object sender, EventArgs e)
        {
            LoadDebitAcct();
        }
        
        protected void LoadDebitAcct()
        {
            string currency = rcbCurrency.SelectedValue;
            if (currency != "" && tbDebitAccount.Text !="")
            {
                DataSet ds = TriTT.Load_Acct_Info_From_BOPENACCOUNT(currency, tbDebitAccount.Text);
                lblNote.Text = "";
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    tbSendingName.Text = dr["CustomerName"].ToString();
                    tbSendingAddress.Text = dr["Address"].ToString();
                    tbTaxCode.Text = dr["DocID"].ToString();
                    tbOldBalance.Text = dr["WorkingAmount"].ToString();
                }
                else
                {
                    lblNote.Text = "Account does not exist.";
                    tbSendingName.Text = "";
                    tbSendingAddress.Text = "";
                    tbTaxCode.Text = "";
                }
            }
        }
        protected void Load_BenAccount(object sender, EventArgs e)
        {
            Load_BenAccount_Info();
        }
        protected void Load_BenAccount_Info()
        {
            if (rcbProductID.SelectedValue == "3000")
            {
                DataSet ds = TriTT.OUTWARD_TRANFER_BY_ACCT_LoadBenAcct(tbBenAccount.Text);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    tbReceivingName.Text = dr["GBFullName"].ToString();
                    tbIDCard.Text = dr["DocID"].ToString();
                    tbIssuePlace.Text = dr["DocIssuePlace"].ToString();
                    if (dr["DocIssueDate"].ToString() !="")
                        rdpIssueDate.SelectedDate = Convert.ToDateTime(dr["DocIssueDate"].ToString());
                    tbPhone.Text = dr["MobilePhone"].ToString();
                }
            }
        }
        protected void Print_Deal_Slip()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");
            
            DataSet ds;
            if (rcbProductID.SelectedValue == "1000")
            {
                //Open template
                string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/OutWardTransactions/transfer_by_account_dien_CMND.doc");
                //Open the template document
                Aspose.Words.Document document = new Aspose.Words.Document(docPath);
                //Execute the mail merge.
                ds = BankProject.DataProvider.TriTT.Print_Deal_slip("Trans_By_Acct", "CMND", tbID.Text.Trim());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].TableName = "Info";
                    document.MailMerge.ExecuteWithRegions(ds.Tables["Info"]);
                    document.Save("TransferByAccount_CMND_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
                }
            }
            else 
            {
                //Open template
                string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/OutWardTransactions/transfer_by_account_dien_CITAD.doc");
                //Open the template document
                Aspose.Words.Document document = new Aspose.Words.Document(docPath);
                //Execute the mail merge.
                ds = BankProject.DataProvider.TriTT.Print_Deal_slip("Trans_By_Acct", "CITAD", tbID.Text.Trim());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //ds.Tables[0].Rows[0]["ChiNhanh"] = ConfigurationManager.AppSettings["ChiNhanh"];
                    //ds.Tables[0].Rows[0]["BranchAddress"] = ConfigurationManager.AppSettings["BranchAddress"];
                    //ds.Tables[0].Rows[0]["BranchTel"] = ConfigurationManager.AppSettings["BranchTel"];
                    ds.Tables[0].TableName = "Info";
                    //ds.Tables[1].TableName = "Detail";
                    //document.MailMerge.ExecuteWithRegions(ds.Tables["Info"]);
                    document.MailMerge.ExecuteWithRegions(ds.Tables["Info"]);
                    // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
                    document.Save("TransferByAccount_CITAD_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
                }
            }
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
            LoadDebitAcct();
        }
        protected void Load_BankCode(string Province)
        {
            DataSet ds = BankProject.DataProvider.Database.BBANKCODE_GetByProvince(Province);
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

        protected void LoadToolBar(bool flag)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = flag;
            RadToolBar1.FindItemByValue("btPreview").Enabled = flag;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = !flag;
            RadToolBar1.FindItemByValue("btReverse").Enabled = !flag;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
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
        

        protected void rcbProductID_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (rcbProductID.SelectedValue)
            {
                case "3000":
                    tbBenAccount.ReadOnly = false;
                    rcbProvince.Enabled = true;
                    rcbBankCode.Enabled = true;

                    break;
                case "1000":
                    tbBenAccount.ReadOnly = true;
                    rcbProvince.Enabled = false;
                    rcbBankCode.Enabled = false;
                    break;

                case "":
                    rcbCurrency.Items.Add(new RadComboBoxItem(""));
                    break;

            }
        }

        protected void rcbProvince_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadBankCode();
        }
        protected void LoadBankCode()
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
        protected void rcbWaiveCharge_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (rcbWaiveCharge.SelectedValue)
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