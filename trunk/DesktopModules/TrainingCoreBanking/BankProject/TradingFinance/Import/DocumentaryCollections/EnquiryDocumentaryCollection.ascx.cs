using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Import.DocumentaryCollections
{
    public partial class EnquiryDocumentaryCollection : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var dsCus = SQLData.B_BCUSTOMERS_OnlyBusiness();

            comboDraweeCusNo.Items.Clear();
            comboDraweeCusNo.Items.Add(new RadComboBoxItem(""));
            comboDraweeCusNo.DataValueField = "CustomerID";
            comboDraweeCusNo.DataTextField = "CustomerID";
            comboDraweeCusNo.DataSource = dsCus;
            comboDraweeCusNo.DataBind();

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

        protected string geturlReview(string Id)
        {
            return "Default.aspx?tabid=217" + "&CodeID=" + Id + "&disable=1&enquiry=true";
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = SQLData.B_BDOCUMETARYCOLLECTION_GetByAmendment(txtCode.Text.Trim(),
                    comboDraweeCusNo.SelectedValue, txtDraweeAddr.Text.Trim(),
                    comboDrawerCusNo.Text, txtDrawerAddr.Text.Trim(),
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
            radGridReview.DataSource = SQLData.B_BDOCUMETARYCOLLECTION_GetByAmendment(txtCode.Text.Trim(),
                    comboDraweeCusNo.SelectedValue, txtDraweeAddr.Text.Trim(),
                    comboDrawerCusNo.Text, txtDrawerAddr.Text.Trim(),
                    "UNA", UserId.ToString());
            radGridReview.DataBind();
        }
    }
}