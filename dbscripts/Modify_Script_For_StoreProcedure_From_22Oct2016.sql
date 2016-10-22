SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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