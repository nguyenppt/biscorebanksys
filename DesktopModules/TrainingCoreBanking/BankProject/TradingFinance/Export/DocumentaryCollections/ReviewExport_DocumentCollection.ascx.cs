using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Export.DocumentaryCollections
{
    public partial class ReviewExport_DocumentCollection : DotNetNuke.Entities.Modules.PortalModuleBase
    {
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
            radGridReview.DataSource = SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_GetbyStatus(ScreenType.ToString("G"), UserId.ToString());

        }
        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=" + TabId.ToString() + "&CodeID=" + id + "&disable=1";
        }
    }
}