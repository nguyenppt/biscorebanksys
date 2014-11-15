using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    /******************************************************************************
     * Description:
     *      Concerate Repository for Loan Interesting payment
     * Created By: 
     *      Nghia Le
     ******************************************************************************/


    /**
     * *****HISTORY****
     * Date                 By                  Description of change
     * **************************************************************************
     * 29-Sep-2014          Nghia               Init code
     *
     * 
     * 
     * 
     * ****************************************************************************
     */
    public class NormalLoanPaymentSchedule:BaseRepository<B_NORMALLOAN_PAYMENT_SCHEDULE>
    {
        // Summary:
        // Get All perios payment plan of a loan contract     
        public IQueryable<B_NORMALLOAN_PAYMENT_SCHEDULE> GetScheduleDetail(string code)
        {
            Expression<Func<B_NORMALLOAN_PAYMENT_SCHEDULE, bool>> query = t => t.Code.Equals(code);

            return Find(query);
        }

        public void DeleteAllScheduleOfContract(String code)
        {
            Expression<Func<B_NORMALLOAN_PAYMENT_SCHEDULE, bool>> query = t => t.Code.Equals(code);
            var ites = Find(query);

            foreach (var it in ites)
            {
                Delete(it);
            }
            Commit();
        }

        public void DeleteAllScheduleOfContract(String code, int replaymentTimes)
        {
            Expression<Func<B_NORMALLOAN_PAYMENT_SCHEDULE, bool>> query = t => t.Code.Equals(code) && t.PeriodRepaid == replaymentTimes;
            var ites = Find(query);

            foreach (var it in ites)
            {
                Delete(it);
            }
            Commit();
        }
    }
}