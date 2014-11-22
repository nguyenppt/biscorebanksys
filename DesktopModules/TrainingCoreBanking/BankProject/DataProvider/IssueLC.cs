using System.Data;
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

        public static void ImportLCPaymentMT756Update(long PaymentId, string PaymentCode, string General, string SendingBankTRN, string RelatedReference, double? AmountCollected, System.DateTime? ValueDate, 
				string Currency, double? Amount, string SenderCorrespondent1, string SenderCorrespondent2, string ReceiverCorrespondent1, string ReceiverCorrespondent2, 
				string DetailOfCharges1, string DetailOfCharges2,string ReceiverCorrespondentType,string ReceiverCorrespondentNo,string ReceiverCorrespondentName,string 
				ReceiverCorrespondentAddr1,string ReceiverCorrespondentAddr2,string ReceiverCorrespondentAddr3,string SenderCorrespondentType,string 
				SenderCorrespondentNo,string SenderCorrespondentName,string SenderCorrespondentAddr1,string SenderCorrespondentAddr2,string SenderCorrespondentAddr3,string 
				SenderToReceiverInformation1,string SenderToReceiverInformation2,string SenderToReceiverInformation3,string DetailOfCharges3)
        {
            sqldata.ndkExecuteNonQuery("P_ImportLCPaymentMT756Update", PaymentId, PaymentCode, General, SendingBankTRN, RelatedReference, AmountCollected, ValueDate,
                Currency, Amount, SenderCorrespondent1, SenderCorrespondent2, ReceiverCorrespondent1, ReceiverCorrespondent2,
                DetailOfCharges1, DetailOfCharges2, ReceiverCorrespondentType, ReceiverCorrespondentNo, ReceiverCorrespondentName,
                ReceiverCorrespondentAddr1, ReceiverCorrespondentAddr2, ReceiverCorrespondentAddr3, SenderCorrespondentType,
                SenderCorrespondentNo, SenderCorrespondentName, SenderCorrespondentAddr1, SenderCorrespondentAddr2, SenderCorrespondentAddr3,
                SenderToReceiverInformation1, SenderToReceiverInformation2, SenderToReceiverInformation3, DetailOfCharges3);
        }
        public static void ImportLCPaymentMT202Update(long PaymentId, string PaymentCode, string TransactionReferenceNumber, string RelatedReference, System.DateTime? ValueDate, string Currency, double? 
				Amount,string OrderingInstitution,string SenderCorrespondent1,string SenderCorrespondent2,string ReceiverCorrespondent1,string ReceiverCorrespondent2,string 
				IntermediaryBank,string AccountWithInstitution,string BeneficiaryBank,string SenderToReceiverInformation,string IntermediaryBankType,string IntermediaryBankName,string 
				IntermediaryBankAddr1,string IntermediaryBankAddr2,string IntermediaryBankAddr3,string AccountWithInstitutionType,string AccountWithInstitutionName,string 
				AccountWithInstitutionAddr1,string AccountWithInstitutionAddr2,string AccountWithInstitutionAddr3,string BeneficiaryBankType,string BeneficiaryBankName,string 
				BeneficiaryBankAddr1,string BeneficiaryBankAddr2,string BeneficiaryBankAddr3,string SenderToReceiverInformation2,string SenderToReceiverInformation3)
        {
            sqldata.ndkExecuteNonQuery("P_ImportLCPaymentMT202Update", PaymentId, PaymentCode, TransactionReferenceNumber, RelatedReference, ValueDate, Currency,
                Amount, OrderingInstitution, SenderCorrespondent1, SenderCorrespondent2, ReceiverCorrespondent1, ReceiverCorrespondent2,
                IntermediaryBank, AccountWithInstitution, BeneficiaryBank, SenderToReceiverInformation, IntermediaryBankType, IntermediaryBankName,
                IntermediaryBankAddr1, IntermediaryBankAddr2, IntermediaryBankAddr3, AccountWithInstitutionType, AccountWithInstitutionName,
                AccountWithInstitutionAddr1, AccountWithInstitutionAddr2, AccountWithInstitutionAddr3, BeneficiaryBankType, BeneficiaryBankName,
                BeneficiaryBankAddr1, BeneficiaryBankAddr2, BeneficiaryBankAddr3, SenderToReceiverInformation2, SenderToReceiverInformation3);
        }

        public static DataSet ExportLCPaymentReport(int ReportType, string PaymentId, string UserId)
        {
            return sqldata.ndkExecuteDataset("P_ExportLCPaymentReport", ReportType, PaymentId, UserId);
        }

        public static DataSet ImportLCDocsProcessDetail4Amend(string DocCode)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCDocsProcessDetail4Amend", DocCode);
        }

        public static DataSet ImportLCPaymentChargeAcc(string PartyCharged, string CustomerID, string Currency)
        {
            return sqldata.ndkExecuteDataset("P_ImportLCPaymentChargeAcc", PartyCharged, CustomerID, Currency);
        }
    }
}