using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    public class CollateralContingentEntryRespository:BaseRepository<BCOLLATERALCONTINGENT_ENTRY>
    {
        public IQueryable<BCOLLATERALCONTINGENT_ENTRY> GetCollateralContingentInfo(String collateralContID)
        {
            Expression<Func<BCOLLATERALCONTINGENT_ENTRY, bool>> query = t => t.CollateralInfoID.Equals(collateralContID);

            return Find(query);
        }
    }
}