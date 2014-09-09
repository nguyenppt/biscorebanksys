using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity
{
    public class AccountOpen
    {
        public string ID { get; set; }
        public string AccountCode { get; set; }
        public string CustomerID { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string ProductLineID { get; set; }
        public string Title { 
            get
            {
                return string.Format("{0} - {1} - {2}",Currency,AccountCode,CustomerName);
            }
        }
        public string Currency { get; set; }
        public string AccountOfficerID { get; set; }
        public string JoinHolderID { get; set; }        
        public decimal? ActualBallance { get; set; }     
        public decimal? ClearedBallance { get; set; }     
        
    }
}
