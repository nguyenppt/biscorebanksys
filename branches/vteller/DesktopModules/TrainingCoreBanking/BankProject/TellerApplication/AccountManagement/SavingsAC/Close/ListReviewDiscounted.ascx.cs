using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close
{
    public partial class ListReviewDiscounted : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        #region Property

        private SavingAccountDAO SavingAccountDAO
        {
            get
            {
                return new SavingAccountDAO();
            }
        }

        #endregion

        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
           
        }
       
        protected void grdSavingAccReviewList_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdSavingAccReviewList.DataSource = SavingAccountDAO.GetDiscountedAccountByCloseStatus();
        }

        #endregion

        #region Helper method

        public string GeturlReview(string refId)
        {
            return string.Format("Default.aspx?tabid={0}&ctl={1}&mid={2}&RefId={3}&disable=true&mode=preview", TabId,"chitiet", ModuleId, refId);
        }

        #endregion        
    }
}