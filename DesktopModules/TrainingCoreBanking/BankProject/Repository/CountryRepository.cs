using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Repository
{
    public class CountryRepository : Repository<BCOUNTRY>
    {
        public override IQueryable<BCOUNTRY> GetAll()
        {
            IList<BCOUNTRY> countries = base.GetAll().ToList();
            countries.Insert(0, new BCOUNTRY { MaQuocGia = string.Empty, TenTV = string.Empty, TenTA = string.Empty });
            return countries.AsQueryable();
        }
    }
}