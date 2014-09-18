using BankProject.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BankProject.DBRespository
{
    public class NormalLoanRepository:BaseRepository<BNEWNORMALLOAN>
    {
        public IQueryable findCustomerCode(String code)
        {
            Expression<Func<BNEWNORMALLOAN, bool>> loan = t => t.Code.Equals(code);

            return Find(loan);
        }

        public IQueryable findCustomerCodeAUT(String code)
        {
            Expression<Func<BNEWNORMALLOAN, bool>> loan = t => t.Code.Equals(code) && t.Status=="AUT";

            return Find(loan);
        }
    }
}