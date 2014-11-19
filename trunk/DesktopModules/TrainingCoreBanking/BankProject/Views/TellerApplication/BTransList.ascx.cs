using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BankProject.DataProvider;
namespace BankProject.Views.TellerApplication
{
    public partial class BTransList : DotNetNuke.Entities.Modules.PortalModuleBase
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
                    RadGridView.DataSource = TriTT.INWARD_TRANS_LIST(tbLegalID.Text);
                    RadGridView.DataBind();
                }
            }
        }
        //public string geturlReview(string ID, string type)
        //{
        //    //switch (type)
        //    //{
        //    //    case "By Cash":
        //    //        return string.Format("Default.aspx?tabid=171&RefID={0}", REFID_4Proc);
        //    //        break;
        //    //    default :
        //    //        return string.Format("Default.aspx?tabid=172&RefID={0}", REFID_4Proc);
        //    //        break;
        //    //}
        //}
        protected void RadGrid1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                RadGridView.DataSource = TriTT.INWARD_TRANS_LIST(tbLegalID.Text);
            }
            else
            {
                RadGridView.DataSource = TriTT.INWARD_TRANS_LIST("");
            }
        }
    }
}