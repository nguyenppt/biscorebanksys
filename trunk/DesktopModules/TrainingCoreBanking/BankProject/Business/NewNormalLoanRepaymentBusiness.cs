using BankProject.DBContext;
using BankProject.DBRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Business
{
    public class NewNormalLoanRepaymentBusiness : INewNormalLoanBusiness<BNEWNORMALLOAN>
    {
        NormalLoanRepository facade = new NormalLoanRepository();
        public BNEWNORMALLOAN Entity
        {
            get;
            set;
        }

        public void loadEntity(ref BNEWNORMALLOAN entry)
        {
            if (entry != null && !String.IsNullOrEmpty(entry.Code))
            {
                entry = facade.findExistingLoan(entry.Code, "AUT", null).FirstOrDefault();
            }
        }

        public void loadEntrities(ref List<BNEWNORMALLOAN> entries)
        {
            entries = facade.findAllNormalLoans("AUT", null, "UAT").ToList();
        }

        public void commitProcess(int userID)
        {
            if (Entity == null || String.IsNullOrEmpty(Entity.Code)) return;
            BNEWNORMALLOAN existLoan = facade.findExistingLoan(Entity.Code, "AUT", null).FirstOrDefault();
            if (existLoan != null)
            {
                Entity.RepaidBy = userID;
                Entity.Repaid_UpdatedDate = facade.GetSystemDatetime();
                Entity.Repaid_Status = "UNA";
                facade.Update(existLoan, Entity);
                facade.Commit();
            }
        }

        public void previewProcess(int userID)
        {
            throw new NotImplementedException();
        }

        public void revertProcess(int userID)
        {
            if (Entity == null || String.IsNullOrEmpty(Entity.Code)) return;
            BNEWNORMALLOAN existLoan = facade.findExistingLoan(Entity.Code, null, null).FirstOrDefault();
            if (existLoan != null)
            {
                Entity = existLoan;
                Entity.UpdatedBy = userID;
                Entity.UpdatedDate = facade.GetSystemDatetime();
                Entity.Repaid_Status = "REV";
                facade.Update(existLoan, Entity);
                facade.Commit();
            }
        }

        public void authorizeProcess(int userID)
        {
            if (Entity == null || String.IsNullOrEmpty(Entity.Code)) return;
            BNEWNORMALLOAN existLoan = facade.findExistingLoan(Entity.Code, null, null).FirstOrDefault();
            if (existLoan != null)
            {
                Entity = existLoan;
                Entity.Repaid_AuthorizedBy = userID;
                Entity.Repaid_AuthorizedDate = facade.GetSystemDatetime();
                Entity.Repaid_Status = "AUT";
                facade.Update(existLoan, Entity);
                facade.Commit();
            }
        }
    }
}