using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Data;

namespace BankProject.Views.TellerApplication
{
    public partial class CollectionForCreditCardPayment : PortalModuleBase
    {
        private static int Id = 7929;

        private void LoadToolBar(bool isauthorise)
        {

            RadToolBar1.FindItemByValue("btdoclines").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btdocnew").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btdraghand").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btreverse").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("searchNew").Enabled = false;
            RadToolBar1.FindItemByValue("print").Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            this.rdpIssueDate.SelectedDate = null;
            //this.txtId.Text = "TT/09161/0" + Id.ToString();
            SetDefault(false);

            

            DataSet ds = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            if (ds != null && ds.Tables[0] != null)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["CustomerID"] = "";
                dr["CustomerName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            cmbCustomerId.DataTextField = "CustomerName";
            cmbCustomerId.DataValueField = "CustomerID";
            cmbCustomerId.DataSource = ds;
            cmbCustomerId.DataBind();

            if (Request.QueryString["IsAuthorize"] != null)
            {
                LoadToolBar(true);
                loaddataPreview();
                //dvAudit.Visible = true;
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }
            else
            {
                LoadToolBar(false);
                //dvAudit.Visible = false;
            }

            this.cmbCustomerId.Focus();

        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "doclines")
            {
                Id++;
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                SetDefault(false);
            }

            if (commandName == "docnew")
            {
                Response.Redirect(EditUrl("chitiet"));
            }

            if (commandName == "draghand" || commandName == "reverse")
            {
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                LoadToolBar(false);
                SetDefault(true);
            }
        }

        private void SetDefault(bool isauthorize)
        {
            this.txtTellerId1.Text = this.UserInfo.Username.ToString();
            this.txtTellerId2.Text = this.UserInfo.Username.ToString();

            if (isauthorize) Id += 6;
            this.txtId.Text = "TT/09161/0" + Id.ToString();
            //this.txtId.Text = "06.000263730" + Id.ToString() + ".0";
        }

        void loaddataPreview()
        {
            if (Request.QueryString["LCCode"] != null)
            {
                string LCCode = Request.QueryString["LCCode"].ToString();
                switch (LCCode)
                {
                    case "1":
                        txtId.Text = "TT/09161/07929";

                        cmbCustomerId.SelectedValue = "1100001";
                        txtFullName.Text = "Phan Van Han";
                        txtAddress.Text = "100 Phan Van Han, Phuong 17, Quan Binh Thanh";

                        cmbDebitCurrency.SelectedValue = "VND";
                        cmbDebitAccount.SelectedValue = "VND";
                        cmbCreditCurrency.SelectedValue = "USD";
                        cmbCreditAccount.SelectedValue = "1";
                        txtTellerId1.Text = "host";
                        txtTellerId2.Text = "host";

                        txtDebitAmtLCY.Value = 213600000;
                        lblCreditAmount.Text = "10,000.00";
                        txtDealRate.Text = "0.2136";

                        txtCreditCardNumber.Text = "5529420350685465";
                       
                        break;

                    case "2":
                        txtId.Text = "TT/09161/07930";
                       
                        cmbCustomerId.SelectedValue = "1100003";
                        txtFullName.Text = "Pham Ngoc Thach";
                        txtAddress.Text = "180 Pham Ngoc Thach, Phuong 1, Quan 1";

                        cmbDebitCurrency.SelectedValue = "VND";
                        cmbDebitAccount.SelectedValue = "VND";
                        cmbCreditCurrency.SelectedValue = "USD";
                        cmbCreditAccount.SelectedValue = "1";
                        txtTellerId1.Text = "host";
                        txtTellerId2.Text = "host";

                        txtDebitAmtLCY.Value = 427400000;
                        lblCreditAmount.Text = "20,000.00";
                        txtDealRate.Text = "0.2136";

                        txtCreditCardNumber.Text = "6529420351485476";

                        break;

                    case "3":
                        txtId.Text = "TT/09161/07931";
                        
                        cmbCustomerId.SelectedValue = "1100005";
                        txtFullName.Text = "Truong Cong Dinh";
                        txtAddress.Text = "270 Truong Cong Dinh, Phuong 5, Quan 3";

                        cmbDebitCurrency.SelectedValue = "VND";
                        cmbDebitAccount.SelectedValue = "VND";
                        cmbCreditCurrency.SelectedValue = "USD";
                        cmbCreditAccount.SelectedValue = "1";
                        txtTellerId1.Text = "host";
                        txtTellerId2.Text = "host";

                        txtDebitAmtLCY.Value = 42740000;
                        lblCreditAmount.Text = "2,000.00";
                        txtDealRate.Text = "0.2136";

                        txtCreditCardNumber.Text = "55659424350915487";

                        break;

                    case "4":
                        txtId.Text = "TT/09161/07932";
                        
                        cmbCustomerId.SelectedValue = "1100004";
                        txtFullName.Text = "Vo Thi Sau";
                        txtAddress.Text = "200 Tran Quoc Thao, Phuong 5, Quan 3";

                        cmbDebitCurrency.SelectedValue = "VND";
                        cmbDebitAccount.SelectedValue = "VND";
                        cmbCreditCurrency.SelectedValue = "USD";
                        cmbCreditAccount.SelectedValue = "1";
                        txtTellerId1.Text = "host";
                        txtTellerId2.Text = "host";

                        txtDebitAmtLCY.Value = 320400000;
                        lblCreditAmount.Text = "15,000";
                        txtDealRate.Text = "0.2136";

                        txtCreditCardNumber.Text = "19659483350928493";
                        break;

                    case "5":
                        txtId.Text = "TT/09161/07933";
                        
                        cmbCustomerId.SelectedValue = "1100002";
                        txtFullName.Text = "Dinh Tien Hoang";
                        txtAddress.Text = "200 Tran Quoc Thao, Phuong 5, Quan 3";

                        cmbDebitCurrency.SelectedValue = "VND";
                        cmbDebitAccount.SelectedValue = "VND";
                        cmbCreditCurrency.SelectedValue = "USD";
                        cmbCreditAccount.SelectedValue = "1";
                        txtTellerId1.Text = "host";
                        txtTellerId2.Text = "host";

                        txtDebitAmtLCY.Value = 192240000;
                        lblCreditAmount.Text = "9,000";
                        txtDealRate.Text = "0.2136";

                        txtCreditCardNumber.Text = "19659483350928493";

                        break;
                }
            }
        }

        protected void cmbCustomerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerName"] = row["CustomerName2"].ToString();
            e.Item.Attributes["Address"] = row["Address"].ToString();
            e.Item.Attributes["IdentityNo"] = row["IdentityNo"].ToString();
            e.Item.Attributes["IssueDate"] = row["IssueDate"].ToString();
            e.Item.Attributes["IssuePlace"] = row["IssuePlace"].ToString();
            e.Item.Attributes["Telephone"] = row["Telephone"].ToString();
        }

    }
}