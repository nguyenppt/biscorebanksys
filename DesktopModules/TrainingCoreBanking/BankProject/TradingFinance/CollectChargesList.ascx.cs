﻿using System;
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
            if (!IsPostBack)
            {
                string Status = bd.TransactionStatus.AUT;
                if (lstType != null && lstType.ToLower().Equals("4appr"))
                    Status = bd.TransactionStatus.UNA;
                //
                radGridReview.DataSource = db.B_CollectCharges.Where(p => p.Status.Equals(Status)).OrderByDescending(p => p.DateTimeCreate)
                    .Select(q => new { q.TransCode, q.TotalChargeAmount, q.ChargeCurrency, q.Status }).ToList();
            }
        }
    }
}