using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity.SavingAcc
{
    public class PeriodicSavingAccount:SavingAccount
    {
        public string AZIsSchedule { get; set; }
        public string AZScheduleType { get; set; }
        public string AZFrequency { get; set; }
    }
}