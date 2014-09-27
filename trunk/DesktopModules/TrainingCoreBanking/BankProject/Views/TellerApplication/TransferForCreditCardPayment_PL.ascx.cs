using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BankProject.DataProvider;
using System.Data;
namespace BankProject.Views.TellerApplication
{
    public partial class TransferForCreditCardPayment_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = TriTT.BTRANSFER_4_CRE_CARD_PAYMENT_Preview_List();
        }
        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=" + this.TabId.ToString() + "&ID=" + id;
        }
    }
}