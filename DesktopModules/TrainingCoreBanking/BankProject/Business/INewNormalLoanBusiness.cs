using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankProject.Business
{
    public interface INewNormalLoanBusiness<T> : ICommonBusiness<T> where T : class
    {
        LoanContractScheduleDS PrepareDataForLoanContractSchedule(T entry, int replaymentTimes);
        void UpdateDataToPriciplePaymentSchedule(T entry, int replaymentTime, int userID);
    }
}
