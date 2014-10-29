using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using Telerik.Web.UI;
using BankProject.Model;
using BankProject.DBContext;
using System.Data;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class ListExportPayment : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private readonly ExportLC entContext = new ExportLC();
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
                if (lstType != null && lstType.ToLower().Equals("4appr"))
                {
                    var query = (from EX in entContext.B_ExportLCPayments
                                 join BE in entContext.BEXPORT_DOCUMENTPROCESSINGs on EX.LCCode equals BE.PaymentId
                                 join AD in entContext.BAdvisingAndNegotiationLCs on BE.LCCode equals AD.NormalLCCode
                                 where EX.Status == "UNA"
                                 select new { EX, AD });
                    var lst = new List<BAdvisingAndNegotiationLC>();
                    foreach (var item in query)
                    {
                        lst.Add(new BAdvisingAndNegotiationLC
                        {
                            Id=item.EX.Id,
                            NormalLCCode=item.EX.LCCode,
                            Amount=item.AD.Amount,
                            ApplicantNo=item.AD.ApplicantNo,
                            Currency=item.AD.Currency,
                            ApplicantName=item.AD.ApplicantName,
                            Status=item.EX.Status
                        });
                    }
                    var source = CreateDataset(lst);
                    radGridReview.DataSource = source;
                }
            }
        }
        private DataSet CreateDataset(List<BAdvisingAndNegotiationLC> ds)
        {
            DataSet datasource = new DataSet();//Tab1
            DataTable tbl1 = new DataTable();
            tbl1.Columns.Add("Id");
            tbl1.Columns.Add("LCCode");
            tbl1.Columns.Add("Amount");
            tbl1.Columns.Add("Currency");
            tbl1.Columns.Add("ApplicantNo");
            tbl1.Columns.Add("ApplicantName");
            tbl1.Columns.Add("Status");
            foreach (var item in ds)
            {
                tbl1.Rows.Add(item.Id,item.NormalLCCode,item.Amount,item.Currency,item.ApplicantNo, item.ApplicantName, item.Status);
            }
            datasource.Tables.Add(tbl1);
            return datasource;
        }
        public string GenerateEnquiryButtons(string TId)
        {
            return "<a href=\"Default.aspx?tabid=" + this.TabId + "&amp;tid=" + TId + "&amp;lst=" + lstType + "\"><img src=\"Icons/bank/text_preview.png\" alt=\"\" title=\"\" style=\"\" width=\"20\"> </a>";
        }
    }
}