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
    public partial class EnquiryLC : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private readonly ExportLC entContext = new ExportLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            InitToolBar(false);
            //bind ApplicantID
            var dsApp = entContext.BAdvisingAndNegotiationLCs.ToList();
            BindBeneficiary(dsApp);
            BindApplicant(dsApp);
            
            //bind Beneficiary
        }
        protected void BindBeneficiary(List<BAdvisingAndNegotiationLC> dsApp)
        {

            var resultBE = dsApp.Select(x => x.BeneficiaryNo).Distinct().ToList();
            rcbBeneficiary.Items.Clear();
            rcbBeneficiary.Items.Add(new RadComboBoxItem(""));
            rcbBeneficiary.DataTextField = "BeneficiaryNo";
            rcbBeneficiary.DataValueField = "BeneficiaryName";
            //rcbBeneficiary.DataSource = CreateDataset(resultBE, "Benef");

            DataSet datasourceBE = new DataSet();//Tab1
            DataTable tblBE = new DataTable();
            tblBE.Columns.Add("BeneficiaryNo");
            tblBE.Columns.Add("BeneficiaryName");
            foreach (var item in resultBE)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    tblBE.Rows.Add(item, item);
                }

            }
            datasourceBE.Tables.Add(tblBE);
            rcbBeneficiary.DataSource = datasourceBE;
            rcbBeneficiary.DataBind();
        }
        protected string geturlReview(string Id)
        {
            return "Default.aspx?tabid=242" + "&CodeID=" + Id + "&enquiry=true";
        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var obj = entContext.BAdvisingAndNegotiationLCs.ToList();
            radGridReview.DataSource = obj;
        }
        protected void btSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        protected void Search()
        {
            var ds = entContext.BAdvisingAndNegotiationLCs.ToList();
            if (!String.IsNullOrEmpty(txtCode.Text))
            {
                ds = ds.Where(x => x.NormalLCCode == txtCode.Text).ToList();  
            }
            if (!String.IsNullOrEmpty(rcbApplicantID.SelectedValue))
            {
                ds = ds.Where(x => x.ApplicantNo == rcbApplicantID.SelectedValue).ToList();
            }
            if (!String.IsNullOrEmpty(txtApplicantName.Text))
            {
                ds = ds.Where(x => x.ApplicantName == txtApplicantName.Text).ToList();
            }
            radGridReview.DataSource = ds;
            //radGridReview.DataSource = SQLData.B_BIMPORT_NORMAILLC_GetByEnquiry(txtCode.Text.Trim(), rcbApplicantID.SelectedValue, txtApplicantName.Text.Trim(), UserId);
            radGridReview.DataBind();
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "search":
                    Search();
                    break;
            }
        }
        protected void BindApplicant(List<BAdvisingAndNegotiationLC> dsApp)
        {
            var result = dsApp.Select(x => x.ApplicantNo).Distinct().ToList();
            //List<BIMPORT_NORMAILLC> dsApp = dsApp.GroupBy(x => x.ApplicantId).Select(g => g.First()).ToList();
            rcbApplicantID.Items.Clear();
            rcbApplicantID.Items.Add(new RadComboBoxItem(""));
            rcbApplicantID.DataTextField = "ApplicantId";
            rcbApplicantID.DataValueField = "ApplicantId";

            DataSet datasource = new DataSet();//Tab1
            DataTable tbl1 = new DataTable();
            tbl1.Columns.Add("ApplicantId");
            tbl1.Columns.Add("ApplicantName");
            foreach (var item in result)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    tbl1.Rows.Add(item, item);
                }

            }
            datasource.Tables.Add(tbl1);
            rcbApplicantID.DataSource = datasource;
            rcbApplicantID.DataBind();
        }
        protected void rcbBeneficiary_ItemDataBound(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var row = e.Text;
            var Name = entContext.BAdvisingAndNegotiationLCs.Where(x => x.BeneficiaryNo == row).FirstOrDefault();
            if (Name == null)
            {
                txtBeneficiaryName.Text = "";
            }
            else
            {
                txtBeneficiaryName.Text = Name.BeneficiaryName;
            }
            Search();
        }
        protected void rcbApplicant_ItemDataBound(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var row = e.Text;
            var Name = entContext.BAdvisingAndNegotiationLCs.Where(x => x.ApplicantNo == row).FirstOrDefault();
            if (Name == null)
            {
                txtApplicantName.Text = "";
            }
            else
            {
                txtApplicantName.Text = Name.ApplicantName;
            }
            Search();
        }
        
        private DataSet CreateDataset(List<BIMPORT_NORMAILLC> ds,string Type)
        {
            
            DataSet datasource = new DataSet();//Tab1
            DataTable tbl1 = new DataTable();
            if (Type == "App")
            {
                tbl1.Columns.Add("ApplicantId");
                tbl1.Columns.Add("ApplicantName");
                foreach (var item in ds)
                {
                    tbl1.Rows.Add(item.ApplicantId, item.ApplicantName);
                    datasource.Tables.Add(tbl1);
                }
            }
            else if(Type=="Benef") {
                tbl1.Columns.Add("BeneficiaryNo");
                tbl1.Columns.Add("BeneficiaryName");
                foreach (var item in ds)
                {
                    tbl1.Rows.Add(item.BeneficiaryNo, item.BeneficiaryName);
                    datasource.Tables.Add(tbl1);
                }
            }
            return datasource;
        }
        protected void InitToolBar(bool flag)
        {
            RadToolBar2.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar2.FindItemByValue("btRevert").Enabled = flag;
            RadToolBar2.FindItemByValue("btReview").Enabled = flag;
            RadToolBar2.FindItemByValue("btSave").Enabled = flag;
            RadToolBar2.FindItemByValue("btPrint").Enabled = flag;
        }
    }
}