SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***
---------------------------------------------------------------------------------
-- 36 oct 2016 : Nghia : Correct for read number with text is wrong
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'B_BOVERSEASTRANSFER_PHIEUCHUYENKHOAN')
BEGIN
DROP PROCEDURE [dbo].[B_BOVERSEASTRANSFER_PHIEUCHUYENKHOAN]
END
GO

CREATE PROCEDURE [dbo].[B_BOVERSEASTRANSFER_PHIEUCHUYENKHOAN]
	@Code varchar(50),
	@UserNameLogin  nvarchar(500)
AS
BEGIN
	declare @CurrentDate varchar(15)
	set @CurrentDate = CONVERT(VARCHAR(10),GETDATE(),101);
	-------------------------------------------------------------
	declare @Table_BDRFROMACCOUNT as table
	(
		Name nvarchar(500),
		Currency varchar(20),
		Id varchar(50)
	)
	insert into @Table_BDRFROMACCOUNT
	
	SELECT Name, Currency, Id
	FROM dbo.BDRFROMACCOUNT
	UNION
	SELECT ACCOUNT, Currency,ACCOUNT
	FROM dbo.BINTERNALBANKPAYMENTACCOUNT
	-----------------------------------------------------------
	
	declare @InterBankSettleAmount FLoat
	set @InterBankSettleAmount = (select InterBankSettleAmount from dbo.BOVERSEASTRANSFERMT103  where OverseasTransferCode = @Code)
	
	declare @DebitCurrency varchar(50)
	declare @CreditCurrency varchar(50)
	declare @DebitAmountDisplay nvarchar(2500)
	declare @CreditAmountDisplay nvarchar(2500)
	
	select 
		@DebitCurrency = DebitCurrency,
		@CreditCurrency = CreditCurrency
	from dbo.BOVERSEASTRANSFER
	where OverseasTransferCode = @Code
	
	set @DebitAmountDisplay = (case when @DebitCurrency = 'JPY' OR @DebitCurrency = 'VND' 
			then (select dbo.f_CurrencyToText(CONVERT(INT, cast(@InterBankSettleAmount as decimal(18,0))), @DebitCurrency))
			else (select dbo.f_CurrencyToText(cast(@InterBankSettleAmount as decimal(18,2)), @DebitCurrency)) end) --as DebitAmountDisplay,
			
	set @CreditAmountDisplay = (case when @CreditCurrency = 'JPY' OR @CreditCurrency = 'VND' 
			then (select dbo.f_CurrencyToText(CONVERT(INT, cast(@InterBankSettleAmount as decimal(18,0))), @CreditCurrency))
			else (select dbo.f_CurrencyToText(cast(@InterBankSettleAmount as decimal(18,2)), @CreditCurrency)) end) --as CreditAmountDisplay,
	-----------------------------------------------------------
	
	declare @BSWIFTCODE as table
	(
		Code varchar(50),
		AccountNo nvarchar(50),
		[Description] nvarchar(500)
	)
	insert into @BSWIFTCODE
	select Code, AccountNo, [Description] from dbo.BSWIFTCODE
	-----------------------------------------------------------
	select @CurrentDate as CurrentDate
	
	select
		ov.OverseasTransferCode,
		@CurrentDate as CurrentDate,
		@UserNameLogin as UserNameLogin,
		case when DebitCurrency = 'JPY' OR DebitCurrency = 'VND' 
		 then REPLACE(CONVERT(varchar, CONVERT(money, cast(@InterBankSettleAmount as decimal(18,0))), 1),'.00','') + ' ' + isnull(DebitCurrency, '')
		 else CONVERT(varchar, CONVERT(money, cast(@InterBankSettleAmount as decimal(18,2))), 1) + ' ' + isnull(DebitCurrency, '') end as DebitAmount,
		DebitAcctNo,
		ISNULL((select Name from @Table_BDRFROMACCOUNT where Id = DebitAcctNo and Currency = DebitCurrency ), 'Tai khoan phai tra trong nghiep vu TTQT')
		
		as DebitAcctNoName,
		DebitAmount,
		
		--case when DebitCurrency = 'JPY' OR DebitCurrency = 'VND' 
			--then (select dbo.f_CurrencyToText(CONVERT(INT, @InterBankSettleAmount), DebitCurrency))
			---else (select dbo.f_CurrencyToText(cast(@InterBankSettleAmount as decimal(18,2)), DebitCurrency)) end as DebitAmountDisplay,
		@DebitAmountDisplay as DebitAmountDisplay,
		
		CreditAccount,
		(select [Description] from @BSWIFTCODE where AccountNo = CreditAccount) as CreditAccountName,
		case when CreditCurrency = 'JPY' OR CreditCurrency = 'VND' 
		then REPLACE(CONVERT(varchar, CONVERT(money, cast(@InterBankSettleAmount as decimal(18,0))), 1),'.00','') + ' ' + isnull(CreditCurrency, '')
		 else CONVERT(varchar, CONVERT(money, cast(@InterBankSettleAmount as decimal(18,2))), 1) + ' ' + isnull(CreditCurrency, '') end as CreditAmount,
		
		--case when CreditCurrency = 'JPY' OR CreditCurrency = 'VND' 
			--then (select dbo.f_CurrencyToText(CONVERT(INT, @InterBankSettleAmount), CreditCurrency))
			--else (select dbo.f_CurrencyToText(cast(@InterBankSettleAmount as decimal(18,2)), CreditCurrency)) end as CreditAmountDisplay,

		@CreditAmountDisplay as CreditAmountDisplay,
		
		AddRemarks,
		(SELECT DATEPART(m, GETDATE())) as [Month],
	    (SELECT DATEPART(d, GETDATE())) as [Day],
	    (SELECT DATEPART(yy, GETDATE())) as [Year]
		
	from dbo.BOVERSEASTRANSFER ov
	where ov.OverseasTransferCode = @Code
