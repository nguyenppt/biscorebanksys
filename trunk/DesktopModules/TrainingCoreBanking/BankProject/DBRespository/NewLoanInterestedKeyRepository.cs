using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    public class NewLoanInterestedKeyRepository: BaseRepository<BLOANINTEREST_KEY>
    {
        public IQueryable<BLOANINTEREST_KEY> GetInterestKey(int durationMonth)
        {
            Expression<Func<BLOANINTEREST_KEY, bool>> query = t => t.MonthLoanRateNo == durationMonth;

            return Find(query);
        }

    }
}