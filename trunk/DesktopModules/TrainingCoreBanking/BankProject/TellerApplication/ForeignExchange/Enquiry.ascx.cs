using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.ForeignExchange
{
    public partial class Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected string lstType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lstType = Request.QueryString["lst"];
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (String.IsNullOrEmpty(lstType))
                radGridReview.DataSource = bd.Teller.TellerForeignExchangeList(this.TabId, null);
            else
                radGridReview.DataSource = bd.Teller.TellerForeignExchangeList(this.TabId, bd.TransactionStatus.UNA);
        }

        public string GenerateEnquiryButtons(string TTNo)
        {
            return "<a href=\"Default.aspx?tabid=" + this.TabId + "&amp;tid=" + TTNo + "&amp;lst=" + lstType + "\"><img src=\"Icons/bank/text_preview.png\" alt=\"\" title=\"\" style=\"\" width=\"20\"> </a>";
        }
    }
}