namespace BankProject.Entity.Administration
{
    using System;
    using System.Collections.Generic;

    public class AccountPeriod
    {
        public AccountPeriod()
        {
            this.Shifts = new List<Shift>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        public DateTime BeginPeriod { get; set; }

        public DateTime EndPeriod { get; set; }

        public WorkingDay WorkingDay { get; set; }

        public int AvailableSlot { get; set; }

        public IList<Shift> Shifts { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsBlocked { get; set; }

        public string WorkingDayDisplay
        {
            get
            {
                return GetWorkingDayString(this.WorkingDay);
            }
        }

        public string ShiftDisplay
        {
            get
            {
                return GetShiftDisplay(this.Shifts);
            }
        }

        private static string GetShiftDisplay(IList<Shift> shifts)
        {
            var shiftStrings = new List<string>();

            foreach (var shift in shifts)
            {
                shiftStrings.Add(string.Format("{0}: {1} - {2}", shift.Title, shift.BeginShift, shift.EndShift));
            }

            return string.Join("<br/>", shiftStrings.ToArray());
        }

        private static string GetWorkingDayString(WorkingDay workingDay)
        {
            var values = Enum.GetValues(typeof(WorkingDay));
            var stringValues = new List<string>();
            foreach (WorkingDay value in values)
            {
                if (value == WorkingDay.None)
                {
                    continue;
                }

                if ((workingDay & value) == value)
                {
                    stringValues.Add(value.ToString());
                }
            }

            return string.Join(", ", stringValues.ToArray());
        }
    }
}