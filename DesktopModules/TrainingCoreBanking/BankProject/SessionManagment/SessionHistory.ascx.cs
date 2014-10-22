using System;

namespace BankProject.SessionManagment
{
    using System.Collections.Generic;
    using System.Linq;

    using BankProject.Repository;

    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.UI.UserControls;

    using Telerik.Web.UI;

    using EntitySessionHistory = Entity.SessionHistory;

    public partial class SessionHistory : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                var users = UserController.GetUsers(0);
                users.Insert(0, new UserInfo() { Username = string.Empty });
                this.cboUsername.DataSource = users;
                this.cboUsername.DataBind();
            }
        }

        protected void radGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.Search(false);
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

        private void Search(bool dataBind = true)
        {
            var username = this.cboUsername.Text;
            int? userId = null;
            var fromDate = this.rdpkFromDate.SelectedDate;
            var toDate = this.rdpkToDate.SelectedDate;

            if (fromDate == null && toDate == null)
            {
                fromDate = DateTime.Today;
                toDate = DateTime.Today;

                this.rdpkFromDate.SelectedDate = fromDate;
                this.rdpkToDate.SelectedDate = toDate;
            }

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
            if (dataBind)
            {
                radGrid.DataBind();  
            }
        }
    }
}