using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;

namespace BankProject.Views.TellerApplication
{
    public partial class SellTravellersChequeList : DotNetNuke.Entities.Modules.PortalModuleBase
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
                case bc.Commands.Search:
                    radGridReview.Rebind();
                    break;
            }
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                //txtCustomerName.Text = "P";
                radGridReview.DataSource = bd.Teller.SellTravellersChequeDetailOrList(txtCustomerName.Text, txtPassportNo.Text, txtPhoneNo.Text);
            }
        }
    }
}