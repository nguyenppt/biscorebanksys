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
    public partial class NewNormalLoan : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        DataTable dtchitiet = new DataTable();
        private string refix_MACODE()
        {
            return "LD";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            LoadToolBar(false);

            DataSet dsCs = DataProvider.DataTam.BCUSTOMERS_INDIVIDUAL_GetbyID("");
            rcbCustomerID.Items.Clear();
            rcbCustomerID.Items.Add(new RadComboBoxItem(""));
            rcbCustomerID.AppendDataBoundItems = true;
            rcbCustomerID.DataSource = dsCs;
            rcbCustomerID.DataTextField = "Display";
            rcbCustomerID.DataValueField = "CustomerID";
            rcbCustomerID.DataBind();

            rcbMainCategory.Items.Clear();
            rcbMainCategory.Items.Add(new RadComboBoxItem(""));
            rcbMainCategory.AppendDataBoundItems = true;
            rcbMainCategory.DataValueField = "CatId";
            rcbMainCategory.DataTextField = "Display";
            rcbMainCategory.DataSource = SQLData.B_BRPODCATEGORY_GetAll_IdOver200();
            rcbMainCategory.DataBind();

            //rcbSubCategory.Items.Clear();
            //rcbSubCategory.Items.Add(new RadComboBoxItem(""));
            //rcbSubCategory.DataValueField = "SubCatId";
            //rcbSubCategory.DataTextField = "Display";
            //rcbSubCategory.DataSource = SQLData.B_BRPODCATEGORY_GetSubAll_IdOver200();
            //rcbSubCategory.DataBind();


            rcbPurposeCode.Items.Clear();
            rcbPurposeCode.Items.Add(new RadComboBoxItem(""));
            rcbPurposeCode.AppendDataBoundItems = true;
            rcbPurposeCode.DataValueField = "Id";
            rcbPurposeCode.DataTextField = "Name";
            rcbPurposeCode.DataSource = SQLData.B_BLOANPURPOSE_GetAll();
            rcbPurposeCode.DataBind();

            rcbLoadGroup.Items.Clear();
            rcbLoadGroup.Items.Add(new RadComboBoxItem(""));
            rcbLoadGroup.AppendDataBoundItems = true;
            rcbLoadGroup.DataValueField = "Id";
            rcbLoadGroup.DataTextField = "Name";
            rcbLoadGroup.DataSource = SQLData.B_BLOANGROUP_GetAll();
            rcbLoadGroup.DataBind();

            //rcbCreditToAccount.Items.Clear();
            //rcbCreditToAccount.Items.Add(new RadComboBoxItem(""));
            //rcbCreditToAccount.DataValueField = "Id";
            //rcbCreditToAccount.DataTextField = "Display";
            //rcbCreditToAccount.DataSource = SQLData.B_BDRFROMACCOUNT_GetAll();
            //rcbCreditToAccount.DataBind();

            //rcbPrinRepAccount.Items.Clear();
            //rcbPrinRepAccount.Items.Add(new RadComboBoxItem(""));
            //rcbPrinRepAccount.DataValueField = "Id";
            //rcbPrinRepAccount.DataTextField = "Display";
            //rcbPrinRepAccount.DataSource = SQLData.B_BDRFROMACCOUNT_GetAll();
            //rcbPrinRepAccount.DataBind();

            //rcbIntRepAccount.Items.Clear();
            //rcbIntRepAccount.Items.Add(new RadComboBoxItem(""));
            //rcbIntRepAccount.DataValueField = "Id";
            //rcbIntRepAccount.DataTextField = "Display";
            //rcbIntRepAccount.DataSource = SQLData.B_BDRFROMACCOUNT_GetAll();
            //rcbIntRepAccount.DataBind();

            //rcbChargRepAccount.Items.Clear();
            //rcbChargRepAccount.Items.Add(new RadComboBoxItem(""));
            //rcbChargRepAccount.DataValueField = "Id";
            //rcbChargRepAccount.DataTextField = "Display";
            //rcbChargRepAccount.DataSource = SQLData.B_BDRFROMACCOUNT_GetAll();
            //rcbChargRepAccount.DataBind();

            cmbAccountOfficer.Items.Clear();
            cmbAccountOfficer.Items.Add(new RadComboBoxItem(""));
            cmbAccountOfficer.AppendDataBoundItems = true;
            cmbAccountOfficer.DataValueField = "Code";
            cmbAccountOfficer.DataTextField = "description";
            cmbAccountOfficer.DataSource = SQLData.B_BACCOUNTOFFICER_GetAll();
            cmbAccountOfficer.DataBind();

            rcbInterestKey.Items.Clear();
            rcbInterestKey.Items.Add(new RadComboBoxItem(""));
            rcbInterestKey.AppendDataBoundItems = true;
            rcbInterestKey.DataValueField = "Id";
            rcbInterestKey.DataTextField = "description";
            rcbInterestKey.DataSource = SQLData.BINTEREST_TERM_GetAll();
            rcbInterestKey.DataBind();

            rcbCurrency.SelectedValue = "VND";
            rdpOpenDate.SelectedDate = DateTime.Now;
            rdpValueDate.SelectedDate = DateTime.Now;
            rcbMainCategory.Focus();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickMainTab();", true);
            if (Request.Params["codeid"] == null)
                tbNewNormalLoan.Text = SQLData.B_BMACODE_GetNewID("CRED_REVOLVING_CONTRACT", refix_MACODE(), ".");
            else
            {
                LoadToolBar(true);
                loadData(Request.Params["codeid"].ToString());
            }
        }

        private void loadData(string code)
        {
            DataSet ds = new DataSet();

            ds = BankProject.DataProvider.Database.BNEWNORMALLOAN_GetByCode(code);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                tbNewNormalLoan.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                rcbCustomerID.SelectedValue = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                rcbCurrency.SelectedValue = ds.Tables[0].Rows[0]["Currency"].ToString();
                rcbCustomerID_SelectedIndexChanged(rcbCustomerID, null);
                rcbMainCategory.SelectedValue = ds.Tables[0].Rows[0]["MainCategory"].ToString();
                LoadSubCategory();
                rcbSubCategory.SelectedValue = ds.Tables[0].Rows[0]["SubCategory"].ToString();
                rcbPurposeCode.SelectedValue = ds.Tables[0].Rows[0]["PurpostCode"].ToString();
                rcbLoadGroup.SelectedValue = ds.Tables[0].Rows[0]["LoanGroup"].ToString();
                if(ds.Tables[0].Rows[0]["LoanAmount"] != null && ds.Tables[0].Rows[0]["LoanAmount"] != DBNull.Value)
                    tbLoanAmount.Text = double.Parse(ds.Tables[0].Rows[0]["LoanAmount"].ToString()).ToString("#,##0.00");

                if (ds.Tables[0].Rows[0]["ApproveAmount"] != null && ds.Tables[0].Rows[0]["ApproveAmount"] != DBNull.Value)
                    tbApprovedAmt.Value = double.Parse(ds.Tables[0].Rows[0]["ApproveAmount"].ToString());

                if (ds.Tables[0].Rows[0]["OpenDate"] != null && ds.Tables[0].Rows[0]["OpenDate"] != DBNull.Value)
                    rdpOpenDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["OpenDate"].ToString());

                if (ds.Tables[0].Rows[0]["ValueDate"] != null && ds.Tables[0].Rows[0]["ValueDate"] != DBNull.Value)
                    rdpValueDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["ValueDate"].ToString());

                if (ds.Tables[0].Rows[0]["MaturityDate"] != null && ds.Tables[0].Rows[0]["MaturityDate"] != DBNull.Value)
                    rdpMaturityDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["MaturityDate"].ToString());

                rcbCreditToAccount.SelectedValue = ds.Tables[0].Rows[0]["CreditAccount"].ToString();
                rcbCommitmentID.Text = ds.Tables[0].Rows[0]["CommitmentID"].ToString();
                rcbLimitReference.SelectedValue = ds.Tables[0].Rows[0]["LimitReference"].ToString();
                rcbRateType.SelectedValue = ds.Tables[0].Rows[0]["RateType"].ToString();
                rcbInterestKey.SelectedValue = ds.Tables[0].Rows[0]["InterestKey"].ToString();

                if (ds.Tables[0].Rows[0]["InterestRate"] != null && ds.Tables[0].Rows[0]["InterestRate"] != DBNull.Value)
                    tbInterestRate.Value = double.Parse(ds.Tables[0].Rows[0]["InterestRate"].ToString());

                if (ds.Tables[0].Rows[0]["IntSpread"] != null && ds.Tables[0].Rows[0]["IntSpread"] != DBNull.Value && ds.Tables[0].Rows[0]["IntSpread"] != "")
                    tbInSpread.Value = double.Parse(ds.Tables[0].Rows[0]["IntSpread"].ToString());

                tbBusDayDef.Text = ds.Tables[0].Rows[0]["BusDayDef"].ToString();

                rcbAutoSch.SelectedValue = ds.Tables[0].Rows[0]["AutoSch"].ToString();
                rcbRepaySchType.SelectedValue = ds.Tables[0].Rows[0]["RepaySchType"].ToString();
                rcbPrinRepAccount.SelectedValue = ds.Tables[0].Rows[0]["PrinRepAccount"].ToString();
                rcbIntRepAccount.SelectedValue = ds.Tables[0].Rows[0]["IntRepAccount"].ToString();
                rcbChargRepAccount.SelectedValue = ds.Tables[0].Rows[0]["ChrgRepAccount"].ToString();
                tbCustomerRemarks.Text = ds.Tables[0].Rows[0]["CustomerRemarks"].ToString();
                cmbAccountOfficer.SelectedValue = ds.Tables[0].Rows[0]["AccountOfficer"].ToString();

                rcbSecured.SelectedValue = ds.Tables[0].Rows[0]["Secured"].ToString();
                rcbCollateralID.SelectedValue = ds.Tables[0].Rows[0]["CollateralID"].ToString();

                if (ds.Tables[0].Rows[0]["AmountAlloc"] != null && ds.Tables[0].Rows[0]["AmountAlloc"] != DBNull.Value)
                    rtbAmountAlloc.Value = double.Parse(ds.Tables[0].Rows[0]["AmountAlloc"].ToString());

                //tbForwardBackWard.Text = ds.Tables[0].Rows[0]["AccountOfficer"].ToString();
                //tbBaseDate.Text = ds.Tables[0].Rows[0]["AccountOfficer"].ToString();

                //rcbCreditToAccount.DataBind();
                //rcbType.SelectedValue = 1;
                //rdpDate.SelectedDate = DateTime.Now.AddYears(-2).AddDays(-23);
                //tbFreq.Text = "M";

                bool isautho = ds.Tables[0].Rows[0]["Status"].ToString() == "AUT";
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, Request.QueryString["codeid"] == null && !isautho);
                LoadToolBar(Request.QueryString["codeid"] != null);

                if (isautho)
                {
                    RadToolBar1.FindItemByValue("btnCommit").Enabled = false;
                    RadToolBar1.FindItemByValue("btnPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btnAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btnReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btnSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btnPrint").Enabled = true;
                }

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickFullTab();", true);
            }
        }

        private void LoadToolBar(bool isAut)
        {
            RadToolBar1.FindItemByValue("btnCommit").Enabled = !isAut;
            RadToolBar1.FindItemByValue("btnPreview").Enabled = !isAut;
            RadToolBar1.FindItemByValue("btnAuthorize").Enabled = isAut;
            RadToolBar1.FindItemByValue("btnReverse").Enabled = isAut;
            RadToolBar1.FindItemByValue("btnSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btnPrint").Enabled = isAut;
        }
       
        //protected void rcbCreditToAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        //{
        //    var row = e.Item.DataItem as DataRowView;
        //    e.Item.Attributes["ID"] = row["CustomerID"].ToString();
        //    e.Item.Attributes["CustomerName2"] = row["CustomerName2"].ToString();
        //}
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string normalLoan = tbNewNormalLoan.Text;
            var ToolBarButton = e.Item as RadToolBarButton;
            string commandName = ToolBarButton.CommandName;
            switch (commandName)
            {
                case "commit":
                    //RadToolBar1.FindItemByValue("btnCommit").Visible = false;
                    //RadToolBar1.FindItemByValue("btnCommit2").Visible = true;
                    //hfCommitNumber.Value = "1";
                    if (rcbAutoSch.SelectedValue == "N" && ListView1.Items.Count == 0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "test", "<script>alert('You need to schedule');</script>");
                        return;
                    }
                    else
                    {
                        if (rcbAutoSch.SelectedValue == "Y" && ListView1.Items.Count > 0)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "test", "<script>alert('The program automatically schedule');</script>");
                            return;
                        }
                        else
                        {
                            if (rcbRateType.SelectedValue == "2" && tbInSpread.Value.HasValue == false && tbInSpread.Value.Value == 0 )
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "test", "<script>alert('Int Spread is required');</script>");
                                return;
                            }
                            else
                            {
                                string MainCategory = rcbMainCategory.Text != "" ? rcbMainCategory.Text.Split('-')[1].Trim() : "";
                                string SubCategory = rcbSubCategory.Text != "" ? rcbSubCategory.Text.Split('-')[1].Trim() : "";
                                string CustomerID = rcbCustomerID.Text != "" ? rcbCustomerID.Text.Split('-')[1].Trim() : "";
                                BankProject.DataProvider.Database.BNEWNORMALLOAN_Insert(tbNewNormalLoan.Text, rcbMainCategory.SelectedValue, MainCategory, rcbSubCategory.SelectedValue, SubCategory,
                                    rcbPurposeCode.SelectedValue, rcbPurposeCode.Text, rcbCustomerID.SelectedValue, CustomerID, rcbLoadGroup.SelectedValue, rcbLoadGroup.Text, rcbCurrency.SelectedValue, tbBusDayDef.Text, "VIET NAM",
                                    tbLoanAmount.Text != "" ? double.Parse(tbLoanAmount.Text) : 0, tbApprovedAmt.Value.HasValue ? tbApprovedAmt.Value.Value : 0, rdpOpenDate.SelectedDate, null, rdpValueDate.SelectedDate,
                                    rdpMaturityDate.SelectedDate, rcbCreditToAccount.SelectedValue, rcbCommitmentID.Text, rcbLimitReference.SelectedValue, rcbRateType.SelectedValue, "366/360", rcbAnnRepMet.SelectedValue,
                                    lblINTERESTBEARING.Text, tbInterestRate.Value.HasValue ? tbInterestRate.Value.Value : 0, rcbInterestKey.SelectedValue, tbInSpread.Text, rcbAutoSch.SelectedValue, null, rcbRepaySchType.SelectedValue,
                                    lblLoanStatus.Text, txtTotalInterestAmt.Value.HasValue ? txtTotalInterestAmt.Value.Value : 0, lblPastdueStatus.Text, rcbPrinRepAccount.SelectedValue, rcbIntRepAccount.SelectedValue,
                                    rcbChargRepAccount.SelectedValue, 0, 0, tbCustomerRemarks.Text, cmbAccountOfficer.SelectedValue, cmbAccountOfficer.Text, rcbSecured.SelectedValue, rcbCollateralID.SelectedValue,
                                    rtbAmountAlloc.Value.HasValue ? rtbAmountAlloc.Value.Value : 0, "", 0, this.UserId);

                                this.Response.Redirect("Default.aspx?tabid=" + this.TabId);
                            }
                        }
                        break;
                    }
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickFullTab();", true);

                case "commit2":
                    RadToolBar1.FindItemByValue("btnPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btnAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btnReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btnCommit2").Enabled = false;
                    SetEnabledControls(false);
                    hfCommit2.Value = "1";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickMainTab();", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickFullTab();", true);
                    break;
                case "Preview":
                    this.Response.Redirect(EditUrl("preview"));
                    break;

                case "authorize":
                    BankProject.DataProvider.Database.BNEWNORMALLOAN_UpdateStatus("AUT", tbNewNormalLoan.Text, this.UserId.ToString());
                    this.Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;

                case "reverse":
                    BankProject.DataProvider.Database.BNEWNORMALLOAN_UpdateStatus("REV", tbNewNormalLoan.Text, this.UserId.ToString());
                    this.Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;

                case "search":
                    break;

                default:
                    RadToolBar1.FindItemByValue("btnCommit").Enabled = true;
                    break;
            }

            //string[] param = new string[4];
            //param[0] = "NewNormalLoan=" + normalLoan;
            //Response.Redirect(EditUrl("", "", "fullview", param));
        }

        private void SetEnabledControls(bool p)
        {
            rcbMainCategory.Enabled = p;
            rcbSubCategory.Enabled = p;
            rcbPurposeCode.Enabled = p;
            rcbCustomerID.Enabled = p;
            rcbLoadGroup.Enabled = p;
            tbLoanAmount.Enabled = p;
            rdpMaturityDate.Enabled = p;
            rcbCreditToAccount.Enabled = p;
            rcbCommitmentID.Enabled = p;
            rcbLimitReference.Enabled = p;
            rcbRateType.Enabled = p;
            rcbAnnRepMet.Enabled = p;
            tbInterestRate.Enabled = p;
            rcbInterestKey.Enabled = p;
            tbInSpread.Enabled = p;
            rcbRepaySchType.Enabled = p;
            rcbCurrency.Enabled = p;
            rdpOpenDate.Enabled = p;
            rdpValueDate.Enabled = p;
            rcbAutoSch.Enabled = p;
            tbApprovedAmt.Enabled = p;
            rcbPrinRepAccount.Enabled = p;
            rcbIntRepAccount.Enabled = p;
            tbCustomerRemarks.Enabled = p;
            cmbAccountOfficer.Enabled = p;
            rcbChargRepAccount.Enabled = p;
            tbBusDayDef.Enabled = p;
            rcbCollateralID.Enabled = p;
            rtbAmountAlloc.Enabled = p;
            rcbSecured.Enabled = p;
            tbForwardBackWard.Enabled = p;
            tbBaseDate.Enabled = p;
            ListView1.Enabled = p;
        }

        protected void rcbCustomerID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbCollateralID.Items.Clear();
            rcbCollateralID.Items.Add(new RadComboBoxItem(""));
            rcbCollateralID.AppendDataBoundItems = true;
            rcbCollateralID.DataValueField = "RightID";
            rcbCollateralID.DataTextField = "Name";
            rcbCollateralID.DataSource = SQLData.BCOLLATERALRIGHT_GetByCustomer(rcbCustomerID.SelectedValue);
            rcbCollateralID.DataBind();

            rcbLimitReference.Items.Clear();
            rcbLimitReference.Items.Add(new RadComboBoxItem(""));
            rcbLimitReference.AppendDataBoundItems = true;
            rcbLimitReference.DataValueField = "SubLimitID";
            rcbLimitReference.DataTextField = "Display";
            rcbLimitReference.DataSource = SQLData.B_CUSTOMER_LIMIT_SUB_GetByCustomer(rcbCustomerID.SelectedValue);
            rcbLimitReference.DataBind();

            
            loadAllAccount();
        }

        protected void rcbCurrency_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            loadAllAccount();
        }

        protected void rcbMainCategory_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSubCategory();
        }

        void loadAllAccount()
        {
            string name = rcbCustomerID.SelectedValue;
            string currency = rcbCurrency.SelectedValue;

            rcbCreditToAccount.Items.Clear();
            rcbIntRepAccount.Items.Clear();
            rcbPrinRepAccount.Items.Clear();
            rcbChargRepAccount.Items.Clear();
            if (name != "" && currency != "")
            {
                DataSet ds = Database.BOPENACCOUNT_LOANACCOUNT_GetByCode(name, currency);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rcbCreditToAccount.Items.Add(new RadComboBoxItem(""));
                    rcbCreditToAccount.DataSource = ds;
                    rcbCreditToAccount.DataValueField = "Id";
                    rcbCreditToAccount.DataTextField = "Display";
                    rcbCreditToAccount.DataBind();

                    rcbPrinRepAccount.Items.Add(new RadComboBoxItem(""));
                    rcbPrinRepAccount.DataValueField = "Id";
                    rcbPrinRepAccount.DataTextField = "Display";
                    rcbPrinRepAccount.DataSource = ds;
                    rcbPrinRepAccount.DataBind();

                    rcbIntRepAccount.Items.Add(new RadComboBoxItem(""));
                    rcbIntRepAccount.DataSource = ds;
                    rcbIntRepAccount.DataValueField = "Id";
                    rcbIntRepAccount.DataTextField = "Display";
                    rcbIntRepAccount.DataBind();

                    rcbChargRepAccount.Items.Add(new RadComboBoxItem(""));
                    rcbChargRepAccount.DataSource = ds;
                    rcbChargRepAccount.DataValueField = "Id";
                    rcbChargRepAccount.DataTextField = "Display";
                    rcbChargRepAccount.DataBind();
                }
            }

        }

        private void LoadSubCategory()
        {
            string id = rcbMainCategory.SelectedValue;

            rcbSubCategory.Items.Clear();
            rcbSubCategory.Items.Add(new RadComboBoxItem(""));
            rcbSubCategory.DataValueField = "SubCatId";
            rcbSubCategory.DataTextField = "Display";
            rcbSubCategory.DataSource = SQLData.B_BRPODCATEGORY_GetSubAll_IdOver200(id);
            rcbSubCategory.DataBind();
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            loadData(tbNewNormalLoan.Text);
        }
    }
}
