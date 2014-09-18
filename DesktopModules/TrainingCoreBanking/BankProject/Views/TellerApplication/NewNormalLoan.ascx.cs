using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BankProject.DataProvider;
using BankProject.Entity;
using System.Collections;
using BankProject.DBRespository;
using BankProject.DBContext;
using BankProject.Common;

namespace BankProject.Views.TellerApplication
{
    public partial class NewNormalLoan : DotNetNuke.Entities.Modules.PortalModuleBase
    {

        DataTable dtchitiet = new DataTable();
        private string REFIX_MACODE = "LD";
        BNEWNORMALLOAN normalLoanEntryM;
        bool isEdit = false;
        bool isAmendPage = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["tabid"] != null)
            {
                if (Request.Params["tabid"] == "202")
                {
                    isAmendPage = true;
                    isEdit = true;
                }
            }
            if (Request.Params["codeid"] == null)
            {
                isEdit = false;
            }
            else
            {
                isEdit = true;
            }

            

            if (IsPostBack) return;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickMainTab();", true);
            init();

        }

        #region Common Function
        private void init()
        {
            LoadToolBar(false);

            LoadMainCategoryCombobox(null);
            LoadCustomerCombobox(null);
            LoadPurposeCode(null);
            LoadGroup(null);
            LoadAccountOfficer(null);
            LoadInterestKey(null);
            InitDefaultData();



            if (!isEdit)
            {
                isEdit = false;
                //init new Loan.
                normalLoanEntryM = new BNEWNORMALLOAN();
                //normalLoanEntryM.Code = "1242131";
                if (!isAmendPage)
                {
                    normalLoanEntryM.Code = generateCode();
                }
                else
                {
                    
                }
                BindData2Field(normalLoanEntryM);
            }
            else
            {
                isEdit = true;
                LoadToolBar(true);
                //Load data incase it is already exist.
                LoadExistData(Request.Params["codeid"].ToString());
                if (isAmendPage)
                {
                    SetEnabledControls(true);
                }
                else
                {
                    SetEnabledControls(false);
                }
            }

            LoadDataTolvLoanControl();


        }
        private void InitDefaultData()
        {
            rcbCurrency.SelectedValue = "VND";
            rdpOpenDate.SelectedDate = DateTime.Now;
            rdpValueDate.SelectedDate = DateTime.Now;
            radcbMainCategory.Focus();
        }
        private void LoadInterestKey(string selectedid)
        {

            BInterestTermRepository facade = new BInterestTermRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(rcbInterestKey, src, "Id", "description", "-Select Interest Key-");

            if (!String.IsNullOrEmpty(selectedid))
            {
                rcbInterestKey.SelectedValue = selectedid;
            }
        }
        private void LoadAccountOfficer(string selectedid)
        {
            AccountOfficerRepository facade = new AccountOfficerRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(cmbAccountOfficer, src, "Code", "description", "-Select Account Officer-");

            if (!String.IsNullOrEmpty(selectedid))
            {
                cmbAccountOfficer.SelectedValue = selectedid;
            }
        }
        private void LoadGroup(string selectedvalue)
        {
            LoanGroupRepository facade = new LoanGroupRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(rcbLoadGroup, src, "Id", "Name", "-Select Load Group-");

            if (!String.IsNullOrEmpty(selectedvalue))
            {
                rcbLoadGroup.SelectedValue = selectedvalue;
            }
        }
        private void LoadPurposeCode(string selectedvalue)
        {
            LoanPurposeRepository facade = new LoanPurposeRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(rcbPurposeCode, src, "Id", "Name", "-Select Purpose Code-");

            if (!String.IsNullOrEmpty(selectedvalue))
            {
                rcbPurposeCode.SelectedValue = selectedvalue;
            }
        }
        private void LoadMainCategoryCombobox(string selectedItem)
        {
            StoreProRepository storeFacade = new StoreProRepository();
            var db = storeFacade.StoreProcessor().B_BRPODCATEGORY_GetAll_IdOver200().ToList();
            Util.LoadData2RadCombo(radcbMainCategory, db, "CatId", "Display", "-Select Main Category-");
            radcbMainCategory.SelectedValue = selectedItem;

        }
        private void LoadCustomerCombobox(string SelectedCus)
        {
            BCustomerRepository facade1 = new BCustomerRepository();
            var db = facade1.getCustomerList("AUT").ToList();
            Util.LoadData2RadCombo(rcbCustomerID, db, "CustomerID", "GBFullName", "-Select Customer Code-");


            if (!String.IsNullOrEmpty(SelectedCus))
            {
                rcbCustomerID.SelectedValue = SelectedCus;
            }

        }

        private void LoadLimitReferenceInfor(string custId, string selectedvalue)
        {

            CustomerLimitSubRepository facade = new CustomerLimitSubRepository();
            var src = facade.FindLimitCusSub(custId).ToList();
            Util.LoadData2RadCombo(rcbLimitReference, src, "SubLimitID", "SubLimitID", "-Select Limit Refer-");


            if (!String.IsNullOrEmpty(selectedvalue))
            {
                rcbLimitReference.SelectedValue = selectedvalue;
            }
        }

        private void LoadCollareralID(string custId, string selectedValue)
        {

            CollorateRightRepository facade = new CollorateRightRepository();
            var src = facade.FindCollorateRightByCust(custId).ToList();
            Util.LoadData2RadCombo(rcbCollateralID, src, "RightID", "RightID", "-Select Collateral ID-");

            if (!String.IsNullOrEmpty(selectedValue))
            {
                rcbCollateralID.SelectedValue = selectedValue;
            }
        }
        void LoadAllAccount(string custID, string currency, string credit, string printRep, string inRep, string chagreRep)
        {

            StoreProRepository storeFacade = new StoreProRepository();
            var ds = storeFacade.StoreProcessor().BOPENACCOUNT_LOANACCOUNT_GetByCode(custID, currency).ToList();


            //Database.BOPENACCOUNT_LOANACCOUNT_GetByCode(name, currency);
            Util.LoadData2RadCombo(rcbCreditToAccount, ds, "Id", "Display", "-Select a credit Account-");
            Util.LoadData2RadCombo(rcbPrinRepAccount, ds, "Id", "Display", "-Select a Print Rep Account-");
            Util.LoadData2RadCombo(rcbIntRepAccount, ds, "Id", "Display", "-Select a Int Rep Account-");
            Util.LoadData2RadCombo(rcbChargRepAccount, ds, "Id", "Display", "-Select a Charge Rep Account-");
            if (!String.IsNullOrEmpty(credit))
            {
                rcbCreditToAccount.SelectedValue = credit;
            }

            if (!String.IsNullOrEmpty(printRep))
            {
                rcbPrinRepAccount.SelectedValue = printRep;
            }

            if (!String.IsNullOrEmpty(inRep))
            {
                rcbIntRepAccount.SelectedValue = inRep;
            }

            if (!String.IsNullOrEmpty(chagreRep))
            {
                rcbChargRepAccount.SelectedValue = chagreRep;
            }

        }

        private void LoadDataTolvLoanControl()
        {
            NewLoanControlRepository facade = new NewLoanControlRepository();
            var db = facade.FindLoanControlByCode(tbNewNormalLoan.Text);
            lvLoanControl.DataSource = db.ToList();
            lvLoanControl.DataBind();
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

        private void CommitData(BNEWNORMALLOAN normalLoanEntry)
        {
            if (isEdit || isAmendPage)
            {
                UpdateExitingData(normalLoanEntry);
            }
            else
            {
                CommitDataCreateNew(normalLoanEntry);
            }
        }

        private void CommitDataCreateNew(BNEWNORMALLOAN normalLoanEntry)
        {
            if (normalLoanEntry != null && !String.IsNullOrEmpty(normalLoanEntry.Code))
            {
                NormalLoanRepository facade = new NormalLoanRepository();
                normalLoanEntry.CreateDate = facade.GetSystemDatetime();
                normalLoanEntry.CreateBy = this.UserId;
                normalLoanEntry.Status = "UNA";
                facade.Add(normalLoanEntry);
                facade.Commit();
            }
            else
            {
                lblMessage.Text = "Cannot commit new Loan";
            }

        }

        private void UpdateExitingData(BNEWNORMALLOAN normalLoanEntry)
        {
            NormalLoanRepository facade = new NormalLoanRepository();
            normalLoanEntry.UpdatedDate = facade.GetSystemDatetime();
            if (isAmendPage)
            {

            }
            else
            {
                normalLoanEntry.UpdatedBy = this.UserId;
            }
            
            facade.Update(facade.GetById(normalLoanEntry.Code),normalLoanEntry);
            facade.Commit();
        }
        private void BindField2Data(ref BNEWNORMALLOAN normalLoanEntry)
        {

            if (normalLoanEntry == null)
            {
                normalLoanEntry = new BNEWNORMALLOAN();
            }
            
            NormalLoanRepository facase = new NormalLoanRepository();
            if (isEdit || isAmendPage)
            {
                normalLoanEntry = facase.GetById(tbNewNormalLoan.Text);
            }


            normalLoanEntry.Code = tbNewNormalLoan.Text;
            normalLoanEntry.MainCategory = radcbMainCategory.SelectedValue;
            normalLoanEntry.MainCategoryName = radcbMainCategory.Text;
            normalLoanEntry.SubCategory = rcbSubCategory.SelectedValue;
            normalLoanEntry.SubCategoryName = rcbSubCategory.Text;
            normalLoanEntry.PurpostCode = rcbPurposeCode.SelectedValue;
            normalLoanEntry.PurpostName = rcbPurposeCode.Text;
            normalLoanEntry.CustomerID = rcbCustomerID.SelectedValue;
            normalLoanEntry.CustomerName = rcbCustomerID.Text;
            normalLoanEntry.LoanGroup = rcbLoadGroup.SelectedValue;
            normalLoanEntry.LoanGroupName = rcbLoadGroup.Text;
            normalLoanEntry.Currency = rcbCurrency.SelectedValue;
            normalLoanEntry.BusDayDef = tbBusDayDef.Text;
            normalLoanEntry.BusDayDefName = "VIET NAM";

            normalLoanEntry.LoanAmount = tbLoanAmount.Text != "" ? decimal.Parse(tbLoanAmount.Text) : 0;
            normalLoanEntry.ApproveAmount = tbApprovedAmt.Value.HasValue ? (decimal)tbApprovedAmt.Value.Value : 0;
            normalLoanEntry.OpenDate = rdpOpenDate.SelectedDate;
            normalLoanEntry.ValueDate = rdpValueDate.SelectedDate;

            normalLoanEntry.MaturityDate = rdpMaturityDate.SelectedDate;
            normalLoanEntry.CreditAccount = rcbCreditToAccount.SelectedValue;
            normalLoanEntry.CommitmentID = rcbCommitmentID.Text;
            normalLoanEntry.LimitReference = rcbLimitReference.SelectedValue;
            normalLoanEntry.RateType = rcbRateType.SelectedValue;
            normalLoanEntry.InterestBasic = "366/360";
            normalLoanEntry.AnnuityRepMet = rcbAnnRepMet.SelectedValue;


            normalLoanEntry.InterestKey = rcbInterestKey.SelectedValue;
            normalLoanEntry.IntSpread = tbInSpread.Text;
            normalLoanEntry.AutoSch = rcbAutoSch.SelectedValue;
            normalLoanEntry.RepaySchType = rcbRepaySchType.SelectedValue;
            normalLoanEntry.Drawdown = rdpDrawdown.SelectedDate;

            normalLoanEntry.ChrgRepAccount = rcbChargRepAccount.SelectedValue;
            normalLoanEntry.ExpectedLoss = (decimal?)tbExpectedLoss.Value;
            normalLoanEntry.LossGivenDef = (decimal?)tbLossGiven.Value;
            normalLoanEntry.CustomerRemarks = tbCustomerRemarks.Text;
            normalLoanEntry.AccountOfficer = cmbAccountOfficer.SelectedValue;
            normalLoanEntry.AccountOfficerName = cmbAccountOfficer.Text;
            normalLoanEntry.Secured = rcbSecured.SelectedValue;
            normalLoanEntry.CollateralID = rcbCollateralID.SelectedValue;
            normalLoanEntry.DefineSch = rcbDefineSch.SelectedValue;
            normalLoanEntry.AmountAlloc = rtbAmountAlloc.Value.HasValue ? (decimal)rtbAmountAlloc.Value.Value : 0;
            normalLoanEntry.IntPayMethod = lblIntPayMethod.Text;
            normalLoanEntry.InterestRate = (decimal?)tbInterestRate.Value;
            normalLoanEntry.PrinRepAccount = rcbPrinRepAccount.SelectedValue;
            normalLoanEntry.IntRepAccount = rcbIntRepAccount.SelectedValue;
            normalLoanEntry.ChrgRepAccount = rcbChargRepAccount.SelectedValue;
            normalLoanEntry.LoanStatus = lbLoanStatus.Text;
            normalLoanEntry.TotalInterestAmt = !String.IsNullOrEmpty(lbTotalInterestAmt.Text) ? Decimal.Parse(lbTotalInterestAmt.Text) : 0;
            normalLoanEntry.PDStatus = lbPDStatus.Text;
        }
        private void BindData2Field(BNEWNORMALLOAN normalLoanEntry)
        {
            if (normalLoanEntry == null)
            {
                return;
            }
            tbNewNormalLoan.Text = normalLoanEntry.Code;
            rcbCustomerID.SelectedValue = normalLoanEntry.CustomerID;
            rcbCurrency.SelectedValue = normalLoanEntry.Currency;
            radcbMainCategory.SelectedValue = normalLoanEntry.MainCategory;
            LoadSubCategory(normalLoanEntry.MainCategory, normalLoanEntry.SubCategory);
            rcbPurposeCode.SelectedValue = normalLoanEntry.PurpostCode;
            rcbLoadGroup.SelectedValue = normalLoanEntry.LoanGroup;
            tbLoanAmount.Text = normalLoanEntry.LoanAmount.ToString();
            tbApprovedAmt.Value = (double?)normalLoanEntry.ApproveAmount;
            rdpOpenDate.SelectedDate = normalLoanEntry.OpenDate;
            rdpValueDate.SelectedDate = normalLoanEntry.ValueDate;
            rdpDrawdown.SelectedDate = normalLoanEntry.Drawdown;
            rdpMaturityDate.SelectedDate = normalLoanEntry.MaturityDate;
            rcbCommitmentID.SelectedValue = normalLoanEntry.CommitmentID;
            LoadLimitReferenceInfor(normalLoanEntry.CustomerID, normalLoanEntry.LimitReference);
            LoadAllAccount(normalLoanEntry.CustomerID, normalLoanEntry.Currency,
                normalLoanEntry.CreditAccount, normalLoanEntry.PrinRepAccount,
                normalLoanEntry.IntRepAccount, normalLoanEntry.ChrgRepAccount);
            LoadCollareralID(normalLoanEntry.CustomerID, normalLoanEntry.CollateralID);
            rcbRateType.SelectedValue = normalLoanEntry.RateType;
            rcbInterestKey.SelectedValue = normalLoanEntry.InterestKey;
            tbInterestRate.Value = (double?)normalLoanEntry.InterestRate;
            tbInSpread.Value = double.Parse(!String.IsNullOrEmpty(normalLoanEntry.IntSpread) ? normalLoanEntry.IntSpread : "0");
            tbBusDayDef.Text = normalLoanEntry.BusDayDef;
            rcbAutoSch.SelectedValue = normalLoanEntry.AutoSch;
            rcbDefineSch.SelectedValue = normalLoanEntry.DefineSch;
            rcbRepaySchType.SelectedValue = normalLoanEntry.RepaySchType;
            tbCustomerRemarks.Text = normalLoanEntry.CustomerRemarks;
            cmbAccountOfficer.SelectedValue = normalLoanEntry.AccountOfficer;
            rcbSecured.SelectedValue = normalLoanEntry.Secured;
            tbExpectedLoss.Value = (double?)normalLoanEntry.ExpectedLoss;
            tbLossGiven.Value = (double?)normalLoanEntry.LossGivenDef;
            rtbAmountAlloc.Value = (double?)normalLoanEntry.AmountAlloc;
            rcbAnnRepMet.SelectedValue = normalLoanEntry.AnnuityRepMet;
            lbLoanStatus.Text = normalLoanEntry.LoanStatus;
            lbTotalInterestAmt.Text = "" + normalLoanEntry.TotalInterestAmt;
            lbPDStatus.Text = normalLoanEntry.PDStatus;
        }




        private void LoadSubCategory(string categoryid, string selectedValue)
        {
            ProductCategoryRepository facade = new ProductCategoryRepository();
            var model = facade.getProductCategory(categoryid).ToList();
            Util.LoadData2RadCombo(rcbSubCategory, model, "SubCatId", "SubCatName", "-Select a Sub Category-");


            if (!String.IsNullOrEmpty(selectedValue))
            {
                rcbSubCategory.SelectedValue = selectedValue;
            }


            //rcbSubCategory.DataBind();
        }
        #endregion

        #region Process for Add New
        /**
            * Genertate bar code
            */
        private string generateCode()
        {
            VietVictoryCoreBankingEntities bd = new VietVictoryCoreBankingEntities();
            StoreProRepository storePro = new StoreProRepository();
            return storePro.StoreProcessor().B_BMACODE_GetNewID("CRED_REVOLVING_CONTRACT", REFIX_MACODE, ".").First<string>();
        }


        #endregion

        #region Process for Modify

        private void LoadExistData(string code)
        {
       
            NormalLoanRepository facde = new NormalLoanRepository();
            var i = isAmendPage?facde.findCustomerCodeAUT(code):facde.findCustomerCode(code);
            foreach (BNEWNORMALLOAN a in i)
            {
                normalLoanEntryM = a;
                processPermissionOnItem(normalLoanEntryM.Status);
                BindData2Field(normalLoanEntryM);
            }


            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickFullTab();", true);
        }

        private void processPermissionOnItem(string itemStatus)
        {
            if (String.IsNullOrEmpty(itemStatus))
            {
                UpdateStatusAuthorizeAction(true, false, false, false, true, false);
                return;
            }

            if (itemStatus.Equals("AUT"))
            {
                if (isAmendPage)
                {
                    UpdateStatusAuthorizeAction(true, true, true, false, false, true);
                }
                else
                {
                    UpdateStatusAuthorizeAction(false, true, false, false, false, true);
                }
                return;
            }

            if (itemStatus.Equals("UAT"))
            {
                UpdateStatusAuthorizeAction(true, true, false, false, false, true);
                return;
            }

        }

        private void UpdateStatusAuthorizeAction(bool allowCommit, bool allowPreview, bool allowAuthorize,
            bool allowReverse, bool allowSearch, bool allowPrint)
        {
            RadToolBar1.FindItemByValue("btnCommit").Enabled = allowCommit;
            RadToolBar1.FindItemByValue("btnPreview").Enabled = allowPreview;
            RadToolBar1.FindItemByValue("btnAuthorize").Enabled = allowAuthorize;
            RadToolBar1.FindItemByValue("btnReverse").Enabled = allowReverse;
            RadToolBar1.FindItemByValue("btnSearch").Enabled = allowSearch;
            RadToolBar1.FindItemByValue("btnPrint").Enabled = allowPrint;
        }




        #endregion

        #region Events
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string normalLoan = tbNewNormalLoan.Text;
            var ToolBarButton = e.Item as RadToolBarButton;
            string commandName = ToolBarButton.CommandName;
            switch (commandName)
            {
                case "commit":
                    if (rcbAutoSch.SelectedValue == "N" && lvLoanControl.Items.Count == 0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "test", "<script>alert('You need to schedule');</script>");
                        return;
                    }
                    else
                    {
                        if (rcbAutoSch.SelectedValue == "Y" && lvLoanControl.Items.Count > 0)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "test", "<script>alert('The program automatically schedule');</script>");
                            return;
                        }
                        else
                        {
                            if (rcbRateType.SelectedValue == "2" && tbInSpread.Value.HasValue == false && tbInSpread.Value.Value == 0)
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "test", "<script>alert('Int Spread is required');</script>");
                                return;
                            }
                            else
                            {

                                BindField2Data(ref normalLoanEntryM);
                                //Commit Data
                                if (isAmendPage)
                                {
                                    normalLoanEntryM.AmendedBy = this.UserId;
                                    normalLoanEntryM.Amend_UpdatedDate = DateTime.Today;
                                    normalLoanEntryM.Amend_Status = "UNA";
                                }
                                
                                CommitData(normalLoanEntryM);

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
                    //BankProject.DataProvider.Database.BNEWNORMALLOAN_UpdateStatus("AUT", tbNewNormalLoan.Text, this.UserId.ToString());
                    BindField2Data(ref normalLoanEntryM);
                    if (isAmendPage)
                    {
                        normalLoanEntryM.Amend_AuthorizedDate = DateTime.Today;
                        normalLoanEntryM.Amend_AuthorizedBy = this.UserId;
                        normalLoanEntryM.Amend_Status = "AUT";
                    }
                    else
                    {
                        normalLoanEntryM.Status = "AUT";
                        normalLoanEntryM.AuthorizedBy = this.UserId;
                        normalLoanEntryM.AuthorizedDate = DateTime.Today;
                       
                    }

                    //normalLoanEntryM.AmendedBy
                    //Commit Data
                    CommitData(normalLoanEntryM);

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
        protected void btSearch_Click(object sender, EventArgs e)
        {
            LoadExistData(tbNewNormalLoan.Text);
        }
        private void SetEnabledControls(bool p)
        {
            radcbMainCategory.Enabled = p;
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
            lvLoanControl.Enabled = p;
        }

        protected void radcbMainCategory_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void rcbCustomerID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadCollareralID(rcbCustomerID.SelectedValue, null);
            LoadLimitReferenceInfor(rcbCustomerID.SelectedValue, null);
            LoadAllAccount(rcbCustomerID.SelectedValue, rcbCurrency.SelectedValue, null, null, null, null);
        }


        protected void rcbCurrency_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadAllAccount(rcbCustomerID.SelectedValue, rcbCurrency.SelectedValue, null, null, null, null);
        }
        protected void Radcbmaincategory_Selectedindexchanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSubCategory(radcbMainCategory.SelectedValue, null);
        }




        #endregion

        protected void lvLoanControl_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            LoadDataTolvLoanControl();
        }

        protected void lvLoanControl_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            String ids = "";
            Label lbl = (lvLoanControl.Items[e.ItemIndex].FindControl("lbID")) as Label;
            if (lbl != null)
                ids = lbl.Text;

            if (!String.IsNullOrEmpty(ids))
            {
                NewLoanControlRepository facade = new NewLoanControlRepository();
                var itm = facade.GetById(Int16.Parse(ids));
                if (itm != null)
                {
                    facade.Delete(itm);
                    facade.Commit();
                    LoadDataTolvLoanControl();
                }

            }
        }

        protected void lvLoanControl_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            DropDownList type = (lvLoanControl.InsertItem.FindControl("TypeTextBox")) as DropDownList;
            RadDatePicker date = (lvLoanControl.InsertItem.FindControl("DateTextBox")) as RadDatePicker;
            RadNumericTextBox amountAction = (lvLoanControl.InsertItem.FindControl("AmountActionTextBox")) as RadNumericTextBox;
            RadNumericTextBox rate = (lvLoanControl.InsertItem.FindControl("RateTextBox")) as RadNumericTextBox;
            RadNumericTextBox notext = (lvLoanControl.InsertItem.FindControl("NoTextBox")) as RadNumericTextBox;
            TextBox freq = (lvLoanControl.InsertItem.FindControl("FreqTextBox")) as TextBox;

            BNewLoanControl item = new BNewLoanControl();
            item.Type = type.SelectedValue;
            item.Date = date.SelectedDate;
            item.AmountAction = String.IsNullOrEmpty(amountAction.Text) ? 0 : Double.Parse(amountAction.Text);
            item.Rate = String.IsNullOrEmpty(rate.Text) ? 0 : Double.Parse(rate.Text);
            item.No = String.IsNullOrEmpty(notext.Text) ? 0 : Double.Parse(notext.Text);
            item.Freq = freq.Text;
            item.Code = tbNewNormalLoan.Text;
            NewLoanControlRepository facade = new NewLoanControlRepository();
            facade.Add(item);
            facade.Commit();
            LoadDataTolvLoanControl();

        }

        protected void tbNewNormalLoan_TextChanged(object sender, EventArgs e)
        {
            if (isAmendPage)
            {
                LoadExistData(tbNewNormalLoan.Text);
            }
        }
    }
}
