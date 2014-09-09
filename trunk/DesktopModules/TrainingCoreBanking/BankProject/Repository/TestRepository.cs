using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Repository
{
    public class TestRepository : IRepository<City>
    {
        public IQueryable<City> GetAll()
        {
            throw new NotImplementedException();
        }

        private IList<Target> GetCities()
        {
            IList<Target> cities = new List<Target>();
            cities.Add(new Target { Code = "2001", Name = "INDIVIDUAL MARKET - HIGHT NET WORTH" });
            cities.Add(new Target { Code = "2002", Name = "Target 2" });
            cities.Add(new Target { Code = "2003", Name = "Target 3" });
            cities.Add(new Target { Code = "2004", Name = "Target 4" });
            cities.Add(new Target { Code = "2005", Name = "Target 5" });
            return cities;
        }    
    }
}