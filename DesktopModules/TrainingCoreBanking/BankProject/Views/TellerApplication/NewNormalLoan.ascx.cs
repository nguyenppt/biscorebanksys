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
using System.Globalization;



namespace BankProject.Views.TellerApplication
{
    public partial class NewNormalLoan : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        INewNormalLoanBusiness<BNEWNORMALLOAN> loanBusiness;
        BNEWNORMALLOAN normalLoanEntryM;
        bool isApprovalRole = false;
        public double remainLoanAmountDis = 0;

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
                    if (IsUnderlimitAmount())
                    {
                        loanBusiness.Entity = normalLoanEntryM;
                        loanBusiness.commitProcess(this.UserId);

                        this.Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    }
                    break;


                case "Preview":
                    this.Response.Redirect(EditUrl("preview"));
                    break;

                case "authorize":
                    BindField2Data(ref normalLoanEntryM);
                    String errorMess = "";
                    if (isCanBeAuthorizeProcess(normalLoanEntryM))
                    {
                        loanBusiness.Entity = normalLoanEntryM;
                        loanBusiness.authorizeProcess(this.UserId);
                        UpdateSchedulePaymentToDB();
                        this.Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    }
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
            //LoadCollareralID(rcbCustomerID.SelectedValue, null, null, null, null);
            //LoadLimitReferenceInfor(rcbCustomerID.SelectedValue, null);
            //LoadAllAccount(rcbCustomerID.SelectedValue, rcbCurrency.SelectedValue, null, null, null, null);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickMainTab();", true);
        }


        protected void rcbCurrency_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadAllAccount(tbHDCustID.Text, rcbCurrency.SelectedValue, null, null, null, null);
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
            DropDownList freq = (lvLoanControl.EditItem.FindControl("FreqTextBox")) as DropDownList;
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
            DropDownList freq = (lvLoanControl.InsertItem.FindControl("FreqTextBox")) as DropDownList;

            BNewLoanControl item = new BNewLoanControl();
            item.Type = type.SelectedValue;
            item.Date = date.SelectedDate;
            item.AmountAction = String.IsNullOrEmpty(amountAction.Text) ? 0 : Double.Parse(amountAction.Text);
            item.Rate = String.IsNullOrEmpty(rate.Text) ? 0 : Double.Parse(rate.Text);
            item.No = String.IsNullOrEmpty(notext.Text) ? 0 : Double.Parse(notext.Text);
            item.Freq = freq.Text;
            item.Code = tbNewNormalLoan.Text;
            item.PeriodRepaid = int.Parse(hfRepaymentTimes.Value);
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
            LoadDataTolvLoanDisbursalSchedule();
            processApproriateAction();
        }


        protected void lvLoanDisbursalSchedule_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lvLoanDisbursalSchedule.EditIndex = -1;
            LoadDataTolvLoanDisbursalSchedule();
        }

        protected void lvLoanDisbursalSchedule_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvLoanDisbursalSchedule.EditIndex = e.NewEditIndex;
            LoadDataTolvLoanDisbursalSchedule();
        }

        protected void lvLoanDisbursalSchedule_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            RadDatePicker date = (lvLoanDisbursalSchedule.EditItem.FindControl("DateTextBox")) as RadDatePicker;
            RadNumericTextBox amountAction = (lvLoanDisbursalSchedule.EditItem.FindControl("AmountActionTextBox")) as RadNumericTextBox;
            RadDatePicker DrawdownDate = (lvLoanDisbursalSchedule.EditItem.FindControl("DrawdownDateTextBox")) as RadDatePicker;
            Label lbID = (lvLoanDisbursalSchedule.EditItem.FindControl("lbID")) as Label;


            B_LOAN_DISBURSAL_SCHEDULE item = new B_LOAN_DISBURSAL_SCHEDULE();
            item.DisbursalDate = date.SelectedDate;
            item.DisbursalAmount = String.IsNullOrEmpty(amountAction.Text) ? 0 : Double.Parse(amountAction.Text);
            item.DrawdownDate = DrawdownDate.SelectedDate;
            item.Code = tbNewNormalLoan.Text;


            if (item.DrawdownDate != null && item.DrawdownDate > DateTime.Now)
            {
                RadWindowManager1.RadAlert("Drawdown Date cannot be after current date!", 340, 150, "Alert", null);
                return;
            }

            LoanDisbursalScheduleRespository facade = new LoanDisbursalScheduleRespository();
            item.ID = Int32.Parse(lbID.Text);
            B_LOAN_DISBURSAL_SCHEDULE exits = facade.GetById(item.ID);
            if (exits != null)
            {
                facade.Update(facade.GetById(item.ID), item);
                facade.Commit();
            }
            lvLoanDisbursalSchedule.EditIndex = -1;
            LoadDataTolvLoanDisbursalSchedule();
        }

        protected void lvLoanDisbursalSchedule_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            String ids = "";
            Label lbl = (lvLoanDisbursalSchedule.Items[e.ItemIndex].FindControl("lbID")) as Label;
            if (lbl != null)
                ids = lbl.Text;

            if (!String.IsNullOrEmpty(ids))
            {
                LoanDisbursalScheduleRespository facade = new LoanDisbursalScheduleRespository();
                var itm = facade.GetById(Int16.Parse(ids));
                if (itm != null)
                {
                    facade.Delete(itm);
                    facade.Commit();
                    LoadDataTolvLoanDisbursalSchedule();
                }

            }
        }

        protected void lvLoanDisbursalSchedule_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            RadDatePicker date = (lvLoanDisbursalSchedule.InsertItem.FindControl("DateTextBox")) as RadDatePicker;
            RadNumericTextBox amountAction = (lvLoanDisbursalSchedule.InsertItem.FindControl("AmountActionTextBox")) as RadNumericTextBox;
            RadDatePicker DrawdownDate = (lvLoanDisbursalSchedule.InsertItem.FindControl("DrawdownDateTextBox")) as RadDatePicker;


            B_LOAN_DISBURSAL_SCHEDULE item = new B_LOAN_DISBURSAL_SCHEDULE();
            item.DisbursalDate = date.SelectedDate;
            item.DisbursalAmount = String.IsNullOrEmpty(amountAction.Text) ? 0 : Double.Parse(amountAction.Text);
            item.DrawdownDate = DrawdownDate.SelectedDate;
            item.Code = tbNewNormalLoan.Text;

            if (item.DrawdownDate != null && item.DrawdownDate > DateTime.Now)
            {
                RadWindowManager1.RadAlert("Drawdown Date cannot be after current date!", 340, 150, "Alert", null);
                return;
            }

            LoanDisbursalScheduleRespository facade = new LoanDisbursalScheduleRespository();
            facade.Add(item);
            facade.Commit();
            LoadDataTolvLoanDisbursalSchedule();
        }


        protected void btnPrintLai_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrintVon_Click(object sender, EventArgs e)
        {

        }

        protected void tbExpectedLoss_TextChanged(object sender, EventArgs e)
        {
            LoanCreditScoringRepository facade = new LoanCreditScoringRepository();
            var score = facade.GetRatingScoring((int)tbExpectedLoss.Value).FirstOrDefault();
            tbLossGiven.Text = score.Rating;

        }
        #endregion

        #region Common Function

        private bool IsUnderlimitAmount()
        {

            StoreProRepository facade = new StoreProRepository();
            if (!isAmendPage)
            {

                var limit = facade.StoreProcessor().B_Normal_Loan_Get_RemainLimitAmount_Exclude_LoanID(normalLoanEntryM.LimitReference, normalLoanEntryM.Code).First<decimal?>();
                if (limit < normalLoanEntryM.LoanAmount)
                {
                    RadWindowManager1.RadAlert("Loan amount cannot exceed " + limit, 340, 150, "Alert", null);
                    return false;
                }
                else
                {
                    var utildate = facade.StoreProcessor().B_Normal_Loan_Get_OfferedUntilDate(normalLoanEntryM.LimitReference).First<DateTime?>();
                    if (utildate != null && normalLoanEntryM.Drawdown != null && normalLoanEntryM.Drawdown > utildate)
                    {
                        RadWindowManager1.RadAlert("Drawdown date cannot be greater than product limit Offered Until date [" + ((DateTime)utildate).ToString("MM/dd/yyyy") + "]", 340, 150, "Alert", null);
                        return false;
                    }
                }

                if (normalLoanEntryM.Drawdown == null && normalLoanEntryM.RateType.Equals("2"))
                {
                    RadWindowManager1.RadAlert("[Fix for Initial] rate type is not supported for this case!", 340, 150, "Alert", null);
                    return false;
                }

            }
            var productLine = facade.StoreProcessor().B_Normal_Loan_Get_Productline_Info(normalLoanEntryM.LimitReference).First();

            if (productLine != null && productLine.MaxSecured != null && productLine.MaxSecured > 0)
            {
                if (!normalLoanEntryM.Secured.Equals("Y"))
                {
                    RadWindowManager1.RadAlert("Field Secured (Y/N) must be Yes", 340, 150, "Alert", null);
                    return false;
                }
            }

            if (normalLoanEntryM.Secured.Equals("Y"))
            {
                if (String.IsNullOrEmpty(normalLoanEntryM.CollateralID)
                    && String.IsNullOrEmpty(normalLoanEntryM.CollateralID_1)
                    && String.IsNullOrEmpty(normalLoanEntryM.CollateralID_2)
                    && String.IsNullOrEmpty(normalLoanEntryM.CollateralID_3))
                {
                    RadWindowManager1.RadAlert("Collateral ID not completed", 340, 150, "Alert", null);
                    return false;
                }

                if (normalLoanEntryM.AmountAlloc == null || normalLoanEntryM.AmountAlloc <= 0)
                {
                    RadWindowManager1.RadAlert("Collateral Amount must be >0", 340, 150, "Alert", null);
                    return false;
                }
            }



            return true;
        }

        private bool isCanBeAuthorizeProcess(BNEWNORMALLOAN normalLoanEntry)
        {
            if (normalLoanEntry == null || String.IsNullOrEmpty(normalLoanEntry.Code))
            {
                
                RadWindowManager1.RadAlert("Loan Contract is Empty!", 340, 150, "Alert", null);
                return false;
            }
            CollateralContingentEntryRespository colaFacade = new CollateralContingentEntryRespository();

            if (!string.IsNullOrEmpty(normalLoanEntry.CollateralID))
            {
                var ite = colaFacade.GetCollateralContingentInfo(normalLoanEntry.CollateralID).FirstOrDefault();
                if (ite == null)
                {

                    RadWindowManager1.RadAlert("This Loan Contract cannot be authorized because Collateral ID [" + normalLoanEntry.CollateralID + "] has not processed contigent entry yet.", 340, 150, "Alert", null);
                    return false;
                }

            }

            if (!string.IsNullOrEmpty(normalLoanEntry.CollateralID_1))
            {
                var ite = colaFacade.GetCollateralContingentInfo(normalLoanEntry.CollateralID_1).FirstOrDefault();
                if (ite == null)
                {
                    RadWindowManager1.RadAlert("This Loan Contract cannot be authorized because Collateral ID [" + normalLoanEntry.CollateralID_1 + "] has not processed contigent entry yet.", 340, 150, "Alert", null);
                    return false;
                }

            }

            if (!string.IsNullOrEmpty(normalLoanEntry.CollateralID_2))
            {
                var ite = colaFacade.GetCollateralContingentInfo(normalLoanEntry.CollateralID_2).FirstOrDefault();
                if (ite == null)
                {
                    RadWindowManager1.RadAlert("This Loan Contract cannot be authorized because Collateral ID [" + normalLoanEntry.CollateralID_2 + "] has not processed contigent entry yet.", 340, 150, "Alert", null);
                    return false;
                }

            }

            if (!string.IsNullOrEmpty(normalLoanEntry.CollateralID_3))
            {
                var ite = colaFacade.GetCollateralContingentInfo(normalLoanEntry.CollateralID_3).FirstOrDefault();
                if (ite == null)
                {
                    RadWindowManager1.RadAlert("This Loan Contract cannot be authorized because Collateral ID [" + normalLoanEntry.CollateralID_3 + "] has not processed contigent Entry yet.", 340, 150, "Alert", null);
                    return false;
                }

            }

            return true;
        }

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
                            UpdateStatusAuthorizeAction(false, true, false, false, false, true);
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
            //LoadCustomerCombobox(null);
            LoadPurposeCode(null);
            LoadGroup(null);
            LoadAccountOfficer(null);
            LoadInterestKey(null);
            LoadBusinessDay(null);
            //InitDefaultData();

            //Load data and binding it to UI
            loanBusiness.loadEntity(ref normalLoanEntryM);
            BindData2Field(normalLoanEntryM);
            LoadDataTolvLoanControl();
            LoadDataTolvLoanDisbursalSchedule();
            //deside which action should be taken care
            InitDefaultData();
            processApproriateAction();

            if (normalLoanEntryM != null && normalLoanEntryM.Drawdown == null)
            {

            }

        }
        private void InitDefaultData()
        {

            radcbMainCategory.Focus();
        }
        private void LoadInterestKey(string selectedid)
        {

            NewLoanInterestedKeyRepository facade = new NewLoanInterestedKeyRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(rcbDepositeRate, src, "MonthLoanRateNo", "LoanInterest_Key", "-Select Interest Key-", true);

            if (!String.IsNullOrEmpty(selectedid))
            {
                rcbDepositeRate.SelectedValue = selectedid;
            }
        }
        private void LoadBusinessDay(string selectedid)
        {

            CountryRepository facade = new CountryRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(rcbBusDay, src, "MaQuocGia", "MaQuocGia", "-Select Business Day-", true);

            if (!String.IsNullOrEmpty(selectedid))
            {
                rcbBusDay.SelectedValue = selectedid;
            }
        }
        private void LoadAccountOfficer(string selectedid)
        {
            AccountOfficerRepository facade = new AccountOfficerRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(cmbAccountOfficer, src, "Code", "description", "-Select Account Officer-", false);

            if (!String.IsNullOrEmpty(selectedid))
            {
                cmbAccountOfficer.SelectedValue = selectedid;
            }
        }
        private void LoadGroup(string selectedvalue)
        {
            LoanGroupRepository facade = new LoanGroupRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(rcbLoadGroup, src, "Id", "Name", "-Select Load Group-", true);

            if (!String.IsNullOrEmpty(selectedvalue))
            {
                rcbLoadGroup.SelectedValue = selectedvalue;
            }
        }
        private void LoadPurposeCode(string selectedvalue)
        {
            LoanPurposeRepository facade = new LoanPurposeRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(rcbPurposeCode, src, "Id", "Name", "-Select Purpose Code-", false);

            if (!String.IsNullOrEmpty(selectedvalue))
            {
                rcbPurposeCode.SelectedValue = selectedvalue;
            }
        }
        private void LoadMainCategoryCombobox(string selectedItem)
        {
            StoreProRepository storeFacade = new StoreProRepository();
            var db = storeFacade.StoreProcessor().B_BRPODCATEGORY_GetAll_IdOver200().ToList();
            Util.LoadData2RadCombo(radcbMainCategory, db, "CatId", "Display", "-Select Main Category-", false);
            radcbMainCategory.SelectedValue = selectedItem;

        }
        

        private void LoadCustomerInformation(string SelectedCus)
        {
            BCustomerRepository facade1 = new BCustomerRepository();
            var db = facade1.getCustomerInfo(SelectedCus,"AUT");

            if (db != null)
            {
                tbHDCustID.Text = db.CustomerID;
                lbCust.Text = db.GBFullName;
            }
            else
            {
                tbHDCustID.Text = String.Empty;
                lbCust.Text = "Not Found!";
            }


        }
        private void LoadLimitReferenceInfor(string custId, string selectedvalue)
        {

            CustomerLimitSubRepository facade = new CustomerLimitSubRepository();
            var src = facade.FindLimitCusSub(custId).ToList();
            Util.LoadData2RadCombo(rcbLimitReference, src, "SubLimitID", "SubLimitID", "-Select Limit Refer-", false);


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
            Util.LoadData2RadCombo(rcbCollateralID, src, "RightID", "RightID", "-Select Collateral ID-", true);
            Util.LoadData2RadCombo(rcbCollateralID1, src, "RightID", "RightID", "-Select Collateral ID-", true);
            Util.LoadData2RadCombo(rcbCollateralID2, src, "RightID", "RightID", "-Select Collateral ID-", true);
            Util.LoadData2RadCombo(rcbCollateralID3, src, "RightID", "RightID", "-Select Collateral ID-", true);

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
            Util.LoadData2RadCombo(rcbCreditToAccount, ds, "Id", "Display", "-Select a credit Account-", true);
            Util.LoadData2RadCombo(rcbPrinRepAccount, ds, "Id", "Display", "-Select a Print Rep Account-", true);
            Util.LoadData2RadCombo(rcbIntRepAccount, ds, "Id", "Display", "-Select a Int Rep Account-", true);
            Util.LoadData2RadCombo(rcbChargRepAccount, ds, "Id", "Display", "-Select a Charge Rep Account-", true);
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
            var db = facade.FindLoanControlByCodeAll(tbNewNormalLoan.Text, int.Parse(hfRepaymentTimes.Value));
            lvLoanControl.DataSource = db.ToList();
            lvLoanControl.DataBind();
        }

        private void LoadDataTolvLoanDisbursalSchedule()
        {
            CalcRemainDisbursalLoanAmount();
            LoanDisbursalScheduleRespository facade = new LoanDisbursalScheduleRespository();
            var db = facade.FindLoanDisbursalByCode(tbNewNormalLoan.Text);
            lvLoanDisbursalSchedule.DataSource = db.ToList();
            lvLoanDisbursalSchedule.DataBind();



        }

        private void CalcRemainDisbursalLoanAmount()
        {
            StoreProRepository facade = new StoreProRepository();
            decimal planAmount = tbLoanAmount.Text != "" ? decimal.Parse(tbLoanAmount.Text) : 0;
            var rus = facade.StoreProcessor().B_LOAN_DISBURSAL_SCHEDULE_Get_Total_LoanAmount(tbNewNormalLoan.Text).First<double?>();
            this.remainLoanAmountDis = rus == null ? 0 : (double)rus;
            remainLoanAmountDis = ((double)planAmount - remainLoanAmountDis);
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
            normalLoanEntry.CustomerID = tbHDCustID.Text;
            normalLoanEntry.CustomerName = lbCust.Text;
            normalLoanEntry.LoanGroup = rcbLoadGroup.SelectedValue;
            normalLoanEntry.LoanGroupName = rcbLoadGroup.Text;
            normalLoanEntry.Currency = rcbCurrency.SelectedValue;
            normalLoanEntry.BusDayDef = rcbBusDay.SelectedValue;


            normalLoanEntry.LoanAmount = tbLoanAmount.Text != "" ? decimal.Parse(tbLoanAmount.Text) : 0;
            normalLoanEntry.ApproveAmount = normalLoanEntry.LoanAmount;
            normalLoanEntry.OpenDate = rdpOpenDate.SelectedDate;
            normalLoanEntry.ValueDate = rdpValueDate.SelectedDate;

            normalLoanEntry.MaturityDate = rdpMaturityDate.SelectedDate;
            normalLoanEntry.CreditAccount = rcbCreditToAccount.SelectedValue;
            normalLoanEntry.LimitReference = rcbLimitReference.SelectedValue;
            normalLoanEntry.RateType = rcbRateType.SelectedValue;
            normalLoanEntry.InterestBasic = "366/360";


            normalLoanEntry.InterestKey = rcbDepositeRate.SelectedValue;
            normalLoanEntry.IntSpread = tbInSpread.Text;

            normalLoanEntry.Drawdown = rdpDrawdown.SelectedDate;

            normalLoanEntry.ChrgRepAccount = rcbChargRepAccount.SelectedValue;
            normalLoanEntry.ExpectedLoss = (decimal?)tbExpectedLoss.Value;
            normalLoanEntry.LossGivenDef = tbLossGiven.Text;
            normalLoanEntry.CustomerRemarks = tbCustomerRemarks.Text;
            normalLoanEntry.AccountOfficer = cmbAccountOfficer.SelectedValue;
            normalLoanEntry.AccountOfficerName = cmbAccountOfficer.Text;
            normalLoanEntry.Secured = rcbSecured.SelectedValue;
            normalLoanEntry.CollateralID = rcbCollateralID.SelectedValue;
            normalLoanEntry.CollateralID_1 = rcbCollateralID1.SelectedValue;
            normalLoanEntry.CollateralID_2 = rcbCollateralID2.SelectedValue;
            normalLoanEntry.CollateralID_3 = rcbCollateralID3.SelectedValue;
            normalLoanEntry.DefineSch = rcbDefineSch.SelectedValue;
            normalLoanEntry.AmountAlloc =  rtbAmountAlloc.Text != "" ? decimal.Parse(rtbAmountAlloc.Text) : 0;
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
            tbHDCustID.Text = tbCustID.Text = normalLoanEntry.CustomerID;
            LoadCustomerInformation(normalLoanEntry.CustomerID);
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
            rcbDepositeRate.SelectedValue = normalLoanEntry.InterestKey;
            tbInterestRate.Value = (double?)normalLoanEntry.InterestRate;
            tbInSpread.Value = double.Parse(!String.IsNullOrEmpty(normalLoanEntry.IntSpread) ? normalLoanEntry.IntSpread : "0");
            rcbBusDay.SelectedValue = normalLoanEntry.BusDayDef;
            rcbDefineSch.SelectedValue = normalLoanEntry.DefineSch;
            tbCustomerRemarks.Text = normalLoanEntry.CustomerRemarks;
            cmbAccountOfficer.SelectedValue = normalLoanEntry.AccountOfficer;
            rcbSecured.SelectedValue = normalLoanEntry.Secured;
            tbExpectedLoss.Value = (double?)normalLoanEntry.ExpectedLoss;
            tbLossGiven.Text = normalLoanEntry.LossGivenDef;
            rtbAmountAlloc.Text =  normalLoanEntry.AmountAlloc != null ? ((decimal)normalLoanEntry.AmountAlloc).ToString("#,##") : null;
            lbLoanStatus.Text = normalLoanEntry.LoanStatus;
            lbTotalInterestAmt.Text = "" + normalLoanEntry.TotalInterestAmt;
            lbPDStatus.Text = normalLoanEntry.PDStatus;
            hfRepaymentTimes.Value = normalLoanEntry.RepaymentTimes.ToString();
        }
        private void LoadSubCategory(string categoryid, string selectedValue)
        {
            ProductCategoryRepository facade = new ProductCategoryRepository();
            var model = facade.getProductCategory(categoryid).ToList();
            Util.LoadData2RadCombo(rcbSubCategory, model, "SubCatId", "SubCatName", "-Select a Sub Category-", false);


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
            tbCustID.Enabled = p;
            rcbLoadGroup.Enabled = p;
            tbLoanAmount.Enabled = p;
            rdpMaturityDate.Enabled = p;
            rcbCreditToAccount.Enabled = p;
            rcbLimitReference.Enabled = p;
            rcbRateType.Enabled = p;
            tbInterestRate.Enabled = p;
            rcbDepositeRate.Enabled = p;
            tbInSpread.Enabled = p;
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
            rcbBusDay.Enabled = p;
            rcbCollateralID.Enabled = p;
            rcbCollateralID1.Enabled = p;
            rcbCollateralID2.Enabled = p;
            rcbCollateralID3.Enabled = p;
            rtbAmountAlloc.Enabled = p;
            rcbSecured.Enabled = p;
            tbForwardBackWard.Enabled = p;
            tbBaseDate.Enabled = p;
            lvLoanControl.Enabled = p;
            lvLoanDisbursalSchedule.Enabled = p;
            tbExpectedLoss.Enabled = p;
            tbLossGiven.Enabled = false;
            rcbDefineSch.Enabled = p;
            tbNewNormalLoan.Enabled = !isApprovalRole;

        }

        private void disableInCaseOfAmend(bool p)
        {
            tbCustID.Enabled = p;
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


            if (isApprovalRole)
            {
                RadToolBar1.FindItemByValue("btnPrint").Enabled = true;
            }
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


        private void PrintLoanDocument()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");
            //Open template
            //string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/LoanContract/LichTraLai.docx");
            bool isRepayment = false;
            var ds = PrepareDataForLoanContractSchedule(ref isRepayment);

            string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/LoanContract/LichTraNoHopDongVay.docx");

            if (isRepayment)
            {
                docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/LoanContract/LichTraNoHopDongVayRepayment.docx");
            }
            //Open the template document
            Aspose.Words.Document document = new Aspose.Words.Document(docPath);
            //Execute the mail merge.
            
            // Fill the fields in the document with user data.
            document.MailMerge.ExecuteWithRegions(ds.DtInfor);
            document.MailMerge.ExecuteWithRegions(ds.DtItems.DefaultView);
            document.MailMerge.ExecuteWithRegions(ds.DateReport);
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            document.Save("LichTraNoHDTinDung_" + tbNewNormalLoan.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf", Aspose.Words.SaveFormat.Pdf, Aspose.Words.SaveType.OpenInApplication, Response);

            //doc.Save("RegisterDocumentaryCollectionMT410_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf", Aspose.Words.SaveFormat.Pdf, Aspose.Words.SaveType.OpenInApplication, Response);
        }

        private void UpdateSchedulePaymentToDB()
        {
            if (normalLoanEntryM == null)
            {
                normalLoanEntryM = new BNEWNORMALLOAN();
                normalLoanEntryM.Code = tbNewNormalLoan.Text;
                loanBusiness.loadEntity(ref normalLoanEntryM);
            }

            if (normalLoanEntryM == null)
                return;

            loanBusiness.UpdateDataToPriciplePaymentSchedule(normalLoanEntryM, normalLoanEntryM.RepaymentTimes, this.UserId);
        }

        

        private LoanContractScheduleDS PrepareDataForLoanContractSchedule(ref bool isRepayment)
        {
            if (normalLoanEntryM == null)
            {
                normalLoanEntryM = new BNEWNORMALLOAN();
                normalLoanEntryM.Code = tbNewNormalLoan.Text;
                loanBusiness.loadEntity(ref normalLoanEntryM);
            }

            if (normalLoanEntryM == null)
                return null;

            if (normalLoanEntryM.RepaymentTimes > 0)
            {
                isRepayment = true;
            }
            else
            {
                isRepayment = false;
            }

            return loanBusiness.PrepareDataForLoanContractSchedule(normalLoanEntryM, normalLoanEntryM.RepaymentTimes);
        }

       
        #endregion

        protected void tbCustID_TextChanged(object sender, EventArgs e)
        {
            LoadCustomerInformation(tbCustID.Text);
            LoadCollareralID(tbCustID.Text, null, null, null, null);
            LoadLimitReferenceInfor(tbCustID.Text, null);
            LoadAllAccount(tbCustID.Text, rcbCurrency.SelectedValue, null, null, null, null);
        }

    }

    
}
