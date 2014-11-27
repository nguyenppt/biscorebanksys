using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DBContext;
using Telerik.Web.UI;
using BankProject.Model;
using BankProject.DataProvider;
using System.Data;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class AdvisingReviewList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private readonly ExportLC entContext = new ExportLC();
        protected const int TabIssueLCAmend = 235;
        protected const int TabIssueLCConfirm = 236;
        protected const int TabIssueLCCancel = 237;
        protected const int TabIssueLCClose = 265;
        public enum AdvisingAndNegotiationScreenType
        {
            Register,
            Amend,
            Cancel,
            Close,
            RegisterCc,
            Acception
        }
        private AdvisingAndNegotiationScreenType ScreenType
        {
            get
            {
                switch (TabId)
                {
                    case TabIssueLCAmend:
                        return AdvisingAndNegotiationScreenType.Amend;
                    case TabIssueLCCancel:
                        return AdvisingAndNegotiationScreenType.Cancel;
                    case TabIssueLCClose:
                        return AdvisingAndNegotiationScreenType.Close;
                    case TabIssueLCConfirm:
                        return AdvisingAndNegotiationScreenType.Acception;
                    default:
                        return AdvisingAndNegotiationScreenType.Register;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            switch (ScreenType)
            {
                case AdvisingAndNegotiationScreenType.Register:
                    radGridReview.DataSource = entContext.BAdvisingAndNegotiationLCs.Where(q => q.Status == "UNA").Select(q => new { q.NormalLCCode,q.Status }).ToList();
                    break;
                case AdvisingAndNegotiationScreenType.Amend:
                    DataSet datasource = new DataSet();//Tab1
                    DataTable tbl1 = new DataTable();
                    tbl1.Columns.Add("NormalLCCode");
                    tbl1.Columns.Add("Status");
                    var lst = entContext.BAdvisingAndNegotiationLCs.Where(q => q.AmendStatus == "UNA" && (q.ActiveRecordFlag == null || q.ActiveRecordFlag == YesNo.YES)).ToList();
                    foreach (var item in lst)
                    {
                        if (!String.IsNullOrEmpty(item.AmendNo))
                        {
                            tbl1.Rows.Add(item.AmendNo, item.AmendStatus);
                        }
                    }
                    datasource.Tables.Add(tbl1);
                    radGridReview.DataSource = datasource;
                    break;
                case AdvisingAndNegotiationScreenType.Cancel:
                    radGridReview.DataSource = entContext.BAdvisingAndNegotiationLCs.Where(q => q.CancelStatus == "UNA").Select(q => new { q.NormalLCCode, Status = q.CancelStatus }).ToList();
                    break;
                case AdvisingAndNegotiationScreenType.Close:
                    radGridReview.DataSource = entContext.BAdvisingAndNegotiationLCs.Where(q => q.CloseStatus == "UNA").Select(q => new { q.NormalLCCode, Status=q.CloseStatus }).ToList();
                    break;
                case AdvisingAndNegotiationScreenType.Acception:
                    radGridReview.DataSource = entContext.BAdvisingAndNegotiationLCs.Where(q => q.AcceptStatus == "UNA").Select(q => new {q.NormalLCCode,Status=q.AcceptStatus }).ToList();
                    break;
                //case AdvisingAndNegotiationScreenType.Amend:
                //    radGridReview.DataSource = entContext.BAdvisingAndNegotiationLCs.Where(q => q.AmendStatus == "UNA" || q.AmendStatus =="REV").Select(q => new { q.NormalLCCode, Status = q.AmendStatus }).ToList();
                //    break;
                //case AdvisingAndNegotiationScreenType.Cancel:
                //    radGridReview.DataSource = entContext.BAdvisingAndNegotiationLCs.Where(q => q.CancelStatus == "UNA" || q.CancelStatus == "REV").Select(q => new { q.NormalLCCode, Status = q.CancelStatus }).ToList();
                //    break;
            }
        }
        public string geturlReview(string id)
        {
            return "Default.aspx?tabid=" + TabId.ToString() + "&LCCode=" + id + "&disable=1";
        }
    }
}