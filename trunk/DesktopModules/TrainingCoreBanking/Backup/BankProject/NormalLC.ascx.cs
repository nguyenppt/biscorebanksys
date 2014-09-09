using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

namespace BankProject
{
    public partial class NormalLC : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return;
            LoadToolBar();

            DataSet ds = DataProvider.DataTam.B_ISSURLC_GetNewID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                tbEssurLCCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
            }

            rcbLCType.DataTextField = "LCTYPE";
            rcbLCType.DataValueField = "LCTYPE"; 
            rcbLCType.DataSource = DataProvider.DataTam.B_BLCTYPES_GetAll();
            rcbLCType.DataBind();

            rcbApplicantID.DataSource = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            rcbApplicantID.DataTextField = "CustomerID";
            rcbApplicantID.DataValueField = "CustomerName";
            rcbApplicantID.DataBind();
        }
        protected void rcbApplicantID_SelectIndexChange(object sender, EventArgs e)
        {
            lblCustomer.Text = rcbApplicantID.SelectedValue.ToString();
        }
        protected void rcbLCType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["LCTYPE"] = row["LCTYPE"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
            e.Item.Attributes["Category"] = row["Category"].ToString();
        } 
        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btdocnew").Enabled = false;
            RadToolBar1.FindItemByValue("btdraghand").Enabled = false;
            RadToolBar1.FindItemByValue("btsearch").Enabled = false;
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "YourCommandName")
            {
                //Your logic
            }
        }
    }
}