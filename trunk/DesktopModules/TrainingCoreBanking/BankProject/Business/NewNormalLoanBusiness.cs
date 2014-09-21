using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using BankProject.DBRespository;

namespace BankProject.Business
{
    public class NewNormalLoanBusiness : INewNormalLoanBusiness<BNEWNORMALLOAN>
    {
        NormalLoanRepository facade = new NormalLoanRepository();
        
        public void loadEntity(ref BNEWNORMALLOAN entry)
        {
            if (entry!= null && !String.IsNullOrEmpty(entry.Code))
            {
                entry = facade.findCustomerCode(entry.Code).FirstOrDefault();
            }
            else
            {
          
                entry = new BNEWNORMALLOAN();
                entry.Code = entry.Code;
            }
        }

        public void loadEntrities(ref List<BNEWNORMALLOAN> entries)
        {
            entries = entries = facade.findAllNormalLoans("UNA", null).ToList();
        }

        public void commitProcess(int userID)
        {
            if(Entity== null || String.IsNullOrEmpty(Entity.Code)) return;
            BNEWNORMALLOAN existLoan = facade.findExistingLoan(Entity.Code, null, null).FirstOrDefault();
            if (existLoan != null)
            {
                Entity.UpdatedBy = userID;
                Entity.UpdatedDate = facade.GetSystemDatetime();
                facade.Update(existLoan, Entity);
            }
            else
            {
                Entity.CreateBy = userID;
                Entity.CreateDate = facade.GetSystemDatetime();
                facade.Add(Entity);
            }
            Entity.Status = "UNA";
            
            facade.Commit();
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
                Entity.AuthorizedBy = userID;
                Entity.AuthorizedDate = facade.GetSystemDatetime();
                Entity.Status = "AUT";
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