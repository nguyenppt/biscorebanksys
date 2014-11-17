using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Data;
using System.Configuration;

namespace BankProject
{
    public partial class OpenCustomerAccount : PortalModuleBase
    {
        private void LoadToolBar(bool IsAuthorize)
        {
            RadToolBar1.FindItemByValue("btCommit").Enabled = !IsAuthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !IsAuthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = IsAuthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = IsAuthorize;
            RadToolBar1.FindItemByValue("btSearchNew").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = IsAuthorize;
            RadToolBar1.FindItemByValue("btEdit").Enabled = IsAuthorize;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.txtId.Text = GeneratedId.ToString();
            if (IsPostBack) return;
            DataSet ds = DataProvider.DataTam.BCUSTOMERS_INDIVIDUAL_GetbyID("");

            cmbCustomerId.Items.Add(new RadComboBoxItem("", ""));
            cmbCustomerId.AppendDataBoundItems = true;
            cmbCustomerId.DataSource = ds;
            cmbCustomerId.DataValueField = "CustomerID";
            cmbCustomerId.DataTextField = "Display";
            cmbCustomerId.DataBind();

            cmbIDJoinHolder.Items.Add(new RadComboBoxItem("", ""));
            cmbIDJoinHolder.AppendDataBoundItems = true;
            cmbIDJoinHolder.DataSource = ds;
            cmbIDJoinHolder.DataValueField = "CustomerID";
            cmbIDJoinHolder.DataTextField = "Display";
            cmbIDJoinHolder.DataBind();

            cmbCategory.Items.Add(new RadComboBoxItem("", ""));
            cmbCategory.AppendDataBoundItems = true;
            cmbCategory.DataSource = DataProvider.DataTam.BCATEGORY_GetAll_2();
            cmbCategory.DataTextField = "Display";
            cmbCategory.DataValueField = "ID";
            cmbCategory.DataBind();

            cmbRestrictTxn.Items.Add(new RadComboBoxItem("", ""));
            cmbRestrictTxn.AppendDataBoundItems = true;
            cmbRestrictTxn.DataSource = DataProvider.Database.B_BRESTRICT_TXN_GetAll();
            cmbRestrictTxn.DataTextField = "Name";
            cmbRestrictTxn.DataValueField = "Id";
            cmbRestrictTxn.DataBind();

            cmbRelationCode.Items.Add(new RadComboBoxItem("", ""));
            cmbRelationCode.AppendDataBoundItems = true;
            cmbRelationCode.DataSource = DataProvider.DataTam.BRELATIONCODE_GetAll();
            cmbRelationCode.DataTextField = "Display";
            cmbRelationCode.DataValueField = "ID";
            cmbRelationCode.DataBind();

            cmbAccountOfficer.Items.Add(new RadComboBoxItem("", ""));
            cmbAccountOfficer.AppendDataBoundItems = true;
            cmbAccountOfficer.DataSource = DataProvider.SQLData.B_BACCOUNTOFFICER_GetAll();
            cmbAccountOfficer.DataTextField = "Description";
            cmbAccountOfficer.DataValueField = "Code";
            cmbAccountOfficer.DataBind();

            if (Request.QueryString["codeid"] != null)
            {
                this.LoadToolBar(true);
                LoadDataPreview("");
            }
            else 
            {
                //this.cmbCurrency.SelectedValue = "VND";
                this.LoadToolBar(false);
                string SoTT = BankProject.DataProvider.Database.B_BMACODE_GetNewSoTT("BOPENACCOUNT").Tables[0].Rows[0]["SoTT"].ToString();
                this.txtId.Text = "07" + SoTT.PadLeft(9, '0') + "5";
            }
            
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "Commit")
            {
                string Category = cmbCategory.Text != "" ? cmbCategory.Text.Split('-')[1].Trim() : "";
                string ProductLine = cmbProductLine.Text != "" ? cmbProductLine.Text.Split('-')[1].Trim() : "";
                string ChargeCode = cmbChargeCode.Text != "" ? cmbChargeCode.Text.Split('-')[1].Trim() : "";
                string RestrictTxn = cmbRestrictTxn.Text != "" ? cmbRestrictTxn.Text : "";
                string RelationCode = cmbRelationCode.Text != "" ? cmbRelationCode.Text.Split('-')[1].Trim() : "";

                //DataSet ds = BankProject.DataProvider.Database.BOPENACCOUNT_KiemTraTK_ThanhToan(cmbCustomerId.SelectedValue, cmbCurrency.SelectedValue);
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "test", string.Format("<script>alert('This customer already has the {0} current account'); </script>", cmbCurrency.SelectedValue));
                //}
                //else
                //{
                    DataProvider.Database.BOPENACCOUNT_Insert(txtId.Text, cmbCustomerId.SelectedValue, lbCustomerType.Text, lbCustomerName.Text, cmbCategory.SelectedValue,
                    Category, cmbCurrency.SelectedValue, txtAccountTitle.Text, txtShortTitle.Text, tbIntCaptoAC.Text,
                    cmbAccountOfficer.SelectedValue, cmbAccountOfficer.Text, cmbProductLine.SelectedValue, ProductLine, cmbChargeCode.SelectedValue,
                    ChargeCode, cmbRestrictTxn.SelectedValue, RestrictTxn, cmbIDJoinHolder.SelectedValue, lbJoinHolderName.Text,
                    cmbRelationCode.SelectedValue, RelationCode, txtJoinNotes.Text, "",
                    "", "", "", "", "", this.UserId.ToString(), "", txtDocID.Text, lbCategoryType.Text);
                    //Response.Redirect(string.Format("Default.aspx?tabid={0}", this.TabId.ToString()));

