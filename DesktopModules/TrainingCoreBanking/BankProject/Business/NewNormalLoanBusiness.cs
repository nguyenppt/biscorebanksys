using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using BankProject.DBRespository;

namespace BankProject.Business
{
    public class NewNormalLoanBusiness :NormalLoanBaseBusiness,  INewNormalLoanBusiness<BNEWNORMALLOAN>
    {
        NormalLoanRepository facade = new NormalLoanRepository();

        public void loadEntity(ref BNEWNORMALLOAN entry)
        {
            BNEWNORMALLOAN temp = null;
            if (entry != null && !String.IsNullOrEmpty(entry.Code))
            {
                temp = facade.findCustomerCode(entry.Code).FirstOrDefault();
            }

            if (temp != null)
            {

                entry = temp;

            }
            else
            {
                entry.Currency = "VND";
                entry.OpenDate = DateTime.Now;
                entry.ValueDate = DateTime.Now;
                entry.Drawdown = DateTime.Now;
                entry.BusDayDef = "VN";
                entry.DefineSch = "Y";
                entry.RateType = "1";//Default is Fix A
            }


        }

        public void loadEntrities(ref List<BNEWNORMALLOAN> entries)
        {
            entries = entries = facade.findAllNormalLoans("UNA", null).ToList();
        }

        public void commitProcess(int userID)
        {
            if (Entity == null || String.IsNullOrEmpty(Entity.Code)) return;
            BNEWNORMALLOAN existLoan = facade.findExistingLoan(Entity.Code, null, null).FirstOrDefault();

            if (existLoan != null)
            {
                Entity.UpdatedBy = userID;
                Entity.UpdatedDate = facade.GetSystemDatetime();
                Entity.Status = "UNA";
                facade.Update(existLoan, Entity);
            }
            else
            {
                Entity.CreateBy = userID;
                Entity.CreateDate = facade.GetSystemDatetime();
                Entity.Status = "UNA";
                Entity.RepaymentTimes = 0;
                facade.Add(Entity);
            }

            updateNormalLoanRepayment(Entity, Entity.RepaymentTimes, ((decimal)Entity.LoanAmount), Entity.Drawdown);
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