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
     *      Concerate Repository for loan disbursal schedular
     * Created By: 
     *      Nghia Le
     ******************************************************************************/


    /**
     * *****HISTORY****
     * Date                 By                  Description of change
     * **************************************************************************
     * 16-Sep-2014          Nghia               Init code
     *
     * 
     * 
     * 
     * ****************************************************************************
     */
    public class LoanDisbursalScheduleRespository:BaseRepository<B_LOAN_DISBURSAL_SCHEDULE>
    {
        public IQueryable<B_LOAN_DISBURSAL_SCHEDULE> FindLoanDisbursalByCode(string code)
        {
            Expression<Func<B_LOAN_DISBURSAL_SCHEDULE, bool>> query = t => t.Code == code;
            return Find(query);
        }

        
    }
}