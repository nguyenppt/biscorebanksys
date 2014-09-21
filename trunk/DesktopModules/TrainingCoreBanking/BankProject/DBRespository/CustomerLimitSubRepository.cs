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
     *      Concerate Repository for Customer Limit Sub Remository
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
    public class CustomerLimitSubRepository:BaseRepository<BCUSTOMER_LIMIT_SUB>
    {
        public IQueryable<BCUSTOMER_LIMIT_SUB> FindLimitCusSub(string custID)
        {
            Expression<Func<BCUSTOMER_LIMIT_SUB, bool>> query = t => t.CustomerID == custID;
            return Find(query);
        }
    }
}