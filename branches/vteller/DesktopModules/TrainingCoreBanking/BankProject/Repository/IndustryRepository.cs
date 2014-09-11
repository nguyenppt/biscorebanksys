using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Repository
{
    public class IndustryRepository : IRepository<Industry>
    {
        public IQueryable<Industry> GetAll()
        {
            return this.GetCities().AsQueryable();
        }

        private IList<Industry> GetCities()
        {
            IList<Industry> cities = new List<Industry>();
            cities.Add(new Industry { Code = "", VNDescription = "", GBDescription = "" });
            cities.Add(new Industry { Code = "1011", VNDescription = "Trong lua", GBDescription   = "Plant rice" });
            cities.Add(new Industry { Code = "1012", VNDescription = "Trong mia", GBDescription = "Plant sugarcane" });
            cities.Add(new Industry { Code = "1013", VNDescription = "Trong cao su",GBDescription = "Plant rubber tree" });
            cities.Add(new Industry { Code = "1014", VNDescription = "Trong ca phe", GBDescription = "Plant coffee tree" });
            cities.Add(new Industry { Code = "1015", VNDescription = "Trong tieu", GBDescription = "Plant pepper tree" });
            cities.Add(new Industry { Code = "1016", VNDescription = "Trong dieu", GBDescription = "Plant cashew tree" });
            cities.Add(new Industry { Code = "1017", VNDescription = "Trong cay an trai", GBDescription = "Plant fruit trees" });
            cities.Add(new Industry { Code = "1018", VNDescription = "Trong cay khac", GBDescription = "Plant other trees" });
            cities.Add(new Industry { Code = "1019", VNDescription = "Nuoi heo", GBDescription = "Pig husbandry" });
            cities.Add(new Industry { Code = "1020", VNDescription = "Nuoi bo", GBDescription = "Feed cow" });
            return cities;
        }

        public Industry GetById()
        {
            throw new NotImplementedException();
        }
    }
}