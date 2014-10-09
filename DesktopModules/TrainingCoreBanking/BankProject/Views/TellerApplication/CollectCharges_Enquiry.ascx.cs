using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BankProject.DataProvider;
using System.Data;
namespace BankProject.Views.TellerApplication
{
    public partial class CollectCharges_Enquiry :DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            RadToolBar2.FindItemByValue("btCommit").Enabled = false;
            RadToolBar2.FindItemByValue("btPreview").Enabled = false;
            RadToolBar2.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar2.FindItemByValue("btReverse").Enabled = false;
            RadToolBar2.FindItemByValue("btSearch").Enabled = true;
            RadToolBar2.FindItemByValue("btPrint").Enabled = false;
        }
        protected void radtoolbar2_onbuttonclick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            string CommandName = ToolBarButton.CommandName;
            if (CommandName == "search")
            {
                if (IsPostBack)
                {
                    RadGridView.DataSource = Database.BCOLLECTCHARGESFROMACCOUNT_Enquiry(rcbCollectionType.SelectedValue, tbID.Text, rcbAccountType.SelectedValue
                        ,tbAccountID.Text,tbCustomerID.Text, tbCustomerName.Text,tbLegalID.Text, tbFromAmt.Value.HasValue ? tbFromAmt.Value.Value : 0,
                        tbToAmt.Value.HasValue ? tbToAmt.Value.Value : 0);
                    RadGridView.DataBind();
                }
            }
        }
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                RadGridView.DataSource = Database.BCOLLECTCHARGESFROMACCOUNT_Enquiry(rcbCollectionType.SelectedValue, tbID.Text, rcbAccountType.SelectedValue
                        , tbAccountID.Text, tbCustomerID.Text, tbCustomerName.Text, tbLegalID.Text, tbFromAmt.Value.HasValue ? tbFromAmt.Value.Value : 0,
                        tbToAmt.Value.HasValue ? tbToAmt.Value.Value : 0);
            }
        }
         protected string getUrlPreview(string Code, string form)
         {
             if (form == "Account")
                 return string.Format("Default.aspx?tabid=140&codeid={0}", Code);
             else 
                 return string.Format("Default.aspx?tabid=141&codeid={0}", Code);
         }
         protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
         {
             string radalertscript =
                 "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                 "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
             Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
         }
    }
    
}