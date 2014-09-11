using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity.SavingAcc
{
    public class SalaryPaymentFrequencyDetail
    {   
        
        public string WorkingAccId { get; set; }
        public string PaymentFrequencyId { get; set; }
        public string PaymentMethod { get; set; }        
        public decimal? CreditAmount  { get; set; }
        public string CreditAccount { get; set; }

    }
}