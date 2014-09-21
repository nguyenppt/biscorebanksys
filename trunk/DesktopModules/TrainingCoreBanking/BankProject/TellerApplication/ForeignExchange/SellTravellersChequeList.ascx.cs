using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;

namespace BankProject.Views.TellerApplication.ForeignExchange
{
    public partial class SellTravellersChequeList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected string lstType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lstType = Request.QueryString["lst"];
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
            if (IsPostBack || !String.IsNullOrEmpty(lstType))
            {
                if (String.IsNullOrEmpty(lstType))
                    radGridReview.DataSource = bd.Teller.SellTravellersChequeDetailOrList(txtCustomerName.Text, txtPassportNo.Text, txtPhoneNo.Text);
                else
                    radGridReview.DataSource = bd.Teller.SellTravellersChequeDetailOrList(bd.TransactionStatus.UNA);
            }
        }

        public string GenerateEnquiryButtons(string TTNo)
        {
            return "<a href=\"Default.aspx?tabid=" + this.TabId + "&amp;tid=" + TTNo + "&amp;lst=" + lstType + "\"><img src=\"Icons/bank/text_preview.png\" alt=\"\" title=\"\" style=\"\" width=\"20\"> </a>";
        }
    }
}