using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dapper;
using BankProject.Entity.SavingAcc;
using System.Data;

namespace BankProject.DataProvider
{
    public class SavingAccountDAO
    {
        private Object lockObject = new Object();

        #region SQL statement

        private readonly string QUERY_GET_ALL_CATEGORY_FOR_SAVING_ACC = @"SELECT Code, NAME FROM BCATEGORY WHERE Type = 3  ORDER BY Code";
        private readonly string QUERY_GET_ALL_TERM = @"SELECT * FROM BINTEREST_TERM ORDER BY ID";
        private readonly string QUERY_GET_ALL_PRODUCT_TYPE = @"SELECT * FROM BPRODUCTTYPE WHERE SavingFunctionType = @SavingFunctionType ORDER BY ProductCode";
        //private readonly string QUERY_GET_WORKING_ACCOUNT_BY_CUSTOMERID = @"SELECT CustomerId, BankCode, BankAccount FROM BCUSTOMERS WHERE CustomerId = @CustomerId";
        private readonly string QUERY_GET_WORKING_ACCOUNT_BY_CUSTOMERID = @"SELECT AccountCode FROM BOPENACCOUNT WHERE 
                                                                            CustomerId = @CustomerId AND Currency = @Currency AND [Status] = 'AUT' AND [AccountStatus] = 'OPEN'";

        private readonly string QUERY_GET_WORKING_ACCOUNT_BY_CUSTOMERID_FOR_OPEN = @"SELECT AccountCode FROM BOPENACCOUNT WHERE 
                                                                            CustomerId = @CustomerId AND Currency = @Currency AND [Status] = 'AUT' AND [AccountStatus] = 'OPEN' AND  ProductLineID = '20003'";

        private readonly string QUERY_GET_ALL_PRODUCT_TYPE_BY_CODE = @"SELECT * FROM BPRODUCTTYPE WHERE ProductCode = @ProductCode";

        private readonly string QUERY_GET_ARREAR_SAVING_ACCOUNT_BY_ID = @"SELECT [RefId],[Status],[CustomerId],[CustomerName]
                                                                              ,[AccCategory],[AccTitle],[ShortTitle],[Currency],[ProductLineId],[JointACHolderId],[JointACHolderName]
                                                                              ,[RelationshipId],[RelationshipName],[Note],[AccountOfferCode],[AZProductCode],[AZPrincipal]
                                                                              ,[AZValueDate],[AZTerm],[AZOriginalMaturityDate],[AZInterestRate],[AZWorkingAccount],[AZMaturityInstr]
                                                                              ,[AZRolloverPR],[TTNo],[TTAccNo],[TTCurrency],[TTForTeller],[TTDebitAccount],[TTNarative],[TTDealRate]
                                                                              ,[CloseStatus],[CloseValueDate],[CloseAmoutLCY],[CloseAmountFCY],[CloseNarative],[CloseForTeller],[CloseCreditAccount]
                                                                        FROM [dbo].[BSAVING_ACC_ARREAR]
                                                                        WHERE [RefId] = @RefId";

        private readonly string QUERY_GET_ARREAR_SAVING_ACCOUNT_BY_STATUS = @"SELECT [RefId],[Status],[CustomerId],[CustomerName]
                                                                              ,[AccCategory],[Currency],[ProductLineId],[AZPrincipal], [AZWorkingAccount]
                                                                        FROM [dbo].[BSAVING_ACC_ARREAR]
                                                                        WHERE [Status] = @Status
                                                                        ORDER BY [CreatedDate] Desc";

        private readonly string INSERT_ARREAR_SAVING_ACCOUNT = @"INSERT INTO [dbo].[BSAVING_ACC_ARREAR]
                                                                    ([RefId],[Status],[CustomerId],[CustomerName],
                                                                    [AccCategory],[AccTitle],[ShortTitle],[Currency],[ProductLineId],
                                                                    [JointACHolderId],[JointACHolderName],[RelationshipId],[RelationshipName],[Note],[AccountOfferCode],
                                                                    [AZProductCode],[AZPrincipal],[AZValueDate],[AZTerm],[AZOriginalMaturityDate],[AZInterestRate],[AZWorkingAccount],[AZMaturityInstr],
                                                                    [AZRolloverPR],[TTNo],[TTAccNo],[TTCurrency],[TTForTeller],[TTDebitAccount] ,[TTNarative],[TTDealRate],[CreatedBy],[CreatedDate])
                                                                VALUES
                                                                    (@RefId, @Status,@CustomerId,@CustomerName,@AccCategory,@AccTitle, @ShortTitle,@Currency,
                                                                    @ProductLineId,@JointACHolderId,@JointACHolderName,@RelationshipId, @RelationshipName, @Note, 
                                                                    @AccountOfferCode,@AZProductCode,@AZPrincipal,@AZValueDate, @AZTerm,@AZOriginalMaturityDate,@AZInterestRate,
                                                                    @AZWorkingAccount,@AZMaturityInstr,@AZRolloverPR,@TTNo,@TTAccNo,@TTCurrency,@TTForTeller,@TTDebitAccount,@TTNarative, @TTDealRate, @CreatedBy, getdate())";

        private readonly string UPDATE_ARREAR_SAVING_ACCOUNT = @"UPDATE [dbo].[BSAVING_ACC_ARREAR]
                                                                   SET Status = @Status, [CustomerId] = @CustomerId,[CustomerName] = @CustomerName,[AccCategory] = @AccCategory,[AccTitle] = @AccTitle,[ShortTitle] = @ShortTitle,[Currency] = @Currency
                                                                      ,[ProductLineId] = @ProductLineId,[JointACHolderId] = @JointACHolderId,[JointACHolderName] = @JointACHolderName,[RelationshipId] = @RelationshipId,[RelationshipName] = @RelationshipName
                                                                      ,[Note] = @Note,[AccountOfferCode] = @AccountOfferCode,[AZProductCode] = @AZProductCode,[AZPrincipal] = @AZPrincipal
                                                                      ,[AZValueDate] = @AZValueDate,[AZTerm] = @AZTerm,[AZOriginalMaturityDate] = @AZOriginalMaturityDate,[AZInterestRate] = @AZInterestRate
                                                                      ,[AZWorkingAccount] = @AZWorkingAccount,[AZMaturityInstr] = @AZMaturityInstr,[AZRolloverPR] = @AZRolloverPR,[TTNo] = @TTNo,[TTAccNo] = @TTAccNo
                                                                      ,[TTCurrency] = @TTCurrency,[TTForTeller] = @TTForTeller,[TTDebitAccount] = @TTDebitAccount,[TTNarative] = @TTNarative, [TTDealRate] = @TTDealRate
                                                                      ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                                                 WHERE [RefId] = @RefId";

        private readonly string AUTHORISE_ARREAR_SAVING_ACCOUNT = @"UPDATE [dbo].[BSAVING_ACC_ARREAR]
                                                                               SET Status = @Status ,AZPreMaturityDate = AZValueDate, AZMaturityDate = AZOriginalMaturityDate 
                                                                                ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                                                                WHERE [RefId] = @RefId";

        private readonly string REVERSE_ARREAR_SAVING_ACCOUNT = @"UPDATE [dbo].[BSAVING_ACC_ARREAR]
                                                                               SET Status = @Status ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                                                                WHERE [RefId] = @RefId";

        private readonly string QUERY_GET_ALL_CURRENCY = @"SELECT * FROM BCURRENCY ORDER BY Code";

