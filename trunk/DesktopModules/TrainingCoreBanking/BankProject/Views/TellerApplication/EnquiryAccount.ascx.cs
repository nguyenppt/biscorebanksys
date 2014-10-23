using DotNetNuke.Entities.Modules;
using System;
using System.Data;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class EnquiryAccount : PortalModuleBase
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
            radGridReview.DataSource = BankProject.DataProvider.Database.BOPENACCOUNT_Enquiry(txtAccountCode.Text, ckLocked.Checked, ckClose.Checked, rcbCustomerType.SelectedValue, tbCustomerID.Text,
                                                                                              tbGBFullName.Text, tbDocID.Text, rcbcategory.SelectedValue, rcbCurrency.SelectedValue, rcbProductLine.SelectedValue);
            radGridReview.DataBind();
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
        
            
            if (IsPostBack)
            {
                radGridReview.DataSource = BankProject.DataProvider.Database.BOPENACCOUNT_Enquiry(txtAccountCode.Text, ckLocked.Checked, ckClose.Checked, rcbCustomerType.SelectedValue, tbCustomerID.Text,
                                                                                              tbGBFullName.Text, tbDocID.Text, rcbcategory.SelectedValue, rcbCurrency.SelectedValue, rcbProductLine.SelectedValue);
            }
            else
            {
                radGridReview.DataSource = BankProject.DataProvider.Database.BOPENACCOUNT_Enquiry("NOTdata", false, false, "NOTdata", "NOTdata", "NOTdata", "NOTdata", "NOTdata", "", "NOTdata");
            }
            
        }
        protected string geturlReview(string ID)
        {
            return "Default.aspx?tabid=119&codeid=" + ID;
        }
        protected void cmbCategory_onitemdatabound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Type"] = row["Type"].ToString();
        }

        protected void cmbCategory_onselectedindexchanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbProductLine.Items.Clear();
            rcbProductLine.Items.Add(new RadComboBoxItem("", ""));
            rcbProductLine.AppendDataBoundItems = true;
            rcbProductLine.DataSource = DataProvider.DataTam.B_BPRODUCTLINE_GetByType(rcbcategory.SelectedItem.Attributes["Type"]);
            rcbProductLine.DataTextField = "Description";
            rcbProductLine.DataValueField = "ProductID";
            rcbProductLine.DataBind();
        }

    }
}