using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;
using BankProject.DBContext;

namespace BankProject.TradingFinance.Export.DocumentaryCollections
{
    public partial class ReviewExport_DocumentCollection : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private VietVictoryCoreBankingEntities _entities = new VietVictoryCoreBankingEntities();
        private ExportDocumentaryScreenType ScreenType
        {
            get
            {
                switch (TabId)
                {
                    case 229:
                        return ExportDocumentaryScreenType.Amend;
                    case 230:
                        return ExportDocumentaryScreenType.Cancel;
                    case 227:
                        return ExportDocumentaryScreenType.RegisterCc;
                    case 377:
                        return ExportDocumentaryScreenType.Acception;
                    default:
                        return ExportDocumentaryScreenType.Register;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (TabId != 229)
            {
                radGridReview.DataSource = SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_GetbyStatus(ScreenType.ToString("G"), UserId.ToString());
            }
            else
            { 
                 
                DataSet datasource = new DataSet();//Tab1
                DataTable tbl1 = new DataTable();
                tbl1.Columns.Add("DocCollectCode");
                tbl1.Columns.Add("CollectionType");
                tbl1.Columns.Add("Currency");
                tbl1.Columns.Add("Amount");
                tbl1.Columns.Add("Status");
                var lst = _entities.BEXPORT_DOCUMETARYCOLLECTION.Where(x => x.Amend_Status == "UNA" && (x.ActiveRecordFlag == null ||x.ActiveRecordFlag == YesNo.YES)).ToList();
                foreach (var item in lst)
                {
                    if (!String.IsNullOrEmpty(item.AmendNo))
                    {
                        tbl1.Rows.Add(item.AmendNo, item.CollectionType,item.Currency,item.Amount,item.Amend_Status);
                    }
                }
                datasource.Tables.Add(tbl1);
                radGridReview.DataSource = datasource;
            }
        }
        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=" + TabId.ToString() + "&CodeID=" + id + "&disable=1";
        }
    }
}