        private readonly string INSERT_PERIODIC_SAVING_ACCOUNT = @"INSERT INTO [dbo].[BSAVING_ACC_PERIODIC]
                                                                    ([RefId],[Status],[CustomerId],[CustomerName],
                                                                    [AccCategory],[AccTitle],[ShortTitle],[Currency],[ProductLineId],
                                                                    [JointACHolderId],[JointACHolderName],[RelationshipId],[RelationshipName],[Note],[AccountOfferCode],
                                                                    [AZProductCode],[AZPrincipal],[AZValueDate],[AZTerm],[AZOriginalMaturityDate],[AZInterestRate],[AZWorkingAccount],[AZMaturityInstr],
                                                                    [AZIsSchedule],[AZScheduleType],[AZFrequency],[TTNo],[TTAccNo],[TTCurrency],[TTForTeller],[TTDebitAccount] ,[TTNarative],[TTDealRate],[CreatedBy],[CreatedDate])
                                                                VALUES
                                                                    (@RefId, @Status,@CustomerId,@CustomerName,@AccCategory,@AccTitle, @ShortTitle,@Currency,
                                                                    @ProductLineId,@JointACHolderId,@JointACHolderName,@RelationshipId, @RelationshipName, @Note, 
                                                                    @AccountOfferCode,@AZProductCode,@AZPrincipal,@AZValueDate, @AZTerm,@AZOriginalMaturityDate,@AZInterestRate,
                                                                    @AZWorkingAccount,@AZMaturityInstr,@AZIsSchedule,@AZScheduleType,@AZFrequency,@TTNo,@TTAccNo,@TTCurrency,@TTForTeller,@TTDebitAccount,@TTNarative, @TTDealRate, @CreatedBy, getdate())";

        private readonly string UPDATE_PERIODIC_SAVING_ACCOUNT = @"UPDATE [dbo].[BSAVING_ACC_PERIODIC]
                                                                   SET Status = @Status, [CustomerId] = @CustomerId,[CustomerName] = @CustomerName,[AccCategory] = @AccCategory,[AccTitle] = @AccTitle,[ShortTitle] = @ShortTitle,[Currency] = @Currency
                                                                      ,[ProductLineId] = @ProductLineId,[JointACHolderId] = @JointACHolderId,[JointACHolderName] = @JointACHolderName,[RelationshipId] = @RelationshipId,[RelationshipName] = @RelationshipName
                                                                      ,[Note] = @Note,[AccountOfferCode] = @AccountOfferCode,[AZProductCode] = @AZProductCode,[AZPrincipal] = @AZPrincipal
                                                                      ,[AZValueDate] = @AZValueDate,[AZTerm] = @AZTerm,[AZOriginalMaturityDate] = @AZOriginalMaturityDate,[AZInterestRate] = @AZInterestRate
                                                                      ,[AZWorkingAccount] = @AZWorkingAccount,[AZMaturityInstr] = @AZMaturityInstr
                                                                      ,[AZIsSchedule] = @AZIsSchedule,[AZScheduleType] = @AZScheduleType,[AZFrequency] = @AZFrequency
                                                                      ,[TTNo] = @TTNo,[TTAccNo] = @TTAccNo,[TTCurrency] = @TTCurrency,[TTForTeller] = @TTForTeller,[TTDebitAccount] = @TTDebitAccount,[TTNarative] = @TTNarative, [TTDealRate] = @TTDealRate
                                                                      ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                                                 WHERE [RefId] = @RefId";

        private readonly string QUERY_GET_PERIODIC_SAVING_ACCOUNT_BY_ID = @"SELECT [RefId],[Status],[CloseStatus],[CustomerId],[CustomerName]
                                                                              ,[AccCategory],[AccTitle],[ShortTitle],[Currency],[ProductLineId],[JointACHolderId],[JointACHolderName]
                                                                              ,[RelationshipId],[RelationshipName],[Note],[AccountOfferCode],[AZProductCode],[AZPrincipal]
                                                                              ,[AZValueDate],[AZTerm],[AZOriginalMaturityDate],[AZInterestRate],[AZWorkingAccount],[AZMaturityInstr]
                                                                              ,[AZIsSchedule],[AZScheduleType],[AZFrequency],[TTNo],[TTAccNo],[TTCurrency],[TTForTeller],[TTDebitAccount],[TTNarative],[TTDealRate]
                                                                              ,[CloseStatus],[CloseValueDate],[CloseAmoutLCY],[CloseAmountFCY],[CloseNarative],[CloseForTeller],[CloseCreditAccount]
                                                                        FROM [dbo].[BSAVING_ACC_PERIODIC]
                                                                        WHERE [RefId] = @RefId";

        private readonly string QUERY_GET_PERIODIC_SAVING_ACCOUNT_BY_STATUS = @"SELECT [RefId],[Status],[CustomerId],[CustomerName]
                                                                              ,[AccCategory],[Currency],[ProductLineId],[AZPrincipal], [AZWorkingAccount]
                                                                        FROM [dbo].[BSAVING_ACC_PERIODIC]
                                                                        WHERE [Status] = @Status
                                                                        ORDER BY [CreatedDate] Desc";

        private readonly string AUTHORISE_PERIODIC_SAVING_ACCOUNT = @"UPDATE [dbo].[BSAVING_ACC_PERIODIC]
                                                                               SET Status = @Status ,AZPreMaturityDate = AZValueDate, AZMaturityDate = AZOriginalMaturityDate 
                                                                                ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                                                                WHERE [RefId] = @RefId";

        private readonly string REVERSE_PERIODIC_SAVING_ACCOUNT = @"UPDATE [dbo].[BSAVING_ACC_PERIODIC]
                                                                               SET Status = @Status ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                                                                WHERE [RefId] = @RefId";

        private readonly string GET_ARREAR_BY_STATUS = @"select 'arrear' as FromTable,[Status],[RefId],[AccCategory],CustomerId,[Currency],[ProductLineId],[AZPrincipal],[CustomerId] from BSAVING_ACC_ARREAR
                                                                        where ([Status]=@Status or @Status='') and ([RefId] = @RefId or ''=@RefId) and ([AccCategory]=@AccCategory or ''=@AccCategory)
                                                                        and ([Currency]=@Currency or ''=@Currency) and ([ProductLineId]=@ProductLineId or ''=@ProductLineId)
                                                                        and ([AZPrincipal]>=@PrincipalFrom or 0=@PrincipalFrom) and ([AZPrincipal]<=@PrincipalTo or 0=@PrincipalTo)
                                                                        and ([CustomerId]=@CustomerId or ''=@CustomerId)";

        private readonly string GET_PERIODIC_BY_STATUS = @"select 'periodic' as FromTable,[Status],[RefId],[AccCategory],CustomerId,[Currency],[ProductLineId],[AZPrincipal],[AZWorkingAccount] from BSAVING_ACC_PERIODIC
                                                                        where ([Status]=@Status or @Status='') and ([RefId] = @RefId or ''=@RefId) and ([AccCategory]=@AccCategory or ''=@AccCategory)
                                                                        and ([Currency]=@Currency or ''=@Currency) and ([ProductLineId]=@ProductLineId or ''=@ProductLineId)
                                                                        and ([AZPrincipal]>=@PrincipalFrom or 0=@PrincipalFrom) and ([AZPrincipal]<=@PrincipalTo or 0=@PrincipalTo)
                                                                        and ([CustomerId]=@CustomerId or ''=@CustomerId)";

