﻿using System;
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
    public partial class EnquiryLC : DotNetNuke.Entities.Modules.PortalModuleBase
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
            {
                refId = ExportLC.Actions.Register;
            }
            //RadToolBar1.FindItemByValue("btSearch").Enabled = (string.IsNullOrEmpty(lstType) || !lstType.ToLower().Equals("4appr"));
            RadToolBar1.FindItemByValue("btSearch").Enabled = true;
            if (IsPostBack) return;
            bc.Commont.initRadComboBox(ref rcbBeneficiaryNumber, "CustomerName", "CustomerID", bd.SQLData.B_BCUSTOMERS_OnlyBusiness());
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
            //if (!string.IsNullOrEmpty(lstType) && lstType.ToLower().Equals("4appr")) 
                loadData();
        }

        private void loadData()
        {
            string status = "";
            if (!string.IsNullOrEmpty(lstType) && lstType.ToLower().Equals("4appr"))
                status = bd.TransactionStatus.UNA;
            //
            IQueryable<BEXPORT_LC> enquiry = dbEntities.BEXPORT_LC.AsQueryable();            
            switch (refId)
            {
                case ExportLC.Actions.Confirm:
                    enquiry = enquiry.Where(p => p.ConfirmStatus.Equals(status));
                    break;
                case ExportLC.Actions.Cancel:
                    enquiry = enquiry.Where(p => p.CancelStatus.Equals(status));
                    break;
                case ExportLC.Actions.Close:
                    enquiry = enquiry.Where(p => p.ClosedStatus.Equals(status));
                    break;
                case ExportLC.Actions.Amend:
                    enquiry = enquiry.Where(p => p.AmendStatus.Equals(status));
                    break;
                default:// ExportLC.Actions.Register:
                    if (status.Equals(""))
                    {
                        enquiry = enquiry.Where(p => p.Status.Equals(bd.TransactionStatus.AUT) 
                                                    || p.Status.Equals(bd.TransactionStatus.UNA) 
                                                    || p.Status.Equals(bd.TransactionStatus.REV));
                    }
                    else
                    {
                        enquiry = enquiry.Where(p => p.Status.Equals(status));
                    }
                    break;
            }
                        
            if (!string.IsNullOrEmpty(txtRefNo.Text))
                enquiry = enquiry.Where(p => p.ExportLCCode.Equals(txtRefNo.Text));
            if (!string.IsNullOrEmpty(txtApplicantName.Text))
                enquiry = enquiry.Where(p => p.ApplicantName.Contains(txtApplicantName.Text));
            if (rcbBeneficiaryNumber.SelectedIndex > 0)
                enquiry = enquiry.Where(p => p.BeneficiaryNo.Equals(rcbBeneficiaryNumber.SelectedValue));
            if (txtIssueDate.SelectedDate.HasValue)
                enquiry = enquiry.Where(p => p.DateOfIssue.Value >= txtIssueDate.SelectedDate.Value);
            if (txtIssueDateTo.SelectedDate.HasValue)
                enquiry = enquiry.Where(p => p.DateOfIssue.Value <= txtIssueDateTo.SelectedDate.Value);
            if (!string.IsNullOrEmpty(txtIssuingBank.Text))
                enquiry = enquiry.Where(p => p.IssuingBankNo.Equals(txtIssuingBank.Text));
            if (!string.IsNullOrEmpty(txtDocumentaryCreditNumber.Text))
                enquiry = enquiry.Where(p => p.ImportLCCode.Equals(txtDocumentaryCreditNumber.Text));
            switch (refId)
            {
                case ExportLC.Actions.Confirm:
                    radGridReview.DataSource = enquiry
                        .OrderByDescending(p => p.ConfirmDate)
                        .Select(q => new { Code = q.ExportLCCode, q.ImportLCCode, q.ApplicantName, q.Amount, q.Currency, q.BeneficiaryNo, q.BeneficiaryName, q.DateOfIssue, q.IssuingBankNo, Status = q.ConfirmStatus })
                        .ToList();
                    //radGridReview.DataBind();
                    return;
                case ExportLC.Actions.Cancel:
                    radGridReview.DataSource = enquiry
                        .OrderByDescending(p => p.CancelDate)
                        .Select(q => new { Code = q.ExportLCCode, q.ImportLCCode, q.ApplicantName, q.Amount, q.Currency, q.BeneficiaryNo, q.BeneficiaryName, q.DateOfIssue, q.IssuingBankNo, Status = q.CancelStatus })
                        .ToList();
                    //radGridReview.DataBind();
                    return;
                case ExportLC.Actions.Close:
                    radGridReview.DataSource = enquiry
                        .OrderByDescending(p => p.ClosedDate)
                        .Select(q => new { Code = q.ExportLCCode, q.ImportLCCode, q.ApplicantName, q.Amount, q.Currency, q.BeneficiaryNo, q.BeneficiaryName, q.DateOfIssue, q.IssuingBankNo, Status = q.ClosedStatus })
                        .ToList();
                    //radGridReview.DataBind();
                    return;
                default:// ExportLC.Actions.Register:
                    var data = enquiry
                        .OrderByDescending(p => p.CreateDate)
                        .Select(q => new { Code = q.ExportLCCode, q.ImportLCCode, q.ApplicantName, q.Amount, q.Currency, q.BeneficiaryNo, q.BeneficiaryName, q.DateOfIssue, q.IssuingBankNo, Status = q.Status })
                        .ToList();
                    radGridReview.DataSource = data;
                    //radGridReview.DataBind();
                    return;
            }
        }
    
    
    }
}