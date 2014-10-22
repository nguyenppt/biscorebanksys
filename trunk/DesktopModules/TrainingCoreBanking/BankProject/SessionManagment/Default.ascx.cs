using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankProject.SessionManagment
{
    using BankProject.Entity.Administration;
    using BankProject.Repository;

    using DotNetNuke.Common;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Users;
    using Telerik.Web.UI;

    public partial class Default : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["command"] == "delete")
            {
                var accountPeriodRepository = new AccountPeriodRepository();
                var accountPeriodIdString = this.Request.QueryString["accountPeriodId"];
                int accountPeriodId;
                if (int.TryParse(accountPeriodIdString, out accountPeriodId))
                {
                    accountPeriodRepository.RemoveAccountPeriod(accountPeriodId); 
                    Response.Redirect(Globals.NavigateURL(this.TabId));
                }
            }
        }

        protected void radGridAccountPeriod_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGridAccountPeriod.DataSource = this.GetDataSource();
        }

        private IEnumerable<AccountPeriod> GetDataSource()
        {
            var acountPeriodRepository = new AccountPeriodRepository();
            return acountPeriodRepository.GetAll();
        }

        protected void radToolBar_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            string commandName = e.CommandName().ToLower();
            switch (commandName)
            {
                case "createnew":
                    this.GoToCreateNew();
                    break;
                case "manageshift":
                    this.ManageShift();
                    break;
                case "search":
                    this.SearchUsername();
                    break;
                case "viewhistory":
                    this.GoToViewHistory();
                    break;
            }
        }

        private void GoToViewHistory()
        {
            this.Response.Redirect(this.EditUrl("SM-SessionHistory"));
        }

        private void GoToCreateNew()
        {
            this.Response.Redirect(this.EditUrl("SM-EditAccountPeriod"));
        }

        private void SearchUsername()
        {
            var username = txtUsername.Text;
            if (string.IsNullOrEmpty(username))
            {
                radGridAccountPeriod.DataSource = this.GetDataSource();
            }
            else
            {
                var userInfo = UserController.GetUserByName(0, username);
                if (userInfo == null)
                {
                    radGridAccountPeriod.DataSource = Enumerable.Empty<AccountPeriod>();
                }
                else
                {
                    var acountPeriodRepository = new AccountPeriodRepository();
                    var accountPeriods = acountPeriodRepository.GetByUserId(userInfo.UserID);

                    radGridAccountPeriod.DataSource = accountPeriods;
                }
            }
            
            radGridAccountPeriod.DataBind();
        }

        private void ManageShift()
        {
            this.Response.Redirect(this.EditUrl("SM-ShiftManage"));
        }

        protected string GetEditAccountPeriodUrl(string accountPeriodId)
        {
            return this.EditUrl("SM-EditAccountPeriod") + "&accountPeriodId=" + accountPeriodId;
        }

        protected string GetDeleteAccountPeriodUrl(string accountPeriodId)
        {
            return Globals.NavigateURL(this.TabId) + "&command=delete&accountPeriodId=" + accountPeriodId;
        }

        protected string GetSessionHistoryUrl(string username)
        {
            return this.EditUrl("SM-SessionHistory") + "&username=" + username;
        }
    }
}