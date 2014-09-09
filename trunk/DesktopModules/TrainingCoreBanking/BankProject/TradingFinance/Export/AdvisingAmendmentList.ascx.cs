using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject
{
    public partial class AdvisingAmendmentList : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            rcbLCType.Items.Clear();
            rcbLCType.Items.Add(new RadComboBoxItem(""));
            rcbLCType.DataTextField = "Description";
            rcbLCType.DataValueField = "LCTYPE";
            rcbLCType.DataSource = DataProvider.DataTam.B_BLCTYPES_GetAll();
            rcbLCType.DataBind();

            DataSet dsc = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            rcbBeneficiaryCustNo.DataSource = dsc;
            rcbBeneficiaryCustNo.DataTextField = "CustomerName";
            rcbBeneficiaryCustNo.DataValueField = "CustomerID";
            rcbBeneficiaryCustNo.DataBind();
        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LoadData(true);
        }
        public string geturlReview(string id)
        {
           //Response.Redirect(EditUrl("chitiet") + "&LCCode=" + id + "&IsAmendment=1");
            return "Default.aspx?tabid=242" + "&LCCode=" + id + "&IsAmendment=1"; //233 la advisingLC
        }

        protected void rcbLCType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["LCTYPE"] = row["LCTYPE"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
            e.Item.Attributes["Category"] = row["Category"].ToString();
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "docnew") LoadData(false);
        }

        private void LoadData(bool isfirst)
        {
            if (isfirst)
                radGridReview.DataSource = DataProvider.Database.B_ADVISING_GetSearch("NOTFILL", "NOTFILL", "NOTFILL");
            else
            {
                radGridReview.DataSource = DataProvider.Database.B_ADVISING_GetSearch(tbEssurLCCode.Text, rcbLCType.SelectedValue, rcbBeneficiaryCustNo.SelectedValue);
                radGridReview.DataBind();
            }
        }
    }
}