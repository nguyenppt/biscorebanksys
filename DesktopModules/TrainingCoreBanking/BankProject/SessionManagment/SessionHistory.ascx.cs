using System;

namespace BankProject.SessionManagment
{
    using System.Collections.Generic;
    using System.Linq;

    using BankProject.Repository;

    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Users;

    using Telerik.Web.UI;

    using EntitySessionHistory = Entity.SessionHistory;

    public partial class SessionHistory : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.cboUsername.DataSource = UserController.GetUsers(0);
                this.cboUsername.DataBind();
            }
        }

        protected void radGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            radGrid.DataSource = this.GetDataSource();
        }

        private IEnumerable<Entity.Administration.SessionHistory> GetDataSource()
        {
            var sessionHistoryRepository = new SessionHistoryRepository();
            return sessionHistoryRepository.GetSessionHistories().OrderByDescending(x => x.CreatedTime);
        }

        protected void radToolBar_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var commandName = e.CommandName().ToLower();
            switch (commandName)
            {
                case "search":
                    this.Search();
                    break;
                case "purge":
                    this.Purge();
                    break;
            }
        }

        private void Purge()
        {
            var username = this.cboUsername.Text;
            int? userId = null;
            var fromDate = this.rdpkFromDate.SelectedDate;
            var toDate = this.rdpkToDate.SelectedDate;

            if (!string.IsNullOrEmpty(username))
            {
                var userInfo = UserController.GetUserByName(0, username);
                if (userInfo != null)
                {
                    userId = userInfo.UserID;
                }
                else
                {
                    userId = -1;
                }
            }
            
            var sessionHistoryRepository = new SessionHistoryRepository();
            sessionHistoryRepository.Purge(userId, fromDate, toDate);
            this.Search();
        }

        private void Search()
        {
            var username = this.cboUsername.Text;
            int? userId = null;
            var fromDate = this.rdpkFromDate.SelectedDate;
            var toDate = this.rdpkToDate.SelectedDate;
            var sessionHistoryRepository = new SessionHistoryRepository();

            if (!string.IsNullOrEmpty(username))
            {
                var userInfo = UserController.GetUserByName(0, username);
                if (userInfo != null)
                {
                    userId = userInfo.UserID;
                }
                else
                {
                    userId = -1;
                }
            }

            radGrid.DataSource = sessionHistoryRepository.GetSessionHistories(userId, fromDate, toDate);
            radGrid.DataBind();
        }
    }
}