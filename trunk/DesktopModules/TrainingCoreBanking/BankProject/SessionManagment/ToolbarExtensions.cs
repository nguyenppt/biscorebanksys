using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.SessionManagment
{
    using Telerik.Web.UI;

    public static class ToolbarExtensions
    {
        public static string CommandName(this RadToolBarEventArgs e)
        {
            var toolbarButton = e.Item as RadToolBarButton;
            if (toolbarButton == null)
            {
                return string.Empty;
            }

            return toolbarButton.CommandName;
        }
    }
}