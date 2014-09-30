﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Linq.Expressions;

namespace BankProject.DBRespository
{
    /******************************************************************************
     * Description:
     *      Concerate Repository for loan principle payment schedule
     * Created By: 
     *      Nghia Le
     ******************************************************************************/


    /**
     * *****HISTORY****
     * Date                 By                  Description of change
     * **************************************************************************
     * 29-Sep-2014          Nghia               Init code
     *
     * 
     * 
     * 
     * ****************************************************************************
     */
    public class NormalLoanPrinciplePaymentSchedule:BaseRepository<B_NORMALLOAN_PRINCIPLE_PAYMENT_SCHEDULE>
    {
        // Summary:
        // Get All perios payment plan of a loan contract     
        public IQueryable<B_NORMALLOAN_PRINCIPLE_PAYMENT_SCHEDULE> GetScheduleDetail(string code)
        {
            Expression<Func<B_NORMALLOAN_PRINCIPLE_PAYMENT_SCHEDULE, bool>> query = t => t.Code.Equals(code);

            return Find(query);
        }

        public void DeleteAllScheduleOfContract(string code)
        {
            Expression<Func<B_NORMALLOAN_PRINCIPLE_PAYMENT_SCHEDULE, bool>> query = t => t.Code.Equals(code);

            var ites = Find(query);

            foreach (var it in ites)
            {
                Delete(it);
            }
            Commit();
        }
    }
}