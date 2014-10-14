namespace BankProject.Entity.Administration
{
    using System;

    public class Shift
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public TimeSpan BeginShift { get; set; }

        public TimeSpan EndShift { get; set; }
    }
}