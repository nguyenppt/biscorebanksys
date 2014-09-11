using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using BankProject.Repository;
using System.Data;
using System.Configuration;
using BankProject.Common;

namespace BankProject.Views.TellerApplication
{
    public partial class CashDeposits : PortalModuleBase
    {
        private void LoadToolBar(bool IsAuthorize)
        {
            RadToolBar1.FindItemByValue("btCommit").Enabled = !IsAuthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !IsAuthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = IsAuthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = IsAuthorize;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = IsAuthorize;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.QueryString["preview"] == null)
                {
                    this.ShowInputView();
                    LoadToolBar(false);
                }
                else
                {
                    this.ShowPreviewView();
                }

                //Session["DataKey"] = txtId.Text;
            }
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {

            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;

            switch (commandName)
            {
                case "Commit":
                    if (hdfCheckCustomer.Value == "0") return;

                    BankProject.DataProvider.Database.BCASHDEPOSIT_Insert(rcbAccountType.SelectedValue, txtId.Text, lbAccountId.Text, lblAmtPaidToCust.Value.HasValue ? lblAmtPaidToCust.Value.Value:0, 
                        lblCustBal.Value.HasValue ? lblCustBal.Value.Value : 0, lblNewCustBal.Value.HasValue ? lblNewCustBal.Value.Value : 0,
                        cmbCurrencyDeposited.SelectedValue, txtAmtDeposited.Value.HasValue ? txtAmtDeposited.Value.Value:0, txtDealRate.Value.HasValue ?  txtDealRate.Value.Value : 0, cmbWaiveCharges.SelectedValue,
                        txtNarrative.Text, txtPrintLnNoOfPS.Text, this.UserId, txtTellerId.Text, cmbCashAccount.SelectedValue);

                    if (cmbWaiveCharges.SelectedValue == "NO") Response.Redirect(EditUrl("waivecharges"));

                    Response.Redirect(string.Format("Default.aspx?tabid={0}", this.TabId.ToString()));

                    //this.EnableControls(true);
                    //this.SetDefaultValues();
                    //LoadToolBar(false);
                    break;

                case "Preview":
                    string unBlockAccountPreviewList = this.EditUrl("CashDepositePreviewList");
                    this.Response.Redirect(unBlockAccountPreviewList);
                    break;

                case "Authorize":
                    DataProvider.Database.BCASHDEPOSIT_UpdateStatus(rcbAccountType.SelectedValue, "AUT", txtId.Text, this.UserId.ToString());
                    Response.Redirect(string.Format("Default.aspx?tabid={0}", this.TabId.ToString()));
                    //LoadToolBar(false);
                    //this.EnableControls(true);
                    //this.SetDefaultValues();
                    //lblAmtPaidToCust.Text = "";
                    //lblCustBal.Text = "";
                    //lblNewCustBal.Text = "";
                    break;

                case "Reverse":
                    DataProvider.Database.BCASHDEPOSIT_UpdateStatus(rcbAccountType.SelectedValue, "REV", txtId.Text, this.UserId.ToString());
                    Response.Redirect(string.Format("Default.aspx?tabid={0}", this.TabId.ToString()));
                    //LoadToolBar(false);
                    //this.SetDefaultValues();
                    //this.EnableControls(true);
                    break;

            }
        }

        private void PrintDocument()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");
            //Open template
            string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/AccountTransaction/CashDeposit.docx");
            //Open the template document
            Aspose.Words.Document document = new Aspose.Words.Document(docPath);
            //Execute the mail merge.
            
