namespace BankProject.Entity.Administration
{
    using System;

    public class SessionHistory
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        public int AccountPeriodId { get; set; }

        public int TotalUser { get; set; }

        public TimeSpan BeginShift { get; set; }

        public TimeSpan EndShift { get; set; }

        public int MaxSession { get; set; }

        public int ShiftId { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime CreatedDate
        {
            get
            {
                return this.CreatedTime.Date;
            }
        }

        public DateTime ModifiedTime { get; set; }
    }
}