using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankProject.Controls
{
    public partial class DynamicControls : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Session["DataKey"] = DateTime.Now.ToString();
            
            
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            
        }


        protected void tbKhanh_Click(object sender, EventArgs e)
        {
            
        }
    }
}