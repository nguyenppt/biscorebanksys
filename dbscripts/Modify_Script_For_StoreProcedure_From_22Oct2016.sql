SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***
---------------------------------------------------------------------------------
-- 24 Dec 2016 : Nghia : Fix VND amount value
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'B_BDOCUMETARYCOLLECTION_VAT_Report')
BEGIN
DROP PROCEDURE [dbo].[B_BDOCUMETARYCOLLECTION_VAT_Report]
END
GO
CREATE PROCEDURE [dbo].[B_BDOCUMETARYCOLLECTION_VAT_Report]
	@Code varchar(50),
	@UserNameLogin  nvarchar(500),
	@ViewType int
AS
BEGIN
	declare @DraweeCusNo varchar(50), @AmendNo varchar(50)
	select @DraweeCusNo = DraweeCusNo, @AmendNo = isnull(AmendNo, @Code)
	from BDOCUMETARYCOLLECTION where DocCollectCode = @Code and ISNULL(ActiveRecordFlag,'Yes') = 'Yes'
	
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
	select CustomerName,IdentityNo, [Address],City, Country from BCUSTOMERS where CustomerID = @DraweeCusNo
	--------------------------------------------
	declare @TongSoTienThanhToan float
	set @TongSoTienThanhToan = (select  sum(ChargeAmt) from dbo.BDOCUMETARYCOLLECTIONCHARGES 
									where DocCollectCode = @AmendNo and [ViewType] = @ViewType)
			
	declare @VAT float
	set @VAT = (@TongSoTienThanhToan * 0.1)
	set @TongSoTienThanhToan = @TongSoTienThanhToan + @VAT
	
	-----------------------------------------------------------------------
	declare @Table_CHARGE as table 
	(
		DocCollectCode [nvarchar](50),
		[ChargeAmt] [float],
		[ChargeCcy] [nvarchar](5),
		[Chargecode] [nvarchar](50),
		[Rowchages] [nvarchar](3),
		[ViewType] [int],
		[ChargeRemarks] [nvarchar](500) ,
		[VATNo] [nvarchar](20) ,
		[ChargeAcct] [nvarchar](20) 
	)
	insert into @Table_CHARGE
	select
		DocCollectCode ,
		[ChargeAmt] ,
		[ChargeCcy] ,
		[Chargecode],
		[Rowchages] ,
		[ViewType]	,
		[ChargeRemarks]	,
		[VATNo],
		[ChargeAcct]
	from dbo.BDOCUMETARYCOLLECTIONCHARGES
	where DocCollectCode = @AmendNo and [ViewType] = @ViewType
	------------------------------------------------------
	
	declare @Cot9_1 float	
	declare @Cot9_2 float
	declare @Cot9_3 float
	
	declare @Cot9_1_ChargeCcy varchar(10)	
	declare @Cot9_2_ChargeCcy varchar(10)	
	declare @Cot9_3_ChargeCcy varchar(10)
		
	declare @Cot9_1Name nvarchar(500)	
	declare @Cot9_2Name nvarchar(500)	
	declare @Cot9_3Name nvarchar(500)	
	
	select 
		@Cot9_1 = ChargeAmt,
		@Cot9_1_ChargeCcy = ChargeCcy,
		@Cot9_1Name = Chargecode
	 from @Table_CHARGE
	 where Rowchages = 1
	
	if isnull(@Cot9_1Name, '') != ''
	begin
		set @Cot9_1Name = (select Name_VN from dbo.BCHARGECODE where Code = @Cot9_1Name)
	end 	
	------------
	select 
		@Cot9_2 = ChargeAmt,
		@Cot9_2_ChargeCcy = ChargeCcy,
		@Cot9_2Name = Chargecode
	 from @Table_CHARGE
	 where Rowchages = 2
	
	if isnull(@Cot9_2Name, '') != ''
	begin
		set @Cot9_2Name = (select Name_VN from dbo.BCHARGECODE where Code = @Cot9_2Name)
	end
	------------
	select 
		@Cot9_3 = ChargeAmt,
		@Cot9_3_ChargeCcy = ChargeCcy,
		@Cot9_3Name = Chargecode
	 from @Table_CHARGE
	 where Rowchages = 3	 
	
	if isnull(@Cot9_3Name, '') != ''
	begin
		set @Cot9_3Name = (select Name_VN from dbo.BCHARGECODE where Code = @Cot9_3Name)
	end 
	
	--------------------------------------------
	select @CurrentDate as CurrentDate
	select
		doc.DocCollectCode,
		(select top 1 VATNo from @Table_CHARGE) as VATNo,
		(select top 1 ChargeAcct from @Table_CHARGE) as  ChargeAcct,
		(select top 1 ChargeRemarks from @Table_CHARGE) as ChargeRemarks,
		
		@UserNameLogin as UserNameLogin,
		doc.DraweeCusName as CustomerName,
		doc.DraweeAddr1 + ', ' +doc.DraweeAddr2 + ', ' + doc.DraweeAddr3 as CustomerAddress,
		(select IdentityNo from @TabCus) as IdentityNo,		
		doc.DraweeCusNo as CustomerID,				
		--(select dbo.fuDocSoThanhChu(@TongSoTienThanhToan)) + ' ' + (select Vietnamese from dbo.BCURRENCY where Code = @Cot9_1_ChargeCcy) as SoTienBangChu,
		case when @Cot9_1_ChargeCcy = 'JPY' OR @Cot9_1_ChargeCcy = 'VND' 
			then (select dbo.f_CurrencyToText(CEILING(@TongSoTienThanhToan), @Cot9_1_ChargeCcy))
			else (select dbo.f_CurrencyToText(cast(@TongSoTienThanhToan as decimal(18,2)), @Cot9_1_ChargeCcy)) end as SoTienBangChu,
			
		case when @Cot9_1_ChargeCcy = 'JPY' OR @Cot9_1_ChargeCcy = 'VND' 
			then CONVERT(varchar, CEILING(@TongSoTienThanhToan)) + ' ' + @Cot9_1_ChargeCcy
			else CONVERT(varchar, CONVERT(money, cast(@TongSoTienThanhToan as decimal(18,2))), 1) + ' ' + @Cot9_1_ChargeCcy end as TongSoTienThanhToan,

		---@Cot9_1_ChargeCcy as test2,
		--CONVERT(varchar, CONVERT(money, cast(@TongSoTienThanhToan as decimal(18,2))), 1) + ' ' + @Cot9_1_ChargeCcy as TongSoTienThanhToan,
		
		case when @Cot9_1_ChargeCcy = 'JPY' OR @Cot9_1_ChargeCcy = 'VND' 
			then CONVERT(varchar, CEILING(@VAT)) + ' ' + @Cot9_1_ChargeCcy + ' PL90304'
			else CONVERT(varchar, CONVERT(money, cast(@VAT as decimal(18,2))), 1) + ' ' + @Cot9_1_ChargeCcy + ' PL90304' end as VAT,
		
		--CONVERT(varchar, CONVERT(money, cast(@VAT as decimal(18,2))), 1) + ' ' + @Cot9_1_ChargeCcy + ' PL90304' as VAT,

		case when @Cot9_1_ChargeCcy = 'JPY' OR @Cot9_1_ChargeCcy = 'VND' 
			then (case when isnull(@Cot9_1, 0) > 0 then CONVERT(varchar, CEILING(@Cot9_1)) + ' ' + @Cot9_1_ChargeCcy + ' PL737869' else '' end)
			else (case when isnull(@Cot9_1, 0) > 0 then CONVERT(varchar, CONVERT(money, @Cot9_1), 1) + ' ' + @Cot9_1_ChargeCcy + ' PL737869' else '' end) end as Cot9_1,

		--case when isnull(@Cot9_1, 0) > 0 then CONVERT(varchar, CONVERT(money, @Cot9_1), 1) + ' ' + @Cot9_1_ChargeCcy + ' PL737869' else '' end as Cot9_1,

		case when @Cot9_1_ChargeCcy = 'JPY' OR @Cot9_1_ChargeCcy = 'VND' 
			then (case when isnull(@Cot9_2, 0) > 0 then CONVERT(varchar, CEILING(@Cot9_2)) + ' ' + @Cot9_2_ChargeCcy + ' PL837870' else '' end)
			else (case when isnull(@Cot9_2, 0) > 0 then CONVERT(varchar, CONVERT(money, @Cot9_2), 1) + ' ' + @Cot9_2_ChargeCcy + ' PL837870' else '' end) end as Cot9_2,
		--case when isnull(@Cot9_2, 0) > 0 then CONVERT(varchar, CONVERT(money, @Cot9_2), 1) + ' ' + @Cot9_2_ChargeCcy + ' PL837870' else '' end as Cot9_2,
		
		case when @Cot9_1_ChargeCcy = 'JPY' OR @Cot9_1_ChargeCcy = 'VND' 
			then (case when isnull(@Cot9_3, 0) > 0 then CONVERT(varchar, CEILING(@Cot9_3), 1) + ' ' + @Cot9_3_ChargeCcy + ' PL837304' else '' end)
			else (case when isnull(@Cot9_3, 0) > 0 then CONVERT(varchar, CONVERT(money, @Cot9_3), 1) + ' ' + @Cot9_3_ChargeCcy + ' PL837304' else '' end) end as Cot9_3,
		--case when isnull(@Cot9_3, 0) > 0 then CONVERT(varchar, CONVERT(money, @Cot9_3), 1) + ' ' + @Cot9_3_ChargeCcy + ' PL837304' else '' end as Cot9_3,
		
		case when isnull(@Cot9_1, 0) > 0 then @Cot9_1Name else '' end as Cot9_1Name,
		case when isnull(@Cot9_2, 0) > 0 then @Cot9_2Name else '' end as Cot9_2Name,
		case when isnull(@Cot9_3, 0) > 0 then @Cot9_3Name else '' end as Cot9_3Name	
		
	from dbo.BDOCUMETARYCOLLECTION doc
	where DocCollectCode = @Code and ISNULL(ActiveRecordFlag,'Yes') = 'Yes'
