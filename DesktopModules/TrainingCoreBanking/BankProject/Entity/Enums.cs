using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity
{
    public enum AuthoriseStatus
    {
        UNA = 0,
        AUT = 1, //should disable all control, switch to readonly mode
        REV = 2,        
    }

    public enum SavingAccFunc
    {
        ARREAR = 0,
        PERIODIC = 1,
        DISCOUNTED = 2
    }
}