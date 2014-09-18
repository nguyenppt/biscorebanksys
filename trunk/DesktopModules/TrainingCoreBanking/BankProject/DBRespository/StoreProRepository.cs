using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BankProject.DBRespository
{
    public class StoreProRepository
    {
        public DBContextBase StoreProcessor()
        {
            return DBContextBase.getInstance();
        }

    }
}