using BankProject.DBContext;
using BankProject.DBRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Business
{
    public class NewNormalLoanRepaymentBusiness :NormalLoanBaseBusiness,  INewNormalLoanBusiness<BNEWNORMALLOAN>
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
                entry = facade.findExistingLoanRepayment(entry.Code, "AUT", null).FirstOrDefault();

                if (entry != null && entry.Drawdown == null)
                {
                    StoreProRepository storeFacade = new StoreProRepository();
                    var outCheck = storeFacade.StoreProcessor().B_Normal_Loan_Process_DisbursalRepaymentCheck(entry.Code).FirstOrDefault();

                    if (outCheck != null && (outCheck.DisbursalDate < DateTime.Now && outCheck.DisbursalDrawdownDate < DateTime.Now)
                        && outCheck.TotalDisbursalAmount >= outCheck.LoanAmount)
                    {
                        //ok for process
                    }
                    else
                    {
                        entry = null;
                    }
                }
            }
        }

        public void loadEntrities(ref List<BNEWNORMALLOAN> entries)
        {
            entries = facade.findAllNormalLoans("AUT", null, "UNA").ToList();
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
            int repaymenttimes = 0;
            if (Entity == null || String.IsNullOrEmpty(Entity.Code)) return;
            BNEWNORMALLOAN existLoan = facade.findExistingLoan(Entity.Code, null, null).FirstOrDefault();
            if (existLoan != null)
            {
                CashRepaymentRepository cashFacade = new CashRepaymentRepository();
                var cashRepay = cashFacade.FindActiveCashRepayment(Entity.CreditAccount).FirstOrDefault();

                if (cashRepay != null && cashRepay.AmountDeposited != null)
                {
                    Entity.Tot_I_Pay_Amt = (Entity.Tot_I_Pay_Amt == null ? 0 : (decimal)Entity.Tot_I_Pay_Amt) 
                        + (cashRepay.AmountDeposited == null ? 0 : (decimal)cashRepay.AmountDeposited);
                }

                repaymenttimes = Entity.RepaymentTimes;
                Entity = existLoan;
                Entity.Repaid_AuthorizedBy = userID;
                Entity.Repaid_AuthorizedDate = facade.GetSystemDatetime();
                Entity.Repaid_Status = "AUT";
                Entity.RepaymentTimes = Entity.RepaymentTimes + 1;
                facade.Update(existLoan, Entity);
                facade.Commit();

                

                if (cashRepay != null && cashRepay.AmountDeposited != null)
                {
                    var cashRepayN = cashFacade.FindActiveCashRepayment(Entity.CreditAccount).FirstOrDefault();
                    cashRepayN.RepaidLoanFlag = 1;
                    cashFacade.Update(cashRepay, cashRepayN);
                    cashFacade.Commit();
                }

                StoreProRepository storeFacade = new StoreProRepository();
                storeFacade.StoreProcessor().B_Normal_Loan_Process_Payment_ClearUnusedSchedule(Entity.Code, repaymenttimes);

            }
        }
    }
}