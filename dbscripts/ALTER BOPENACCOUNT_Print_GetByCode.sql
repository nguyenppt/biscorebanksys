USE [bisolutions_vvcb]
GO
/****** Object:  StoredProcedure [dbo].[BOPENACCOUNT_Print_GetByCode]    Script Date: 8/21/2014 9:03:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







ALTER PROCEDURE [dbo].[BOPENACCOUNT_Print_GetByCode]
	@Code nvarchar(20)
AS
BEGIN
	
	select 
		un.AccountCode
		, un.customerID
		,cus.gbfullname as customername
		,cus.GBStreet + ', ' + GBDist + ', ' + TenTinhThanh customerAddress
		,cus.Docid
		, cus.DocIssueDate as IssueDate
		, cus.DocIssuePlace as IssuePlace
		, Createdate
		, N'' ChiNhanh
		, N'' BranchAddress
		, N'' BranchTel
		, N'' SerialNo
		, un.JoinHolderName Co_Owner
		, un.Currency
	from bopenaccount un
	join bcustomer_info cus on un.customerid = cus.customerid
	where un.AccountCode = @Code

	select *
	from
	(
		--cash deposit
		select 
			createdate as [Date]
			, 'GTV' as TransactionCode
			, un.AmountDeposited as TransactionAmount
			, un.NewCustBallance as Ballance
			, (select case when un.currency = 'VND' then VND else USD end from BINTEREST_RATE where Term ='NON') InterestRate
			, un.TellerName as Teller
		from
		(
			select a.ID, Code,b.customerID,b.currency, AccountType,CustomerAccount, b.AccountCode, AmtPaidToCust, b.WorkingAmount as CustBallance, ISNULL(b.WorkingAmount, 0) + AmtPaidToCust as NewCustBallance, CurrencyDeposited, AmountDeposited, DealRate, WaiveCharges, a.Narrative, 
								 PrintLnNoOfPS, a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status,TellerName,CashAccount
			 from dbo.BCASHDEPOSIT a
				join bopenaccount b on a.CustomerAccount = convert(nvarchar(20), b.id)
			where b.AccountCode =@Code
			union all
			select a.ID, Code, b.customerID,b.currency,AccountType,CustomerAccount, b.Refid as AccountCode, AmtPaidToCust, b.AZPrincipal as CustBallance, ISNULL(b.AZPrincipal, 0) + AmtPaidToCust as NewCustBallance, CurrencyDeposited, AmountDeposited, DealRate, WaiveCharges, a.Narrative, 
								 PrintLnNoOfPS, a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status,TellerName,CashAccount
			 from dbo.BCASHDEPOSIT a
				join BSAVING_ACC_ARREAR b on a.CustomerAccount =  b.Refid
			where b.Refid =@Code
			union all
			select a.ID, Code, b.customerID,b.currency,AccountType,CustomerAccount, b.Refid as AccountCode, AmtPaidToCust, b.AZPrincipal as CustBallance, ISNULL(b.AZPrincipal, 0) + AmtPaidToCust as NewCustBallance, CurrencyDeposited, AmountDeposited, DealRate, WaiveCharges, a.Narrative, 
								 PrintLnNoOfPS, a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status,TellerName,CashAccount
			 from dbo.BCASHDEPOSIT a
				join BSAVING_ACC_PERIODIC b on a.CustomerAccount =  b.Refid
			where b.Refid =@Code
			union all
			select a.ID, Code, b.customerID,b.tdcurrency,AccountType,CustomerAccount, b.Refid as AccountCode, AmtPaidToCust, b.TDAmmount as CustBallance, ISNULL(b.TDAmmount, 0) + AmtPaidToCust as NewCustBallance, CurrencyDeposited, AmountDeposited, a.DealRate, WaiveCharges, a.Narrative, 
								 PrintLnNoOfPS, a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status,TellerName,CashAccount
			 from dbo.BCASHDEPOSIT a
				join BSAVING_ACC_DISCOUNTED b on a.CustomerAccount =  b.Refid
			where b.Refid =@Code
		
		) un

		union all

		--cash withdrawal
		select 
			createdate as [Date]
			, 'RTV' as TransactionCode
			, un.AmountPaid as TransactionAmount
			, un.NewCustBallance as Ballance
			, (select case when un.currency = 'VND' then VND else USD end from BINTEREST_RATE where Term ='NON') InterestRate
			, un.TellerName
		from
		(
			select a.ID, Code,b.customerId, b.currency, AccountType, CustomerAccount, b.AccountCode, AmountPaid,  b.WorkingAmount as CustBallance, ISNULL(b.WorkingAmount, 0) - Amount as NewCustBallance, CurrencyPaid, Amount, DealRate, WaiveCharges, a.Narrative, 
								 PrintLnNoOfPS, a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status,TellerName,CashAccount
			 from dbo.BCASHWITHRAWAL a
				join bopenaccount b on a.CustomerAccount = convert(nvarchar(20), b.id)
			where b.AccountCode =@Code
			union all
			select a.ID, Code,b.customerId, b.currency, AccountType, CustomerAccount, b.RefID as AccountCode, AmountPaid,  b.AZPrincipal as CustBallance, ISNULL(b.AZPrincipal, 0) - Amount as NewCustBallance, CurrencyPaid, Amount, DealRate, WaiveCharges, a.Narrative, 
								 PrintLnNoOfPS, a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status,TellerName,CashAccount
			 from dbo.BCASHWITHRAWAL a
				join BSAVING_ACC_ARREAR b on a.CustomerAccount = b.RefID
			where b.RefID =@Code
			union all
			select a.ID, Code,b.customerId, b.currency, AccountType, CustomerAccount, b.RefID as AccountCode, AmountPaid,  b.AZPrincipal as CustBallance, ISNULL(b.AZPrincipal, 0) - Amount as NewCustBallance, CurrencyPaid, Amount, DealRate, WaiveCharges, a.Narrative, 
								 PrintLnNoOfPS, a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status,TellerName,CashAccount
			 from dbo.BCASHWITHRAWAL a
				join BSAVING_ACC_PERIODIC b on a.CustomerAccount = b.RefID
			where b.RefID =@Code
			union all
			select a.ID, Code,b.customerId, b.tdcurrency, AccountType, CustomerAccount, b.RefID as AccountCode, AmountPaid,  b.TDAmmount as CustBallance, ISNULL(b.TDAmmount, 0) - Amount as NewCustBallance, CurrencyPaid, Amount, a.DealRate, WaiveCharges, a.Narrative, 
								 PrintLnNoOfPS, a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status,TellerName,CashAccount
			 from dbo.BCASHWITHRAWAL a
				join BSAVING_ACC_DISCOUNTED b on a.CustomerAccount = b.RefID
			where b.RefID =@Code
		) un

		union all

		--closeaccount
		select
			Close_DebitDate
			, 'TTS' TransactionCode
			, Close_CreditAmount
			, WorkingAmount
			, (select case when currency = 'VND' then VND else USD end from BINTEREST_RATE where Term ='NON') InterestRate
			, ''
		from BOPENACCOUNT
		WHERE AccountCode = @Code and Status = 'CLOSE'
	) Final
END






