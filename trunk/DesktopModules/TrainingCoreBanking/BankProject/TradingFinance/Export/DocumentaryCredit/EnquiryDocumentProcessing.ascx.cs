using System;
using System.Data;
using System.Web.UI;
using DotNetNuke.Common;
using Telerik.Web.UI;
using BankProject.DBContext;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using BankProject.DataProvider;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;
using BankProject.Helper;
using BankProject.Model;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class EnquiryDocumentProcessing : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private ExportLCDocProcessing dbEntities = new ExportLCDocProcessing();
        protected string lstType = "";
        protected int refId;
        protected void Page_Load(object sender, EventArgs e)
        {
            lstType = Request.QueryString["lst"];
            if (!string.IsNullOrEmpty(Request.QueryString["refid"]))
                refId = Convert.ToInt32(Request.QueryString["refid"]);
            else
                refId = ExportLCDocProcessing.Actions.Register;
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
            IQueryable<BEXPORT_LC_DOCS_PROCESSING> enquiry = dbEntities.BEXPORT_LC_DOCS_PROCESSING.AsQueryable();
            if (!string.IsNullOrEmpty(lstType) && lstType.ToLower().Equals("4appr"))
            {
                switch (refId)
                {
                    case ExportLCDocProcessing.Actions.Reject:
                        enquiry = enquiry.Where(p => (p.RejectStatus.Equals(bd.TransactionStatus.UNA) && p.ActiveRecordFlag.Equals("Yes")));
                        break;
                    case ExportLCDocProcessing.Actions.Accept:
                        enquiry = enquiry.Where(p => (p.AcceptStatus.Equals(bd.TransactionStatus.UNA) && p.ActiveRecordFlag.Equals("Yes")));
                        break;
                    case ExportLCDocProcessing.Actions.Amend:
                        enquiry = enquiry.Where(p => (p.AmendStatus.Equals(bd.TransactionStatus.UNA) && p.ActiveRecordFlag.Equals("Yes")));
                        break;
                    default:// ExportLC.Actions.Register:
                        enquiry = enquiry.Where(p => (p.Status.Equals(bd.TransactionStatus.UNA) && p.ActiveRecordFlag.Equals("Yes")));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(txtRefNo.Text))
                enquiry = enquiry.Where(p => p.DocCode.StartsWith(txtRefNo.Text));
            if (!string.IsNullOrEmpty(txtApplicantName.Text))
                enquiry = enquiry.Where(p => p.ApplicantName.Contains(txtApplicantName.Text));
            if (!string.IsNullOrEmpty(txtBeneficiaryName.Text))
                enquiry = enquiry.Where(p => p.BeneficiaryName.Contains(txtBeneficiaryName.Text));
            switch (refId)
            {
                case ExportLCDocProcessing.Actions.Reject:
                    radGridReview.DataSource = enquiry
                        .OrderByDescending(p => p.RejectDate)
                        .Select(q => new { Code = q.DocCode, ImportLCCode = q.DocumentaryCreditNo, q.ApplicantName, q.Amount, q.Currency, q.BeneficiaryName, q.IssuingBankNo, Status = q.RejectStatus })
                        .ToList();
                    return;
                case ExportLCDocProcessing.Actions.Accept:
                    radGridReview.DataSource = enquiry
                        .OrderByDescending(p => p.AcceptDate)
                        .Select(q => new { Code = q.DocCode, ImportLCCode = q.DocumentaryCreditNo, q.ApplicantName, q.Amount, q.Currency, q.BeneficiaryName, q.IssuingBankNo, Status = q.AcceptStatus })
                        .ToList();
                    return;
                case ExportLCDocProcessing.Actions.Amend:
                    radGridReview.DataSource = enquiry
                        .OrderByDescending(p => p.AmendDate)
                        .Select(q => new { Code = q.AmendNo, ImportLCCode = q.DocumentaryCreditNo, q.ApplicantName, q.Amount, q.Currency, q.BeneficiaryName, q.IssuingBankNo, Status = q.AmendStatus })
                        .ToList();
                    return;
                default:// ExportLC.Actions.Register:
                    radGridReview.DataSource = enquiry
                        .OrderByDescending(p => p.CreateDate)
                        .Select(q => new { Code = q.DocCode, ImportLCCode = q.DocumentaryCreditNo, q.ApplicantName, q.Amount, q.Currency, q.BeneficiaryName, q.IssuingBankNo, Status = q.Status })
                        .ToList();
                    return;
            }
        }
    }
}