using BankProject.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BankProject.DBRespository
{
    public class BCustomerRepository:BaseRepository<BCUSTOMER_INFO>
    {
        public IQueryable<BCUSTOMER_INFO> getCustomerList(String _status)
        {
            Expression<Func<BCUSTOMER_INFO, bool>> query = c => c.Status.Equals(_status);
            return Find(query);
        }
    }
}