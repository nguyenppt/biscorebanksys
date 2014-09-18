using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    public class ProductCategoryRepository: BaseRepository<BRPODCATEGORY>
    {
        public IQueryable<BRPODCATEGORY> getProductCategory(string catid)
        {
            Expression<Func<BRPODCATEGORY, bool>> query = p => p.CatId == catid;
            return Find(query);
        }
    }
}