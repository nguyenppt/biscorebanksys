﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DBContext;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class AdvisingReviewList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private readonly VietVictoryCoreBankingEntities entContext = new VietVictoryCoreBankingEntities();
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
                    case 235:
                        return AdvisingAndNegotiationScreenType.Amend;
                    case 237:
                        return AdvisingAndNegotiationScreenType.Cancel;
                    case 265:
                        return AdvisingAndNegotiationScreenType.Close;
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
                    radGridReview.DataSource = entContext.BAdvisingAndNegotiationLCs.Where(q => q.AmendStatus == "UNA" || q.AmendStatus =="REV").Select(q => new { q.NormalLCCode, Status = q.AmendStatus }).ToList();
                    break;
                case AdvisingAndNegotiationScreenType.Cancel:
                    radGridReview.DataSource = entContext.BAdvisingAndNegotiationLCs.Where(q => q.CancelStatus == "UNA" || q.CancelStatus == "REV").Select(q => new { q.NormalLCCode, Status = q.CancelStatus }).ToList();
                    break;
                case AdvisingAndNegotiationScreenType.Close:
                    radGridReview.DataSource = entContext.BAdvisingAndNegotiationLCs.Where(q => q.CloseStatus == "UNA" || q.CancelStatus == "REV").Select(q => new { q.NormalLCCode, Status=q.CloseStatus }).ToList();
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