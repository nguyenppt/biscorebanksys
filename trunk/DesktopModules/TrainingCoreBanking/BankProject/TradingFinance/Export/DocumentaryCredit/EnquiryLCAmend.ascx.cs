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
    public partial class EnquiryLCAmend : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private ExportLC dbEntities = new ExportLC();
        protected string lstType = "";
        protected int refId;
        protected void Page_Load(object sender, EventArgs e)
        {
            lstType = Request.QueryString["lst"];
            if (!string.IsNullOrEmpty(Request.QueryString["refid"]))
                refId = Convert.ToInt32(Request.QueryString["refid"]);
            else
                refId = ExportLC.Actions.Amend;
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
            IQueryable<BEXPORT_LC_AMEND> enquiry = dbEntities.BEXPORT_LC_AMEND.AsQueryable();
            if (!string.IsNullOrEmpty(lstType) && lstType.ToLower().Equals("4appr"))
            {
                switch (refId)
                {
                    case ExportLC.Actions.Amend:
                        enquiry = enquiry.Where(p => p.AmendStatus.Equals(bd.TransactionStatus.UNA));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(txtRefNo.Text))
                enquiry = enquiry.Where(p => p.ImportLCCode.Equals(txtRefNo.Text));
            if (!string.IsNullOrEmpty(txtBeneficiaryID.Text))
                enquiry = enquiry.Where(p => p.BeneficiaryNo.Equals(txtBeneficiaryID.Text));
            if (!string.IsNullOrEmpty(txtBeneficiaryName.Text))
                enquiry = enquiry.Where(p => p.BeneficiaryName.Contains(txtBeneficiaryName.Text));
            if (txtIssueDate.SelectedDate.HasValue)
                enquiry = enquiry.Where(p => p.DateOfIssue.Equals(txtIssueDate.SelectedDate));
            if (!string.IsNullOrEmpty(txtIssuingBank.Text))
                enquiry = enquiry.Where(p => p.IssuingBankNo.Equals(txtIssuingBank.Text));
            switch (refId)
            {
                case ExportLC.Actions.Amend:
                    radGridReview.DataSource = enquiry
                        .OrderByDescending(p => p.AmendDate)
                        .Select(q => new { Code = q.AmendNo, q.ImportLCCode, IncreaseAmount = q.IncreaseOfDocumentaryCreditAmount, DecreaseAmount = q.DecreaseOfDocumentaryCreditAmount, q.BeneficiaryNo, q.BeneficiaryName, q.DateOfIssue, q.IssuingBankNo, Status = q.AmendStatus })
                        .ToList();
                    return;
            }
        }
    }
}