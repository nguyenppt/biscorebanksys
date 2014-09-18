using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    public class CollorateRightRepository:BaseRepository<BCOLLATERALRIGHT>
    {
        public IQueryable<BCOLLATERALRIGHT> FindCollorateRightByCust(string custID)
        {
            Expression<Func<BCOLLATERALRIGHT, bool>> query = t => t.CustomerID == custID;
            return Find(query);
        }
    }
}