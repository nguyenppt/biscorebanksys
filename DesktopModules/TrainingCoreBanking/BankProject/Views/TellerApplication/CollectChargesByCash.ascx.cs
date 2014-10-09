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
    public partial class CollectChargesByCash : PortalModuleBase
    {
        const double percentVat = 0.1;//10%
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            DataSet dsc = DataProvider.DataTam.BCUSTOMERS_INDIVIDUAL_GetbyID("");
            rcbCustomerID.DataSource = dsc;
            rcbCustomerID.DataTextField = "Display";
            rcbCustomerID.DataValueField = "CustomerID";
            rcbCustomerID.DataBind();

            rcbCategoryPL.Items.Add(new RadComboBoxItem("", ""));
            rcbCategoryPL.AppendDataBoundItems = true;
            rcbCategoryPL.DataSource = DataProvider.Database.B_BBPLACCOUNT_GetAll();
            rcbCategoryPL.DataTextField = "Display";
            rcbCategoryPL.DataValueField = "Id";
            rcbCategoryPL.DataBind();

            var Currency = TriTT.B_LoadCurrency("", "");
            rcbCurrency.Items.Clear();
            rcbCurrency.Items.Add(new RadComboBoxItem(""));
            rcbCurrency.AppendDataBoundItems = true;
            rcbCurrency.DataValueField = "Code";
            rcbCurrency.DataTextField = "Code";
            rcbCurrency.DataSource = Currency;
            rcbCurrency.DataBind();

            if (Request.QueryString["codeid"] == null)
            {
                LoadToolBar(false);
                firstLoad();
            }
            else
            {
                LoadData("");
            }
        }
        void firstLoad()
        {
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
            txtId.Text = TriTT.B_BMACODE_GetNewID_3par("COLL_CONTIN_ENTRY", "TT", ".");
            string SoTT = BankProject.DataProvider.Database.B_BMACODE_GetNewSoTT("VATSERIALNO").Tables[0].Rows[0]["SoTT"].ToString();
            this.tbVATSerialNo.Text = SoTT.PadLeft(4, '0');
            txtTellerId.Text = this.UserInfo.Username;
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

            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "commit":
                    string CategoryPL = rcbCategoryPL.Text != "" ? rcbCategoryPL.Text.Split('-')[1].Trim() : "";
                    if (lblCustomerName.Text != "")
                    {
                        ShowMsgBox("CustomerID not exists. Please check again !"); return;
                    }
                    BankProject.DataProvider.Database.BCOLLECTCHARGESBYCASH_Insert(rcbAccountType.SelectedValue, txtId.Text, tbCustomerID.Text, tbAddress.Text, tbLegalID.Text,tbIsssuedDate.SelectedDate
                                                        , tbPlaceOfIss.Text,txtTellerId.Text,rcbCurrency.SelectedValue,rcbCashAccount.SelectedValue, tbChargeAmountLCY.Value.HasValue ? tbChargeAmountLCY.Value.Value : 0
                                                        , tbChargeAmountFCY.Value.HasValue ? tbChargeAmountFCY.Value.Value : 0, tbValueDate.SelectedDate,  rcbCategoryPL.SelectedValue, CategoryPL
                                                        , tbDealRate.Value.HasValue ? tbDealRate.Value.Value : 0, tbVatAmountLCY.Value.HasValue ? tbVatAmountLCY.Value.Value : 0
                                                        , tbVatAmountFCY.Value.HasValue ? tbVatAmountFCY.Value.Value : 0, tbTotalAmountLCY.Value.HasValue ? tbTotalAmountLCY.Value.Value : 0
                                                        , tbTotalAmountFCY.Value.HasValue ? tbTotalAmountFCY.Value.Value : 0, tbVATSerialNo.Text, tbNarrative.Text, this.UserId
                                                        , tbCustomerName.Text, tbCustomerName.Text, rcbCashAccount.Text.Replace(rcbCurrency.SelectedValue + "-", ""));

                    BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                    firstLoad();
                    break;

                case "Preview":
                    string unBlockAccountPreviewList = this.EditUrl("chitiet");
                    this.Response.Redirect(unBlockAccountPreviewList);
                    break;

                case "authorize":
                    DataProvider.Database.BCOLLECTCHARGESBYCASH_UpdateStatus(rcbAccountType.SelectedValue, "AUT", txtId.Text, this.UserId.ToString());
                    LoadToolBar(false);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    firstLoad();

                    break;

                case "reverse":
                    DataProvider.Database.BCOLLECTCHARGESBYCASH_UpdateStatus(rcbAccountType.SelectedValue, "REV", txtId.Text, this.UserId.ToString());
                    LoadToolBar(false);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    if (rcbCurrency.SelectedValue == "VND")
                    {
                        tbChargeAmountFCY.Enabled = false;
                    }
                    else tbChargeAmountLCY.Enabled = false;
                    //firstLoad();
                    break;

                case "print":
                    PrintVat();
                    break;
            }
        }

        protected void rcbCustomerID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //DataRowView row = e.Item.DataItem as DataRowView;
            //e.Item.Attributes["CustomerName"] = row["CustomerName2"].ToString();
            //e.Item.Attributes["Address"] = row["Address"].ToString();
            //e.Item.Attributes["IdentityNo"] = row["IdentityNo"].ToString();
            //e.Item.Attributes["IssueDate"] = row["IssueDate"].ToString();
            //e.Item.Attributes["IssuePlace"] = row["IssuePlace"].ToString();
        }

        protected void rcbCurrency_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (rcbCurrency.SelectedValue)
            {
                case "VND":
                    tbChargeAmountFCY.Value = null;
                    tbVatAmountFCY.Value = null;
                    tbTotalAmountFCY.Value = null;

                    tbChargeAmountFCY.Enabled = false;
                    tbVatAmountFCY.Enabled = false;
                    tbTotalAmountFCY.Enabled = false;

                    tbChargeAmountLCY.Enabled = true;
                    tbVatAmountLCY.Enabled = true;
                    tbTotalAmountLCY.Enabled = true;
                    tbChargeAmountLCY_TextChanged(sender, null);
                    break;

                default:
                    tbChargeAmountLCY.Value = null;
                    tbVatAmountLCY.Value = null;
                    tbTotalAmountLCY.Value = null;

                    tbChargeAmountLCY.Enabled = false;
                    tbVatAmountLCY.Enabled = false;
                    tbTotalAmountLCY.Enabled = false;

                    tbChargeAmountFCY.Enabled = true;
                    tbVatAmountFCY.Enabled = true;
                    tbTotalAmountFCY.Enabled = true;
                    tbChargeAmountFCY_TextChanged(sender, null);
                    break;
            }

            rcbCashAccount.Items.Clear();
            rcbCashAccount.Items.Add(new RadComboBoxItem("", ""));
            rcbCashAccount.AppendDataBoundItems = true;
            rcbCashAccount.DataSource = DataProvider.Database.BOPENACCOUNT_INTERNAL_GetByCode(rcbAccountType.SelectedValue, tbCustomerID.Text, rcbCurrency.SelectedValue, "");
            rcbCashAccount.DataTextField = "Display";
            rcbCashAccount.DataValueField = "ID";
            rcbCashAccount.DataBind();
        }

        protected void tbChargeAmountLCY_TextChanged(object sender, EventArgs e)
        {
            tbVatAmountLCY.Value = tbChargeAmountLCY.Value * percentVat;
            tbTotalAmountLCY.Value = tbChargeAmountLCY.Value + tbVatAmountLCY.Value;
        }
        protected void tbCustomerID_TextChanged(object sender, EventArgs e)
        {
            LoadCustomerInfo(tbCustomerID.Text);
        }
        protected void LoadCustomerInfo(string CustomerID)
        {
            DataSet ds = DataTam.BCUSTOMERS_INDIVIDUAL_GetbyID(CustomerID);
            lblCustomerName.Text = "";
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                tbCustomerName.Text = dr["GBFullName"].ToString();
                tbAddress.Text = dr["Address"].ToString();
                tbLegalID.Text = dr["DocID"].ToString();
                tbPlaceOfIss.Text = dr["IssuePlace"].ToString();
                if (dr["IssueDate"].ToString() != "")
                {
                    tbIsssuedDate.SelectedDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                }
            }
            else { lblCustomerName.Text = "Customer ID not exists."; }
        }
        protected void tbChargeAmountFCY_TextChanged(object sender, EventArgs e)
        {
            double DealRateValue = 1;
            if (tbDealRate.Value > 0)
            {
                DealRateValue = tbDealRate.Value.Value;
            }
            tbVatAmountFCY.Value = tbChargeAmountFCY.Value * percentVat;
            tbTotalAmountFCY.Value = tbVatAmountFCY.Value + tbChargeAmountFCY.Value;

            tbChargeAmountLCY.Value = tbChargeAmountFCY.Value * DealRateValue;
            tbVatAmountLCY.Value = tbChargeAmountLCY.Value * percentVat;
            tbTotalAmountLCY.Value = tbVatAmountLCY.Value + tbChargeAmountLCY.Value;
        }

        protected void tbDealRate_TextChanged(object sender, EventArgs e)
        {
            if (tbChargeAmountFCY.Value > 0)
                tbChargeAmountFCY_TextChanged(sender, null);
            else
                tbChargeAmountLCY_TextChanged(sender, null);
        }

        protected void tbVatAmountLCY_TextChanged(object sender, EventArgs e)
        {
            tbTotalAmountLCY.Value = tbVatAmountLCY.Value + tbChargeAmountLCY.Value;
        }
        
        protected void tbVatAmountFCY_TextChanged(object sender, EventArgs e)
        {
            tbTotalAmountFCY.Value = tbVatAmountFCY.Value + tbChargeAmountFCY.Value;
        }

        protected void rcbAccountType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbCashAccount.Items.Clear();
            rcbCashAccount.Items.Add(new RadComboBoxItem("", ""));
            rcbCashAccount.AppendDataBoundItems = true;
            rcbCashAccount.DataSource = DataProvider.Database.BOPENACCOUNT_INTERNAL_GetByCode(rcbAccountType.SelectedValue, rcbCustomerID.SelectedValue, rcbCurrency.SelectedValue, "");
            rcbCashAccount.DataTextField = "Display";
            rcbCashAccount.DataValueField = "ID";
            rcbCashAccount.DataBind();
        }

        private void LoadData(string code)
        {
            DataSet ds;
            if (code != "")
                ds = DataProvider.Database.BCOLLECTCHARGESBYCASH_GetByCode(code);
            else
                //ds = DataProvider.Database.BCOLLECTCHARGESBYCASH_GetByID(int.Parse(Request.QueryString["codeid"].ToString()));
                ds = DataProvider.Database.BCOLLECTCHARGESBYCASH_GetByCode(Request.QueryString["codeid"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtId.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                //rcbCustomerID.SelectedValue = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                tbCustomerID.Text = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                tbAddress.Text = ds.Tables[0].Rows[0]["CustomerAddress"].ToString();
                tbLegalID.Text = ds.Tables[0].Rows[0]["DocID"].ToString();
                if (ds.Tables[0].Rows[0]["DocIssueDate"] != null && ds.Tables[0].Rows[0]["DocIssueDate"] != DBNull.Value)
                    tbIsssuedDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["DocIssueDate"].ToString());
                tbPlaceOfIss.Text = ds.Tables[0].Rows[0]["DocIssuePlace"].ToString();
                txtTellerId.Text = ds.Tables[0].Rows[0]["Teller"].ToString();

                this.rcbCurrency.SelectedValue = ds.Tables[0].Rows[0]["Currency"].ToString();
                this.rcbAccountType.SelectedValue = ds.Tables[0].Rows[0]["AccountType"].ToString();
                rcbCurrency_SelectedIndexChanged(rcbCurrency, null);
                this.rcbCashAccount.SelectedValue = ds.Tables[0].Rows[0]["CustomerAccount"].ToString();
                rcbCategoryPL.SelectedValue = ds.Tables[0].Rows[0]["CategoryPLCode"].ToString();
                tbCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                tbCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName_VangLai"].ToString();
                if (ds.Tables[0].Rows[0]["ValueDate"] != null && ds.Tables[0].Rows[0]["ValueDate"] != DBNull.Value)
                    tbValueDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["ValueDate"].ToString());

                this.tbChargeAmountLCY.Value = ds.Tables[0].Rows[0]["ChargAmountLCY"] != null && ds.Tables[0].Rows[0]["ChargAmountLCY"] != DBNull.Value ?
                                                double.Parse(ds.Tables[0].Rows[0]["ChargAmountLCY"].ToString()) : 0;

                this.tbChargeAmountFCY.Value = ds.Tables[0].Rows[0]["ChargAmountFCY"] != null && ds.Tables[0].Rows[0]["ChargAmountFCY"] != DBNull.Value ?
                                              double.Parse(ds.Tables[0].Rows[0]["ChargAmountFCY"].ToString()) : 0;

                this.tbDealRate.Value = ds.Tables[0].Rows[0]["DealRate"] != null && ds.Tables[0].Rows[0]["DealRate"] != DBNull.Value ?
                                      double.Parse(ds.Tables[0].Rows[0]["DealRate"].ToString()) : 0;

                this.tbVatAmountLCY.Value = ds.Tables[0].Rows[0]["VatAmountLCY"] != null && ds.Tables[0].Rows[0]["VatAmountLCY"] != DBNull.Value ?
                                                double.Parse(ds.Tables[0].Rows[0]["VatAmountLCY"].ToString()) : 0;

                this.tbVatAmountFCY.Value = ds.Tables[0].Rows[0]["VatAmountFCY"] != null && ds.Tables[0].Rows[0]["VatAmountFCY"] != DBNull.Value ?
                                              double.Parse(ds.Tables[0].Rows[0]["VatAmountFCY"].ToString()) : 0;

                this.tbTotalAmountLCY.Value = ds.Tables[0].Rows[0]["TotalAmountLCY"] != null && ds.Tables[0].Rows[0]["TotalAmountLCY"] != DBNull.Value ?
                                              double.Parse(ds.Tables[0].Rows[0]["TotalAmountLCY"].ToString()) : 0;

                this.tbTotalAmountFCY.Value = ds.Tables[0].Rows[0]["TotalAmountFCY"] != null && ds.Tables[0].Rows[0]["TotalAmountFCY"] != DBNull.Value ?
                                              double.Parse(ds.Tables[0].Rows[0]["TotalAmountFCY"].ToString()) : 0;

                tbNarrative.Text = ds.Tables[0].Rows[0]["Narrative"].ToString();
                tbVATSerialNo.Text = ds.Tables[0].Rows[0]["VatSerialNo"].ToString();
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

        private void PrintVat()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");
            //Open template
            string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/CollectCharge/CollectChargeVAT.doc");
            //Open the template document
            Aspose.Words.Document document = new Aspose.Words.Document(docPath);
            //Execute the mail merge.

            var ds = BankProject.DataProvider.Database.BCOLLECTCHARGESBYCASH_Print_GetByCode(txtId.Text);
            document.MailMerge.ExecuteWithRegions(ds.Tables[0]); 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            document.Save("BCOLLECTCHARGESBYCASH_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }

        protected void btSearch_Click1(object sender, EventArgs e)
        {
            LoadData(txtId.Text);
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