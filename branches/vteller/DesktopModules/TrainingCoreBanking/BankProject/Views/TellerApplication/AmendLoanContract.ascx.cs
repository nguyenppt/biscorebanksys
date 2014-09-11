using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class AmendLoanContract : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            else
            {
                if (Request.QueryString["CustomerID"] != null)
                    Response.Redirect(EditUrl("CustomerID", Request.QueryString["CustomerID"], "AmendLoanContract_Item"));
                else
                {
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = true;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                    RadGrid.DataSource = TriTT.B_BPastDueLoanRepayment_FindItem("", "", "");
                }
            }
        }
        public string getUrlReview(string CustomerID)
        {
            return "Default.aspx?tabid=" + this.TabId.ToString() + "&&CustomerID=" + CustomerID.ToString();
        }

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolbarButton = e.Item as RadToolBarButton;
            var CommandName = toolbarButton.CommandName;
            if (CommandName == "search")
            {
                Search();
            }
        }
        protected void RadGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid.DataSource = TriTT.B_BPastDueLoanRepayment_FindItem("!", "", "");
        }
        protected void Search()
        {
            //if (string.IsNullOrEmpty(tbLoanContractReference.Text.Trim()) && string.IsNullOrEmpty(tbCustomerName.Text.Trim())
            //    && string.IsNullOrEmpty(TbCustomerID.Text.Trim()) & string.IsNullOrEmpty(rdpRepayDate.SelectedDate.ToString().Trim()))
            //    RadGrid1.DataSource = TriTT.B_BPastDueLoanRepayment_FindItem("!", "", "");
            //else
            //    RadGrid1.DataSource = TriTT.B_BPastDueLoanRepayment_FindItem(tbLoanContractReference.Text.Trim(), TbCustomerID.Text.Trim(), tbCustomerName.Text.Trim());//,rdpRepayDate.SelectedDate.ToString().Trim()
            //RadGrid1.DataBind();
        }
       
    }
}