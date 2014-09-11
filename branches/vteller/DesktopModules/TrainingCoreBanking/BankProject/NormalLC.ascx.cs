using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using Telerik.Web.UI.Calendar;
using System.IO;
using Aspose.Words;

namespace BankProject
{
    public partial class NormalLC : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //this.NormalLCEdit1.PortalModule = this;
        }
    }
}