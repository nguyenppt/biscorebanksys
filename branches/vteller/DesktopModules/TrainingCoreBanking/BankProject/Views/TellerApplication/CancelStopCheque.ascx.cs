using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class CancelStopCheque : PortalModuleBase
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtId.Text = "03.000237890.2";
            }
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {

        }
    }
}