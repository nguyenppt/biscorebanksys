using BankProject.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class NewNomarlLoan_FullView : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();
            tbNewNormalLoan.Text = Request.Params["NewNormalLoan"];
            tbForwardBackWard.Focus();
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
            var ToolBarButton = e.Item as RadToolBarButton;
            var commandname = ToolBarButton.CommandName;
            if (commandname == "commit")
            {
                tbNewNormalLoan.Text = SQLData.B_BMACODE_GetNewID("CRED_REVOLVING_CONTRACT", refix_MACODE(), "/");
               
            }
            //fulltab.Attributes.Add("style", "display: ");
            string[] param = new string[4];
            param[0] = "NewNormalLoan=" + tbNewNormalLoan.Text;
            Response.Redirect(EditUrl("", "", "fullview", param));
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "fullview", new string[] { tbNewNormalLoan.Text}),true);
        }
        private string refix_MACODE()
        {
            return "LD";
        }
    }
}