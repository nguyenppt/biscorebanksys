SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***
---------------------------------------------------------------------------------
-- 1 Apr 2017 : Nghia : Fix loi Credit account ph?i hi?n th? tài kho?n ký qu? ngo?i t? tuong ?ng v?i don v? ti?n t? c?a TF nh? thu dã kh?i t?o
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'PROVISIONTRANSFER_DC_GetByLCNo')
BEGIN
DROP PROCEDURE [dbo].[PROVISIONTRANSFER_DC_GetByLCNo]
END
GO
CREATE PROCEDURE [dbo].[PROVISIONTRANSFER_DC_GetByLCNo]
	@NormalLCCode nvarchar(50),
	@Type nvarchar(50)
AS
BEGIN
	select ProvisionNo, Un.[NormalLCCode], Un.[ApplicantID], Un.[ApplicantName],Un.Currency, Un.Amount
			,acc.[CustomerID], acc.[Currentcy] ,acc.[AccountName],acc.[DepositCode],isnull(dc.CreditAmount, 0) as CreditAmount
	from
	(
		SELECT [NormalLCCode] ,[ApplicantID] ,[ApplicantName], Currency ,Amount
		FROM [dbo].BIMPORT_NORMAILLC
		WHERE [NormalLCCode] <> '' 
		and [NormalLCCode] = @NormalLCCode 
		and @Type ='LC' --fix hard code la LC

		union all

		SELECT [DocCollectCode], [DraweeCusNo], [DraweeCusName], [Currency], isnull([Amount_Old], [Amount]) 
		FROM [dbo].[BDOCUMETARYCOLLECTION]
		WHERE [DocCollectCode] <> '' 
		and [DocCollectCode] = @NormalLCCode 
		and @Type ='DOC'--fix hard code la DOC
	) Un 
	left join BACCOUNTS acc on Un.[ApplicantID] = acc.CustomerID and Un.Currency = acc.Currentcy
	left join PROVISIONTRANSFER_DC dc on Un.NormalLCCode = dc.LCNo
		
END

GO
/***
---------------------------------------------------------------------------------
-- 19 Mar 2017 : Nghia : Fix loi show data ko dung trong truong hop cancel
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'B_DOCUMENTARYCOLLECTIONCANCEL_PHIEUXUATNGOAIBANG_REPORT')
BEGIN
DROP PROCEDURE [dbo].[B_DOCUMENTARYCOLLECTIONCANCEL_PHIEUXUATNGOAIBANG_REPORT]
END
GO

CREATE PROCEDURE [dbo].[B_DOCUMENTARYCOLLECTIONCANCEL_PHIEUXUATNGOAIBANG_REPORT]
	@Code varchar(50),
	@CurrentUserLogin nvarchar(250)
AS
BEGIN
	declare @CurrentDate varchar(12)
	set @CurrentDate = CONVERT(VARCHAR(10),GETDATE(),101);	
	----------------------------
	declare @TabCus  as table
	(
		CustomerName nvarchar(500),
		IdentityNo nvarchar(20),
		[Address] nvarchar(500),
		City nvarchar(500),
		Country nvarchar(500)
	)
	insert into @TabCus
	select CustomerName,IdentityNo, [Address],City, Country from BCUSTOMERS
	where CustomerID = (select DraweeCusNo from BDOCUMETARYCOLLECTION where DocCollectCode = @Code and (ActiveRecordFlag = 'YES' or ActiveRecordFlag is NULL))
	---------------------------
	declare @IncreaseMental float
	set @IncreaseMental = (select max(IncreaseMental) from dbo.BINCOMINGCOLLECTIONPAYMENT 
	where CollectionPaymentCode = @Code)
	----------------------------
	declare @totalAmt decimal(18,2)
	set @totalAmt = (select case when isnull(@IncreaseMental,0) > 0 
								then CONVERT(money, (Amount - @IncreaseMental)) 
								else  CONVERT(money, Amount) end
					from dbo.BDOCUMETARYCOLLECTION  where DocCollectCode = @Code and (ActiveRecordFlag = 'YES' or ActiveRecordFlag is NULL))
	-----------------------------

	
	select @CurrentDate as CurrentDate
	select
		DocCollectCode,
		@CurrentUserLogin as CurrentUserLogin,
		DraweeCusNo,
		DraweeCusName,
		DraweeAddr1,
		DraweeAddr2,
		DraweeAddr3,
		(select CustomerName from @TabCus) as CustomerName,
		(select IdentityNo from @TabCus) as IdentityNo,
	    (select [Address] from @TabCus) as [Address],
	    (select City from @TabCus) as City,
	    (select Country from @TabCus) as Country,
	    --CASE WHEN ISNULL(@IncreaseMental, 0) > 0 THEN (select dbo.fuDocSoThanhChu((Amount - @IncreaseMental))) ELSE (select dbo.fuDocSoThanhChu(Amount)) END SoTienVietBangChu,			
	    case when Currency = 'JPY' OR Currency = 'VND' 
			then (select dbo.f_CurrencyToText(CONVERT(INT, @totalAmt), Currency))
			else (select dbo.f_CurrencyToText(cast(@totalAmt as decimal(18,2)), Currency)) end as SoTienVietBangChu,

	    Currency,
	    @CurrentDate as CurrentDate,	    
	    --case when isnull(@IncreaseMental,0) > 0 then CONVERT(varchar, CONVERT(money, (Amount - @IncreaseMental)), 1) else CONVERT(varchar, CONVERT(money, Amount), 1) end as Amount,			
	    cast(@totalAmt as decimal(18,2)) as Amount,
	    (select Vietnamese from dbo.BCURRENCY where Code = Currency) as Vietnamese,
	    (SELECT DATEPART(m, GETDATE())) as [Month],
	    (SELECT DATEPART(d, GETDATE())) as [Day],
	    (SELECT DATEPART(yy, GETDATE())) as [Year]
	    
	from dbo.BDOCUMETARYCOLLECTION
	where DocCollectCode = @Code and (ActiveRecordFlag = 'YES' or ActiveRecordFlag is NULL)
	
END

GO