END
GO


/***
---------------------------------------------------------------------------------
-- 18 Dec 2016 : Nghia : Remove duplicate currency
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'B_INCOMINGCOLLECTIONPAYMENT_PHIEUCHUYENKHOAN_Report')
BEGIN
DROP PROCEDURE [dbo].[B_INCOMINGCOLLECTIONPAYMENT_PHIEUCHUYENKHOAN_Report]
END
GO
CREATE PROCEDURE [dbo].[B_INCOMINGCOLLECTIONPAYMENT_PHIEUCHUYENKHOAN_Report]
	@Code varchar(50),
	@UserNameLogin  nvarchar(500)
AS
BEGIN
	declare @CurrentDate varchar(15)
	set @CurrentDate = CONVERT(VARCHAR(10),GETDATE(),101);
	--declare @ChargeAmt FLOAT
	--declare @VAT FLOAT
	--declare @SoTienTaiKhoanCo FLOAT
	---- get partycharge: B/AC from BINCOMINGCOLLECTIONPAYMENTCHARGES
	--declare @PartyCharged varchar(10)
	--set @PartyCharged = (select top 1 PartyCharged
	--						from dbo.BINCOMINGCOLLECTIONPAYMENTCHARGES 
	--						where CollectionPaymentCode = @Code)
							
	--set @ChargeAmt = isnull((select sum(ChargeAmt) 
	--						from BINCOMINGCOLLECTIONPAYMENTCHARGES 
	--						where CollectionPaymentCode = @Code),0)
							
	--set @PaymentAmount = isnull((select sum(PaymentAmount) 
	--						from BINCOMINGCOLLECTIONPAYMENT 
	--						where CollectionPaymentCode = @Code),0)
							
	--set @VAT =  isnull((@ChargeAmt * 0.1), 0)
	
	--1. N?u code phí là B thì m?i có VAT 
	--va (S? ti?n phí này s? du?c TRU vào s? ti?n trong amount c?a 202 
	--và TRU vào trong phi?u chuy?n kho?n)
	--if @PartyCharged = 'B'
	--begin
	--	set @ChargeAmt = @ChargeAmt - @VAT
	--end
	-- 2. N?u code phí là AC thì không có VAT. 
	--S? ti?n phí này s? du?c c?ng vào s? ti?n trong amount c?a 202 
	--và c?ng vào trong phi?u chuy?n kho?n
	--else if @PartyCharged = 'AC'
	--begin
	--	set @ChargeAmt = @ChargeAmt + @VAT
	--end
	declare @VATNo nvarchar(500)
	set @VATNo = (select top 1 VATNo from 
				dbo.BINCOMINGCOLLECTIONPAYMENTCHARGES 
				where CollectionPaymentCode = @Code)
	
	----------------------------------------------------------
	declare @Table_TempKhoanNo as table 
	(
		Position INT,
		Value nvarchar(2000)
	)
	insert into @Table_TempKhoanNo
	select position,value from dbo.fn_Split((select top 1 DrFromAccount from dbo.BINCOMINGCOLLECTIONPAYMENT where PaymentId = @Code), '-')
	-------------------------------------------------------------
	select @CurrentDate as CurrentDate
	select
		pay.CollectionPaymentCode,
		@UserNameLogin as UserNameLogin,
		@VATNo as VATNo,
		(SELECT DATEPART(m, GETDATE())) as [Month],
	    (SELECT DATEPART(d, GETDATE())) as [Day],
	    (SELECT DATEPART(yy, GETDATE())) as [Year],	    
		(select cast(Value as nvarchar) from @Table_TempKhoanNo where Position = 1) as SoTaiKhoanNo,
		(select '' + Value from @Table_TempKhoanNo where Position = 2) as TenTaiKhoanNo,
		CONVERT(varchar, CONVERT(money, mt202.Amount), 1) + ' ' + pay.Currency as SoTienTaiKhoanNo,
		(select dbo.f_CurrencyToTextVn(mt202.Amount, mt202.Currency)) as SoTienTaiKhoanNoBangChu,
		
		(select AccountNo from dbo.BSWIFTCODE where Code =  NostroAcct) as SoTaiKhoanCo,
		(select [Description] from dbo.BSWIFTCODE where Code =  NostroAcct) as TenTaiKhoanCo,
		 CONVERT(varchar, CONVERT(money, (mt202.Amount)), 1) + ' ' + pay.Currency as SoTienTaiKhoanCo,
		 (select dbo.f_CurrencyToText(mt202.Amount, mt202.Currency)) as SoTienTaiKhoanCoBangChu
		
	from dbo.BINCOMINGCOLLECTIONPAYMENT pay
	inner join dbo.BINCOMINGCOLLECTIONPAYMENTMT202 mt202 on mt202.CollectionPaymentCode = pay.PaymentId
	where pay.PaymentId = @Code
END

GO
/***
---------------------------------------------------------------------------------
-- 13 Dec 2016 : Nghia : Modify Update the status change to AUT
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'B_BFREETEXTMESSAGE_Insert')
BEGIN
DROP PROCEDURE [dbo].[B_BFREETEXTMESSAGE_Insert]
END
GO
CREATE PROCEDURE [dbo].[B_BFREETEXTMESSAGE_Insert]
	@Id varchar(10)
	,@WaiveCharge varchar(20)
	,@TFNo varchar(50)
	,@CableType varchar(20)
	,@ReviverDesc nvarchar(500)
	,@ReviverCode varchar(50)
	,@RelatedReference nvarchar(500)
	,@Narrative nvarchar(4000)
	,@CurrentUserId INT
AS
BEGIN
	IF NOT EXISTS(SELECT Id FROM dbo.[BFREETEXTMESSAGE] WHERE Id = @Id)
	begin
		INSERT INTO [dbo].[BFREETEXTMESSAGE]
           ([WaiveCharge]
           ,[TFNo]
           ,[CableType]
           ,[ReviverDesc]
           ,[ReviverCode]
           ,[RelatedReference]
           ,[Narrative]
           ,[Status]
           , CreateDate
           , CreateBy
           )
     VALUES
           (
			@WaiveCharge
			,@TFNo
			,@CableType 
			,@ReviverDesc 
			,@ReviverCode
			,@RelatedReference
			,@Narrative 
			,'UNA'
			, getdate()
			, @CurrentUserId
           )
	end
	else 
	begin
		UPDATE [dbo].[BFREETEXTMESSAGE]
		   SET [WaiveCharge] = @WaiveCharge
			  ,[TFNo] = @TFNo
			  ,[CableType] = @CableType
			  ,[ReviverDesc] = @ReviverDesc
			  ,[ReviverCode] =@ReviverCode
			  ,[RelatedReference] = @RelatedReference
			  ,[Narrative] = @Narrative
			  ,[Status] = 'UNA'
			
			  ,[UpdatedDate] = getdate()
			  ,[UpdatedBy] = @CurrentUserId
			 
		 WHERE Id = @Id
	end
	

END

GO
/***
---------------------------------------------------------------------------------
-- 13 Nov 2016 : Nghia : Add RemittingType to table
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'B_BDOCUMETARYCOLLECTION_GetByDocCollectCode')
BEGIN
DROP PROCEDURE [dbo].[B_BDOCUMETARYCOLLECTION_GetByDocCollectCode]
END
GO
CREATE PROCEDURE [dbo].[B_BDOCUMETARYCOLLECTION_GetByDocCollectCode] --'TF-14228-00612', 218
	@DocCollectCode varchar(50),
	@ViewType int
AS
BEGIN
	-- B_BDOCUMETARYCOLLECTION_GetByDocCollectCode 'TF-14274-00181', 218
	DECLARE @IncreaseMental float, @DocsCode VARCHAR(50), @AmendNo VARCHAR(50), @NewAmendNo VARCHAR(50)
	SET @IncreaseMental = 0
	IF @ViewType = 218--Amend
	BEGIN
		IF CHARINDEX('.', @DocCollectCode) > 0--Load detail by AmendNo
		BEGIN
			SET @DocsCode = SUBSTRING(@DocCollectCode, 1, CHARINDEX('.', @DocCollectCode)-1)
			SET @AmendNo = @DocCollectCode
			SET @NewAmendNo = ''
		END
		ELSE--Load Docs for amend
		BEGIN
			SET @DocsCode = @DocCollectCode
			--is amending ?
			SELECT @AmendNo = isnull(AmendNo, @DocsCode) from dbo.BDOCUMETARYCOLLECTION
			where DocCollectCode = @DocsCode AND ISNULL(ActiveRecordFlag,'Yes') = 'Yes' AND isnull(Amend_Status,'') = 'UNA'
			IF @AmendNo IS NULL
			BEGIN
				--create new AmendNo
				SELECT @NewAmendNo = max(ISNULL(AmendNo,'')) 
				from dbo.BDOCUMETARYCOLLECTION where DocCollectCode = @DocsCode 
				IF @NewAmendNo = ''
					SET @NewAmendNo = @DocsCode + '.1'
				ELSE
				BEGIN
					DECLARE @i BIGINT
					SET @i = CHARINDEX('.', @NewAmendNo)
					SET @NewAmendNo = @DocsCode + '.' + CAST((cast(SUBSTRING(@NewAmendNo, @i + 1, LEN(@NewAmendNo) - @i) AS BIGINT) + 1) AS VARCHAR)
				END
				--- get lastest record
				SELECT @AmendNo = isnull(AmendNo, @DocsCode) from dbo.BDOCUMETARYCOLLECTION
				where DocCollectCode = @DocsCode AND ISNULL(ActiveRecordFlag,'Yes') = 'Yes'
			END
		END
		---Load detail
		--PRINT @DocsCode + '^' + @AmendNo + '^' + @NewAmendNo
	END
	ELSE
	BEGIN
		SET @DocsCode = @DocCollectCode
		
		set @IncreaseMental = (select max(IncreaseMental) from dbo.BINCOMINGCOLLECTIONPAYMENT 
		where CollectionPaymentCode = @DocCollectCode)
		
		SELECT @AmendNo = AmendNo from dbo.BDOCUMETARYCOLLECTION
		where DocCollectCode = @DocCollectCode AND ISNULL(ActiveRecordFlag,'Yes') = 'Yes'		
	END
	
	-----------------
	select [Id],[DocCollectCode],[CollectionType],[RemittingBankNo],[RemittingBankAddr],[RemittingBankAcct],[RemittingBankRef],[DraweeType],[DraweeCusNo],
		[DraweeAddr1],[DraweeAddr2],[DraweeAddr3],[ReimbDraweeAcct],[DrawerType],[DrawerCusNo],[DrawerAddr],[Currency],[Amount],[DocsReceivedDate],[MaturityDate],
		[Tenor],[Tenor_New],[Days],[TracerDate],[TracerDate_New],[ReminderDays],[Commodity],[DocsCode1],[NoOfOriginals1],[NoOfCopies1],[DocsCode2],[NoOfOriginals2],
		[NoOfCopies2],[OtherDocs],[InstructionToCus],[Status],[CreateDate],[CreateBy],[UpdatedDate],[UpdatedBy],[AuthorizedBy],[AuthorizedDate],[DrawerAddr1],[DrawerAddr2],
		[Remarks],[CancelDate],[ContingentExpiryDate],[DrawerCusName],[DraweeCusName],[AccountOfficer],[ExpressNo],[InvoiceNo],[CancelRemark],[RemittingBankAddr2],
		[RemittingBankAddr3],[Cancel_Status],[CancelBy],[AcceptedDate],[AcceptRemarks],[Accept_Status],[AcceptBy],[AcceptByDate],[PaymentFullFlag],
		case when @NewAmendNo is null then [Amend_Status] else NULL end [Amend_Status],
		isnull(case 
			when @ViewType = 217 then isnull(Amount,0)
			when @ViewType = 218 then isnull(Amount,0)
			when @ViewType = 281 then isnull(Amount,0)
			when @ViewType = 219 then 
				case when isnull(@IncreaseMental,0) > 0 then (Amount - @IncreaseMental) else Amount end 
			end, 0) as B4_AUT_Amount, isnull(OldAmount,0) Amount_Old,
		DraftNo, @NewAmendNo NewAmendNo, AmendNo, RemittingType
	from dbo.BDOCUMETARYCOLLECTION
	where DocCollectCode = @DocsCode AND ISNULL(AmendNo, @DocsCode) = ISNULL(@AmendNo, @DocsCode) AND ISNULL(ActiveRecordFlag,'Yes') = 'Yes'	
	
	-- tab Charge
	select * from dbo.BDOCUMETARYCOLLECTIONCHARGES
	where DocCollectCode = @DocCollectCode and [Rowchages] = '1' and ViewType = @ViewType
	
	select * from dbo.BDOCUMETARYCOLLECTIONCHARGES
	where DocCollectCode = @DocCollectCode and [Rowchages] = '2' and ViewType = @ViewType
	-- tab Charge
	
	-- tab MT410
	select 'MT410' as [Identifier],* from dbo.BDOCUMETARYCOLLECTIONMT410
	where DocCollectCode = ISNULL(@AmendNo, @DocsCode)

	-- tab MT412
	select 'MT412' as [Identifier],* from dbo.BDOCUMETARYCOLLECTIONMT412
	where DocCollectCode = @DocsCode
END
GO


/***
---------------------------------------------------------------------------------
-- 13 Nov 2016 : Nghia : Add RemittingType to insert script
---------------------------------------------------------------------------------
***/
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME = 'B_BDOCUMETARYCOLLECTION_Insert')
BEGIN
DROP PROCEDURE [dbo].[B_BDOCUMETARYCOLLECTION_Insert]
END
GO

