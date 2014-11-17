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
using DotNetNuke.Entities.Tabs;



namespace BankProject.Views.TellerApplication
{
    public partial class NewNormalLoan_Repayment : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        INewNormalLoanBusiness<BNEWNORMALLOAN> loanBusiness;
        BNEWNORMALLOAN normalLoanEntryM;
        bool isApprovalRole = false;
        public double remainLoanAmountDis = 0;
        private string REFIX_MACODE = "LD";

        protected void Page_Load(object sender, EventArgs e)
        {
            //var test = TabController.CurrentPage.ContentKey;
            if (Request.Params["role"] != null)
            {
                if (Request.Params["role"].Equals("authorize"))
                {
                    isApprovalRole = true;
                }
            }

            loanBusiness = new NewNormalLoanRepaymentBusiness();
            if (Request.Params["codeid"] != null)
            {
                tbNewNormalLoan.Text = Request.Params["codeid"];
            }

            if (!IsPostBack)
            {

                normalLoanEntryM = new BNEWNORMALLOAN();
                normalLoanEntryM.Code = tbNewNormalLoan.Text;
                loanBusiness.loadEntity(ref normalLoanEntryM);
                init();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickMainTab();", true);
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
                    updateRepaymentAmount(normalLoanEntryM, (decimal)tbOutstandingAmount.Value);

                    this.Response.Redirect("Default.aspx?tabid=" + this.TabId);

                    break;


                case "Preview":
                    this.Response.Redirect(EditUrl("process", "repayment", "preview"));
                    break;

                case "authorize":
                    BindField2Data(ref normalLoanEntryM);
                    loanBusiness.Entity = normalLoanEntryM;
                    updateRepaymentAmount(normalLoanEntryM, (decimal)tbOutstandingAmount.Value);
                    loanBusiness.authorizeProcess(this.UserId);                   
                    UpdateSchedulePaymentToDB();
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


        //protected void rcbCustomerID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{

        //    LoadLimitReferenceInfor(rcbCustomerID.SelectedValue, null);
        //    LoadAllAccount(rcbCustomerID.SelectedValue, rcbCurrency.SelectedValue, null, null, null, null);
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "clickMainTab();", true);
        //}


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
            item.PeriodRepaid = int.Parse(hfRepaymentTimes.Value);
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
            //LoadDataTolvLoanDisbursalSchedule();
            processApproriateAction();
        }




        protected void btnPrintLai_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrintVon_Click(object sender, EventArgs e)
        {

        }

        protected void tbCustID_TextChanged(object sender, EventArgs e)
        {
            LoadCustomerInformation(tbCustID.Text);        
            LoadLimitReferenceInfor(tbCustID.Text, null);
            LoadAllAccount(tbCustID.Text, rcbCurrency.SelectedValue, null, null, null, null);
        }

        

        #endregion

        #region Common Function

