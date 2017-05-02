SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
	    (select Vietnamese from dbo.BCURRENCY where Code = Currency) as Vietnamese,
	    (SELECT DATEPART(m, GETDATE())) as [Month],
	    (SELECT DATEPART(d, GETDATE())) as [Day],
	    (SELECT DATEPART(yy, GETDATE())) as [Year]
	    
	from dbo.BDOCUMETARYCOLLECTION
	where DocCollectCode = @Code and (ActiveRecordFlag = 'YES' or ActiveRecordFlag is NULL)
	
END

GO