CREATE PROCEDURE [dbo].[B_BDOCUMETARYCOLLECTION_Insert]
		  @DocCollectCode varchar(50),
           @CollectionType nvarchar(250),
		   @RemittingType varchar(50),
           @RemittingBankNo nvarchar(250),
           @RemittingBankAddr nvarchar(250),
           @RemittingBankAcct nvarchar(250),
           @RemittingBankRef nvarchar(500),
          @DraweeCusNo nvarchar(250),
           @DraweeAddr1 nvarchar(500),
           @DraweeAddr2 nvarchar(500),
          @DraweeAddr3 nvarchar(500),
           @ReimbDraweeAcct nvarchar(250),
           @DrawerCusNo nvarchar(250),
           @DrawerAddr nvarchar(500),
           @Currency nvarchar(250),
           @Amount nvarchar(250),
           @DocsReceivedDate nvarchar(250),
           @MaturityDate nvarchar(250),
           @Tenor nvarchar(250),
           @Days nvarchar(250),
           @TracerDate nvarchar(250),
           @ReminderDays nvarchar(250),
           @Commodity nvarchar(250),
           @DocsCode1 nvarchar(250),
           @NoOfOriginals1 nvarchar(250),
           @NoOfCopies1 nvarchar(250),
           @DocsCode2 nvarchar(250),
           @NoOfOriginals2 nvarchar(250),
           @NoOfCopies2 nvarchar(250),
           @OtherDocs nvarchar(4000),
           @InstructionToCus nvarchar(4000),
           @CurrentUserId varchar(5),
           @DrawerAddr1 nvarchar(500),
           @DrawerAddr2 nvarchar(500),
           @Remarks nvarchar(500),
           @CancelDate nvarchar(250),
           @ContingentExpiryDate nvarchar(250),           
           @DrawerCusName nvarchar(500),
           @DraweeCusName nvarchar(500),
           @DraweeType varchar(50),
           @DrawerType varchar(50),
           @AccountOfficer NVARCHAR(250),
           @ExpressNo NVARCHAR(500),
           @InvoiceNo NVARCHAR(500),
           @CancelRemark NVARCHAR(500),
           @RemittingBankAddr2 nvarchar(500),
           @RemittingBankAddr3 nvarchar(500),
           @comeFromUrl varchar(50),
           @AcceptedDate  varchar(100),
           @AcceptRemarks  nvarchar(500),
           @DraftNo nvarchar(500)
