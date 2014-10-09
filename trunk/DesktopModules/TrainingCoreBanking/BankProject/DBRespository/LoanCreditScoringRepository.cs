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
    public class LoanCreditScoringRepository:BaseRepository<B_LOAN_CREDIT_SCORING> 
    {
        public IQueryable<B_LOAN_CREDIT_SCORING> GetRatingScoring(int score)
        {
            Expression<Func<B_LOAN_CREDIT_SCORING, bool>> query = t => score >= t.ScoreFrom;

            return Find(query);
        }
    }
}