        string CLOSE_UPDATE_ARREAR_SAVING_ACCOUNT = @"UPDATE [dbo].[BSAVING_ACC_ARREAR]
                                                                            SET [CloseStatus] = @CloseStatus ,[CloseValueDate] = @CloseValueDate,
                                                                                [CloseAmoutLCY] = @CloseAmoutLCY, [CloseAmountFCY] = @CloseAmountFCY
                                                                                ,[CloseNarative] = @CloseNarative, [CloseForTeller] = @CloseForTeller,
                                                                                [CloseCreditAccount] = @CloseCreditAccount
                                                                                WHERE [RefId] = @RefId";
        string CLOSE_UPDATE_PERIODIC_SAVING_ACCOUNT = @"UPDATE [dbo].[BSAVING_ACC_PERIODIC]
                                                                            SET [CloseStatus] = @CloseStatus ,[CloseValueDate] = @CloseValueDate, 
                                                                                [CloseAmoutLCY] = @CloseAmoutLCY, [CloseAmountFCY] = @CloseAmountFCY
                                                                                ,[CloseNarative] = @CloseNarative, [CloseForTeller] = @CloseForTeller,
                                                                                [CloseCreditAccount] = @CloseCreditAccount
                                                                                WHERE [RefId] = @RefId";

        private readonly string QUERY_GET_ARREAR_PERIODIC_SAVING_ACCOUNT_BY_HOLD = @"SELECT 'periodic' as FromTable,[CreatedDate],[RefId],[Status],[CustomerId],[CustomerName]
                                                                              ,[AccCategory],[Currency],[ProductLineId],[AZPrincipal], [AZWorkingAccount],
                                                                        CASE WHEN [CloseAmoutLCY] = 0 or  [CloseAmoutLCY] is null THEN [CloseAmountFCY] ELSE [CloseAmoutLCY] END AS TotalAmt
                                                                        FROM [dbo].[BSAVING_ACC_PERIODIC]
                                                                        WHERE [Status] = @Status and [CloseStatus]=@CloseStatus and  [Hold] = @Hold
                                                                        Union  
                                                                        SELECT 'arrear' as FromTable,[CreatedDate],[RefId],[Status],[CustomerId],[CustomerName]
                                                                              ,[AccCategory],[Currency],[ProductLineId],[AZPrincipal], [AZWorkingAccount],
                                                                         CASE WHEN [CloseAmoutLCY] = 0 or  [CloseAmoutLCY] is null THEN [CloseAmountFCY] ELSE [CloseAmoutLCY] END AS TotalAmt
                                                                        FROM [dbo].[BSAVING_ACC_ARREAR]
                                                                        WHERE [Status] = @Status and [CloseStatus]=@CloseStatus and  [Hold] = @Hold

                                                                        ORDER BY [CreatedDate] Desc";

        private readonly string QUERY_GET_ARREAR_PERIODIC_SAVING_ACCOUNT_BY_CLOSESTATUS = @"SELECT 'periodic' as FromTable,[CreatedDate],[RefId],[Status],[CustomerId],[CustomerName]
                                                                              ,[AccCategory],[Currency],[ProductLineId],[AZPrincipal], [AZWorkingAccount]
                                                                        FROM [dbo].[BSAVING_ACC_PERIODIC]
                                                                        WHERE [Status] = @Status and [CloseStatus]=@CloseStatus
                                                                        Union  
                                                                        SELECT 'arrear' as FromTable,[CreatedDate],[RefId],[Status],[CustomerId],[CustomerName]
                                                                              ,[AccCategory],[Currency],[ProductLineId],[AZPrincipal], [AZWorkingAccount]
                                                                        FROM [dbo].[BSAVING_ACC_ARREAR]
                                                                        WHERE [Status] = @Status and [CloseStatus]=@CloseStatus

                                                                        ORDER BY [CreatedDate] Desc";

        private readonly string QUERY_GET_INTEREST_SAVING_ACCOUNT_BY_ID = @"SELECT [StartDate],[EndDate],
                                                                            CASE WHEN [NonTermInterestAmt] =0  THEN [TermInterestAmt] ELSE [NonTermInterestAmt] END AS InterestAmt
                                                                            ,CASE WHEN [TermInterestAmt] =0  THEN [NonInterestRate] ELSE [InterestRate] END AS InterestRate
                                                                        from dbo.BSAVING_ACC_INTEREST
                                                                        WHERE [RefId] = @RefId
                                                                        ORDER BY [EndDate] Desc";

        private readonly string QUERY_GET_ALL_WORKING_ACCOUNT = @"SELECT [ID] ,[AccountCode],[CustomerID],[CustomerType],[CustomerName] ,[ProductLineID]
                                                                        ,[Currency]
                                                                        from [dbo].[BOPENACCOUNT]
                                                                        WHERE [Status] = 'AUT' and [AccountStatus] = 'OPEN' AND CategoryType = 2
                                                                        ORDER BY [ID] Desc";

        private readonly string QUERY_GET_ALL_ACCOUNT_OPEN_BY_ID = @"SELECT [ID] ,[AccountCode],[CustomerID],[CustomerType],[ProductLineID]
                                                                        ,[Currency],[CustomerName],[AccountOfficerID],[JoinHolderID],[ActualBallance],[ClearedBallance]
                                                                        from [dbo].[BOPENACCOUNT]
                                                                        WHERE [ID] = @ID";

        private readonly string QUERY_INTERNAL_BANK_ACCOUNT_BY_CURRENCY = @"SELECT [Code], [Account],[Currency]
                                                                            FROM [dbo].[BINTERNALBANKACCOUNT]
                                                                            WHERE [Currency] = @Currency
                                                                            UNION 
                                                                            SELECT 0, [AccountCode] as Account, [Currency]
                                                                            FROM BOPENACCOUNT 
                                                                            WHERE (CategoryType = '1' OR CategoryType = '2') AND ([CustomerID]=@CustomerId) AND [Currency] = @Currency AND [Status] = 'AUT' AND AccountStatus= 'OPEN'";
        #endregion

        private SqlDataProvider DataProvider
        {
            get { return new SqlDataProvider(); }
        }

        #region Get ref data

