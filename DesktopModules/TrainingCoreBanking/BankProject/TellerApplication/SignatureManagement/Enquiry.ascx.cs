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
        protected string lstType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lstType = Request.QueryString["lst"];
            RadToolBar1.FindItemByValue("btSearch").Enabled = (lstType == null);
            divSearchBox.Visible = (lstType == null);
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
            if (IsPostBack || lstType != null)
            {
                if (lstType != null && lstType.ToLower().Equals("4appr"))
                    radGridReview.DataSource = bd.Customer.SignatureList(bd.TransactionStatus.UNA, txtCustomerId.Text, txtCustomerName.Text);
                else
                    radGridReview.DataSource = bd.Customer.SignatureList(null, txtCustomerId.Text, txtCustomerName.Text);
            }
        }

        public string GenerateEnquiryButtons(string CustomerId)
        {
            if (lstType != null)
                return "<a href=\"Default.aspx?tabid=285&amp;tid=" + CustomerId + "\"><img src=\"Icons/bank/text_preview.png\" alt=\"\" title=\"\" style=\"\" width=\"20\"> </a>";

            return "<a href=\"Default.aspx?tabid=287&amp;tid=" + CustomerId + "\"><img src=\"Icons/bank/text_preview.png\" alt=\"\" title=\"\" style=\"\" width=\"20\"> </a>";
        }
    }
}