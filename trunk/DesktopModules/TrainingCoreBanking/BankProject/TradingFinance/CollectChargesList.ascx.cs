using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using Telerik.Web.UI;
using BankProject.DBContext;

namespace BankProject.TradingFinance
{
    public partial class CollectChargesList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        VietVictoryCoreBankingEntities db = new VietVictoryCoreBankingEntities();
        protected string lstType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lstType = Request.QueryString["lst"];
            RadToolBar1.FindItemByValue("btSearch").Enabled = (string.IsNullOrEmpty(lstType) || !lstType.ToLower().Equals("4appr"));
            if (IsPostBack) return;            
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case bc.Commands.Search:
                    loadData();
                    radGridReview.Rebind();
                    break;
            }
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !string.IsNullOrEmpty(lstType) && lstType.ToLower().Equals("4appr")) loadData();
        }

        private void loadData()
        {
            string Status = bd.TransactionStatus.AUT;
            if (!string.IsNullOrEmpty(lstType) && lstType.ToLower().Equals("4appr"))
                Status = bd.TransactionStatus.UNA;
            string RefNo = txtRefNo.Text.Trim(), ChargeAccount = txtChargeAccount.Text.Trim();
            //
            radGridReview.DataSource = db.B_CollectCharges
                .Where(p => p.Status.Equals(Status) &&
                    (string.IsNullOrEmpty(RefNo) || p.TransCode.ToLower().Contains(RefNo)) &&
                    (string.IsNullOrEmpty(ChargeAccount) || p.ChargeAcct.Contains(ChargeAccount)))
                .OrderByDescending(p => p.DateTimeCreate)
                .Select(q => new { q.TransCode, ChargeAccount = q.ChargeAcct, q.TotalChargeAmount, q.ChargeCurrency, q.Status }).ToList();            
        }
    }
}