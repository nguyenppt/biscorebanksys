using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        //T GetById();
    }
}