using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Repository
{
    public class CityRepository : IRepository<City>
    {
        public CityRepository()
        {            
        }       

        public IQueryable<City> GetAll()
        {
            return this.GetCities().AsQueryable();
        }

        private IList<City> GetCities()
        {
            IList<City> cities = new List<City>();
            cities.Add(new City { Code = "", Name = "" }); 
            cities.Add(new City {  Code = "1000", Name = "TP.HO CHI MINH" });
            cities.Add(new City { Code = "1010", Name = "DA NANG" });
            cities.Add(new City { Code = "1200", Name = "QUANG NAM" });
            cities.Add(new City { Code = "1300", Name = "QUANG NGAI" });
            cities.Add(new City { Code = "1400", Name = "BINH DINH" });
            cities.Add(new City { Code = "1500", Name = "PHU YEN" });
            cities.Add(new City { Code = "1600", Name = "KHANH HOA" });            
            cities.Add(new City { Code = "1800", Name = "BINH THUAN" });
            cities.Add(new City { Code = "1900", Name = "NINH THUAN" });
            cities.Add(new City { Code = "2000", Name = "GIA LAI" });
            return cities;
        }

        public City GetById()
        {
            throw new NotImplementedException();
        }
    }
}