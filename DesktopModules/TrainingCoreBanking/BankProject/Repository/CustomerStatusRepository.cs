using System;
using System.Collections.Generic;
using System.Linq;
using BankProject.Entity;

namespace BankProject.Repository
{
    public class CustomerStatusRepository : IRepository<CustomerStatus>
    {
        public IQueryable<CustomerStatus> GetAll()
        {
            return this.GetCities().AsQueryable();
        }

        private IList<CustomerStatus> GetCities()
        {
            IList<CustomerStatus> cities = new List<CustomerStatus>();
            cities.Add(new CustomerStatus { Code = "", Name = "" });
            cities.Add(new CustomerStatus { Code = "1001", Name = "Single" });
            cities.Add(new CustomerStatus { Code = "2001", Name = "Married" });
            cities.Add(new CustomerStatus { Code = "3001", Name = "Divorced" });
            return cities;
        }      
    }
}