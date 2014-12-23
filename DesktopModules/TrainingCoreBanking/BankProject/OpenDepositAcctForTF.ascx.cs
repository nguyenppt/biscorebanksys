using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using BankProject.DBRespository;
using BankProject.Common;
using bc = BankProject.Controls;

namespace BankProject
{
    public partial class OpenDepositAcctForTF : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            bc.Commont.initRadComboBox(ref rcbCustomerID, "CustomerName", "CustomerID", DataProvider.DataTam.B_BCUSTOMERS_GetAll());
            //
            CurrencyRepository cr = new CurrencyRepository();
            Util.LoadData2RadCombo(rcbCurrentcy, cr.GetAll().ToList(), "Code", "Code", "-Select Currency-", false);
            //
            ProduceLineRepository pr = new ProduceLineRepository();
            Util.LoadData2RadCombo(rcbProductLine, pr.LoadProductLineList("6").ToList(), "Description", "Description", "-Select Product Line-", true);
            if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                tbDepositCode.Text = Request.QueryString["tid"];
                loadDetail();
            }
            else LoadNewsID();
        }

        private void LoadNewsID()
        {
            DataSet ds = DataProvider.DataTam.B_BDEPOSITACCTS_GetNewID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                tbDepositCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName.Equals(bc.Commands.Commit))
            {
                DataProvider.Database.B_BACCOUNTS_Insert(rcbCustomerID.SelectedValue, rcbCategoryCode.SelectedItem.Text, rcbCurrentcy.SelectedValue, tbAccountName.Text,
                tbShortName.Text, tbAccountMnemonic.Text, rcbProductLine.SelectedValue, rcbJointHolderID.SelectedValue, rcbRelationCode.SelectedValue, tbNotes.Text, "", "",
                "1", "203_TRADE20_I_INAU", DateTime.Now.ToString(), DateTime.Now.ToString(), "203_TRADE20", "VN-001-1611   CHI NHANH", "1", "", tbDepositCode.Text);
                Response.Redirect("Default.aspx?tabid=" + this.TabId);
            }
            
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            loadDetail();
        }

        private void loadDetail()
        {
            string depositCode = tbDepositCode.Text.Trim();
            if (string.IsNullOrEmpty(depositCode)) return;
            DataSet ds = DataProvider.Database.B_BACCOUNTS_GetbyID(tbDepositCode.Text);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                lblCurrNo.Text = "1";
                lblInputter.Text = "203_TRADE20_I_INAU";
                lblDateTime.Text = dt.Rows[0]["DateTime"].ToString();
                lblDateTime2.Text = dt.Rows[0]["DateTime2"].ToString();
                lblAuthoriser.Text = "203_TRADE20";
                lblCoCode.Text = "VN-001-1611   CHI NHANH";
                lblDeptCode.Text = "1";
                rcbCustomerID.SelectedValue = dt.Rows[0]["CustomerID"].ToString();
                tbAccountName.Text = dt.Rows[0]["AccountName"].ToString();
                tbAccountMnemonic.Text = dt.Rows[0]["AccountMnemonic"].ToString();
                tbNotes.Text = dt.Rows[0]["Notes"].ToString();
                tbShortName.Text = dt.Rows[0]["ShortName"].ToString();
                rcbCurrentcy.SelectedValue = dt.Rows[0]["Currentcy"].ToString();
                rcbProductLine.SelectedValue = dt.Rows[0]["ProductLine"].ToString();

                lblDepositCode.Text = "TKKQ " + rcbCurrentcy.SelectedValue + " " + rcbCustomerID.SelectedValue;
            }
        }
    }
}