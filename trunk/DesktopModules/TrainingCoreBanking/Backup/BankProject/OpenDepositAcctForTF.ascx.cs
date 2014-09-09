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
    public partial class OpenDepositAcctForTF : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();

            rcbCustomerID.DataSource = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            rcbCustomerID.DataTextField = "CustomerID";
            rcbCustomerID.DataValueField = "CustomerName";
            rcbCustomerID.DataBind();

            DataSet ds = DataProvider.DataTam.B_BDEPOSITACCTS_GetNewID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                tbDepositCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
            }

        }
        protected void rcbCustomerID_SelectIndexChange(object sender, EventArgs e)
        {
            lblCustomer.Text = rcbCustomerID.SelectedValue.ToString();
        }
        private void LoadToolBar()
        {
            //RadToolBar1.FindItemByValue("bplaybackprev").Enabled = false;
            //RadToolBar1.FindItemByValue("btplaybacknext").Enabled = false;
            //RadToolBar1.FindItemByValue("btdoclines").Enabled = false;
            RadToolBar1.FindItemByValue("btdocnew").Enabled = false;
            RadToolBar1.FindItemByValue("btdraghand").Enabled = false;
            RadToolBar1.FindItemByValue("btsearch").Enabled = false;
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "doclines")
            {
                lblCurrNo.Text = "1";
                lblInputter.Text = "203_TRADE20_I_INAU";
                lblDateTime.Text = DateTime.Now.ToString();
                lblDateTime2.Text = DateTime.Now.ToString();
                lblAuthoriser.Text = "203_TRADE20";
                lblCoCode.Text = "VN-001-1611   CHI NHANH";
                lblDeptCode.Text = "1";
                lblCustomerID.Text = rcbCustomerID.SelectedItem.Text;
                rcbCustomerID.Visible = false;
                tbDepositCode.Enabled = false;
                lblDepositCode.Text = "TKKQ " + rcbCurrentcy.SelectedValue + " " + rcbCustomerID.SelectedValue;   
            }
        }
    }
}