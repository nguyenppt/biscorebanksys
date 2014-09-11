using DotNetNuke.UI.Skins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankProject
{
    public partial class TopMenu : SkinObjectBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RadMenu1.DataFieldID = "MenuID";
                RadMenu1.DataTextField = "MenuName";
                RadMenu1.DataFieldParentID = "ParentID";
             
                RadMenu1.DataSource = DataProvider.DataTam.B_BMENUTOP_GetAll();
                RadMenu1.DataBind();
            }

            //RadMenu1.Flow = (Telerik.Web.UI.ItemFlow)Enum.Parse(typeof(Telerik.Web.UI.ItemFlow), "Horizontal", true);
        }
    }
}