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
using BankProject.Business;



namespace BankProject.Views.TellerApplication
{
    public partial class NewNormalLoan : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        INewNormalLoanBusiness<BNEWNORMALLOAN> loanBusiness;
        BNEWNORMALLOAN normalLoanEntryM;
        DataTable dtchitiet = new DataTable();
        bool isApprovalRole = false;

        private string REFIX_MACODE = "LD";
        bool isEdit = false;
        bool isAmendPage = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["role"] != null)
            {
                if (Request.Params["role"].Equals("authorize"))
                {
                    isApprovalRole = true;
                }
            }

            if (Request.Params["tabid"] != null)
            {
                if (Request.Params["tabid"] == "202")
                {
                    isAmendPage = true;
                    isEdit = true;
                    loanBusiness = new NewNormalLoanAmendBusiness();
                    if (!IsPostBack)
                    {
                        if (Request.Params["codeid"] != null)
                        {
                            tbNewNormalLoan.Text = Request.Params["codeid"];
                        }
                        normalLoanEntryM = new BNEWNORMALLOAN();
                        normalLoanEntryM.Code = tbNewNormalLoan.Text;
                        init();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickMainTab();", true);
                    }

                }
                else
                {
                    loanBusiness = new NewNormalLoanBusiness();
                    if (!IsPostBack)
                    {
                        if (Request.Params["codeid"] == null)
                        {
                            tbNewNormalLoan.Text = generateCode();
                        }
                        else
                        {
                            tbNewNormalLoan.Text = Request.Params["codeid"];
                        }
                        normalLoanEntryM = new BNEWNORMALLOAN();
                        normalLoanEntryM.Code = tbNewNormalLoan.Text;
                        init();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickMainTab();", true);
                    }
                }
            }

        }

        #region Events
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string normalLoan = tbNewNormalLoan.Text;
            var ToolBarButton = e.Item as RadToolBarButton;
            string commandName = ToolBarButton.CommandName;
            switch (commandName)
            {
                case "commit":
                    BindField2Data(ref normalLoanEntryM);
                    loanBusiness.Entity = normalLoanEntryM;
                    loanBusiness.commitProcess(this.UserId);

                    this.Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;

                case "commit2":
                    //RadToolBar1.FindItemByValue("btnPreview").Enabled = true;
                    //RadToolBar1.FindItemByValue("btnAuthorize").Enabled = false;
                    //RadToolBar1.FindItemByValue("btnReverse").Enabled = false;
                    //RadToolBar1.FindItemByValue("btnCommit2").Enabled = false;
                    //SetEnabledControls(false);
                    //hfCommit2.Value = "1";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickMainTab();", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickFullTab();", true);
                    break;
                case "Preview":
                    this.Response.Redirect(EditUrl("preview"));
                    break;

                case "authorize":
                    BindField2Data(ref normalLoanEntryM);
                    loanBusiness.Entity = normalLoanEntryM;
                    loanBusiness.authorizeProcess(this.UserId);

                    this.Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;

                case "reverse":
                    BindField2Data(ref normalLoanEntryM);
                    loanBusiness.Entity = normalLoanEntryM;
                    loanBusiness.revertProcess(this.UserId);
                    this.Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
                case "search":
                    this.Response.Redirect(EditUrl("preview"));
                    break;
                case "print":
                    PrintLoanDocument();
                    break;
                default:
                    RadToolBar1.FindItemByValue("btnCommit").Enabled = true;
                    break;
            }

        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            //LoadExistData(tbNewNormalLoan.Text);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickFullTab();", true);
        }


        protected void rcbCustomerID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadCollareralID(rcbCustomerID.SelectedValue, null, null, null, null);
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

        protected void lvLoanControl_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lvLoanControl.EditIndex = -1;
            LoadDataTolvLoanControl();
        }

        protected void lvLoanControl_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvLoanControl.EditIndex = e.NewEditIndex;
            LoadDataTolvLoanControl();
        }

        protected void lvLoanControl_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            DropDownList type = (lvLoanControl.EditItem.FindControl("TypeTextBox")) as DropDownList;
            RadDatePicker date = (lvLoanControl.EditItem.FindControl("DateTextBox")) as RadDatePicker;
            RadNumericTextBox amountAction = (lvLoanControl.EditItem.FindControl("AmountActionTextBox")) as RadNumericTextBox;
            RadNumericTextBox rate = (lvLoanControl.EditItem.FindControl("RateTextBox")) as RadNumericTextBox;
            RadNumericTextBox notext = (lvLoanControl.EditItem.FindControl("NoTextBox")) as RadNumericTextBox;
            TextBox freq = (lvLoanControl.EditItem.FindControl("FreqTextBox")) as TextBox;
            Label lbID = (lvLoanControl.EditItem.FindControl("lbID")) as Label;

            BNewLoanControl item = new BNewLoanControl();
            item.Type = type.SelectedValue;
            item.Date = date.SelectedDate;
            item.AmountAction = String.IsNullOrEmpty(amountAction.Text) ? 0 : Double.Parse(amountAction.Text);
            item.Rate = String.IsNullOrEmpty(rate.Text) ? 0 : Double.Parse(rate.Text);
            item.No = String.IsNullOrEmpty(notext.Text) ? 0 : Double.Parse(notext.Text);
            item.Freq = freq.Text;
            item.Code = tbNewNormalLoan.Text;
            item.ID = Int32.Parse(lbID.Text);
            NewLoanControlRepository facade = new NewLoanControlRepository();
            BNewLoanControl exits = facade.GetById(item.ID);
            if (exits != null)
            {
                facade.Update(facade.GetById(item.ID), item);
                facade.Commit();
            }
            lvLoanControl.EditIndex = -1;
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

            normalLoanEntryM = new BNEWNORMALLOAN();
            normalLoanEntryM.Code = tbNewNormalLoan.Text;
            loanBusiness.loadEntity(ref normalLoanEntryM);
            BindData2Field(normalLoanEntryM);
            LoadDataTolvLoanControl();
            processApproriateAction();
        }


        #endregion



        #region Common Function
        private void processApproriateAction()
        {
            if (normalLoanEntryM == null || String.IsNullOrEmpty(normalLoanEntryM.Code))
            {
                UpdateStatusAuthorizeAction(false, true, false, false, false, false);
                SetEnabledControls(false);
                return;
            }

            UpdateStatusAuthorizeAction(false, false, false, false, false, false);

            bool isUnAuthorize = (normalLoanEntryM.Status == null)
                || "UNA".Equals(normalLoanEntryM.Status)
                || "REV".Equals(normalLoanEntryM.Status);
            bool isUnauthorizeAmend = (normalLoanEntryM.Amend_Status == null)
                || "UNA".Equals(normalLoanEntryM.Amend_Status)
                || "REV".Equals(normalLoanEntryM.Amend_Status);
            bool isReverse = "REV".Equals(normalLoanEntryM.Amend_Status);

            bool isAuthorize = "AUT".Equals(normalLoanEntryM.Status);
            bool isAuthorizeAmend = "AUT".Equals(normalLoanEntryM.Amend_Status);

            //process Unauthorize
            if (isAmendPage)
            {
                if (!isAuthorize)//Khong du dieu kien de Amend
                {

                    UpdateStatusAuthorizeAction(false, true, false, false, false, false);
                }
                else//du dieu kien de amend
                {
                    if (isAuthorizeAmend) //amend da duoc authorize
                    {
                        //approval user
                        if (isApprovalRole)
                        {
                            UpdateStatusAuthorizeAction(false, true, false, false, false, false);
                        }
                        else//normal user
                        {
                            UpdateStatusAuthorizeAction(true, true, false, false, false, false);
                        }


                    }
                    else // chua authorize hoac dang cho authorize
                    {
                        if (isApprovalRole)
                        {
                            UpdateStatusAuthorizeAction(false, true, true, true, false, true);
                        }
                        else
                        {
                            UpdateStatusAuthorizeAction(true, true, false, false, false, false);
                        }
                    }
                }

            }
            else //input page
            {
                if (isAuthorize)//trang thai authorize
                {
                    UpdateStatusAuthorizeAction(false, true, false, false, false, false);
                }
                else if (isReverse)//trang thai reserve
                {
                    UpdateStatusAuthorizeAction(true, true, false, false, false, false);
                }
                else //trang thai cho authorize
                {
                    if (isApprovalRole)//approval guy
                    {
                        UpdateStatusAuthorizeAction(false, true, true, true, false, true);
                    }
                    else//normal user
                    {
                        UpdateStatusAuthorizeAction(true, true, false, false, false, false);
                    }
                }
            }

        }
        private void init()
        {
            LoadMainCategoryCombobox(null);
            LoadCustomerCombobox(null);
            LoadPurposeCode(null);
            LoadGroup(null);
            LoadAccountOfficer(null);
            LoadInterestKey(null);
            //InitDefaultData();

            //Load data and binding it to UI
            loanBusiness.loadEntity(ref normalLoanEntryM);
            BindData2Field(normalLoanEntryM);
            LoadDataTolvLoanControl();
            //deside which action should be taken care
            InitDefaultData();
            processApproriateAction();

        }
        private void InitDefaultData()
        {
            //rcbCurrency.SelectedValue = "VND";
            //rdpOpenDate.SelectedDate = DateTime.Now;
            //rdpValueDate.SelectedDate = DateTime.Now;
            //rdpMaturityDate.SelectedDate = DateTime.Now;
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
        private void LoadCollareralID(string custId, string selectedValue1
            , string selectedValue2, string selectedValue3, string selectedValue4)
        {

            CollateralInformationRepository facade = new CollateralInformationRepository();
            //var src = facade.FindCollorateRightByCust(custId).ToList();
            var src = facade.FindCollorateInformationByCust(custId).ToList();
            Util.LoadData2RadCombo(rcbCollateralID, src, "RightID", "RightID", "-Select Collateral ID-");
            Util.LoadData2RadCombo(rcbCollateralID1, src, "RightID", "RightID", "-Select Collateral ID-");
            Util.LoadData2RadCombo(rcbCollateralID2, src, "RightID", "RightID", "-Select Collateral ID-");
            Util.LoadData2RadCombo(rcbCollateralID3, src, "RightID", "RightID", "-Select Collateral ID-");

            if (!String.IsNullOrEmpty(selectedValue1))
            {
                rcbCollateralID.SelectedValue = selectedValue1;
            }
            if (!String.IsNullOrEmpty(selectedValue2))
            {
                rcbCollateralID1.SelectedValue = selectedValue2;
            }
            if (!String.IsNullOrEmpty(selectedValue3))
            {
                rcbCollateralID2.SelectedValue = selectedValue3;
            }
            if (!String.IsNullOrEmpty(selectedValue4))
            {
                rcbCollateralID3.SelectedValue = selectedValue4;
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


            normalLoanEntry.LoanAmount = tbLoanAmount.Text != "" ? decimal.Parse(tbLoanAmount.Text) : 0;
            normalLoanEntry.ApproveAmount = tbApprovedAmt.Value.HasValue ? (decimal)tbApprovedAmt.Value.Value : 0;
            normalLoanEntry.OpenDate = rdpOpenDate.SelectedDate;
            normalLoanEntry.ValueDate = rdpValueDate.SelectedDate;

            normalLoanEntry.MaturityDate = rdpMaturityDate.SelectedDate;
            normalLoanEntry.CreditAccount = rcbCreditToAccount.SelectedValue;
            normalLoanEntry.LimitReference = rcbLimitReference.SelectedValue;
            normalLoanEntry.RateType = rcbRateType.SelectedValue;
            normalLoanEntry.InterestBasic = "366/360";
            normalLoanEntry.AnnuityRepMet = rcbAnnRepMet.SelectedValue;


            normalLoanEntry.InterestKey = rcbInterestKey.SelectedValue;
            normalLoanEntry.IntSpread = tbInSpread.Text;
            //normalLoanEntry.AutoSch = rcbAutoSch.SelectedValue;
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
            normalLoanEntry.CollateralID_1 = rcbCollateralID1.SelectedValue;
            normalLoanEntry.CollateralID_2 = rcbCollateralID2.SelectedValue;
            normalLoanEntry.CollateralID_3 = rcbCollateralID3.SelectedValue;
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
            tbLoanAmount.Text = normalLoanEntry.LoanAmount != null ? ((decimal)normalLoanEntry.LoanAmount).ToString("#,##") : null;
            tbApprovedAmt.Value = (double?)normalLoanEntry.ApproveAmount;
            rdpOpenDate.SelectedDate = normalLoanEntry.OpenDate;
            rdpValueDate.SelectedDate = normalLoanEntry.ValueDate;
            rdpDrawdown.SelectedDate = normalLoanEntry.Drawdown;
            rdpMaturityDate.SelectedDate = normalLoanEntry.MaturityDate;
            LoadLimitReferenceInfor(normalLoanEntry.CustomerID, normalLoanEntry.LimitReference);
            LoadAllAccount(normalLoanEntry.CustomerID, normalLoanEntry.Currency,
            normalLoanEntry.CreditAccount, normalLoanEntry.PrinRepAccount,
            normalLoanEntry.IntRepAccount, normalLoanEntry.ChrgRepAccount);
            LoadCollareralID(normalLoanEntry.CustomerID, normalLoanEntry.CollateralID, normalLoanEntry.CollateralID_1, normalLoanEntry.CollateralID_2, normalLoanEntry.CollateralID_3);
            rcbRateType.SelectedValue = normalLoanEntry.RateType;
            rcbInterestKey.SelectedValue = normalLoanEntry.InterestKey;
            tbInterestRate.Value = (double?)normalLoanEntry.InterestRate;
            tbInSpread.Value = double.Parse(!String.IsNullOrEmpty(normalLoanEntry.IntSpread) ? normalLoanEntry.IntSpread : "0");
            tbBusDayDef.Text = normalLoanEntry.BusDayDef;
            //rcbAutoSch.SelectedValue = normalLoanEntry.AutoSch;
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
            rdpDrawdown.Enabled = p;
            tbApprovedAmt.Enabled = p;
            rcbPrinRepAccount.Enabled = p;
            rcbIntRepAccount.Enabled = p;
            tbCustomerRemarks.Enabled = p;
            cmbAccountOfficer.Enabled = p;
            rcbChargRepAccount.Enabled = p;
            tbBusDayDef.Enabled = p;
            rcbCollateralID.Enabled = p;
            rcbCollateralID1.Enabled = p;
            rcbCollateralID2.Enabled = p;
            rcbCollateralID3.Enabled = p;
            rtbAmountAlloc.Enabled = p;
            rcbSecured.Enabled = p;
            tbForwardBackWard.Enabled = p;
            tbBaseDate.Enabled = p;
            lvLoanControl.Enabled = p;
            tbExpectedLoss.Enabled = p;
            tbLossGiven.Enabled = p;
            rcbRepaySchType.Enabled = p;
            rcbDefineSch.Enabled = p;
            tbNewNormalLoan.Enabled = !isApprovalRole;

        }

        private void disableInCaseOfAmend(bool p)
        {
            rcbCustomerID.Enabled = p;
            rcbCurrency.Enabled = p;
            tbLoanAmount.Enabled = p;
            tbApprovedAmt.Enabled = p;
            rdpOpenDate.Enabled = p;
            rdpDrawdown.Enabled = p;
            rdpValueDate.Enabled = p;
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

            SetEnabledControls(allowCommit);
            if (isAmendPage && allowCommit)
            {
                disableInCaseOfAmend(false);
            }
        }


        private string generateCode()
        {
            VietVictoryCoreBankingEntities bd = new VietVictoryCoreBankingEntities();
            StoreProRepository storePro = new StoreProRepository();
            return storePro.StoreProcessor().B_BMACODE_GetNewID("CRED_REVOLVING_CONTRACT", REFIX_MACODE, ".").First<string>();
        }

        //private void PrintLoanDocument1()
        //{
        //    Aspose.Words.License license = new Aspose.Words.License();
        //    license.SetLicense("Aspose.Words.lic");
        //    //Open template
        //    string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/LoanContract/LichTraVon.docx");
        //    //Open the template document
        //    Aspose.Words.Document document = new Aspose.Words.Document(docPath);
        //    //Execute the mail merge.
        //    var ds = PrepareData2Print();
        //    // Fill the fields in the document with user data.
        //    document.MailMerge.ExecuteWithRegions(ds.Tables["Info"]);
        //    document.MailMerge.ExecuteWithRegions(ds.Tables["Items"]);
        //    document.MailMerge.ExecuteWithRegions(ds.Tables["DateInfor"]);
        //    // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
        //    document.Save("LichTraVonHDTinDung_" + tbNewNormalLoan.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf", Aspose.Words.SaveFormat.Pdf, Aspose.Words.SaveType.OpenInBrowser, Response);
        //}

        //private void PrintLoanDocument2()
        //{
        //    Aspose.Words.License license = new Aspose.Words.License();
        //    license.SetLicense("Aspose.Words.lic");
        //    //Open template
        //    string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/LoanContract/LichTraLai.docx");
        //    //Open the template document
        //    Aspose.Words.Document document = new Aspose.Words.Document(docPath);
        //    //Execute the mail merge.
        //    var ds = PrepareInterestDate2Print();
        //    // Fill the fields in the document with user data.
        //    document.MailMerge.ExecuteWithRegions(ds.Tables["Info"]);
        //    document.MailMerge.ExecuteWithRegions(ds.Tables["Items"]);
        //    document.MailMerge.ExecuteWithRegions(ds.Tables["DateInfor"]);
        //    // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
        //    document.Save("LichTraLaiHDTinDung_" + tbNewNormalLoan.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf", Aspose.Words.SaveFormat.Pdf, Aspose.Words.SaveType.OpenInBrowser, Response);

        //    //doc.Save("RegisterDocumentaryCollectionMT410_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf", Aspose.Words.SaveFormat.Pdf, Aspose.Words.SaveType.OpenInApplication, Response);
        //}

        private void PrintLoanDocument()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");
            //Open template
            string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/LoanContract/LichTraVon.docx");
            string docPath2 = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/LoanContract/LichTraLai.docx");
            //Open the template document
            Aspose.Words.Document document = new Aspose.Words.Document(docPath);
            Aspose.Words.Document document2 = new Aspose.Words.Document(docPath2);
            //Execute the mail merge.
            var ds = PrepareData2Print();
            // Fill the fields in the document with user data.
            document.MailMerge.ExecuteWithRegions(ds.Tables["Info"]);
            document.MailMerge.ExecuteWithRegions(ds.Tables["Items"]);
            document.MailMerge.ExecuteWithRegions(ds.Tables["DateInfor"]);

            var ds2 = PrepareInterestDate2Print();
            // Fill the fields in the document with user data.
            document2.MailMerge.ExecuteWithRegions(ds2.Tables["Info"]);
            document2.MailMerge.ExecuteWithRegions(ds2.Tables["Items"]);
            document2.MailMerge.ExecuteWithRegions(ds2.Tables["DateInfor"]);

            document.AppendDocument(document2, Aspose.Words.ImportFormatMode.KeepSourceFormatting);
            //document2.Save("LichTraVonHDTinDung_" + tbNewNormalLoan.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf", Aspose.Words.SaveFormat.Pdf, Aspose.Words.SaveType.OpenInBrowser, Response);
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            document.Save("LichTraVonLaiHDTinDung_" + tbNewNormalLoan.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf", Aspose.Words.SaveFormat.Pdf, Aspose.Words.SaveType.OpenInBrowser, Response);

        }



        private DataSet PrepareInterestDate2Print()
        {
            if (normalLoanEntryM == null)
            {
                normalLoanEntryM = new BNEWNORMALLOAN();
                normalLoanEntryM.Code = tbNewNormalLoan.Text;
                loanBusiness.loadEntity(ref normalLoanEntryM);
            }

            if (normalLoanEntryM == null)
                return null;
            DataSet ds = new DataSet();

            NewLoanControlRepository facade = new NewLoanControlRepository();
            BNewLoanControl it = facade.FindLoanControl(normalLoanEntryM.Code, "I").FirstOrDefault();

            //condition check;
            bool canProcessInterest = true;

            //variable data for 

            string rateType = "1"; //1 = Fix A, 2 = Fix B, 3 = Ferioric;
            decimal interestedValue = 0;

            int durationDays = 0;//day
            int circleDuration = 0; //month
            int numberOfCircle = 0;
            decimal loanAmount = 0;
            int interestedKey = 0;

            string fregString = "Unknown";

            DateTime drawDownDate;
            DateTime startDate;
            DateTime endDate;
            DateTime startIdentifyIntDate;

            //init master data:
            startDate = (DateTime)normalLoanEntryM.ValueDate;
            endDate = (DateTime)normalLoanEntryM.MaturityDate;
            drawDownDate = normalLoanEntryM.Drawdown == null ? startDate : (DateTime)normalLoanEntryM.Drawdown;
            loanAmount = (decimal)normalLoanEntryM.LoanAmount;

            durationDays = endDate.Subtract(startDate).Days;
            DateTime? tempDate = null;
            canProcessInterest = FillInterestInformation(it, ref fregString, ref circleDuration, ref tempDate);
            startIdentifyIntDate = tempDate == null ? drawDownDate : (DateTime)tempDate;

            numberOfCircle = durationDays / (30 * circleDuration);
            if (numberOfCircle <= 0)
            {
                numberOfCircle = 1;
            }
            rateType = String.IsNullOrEmpty(normalLoanEntryM.RateType) ? "1" : normalLoanEntryM.RateType;
            if (rateType.Equals("3"))//periodic interest = interestedRate + int speed
            {
                interestedValue = (normalLoanEntryM.InterestRate == null ? 0 : (decimal)normalLoanEntryM.InterestRate)
                    + (String.IsNullOrEmpty(normalLoanEntryM.IntSpread) ? 0 : Decimal.Parse(normalLoanEntryM.IntSpread));
            }
            else // interest = interestedRate
            {
                interestedValue = (normalLoanEntryM.InterestRate == null ? 0 : (decimal)normalLoanEntryM.InterestRate);
            }
            interestedKey = (int)(durationDays / 30);

            ds.Tables.Add(GetCusomerInformation(normalLoanEntryM, fregString, interestedKey, interestedValue));
            ds.Tables.Add(GetInterestTableScheduler(rateType, numberOfCircle, loanAmount, circleDuration, startIdentifyIntDate,
                endDate, interestedValue));

            DataTable dateReport = new DataTable("DateInfor");
            dateReport.Columns.Add("day");
            dateReport.Columns.Add("month");
            dateReport.Columns.Add("year");
            var rowD = dateReport.NewRow();
            DateTime today = DateTime.Today;
            rowD["day"] = today.ToString("dd");
            rowD["month"] = today.ToString("MM");
            rowD["year"] = today.ToString("yyyy");
            dateReport.Rows.Add(rowD);
            ds.Tables.Add(dateReport);

            return ds;
        }

        private DataTable GetInterestTableScheduler(string rateType, int numberOfCircle, decimal loanAmount, int circleDuration, DateTime startIdentifyIntDate, DateTime endDate, decimal interestValued)
        {
            DateTime beginDate = startIdentifyIntDate;
            DateTime previoudDate = (DateTime)normalLoanEntryM.ValueDate;
            DataTable dtItems = new DataTable("Items");
            dtItems.Columns.Add("ky");
            dtItems.Columns.Add("ngaytra");
            dtItems.Columns.Add("sotientra");
            dtItems.Columns.Add("duno");

            Int32 moneyPercicle = (Int32)(loanAmount / numberOfCircle);
            decimal remainLoanAmount = loanAmount;
            decimal interestAmount = 0;

            for (int i = 0; i < numberOfCircle; i++)
            {

                interestAmount = ((beginDate.Subtract(previoudDate).Days * ((interestValued / 36000) * remainLoanAmount)));

                if (rateType.Equals("2"))
                {
                    //do nothing;
                }
                else
                {
                    remainLoanAmount = remainLoanAmount - moneyPercicle;
                }

                if (remainLoanAmount < 0)
                {
                    remainLoanAmount = 0;
                }

                if (i == numberOfCircle - 1)
                {
                    if (remainLoanAmount > 0)
                    {
                        moneyPercicle = (int)(moneyPercicle + remainLoanAmount);
                        interestAmount += ((beginDate.Subtract(previoudDate).Days * ((interestValued / 36000) * remainLoanAmount)));

                        remainLoanAmount = 0;
                    }

                    if (beginDate > endDate)
                    {
                        beginDate = endDate;
                    }
                }

                var row = dtItems.NewRow();
                row["ky"] = i + 1;
                row["ngaytra"] = beginDate.ToString("dd/MM/yyyy");
                row["sotientra"] = interestAmount.ToString("#,###");
                row["duno"] = remainLoanAmount == 0 ? "0" : (remainLoanAmount.ToString("#,###"));
                dtItems.Rows.Add(row);
                previoudDate = beginDate;
                beginDate = beginDate.AddMonths(circleDuration);
            }

            return dtItems;
        }

        private DataTable GetCusomerInformation(BNEWNORMALLOAN normalLoanEntryM, string fregString, int interestedKey, decimal interestedValue)
        {
            DataTable infoTb = new DataTable("Info");
            infoTb.Columns.Add("Code");
            infoTb.Columns.Add("Customer");
            infoTb.Columns.Add("LoanAmount");
            infoTb.Columns.Add("Drawdown");
            infoTb.Columns.Add("InterestKey");
            infoTb.Columns.Add("Freq");
            infoTb.Columns.Add("interest");

            var row = infoTb.NewRow();
            row["Code"] = normalLoanEntryM.Code;
            row["Customer"] = normalLoanEntryM.CustomerName;
            row["LoanAmount"] = ((decimal)normalLoanEntryM.LoanAmount).ToString("#,###");
            row["Drawdown"] = (normalLoanEntryM.Drawdown == null ? "" : ((DateTime)normalLoanEntryM.Drawdown).ToString("dd/MM/yyyy"));
            row["InterestKey"] = (int)interestedKey + " Tháng";
            row["Freq"] = fregString;
            row["interest"] = interestedValue.ToString("#,###.##");

            infoTb.Rows.Add(row);


            return infoTb;
        }

        private bool FillInterestInformation(BNewLoanControl loanControl, ref string fregStr, ref int circleDuration, ref DateTime? interestedStartDate)
        {
            if (loanControl == null)
            {
                return false;
            }

            interestedStartDate = loanControl.Date;
            if (String.IsNullOrEmpty(loanControl.Freq))
            {
                return false;
            }
            string freg = loanControl.Freq.ToUpper();

            if (freg.Equals("M"))
            {
                circleDuration = 1;
                fregStr = "1 Tháng";
            }
            else if (freg.Equals("Q"))
            {
                circleDuration = 3;
                fregStr = "1 Quý";
            }
            else if (freg.Equals("Y"))
            {
                circleDuration = 12;
                fregStr = "1 Năm";
            }
            else
            {
                return false;
            }
            return true;
        }

        private DataSet PrepareData2Print()
        {
            if (normalLoanEntryM == null)
            {
                normalLoanEntryM = new BNEWNORMALLOAN();
                normalLoanEntryM.Code = tbNewNormalLoan.Text;
                loanBusiness.loadEntity(ref normalLoanEntryM);
            }
            if (normalLoanEntryM == null)
                return null;
            NewLoanControlRepository facade = new NewLoanControlRepository();
            BNewLoanControl it = facade.FindLoanControl(normalLoanEntryM.Code, "P").FirstOrDefault();

            DateTime startDate = (DateTime)normalLoanEntryM.ValueDate;
            DateTime endDate = (DateTime)normalLoanEntryM.MaturityDate;
            int numberofday = (endDate).Subtract(startDate).Days;
            decimal money = (decimal)normalLoanEntryM.LoanAmount;
            int cicleMonth = 7;
            int noOfCicle = 0;
            int moneyPercicle = 0;
            string freg = "";

            if (it == null)
            {
                return null;
            }

            if (it.Freq.Equals("M"))
            {
                cicleMonth = 1;
                freg = "1 Tháng";
            }
            else if (it.Freq.Equals("Q"))
            {
                cicleMonth = 3;
                freg = "1 Quý";
            }
            else if (it.Freq.Equals("Y"))
            {
                cicleMonth = 12;
                freg = "1 Năm";
            }

            if (it.Date != null)
            {
                startDate = (DateTime)it.Date;
            }


            var ds = new DataSet();
            var infoTb = new DataTable("Info");
            infoTb.Columns.Add("Code");
            infoTb.Columns.Add("Customer");
            infoTb.Columns.Add("LoanAmount");
            infoTb.Columns.Add("Drawdown");
            infoTb.Columns.Add("InterestKey");
            infoTb.Columns.Add("Freq");

            var row = infoTb.NewRow();
            row["Code"] = normalLoanEntryM.Code;
            row["Customer"] = normalLoanEntryM.CustomerName;
            row["LoanAmount"] = ((decimal)normalLoanEntryM.LoanAmount).ToString("#,###");
            row["Drawdown"] = (normalLoanEntryM.Drawdown == null ? "" : ((DateTime)normalLoanEntryM.Drawdown).ToString("dd/MM/yyyy"));
            row["InterestKey"] = (int)numberofday / 30 + " Tháng";
            row["Freq"] = freg;

            infoTb.Rows.Add(row);
            ds.Tables.Add(infoTb);

            var itemTb = new DataTable("Items");
            itemTb.Columns.Add("ky");
            itemTb.Columns.Add("ngaytra");
            itemTb.Columns.Add("sotientra");
            itemTb.Columns.Add("duno");

            var rateType = 1;
            rateType = String.IsNullOrEmpty(normalLoanEntryM.RateType) ? 1 : Int16.Parse(normalLoanEntryM.RateType);

            if (rateType != 2)
            {
                noOfCicle = numberofday / (30 * cicleMonth);
                if (noOfCicle > 0)
                {
                    moneyPercicle = (int)(money / noOfCicle);
                    DateTime plandate = startDate;
                    for (int i = 0; i < noOfCicle; i++)
                    {
                        plandate = plandate.AddMonths(cicleMonth);
                        money = money - moneyPercicle;
                        if (money < 0)
                        {
                            money = 0;
                        }

                        if (i == noOfCicle - 1)
                        {
                            if (money > 0)
                            {
                                moneyPercicle = (int)(moneyPercicle + money);
                                money = 0;
                            }

                            if (plandate > endDate)
                            {
                                plandate = endDate;
                            }
                        }
                        row = itemTb.NewRow();
                        row["ky"] = i + 1;
                        row["ngaytra"] = plandate.ToString("dd/MM/yyyy");
                        row["sotientra"] = moneyPercicle.ToString("#,###");
                        row["duno"] = money == 0 ? "0" : (money.ToString("#,###"));
                        itemTb.Rows.Add(row);

                    }
                    ds.Tables.Add(itemTb);
                }

            }
            else
            {
                row = itemTb.NewRow();
                row["ky"] = 1;
                row["ngaytra"] = ((DateTime)normalLoanEntryM.MaturityDate).ToString("dd/MM/yyyy");
                row["sotientra"] = ((decimal)normalLoanEntryM.LoanAmount).ToString("#,###");
                row["duno"] = "0";
                itemTb.Rows.Add(row);
                ds.Tables.Add(itemTb);
            }


            DataTable dateReport = new DataTable("DateInfor");
            dateReport.Columns.Add("day");
            dateReport.Columns.Add("month");
            dateReport.Columns.Add("year");
            var rowD = dateReport.NewRow();
            DateTime today = DateTime.Today;
            rowD["day"] = today.ToString("dd");
            rowD["month"] = today.ToString("MM");
            rowD["year"] = today.ToString("yyyy");
            dateReport.Rows.Add(rowD);
            ds.Tables.Add(dateReport);

            return ds;

        }


        private void PrepareData2Print(ref Aspose.Cells.Worksheet worksheet)
        {

            if (normalLoanEntryM == null)
            {
                normalLoanEntryM = new BNEWNORMALLOAN();
                normalLoanEntryM.Code = tbNewNormalLoan.Text;
                loanBusiness.loadEntity(ref normalLoanEntryM);
            }
            if (normalLoanEntryM == null)
                return;

            NewLoanControlRepository facade = new NewLoanControlRepository();
            BNewLoanControl it = facade.FindLoanControl(normalLoanEntryM.Code, "P").FirstOrDefault();

            DateTime startDate = (DateTime)normalLoanEntryM.ValueDate;
            DateTime endDate = (DateTime)normalLoanEntryM.MaturityDate;
            int numberofday = (endDate).Subtract(startDate).Days;
            decimal money = (decimal)normalLoanEntryM.LoanAmount;
            int cicleMonth = 7;
            int noOfCicle = 0;
            int moneyPercicle = 0;

            if (it == null)
            {
                return;
            }

            int index = 1;
            worksheet.AutoFitColumn(1);
            worksheet.Cells["A" + index++].PutValue("LỊCH TRẢ NỢ HỢP ĐỒNG TÍN DỤNG SỐ " + normalLoanEntryM.Code);
            worksheet.Cells["B" + index++].PutValue("Ngày: " + DateTime.Now.ToString("dd/MM/yyyy"));
            index++;
            worksheet.Cells["A" + index++].PutValue("Khách hàng: Ông/Bà " + normalLoanEntryM.CustomerName);
            worksheet.Cells["A" + index++].PutValue("Số tiền vay: " + ((decimal)normalLoanEntryM.LoanAmount).ToString("#,###"));
            worksheet.Cells["A" + index++].PutValue("Ngày rút tiền:  " + (normalLoanEntryM.Drawdown == null ? "" : ((DateTime)normalLoanEntryM.Drawdown).ToString("dd/MM/yyyy")));
            worksheet.Cells["A" + index++].PutValue("Thời hạn: " + (int)numberofday / 30 + " Tháng");
            if (it.Freq.Equals("M"))
            {
                cicleMonth = 1;
                worksheet.Cells["A" + index++].PutValue("Định kỳ trả nợ: 1 Tháng");
            }
            else if (it.Freq.Equals("Q"))
            {
                cicleMonth = 3;
                worksheet.Cells["A" + index++].PutValue("Định kỳ trả nợ: 1 Quý");
            }
            else if (it.Freq.Equals("Y"))
            {
                cicleMonth = 12;
                worksheet.Cells["A" + index++].PutValue("Định kỳ trả nợ: 1 Năm");
            }
            index++;
            worksheet.Cells["A" + index].PutValue("Kỳ");
            worksheet.Cells["B" + index].PutValue("Ngày trả");
            worksheet.Cells["C" + index].PutValue("Số tiền phải trả");
            worksheet.Cells["D" + index].PutValue("Dư nợ còn lại");

            index++;

            //if (numberofday % (30*cicleMonth) == 0)
            //{
            noOfCicle = numberofday / (30 * cicleMonth);
            //}
            //else
            //{
            //    noOfCicle = (numberofday / cicleMonth) + 1;
            //}
            if (noOfCicle > 0)
            {
                moneyPercicle = (int)(money / noOfCicle);
                DateTime plandate = startDate;
                for (int i = 0; i < noOfCicle; i++)
                {
                    plandate = plandate.AddMonths(cicleMonth);
                    money = money - moneyPercicle;
                    if (money < 0)
                    {
                        money = 0;
                    }

                    if (i == noOfCicle - 1)
                    {
                        if (money > 0)
                        {
                            moneyPercicle = (int)(moneyPercicle + money);
                            money = 0;
                        }

                        if (plandate > endDate)
                        {
                            plandate = endDate;
                        }
                    }
                    worksheet.Cells["A" + index].PutValue(i + 1);
                    worksheet.Cells["B" + index].PutValue(plandate.ToString("dd/MM/yyyy"));
                    worksheet.Cells["C" + index].PutValue(moneyPercicle.ToString("#,###"));
                    worksheet.Cells["D" + index].PutValue(money.ToString("#,###"));

                    index++;
                }
            }
            index++;
            worksheet.Cells["A" + index].PutValue("Tp.HCM, Ngày " + DateTime.Now.ToString("dd") + " tháng " + DateTime.Now.ToString("MM") + " năm " + DateTime.Now.ToString("yyyy"));
            index++;
            worksheet.Cells["A" + index].PutValue("Bên vay");
            worksheet.Cells["D" + index].PutValue("Bên cho vay");

        }

        #endregion

        protected void tbBusDayDef_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
