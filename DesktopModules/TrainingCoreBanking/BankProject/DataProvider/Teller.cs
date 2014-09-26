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
        //
        public static DataTable InternalBankAccount()
        {
            return sqldata.ndkExecuteDataset("P_BINTERNALBANKACCOUNT").Tables[0];
        }
        public static DataTable InternalBankPaymentAccount(string AccountPrefix)
        {
            return sqldata.ndkExecuteDataset("P_BINTERNALBANKPAYMENTACCOUNT", AccountPrefix).Tables[0];
        }

        public static DataTable ExchangeRate()
        {
            return Database.ExchangeRate();
        }

        public static void InsertCashWithrawalForBuyingTC(string TransID, string Account, string Currency, double? ExchangeRate, double? AmtLCY, double? AmtFCY, string CurrencyPaid, double? DealRate, double? AmtPaidToCust, string TellerID, string WaiveCharges, string Narrative, string UserCreate)
        {
            sqldata.ndkExecuteNonQuery("P_CashWithrawalForBuyingTCUpdate", TransID, Account, Currency, ExchangeRate, AmtLCY, AmtFCY, CurrencyPaid, DealRate, AmtPaidToCust, TellerID, WaiveCharges, Narrative, UserCreate);
        }

        public static DataTable CashWithrawalForBuyingTCList(string Status, string CustomerID, string CustomerName)
        {
            return sqldata.ndkExecuteDataset("P_CashWithrawalForBuyingTCDetailOrList", Status, null, CustomerID, CustomerName).Tables[0];
        }

        public static DataTable CashWithrawalForBuyingTCDetail(string TransID)
        {
            return sqldata.ndkExecuteDataset("P_CashWithrawalForBuyingTCDetailOrList", null, TransID, null, null).Tables[0];
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
        public static DataTable SellTravellersChequeDetailOrList(string Status)
        {
            return SellTravellersChequeDetailOrList(null, Status, null, null, null);
        }
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
        //
        public static DataTable TellerForeignExchangeList(int TabId, string Status)
        {
            return sqldata.ndkExecuteDataset("P_TellerForeignExchangeList", TabId, Status).Tables[0];
        }
        //
        public static DataTable TellerForeignExchangeIssuer(int TabId)
        {
            DataTable tbList = new DataTable();
            tbList.Columns.Add(new DataColumn("Text", typeof(string)));
            tbList.Columns.Add(new DataColumn("Value", typeof(string)));
            //
            DataRow dr = tbList.NewRow();
            dr["Value"] = "AMEX";
            dr["Text"] = dr["Value"];
            tbList.Rows.Add(dr);
            //
            dr = tbList.NewRow();
            dr["Value"] = "CITI CORP";
            dr["Text"] = dr["Value"];
            tbList.Rows.Add(dr);
            //
            dr = tbList.NewRow();
            dr["Value"] = "MASTER CARD";
            dr["Text"] = dr["Value"];
            tbList.Rows.Add(dr);
            //
            dr = tbList.NewRow();
            dr["Value"] = "THOMAS COOK";
            dr["Text"] = dr["Value"];
            tbList.Rows.Add(dr);
            //
            dr = tbList.NewRow();
            dr["Value"] = "VISA";
            dr["Text"] = dr["Value"];
            tbList.Rows.Add(dr);

            return tbList;
        }
        //
        public static void BuyTravellersChequeUpdate(string Command, string TTNo, string CustomerName, string CustomerAddress, string CustomerPassportNo, string CustomerPassportDateOfIssue, string CustomerPassportPlaceOfIssue, string CustomerPhoneNo, string TellerID, string TCCurrency, string DrAccount, double? TCAmount, string CurrencyPaid, string CrTellerID, string CrAccount, double? ExchangeRate, double? ChargeAmtLCY, double? ChargeAmtFCY, double? AmountPaid, string Narrative, string TCIssuer, string Denomination, string Unit, string SerialNo, string UserExecute)
        {
            sqldata.ndkExecuteNonQuery("P_BuyTravellersChequeUpdate", Command, TTNo, CustomerName, CustomerAddress, CustomerPassportNo, CustomerPassportDateOfIssue , CustomerPassportPlaceOfIssue , CustomerPhoneNo , TellerID, TCCurrency, DrAccount , TCAmount, CurrencyPaid, CrTellerID, CrAccount , ExchangeRate , ChargeAmtLCY , ChargeAmtFCY , AmountPaid , Narrative , TCIssuer, Denomination, Unit, SerialNo, UserExecute);
        }
        public static DataTable BuyTravellersChequeDetailOrList(string Status)
        {
            return BuyTravellersChequeDetailOrList(null, Status, null, null, null);
        }
        public static DataTable BuyTravellersChequeDetailOrList(string TTNo, string Status)
        {
            return BuyTravellersChequeDetailOrList(TTNo, Status, null, null, null);
        }
        public static DataTable BuyTravellersChequeDetailOrList(string CustomerName, string PassportNo, string PhoneNo)
        {
            return BuyTravellersChequeDetailOrList(null, null, CustomerName, PassportNo, PhoneNo);
        }
        private static DataTable BuyTravellersChequeDetailOrList(string TTNo, string Status, string CustomerName, string PassportNo, string PhoneNo)
        {
            return sqldata.ndkExecuteDataset("P_BuyTravellersChequeDetailOrList", TTNo, Status, CustomerName, PassportNo, PhoneNo).Tables[0];
        }
        public static void BuyTravellersChequeUpdateStatus(string TTNo, string Status, string UserUpdate)
        {
            sqldata.ndkExecuteNonQuery("P_BuyTravellersChequeUpdateStatus", TTNo, Status, UserUpdate);
        }
        //
        public static void ForeignExchangeUpdate(string Command, string TTNo, string CustomerName, string CustomerAddress, string CustomerPassportNo, 
            string CustomerPassportDateOfIssue, string CustomerPassportPlaceOfIssue, string CustomerPhoneNo, string TellerID, string DebitCurrency, 
            string DebitAccount, double? DebitAmtLCY, double? DebitAmtFCY, string CurrencyPaid, string CrTellerID, string CreditAccount, double? DealRate, 
            double? AmountPaid, string Narrative, string UserExecute)
        {
            sqldata.ndkExecuteNonQuery("P_ForeignExchangeUpdate", Command, TTNo, CustomerName, CustomerAddress, CustomerPassportNo, CustomerPassportDateOfIssue,
            CustomerPassportPlaceOfIssue, CustomerPhoneNo, TellerID, DebitCurrency, DebitAccount, DebitAmtLCY, DebitAmtFCY,
            CurrencyPaid, CrTellerID, CreditAccount, DealRate, AmountPaid, Narrative, UserExecute);
        }
        public static DataTable ForeignExchangeDetailOrList(string Status)
        {
            return ForeignExchangeDetailOrList(null, Status, null, null, null);
        }
        public static DataTable ForeignExchangeDetailOrList(string TTNo, string Status)
        {
            return ForeignExchangeDetailOrList(TTNo, Status, null, null, null);
        }
        public static DataTable ForeignExchangeDetailOrList(string CustomerName, string PassportNo, string PhoneNo)
        {
            return ForeignExchangeDetailOrList(null, null, CustomerName, PassportNo, PhoneNo);
        }
        private static DataTable ForeignExchangeDetailOrList(string TTNo, string Status, string CustomerName, string PassportNo, string PhoneNo)
        {
            return sqldata.ndkExecuteDataset("P_ForeignExchangeDetailOrList", TTNo, Status, CustomerName, PassportNo, PhoneNo).Tables[0];
        }
        public static void ForeignExchangeUpdateStatus(string TTNo, string Status, string UserUpdate)
        {
            sqldata.ndkExecuteNonQuery("P_ForeignExchangeUpdateStatus", TTNo, Status, UserUpdate);
        }
        //
        public static void WUXOOMCashAdvanceUpdate(string Command, string TTNo, string CustomerName, string CustomerAddress, string CustomerPassportNo,
            string CustomerPassportDateOfIssue, string CustomerPassportPlaceOfIssue, string CustomerPhoneNo, string TellerID, string DebitCurrency,
            string DebitAccount, double? DebitAmtLCY, double? DebitAmtFCY, string CreditCurrency, string CrTellerID, string CreditAccount, double? DealRate,
            double? AmountPaid, string Narrative, string UserExecute)
        {
            sqldata.ndkExecuteNonQuery("P_WUXOOMCashAdvanceUpdate", Command, TTNo, CustomerName, CustomerAddress, CustomerPassportNo, CustomerPassportDateOfIssue,
            CustomerPassportPlaceOfIssue, CustomerPhoneNo, TellerID, DebitCurrency, DebitAccount, DebitAmtLCY, DebitAmtFCY,
            CreditCurrency, CrTellerID, CreditAccount, DealRate, AmountPaid, Narrative, UserExecute);
        }
        public static DataTable WUXOOMCashAdvanceDetailOrList(string Status)
        {
            return WUXOOMCashAdvanceDetailOrList(null, Status, null, null, null);
        }
        public static DataTable WUXOOMCashAdvanceDetailOrList(string TTNo, string Status)
        {
            return WUXOOMCashAdvanceDetailOrList(TTNo, Status, null, null, null);
        }
        public static DataTable WUXOOMCashAdvanceDetailOrList(string CustomerName, string PassportNo, string PhoneNo)
        {
            return WUXOOMCashAdvanceDetailOrList(null, null, CustomerName, PassportNo, PhoneNo);
        }
        private static DataTable WUXOOMCashAdvanceDetailOrList(string TTNo, string Status, string CustomerName, string PassportNo, string PhoneNo)
        {
            return sqldata.ndkExecuteDataset("P_WUXOOMCashAdvanceDetailOrList", TTNo, Status, CustomerName, PassportNo, PhoneNo).Tables[0];
        }
        public static void WUXOOMCashAdvanceUpdateStatus(string TTNo, string Status, string UserUpdate)
        {
            sqldata.ndkExecuteNonQuery("P_WUXOOMCashAdvanceUpdateStatus", TTNo, Status, UserUpdate);
        }
        //
        public static void ExchangeBanknotesManyDenoUpdate(string Command, string TTNo, string CustomerName, string CustomerAddress, string CustomerPassportNo,
            string CustomerPassportDateOfIssue, string CustomerPassportPlaceOfIssue, string CustomerPhoneNo, string TellerID, string DebitCurrency,
            string DebitAccount, double? DebitAmount, string Narrative, DateTime? ValueDate, string CrTellerID, string CreditAccount, double? ExchangeRate,
            double? AmountPaid, string DenominationNum, string DenominationUnit, string DenominationRate, string UserExecute)
        {
            sqldata.ndkExecuteNonQuery("P_ExchangeBanknotesManyDenoUpdate", Command, TTNo, CustomerName, CustomerAddress, CustomerPassportNo, CustomerPassportDateOfIssue,
            CustomerPassportPlaceOfIssue, CustomerPhoneNo, TellerID, DebitCurrency, DebitAccount, DebitAmount, Narrative, ValueDate,
            CrTellerID, CreditAccount, ExchangeRate, AmountPaid, DenominationNum, DenominationUnit, DenominationRate, UserExecute);
        }
        public static DataTable ExchangeBanknotesManyDenoDetailOrList(string Status)
        {
            return ExchangeBanknotesManyDenoDetailOrList(null, Status, null, null, null);
        }
        public static DataTable ExchangeBanknotesManyDenoDetailOrList(string TTNo, string Status)
        {
            return ExchangeBanknotesManyDenoDetailOrList(TTNo, Status, null, null, null);
        }
        public static DataTable ExchangeBanknotesManyDenoDetailOrList(string CustomerName, string PassportNo, string PhoneNo)
        {
            return ExchangeBanknotesManyDenoDetailOrList(null, null, CustomerName, PassportNo, PhoneNo);
        }
        private static DataTable ExchangeBanknotesManyDenoDetailOrList(string TTNo, string Status, string CustomerName, string PassportNo, string PhoneNo)
        {
            return sqldata.ndkExecuteDataset("P_ExchangeBanknotesManyDenoDetailOrList", TTNo, Status, CustomerName, PassportNo, PhoneNo).Tables[0];
        }
        public static void ExchangeBanknotesManyDenoUpdateStatus(string TTNo, string Status, string UserUpdate)
        {
            sqldata.ndkExecuteNonQuery("P_ExchangeBanknotesManyDenoUpdateStatus", TTNo, Status, UserUpdate);
        }
    }
}