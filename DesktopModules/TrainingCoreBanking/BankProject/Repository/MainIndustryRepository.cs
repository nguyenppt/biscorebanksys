using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Repository
{
    public class MainIndustryRepository : IRepository<MainIndustry>
    {
        public IQueryable<MainIndustry> GetAll()
        {
            return this.GetCities().AsQueryable();
        }

        public MainIndustry GetById()
        {
            throw new NotImplementedException();
        }

        private IList<MainIndustry> GetCities()
        {
            IList<MainIndustry> cities = new List<MainIndustry>();
            cities.Add(new MainIndustry { Id = 1, Code = "", VNDescription = "", GBDescription = "" });
            cities.Add(new MainIndustry { Id = 1, Code = "101", VNDescription = "Nong Nghiep", GBDescription = "Agriculture" });
            cities.Add(new MainIndustry { Id = 2, Code = "102", VNDescription = "Lam Nghiep", GBDescription = "Forestry" });
            cities.Add(new MainIndustry { Id = 3, Code = "121", VNDescription = "Thuy San", GBDescription = "Aguatic Product" });
            cities.Add(new MainIndustry { Id = 4, Code = "141", VNDescription = "Cong nghiep khai thac", GBDescription = "Mine Exploitation Industry" });
            cities.Add(new MainIndustry { Id = 5, Code = "151", VNDescription = "Thuc pham", GBDescription = "Food" });            
            cities.Add(new MainIndustry { Id = 5, Code = "152", VNDescription = "Thuoc La", GBDescription = "Tobacco" });
            cities.Add(new MainIndustry { Id = 5, Code = "153", VNDescription = "Det, dan len", GBDescription = "Weave, knit" });            
            cities.Add(new MainIndustry { Id = 5, Code = "154", VNDescription = "May mac", GBDescription = "Garment" });
            cities.Add(new MainIndustry { Id = 5, Code = "155", VNDescription = "Thuoc da", GBDescription = "Leather" });
            return cities;
        }
    }
}