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
     *      Concerate Repository for BProducategory
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
    public class ProductCategoryRepository: BaseRepository<BRPODCATEGORY>
    {
        public IQueryable<BRPODCATEGORY> getProductCategory(string catid)
        {
            Expression<Func<BRPODCATEGORY, bool>> query = p => p.CatId == catid;
            return Find(query);
        }
    }
}