END
GO

/***
---------------------------------------------------------------------------------
-- 36 oct 2016 : Nghia : Correct for read number with text is wrong
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'B_BOVERSEASTRANSFER_VAT_REPORT')
BEGIN
DROP PROCEDURE [dbo].[B_BOVERSEASTRANSFER_VAT_REPORT]
END
GO

CREATE PROCEDURE [dbo].[B_BOVERSEASTRANSFER_VAT_REPORT]
	@Code varchar(50),
	@UserNameLogin  nvarchar(500)
AS
BEGIN
	declare @CurrentDate varchar(12)
	set @CurrentDate = CONVERT(VARCHAR(10),GETDATE(),101);	
	
	declare @TabCus  as table
	(
		CustomerName nvarchar(500),
		IdentityNo nvarchar(20),
		[Address] nvarchar(500),
		City nvarchar(500),
		Country nvarchar(500)
	)
	insert into @TabCus
	select CustomerName, IdentityNo, [Address], City, Country from dbo.BCUSTOMERS
	where CustomerID = (select OtherBy from dbo.BOVERSEASTRANSFER where OverseasTransferCode = @Code)
	
	
	----------------------------------------------
	
	declare @BOVERSEASTRANSFERCHARGECOMMISSION as table
	(
		VATNo nvarchar(250),		
		AddRemarks1 nvarchar(250),
		AddRemarks2 nvarchar(250),
		
		CommissionCurrency  varchar(50),
		CommissionAmount float,
		CommissionType nvarchar(250),
		
		ChargeType nvarchar(250),
		ChargeAmount float,
		ChargeCurrency varchar(50),
		ChargeAcct nvarchar(250)
	)
	insert into @BOVERSEASTRANSFERCHARGECOMMISSION
	select top 1
		VATNo ,		
		AddRemarks1,
		AddRemarks2,
		
		CommissionCurrency  ,
		CommissionAmount ,
		CommissionType ,
		
		ChargeType ,
		ChargeAmount ,
		ChargeCurrency ,
		ChargeAcct 
	from dbo.BOVERSEASTRANSFERCHARGECOMMISSION
	where OverseasTransferCode = @Code
	--------------------------------------------
	
	declare @TongSoTienThanhToan float
	declare @TotalCharge float
	declare @VAT float
	
	set @TotalCharge = (select (CommissionAmount + ChargeAmount) from @BOVERSEASTRANSFERCHARGECOMMISSION)		
		
	set @VAT = (@TotalCharge * 0.1)
	set @TongSoTienThanhToan = @TotalCharge + @VAT	
	
	declare @AddRemark1 nvarchar(250)
	declare @AddRemarks2 nvarchar(250)
	declare @ChargeCurrency varchar(50)
	declare @CommissionAmount float
	declare @ChargeAmount float
	declare @CommissionCurrency  varchar(50)
	declare @ChargeType nvarchar(250)
	declare @CommissionType nvarchar(250)
	
	set @AddRemark1 = (select AddRemarks1 from @BOVERSEASTRANSFERCHARGECOMMISSION)
	set @AddRemarks2 = (select AddRemarks2 from @BOVERSEASTRANSFERCHARGECOMMISSION)
	set @ChargeCurrency = (select ChargeCurrency from @BOVERSEASTRANSFERCHARGECOMMISSION)
	set @CommissionAmount = (select CommissionAmount from @BOVERSEASTRANSFERCHARGECOMMISSION)
	set @ChargeAmount = (select ChargeAmount from @BOVERSEASTRANSFERCHARGECOMMISSION)
	set @CommissionCurrency =  (select CommissionCurrency from @BOVERSEASTRANSFERCHARGECOMMISSION)
	set @ChargeType =  (select ChargeType from @BOVERSEASTRANSFERCHARGECOMMISSION)
	set @CommissionType =  (select CommissionType from @BOVERSEASTRANSFERCHARGECOMMISSION)
	--------------------------------------------
	
	declare @BCHARGECODE as table
	(
		Code  varchar(20),
		Name_VN nvarchar(100)
	)
	insert into @BCHARGECODE
	select Code, Name_VN  from dbo.BCHARGECODE
	------------------------------
	select @CurrentDate as CurrentDate
	
	select
		ov.OverseasTransferCode,
		(select VATNo from @BOVERSEASTRANSFERCHARGECOMMISSION) as VATNo,
		(select ChargeAcct from @BOVERSEASTRANSFERCHARGECOMMISSION) as ChargeAcct,
		
		@UserNameLogin as UserNameLogin,
		
		case when isnull(@AddRemarks2, '') != '' then @AddRemark1 + ', ' + @AddRemarks2	else @AddRemark1 end as ChargeRemarks,
				
		ov.OtherBy as CustomerID,
		ov.OtherBy2  as CustomerName,
		(select IdentityNo from @TabCus) as IdentityNo,		
		ov.OtherBy3 + ', ' + ov.OtherBy4 + ', ' + ov.OtherBy5 as CustomerAddress,
		
		case when @ChargeCurrency = 'JPY' OR @ChargeCurrency = 'VND' 
			then (select dbo.f_CurrencyToText(CONVERT(INT, cast(@TongSoTienThanhToan as decimal(18,0))), @ChargeCurrency))
			else (select dbo.f_CurrencyToText(cast(@TongSoTienThanhToan as decimal(18,2)), @ChargeCurrency)) end as SoTienBangChu,
			
		case when @ChargeCurrency = 'JPY' OR @ChargeCurrency = 'VND' 
			then (REPLACE(CONVERT(varchar, CONVERT(money, cast(@TongSoTienThanhToan as decimal(18,0))), 1),'.00','') + ' ' + @ChargeCurrency)
			else (CONVERT(varchar, CONVERT(money, cast(@TongSoTienThanhToan as decimal(18,2))), 1) + ' ' + @ChargeCurrency) end as TongSoTienThanhToan,

		--CONVERT(varchar, CONVERT(money, cast(@TongSoTienThanhToan as decimal(18,2))), 1) + ' ' + @ChargeCurrency as TongSoTienThanhToan,		
		
		case when @ChargeCurrency = 'JPY' OR @ChargeCurrency = 'VND' 
			then (REPLACE(CONVERT(varchar, CONVERT(money, cast(@VAT as decimal(18,0))), 1),'.00','') + ' ' + @ChargeCurrency)
			else (CONVERT(varchar, CONVERT(money, cast(@VAT as decimal(18,2))), 1) + ' ' + @ChargeCurrency) end as VAT,
		
		--CONVERT(varchar, CONVERT(money, cast(@VAT as decimal(18,2))), 1) + ' ' + @ChargeCurrency as VAT,
		
		case when @CommissionCurrency = 'JPY' OR @CommissionCurrency = 'VND' 
			then (REPLACE(CONVERT(varchar, CONVERT(money, cast(@CommissionAmount as decimal(18,0))), 1),'.00','') + ' ' + @CommissionCurrency + '  PL70020')
			else (CONVERT(varchar, CONVERT(money, cast(@CommissionAmount as decimal(18,2))), 1) + ' ' + @CommissionCurrency + '  PL70020') end as CommissionAmount,

		--CONVERT(varchar, CONVERT(money, cast(@CommissionAmount as decimal(18,2))), 1) + ' ' + @CommissionCurrency + '  PL70020'  as CommissionAmount,

		case when @ChargeCurrency = 'JPY' OR @ChargeCurrency = 'VND' 
			then (REPLACE(CONVERT(varchar, CONVERT(money, cast(@ChargeAmount as decimal(18,0))), 1),'.00','') + ' ' + @ChargeCurrency+ '  PL70023')
			else (CONVERT(varchar, CONVERT(money, cast(@ChargeAmount as decimal(18,2))), 1) + ' ' + @ChargeCurrency + '  PL70023') end as ChargeAmount,
		--CONVERT(varchar, CONVERT(money, cast(@ChargeAmount as decimal(18,2))), 1) + ' ' + @ChargeCurrency + '  PL70023'  as ChargeAmount,
		
		(select Name_VN from @BCHARGECODE where Code = @CommissionType) as CommissionType_VN,
		(select Name_VN from @BCHARGECODE where Code = @ChargeType) as ChargeType_VN,	
			
		@ChargeType as ChargeType,
		@ChargeCurrency as ChargeCurrency,
		@CommissionCurrency as CommissionCurrency
		
	from dbo.BOVERSEASTRANSFER ov 
	where ov.OverseasTransferCode = @code
END

GO

/***
---------------------------------------------------------------------------------
-- 23 oct 2016 : Nghia : Update for Thu Chi Ho - bullcurrency_change
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'B_BFOREIGNEXCHANGE_GetByDebitAccount')
BEGIN
DROP PROCEDURE [dbo].[B_BFOREIGNEXCHANGE_GetByDebitAccount]
END
GO

CREATE PROCEDURE [dbo].[B_BFOREIGNEXCHANGE_GetByDebitAccount]
	@Code varchar(50),
	@Currency varchar(10),
	@CustomerName nvarchar(500),
	@CallFrom varchar(50)
AS
BEGIN
	if @CallFrom = 'text_chage'
	begin
		select id, name, currency, amount, CustomerID from dbo.BDRFROMACCOUNT
		where Id = @Code  and Currency = @Currency
		union all
		select t.thuchihoaccount as ID, cus.CustomerName as Name, t.currency, 0 as [amount], cus.CustomerID
		from BCUSTOMERS cus 
			inner join BINTERNALBANKACCOUNT t on 1 = 1
		where cus.CustomerID = '1100006' and t.thuchihoaccount = @Code and  t.Currency = @Currency
	end
	else if @CallFrom = 'bullcurrency_change'
	begin
		select * from (
		select id, name, currency, amount, CustomerID from dbo.BDRFROMACCOUNT
		where Name = @CustomerName and Currency = @Currency
		union all
		select t.thuchihoaccount as ID, cus.CustomerName as Name, t.currency, 0 as [amount], cus.CustomerID
		from BCUSTOMERS cus 
			inner join BINTERNALBANKACCOUNT t on 1 = 1
		where cus.CustomerID = '1100006' and cus.CustomerName = @CustomerName and  t.Currency = @Currency
		) a
	end
	else if @CallFrom = 'provision_transfers'
	begin
		select id, name, currency, amount, CustomerID from dbo.BDRFROMACCOUNT
		where CustomerID = @CustomerName and Currency = @Currency
	end
END

GO


/***
---------------------------------------------------------------------------------
-- 22 oct 2016 : Nghia : In case of data is update,the status should be change to UNA
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'B_BFOREIGNEXCHANGE_Insert')
BEGIN
DROP PROCEDURE [dbo].[B_BFOREIGNEXCHANGE_Insert]
END
GO

CREATE PROCEDURE [dbo].[B_BFOREIGNEXCHANGE_Insert]
	@Code varchar(50)
	,@TransactionType varchar(50)
	,@FTNo varchar(50)
	,@DealType varchar(50)
	,@Counterparty varchar(50)
	,@DealDate varchar(50)
	,@ValueDate varchar(50)
	,@ExchangeType varchar(50)
	,@BuyCurrency varchar(10)
	,@BuyAmount float
	,@SellCurrency varchar(10)
	,@SellAmount float
	,@Rate float
	,@CustomerReceiving varchar(50)
	,@CustomerPaying varchar(50)
	,@AccountOfficer varchar(50)
	, @CurrentUserId INT
	, @Comment1 nvarchar(50) = null
	, @Comment2 nvarchar(50) =  null
	, @Comment3 nvarchar(50) = null
AS
BEGIN
	IF NOT EXISTS(SELECT Code FROM dbo.BFOREIGNEXCHANGE WHERE Code = @Code)
	begin
		INSERT INTO [dbo].[BFOREIGNEXCHANGE]
           ([Code]
           ,[TransactionType]
           ,[FTNo]
           ,[DealType]
           ,[Counterparty]
           ,[DealDate]
           ,[ValueDate]
           ,[ExchangeType]
           ,[BuyCurrency]
           ,[BuyAmount]
           ,[SellCurrency]
           ,[SellAmount]
           ,[Rate]
           ,[CustomerReceiving]
           ,[CustomerPaying]
           ,[AccountOfficer]
           ,[Status]
           , CreateBy
           , CreateDate
           , Comment1
           , Comment2
           , Comment3)
     VALUES
           (@Code
            ,@TransactionType
			,@FTNo
			,@DealType
			,@Counterparty
			,@DealDate
			,@ValueDate
			,@ExchangeType
			,@BuyCurrency
			,@BuyAmount
			,@SellCurrency
			,@SellAmount
			,@Rate
			,@CustomerReceiving
			,@CustomerPaying
			,@AccountOfficer
			,'UNA'
			, @CurrentUserId
			, getdate()
			, @Comment1
			, @Comment2
			, @Comment3)
	end
	else
	begin
		UPDATE [dbo].[BFOREIGNEXCHANGE]
		   SET [TransactionType] = @TransactionType
			  ,[FTNo] = @FTNo
			  ,[DealType] = @DealType
			  ,[Counterparty] = @Counterparty
			  ,[DealDate] = @DealDate
			  ,[ValueDate] = @ValueDate
			  ,[ExchangeType] = @ExchangeType
			  ,[BuyCurrency] = @BuyCurrency
			  ,[BuyAmount] = @BuyAmount
			  ,[SellCurrency] = @SellCurrency
			  ,[SellAmount] = @SellAmount
			  ,[Rate] = @Rate
			  ,[CustomerReceiving] = @CustomerReceiving
			  ,[CustomerPaying] = @CustomerPaying
			  ,[AccountOfficer] = @AccountOfficer     
			  ,[Status] = 'UNA'
			  ,[UpdatedDate] = getdate()
			  ,[UpdatedBy] = @CurrentUserId
			  , Comment1 = @Comment1
			  , Comment2 = @Comment2
			  , Comment3 = @Comment3
		     
		 WHERE [Code] = @Code
	end
	
	------- update 
	--PROVISIONTRANSFER_DC.CreditAmount = (Sell Amount + CreditAmount) 
	--where LCNo = FTNo

	if @TransactionType != 'TT'
	begin
		declare @CreditAmount float
		set @CreditAmount = isnull((select max(CreditAmount) from dbo.PROVISIONTRANSFER_DC where LCNo = @Code), 0)
		
		set @CreditAmount = (@CreditAmount + @SellAmount)
		
		update dbo.PROVISIONTRANSFER_DC
		set CreditAmount = @CreditAmount
		where LCNo = @Code
	end
END

GO