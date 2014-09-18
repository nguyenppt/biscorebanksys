using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankProject.DBRespository
{
    public interface IRepositoryDB<T>
    {
        IQueryable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity, T entryNew);
        void Commit();
        T GetById(Object id);
    }
}
