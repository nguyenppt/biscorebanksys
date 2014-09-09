using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.SignatureManagement
{
    public partial class Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case BankProject.Controls.Commands.Search:
                    //radGridReview.DataSource = BankProject.DataProvider.Customer.SignatureList(txtCustomerId.Text, txtCustomerName.Text);
                    radGridReview.Rebind();
                    break;
            }
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                radGridReview.DataSource = BankProject.DataProvider.Customer.SignatureList(txtCustomerId.Text, txtCustomerName.Text);
            }
        }

        public string generateURLs(string id, string Status)
        {
            string urls, url, icon;
            //view
            icon = "<img src=\"Icons/bank/preview2.png\" class=\"enquiryButton\" />";
            url = "Default.aspx?tabid=287&cid=" + id;
            urls = "<a href=\"" + url + "\" title=\"View\">" + icon + "</a>";
            //Edit
            icon = "<img src=\"Icons/bank/edit.png\" class=\"enquiryButton\" />";
            url = "Default.aspx?tabid=288&cid=" + id;
            /*if (!Status.Equals(BankProject.DataProvider.TransactionStatus.NAU))
            {
                url = "#";
                icon = "<img src=\"Icons/bank/edit.png\" class=\"enquiryButton\" style=\"opacity:0.5;\" />";
            }*/
            urls += "<a href=\"" + url + "\" title=\"Edit\">" + icon + "</a>";
            /*Reverse
            icon = "<img src=\"Icons/bank/delete.png\" class=\"enquiryButton\" />";
            url = "Default.aspx?tabid=285&cid=" + id;
            if (!Status.Equals(BankProject.DataProvider.TransactionStatus.NAU))
            {
                url = "#";
                icon = "<img src=\"Icons/bank/delete.png\" class=\"enquiryButton\" style=\"opacity:0.5;\" />";
            }
            urls += "<a href=\"" + url + "\" title=\"Reverse\">" + icon + "</a>";
            //Approve
            icon = "<img src=\"Icons/bank/approve.png\" class=\"enquiryButton\" />";
            url = "Default.aspx?tabid=285&cid=" + id;
            if (!Status.Equals(BankProject.DataProvider.TransactionStatus.NAU))
            {
                url = "#";
                icon = "<img src=\"Icons/bank/approve.png\" class=\"enquiryButton\" style=\"opacity:0.5;\" />";
            }
            urls += "<a href=\"" + url + "\" title=\"Approve\">" + icon + "</a>";
            */
            return urls;
        }
    }
}