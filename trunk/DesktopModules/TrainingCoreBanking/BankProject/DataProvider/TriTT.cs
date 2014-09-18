using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace BankProject.DataProvider
{
    public static class TriTT
    {
        private static SqlDataProvider sqldata = new SqlDataProvider();
        private static string ConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["VietVictoryCoreBanking"].ConnectionString;
        }

        #region Get Data from Sql Server

        public static string B_BMACODE_GetNewID_2(string MaCode, string refix, string flat = "-")
        {
           
            string s = sqldata.ndkExecuteDataset("B_BMACODE_GetNewID_2", refix, flat).Tables[0].Rows[0]["Code"].ToString();
            return s;
        }
        public static string B_BMACODE_GetNewID_3par(string MaCode, string refix, string flat = "/")
        {
            string chuoi = sqldata.ndkExecuteDataset("B_BMACODE_GetNewID_3par", refix, flat).Tables[0].Rows[0]["Code"].ToString();
            return chuoi;
        }
        public static string B_BMACODE_GetNewID_3part_new(string StoredProc, string MaCode, string refix, string flat)
        {
            string chuoi = sqldata.ndkExecuteDataset(StoredProc, MaCode, refix, flat).Tables[0].Rows[0]["Code"].ToString();
            return chuoi;
        }
        public static string B_BMACODE_NewID_3par_CashRepayment(string MaCode, string refix, string flat = "/")
        {
            string chuoi = sqldata.ndkExecuteDataset("B_BMACODE_NewID_3par_CashRepayment", refix, flat).Tables[0].Rows[0]["Code"].ToString();
            return chuoi;
        }
        public static string B_BMACODE_NewID_3par_PastDueLoanReference(string MaCode, string refix, string flat = "/")
        {
            string chuoi = sqldata.ndkExecuteDataset("B_BMACODE_NewID_3par_PastDueLoanReference", refix, flat).Tables[0].Rows[0]["Code"].ToString();
            return chuoi;
        }
        public static string B_BMACODE_Amend_Loan_Contract(string MaCode, string refix, string flag = "/")
        {
            string chuoi = sqldata.ndkExecuteDataset("B_BMACODE_Amend_Loan_Contract", refix, flag).Tables[0].Rows[0]["Code"].ToString();
            return chuoi;
        }
        public static string B_BMACODE_3part_varMaCode_varSP(string SP_Name, string MaCode_BMACODE, string refix, string flag = "/")
        {
            string chuoi = sqldata.ndkExecuteDataset(SP_Name, MaCode_BMACODE, refix,flag).Tables[0].Rows[0]["Code"].ToString();
            return chuoi;
        }
   

        public static DataSet B_BCOUNTRY_GetAll()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_BCOUNTRY_GetAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@CVID", cvid);
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    adapt.Fill(dsInfo);

                    cmd.Dispose();
                    conn.Close();

                    return dsInfo;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                return null;
            }
        }
        public static DataSet B_LoadCurrency(string Cur1, string Cur2)
        {
            return sqldata.ndkExecuteDataset("B_LoadCurrency", Cur1, Cur2);
        }
        public static DataSet B_OPEN_LOANWORK_ACCT_Get_ALLCustomerID()
        {
            return sqldata.ndkExecuteDataset("B_OPEN_LOANWORK_ACCT_Get_ALLCustomerID");
        }
        public static DataSet B_BCRFROMACCOUNT_GetByCustomer(string currency)
        {
            return sqldata.ndkExecuteDataset("B_BCRFROMACCOUNT_GetByCurrency", currency);
        }

        public static DataSet B_OPEN_LOANWORK_ACCT_Get_ALLCategory(string StoredProc,string CategoryType) // lay du lieu Code Category, co variable chon STored Proc va TYPE
        {
            return sqldata.ndkExecuteDataset(StoredProc, CategoryType);
        }
        public static DataSet BCUSTOMER_SAVINGACCT()
        {
            return sqldata.ndkExecuteDataset("dbo.BCUSTOMER_SAVINGACCT");
        }

        public static string B_CHEQUE_ISSUE_NO(string MaCode)
        {
            return sqldata.ndkExecuteDataset("B_BCHEQUE_ISSUE_getNo", MaCode).Tables[0].Rows[0]["SoTT"].ToString();
        }
        public static string B_BCUSTOMER_GetID(string MaCode)  //lay soTT cho Open Individual Customer Co luu du lieu
        {
            return sqldata.ndkExecuteDataset("B_BCUSTOMER_GetID", MaCode).Tables[0].Rows[0]["SoTT"].ToString();
        }
        public static string B_BCUSTOMER_GetID_Corporate(string SP_Procedure, string MaCode) //lay soTT cho Open Corporate Customer Co luu du lieu
        {
            return sqldata.ndkExecuteDataset(SP_Procedure, MaCode).Tables[0].Rows[0]["SoTT"].ToString();
        }

        public static DataSet B_BLOANACCOUNT_getbyCurrency(string CustomerName, string Currency)
        {
            return sqldata.ndkExecuteDataset("B_BLOANACCOUNT_getbyCurrency", CustomerName, Currency);
        }


        internal static DataSet B_BCHEQUERETURN_findItem(string tbChequeReference, string tbCustomerID, string tbCustomerName, string IssueDate)
        {
            return sqldata.ndkExecuteDataset("B_BCHEQUERETURN_findItem", tbChequeReference, tbCustomerID, tbCustomerName, IssueDate);
        }
       
        public static DataSet B_BPastDueLoanRepayment_FindItem(string LoanContractReference, string CustomerID, string CustomerName)
        {
            return sqldata.ndkExecuteDataset("B_BPastDueLoanRepayment_FindItem", LoanContractReference, CustomerID, CustomerName);
        }
        #endregion
        #region cac ham luu du lieu xuong Database
        public static void OPEN_CORPORATE_CUSTOMER_Insert_Account
            (string CustomerID, string Status, string GBShortName, string GBFullName, DateTime? IncorpDate, string GBStreet, string GBDist, string MaTinhThanh,
            string TenTinhThanh, string CountryCode, string CountryName, string NationalityCode, string NationalityName, string ResidenceCode, string ResidenceName
            , string DocType, string DocID, string DocIssuePlace, DateTime? DocIssueDate, DateTime? DocExpiryDate,
            string ContactPerson, string Position, string Telephone, string EmailAddress, string Remarks
            , string SectorCode, string SectorName, string SubSectorCode, string SubSectorName, string IndustryCode, string IndustryName, string SubIndustryCode
            , string SubIndustryName, string TargetCode, string AccountOfficer, DateTime? ContactDate, string RelationCode, string OfficeNumber
            , string TotalCapital, string NoOfEmployee, string TotalAssets, string TotalRevenue, string CustomerLiability, string LegacyRef, string ApprovedUser)
        {
            sqldata.ndkExecuteNonQuery("OPEN_CORPORATE_CUSTOMER_Insert_Account", CustomerID, Status, GBShortName, GBFullName, IncorpDate, GBStreet, GBDist, MaTinhThanh, TenTinhThanh,
                CountryCode, CountryName, NationalityCode, NationalityName, ResidenceCode, ResidenceName
            , DocType, DocID, DocIssuePlace, DocIssueDate, DocExpiryDate, ContactPerson, Position, Telephone, EmailAddress, Remarks
            , SectorCode, SectorName, SubSectorCode, SubSectorName, IndustryCode, IndustryName, SubIndustryCode
            , SubIndustryName, TargetCode, AccountOfficer, ContactDate, RelationCode, OfficeNumber
            , TotalCapital, NoOfEmployee, TotalAssets, TotalRevenue, CustomerLiability, LegacyRef, ApprovedUser);
        }
        public static void B_OPEN_LOANWORK_ACCT_Insert_Update_Acct(string RefID, string CustomerID, string Status, string GBFullName, string DocType, string DocID, string DocIssuePlace
            , DateTime? DocIssueDate, DateTime? DocExpiryDate, string CategoryCode, string CategoryName, string AccountName, string ShortTittle, string Mnemonic
            , string CurrencyCode, string CurrencyDescr, string ProductLineCode, string ProductLineDescr, string AlternateAcct, string CreatedUser)
        {
            sqldata.ndkExecuteNonQuery("B_OPEN_LOANWORK_ACCT_Insert_Update_Acct", RefID, CustomerID, Status, GBFullName, DocType, DocID, DocIssuePlace
            , DocIssueDate, DocExpiryDate, CategoryCode, CategoryName, AccountName, ShortTittle, Mnemonic
            , CurrencyCode, CurrencyDescr, ProductLineCode, ProductLineDescr, AlternateAcct, CreatedUser);
        }
        public static DataSet OPEN_CORPORATE_CUSTOMER_review_Account(string CustomerID, string Status, string CustomerType, string LoadFor_List1_review2)
        {
            return sqldata.ndkExecuteDataset("OPEN_CORPORATE_CUSTOMER_review_Account", CustomerID, Status, CustomerType, LoadFor_List1_review2);
        }
        public static DataSet OPEN_CORPORATE_CUSTOMER_review_Account_search_tai_Form(string CustomerID,string CustomerType)
        {
            return sqldata.ndkExecuteDataset("OPEN_CORPORATE_CUSTOMER_review_Account_search_tai_Form", CustomerID, CustomerType);
        }
        public static void OPEN_CORPORATE_CUSTOMER_Authorize_Account(string CustomerID, String Status)
        {
            sqldata.ndkExecuteNonQuery("OPEN_CORPORATE_CUSTOMER_Authorize_Account", CustomerID, Status);
        }
        public static DataSet OPEN_INDIVIDUAL_CUSTOMER_CheckDocID_Exists(string CustomerID,string CustomerType, string DocID)
        {
            return sqldata.ndkExecuteDataset("OPEN_INDIVIDUAL_CUSTOMER_CheckDocID_Exists", CustomerID, CustomerType,  DocID);
        }
        public static DataSet B_BCASHWITHDRAWAL_Load_Customer_WorkingAmt(string AccountType, string CustomerAccountCode,string Currency)
        {
            return sqldata.ndkExecuteDataset("B_BCASHWITHDRAWAL_Load_Customer_WorkingAmt",AccountType ,CustomerAccountCode,Currency);
        }
        #endregion
        #region B_OPEN_LOAN_WORKING_ACCOUNT
        public static void B_OPEN_LOANWORK_ACCT_Update_Status(string RefID, string CustomerID, string Status)
        {
            sqldata.ndkExecuteNonQuery("B_OPEN_LOANWORK_ACCT_Update_Status", RefID, CustomerID, Status);
        }
        public static DataSet B_OPEN_LOANWORK_ACCT_List_Preview(string status)
        {
            return sqldata.ndkExecuteDataset("B_OPEN_LOANWORK_ACCT_List_Preview", status);
        }
        public static DataSet B_OPEN_LOANWORK_ACCT_Load_Account(string RefID)
        {
            return sqldata.ndkExecuteDataset("B_OPEN_LOANWORK_ACCT_Load_Account", RefID);
        }
        public static DataSet B_OPEN_LOANWORK_ACCT_Enquiry_Customer(string AccountID, string CustomerID, string GBFullName, string Currency, string ProductLineCode
            ,string CategoryCode, string DocID)
        {
            return sqldata.ndkExecuteDataset("B_OPEN_LOANWORK_ACCT_Enquiry_Customer", AccountID, CustomerID, GBFullName, Currency, ProductLineCode, CategoryCode, DocID);
        }
        public static DataSet B_OPEN_LOANWORK_ACCT_Check_Acct_Exist(string CustomerID,string Currency)
        {
            return sqldata.ndkExecuteDataset("B_OPEN_LOANWORK_ACCT_Check_Acct_Exist",CustomerID,Currency);
        }
