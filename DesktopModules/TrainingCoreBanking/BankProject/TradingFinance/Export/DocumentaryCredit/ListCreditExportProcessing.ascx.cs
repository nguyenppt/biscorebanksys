using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using BankProject.DBContext;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class ListCreditExportProcessing : PortalModuleBase
    {
        private readonly VietVictoryCoreBankingEntities entContext = new VietVictoryCoreBankingEntities();
        protected const int TabDocsWithNoDiscrepancies = 239;
        protected const int TabDocsWithDiscrepancies = 240;
        protected const int TabDocsReject = 241;
        protected const int TabDocsAmend = 376;
        protected const int TabDocsAccept = 244;
        protected string lstType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lstType = Request.QueryString["lst"];
        }

        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=" + TabId.ToString() + "&tid=" + id + "&lst=" + lstType;
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string Status = null;
            if (!string.IsNullOrEmpty(lstType)) Status = bd.TransactionStatus.UNA;
            if (this.TabId ==TabDocsWithDiscrepancies||this.TabId==TabDocsWithNoDiscrepancies)
            {
                radGridReview.DataSource = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.Status == Status).ToList();
            }
            else if (this.TabId == TabDocsAmend)
            {
                radGridReview.DataSource = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.AmendStatus == Status).ToList();
            }
            else if (this.TabId == TabDocsAccept)
            {
                radGridReview.DataSource = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.AcceptStatus == Status).ToList();
            }
            else if (this.TabId == TabDocsReject)
            {
                radGridReview.DataSource = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.RejectStatus == Status).ToList();
            }   
        }
    }
}