using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BankProject.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class
    {

        public Repository()
        {
            this.DataContext = new BankProjectModelsDataContext();
            string connectionString = ConfigurationManager.ConnectionStrings["VietVictoryCoreBanking"].ConnectionString;
            //this.DataContext.Database.Connection = new SqlConnection(connectionString);
        }
        protected BankProjectModelsDataContext DataContext { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            ///return this.DataContext.Set<T>().AsQueryable();
            return this.DataContext.GetTable<T>().AsQueryable();
        }
    }
}