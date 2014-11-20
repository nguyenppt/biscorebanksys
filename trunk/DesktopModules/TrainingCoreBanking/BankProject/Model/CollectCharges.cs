﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Model.CollectCharges.Reports
{
    public class VAT : BankProject.DBContext.B_CollectCharges
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string IdentityNo { get; set; }
        public string UserName { get; set; }
        public string ChargeRemarks { get; set; }
        //
        public string TotalChargeAmountWord { get; set; }
    }
}