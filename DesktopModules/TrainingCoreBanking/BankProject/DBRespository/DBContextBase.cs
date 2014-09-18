using BankProject.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.DBRespository
{
    public class DBContextBase : VietVictoryCoreBankingEntities
    {
        private static DBContextBase context;

        private DBContextBase()
        {
        }

        public static DBContextBase getInstance()
        {
            if (context == null)
                context = new DBContextBase();

            return context;
        }
    }
}