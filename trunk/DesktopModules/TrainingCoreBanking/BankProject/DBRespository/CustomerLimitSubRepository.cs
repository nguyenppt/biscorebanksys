using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    public class CustomerLimitSubRepository:BaseRepository<BCUSTOMER_LIMIT_SUB>
    {
        public IQueryable<BCUSTOMER_LIMIT_SUB> FindLimitCusSub(string custID)
        {
            Expression<Func<BCUSTOMER_LIMIT_SUB, bool>> query = t => t.CustomerID == custID;
            return Find(query);
        }
    }
}