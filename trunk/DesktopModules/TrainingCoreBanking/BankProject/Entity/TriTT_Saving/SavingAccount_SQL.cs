using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using BankProject.Entity.TriTT_Saving;
using BankProject.Entity;
using BankProject.DataProvider;

namespace BankProject.Entity.TriTT_Saving
{
    public class SavingAccount_SQL
    {
        #region SQL Statement
        private readonly string UPDATE_SAVING_ACCOUNT = @"UPDATE [dbo].[BCUSTOMER_INFO] 
                                                        SET [Status]=@Status, [FirstName]=@FirstName, [LastName]=@LastName, [MiddleName]=@MiddleName, [GBShortName]=@GBShortName, [GBFullName]=@GBFullName, [BirthDay]=@BirthDay, [GBStreet]=@GBStreet,
                                                        [GBDist]=@GBDist, [MobilePhone]=@MobilePhone, [MaTinhThanh]=@MaTinhThanh, [TenTinhThanh]=@TenTinhThanh, [CountryCode]=@CountryCode, [CountryName]=@CountryName, [NationalityCode]=@NationalityCode, [NationalityName]=@NationalityName, 
                                                        [ResidenceCode]=@ResidenceCode, [ResidenceName]=@ResidenceName, [DocType]=@DocType, [DocID]=@DocID, [DocIssuePlace]=@DocIssuePlace, [DocIssueDate]=@DocIssueDate, [DocExpiryDate]=@DocExpiryDate, [SectorCode]=@SectorCode, [SectorName]=@SectorName, 
                                                        [SubSectorCode]=@SubSectorCode, [SubSectorName]=@SubSectorName, [IndustryCode]=@IndustryCode, [IndustryName]=@IndustryName, [SubIndustryCode]=@SubIndustryCode, [SubIndustryName]=@SubIndustryName, [TargetCode]=@TargetCode, [MaritalStatus]=@MaritalStatus, [AccountOfficer]=@AccountOfficer,
                                                        [Gender]=@Gender, [Title]=@Title, [ContactDate]=@ContactDate, [RelationCode]=@RelationCode, [OfficeNumber]=@OfficeNumber, [FaxNumber]=@FaxNumber, [NoOfDependant]=@NoOfDependant, [NoOfChildUnder15]=@NoOfChildUnder15, [NoOfChildUnder25]=@NoOfChildUnder25, [NoOfchildOver25]=@NoOfchildOver25, 
                                                        [HomeOwnerShip]=@HomeOwnerShip, [ResidenceType]=@ResidenceType, [EmploymentStatus]=@EmploymentStatus, [CompanyName]=@CompanyName, [Currency]=@Currency, [MonthlyIncome]=@MonthlyIncome, [OfficeAddress]=@OfficeAddress, 
                                                        [CustomerLiability]=@CustomerLiability, [ApprovedUser] = @ApprovedUser, [EmailAddress]= @EmailAddress
                                                        where [CustomerID]=@CustomerID";
        private readonly string CREATE_SAVING_ACCOUNT = @"INSERT INTO  [dbo].[BCUSTOMER_INFO] ( [CustomerID], [Status], [FirstName], [LastName], [MiddleName], [GBShortName], [GBFullName], [BirthDay], [GBStreet]
                                                        , [GBDist], [MobilePhone], [MaTinhThanh], [TenTinhThanh], [CountryCode], [CountryName], [NationalityCode], [NationalityName], 
                                                        [ResidenceCode], [ResidenceName], [DocType], [DocID], [DocIssuePlace], [DocIssueDate], [DocExpiryDate], [SectorCode], [SectorName], 
                                                        [SubSectorCode], [SubSectorName], [IndustryCode], [IndustryName], [SubIndustryCode], [SubIndustryName], [TargetCode], [MaritalStatus], [AccountOfficer],
                                                        [Gender], [Title], [ContactDate], [RelationCode], [OfficeNumber], [FaxNumber], [NoOfDependant], [NoOfChildUnder15], [NoOfChildUnder25], [NoOfchildOver25], 
                                                        [HomeOwnerShip], [ResidenceType], [EmploymentStatus], [CompanyName], [Currency], [MonthlyIncome], [OfficeAddress], [CustomerLiability],[ApprovedUser],EmailAddress  )
                                                        VALUES
                                                        (@CustomerID, @Status, @FirstName, @LastName, @MiddleName, @GBShortName, @GBFullName, @BirthDay, @GBStreet,
                                                        @GBDist, @MobilePhone, @MaTinhThanh, @TenTinhThanh, @CountryCode, @CountryName, @NationalityCode, @NationalityName, 
                                                        @ResidenceCode, @ResidenceName, @DocType, @DocID, @DocIssuePlace, @DocIssueDate, @DocExpiryDate, @SectorCode, @SectorName, 
                                                        @SubSectorCode, @SubSectorName, @IndustryCode, @IndustryName, @SubIndustryCode, @SubIndustryName, @TargetCode, @MaritalStatus, @AccountOfficer,
                                                        @Gender, @Title, @ContactDate, @RelationCode, @OfficeNumber, @FaxNumber, @NoOfDependant, @NoOfChildUnder15, @NoOfChildUnder25, @NoOfchildOver25, 
                                                        @HomeOwnerShip, @ResidenceType, @EmploymentStatus, @CompanyName, @Currency, @MonthlyIncome, @OfficeAddress, @CustomerLiability,@ApprovedUser, @EmailAddress)";
        private readonly string QUERY_GET_INDIVIDUAL_CUSTOMER_ACCOUNT_BY_STATUS = @"SELECT [CustomerID], [Status], [GBShortName] ,[TenTinhThanh], [NationalityName], [IndustryName], [TargetCode],[DocID] 
		                                               FROM  [dbo].[BCUSTOMER_INFO] WHERE [Status]=@Status and CustomerType='P' ORDER BY ContactDate Desc";

