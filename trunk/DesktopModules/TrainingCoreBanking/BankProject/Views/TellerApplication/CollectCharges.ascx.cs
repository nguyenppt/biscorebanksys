using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class CollectCharges : PortalModuleBase
    {
        const double percentVat = 0.1;//10%
        private string Refix_BMACODE()
        {
            return "TT";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            rcbCategoryPL.Items.Add(new RadComboBoxItem("", ""));
            rcbCategoryPL.AppendDataBoundItems = true;
            rcbCategoryPL.DataSource = DataProvider.Database.B_BBPLACCOUNT_GetAll();
            rcbCategoryPL.DataTextField = "Display";
            rcbCategoryPL.DataValueField = "Id";
            rcbCategoryPL.DataBind();

            if (Request.QueryString["codeid"] == null)
            {
                LoadToolBar(false);
                firstLoad();
            }
            else
            {
                LoadData(Request.QueryString["codeid"].ToString());
            }

        }

        private void firstLoad()
        {
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
            rcbCurrency.Focus();
            tbDepositCode.Text = TriTT.B_BMACODE_GetNewID_3par("COLL_CONTIN_ENTRY", Refix_BMACODE(), ".");
            string SoTT = BankProject.DataProvider.Database.B_BMACODE_GetNewSoTT("VATSERIALNO").Tables[0].Rows[0]["SoTT"].ToString();
            this.tbVATSerialNo.Text = SoTT.PadLeft(4, '0');
            rdpValueDate.SelectedDate = DateTime.Now;

        }

        private void LoadToolBar(bool IsAuthorize)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = !IsAuthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !IsAuthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = IsAuthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = IsAuthorize;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = IsAuthorize;
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            var commandName = ToolBarButton.CommandName;
            switch (commandName)
            {
                case "commit":
                    if (hdfCheckCustomer.Value == "0") return;

                    string CategoryPL = rcbCategoryPL.Text != "" ? rcbCategoryPL.Text.Split('-')[1].Trim() : "";
                    BankProject.DataProvider.Database.BCOLLECTCHARGESFROMACCOUNT_Insert(rcbAccountType.SelectedValue, tbDepositCode.Text, rcbDebitAccount.Text, tbChargeAmountLCY.Value.HasValue ? tbChargeAmountLCY.Value.Value : 0
                                                        , tbChargeAmountFCY.Value.HasValue ? tbChargeAmountFCY.Value.Value : 0, rdpValueDate.SelectedDate, rcbCategoryPL.SelectedValue, CategoryPL
                                                        , txtDealRate.Value.HasValue ? txtDealRate.Value.Value : 0, tbVATAmountLCY.Value.HasValue ? tbVATAmountLCY.Value.Value : 0
                                                        , tbVATAmount.Value.HasValue ? tbVATAmount.Value.Value : 0, tbTotalAmountLCY.Value.HasValue ? tbTotalAmountLCY.Value.Value : 0
                                                        , tbTotalAmount.Value.HasValue ? tbTotalAmount.Value.Value : 0, tbVATSerialNo.Text, tbNarrative.Text, this.UserId
                                                        , lblCustomerID.Text, lblCustomerName.Text, rcbCurrency.Text);

                    BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                    firstLoad();
                    break;

                case "Preview":
                    string unBlockAccountPreviewList = this.EditUrl("chitiet");
                    this.Response.Redirect(unBlockAccountPreviewList);
                    break;

                case "authorize":
                    DataSet ds = Database.BCOLLECTCHARGESFROMACCOUNT_Check_Available_Amt(rcbAccountType.SelectedValue, tbDepositCode.Text);
                    if (ds.Tables[0].Rows[0]["Update_Status"].ToString() == "OK")
                    {
                        DataProvider.Database.BCOLLECTCHARGESFROMACCOUNT_UpdateStatus(rcbAccountType.SelectedValue, "AUT", tbDepositCode.Text, this.UserId.ToString());
                        LoadToolBar(false);
                        BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                        firstLoad();
                    }
                    else
                    {
                        ShowMsgBox("You can not Authorize this transaction, Your available Account Amount is " + 
                            string.Format("{0:C}", Convert.ToDouble( ds.Tables[0].Rows[0]["Available_Amt"].ToString())).Replace("$","")
                            + " .Please check again !"); 
                        return;
                    }
                    break;

                case "reverse":
                    DataProvider.Database.BCOLLECTCHARGESFROMACCOUNT_UpdateStatus(rcbAccountType.SelectedValue, "REV", tbDepositCode.Text, this.UserId.ToString());
                    LoadToolBar(false);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    if (rcbCurrency.Text == "VND")
                        tbChargeAmountFCY.Enabled = false;
                    else tbChargeAmountLCY.Enabled = false;
                    //firstLoad();
                    break;

                case "print":
                    PrintVat();
                    break;
            }
        }

        private void PrintVat()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");
            //Open template
            string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/CollectCharge/CollectChargeVAT.doc");
            //Open the template document
            Aspose.Words.Document document = new Aspose.Words.Document(docPath);
            //Execute the mail merge.

            var ds = BankProject.DataProvider.Database.BCOLLECTCHARGESFROMACCOUNT_Print_GetByCode(tbDepositCode.Text);
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].TableName = "Table";
                document.MailMerge.ExecuteWithRegions(ds.Tables["Table"]); //moas mat thoi jan voi cuc gach nay woa 
                // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
                document.Save("BCOLLECTCHARGESFROMACCOUNT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
            }
        }

        protected void tbChargeAmountLCY_TextChanged(object sender, EventArgs e)
        {
            tbVATAmountLCY.Value = tbChargeAmountLCY.Value * percentVat;
            tbTotalAmountLCY.Value = tbVATAmountLCY.Value + tbChargeAmountLCY.Value;
        }

        protected void tbChargeAmountFCY_TextChanged(object sender, EventArgs e)
        {
            double DealRateValue = 1;
            if (txtDealRate.Value > 0)
            {
                DealRateValue = txtDealRate.Value.Value;
            }
            tbVATAmount.Value = tbChargeAmountFCY.Value * percentVat;
            tbTotalAmount.Value = tbVATAmount.Value + tbChargeAmountFCY.Value;

            tbChargeAmountLCY.Value = tbChargeAmountFCY.Value * DealRateValue;
            tbVATAmountLCY.Value = tbChargeAmountLCY.Value * percentVat;
            tbTotalAmountLCY.Value = tbVATAmountLCY.Value + tbChargeAmountLCY.Value;
        }

        protected void txtDealRate_TextChanged(object sender, EventArgs e)
        {
            if (tbChargeAmountFCY.Value > 0)
                tbChargeAmountFCY_TextChanged(sender, null);
            else
                tbChargeAmountLCY_TextChanged(sender, null);
        }

        protected void tbVATAmountLCY_TextChanged(object sender, EventArgs e)
        {
            tbTotalAmountLCY.Value = tbVATAmountLCY.Value + tbChargeAmountLCY.Value;
        }

        protected void tbVATAmount_TextChanged(object sender, EventArgs e)
        {
            tbTotalAmount.Value = tbVATAmount.Value + tbChargeAmountFCY.Value;
        }

        protected void cmbCustomerAccount_TextChanged(object sender, EventArgs e)
        {
            if (rcbDebitAccount.Text != "" && rcbAccountType.SelectedValue != "")
            {
                DataSet ds = BankProject.DataProvider.Database.BOPENACCOUNT_GetByCode_OPEN(rcbDebitAccount.Text, rcbAccountType.SelectedValue);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblCustomerID.Text = ds.Tables[0].Rows[0]["CustomerId"].ToString();
                    lblCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    lbAccountId.Text = ds.Tables[0].Rows[0]["Id"].ToString();
                    lbAccountTitle.Text = ds.Tables[0].Rows[0]["AccountTitle"].ToString();
                    rcbCurrency.Text = ds.Tables[0].Rows[0]["Currency"].ToString();

                    //if (ds.Tables[0].Rows[0]["WorkingAmount"] != null && ds.Tables[0].Rows[0]["WorkingAmount"] != DBNull.Value)
                    //    lblCustBal.Value = double.Parse(ds.Tables[0].Rows[0]["WorkingAmount"].ToString());

                    lbErrorAccount.Visible = false;
                    hdfCheckCustomer.Value = "1";

                    switch (rcbCurrency.Text)
                    {
                        case "VND":
                            tbChargeAmountFCY.Value = null;
                            tbVATAmount.Value = null;
                            tbTotalAmount.Value = null;

                            tbChargeAmountFCY.Enabled = false;
                            tbVATAmount.Enabled = false;
                            tbTotalAmount.Enabled = false;

                            tbChargeAmountLCY.Enabled = true;
                            tbVATAmountLCY.Enabled = true;
                            tbTotalAmountLCY.Enabled = true;
                            tbChargeAmountLCY_TextChanged(sender, null);
                            break;

                        default:
                            tbChargeAmountLCY.Value = null;
                            tbVATAmountLCY.Value = null;
                            tbTotalAmountLCY.Value = null;

                            tbChargeAmountLCY.Enabled = false;
                            tbVATAmountLCY.Enabled = false;
                            tbTotalAmountLCY.Enabled = false;

                            tbChargeAmountFCY.Enabled = true;
                            tbVATAmount.Enabled = true;
                            tbTotalAmount.Enabled = true;
                            tbChargeAmountFCY_TextChanged(sender, null);
                            break;
                    }
                }
                else
                {
                    hdfCheckCustomer.Value = "0";
                    lbErrorAccount.Visible = true;
                    lbErrorAccount.Text = "Customer Account does not exist";
                    lblCustomerID.Text = "";
                    lblCustomerName.Text = "";
                    rcbDebitAccount.Text = "";
                    lbAccountTitle.Text = "";
                    rcbCurrency.Text = "";
                    //lblCustBal.Text = "";
                }
            }
            else
            {
                hdfCheckCustomer.Value = "0";
                lbErrorAccount.Text = "";
                lblCustomerID.Text = "";
                lblCustomerName.Text = "";
                rcbDebitAccount.Text = "";
                lbAccountTitle.Text = "";
                rcbCurrency.Text = "";
                //lblCustBal.Text = "";
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            LoadData(tbDepositCode.Text);
        }

        protected void btInVat_Click(object sender, EventArgs e)
        {
            PrintVat();
        }
        
        protected void rcbAccountType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbCustomerAccount_TextChanged(rcbDebitAccount, null);
        }

        private void LoadData(string code)
        {
            DataSet ds;
            //if (code != "")
            //    ds = DataProvider.Database.BCOLLECTCHARGESFROMACCOUNT_GetByCode(code);
            //else
                ds = DataProvider.Database.BCOLLECTCHARGESFROMACCOUNT_GetByCodeDebit(Request.QueryString["codeid"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                tbDepositCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                this.rcbDebitAccount.Text = ds.Tables[0].Rows[0]["CustomerAccount"].ToString();
                lblCustomerID.Text = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                lblCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                rcbCurrency.Text = ds.Tables[0].Rows[0]["Currency"].ToString();
                if (Convert.ToDouble(ds.Tables[0].Rows[0]["ChargAmountLCY"].ToString()) != 0)
                {
                    tbChargeAmountLCY.Text = ds.Tables[0].Rows[0]["ChargAmountLCY"].ToString();
                }
                if (Convert.ToDouble(ds.Tables[0].Rows[0]["ChargAmountFCY"].ToString()) != 0)
                {
                    tbChargeAmountFCY.Text = ds.Tables[0].Rows[0]["ChargAmountFCY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ValueDate"].ToString() != "")
                {
                    rdpValueDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ValueDate"].ToString());
                }
                rcbCategoryPL.SelectedValue = ds.Tables[0].Rows[0]["CategoryPLCode"].ToString();
                rcbAccountType.SelectedValue = ds.Tables[0].Rows[0]["AccountType"].ToString();
                if (Convert.ToDouble(ds.Tables[0].Rows[0]["DealRate"].ToString()) != 0)
                {
                    txtDealRate.Text = ds.Tables[0].Rows[0]["DealRate"].ToString();
                }
                if (Convert.ToDouble(ds.Tables[0].Rows[0]["VatAmountLCY"].ToString()) != 0)
                {
                    tbVATAmountLCY.Text = ds.Tables[0].Rows[0]["VatAmountLCY"].ToString();
                }
                if (Convert.ToDouble(ds.Tables[0].Rows[0]["VatAmountFCY"].ToString()) != 0)
                {
                    tbVATAmount.Text = ds.Tables[0].Rows[0]["VatAmountFCY"].ToString();
                }
                if (Convert.ToDouble(ds.Tables[0].Rows[0]["TotalAmountLCY"].ToString()) != 0)
                {
                    tbTotalAmountLCY.Text = ds.Tables[0].Rows[0]["TotalAmountLCY"].ToString();
                }
                if (Convert.ToDouble(ds.Tables[0].Rows[0]["TotalAmountFCY"].ToString()) != 0)
                {
                    tbTotalAmount.Text = ds.Tables[0].Rows[0]["TotalAmountFCY"].ToString();
                }
                tbNarrative.Text = ds.Tables[0].Rows[0]["Narrative"].ToString();
                tbVATSerialNo.Text = ds.Tables[0].Rows[0]["VatSerialNo"].ToString();

                //cmbCustomerAccount_TextChanged(rcbDebitAccount, null);
                //this.lbAccountId.Text = ds.Tables[0].Rows[0]["CustomerAccount"].ToString();
                //this.rcbAccountType.SelectedValue = ds.Tables[0].Rows[0]["AccountType"].ToString();
                //if (ds.Tables[0].Rows[0]["ValueDate"] != null && ds.Tables[0].Rows[0]["ValueDate"] != DBNull.Value)
                //    rdpValueDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["ValueDate"].ToString());

                //this.tbChargeAmountLCY.Value = ds.Tables[0].Rows[0]["ChargAmountLCY"] != null && ds.Tables[0].Rows[0]["ChargAmountLCY"] != DBNull.Value ?
                //                                double.Parse(ds.Tables[0].Rows[0]["ChargAmountLCY"].ToString()) : 0;

                //this.tbChargeAmountFCY.Value = ds.Tables[0].Rows[0]["ChargAmountFCY"] != null && ds.Tables[0].Rows[0]["ChargAmountFCY"] != DBNull.Value ?
                //                              double.Parse(ds.Tables[0].Rows[0]["ChargAmountFCY"].ToString()) : 0;

                //this.txtDealRate.Value = ds.Tables[0].Rows[0]["DealRate"] != null && ds.Tables[0].Rows[0]["DealRate"] != DBNull.Value ?
                //                      double.Parse(ds.Tables[0].Rows[0]["DealRate"].ToString()) : 0;

                //this.tbVATAmountLCY.Value = ds.Tables[0].Rows[0]["VatAmountLCY"] != null && ds.Tables[0].Rows[0]["VatAmountLCY"] != DBNull.Value ?
                //                                double.Parse(ds.Tables[0].Rows[0]["VatAmountLCY"].ToString()) : 0;

                //this.tbVATAmount.Value = ds.Tables[0].Rows[0]["VatAmountFCY"] != null && ds.Tables[0].Rows[0]["VatAmountFCY"] != DBNull.Value ?
                //                              double.Parse(ds.Tables[0].Rows[0]["VatAmountFCY"].ToString()) : 0;

                //this.tbTotalAmountLCY.Value = ds.Tables[0].Rows[0]["TotalAmountLCY"] != null && ds.Tables[0].Rows[0]["TotalAmountLCY"] != DBNull.Value ?
                //                              double.Parse(ds.Tables[0].Rows[0]["TotalAmountLCY"].ToString()) : 0;

                //this.tbTotalAmount.Value = ds.Tables[0].Rows[0]["TotalAmountFCY"] != null && ds.Tables[0].Rows[0]["TotalAmountFCY"] != DBNull.Value ?
                //                              double.Parse(ds.Tables[0].Rows[0]["TotalAmountFCY"].ToString()) : 0;             

                //this.txtNarrative.Text = ds.Tables[0].Rows[0]["Narrative"].ToString();
                //this.txtPrintLnNoOfPS.Text = ds.Tables[0].Rows[0]["PrintLnNoOfPS"].ToString();
                bool isautho = ds.Tables[0].Rows[0]["Status"].ToString() == "AUT";
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, Request.QueryString["codeid"] == null && !isautho);
                LoadToolBar(Request.QueryString["codeid"] != null);

                if (isautho)
                {
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                }
            }
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