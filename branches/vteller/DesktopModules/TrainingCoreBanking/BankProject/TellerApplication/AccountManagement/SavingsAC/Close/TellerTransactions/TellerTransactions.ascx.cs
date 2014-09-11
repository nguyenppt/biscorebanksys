using System;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.AccountManagement.SavingsAC.Close.TellerTransactions
{
    public partial class TellerTransactions : DotNetNuke.Entities.Modules.PortalModuleBase
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
            grdSavingAccReviewList.DataSource = SavingAccountDAO.GetSavingAccountHold();
        }

        #endregion

        #region Helper method

        public string GeturlReview(string refId, string from)
        {
            return string.Format("Default.aspx?tabid={0}&ctl=view&mid={1}&RefId={2}&disable=true&mode=preview&from={3}", TabId, ModuleId, refId, from);
        }

        #endregion        

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (IsPostBack) return;
        //}

        //protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    radGridReview.DataSource = ThanhLP.B_BPRECLOSURE_TELLER_TRANS();
        //}

        //public string geturlReview(string id, string typeCode)

        //{
        //    string[] param = new string[2];
        //    param[0] = "CodeID=" + id;
        //    param[1] = "TypeCode=" + typeCode;
        //    //param[2] = "AmountLcy=" + AmountLcy;
        //    return EditUrl("", "", "VIEW", param);
        //}
    }
}