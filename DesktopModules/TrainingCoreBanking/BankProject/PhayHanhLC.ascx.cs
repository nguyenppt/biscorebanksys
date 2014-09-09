using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject
{
    public partial class PhayHanhLC : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();

            DataSet dsc = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            rcbOrderedby.DataSource = dsc;
            rcbOrderedby.DataTextField = "CustomerName";
            rcbOrderedby.DataValueField = "CustomerID";
            rcbOrderedby.DataBind();

            rcbDebitAccount.DataTextField = "ID";
            rcbDebitAccount.DataValueField = "Name";
            rcbDebitAccount.DataSource = DataProvider.KhanhND.B_BDEBITACCOUNTS_GetByCurrency("VND");
            rcbDebitAccount.DataBind();


            if (Request.QueryString["lid"] != null)
            {
                tbDepositCode.Text = Request.QueryString["lid"].ToString().Replace("-", "/");
                tbLCNo.Text = Request.QueryString["lid"].ToString().Replace("-", "");
                string lccode = Request.QueryString["lid"].ToString();
                LoadData(lccode);
                if (Request.QueryString["disable"] != null)
                {
                    hdfDisable.Value = Request.QueryString["disable"].ToString();
                    if (hdfDisable.Value == "1")
                    {
                        BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                        RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    }

                }

            }
            else
            {
                DataSet ds = DataProvider.DataTam.B_ISSURLC_GetNewID();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tbDepositCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                }
                rdpDebitDDate.SelectedDate = DateTime.Now;
                rdpDebitDDate.Enabled = false;
                rdpCreditDate.SelectedDate = DateTime.Now;
                rdpCreditDate.Enabled = false;
            }
            Session["DataKey"] = tbDepositCode.Text;
            tbAddResmarks.ReLoadControl(tbDepositCode.Text);

        }

        private void LoadData(string lccode)
        {
            DataSet ds = DataProvider.Database.B_BNORMAILLCPROVITIONTRANSFER_GetByNormalLCCode(lccode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rcbOrderedby.SelectedValue = ds.Tables[0].Rows[0]["Orderedby"].ToString();
                tbDebitRef.Text = ds.Tables[0].Rows[0]["DebitRef"].ToString();
                rcbDebitAccount.SelectedValue = ds.Tables[0].Rows[0]["DebitAccount"].ToString();
                rcbDebitCurrency.SelectedValue = ds.Tables[0].Rows[0]["DebitCurrency"].ToString();
                tbDebitAmout.Text = ds.Tables[0].Rows[0]["DebitAmout"].ToString();
                rdpDebitDDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["DebitDate"].ToString());
                tbCreditAccount.Text = ds.Tables[0].Rows[0]["CreditAccount"].ToString();
                lblCreditCurrency.Text = ds.Tables[0].Rows[0]["CreditCurrency"].ToString();
                tbTreasuryRate.Text = ds.Tables[0].Rows[0]["TreasuryRate"].ToString();
                tbCreditAmount.Text = ds.Tables[0].Rows[0]["CreditAmount"].ToString();
                rdpCreditDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreditDate"].ToString());
                tbVATSerialNo.Text = ds.Tables[0].Rows[0]["VATSerialNo"].ToString();
                lblDebitAccount.Text = "TKTT " + rcbDebitAccount.SelectedValue;
                //lblCreditAccount.Text = "TKKQ " +tbCreditAccount.Text;
                lblOrderedby.Text = rcbOrderedby.SelectedValue;
                if (ds.Tables[0].Rows[0]["Status"].ToString() == "UNA" && Request.QueryString["disable"] != null)
                {
                    RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;

                }
                LoadCreaditAccount();
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            }
        }
        private void LoadToolBar()
        {
            //RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            //RadToolBar1.FindItemByValue("btSearch").Enabled = false;
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "print" || commandName == "commit")
            {
                //lblDebitCurrency.Text = "VND  <span style='color:Blue;'><i>Dong</i></span>";
                //lblCreditCurrency.Text = "USD <span style='color:Blue;'><i>USD</i></span>";
                lblDebitAccount.Text = "TKTT " + rcbDebitAccount.SelectedValue;
                lblCreditAccount.Text = "TKKQ " + tbCreditAccount.Text;
                DataProvider.Database.B_BNORMAILLCPROVITIONTRANSFER_Insert(Request.QueryString["lid"].ToString(), rcbOrderedby.SelectedValue, tbDebitRef.Text, rcbDebitAccount.SelectedValue,
                    rcbDebitCurrency.SelectedValue, tbDebitAmout.Text, rdpDebitDDate.SelectedDate.ToString(), tbCreditAccount.Text, lblCreditCurrency.Text, tbTreasuryRate.Text,
                    tbCreditAmount.Text, rdpCreditDate.SelectedDate.ToString(), tbVATSerialNo.Text);
                RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                if(Request.QueryString["disable"]==null)
                    DataProvider.KhanhND.B_BNORMAILLCPROVITIONTRANSFER_UpdateStatus("UNA", tbLCNo.Text);
            }
            else if (commandName == "Preview")
            {
                Response.Redirect("Default.aspx?tabid=92&ctl=reviewlist&mid=462");
            }
            else if (commandName == "authorize")
            {
                DataProvider.KhanhND.B_BNORMAILLCPROVITIONTRANSFER_UpdateStatus("AUT", tbLCNo.Text);
                RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            }
            else if (commandName == "reverse")
            {
                DataProvider.KhanhND.B_BNORMAILLCPROVITIONTRANSFER_UpdateStatus("REV", tbLCNo.Text);
                RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            }
            else if (commandName == "search")
            {
                LoadData(tbDepositCode.Text);
            }

        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            
        }

        protected void tbDepositCode_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = DataProvider.Database.B_BACCOUNTS_GetbyID(tbDepositCode.Text);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                RadWindowManager1.RadAlert("LC No. not exist!", 200, 100, "Message", "");
                tbDepositCode.Text = "";
                tbLCNo.Text = "";
            }
            else
            {
                tbLCNo.Text = tbDepositCode.Text;
            }
        }

        protected void tbCreditAccount_TextChanged(object sender, EventArgs e)
        {
            LoadCreaditAccount();
        }

        private void LoadCreaditAccount()
        {
            DataSet ds = DataProvider.KhanhND.B_BCUSTOMERS_GetByDepositCode(tbCreditAccount.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblCreditAccount.Text = "TKKQ " + ds.Tables[0].Rows[0]["CustomerName"].ToString();
                lblCreditCurrency.Text = ds.Tables[0].Rows[0]["Currentcy"].ToString();
            }
        }
    }
}