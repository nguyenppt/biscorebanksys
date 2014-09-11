using System;
using System.Collections.Generic;
using System.Linq;
using BankProject.Entity;

namespace BankProject.Repository
{
    public class NationalityRepository : Repository<Nationality>
    {
        public new IQueryable<Nationality> GetAll()
        {
            IList<Nationality> nationalities = (from country in this.DataContext.BCOUNTRies
                                               select new Nationality
                                               {
                                                   Code = country.MaQuocGia,
                                                   TenTA = country.TenTA,
                                                   TenTV = country.TenTV
                                               }).ToList();

            nationalities.Insert(0, new Nationality { Code = string.Empty });
            return nationalities.AsQueryable();
        }

        private IList<Nationality> GetNationalities()
        {
            IList<Nationality> nationalities = new List<Nationality>();
            nationalities.Add(new Nationality { Id = 1, Code = "", TenTA = "" });
            nationalities.Add(new Nationality { Id = 1, Code = "VN", TenTA = "Viet Nam" });
            nationalities.Add(new Nationality { Id = 2, Code = "TQ", TenTA = "Nationality 2" });
            nationalities.Add(new Nationality { Id = 3, Code = "MY", TenTA = "Nationality 3" });
            nationalities.Add(new Nationality { Id = 4, Code = "ANH", TenTA = "Nationality 4" });
            nationalities.Add(new Nationality { Id = 5, Code = "PHAP", TenTA = "Nationality 5" });
            return nationalities;
        }


        public Nationality GetById()
        {
            throw new NotImplementedException();
        }
    }
}