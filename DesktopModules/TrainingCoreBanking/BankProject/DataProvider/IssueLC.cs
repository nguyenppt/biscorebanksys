﻿using System.Data;
namespace BankProject.DataProvider
{
    public static class IssueLC
    {
        private static SqlDataProvider sqldata = new SqlDataProvider();

        public static void ImportDocumentProcessingReject(string LCCcode, string UserExecute, string RejectStatus, string RejectDrawType)
        {
            sqldata.ndkExecuteNonQuery("P_ImportDocumentProcessingReject", LCCcode, UserExecute, RejectStatus, RejectDrawType);
        }

        public static DataTable GetDocForPayment(string DocCode)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCGetDocForPayment", DocCode).Tables[0];
        }

        public static DataTable GetDepositAccount(string CustomerID, string Currency)
        {
            return sqldata.ndkExecuteDataset("P_DepositAccount", CustomerID, Currency).Tables[0];
        }

        public static DataTable PaymentMethod()
        {
            return sqldata.ndkExecuteDataset("P_PaymentMethod").Tables[0];
        }

        public static DataSet ImportLCPaymentDetail(string LCCode, long? PaymentId)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCPaymentDetail", LCCode, PaymentId);
        }

        public static DataTable ImportLCPaymentUpdate(long PaymentId, string LCCode, string DrawType, double? DrawingAmount, string Currency, string DepositAccount, double? ExchangeRate, double? AmtDRFrAcctCcy, double? ProvAmtRelease,
                string ProvCoverAcct, double? ProvExchangeRate, double? CoverAmount, string PaymentMethod, string NostroAcct, double? AmountCredited, string PaymentRemarks, string FullyUtilised, string WaiveCharges,
                string ChargeRemarks, string VATNo, string UserExecute)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCPaymentUpdate", PaymentId, LCCode, DrawType, DrawingAmount, Currency, DepositAccount, ExchangeRate, AmtDRFrAcctCcy, ProvAmtRelease,
                ProvCoverAcct, ProvExchangeRate, CoverAmount, PaymentMethod, NostroAcct, AmountCredited, PaymentRemarks, FullyUtilised, 
                WaiveCharges, ChargeRemarks, VATNo, UserExecute).Tables[0];
        }

        public static void ImportLCPaymentChargeUpdate(long PaymentId, string ChargeTab, string ChargeCode, string ChargeAcct, string ChargeCcy, double? ExchangeRate, double? ChargeAmt, string PartyCharged, string AmortCharge, 
            string ChargeStatus, string TaxCode, double? TaxAmt)
        {
            sqldata.ndkExecuteNonQuery("P_ImportLCPaymentChargeUpdate", PaymentId, ChargeTab, ChargeCode, ChargeAcct, ChargeCcy, ExchangeRate, ChargeAmt, PartyCharged, AmortCharge, ChargeStatus, TaxCode, TaxAmt);
        }

        public static void ImportLCPaymentUpdateStatus(long PaymentId, string NewStatus, string UserExecute)
        {
            sqldata.ndkExecuteNonQuery("P_ImportLCPaymentUpdateStatus", PaymentId, NewStatus, UserExecute);
        }

        public static DataSet ImportLCPaymentReport(int ReportType, long PaymentId, string UserId)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCPaymentReport", ReportType, PaymentId, UserId);
        }

        public static DataTable ImportLCPaymentList(string Status)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCPaymentList", Status).Tables[0];
        }

        public static string GetVatNo()
        {
            DataTable tDetail = Database.B_BMACODE_GetNewSoTT("VATNO").Tables[0];
            if (tDetail == null || tDetail.Rows.Count <= 0) return null;

            return tDetail.Rows[0]["SoTT"].ToString();
        }

        public static DataSet ImportLCDocsProcessDetail(string LCCode, string DocCode)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCDocsProcessDetail", LCCode, DocCode);
        }

        public static DataTable ImportLCDetailForDocProcess(string LCCode)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCDetailForDocProcess", LCCode).Tables[0];
        }

        public static DataTable ImportLCDocsList(string Status, int TabId)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCDocsList", Status, TabId).Tables[0];
        }

        public static DataTable ImportLCIsValidToClose(string LCCode)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCIsValidToClose", LCCode).Tables[0];
        }

        public static void ImportLCClose(string UserExecute, string LCCode, string Status)
        {
            ImportLCClose(UserExecute, LCCode, Status, null, null, null);
        }
        public static void ImportLCClose(string UserExecute, string LCCode, string Status, string GenerateDelivery, string ExternalReference, string Remark)
        {
            sqldata.ndkExecuteNonQuery("P_ImportLCClose", UserExecute, LCCode, Status, GenerateDelivery, ExternalReference, Remark);
        }

        public static DataSet ImportLCDocumentReport(int ReportType, string PaymentId, string UserId)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCDocumenyReport", ReportType, PaymentId, UserId);
        }
    }
}