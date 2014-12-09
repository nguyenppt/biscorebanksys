using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    public class ProduceLineRepository:BaseRepository<BPRODUCTLINE>
    {
        public IQueryable<BPRODUCTLINE> LoadProductLineList(string type)
        {
            Expression<Func<BPRODUCTLINE, bool>> query = t => t.Type.Equals(type);
            return Find(query);
        }
    }
}