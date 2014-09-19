using BankProject.Entity;
using BankProject.Repository;
using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BankProject.Entity.TriTT_Saving;
using BankProject.DataProvider;

namespace BankProject.TellerApplication.CustomerManagement.OpenInvidualCustomer
{
    public partial class Index : PortalModuleBase
    {
        #region Data

        public static string StatusAccount_from_Search_action = "";
        public Index()
        {
            this.cityRepository = new CityRepository();
            this.documentTypeRepository = new DocumentTypeRepository();
            this.sectorRepository = new SectorRepository();
            this.mainSectorRepository = new MainSectorRepository();
            this.targetRepository = new TargetRepository();
            this.customerStatusRepository = new CustomerStatusRepository();
            this.countryRepository = new CountryRepository();
        }

        public CityRepository cityRepository { get; set; }

        public CountryRepository countryRepository { get; set; }

        public CustomerStatusRepository customerStatusRepository { get; set; }

        public DocumentTypeRepository documentTypeRepository { get; set; }

        public MainSectorRepository mainSectorRepository { get; set; }

        public SectorRepository sectorRepository { get; set; }

        public TargetRepository targetRepository { get; set; }
        
        public void LoadCities()
        {
            cmbCity.Items.Clear();
            cmbCity.Items.Add(new RadComboBoxItem("")); ;
            this.cmbCity.DataSource = BankProject.DataProvider.Database.BPROVINCE_GetAll();
            this.cmbCity.DataTextField = "TenTinhThanh";
            this.cmbCity.DataValueField = "MaTinhThanh";
            this.cmbCity.DataBind();
        }
        protected void cmbCity_OnItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView dr = e.Item.DataItem as DataRowView;
            e.Item.Attributes["MaTinhThanh"] = dr["MaTinhThanh"].ToString();
            e.Item.Attributes["TenTinhThanh"] = dr["TenTinhThanh"].ToString();
        }
        public void LoadDocumentTypes()
        {
            this.cmbDocType.DataSource = this.documentTypeRepository.GetAll();
            this.cmbDocType.DataTextField = "Name";
            this.cmbDocType.DataValueField = "Id";
            this.cmbDocType.DataBind();
        }

        public void LoadNationlities()
        {
            this.cmbNationality.DataSource = DataProvider.SQLData.B_BCOUNTRY_GetAll();
            this.cmbNationality.DataValueField = "MaQuocGia";
            this.cmbNationality.DataTextField = "TenTA";
            this.cmbNationality.DataBind();
        }
        
        public void LoadResidences()
        {
            //this.cmbRecidence.DataSource = this.nationalityRepository.GetAll();
            this.cmbRecidence.DataSource = DataProvider.SQLData.B_BCOUNTRY_GetAll();
            this.cmbRecidence.DataValueField = "MaQuocGia";
            this.cmbRecidence.DataTextField = "TenTA";
            this.cmbRecidence.DataBind();
        }
        private void LoadAccountOfficers()
        {
            //this.cmbAccountOfficer.DataSource = this.accountOfficerStatusRepository.GetAll();
            this.cmbAccountOfficer.DataSource = BankProject.DataProvider.SQLData.B_BACCOUNTOFFICER_GetAll();
            this.cmbAccountOfficer.DataValueField = "Code";
            this.cmbAccountOfficer.DataTextField = "Description";
            this.cmbAccountOfficer.DataBind();
        }

        private void LoadCountries()
        {
            this.cmbCountry.DataSource = DataProvider.SQLData.B_BCOUNTRY_GetAll();
            this.cmbCountry.DataValueField = "MaQuocGia";
            this.cmbCountry.DataTextField = "TenTA";
            this.cmbCountry.DataBind();
        }

        private void LoadCustomerStatus()
        {
            this.cmbMaritalStatus.DataSource = this.customerStatusRepository.GetAll();
            this.cmbMaritalStatus.DataTextField = "Name";
            this.cmbMaritalStatus.DataValueField = "Name";
            this.cmbMaritalStatus.DataBind();
        }

