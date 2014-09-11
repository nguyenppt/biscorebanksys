using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BankProject.DataProvider
{
    public static class Teller
    {
        private static SqlDataProvider sqldata = new SqlDataProvider();

        public static string GenerateTTId()
        {
            return BankProject.DataProvider.SQLData.B_BMACODE_GetNewID("FOREIGNEXCHANGE", "TT", ".");
        }

        public static DataTable AccountForBuyingTC()
        {
            return AccountForBuyingTC(null);
        }
        public static DataTable AccountForBuyingTC(string AccountNo)
        {
            return sqldata.ndkExecuteDataset("P_CashWithrawalForBuyingTCAccounts", AccountNo).Tables[0];
        }

        public static DataTable ExchangeRate()
        {
            return sqldata.ndkExecuteDataset("P_ExchangeRate").Tables[0];
        }

        public static void InsertCashWithrawalForBuyingTC(string TransID, string Account, string Currency, double? ExchangeRate, double? AmtLCY, double? AmtFCY, string CurrencyPaid, double? DealRate, double? AmtPaidToCust, string TellerID, string WaiveCharges, string Narrative, string UserCreate)
        {
            sqldata.ndkExecuteNonQuery("P_CashWithrawalForBuyingTCUpdate", TransID, Account, Currency, ExchangeRate, AmtLCY, AmtFCY, CurrencyPaid, DealRate, AmtPaidToCust, TellerID, WaiveCharges, Narrative, UserCreate);
        }

        public static DataTable CashWithrawalForBuyingTCList(string CustomerID, string CustomerName)
        {
            return sqldata.ndkExecuteDataset("P_CashWithrawalForBuyingTCDetailOrList", null, CustomerID, CustomerName).Tables[0];
        }

        public static DataTable CashWithrawalForBuyingTCDetail(string TransID)
        {
            return sqldata.ndkExecuteDataset("P_CashWithrawalForBuyingTCDetailOrList", TransID, null, null).Tables[0];
        }

        public static void UpdateCashWithrawalForBuyingTC(string TransID, string Status)
        {
            sqldata.ndkExecuteNonQuery("P_CashWithrawalForBuyingTCUpdateStatus", TransID, Status);
        }
        //add new & edit
        public static void SellTravellersChequeUpdate(string Command, string TTNo, string CustomerName, string CustomerAddress,	string CustomerPassportNo,	string CustomerPassportDateOfIssue, string CustomerPassportPlaceOfIssue, string CustomerPhoneNo,
            string TellerID, string TCCurrency, string DebitAccount, double? TCAmount, string DrCurrency, string CrAccount, double? AmountDebited, double? ExchangeRate, string Narrative, string UserCreate)
        {
            sqldata.ndkExecuteNonQuery("P_SellTravellersChequeUpdate", Command, TTNo, CustomerName, CustomerAddress,	CustomerPassportNo,	CustomerPassportDateOfIssue,
			CustomerPassportPlaceOfIssue, CustomerPhoneNo, TellerID, TCCurrency, DebitAccount, TCAmount, DrCurrency, CrAccount,
			AmountDebited, ExchangeRate, Narrative, UserCreate);
        }
        //get detail or list
        public static DataTable SellTravellersChequeDetailOrList(string TTNo, string Status)
        {
            return SellTravellersChequeDetailOrList(TTNo, Status, null, null, null);
        }
        public static DataTable SellTravellersChequeDetailOrList(string CustomerName, string PassportNo, string PhoneNo)
        {
            return SellTravellersChequeDetailOrList(null, null, CustomerName, PassportNo, PhoneNo);
        }
        private static DataTable SellTravellersChequeDetailOrList(string TTNo, string Status, string CustomerName, string PassportNo, string PhoneNo)
        {
            return sqldata.ndkExecuteDataset("P_SellTravellersChequeDetailOrList", TTNo, Status, CustomerName, PassportNo, PhoneNo).Tables[0];
        }
        //update status
        public static void SellTravellersChequeUpdateStatus(string TTNo, string Status, string UserUpdate)
        {
            sqldata.ndkExecuteNonQuery("P_SellTravellersChequeUpdateStatus", TTNo, Status, UserUpdate);
        }
    }
}