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
using BankProject.DataProvider;

namespace BankProject
{
    public partial class OpenCorpCustomer : PortalModuleBase
    {
        #region Page_Load
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["CustomerID"] != null)
                {
                    LoadDataForCombobox(); // chuan bi du lieu cho form 
                    LoadDataToReview(Request.QueryString["CustomerID"].ToString());
                    string mode = Request.QueryString["mode"].ToString();
                    switch (mode)
                    {
                        case "preview":
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                            txtTelephone.Enabled = false;
                            txtOfficeNumber.Enabled = false;
                            break;
                        case "audit": //cho phep hien va chinh sua thong tin cua ACC tu` form CUSTOMER ENQUIRY link qua ,co status != AUT,  
                            LoadToolBar(false); // hien button authorize, reverse 
                            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                            break;
                    }
                }
                else
                { 
                    FirstLoad(); 
                }
            }
        }
        #endregion
        #region Property
        public OpenCorpCustomer()
        {
            this.documentTypeRepository = new DocumentTypeRepository();
            this.targetRepository = new TargetRepositoryCorp();
            this.customerStatusRepository = new CustomerStatusRepository();
        }
        private void LoadCountries()
        {
            cmbCountry.Items.Clear();
            cmbCountry.Items.Add(new RadComboBoxItem(""));
            this.cmbCountry.DataSource = SQLData.B_BCOUNTRY_GetAll();
            cmbCountry.DataTextField = "TenTA";
            cmbCountry.DataValueField = "MaQuocGia";
            this.cmbCountry.DataBind();
        }
        public void LoadNationlities()
        {
            this.cmbNationality.DataSource = SQLData.B_BCOUNTRY_GetAll();
            this.cmbNationality.DataValueField = "MaQuocGia";
            this.cmbNationality.DataTextField ="TenTA";
            this.cmbNationality.DataBind();
        }
        public void LoadResidences()
        {
            this.cmbRecidence.DataSource = SQLData.B_BCOUNTRY_GetAll();
            this.cmbRecidence.DataValueField = "MaQuocGia";
            this.cmbRecidence.DataTextField = "TenTA";
            this.cmbRecidence.DataBind();
        }

        public CustomerStatusRepository customerStatusRepository { get; set; }

        public DocumentTypeRepository documentTypeRepository { get; set; }

        public TargetRepositoryCorp targetRepository { get; set; }

        public void LoadCities()
        {
            cmbCity.Items.Clear();
            cmbCity.Items.Add(new RadComboBoxItem(""));
            this.cmbCity.DataSource = BankProject.DataProvider.Database.BPROVINCE_GetAll();
            this.cmbCity.DataTextField = "TenTinhThanh";
            this.cmbCity.DataValueField = "MaTinhThanh";
            this.cmbCity.DataBind();
        }

        public void LoadDocumentTypes()
        {
            this.cmbDocType.DataSource = this.documentTypeRepository.GetAll();
            this.cmbDocType.DataValueField = "Id";
            this.cmbDocType.DataTextField = "Name";
            this.cmbDocType.DataBind();

            this.cmbInVisibleDocType.DataSource = this.documentTypeRepository.GetAll();
            this.cmbInVisibleDocType.DataValueField = "Id";
            this.cmbInVisibleDocType.DataTextField = "Name";
            this.cmbInVisibleDocType.DataBind();
        }
        
        private void LoadAccountOfficers()
        {
            this.cmbAccountOfficer.DataSource = BankProject.DataProvider.SQLData.B_BACCOUNTOFFICER_GetAll();
            this.cmbAccountOfficer.DataValueField = "Code";
            this.cmbAccountOfficer.DataTextField = "description";
            this.cmbAccountOfficer.DataBind();
        }

        private void LoadMainIndustries()
        {
            DataSet ds = BankProject.DataProvider.Database.BINDUSTRY_GetAll();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["IndustryCode"] = "";
                dr["IndustryName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            this.cmbMainIndustry.DataSource = ds;
            this.cmbMainIndustry.DataValueField = "IndustryCode";
            this.cmbMainIndustry.DataTextField = "IndustryName";
            this.cmbMainIndustry.DataBind();
        }

        private void LoadIndustries(string code)
        {
            cmbIndustry.Items.Clear();
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
            this.cmbMainSector.DataSource = BankProject.DataProvider.Database.BSECTOR_GetAll();
            this.cmbMainSector.DataTextField = "SECTORNAME";
            this.cmbMainSector.DataValueField = "SECTORCODE";
            this.cmbMainSector.DataBind();
        }

        private void LoadSectors(string code)
        {
            cmbSector.Items.Clear();
            DataSet ds = Database.BSECTOR_GetByCode(code);
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["SUBSECTORNAME"] = "";
                dr["SUBSECTORCODE"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            this.cmbSector.DataSource = ds;
            cmbSector.DataValueField = "SUBSECTORCODE";
            cmbSector.DataTextField = "SUBSECTORNAME";
            cmbSector.DataBind(); 
        }

        private void LoadTargets()
        {
            this.cmbTarget.DataSource = this.targetRepository.GetAll();
            this.cmbTarget.DataValueField = "Code";
            this.cmbTarget.DataTextField = "Name";
            this.cmbTarget.DataBind();
        }
        #endregion
        #region Events
        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "commit":
                    //if (hdfDisable.Value == "0") return;       xem lai 
                    var Status = "UNA";
                    TriTT.OPEN_CORPORATE_CUSTOMER_Insert_Account(txtId.Text, Status, txtGBShortName.Text, txtGBFullName.Text, rdpIncorpDate.SelectedDate, txtGBStreet.Text
                , txtGBDist.Text, cmbCity.SelectedValue, cmbCity.Text.Replace(cmbCity.SelectedValue + " - ", "")
                , cmbCountry.SelectedValue, cmbCountry.Text.Replace(cmbCountry.SelectedValue + " - ", ""), cmbNationality.SelectedValue, cmbNationality.Text.Replace(cmbNationality.SelectedValue + " - ", "")
                , cmbRecidence.SelectedValue, cmbRecidence.Text.Replace(cmbRecidence.SelectedValue + " - ", ""), cmbDocType.Text, txtDocID.Text, txtDocIssuePlace.Text
                , rdpDocIssueDate.SelectedDate, rdpDocExpiryDate.SelectedDate, txtContactPerson.Text, txtPosition.Text, txtTelephone.Text, txtEmailAddress.Text, txtRemarks.Text
                , cmbMainSector.SelectedValue, cmbMainSector.Text, cmbSector.SelectedValue, cmbSector.Text, cmbMainIndustry.SelectedValue, cmbMainIndustry.Text, cmbIndustry.SelectedValue, cmbIndustry.Text
                , cmbTarget.SelectedValue, cmbAccountOfficer.Text, rdpContactDate.SelectedDate, cmbRelationCode.SelectedValue, txtOfficeNumber.Text
                , txtTotalCapital.Text, txtNoOfEmployee.Text, txtTotalAssets.Text, txtTotalRevenue.Text, txtCustomerLiability.Text, txtLegacyRef.Text, UserInfo.Username.ToString());
                    BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                    Response.Redirect("Default.aspx?tabid=256&mid=744");
                    ClearComboBox();
                    break;
                case "Preview":
                    string[] param = new string[1];
                    param[0] = "From=OpenCorporateCustomer";
                    Response.Redirect(EditUrl("", "", "ListReview_SourceOf_OpenIndividualCustomer", param));
                    break;
                case "authorize":
                    Status = "AUT";
                    TriTT.OPEN_CORPORATE_CUSTOMER_Authorize_Account(txtId.Text, Status);
                    Response.Redirect("Default.aspx?tabid=256&mid=744");
                    break;
                case "reverse":
                    Status = "REV";
                    TriTT.OPEN_CORPORATE_CUSTOMER_Authorize_Account(txtId.Text, Status); //up date status REV
                    //Response.Redirect(string.Format("Default.aspx?tabid={0}&mid={1}&CustomerID={3}&disable=true&mode=reverse", TabId, ModuleId, txtId.Text));
                    LoadToolBar(true); // Enable button commit data
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true); //mo du lieu cho phep chinh sua
                    txtId.Enabled = false;  // khong cho hieu chinh Ma khach hang
                    break;
                case "search":
                    LoadDataToReview_Search_tai_Form(txtId.Text);
                    //BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    break;
            }
        }
        private void LoadDataToReview(string CustomerID)
        {
            var Status_for_review_Or_audit = "UNA";
           
            ///doan code (1) de chon Load nhung ACC chua AUT tu List_preview hoac load ACC tuy theo status duoc link tu CUstomer ENQUIRY
            if  (Request.Params.Keys.Get(3) == "status" &&  Request.QueryString["status"].ToString() != "UNA") 
            { Status_for_review_Or_audit = Request.QueryString["status"].ToString(); } 
            //end doan code (1) Load For Form CUSTOMER ENQUIRY , Phan loai Status de load Acc Customer

            DataSet ds = TriTT.OPEN_CORPORATE_CUSTOMER_review_Account(CustomerID, Status_for_review_Or_audit, "C", "2");
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            { 
               txtId.Text= CustomerID; 
               txtGBShortName.Text =ds.Tables[0].Rows[0]["GBShortName"].ToString();
               txtGBFullName.Text= ds.Tables[0].Rows[0]["GBFullName"].ToString();
               if (ds.Tables[0].Rows[0]["IncorpDate"].ToString() != "")
               {
                   rdpIncorpDate.SelectedDate =Convert.ToDateTime(ds.Tables[0].Rows[0]["IncorpDate"].ToString());
               }
                txtGBStreet.Text = ds.Tables[0].Rows[0]["GBStreet"].ToString();
                txtGBDist.Text = ds.Tables[0].Rows[0]["GBDist"].ToString();
                cmbCity.SelectedValue = ds.Tables[0].Rows[0]["MaTinhThanh"].ToString();
                cmbCity.Text=cmbCity.SelectedValue+" - "+ ds.Tables[0].Rows[0]["TenTinhThanh"].ToString();
                cmbCountry.SelectedValue = ds.Tables[0].Rows[0]["CountryCode"].ToString();
                cmbCountry.Text=cmbCountry.SelectedValue+" - "+ ds.Tables[0].Rows[0]["CountryName"].ToString();
                /////////////////////
                cmbNationality.SelectedValue = ds.Tables[0].Rows[0]["NationalityCode"].ToString();
                cmbNationality.Text=cmbNationality.SelectedValue+" - "+ ds.Tables[0].Rows[0]["NationalityName"].ToString();
                cmbRecidence.SelectedValue = ds.Tables[0].Rows[0]["ResidenceCode"].ToString();
                cmbRecidence.Text=cmbRecidence.SelectedValue+" - "+ ds.Tables[0].Rows[0]["ResidenceName"].ToString();
                cmbDocType.SelectedItem.Text =  ds.Tables[0].Rows[0]["DocType"].ToString();
                txtDocID.Text =  ds.Tables[0].Rows[0]["DocID"].ToString();
                txtDocIssuePlace.Text = ds.Tables[0].Rows[0]["DocIssuePlace"].ToString();
                if (ds.Tables[0].Rows[0]["DocIssueDate"].ToString() != "")
                {
                    rdpDocIssueDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DocIssueDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DocExpiryDate"].ToString() != "")
                {
                    rdpDocExpiryDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DocExpiryDate"].ToString());
                }
                txtContactPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                txtPosition.Text = ds.Tables[0].Rows[0]["Position"].ToString();
                txtTelephone.Text = ds.Tables[0].Rows[0]["Telephone"].ToString();
                txtEmailAddress.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                cmbMainSector.SelectedValue= ds.Tables[0].Rows[0]["SectorCode"].ToString();
                cmbMainSector.Text = cmbMainSector.SelectedValue+" - "+ ds.Tables[0].Rows[0]["SectorName"].ToString();
                cmbMainSector_SelectedIndexChanged(cmbMainSector,null);
                cmbSector.SelectedValue= ds.Tables[0].Rows[0]["SubSectorCode"].ToString();
                cmbSector.Text = cmbSector.SelectedValue+" - "+ ds.Tables[0].Rows[0]["SubSectorName"].ToString();

                cmbMainIndustry.SelectedValue= ds.Tables[0].Rows[0]["IndustryCode"].ToString();
                cmbMainIndustry.Text = cmbMainIndustry.SelectedValue+" - "+ ds.Tables[0].Rows[0]["IndustryName"].ToString();
                cmbMainIndustry_SelectedIndexChanged(cmbMainIndustry,null);
                cmbIndustry.SelectedValue= ds.Tables[0].Rows[0]["SubIndustryCode"].ToString();
                cmbIndustry.Text = cmbIndustry.SelectedValue+" - "+ ds.Tables[0].Rows[0]["SubIndustryName"].ToString();
                cmbTarget.SelectedValue = ds.Tables[0].Rows[0]["TargetCode"].ToString();
                cmbAccountOfficer.SelectedItem.Text = ds.Tables[0].Rows[0]["AccountOfficer"].ToString();
                string date = ds.Tables[0].Rows[0]["ContactDate"].ToString();
                if (date != "") 
                {
                    rdpContactDate.SelectedDate = Convert.ToDateTime(date); 
                }
                cmbRelationCode.SelectedValue =  ds.Tables[0].Rows[0]["RelationCode"].ToString();
                txtOfficeNumber.Text = ds.Tables[0].Rows[0]["OfficeNumber"].ToString();
                txtTotalCapital.Text = ds.Tables[0].Rows[0]["TotalCapital"].ToString();
                txtNoOfEmployee.Text = ds.Tables[0].Rows[0]["NoOfEmployee"].ToString();
                txtTotalAssets.Text = ds.Tables[0].Rows[0]["TotalAssets"].ToString();
                txtTotalRevenue.Text = ds.Tables[0].Rows[0]["TotalRevenue"].ToString();
                txtCustomerLiability.Text = ds.Tables[0].Rows[0]["CustomerLiability"].ToString();
                txtLegacyRef.Text = ds.Tables[0].Rows[0]["LegacyRef"].ToString();
            }
        }
        #endregion
        #region Help Methods
        private void FirstLoad()
        {
            this.txtId.Text = "2" + TriTT.B_BCUSTOMER_GetID_Corporate("B_BCUSTOMER_GetID_Corporate", "OPEN_CORPORATE").PadLeft(6, '0');
            this.divDocType.Visible = false;
            this.LoadCities();
            this.LoadCountries();
            this.LoadNationlities();
            this.LoadResidences();
            this.LoadDocumentTypes();// load luon data cho truong cmbInVisibleDocType
            this.LoadMainSectors();
            this.LoadMainIndustries();
            this.LoadTargets();
            //this.LoadCustomerStatus();
            this.LoadAccountOfficers();
            this.LoadToolBar(true);
           // cmbCustomerStatus.Enabled = false;
        }
        private void LoadToolBar(bool enable)
        {

            RadToolBar1.FindItemByValue("btCommitData").Enabled = enable;
            RadToolBar1.FindItemByValue("btPreview").Enabled = enable;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = !enable;
            RadToolBar1.FindItemByValue("btReverse").Enabled = !enable;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        private void LoadDataForCombobox() // when review, co gia tri de set selected VAlues
        {
            divDocType.Visible = false;
            this.LoadCities();
            this.LoadCountries();
            this.LoadNationlities();
            this.LoadResidences();
            this.LoadDocumentTypes();// load luon data cho truong cmbInVisibleDocType
            this.LoadMainSectors();
            this.LoadMainIndustries();
            this.LoadTargets();
            //this.LoadCustomerStatus();
            this.LoadAccountOfficers();
        }
        protected void btThem_Click(object sender, ImageClickEventArgs e)
        {
            divDocType.Visible = false;
            ////divChargeInfo2.Visible = true;
        }
        protected void cmbMainSector_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSectors(cmbMainSector.SelectedValue);
        }
        private void ClearComboBox()
        {
            cmbCity.Text = "" ;
            cmbDocType.Text = "";
            cmbSector.Text = "";
            cmbMainIndustry.Text = "";
        }

        protected void cmbMainIndustry_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadIndustries(cmbMainIndustry.SelectedValue);
        }
        protected void btSearch_Click(object sender, EventArgs e)
        {
            LoadDataToReview_Search_tai_Form(txtId.Text);
            switch (bientoancuc.StatusAccount_from_Search_action)
            { 
                case "REV":
                case "UNA":
                    //BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    LoadToolBar(true);
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

        protected class bientoancuc
        {
            public static string StatusAccount_from_Search_action;
        }

        private void LoadDataToReview_Search_tai_Form(string CustomerID)  //load customer ACcount khi search tai form
        {
            DataSet ds = TriTT.OPEN_CORPORATE_CUSTOMER_review_Account_search_tai_Form(CustomerID,"C");
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                txtId.Text = CustomerID;
                txtGBShortName.Text = ds.Tables[0].Rows[0]["GBShortName"].ToString();
                txtGBFullName.Text = ds.Tables[0].Rows[0]["GBFullName"].ToString();
                if (ds.Tables[0].Rows[0]["IncorpDate"].ToString() != "")
                {
                    rdpIncorpDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["IncorpDate"].ToString());
                }
                txtGBStreet.Text = ds.Tables[0].Rows[0]["GBStreet"].ToString();
                txtGBDist.Text = ds.Tables[0].Rows[0]["GBDist"].ToString();
                cmbCity.SelectedValue = ds.Tables[0].Rows[0]["MaTinhThanh"].ToString();
                cmbCity.Text = cmbCity.SelectedValue + " - " + ds.Tables[0].Rows[0]["TenTinhThanh"].ToString();
                cmbCountry.SelectedValue = ds.Tables[0].Rows[0]["CountryCode"].ToString();
                cmbCountry.Text = cmbCountry.SelectedValue + " - " + ds.Tables[0].Rows[0]["CountryName"].ToString();
                /////////////////////
                cmbNationality.SelectedValue = ds.Tables[0].Rows[0]["NationalityCode"].ToString();
                cmbNationality.Text = cmbNationality.SelectedValue + " - " + ds.Tables[0].Rows[0]["NationalityName"].ToString();
                cmbRecidence.SelectedValue = ds.Tables[0].Rows[0]["ResidenceCode"].ToString();
                cmbRecidence.Text = cmbRecidence.SelectedValue + " - " + ds.Tables[0].Rows[0]["ResidenceName"].ToString();
                cmbDocType.SelectedItem.Text = ds.Tables[0].Rows[0]["DocType"].ToString();
                txtDocID.Text = ds.Tables[0].Rows[0]["DocID"].ToString();
                txtDocIssuePlace.Text = ds.Tables[0].Rows[0]["DocIssuePlace"].ToString();
                if (ds.Tables[0].Rows[0]["DocIssueDate"].ToString() != "")
                {
                    rdpDocIssueDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DocIssueDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DocExpiryDate"].ToString() != "")
                {
                    rdpDocExpiryDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DocExpiryDate"].ToString());
                }
                txtContactPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                txtPosition.Text = ds.Tables[0].Rows[0]["Position"].ToString();
                txtTelephone.Text = ds.Tables[0].Rows[0]["Telephone"].ToString();
                txtEmailAddress.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                cmbMainSector.SelectedValue = ds.Tables[0].Rows[0]["SectorCode"].ToString();
                cmbMainSector.Text = cmbMainSector.SelectedValue + " - " + ds.Tables[0].Rows[0]["SectorName"].ToString();
                cmbMainSector_SelectedIndexChanged(cmbMainSector, null);
                cmbSector.SelectedValue = ds.Tables[0].Rows[0]["SubSectorCode"].ToString();
                cmbSector.Text = cmbSector.SelectedValue + " - " + ds.Tables[0].Rows[0]["SubSectorName"].ToString();

                cmbMainIndustry.SelectedValue = ds.Tables[0].Rows[0]["IndustryCode"].ToString();
                cmbMainIndustry.Text = cmbMainIndustry.SelectedValue.ToString() + " - " + ds.Tables[0].Rows[0]["IndustryName"].ToString();
                cmbMainIndustry_SelectedIndexChanged(cmbMainIndustry, null);
                cmbIndustry.SelectedValue = ds.Tables[0].Rows[0]["SubIndustryCode"].ToString();
                cmbIndustry.Text = cmbIndustry.SelectedValue.ToString() + " - " + ds.Tables[0].Rows[0]["SubIndustryName"].ToString();
                cmbTarget.SelectedValue = ds.Tables[0].Rows[0]["TargetCode"].ToString();
                cmbAccountOfficer.SelectedItem.Text = ds.Tables[0].Rows[0]["AccountOfficer"].ToString();
                string date = ds.Tables[0].Rows[0]["ContactDate"].ToString();
                if (date != "")
                {
                    rdpContactDate.SelectedDate = Convert.ToDateTime(date);
                }
                cmbRelationCode.SelectedValue = ds.Tables[0].Rows[0]["RelationCode"].ToString();
                txtOfficeNumber.Text = ds.Tables[0].Rows[0]["OfficeNumber"].ToString();
                txtTotalCapital.Text = ds.Tables[0].Rows[0]["TotalCapital"].ToString();
                txtNoOfEmployee.Text = ds.Tables[0].Rows[0]["NoOfEmployee"].ToString();
                txtTotalAssets.Text = ds.Tables[0].Rows[0]["TotalAssets"].ToString();
                txtTotalRevenue.Text = ds.Tables[0].Rows[0]["TotalRevenue"].ToString();
                txtCustomerLiability.Text = ds.Tables[0].Rows[0]["CustomerLiability"].ToString();
                txtLegacyRef.Text = ds.Tables[0].Rows[0]["LegacyRef"].ToString();
                bientoancuc.StatusAccount_from_Search_action= ds.Tables[0].Rows[0]["Status"].ToString();
            }
        }
        #endregion

        
    }
}