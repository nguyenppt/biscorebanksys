using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;

namespace BankProject.TellerApplication.SignatureManagement
{
    public partial class Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RadToolBar1.FindItemByValue("btSearch").Enabled = (Request.QueryString["lst"] == null);
            divSearchBox.Visible = (Request.QueryString["lst"] == null);
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case bc.Commands.Search:
                    radGridReview.Rebind();
                    break;
            }
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack || Request.QueryString["lst"] != null)
            {
                radGridReview.DataSource = bd.Customer.SignatureList(txtCustomerId.Text, txtCustomerName.Text);
            }
        }

        public string GenerateEnquiryButtons(string CustomerId)
        {
            if (Request.QueryString["lst"] != null)
                return "<a href=\"Default.aspx?tabid=285&amp;tid=" + CustomerId + "\"><img src=\"Icons/bank/text_preview.png\" alt=\"\" title=\"\" style=\"\" width=\"20\"> </a>";

            return "<a href=\"Default.aspx?tabid=287&amp;tid=" + CustomerId + "\"><img src=\"Icons/bank/text_preview.png\" alt=\"\" title=\"\" style=\"\" width=\"20\"> </a>";
        }
    }
}