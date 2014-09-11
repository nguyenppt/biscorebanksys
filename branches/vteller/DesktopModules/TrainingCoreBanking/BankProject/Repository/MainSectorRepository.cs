using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Repository
{
    public class MainSectorRepository : IRepository<MainSector>
    {
        public IQueryable<MainSector> GetAll()
        {
            return this.GetCities().AsQueryable();
        }

        public IQueryable<MainSector> GetForIndividualCustomer()
        {
            IList<MainSector> mainSectors = (from ms in this.GetCities()
                                             where ms.Id == 200
                                             select ms).AsQueryable().ToList();
            mainSectors.Insert(0, new MainSector { Code = string.Empty });
            return mainSectors.AsQueryable();
        }
        public MainSector GetById()
        {
            throw new NotImplementedException();
        }

        private IList<MainSector> GetCities()
        {
            IList<MainSector> cities = new List<MainSector>();
            cities.Add(new MainSector { Id = 1000, Code = "", Name = "", ShortName = "" }); //VNDescription
            cities.Add(new MainSector { Id = 200, Code = "200", Name = "Thanh phan kinh te ca the", ShortName = "Individuals" });
            return cities;
        }
    }
}