using System;
using System.Collections.Generic;
using System.Linq;
using BankProject.Entity;

namespace BankProject.Repository
{
    public class AccountOfficerRepository : IRepository<AccountOfficer>
    {
        public IQueryable<AccountOfficer> GetAll()
        {
            return this.GetCities().AsQueryable();
        }

        private IList<AccountOfficer> GetCities()
        {
            IList<AccountOfficer> cities = new List<AccountOfficer>();
            cities.Add(new AccountOfficer { Code = "", Name = "" }); 
            cities.Add(new AccountOfficer { Code = "695", Name = "Tran Nhut Tan" });            
            return cities;
        }

        public Industry GetById()
        {
            throw new NotImplementedException();
        }
    }
}