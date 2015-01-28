using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    /**
     * *****HISTORY****
     * Date                 By                  Description of change
     * **************************************************************************
     * 10-Jan-2015          Nghia               Init code
     *
     * 
     * 
     * 
     * ****************************************************************************
     */
    public class SwiftCodeRepository:BaseRepository<BSWIFTCODE>
    {
        public IQueryable<BSWIFTCODE> FindSwiftCodeAssociateWithCurrency(String currency)
        {
            Expression<Func<BSWIFTCODE, bool>> query = t => t.Currency.Equals(currency);
            return Find(query);
        }
    }
}