        private void LoadCustomerInformation(string SelectedCus)
        {
            BCustomerRepository facade1 = new BCustomerRepository();
            var db = facade1.getCustomerInfo(SelectedCus, "AUT");

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

        private void processApproriateAction()
        {
            if (normalLoanEntryM == null || String.IsNullOrEmpty(normalLoanEntryM.Code))
            {
                UpdateStatusAuthorizeAction(false, true, false, false, false, false);
                SetEnabledControls(false);
                return;
            }

            UpdateStatusAuthorizeAction(true, false, false, false, false, false);

            bool isUnAuthorize = (normalLoanEntryM.Status == null)
                || "UNA".Equals(normalLoanEntryM.Status)
                || "REV".Equals(normalLoanEntryM.Status);
            
            bool isReverse = "REV".Equals(normalLoanEntryM.Amend_Status);
            bool isAuthorize = "AUT".Equals(normalLoanEntryM.Status);

            if (!isAuthorize)//trang thai authorize
            {
                UpdateStatusAuthorizeAction(false, false, false, false, false, false);
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
        private void init()
        {
            LoadMainCategoryCombobox(null);
            LoadPurposeCode(null);
            LoadGroup(null);
            LoadInterestKey(null);
            LoadBusinessDay(null);


            //Load data and binding it to UI
            loanBusiness.loadEntity(ref normalLoanEntryM);
            BindData2Field(normalLoanEntryM);

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
            Util.LoadData2RadCombo(rcbDepositeRate, src, "Id", "LoanInterest_Key", "-Select Interest Key-", true);

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
        //private void LoadCustomerCombobox(string SelectedCus)
        //{
        //    BCustomerRepository facade1 = new BCustomerRepository();
        //    var db = facade1.getCustomerList("AUT");
        //    List<BCUSTOMER_INFO> hh = db.ToList<BCUSTOMER_INFO>();
        //    Util.LoadData2RadCombo(rcbCustomerID, hh, "CustomerID", "ID_FullName", "-Select Customer Code-", false);


        //    if (!String.IsNullOrEmpty(SelectedCus))
        //    {
        //        rcbCustomerID.SelectedValue = SelectedCus;
        //    }

        //}
        

        void LoadAllAccount(string custID, string currency, string credit, string printRep, string inRep, string chagreRep)
        {

            StoreProRepository storeFacade = new StoreProRepository();
            var ds = storeFacade.StoreProcessor().BOPENACCOUNT_LOANACCOUNT_GetByCode(custID, currency).ToList();


            //Database.BOPENACCOUNT_LOANACCOUNT_GetByCode(name, currency);
            Util.LoadData2RadCombo(rcbCreditToAccount, ds, "Id", "Display", "-Select a credit Account-", true);

            if (!String.IsNullOrEmpty(credit))
            {
                rcbCreditToAccount.SelectedValue = credit;
            }



        }
        private void LoadDataTolvLoanControl()
        {

            NewLoanControlRepository facade = new NewLoanControlRepository();
            var db = facade.FindLoanControlByCode(tbNewNormalLoan.Text, int.Parse(hfRepaymentTimes.Value));
            lvLoanControl.DataSource = db.ToList();
            lvLoanControl.DataBind();

        }

        private void LoadDataTolvLoanDisbursalSchedule()
        {
            CalcRemainDisbursalLoanAmount();
            LoanDisbursalScheduleRespository facade = new LoanDisbursalScheduleRespository();
            var db = facade.FindLoanDisbursalByCode(tbNewNormalLoan.Text);



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

            NormalLoanRepository facase = new NormalLoanRepository();
            if (!String.IsNullOrWhiteSpace(tbNewNormalLoan.Text))
            {
                normalLoanEntry = facase.GetById(tbNewNormalLoan.Text);
            }


        }
        private void BindData2Field(BNEWNORMALLOAN normalLoanEntry)
        {
            if (normalLoanEntry == null)
            {
                normalLoanEntry = new BNEWNORMALLOAN();
                normalLoanEntry.Code = tbNewNormalLoan.Text;
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
            rcbRateType.SelectedValue = normalLoanEntry.RateType;
            rcbDepositeRate.SelectedValue = normalLoanEntry.InterestKey;
            tbInterestRate.Value = (double?)normalLoanEntry.InterestRate;
            tbInSpread.Value = double.Parse(!String.IsNullOrEmpty(normalLoanEntry.IntSpread) ? normalLoanEntry.IntSpread : "0");
            rcbBusDay.SelectedValue = normalLoanEntry.BusDayDef;
            rcbDefineSch.SelectedValue = normalLoanEntry.DefineSch;
            lbLoanStatus.Text = normalLoanEntry.LoanStatus;
            lbTotalInterestAmt.Text = "" + normalLoanEntry.TotalInterestAmt;
            lbPDStatus.Text = normalLoanEntry.PDStatus;
            hfRepaymentTimes.Value = (normalLoanEntry.RepaymentTimes + 1) + "";
            LoadDataTolvLoanControl();
            LoadRemainLoanAmount();
            


            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Dis", "LoadDrawdown();", true);
        }

        private void LoadRemainLoanAmount()
        {
            StoreProRepository facade = new StoreProRepository();
            CashRepaymentRepository cashFacade = new CashRepaymentRepository();
            double remainAmount = 0;
            var amt = facade.StoreProcessor().B_Normal_Loan_GetRemainLoanAmount(tbNewNormalLoan.Text).FirstOrDefault();
            remainAmount = amt == null ? 0 : (double)amt;

            if(normalLoanEntryM!=null && !String.IsNullOrEmpty(normalLoanEntryM.CreditAccount)){
                var cashRepay = cashFacade.FindActiveCashRepayment(normalLoanEntryM.CreditAccount).FirstOrDefault();

                if (cashRepay != null && cashRepay.AmountDeposited != null)
                {
                    remainAmount = remainAmount - (cashRepay.AmountDeposited == null ? 0 : (double)cashRepay.AmountDeposited);
                }

            }

            tbOutstandingAmount.Value = remainAmount;

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

            rcbBusDay.Enabled = p;
            tbOutstandingAmount.Enabled = false;
            lvLoanControl.Enabled = p;

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
            SetEnabledControls(false);
            if (normalLoanEntryM != null && !String.IsNullOrEmpty(normalLoanEntryM.Code))
            {
                lvLoanControl.Enabled = allowCommit;
            }
            else
            {
                lvLoanControl.Enabled = false;
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
            loanBusiness.UpdateDataToPriciplePaymentSchedule(normalLoanEntryM, int.Parse(hfRepaymentTimes.Value), this.UserId);

        }

        private void updateRepaymentAmount(BNEWNORMALLOAN loan, decimal newAmount)
        {
            NormalLoanRepaymentRepository facade = new NormalLoanRepaymentRepository();
            BNEWNORMALLOAN_REPAYMENT existLoanRepay = facade.FindRepaymentAmount(loan.Code, int.Parse(hfRepaymentTimes.Value)).FirstOrDefault();

            if (existLoanRepay != null)
            {
                BNEWNORMALLOAN_REPAYMENT existLoanRepayOld = facade.FindRepaymentAmount(loan.Code, int.Parse(hfRepaymentTimes.Value)).FirstOrDefault();
                existLoanRepay.LoanAmount = newAmount;
                facade.Update(existLoanRepayOld, existLoanRepay);
            }
            else
            {
                existLoanRepay = new BNEWNORMALLOAN_REPAYMENT();
                existLoanRepay.RepaymentTimes = int.Parse(hfRepaymentTimes.Value);
                existLoanRepay.ActivatedDate = DateTime.Now;
                existLoanRepay.LoanAmount = newAmount;
                existLoanRepay.Code = loan.Code;
                facade.Add(existLoanRepay);
            }
            facade.Commit();

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

            return loanBusiness.PrepareDataForLoanContractSchedule(normalLoanEntryM, int.Parse(hfRepaymentTimes.Value));
        }

        #endregion

    }
   
}
