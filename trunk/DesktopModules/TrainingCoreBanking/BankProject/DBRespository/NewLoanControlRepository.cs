using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    public class NewLoanControlRepository:BaseRepository<BNewLoanControl>
    {
        public IQueryable<BNewLoanControl> FindLoanControlByCode(string code)
        {
            Expression<Func<BNewLoanControl, bool>> query = t => t.Code == code;
            return Find(query);
        }
    }
}