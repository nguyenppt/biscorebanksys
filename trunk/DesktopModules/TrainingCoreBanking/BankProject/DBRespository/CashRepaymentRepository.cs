using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    public class CashRepaymentRepository:BaseRepository<BCASH_REPAYMENT>
    {
        public IQueryable<BCASH_REPAYMENT> FindActiveCashRepayment(string custAccID)
        {
            Expression<Func<BCASH_REPAYMENT, bool>> query = t => t.CustomerAccountID.Equals(custAccID) 
                && t.ActiveFlag.Equals("1") && t.RepaidLoanFlag == 0 && t.Status.Equals("AUT");
            return Find(query);
        }
    }
}