                    BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                    string SoTT = BankProject.DataProvider.Database.B_BMACODE_GetNewSoTT("BOPENACCOUNT").Tables[0].Rows[0]["SoTT"].ToString();
                    this.txtId.Text = "07" + SoTT.PadLeft(9, '0') + "5";
                //}
            }

                
                
                

            if (commandName == "Preview")
            {
                Response.Redirect(EditUrl("chitiet"));
            }

            if (commandName == "Authorize")
            {
                DataProvider.Database.BOPENACCOUNT_UpdateStatus("AUT", txtId.Text, this.UserId.ToString());
                Response.Redirect(string.Format("Default.aspx?tabid={0}", this.TabId.ToString()));
                //BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                //BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                //this.LoadToolBar(false);
                //string SoTT = BankProject.DataProvider.Database.B_BMACODE_GetNewSoTT("BOPENACCOUNT").Tables[0].Rows[0]["SoTT"].ToString();
                //this.txtId.Text = "07." + SoTT.PadLeft(9, '0') + ".5";
                //this.txtId.Text = "06." + ("0000000000" + SoTT).Substring(("0000000000" + SoTT).Length - 10);
            }

            if (commandName == "Reverse")
            {
                DataProvider.Database.BOPENACCOUNT_UpdateStatus("REV", txtId.Text, this.UserId.ToString());
                Response.Redirect(string.Format("Default.aspx?tabid={0}", this.TabId.ToString()));
                //BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                //BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                //this.LoadToolBar(false);
                //string SoTT = BankProject.DataProvider.Database.B_BMACODE_GetNewSoTT("BOPENACCOUNT").Tables[0].Rows[0]["SoTT"].ToString();
                //this.txtId.Text = "07." + SoTT.PadLeft(9, '0') + ".5";
                //this.txtId.Text = "06." + ("0000000000" + SoTT).Substring(("0000000000" + SoTT).Length - 10);
            }

