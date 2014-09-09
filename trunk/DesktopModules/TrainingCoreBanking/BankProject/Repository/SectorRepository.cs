using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Repository
{
    public class SectorRepository : IRepository<Sector>
    {
        public IQueryable<Sector> GetAll()
        {
            return this.GetCities().AsQueryable();
        }

        public IQueryable<Sector> GetForIndividualCustomer()
        {
            IList<Sector> sectors = this.GetCities().Where(s => s.Code == "2001").ToList();
            sectors.Insert(0, new Sector { Code = string.Empty });
            return sectors.AsQueryable();
        }

        public Sector GetById()
        {
            throw new NotImplementedException();
        }

        private IList<Sector> GetCities()
        {
            IList<Sector> cities = new List<Sector>();
            cities.Add(new Sector { Id = 1, Code = "", Name = "" });
            cities.Add(new Sector { Id = 2001, Code = "2001", Name = "Thanh Phan Kinh Te Ca The" });
            cities.Add(new Sector { Id = 1301, Code = "1301", Name = "Cty trach nhiem huu han Nha nuoc" });
            cities.Add(new Sector { Id = 1309, Code = "1309", Name = "Cty trach nhiem huu han khac" });
            cities.Add(new Sector { Id = 1300, Code = "1300", Name = "Cong ty trach nhiem Huu han" });
            cities.Add(new Sector { Id = 1400, Code = "1400", Name = "DN co von dau tu nuoc ngoai"});
            cities.Add(new Sector { Id = 1500, Code = "1500", Name = "To chuc kinh te tap the" } );
            cities.Add(new Sector { Id = 1600, Code = "1600", Name = "Thanh phan kinh te ca the" });
            cities.Add(new Sector { Id = 1900, Code = "1900", Name = "To chuc kinh te khac" });
            cities.Add(new Sector { Id = 2800, Code = "2800", Name = "Corporation" });
            return cities;
        }
    }
}