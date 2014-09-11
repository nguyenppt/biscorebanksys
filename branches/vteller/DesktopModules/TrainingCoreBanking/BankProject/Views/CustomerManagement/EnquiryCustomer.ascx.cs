using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using BankProject.DataProvider;
namespace BankProject.Views.CustomerManagement
{
    public partial class EnquiryCustomer : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadToolBar();
                Load_MainSector();
                Load_MainIndustry();
            }
        }
        protected void radtoolbar2_onbuttonclick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            var commandName = ToolBarButton.CommandName;
            switch (commandName)
            {
                case "search":
                    Search_Customer();
                    break;
            }
        }
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                RadGrid1.DataSource = TriTT.ENQUIRY_CUSTOMER_Search_Account_Customer(rcbCustomerType.SelectedValue, tbCustomerID.Text.Trim(), tbCellPhone.Text.Trim(), tbGBFullName.Text.Trim(),
                tbDocID.Text.Trim(), rcbMainSector.SelectedValue, rcbSubSector.SelectedValue, rcbMainIndustry.SelectedValue, rcbSubIndustry.SelectedValue);
            } else
            RadGrid1.DataSource = TriTT.ENQUIRY_CUSTOMER_Search_Account_Customer("!","!","","","","","","","");
        }
        public string geturlReview(string CustomerID, string Status) /// co 3 truong hop cho Status: REV, UNA , AUT
        {   
            var mode = "preview";
            if(Status != "AUT") { mode = "audit";}
            switch (CustomerID.StartsWith("1")) 
            {
                case true:
                    return string.Format("Default.aspx?tabid={0}&mid={1}&CustomerID={2}&status={3}&disable=true&mode={4}", 112, 533, CustomerID,Status,mode);
                    break;/// redirect ve trang web OPEN INDIVIDUAL CUSTOMER
                default:
                    return string.Format("Default.aspx?tabid={0}&mid={1}&CustomerID={2}&status={3}&disable=true&mode={4}", 256, 744, CustomerID,Status, mode);
                    break; /// redirect ve trang web OPEN CORPORATE CUSTOMER
            }
        }
        #region Help Methods
        protected void Search_Customer()
        {
            if (IsPostBack)
            {
                RadGrid1.DataSource = TriTT.ENQUIRY_CUSTOMER_Search_Account_Customer(rcbCustomerType.SelectedValue, tbCustomerID.Text.Trim(), tbCellPhone.Text.Trim(), tbGBFullName.Text.Trim(),
                    tbDocID.Text.Trim(), rcbMainSector.SelectedValue, rcbSubSector.SelectedValue, rcbMainIndustry.SelectedValue, rcbSubIndustry.SelectedValue);
                RadGrid1.DataBind();
            }
            else
            {
                RadGrid1.DataSource = TriTT.ENQUIRY_CUSTOMER_Search_Account_Customer("!1", tbCustomerID.Text.Trim(), tbCellPhone.Text.Trim(), tbGBFullName.Text.Trim(),
                     tbDocID.Text.Trim(), rcbMainSector.SelectedValue, "!", rcbMainIndustry.SelectedValue, rcbSubIndustry.SelectedValue);
                RadGrid1.DataBind();
            }
        }
        protected void Load_MainSector()
        {
            //rcbMainSector.DataSource = Database.BSECTOR_GetAll();
            //rcbMainSector.DataTextField = "SECTORNAME";
            //rcbMainSector.DataValueField = "SECTORCODE";
            //rcbMainSector.DataBind();

            DataSet ds = Database.BSECTOR_GetAll();
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["SECTORCODE"] = "";
                dr["SECTORNAME"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbMainSector.DataSource = ds;
            rcbMainSector.DataValueField = "SECTORCODE";
            rcbMainSector.DataTextField = "SECTORNAME";
            rcbMainSector.DataBind();
        }
        protected void Load_MainIndustry()
        {
            DataSet ds = Database.BINDUSTRY_GetAll();
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["IndustryCode"] = "";
                dr["IndustryName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
                rcbMainIndustry.DataSource = ds;
                rcbMainIndustry.DataTextField = "IndustryName";
                rcbMainIndustry.DataValueField = "IndustryCode";
                rcbMainIndustry.DataBind();
            
        }
        protected void LoadSubIndustry(string MainIndustryCode)
        { 
            rcbSubIndustry.Items.Clear();
             DataSet ds  = Database.BINDUSTRY_GetByCoe(MainIndustryCode);
             if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
             {
                 DataRow dr = ds.Tables[0].NewRow();
                 dr["SubIndustryCode"]="";
                 dr["SubIndustryName"] = "";
                 ds.Tables[0].Rows.InsertAt(dr, 0);
             }
             rcbSubIndustry.DataSource = ds;
             rcbSubIndustry.DataValueField = "SubIndustryCode";
             rcbSubIndustry.DataTextField = "SubIndustryName";
             rcbSubIndustry.DataBind();
        }
        protected void LoadSubSector(string MainSectorCode)
        {
            rcbSubSector.Items.Clear();
            DataSet ds = Database.BSECTOR_GetByCode(MainSectorCode);
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["SUBSECTORCODE"] = "";
                dr["SUBSECTORNAME"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbSubSector.DataSource = ds;
            rcbSubSector.DataValueField = "SUBSECTORCODE";
            rcbSubSector.DataTextField = "SUBSECTORNAME";
            rcbSubSector.DataBind(); 
        }
        protected void LoadToolBar()
        {
            RadToolBar2.FindItemByValue("btCommit").Enabled = false;
            RadToolBar2.FindItemByValue("btReview").Enabled = false;
            RadToolBar2.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar2.FindItemByValue("btRevert").Enabled = false;
            RadToolBar2.FindItemByValue("btPrint").Enabled = false;
            RadToolBar2.FindItemByValue("btSearch").Enabled = true;
        }

        protected void OnIndexSelectedIndexChanged_rcbMainIndustry_rcbSubIndustry(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSubIndustry(rcbMainIndustry.SelectedValue);
        }
        protected void OnIndexSelectedIndexChanged_rcbMainSector_rcbSubSector(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSubSector(rcbMainSector.SelectedValue);
        }
        protected void rcbCustomerType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (rcbCustomerType.SelectedValue == "P")
            {
                Load_MainSector(); // moi lan refresh thi phai load lai data cho combox de co gia tri maf gan 
                rcbMainSector.SelectedValue = "200";
                LoadSubSector(rcbMainSector.SelectedValue);
                rcbSubSector.SelectedValue = "2001";
                rcbMainSector.Enabled = rcbSubSector.Enabled = false;
            }
            else
            {
                if (rcbCustomerType.SelectedValue == "C")
                {
                    rcbMainSector.Items.Clear();
                    rcbMainSector.Text = "";
                    rcbSubSector.Items.Clear();
                    rcbSubSector.Text = "";
                    rcbMainSector.Enabled = rcbSubSector.Enabled = true;
                    Load_MainSector();
                    rcbMainSector.Items.Remove(rcbMainSector.FindItemIndexByValue("200"));// loai bo thanh phan kinh te ca the
                } 
                else 
                {
                    rcbMainSector.Items.Clear();
                    rcbMainSector.Text = "";
                    rcbSubSector.Items.Clear();
                    rcbSubSector.Text = "";
                    rcbMainSector.Enabled = rcbSubSector.Enabled = true;
                    Load_MainSector();
                }
            }
        }
        #endregion
    }
}