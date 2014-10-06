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
                    //UpdateSchedulePaymentToDB();
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


        protected void btnPrintLai_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrintVon_Click(object sender, EventArgs e)
        {

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
            LoadCustomerCombobox(null);
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

            NewLoanInterestedKeyRepository facade = new NewLoanInterestedKeyRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(rcbDepositeRate, src, "Id", "LoanInterest_Key", "-Select Interest Key-");

            if (!String.IsNullOrEmpty(selectedid))
            {
                rcbDepositeRate.SelectedValue = selectedid;
            }
        }
        private void LoadBusinessDay(string selectedid)
        {

            CountryRepository facade = new CountryRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(rcbBusDay, src, "MaQuocGia", "MaQuocGia", "-Select Business Day-");

            if (!String.IsNullOrEmpty(selectedid))
            {
                rcbBusDay.SelectedValue = selectedid;
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
            var db = facade1.getCustomerList("AUT");
            List<BCUSTOMER_INFO> hh = db.ToList<BCUSTOMER_INFO>();
            Util.LoadData2RadCombo(rcbCustomerID, hh, "CustomerID", "ID_FullName", "-Select Customer Code-");


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
            normalLoanEntry.CustomerName = rcbCustomerID.Text.Remove(0, rcbCustomerID.Text.IndexOf("-") + 1);
            normalLoanEntry.LoanGroup = rcbLoadGroup.SelectedValue;
            normalLoanEntry.LoanGroupName = rcbLoadGroup.Text;
            normalLoanEntry.Currency = rcbCurrency.SelectedValue;
            normalLoanEntry.BusDayDef = rcbBusDay.SelectedValue;


            normalLoanEntry.LoanAmount = tbLoanAmount.Text != "" ? decimal.Parse(tbLoanAmount.Text) : 0;
            normalLoanEntry.ApproveAmount = tbApprovedAmt.Value.HasValue ? (decimal)tbApprovedAmt.Value.Value : 0;
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
            rcbDepositeRate.SelectedValue = normalLoanEntry.InterestKey;
            tbInterestRate.Value = (double?)normalLoanEntry.InterestRate;
            tbInSpread.Value = double.Parse(!String.IsNullOrEmpty(normalLoanEntry.IntSpread) ? normalLoanEntry.IntSpread : "0");
            rcbBusDay.SelectedValue = normalLoanEntry.BusDayDef;
            rcbDefineSch.SelectedValue = normalLoanEntry.DefineSch;
            tbCustomerRemarks.Text = normalLoanEntry.CustomerRemarks;
            cmbAccountOfficer.SelectedValue = normalLoanEntry.AccountOfficer;
            rcbSecured.SelectedValue = normalLoanEntry.Secured;
            tbExpectedLoss.Value = (double?)normalLoanEntry.ExpectedLoss;
            tbLossGiven.Value = (double?)normalLoanEntry.LossGivenDef;
            rtbAmountAlloc.Value = (double?)normalLoanEntry.AmountAlloc;
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
            tbExpectedLoss.Enabled = p;
            tbLossGiven.Enabled = p;
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
            string docPath = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/LoanContract/LichTraNoHopDongVay.docx");
            //Open the template document
            Aspose.Words.Document document = new Aspose.Words.Document(docPath);
            //Execute the mail merge.
            var ds = PrepareDataForLoanContractSchedule();
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
            UpdateDataToPriciplePaymentSchedule();

        }

        private void UpdateDataToPriciplePaymentSchedule()
        {
            NormalLoanPaymentSchedule facde = new NormalLoanPaymentSchedule();
            var ds = PrepareDataForLoanContractSchedule();
            DataTable dtInfor = ds.DtInfor;
            DataTable dtItems = ds.DtItems;

            if (dtInfor == null || dtItems == null)
            {
                return;
            }
            facde.DeleteAllScheduleOfContract(dtInfor.Rows[0]["Code"].ToString());

            foreach (DataRow it in dtItems.Rows)
            {
                B_NORMALLOAN_PAYMENT_SCHEDULE princleSchedue = new B_NORMALLOAN_PAYMENT_SCHEDULE();
                princleSchedue.Code = dtInfor.Rows[0]["Code"].ToString();
                princleSchedue.CustomerID = dtInfor.Rows[0]["CustomerID"].ToString();
                princleSchedue.LoanAmount = decimal.Parse((dtInfor.Rows[0]["LoanAmount"].ToString().Replace(",", "")));
                
                if(dtInfor.Rows[0]["Drawdown"]!=null)
                    princleSchedue.Drawdown = (DateTime)dtInfor.Rows[0]["Drawdown"];
                princleSchedue.InterestKey = dtInfor.Rows[0]["InterestKey"].ToString();
                princleSchedue.Freq = dtInfor.Rows[0]["Freq"].ToString();

                princleSchedue.Interest = (Int32)dtInfor.Rows[0]["interest"];


                princleSchedue.Period = (Int16)it["Perios"];

                princleSchedue.DueDate = (DateTime)it["DueDate"];

                princleSchedue.PrincipalAmount = (Decimal)it["Principle"];

                princleSchedue.PrinOS = (Decimal)it["PrinOS"];

                princleSchedue.InterestAmount = (Decimal)it["InterestAmount"];
                princleSchedue.CreateBy = this.UserId;
                princleSchedue.CreateDate = facde.GetSystemDatetime();
                facde.Add(princleSchedue);

            }
            facde.Commit();
        }

        private LoanContractScheduleDS PrepareDataForLoanContractSchedule()
        {
            if (normalLoanEntryM == null)
            {
                normalLoanEntryM = new BNEWNORMALLOAN();
                normalLoanEntryM.Code = tbNewNormalLoan.Text;
                loanBusiness.loadEntity(ref normalLoanEntryM);
            }

            if (normalLoanEntryM == null)
                return null;

            LoanContractScheduleDS dsOut = new LoanContractScheduleDS();

            DataRow drInfor = dsOut.DtInfor.NewRow();
            drInfor[dsOut.Cl_code] = normalLoanEntryM.Code;
            drInfor[dsOut.Cl_custID] = normalLoanEntryM.CustomerID;
            drInfor[dsOut.Cl_cust] = normalLoanEntryM.CustomerName;
            drInfor[dsOut.Cl_drawdown] = normalLoanEntryM.Drawdown;
            drInfor[dsOut.Cl_loadAmount] = normalLoanEntryM.LoanAmount;
            drInfor[dsOut.Cl_interestKey] = "";
            drInfor[dsOut.Cl_interest] = 0;
            dsOut.DtInfor.Rows.Add(drInfor);

            paymentProcess(ref dsOut);
            interestProcess(ref dsOut);



            return dsOut;
        }

        private void interestProcess(ref LoanContractScheduleDS ds)
        {
            NewLoanControlRepository facade = new NewLoanControlRepository();
            BNewLoanControl it = facade.FindLoanControl(normalLoanEntryM.Code, "I+P").
                Union(facade.FindLoanControl(normalLoanEntryM.Code, "I")).FirstOrDefault();//All get Priciple date

            if (it == null || String.IsNullOrEmpty(it.Freq))
                return;

            int freq = 0;
            String rateType = "1";
            DateTime drawdownDate = normalLoanEntryM.Drawdown == null ? (DateTime)normalLoanEntryM.ValueDate : (DateTime)normalLoanEntryM.Drawdown;
            DateTime startDate = (DateTime)normalLoanEntryM.ValueDate;
            DateTime endDate = (DateTime)normalLoanEntryM.MaturityDate;
            DateTime startInterestDate;
            DateTime prevInterestDate = drawdownDate;
            int durationDate = endDate.Subtract(startDate).Days;
            int perios = 0;
            decimal interestedValue = 0;

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

            ds.DtInfor.Rows[0][ds.Cl_interest] = interestedValue;
            ds.DtInfor.Rows[0][ds.Cl_interestKey] = durationDate / 30;


            if (it.Freq.Equals("E"))
            {
                freq = 0;
                startInterestDate = it.Date == null ? (DateTime)endDate : (DateTime)it.Date;
            }
            else
            {
                freq = int.Parse(it.Freq);
                startInterestDate = it.Date == null ? ((DateTime)drawdownDate).AddMonths(freq) : (DateTime)it.Date;
            }


            if (freq > 0)
            {
                perios = durationDate / (freq * 30);
            }
            else
            {
                perios = 1;
            }

            for (int i = 0; i < perios; i++)
            {
                DataRow dr = findInstallmantRow(startInterestDate, ds);
                if (dr != null)
                {
                    dr[ds.Cl_isInterestedRow] = true;
                }
                else
                {
                    dr = ds.DtItems.NewRow();
                    dr[ds.Cl_dueDate] = startInterestDate;
                    dr[ds.Cl_isInterestedRow] = true;
                    dr[ds.Cl_principle] = 0;
                    ds.DtItems.Rows.Add(dr);
                }
                prevInterestDate = startInterestDate;
                startInterestDate = startInterestDate.AddMonths(freq);

                dr[ds.Cl_durationDate] = startInterestDate.Subtract(prevInterestDate).Days;

                if (startInterestDate.Subtract(endDate).Days > 0)
                    startInterestDate = endDate;

            }

            ds.DtItems.DefaultView.Sort = "DueDate asc";
            ds.DtItems = ds.DtItems.DefaultView.ToTable();

            decimal interestAmount = 0;
            decimal currentAmount = (decimal)normalLoanEntryM.LoanAmount;
            for (int i = 0; i < ds.DtItems.Rows.Count; i++)
            {
                DataRow dr = ds.DtItems.Rows[i];
                if (dr["IntestedRow"] != null && (Boolean)dr["IntestedRow"])
                {
                    if (String.IsNullOrEmpty(dr["PrinOS"].ToString()))
                    {
                        dr["PrinOS"] = currentAmount;
                    }
                    interestAmount = int.Parse(dr["duration_day"].ToString()) * ((interestedValue / 36000) * currentAmount);
                    dr["InterestAmount"] = interestAmount;
                }
                else
                {
                    dr["InterestAmount"] = 0;
                }
                dr["Perios"] = i + 1;
                currentAmount = (decimal)dr["PrinOS"];


            }



        }



        private DataRow findInstallmantRow(DateTime date, LoanContractScheduleDS ds)
        {
            DataRow[] drs = ds.DtItems.Select("DueDate = '" + date.ToString() + "'");
            if (drs != null)
            {
                return drs.SingleOrDefault();
            }

            return null;
        }

        private void paymentProcess(ref LoanContractScheduleDS ds)
        {
            int rateType = 1; //default is Fix A/ Periodic --> Du no giam dan. (fix B is du no ban dau)

            if (normalLoanEntryM == null)
                return;

            int numberOfPerios = 0;
            int fregV = 0;
            decimal instalmant = 0;
            decimal instalmantEnd = 0;
            decimal remainAmount = 0;
            DateTime? periosDate = null;
            DateTime? endDate = null;

            rateType = String.IsNullOrEmpty(normalLoanEntryM.RateType) ? 1 : int.Parse(normalLoanEntryM.RateType);

            if (rateType == 1)//du no giam dan
            {


                remainAmount = (decimal)normalLoanEntryM.LoanAmount;
                getPaymentInputControl(ref periosDate, ref endDate, ref numberOfPerios, ref instalmant, ref instalmantEnd, ref fregV);

                ds.DtInfor.Rows[0][ds.Cl_freq] = fregV == 0 ? "Cuối kỳ" : fregV + " Tháng";

                DataRow dr;
                for (int i = 0; i < numberOfPerios; i++)
                {
                    remainAmount = remainAmount - instalmant;
                    dr = ds.DtItems.NewRow();
                    dr[ds.Cl_dueDate] = periosDate;
                    dr[ds.Cl_principle] = instalmant;
                    dr[ds.Cl_PrintOs] = remainAmount;
                    ds.DtItems.Rows.Add(dr);
                    periosDate = ((DateTime)periosDate).AddMonths(fregV);
                }
                dr = ds.DtItems.NewRow();
                dr[ds.Cl_dueDate] = endDate;
                dr[ds.Cl_principle] = instalmantEnd;
                dr[ds.Cl_PrintOs] = 0;
                ds.DtItems.Rows.Add(dr);


            }
            else
            {
                ds.DtInfor.Rows[0][ds.Cl_freq] = "Cuối kỳ";
                NewLoanControlRepository facade = new NewLoanControlRepository();
                BNewLoanControl it = facade.FindLoanControl(normalLoanEntryM.Code, "EP").FirstOrDefault();//All get Priciple date
                if (it != null)
                {
                    instalmantEnd = it.AmountAction == null ? (decimal)normalLoanEntryM.LoanAmount : (decimal)it.AmountAction;
                    endDate = it.Date == null ? normalLoanEntryM.MaturityDate : it.Date;

                }
                DataRow dr = ds.DtItems.NewRow();
                dr[ds.Cl_dueDate] = endDate == null ? normalLoanEntryM.MaturityDate : endDate;
                dr[ds.Cl_principle] = instalmantEnd == 0 ? normalLoanEntryM.LoanAmount : instalmantEnd;
                dr[ds.Cl_PrintOs] = 0;
                ds.DtItems.Rows.Add(dr);

            }

        }


        private void getPaymentInputControl(ref DateTime? periosStartDate, ref DateTime? periosEndDate, ref int numberOfPerios,
            ref decimal instalmant, ref decimal instalmantEnd, ref int freg)
        {
            NewLoanControlRepository facade = new NewLoanControlRepository();
            BNewLoanControl it = facade.FindLoanControl(normalLoanEntryM.Code, "I+P").
                Union(facade.FindLoanControl(normalLoanEntryM.Code, "P")).FirstOrDefault();//All get Priciple date

            if (it != null)
            {
                periosStartDate = it.Date;
                instalmant = it.AmountAction == null ? 0 : (decimal)it.AmountAction;
                numberOfPerios = it.No == null ? 0 : (int)it.No;
                if (!String.IsNullOrEmpty(it.Freq))
                {
                    if (it.Freq.Equals("E"))
                    {
                        freg = 0;
                    }
                    else
                    {
                        freg = int.Parse(it.Freq);
                    }
                }
                else
                {
                    freg = 0;
                }

            }

            if (periosStartDate == null)
            {
                DateTime? drawdown = normalLoanEntryM.Drawdown;
                if (drawdown == null)
                    drawdown = normalLoanEntryM.ValueDate;
                periosStartDate = ((DateTime)drawdown).AddMonths(freg);
            }

            if (numberOfPerios == 0)
            {
                DateTime startDate = (DateTime)normalLoanEntryM.ValueDate;
                DateTime endDate = (DateTime)normalLoanEntryM.MaturityDate;
                int durationLoan = endDate.Subtract(startDate).Days;
                int fregV = freg * 30;



                if (fregV > 0)
                {
                    numberOfPerios = durationLoan / fregV;
                }

            }

            if (instalmant == 0)
            {
                if (numberOfPerios > 0)
                {

                    instalmant = (Int32)(((decimal)normalLoanEntryM.LoanAmount) / numberOfPerios);

                }


            }

            if (numberOfPerios > 0)
            {
                numberOfPerios--;
            }

            it = facade.FindLoanControl(normalLoanEntryM.Code, "EP").FirstOrDefault();
            if (it != null)
            {
                instalmantEnd = it.AmountAction == null ? 0 : (decimal)it.AmountAction;
                periosEndDate = it.Date;
            }

            if (instalmantEnd == 0)
            {
                instalmantEnd = (decimal)normalLoanEntryM.LoanAmount - numberOfPerios * instalmant;
            }

            if (periosEndDate == null)
            {
                periosEndDate = normalLoanEntryM.MaturityDate;
            }


        }



        private DataSet PrepareDate2Print()
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
            BNewLoanControl it = facade.FindLoanControl(normalLoanEntryM.Code, "P").FirstOrDefault();//All get Priciple date

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
                endDate, interestedValue, drawDownDate));

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

        private DataTable GetInterestTableScheduler(string rateType, int numberOfCircle, decimal loanAmount, int circleDuration,
            DateTime startIdentifyIntDate, DateTime endDate, decimal interestValued, DateTime drawdowndate)
        {
            DateTime beginDate = startIdentifyIntDate;
            DateTime previoudDate = drawdowndate;
            DataTable dtItems = new DataTable("Items");
            dtItems.Columns.Add("Perios");
            dtItems.Columns.Add("DueDate");
            dtItems.Columns.Add("Principle");
            dtItems.Columns.Add("InterestAmount");
            dtItems.Columns.Add("PrinOS");

            Int32 moneyPercicle = (Int32)(loanAmount / numberOfCircle);
            decimal pricipleAmount = 0;
            decimal remainLoanAmount = loanAmount;
            decimal interestAmount = 0;
            var schType = "I";//Intallment, N: Non-redemption
            schType = String.IsNullOrEmpty(normalLoanEntryM.RepaySchType) ? "I" : normalLoanEntryM.RepaySchType;

            for (int i = 0; i < numberOfCircle; i++)
            {

                interestAmount = ((beginDate.Subtract(previoudDate).Days * ((interestValued / 36000) * remainLoanAmount)));

                if (schType.Equals("N"))
                {
                    pricipleAmount = 0;
                }
                else
                {
                    remainLoanAmount = remainLoanAmount - moneyPercicle;
                    pricipleAmount = moneyPercicle;
                }

                if (remainLoanAmount < 0)
                {
                    remainLoanAmount = 0;
                }

                if (i == numberOfCircle - 1)
                {
                    if (schType.Equals("N"))
                    {
                        pricipleAmount = remainLoanAmount;
                        remainLoanAmount = 0;
                    }

                    if (remainLoanAmount > 0)
                    {
                        moneyPercicle = (int)(moneyPercicle + remainLoanAmount);
                        interestAmount += ((beginDate.Subtract(previoudDate).Days * ((interestValued / 36000) * remainLoanAmount)));
                        pricipleAmount = moneyPercicle;
                        remainLoanAmount = 0;
                    }

                    if (beginDate > endDate)
                    {
                        beginDate = endDate;
                    }
                }

                var row = dtItems.NewRow();
                row["Perios"] = i + 1;
                row["DueDate"] = beginDate.ToString("dd/MM/yyyy");
                row["Principle"] = pricipleAmount == 0 ? "0" : pricipleAmount.ToString("#,###");
                row["InterestAmount"] = interestAmount.ToString("#,###");
                row["PrinOS"] = remainLoanAmount == 0 ? "0" : (remainLoanAmount.ToString("#,###"));
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
            infoTb.Columns.Add("CustomerID");
            infoTb.Columns.Add("LoanAmount");
            infoTb.Columns.Add("Drawdown");
            infoTb.Columns.Add("InterestKey");
            infoTb.Columns.Add("Freq");
            infoTb.Columns.Add("interest");

            var row = infoTb.NewRow();
            row["Code"] = normalLoanEntryM.Code;
            row["Customer"] = normalLoanEntryM.CustomerName;
            row["CustomerID"] = normalLoanEntryM.CustomerID;
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


        #endregion

    }

    class LoanContractScheduleDS : DataSet
    {
        DataTable dateReport = new DataTable("DateInfor");

        public DataTable DateReport
        {
            get { return dateReport; }
            set { dateReport = value; }
        }
        DataTable dtInfor = new DataTable("Info");

        public DataTable DtInfor
        {
            get { return dtInfor; }
            set { dtInfor = value; }
        }
        DataTable dtItems = new DataTable("Items");

        public DataTable DtItems
        {
            get { return dtItems; }
            set { dtItems = value; }
        }

        DataColumn cl_code = new DataColumn("Code");

        public DataColumn Cl_code
        {
            get { return cl_code; }
            set { cl_code = value; }
        }
        DataColumn cl_cust = new DataColumn("Customer");

        public DataColumn Cl_cust
        {
            get { return cl_cust; }
            set { cl_cust = value; }
        }
        DataColumn cl_custID = new DataColumn("CustomerID");

        public DataColumn Cl_custID
        {
            get { return cl_custID; }
            set { cl_custID = value; }
        }


        DataColumn cl_loadAmount = new DataColumn("LoanAmount", Type.GetType("System.Decimal"));

        public DataColumn Cl_loadAmount
        {
            get { return cl_loadAmount; }
            set { cl_loadAmount = value; }
        }
        DataColumn cl_drawdown = new DataColumn("Drawdown", Type.GetType("System.DateTime"));

        public DataColumn Cl_drawdown
        {
            get { return cl_drawdown; }
            set { cl_drawdown = value; }
        }
        DataColumn cl_interestKey = new DataColumn("InterestKey");

        public DataColumn Cl_interestKey
        {
            get { return cl_interestKey; }
            set { cl_interestKey = value; }
        }
        DataColumn cl_freq = new DataColumn("Freq");

        public DataColumn Cl_freq
        {
            get { return cl_freq; }
            set { cl_freq = value; }
        }
        DataColumn cl_interest = new DataColumn("interest", Type.GetType("System.Int32"));

        public DataColumn Cl_interest
        {
            get { return cl_interest; }
            set { cl_interest = value; }
        }

        DataColumn cl_perious = new DataColumn("Perios", Type.GetType("System.Int16"));

        public DataColumn Cl_perious
        {
            get { return cl_perious; }
            set { cl_perious = value; }
        }
        DataColumn cl_dueDate = new DataColumn("DueDate", Type.GetType("System.DateTime"));

        public DataColumn Cl_dueDate
        {
            get { return cl_dueDate; }
            set { cl_dueDate = value; }
        }
        DataColumn cl_principle = new DataColumn("Principle", Type.GetType("System.Decimal"));

        public DataColumn Cl_principle
        {
            get { return cl_principle; }
            set { cl_principle = value; }
        }
        DataColumn cl_interestAmount = new DataColumn("InterestAmount", Type.GetType("System.Decimal"));

        public DataColumn Cl_interestAmount
        {
            get { return cl_interestAmount; }
            set { cl_interestAmount = value; }
        }
        DataColumn cl_PrintOs = new DataColumn("PrinOS", Type.GetType("System.Decimal"));

        public DataColumn Cl_PrintOs
        {
            get { return cl_PrintOs; }
            set { cl_PrintOs = value; }
        }

        DataColumn cl_isInterestedRow = new DataColumn("IntestedRow", Type.GetType("System.Boolean"));

        public DataColumn Cl_isInterestedRow
        {
            get { return cl_isInterestedRow; }
            set { cl_isInterestedRow = value; }
        }

        DataColumn cl_durationDate = new DataColumn("duration_day");

        public DataColumn Cl_durationDate
        {
            get { return cl_durationDate; }
            set { cl_durationDate = value; }
        }




        public LoanContractScheduleDS()
        {
            dtInfor.Columns.Add(cl_code);
            dtInfor.Columns.Add(cl_cust);
            dtInfor.Columns.Add(cl_custID);
            dtInfor.Columns.Add(cl_loadAmount);
            dtInfor.Columns.Add(cl_drawdown);
            dtInfor.Columns.Add(cl_interestKey);
            dtInfor.Columns.Add(cl_freq);
            dtInfor.Columns.Add(cl_interest);

            dtItems.Columns.Add(cl_perious);
            dtItems.Columns.Add(cl_dueDate);
            dtItems.Columns.Add(cl_principle);
            dtItems.Columns.Add(cl_interestAmount);
            dtItems.Columns.Add(cl_PrintOs);
            dtItems.Columns.Add(cl_isInterestedRow);
            dtItems.Columns.Add(cl_durationDate);

            dateReport.Columns.Add("day");
            dateReport.Columns.Add("month");
            dateReport.Columns.Add("year");
            var rowD = dateReport.NewRow();
            DateTime today = DateTime.Today;
            rowD["day"] = today.ToString("dd");
            rowD["month"] = today.ToString("MM");
            rowD["year"] = today.ToString("yyyy");
            dateReport.Rows.Add(rowD);


        }

    }
}
