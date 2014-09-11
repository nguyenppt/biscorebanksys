using BankProject.Repository;
using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class FTAccountClose2 : PortalModuleBase
    {
        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btnCommit").Enabled = false;
            RadToolBar1.FindItemByValue("btnPreview").Enabled = false;
            RadToolBar1.FindItemByValue("searchNew").Enabled = false;
            RadToolBar1.FindItemByValue("print").Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.lblDateTime1.Text = DateTime.Now.ToString("MM/dd/yyyy");
                this.lblDateTime2.Text = DateTime.Now.ToString("MM/dd/yyyy");
                this.LoadToolBar();
                this.InitToolBar(true);
                this.lblDebitDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }

            if (Request.QueryString["codeid"] != null)
            {
                int codeId = int.Parse(Request.QueryString["codeid"]);
                ListItemRepository repository = new ListItemRepository();
                Entity.ListItem listItem = repository.GetFTAccountCodeById(codeId);
                Entity.ListItem listItem2 = repository.GetCloseAccountCodeById(codeId);
                this.txtId.Text = listItem.Code;
                lblClosedAccount.Text = listItem2.Code;
                lblCustomerName.Text = listItem2.CustomerName;
            }
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "btAuthorize":
                case "btRevert":
                    //string urlFTAccountClose = this.EditUrl("CloseAccount");
                    string urlFTAccountClose = "Default.aspx?tabid=" + this.TabId.ToString();
                    this.Response.Redirect(urlFTAccountClose);
                    break;               
            }     
        }

        protected void InitToolBar(bool flag)
        {
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar1.FindItemByValue("btRevert").Enabled = flag;            
        }
    }
}