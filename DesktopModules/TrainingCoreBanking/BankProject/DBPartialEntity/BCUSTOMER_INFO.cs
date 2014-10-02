using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.DBContext
{
    public partial class BCUSTOMER_INFO
    {

        public string ID_FullName
        {
            get { 
                return CustomerID + " - " + GBFullName; 
            }
            
        }
    }
}