        private void LoadMainIndustries()
        {
            DataSet ds = BankProject.DataProvider.Database.BINDUSTRY_GetAll();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["IndustryCode"] = "";
                dr["industryName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            this.cmbMainIndustry.DataSource = ds;
            this.cmbMainIndustry.DataValueField = "IndustryCode";
            this.cmbMainIndustry.DataTextField = "industryName";
            this.cmbMainIndustry.DataBind();
        }

        private void LoadIndustries(string code)// load cho Industry Code
        {
            DataSet ds = BankProject.DataProvider.Database.BINDUSTRY_GetByCoe(code);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["SubIndustryName"] = "";
                dr["SubIndustryCode"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            this.cmbIndustry.DataSource = ds;
            this.cmbIndustry.DataValueField = "SubIndustryCode";
            this.cmbIndustry.DataTextField = "SubIndustryName";
            this.cmbIndustry.DataBind();
        }

        private void LoadMainSectors()
        {
            this.cmbMainSector.DataSource = this.mainSectorRepository.GetAll();
            this.cmbMainSector.DataValueField = "Code";
            this.cmbMainSector.DataBind();
        }

        private void LoadSectors()
        {
            this.cmbSubSector.DataSource = this.sectorRepository.GetAll();
            this.cmbSubSector.DataValueField = "Code";
            this.cmbSubSector.DataBind();
        }

        private void LoadTargets()
        {
            this.cmbTarget.DataSource = this.targetRepository.GetAll();
            this.cmbTarget.DataValueField = "Code";
            this.cmbTarget.DataBind();
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtId.Text = "1" + TriTT.B_BCUSTOMER_GetID("OPEN_CUSTOMER").ToString().PadLeft(6,'0');
                this.txtFaxNumber.Text="";
                if (Request.QueryString["disable"]!= null)
                {
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                }
                this.LoadCities();
                this.LoadNationlities();
                this.LoadResidences();
                this.LoadDocumentTypes();
                this.LoadSectors();
                this.LoadMainSectors();
                this.LoadMainIndustries();
                this.LoadTargets();
                this.LoadCustomerStatus();
                this.LoadAccountOfficers();
                this.LoadCountries();
                this.cmbMainSector.SelectedIndex = 1;
                this.cmbSubSector.SelectedIndex = 1;
                this.LoadToolBar(true);
                LoadOrGenerateDefaultData();
            }
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch(commandName) 
            {
                case "commit":
                    if (CommitSaveAcctoDB())
                    { Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId)); }
                    //BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                break;
                case "preview":
                    string[] param = new string[1];
                    param[0] = "From=OpenIndividualCustomer";
                    Response.Redirect(EditUrl("", "", "Index_ListReview", param));
                break;
                case "authorize":
                if (TriTT.OPEN_INDIVIDUAL_CUSTOMER_CheckDocID_Exists("", "P",txtDocID.Text).Tables[0].Rows[0]["Exists"].ToString() == "YES")
                    {
                        ShowMsgBox("This Doc ID is existed. Please check again !");
                        return;
                    }
                    SavingAccount_SQL.AuthorizeIndividualCustomerAccount(CustomerIDToReview);
                    Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}", TabId, ModuleId));
                break;
                case "reverse":
                    SavingAccount_SQL.ReverseIndividualCustomerAccount(CustomerIDToReview);
                    //LoadDataforCommboBox(); //Load data cho cac commboxs, phuc vu viec hieu chinh thong tin de commit lai
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;// mo button de commit lai
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    cmbSubSector.Enabled = cmbMainSector.Enabled = txtId.Enabled = false;
                break;
            }
        }
        protected void LoadToolBar(bool isauthorize)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void LoadToolBar_AllFalse()
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        //private void LoadToolBar()
        //{
        //    switch (Mode)
        //    {
        //        case "audit":
        //            RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
        //            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
        //            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        //            switch (Status) // danh cho truong hop link tu CUSTOMER ENQUIRY qua
        //            {
        //                case "REV":
        //                case "UNA":
        //                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
        //                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
        //                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true); /// danh cho truong hop audit thong tin, link tu form CUSTOMER ENQUIRY
        //                    break;
        //            }
        //            break;
        //        case "preview":
        //            RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
        //            RadToolBar1.FindItemByValue("btReverse").Enabled = true;
        //            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
        //             // danh cho truong hop link tu CUSTOMER ENQUIRY qua
        //            if (Request.Params.Keys.Get(3) == "status")
        //            {
        //                switch (Status) // danh cho truong hop link tu CUSTOMER ENQUIRY qua
        //                {
        //                    case "AUT":
        //                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
        //                        RadToolBar1.FindItemByValue("btReverse").Enabled = false;
        //                        break;
        //                }
        //            }
        //                    break;
        //        default :
        //            RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
        //            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
        //            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        //            break;
        //    }
        //}
        private void LoadDataforCommboBox()
        {
            this.LoadCities();
            this.LoadNationlities();
            this.LoadResidences();
            this.LoadDocumentTypes();
            this.LoadSectors();
            this.LoadMainSectors();
            this.LoadMainIndustries();
            this.LoadTargets();
            this.LoadCustomerStatus();
            this.LoadAccountOfficers();
            this.LoadCountries();
            //LoadOrGenerateDefaultData();
            this.cmbMainSector.SelectedIndex = 1;
            this.cmbSubSector.SelectedIndex = 1;
        }
        protected void cmbMainIndustry_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadIndustries(cmbMainIndustry.SelectedValue);
        }
