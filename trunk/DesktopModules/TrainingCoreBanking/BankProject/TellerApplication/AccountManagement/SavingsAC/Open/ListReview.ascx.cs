using System;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Open
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
            string from = Request.QueryString["From"];
            switch (from)
            {
                case "Arrear":
                default:
                    grdSavingAccReviewList.DataSource = SavingAccountDAO.GetArrearSavingAccountByStatus(Entity.AuthoriseStatus.UNA);
                    break;
                case "Periodic":
                    grdSavingAccReviewList.DataSource = SavingAccountDAO.GetPeriodicSavingAccountByStatus(Entity.AuthoriseStatus.UNA);
                    break;
                
            }
        }

        #endregion

        #region Helper method

        public string GeturlReview(string refId)
        {
            return string.Format("Default.aspx?tabid={0}&mid={1}&RefId={2}&disable=true&mode=preview", TabId, ModuleId, refId);
        }

        #endregion        
    }
}