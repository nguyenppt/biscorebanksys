using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity
{
    public class BlockedAccount
    {
        public int Id { get; set; }

        public string Amount { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int Status { get; set; }
    }
}