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
    public partial class EnquiryCheque : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            var dsCus = DataTam.B_BCUSTOMERS_GetAll();
            if (dsCus != null && dsCus.Tables.Count > 0 && dsCus.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsCus.Tables[0].NewRow();
                dr["CustomerID"] = "";
                dr["CustomerName"] = "";
                dsCus.Tables[0].Rows.InsertAt(dr, 0);

                TbCustomerID.Items.Clear();
                TbCustomerID.DataValueField = "CustomerID";
                TbCustomerID.DataTextField = "CustomerName";
                TbCustomerID.DataSource = dsCus;
                TbCustomerID.DataBind();
            }

            LoadToolBar(false);
        }

        protected void LoadToolBar(bool flag)
        {
            RadToolBar2.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar2.FindItemByValue("btRevert").Enabled = flag;
            RadToolBar2.FindItemByValue("btReview").Enabled = flag;
            RadToolBar2.FindItemByValue("btSave").Enabled = flag;
            RadToolBar2.FindItemByValue("btPrint").Enabled = flag;
            RadToolBar2.FindItemByValue("btSearch").Enabled = !flag;
        }
        protected void btSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = TriTT.B_BCHEQUERETURN_findItem("", "", "","");    
        }
        protected void RadToolBar2_OnButtonClick(object sender, RadToolBarEventArgs e)
        { 
            var toolbarButton = e.Item as RadToolBarButton;
            var CommandName = toolbarButton.CommandName;
            if (CommandName == "search")
            {
                Search();
            }
        }
        protected void Search()
        {

            ///////////////
            if (string.IsNullOrEmpty(tbChequeReference.Text.Trim()) && string.IsNullOrEmpty(tbCustomerName.Text.Trim()) 
                && string.IsNullOrEmpty(TbCustomerID.Text.Trim())
            && string.IsNullOrEmpty(rdpIssueDate.SelectedDate.ToString().Trim()))
                
                RadGrid1.DataSource = TriTT.B_BCHEQUERETURN_findItem("", "", "","");

            else 
                RadGrid1.DataSource = TriTT.B_BCHEQUERETURN_findItem(tbChequeReference.Text.Trim(), TbCustomerID.SelectedValue, tbCustomerName.Text.Trim(),
                rdpIssueDate.SelectedDate.ToString());
            RadGrid1.DataBind();
        }

        protected string geturlReview(string Id)
        {
            return "Default.aspx?tabid=136&PRCode=0&IsAuthorize=1";
        }
    }
    
}