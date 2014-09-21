using BankProject.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BankProject.DBRespository
{
    public class NormalLoanRepository:BaseRepository<BNEWNORMALLOAN>
    {
        public IQueryable<BNEWNORMALLOAN> findCustomerCode(String code)
        {
            Expression<Func<BNEWNORMALLOAN, bool>> loan = t => t.Code.Equals(code);

            return Find(loan);
        }

        public IQueryable<BNEWNORMALLOAN> findCustomerCodeAUT(String code)
        {
            Expression<Func<BNEWNORMALLOAN, bool>> loan = t => t.Code.Equals(code) && t.Status=="AUT";

            return Find(loan);
        }

        public IQueryable<BNEWNORMALLOAN> findAllNormalLoans(string status, string amendStatus)
        {
            Expression<Func<BNEWNORMALLOAN, bool>> loan = t => t.Status == status
                 && (String.IsNullOrEmpty(amendStatus) || t.Amend_Status == amendStatus);

            return Find(loan);
        }

        public IQueryable<BNEWNORMALLOAN> findExistingLoan(String code, string status, string amendStatus)
        {
            Expression<Func<BNEWNORMALLOAN, bool>> loan = t => t.Code.Equals(code) && (String.IsNullOrEmpty(status) || t.Status == status) 
                && (String.IsNullOrEmpty(amendStatus) || t.Amend_Status == amendStatus);

            return Find(loan);
        }
    }
}