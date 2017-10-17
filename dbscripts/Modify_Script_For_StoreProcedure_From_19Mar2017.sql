SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***
---------------------------------------------------------------------------------
-- Add Testing Procedue
---------------------------------------------------------------------------------
***/
CREATE TABLE [dbo].[TestTable](
	[TestData] [nvarchar](50) NULL
) ON [PRIMARY]

GO

IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'NGUYEN_TESTING_Insert')
BEGIN
DROP PROCEDURE [dbo].[NGUYEN_TESTING_Insert]
END
GO
CREATE PROCEDURE [dbo].[NGUYEN_TESTING_Insert]
AS
BEGIN
	Insert into [TestTable] values ('NghiaTest')
END



/***
---------------------------------------------------------------------------------
-- 25 June 2017 : Nghia : 
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'BEXPORT_LC_DOCS_PROCESSING_Report_Cover')
BEGIN
DROP PROCEDURE [dbo].[BEXPORT_LC_DOCS_PROCESSING_Report_Cover]
END
GO
CREATE PROCEDURE [dbo].[BEXPORT_LC_DOCS_PROCESSING_Report_Cover]
	@Code varchar(50),
	@UserNameLogin  nvarchar(500)
AS
BEGIN
	Select top 1 * from BEXPORT_LC_DOCS_PROCESSING Where DocCode = @Code
	 and ActiveRecordFlag = 'Yes';

END


GO
ALTER FUNCTION [dbo].[f_CurrencyToTextVn]
(
	@sNumber nvarchar(4000),
	@ccyCode varchar(3)
)
RETURNS nvarchar(4000) AS

BEGIN
	DECLARE @ccyName nvarchar(50);
	DECLARE @ccyPence nvarchar(250);
	DECLARE @integerNum nvarchar(4000);
	DECLARE @penceNum nvarchar(4000);
	DECLARE @result nvarchar(4000);

	select @ccyName = Vietnamese, @ccyPence = Pence from dbo.bcurrency where code = @ccyCode;
	if CHARINDEX ('.',@sNumber) > 0 
	begin
		select @integerNum = SUBSTRING(@sNumber,0,CHARINDEX ('.',@sNumber));
		select @penceNum=substring(@sNumber,CHARINDEX ('.',@sNumber) + 1,len(@sNumber)-len(@integerNum));
		if (len(@penceNum) < 2)
			set @penceNum = @penceNum + '0';
		select @result = REPLACE(lower(dbo.fuDocSoThanhChu(@penceNum)), '  ', ' ');
		IF @ccyCode = 'VND'
		BEGIN
			IF ISNULL(@result,'') != ''
				select @result = (REPLACE(lower(dbo.fuDocSoThanhChu(@integerNum)), '  ', ' ') + N' l? ' + @result + @ccyName);
			else
				select @result = (REPLACE(lower(dbo.fuDocSoThanhChu(@integerNum)), '  ', ' ') + @ccyName);
		END
		else
		BEGIN
			IF ISNULL(@result,'') != ''
				select @result = (REPLACE(lower(dbo.fuDocSoThanhChu(@integerNum)), '  ', ' ') + ' ' + @ccyName + N' và ' + @result + ' ' + isnull(@ccyPence,''));
			else
				select @result = (REPLACE(lower(dbo.fuDocSoThanhChu(@integerNum)), '  ', ' ') + ' ' + @ccyName);
		end
	end
	else
	begin
		select @result = lower(dbo.fuDocSoThanhChu(@sNumber)) + ' ' + @ccyName;
	end
	---Loai bo khoang trang thua
	while len(@result) > 0
	begin
		if charindex('  ', @result) > 0
			set @result = replace(@result, '  ', ' ')
		else
			break
	end
	set @result = ltrim(rtrim(@result))
	
	return UPPER(Left(@result, 1)) + SUBSTRING(@result,2, 4000);
END


GO
/***
---------------------------------------------------------------------------------
-- 2 May 2017 : Nghia : Add Nostro account in report
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'B_INCOMINGCOLLECTIONPAYMENT_VAT_B_Report')
BEGIN
DROP PROCEDURE [dbo].[B_INCOMINGCOLLECTIONPAYMENT_VAT_B_Report]
END
GO
CREATE PROCEDURE [dbo].[B_INCOMINGCOLLECTIONPAYMENT_VAT_B_Report]
	@Code varchar(50),
	@UserNameLogin  nvarchar(500)
AS
BEGIN-- B_INCOMINGCOLLECTIONPAYMENT_VAT_B_Report 'TF-14235-00016.1', 'a'
	declare @CurrentDate varchar(12)
	set @CurrentDate = CONVERT(VARCHAR(10),GETDATE(),101);	
	declare @DepositCode_BACCOUNTS nvarchar(50)
	
	declare @Tab_temp as table 
	(
		id int,
		part nvarchar(4000)
	)
	insert into @Tab_temp
	select id,part from [dbo].SplitString ((select DrFromAccount from dbo.BINCOMINGCOLLECTIONPAYMENT where PaymentId = @Code), '-')
	set @DepositCode_BACCOUNTS = (select LTRIM(RTRIM(part)) from @Tab_temp where id = 1)	
	
	declare @TabCus  as table
	(
		CustomerName nvarchar(500),
		IdentityNo nvarchar(20),
		[Address] nvarchar(500),
		City nvarchar(500),
		Country nvarchar(500),
		BankAccount nvarchar(50),
		CustomerID nvarchar(50)
	)
	insert into @TabCus
	select 
		top 1 CustomerName,
		IdentityNo, 
		[Address],
		City, 
		Country, 
		BankAccount,
		CustomerID
	from BCUSTOMERS
	where CustomerID = (select CustomerID from dbo.BACCOUNTS where DepositCode = @DepositCode_BACCOUNTS)	
	--------------------------------------------	
	declare @Table_ChargeCode as table 
	(
		Code varchar(20),
		Name_VN nvarchar(100),
		PaymentIC varchar(1)
	)
	insert into @Table_ChargeCode
	select Code,Name_VN,PaymentIC from  dbo.BCHARGECODE
	where isnull(PaymentIC, '') != ''
	
	-------------------------------------------
	declare @OrginalCode varchar(50)
	set @OrginalCode = SUBSTRING(@Code,0,15)
	
	declare @Table_PaymentCharge as table 
	(
		CollectionPaymentCode varchar(50),
		ChargeAmt float,
		Chargecode nvarchar(50),
		ChargeCcy varchar(5),
		Rowchages int,
		PartyCharged varchar(5)
	)
	insert into @Table_PaymentCharge
	select 
		CollectionPaymentCode,
		ChargeAmt,
		Chargecode,
		ChargeCcy,
		Rowchages,
		PartyCharged
	from dbo.BINCOMINGCOLLECTIONPAYMENTCHARGES
	where SUBSTRING(CollectionPaymentCode,0,15) = @OrginalCode
	and PartyCharged = 'B'
	--------------------------------------------
	declare @TongSoTienThanhToan float
	set @TongSoTienThanhToan = (select sum(ChargeAmt)
		from dbo.BINCOMINGCOLLECTIONPAYMENTCHARGES 
		where SUBSTRING(CollectionPaymentCode,0,15) = @OrginalCode
			  and PartyCharged = 'B')
			
	declare @VAT float
	set @VAT = (@TongSoTienThanhToan * 0.1)
	set @TongSoTienThanhToan = @TongSoTienThanhToan + @VAT
	
	declare @Cot9_1 FLOAT
	declare @Cot9_2 FLOAT
	declare @Cot9_3 FLOAT	
	declare @Cot9_4 FLOAT
	declare @Cot9_1_Currency nvarchar(500)	
	declare @Cot9_2_Currency nvarchar(500)	
	declare @Cot9_3_Currency nvarchar(500)	
	declare @Cot9_4_Currency nvarchar(500)	
	
	declare @Cot9_1Name nvarchar(500)	
	declare @Cot9_2Name nvarchar(500)
	declare @Cot9_3Name nvarchar(500)	
	declare @Cot9_4Name nvarchar(500)	
	
	set @Cot9_1Name = (select Chargecode from @Table_PaymentCharge
		 where CollectionPaymentCode = @Code and Rowchages = 1)
	select @Cot9_1 = isnull(ChargeAmt, 0), @Cot9_1_Currency = ChargeCcy
		 from @Table_PaymentCharge
		 where CollectionPaymentCode = @Code and Rowchages = 1
		 
	set @Cot9_2Name = (select Chargecode from @Table_PaymentCharge
		 where CollectionPaymentCode = @Code and Rowchages = 2)
	select @Cot9_2 = isnull(ChargeAmt, 0), @Cot9_2_Currency = ChargeCcy
		 from @Table_PaymentCharge
		 where CollectionPaymentCode = @Code and Rowchages = 2
		 
	set @Cot9_3Name = (select Chargecode from @Table_PaymentCharge
		 where CollectionPaymentCode = @Code and Rowchages = 3)
	select @Cot9_3 = isnull(ChargeAmt, 0), @Cot9_3_Currency = ChargeCcy
		 from @Table_PaymentCharge
		 where CollectionPaymentCode = @Code and Rowchages = 3	
	
	set @Cot9_4Name = (select Chargecode from @Table_PaymentCharge
		 where CollectionPaymentCode = @Code and Rowchages = 4)
	select @Cot9_4 = isnull(ChargeAmt, 0), @Cot9_4_Currency = ChargeCcy
		 from @Table_PaymentCharge
		 where CollectionPaymentCode = @Code and Rowchages = 4		 
	
	set @Cot9_1Name = (select Name_VN from @Table_ChargeCode where Code = @Cot9_1Name)
	set @Cot9_2Name = (select Name_VN from @Table_ChargeCode where Code = @Cot9_2Name)
	set @Cot9_3Name = (select Name_VN from @Table_ChargeCode where Code = @Cot9_3Name)
	set @Cot9_4Name = (select Name_VN from @Table_ChargeCode where Code = @Cot9_4Name)
	--------------------------------------------
	declare @Tab_Charge as table
	(
		VATNo nvarchar(500),
		ChargeAcct nvarchar(500),
		ChargeRemarks nvarchar(500),
		ChargeCcy nvarchar(500)
	)
	insert into @Tab_Charge
	select VATNo,ChargeAcct,ChargeRemarks,ChargeCcy 
		from dbo.BINCOMINGCOLLECTIONPAYMENTCHARGES 
		 where CollectionPaymentCode = @Code and Rowchages = 1 
	
	select @CurrentDate as CurrentDate
	select
		doc.CollectionPaymentCode,
		@UserNameLogin as UserNameLogin,
		
		(select VATNo from @Tab_Charge) as VATNo,
		(select ChargeAcct from @Tab_Charge) as ChargeAcct,		
		(select ChargeRemarks from @Tab_Charge) as ChargeRemarks,
		
		(select CustomerName from @TabCus) as CustomerName,
		(select [Address] from @TabCus) as [Address],
		(select IdentityNo from @TabCus) as IdentityNo,		
		(select City from @TabCus) as City,		
		(select Country from @TabCus) as Country,		
		(select BankAccount from @TabCus) as BankAccount,
		(select CustomerID from @TabCus) as CustomerID,	
		(select top 1 DepositCode from BACCOUNTS where CustomerId = (select CustomerID from @TabCus) and currentcy = (select ChargeCcy from @Tab_Charge)) as DepositCode,
		CONVERT(varchar, CONVERT(money, @VAT), 1) + ' ' + (select ChargeCcy from @Tab_Charge) + ' PL52768' as VAT,
		--(select dbo.fuDocSoThanhChu(@TongSoTienThanhToan)) + ' ' + isnull((select Vietnamese from dbo.BCURRENCY where Code = (select ChargeCcy from @Tab_Charge)),'') as SoTienBangChu,
		(select dbo.f_CurrencyToText(cast(@TongSoTienThanhToan as decimal(18,2)), (select ChargeCcy from @Tab_Charge))) as SoTienBangChu,
		
		case when @Cot9_1 != 0 then CONVERT(varchar, CONVERT(money, @Cot9_1), 1) + ' ' + @Cot9_1_Currency + ' PL52324' else '' end as Cot9_1,
		case when @Cot9_2 != 0 then CONVERT(varchar, CONVERT(money, @Cot9_2), 1) + ' ' + @Cot9_2_Currency + ' PL52356' else '' end as Cot9_2,
		case when @Cot9_3 != 0 then CONVERT(varchar, CONVERT(money, @Cot9_3), 1) + ' ' + @Cot9_3_Currency + ' PL52477' else '' end as Cot9_3,
		case when @Cot9_4 != 0 then CONVERT(varchar, CONVERT(money, @Cot9_4), 1) + ' ' + @Cot9_4_Currency + ' PL52566' else '' end as Cot9_4,
		
		case when @Cot9_1 != 0 then @Cot9_1Name else '' end as Cot9_1Name,
		case when @Cot9_2 != 0 then @Cot9_2Name else '' end as Cot9_2Name,
		case when @Cot9_3 != 0 then @Cot9_3Name else '' end as Cot9_3Name,
		case when @Cot9_4 != 0 then @Cot9_4Name else '' end as Cot9_4Name,
		
		CONVERT(varchar, CONVERT(money, @TongSoTienThanhToan), 1) + ' ' + (select ChargeCcy from @Tab_Charge) as TongSoTienThanhToan,
		(SELECT DATEPART(m, GETDATE())) as [Month],
	    (SELECT DATEPART(d, GETDATE())) as [Day],
	    (SELECT DATEPART(yy, GETDATE())) as [Year]
		
	from dbo.BINCOMINGCOLLECTIONPAYMENT doc
	where doc.PaymentId = @Code
END
GO

/***
---------------------------------------------------------------------------------
-- 2 May 2017 : Nghia : Add Nostro account in report
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'P_ReadNumber')
BEGIN
DROP PROCEDURE [dbo].[P_ReadNumber]
END
GO

CREATE PROCEDURE [dbo].[P_ReadNumber](
	@money decimal(18,2),
	@Currency VARCHAR(50))
as
Begin
	Select dbo.f_CurrencyToTextVn((@money), @Currency) SoTienBangChu
End
Go


/***
---------------------------------------------------------------------------------
-- 2 May 2017 : Nghia : Add Nostro account in report
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'P_ExportLCSettlementReport')
BEGIN
DROP PROCEDURE [dbo].[P_ExportLCSettlementReport]
END
GO
CREATE PROCEDURE [dbo].[P_ExportLCSettlementReport](
	@ReportType smallint,--1 : PhieuChuyenKhoan, 2 : VAT
	@PaymentId VARCHAR(50),
	@UserId varchar(50))
as
-- P_ExportLCSettlementReport 2, 'TF-14250-00149.1', 'a'
begin
	declare @DocId bigint, @VATNo varchar(50), @LCCode varchar(50)
	declare @CustomerID varchar(50), @CustomerName nvarchar(250), @CustomerIDNo varchar(50), @Address1 nvarchar(500), @Address2 nvarchar(500), @Address3 nvarchar(500), @CustomerBankAcc NVARCHAR(50), @CollectionType nvarchar(10)	
	declare @TaiKhoanNo nvarchar(50), @TenTaiKhoanNo nvarchar(max), @TaiKhoanCoId varchar(50), @TaiKhoanCo varchar(50), @TenTaiKhoanCo nvarchar(max), @currency nvarchar(10)
	declare @OverseasMinus float, @OverseasPlus float, @Amount float
	---1 : PhieuChuyenKhoan
	if @ReportType = 1
	begin			
		
		---
		select @CustomerID = BeneficiaryNo, @CustomerName = BeneficiaryName
		from BEXPORT_LC_DOCS_PROCESSING where [AmendNo] = @PaymentId
		---
		
		select @CustomerIDNo = IdentityNo, @CustomerBankAcc = BankAccount, @Address1 = [Address], @Address2 = [City], @Address3 = [Country]
		from dbo.BCUSTOMERS where CustomerID = @CustomerID

		select  top 1 @TaiKhoanCoId = [CreditAccount], @currency = Currency from BEXPORT_DOCS_PROCESSING_SETTLEMENT where PaymentId = @PaymentId
		set @VATNo = (SELECT top 1 VATNo FROM BEXPORT_DOCS_PROCESSING_SETTLEMENT_CHARGES WHERE CollectionPaymentCode = @PaymentId)
		----

		select @TaiKhoanCo = CustomerId from [BCRFROMACCOUNT] where Id = @TaiKhoanCoId

		select @TenTaiKhoanCo = [CustomerName]
		from dbo.BCUSTOMERS where CustomerID = @TaiKhoanCo
		----
	--	(select  top 1  @TenTaiKhoanCo = DrawerCusName, @CollectionType = CollectionType from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode)

		
		select (SELECT DATEPART(d, GETDATE())) as [Day], (SELECT DATEPART(m, GETDATE())) as [Month], (SELECT DATEPART(yy, GETDATE())) as [Year],
			[PaymentId] LCCode, @VATNo VATNo, @UserId CurrentUserLogin, 
			@CustomerBankAcc SoTaiKhoanNo, @CustomerName TenTaiKhoanNo,
			--Currency + cast(DrawingAmount AS VARCHAR) SoTienTaiKhoanNo, 
			case when se.Currency = 'JPY' OR se.Currency = 'VND' 
				then (REPLACE(CONVERT(varchar, CONVERT(money, cast((DrawingAmount) as decimal(18,0))), 1),'.00','') + ' ' + se.Currency)
				else (CONVERT(varchar, CONVERT(money, cast((DrawingAmount) as decimal(18,2))), 1) + ' ' + se.Currency) end as SoTienTaiKhoanNo,

			--REPLACE(CONVERT(varchar, CONVERT(money, cast(isnull(DrawingAmount,0) as decimal(18,2))), 1) , '.00', '')+ ' ' + Currency AS SoTienTaiKhoanNo,
			dbo.f_CurrencyToTextVn((DrawingAmount), se.Currency) SoTienTaiKhoanNoBangChu,
			--Currency + cast(DrawingAmount AS VARCHAR) SoTienTaiKhoanCo, 
			
			case when se.Currency = 'JPY' OR se.Currency = 'VND' 
				then (REPLACE(CONVERT(varchar, CONVERT(money, cast((DrawingAmount) as decimal(18,0))), 1),'.00','') + ' ' + se.Currency)
				else (CONVERT(varchar, CONVERT(money, cast((DrawingAmount) as decimal(18,2))), 1) + ' ' + se.Currency) end as SoTienTaiKhoanCo,
			
			--REPLACE(CONVERT(varchar, CONVERT(money, cast(isnull(DrawingAmount,0) as decimal(18,2))), 1), '.00', '') + ' ' + Currency AS SoTienTaiKhoanCo,

			dbo.f_CurrencyToTextVn((DrawingAmount), se.Currency) SoTienTaiKhoanCoBangChu,
			CreditAccount SoTaiKhoanCo, @TenTaiKhoanCo TenTaiKhoanCo, LCType CollectionType, NostroAccount, sw.Description NostroAccountName
		from BEXPORT_DOCS_PROCESSING_SETTLEMENT se 
			left join BSWIFTCODE sw on sw.AccountNo = NostroAccount and sw.Currency = se.Currency
		
		where PaymentId = @PaymentId
		
		return
	end
	---2 : VAT
	if @ReportType = 2
	begin
		Declare @collType nvarchar(10);
		Declare @chargeRemark nvarchar(225);
		set @TaiKhoanCo = (select  top 1  CreditAccount from BEXPORT_DOCS_PROCESSING_SETTLEMENT where PaymentId = @PaymentId) 
		set @TaiKhoanNo = (select  top 1  NostroAccount from BEXPORT_DOCS_PROCESSING_SETTLEMENT_MT910 where PaymentId = @PaymentId)
		set @TenTaiKhoanNo = (select  top 1  [Description] from BSWIFTCODE where AccountNo = @TaiKhoanNo)
		--set @TenTaiKhoanCo = (select  top 1  DrawerCusName + ' - ' + DrawerAddr1 + ' ' + DrawerAddr2 + ' ' + DrawerAddr3 from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode)
		

		set @VATNo = (SELECT top 1 VATNo FROM BEXPORT_DOCS_PROCESSING_SETTLEMENT_CHARGES WHERE CollectionPaymentCode = @PaymentId)
		
		---
		select @CustomerID = BeneficiaryNo, @CustomerName = BeneficiaryName
		from BEXPORT_LC_DOCS_PROCESSING where [AmendNo] like SUBSTRING(@PaymentId,0,14) + '%'
		---
		select @CustomerIDNo = IdentityNo, @CustomerBankAcc = BankAccount, @Address1 = [Address], @Address2 = [City], @Address3 = [Country]
		from dbo.BCUSTOMERS where CustomerID = @CustomerID
		---
		declare @TongSoTienThanhToan float, @TongVAT float
		select @TongSoTienThanhToan = sum(cast(isnull(ChargeAmt,'0') as float) + cast(isnull(TaxAmt,'0') as float)), @TongVAT = sum(cast(isnull(TaxAmt,'0') as float))
		from dbo.BEXPORT_DOCS_PROCESSING_SETTLEMENT_CHARGES where CollectionPaymentCode = @PaymentId and PartyCharged = 'A' and Chargecode NOT IN ('ELC.OVERSEASPLUS', 'ELC.OVERSEASMINUS')
		---
		select a.CollectionPaymentCode PaymentId, a.Chargecode ChargeTab, b.Name_vn ChargeName, ChargeAmt, TaxAmt, b.PLAccount, a.ChargeCcy, rowchages
		into #tblCharge
		from BEXPORT_DOCS_PROCESSING_SETTLEMENT_CHARGES a
			inner join BCHARGECODE b on a.ChargeCode = b.Code
		where CollectionPaymentCode = @PaymentId and ChargeAmt is not null

		Set @chargeRemark = (select top 1 a.ChargeRemarks
		from BEXPORT_DOCS_PROCESSING_SETTLEMENT_CHARGES a
			inner join BCHARGECODE b on a.ChargeCode = b.Code
		where CollectionPaymentCode = @PaymentId and ChargeAmt is not null)
		---
		select PaymentId Id, (SELECT DATEPART(d, GETDATE())) as [Day], (SELECT DATEPART(m, GETDATE())) as [Month], (SELECT DATEPART(yy, GETDATE())) as [Year],
			[PaymentId] LCCode, @UserId CurrentUserLogin, @CustomerName CustomerName, @CustomerID CustomerID, @CustomerIDNo IdentityNo, 
			(@Address1 + ' ' + @Address2 + ' ' + @Address3) [Address],
			@CustomerBankAcc BankAccount, @CustomerBankAcc DebitAccount, @TaiKhoanCo CreaditAccount, @VATNo VATNo,
			SUBSTRING(a.Currency,1,3) + ' ' + CONVERT(varchar, CONVERT(money, @TongSoTienThanhToan), 1) TongSoTienThanhToan, 
			dbo.f_CurrencyToTextVn(@TongSoTienThanhToan, SUBSTRING(a.Currency,1,3)) SoTienBangChu,
			SUBSTRING(a.Currency,1,3) + ' ' + CONVERT(varchar, CONVERT(money, @TongVAT), 1) + ' PL90304' VAT, [LCType] CollectionType
		into #tblPayment
		from BEXPORT_DOCS_PROCESSING_SETTLEMENT a where PaymentId = @PaymentId
		---
		select a.*, @chargeRemark ChargeRemarks
			, b1.ChargeName ChargeName_1
			--, b1.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b1.ChargeAmt )) + CONVERT(money,  ISNULL(b1.TaxAmt,0)), 1) + ' ' + b1.PLAccount ChargeInfo_1
			, b1.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b1.ChargeAmt )), 1) + ' ' + b1.PLAccount ChargeInfo_1
			, b2.ChargeName ChargeName_2
			--, b2.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b2.ChargeAmt )) + CONVERT(money,  ISNULL(b2.TaxAmt,0)), 1) + ' ' + b2.PLAccount ChargeInfo_2
			, b2.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b2.ChargeAmt )), 1) + ' ' + b2.PLAccount ChargeInfo_2
			, b3.ChargeName ChargeName_3
			--, b3.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b3.ChargeAmt )) + CONVERT(money,  ISNULL(b3.TaxAmt,0)), 1) + ' ' + b3.PLAccount ChargeInfo_3
			, b3.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b3.ChargeAmt )) , 1) + ' ' + b3.PLAccount ChargeInfo_3
			, b4.ChargeName ChargeName_4
			---, b4.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b4.ChargeAmt )) + CONVERT(money,  ISNULL(b4.TaxAmt,0)), 1) + ' ' + b4.PLAccount ChargeInfo_4
			, b4.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b4.ChargeAmt )), 1) + ' ' + b4.PLAccount ChargeInfo_4
		from #tblPayment a
			left join #tblCharge b1 on a.Id = b1.PaymentId and b1.ChargeTab = 'ELC.RECEIVE' and b1.rowchages = 1--'ELC.RECEIVE'
			left join #tblCharge b2 on a.Id = b2.PaymentId and b2.ChargeTab = 'ELC.COURIER' and b2.rowchages = 2--'ELC.COURIER'
			left join #tblCharge b3 on a.Id = b3.PaymentId and b3.ChargeTab = 'ELC.OTHER' and b3.rowchages = 3
			left join #tblCharge b4 on a.Id = b4.PaymentId and b4.ChargeTab = 'ELC.SETTLEMENT'and b4.rowchages = 4
		
		return
	END

	---3: phieu xuat ngoai bang
	if @ReportType=3
	begin	
		select @LCCode =  CollectionPaymentCode from BEXPORT_DOCS_PROCESSING_SETTLEMENT where PaymentId = @PaymentId

		---
		select @CustomerID = BeneficiaryNo, 
		@CustomerName = BeneficiaryName, 
		@currency = Currency, 
		@Amount = Amount
		from BEXPORT_LC_DOCS_PROCESSING where [AmendNo] like @LCCode + '%'
		---
		select @CustomerIDNo = IdentityNo, @CustomerBankAcc = BankAccount, @Address1 = [Address], @Address2 = [City], @Address3 = [Country]
		from dbo.BCUSTOMERS where CustomerID = @CustomerID

		select (SELECT DATEPART(d, GETDATE())) as [Day], (SELECT DATEPART(m, GETDATE())) as [Month], (SELECT DATEPART(yy, GETDATE())) as [Year], @UserId CurrentUserLogin,
		@PaymentId DocCollectCode, @CustomerName CustomerName, @CustomerIDNo IdentityNo, 
		@Address1 [Address], @Address2 City, @Address3 Country, @Amount Amount, @currency Currency,
		dbo.f_CurrencyToTextVn(@Amount, SUBSTRING(a.Currency,1,3)) SoTienVietBangChu
		from BEXPORT_DOCS_PROCESSING_SETTLEMENT a where PaymentId = @PaymentId
	end 
end

GO

/***
---------------------------------------------------------------------------------
-- 2 May 2017 : Nghia : 
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'P_ExportLCPaymentReport')
BEGIN
DROP PROCEDURE [dbo].[P_ExportLCPaymentReport]
END
GO
CREATE PROCEDURE [dbo].[P_ExportLCPaymentReport](
	@ReportType smallint,--1 : PhieuChuyenKhoan, 2 : VAT
	@PaymentId VARCHAR(50),
	@UserId varchar(50))
as
-- P_ExportLCPaymentReport 2, 'TF-14250-00149.1', 'a'
begin
	declare @DocId bigint, @VATNo varchar(50), @LCCode varchar(50)
	declare @CustomerID varchar(50), @CustomerName nvarchar(250), @CustomerIDNo varchar(50), @Address1 nvarchar(500), @Address2 nvarchar(500), @Address3 nvarchar(500), @CustomerBankAcc NVARCHAR(50), @CollectionType nvarchar(10)	
	declare @TaiKhoanNo nvarchar(50), @TenTaiKhoanNo nvarchar(max), @TenTaiKhoanCo nvarchar(max), @currency nvarchar(10)
	declare @OverseasMinus float, @OverseasPlus float, @Amount float
	---1 : PhieuChuyenKhoan
	if @ReportType = 1
	begin			
		
		select  top 1 @LCCode = CollectionPaymentCode, @currency = Currency from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId
		set @VATNo = (SELECT top 1 VATNo FROM BOUTGOINGCOLLECTIONPAYMENTCHARGES WHERE CollectionPaymentCode = @PaymentId)
		(SELECT top 1 @OverseasMinus = ChargeAmt FROM BOUTGOINGCOLLECTIONPAYMENTCHARGES WHERE CollectionPaymentCode = @PaymentId and Chargecode = 'EC.OVERSEASMINUS')
		(SELECT top 1 @OverseasPlus = ChargeAmt FROM BOUTGOINGCOLLECTIONPAYMENTCHARGES WHERE CollectionPaymentCode = @PaymentId and Chargecode = 'EC.OVERSEASPLUS')

		set @TaiKhoanNo = (select  top 1  PresentorCusNo from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId)
		set @TenTaiKhoanNo = (select  top 1  [Description] from BSWIFTCODE where AccountNo = @TaiKhoanNo)
		--set @TenTaiKhoanCo = (select  top 1  DrawerCusName + ' - ' + DrawerAddr1 + ' ' + DrawerAddr2 + ' ' + DrawerAddr3 from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode)
		(select  top 1 @CustomerID = DrawerCusNo, @TenTaiKhoanCo = DrawerCusName, @CollectionType = CollectionType from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode)

		---
		select @CustomerIDNo = IdentityNo, @CustomerBankAcc = BankAccount, @Address1 = [Address]
		from dbo.BCUSTOMERS where CustomerID = @CustomerID
		---

		select (SELECT DATEPART(d, GETDATE())) as [Day], (SELECT DATEPART(m, GETDATE())) as [Month], (SELECT DATEPART(yy, GETDATE())) as [Year],
			CollectionPaymentCode LCCode, @VATNo VATNo, @UserId CurrentUserLogin, 
			@TaiKhoanNo SoTaiKhoanNo, @TenTaiKhoanNo TenTaiKhoanNo,
			--Currency + cast(DrawingAmount AS VARCHAR) SoTienTaiKhoanNo, 
			case when Currency = 'JPY' OR Currency = 'VND' 
				then (REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),DrawingAmount) as decimal(18,0))), 1),'.00','') + ' ' + Currency)
				else (CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),DrawingAmount) as decimal(18,2))), 1) + ' ' + Currency) end as SoTienTaiKhoanNo,

			--REPLACE(CONVERT(varchar, CONVERT(money, cast(isnull(DrawingAmount,0) as decimal(18,2))), 1) , '.00', '')+ ' ' + Currency AS SoTienTaiKhoanNo,
			dbo.f_CurrencyToTextVn(convert(numeric(32,0),DrawingAmount), Currency) SoTienTaiKhoanNoBangChu,
			--Currency + cast(DrawingAmount AS VARCHAR) SoTienTaiKhoanCo, 
			
			case when Currency = 'JPY' OR Currency = 'VND' 
				then (REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),DrawingAmount) as decimal(18,0))), 1),'.00','') + ' ' + Currency)
				else (CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),DrawingAmount) as decimal(18,2))), 1) + ' ' + Currency) end as SoTienTaiKhoanCo,
			
			--REPLACE(CONVERT(varchar, CONVERT(money, cast(isnull(DrawingAmount,0) as decimal(18,2))), 1), '.00', '') + ' ' + Currency AS SoTienTaiKhoanCo,

			dbo.f_CurrencyToTextVn(convert(numeric(32,0),DrawingAmount), Currency) SoTienTaiKhoanCoBangChu,
			@CustomerBankAcc SoTaiKhoanCo, @TenTaiKhoanCo TenTaiKhoanCo, @CollectionType CollectionType
		from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId
		
		return
	end
	---2 : VAT
	if @ReportType = 2
	begin
		Declare @TaiKhoanCo nvarchar(50);
		Declare @collType nvarchar(10);
		Declare @DebitAccount nvarchar(50);
		(select  top 1 @TaiKhoanCo = CreditAccount, @TaiKhoanNo =PresentorCusNo from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId) 
		--set @TaiKhoanNo = (select  top 1  PresentorCusNo from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId)
		set @TenTaiKhoanNo = (select  top 1  [Description] from BSWIFTCODE where AccountNo = @TaiKhoanNo)
		--set @TenTaiKhoanCo = (select  top 1  DrawerCusName + ' - ' + DrawerAddr1 + ' ' + DrawerAddr2 + ' ' + DrawerAddr3 from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode)
		

		set @VATNo = (SELECT top 1 VATNo FROM BOUTGOINGCOLLECTIONPAYMENTCHARGES WHERE CollectionPaymentCode = @PaymentId)
		
		select @LCCode =  CollectionPaymentCode from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId
		---
		select @CustomerID = DrawerCusNo, @CustomerName = DrawerCusName, @collType = CollectionType
		from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode
		---
		select @CustomerIDNo = IdentityNo, @CustomerBankAcc = BankAccount, @Address1 = [Address]
		from dbo.BCUSTOMERS where CustomerID = @CustomerID
		
		---
		declare @TongSoTienThanhToan float, @TongVAT float
		select @TongSoTienThanhToan = sum(cast(isnull(ChargeAmt,'0') as float) + cast(isnull(TaxAmt,'0') as float)), 
			@TongVAT = sum(cast(isnull(TaxAmt,'0') as float))
		from dbo.BOUTGOINGCOLLECTIONPAYMENTCHARGES where CollectionPaymentCode = @PaymentId and PartyCharged = 'B' and Chargecode NOT IN ('EC.OVERSEASPLUS', 'EC.OVERSEASMINUS')
		---
		select a.CollectionPaymentCode PaymentId, a.Chargecode ChargeTab, b.Name_vn ChargeName, ChargeAmt, TaxAmt, b.PLAccount, a.ChargeCcy, rowchages
		into #tblCharge
		from BOUTGOINGCOLLECTIONPAYMENTCHARGES a
			inner join BCHARGECODE b on a.ChargeCode = b.Code
		where CollectionPaymentCode = @PaymentId and ChargeAmt is not null
		---
		select PaymentId Id, (SELECT DATEPART(d, GETDATE())) as [Day], (SELECT DATEPART(m, GETDATE())) as [Month], (SELECT DATEPART(yy, GETDATE())) as [Year],
			@LCCode LCCode, @UserId CurrentUserLogin, @CustomerName CustomerName, @CustomerID CustomerID, @CustomerIDNo IdentityNo, @Address1 [Address],
			@CustomerBankAcc BankAccount, bacc.DepositCode DebitAccount, @TaiKhoanCo CreaditAccount, @VATNo VATNo,
			--SUBSTRING(a.Currency,1,3) + ' ' + CONVERT(varchar, CONVERT(money, @TongSoTienThanhToan), 1) TongSoTienThanhToan, 
			case when a.Currency = 'JPY' OR a.Currency = 'VND' 
				then (SUBSTRING(a.Currency,1,3) + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),@TongSoTienThanhToan) as decimal(18,0))), 1),'.00',''))
				else (SUBSTRING(a.Currency,1,3) + ' ' + CONVERT(varchar, CONVERT(money, @TongSoTienThanhToan), 1)) end as TongSoTienThanhToan,

			dbo.f_CurrencyToTextVn(convert(numeric(32,0),@TongSoTienThanhToan), SUBSTRING(a.Currency,1,3)) SoTienBangChu,
			--SUBSTRING(a.Currency,1,3) + ' ' + CONVERT(varchar, CONVERT(money, @TongVAT), 1) + ' PL90304' VAT, 
			case when a.Currency = 'JPY' OR a.Currency = 'VND' 
				then (SUBSTRING(a.Currency,1,3) + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),@TongVAT) as decimal(18,0))), 1),'.00','')  + ' PL90304')
				else (SUBSTRING(a.Currency,1,3) + ' ' + CONVERT(varchar, CONVERT(money, @TongVAT), 1)  + ' PL90304') end as VAT,
			@collType CollectionType
		into #tblPayment
		from BOUTGOINGCOLLECTIONPAYMENT a 
			left join BACCOUNTS bacc on bacc.CustomerID = @CustomerID and Currentcy = a.Currency
		where PaymentId = @PaymentId
		---
		select a.*, 
			  --b1.ChargeName ChargeName_1, b1.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b1.ChargeAmt )), 1) + ' ' + b1.PLAccount ChargeInfo_1
			 case when b1.ChargeCcy = 'JPY' OR b1.ChargeCcy = 'VND' 
				then (b1.ChargeCcy + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),b1.ChargeAmt) as decimal(18,0))), 1),'.00','')  + b1.PLAccount)
				else (b1.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, b1.ChargeAmt), 1)  + b1.PLAccount) end as ChargeInfo_1
			--, b2.ChargeName ChargeName_2, b2.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b2.ChargeAmt )), 1) + ' ' + b2.PLAccount ChargeInfo_2
			, case when b2.ChargeCcy = 'JPY' OR b2.ChargeCcy = 'VND' 
				then (b2.ChargeCcy + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),b2.ChargeAmt) as decimal(18,0))), 1),'.00','')  + b2.PLAccount)
				else (b2.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, b2.ChargeAmt), 1)  + b2.PLAccount) end as ChargeInfo_2
			--, b3.ChargeName ChargeName_3, b3.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b3.ChargeAmt )), 1) + ' ' + b3.PLAccount ChargeInfo_3
			, case when b3.ChargeCcy = 'JPY' OR b3.ChargeCcy = 'VND' 
				then (b3.ChargeCcy + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),b3.ChargeAmt) as decimal(18,0))), 1),'.00','')  + b3.PLAccount)
				else (b3.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, b3.ChargeAmt), 1)  + b3.PLAccount) end as ChargeInfo_3
			--, b4.ChargeName ChargeName_4, b4.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b4.ChargeAmt )), 1) + ' ' + b4.PLAccount ChargeInfo_4
			,case when b4.ChargeCcy = 'JPY' OR b4.ChargeCcy = 'VND' 
				then (b4.ChargeCcy + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),b4.ChargeAmt) as decimal(18,0))), 1),'.00','')  + b4.PLAccount)
				else (b4.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, b4.ChargeAmt), 1)  + b4.PLAccount) end as ChargeInfo_4
		from #tblPayment a
			left join #tblCharge b1 on a.Id = b1.PaymentId and b1.ChargeTab = 'EC.PAYMENT' and b1.rowchages = 4--'EC.RECEIVE'
			left join #tblCharge b2 on a.Id = b2.PaymentId and b2.ChargeTab = 'EC.CABLE' and b2.rowchages = 2--'EC.COURIER'
			left join #tblCharge b3 on a.Id = b3.PaymentId and b3.ChargeTab = 'EC.HANDLING'
			left join #tblCharge b4 on a.Id = b4.PaymentId and b4.ChargeTab = 'EC.OTHER'
		
		return
	END

	---3: phieu xuat ngoai bang
	if @ReportType=3
	begin	
		select @LCCode =  CollectionPaymentCode from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId
		---
		select @CustomerID = DrawerCusNo, @CustomerName = DrawerCusName, @Address1 = DrawerAddr1, 
		@Address2 = DrawerAddr2, @Address3 = DrawerAddr3, @Amount = Amount, @currency = Currency
		from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode

		select @CustomerIDNo = IdentityNo, @CustomerBankAcc = BankAccount, @Address1 = [Address]
		from dbo.BCUSTOMERS where CustomerID = @CustomerID

		select (SELECT DATEPART(d, GETDATE())) as [Day], (SELECT DATEPART(m, GETDATE())) as [Month], (SELECT DATEPART(yy, GETDATE())) as [Year], @UserId CurrentUserLogin,
		@PaymentId DocCollectCode, @CustomerName CustomerName, @CustomerIDNo IdentityNo, 
		@Address1 [Address], @Address2 City, @Address3 Country, @Amount Amount, @currency Currency,
		dbo.f_CurrencyToTextVn(convert(numeric(32,0),@Amount), SUBSTRING(a.Currency,1,3)) SoTienVietBangChu
		from BOUTGOINGCOLLECTIONPAYMENT a where PaymentId = @PaymentId
	end 
end


GO
/***
---------------------------------------------------------------------------------
-- 9 Apr 2017 : Nghia : FT ph?i hi?n dúng format và dúng ngày giao d?ch
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'ProvisionTransfer_GetNewID')
BEGIN
DROP PROCEDURE [dbo].[ProvisionTransfer_GetNewID]
END
GO
CREATE Procedure [dbo].[ProvisionTransfer_GetNewID] 

as
DECLARE @MAXValue VARCHAR(10),@NEWValue VARCHAR(10),@NEW_ID VARCHAR(10);
SELECT @MAXValue=(select SoTT from BMACODE where MaCode='ISSURLC' )
update BMACODE set SoTT = SoTT + 1 where MaCode = 'ISSURLC'
SET @NEWValue= REPLACE(@MaxValue,'03.','')+1
SET @NEW_ID = ''+
    CASE
       WHEN LEN(@NEWValue)<5
          THEN REPLICATE('0',5-LEN(@newValue))
          ELSE ''
       END +
       @NEWValue
DECLARE @NumberOfDay int
SET @NumberOfDay = DATEDIFF(Day,CONVERT(datetime,'1/1/' + convert(nvarchar,YEAR(getdate()),103)),getdate()) + 1;
DECLARE @NumberOfDayStr nvarchar(3)
SET @NumberOfDayStr = replicate('0', 3 - len(@NumberOfDay)) + cast (@NumberOfDay as varchar)

select 'FT-'+CONVERT(nvarchar,right(YEAR(getdate()),2))+@NumberOfDayStr +'-' + @NEW_ID as Code

GO
/***
---------------------------------------------------------------------------------
-- 2 Apr 2017 : Nghia : Update currency display data
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'P_ExportLCPaymentReport')
BEGIN
DROP PROCEDURE [dbo].[P_ExportLCPaymentReport]
END
GO
CREATE PROCEDURE [dbo].[P_ExportLCPaymentReport](
	@ReportType smallint,--1 : PhieuChuyenKhoan, 2 : VAT
	@PaymentId VARCHAR(50),
	@UserId varchar(50))
as
-- P_ExportLCPaymentReport 2, 'TF-14250-00149.1', 'a'
begin
	declare @DocId bigint, @VATNo varchar(50), @LCCode varchar(50)
	declare @CustomerID varchar(50), @CustomerName nvarchar(250), @CustomerIDNo varchar(50), @Address1 nvarchar(500), @Address2 nvarchar(500), @Address3 nvarchar(500), @CustomerBankAcc NVARCHAR(50), @CollectionType nvarchar(10)	
	declare @TaiKhoanNo nvarchar(50), @TenTaiKhoanNo nvarchar(max), @TenTaiKhoanCo nvarchar(max), @currency nvarchar(10)
	declare @OverseasMinus float, @OverseasPlus float, @Amount float
	---1 : PhieuChuyenKhoan
	if @ReportType = 1
	begin			
		
		select  top 1 @LCCode = CollectionPaymentCode, @currency = Currency from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId
		set @VATNo = (SELECT top 1 VATNo FROM BOUTGOINGCOLLECTIONPAYMENTCHARGES WHERE CollectionPaymentCode = @PaymentId)
		(SELECT top 1 @OverseasMinus = ChargeAmt FROM BOUTGOINGCOLLECTIONPAYMENTCHARGES WHERE CollectionPaymentCode = @PaymentId and Chargecode = 'EC.OVERSEASMINUS')
		(SELECT top 1 @OverseasPlus = ChargeAmt FROM BOUTGOINGCOLLECTIONPAYMENTCHARGES WHERE CollectionPaymentCode = @PaymentId and Chargecode = 'EC.OVERSEASPLUS')

		set @TaiKhoanNo = (select  top 1  PresentorCusNo from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId)
		set @TenTaiKhoanNo = (select  top 1  [Description] from BSWIFTCODE where AccountNo = @TaiKhoanNo)
		--set @TenTaiKhoanCo = (select  top 1  DrawerCusName + ' - ' + DrawerAddr1 + ' ' + DrawerAddr2 + ' ' + DrawerAddr3 from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode)
		(select  top 1 @CustomerID = DrawerCusNo, @TenTaiKhoanCo = DrawerCusName, @CollectionType = CollectionType from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode)

		---
		select @CustomerIDNo = IdentityNo, @CustomerBankAcc = BankAccount, @Address1 = [Address]
		from dbo.BCUSTOMERS where CustomerID = @CustomerID
		---

		select (SELECT DATEPART(d, GETDATE())) as [Day], (SELECT DATEPART(m, GETDATE())) as [Month], (SELECT DATEPART(yy, GETDATE())) as [Year],
			CollectionPaymentCode LCCode, @VATNo VATNo, @UserId CurrentUserLogin, 
			@TaiKhoanNo SoTaiKhoanNo, @TenTaiKhoanNo TenTaiKhoanNo,
			--Currency + cast(DrawingAmount AS VARCHAR) SoTienTaiKhoanNo, 
			case when Currency = 'JPY' OR Currency = 'VND' 
				then (REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),DrawingAmount) as decimal(18,0))), 1),'.00','') + ' ' + Currency)
				else (CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),DrawingAmount) as decimal(18,2))), 1) + ' ' + Currency) end as SoTienTaiKhoanNo,

			--REPLACE(CONVERT(varchar, CONVERT(money, cast(isnull(DrawingAmount,0) as decimal(18,2))), 1) , '.00', '')+ ' ' + Currency AS SoTienTaiKhoanNo,
			dbo.f_CurrencyToTextVn(convert(numeric(32,0),DrawingAmount), Currency) SoTienTaiKhoanNoBangChu,
			--Currency + cast(DrawingAmount AS VARCHAR) SoTienTaiKhoanCo, 
			
			case when Currency = 'JPY' OR Currency = 'VND' 
				then (REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),DrawingAmount) as decimal(18,0))), 1),'.00','') + ' ' + Currency)
				else (CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),DrawingAmount) as decimal(18,2))), 1) + ' ' + Currency) end as SoTienTaiKhoanCo,
			
			--REPLACE(CONVERT(varchar, CONVERT(money, cast(isnull(DrawingAmount,0) as decimal(18,2))), 1), '.00', '') + ' ' + Currency AS SoTienTaiKhoanCo,

			dbo.f_CurrencyToTextVn(convert(numeric(32,0),DrawingAmount), Currency) SoTienTaiKhoanCoBangChu,
			@CustomerBankAcc SoTaiKhoanCo, @TenTaiKhoanCo TenTaiKhoanCo, @CollectionType CollectionType
		from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId
		
		return
	end
	---2 : VAT
	if @ReportType = 2
	begin
		Declare @TaiKhoanCo nvarchar(50);
		Declare @collType nvarchar(10);
		(select  top 1 @TaiKhoanCo = CreditAccount, @TaiKhoanNo =PresentorCusNo from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId) 
		--set @TaiKhoanNo = (select  top 1  PresentorCusNo from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId)
		set @TenTaiKhoanNo = (select  top 1  [Description] from BSWIFTCODE where AccountNo = @TaiKhoanNo)
		--set @TenTaiKhoanCo = (select  top 1  DrawerCusName + ' - ' + DrawerAddr1 + ' ' + DrawerAddr2 + ' ' + DrawerAddr3 from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode)
		

		set @VATNo = (SELECT top 1 VATNo FROM BOUTGOINGCOLLECTIONPAYMENTCHARGES WHERE CollectionPaymentCode = @PaymentId)
		
		select @LCCode =  CollectionPaymentCode from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId
		---
		select @CustomerID = DrawerCusNo, @CustomerName = DrawerCusName, @collType = CollectionType
		from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode
		---
		select @CustomerIDNo = IdentityNo, @CustomerBankAcc = BankAccount, @Address1 = [Address]
		from dbo.BCUSTOMERS where CustomerID = @CustomerID
		---
		declare @TongSoTienThanhToan float, @TongVAT float
		select @TongSoTienThanhToan = sum(cast(isnull(ChargeAmt,'0') as float) + cast(isnull(TaxAmt,'0') as float)), @TongVAT = sum(cast(isnull(TaxAmt,'0') as float))
		from dbo.BOUTGOINGCOLLECTIONPAYMENTCHARGES where CollectionPaymentCode = @PaymentId and PartyCharged = 'A' and Chargecode NOT IN ('EC.OVERSEASPLUS', 'EC.OVERSEASMINUS')
		---
		select a.CollectionPaymentCode PaymentId, a.Chargecode ChargeTab, b.Name_vn ChargeName, ChargeAmt, TaxAmt, b.PLAccount, a.ChargeCcy, rowchages
		into #tblCharge
		from BOUTGOINGCOLLECTIONPAYMENTCHARGES a
			inner join BCHARGECODE b on a.ChargeCode = b.Code
		where CollectionPaymentCode = @PaymentId and ChargeAmt is not null
		---
		select PaymentId Id, (SELECT DATEPART(d, GETDATE())) as [Day], (SELECT DATEPART(m, GETDATE())) as [Month], (SELECT DATEPART(yy, GETDATE())) as [Year],
			@LCCode LCCode, @UserId CurrentUserLogin, @CustomerName CustomerName, @CustomerID CustomerID, @CustomerIDNo IdentityNo, @Address1 [Address],
			@CustomerBankAcc BankAccount, @CustomerBankAcc DebitAccount, @TaiKhoanCo CreaditAccount, @VATNo VATNo,
			--SUBSTRING(a.Currency,1,3) + ' ' + CONVERT(varchar, CONVERT(money, @TongSoTienThanhToan), 1) TongSoTienThanhToan, 
			case when a.Currency = 'JPY' OR a.Currency = 'VND' 
				then (SUBSTRING(a.Currency,1,3) + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),@TongSoTienThanhToan) as decimal(18,0))), 1),'.00',''))
				else (SUBSTRING(a.Currency,1,3) + ' ' + CONVERT(varchar, CONVERT(money, @TongSoTienThanhToan), 1)) end as TongSoTienThanhToan,

			dbo.f_CurrencyToTextVn(convert(numeric(32,0),@TongSoTienThanhToan), SUBSTRING(a.Currency,1,3)) SoTienBangChu,
			--SUBSTRING(a.Currency,1,3) + ' ' + CONVERT(varchar, CONVERT(money, @TongVAT), 1) + ' PL90304' VAT, 
			case when a.Currency = 'JPY' OR a.Currency = 'VND' 
				then (SUBSTRING(a.Currency,1,3) + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),@TongVAT) as decimal(18,0))), 1),'.00','')  + ' PL90304')
				else (SUBSTRING(a.Currency,1,3) + ' ' + CONVERT(varchar, CONVERT(money, @TongVAT), 1)  + ' PL90304') end as VAT,
			@collType CollectionType
		into #tblPayment
		from BOUTGOINGCOLLECTIONPAYMENT a where PaymentId = @PaymentId
		---
		select a.*, 
			  --b1.ChargeName ChargeName_1, b1.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b1.ChargeAmt )), 1) + ' ' + b1.PLAccount ChargeInfo_1
			 case when b1.ChargeCcy = 'JPY' OR b1.ChargeCcy = 'VND' 
				then (b1.ChargeCcy + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),b1.ChargeAmt) as decimal(18,0))), 1),'.00','')  + b1.PLAccount)
				else (b1.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, b1.ChargeAmt), 1)  + b1.PLAccount) end as ChargeInfo_1
			--, b2.ChargeName ChargeName_2, b2.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b2.ChargeAmt )), 1) + ' ' + b2.PLAccount ChargeInfo_2
			, case when b2.ChargeCcy = 'JPY' OR b2.ChargeCcy = 'VND' 
				then (b2.ChargeCcy + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),b2.ChargeAmt) as decimal(18,0))), 1),'.00','')  + b2.PLAccount)
				else (b2.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, b2.ChargeAmt), 1)  + b2.PLAccount) end as ChargeInfo_2
			--, b3.ChargeName ChargeName_3, b3.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b3.ChargeAmt )), 1) + ' ' + b3.PLAccount ChargeInfo_3
			, case when b3.ChargeCcy = 'JPY' OR b3.ChargeCcy = 'VND' 
				then (b3.ChargeCcy + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),b3.ChargeAmt) as decimal(18,0))), 1),'.00','')  + b3.PLAccount)
				else (b3.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, b3.ChargeAmt), 1)  + b3.PLAccount) end as ChargeInfo_3
			--, b4.ChargeName ChargeName_4, b4.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, (b4.ChargeAmt )), 1) + ' ' + b4.PLAccount ChargeInfo_4
			,case when b4.ChargeCcy = 'JPY' OR b4.ChargeCcy = 'VND' 
				then (b4.ChargeCcy + ' ' + REPLACE(CONVERT(varchar, CONVERT(money, cast(convert(numeric(32,0),b4.ChargeAmt) as decimal(18,0))), 1),'.00','')  + b4.PLAccount)
				else (b4.ChargeCcy + ' ' + CONVERT(varchar, CONVERT(money, b4.ChargeAmt), 1)  + b4.PLAccount) end as ChargeInfo_4
		from #tblPayment a
			left join #tblCharge b1 on a.Id = b1.PaymentId and b1.ChargeTab = 'EC.PAYMENT' and b1.rowchages = 4--'EC.RECEIVE'
			left join #tblCharge b2 on a.Id = b2.PaymentId and b2.ChargeTab = 'EC.CABLE' and b2.rowchages = 2--'EC.COURIER'
			left join #tblCharge b3 on a.Id = b3.PaymentId and b3.ChargeTab = 'EC.HANDLING'
			left join #tblCharge b4 on a.Id = b4.PaymentId and b4.ChargeTab = 'EC.OTHER'
		
		return
	END

	---3: phieu xuat ngoai bang
	if @ReportType=3
	begin	
		select @LCCode =  CollectionPaymentCode from BOUTGOINGCOLLECTIONPAYMENT where PaymentId = @PaymentId
		---
		select @CustomerID = DrawerCusNo, @CustomerName = DrawerCusName, @Address1 = DrawerAddr1, 
		@Address2 = DrawerAddr2, @Address3 = DrawerAddr3, @Amount = Amount, @currency = Currency
		from BEXPORT_DOCUMETARYCOLLECTION where DocCollectCode = @LCCode

		select @CustomerIDNo = IdentityNo, @CustomerBankAcc = BankAccount, @Address1 = [Address]
		from dbo.BCUSTOMERS where CustomerID = @CustomerID

		select (SELECT DATEPART(d, GETDATE())) as [Day], (SELECT DATEPART(m, GETDATE())) as [Month], (SELECT DATEPART(yy, GETDATE())) as [Year], @UserId CurrentUserLogin,
		@PaymentId DocCollectCode, @CustomerName CustomerName, @CustomerIDNo IdentityNo, 
		@Address1 [Address], @Address2 City, @Address3 Country, @Amount Amount, @currency Currency,
		dbo.f_CurrencyToTextVn(convert(numeric(32,0),@Amount), SUBSTRING(a.Currency,1,3)) SoTienVietBangChu
		from BOUTGOINGCOLLECTIONPAYMENT a where PaymentId = @PaymentId
	end 
end
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
		--cast(@totalAmt as decimal(18,2)) as Amount,
		case when Currency = 'JPY' OR Currency = 'VND' 
				then REPLACE(CONVERT(varchar, CONVERT(money, cast((@totalAmt) as decimal(18,2))), 1),'.00','')
				else (CONVERT(varchar, CONVERT(money, cast((@totalAmt) as decimal(18,2))), 1)) end as Amount,
	    (select Vietnamese from dbo.BCURRENCY where Code = Currency) as Vietnamese,
	    (SELECT DATEPART(m, GETDATE())) as [Month],
	    (SELECT DATEPART(d, GETDATE())) as [Day],
	    (SELECT DATEPART(yy, GETDATE())) as [Year]
	    
	from dbo.BDOCUMETARYCOLLECTION
	where DocCollectCode = @Code and (ActiveRecordFlag = 'YES' or ActiveRecordFlag is NULL)
	
END

GO