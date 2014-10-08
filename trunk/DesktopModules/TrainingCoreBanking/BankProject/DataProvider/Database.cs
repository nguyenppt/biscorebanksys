using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BankProject.DataProvider
{
    public class Database
    {
        static SqlDataProvider sqldata = new SqlDataProvider();
        public static DataSet B_BACCOUNTS_Insert(string CustomerID, string CategoryCode, string Currentcy, string AccountName, string ShortName, string AccountMnemonic
            , string ProductLine, string JointHolderID, string RelationCode, string Notes, string Override, string RecordStatus, string CurrNo, string Inputter,
            string DateTime, string DateTime2, string Authoriser, string CoCode, string DeptCode, string AuditorCode, string DepositCode)
        {
            return sqldata.ndkExecuteDataset("B_BACCOUNTS_Insert", CustomerID, CategoryCode, Currentcy, AccountName, ShortName, AccountMnemonic
            , ProductLine, JointHolderID, RelationCode, Notes, Override, RecordStatus, CurrNo, Inputter,
            DateTime, DateTime2, Authoriser, CoCode, DeptCode, AuditorCode, DepositCode);
        }
        public static DataSet B_BMACODE_GetNewSoTT(string MaCode)
        {
            return sqldata.ndkExecuteDataset("B_BMACODE_GetNewSoTT", MaCode);
        }
        public static DataSet BSECTOR_GetByCode(string MaCode)
        {
            return sqldata.ndkExecuteDataset("BSECTOR_GetByCode", MaCode);
        }
        public static DataSet BSECTOR_GetAll()
        {
            return sqldata.ndkExecuteDataset("BSECTOR_GetAll");
        }
        public static DataSet BINDUSTRY_GetByCoe(string MaCode)
        {
            return sqldata.ndkExecuteDataset("BINDUSTRY_GetByCode", MaCode);
        }
        public static DataSet BINDUSTRY_GetAll()
        {
            return sqldata.ndkExecuteDataset("BINDUSTRY_GetAll");
        }
        public static DataSet B_BNORMAILLCPROVITIONTRANSFER_GetByNormalLCCode(string NormalLCCode)
        {
            return sqldata.ndkExecuteDataset("B_BNORMAILLCPROVITIONTRANSFER_GetByNormalLCCode", NormalLCCode);
        }
        public static DataSet B_BACCOUNTS_GetbyID(string DepositCode)
        {
            return sqldata.ndkExecuteDataset("B_BACCOUNTS_GetbyID", DepositCode);
        }
        public static DataSet B_BNORMAILLC_GetbyStatus(string Status,string UserID)
        {
            return sqldata.ndkExecuteDataset("B_BNORMAILLC_GetbyStatus", Status, UserID);
        }
        public static DataSet B_BNORMAILLC_GetbyNormalLCCode(string NormalLCCode)
        {
            return sqldata.ndkExecuteDataset("B_BNORMAILLC_GetbyNormalLCCode", NormalLCCode);
        }
        public static DataSet BOPENACCOUNT_LOANACCOUNT_GetByCode(string CustomerName, string currency)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_LOANACCOUNT_GetByCode", CustomerName, currency);
        }
        //ve sau se doi customerName thanh customerID
        public static DataSet B_BDRFROMACCOUNT_GetByCustomer(string CustomerName, string currency)
        {
            return sqldata.ndkExecuteDataset("B_BDRFROMACCOUNT_GetByCustomer", CustomerName, currency);
        }

        public static DataSet B_BCRFROMACCOUNT_GetByCustomer(string CustomerName, string currency)
        {
            return sqldata.ndkExecuteDataset("B_BCRFROMACCOUNT_GetByCustomer", CustomerName, currency);
        }

        public static DataSet BENCOM_GetALL()
        {
            return sqldata.ndkExecuteDataset("BENCOM_GetALL");
        }

        public static DataSet BENCOM_SetCreditAccount(string currency, string BenCom)
        {
            return sqldata.ndkExecuteDataset("BENCOM_SetCreditAccount", currency, BenCom);
        }

        public static DataSet BENCOM_SetCreditAccount_ByProduct(string currency, string BenCom, string product)
        {
            return sqldata.ndkExecuteDataset("BENCOM_SetCreditAccount_ByProduct", currency, BenCom, product);
        }


        public static DataSet BPROVINCE_GetAll()
        {
            return sqldata.ndkExecuteDataset("BPROVINCE_GetAll");
        }

        public static DataSet BBANKCODE_GetByProvince(string Province)
        {
            return sqldata.ndkExecuteDataset("BBANKCODE_GetByProvince", Province);
        }

        public static void BOPENACCOUNT_CalculatorInterestAmount(DateTime? date)
        {
            sqldata.ndkExecuteNonQuery("BOPENACCOUNT_CalculatorInterestAmount", date);
        }

        public static DataSet B_BDRFROMACCOUNT_OtherCustomer(string CustomerName, string currency)
        {
            return sqldata.ndkExecuteDataset("B_BDRFROMACCOUNT_OtherCustomer", CustomerName, currency);
        }

        public static DataSet B_BCRFROMACCOUNT_OtherCustomer(string CustomerName, string currency)
        {
            return sqldata.ndkExecuteDataset("B_BCRFROMACCOUNT_OtherCustomer", CustomerName, currency);
        }
        #region Provision Transfer DC
        public static DataSet PROVISIONTRANSFER_DC_GetbyStatus(string Status, string UserID)
        {
            return sqldata.ndkExecuteDataset("PROVISIONTRANSFER_DC_GetbyStatus", Status, UserID);
        }

        public static void ProvisionTransfer_DC_UpdateStatus(string Status, string NormalLCCode, string userid)
        {
            sqldata.ndkExecuteNonQuery("ProvisionTransfer_DC_UpdateStatus", Status, NormalLCCode, userid);
        }

        public static DataSet ProvisionTransfer_DC_GetByNormalLCCode(string NormalLCCode)
        {
            return sqldata.ndkExecuteDataset("ProvisionTransfer_DC_GetByNormalLCCode", NormalLCCode);
        }

        public static DataSet ProvisionTransfer_DC_GetByLCNo(string LCNo, string Type)
        {
            return sqldata.ndkExecuteDataset("ProvisionTransfer_DC_GetByLCNo", LCNo, Type);
        }

        public static void ProvisionTransfer_DC_Insert(string ProvisionNo, string LCNo, string Orderedby, string DebitRef, string DebitAccount, string DebitCurrency, string DebitAmout
         , string DebitDate, string CreditAccount, string CreditCurrency, string TreasuryRate, string CreditAmount,string CreditDate, string VATSerialNo,string Type, string TypeDescription, string userid
            , string AddRemarks1, string AddRemarks2)
        {
            sqldata.ndkExecuteNonQuery("ProvisionTransfer_DC_Insert",ProvisionNo, LCNo, Orderedby, DebitRef, DebitAccount, DebitCurrency, DebitAmout
            , DebitDate, CreditAccount, CreditCurrency, TreasuryRate, CreditAmount, CreditDate, VATSerialNo, Type, TypeDescription, userid, AddRemarks1, AddRemarks2);
        }
        #endregion

        #region BOPENACCOUNT
        public static DataSet BOPENACCOUNT_Search(string AccountCode, bool Locked, string CustomerType, string CustomerID, string CustomerName, string docid, string category, string currency, string ProductLine)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_Search", AccountCode, Locked, CustomerType, CustomerID, CustomerName, docid, category, currency, ProductLine);
        }
        public static DataSet BOPENACCOUNT_Search_BLocked_Acct(string AccountCode, bool Locked, string CustomerType, string CustomerID, string CustomerName, string docid, string category, string currency, string ProductLine)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_Search_BLocked_Acct", AccountCode, Locked, CustomerType, CustomerID, CustomerName, docid, category, currency, ProductLine);
        }
        public static DataSet BOPENACCOUNT_Enquiry(string AccountCode, bool Locked, bool Close, string CustomerType, string CustomerID, string CustomerName, string docid, string category, string currency, string ProductLine)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_Enquiry", AccountCode, Locked, Close, CustomerType, CustomerID, CustomerName, docid, category, currency, ProductLine);
        }
        public static DataSet BOPENACCOUNT_EnquiryTransaction(string AccountType, string transactiontype, string Refid, string currency, string CustomerType, string CustomerID, string CustomerName, string AccountCode, double? amountFrom, double? amountTo, DateTime? date)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_EnquiryTransaction", AccountType, transactiontype, Refid, currency, CustomerType, CustomerID, CustomerName, AccountCode, amountFrom, amountTo, date);
        }
        public static DataSet BOPENACCOUNT_GetbyStatus(string Status, string AccountStatus, string UserID)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_GetbyStatus", Status, AccountStatus, UserID);
        }

        public static void BOPENACCOUNT_UpdateStatus(string Status, string NormalLCCode, string userid)
        {
            sqldata.ndkExecuteNonQuery("BOPENACCOUNT_UpdateStatus", Status, NormalLCCode, userid);
        }

        public static DataSet BOPENACCOUNT_GetByID(int id)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_GetByID", id);
        }

        public static DataSet BOPENACCOUNT_INTEREST_GetById(int id)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_INTEREST_GetById", id);
        }

        public static DataSet BOPENACCOUNT_INTEREST_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_INTEREST_GetByCode", code);
        }
        public static DataSet BOPENACCOUNT_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_GetByCode", code);
        }

        public static DataSet BOPENACCOUNT_GetByCode_OPEN(string code, string accountType)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_GetByCode_OPEN", code, accountType);
        }

        public static DataSet BOPENACCOUNT_INTERNAL_GetByCode(string accounttype, string customerId, string currency, string DifferCode)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_INTERNAL_GetByCode", accounttype, customerId, currency, DifferCode);
        }

        public static void BOPENACCOUNT_Insert(string AccountCode, string CustomerID, string CustomerType, string CustomerName, string CategoryID, string CategoryName, string Currency, string AccountTitle,
                                    string ShortTitle, string IntCapToAC, string AccountOfficerID, string AccountOfficerName, string ProductLineID, string ProductLineName, string ChargeCode, string ChargeCodeName,
                                    string RestrictTxnID, string RestrictTxnName, string JoinHolderID, string JoinHolderName, string RelationCode, string RelationCodeName, string JoinNotes, string AlternateAct,
                                    string CloseOnline, string CloseMode, string CreditCurrency, string AccountPaid, string Narrative, string Userid, string AccountStatus, string docid,string categorytype)
        {
            sqldata.ndkExecuteNonQuery("BOPENACCOUNT_Insert", AccountCode, CustomerID, CustomerType, CustomerName, CategoryID, CategoryName, Currency, AccountTitle, ShortTitle, IntCapToAC, AccountOfficerID,
                         AccountOfficerName, ProductLineID, ProductLineName, ChargeCode, ChargeCodeName, RestrictTxnID, RestrictTxnName, JoinHolderID, JoinHolderName, RelationCode, RelationCodeName,
                         JoinNotes, AlternateAct, CloseOnline, CloseMode, CreditCurrency, AccountPaid, Narrative, Userid, AccountStatus,docid, categorytype);
        }

        public static DataSet BOPENACCOUNT_KiemTraTK_ThanhToan(string customerid, string currency)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_KiemTraTK_ThanhToan", customerid, currency);
        }
		
        public static DataSet BOPENACCOUNT_Print_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_Print_GetByCode", code);
        }
        #endregion

        #region BOPENACCOUNT_CLOSE
        public static DataSet BOPENACCOUNT_CLOSE_GetbyStatus(string Status, string UserID)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_CLOSE_GetbyStatus", Status, UserID);
        }

        public static void BOPENACCOUNT_CLOSE_UpdateStatus(string Status, string NormalLCCode, string userid)
        {
            sqldata.ndkExecuteNonQuery("BOPENACCOUNT_CLOSE_UpdateStatus", Status, NormalLCCode, userid);
        }

        public static DataSet BOPENACCOUNT_CLOSE_GetByID(int id)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_CLOSE_GetByID", id);
        }

        public static DataSet BOPENACCOUNT_CLOSE_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_CLOSE_GetByCode", code);
        }

        public static void BOPENACCOUNT_CLOSE_Update(string AccountCode, string CloseOnline, string CloseMode, string Close_StandingOrders, string Close_UnclearedEntries,
                                    string Close_ChequesOS, string Close_BankCards, string Close_CCChgsOS, double Close_TotalCreditInterest, double Close_TotalDebitInterest,
                                    double Close_TotalCharges, double Close_TotalVAT, DateTime? Close_DebitDate, string CreditCurrency, string AccountPaid, double Close_CreditAmount, string Narrative)
        {
            sqldata.ndkExecuteNonQuery("BOPENACCOUNT_CLOSE_Update", AccountCode, CloseOnline, CloseMode, Close_StandingOrders, Close_UnclearedEntries, Close_ChequesOS, Close_BankCards, Close_CCChgsOS,
                Close_TotalCreditInterest, Close_TotalDebitInterest, Close_TotalCharges, Close_TotalVAT, Close_DebitDate, CreditCurrency, AccountPaid, Close_CreditAmount, Narrative);
        }
        #endregion

        #region BOPENACCOUNT_BLOCK
        public static DataSet BOPENACCOUNT_BLOCK_GetbyStatus(string Status, string UserID)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_BLOCK_GetbyStatus", Status, UserID);
        }

        public static void BOPENACCOUNT_BLOCK_UpdateStatus(string Status, string NormalLCCode, string userid)
        {
            sqldata.ndkExecuteNonQuery("BOPENACCOUNT_BLOCK_UpdateStatus", Status, NormalLCCode, userid);
        }

        public static DataSet BOPENACCOUNT_BLOCK_GetByID(int id)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_BLOCK_GetByID", id);
        }

        public static DataSet BOPENACCOUNT_BLOCK_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_BLOCK_GetByCode", code);
        }

        public static void BOPENACCOUNT_BLOCK_Update(string AccountCode, double Block_Amount, DateTime? Block_FromDate, DateTime? Block_ToDate, string Block_Description
                    ,bool BlockAccount )
        {
            sqldata.ndkExecuteNonQuery("BOPENACCOUNT_BLOCK_Update", AccountCode, Block_Amount, Block_FromDate, Block_ToDate, Block_Description, BlockAccount);
        }
        #endregion

        #region BOPENACCOUNT_UnBLOCK
        public static DataSet BOPENACCOUNT_UnBLOCK_GetbyStatus(string Status, string UserID)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_UnBLOCK_GetbyStatus", Status, UserID);
        }

        public static void BOPENACCOUNT_UnBLOCK_UpdateStatus(string Status, string NormalLCCode, string userid, bool BlockAccount_temp)
        {
            sqldata.ndkExecuteNonQuery("BOPENACCOUNT_UnBLOCK_UpdateStatus", Status, NormalLCCode, userid, BlockAccount_temp);
        }

        public static DataSet BOPENACCOUNT_UnBLOCK_GetByID(int id)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_UnBLOCK_GetByID", id);
        }

        public static DataSet BOPENACCOUNT_UnBLOCK_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BOPENACCOUNT_UnBLOCK_GetByCode", code);
        }

        #endregion

        #region BTRANSFERWITHDRAWAL
        public static DataSet BTRANSFERWITHDRAWAL_GetbyStatus(string Status, string UserID)
        {
            return sqldata.ndkExecuteDataset("BTRANSFERWITHDRAWAL_GetbyStatus", Status, UserID);
        }

        public static void BTRANSFERWITHDRAWAL_UpdateStatus(string accounttype, string Status, string NormalLCCode, string userid)
        {
            sqldata.ndkExecuteNonQuery("BTRANSFERWITHDRAWAL_UpdateStatus", accounttype, Status, NormalLCCode, userid);
        }

        public static DataSet BTRANSFERWITHDRAWAL_GetByID(int id)
        {
            return sqldata.ndkExecuteDataset("BTRANSFERWITHDRAWAL_GetByID", id);
        }

        public static DataSet BTRANSFERWITHDRAWAL_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BTRANSFERWITHDRAWAL_GetByCode", code);
        }

        public static void BTRANSFERWITHDRAWAL_Insert(string accounttype, string Code, string DebitAccount, double DebitAmount, double CustBallance, double NewCustBallance, DateTime? DebitValueDate,
                                                    string CreditAccount, double AmountCreditForCustomer, double DealRate, DateTime? CreditValueDate, string WaiveCharges, string Narrative, int UserId)
        {
            sqldata.ndkExecuteNonQuery("BTRANSFERWITHDRAWAL_Insert", accounttype, Code, DebitAccount, DebitAmount, CustBallance, NewCustBallance, DebitValueDate, CreditAccount, AmountCreditForCustomer, DealRate, CreditValueDate,
                         WaiveCharges, Narrative, UserId);
        }

        public static DataSet BCASHWITHRAWAL_Print_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BCASHWITHRAWAL_Print_GetByCode", code);
        }
        #endregion

        #region BCASHDEPOSIT
        public static DataSet BCASHDEPOSIT_GetbyStatus(string Status, string UserID)
        {
            return sqldata.ndkExecuteDataset("BCASHDEPOSIT_GetbyStatus", Status, UserID);
        }

        public static void BCASHDEPOSIT_UpdateStatus(string AccountType, string Status, string NormalLCCode, string userid)
        {
            sqldata.ndkExecuteNonQuery("BCASHDEPOSIT_UpdateStatus", AccountType, Status, NormalLCCode, userid);
        }

        public static DataSet BCASHDEPOSIT_GetByID(int id)
        {
            return sqldata.ndkExecuteDataset("BCASHDEPOSIT_GetByID", id);
        }

        public static DataSet BCASHDEPOSIT_Print_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BCASHDEPOSIT_Print_GetByCode", code);
        }

        public static DataSet BCASHDEPOSIT_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BCASHDEPOSIT_GetByCode", code);
        }

        public static void BCASHDEPOSIT_Insert(string AccountType, string Code, string CustomerAccount, double AmtPaidToCust, double CustBallance, double NewCustBallance, string CurrencyDeposited,
                                                double AmountDeposited, double DealRate, string WaiveCharges, string Narrative, string PrintLnNoOfPS, int Userid, string teller, string cashaccount
            )
        {
            sqldata.ndkExecuteNonQuery("BCASHDEPOSIT_Insert", AccountType, Code, CustomerAccount, AmtPaidToCust, CustBallance, NewCustBallance, CurrencyDeposited, AmountDeposited,
                                    DealRate, WaiveCharges, Narrative, PrintLnNoOfPS, Userid, teller, cashaccount);
        }
        #endregion

        #region BCASHWITHRAWAL
        public static DataSet BCASHWITHRAWAL_GetbyStatus(string Status, string UserID)
        {
            return sqldata.ndkExecuteDataset("BCASHWITHRAWAL_GetbyStatus", Status, UserID);
        }

        public static void BCASHWITHRAWAL_UpdateStatus(string accountType, string Status, string NormalLCCode, string userid)
        {
            sqldata.ndkExecuteNonQuery("BCASHWITHRAWAL_UpdateStatus", accountType, Status, NormalLCCode, userid);
        }

        public static DataSet BCASHWITHRAWAL_GetByID(int id)
        {
            return sqldata.ndkExecuteDataset("BCASHWITHRAWAL_GetByID", id);
        }

        public static DataSet BCASHWITHRAWAL_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BCASHWITHRAWAL_GetByCode", code);
        }

        public static void BCASHWITHRAWAL_Insert(string accountType, string Code, string CustomerAccount, double AmtPaidToCust, double CustBallance, double NewCustBallance, string CurrencyDeposited,
                                                double AmountDeposited, double DealRate, string WaiveCharges, string Narrative, string PrintLnNoOfPS, int Userid, string teller, string cashaccount
            )
        {
            sqldata.ndkExecuteNonQuery("BCASHWITHRAWAL_Insert", accountType, Code, CustomerAccount, AmtPaidToCust, CustBallance, NewCustBallance, CurrencyDeposited, AmountDeposited,
                                    DealRate, WaiveCharges, Narrative, PrintLnNoOfPS, Userid, teller, cashaccount);
        }
        #endregion

        #region B_AdvisingLC
        public static void B_ADVISING_Insert(string AdvisingLCCode, string LCType, string LCNumber, string BeneficiaryCustNo, string BeneficiaryAddr, string BeneficiaryAcct, string IssuingBankNo, string IssBankAddr, string IssBankAcct,
                      string ApplicantNo, string ApplicantAddr, string ApplicantBank, string ReimbBankRef, string ReimbBankNo, string ReimbBankAddr, string AdviceThruBank, string AdvThruAddr, string AvailablewithCustno,
                      string AvailablewithAddr, string Currency, string Amount, string ToleranceIncr, string ToleranceDecr, string IssuingDate, string ExpiryDate, string ExpiryPlace, string ContingentExpiryDate, string Commodity, string AdvisedBy,string GenerateDelivery, string UserID)
        {
            sqldata.ndkExecuteNonQuery("B_ADVISING_Insert", AdvisingLCCode, LCType, LCNumber, BeneficiaryCustNo, BeneficiaryAddr, BeneficiaryAcct, IssuingBankNo, IssBankAddr, IssBankAcct,
                          ApplicantNo, ApplicantAddr, ApplicantBank, ReimbBankRef, ReimbBankNo, ReimbBankAddr, AdviceThruBank, AdvThruAddr, AvailablewithCustno,
                          AvailablewithAddr, Currency, Amount, ToleranceIncr, ToleranceDecr, IssuingDate == "" ? null : IssuingDate, ExpiryDate == "" ? null : ExpiryDate, ExpiryPlace, ContingentExpiryDate == "" ? null : ContingentExpiryDate, Commodity, AdvisedBy,GenerateDelivery, UserID);
        }

        public static DataSet B_ADVISING_GetbyStatus(string Status, string UserID)
        {
            return sqldata.ndkExecuteDataset("B_ADVISING_GetbyStatus", Status, UserID);
        }

        public static DataSet B_ADVISING_GetSearch(string LCCode, string LCType, string cusno)
        {
            return sqldata.ndkExecuteDataset("B_ADVISING_GetSearch", LCCode, LCType, cusno);
        }

        public static DataSet B_ADVISING_GetbyLCCode(string LCCode)
        {
            return sqldata.ndkExecuteDataset("B_ADVISING_GetbyLCCode", LCCode);
        }

        public static void B_ADVISING_Authorize(string LCCode)
        {
            sqldata.ndkExecuteNonQuery("B_ADVISING_Authorize", LCCode);
        }

        public static void B_ADVISING_CHARGES_Insert(string LCCode, string WaiveCharges, string Chargecode, string ChargeAcct, string ChargePeriod, string ChargeCcy
            , string ExchRate, string ChargeAmt, string PartyCharged, string OmortCharges, string AmtInLocalCCY, string AmtDRfromAcct, string ChargeStatus
            , string ChargeRemarks, string VATNo, string TaxCode, string TaxCcy, string TaxAmt, string TaxinLCCYAmt, string TaxDate, string Rowchages)
        {
            sqldata.ndkExecuteNonQuery("B_ADVISING_CHARGES_Insert", LCCode, WaiveCharges, Chargecode, ChargeAcct, ChargePeriod, ChargeCcy
            , ExchRate, ChargeAmt, PartyCharged, OmortCharges, AmtInLocalCCY, AmtDRfromAcct, ChargeStatus
            , ChargeRemarks, VATNo, TaxCode, TaxCcy, TaxAmt, TaxinLCCYAmt, TaxDate, Rowchages);
        }
        #endregion

        #region B_BNormal
        public static void B_BNORMAILLC_Insert(string NormalLCCode, string LCType, string ApplicantID, string ApplicantName, string ApplicantAddr1, string ApplicantAddr2, string ApplicantAddr3
            , string ApplicantAcct, string CcyAmount, string Sotien, string CrTolerance, string DrTolerance, string IssuingDate, string ExpiryDate, string ExpiryPlace, string ContingentExpiry, string PayType
            , string PaymentpCt, string PaymentPortion, string AccptTimeBand, string LimitRef, string BeneficiaryNo, string BeneficiaryNameAddr1, string BeneficiaryNameAddr2,
            string BeneficiaryNameAddr3, string AdviseBankRef, string AdviseBankNo, string AdviseBankAddr1, string AdviseBankAddr2, string AdviseBankAddr3, string AdviseBankAcct,
            string ReimbBankNo, string ReimbBankAddr1, string ReimbBankAddr2, string ReimbBankAddr3, string ReimbBankAcct, string AdviseThruNo, string AdviseThruAddr1, string AdviseThruAddr2
            , string AdviseThruAddr3, string AdviseThruAcct, string AvailWithNo, string AvailWithNameAddr, string Commodity, string Prov, string OldLCNo, string UserID, string AccountOfficer, string ContactNo, string LcAmountSecured
            , string LcAmountUnSecured, string LoanPrincipal)
        {
            sqldata.ndkExecuteNonQuery("B_BNORMAILLC_Insert", NormalLCCode, LCType, ApplicantID, ApplicantName, ApplicantAddr1, ApplicantAddr2, ApplicantAddr3
            , ApplicantAcct, CcyAmount, Sotien, CrTolerance, DrTolerance, IssuingDate, ExpiryDate, ExpiryPlace, ContingentExpiry, PayType
            ,  PaymentpCt,  PaymentPortion,  AccptTimeBand,  LimitRef,  BeneficiaryNo,  BeneficiaryNameAddr1,  BeneficiaryNameAddr2,
             BeneficiaryNameAddr3,  AdviseBankRef,  AdviseBankNo,  AdviseBankAddr1,  AdviseBankAddr2,  AdviseBankAddr3,  AdviseBankAcct,
             ReimbBankNo,  ReimbBankAddr1,  ReimbBankAddr2,  ReimbBankAddr3,  ReimbBankAcct,  AdviseThruNo,  AdviseThruAddr1,  AdviseThruAddr2
            , AdviseThruAddr3, AdviseThruAcct, AvailWithNo, AvailWithNameAddr, Commodity, Prov, OldLCNo, UserID,AccountOfficer, ContactNo, LcAmountSecured
            , LcAmountUnSecured, LoanPrincipal);
        }

        public static void B_BNORMALLCCHARGES_Insert(string NormalLCCode, string WaiveCharges, string Chargecode, string ChargeAcct, string ChargePeriod, string ChargeCcy
            , string ExchRate, string ChargeAmt, string PartyCharged, string OmortCharges, string AmtInLocalCCY, string AmtDRfromAcct, string ChargeStatus
            , string ChargeRemarks, string VATNo, string TaxCode, string TaxCcy, string TaxAmt, string TaxinLCCYAmt, string TaxDate, string Rowchages)
        {
            sqldata.ndkExecuteNonQuery("B_BNORMALLCCHARGES_Insert",NormalLCCode, WaiveCharges, Chargecode, ChargeAcct, ChargePeriod, ChargeCcy
            , ExchRate, ChargeAmt, PartyCharged, OmortCharges, AmtInLocalCCY, AmtDRfromAcct, ChargeStatus
            , ChargeRemarks, VATNo, TaxCode, TaxCcy, TaxAmt, TaxinLCCYAmt, TaxDate, Rowchages);
        }

        public static void B_BNORMAILLCPROVITIONTRANSFER_Insert(string LCNo, string Orderedby, string DebitRef, string DebitAccount, string DebitCurrency, string DebitAmout
            , string DebitDate, string CreditAccount, string CreditCurrency, string TreasuryRate, string CreditAmount, string CreditDate, string VATSerialNo)
        {
            sqldata.ndkExecuteNonQuery("B_BNORMAILLCPROVITIONTRANSFER_Insert", LCNo, Orderedby, DebitRef, DebitAccount, DebitCurrency, DebitAmout
            , DebitDate, CreditAccount, CreditCurrency, TreasuryRate, CreditAmount, CreditDate, VATSerialNo);
        }
        #endregion

        #region BCOLLECTCHARGESFROMACCOUNT
        public static DataSet BCOLLECTCHARGESFROMACCOUNT_GetbyStatus(string Status, string UserID)
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESFROMACCOUNT_GetbyStatus", Status, UserID);
        }
        public static DataSet BCOLLECTCHARGESFROMACCOUNT_GetbyStatus_2()
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESFROMACCOUNT_GetbyStatus_2");
        }
        public static DataSet BCOLLECTCHARGESFROMACCOUNT_Check_Available_Amt(string AccountType, string AccountCode)
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESFROMACCOUNT_Check_Available_Amt", AccountType, AccountCode);
        }
        public static void BCOLLECTCHARGESFROMACCOUNT_UpdateStatus(string AccountType, string Status, string NormalLCCode, string userid)
        {
             sqldata.ndkExecuteNonQuery("BCOLLECTCHARGESFROMACCOUNT_UpdateStatus", AccountType, Status, NormalLCCode, userid);
        }
        
        public static DataSet BCOLLECTCHARGESFROMACCOUNT_GetByID(int  Code)
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESFROMACCOUNT_GetByID", Code);
        }
        public static DataSet BCOLLECTCHARGESFROMACCOUNT_GetByCodeDebit(string Code)
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESFROMACCOUNT_GetByCodeDebit", Code);
        }
        public static DataSet BCOLLECTCHARGESFROMACCOUNT_Print_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESFROMACCOUNT_Print_GetByCode", code);
        }

        public static DataSet BCOLLECTCHARGESFROMACCOUNT_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESFROMACCOUNT_GetByCode", code);
        }
		
        public static void BCOLLECTCHARGESFROMACCOUNT_Insert(string AccountType, string Code, string CustomerAccount, double ChargAmountLCY, double ChargAmountFCY, DateTime? ValueDate, string CategoryPLCode,
                                            string CategoryPLName, double DealRate, double VatAmountLCY, double VatAmountFCY, double TotalAmountLCY, double TotalAmountFCY, string VatSerialNo, string Narrative, int UserId
                                                ,string CustomerID, string CustomerName, string Currency)
        {
            sqldata.ndkExecuteNonQuery("BCOLLECTCHARGESFROMACCOUNT_Insert", AccountType, Code, CustomerAccount, ChargAmountLCY, ChargAmountFCY, ValueDate, CategoryPLCode, CategoryPLName, DealRate,
                                    VatAmountLCY, VatAmountFCY, TotalAmountLCY, TotalAmountFCY, VatSerialNo, Narrative, UserId, CustomerID,CustomerName,Currency);
        }
        public static DataSet BCOLLECTCHARGESFROMACCOUNT_Enquiry(string CollectionType, string RefID, string AccountType, string AccountID, string CustomerID
            , string CustomerName, string LegalID, double FromChargesAmt, double ToChargesAmt)
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESFROMACCOUNT_Enquiry", CollectionType, RefID, AccountType, AccountID, CustomerID, CustomerName, LegalID
                , FromChargesAmt, ToChargesAmt);
        }
        #endregion
		
        #region BCOLLECTCHARGESBYCASH
        public static DataSet BCOLLECTCHARGESBYCASH_GetbyStatus(string Status, string UserID)
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESBYCASH_GetbyStatus", Status, UserID);
        }
		
        public static void BCOLLECTCHARGESBYCASH_UpdateStatus(string AccountType, string Status, string NormalLCCode, string userid)
        {
            sqldata.ndkExecuteNonQuery("BCOLLECTCHARGESBYCASH_UpdateStatus", AccountType, Status, NormalLCCode, userid);
        }
		
        public static DataSet BCOLLECTCHARGESBYCASH_GetByID(int id)
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESBYCASH_GetByID", id);
        }

        public static DataSet BCOLLECTCHARGESBYCASH_Print_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESBYCASH_Print_GetByCode", code);
        }
		
        public static DataSet BCOLLECTCHARGESBYCASH_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BCOLLECTCHARGESBYCASH_GetByCode", code);
        }
		
        public static void BCOLLECTCHARGESBYCASH_Insert(string AccountType, string Code, string customerid, string customerAddress, string docid, DateTime? docissuedate, string docissueplace,
                                                    string teller, string currency, string CustomerAccount, double ChargAmountLCY, double ChargAmountFCY, DateTime? ValueDate, string CategoryPLCode,
                                                    string CategoryPLName, double DealRate, double VatAmountLCY, double VatAmountFCY, double TotalAmountLCY, double TotalAmountFCY, string VatSerialNo, string Narrative, int UserId
            )
        {
            sqldata.ndkExecuteNonQuery("BCOLLECTCHARGESBYCASH_Insert", AccountType, Code, customerid, customerAddress, docid, docissuedate, docissueplace,
                                    teller, currency, CustomerAccount, ChargAmountLCY, ChargAmountFCY, ValueDate, CategoryPLCode, CategoryPLName, DealRate,
                                    VatAmountLCY, VatAmountFCY, TotalAmountLCY, TotalAmountFCY, VatSerialNo, Narrative, UserId);
        }
        #endregion
		
        #region BNEWNORMALLOAN
        public static DataSet BNEWNORMALLOAN_GetbyStatus(string Status, string UserID)
        {
            return sqldata.ndkExecuteDataset("BNEWNORMALLOAN_GetbyStatus", Status, UserID);
        }
        
		public static void BNEWNORMALLOAN_UpdateStatus(string Status, string NormalLCCode, string userid)
        {
            sqldata.ndkExecuteNonQuery("BNEWNORMALLOAN_UpdateStatus", Status, NormalLCCode, userid);
        }
        
        public static DataSet BNEWNORMALLOAN_Print_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BNEWNORMALLOAN_Print_GetByCode", code);
        }

        public static DataSet BNEWNORMALLOAN_GetByCode(string code)
        {
            return sqldata.ndkExecuteDataset("BNEWNORMALLOAN_GetByCode", code);
        }

        public static void BNEWNORMALLOAN_Insert(string Code, string MainCategory, string MainCategoryName, string SubCategory, string SubCategoryName, string PurpostCode,
            string PurpostName, string CustomerID, string CustomerName, string LoanGroup, string LoanGroupName, string Currency, string BusDayDef, string BusDayDefName,
            double LoanAmount, double ApproveAmount, DateTime? OpenDate, DateTime? Drawdown, DateTime? ValueDate, DateTime? MaturityDate, string CreditAccount, string CommitmentID,
            string LimitReference, string RateType, string InterestBasic, string AnnuityRepMet, string IntPayMethod, double InterestRate, string InterestKey, string IntSpread,
            string AutoSch, string DefineSch, string RepaySchType, string LoanStatus, double TotalInterestAmt, string PDStatus, string PrinRepAccount, string IntRepAccount,
            string ChrgRepAccount, double ExpectedLoss, double LossGivenDef, string CustomerRemarks, string AccountOfficer, string AccountOfficerName, string Secured,
            string CollateralID, double AmountAlloc, string CountryRisk, double LegacyRef, int UserId
            )
        {
            sqldata.ndkExecuteNonQuery("BNEWNORMALLOAN_Insert", Code, MainCategory, MainCategoryName, SubCategory, SubCategoryName, PurpostCode,
                    PurpostName, CustomerID, CustomerName, LoanGroup, LoanGroupName, Currency, BusDayDef, BusDayDefName,
                    LoanAmount, ApproveAmount, OpenDate, Drawdown, ValueDate, MaturityDate, CreditAccount, CommitmentID,
                    LimitReference, RateType, InterestBasic, AnnuityRepMet, IntPayMethod, InterestRate, InterestKey, IntSpread,
                    AutoSch, DefineSch, RepaySchType, LoanStatus, TotalInterestAmt, PDStatus, PrinRepAccount, IntRepAccount,
                    ChrgRepAccount, ExpectedLoss, LossGivenDef, CustomerRemarks, AccountOfficer, AccountOfficerName, Secured,
                    CollateralID, AmountAlloc, CountryRisk, LegacyRef, UserId);
        }  

		public static DataSet BNEWNORMALLOAN_Enquiry(string Code, string CustomerType, string CustomerID, string CustomerName, string docid, string MainCategory, string currency, string SubCategory)
        {
            return sqldata.ndkExecuteDataset("BNEWNORMALLOAN_Enquiry", Code, CustomerType, CustomerID, CustomerName, docid, MainCategory, currency, SubCategory);
        }
        #endregion
		
        public static DataSet B_BRESTRICT_TXN_GetAll()
        {
            return sqldata.ndkExecuteDataset("[dbo].[BRESTRICT_TXN_GetAll]");
        }
        public static DataSet B_BBPLACCOUNT_GetAll()
        {
            return sqldata.ndkExecuteDataset("[dbo].[BPLACCOUNT_GetAll]");
        }

        public static DataTable ExchangeRate()
        {
            return sqldata.ndkExecuteDataset("P_ExchangeRate").Tables[0];
        }
    }
}