#endregion 
        #region B_OPEN_COMMITMENT_CONTRACT
        public static DataSet B_OPEN_COMMITMENT_CONT_Load_ALLRepayAcct(string CustomerID, string Currency, string CategoryType)
        {
            return sqldata.ndkExecuteDataset("B_OPEN_COMMITMENT_CONT_Load_ALLRepayAcct",CustomerID, Currency, CategoryType);
        }
        public static void B_OPEN_COMMITMENT_CONT_Insert_Update_Acct(string ID, string Status,string CategoryCode, string CategoryName, string CustomerID, string GBFullName
            , string DocType, string DocID, string DocIssuePlace, DateTime? DocIssueDate, string CurrencyCode, string CommitmentAmt, DateTime? StartDate
            , DateTime? EndDate, string CommitmentFeeStart, string CommitmentFeeEnd, string AvailableAmt, string TrancheAmt, DateTime? DDStartDate, DateTime? DDEndDate, string IntRepayAcctID
            , string IntRepayAcctName, string Secured, string CustomerRemark,string AccountOfficeID, string AccountOfficerName, string ApproveUser)
        {
            sqldata.ndkExecuteNonQuery("B_OPEN_COMMITMENT_CONT_Insert_Update_Acct", ID, Status, CategoryCode, CategoryName, CustomerID, GBFullName
            , DocType, DocID, DocIssuePlace, DocIssueDate, CurrencyCode, CommitmentAmt, StartDate
            , EndDate, CommitmentFeeStart, CommitmentFeeEnd, AvailableAmt, TrancheAmt, DDStartDate, DDEndDate, IntRepayAcctID
            , IntRepayAcctName, Secured, CustomerRemark, AccountOfficeID, AccountOfficerName, ApproveUser);
        }
        public static DataSet B_OPEN_COMMITMENT_CONT_Check_Acct_Exist(string CategoryCode,string CustomerID, string Currency)
        {
            return sqldata.ndkExecuteDataset("B_OPEN_COMMITMENT_CONT_Check_Acct_Exist", CategoryCode, CustomerID, Currency);
        }
        public static DataSet ENQUIRY_CUSTOMER_Search_Account_Customer(string CustomerType, string CustomerID, string CellPhone, string GBFullName, string DocID,
                string MainSectorCode, string SubSectorCode, string MainIndustryCode, string SubIndustryCode)
        {
            return sqldata.ndkExecuteDataset("ENQUIRY_CUSTOMER_Search_Account_Customer", CustomerType, CustomerID, CellPhone, GBFullName, DocID, MainSectorCode,
                SubSectorCode, MainIndustryCode, SubIndustryCode);
        }
        public static void B_OPEN_COMMITMENT_CONT_Update_Status(string RefID, string CustomerID, string Status)
        {
            sqldata.ndkExecuteNonQuery("B_OPEN_COMMITMENT_CONT_Update_Status", RefID, CustomerID, Status);
        }
        public static DataSet B_OPEN_COMMITMENT_CONT_Load_Acct(string RefID)
        {
            return sqldata.ndkExecuteDataset("B_OPEN_COMMITMENT_CONT_Load_Acct", RefID);
        }
        public static DataSet B_OPEN_COMMITMENT_CONT_Enquiry_Account(string ID, string CustomerID, string GBFullName, string Currency, string Category, string DocID
            , string IntRepayAcc)
        {
            return sqldata.ndkExecuteDataset("B_OPEN_COMMITMENT_CONT_Enquiry_Account", ID, CustomerID, GBFullName, Currency, Category, DocID, IntRepayAcc);
        }
        #endregion
        #region B_DEFINE_CUSTOMER_LIMIT
        public static void B_CUSTOMER_LIMIT_Insert_Update(string MainLimitID, string CustomerID, string CommitmentType, string CurrencyCode, string CountryCode, string CountryName
            , DateTime? ApprovedDate, DateTime? OfferedUntil, DateTime? ExpiryDate, DateTime? Proposaldate, DateTime? AvailableDate, decimal? InternalLimitAmt
            , decimal? AdvisedAmt, decimal? OriginalLimit, string Note, string Mode, decimal? MaxTotal, string ApprovedUser)
        {
             sqldata.ndkExecuteNonQuery("B_CUSTOMER_LIMIT_Insert_Update", MainLimitID, CustomerID, CommitmentType, CurrencyCode, CountryCode, CountryName
            , ApprovedDate, OfferedUntil, ExpiryDate, Proposaldate, AvailableDate, InternalLimitAmt, AdvisedAmt, OriginalLimit, Note, Mode, MaxTotal, ApprovedUser);
        }
        public static string B_CUSTOMER_LIMIT_Check_CustomerID(string CustomerID)
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_LIMIT_Check_CustomerID", CustomerID).Tables[0].Rows[0]["CustomerID"].ToString();
        }
        public static DataSet B_CUSTOMER_LIMIT_Load_CollateralType()
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_LIMIT_Load_CollateralType");
        }
        public static DataSet B_CUSTOMER_LIMIT_Load_CollateralCode(string CollateralTypeCode)
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_LIMIT_Load_CollateralCode", CollateralTypeCode);
        }
        public static DataSet B_CUSTOMER_LIMIT_Load_Customer_Limit(string CustomerLimitID)
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_LIMIT_Load_Customer_Limit", CustomerLimitID);
        }
        public static void B_CUSTOMER_LIMIT_SUB_Insert_Update(string MainLimitID,string SubLimitID, string CustomerID,string SubCommitmentType, string STTSub, string mode, string CollateralTypeCode
            , string CollateralTypeName, string CollateralCode, string CollateralName, string CollReqdAmt, string CollReqdPct, string UptoPeriod, string PeriodAmt
            , string PeriodPct, decimal MaxSecured, decimal MaxUnSecured, decimal MaxTotal, string OtherSecured, string CollateralRight, string AmtSecured
            , string Onlinelimit, string AvailableAmt, string TotalOutstand, string ApprovedUser, string MainComtType)
        {
            sqldata.ndkExecuteNonQuery("B_CUSTOMER_LIMIT_SUB_Insert_Update",MainLimitID, SubLimitID, CustomerID, SubCommitmentType, STTSub, mode, CollateralTypeCode
                                        , CollateralTypeName, CollateralCode, CollateralName, CollReqdAmt, CollReqdPct, UptoPeriod, PeriodAmt
                                        , PeriodPct, MaxSecured, MaxUnSecured, MaxTotal, OtherSecured, CollateralRight, AmtSecured
                                        , Onlinelimit, AvailableAmt, TotalOutstand, ApprovedUser, MainComtType);
        }
        public static DataSet B_CUSTOMER_LIMIT_SUB_check_SubLimitID(string SubLimitID)
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_LIMIT_SUB_check_SubLimitID", SubLimitID);
        }
        public static DataSet B_CUSTOMER_LIMIT_SUB_Load_for_tab_ORTHER_DETAILS(string SubLimitID)
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_LIMIT_SUB_Load_for_tab_ORTHER_DETAILS", SubLimitID);
        }
        public static DataSet B_CUSTOMER_LIMIT_ENQUIRY(string MaHanMucCha, string MaHanMucCon, string CustomerName, string CustomerID, string CollateralType,
            string CollateralCode, string Currency, double FromIntLimitAmt, double ToItnLimitAmt)
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_LIMIT_ENQUIRY", MaHanMucCha, MaHanMucCon, CustomerName, CustomerID, CollateralType, CollateralCode,
                Currency, FromIntLimitAmt, ToItnLimitAmt);
        }
        public static string B_CUSTOMER_LIMIT_LoadCustomerName(string CustomerID)
        {
            if (sqldata.ndkExecuteDataset("B_CUSTOMER_LIMIT_LoadCustomerName", CustomerID).Tables[0].Rows.Count > 0)

            { return sqldata.ndkExecuteDataset("B_CUSTOMER_LIMIT_LoadCustomerName", CustomerID).Tables[0].Rows[0]["CustomerName"].ToString(); }
            else return null;
        }
        #endregion
        #region INPUT CUSTOMER_RIGHT_Load_SubLimitID
        public static DataSet B_CUSTOMER_RIGHT_Load_SubLimitID(string CustomerID)
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_RIGHT_Load_SubLimitID", CustomerID);
        }
        public static DataSet B_CUSTOMER_RIGHT_Load_CollateralTYpe(string SublimitID)
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_RIGHT_Load_CollateralTYpe", SublimitID);
        }
        public static void B_CUSTOMER_RIGHT_Insert_Update(string RightID, string RightNo, string CustomerID, string CustomerName, string MainLimitID, string SubLimitID, string CollateralTypeCode, string CollateralTypeName, string CollateralCode, string CollateralName
      , DateTime? ValidityDate, DateTime? ExpiryDate, string Notes, string ApprovedUser)
        {
            sqldata.ndkExecuteNonQuery("B_CUSTOMER_RIGHT_Insert_Update", RightID, RightNo, CustomerID, CustomerName, MainLimitID, SubLimitID, CollateralTypeCode, CollateralTypeName, CollateralCode, CollateralName
      , ValidityDate, ExpiryDate, Notes, ApprovedUser);
        }
        public static DataSet B_CUSTOMER_RIGHT_Load_RightID(string RightID)
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_RIGHT_Load_RightID", RightID);
        }
        public static DataSet B_CUSTOMER_RIGHT_Enquiry(string HanmucCha, string HanMucCon, string CustomerName, string CustomerID, string CollateralTypeCode,
            string CollateralCode, string RightID)
        {
            return sqldata.ndkExecuteDataset("B_CUSTOMER_RIGHT_Enquiry", HanmucCha, HanMucCon, CustomerName, CustomerID, CollateralTypeCode, CollateralCode
                ,RightID);
        }
        #endregion
        #region INPUT COLLATERAL INFORMATION
        public static DataSet B_COLLATERAL_INFO_GetCollateralStatus()
        {
            return sqldata.ndkExecuteDataset("B_COLLATERAL_INFO_GetStatus");
        }
        public static DataSet B_COLLATERAL_INFO_GetBankBranch()
        {
            return sqldata.ndkExecuteDataset("B_COLLATERAL_INFO_GetBankBranch");
        }
        public static DataSet B_COLLATERAL_INFO_LoadExistData(string RightID)
        {
            return sqldata.ndkExecuteDataset("B_COLLATERAL_INFO_CheckRightID", RightID);
        }
        public static void B_COLLATERAL_INFO_Insert_Update(string RightID, string CollateralInfoID, string CollateralTypeCode, string CollateralTypeName,
            string CollateralCode, string CollateralName, string ContingentAcctID, string ContingentAcctName, string Description, string Address, string CollateralStatusID,
            string CollateralStatusDesc, string CustomerID, string CustomreIDName, string Note, string CompanyStorageID, string CompanyStorageDesc, string ProductLimitID,string Currency
        , string CountryCode, string CountryName, decimal? NominalValue, decimal? MaxValue, decimal? ProvisionValue, decimal? ExecutionValue, decimal? AllocatedAmt, DateTime? ValueDate,
            DateTime? ExpiryDate, DateTime? ReviewDateFreq, string ApprovedUser)
        {
            sqldata.ndkExecuteNonQuery("B_COLLATERAL_INFO_Insert_Update", RightID, CollateralInfoID, CollateralTypeCode, CollateralTypeName, CollateralCode, CollateralName,
                ContingentAcctID, ContingentAcctName, Description, Address, CollateralStatusID, CollateralStatusDesc, CustomerID, CustomreIDName, Note,
                CompanyStorageID, CompanyStorageDesc, ProductLimitID, Currency, CountryCode, CountryName, NominalValue, MaxValue, ProvisionValue, ExecutionValue,
                AllocatedAmt, ValueDate, ExpiryDate, ReviewDateFreq, ApprovedUser);
        }
        public static DataSet B_COLLATERAL_INFO_LoadExistColl_InfoExists(string CollateralInfoID)
        {
            return sqldata.ndkExecuteDataset("B_COLLATERAL_INFO_LoadExistColl_InfoExists", CollateralInfoID);
        }
        public static DataSet B_COLLATERAL_INFO_Enquiry(string RightID, string CollateralInfoID, string CustomerName, string CustomerID, string CollateralType,
            string CollateralCode, string Currency, decimal FromNominalValue, decimal ToNominalValue, string ContingentAcctID)
        {
            return sqldata.ndkExecuteDataset("B_COLLATERAL_INFO_Enquiry", RightID, CollateralInfoID, CustomerName, CustomerID, CollateralType, CollateralCode,
                Currency, FromNominalValue, ToNominalValue, ContingentAcctID);
        }
        public static DataSet LoaContAcctFromDB(string CollateralTypeCode, string Currency)
        {
            return sqldata.ndkExecuteDataset("LoaContAcctFromDB", CollateralTypeCode, Currency);
        }
        public static void B_CONTINGENT_ENTRY_Insert_Update(string CollateralInfoID, string ContingentEntryID, string CustomerID, string CustomerAddress, string DocIDTaxCode
            , string DateOfIssue, string TransactionCode, string TransactionName, string DCMode, string DCName, string Currency, string AccountNo, string AccountName,
            decimal? Amount, decimal? DealRate, DateTime? ValueDate, string Narrative, string ApprovedUser)
        {
            sqldata.ndkExecuteNonQuery("B_CONTINGENT_ENTRY_Insert_Update", CollateralInfoID, ContingentEntryID, CustomerID, CustomerAddress, DocIDTaxCode, DateOfIssue, TransactionCode,
                TransactionName, DCMode, DCName, Currency, AccountNo, AccountName, Amount, DealRate, ValueDate, Narrative, ApprovedUser);
        }
        public static DataSet B_COLLATERAL_INFO_LoadCustomer_Info(string CustomerID)
        {
            return sqldata.ndkExecuteDataset("B_COLLATERAL_INFO_LoadCustomer_Info", CustomerID);
        }
        public static DataSet B_COLLATERAL_INFO_LoadCurrency_forEach_Customer(string CustomerID)
        {
            return sqldata.ndkExecuteDataset("B_COLLATERAL_INFO_LoadCurrency_forEach_Customer", CustomerID);
        }
        public static DataSet B_COLLATERAL_INFO_Load_ProductLimit(string CustomerID)
        {
            return sqldata.ndkExecuteDataset("B_COLLATERAL_INFO_Load_ProductLimit", CustomerID);
        }
        #endregion
        #region B_CHEQUE ISSUE
        public static DataSet B_CHEQUEISSUE_Load_ChequeStatus()
        {
            return sqldata.ndkExecuteDataset("B_CHEQUEISSUE_Load_ChequeStatus");
        }
        public static DataSet B_CHEQUEISSUE_Check_WorkingAcct(string AcctID, string Currency)
        {
            return sqldata.ndkExecuteDataset("B_CHEQUEISSUE_Check_WorkingAcct", AcctID, Currency);
        }
        public static void B_CHEQUEISSUE_Insert_Update(string  ChequeID,string WorkingAcctID,string ChequeType,string ChequeStatusID,string ChequeStatusDesc ,
            string Currency, DateTime? IssueDate, double Quantity, double ChequeNoStart, double ChequeNoEnd, double? NextTransCom, string Status, string ApprovedUser, string CustomerID)
        {
            sqldata.ndkExecuteNonQuery("B_CHEQUEISSUE_Insert_Update", ChequeID, WorkingAcctID, ChequeType, ChequeStatusID, ChequeStatusDesc, Currency, IssueDate
                , Quantity, ChequeNoStart, ChequeNoEnd, NextTransCom, Status, ApprovedUser, CustomerID);
        }
        public static DataSet B_CHEQUEISSUE_LoadChequeID(string ChequeID)
        {
            return sqldata.ndkExecuteDataset("B_CHEQUEISSUE_LoadChequeID", ChequeID);
        }
        public static void B_CHEQUEISSUE_Update_Status(string ChequeID, string Status)
        {
            sqldata.ndkExecuteNonQuery("B_CHEQUEISSUE_Update_Status", ChequeID, Status);
        }
        public static DataSet B_CHEQUEISSUE_Preview_2_Authorize()
        {
            return sqldata.ndkExecuteDataset("B_CHEQUEISSUE_Preview_2_Authorize");
        }
        public static DataSet B_CHEQUEISSUE_Load_ChequeType()
        {
            return sqldata.ndkExecuteDataset("B_CHEQUEISSUE_Load_ChequeType");
        }
        public static DataSet B_CHEQUEISSUE_Enquiry_Cheque(string ChequeID, string LoadWorkingAcct , string ChequeType, DateTime? Issueddate, double? ChequeNo)
        {
            return sqldata.ndkExecuteDataset("B_CHEQUEISSUE_Enquiry_Cheque", ChequeID, LoadWorkingAcct, ChequeType, Issueddate, ChequeNo);
        }
        #endregion
        #region CHEQUE _ WITHDRAWAL
        public static DataSet CHEQUE_WITHDRAWAL_LoadCustomerAcct(string Currency)
        {
            return sqldata.ndkExecuteDataset("CHEQUE_WITHDRAWAL_LoadCustomerAcct", Currency);
        }
        public static DataSet CHEQUE_WITHDRAWAL_LoadChequeType()
        {
            return sqldata.ndkExecuteDataset("CHEQUE_WITHDRAWAL_LoadChequeType");
        }
        public static DataSet CHEQUE_WITHDRAWAL_LoadAcctPaid(string CurrencyPaid)
        {
            return sqldata.ndkExecuteDataset("CHEQUE_WITHDRAWAL_LoadAcctPaid", CurrencyPaid);
        }
        public static void CHEQUE_WITHDRAWAL_Insert_Update(string ID, string CustomerID, string CustomerName, string Currency, string AccountCode, string AccountName
            , decimal? AmountLCY, decimal? OldBalance, decimal? NewBalance, string ChequeType, string ChequeDesc, decimal? ChequeNo, string TellerID
            , string CurrencyPaid, string AccountPaidCode, string AccountPaidName, decimal? DealRate, decimal AmountPaid, string WaiveCharge, string Narrative, string BeneficialName,
            string Address, string LegalID, DateTime? IssuedDate, string PlaceOfIssue, string Status)
        {
             sqldata.ndkExecuteNonQuery("CHEQUE_WITHDRAWAL_Insert_Update", ID, CustomerID, CustomerName, Currency, AccountCode, AccountName, AmountLCY, OldBalance
                , NewBalance, ChequeType, ChequeDesc, ChequeNo, TellerID, CurrencyPaid, AccountPaidCode, AccountPaidName, DealRate, AmountPaid, WaiveCharge,
                Narrative, BeneficialName, Address, LegalID, IssuedDate, PlaceOfIssue, Status);
        }
        public static DataSet CHEQUE_WITHDRAWAL_LoadPreviewList()
        {
            return sqldata.ndkExecuteDataset("CHEQUE_WITHDRAWAL_LoadPreviewList");
        }
        public static DataSet CHEQUE_WITHDRAWAL_LoadChequeID(string ID)
        {
            return sqldata.ndkExecuteDataset("CHEQUE_WITHDRAWAL_LoadChequeID",ID);
        }
        public static void CHEQUE_WITHDRAWAL_Update_Status(string ID, string Status, string AccountCode, string Currency, double NewCustBalance)
        {
            sqldata.ndkExecuteNonQuery("CHEQUE_WITHDRAWAL_Update_Status", ID, Status, AccountCode, Currency, NewCustBalance);
        }
        public static DataSet CHEQUE_WITHDRAWAL_Enquiry(string WithDrawalID, string WorkingAcct, double? ChequeNo, string CustomerID, string CustomerName
            , DateTime? WithdrawalDate, string ChequeType, string LegalID, double? FromAmount, double? ToAmount)
        {
            return sqldata.ndkExecuteDataset("CHEQUE_WITHDRAWAL_Enquiry", WithDrawalID, WorkingAcct, ChequeNo, CustomerID, CustomerName, WithdrawalDate, ChequeType
                , LegalID, FromAmount, ToAmount);
        }
        #endregion
        #region CHEQUE _ TRANSFER 
        public static string B_BMACODE_ID_3par_CHEQUE_TRANS(string refix, string flat, string Macode)
        {
            return sqldata.ndkExecuteDataset("B_BMACODE_ID_3par_CHEQUE_TRANS", refix, flat, Macode).Tables[0].Rows[0]["Code"].ToString(); 
        }
        public static void BCHEQUE_TRANSFER_Insert_Update(string ID, string Status, string CustomerID, string CustomerName, string DebitCurrency, string DebitAcctCode
            , string DebitAcctName, double? DebitAmount, double? OldCustBalance, double? NewCustBalance, string ChequeType, string ChequeDesc
            , double? ChequeNo, DateTime? DebitValueDate, string CreditCurrency, string CreditAcctCode, string CreditAcctName, decimal? DealRate, DateTime? ExposureDate, 
            double? AmtCreditForCust, DateTime? CreditValueDate, string WaiveCharge
            , string Narrative, string BeneficialName, string Address, string LegalID, DateTime? IssuedDate, string PlaceOfIssue, string ApprovedUser)
        {
            sqldata.ndkExecuteNonQuery("BCHEQUE_TRANSFER_Insert_Update", ID, Status, CustomerID, CustomerName, DebitCurrency, DebitAcctCode, DebitAcctName,
                DebitAmount, OldCustBalance, NewCustBalance, ChequeType, ChequeDesc
            , ChequeNo, DebitValueDate, CreditCurrency, CreditAcctCode, CreditAcctName, DealRate, ExposureDate, AmtCreditForCust, CreditValueDate, WaiveCharge
            , Narrative, BeneficialName, Address, LegalID, IssuedDate, PlaceOfIssue, ApprovedUser);
        }
        public static DataSet BCHEQUE_TRANSFER_LoadPreviewList()
        {
            return sqldata.ndkExecuteDataset("BCHEQUE_TRANSFER_LoadPreviewList");
        }
        public static DataRow BCHEQUE_TRANSFER_LoadTransferID_Data(string ID)
        {
            return sqldata.ndkExecuteDataset("BCHEQUE_TRANSFER_LoadTransferID_Data", ID).Tables[0].Rows[0];
        }
        public static void BCHEQUE_TRANSFER_Update_Status(string ID, string Status, string AccountCode, string debitCurrency, double? NewCustBalance)
        {
             sqldata.ndkExecuteNonQuery("BCHEQUE_TRANSFER_Update_Status", ID, Status, AccountCode, debitCurrency, NewCustBalance);
        }
        public static DataSet CHEQUE_TRANSFER_Enquiry(string TransferID, string WorkingAcct, double? ChequeNo, string CustomerID, string CustomerName
            , DateTime? TransferDate, string ChequeType, string LegalID, double? FromAmount, double? ToAmount)
        {
            return sqldata.ndkExecuteDataset("CHEQUE_TRANSFER_Enquiry", TransferID, WorkingAcct, ChequeNo, CustomerID, CustomerName, TransferDate, ChequeType
                , LegalID, FromAmount, ToAmount);
        }
        #endregion
        #region CHEQUE_STOP 
        public static DataSet CHEQUE_STOP_LoadAcct_FromChequeIssues(string AccountID)
        {
            return sqldata.ndkExecuteDataset("CHEQUE_STOP_LoadAcct_FromChequeIssues", AccountID);
        }
        public static void CHEQUE_STOP_Insert_Update(string AccountID, string Status, string CustomerID, string CustomerName, string Currency, string ReasonStopID,
            string ReasonStopDesc, double? FromChequeSerial, double? ToChequeSerial, double? NoOfLeave, string ChequeType,
            string ChequeDesc, double? FromAmount, double? ToAmount, string WaiveCharge, DateTime? ActiveDate, string ApprovedUser)
        {
            sqldata.ndkExecuteNonQuery("CHEQUE_STOP_Insert_Update", AccountID, Status, CustomerID, CustomerName, Currency, ReasonStopID, ReasonStopDesc, FromChequeSerial
                , ToChequeSerial, NoOfLeave, ChequeType, ChequeDesc, FromAmount, ToAmount, WaiveCharge, ActiveDate, ApprovedUser);
        }
        public static DataSet CHEQUE_STOP_LoadAcct_FromChequeStopPayment(string AccountID)
        {
            return sqldata.ndkExecuteDataset("CHEQUE_STOP_LoadAcct_FromChequeStopPayment", AccountID);
        }
        public static void CHEQUE_STOP_Update_Status(string AccountID, string Status)
        {
             sqldata.ndkExecuteNonQuery("CHEQUE_STOP_Update_Status", AccountID, Status);
        }
        public static DataSet CHEQUE_STOP_Preview_List()
        {
            return sqldata.ndkExecuteDataset("CHEQUE_STOP_Preview_List");
        }
        public static DataSet CHEQUE_STOP_Enquiry(string DocID, string WorkingAcct, double? ChequeNo, string CustomerID, string CustomerName, DateTime? ActiveDate,
            string ChequeType, string StoppingReason)
        {
            return sqldata.ndkExecuteDataset("CHEQUE_STOP_Enquiry", DocID, WorkingAcct, ChequeNo, CustomerID, CustomerName, ActiveDate, ChequeType, StoppingReason);
        }
        #endregion
        #region CHEQUE_ CANCLE_ STOP_ PAYMENT
        public static DataSet CHEQUE_CANCLE_STOP_check_exists(string AccountID)
        {
            return sqldata.ndkExecuteDataset("CHEQUE_CANCLE_STOP_check_exists", AccountID);
        }
        public static void CHEQUE_CANCLE_STOP_Insert_Update(string WorkingAcct, string status, double? SerialNo, string ChequeType, string ChequeDesc,
            DateTime? ActiveDate, string ApprovedUser)
        {
            sqldata.ndkExecuteNonQuery("CHEQUE_CANCLE_STOP_Insert_Update", WorkingAcct, status, SerialNo, ChequeType, ChequeDesc, ActiveDate, ApprovedUser);
        }
        public static DataSet CHEQUE_CANCLE_STOP_Preview_List()
        {
            return sqldata.ndkExecuteDataset("CHEQUE_CANCLE_STOP_Preview_List");
        }
        public static DataRow CHEQUE_CANCLE_STOP_LoadData(string AccountID, double SerialNo)
        {
            return sqldata.ndkExecuteDataset("CHEQUE_CANCLE_STOP_LoadData", AccountID, SerialNo).Tables[0].Rows[0];
        }
        public static void CHEQUE_CANCLE_STOP_Update_Status(string AccountID, double? SerialNo, string Status)
        {
            sqldata.ndkExecuteNonQuery("CHEQUE_CANCLE_STOP_Update_Status", AccountID, SerialNo, Status);
        }
        public static DataSet CHEQUE_CANCLE_STOP_Enquiry(string AccountID, double? SerialNo, string DocID, string CustomerID
            , string CustomerName, DateTime? ActiveDate, string ChequeType)
        {
            return sqldata.ndkExecuteDataset("CHEQUE_CANCLE_STOP_Enquiry", AccountID, SerialNo, DocID, CustomerID, CustomerName, ActiveDate, ChequeType);
        }
        #endregion
        #region CHEQUE_ RETURN
        public static DataSet B_CHEQUE_RETURN_LoadCheque_Data(string ChequeID, string WorkingAcct)
        {
            return sqldata.ndkExecuteDataset("B_CHEQUE_RETURN_LoadCheque_Data", ChequeID, WorkingAcct);
        }
        public static DataSet B_CHEQUE_RETURN_LoadCheque_Totalused(string ChequeID, string WorkingAcct)
        {
            return sqldata.ndkExecuteDataset("B_CHEQUE_RETURN_LoadCheque_Totalused", ChequeID, WorkingAcct);
        }
        public static DataSet B_CHEQUE_RETURN_LoadCheque_TotalStopped(string ChequeID, string WorkingAcct)
        {
            return sqldata.ndkExecuteDataset("B_CHEQUE_RETURN_LoadCheque_TotalStopped", ChequeID, WorkingAcct);
        }
        public static DataSet B_CHEQUE_RETURN_LoadCheque_TotalCancle(string ChequeID, string WorkingAcct)
        {
            return sqldata.ndkExecuteDataset("B_CHEQUE_RETURN_LoadCheque_TotalCancle", ChequeID, WorkingAcct);
        }
        public static void B_CHEQUE_RETURN_Insert_Update(string RefCheque, string Status,string WorkingAcct, double? TotalIssued, double? TotalUsed, double? TotalHeld
      , string ChequeNos, double? ChequeNoStart, double? ChequeNoEnd, double? PresentedCheque, double? TotalStopped, double? ReturnedCheque, string Narrative
      , string ApprovedUser)
        {
            sqldata.ndkExecuteNonQuery("B_CHEQUE_RETURN_Insert_Update", RefCheque,Status, WorkingAcct, TotalIssued, TotalUsed, TotalHeld, ChequeNos, ChequeNoStart
                , ChequeNoEnd, PresentedCheque, TotalStopped, ReturnedCheque, Narrative, ApprovedUser);
        }
        public static DataSet B_CHEQUE_RETURN_check_cheque_in_used(string ChequeType,double? ChequeReturned)
        {
            return sqldata.ndkExecuteDataset("B_CHEQUE_RETURN_check_cheque_in_used",ChequeType, ChequeReturned);
        }
        public static DataSet B_CHEQUE_RETURN_Preview_List(string RefCheque, double ? ReturnedCheque)
        {
            return sqldata.ndkExecuteDataset("B_CHEQUE_RETURN_Preview_List", RefCheque, ReturnedCheque);
        }
        public static void B_CHEQUE_RETURN_Update_Status(string RefCheque, double ReturnedCheque, string Status)
        {
            sqldata.ndkExecuteNonQuery("B_CHEQUE_RETURN_Update_Status", RefCheque, ReturnedCheque, Status);
        }
        public static DataSet B_CHEQUE_RETURN_Enquiry(string RefCheque, string WorkingAcct, string DocID,string CustomerId, string CustomerName ,
            DateTime? ActiveDate, string ChequeType ,double? ReturnedCheque)
        {
            return sqldata.ndkExecuteDataset("B_CHEQUE_RETURN_Enquiry", RefCheque,WorkingAcct, DocID, CustomerId, CustomerName, ActiveDate,ChequeType ,ReturnedCheque);
        }
        public static DataSet B_CHEQUE_RETURN_check_cheque_in_Returned(string ChequeType, double ChequeNo)
        {
            return sqldata.ndkExecuteDataset("B_CHEQUE_RETURN_check_cheque_in_Returned",ChequeType, ChequeNo);
        }
        #endregion
    }
}