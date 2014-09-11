using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class ChequeIssueCollectCharges : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtId.Text = "TT/09161/08856";
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        { }
    }
}