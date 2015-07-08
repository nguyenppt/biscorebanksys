using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Import.DocumentaryCredit
{
    public partial class PaymentList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private string lstType = "";
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
                case BankProject.Controls.Commands.Search:
                    radGridReview.Rebind();
                    break;
            }
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //if (!IsPostBack)
            //{
                if (lstType != null && lstType.ToLower().Equals("4appr"))
                    radGridReview.DataSource = bd.IssueLC.ImportLCPaymentList(bd.TransactionStatus.UNA);
                else
                    radGridReview.DataSource = bd.IssueLC.ImportLCPaymentList(null);
            //}
        }

        public string GenerateEnquiryButtons(string TId)
        {
            return "<a href=\"Default.aspx?tabid=" + this.TabId + "&amp;tid=" + TId + "&amp;lst=" + lstType + "\"><img src=\"Icons/bank/text_preview.png\" alt=\"\" title=\"\" style=\"\" width=\"20\"> </a>";
        }
    }
}