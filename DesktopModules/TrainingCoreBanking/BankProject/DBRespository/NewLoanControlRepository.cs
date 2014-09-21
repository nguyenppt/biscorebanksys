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
     *      Concerate Repository for New normal Loan
     * Created By: 
     *      Nghia Le
     ******************************************************************************/


    /**
     * *****HISTORY****
     * Date                 By                  Description of change
     * **************************************************************************
     * 10-Sep-2014          Nghia               Init code
     *
     * 
     * 
     * 
     * ****************************************************************************
     */
    public class NewLoanControlRepository:BaseRepository<BNewLoanControl>
    {
        public IQueryable<BNewLoanControl> FindLoanControlByCode(string code)
        {
            Expression<Func<BNewLoanControl, bool>> query = t => t.Code == code;
            return Find(query);
        }
    }
}