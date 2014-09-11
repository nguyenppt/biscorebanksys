using System;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close
{
    public partial class ListReview : DotNetNuke.Entities.Modules.PortalModuleBase
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
            grdSavingAccReviewList.DataSource = SavingAccountDAO.GetSavingAccountCloseStatus();
        }

        #endregion

        #region Helper method

        public string GeturlReview(string refId,string from)
        {
            return string.Format("Default.aspx?tabid={0}&ctl=input&mid={1}&RefId={2}&disable=true&mode=preview&from={3}", TabId, ModuleId, refId, from);
        }

        #endregion        
    }
}