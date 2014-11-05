using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace BankProject.SessionManagment
{
    using System.Globalization;

    using BankProject.Entity.Administration;
    using BankProject.Repository;

    using DotNetNuke.Common;
    using DotNetNuke.Entities.Modules;

    using Telerik.Web.UI;

    public partial class ShiftManage : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["command"] == "delete")
            {
                var shiftRepository = new ShiftRepository();
                var shiftId = this.Request.QueryString["shiftId"];
                int shiftIdInt;
                if (int.TryParse(shiftId, out shiftIdInt))
                {
                    shiftRepository.RemoveShift(shiftIdInt);
                }

                this.GoToShiftMainPage();
            }

            if (!this.IsPostBack)
            {
                var shiftIdString = this.Request.QueryString["shiftId"];
                if (string.IsNullOrEmpty(shiftIdString))
                {
                    // New shift
                }
                else
                {
                    // Edit shift
                    int shiftId;
                    if (int.TryParse(shiftIdString, out shiftId))
                    {
                        this.BindShift(shiftId);
                    }
                    else
                    {
                        this.GoToShiftMainPage();
                    }
                }
            }
            
            this.RefreshGrid();
        }

        private void RefreshGrid()
        {
            this.rgrdMain.DataSource = this.GetDataSource();
            this.rgrdMain.DataBind();
        }

        protected void radToolBar_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var command = e.CommandName();
            switch (command.ToLower())
            {
                case "back":
                    this.Response.Redirect("Default.aspx?tabId=" + this.TabId);
                    break;
                case "commit":
                    this.CommitShift();
                    GoToShiftMainPage();
                    break;
            }
        }

        protected void rgrdMain_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.rgrdMain.DataSource = this.GetDataSource();
        }

        private IEnumerable<Shift> GetDataSource()
        {
            var shiftRepository = new ShiftRepository();
            return shiftRepository.GetShifts();
        }

        protected string GetEditShiftUrl(string shiftId)
        {
            return "Default.aspx?tabId=" + TabId + "&ShiftId=" + shiftId;
        }

        protected string GetDeleteShiftUrl(string shiftId)
        {
            return this.EditUrl(this.TabId, string.Empty, true, "command=delete", "shiftId=" + shiftId);
        }

        private void BindShift(int shiftId)
        {
            var shiftRepository = new ShiftRepository();
            var shift = shiftRepository.GetShift(shiftId);
            if (shift == null)
            {
                this.GoToShiftMainPage();
                return;
            }

            var date = new DateTime(2000, 1, 1);
            hfShiftId.Value = shift.Id.ToString(CultureInfo.InvariantCulture);
            txtTitle.Text = shift.Title;
            tprBeginShift.SelectedDate = date + shift.BeginShift;
            tprEndShift.SelectedDate = date + shift.EndShift;
        }

        private Shift BindShift()
        {
            var shift = new Shift();

            shift.Id = string.IsNullOrEmpty(hfShiftId.Value) ? 0 : int.Parse(hfShiftId.Value);
            shift.Title = txtTitle.Text;
            shift.BeginShift = tprBeginShift.SelectedDate.Value.TimeOfDay;
            shift.EndShift = tprEndShift.SelectedDate.Value.TimeOfDay;

            return shift;
        }

        private void GoToShiftMainPage()
        {
            this.Response.Redirect("Default.aspx?tabId=" + this.TabId);
        }

        private void CommitShift()
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            var shift = this.BindShift();

            if (shift.Id == 0)
            {
                this.AddShift(shift);
            }
            else
            {
                this.UpdateShift(shift);
            }

            //this.RefreshGrid();
        }

        private void UpdateShift(Shift shift)
        {
            var shiftRepository = new ShiftRepository();
            shiftRepository.UpdateShift(shift);
        }

        private void AddShift(Shift shift)
        {
            var shiftRepository = new ShiftRepository();
            shiftRepository.AddShift(shift);

            Globals.NavigateURL(this.TabId);
        }

        protected void tprBeginShift_OnValidate(object source, ServerValidateEventArgs args)
        {
            if (this.tprBeginShift.SelectedDate != null && this.tprEndShift.SelectedDate != null)
            {
                if (this.tprBeginShift.SelectedDate.Value.TimeOfDay >= this.tprEndShift.SelectedDate.Value.TimeOfDay)
                {
                    args.IsValid = false;
                }
            }
        }
    }
}