        public string GenerateDepositeCode(SavingAccFunc savingAccFun)
        {
            lock (lockObject)
            {
                using (var conn = new SqlConnection(DataProvider.ConnectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand("[B_SAVING_ACC_GET_NEWID]", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("SavingAccFunc", savingAccFun.ToString());
                        return command.ExecuteScalar().ToString();
                    }
                }
            }            
        }

        public IList<InternalBankAccount> GetInternalBankAccountByCurrency(string currency, string customerId)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<InternalBankAccount>(QUERY_INTERNAL_BANK_ACCOUNT_BY_CURRENCY, new { Currency = currency, CustomerId = customerId }).ToList();
            }
        }
        public IList<InternalBankAccount> GetAllDebitAccountCurrency(string currency)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<InternalBankAccount>("SELECT [Code], [Account],[Currency] FROM [dbo].[BINTERNALBANKACCOUNT] WHERE [Currency] = @Currency", new { Currency = currency }).ToList();
            }
        }
        public IList<AccountOpen> GetAllWorkingAccount()
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<AccountOpen>(QUERY_GET_ALL_WORKING_ACCOUNT).ToList();
            }
        }
        public AccountOpen GetAccountOpenById(string id)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<AccountOpen>(QUERY_GET_ALL_ACCOUNT_OPEN_BY_ID, new { ID = id }).FirstOrDefault();
            }
        }

        public IList<Currency> GetAllCurrency()
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<Currency>(QUERY_GET_ALL_CURRENCY).ToList();
            }
        }

        public IList<Category> GetAllCategoryForSavingAcc()
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<Category>(QUERY_GET_ALL_CATEGORY_FOR_SAVING_ACC).ToList();
            }
        }

        public IList<Term> GetAllTerm()
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<Term>(QUERY_GET_ALL_TERM).ToList();
            }
        }

        public IList<ProductType> GetAllProductType(string savingFunctionType)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<ProductType>(QUERY_GET_ALL_PRODUCT_TYPE, new { SavingFunctionType  = savingFunctionType}).ToList();
            }
        }

        public ProductType GetProductTypeByCode(string productCode)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<ProductType>(QUERY_GET_ALL_PRODUCT_TYPE_BY_CODE, new { ProductCode = productCode }).FirstOrDefault();
            }
        }

        public IList<WorkingBankAccount> GetWorkingBankAccountByCustomerId(string customerId,string currency)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<WorkingBankAccount>(QUERY_GET_WORKING_ACCOUNT_BY_CUSTOMERID, new { 
                    CustomerId = customerId ,
                    Currency = currency
                }).ToList();
            }
        }

        public IList<WorkingBankAccount> GetWorkingBankAccountByCustomerIdForOpen(string customerId, string currency)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<WorkingBankAccount>(QUERY_GET_WORKING_ACCOUNT_BY_CUSTOMERID_FOR_OPEN, new
                {
                    CustomerId = customerId,
                    Currency = currency
                }).ToList();
            }
        }


        public DataTable GetAllProductLinesForSavingAcc()
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(@"SELECT ProductId, ProductId + ' - ' +  Description as Description FROM BPRODUCTLINE WHERE Type = 3", conn))
                {                    
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }

        public DataTable GetAllProductLinesForDiscounted()
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(@"SELECT ProductId, ProductId + ' - ' +  Description as Description FROM BPRODUCTLINE WHERE Type = 5", conn))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }

        public DataTable GetAllAuthorisedCustomer()
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(@"SELECT CustomerID,[GBStreet],[DocID],[DocIssueDate], CustomerID + ' - ' + GBFullName as CustomerName FROM BCUSTOMER_INFO WHERE Status = 'AUT'", conn))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }
        public DataTable GetAuthorisedCustomerById(string id)
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(@"SELECT CustomerID,GBShortName,GBFullName,[GBStreet],[DocID],[DocIssueDate], CustomerID + ' - ' + GBFullName as CustomerName FROM BCUSTOMER_INFO WHERE Status = 'AUT' AND CustomerID=@CustomerID", conn))
                {
                    command.Parameters.AddWithValue("CustomerID", id);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }

        #endregion

        #region Arrear Saving account

        public bool CreateNewArrearSavingAccount(ArrearSavingAccount arrearSavingAccount)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(INSERT_ARREAR_SAVING_ACCOUNT, arrearSavingAccount) > 0;
            }
        }

        public bool UpdateArrearSavingAccount(ArrearSavingAccount arrearSavingAccount)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(UPDATE_ARREAR_SAVING_ACCOUNT, arrearSavingAccount) > 0;
            }
        }

        public ArrearSavingAccount GetArrearSavingAccountById(string refId)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<ArrearSavingAccount>(QUERY_GET_ARREAR_SAVING_ACCOUNT_BY_ID, new { RefId = refId }).FirstOrDefault();
            }
        }

        public bool CheckArrearSavingAccountExist(string refId)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<bool>("SELECT 1 FROM BSAVING_ACC_ARREAR WHERE RefId = @RefId", new { RefId = refId }).Any();
            }
        }

        public DataTable GetArrearSavingAccountByStatus(AuthoriseStatus status)
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(QUERY_GET_ARREAR_SAVING_ACCOUNT_BY_STATUS, conn))
                {
                    command.Parameters.AddWithValue("Status", status.ToString());
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }

        public bool ApproveArrearSavingAccount(string refId, string updatedBy)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(AUTHORISE_ARREAR_SAVING_ACCOUNT, new
                {
                    Status = AuthoriseStatus.AUT.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }

        public bool ReverseArrearSavingAccount(string refId, string updatedBy)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(REVERSE_ARREAR_SAVING_ACCOUNT, new
                {
                    Status = AuthoriseStatus.REV.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }

        #endregion

        #region Periodic Saving account

        public bool CreateNewPeriodicSavingAccount(PeriodicSavingAccount periodicSavingAccount)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(INSERT_PERIODIC_SAVING_ACCOUNT, periodicSavingAccount) > 0;
            }
        }

        public bool UpdatePeriodicSavingAccount(PeriodicSavingAccount periodicSavingAccount)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(UPDATE_PERIODIC_SAVING_ACCOUNT, periodicSavingAccount) > 0;
            }
        }

        public PeriodicSavingAccount GetPeriodicSavingAccountById(string refId)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<PeriodicSavingAccount>(QUERY_GET_PERIODIC_SAVING_ACCOUNT_BY_ID, new { RefId = refId }).FirstOrDefault();
            }
        }

        public bool CheckPeriodicSavingAccountExist(string refId)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<bool>("SELECT 1 FROM [BSAVING_ACC_PERIODIC] WHERE RefId = @RefId", new { RefId = refId }).Any();
            }
        }

        public DataTable GetPeriodicSavingAccountByStatus(AuthoriseStatus status)
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(QUERY_GET_PERIODIC_SAVING_ACCOUNT_BY_STATUS, conn))
                {
                    command.Parameters.AddWithValue("Status", status.ToString());
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }

        public bool ApprovePeriodicSavingAccount(string refId, string updatedBy)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(AUTHORISE_PERIODIC_SAVING_ACCOUNT, new
                {
                    Status = AuthoriseStatus.AUT.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }

        public bool ReversePeriodicSavingAccount(string refId, string updatedBy)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(REVERSE_PERIODIC_SAVING_ACCOUNT, new
                {
                    Status = AuthoriseStatus.REV.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }

        #endregion

        #region Discounted
        public bool CreateNewDiscountedAccount(DiscountedAccount discountedAccount)
        {
            string INSERT_DISCOUNTED_ACCOUNT = @"INSERT INTO [dbo].[BSAVING_ACC_DISCOUNTED]
                                                                    ([refId],[LDId],[Status],[WorkingAccId],[CustomerId],[AmmountLCY],[AmountFCY],[Narrative],[DealRate]
                                                                      ,[PaymentCCY],[ForTeller],[DebitAccount],[TDCustomerId],[TDJoinHolderId],[TDProductLineId],[TDCurrency]
                                                                      ,[TDAmmount],[TDValueDate],[TDBusDayDate],[TDTerm],[TDFinalMatDate],[TDInterestRate],[TDTotalIntamt],[TDWorkingAccountId],[TDWorkingAccountName]
                                                                      ,[TDAccountOfficerId],[DPDrAccountId],[DPDrAccountName],[DPAmountLCY],[DPAmountFCY],[DPNarrative],[DPPaymentCcy],[DPForTeller],[DPCreditAccount]
                                                                      ,[DPExchRate],[CreatedBy],[CreatedDate])
                                                                VALUES
                                                                    (@refId,@LDId, @Status,@WorkingAccId,@CustomerId, @AmmountLCY, @AmountFCY, @Narrative, @DealRate
                                                                      , @PaymentCCY, @ForTeller, @DebitAccount, @TDCustomerId, @TDJoinHolderId, @TDProductLineId, @TDCurrency
                                                                      , @TDAmmount, @TDValueDate, @TDBusDayDate, @TDTerm, @TDFinalMatDate, @TDInterestRate, @TDTotalIntamt, @TDWorkingAccountId,@TDWorkingAccountName
                                                                      , @TDAccountOfficerId, @DPDrAccountId,@DPDrAccountName, @DPAmountLCY, @DPAmountFCY, @DPNarrative, @DPPaymentCcy, @DPForTeller, @DPCreditAccount
                                                                      , @DPExchRate, @CreatedBy, getdate())";

            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(INSERT_DISCOUNTED_ACCOUNT, discountedAccount) > 0;
            }
        }

        public bool UpdateDiscountedAccount(DiscountedAccount discountedAccount)
        {
            string UPDATE_DISCOUNTED_ACCOUNT = @"UPDATE [dbo].[BSAVING_ACC_DISCOUNTED]
                                                                   SET [LDId]=@LDId,[Status]= @Status,[WorkingAccId]=@WorkingAccId,[CustomerId]= @CustomerId,[AmmountLCY]= @AmmountLCY,[AmountFCY]= @AmountFCY,[Narrative]= @Narrative
                                                                      ,[DealRate]= @DealRate,[PaymentCCY]= @PaymentCCY,[ForTeller]= @ForTeller
                                                                      ,[DebitAccount]= @DebitAccount,[TDCustomerId]= @TDCustomerId,[TDJoinHolderId]= @TDJoinHolderId
                                                                      ,[TDProductLineId]= @TDProductLineId,[TDCurrency]= @TDCurrency,[TDAmmount]= @TDAmmount
                                                                      ,[TDValueDate]= @TDValueDate,[TDBusDayDate]= @TDBusDayDate,[TDTerm]= @TDTerm
                                                                      ,[TDFinalMatDate]= @TDFinalMatDate,[TDInterestRate]= @TDInterestRate,[TDTotalIntamt]= @TDTotalIntamt
                                                                      ,[TDWorkingAccountId]= @TDWorkingAccountId,[TDWorkingAccountName]= @TDWorkingAccountName,[TDAccountOfficerId]= @TDAccountOfficerId
                                                                      ,[DPDrAccountId]= @DPDrAccountId,[DPDrAccountName]= @DPDrAccountName
                                                                      ,[DPAmountLCY]= @DPAmountLCY,[DPAmountFCY]= @DPAmountFCY,[DPNarrative]= @DPNarrative
                                                                      ,[DPPaymentCcy]= @DPPaymentCcy,[DPForTeller]= @DPForTeller,[DPCreditAccount]= @DPCreditAccount
                                                                      ,[DPExchRate]= @DPExchRate,[UpdatedBy]= @UpdatedBy
                                                                      ,[UpdatedDate]= getdate()  WHERE [refId] = @refId";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(UPDATE_DISCOUNTED_ACCOUNT, discountedAccount) > 0;
            }
        }

        public DiscountedAccount GetDiscountedAccountById(string refId)
        {
            string GET_DISCOUNTED_ACCOUNT = @"SELECT [refId],[LDId],[Status],[WorkingAccId],[CustomerId],[AmmountLCY],[AmountFCY],[Narrative],[DealRate]
                                                                      ,[CloseStatus],[CloseDate],[CloseRateVDate],[CloseInterest],[CloseDealRate],[CloseTeller],[CloseNarrative],[CloseAmountLCY],[CloseAmountFCY],[CloseCurrency]
                                                                      ,[PaymentCCY],[ForTeller],[DebitAccount],[TDCustomerId],[TDJoinHolderId],[TDProductLineId],[TDCurrency]
                                                                      ,[TDAmmount],[TDValueDate],[TDBusDayDate],[TDTerm],[TDFinalMatDate],[TDInterestRate],[TDTotalIntamt],[TDWorkingAccountId],[TDWorkingAccountName]
                                                                      ,[TDAccountOfficerId],[DPDrAccountId],[DPDrAccountName],[DPAmountLCY],[DPAmountFCY],[DPNarrative],[DPPaymentCcy],[DPForTeller],[DPCreditAccount]
                                                                      ,[DPExchRate],[CreatedBy],[CreatedDate] FROM [dbo].[BSAVING_ACC_DISCOUNTED] WHERE [refId] = @refId";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<DiscountedAccount>(GET_DISCOUNTED_ACCOUNT, new { RefId = refId }).FirstOrDefault();
            }
        }

        public bool CheckDiscountedAccountExist(string refId)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<bool>("SELECT 1 FROM BSAVING_ACC_DISCOUNTED WHERE RefId = @RefId", new { RefId = refId }).Any();
            }
        }

        public DataTable GetDiscountedAccountByStatus(AuthoriseStatus status)
        {
            string GET_DISCOUNTED_ACCOUNT_BY_STATUS = @"SELECT [refId],[LDId],[Status],[WorkingAccId],[CustomerId],[AmmountLCY],[AmountFCY],[Narrative],[DealRate]
                                                                      ,[PaymentCCY],[ForTeller],[DebitAccount],[TDCustomerId],[TDJoinHolderId],[TDProductLineId],[TDCurrency]
                                                                      ,[TDAmmount],[TDValueDate],[TDBusDayDate],[TDTerm],[TDFinalMatDate],[TDInterestRate],[TDTotalIntamt],[TDWorkingAccountId],[TDWorkingAccountName]
                                                                      ,[TDAccountOfficerId],[DPDrAccountId],[DPDrAccountName],[DPAmountLCY],[DPAmountFCY],[DPNarrative],[DPPaymentCcy],[DPForTeller],[DPCreditAccount]
                                                                      ,[DPExchRate],[CreatedBy],[CreatedDate] FROM [dbo].[BSAVING_ACC_DISCOUNTED] WHERE [Status] = @Status";

            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(GET_DISCOUNTED_ACCOUNT_BY_STATUS, conn))
                {
                    command.Parameters.AddWithValue("Status", status.ToString());
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }
        
        public DataTable SearchDiscountedAccountByStatus(string status,string refId,string workingAccId, string workingAccName,
            decimal? valueDateF, decimal? valueDateT, string curency, string lDId)
        {
            string SEARCH_DISCOUNTED_ACCOUNT_BY_STATUS = @"SELECT [refId],[Status],[TDAmmount],[LDId],[WorkingAccId]
                                                        ,[TDWorkingAccountId],[TDWorkingAccountName],[TDCurrency]                                                                      
                                                        FROM [dbo].[BSAVING_ACC_DISCOUNTED] WHERE 
                                                        ([Status] = @Status or ''=@Status)
                                                        and ([refId] = @refId or  ''=@refId)                                                       
                                                        and ([LDId] = @LDId or  ''=@LDId)                                                       
                                                        and ([TDWorkingAccountId] = @TDWorkingAccountId or  ''=@TDWorkingAccountId)
                                                        and ([TDWorkingAccountName] = @TDWorkingAccountName or  ''=@TDWorkingAccountName)
                                                        and ([TDCurrency] = @TDCurrency or  ''=@TDCurrency)
                                                        and ([TDAmmount]>=@valueDateF or @valueDateF =0)
                                                        and ([TDAmmount]<=@valueDateT or @valueDateT =0)
                                                        ";
            
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(SEARCH_DISCOUNTED_ACCOUNT_BY_STATUS, conn))
                {
                    command.Parameters.AddWithValue("Status", status.ToString());
                    command.Parameters.AddWithValue("refId", refId);
                    command.Parameters.AddWithValue("LDId", lDId);
                    command.Parameters.AddWithValue("TDWorkingAccountId", workingAccId);
                    command.Parameters.AddWithValue("TDWorkingAccountName", workingAccName);
                    command.Parameters.AddWithValue("valueDateF", valueDateF.HasValue ? valueDateF.Value : 0);
                    command.Parameters.AddWithValue("valueDateT", valueDateT.HasValue ? valueDateT.Value : 0);
                    command.Parameters.AddWithValue("TDCurrency", curency);
                    
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }
        
        public bool ApproveDiscountedAccount(string refId, string updatedBy)
        {
            string AUTHORISE_DISCOUNTED_ACCOUNT = @"UPDATE [dbo].[BSAVING_ACC_DISCOUNTED] SET [Status]= @Status,[UpdatedBy]= @UpdatedBy WHERE [refId] = @refId";


            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(AUTHORISE_DISCOUNTED_ACCOUNT, new
                {
                    Status = AuthoriseStatus.AUT.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }

        public bool ReverseDiscountedAccount(string refId, string updatedBy)
        {
            string REVERSE_DISCOUNTED_ACCOUNT = @"UPDATE [dbo].[BSAVING_ACC_DISCOUNTED] SET [Status]= @Status,[UpdatedBy]= @UpdatedBy WHERE [refId] = @refId";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(REVERSE_DISCOUNTED_ACCOUNT, new
                {
                    Status = AuthoriseStatus.REV.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }
        #endregion

        #region Close
        public bool HoldArrearSavingAccount(string refId, string updatedBy)
        {
            string sqlcommandstring = @"UPDATE [dbo].[BSAVING_ACC_ARREAR]
                                        SET Hold = @Hold 
                                        ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                        WHERE [RefId] = @RefId";

            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(sqlcommandstring, new
                {
                    Hold = 1,
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }
        public bool HoldPeriodicSavingAccount(string refId, string updatedBy)
        {
            string sqlcommandstring = @"UPDATE [dbo].[BSAVING_ACC_PERIODIC]
                                        SET Hold = @Hold 
                                        ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                        WHERE [RefId] = @RefId";

            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(sqlcommandstring, new
                {
                    Hold = 1,
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }

        public DataTable GetPeriodicArrearByStatus(string status, string refId, string accCategory, string currency,
                                                    string productLineId, double principalFrom, double principalTo, string customerId,
                                                    string type)
        {
            var table = new DataTable();
            var sqlcommandstring = GET_ARREAR_BY_STATUS;
            if (type == "Periodic")
                sqlcommandstring = GET_PERIODIC_BY_STATUS;
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(sqlcommandstring, conn))
                {
                    command.Parameters.AddWithValue("Status", status.ToString());
                    command.Parameters.AddWithValue("RefId", refId);
                    command.Parameters.AddWithValue("AccCategory", accCategory);
                    command.Parameters.AddWithValue("Currency", currency);
                    command.Parameters.AddWithValue("ProductLineId", productLineId);
                    command.Parameters.AddWithValue("PrincipalFrom", principalFrom);
                    command.Parameters.AddWithValue("PrincipalTo", principalTo);
                    command.Parameters.AddWithValue("CustomerId", customerId);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }

        public DataTable GetSavingAccountCloseStatus()
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(QUERY_GET_ARREAR_PERIODIC_SAVING_ACCOUNT_BY_CLOSESTATUS, conn))
                {
                    command.Parameters.AddWithValue("CloseStatus", AuthoriseStatus.UNA.ToString());                   
                    command.Parameters.AddWithValue("Status", AuthoriseStatus.AUT.ToString());
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }
        public bool PeriodicArrearCommitClose(string fromTable, string redId, DateTime? closeValueDate, double? closeAmoutLCY, double? closeAmountFCY,
                                                    string closeNarative, string closeForTeller, string closeCreditAccount)
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                string sqlcommandstring = CLOSE_UPDATE_PERIODIC_SAVING_ACCOUNT;
                if (fromTable == "arrear")
                    sqlcommandstring = CLOSE_UPDATE_ARREAR_SAVING_ACCOUNT;

                using (var command = new SqlCommand(sqlcommandstring, conn))
                {                   
                    return conn.Execute(sqlcommandstring, new {
                        RefId = redId,
                        CloseStatus = AuthoriseStatus.UNA.ToString(),
                        CloseAmoutLCY = closeAmoutLCY.HasValue ? (decimal?)closeAmoutLCY.Value : null,
                        CloseAmountFCY = closeAmountFCY.HasValue ? (decimal?)closeAmountFCY.Value : null,
                        CloseNarative = closeNarative,
                        CloseForTeller = closeForTeller,
                        CloseCreditAccount = closeCreditAccount,
                        CloseValueDate = closeValueDate.Value.Date
                    }) > 0;                 
                }
               
            }
        }

        public bool ApproveSavingAccount(string refId, string updatedBy, string fromTable)
        {

            string sqlcommandstring = @"UPDATE [dbo].[BSAVING_ACC_PERIODIC]
                                        SET CloseStatus = @CloseStatus 
                                        ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                        WHERE [RefId] = @RefId";
            if (fromTable == "arrear")
                sqlcommandstring = @"UPDATE [dbo].[BSAVING_ACC_ARREAR]
                                        SET CloseStatus = @CloseStatus 
                                        ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                        WHERE [RefId] = @RefId"; 
 
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(sqlcommandstring, new
                {
                    CloseStatus = AuthoriseStatus.AUT.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }

        public bool ReverseSavingAccount(string refId, string updatedBy, string fromTable)
        {

            string sqlcommandstring = @"UPDATE [dbo].[BSAVING_ACC_PERIODIC]
                                        SET CloseStatus = @CloseStatus 
                                        ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                        WHERE [RefId] = @RefId";
            if (fromTable == "arrear")
                sqlcommandstring = @"UPDATE [dbo].[BSAVING_ACC_ARREAR]
                                        SET CloseStatus = @CloseStatus 
                                        ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                        WHERE [RefId] = @RefId";

            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(sqlcommandstring, new
                {
                    CloseStatus = AuthoriseStatus.REV.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }

        public DataTable GetInteresAccountById(string  refId)
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(QUERY_GET_INTEREST_SAVING_ACCOUNT_BY_ID, conn))
                {
                    command.Parameters.AddWithValue("RefId", refId);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }

        public DataTable GetSavingAccountHold()
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(QUERY_GET_ARREAR_PERIODIC_SAVING_ACCOUNT_BY_HOLD, conn))
                {
                    command.Parameters.AddWithValue("CloseStatus", AuthoriseStatus.AUT.ToString());
                    command.Parameters.AddWithValue("Status", AuthoriseStatus.AUT.ToString());
                    command.Parameters.AddWithValue("Hold",1);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }
        #endregion

        #region CloseDiscounted
        public bool DiscountedCommitClose(string redId, DateTime? closeDate, DateTime? closeRateVDate, decimal? closeInterest,
            decimal? closeDealRate, string closeTeller, string closeNarrative, decimal? closeAmountLCY, decimal? closeAmountFCY, string closeCurrency)
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                string sqlcommandstring = @"UPDATE [dbo].[BSAVING_ACC_DISCOUNTED] SET [CloseStatus] = @CloseStatus
                                            ,CloseDate =@CloseDate,CloseInterest = @CloseInterest,CloseRateVDate = @CloseRateVDate
                                            ,CloseDealRate = @CloseDealRate,CloseTeller = @CloseTeller,CloseNarrative = @CloseNarrative
                                            ,CloseAmountLCY = @CloseAmountLCY, CloseAmountFCY=@CloseAmountFCY,CloseCurrency = @CloseCurrency
                                            Where refId = @refId";            

                using (var command = new SqlCommand(sqlcommandstring, conn))
                {
                    return conn.Execute(sqlcommandstring, new
                    {
                        RefId = redId,
                        CloseStatus = AuthoriseStatus.UNA.ToString(),
                        CloseDate = closeDate.HasValue? (DateTime?)closeDate.Value.Date:null,
                        CloseRateVDate = closeRateVDate.HasValue? (DateTime?)closeRateVDate.Value.Date:null,
                        CloseInterest = (decimal?)closeInterest,
                        CloseDealRate = (decimal?)closeDealRate,
                        CloseTeller = closeTeller,
                        CloseNarrative = closeNarrative,
                        CloseAmountLCY = (decimal?)closeAmountLCY,
                        CloseAmountFCY = (decimal?)closeAmountFCY,
                        CloseCurrency = closeCurrency
                    }) > 0;
                }

            }
        }

        public DataTable GetDiscountedAccountByCloseStatus()
        {
            string GET_DISCOUNTED_ACCOUNT_BY_CLOSE_STATUS = @"SELECT [refId],[LDId],[Status],[CustomerId],[AmmountLCY],[AmountFCY],[Narrative],[DealRate]
                                                                      ,[PaymentCCY],[ForTeller],[DebitAccount],[TDCustomerId],[TDJoinHolderId],[TDProductLineId],[TDCurrency]
                                                                      ,[TDAmmount],[TDValueDate],[TDBusDayDate],[TDTerm],[TDFinalMatDate],[TDInterestRate],[TDTotalIntamt],[TDWorkingAccountId],[TDWorkingAccountName]
                                                                      ,[TDAccountOfficerId],[DPDrAccountId],[DPDrAccountName],[DPAmountLCY],[DPAmountFCY],[DPNarrative],[DPPaymentCcy],[DPForTeller],[DPCreditAccount]
                                                                      ,[DPExchRate],[CreatedBy],[CreatedDate] FROM [dbo].[BSAVING_ACC_DISCOUNTED] WHERE [Status] = @Status and [CloseStatus]=@CloseStatus";

            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(GET_DISCOUNTED_ACCOUNT_BY_CLOSE_STATUS, conn))
                {
                    command.Parameters.AddWithValue("Status", AuthoriseStatus.AUT.ToString());
                    command.Parameters.AddWithValue("CloseStatus", AuthoriseStatus.UNA.ToString());
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }
        public bool ApproveDiscountedAccount(string refId, string updatedBy, string fromTable)
        {

            string sqlcommandstring = @"UPDATE [dbo].[BSAVING_ACC_DISCOUNTED]
                                        SET CloseStatus = @CloseStatus 
                                        ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                        WHERE [RefId] = @RefId";          
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(sqlcommandstring, new
                {
                    CloseStatus = AuthoriseStatus.AUT.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }
        public bool ReverseDiscountedAccount(string refId, string updatedBy, string fromTable)
        {

            string sqlcommandstring = @"UPDATE [dbo].[BSAVING_ACC_DISCOUNTED]
                                        SET CloseStatus = @CloseStatus 
                                        ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                                        WHERE [RefId] = @RefId";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(sqlcommandstring, new
                {
                    CloseStatus = AuthoriseStatus.REV.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }
        #endregion

        #region PaymentFreqency
        public IList<AccountOpen> GetAccountOpenByType(string type)
        {
             string QUERY = @"SELECT [ID] ,[AccountCode],[CustomerID],[CustomerType],[ProductLineID]
                                                                        ,[Currency],[CustomerName],[AccountOfficerID],[JoinHolderID],[ActualBallance],[ClearedBallance]
                                                                        from [dbo].[BOPENACCOUNT]
                                                                        WHERE [CustomerType] = @Type AND [Status] = 'AUT' AND [AccountStatus] = 'OPEN' AND [Currency]='VND'";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<AccountOpen>(QUERY, new { @Type = type }).ToList();
            }
        }
        public AccountOpen GetAccountOpenByTypeAndCustomerID(string type, string workingAccId)
        {
            string QUERY = @"SELECT [ID] ,[AccountCode],[CustomerID],[CustomerType],[ProductLineID]
                                                                        ,[Currency],[CustomerName],[AccountOfficerID],[JoinHolderID],[ActualBallance],[ClearedBallance]
                                                                        from [dbo].[BOPENACCOUNT]
                                                                        WHERE [CustomerType] = @Type AND [AccountCode] = @WorkingAccId AND [Status] = 'AUT' AND [AccountStatus] = 'OPEN'";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<AccountOpen>(QUERY, new { @Type = type, @WorkingAccId = workingAccId }).FirstOrDefault();
            }
        }
        public IList<CommonObject> GetSalaryPaymentMethod()
        {
            string QUERY = @"SELECT [ID] ,[Code] from [dbo].[BSalaryPaymentMethod]";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<CommonObject>(QUERY).ToList();
            }
        }
        public IList<CommonObject> GetSalaryPaymentFrequencyTerm()
        {
            string QUERY = @"SELECT [TermID] as [Id] ,[Code] from [dbo].[BSalaryPaymentFrequencyTerm]";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<CommonObject>(QUERY).ToList();
            }
        }
        private void UpdateSalaryPaymentFrequencyDetail(List<SalaryPaymentFrequencyDetail> paymentFrequencyDetail,string paymentFrequencyId)
        {
            string deleteAll =@"DELETE [dbo].[BSalaryPaymentFrequencyDetail]
                                WHERE PaymentFrequencyId = @PaymentFrequencyId";
            string QUERY = @"INSERT INTO [dbo].[BSalaryPaymentFrequencyDetail]
                           ([WorkingAccId]
                           ,[PaymentFrequencyId]
                           ,[PaymentMethod]
                           ,[CreditAmount]
                           ,[CreditAccount])
                            VALUES
                           (@WorkingAccId,@PaymentFrequencyId,@PaymentMethod,@CreditAmount,@CreditAccount)";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                conn.Query<bool>(deleteAll, new { @PaymentFrequencyId = paymentFrequencyId }).Any();
                foreach (SalaryPaymentFrequencyDetail item in paymentFrequencyDetail)
                {
                    item.PaymentFrequencyId = paymentFrequencyId;
                    conn.Execute(QUERY, item);
                }
            }
           
        }

        public bool UpdateSalaryPaymentFrequency(SalaryPaymentFrequency paymentFrequency, List<SalaryPaymentFrequencyDetail> paymentFrequencyDetail)
        {
            string QUERY = @"UPDATE [dbo].[BSalaryPaymentFrequency]
                           SET [Status] =@Status 
                              ,[NonFrequency] = @NonFrequency
                              ,[OpenAccounId] = @OpenAccounId
                              ,[CustomerId] = @CustomerId
                              ,[CustomerName] = @CustomerName
                              ,[Currency] = @Currency
                              ,[TotalDebitAmt] = @TotalDebitAmt
                              ,[Fequency] = @Fequency
                              ,[Term] = @Term
                              ,[EndDate] = @EndDate
                              ,[OrderingCust] = @OrderingCust
                              ,[UpdatedBy] = @UpdatedBy
                              ,[UpdatedDate] = getdate()            
                         WHERE [RefId] = @RefId";
            UpdateSalaryPaymentFrequencyDetail(paymentFrequencyDetail, paymentFrequency.RefId);
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                paymentFrequency.Status = AuthoriseStatus.UNA.ToString();
                return conn.Execute(QUERY, paymentFrequency) > 0;
            }
        }
        public bool CreateNewSalaryPaymentFrequency(SalaryPaymentFrequency paymentFrequency,List<SalaryPaymentFrequencyDetail> paymentFrequencyDetail)
        {
            string QUERY = @"INSERT INTO [dbo].[BSalaryPaymentFrequency]
                           ([RefId],[Status]
                           ,[OpenAccounId]
                           ,[CustomerId]
                           ,[CustomerName]
                           ,[Currency]
                           ,[TotalDebitAmt]
                           ,[Fequency]
                           ,[Term]
                           ,[EndDate]
                           ,[OrderingCust]
                           ,[NonFrequency]
                           ,[CreatedBy]
                           ,[CreatedDate])
                     VALUES(@RefId,@Status,@OpenAccounId,@CustomerId,@CustomerName,@Currency,@TotalDebitAmt
                            ,@Fequency,@Term,@EndDate,@OrderingCust,@NonFrequency,@CreatedBy, getdate())";
            UpdateSalaryPaymentFrequencyDetail(paymentFrequencyDetail, paymentFrequency.RefId);
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                paymentFrequency.Status = AuthoriseStatus.UNA.ToString();
                return conn.Execute(QUERY, paymentFrequency) > 0;
            }
        }
        public bool CheckSalaryPaymentFrequencyExist(string refId)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<bool>("SELECT 1 FROM [BSalaryPaymentFrequency] WHERE RefId = @RefId", new { RefId = refId }).Any();
            }
        }
        public SalaryPaymentFrequency GetSalaryPaymentFrequencyId(string refId)
        {
            string QUERY = @"SELECT * FROM [dbo].[BSalaryPaymentFrequency] WHERE RefId=@RefId";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<SalaryPaymentFrequency>(QUERY, new { RefId = refId }).FirstOrDefault();
            }
        }
        public IList<SalaryPaymentFrequencyDetail> GetSalaryPaymentDetailFrequencyId(string refId)
        {
            string QUERY = @"SELECT * FROM [dbo].[BSalaryPaymentFrequencyDetail] WHERE PaymentFrequencyId=@RefId";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<SalaryPaymentFrequencyDetail>(QUERY, new { RefId = refId }).ToList();
            }
        }
        public IList<SalaryPaymentFrequency> GetSalaryPaymentFrequency(string nonFrequency)
        {
            string QUERY = @"SELECT * from [dbo].[BSalaryPaymentFrequency] WHERE [Status] = 'UNA' AND NonFrequency=@NonFrequency";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<SalaryPaymentFrequency>(QUERY, new { @NonFrequency = nonFrequency }).ToList();
            }
        }

        private void UpdateBallance(string openAccountId,decimal? value)
        {
            string QUERY = @"UPDATE [dbo].[BOPENACCOUNT]
                            SET ActualBallance = ActualBallance - @Value ,[ClearedBallance] = ClearedBallance - @Value, [WorkingAmount] = WorkingAmount - @Value 
                            WHERE [ID] = @RefId";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                conn.Execute(QUERY, new { RefId = openAccountId, Value = value });
            }
        }

        private void UpdateBallancePAccount(string id)
        {
            string QUERY = @"UPDATE [dbo].[BOPENACCOUNT]
                            SET [ActualBallance] = 
                            ( CASE
                                 WHEN ([ActualBallance] IS NULL) THEN   @Value
                                 ELSE ([ActualBallance]  +  @Value)
                               END
                            ),
                            [ClearedBallance] = 
                            ( CASE
                                 WHEN ([ClearedBallance] IS NULL) THEN   @Value
                                 ELSE ([ClearedBallance]  +  @Value)
                               END
                            ),
                            [WorkingAmount] = 
                            ( CASE
                                 WHEN ([WorkingAmount] IS NULL) THEN   @Value
                                 ELSE ([WorkingAmount]  +  @Value)
                               END
                            )
                            WHERE [CustomerType] = @Type AND [AccountCode] = @WorkingAccId AND [Status] = 'AUT' AND [AccountStatus] = 'OPEN'";
            var lobj = GetSalaryPaymentDetailFrequencyId(id);
            if (lobj.Count > 0)
            {
               
                using (var conn = new SqlConnection(DataProvider.ConnectionString))
                {
                    foreach(SalaryPaymentFrequencyDetail item in lobj)
                    {
                        conn.Execute(QUERY, new
                        {
                            Type = 'P',
                            Value = item.CreditAmount
                            ,WorkingAccId = item.WorkingAccId                           
                        });
                     }
                }
            }
        }
        public bool ApproveSalaryPaymentFrequency(string refId, string updatedBy)
        {
            string QUERY = @"UPDATE [dbo].[BSalaryPaymentFrequency]
                            SET Status = @Status ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                            WHERE [RefId] = @RefId";

            var obj = GetSalaryPaymentFrequencyId(refId);

            UpdateBallance(obj.OpenAccounId, obj.TotalDebitAmt);
            UpdateBallancePAccount(obj.RefId);
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(QUERY, new
                {
                    Status = AuthoriseStatus.AUT.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }
        public bool ReverseSalaryPaymentFrequency(string refId, string updatedBy)
        {
            string QUERY = @"UPDATE [dbo].[BSalaryPaymentFrequency]
                            SET Status = @Status ,[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getdate() 
                            WHERE [RefId] = @RefId";
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(QUERY, new
                {
                    Status = AuthoriseStatus.REV.ToString(),
                    UpdatedBy = updatedBy,
                    RefId = refId
                }) > 0;
            }
        }
        public string B_BMACODE_SalaryPayment()
        {
            lock (lockObject)
            {
                using (var conn = new SqlConnection(DataProvider.ConnectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand("[B_BMACODE_SalaryPayment]", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("MaCode", "SALARY_PAYMENT");
                        command.Parameters.AddWithValue("Refix", DateTime.Now.ToString("yyyyMMdd"));
                        command.Parameters.AddWithValue("Flat",".");
                        return command.ExecuteScalar().ToString();
                    }
                }
            }    
           
        }

        public DataTable SearchSalaryPaymentFrequency(string status, string customerId, decimal? valueF,
            decimal? valueT, string curency, string nonFrequency)
        {
            string SEARCH_QUERY = @"SELECT * FROM [dbo].[BSalaryPaymentFrequency] WHERE 
                                            ([Status] = @Status or ''=@Status)
                                            and ([NonFrequency] = @NonFrequency or  ''=@NonFrequency)                                                       
                                            and ([CustomerId] = @CustomerId or  ''=@CustomerId)                                                       
                                            and ([Currency] = @Currency or  ''=@Currency)                                                                                                               
                                            and ([TotalDebitAmt]>=@valueF or @valueF =0)
                                            and ([TotalDebitAmt]<=@valueT or @valueT =0)
                                            ";

            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(SEARCH_QUERY, conn))
                {
                    command.Parameters.AddWithValue("Status", status.ToString());
                    command.Parameters.AddWithValue("CustomerId", customerId);
                    command.Parameters.AddWithValue("Currency", curency);
                    command.Parameters.AddWithValue("valueF", valueF.HasValue ? valueF.Value : 0);
                    command.Parameters.AddWithValue("valueT", valueT.HasValue ? valueT.Value : 0);
                    command.Parameters.AddWithValue("NonFrequency", nonFrequency);

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
                return table;
            }
        }
        #endregion
    }
}