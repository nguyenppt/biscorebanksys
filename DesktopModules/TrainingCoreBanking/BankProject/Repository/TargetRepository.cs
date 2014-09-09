using System;
using System.Collections.Generic;
using System.Linq;
using BankProject.Entity;

namespace BankProject.Repository
{
    public class TargetRepository : IRepository<Target>
    {
        public IQueryable<Target> GetAll()
        {
            return this.GetCities().AsQueryable();
        }

        private IList<Target> GetCities()
        {
            IList<Target> cities = new List<Target>();
            cities.Add(new Target { Code = "", Name = "" });
            cities.Add(new Target { Code = "2001", Name = "INDIVIDUAL MARKET - HIGHT NET WORTH" });
            return cities;
        }             
    }
}