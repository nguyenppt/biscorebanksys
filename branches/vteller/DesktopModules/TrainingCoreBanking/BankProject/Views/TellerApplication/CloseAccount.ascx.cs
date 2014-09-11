using System;
using BankProject.Repository;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Data;

namespace BankProject.Views.TellerApplication
{
    public partial class CloseAccount : PortalModuleBase
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
            if (this.IsPostBack) return;


            if (Request.QueryString["codeid"] != null)
            {
                this.SetAccountId();//khi chon tu close account
                this.LoadToolBar(false);
                return;
            }
            if (Request.QueryString["closeid"] != null)
            {
                filldata("");//khi chon preview de duyet,
                return;
            }

            //Session["DataKey"] = txtId.Text;
        }

        void firstload()
        {
            this.LoadToolBar(false);
           
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
            this.lblDebitDate.SelectedDate = DateTime.Today;
            lbDateTime1.Text = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
            lbDateTime2.Text = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
        }

        private void SetAccountId()
        {
            DataSet ds = new DataSet();
            int codeId = 0;

            codeId = int.Parse(Request.QueryString["codeid"]);
            ds = BankProject.DataProvider.Database.BOPENACCOUNT_INTEREST_GetById(codeId);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                ds = BankProject.DataProvider.Database.BOPENACCOUNT_GetByID(codeId);
            }
            else
            {
                bool isround = ds.Tables[0].Rows[0]["Currency"].ToString() == "VND";
                lblWorkingBallance.Value = isround ? Math.Round(double.Parse(ds.Tables[0].Rows[0]["WorkingAmount"].ToString())) 
                                        : double.Parse(ds.Tables[0].Rows[0]["WorkingAmount"].ToString());
                lblTotalCreditInterest.Value = isround ? Math.Round(double.Parse(ds.Tables[0].Rows[0]["InterestAmount"].ToString()))
                                        : double.Parse(ds.Tables[0].Rows[0]["InterestAmount"].ToString());
                lblDebitAmount.Value = isround ? Math.Round(double.Parse(ds.Tables[0].Rows[0]["WorkingAmount"].ToString())) + Math.Round(double.Parse(ds.Tables[0].Rows[0]["InterestAmount"].ToString()))
                                            :double.Parse(ds.Tables[0].Rows[0]["WorkingAmount"].ToString()) + double.Parse(ds.Tables[0].Rows[0]["InterestAmount"].ToString());
                lblCreditAmount.Value = lblDebitAmount.Value;
            }

            lblCurrency.Text = ds.Tables[0].Rows[0]["Currency"].ToString();
            lbCreditCurrency.Text = ds.Tables[0].Rows[0]["Currency"].ToString();
            lblCustomer.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
            lblCustomerId.Text = ds.Tables[0].Rows[0]["CustomerId"].ToString();
            lblClosedAccount.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
            txtId.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
            this.lblDebitDate.SelectedDate = DateTime.Today;
            lbDateTime1.Text = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
            lbDateTime2.Text = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            //if (hdfDisable.Value == "0") return;

            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "Preview":
                    string urlReviewCloseAccountList = this.EditUrl("ReviewCloseAccountList");
                    this.Response.Redirect(urlReviewCloseAccountList);
                    break;

                case "Commit":
                    BankProject.DataProvider.Database.BOPENACCOUNT_CLOSE_Update(txtId.Text, cmbCloseOnline.SelectedValue, cmbCloseMode.SelectedValue, lblStandingOrders.Text,
                                                     lblUnclearedEntries.Text, lblChequesOS.Text, lblBankCards.Text, lblCCChgsOS.Text,
                                                     lblTotalCreditInterest.Value.HasValue ? lblTotalCreditInterest.Value.Value : 0, 0, 0, 0, lblDebitDate.SelectedDate
                                                     , cmbCreditCurrency.SelectedValue, cmbAccountPaid.SelectedValue, lblCreditAmount.Value.HasValue ? lblCreditAmount.Value.Value : 0, txtNarrative.Text);

                    firstload();
                    break;

                case "Authorize":
                    DataProvider.Database.BOPENACCOUNT_CLOSE_UpdateStatus("AUT", txtId.Text, this.UserId.ToString());
                    firstload();
                    break;

                case "Reverse":
                    DataProvider.Database.BOPENACCOUNT_CLOSE_UpdateStatus("REV", txtId.Text, this.UserId.ToString());
                    firstload();
                    break;
            }
        }

        void filldata(string code)
        {
            DataSet ds;
            if (code != "")
                ds = DataProvider.Database.BOPENACCOUNT_CLOSE_GetByCode(code);
            else
                ds = DataProvider.Database.BOPENACCOUNT_CLOSE_GetByID(int.Parse(Request.QueryString["closeid"].ToString()));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtId.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                cmbCloseMode.SelectedValue = ds.Tables[0].Rows[0]["CloseMode"].ToString();
                cmbCloseOnline.SelectedValue = ds.Tables[0].Rows[0]["CloseOnline"].ToString();
                lblCurrency.Text = ds.Tables[0].Rows[0]["Currency"].ToString();
                lblWorkingBallance.Value = double.Parse(ds.Tables[0].Rows[0]["WorkingAmount"].ToString());
                lblStandingOrders.Text = ds.Tables[0].Rows[0]["Close_StandingOrders"].ToString();
                lblUnclearedEntries.Text = ds.Tables[0].Rows[0]["Close_UnclearedEntries"].ToString();
                lblChequesOS.Text = ds.Tables[0].Rows[0]["Close_ChequesOS"].ToString();
                lblBankCards.Text = ds.Tables[0].Rows[0]["Close_BankCards"].ToString();
                lblCCChgsOS.Text = ds.Tables[0].Rows[0]["Close_CCChgsOS"].ToString();

                lblTotalCreditInterest.Value = double.Parse(ds.Tables[0].Rows[0]["Close_TotalCreditInterest"].ToString());

                if (ds.Tables[0].Rows[0]["Close_TotalCreditInterest"] != null && ds.Tables[0].Rows[0]["Close_TotalCreditInterest"] != DBNull.Value)
                    lblTotalCreditInterest.Value = double.Parse(ds.Tables[0].Rows[0]["Close_TotalCreditInterest"].ToString());

                if (ds.Tables[0].Rows[0]["Close_TotalCharges"] != null && ds.Tables[0].Rows[0]["Close_TotalCharges"] != DBNull.Value)
                    lblTotalCharges.Text = ds.Tables[0].Rows[0]["Close_TotalCharges"].ToString();

                if (ds.Tables[0].Rows[0]["Close_TotalVAT"] != null && ds.Tables[0].Rows[0]["Close_TotalVAT"] != DBNull.Value)
                    lblTotalVAT.Text = ds.Tables[0].Rows[0]["Close_TotalVAT"].ToString();

                lbCreditCurrency.Text = ds.Tables[0].Rows[0]["Currency"].ToString();
                lblCustomer.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                lblCustomerId.Text = ds.Tables[0].Rows[0]["CustomerId"].ToString();
                lblClosedAccount.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                lblDebitAmount.Value = double.Parse(ds.Tables[0].Rows[0]["Close_CreditAmount"].ToString());

                if (ds.Tables[0].Rows[0]["Close_DebitDate"] != null && ds.Tables[0].Rows[0]["Close_DebitDate"] != DBNull.Value)
                    lblDebitDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["Close_DebitDate"].ToString());

                cmbCreditCurrency.SelectedValue = ds.Tables[0].Rows[0]["Currency"].ToString();
                cmbCreditCurrency_OnSelectedIndexChanged(cmbCreditCurrency, null);
                cmbAccountPaid.SelectedValue = ds.Tables[0].Rows[0]["AccountPaid"].ToString();
                lblCreditAmount.Value = lblDebitAmount.Value;
                txtNarrative.Text = ds.Tables[0].Rows[0]["Narrative"].ToString();

                bool isautho = ds.Tables[0].Rows[0]["Close_Status"].ToString() == "AUT";
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, Request.QueryString["closeid"] == null && !isautho);
                LoadToolBar(Request.QueryString["closeid"] != null);

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

        protected void btSearch_Click(object sender, EventArgs e)
        {
            filldata(txtId.Text);
        }

        protected void cmbCreditCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbAccountPaid.Items.Clear();
            DataSet ds = BankProject.DataProvider.Database.BOPENACCOUNT_INTERNAL_GetByCode("1", lblCustomerId.Text, cmbCreditCurrency.SelectedValue, lblClosedAccount.Text);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cmbAccountPaid.Items.Add(new RadComboBoxItem("", ""));
                cmbAccountPaid.AppendDataBoundItems = true;
                cmbAccountPaid.DataTextField = "Display";
                cmbAccountPaid.DataValueField = "Id";
                cmbAccountPaid.DataSource = ds;
                cmbAccountPaid.DataBind();
            }
        }
    }
}