using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BankProject.Business
{
    public class LoanContractScheduleDS
    {
        DataTable dateReport = new DataTable("DateInfor");

        public DataTable DateReport
        {
            get { return dateReport; }
            set { dateReport = value; }
        }
        DataTable dtInfor = new DataTable("Info");

        public DataTable DtInfor
        {
            get { return dtInfor; }
            set { dtInfor = value; }
        }
        DataTable dtItems = new DataTable("Items");

        public DataTable DtItems
        {
            get { return dtItems; }
            set { dtItems = value; }
        }

        DataColumn cl_code = new DataColumn("Code");

        public DataColumn Cl_code
        {
            get { return cl_code; }
            set { cl_code = value; }
        }
        DataColumn cl_cust = new DataColumn("Customer");

        public DataColumn Cl_cust
        {
            get { return cl_cust; }
            set { cl_cust = value; }
        }
        DataColumn cl_custID = new DataColumn("CustomerID");

        public DataColumn Cl_custID
        {
            get { return cl_custID; }
            set { cl_custID = value; }
        }


        DataColumn cl_loadAmount = new DataColumn("LoanAmount", Type.GetType("System.Decimal"));

        public DataColumn Cl_loadAmount
        {
            get { return cl_loadAmount; }
            set { cl_loadAmount = value; }
        }

        DataColumn cl_loadAmountRepayment = new DataColumn("LoadAmountRepayment", Type.GetType("System.Decimal"));

        public DataColumn Cl_loadAmountRepayment
        {
            get { return cl_loadAmountRepayment; }
            set { cl_loadAmountRepayment = value; }
        }

        DataColumn cl_drawdown = new DataColumn("Drawdown", Type.GetType("System.DateTime"));

        public DataColumn Cl_drawdown
        {
            get { return cl_drawdown; }
            set { cl_drawdown = value; }
        }
        DataColumn cl_interestKey = new DataColumn("InterestKey");

        public DataColumn Cl_interestKey
        {
            get { return cl_interestKey; }
            set { cl_interestKey = value; }
        }
        DataColumn cl_freq = new DataColumn("Freq");

        public DataColumn Cl_freq
        {
            get { return cl_freq; }
            set { cl_freq = value; }
        }
        DataColumn cl_interest = new DataColumn("interest", Type.GetType("System.Decimal"));

        public DataColumn Cl_interest
        {
            get { return cl_interest; }
            set { cl_interest = value; }
        }

        DataColumn cl_perious = new DataColumn("Perios", Type.GetType("System.Int16"));

        public DataColumn Cl_perious
        {
            get { return cl_perious; }
            set { cl_perious = value; }
        }
        DataColumn cl_dueDate = new DataColumn("DueDate", Type.GetType("System.DateTime"));

        public DataColumn Cl_dueDate
        {
            get { return cl_dueDate; }
            set { cl_dueDate = value; }
        }
        DataColumn cl_principle = new DataColumn("Principle", Type.GetType("System.Decimal"));

        public DataColumn Cl_principle
        {
            get { return cl_principle; }
            set { cl_principle = value; }
        }
        DataColumn cl_interestAmount = new DataColumn("InterestAmount", Type.GetType("System.Decimal"));

        public DataColumn Cl_interestAmount
        {
            get { return cl_interestAmount; }
            set { cl_interestAmount = value; }
        }
        DataColumn cl_PrintOs = new DataColumn("PrinOS", Type.GetType("System.Decimal"));

        public DataColumn Cl_PrintOs
        {
            get { return cl_PrintOs; }
            set { cl_PrintOs = value; }
        }

        DataColumn cl_PrintOSPlan = new DataColumn("PrinOSPlan", Type.GetType("System.Decimal"));

        public DataColumn Cl_PrintOSPlan
        {
            get { return cl_PrintOSPlan; }
            set { cl_PrintOSPlan = value; }
        }

        DataColumn cl_DisbursalAmount = new DataColumn("DisbursalAmount", Type.GetType("System.Decimal"));

        public DataColumn Cl_DisbursalAmount
        {
            get { return cl_DisbursalAmount; }
            set { cl_DisbursalAmount = value; }
        }

        DataColumn cl_isInterestedRow = new DataColumn("IntestedRow", Type.GetType("System.Boolean"));

        public DataColumn Cl_isInterestedRow
        {
            get { return cl_isInterestedRow; }
            set { cl_isInterestedRow = value; }
        }

        DataColumn cl_isPaymentRow = new DataColumn("PaymentRow", Type.GetType("System.Boolean"));

        public DataColumn Cl_isPaymentRow
        {
            get { return cl_isPaymentRow; }
            set { cl_isPaymentRow = value; }
        }

        DataColumn cl_isDisbursalRow = new DataColumn("DisbursalRow", Type.GetType("System.Boolean"));

        public DataColumn Cl_isDisbursalRow
        {
            get { return cl_isDisbursalRow; }
            set { cl_isDisbursalRow = value; }
        }

        DataColumn cl_isPeriodicAutomaticRow = new DataColumn("PeriodicAutomaticRow", Type.GetType("System.Boolean"));

        public DataColumn Cl_isPeriodicAutomaticRow
        {
            get { return cl_isPeriodicAutomaticRow; }
            set { cl_isPeriodicAutomaticRow = value; }
        }

        DataColumn cl_durationDate = new DataColumn("duration_day");

        public DataColumn Cl_durationDate
        {
            get { return cl_durationDate; }
            set { cl_durationDate = value; }
        }




        public LoanContractScheduleDS()
        {
            dtInfor.Columns.Add(cl_code);
            dtInfor.Columns.Add(cl_cust);
            dtInfor.Columns.Add(cl_custID);
            dtInfor.Columns.Add(cl_loadAmount);
            dtInfor.Columns.Add(cl_loadAmountRepayment);
            dtInfor.Columns.Add(cl_drawdown);
            dtInfor.Columns.Add(cl_interestKey);
            dtInfor.Columns.Add(cl_freq);
            dtInfor.Columns.Add(cl_interest);

            dtItems.Columns.Add(cl_perious);
            dtItems.Columns.Add(cl_dueDate);
            dtItems.Columns.Add(cl_principle);
            dtItems.Columns.Add(cl_interestAmount);
            dtItems.Columns.Add(cl_PrintOs);
            dtItems.Columns.Add(cl_PrintOSPlan);
            dtItems.Columns.Add(cl_isPaymentRow);
            dtItems.Columns.Add(cl_isInterestedRow);
            dtItems.Columns.Add(cl_isDisbursalRow);
            dtItems.Columns.Add(cl_isPeriodicAutomaticRow);
            dtItems.Columns.Add(cl_durationDate);
            dtItems.Columns.Add(cl_DisbursalAmount);


            dateReport.Columns.Add("day");
            dateReport.Columns.Add("month");
            dateReport.Columns.Add("year");
            var rowD = dateReport.NewRow();
            DateTime today = DateTime.Today;
            rowD["day"] = today.ToString("dd");
            rowD["month"] = today.ToString("MM");
            rowD["year"] = today.ToString("yyyy");
            dateReport.Rows.Add(rowD);


        }

    }
}