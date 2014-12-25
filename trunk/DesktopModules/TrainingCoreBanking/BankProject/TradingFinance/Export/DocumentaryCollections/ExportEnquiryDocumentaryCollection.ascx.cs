using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Export.DocumentaryCollections
{
    public partial class ExportEnquiryDocumentaryCollection : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var dsCus = DataTam.B_BCUSTOMERS_GetAll();

            DataView dv = new DataView(dsCus.Tables[0]);

            dv.RowFilter = "CustomerID like '2%'"; 
            comboDrawerCusNo.Items.Clear();
            comboDrawerCusNo.Items.Add(new RadComboBoxItem(""));
            comboDrawerCusNo.DataValueField = "CustomerID";
            comboDrawerCusNo.DataTextField = "CustomerID";
            comboDrawerCusNo.DataSource = dv;
            comboDrawerCusNo.DataBind();

            InitToolBar(false);
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void InitToolBar(bool flag)
        {
            RadToolBar2.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar2.FindItemByValue("btRevert").Enabled = flag;
            RadToolBar2.FindItemByValue("btReview").Enabled = flag;
            RadToolBar2.FindItemByValue("btSave").Enabled = flag;
            RadToolBar2.FindItemByValue("btPrint").Enabled = flag;
        }

        protected string geturlReview(string Id, string Status)
        {
            return "Default.aspx?tabid=226" + "&CodeID=" + Id + "&enquiry=true" + "&Status=" + Status;
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_GetByAmendment(txtCode.Text.Trim(),
                    txtDrawee.Text, txtDraweeAddr.Text.Trim(),
                    comboDrawerCusNo.SelectedValue, txtDrawerAddr.Text.Trim(),
                    "UNA", UserId.ToString());
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

        protected void comboDraweeCusNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
            e.Item.Attributes["CustomerName2"] = row["CustomerName2"].ToString();
        }

        protected void Search()
        {
            radGridReview.DataSource = SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_GetByAmendment(txtCode.Text.Trim(),
                    txtDrawee.Text, txtDraweeAddr.Text.Trim(),
                    comboDrawerCusNo.SelectedValue, txtDrawerAddr.Text.Trim(),
                    "UNA", UserId.ToString());
            radGridReview.DataBind();
        }
    }
}