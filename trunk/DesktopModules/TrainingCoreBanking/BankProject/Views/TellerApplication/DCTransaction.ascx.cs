using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class DCTransaction : PortalModuleBase
    {
        private static int Id = 7890;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            txtId.Text = TriTT.B_BMACODE_GetNewID_2("COLL_CONTIN_ENTRY", "DC", "-");

            LoadToolBar();

            DataSet dsc = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            rcbCustomerID.DataSource = dsc;
            rcbCustomerID.DataTextField = "CustomerName";
            rcbCustomerID.DataValueField = "CustomerID";
            rcbCustomerID.DataBind();
        }

        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {

            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                Id++;
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                txtId.Text = TriTT.B_BMACODE_GetNewID_2("COLL_CONTIN_ENTRY", "DC", "-");
                //this.txtId.Text = "TT/09161/0" + Id.ToString();
                //this.cmbCustomerAccount.SelectedIndex = 0;
                //this.txtAmtFCY.Text = string.Empty;
                //this.txtAmtLCY.Text = string.Empty;
                //this.txtNarrative.Text = string.Empty;

                //this.cmbCurrencyPaid.SelectedIndex = 0;
                //this.txtDealRate.Text = string.Empty;
                //this.cmbWaiveCharges.SelectedIndex = 0;
            }
        }

        protected void rcbCustomerID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerName"] = row["CustomerName2"].ToString();
            e.Item.Attributes["Address"] = row["Address"].ToString();
            e.Item.Attributes["IdentityNo"] = row["IdentityNo"].ToString();
            e.Item.Attributes["IssueDate"] = row["IssueDate"].ToString();
            e.Item.Attributes["IssuePlace"] = row["IssuePlace"].ToString();
        }
        
    }
}