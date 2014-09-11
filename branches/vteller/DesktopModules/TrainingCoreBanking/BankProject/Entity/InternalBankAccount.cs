using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity
{
    public class InternalBankAccount
    {
        public string Code { get; set; }
        public string Account { get; set; }
        public string Currency { get; set; }
        public string AccountTitle
        {
            get
            {
                return string.Format("{0} - {1}", Currency, Account);
            }
        }
       
    }
}