AS
BEGIN
	IF CHARINDEX('.', @DocCollectCode) > 0---Amend
	BEGIN
		IF EXISTS(SELECT DocCollectCode FROM BDOCUMETARYCOLLECTION WHERE ISNULL(AmendNo,'') = @DocCollectCode)
		BEGIN
			update BDOCUMETARYCOLLECTION set ActiveRecordFlag = 'No' 
			where isnull(ActiveRecordFlag,'Yes') = 'Yes' and DocCollectCode = (SELECT DocCollectCode FROM BDOCUMETARYCOLLECTION WHERE ISNULL(AmendNo,'') = @DocCollectCode)
			---
			UPDATE [dbo].[BDOCUMETARYCOLLECTION]
			SET [CollectionType] = @CollectionType,[RemittingBankNo] = @RemittingBankNo,[RemittingBankAddr] = @RemittingBankAddr,[RemittingBankAcct] = @RemittingBankAcct,
				[RemittingBankRef] = @RemittingBankRef,[DraweeCusNo] = @DraweeCusNo,[DraweeAddr1] = @DraweeAddr1,[DraweeAddr2] = @DraweeAddr2,[DraweeAddr3] = @DraweeAddr3,
				[ReimbDraweeAcct] = @ReimbDraweeAcct,[DrawerCusNo] = @DrawerCusNo,[DrawerAddr] = @DrawerAddr,[Currency] = @Currency,[DocsReceivedDate] = @DocsReceivedDate,
				[MaturityDate] = @MaturityDate,[Tenor] = @Tenor,[Days] = @Days,[TracerDate] = @TracerDate,[ReminderDays] = @ReminderDays,[Commodity] = @Commodity,[DocsCode1] = @DocsCode1,
				[NoOfOriginals1] = @NoOfOriginals1,[NoOfCopies1] = @NoOfCopies1,[DocsCode2] = @DocsCode2,[NoOfOriginals2] = @NoOfOriginals2,[NoOfCopies2] = @NoOfCopies2,[OtherDocs] = @OtherDocs,
				[InstructionToCus] = @InstructionToCus, DrawerAddr1 = @DrawerAddr1, DrawerAddr2 = @DrawerAddr2, Remarks = @Remarks, [Amount] = @Amount,
				ContingentExpiryDate = @ContingentExpiryDate, DrawerCusName = @DrawerCusName, DraweeCusName = @DraweeCusName, DraweeType  = @DraweeType, DrawerType = @DrawerType, 
				AccountOfficer = @AccountOfficer, ExpressNo = @ExpressNo, InvoiceNo = @InvoiceNo, RemittingBankAddr2 = @RemittingBankAddr2, RemittingBankAddr3 = @RemittingBankAddr3, 
				DraftNo = @DraftNo, Amend_Status = 'UNA', ActiveRecordFlag = 'Yes', RemittingType = @RemittingType
			WHERE ISNULL(AmendNo,'') = @DocCollectCode			
		END
		ELSE
		BEGIN
			declare @DocsCode varchar(50)
			set @DocsCode = substring(@DocCollectCode,1,CHARINDEX('.', @DocCollectCode)-1)
			---
			INSERT INTO [dbo].[BDOCUMETARYCOLLECTION]([DocCollectCode],[CollectionType],[RemittingBankNo],[RemittingBankAddr],[RemittingBankAcct],[RemittingBankRef],[DraweeType],
				[DraweeCusNo],[DraweeAddr1],[DraweeAddr2],[DraweeAddr3],[ReimbDraweeAcct],[DrawerType],[DrawerCusNo],[DrawerAddr],[Currency],[Amount],[Amount_Old],[DocsReceivedDate],
				[MaturityDate],[Tenor],[Tenor_New],[Days],[TracerDate],[TracerDate_New],[ReminderDays],[Commodity],[DocsCode1],[NoOfOriginals1],[NoOfCopies1],[DocsCode2],[NoOfOriginals2],
				[NoOfCopies2],[OtherDocs],[InstructionToCus],[Status],[CreateDate],[CreateBy],[UpdatedDate],[UpdatedBy],[AuthorizedBy],[AuthorizedDate],[DrawerAddr1],[DrawerAddr2],[Remarks],
				[CancelDate],[ContingentExpiryDate],[Amend_Status],[DrawerCusName],[DraweeCusName],[AccountOfficer],[ExpressNo],[InvoiceNo],[CancelRemark],[RemittingBankAddr2],[RemittingBankAddr3],
				[Cancel_Status],[CancelBy],[AcceptedDate],[AcceptRemarks],[Accept_Status],[AcceptBy],[AcceptByDate],[PaymentFullFlag],[B4_AUT_Amount],[PaymentNo],[PaymentId],[DraftNo],[AmendNo],
				[ActiveRecordFlag],[OldAmount],[RefAmendNo], [RemittingType])
			SELECT DocCollectCode,@CollectionType,@RemittingBankNo,@RemittingBankAddr,@RemittingBankAcct,@RemittingBankRef,@DraweeType,@DraweeCusNo,@DraweeAddr1,@DraweeAddr2,@DraweeAddr3,
				@ReimbDraweeAcct,@DrawerType,@DrawerCusNo,@DrawerAddr,@Currency,@Amount, Amount_Old,@DocsReceivedDate,@MaturityDate,@Tenor, Tenor_New,@Days,@TracerDate, TracerDate_New,@ReminderDays,
				@Commodity,@DocsCode1,@NoOfOriginals1,@NoOfCopies1,@DocsCode2,@NoOfOriginals2,@NoOfCopies2,@OtherDocs,@InstructionToCus, [Status], CreateDate, CreateBy, UpdatedDate, UpdatedBy, AuthorizedBy,
				AuthorizedDate,@DrawerAddr1,@DrawerAddr2,@Remarks, CancelDate,@ContingentExpiryDate, 'UNA',@DrawerCusName,@DraweeCusName,@AccountOfficer,@ExpressNo,@InvoiceNo, CancelRemark,@RemittingBankAddr2,
				@RemittingBankAddr3, Cancel_Status, CancelBy, AcceptedDate, AcceptRemarks, Accept_Status, AcceptBy, AcceptByDate, PaymentFullFlag, B4_AUT_Amount, PaymentNo, PaymentId,@DraftNo,@DocCollectCode,'No',
				Amount, AmendNo, @RemittingType
			FROM [BDOCUMETARYCOLLECTION] WHERE DocCollectCode = @DocsCode and ISNULL(ActiveRecordFlag,'Yes') = 'Yes'
			
			update [BDOCUMETARYCOLLECTION] set ActiveRecordFlag = 'No' WHERE DocCollectCode = @DocsCode and ISNULL(ActiveRecordFlag,'Yes') = 'Yes'
			
			update [BDOCUMETARYCOLLECTION] set ActiveRecordFlag = 'Yes' where AmendNo = @DocCollectCode
		END
		---Must return here !!!
		RETURN
	END
	---
	IF NOT EXISTS(SELECT DocCollectCode FROM BDOCUMETARYCOLLECTION WHERE DocCollectCode = @DocCollectCode)
	begin
		INSERT INTO [dbo].[BDOCUMETARYCOLLECTION]([DocCollectCode],[CollectionType],[RemittingBankNo],[RemittingBankAddr],[RemittingBankAcct],
			[RemittingBankRef],[DraweeCusNo],[DraweeAddr1],[DraweeAddr2],[DraweeAddr3],[ReimbDraweeAcct],[DrawerCusNo],[DrawerAddr],[Currency],
			[Amount],[DocsReceivedDate],[MaturityDate],[Tenor],[Days],[TracerDate],[ReminderDays],[Commodity],[DocsCode1],[NoOfOriginals1],
			[NoOfCopies1],[DocsCode2],[NoOfOriginals2],[NoOfCopies2],[OtherDocs],[InstructionToCus], CreateDate, CreateBy, DrawerAddr1, DrawerAddr2, 
			Remarks, [Status], CancelDate, ContingentExpiryDate, DrawerCusName, DraweeCusName, DraweeType, DrawerType, AccountOfficer, ExpressNo, InvoiceNo, 
			CancelRemark, RemittingBankAddr2, RemittingBankAddr3, AcceptedDate, AcceptRemarks, B4_AUT_Amount, DraftNo, RemittingType)
		VALUES(@DocCollectCode ,@CollectionType ,@RemittingBankNo ,@RemittingBankAddr ,@RemittingBankAcct,@RemittingBankRef ,@DraweeCusNo ,@DraweeAddr1 ,
			@DraweeAddr2 ,@DraweeAddr3 ,@ReimbDraweeAcct ,@DrawerCusNo ,@DrawerAddr ,@Currency ,@Amount ,@DocsReceivedDate,@MaturityDate ,@Tenor ,
			@Days ,@TracerDate ,@ReminderDays ,@Commodity ,@DocsCode1 ,@NoOfOriginals1 ,@NoOfCopies1 ,@DocsCode2 ,@NoOfOriginals2 ,@NoOfCopies2 ,
			@OtherDocs ,@InstructionToCus,getdate(),@CurrentUserId,@DrawerAddr1,@DrawerAddr2,@Remarks,'UNA',@CancelDate,@ContingentExpiryDate,@DrawerCusName,
			@DraweeCusName,@DraweeType,@DrawerType,@AccountOfficer,@ExpressNo,@InvoiceNo,@CancelRemark,@RemittingBankAddr2,@RemittingBankAddr3,@AcceptedDate,
			@AcceptRemarks,@Amount,@DraftNo, @RemittingType)
		
		RETURN
	end
	---Update
	-- get old values
	declare @Amount_old float;
	declare @Tenor_old nvarchar(250);
	declare @TracerDate_old date;
	
	select 
		@Amount_old = isnull(Amount, 0),
		@Tenor_old = Tenor,
		@TracerDate_old = TracerDate
	from BDOCUMETARYCOLLECTION
	where DocCollectCode = @DocCollectCode
	-- get old values
	
	--- update new Values
	--if @Amount_old <> @Amount
	--begin
	--	UPDATE [dbo].[BDOCUMETARYCOLLECTION]
	--		set Amount_Old = @Amount_old
	--	where DocCollectCode = @DocCollectCode
	--end
	
	if @Tenor_old <> @Tenor
	begin
		UPDATE [dbo].[BDOCUMETARYCOLLECTION]
			set Tenor_New = @Tenor_old
		where DocCollectCode = @DocCollectCode
	end
	
	if CAST(@TracerDate_old AS DATE) <> CAST(@TracerDate AS DATE)
	begin
		UPDATE [dbo].[BDOCUMETARYCOLLECTION]
			set TracerDate_New = @TracerDate_old
		where DocCollectCode = @DocCollectCode
	end		
	--- update new Values
	
	UPDATE [dbo].[BDOCUMETARYCOLLECTION]
	SET [CollectionType] = @CollectionType,[RemittingBankNo] = @RemittingBankNo,[RemittingBankAddr] = @RemittingBankAddr,[RemittingBankAcct] = @RemittingBankAcct,
		[RemittingBankRef] = @RemittingBankRef,[DraweeCusNo] = @DraweeCusNo,[DraweeAddr1] = @DraweeAddr1,[DraweeAddr2] = @DraweeAddr2,[DraweeAddr3] = @DraweeAddr3,
		[ReimbDraweeAcct] = @ReimbDraweeAcct,[DrawerCusNo] = @DrawerCusNo,[DrawerAddr] = @DrawerAddr,[Currency] = @Currency,[DocsReceivedDate] = @DocsReceivedDate,
		[MaturityDate] = @MaturityDate,[Tenor] = @Tenor,[Days] = @Days,[TracerDate] = @TracerDate,[ReminderDays] = @ReminderDays,[Commodity] = @Commodity,[DocsCode1] = @DocsCode1,
		[NoOfOriginals1] = @NoOfOriginals1,[NoOfCopies1] = @NoOfCopies1,[DocsCode2] = @DocsCode2,[NoOfOriginals2] = @NoOfOriginals2,[NoOfCopies2] = @NoOfCopies2,[OtherDocs] = @OtherDocs,
		[InstructionToCus] = @InstructionToCus, UpdatedBy = @CurrentUserId, UpdatedDate = getdate(), DrawerAddr1 = @DrawerAddr1, DrawerAddr2 = @DrawerAddr2, Remarks = @Remarks, 
		CancelDate = @CancelDate, ContingentExpiryDate = @ContingentExpiryDate, DrawerCusName = @DrawerCusName, DraweeCusName = @DraweeCusName, DraweeType  = @DraweeType, DrawerType = @DrawerType, 
		AccountOfficer = @AccountOfficer, ExpressNo = @ExpressNo, InvoiceNo = @InvoiceNo, CancelRemark = @CancelRemark, RemittingBankAddr2 = @RemittingBankAddr2, RemittingBankAddr3 = @RemittingBankAddr3, 
		AcceptedDate = @AcceptedDate, AcceptRemarks = @AcceptRemarks, DraftNo = @DraftNo, RemittingType = @RemittingType
	WHERE [DocCollectCode] = @DocCollectCode	
	
	if @comeFromUrl = 'incollamendment' -- Incoming Collection Amendments
	begin
		UPDATE [dbo].[BDOCUMETARYCOLLECTION]
		SET 
		  Amend_Status = 'UNA',
		  B4_AUT_Amount = @Amount
		  --Amount = 0
		WHERE DocCollectCode = @DocCollectCode
	end
	else if @comeFromUrl = 'doccolcancel'
	begin
		UPDATE [dbo].[BDOCUMETARYCOLLECTION]
		SET 
		  Cancel_Status = 'UNA'
		  --[Amount] = @Amount
		WHERE DocCollectCode = @DocCollectCode
	end
	else if @comeFromUrl = 'incollaccepted'
	begin
		UPDATE [dbo].[BDOCUMETARYCOLLECTION]
		SET 
		  Accept_Status = 'UNA'
		  --[Amount] = @Amount
		WHERE DocCollectCode = @DocCollectCode
	end
	else -- Register Documetary Collection
	begin
		UPDATE [dbo].[BDOCUMETARYCOLLECTION]
		SET 
		  [Status] = 'UNA',
		  [Amount] = @Amount
		WHERE DocCollectCode = @DocCollectCode
	end
END

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
		select t.currency + '-' + t.Account as ID, cus.CustomerName as Name, t.currency, 0 as [amount], cus.CustomerID
		from BCUSTOMERS cus 
			inner join BINTERNALBANKACCOUNT t on 1 = 1
		where cus.CustomerID = '1100006' and t.Account = @Code and  t.Currency = @Currency
	end
	else if @CallFrom = 'bullcurrency_change'
	begin
		select * from (
		select id, name, currency, amount, CustomerID from dbo.BDRFROMACCOUNT
		where Name = @CustomerName and Currency = @Currency
		union all
		select t.currency + '-' + t.Account as ID, cus.CustomerName as Name, t.currency, 0 as [amount], cus.CustomerID
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