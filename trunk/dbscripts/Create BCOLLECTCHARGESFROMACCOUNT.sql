USE [bisolutions_vvcb]
GO

/****** Object:  Table [dbo].[BCOLLECTCHARGESFROMACCOUNT]    Script Date: 8/22/2014 1:07:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BCOLLECTCHARGESFROMACCOUNT](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](20) NULL,
	[AccountType] [nvarchar](5) NULL,
	[CustomerAccount] [nvarchar](20) NULL,
	[AccountCode] [nvarchar](20) NULL,
	[ChargAmountLCY] [numeric](18, 0) NULL,
	[ChargAmountFCY] [numeric](18, 4) NULL,
	[ValueDate] [datetime] NULL,
	[CategoryPLCode] [varchar](50) NULL,
	[CategoryPLName] [nvarchar](500) NULL,
	[DealRate] [numeric](18, 5) NULL,
	[VatAmountLCY] [numeric](18, 0) NULL,
	[VatAmountFCY] [numeric](18, 4) NULL,
	[TotalAmountLCY] [numeric](18, 0) NULL,
	[TotalAmountFCY] [numeric](18, 4) NULL,
	[VatSerialNo] [nvarchar](250) NULL,
	[Narrative] [nvarchar](250) NULL,
	[CreateBy] [bigint] NULL,
	[CreateDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[AuthorizedBy] [bigint] NULL,
	[AuthorizedDate] [datetime] NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_BCOLLECTCHARGESFROMACCOUNT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
GO

/****** Object:  StoredProcedure [dbo].[BCOLLECTCHARGESFROMACCOUNT_GetByCode]    Script Date: 8/22/2014 12:28:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[BCOLLECTCHARGESFROMACCOUNT_GetByCode]
	@Code nvarchar(20)
AS
BEGIN
	select un.*, at.AccountTypeName
	from
	(
		select a.ID, Code, AccountType,CustomerAccount, b.AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.WorkingAmount as CustBallance, ISNULL(b.WorkingAmount, 0) - ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join bopenaccount b on a.CustomerAccount = convert(nvarchar(20), b.id)
		where a.Code =@Code
		union all
		select a.ID, Code, AccountType,CustomerAccount, b.Refid as AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.AZPrincipal as CustBallance, ISNULL(b.AZPrincipal, 0) - ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join BSAVING_ACC_ARREAR b on a.CustomerAccount =  b.Refid
		where a.Code =@Code
		union all
		select a.ID, Code, AccountType,CustomerAccount, b.Refid as AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.AZPrincipal as CustBallance, ISNULL(b.AZPrincipal, 0) - ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join BSAVING_ACC_PERIODIC b on a.CustomerAccount =  b.Refid
		where a.Code =@Code
		union all
		select a.ID, Code, AccountType,CustomerAccount, b.Refid as AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.TDAmmount as CustBallance, ISNULL(b.TDAmmount, 0) - ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join BSAVING_ACC_DISCOUNTED b on a.CustomerAccount =  b.Refid
		where a.Code =@Code
		
	) un
	join 
	(
		select '1' id, 'Non Term Saving Account' as AccountTypeName
		union all
		select '2', 'Saving Account - Arrear' 
		union all
		select '3', 'Saving Account - Periodic'
		union all
		select '4', 'Saving Account - Discounted'
	) at on un.AccountType = at.id
END





GO

/****** Object:  StoredProcedure [dbo].[BCOLLECTCHARGESFROMACCOUNT_GetByID]    Script Date: 8/22/2014 12:28:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[BCOLLECTCHARGESFROMACCOUNT_GetByID]
	@ID int
AS
BEGIN
	select un.*, at.AccountTypeName
	from
	(
		select a.ID, Code, AccountType,CustomerAccount, b.AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.WorkingAmount as CustBallance, ISNULL(b.WorkingAmount, 0) - ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join bopenaccount b on a.CustomerAccount = convert(nvarchar(20), b.id)
		where a.ID =@ID
		union all
		select a.ID, Code, AccountType,CustomerAccount, b.Refid as AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.AZPrincipal as CustBallance, ISNULL(b.AZPrincipal, 0) - ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join BSAVING_ACC_ARREAR b on a.CustomerAccount =  b.Refid
		where a.ID =@ID
		union all
		select a.ID, Code, AccountType,CustomerAccount, b.Refid as AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.AZPrincipal as CustBallance, ISNULL(b.AZPrincipal, 0) - ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join BSAVING_ACC_PERIODIC b on a.CustomerAccount =  b.Refid
		where a.ID =@ID
		union all
		select a.ID, Code, AccountType,CustomerAccount, b.Refid as AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.TDAmmount as CustBallance, ISNULL(b.TDAmmount, 0) - ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join BSAVING_ACC_DISCOUNTED b on a.CustomerAccount =  b.Refid
		where a.ID =@ID
		
	) un
	join 
	(
		select '1' id, 'Non Term Saving Account' as AccountTypeName
		union all
		select '2', 'Saving Account - Arrear' 
		union all
		select '3', 'Saving Account - Periodic'
		union all
		select '4', 'Saving Account - Discounted'
	) at on un.AccountType = at.id
END




GO

/****** Object:  StoredProcedure [dbo].[BCOLLECTCHARGESFROMACCOUNT_GetbyStatus]    Script Date: 8/22/2014 12:28:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[BCOLLECTCHARGESFROMACCOUNT_GetbyStatus]
	@Status varchar(50),
	@UserId varchar(5)
AS
BEGIN
	select un.*, at.AccountTypeName
	from
	(
		select a.ID, Code, AccountType,CustomerAccount, b.AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.WorkingAmount as CustBallance, ISNULL(b.WorkingAmount, 0) - ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join bopenaccount b on a.CustomerAccount = convert(nvarchar(20), b.id)
		where a.Status = @Status
			and a.CreateBy = @UserId
		union all
		select a.ID, Code, AccountType,CustomerAccount, b.Refid as AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.AZPrincipal as CustBallance, ISNULL(b.AZPrincipal, 0) - ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join BSAVING_ACC_ARREAR b on a.CustomerAccount =  b.Refid
		where a.Status = @Status
			and a.CreateBy = @UserId
		union all
		select a.ID, Code, AccountType,CustomerAccount, b.Refid as AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.AZPrincipal as CustBallance, ISNULL(b.AZPrincipal, 0) - ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join BSAVING_ACC_PERIODIC b on a.CustomerAccount =  b.Refid
		where a.Status = @Status
			and a.CreateBy = @UserId
		union all
		select a.ID, Code, AccountType,CustomerAccount, b.Refid as AccountCode, ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) ChargAmount, b.TDAmmount as CustBallance, ISNULL(b.TDAmmount, 0) - ISNULL(NULLIF(ChargAmountFCY, 0), ChargAmountFCY) as NewCustBallance
		, ValueDate, CategoryPLCode, CategoryPLName, a.DealRate, ISNULL(NULLIF(VatAmountFCY, 0), VatAmountFCY) VatAmount, ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) TotalAmount, a.Narrative, 
							  a.CreateBy, a.CreateDate, a.UpdatedBy, a.UpdatedDate, a.AuthorizedBy, a.AuthorizedDate, a.Status
		 from dbo.BCOLLECTCHARGESFROMACCOUNT a
			join BSAVING_ACC_DISCOUNTED b on a.CustomerAccount =  b.Refid
		where a.Status = @Status
			and a.CreateBy = @UserId
	) un
	join 
	(
		select '1' id, 'Non Term Saving Account' as AccountTypeName
		union all
		select '2', 'Saving Account - Arrear' 
		union all
		select '3', 'Saving Account - Periodic'
		union all
		select '4', 'Saving Account - Discounted'
	) at on un.AccountType = at.id
END




GO

/****** Object:  StoredProcedure [dbo].[BCOLLECTCHARGESFROMACCOUNT_Insert]    Script Date: 8/22/2014 12:28:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[BCOLLECTCHARGESFROMACCOUNT_Insert]
	@AccountType nvarchar(5),
	@Code nvarchar(20),
	@CustomerAccount nvarchar(20),
	@ChargAmountLCY numeric(18, 0),
	@ChargAmountFCY numeric(18, 4),
	@ValueDate date = null,
	@CategoryPLCode varchar(50),
	@CategoryPLName nvarchar(500),
	@DealRate numeric(18, 5),
	@VatAmountLCY numeric(18, 0),
	@VatAmountFCY numeric(18, 4),
	@TotalAmountLCY numeric(18, 0),
	@TotalAmountFCY numeric(18, 4),
	@VatSerialNo nvarchar(250),
	@Narrative nvarchar(250),
	@UserId int
AS
BEGIN
	--update binh thuong hoac neu la loai 2 thi ko cho insert chi update tk cu
	if not exists(select Code from dbo.BCOLLECTCHARGESFROMACCOUNT where Code = @Code)
	begin
		INSERT INTO dbo.BCOLLECTCHARGESFROMACCOUNT
			   (Code, AccountType, CustomerAccount, ChargAmountLCY, ChargAmountFCY, ValueDate, CategoryPLCode, CategoryPLName, DealRate, 
                         VatAmountLCY, VatAmountFCY, TotalAmountLCY, TotalAmountFCY, VatSerialNo, Narrative, 
						 CreateBy, 
                         CreateDate, 
						 Status
			   )
		 VALUES
			   (@Code, @AccountType, @CustomerAccount, @ChargAmountLCY, @ChargAmountFCY, @ValueDate, @CategoryPLCode, @CategoryPLName, @DealRate, 
                         @VatAmountLCY, @VatAmountFCY, @TotalAmountLCY, @TotalAmountFCY, @VatSerialNo, @Narrative 
				, @UserId   
				, getdate()
				, 'UNA'
			)
	end
	else 
	begin
		UPDATE dbo.BCOLLECTCHARGESFROMACCOUNT
		   SET 
			 AccountType=@AccountType
			 , CustomerAccount=@CustomerAccount
			 , ChargAmountLCY=@ChargAmountLCY
			 , ChargAmountFCY=@ChargAmountFCY
			 , ValueDate=@ValueDate
			 , CategoryPLCode=@CategoryPLCode
			 , CategoryPLName=@CategoryPLName
			 , DealRate=@DealRate
			 , VatAmountLCY=@VatAmountLCY
			 , VatAmountFCY=@VatAmountFCY
			 , TotalAmountLCY=@TotalAmountLCY
			 , TotalAmountFCY=@TotalAmountFCY
			 , VatSerialNo=@VatSerialNo
			 , Narrative=@Narrative
			,UpdatedDate = getdate()  
			,UpdatedBy = @UserId 
			,status='UNA'
		 WHERE Code = @Code
	end
END




GO

/****** Object:  StoredProcedure [dbo].[BCOLLECTCHARGESFROMACCOUNT_UpdateStatus]    Script Date: 8/22/2014 12:28:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[BCOLLECTCHARGESFROMACCOUNT_UpdateStatus]
	@AccountType nvarchar(5),
	@Status varchar(50),
	@Code varchar(50),
	@UserId varchar(10)
AS
BEGIN
if @Status = 'AUT'
	begin
		BEGIN TRANSACTION
		BEGIN TRY
			update BCOLLECTCHARGESFROMACCOUNT set Status = @Status, AuthorizedBy=@UserId,AuthorizedDate=GETDATE()
			where Code = @Code

			declare @customerAccountId nvarchar(20) = (select CustomerAccount from BCOLLECTCHARGESFROMACCOUNT where Code = @Code)

			declare @AmtPaidToCust numeric(18, 4) = (select ISNULL(NULLIF(TotalAmountFCY, 0), TotalAmountFCY) from BCOLLECTCHARGESFROMACCOUNT where Code = @Code)

			if @AccountType = '1' --non term
			begin
				--Khi AUT thi update vao openAcc
				update BOPENACCOUNT set ActualBallance= ISNULL(ActualBallance, 0)-@AmtPaidToCust,
										ClearedBallance=ISNULL(ClearedBallance,0)-@AmtPaidToCust,
										WorkingAmount=ISNULL(WorkingAmount,0)-@AmtPaidToCust,
										UpdatedBy=@UserId,
										UpdatedDate=GETDATE()
				where ID = @customerAccountId
			end

			if @AccountType = '2' --arrear
			begin
				--Khi AUT thi update vao openAcc
				update BSAVING_ACC_ARREAR set AZPrincipal= ISNULL(AZPrincipal, 0)-@AmtPaidToCust,
												UpdatedBy=@UserId,
												UpdatedDate=GETDATE()
				where RefID = @customerAccountId
			end

			if @AccountType = '3' --periolic
			begin
				--Khi AUT thi update vao openAcc
				update BSAVING_ACC_PERIODIC set AZPrincipal= ISNULL(AZPrincipal, 0)-@AmtPaidToCust,
												UpdatedBy=@UserId,
												UpdatedDate=GETDATE()
				where RefID = @customerAccountId
			end

			if @AccountType = '4' --discounted
			begin
				--Khi AUT thi update vao openAcc
				update BSAVING_ACC_DISCOUNTED set TDAmmount= ISNULL(TDAmmount, 0)-@AmtPaidToCust,
												UpdatedBy=@UserId,
												UpdatedDate=GETDATE()
				where RefID = @customerAccountId
			end
		COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			ROLLBACK
			DECLARE @ErrorMessage VARCHAR(2000)
		   SELECT @ErrorMessage = 'Lỗi: ' + ERROR_MESSAGE()
		   RAISERROR(@ErrorMessage, 16, 1)
		END CATCH
	end
else
	begin
		update BCOLLECTCHARGESFROMACCOUNT set Status = @Status
		where Code = @Code
	end
END




GO


