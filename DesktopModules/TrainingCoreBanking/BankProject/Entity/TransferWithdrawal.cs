using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity
{
    public class TransferWithdrawal
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public int Currency { get; set; }

        public int MyProperty { get; set; }
    }
}