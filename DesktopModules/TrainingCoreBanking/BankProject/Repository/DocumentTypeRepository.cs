using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Repository
{
    public class DocumentTypeRepository : IRepository<DocumentType>
    {
        public IQueryable<DocumentType> GetAll()
        {
            return this.GetNationalities().AsQueryable();
        }

        public DocumentType GetById()
        {
            throw new NotImplementedException();
        }

        private IList<DocumentType> GetNationalities()
        {
            IList<DocumentType> nationalities = new List<DocumentType>();
            nationalities.Add(new DocumentType { Id = 0, Name = "" });
            nationalities.Add(new DocumentType { Id = 1, Name = "COM.CONT.PER.INFO" });
            nationalities.Add(new DocumentType { Id = 2, Name = "DRIVING.LICENSE" });
            nationalities.Add(new DocumentType { Id = 3, Name = "ESTAB.LIC.CODE" });
            nationalities.Add(new DocumentType { Id = 4, Name = "NATIONAL.ID" });
            nationalities.Add(new DocumentType { Id = 5, Name = "OTHER.DOC" });
            nationalities.Add(new DocumentType { Id = 6, Name = "PASSPORT" });
            nationalities.Add(new DocumentType { Id = 7, Name = "TAX CODE" });
            return nationalities;
        }
    }
}