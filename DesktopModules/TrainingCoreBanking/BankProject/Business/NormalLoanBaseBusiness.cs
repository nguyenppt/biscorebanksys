using BankProject.DBContext;
using BankProject.DBRespository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BankProject.Business
{
    public class NormalLoanBaseBusiness
    {
        DateTime? disbursalDate;

        public LoanContractScheduleDS PrepareDataForLoanContractSchedule(BNEWNORMALLOAN normalLoanEntryM, int replaymentTimes)
        {

            if (normalLoanEntryM == null)
                return null;


            LoanContractScheduleDS dsOut = new LoanContractScheduleDS();
            NormalLoanRepaymentRepository facade = new NormalLoanRepaymentRepository();
            BNEWNORMALLOAN_REPAYMENT entry = facade.FindRepaymentAmount(normalLoanEntryM.Code, replaymentTimes).FirstOrDefault();

            DataRow drInfor = dsOut.DtInfor.NewRow();
            drInfor[dsOut.Cl_code] = normalLoanEntryM.Code;
            drInfor[dsOut.Cl_custID] = normalLoanEntryM.CustomerID;
            drInfor[dsOut.Cl_cust] = normalLoanEntryM.CustomerName;
            if (normalLoanEntryM.Drawdown != null)
            {
                if (entry != null && entry.ActivatedDate != null)
                {
                    drInfor[dsOut.Cl_drawdown] = entry.ActivatedDate;
                }
                else
                {
                    drInfor[dsOut.Cl_drawdown] = normalLoanEntryM.Drawdown;
                }
            }

            drInfor[dsOut.Cl_loadAmount] = normalLoanEntryM.LoanAmount;
            drInfor[dsOut.Cl_loadAmountRepayment] = getCurrentLoanAmount(normalLoanEntryM, replaymentTimes);
            drInfor[dsOut.Cl_interestKey] = "";
            drInfor[dsOut.Cl_interest] = 0;
            dsOut.DtInfor.Rows.Add(drInfor);

            bool isDisbursal = normalLoanEntryM.Drawdown == null ? true : false;

            //Process payment
            PaymentProcess(ref dsOut, normalLoanEntryM, replaymentTimes);
            //Process disbursal
            if (isDisbursal)
            {
                DisbursalProcess(ref dsOut, normalLoanEntryM, ref disbursalDate);
            }
            //Process interest
            InterestProcess(ref dsOut, normalLoanEntryM, replaymentTimes, disbursalDate);

            return dsOut;
        }

        public void UpdateDataToPriciplePaymentSchedule(BNEWNORMALLOAN normalLoanEntryM, int replaymentTime, int userID)
        {
            NormalLoanPaymentSchedule facde = new NormalLoanPaymentSchedule();
            var ds = PrepareDataForLoanContractSchedule(normalLoanEntryM, replaymentTime);
            DataTable dtInfor = ds.DtInfor;
            DataTable dtItems = ds.DtItems;

            if (dtInfor == null || dtItems == null)
            {
                return;
            }
            facde.DeleteAllScheduleOfContract(dtInfor.Rows[0]["Code"].ToString(), replaymentTime);

            foreach (DataRow it in dtItems.Rows)
            {
                B_NORMALLOAN_PAYMENT_SCHEDULE princleSchedue = new B_NORMALLOAN_PAYMENT_SCHEDULE();
                princleSchedue.Code = dtInfor.Rows[0]["Code"].ToString();
                princleSchedue.CustomerID = dtInfor.Rows[0]["CustomerID"].ToString();
                princleSchedue.LoanAmount = decimal.Parse((dtInfor.Rows[0]["LoanAmount"].ToString().Replace(",", "")));
                princleSchedue.Drawdown = String.IsNullOrEmpty(dtInfor.Rows[0]["Drawdown"].ToString()) ? this.disbursalDate : (DateTime)dtInfor.Rows[0]["Drawdown"];
                princleSchedue.InterestKey = dtInfor.Rows[0]["InterestKey"].ToString();
                princleSchedue.Freq = dtInfor.Rows[0]["Freq"].ToString();
                princleSchedue.PeriodRepaid = replaymentTime;
                princleSchedue.Interest = (Decimal)dtInfor.Rows[0]["interest"];


                princleSchedue.Period = (Int16)it["Perios"];
                princleSchedue.DueDate = (DateTime)it["DueDate"];
                princleSchedue.PrincipalAmount = (Decimal)it["Principle"];
                princleSchedue.PrinOS = (Decimal)it["PrinOS"];
                princleSchedue.InterestAmount = (Decimal)it["InterestAmount"];
                princleSchedue.CreateBy = userID;
                princleSchedue.CreateDate = facde.GetSystemDatetime();
                facde.Add(princleSchedue);

            }
            facde.Commit();

            StoreProRepository storeFacase = new StoreProRepository();
            storeFacase.StoreProcessor().B_Normal_Loan_Process_Payment_AmendAuthorizeProcess(dtInfor.Rows[0]["Code"].ToString(), replaymentTime);


        }

        private decimal getCurrentLoanAmount(BNEWNORMALLOAN normalLoanEntryM, int replaymentTimes)
        {
            NormalLoanRepaymentRepository facade = new NormalLoanRepaymentRepository();
            BNEWNORMALLOAN_REPAYMENT entry = facade.FindRepaymentAmount(normalLoanEntryM.Code, replaymentTimes).FirstOrDefault();
            if (entry != null)
            {
                return entry.LoanAmount;
            }
            else
            {
                return (decimal)normalLoanEntryM.LoanAmount;
            }
        }

        private void PaymentProcess(ref LoanContractScheduleDS ds, BNEWNORMALLOAN normalLoanEntryM, int replaymentTimes)
        {
            int rateType = 1; //default is Fix A/ Periodic --> Du no giam dan. (fix B is du no ban dau)
            bool isDisbursalType = normalLoanEntryM.Drawdown == null ? true : false;

            int numberOfPerios = 0;
            int fregV = 0;
            decimal instalmant = 0;
            decimal instalmantEnd = 0;
            decimal remainAmount = 0;
            decimal remainAmountActual = 0;

            DateTime? periosDate = null;
            DateTime? endDate = null;
            int instalmentdDay = 0;


            rateType = String.IsNullOrEmpty(normalLoanEntryM.RateType) ? 1 : int.Parse(normalLoanEntryM.RateType);

            if (isDisbursalType)
            {
                remainAmountActual = 0;
            }
            else
            {
                remainAmountActual = getCurrentLoanAmount(normalLoanEntryM, replaymentTimes);
            }
            remainAmount = getCurrentLoanAmount(normalLoanEntryM, replaymentTimes);

            //if (rateType != 2)//du no giam dan
            //{

                getPaymentInputControl(ref periosDate, ref endDate, ref numberOfPerios, ref instalmant, ref instalmantEnd, ref fregV, normalLoanEntryM, replaymentTimes);
                instalmentdDay = periosDate != null ? ((DateTime)periosDate).Day : 0;
                ds.DtInfor.Rows[0][ds.Cl_freq] = fregV == 0 ? "Cuối kỳ" : fregV + " Tháng";

                DataRow dr;
                for (int i = 0; i < numberOfPerios; i++)
                {
                    remainAmount = remainAmount - instalmant;
                    remainAmountActual = remainAmountActual - instalmant;
                    dr = ds.DtItems.NewRow();
                    dr[ds.Cl_dueDate] = periosDate;
                    dr[ds.Cl_principle] = instalmant;
                    dr[ds.Cl_PrintOSPlan] = remainAmount;
                    dr[ds.Cl_isInterestedRow] = false;
                    dr[ds.Cl_isPeriodicAutomaticRow] = false;
                    dr[ds.Cl_isPaymentRow] = true;
                    dr[ds.Cl_isDisbursalRow] = false;
                    dr[ds.Cl_PrintOs] = remainAmountActual;
                    dr[ds.Cl_interestAmount] = 0;
                    dr[ds.Cl_DisbursalAmount] = 0;
                    ds.DtItems.Rows.Add(dr);
                    periosDate = ((DateTime)periosDate).AddMonths(fregV);
                    try
                    {
                        periosDate = new DateTime(((DateTime)periosDate).Year, ((DateTime)periosDate).Month, instalmentdDay);
                    }
                    catch
                    {
                    }
                }

                //remainAmount = remainAmount - instalmant;
                //remainAmountActual = remainAmountActual - instalmant;
                remainAmount = remainAmount - instalmantEnd;
                remainAmountActual = remainAmountActual - instalmantEnd;
                dr = ds.DtItems.NewRow();
                dr[ds.Cl_dueDate] = endDate;
                dr[ds.Cl_principle] = instalmantEnd;
                dr[ds.Cl_PrintOSPlan] = remainAmount;
                dr[ds.Cl_isInterestedRow] = false;
                dr[ds.Cl_isDisbursalRow] = false;
                dr[ds.Cl_isPeriodicAutomaticRow] = false;
                dr[ds.Cl_isPaymentRow] = true;
                dr[ds.Cl_PrintOs] = remainAmountActual;
                dr[ds.Cl_interestAmount] = 0;
                dr[ds.Cl_DisbursalAmount] = 0;
                ds.DtItems.Rows.Add(dr);


            //}
            //else
            //{
            //    ds.DtInfor.Rows[0][ds.Cl_freq] = "Cuối kỳ";
            //    NewLoanControlRepository facade = new NewLoanControlRepository();
            //    BNewLoanControl it = facade.FindLoanControl(normalLoanEntryM.Code, replaymentTimes, "EP")
            //        .Union(facade.FindLoanControl(normalLoanEntryM.Code, replaymentTimes, "P"))
            //        .Union(facade.FindLoanControl(normalLoanEntryM.Code, replaymentTimes, "I+P")).FirstOrDefault();//All get Priciple date
            //    if (it != null)
            //    {
            //        instalmantEnd = it.AmountAction == null ? (decimal)normalLoanEntryM.LoanAmount : (decimal)it.AmountAction;
            //        endDate = it.Date == null ? normalLoanEntryM.MaturityDate : it.Date;

            //    }


            //    instalmant = (decimal)(instalmantEnd == 0 ? normalLoanEntryM.LoanAmount : instalmantEnd);

            //    DataRow dr = ds.DtItems.NewRow();
            //    dr[ds.Cl_dueDate] = endDate == null ? normalLoanEntryM.MaturityDate : endDate;
            //    dr[ds.Cl_principle] = instalmantEnd == 0 ? normalLoanEntryM.LoanAmount : instalmantEnd;
            //    dr[ds.Cl_PrintOSPlan] = 0;
            //    dr[ds.Cl_PrintOs] = remainAmountActual - instalmant;
            //    dr[ds.Cl_isInterestedRow] = false;
            //    dr[ds.Cl_isPeriodicAutomaticRow] = false;
            //    dr[ds.Cl_isDisbursalRow] = false;
            //    dr[ds.Cl_isPaymentRow] = true;
            //    dr[ds.Cl_DisbursalAmount] = 0;
            //    ds.DtItems.Rows.Add(dr);

            //}

        }

        private void DisbursalProcess(ref LoanContractScheduleDS ds, BNEWNORMALLOAN normalLoanEntryM, ref DateTime? disbursalDate)
        {

            LoanDisbursalScheduleRespository facade = new LoanDisbursalScheduleRespository();
            var disbursalIts = facade.FindLoanDisbursalByCode(normalLoanEntryM.Code);
            DateTime? disbursalDrawdawnDate;
            if (disbursalIts != null)
            {
                foreach (B_LOAN_DISBURSAL_SCHEDULE dis in disbursalIts)
                {
                    disbursalDrawdawnDate = dis.DrawdownDate == null ? dis.DisbursalDate : dis.DrawdownDate;

                    if (disbursalDrawdawnDate == null)
                    {
                        continue;
                    }
                    DataRow dr = findInstallmantRow((DateTime)disbursalDrawdawnDate, ds);
                    if (dr == null)
                    {
                        dr = ds.DtItems.NewRow();
                        dr[ds.Cl_dueDate] = disbursalDrawdawnDate;
                        dr[ds.Cl_isInterestedRow] = false;
                        dr[ds.Cl_isPeriodicAutomaticRow] = false;
                        dr[ds.Cl_isPaymentRow] = false;
                        dr[ds.Cl_principle] = 0;
                        dr[ds.Cl_PrintOs] = 0;
                        ds.DtItems.Rows.Add(dr);
                    }

                    dr[ds.Cl_DisbursalAmount] = dis.DisbursalAmount;
                    dr[ds.Cl_isDisbursalRow] = true;


                }
            }

            ds.DtItems.DefaultView.Sort = "DueDate asc";
            ds.DtItems = ds.DtItems.DefaultView.ToTable();
            decimal currentProcessAmount = 0;

            if (ds.DtItems != null && ds.DtItems.Rows.Count > 0)
            {
                disbursalDate = (DateTime)ds.DtItems.Rows[0][ds.Cl_dueDate.ColumnName];
            }

            foreach (DataRow dr in ds.DtItems.Rows)
            {
                if ((decimal)dr[ds.Cl_PrintOs.ColumnName] != 0)
                {
                    currentProcessAmount = (decimal)dr[ds.Cl_PrintOs.ColumnName];
                }
                else
                {
                    dr[ds.Cl_PrintOs.ColumnName] = currentProcessAmount;
                }


            }

            //Process update disbursal amount to prinos
            currentProcessAmount = 0;
            foreach (DataRow dr in ds.DtItems.Rows)
            {
                if (normalLoanEntryM.RepaymentTimes > 0)
                {
                    currentProcessAmount = getCurrentLoanAmount(normalLoanEntryM, normalLoanEntryM.RepaymentTimes);
                }
                else
                {
                    currentProcessAmount = currentProcessAmount + (decimal)dr[ds.Cl_DisbursalAmount.ColumnName];
                }
                dr[ds.Cl_PrintOs.ColumnName] = (decimal)dr[ds.Cl_PrintOs.ColumnName] + currentProcessAmount;
            }

        }

        private void InterestProcess(ref LoanContractScheduleDS ds, BNEWNORMALLOAN normalLoanEntryM, int replaymentTimes, DateTime? disbursalDate)
        {
            NewLoanControlRepository facade = new NewLoanControlRepository();
            BNewLoanControl it = facade.FindLoanControl(normalLoanEntryM.Code, replaymentTimes, "I+P").
                Union(facade.FindLoanControl(normalLoanEntryM.Code, replaymentTimes, "I")).FirstOrDefault();//All get Priciple date

            if (it == null || String.IsNullOrEmpty(it.Freq))
            {
                it = facade.FindLoanControl(normalLoanEntryM.Code, replaymentTimes, "EP").FirstOrDefault();//All get Priciple date
            }
            if (it == null || String.IsNullOrEmpty(it.Freq))
            {
                return;
            }

            int freq = 0;
            String rateType = "1";
            DateTime drawdownDate = normalLoanEntryM.Drawdown == null ? (disbursalDate == null ? (DateTime)normalLoanEntryM.ValueDate : (DateTime)disbursalDate) : (DateTime)normalLoanEntryM.Drawdown;
            DateTime startDate = (DateTime)normalLoanEntryM.ValueDate;
            DateTime endDate = (DateTime)normalLoanEntryM.MaturityDate;
            DateTime startInterestDate = drawdownDate;
            DateTime prevInterestDate = drawdownDate;
            int durationDate = endDate.Subtract(startDate).Days;
            int interestDay = 0;
            int perios = 0;
            decimal interestedValue = 0;
            decimal interestedValue2 = 0;

            rateType = String.IsNullOrEmpty(normalLoanEntryM.RateType) ? "1" : normalLoanEntryM.RateType;
            if (rateType.Equals("3"))//periodic interest = interestedRate + int speed
            {
                interestedValue = (normalLoanEntryM.InterestRate == null ? 0 : (decimal)normalLoanEntryM.InterestRate)
                    + (String.IsNullOrEmpty(normalLoanEntryM.IntSpread) ? 0 : Decimal.Parse(normalLoanEntryM.IntSpread));
                PeriodicProcess(ref ds, normalLoanEntryM, drawdownDate, replaymentTimes, ref interestedValue2);
            }
            else // interest = interestedRate
            {
                interestedValue = (normalLoanEntryM.InterestRate == null ? 0 : (decimal)normalLoanEntryM.InterestRate);
            }



            ds.DtInfor.Rows[0][ds.Cl_interest] = interestedValue;
            ds.DtInfor.Rows[0][ds.Cl_interestKey] = durationDate / 30;


            if (it.Freq.Equals("E"))
            {
                freq = 0;
                startInterestDate = it.Date == null ? (DateTime)endDate : (DateTime)it.Date;
            }
            else
            {
                freq = int.Parse(it.Freq);
                startInterestDate = it.Date == null ? ((DateTime)drawdownDate) : (DateTime)it.Date;
            }
            interestDay = startInterestDate.Day;

            if (freq > 0)
            {
                perios = durationDate / (freq * 30);
            }
            else
            {
                perios = 1;
            }

            for (int i = 0; i < perios; i++)
            {
                if (i == perios - 1)
                {
                    it = facade.FindLoanControl(normalLoanEntryM.Code, replaymentTimes, "EP").FirstOrDefault();
                    if (it != null)
                    {
                        startInterestDate = it.Date == null ? startInterestDate : (DateTime)it.Date;
                    }
                    else
                    {
                        startInterestDate = (DateTime)normalLoanEntryM.MaturityDate;
                    }
                }

                DataRow dr = findInstallmantRow(startInterestDate, ds);
                if (dr == null)
                {
                    dr = ds.DtItems.NewRow();
                    dr[ds.Cl_dueDate.ColumnName] = startInterestDate;
                    dr[ds.Cl_isDisbursalRow.ColumnName] = false;
                    dr[ds.Cl_isPaymentRow.ColumnName] = false;
                    dr[ds.Cl_isPeriodicAutomaticRow] = false;
                    dr[ds.Cl_principle.ColumnName] = 0;
                    dr[ds.Cl_PrintOs.ColumnName] = 0;
                    ds.DtItems.Rows.Add(dr);
                }
                dr[ds.Cl_isInterestedRow.ColumnName] = true;
                dr[ds.Cl_durationDate.ColumnName] = startInterestDate.Subtract(prevInterestDate).Days;

                prevInterestDate = startInterestDate;
                startInterestDate = startInterestDate.AddMonths(freq);

                try
                {
                    startInterestDate = new DateTime(((DateTime)startInterestDate).Year, ((DateTime)startInterestDate).Month, interestDay);
                }
                catch
                {
                }

                if (startInterestDate.Subtract(endDate).Days > 0)
                    startInterestDate = endDate;

            }


            ds.DtItems.DefaultView.Sort = "DueDate asc";
            ds.DtItems = ds.DtItems.DefaultView.ToTable();

            decimal interestAmount = 0;
            decimal currentAmount = getCurrentLoanAmount(normalLoanEntryM, replaymentTimes);

            decimal amountTemp = 0;
            prevInterestDate = drawdownDate;
            int durationsDay = 0;

            int periousIndex = 0;
            DataRow removeRow = null;
            List<DataRow> removeL = new List<DataRow>();
            for (int i = 0; i < ds.DtItems.Rows.Count; i++)
            {
                DataRow dr = ds.DtItems.Rows[i];
                durationsDay = ((DateTime)dr[ds.Cl_dueDate.ColumnName]).Subtract(prevInterestDate).Days;


                if (rateType.Equals("2"))//fix for initial
                {
                    interestAmount = durationsDay * ((interestedValue / 36000) * (decimal)normalLoanEntryM.LoanAmount);
                }
                else
                {
                    interestAmount = durationsDay * ((interestedValue / 36000) * currentAmount);
                }

                dr[ds.Cl_durationDate.ColumnName] = durationsDay;

                if (!(bool)dr[ds.Cl_isPaymentRow.ColumnName] && !(bool)dr[ds.Cl_isDisbursalRow.ColumnName])
                {
                    dr[ds.Cl_PrintOs.ColumnName] = currentAmount;
                }

                if (dr[ds.Cl_isInterestedRow.ColumnName] != null && (bool)dr[ds.Cl_isInterestedRow.ColumnName])
                {

                    dr[ds.Cl_interestAmount.ColumnName] = interestAmount + amountTemp;
                    amountTemp = 0;
                }
                else
                {
                    amountTemp += interestAmount;
                    dr[ds.Cl_interestAmount.ColumnName] = 0;

                }

                if (dr[ds.Cl_isPeriodicAutomaticRow.ColumnName] != null && (bool)dr[ds.Cl_isPeriodicAutomaticRow.ColumnName])
                {
                    interestedValue = interestedValue2;
                }

                if (!(bool)dr[ds.Cl_isInterestedRow.ColumnName]&& !(bool)dr[ds.Cl_isPaymentRow.ColumnName]
                     && ((bool)dr[ds.Cl_isPeriodicAutomaticRow.ColumnName] || (bool)dr[ds.Cl_isDisbursalRow.ColumnName]))
                {
                    removeL.Add(dr);
                    //removeRow = dr;
                }
                else
                {
                    periousIndex++;
                    dr[ds.Cl_perious.ColumnName] = periousIndex;
                }

                currentAmount = dr[ds.Cl_PrintOs.ColumnName] != null ? (decimal)dr[ds.Cl_PrintOs.ColumnName] : 0;

                prevInterestDate = (DateTime)dr[ds.Cl_dueDate.ColumnName];


            }

            foreach (var removeit in removeL)
            {
                ds.DtItems.Rows.Remove(removeit);
            }
        }

        private void PeriodicProcess(ref LoanContractScheduleDS ds, BNEWNORMALLOAN normalLoanEntryM, DateTime startDrawdownDate, int replaymentTimes, ref decimal newInterestKey)
        {
            if (normalLoanEntryM == null || String.IsNullOrEmpty(normalLoanEntryM.Code))
            {
                return;
            }
            int rateType = 1; //default is Fix A/ Periodic --> Du no giam dan. (fix B is du no ban dau)
            rateType = String.IsNullOrEmpty(normalLoanEntryM.RateType) ? 1 : int.Parse(normalLoanEntryM.RateType);

            if (rateType != 3) //peridodic case
            {
                return;
            }
            NewLoanControlRepository facadeL = new NewLoanControlRepository();
            BNewLoanControl it = facadeL.FindLoanControl(normalLoanEntryM.Code, replaymentTimes, "AC").FirstOrDefault();
            NewLoanInterestedKeyRepository facade = new NewLoanInterestedKeyRepository();
            BLOANINTEREST_KEY interestKey = null;

            if (it != null && !String.IsNullOrEmpty(it.Freq))
            {
                interestKey = facade.GetInterestKey(int.Parse(it.Freq)).FirstOrDefault();
            }
            else
            {
                if (String.IsNullOrEmpty(normalLoanEntryM.InterestKey))
                {
                    return;
                }
                interestKey = facade.GetInterestKey(int.Parse(normalLoanEntryM.InterestKey)).FirstOrDefault();
            }



            if (interestKey != null)
            {
                if (normalLoanEntryM.Currency.Equals("VND"))
                {
                    newInterestKey = (interestKey.VND_InterestRate == null ? 0 : (decimal)interestKey.VND_InterestRate)
                    + (String.IsNullOrEmpty(normalLoanEntryM.IntSpread) ? 0 : Decimal.Parse(normalLoanEntryM.IntSpread));
                }
                else if (normalLoanEntryM.Currency.Equals("USD"))
                {
                    newInterestKey = (interestKey.USD_InterestRate == null ? 0 : (decimal)interestKey.USD_InterestRate)
                    + (String.IsNullOrEmpty(normalLoanEntryM.IntSpread) ? 0 : Decimal.Parse(normalLoanEntryM.IntSpread));
                }
                else
                {
                    newInterestKey = (normalLoanEntryM.InterestRate == null ? 0 : (decimal)normalLoanEntryM.InterestRate)
                   + (String.IsNullOrEmpty(normalLoanEntryM.IntSpread) ? 0 : Decimal.Parse(normalLoanEntryM.IntSpread));
                }



                DateTime newrateDate = startDrawdownDate.AddMonths((int)(interestKey.MonthLoanRateNo));
                DataRow dr = findInstallmantRow(newrateDate, ds);
                if (dr == null)
                {
                    dr = ds.DtItems.NewRow();
                    dr[ds.Cl_dueDate.ColumnName] = newrateDate;
                    dr[ds.Cl_isDisbursalRow.ColumnName] = false;
                    dr[ds.Cl_isPaymentRow.ColumnName] = false;
                    dr[ds.Cl_isPeriodicAutomaticRow] = true;
                    dr[ds.Cl_isInterestedRow.ColumnName] = false;
                    dr[ds.Cl_principle.ColumnName] = 0;
                    dr[ds.Cl_PrintOs.ColumnName] = 0;
                    ds.DtItems.Rows.Add(dr);
                }
                else
                {
                    dr[ds.Cl_isPeriodicAutomaticRow.ColumnName] = true;
                }

            }


        }

        private DataRow findInstallmantRow(DateTime date, LoanContractScheduleDS ds)
        {
            DataRow[] drs = ds.DtItems.Select("DueDate = '" + date.ToString() + "'");
            if (drs != null)
            {
                return drs.SingleOrDefault();
            }

            return null;
        }

        private void getPaymentInputControl(ref DateTime? periosStartDate, ref DateTime? periosEndDate, ref int numberOfPerios,
            ref decimal instalmant, ref decimal instalmantEnd, ref int freg, BNEWNORMALLOAN normalLoanEntryM, int replaymentTimes)
        {
            NewLoanControlRepository facade = new NewLoanControlRepository();
            BNewLoanControl it = facade.FindLoanControl(normalLoanEntryM.Code, replaymentTimes, "I+P").
                Union(facade.FindLoanControl(normalLoanEntryM.Code, replaymentTimes, "P")).FirstOrDefault();//All get Priciple date

            if (it != null)
            {
                periosStartDate = it.Date;
                instalmant = it.AmountAction == null ? 0 : (decimal)it.AmountAction;
                numberOfPerios = it.No == null ? 0 : (int)it.No;
                if (!String.IsNullOrEmpty(it.Freq))
                {
                    if (it.Freq.Equals("E"))
                    {
                        freg = 0;
                    }
                    else
                    {
                        freg = int.Parse(it.Freq);
                    }
                }
                else
                {
                    freg = 0;
                }

            }

            if (periosStartDate == null)
            {
                DateTime? drawdown = normalLoanEntryM.Drawdown;
                if (drawdown == null)
                    drawdown = normalLoanEntryM.ValueDate;
                periosStartDate = ((DateTime)drawdown).AddMonths(freg);
            }

            if (numberOfPerios == 0)
            {
                DateTime startDate = (DateTime)normalLoanEntryM.ValueDate;
                DateTime endDate = (DateTime)normalLoanEntryM.MaturityDate;
                //int durationLoan = endDate.Subtract(startDate).Days;
                int durationMonthLoan = ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month;
                int fregV = freg * 30;



                if (freg > 0)
                {
                    numberOfPerios = durationMonthLoan / freg;
                }

            }

            if (instalmant == 0)
            {
                if (numberOfPerios > 0)
                {

                    instalmant = (Int32)((getCurrentLoanAmount(normalLoanEntryM, replaymentTimes)) / numberOfPerios);

                }


            }

            if (numberOfPerios > 0 && (it.No == null || it.No == 0))
            {
                numberOfPerios--;
            }

            it = facade.FindLoanControl(normalLoanEntryM.Code, replaymentTimes, "EP").FirstOrDefault();
            if (it != null)
            {
                instalmantEnd = it.AmountAction == null ? 0 : (decimal)it.AmountAction;
                periosEndDate = it.Date;
            }

            if (instalmantEnd == 0)
            {
                instalmantEnd = getCurrentLoanAmount(normalLoanEntryM, replaymentTimes) - numberOfPerios * instalmant;
            }

            if (periosEndDate == null)
            {
                periosEndDate = normalLoanEntryM.MaturityDate;
            }


        }

        protected void updateNormalLoanRepayment(BNEWNORMALLOAN loan, int repaymentTimes, decimal newAmount, DateTime? activateDate)
        {
            NormalLoanRepaymentRepository facade = new NormalLoanRepaymentRepository();
            BNEWNORMALLOAN_REPAYMENT existLoanRepay = facade.FindRepaymentAmount(loan.Code, repaymentTimes).FirstOrDefault();

            if (existLoanRepay != null)
            {
                BNEWNORMALLOAN_REPAYMENT existLoanRepayOld = facade.FindRepaymentAmount(loan.Code, repaymentTimes).FirstOrDefault();
                existLoanRepay.LoanAmount = newAmount;
                facade.Update(existLoanRepayOld, existLoanRepay);
            }
            else
            {
                existLoanRepay = new BNEWNORMALLOAN_REPAYMENT();
                existLoanRepay.RepaymentTimes = repaymentTimes;
                existLoanRepay.ActivatedDate = activateDate;
                existLoanRepay.LoanAmount = newAmount;
                existLoanRepay.Code = loan.Code;
                facade.Add(existLoanRepay);
            }
            facade.Commit();
        }
    }
}