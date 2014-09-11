using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TradingFinance
{
    public partial class EnquiryOverseasFundsTransfer : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            InitToolBar(false);

            comboTransactionType.Items.Clear();
            comboTransactionType.Items.Add(new RadComboBoxItem(""));
            comboTransactionType.DataValueField = "Description";
            comboTransactionType.DataTextField = "Id";
            comboTransactionType.DataSource = SQLData.CreateGenerateDatas("TabAccountTransfer_TransactionType");
            comboTransactionType.DataBind();

            comboCountryCode.Items.Clear();
            comboCountryCode.Items.Add(new RadComboBoxItem(""));
            comboCountryCode.DataValueField = "TenTA";
            comboCountryCode.DataTextField = "TenTA";
            comboCountryCode.DataSource = SQLData.B_BCOUNTRY_GetAll();
            comboCountryCode.DataBind();
        }

        protected void InitToolBar(bool flag)
        {
            RadToolBar2.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar2.FindItemByValue("btRevert").Enabled = flag;
            RadToolBar2.FindItemByValue("btReview").Enabled = flag;
            RadToolBar2.FindItemByValue("btSave").Enabled = flag;
            RadToolBar2.FindItemByValue("btPrint").Enabled = flag;
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
        }

        protected void comboTransactionType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }

        protected void LoadCommoditySerByTransactionType()
        {
            if (comboTransactionType.SelectedItem == null)
            {
                comboCommoditySer.Items.Clear();
                comboCommoditySer.Items.Add(new RadComboBoxItem(""));
                comboCommoditySer.DataTextField = "Name";
                comboCommoditySer.DataValueField = "Id";
                comboCommoditySer.DataSource = SQLData.B_BCOMMODITY_GetByTransactionType("XXX");
                comboCommoditySer.DataBind();
            }
            else
            {
                comboCommoditySer.Items.Clear();
                comboCommoditySer.Items.Add(new RadComboBoxItem(""));
                comboCommoditySer.DataTextField = "Name";
                comboCommoditySer.DataValueField = "Id";
                comboCommoditySer.DataSource = SQLData.B_BCOMMODITY_GetByTransactionType(comboTransactionType.SelectedItem.Attributes["Id"].Substring(0, 3));
                comboCommoditySer.DataBind();
            }
        }

        protected void comboTransactionType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadCommoditySerByTransactionType();
        }

        protected string geturlReview(string Id)
        {
            return "Default.aspx?tabid=251" + "&CodeID=" + Id;
        }

        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = SQLData.B_BDOCUMETARYCOLLECTION_GetByEnquiry(txtCode.Text.Trim(), comboTransactionType.SelectedValue, comboCountryCode.SelectedValue, comboCommoditySer.SelectedValue, txtCusId.Text.Trim(), txtCusName.Text.Trim(), UserId);
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

        protected void Search()
        {
            radGridReview.DataSource = SQLData.B_BDOCUMETARYCOLLECTION_GetByEnquiry(txtCode.Text.Trim(),comboTransactionType.SelectedValue,comboCountryCode.SelectedValue, comboCommoditySer.SelectedValue, txtCusId.Text.Trim(), txtCusName.Text.Trim(), UserId);
            radGridReview.DataBind();
        }
    }
}