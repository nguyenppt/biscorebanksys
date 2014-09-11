using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BankProject.Entity.TriTT_Saving;
using BankProject.DataProvider;

namespace BankProject.Views.CustomerManagement.OpenInvidualCustomer
{
    public partial class Index_ListReview : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        #region Property
        private SavingAccount_SQL SavingAccount_SQL
        {
            get 
            {
                return new SavingAccount_SQL();
            }
        }
        #endregion
        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }
        protected void RadGridAccountReviewList_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string From = Request.QueryString["From"];
            switch (From)
            { 
                case "OpenIndividualCustomer":
                    if (IsPostBack)  RadGridAccountReviewList.DataSource = SavingAccount_SQL.GetIndividualCustomerBySatus(Entity.AuthoriseStatus.UNA);
                else
                    RadGridAccountReviewList.DataSource = SavingAccount_SQL.GetIndividualCustomerBySatus(Entity.AuthoriseStatus.UNA);
                    break;
                case "OpenCorporateCustomer":
                    if (IsPostBack) RadGridAccountReviewList.DataSource = TriTT.OPEN_CORPORATE_CUSTOMER_review_Account("", "UNA", "C", "1");
                    else RadGridAccountReviewList.DataSource = TriTT.OPEN_CORPORATE_CUSTOMER_review_Account("","UNA","C","1");
                     break;
            }
        }
        #endregion

        #region Helper Method
        public string GeturlReview(string CustomerID)
        {
            switch (CustomerID.StartsWith("1"))
            {
                case true:
                    return string.Format("Default.aspx?tabid={0}&mid={1}&CustomerID={2}&disable=true&mode=preview", TabId, ModuleId, CustomerID);
                    break;/// redirect ve trang web OPEN INDIVIDUAL CUSTOMER
                default:
                    return string.Format("Default.aspx?tabid={0}&mid={1}&CustomerID={2}&disable=true&mode=audit", 256, ModuleId, CustomerID); // mode= audit cho phu hop voi code  OPEN CORPORATE CUSTOMER
                    break; /// redirect ve trang web OPEN CORPORATE CUSTOMER
            }
        }
        #endregion
    }
}