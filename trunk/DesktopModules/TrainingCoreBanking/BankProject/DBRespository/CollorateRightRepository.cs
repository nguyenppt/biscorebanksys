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
     *      Concerate Repository for Collaterat Right
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
    public class CollorateRightRepository:BaseRepository<BCOLLATERALRIGHT>
    {
        public IQueryable<BCOLLATERALRIGHT> FindCollorateRightByCust(string custID)
        {
            Expression<Func<BCOLLATERALRIGHT, bool>> query = t => t.CustomerID == custID;
            return Find(query);
        }
    }
}