            var ds = BankProject.DataProvider.Database.BCASHDEPOSIT_Print_GetByCode(txtId.Text);
            document.MailMerge.ExecuteWithRegions(ds.Tables[0]); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            document.Save("CashDeposit_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }

        private void PrintOpenAcc()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");
            //Open template
            string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/MainAccount/OpenAccount.docx");
            //Open the template document
            Aspose.Words.Document document = new Aspose.Words.Document(docPath);
            //Execute the mail merge.

            var ds = BankProject.DataProvider.Database.BOPENACCOUNT_Print_GetByCode(cmbCustomerAccount.Text);

            if (ds != null && ds.Tables.Count > 0)
            {
                ds.Tables[0].Rows[0]["ChiNhanh"] = ConfigurationManager.AppSettings["ChiNhanh"];
                ds.Tables[0].Rows[0]["BranchAddress"] = ConfigurationManager.AppSettings["BranchAddress"];
                ds.Tables[0].Rows[0]["BranchTel"] = ConfigurationManager.AppSettings["BranchTel"];

                ds.Tables[0].TableName = "Info";
                ds.Tables[1].TableName = "Detail";
            }

            document.MailMerge.ExecuteWithRegions(ds.Tables["Info"]);
            document.MailMerge.ExecuteWithRegions(ds.Tables["Detail"]);
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            document.Save("OpenAccount_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }

        private void EnableControls(bool enable)
        {
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, enable);
        }       

        private void ShowPreviewView()
        {
            if (Request.QueryString["preview"] == null)
                return;

            this.SetPreviewValues("");
        }

        private void ShowInputView()
        {
            this.SetDefaultValues();
        }

        private void SetPreviewValues(string code)
        {
            DataSet ds;
            if (code != "")
                ds = DataProvider.Database.BCASHDEPOSIT_GetByCode(code);
            else
                ds = DataProvider.Database.BCASHDEPOSIT_GetByID(int.Parse(Request.QueryString["codeid"].ToString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtId.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                

                this.cmbCustomerAccount.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                this.lbAccountId.Text = ds.Tables[0].Rows[0]["CustomerAccount"].ToString();
                this.rcbAccountType.SelectedValue = ds.Tables[0].Rows[0]["AccountType"].ToString();
                cmbCustomerAccount_TextChanged(cmbCustomerAccount, null);
                this.txtTellerId.Text = ds.Tables[0].Rows[0]["TellerName"].ToString();
                this.cmbCurrencyDeposited.SelectedValue = ds.Tables[0].Rows[0]["CurrencyDeposited"].ToString();
                cmbCurrencyDeposited_OnSelectedIndexChanged(cmbCurrencyDeposited, null);
                this.cmbCashAccount.SelectedValue = ds.Tables[0].Rows[0]["CashAccount"].ToString();
                this.lblAmtPaidToCust.Value = ds.Tables[0].Rows[0]["AmtPaidToCust"] != null && ds.Tables[0].Rows[0]["AmtPaidToCust"] != DBNull.Value ? 
                                                double.Parse(ds.Tables[0].Rows[0]["AmtPaidToCust"].ToString()):0;

                this.lblCustBal.Value = ds.Tables[0].Rows[0]["CustBallance"] != null && ds.Tables[0].Rows[0]["CustBallance"] != DBNull.Value ? 
                                                double.Parse(ds.Tables[0].Rows[0]["CustBallance"].ToString()) : 0;

                this.lblNewCustBal.Value = ds.Tables[0].Rows[0]["NewCustBallance"] != null && ds.Tables[0].Rows[0]["NewCustBallance"] != DBNull.Value
                                            ? double.Parse(ds.Tables[0].Rows[0]["NewCustBallance"].ToString()) : 0;

                this.txtAmtDeposited.Value = ds.Tables[0].Rows[0]["AmountDeposited"] != null && ds.Tables[0].Rows[0]["AmountDeposited"] != DBNull.Value ? 
                                            double.Parse(ds.Tables[0].Rows[0]["AmountDeposited"].ToString()) : 0;

                this.txtDealRate.Value = ds.Tables[0].Rows[0]["DealRate"] != null && ds.Tables[0].Rows[0]["DealRate"] != DBNull.Value ?
                                        double.Parse(ds.Tables[0].Rows[0]["DealRate"].ToString()) : 0;

                this.cmbWaiveCharges.SelectedValue = ds.Tables[0].Rows[0]["WaiveCharges"].ToString();
                txtNarrative.Text = ds.Tables[0].Rows[0]["Narrative"].ToString();
                txtPrintLnNoOfPS.Text = ds.Tables[0].Rows[0]["PrintLnNoOfPS"].ToString();
                //this.txtNarrative.Text = ds.Tables[0].Rows[0]["Narrative"].ToString();
                //this.txtPrintLnNoOfPS.Text = ds.Tables[0].Rows[0]["PrintLnNoOfPS"].ToString();

                bool isautho = ds.Tables[0].Rows[0]["Status"].ToString() == "AUT";
                this.EnableControls(Request.QueryString["codeid"] == null && !isautho);
                LoadToolBar(Request.QueryString["codeid"] != null);

                if (isautho)
                {
                    RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                }
            }
        }

        private void SetDefaultValues()
        {
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
            string SoTT = BankProject.DataProvider.Database.B_BMACODE_GetNewSoTT("BCASTDEPOSIT").Tables[0].Rows[0]["SoTT"].ToString();
            this.txtId.Text = "TT." + Util. getIDDate() +"."+ SoTT.PadLeft(5, '0');
            txtPrintLnNoOfPS.Text = "1";
            this.txtTellerId.Text = this.UserInfo.Username.ToString();            
        }

       

        protected void cmbCustomerAccount_TextChanged(object sender, EventArgs e)
        {
            if (cmbCustomerAccount.Text != "" && rcbAccountType.SelectedValue != "")
            {
                DataSet ds = BankProject.DataProvider.Database.BOPENACCOUNT_GetByCode_OPEN(cmbCustomerAccount.Text, rcbAccountType.SelectedValue);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblCustomerId.Text = ds.Tables[0].Rows[0]["CustomerId"].ToString();
                    lblCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    lbAccountId.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                    lbAccountTitle.Text = ds.Tables[0].Rows[0]["AccountTitle"].ToString();
                    cmbCurrency.Text = ds.Tables[0].Rows[0]["Currency"].ToString();

                    if (ds.Tables[0].Rows[0]["WorkingAmount"] != null && ds.Tables[0].Rows[0]["WorkingAmount"] != DBNull.Value)
                        lblCustBal.Value = double.Parse(ds.Tables[0].Rows[0]["WorkingAmount"].ToString());

                    lbErrorAccount.Visible = false;
                    hdfCheckCustomer.Value = "1";
                }
                else
                {
                    hdfCheckCustomer.Value = "0";
                    lbErrorAccount.Visible = true;
                    lbErrorAccount.Text = "Customer Account does not exist";
                    lblCustomerId.Text = "";
                    lblCustomerName.Text = "";
                    cmbCustomerAccount.Text = "";
                    lbAccountTitle.Text = "";
                    cmbCurrency.Text = "";
                    lblCustBal.Text = "";
                }
            }
            else
            {
                hdfCheckCustomer.Value = "0";
                lbErrorAccount.Text = "";
                lblCustomerId.Text = "";
                lblCustomerName.Text = "";
                cmbCustomerAccount.Text = "";
                lbAccountTitle.Text = "";
                cmbCurrency.Text = "";
                lblCustBal.Text = "";
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            SetPreviewValues(txtId.Text);//
        }

        protected void btPrintDeal_Click(object sender, EventArgs e)
        {
            PrintDocument();
        }

        protected void btPrintOpen_Click(object sender, EventArgs e)
        {
            PrintOpenAcc();
        }

        protected void rcbAccountType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbCustomerAccount_TextChanged(cmbCustomerAccount, null);
        }

        protected void cmbCurrencyDeposited_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbCashAccount.Items.Clear();
            cmbCashAccount.Items.Add(new RadComboBoxItem("", ""));
            cmbCashAccount.AppendDataBoundItems = true;
            cmbCashAccount.DataSource = DataProvider.Database.BOPENACCOUNT_INTERNAL_GetByCode(rcbAccountType.SelectedValue, lblCustomerId.Text, cmbCurrencyDeposited.SelectedValue, cmbCustomerAccount.Text);
            cmbCashAccount.DataTextField = "Display";
            cmbCashAccount.DataValueField = "ID";
            cmbCashAccount.DataBind();
        }
    }
}