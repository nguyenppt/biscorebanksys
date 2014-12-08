using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    public class ExchangeRatesRepository:BaseRepository<B_ExchangeRates>
    {
        public IQueryable<B_ExchangeRates> GetRate(string currency)
        {
            Expression<Func<B_ExchangeRates, bool>> query = t => t.Currency.Equals(currency);
            return Find(query);
        }
    }
}