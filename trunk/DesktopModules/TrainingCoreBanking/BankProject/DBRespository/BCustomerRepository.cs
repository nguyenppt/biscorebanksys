using BankProject.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BankProject.DBRespository
{
    /******************************************************************************
     * Description:
     *      Concerate Repository for Accout Customer Infor
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
    public class BCustomerRepository:BaseRepository<BCUSTOMER_INFO>
    {
        public IQueryable<BCUSTOMER_INFO> getCustomerList(String _status)
        {
            Expression<Func<BCUSTOMER_INFO, bool>> query = c => c.Status.Equals(_status);
            return Find(query);
        }
    }
}