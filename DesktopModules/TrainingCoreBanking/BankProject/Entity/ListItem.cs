using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity
{
    public class ListItem
    {
        public ListItem()
        {
            this.Status = 1;
        }

        public int Id { get; set; }

        public string Code { get; set; }

        public string CustomerName { get; set; }

        public string OpenActual { get; set; }

        public string OnlineActual { get; set; }

        public string OpenTotalCredit { get; set; }

        public string OnlineTotalCredit { get; set; }

        public string Currency { get; set; }

        public string DebitAccount { get; set; }

        public int Status { get; set; }
    }
}