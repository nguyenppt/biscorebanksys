using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity.SavingAcc
{
    public class SalaryPaymentFrequency
    {
        public string RefId { get; set; }
        public string Status { get; set; }
        public string OpenAccounId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Currency { get; set; }
        public decimal? TotalDebitAmt { get; set; }
        public DateTime? Fequency { get; set; }
        public string Term { get; set; }
        public DateTime? EndDate { get; set; }
        public string OrderingCust { get; set; }
        public string NonFrequency { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}