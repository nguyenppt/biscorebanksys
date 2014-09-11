using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Import.DocumentaryCredit
{
    public partial class ProvisionTransfer_DC_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();
        }
        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = true;
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridReview.DataSource = SQLData.PROVISIONTRANSFER_DC_GetByPreview(txtRefNo.Text.Trim(), txtLCNo.Text.Trim(), "UNA", UserId);
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

        public string geturlReview(string id)
        {

            return "Default.aspx?tabid=279&Codeid=" + id + "&disable=1&enquiry=true";
        }

        protected void Search()
        {
            radGridReview.DataSource = SQLData.PROVISIONTRANSFER_DC_GetByPreview(txtRefNo.Text.Trim(),
                                                                                 txtLCNo.Text.Trim(), "UNA", UserId);
            radGridReview.DataBind();
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}