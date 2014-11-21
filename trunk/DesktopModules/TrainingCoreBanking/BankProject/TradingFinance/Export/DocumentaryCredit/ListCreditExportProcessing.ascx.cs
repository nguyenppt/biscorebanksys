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
using System.Data;
using BankProject.Model;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class ListCreditExportProcessing : PortalModuleBase
    {
        private readonly ExportLC entContext = new ExportLC();
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
        private DataSet CreateDataset(List<BEXPORT_DOCUMENTPROCESSING> ds)
        {
            DataSet datasource = new DataSet();//Tab1
            DataTable tbl1 = new DataTable();
            tbl1.Columns.Add("PaymentId");
            tbl1.Columns.Add("Amount");
            tbl1.Columns.Add("Currency");
            tbl1.Columns.Add("Status");
            foreach (var item in ds)
            {
                if (this.TabId == TabDocsWithDiscrepancies || this.TabId == TabDocsWithNoDiscrepancies)
                {
                    tbl1.Rows.Add(item.PaymentId, item.Amount, item.Currency, item.Status);
                }
                else if (this.TabId == TabDocsAmend)
                {
                    tbl1.Rows.Add(item.AmendNo, item.Amount, item.Currency, item.AmendStatus);
                }
                else if (this.TabId == TabDocsAccept)
                {
                    tbl1.Rows.Add(item.PaymentId, item.Amount, item.Currency, item.AcceptStatus);
                }
                else if (this.TabId == TabDocsReject)
                {
                    tbl1.Rows.Add(item.PaymentId, item.Amount, item.Currency, item.RejectStatus);
                }
                
                
            }
            datasource.Tables.Add(tbl1);
            return datasource;
        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string Status = null;
            var ds = new List<BEXPORT_DOCUMENTPROCESSING>();
            if (!string.IsNullOrEmpty(lstType)) Status = bd.TransactionStatus.UNA;
            if (this.TabId ==TabDocsWithDiscrepancies||this.TabId==TabDocsWithNoDiscrepancies)
            {
                ds= entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.Status == Status).ToList();
            }
            else if (this.TabId == TabDocsAmend)
            {
                ds = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.AmendStatus == Status).ToList();
            }
            else if (this.TabId == TabDocsAccept)
            {
                ds = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.AcceptStatus == Status).ToList();
            }
            else if (this.TabId == TabDocsReject)
            {
                 ds= entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.RejectStatus == Status).ToList();
            }   
            
                var lst=CreateDataset(ds);
                radGridReview.DataSource = lst;            
        }
    }
}