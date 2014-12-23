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
    public partial class OpenDepositAcctForTFList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        VietVictoryCoreBankingEntities db = new VietVictoryCoreBankingEntities();
        protected string lstType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            lstType = Request.QueryString["lst"];
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
            string DepositeAccount = txtDepositeAccount.Text.Trim(),
                    CustomerID = txtCustomerID.Text.Trim(), CustomerName = txtCustomerName.Text.Trim(),
                    AccountName = txtAccountName.Text.Trim(), AccountMnemonic = txtAccountMnemonic.Text.Trim();
            //
            radGridReview.DataSource = db.BACCOUNTS
                .Where(p => (string.IsNullOrEmpty(DepositeAccount) || p.DepositCode.ToLower().Contains(DepositeAccount)) &&
                            (string.IsNullOrEmpty(CustomerID) || p.CustomerID.Contains(CustomerID)) &&
                            (string.IsNullOrEmpty(CustomerName) || p.AccountName.ToLower().Contains(CustomerName)) &&
                            (string.IsNullOrEmpty(AccountName) || p.AccountName.ToLower().Contains(AccountName)) &&
                            (string.IsNullOrEmpty(AccountMnemonic) || p.AccountMnemonic.ToLower().Contains(AccountMnemonic)))
                .OrderByDescending(p => p.DateTime)
                .Select(q => new { q.DepositCode, q.Currentcy, q.CustomerID, q.AccountName, q.AccountMnemonic, q.ProductLine }).ToList();
        }
    }
}