            if (commandName == "print")
                PrintDocument();
            if (commandName == "edit")
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                LoadToolBar(false);
            }
        }

        private void LoadDataPreview(string code)
        {
            DataSet ds;
            if(code != "")
                ds = DataProvider.Database.BOPENACCOUNT_GetByCode(code);
            else
                ds = DataProvider.Database.BOPENACCOUNT_GetByID(int.Parse(Request.QueryString["codeid"].ToString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtId.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                cmbCustomerId.SelectedValue = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                lbCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                lbCustomerType.Text = ds.Tables[0].Rows[0]["CustomerType"].ToString();
                txtDocID.Text = ds.Tables[0].Rows[0]["DocID"].ToString();
                cmbCurrency.SelectedValue = ds.Tables[0].Rows[0]["Currency"].ToString();
                cmbCategory.SelectedValue = ds.Tables[0].Rows[0]["CategoryID"].ToString();
                cmbCategory_onselectedindexchanged(cmbCategory, null);
                cmbProductLine.SelectedValue = ds.Tables[0].Rows[0]["ProductLineID"].ToString();

                txtAccountTitle.Text = ds.Tables[0].Rows[0]["AccountTitle"].ToString();
                txtShortTitle.Text = ds.Tables[0].Rows[0]["ShortTitle"].ToString();
                tbIntCaptoAC.Text = ds.Tables[0].Rows[0]["IntCapToAC"].ToString();
                cmbAccountOfficer.SelectedValue = ds.Tables[0].Rows[0]["AccountOfficerID"].ToString();

                cmbChargeCode.SelectedValue = ds.Tables[0].Rows[0]["ChargeCode"].ToString();
                cmbRestrictTxn.SelectedValue = ds.Tables[0].Rows[0]["RestrictTxnID"].ToString();
                cmbIDJoinHolder.SelectedValue = ds.Tables[0].Rows[0]["JoinHolderID"].ToString();
                lbJoinHolderName.Text = ds.Tables[0].Rows[0]["JoinHolderName"].ToString();
                cmbRelationCode.SelectedValue = ds.Tables[0].Rows[0]["RelationCode"].ToString();
                txtJoinNotes.Text = ds.Tables[0].Rows[0]["JoinNotes"].ToString();

                bool isautho = ds.Tables[0].Rows[0]["Status"].ToString() == "AUT";
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, Request.QueryString["codeid"] == null && !isautho);
                LoadToolBar(Request.QueryString["codeid"] != null);

                if (isautho)
                {
                    RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearchNew").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                    RadToolBar1.FindItemByValue("btEdit").Enabled = true;
                }

            }
        }

        protected void cmbCategory_onitemdatabound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Type"] = row["Type"].ToString();
        }

        protected void cmbCategory_onselectedindexchanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbProductLine.Items.Clear();
            cmbProductLine.Items.Add(new RadComboBoxItem("", ""));
            cmbProductLine.AppendDataBoundItems = true;
            cmbProductLine.DataSource = DataProvider.DataTam.B_BPRODUCTLINE_GetByType(cmbCategory.SelectedItem.Attributes["Type"]);
            cmbProductLine.DataTextField = "Description";
            cmbProductLine.DataValueField = "ProductID";
            cmbProductLine.DataBind();

            lbCategoryType.Text = cmbCategory.SelectedItem.Attributes["Type"];
        }

        protected void cmbCustomerId_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataSet ds = BankProject.DataProvider.DataTam.BCUSTOMERS_INDIVIDUAL_GetbyID(cmbCustomerId.SelectedValue);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lbCustomerName.Text = ds.Tables[0].Rows[0]["GBFullName"].ToString();
                lbCustomerType.Text = ds.Tables[0].Rows[0]["CustomerType"].ToString();
                txtDocID.Text = ds.Tables[0].Rows[0]["DocID"].ToString();
            }
            else
            {
                cmbCustomerId.Text = "";
                lbCustomerName.Text = "";
                txtDocID.Text = "";
            }
        }

        protected void cmbIDJoinHolder_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataSet ds = BankProject.DataProvider.DataTam.BCUSTOMERS_INDIVIDUAL_GetbyID(cmbIDJoinHolder.SelectedValue);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lbJoinHolderName.Text = ds.Tables[0].Rows[0]["GBFullName"].ToString();
            }
            else
            {
                cmbIDJoinHolder.Text = "";
                lbJoinHolderName.Text = "";
            }
        }
        protected void btSearch_Click(object sender, EventArgs e)
        {
            LoadDataPreview(txtId.Text);
        }

        private void PrintDocument()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");
            //Open template
            string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/MainAccount/OpenAccount.docx");
            //Open the template document
            Aspose.Words.Document document = new Aspose.Words.Document(docPath);
            //Execute the mail merge.

            var ds = BankProject.DataProvider.Database.BOPENACCOUNT_Print_GetByCode(txtId.Text);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
    }
}