using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    public class NormalLoanRepaymentRepository:BaseRepository<BNEWNORMALLOAN_REPAYMENT>
    {
        public IQueryable<BNEWNORMALLOAN_REPAYMENT> FindRepaymentAmount(String code, int replaymentTimes)
        {
            Expression<Func<BNEWNORMALLOAN_REPAYMENT, bool>> loan = t => t.Code.Equals(code) && t.RepaymentTimes == replaymentTimes;
            return Find(loan);
        }
    }
}