using System;
using System.Data;
using BankProject.DataProvider;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Import.DocumentaryCredit
{
    public partial class EnquiryIssueLC : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            InitToolBar(false);

            var dsApp = SQLData.B_BCUSTOMER_INFO_GetByStatus("AUT");
            rcbApplicantID.Items.Clear();
            rcbApplicantID.Items.Add(new RadComboBoxItem(""));
            rcbApplicantID.DataTextField = "Display";
            rcbApplicantID.DataValueField = "CustomerID";
            rcbApplicantID.DataSource = dsApp;
            rcbApplicantID.DataBind();
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = SQLData.B_BIMPORT_NORMAILLC_GetByEnquiry(txtCode.Text.Trim(), rcbApplicantID.SelectedValue, txtApplicantName.Text.Trim(), UserId);
        }

        protected string geturlReview(string Id)
        {
            return "Default.aspx?tabid=92" + "&CodeID=" + Id + "&enquiry=true";
        }

        protected void Search()
        {
            radGridReview.DataSource = SQLData.B_BIMPORT_NORMAILLC_GetByEnquiry(txtCode.Text.Trim(), rcbApplicantID.SelectedValue, txtApplicantName.Text.Trim(), UserId);
            radGridReview.DataBind();
        }

        protected void InitToolBar(bool flag)
        {
            RadToolBar2.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar2.FindItemByValue("btRevert").Enabled = flag;
            RadToolBar2.FindItemByValue("btReview").Enabled = flag;
            RadToolBar2.FindItemByValue("btSave").Enabled = flag;
            RadToolBar2.FindItemByValue("btPrint").Enabled = flag;
        }

        protected void rcbApplicantID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerName"] = row["GBFullName"].ToString();
            e.Item.Attributes["Address"] = row["GBStreet"].ToString();
            e.Item.Attributes["IdentityNo"] = row["DocID"].ToString();
            e.Item.Attributes["IssueDate"] = row["DocIssueDate"].ToString();
            e.Item.Attributes["IssuePlace"] = row["DocIssuePlace"].ToString();
            e.Item.Attributes["City"] = row["GBDist"].ToString();
            e.Item.Attributes["Country"] = row["CountryName"].ToString();
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "search":
                    Search();
                    break;
            }
        }
    }
}