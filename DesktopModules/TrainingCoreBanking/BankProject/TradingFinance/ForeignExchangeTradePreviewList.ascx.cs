using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using Telerik.Web.UI;
using BankProject.DBContext;

namespace BankProject.Views.TellerApplication
{
    public partial class ForeignExchangeTradePreviewList : DotNetNuke.Entities.Modules.PortalModuleBase
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
            string RefNo = txtRefNo.Text.Trim(), TFNo = txtTFNo.Text.Trim();
            //
            radGridReview.DataSource = db.BFOREIGNEXCHANGEs
                .Where(p => p.Status.Equals(Status) &&
                    (string.IsNullOrEmpty(RefNo) || p.Code.ToLower().Contains(RefNo)) &&
                    (string.IsNullOrEmpty(TFNo) || p.FTNo.Contains(TFNo)))
                .OrderByDescending(p => p.CreateDate)
                .Select(q => new { RefNo = q.Code, TFNo = q.FTNo, q.BuyAmount, q.BuyCurrency, q.SellAmount, q.SellCurrency, q.Status }).ToList();
        }
    }
}