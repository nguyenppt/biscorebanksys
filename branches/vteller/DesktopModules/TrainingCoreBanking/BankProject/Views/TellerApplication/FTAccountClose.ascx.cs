using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using BankProject.Repository;
using System.Data;

namespace BankProject.Views.TellerApplication
{
    public partial class FTAccountClose : PortalModuleBase
    {
        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btdocnew").Enabled = false;
            RadToolBar1.FindItemByValue("btdraghand").Enabled = false;
            RadToolBar1.FindItemByValue("btsearch").Enabled = false;
            RadToolBar1.FindItemByValue("searchNew").Enabled = false;
            RadToolBar1.FindItemByValue("print").Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadToolBar();
                this.lblDebitDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                lbDateTime1.Text = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
                lbDateTime2.Text = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
                if (Request.QueryString["codeid"] != null)
                {
                    int codeId = int.Parse(Request.QueryString["codeid"]);
                    ListItemRepository repository = new ListItemRepository();
                    Entity.ListItem listItem = repository.GetCloseAccountCodeById(codeId);
                    this.txtId.Text = listItem.Code;
                    lblClosedAccount.Text = listItem.Code;
                    lblCustomerName.Text = listItem.CustomerName;
                    lblDebitAmount.Text = listItem.OnlineActual;
                    lblCurrency.Text = listItem.Currency;
                    lblCurrencyUnit.Text = listItem.Currency == "VND" ? "DONG" : "$";
                }
            }
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "doclines":
                    RadToolBar1.FindItemByValue("btdocnew").Enabled = true;
                    BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                    break;
                case "docnew":
                    string urlFTAccountClose = this.EditUrl("ReviewCloseAccountList");
                this.Response.Redirect(urlFTAccountClose);
                    break;
                default:
                    break;
            }                   
        }
        protected void cmbCreditCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbAccountPaid.Items.Clear();
            DataSet ds = BankProject.DataProvider.Database.B_BDRFROMACCOUNT_GetByCustomer("", cmbCreditCurrency.SelectedValue);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr2 = ds.Tables[0].NewRow();
                dr2["DisplayHasCurrency"] = "USD10001.1221.1131";
                dr2["Id"] = "USD10001.1221.1131";
                ds.Tables[0].Rows.InsertAt(dr2, 0);

                DataRow dr3 = ds.Tables[0].NewRow();
                dr3["DisplayHasCurrency"] = "EUR10001.1221.1131";
                dr3["Id"] = "EUR10001.1221.1131";
                ds.Tables[0].Rows.InsertAt(dr3, 0);

                DataRow dr4 = ds.Tables[0].NewRow();
                dr4["DisplayHasCurrency"] = "GBP10001.1221.1131";
                dr4["Id"] = "GBP10001.1221.1131";
                ds.Tables[0].Rows.InsertAt(dr4, 0);

                DataRow dr5 = ds.Tables[0].NewRow();
                dr5["DisplayHasCurrency"] = "JPY10001.1221.1131";
                dr5["Id"] = "JPY10001.1221.1131";

                DataRow dr1 = ds.Tables[0].NewRow();
                dr1["DisplayHasCurrency"] = "VND10001.1221.1131";
                dr1["Id"] = "VND10001.1221.1131";
                ds.Tables[0].Rows.InsertAt(dr1, 0);

                DataRow dr = ds.Tables[0].NewRow();
                dr["DisplayHasCurrency"] = "";
                dr["Id"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);

                cmbAccountPaid.DataTextField = "DisplayHasCurrency";
                cmbAccountPaid.DataValueField = "Id";
                cmbAccountPaid.DataSource = ds;
                cmbAccountPaid.DataBind();
            }
        }
    }
}