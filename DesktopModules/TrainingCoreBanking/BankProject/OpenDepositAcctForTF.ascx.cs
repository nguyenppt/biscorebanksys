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

namespace BankProject
{
    public partial class OpenDepositAcctForTF : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();

            rcbCustomerID.DataSource = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            rcbCustomerID.DataTextField = "CustomerName";
            rcbCustomerID.DataValueField = "CustomerID";
            rcbCustomerID.DataBind();

            LoadCurrency(null);
            LoadProductLine(null);
            LoadNewsID();

            

        }

        private void LoadCurrency(string selectedid)
        {
            CurrencyRepository facade = new CurrencyRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(rcbCurrentcy, src, "Code", "Code", "-Select Currency-", false);

            if (!String.IsNullOrEmpty(selectedid))
            {
                rcbCurrentcy.SelectedValue = selectedid;
            }
        }

        private void LoadProductLine(string selectedid)
        {
            ProduceLineRepository facade = new ProduceLineRepository();
            var src = facade.LoadProductLineList("6").ToList();
            Util.LoadData2RadCombo(rcbProductLine, src, "Description", "Description", "-Select Product Line-", true);

            if (!String.IsNullOrEmpty(selectedid))
            {
                rcbProductLine.SelectedValue = selectedid;
            }
        }

        private void LoadNewsID()
        {
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
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                

                DataProvider.Database.B_BACCOUNTS_Insert(rcbCustomerID.SelectedValue, rcbCategoryCode.SelectedItem.Text, rcbCurrentcy.SelectedValue, tbAccountName.Text,
                tbShortName.Text, tbAccountMnemonic.Text, rcbProductLine.SelectedValue, rcbJointHolderID.SelectedValue, rcbRelationCode.SelectedValue, tbNotes.Text, "", "",
                "1", "203_TRADE20_I_INAU", DateTime.Now.ToString(), DateTime.Now.ToString(), "203_TRADE20", "VN-001-1611   CHI NHANH", "1", "", tbDepositCode.Text);

                LoadNewsID();

                tbAccountMnemonic.Text = "";
                tbAccountName.Text = "";
                tbNotes.Text = "";
                tbShortName.Text = "";
                rcbCustomerID.SelectedValue = "";
                rcbCurrentcy.SelectedValue = "";
                rcbProductLine.SelectedValue = "";
                lblCustomer.Text = "";
            }
            
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            DataSet ds = DataProvider.Database.B_BACCOUNTS_GetbyID(tbDepositCode.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {

                lblCurrNo.Text = "1";
                lblInputter.Text = "203_TRADE20_I_INAU";
                lblDateTime.Text = ds.Tables[0].Rows[0]["DateTime"].ToString();
                lblDateTime2.Text = ds.Tables[0].Rows[0]["DateTime2"].ToString();
                lblAuthoriser.Text = "203_TRADE20";
                lblCoCode.Text = "VN-001-1611   CHI NHANH";
                lblDeptCode.Text = "1";
                lblCustomer.Text = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                rcbCustomerID.SelectedValue = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                tbAccountName.Text = ds.Tables[0].Rows[0]["AccountName"].ToString();
                tbAccountMnemonic.Text = ds.Tables[0].Rows[0]["AccountMnemonic"].ToString();
                tbNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
                tbShortName.Text = ds.Tables[0].Rows[0]["ShortName"].ToString();
                rcbCurrentcy.SelectedValue = ds.Tables[0].Rows[0]["Currentcy"].ToString();
                rcbProductLine.SelectedValue = ds.Tables[0].Rows[0]["ProductLine"].ToString();

                lblDepositCode.Text = "TKKQ " + rcbCurrentcy.SelectedValue + " " + rcbCustomerID.SelectedValue; 
                
            }
        }
    }
}