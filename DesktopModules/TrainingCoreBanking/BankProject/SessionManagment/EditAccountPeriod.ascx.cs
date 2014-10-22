using System;
using System.Collections.Generic;
using System.Linq;

namespace BankProject.SessionManagment
{
    using System.Globalization;

    using BankProject.Entity.Administration;
    using BankProject.Repository;

    using DotNetNuke.Common;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Users;
    using Telerik.Web.UI;

    public partial class EditAccountPeriod : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.cboUsername.DataSource = this.GetUserDataSource();
                this.cboUsername.DataBind();

                var shifts = this.GetShiftDataSource();
                this.cboShift1.DataSource = shifts;
                this.cboShift1.DataBind();
                this.cboShift2.DataSource = shifts;
                this.cboShift2.DataBind();
                this.cboShift3.DataSource = shifts;
                this.cboShift3.DataBind();

                var accountPeriodIdString = this.Request.QueryString["accountPeriodId"];
                if (!string.IsNullOrEmpty(accountPeriodIdString))
                {
                    var accountPeriodId = int.Parse(accountPeriodIdString);
                    this.LoadAccountPeriod(accountPeriodId);
                }
            }
        }

        private void LoadAccountPeriod(int accountPeriodId)
        {
            var accountPeriodRepository = new AccountPeriodRepository();
            var accountPeriod = accountPeriodRepository.GetAccountPeriod(accountPeriodId);
            if (accountPeriod != null)
            {
                this.BindAccountPeriod(accountPeriod);
            }
        }

        private void BindAccountPeriod(AccountPeriod accountPeriod)
        {
            hfAccountPeriodId.Value = accountPeriod.Id.ToString(CultureInfo.InvariantCulture);
            txtTitle.Text = accountPeriod.Title;
            cboUsername.SelectedValue = accountPeriod.UserId.ToString(CultureInfo.InvariantCulture);
            rdpkBeginPeriod.SelectedDate = accountPeriod.BeginPeriod;
            rdpkEndPeriod.SelectedDate = accountPeriod.EndPeriod;
            rnumAvailableSlot.Value = accountPeriod.AvailableSlot;
            chkIsEnabled.Checked = accountPeriod.IsEnabled;
            chkIsBlocked.Checked = accountPeriod.IsBlocked;

            this.BindWorkingDay(accountPeriod.WorkingDay);
            this.BindShifts(accountPeriod.Shifts);
        }

        private void BindShifts(IList<Shift> shifts)
        {
            for (int index = 0; index < shifts.Count && index < 3; index++)
            {
                var shift = shifts[index];

                if (index == 0)
                {
                    cboShift1.SelectedValue = shift.Id.ToString(CultureInfo.InvariantCulture);
                }

                else if (index == 1)
                {
                    cboShift2.SelectedValue = shift.Id.ToString(CultureInfo.InvariantCulture);
                }

                else if (index == 2)
                {
                    cboShift3.SelectedValue = shift.Id.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        private IEnumerable<UserInfo> GetUserDataSource()
        {
            return UserController.GetUsers(0).Cast<UserInfo>();
        }

        private IEnumerable<Shift> GetShiftDataSource()
        {
            var shiftRepository = new ShiftRepository();
            var shifts = shiftRepository.GetShifts().ToList();
            shifts.Insert(0, new Shift { Id = 0, Title = string.Empty });

            return shifts;
        }

        private AccountPeriod BindAccountPeriod()
        {
            var accountPeriod = new AccountPeriod();

            var accountPeriodIdString = hfAccountPeriodId.Value;
            if (!string.IsNullOrEmpty(accountPeriodIdString))
            {
                accountPeriod.Id = int.Parse(accountPeriodIdString);
            }

            accountPeriod.Title = txtTitle.Text;
            accountPeriod.UserId = int.Parse(cboUsername.SelectedValue);
            accountPeriod.BeginPeriod = rdpkBeginPeriod.SelectedDate.Value;
            accountPeriod.EndPeriod = rdpkEndPeriod.SelectedDate.Value;
            accountPeriod.AvailableSlot = (int)this.rnumAvailableSlot.Value;
            accountPeriod.IsBlocked = chkIsBlocked.Checked;
            accountPeriod.IsEnabled = chkIsEnabled.Checked;
            accountPeriod.WorkingDay = this.BindWorkingDay();

            var shifts = this.BindShifts();
            accountPeriod.Shifts.Clear();
            foreach (var shift in shifts)
            {
                accountPeriod.Shifts.Add(shift);
            }

            return accountPeriod;
        }

        private IEnumerable<Shift> BindShifts()
        {
            var shifts = new List<Shift>();
            var shiftIds = new List<int>();
            if (cboShift1.SelectedValue != null)
            {
                shiftIds.Add(int.Parse(cboShift1.SelectedValue));
            }

            if (cboShift2.SelectedValue != null)
            {
                shiftIds.Add(int.Parse(cboShift2.SelectedValue));
            }

            if (cboShift3.SelectedValue != null)
            {
                shiftIds.Add(int.Parse(cboShift3.SelectedValue));
            }

            var distShiftIds = shiftIds.Where(x => x > 0).Distinct().Distinct();
            foreach (var shiftId in distShiftIds)
            {
                shifts.Add(new Shift { Id = shiftId });
            }

            return shifts;
        }

        private WorkingDay BindWorkingDay()
        {
            var workingDay = WorkingDay.None;
            if (chkSunday.Checked)
            {
                workingDay = workingDay | WorkingDay.Sunday;
            }

            if (chkMonday.Checked)
            {
                workingDay = workingDay | WorkingDay.Monday;
            }

            if (chkTuesday.Checked)
            {
                workingDay = workingDay | WorkingDay.Tuesday;
            }

            if (chkWednesday.Checked)
            {
                workingDay = workingDay | WorkingDay.Wednesday;
            }

            if (chkThursday.Checked)
            {
                workingDay = workingDay | WorkingDay.Thursday;
            }

            if (chkFriday.Checked)
            {
                workingDay = workingDay | WorkingDay.Friday;
            }

            if (chkSaturday.Checked)
            {
                workingDay = workingDay | WorkingDay.Saturday;
            }

            return workingDay;
        }

        private void BindWorkingDay(WorkingDay workingDay)
        {
            chkSunday.Checked = workingDay.Is(WorkingDay.Sunday);
            chkMonday.Checked = workingDay.Is(WorkingDay.Monday);
            chkTuesday.Checked = workingDay.Is(WorkingDay.Tuesday);
            chkWednesday.Checked = workingDay.Is(WorkingDay.Wednesday);
            chkThursday.Checked = workingDay.Is(WorkingDay.Thursday);
            chkFriday.Checked = workingDay.Is(WorkingDay.Friday);
            chkSaturday.Checked = workingDay.Is(WorkingDay.Saturday);
        }

        protected void radToolBar_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var commandName = e.CommandName().ToLower();
            switch (commandName)
            {
                case "commit":
                    this.CommitData();
                    break;
                case "back":
                    this.Response.Redirect("Default.aspx?tabId=" + this.TabId);
                    break;
            }
        }

        private void CommitData()
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            var accountPeriod = this.BindAccountPeriod();
            var accountPeriodRepository = new AccountPeriodRepository();
            if (accountPeriod.Id == 0)
            {
                accountPeriodRepository.AddAccountPeriod(accountPeriod);
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                accountPeriodRepository.UpdateAccountPeriod(accountPeriod);
            }
        }
    }
}