using BankProject.DataProvider;
using DotNetNuke.Entities.Modules;
using System;
using System.Data;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class NewNormalLoanEnquiry : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            LoadToolBar();

            rcbcategory.DataSource = DataProvider.DataTam.BCATEGORY_GetAll();
            rcbcategory.DataTextField = "NAME";
            rcbcategory.DataValueField = "ID";
            rcbcategory.DataBind();
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

        protected void radtoolbar2_onbuttonclick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            var commandName = ToolBarButton.CommandName;
            switch (commandName)
            {
                case "search":
                    LoadData();
                    break;
            }
        }

        private void LoadData()
        {
            if (IsPostBack)
            {
                radGridReview.DataSource = BankProject.DataProvider.Database.BNEWNORMALLOAN_Enquiry(txtRefCode.Text, rcbCustomerType.SelectedValue, tbCustomerID.Text,
                                                                                                  tbGBFullName.Text, tbDocID.Text, rcbcategory.SelectedValue, rcbCurrency.SelectedValue, rcbSubCategory.SelectedValue);
                radGridReview.DataBind();
            }
            else
            {
                radGridReview.DataSource = BankProject.DataProvider.Database.BNEWNORMALLOAN_Enquiry(txtRefCode.Text, rcbCustomerType.SelectedValue, tbCustomerID.Text,
                                                                                                  tbGBFullName.Text, tbDocID.Text, rcbcategory.SelectedValue, rcbCurrency.SelectedValue, rcbSubCategory.SelectedValue);
            }
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                radGridReview.DataSource = BankProject.DataProvider.Database.BNEWNORMALLOAN_Enquiry(txtRefCode.Text, rcbCustomerType.SelectedValue, tbCustomerID.Text,
                                                                                                  tbGBFullName.Text, tbDocID.Text, rcbcategory.SelectedValue, rcbCurrency.SelectedValue, rcbSubCategory.SelectedValue);
            }
            else
            radGridReview.DataSource = BankProject.DataProvider.Database.BNEWNORMALLOAN_Enquiry("NOTdata", "NOTdata", "NOTdata", "NOTdata", "NOTdata", "NOTdata", "", "NOTdata");
        }

        protected void cmbCategory_onselectedindexchanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSubCategory();
        }

        private void LoadSubCategory()
        {
            string id = rcbcategory.SelectedValue;

            rcbSubCategory.Items.Clear();
            rcbSubCategory.Items.Add(new RadComboBoxItem(""));
            rcbSubCategory.DataValueField = "SubCatId";
            rcbSubCategory.DataTextField = "Display";
            rcbSubCategory.DataSource = SQLData.B_BRPODCATEGORY_GetSubAll_IdOver200(id);
            rcbSubCategory.DataBind();
        }

    }
}