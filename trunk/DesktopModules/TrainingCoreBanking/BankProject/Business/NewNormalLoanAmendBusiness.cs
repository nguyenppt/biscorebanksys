using BankProject.DBContext;
using BankProject.DBRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Business
{
    public class NewNormalLoanAmendBusiness : INewNormalLoanBusiness<BNEWNORMALLOAN>
    {
        NormalLoanRepository facade = new NormalLoanRepository();   

        public void loadEntity(ref BNEWNORMALLOAN entry)
        {
            if (entry != null && !String.IsNullOrEmpty(entry.Code))
            {
                entry = facade.findExistingLoan(entry.Code, "AUT", null).FirstOrDefault();
            }

        }

        public void loadEntrities(ref List<BNEWNORMALLOAN> entries)
        {
            entries = facade.findAllNormalLoans("AUT", "UNA").
                Union(facade.findAllNormalLoans("AUT", String.Empty)).ToList();
        }

        public void commitProcess(int userID)
        {
            if (Entity == null || String.IsNullOrEmpty(Entity.Code)) return;
            BNEWNORMALLOAN existLoan = facade.findExistingLoan(Entity.Code, "AUT", null).FirstOrDefault();
            if (existLoan != null)
            {
                Entity.AmendedBy = userID;
                Entity.Amend_UpdatedDate = facade.GetSystemDatetime();
                Entity.Amend_Status = "UNA";
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
                Entity.Status = "REV";
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
                Entity.Amend_AuthorizedBy = userID;
                Entity.Amend_AuthorizedDate = facade.GetSystemDatetime();
                Entity.Amend_Status = "AUT";
                facade.Update(existLoan, Entity);
                facade.Commit();
            }
        }



        public BNEWNORMALLOAN Entity
        {
            get;
            set;
        }
    }
}