        private readonly string QUERY_GET_INDIVIDUAL_CUSTOMER_ACCOUNT_BY_ID = @"SELECT  [CustomerID], [Status], [FirstName] ,[LastName],[MiddleName],[GBShortName],[GBFullName],[BirthDay] ,[GBStreet],[GBDist]
                                                      ,[MobilePhone],[MaTinhThanh],[TenTinhThanh] ,[CountryCode] ,[CountryName] ,[NationalityCode],[NationalityName]
                                                      ,[ResidenceCode],[ResidenceName],[DocType],[DocID],[DocIssuePlace],[DocIssueDate],[DocExpiryDate],[SectorCode]
                                                      ,[SectorName],[SubSectorCode],[SubSectorName] ,[IndustryCode] ,[IndustryName] ,[SubIndustryCode],[SubIndustryName]
                                                      ,[TargetCode],[MaritalStatus] ,[AccountOfficer],[Gender],[Title] ,[ContactDate],[RelationCode],[OfficeNumber]
                                                      ,[FaxNumber],[NoOfDependant] ,[NoOfChildUnder15],[NoOfChildUnder25],[NoOfchildOver25] ,[HomeOwnerShip],[ResidenceType]
                                                      ,[EmploymentStatus],[CompanyName],[Currency],[MonthlyIncome] ,[OfficeAddress],[CustomerLiability],[ApprovedUser],[EmailAddress]
                                                         FROM [dbo].[BCUSTOMER_INFO] where [CustomerID]=@CustomerID";
        private readonly string UPDATE_SET_STATUS_FOR_INDIVIDUAL_CUSTOMER_ACCOUNT = @"UPDATE [dbo].[BCUSTOMER_INFO] SET [Status]=@Status where CustomerID = @CustomerID";
        #endregion
        private SqlDataProvider DataProvider
        {
            get { return new SqlDataProvider(); }
        }
        #region Individual Customer Account
        public bool CheckIndividualCustomerExist(string customerID)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<bool>("SELECT 1 FROM BCUSTOMER_INFO WHERE CustomerID= @CustomerID", new { CustomerID = customerID }).Any();
            }                                                                                      //tao tham so:CustomerID gan vao @CustomerID o cac cau SQL
        }

        //du lieu duoc lay ve la Account, co cac thuoc tinh san roi` nen duoc luu vao class SavingAccount
        public SavingAccount GetIndividualCustomer_ByID(string CustomerIDToReview)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<SavingAccount>(QUERY_GET_INDIVIDUAL_CUSTOMER_ACCOUNT_BY_ID, new { CustomerID = CustomerIDToReview }).FirstOrDefault();
            }
        }

        // du lieu lay ve duoc dua vao Grid View, chua co thuoc tinh san nen phai tao them table, adapter, du lieu se lay tu table ==> adapter
        public DataTable GetIndividualCustomerBySatus(AuthoriseStatus status)
        {
            var table = new DataTable();
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                using (var command = new SqlCommand(QUERY_GET_INDIVIDUAL_CUSTOMER_ACCOUNT_BY_STATUS, conn))
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

        public bool AuthorizeIndividualCustomerAccount(string customerID)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(UPDATE_SET_STATUS_FOR_INDIVIDUAL_CUSTOMER_ACCOUNT, new
                {
                    Status = AuthoriseStatus.AUT.ToString(),
                    //AccountOfficer = updatedBy,
                    CustomerID = customerID
                }) > 0;
            }
        }

        public bool ReverseIndividualCustomerAccount(string CustomerID)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(UPDATE_SET_STATUS_FOR_INDIVIDUAL_CUSTOMER_ACCOUNT, new
                {
                Status = AuthoriseStatus.REV.ToString(),
                CustomerID = CustomerID}   ) > 0;
            }
        }

        #endregion

        #region Saving Account

        public bool UpdateSavingAccount(SavingAccount SavingAccount)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(UPDATE_SAVING_ACCOUNT, SavingAccount) > 0;// return true cho UpdateSavingAccount khi thuc hien thanh cong
            }
        }
        public bool CreateSavingAccount(SavingAccount SavingAccount)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Execute(CREATE_SAVING_ACCOUNT, SavingAccount) > 0;// return true cho CreateSavingAccount khi thuc hien thanh cong
            }
        }
        #endregion
    }
}