#region property
        private SavingAccount_SQL SavingAccount_SQL
        {
            get
            {
                return new SavingAccount_SQL();
            }
        }
        public string Mode
        {
            get
            {
                string mode = string.IsNullOrEmpty(Request.QueryString["mode"]) ? "" : Request.QueryString["mode"].ToLower();
                return mode;
            }
        }
        public string Status
        {
            get
            {
                string status = string.IsNullOrEmpty(Request.QueryString["status"]) ? "AUT" : Request.QueryString["status"];
                return status;
            }
        } 
        public bool DisableForm
        {
            get
            {
                bool disable = false;
                return Boolean.TryParse(Request.QueryString["disable"], out disable) || Mode == "preview";
            }
        }
        public string CustomerIDToReview
        {
            get
            {
                return Request.QueryString["CustomerID"];
            }
        }
#endregion

#region private method
       
        private void LoadOrGenerateDefaultData()
        {
            if (Request.QueryString["CustomerID"] != null)
            {
                txtId.Text = Request.QueryString["CustomerID"].ToString();
                BindDataToControl(Request.QueryString["CustomerID"].ToString());
            }
                //switch (Mode)
                //{
                //    case "preview":
                //        txtId.Text = CustomerIDToReview;
                //        BindDataToControl(CustomerIDToReview);
                //        break;
                //    case "audit": // lam them cho du cac truong` hop cho mode tren URL, k lam` case nay cung dc
                //        txtId.Text = CustomerIDToReview;
                //        BindDataToControl(CustomerIDToReview);
                //        break;
                //    default:
                //        break;
                //}
        }
        private bool CommitSaveAcctoDB()
        {
            var IndividualAccount = new IndividualAccount(); // tao 1 doi tuong co day du properties
            BuildSavingAccount(IndividualAccount); // gan gia tri hien co cho doi tuong
            if (TriTT.OPEN_INDIVIDUAL_CUSTOMER_CheckDocID_Exists("", "P", IndividualAccount.DocID).Tables[0].Rows[0]["Exists"].ToString() == "YES")
            {
                ShowMsgBox("This Doc ID is existed. Please check again !");
                return false;
            }
            if (SavingAccount_SQL.CheckIndividualCustomerExist(IndividualAccount.CustomerID))
            {
                IndividualAccount.AccountOfficer = this.cmbAccountOfficer.SelectedItem.Text;
                return SavingAccount_SQL.UpdateSavingAccount(IndividualAccount);
            }
            else
            {
                IndividualAccount.AccountOfficer = this.cmbAccountOfficer.SelectedItem.Text;
                return SavingAccount_SQL.CreateSavingAccount(IndividualAccount);
            }
        }

      

        private void BuildSavingAccount(IndividualAccount IndividualAcc)
        {
            IndividualAcc.CustomerID = txtId.Text;
            IndividualAcc.Status = AuthoriseStatus.UNA.ToString();
            IndividualAcc.FirstName = txtFirstName.Text;
            IndividualAcc.LastName= txtLastName.Text; 
            IndividualAcc.MiddleName =txtMiddleName.Text;
            IndividualAcc.GBShortName =txtGBShortName.Text;
            IndividualAcc.GBFullName =txtGBFullName.Text;
            IndividualAcc.BirthDay =rdpBirthDay.SelectedDate;
            IndividualAcc.GBStreet =txtStreet.Text;
            IndividualAcc.GBDist =txtGBTownDist.Text;
            IndividualAcc.MobilePhone =txtMobilePhone.Text;
            IndividualAcc.MaTinhThanh =  cmbCity.SelectedValue;
            IndividualAcc.TenTinhThanh = cmbCity.SelectedItem.Text.Replace(IndividualAcc.MaTinhThanh + " - ", "");
            IndividualAcc.CountryCode = cmbCountry.SelectedValue;
            IndividualAcc.CountryName = cmbCountry.SelectedItem.Text.Replace(IndividualAcc.CountryCode + " - ", "");
            IndividualAcc.NationalityCode =cmbNationality.SelectedValue;
            IndividualAcc.NationalityName = cmbNationality.SelectedItem.Text.Replace(IndividualAcc.NationalityCode + " - ", "");

            IndividualAcc.ResidenceCode = cmbNationality.SelectedValue;
            IndividualAcc.ResidenceName = cmbNationality.SelectedItem.Text.Replace(IndividualAcc.ResidenceCode + " - ", "");
            IndividualAcc.DocType =cmbDocType.SelectedItem.Text;
            IndividualAcc.DocID = txtDocID.Text;
            IndividualAcc.DocIssuePlace = txtDocIssuePlace.Text;
            IndividualAcc.DocIssueDate =rdpDocIssueDate.SelectedDate;
            IndividualAcc.DocExpiryDate =rdpDocExpiry.SelectedDate;
            IndividualAcc.SectorCode = cmbMainSector.SelectedValue;
            IndividualAcc.SectorName =cmbMainSector.SelectedItem.Text.Replace(IndividualAcc.SectorCode+"-","");
            IndividualAcc.SubSectorCode =cmbSubSector.SelectedValue;
            IndividualAcc.SubSectorName =cmbSubSector.SelectedItem.Text.Replace(IndividualAcc.SubSectorCode+"-","");
            IndividualAcc.IndustryCode = cmbMainIndustry.SelectedValue;
            IndividualAcc.IndustryName = cmbMainIndustry.Text.Replace(IndividualAcc.IndustryCode+" - ","");
            IndividualAcc.SubIndustryCode = cmbIndustry.SelectedValue;
            IndividualAcc.SubIndustryName = cmbIndustry.Text;
            IndividualAcc.TargetCode = cmbTarget.SelectedValue;
            IndividualAcc.MaritalStatus = cmbMaritalStatus.SelectedValue;
            IndividualAcc.AccountOfficer = cmbAccountOfficer.SelectedValue;
            IndividualAcc.Gender = rcbGender.SelectedValue;
            IndividualAcc.Title = rcbTitle.SelectedValue;
            IndividualAcc.ContactDate = rdpContactDate.SelectedDate;
            IndividualAcc.RelationCode = cmbRelationCode.SelectedValue;
            IndividualAcc.OfficeNumber = txtOfficeNumber.Text;
            IndividualAcc.FaxNumber = txtFaxNumber.Text; 
            IndividualAcc.NoOfDependant = txtNoOfDependants.Text;
            IndividualAcc.NoOfChildUnder15 = txtNoChild15.Text;
            IndividualAcc.NoOfChildUnder25 = txtNoChild15_25.Text;
            IndividualAcc.NoOfchildOver25 = txtNoChild25.Text;
            IndividualAcc.HomeOwnerShip = rcbHomeOwnership.SelectedValue;
            IndividualAcc.ResidenceType = rcbResidenceType.SelectedValue;
            IndividualAcc.EmploymentStatus = rcbEmployeementStatus.SelectedValue;
            IndividualAcc.CompanyName = txtCompanyName.Text;
            IndividualAcc.Currency = cmbCurrency.SelectedValue;
            IndividualAcc.MonthlyIncome = txtMonthlyIncome.Text;
            IndividualAcc.OfficeAddress = txtOfficeAddress.Text;
            IndividualAcc.CustomerLiability = txtCustomerLiability.Text;
            IndividualAcc.ApprovedUser = UserInfo.Username.ToString();
            IndividualAcc.EmailAddress = tbEmailAddress.Text;
        }
        private void BindDataToControl(string CustomerIDToReview)
        {
            if (CustomerIDToReview.StartsWith("2"))
            {
                ShowMsgBox("This is Corporate Customer. Please go to function Open Corporate Customer to view the information.");
                return;
            }
            // ham GetIndividualCustomer_ByID kem theo tham so CustomerIDToReview trong Class SavingAccount_SQL se tra ve 1 doi tuong co cac 
            // Attributes duoc mieu ta trong lop doi tuong Class SavingAccount
            var IndividualCustomer = SavingAccount_SQL.GetIndividualCustomer_ByID(CustomerIDToReview);
            if (IndividualCustomer == null)
            { return; }
            if (IndividualCustomer.Status == AuthoriseStatus.AUT.ToString()) 
            {
                RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                txtMobilePhone.Enabled = false;
                txtOfficeNumber.Enabled = false;
                txtFaxNumber.Enabled = false;
            }   
            // neu doi tuong khong null thi gan du lieu vao cac attributes cua IndividualCustomer 
            txtId.Text= IndividualCustomer.CustomerID;
            txtFirstName.Text = IndividualCustomer.FirstName;
            txtLastName.Text  = IndividualCustomer.LastName;
            txtMiddleName.Text = IndividualCustomer.MiddleName;
            txtGBShortName.Text = IndividualCustomer.GBShortName;
            txtGBFullName.Text = IndividualCustomer.GBFullName;
            rdpBirthDay.SelectedDate = IndividualCustomer.BirthDay;
            txtStreet.Text = IndividualCustomer.GBStreet;
            txtGBTownDist.Text = IndividualCustomer.GBDist;
            txtMobilePhone.Text = IndividualCustomer.MobilePhone;
            cmbCity.SelectedValue =IndividualCustomer.MaTinhThanh;
            cmbCity.SelectedItem.Text = cmbCity.SelectedValue + " - " + IndividualCustomer.TenTinhThanh;
            cmbCountry.SelectedValue = IndividualCustomer.CountryCode;
            cmbCountry.Text =IndividualCustomer.CountryCode+" - "+ IndividualCustomer.CountryName;

            cmbNationality.SelectedValue = IndividualCustomer.NationalityCode;
            cmbNationality.Text =IndividualCustomer.NationalityCode+" - "+ IndividualCustomer.NationalityName; 
            cmbRecidence.SelectedValue = IndividualCustomer.ResidenceCode;
            cmbNationality.Text = IndividualCustomer.ResidenceCode+" - "+IndividualCustomer.ResidenceName;
            cmbDocType.SelectedItem.Text = IndividualCustomer.DocType;
            txtDocID.Text = IndividualCustomer.DocID;
            txtDocIssuePlace.Text = IndividualCustomer.DocIssuePlace;
            rdpDocIssueDate.SelectedDate = IndividualCustomer.DocIssueDate;
            rdpDocExpiry.SelectedDate = IndividualCustomer.DocExpiryDate;
            //cmbMainSector.SelectedValue = IndividualCustomer.SectorCode;
            cmbMainSector.Text = IndividualCustomer.SectorCode+" - "+IndividualCustomer.SectorName;
            //cmbSubSector.SelectedValue = IndividualCustomer.SubSectorCode;
            cmbSubSector.Text=IndividualCustomer.SubSectorCode+" - "+ IndividualCustomer.SubSectorName;

            cmbMainIndustry.SelectedValue = IndividualCustomer.IndustryCode;
            cmbMainIndustry_SelectedIndexChanged(cmbMainIndustry, null);
            cmbMainIndustry.SelectedItem.Text =IndividualCustomer.IndustryCode+" - "+ IndividualCustomer.IndustryName;
            cmbIndustry.SelectedValue = IndividualCustomer.SubIndustryCode;
            cmbIndustry.SelectedItem.Text = IndividualCustomer.SubIndustryName;
            cmbTarget.SelectedValue = IndividualCustomer.TargetCode;
            cmbMaritalStatus.SelectedValue = IndividualCustomer.MaritalStatus;
            cmbAccountOfficer.SelectedItem.Text= IndividualCustomer.AccountOfficer;
            rcbGender.SelectedValue = IndividualCustomer.Gender;
            rcbTitle.SelectedValue = IndividualCustomer.Title;
            rdpContactDate.SelectedDate = IndividualCustomer.ContactDate;
            cmbRelationCode.SelectedValue = IndividualCustomer.RelationCode;
            txtOfficeNumber.Text = IndividualCustomer.OfficeNumber;

            txtFaxNumber.Text = IndividualCustomer.FaxNumber;
            txtNoOfDependants.Text = IndividualCustomer.NoOfDependant;
            txtNoChild15.Text = IndividualCustomer.NoOfChildUnder15;
            txtNoChild15_25.Text = IndividualCustomer.NoOfChildUnder25;
            txtNoChild25.Text = IndividualCustomer.NoOfchildOver25;
            rcbHomeOwnership.SelectedValue = IndividualCustomer.HomeOwnerShip;
            rcbResidenceType.SelectedValue = IndividualCustomer.ResidenceType;
            rcbEmployeementStatus.SelectedValue = IndividualCustomer.EmploymentStatus;
            txtCompanyName.Text = IndividualCustomer.CompanyName;
            cmbCurrency.SelectedValue = IndividualCustomer.Currency;
            txtMonthlyIncome.Text = IndividualCustomer.MonthlyIncome;
            txtOfficeAddress.Text = IndividualCustomer.OfficeAddress;
            txtCustomerLiability.Text = IndividualCustomer.CustomerLiability;
            tbEmailAddress.Text = IndividualCustomer.EmailAddress;
            StatusAccount_from_Search_action = IndividualCustomer.Status;
            if (IndividualCustomer.Status == "AUT")
            {
                imgSignature.ImageUrl = "";
                lnkSignature.NavigateUrl = "#";
                if (IndividualCustomer.Signatures != null)
                {
                    imgSignature.ImageUrl = "~/" + BankProject.DataProvider.Customer.SignaturePath + "/" + IndividualCustomer.Signatures;
                    lnkSignature.NavigateUrl = imgSignature.ImageUrl;
                }
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                LoadToolBar_AllFalse();
                return;
            }
            LoadToolBar(false);
        }
    
#endregion

        protected void btSearch_Click(object sender, EventArgs e)
        {
            BindDataToControl(txtId.Text);
            switch (StatusAccount_from_Search_action)
            {
                case "REV":
                case "UNA":
                    //BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                    break;
                case "AUT":
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                    break;
            }
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