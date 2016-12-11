SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***
---------------------------------------------------------------------------------
-- 11 Dec 2016 : Nghia : viet nam currency
---------------------------------------------------------------------------------
***/
ALTER FUNCTION [dbo].[fuDocSoThanhChu](@SoCanDoc bigint)
RETURNS nvarchar(200)
AS
BEGIN
DECLARE @DocThanhChu nvarchar(200)
DECLARE @String nvarchar(50)
IF len(@SoCanDoc)>15
BEGIN
SET @DocThanhChu=N'So qua lon, khong doc duoc'
END
ELSE
SET @String =Replace(Convert(VARCHAR,CAST(@SoCanDoc AS MONEY),1 ),'.00','')
BEGIN
DECLARE @Count int
SELECT @Count = COUNT(*) FROM dbo.SplitString(@String,',')
DECLARE @tram nvarchar(10)
DECLARE @Nghin nvarchar(10)
DECLARE @Trieu nvarchar(10)
DECLARE @ty nvarchar(10)
DECLARE @nghinty nvarchar(10)
DECLARE @trieuty nvarchar(10)
IF @Count=1
BEGIN
SET @DocThanhChu=dbo.fuDocBaSo(@SoCanDoc)
END
IF @Count=2
BEGIN
SELECT @Nghin=part FROM dbo.SplitString(@String,',') WHERE id=1
SELECT @tram=part FROM dbo.SplitString(@String,',') WHERE id=2
SET @DocThanhChu=dbo.fuDocBaSo(@Nghin)+N' nghìn '+ dbo.fuDocBaSo_Ben(@tram)
END
IF @Count=3
BEGIN
SELECT @Trieu=part FROM dbo.SplitString(@String,',') WHERE id=1
SELECT @Nghin=part FROM dbo.SplitString(@String,',') WHERE id=2
SELECT @tram = part FROM dbo.SplitString(@String,',') WHERE id=3
IF Cast(@Nghin as int)>0
BEGIN
IF Cast(@tram as int)>0
BEGIN
SET @DocThanhChu=dbo.fuDocBaSo(@Trieu) +N' triệu' + 
dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn'+ dbo.fuDocBaSo_Ben(@tram)
END
ELSE
BEGIN
SET @DocThanhChu=dbo.fuDocBaSo(@Trieu) +N' triệu' + 
dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn'
END
End 
ELSE
BEGIN
if Cast(@tram as int) =0
SET @DocThanhChu=dbo.fuDocBaSo(@Trieu) +N' triệu'
else
SET @DocThanhChu=dbo.fuDocBaSo(@Trieu) +N' triệu' + 
dbo.fuDocBaSo_Ben(@tram)
END
END
IF @Count=4
BEGIN
SELECT @ty=part FROM dbo.SplitString(@String,',') WHERE id=1
SELECT @Trieu=part FROM dbo.SplitString(@String,',') WHERE id=2
SELECT @Nghin=part FROM dbo.SplitString(@String,',') WHERE id=3
SELECT @tram = part FROM dbo.SplitString(@String,',') WHERE id=4
if cast(@Trieu as int)>0
BEGIN
IF cast(@Nghin as int)>0
BEGIN
if cast(@tram as int)>0
BEGIN
SET @DocThanhChu=dbo.fuDocBaSo(@ty) +N' tỷ' 
+ dbo.fuDocBaSo_Ben(@Trieu) + N' triệu '
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn ' 
+ dbo.fuDocBaSo_Ben(@tram)
END
else
BEGIN
SET @DocThanhChu=dbo.fuDocBaSo(@ty) +N' tỷ' 
+ dbo.fuDocBaSo_Ben(@Trieu) + N' triệu'
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn' 
END
END
ELSE
BEGIN
IF cast(@tram as int)>0
BEGIN
SET @DocThanhChu=dbo.fuDocBaSo(@ty) +N' tỷ' 
+ dbo.fuDocBaSo_Ben(@Trieu) + N' triệu '
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn ' 
+ dbo.fuDocBaSo_Ben(@tram)
END
ELSE
BEGIN
SET @DocThanhChu=dbo.fuDocBaSo(@ty) +N' tỷ' 
+ dbo.fuDocBaSo_Ben(@Trieu) + N' triệu '
END
END
END 
ELSE
BEGIN
if cast(@Nghin as int)>0
BEGIN
if Cast(@tram as int)>0
BEGIN
SET @DocThanhChu=dbo.fuDocBaSo(@ty) +N' tỷ' 
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn ' 
+ dbo.fuDocBaSo_Ben(@tram)
END
else
BEGIN
SET @DocThanhChu=dbo.fuDocBaSo(@ty) +N' tỷ' 
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn ' 
END
END
else
if cast(@tram as int)>0
BEGIN
SET @DocThanhChu=dbo.fuDocBaSo(@ty) +N' tỷ' 
+ dbo.fuDocBaSo_Ben(@tram)
END
else
BEGIN
SET @DocThanhChu=dbo.fuDocBaSo(@ty) +N' tỷ' 
END
END
END
IF @Count=5
BEGIN
SELECT @nghinty =part FROM dbo.SplitString(@String,',') WHERE id=1
SELECT @ty=part FROM dbo.SplitString(@String,',') WHERE id=2
SELECT @Trieu=part FROM dbo.SplitString(@String,',') WHERE id=3
SELECT @Nghin=part FROM dbo.SplitString(@String,',') WHERE id=4
SELECT @tram = part FROM dbo.SplitString(@String,',') WHERE id=5
if cast(@ty as int)>0
BEGIN
if cast(@Trieu as int)>0
BEGIN
if cast(@Nghin as int)>0
BEGIN
if cast(@tram as int)>0
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn' 
+dbo.fuDocBaSo_Ben(@ty) +N' tỷ' 
+dbo.fuDocBaSo_Ben(@Trieu) + N' triệu'
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn' 
+ dbo.fuDocBaSo_Ben(@tram)
else
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn' 
+dbo.fuDocBaSo_Ben(@ty) +N' tỷ' 
+dbo.fuDocBaSo_Ben(@Trieu) + N' triệu'
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn' 
END
else
BEGIN
if cast(@tram as int)>0
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn' 
+dbo.fuDocBaSo_Ben(@ty) +N' tỷ' 
+dbo.fuDocBaSo_Ben(@Trieu) + N' triệu'
+ dbo.fuDocBaSo_Ben(@tram)
else
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn' 
+dbo.fuDocBaSo_Ben(@ty) +N' tỷ' 
+dbo.fuDocBaSo_Ben(@Trieu) + N' triệu'
END
END
else
BEGIN
if cast(@Nghin as int)>0
BEGIN
if cast(@tram as int)>0
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn' 
+dbo.fuDocBaSo_Ben(@ty) +N' tỷ' 
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn' 
+ dbo.fuDocBaSo_Ben(@tram)
else
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn' 
+dbo.fuDocBaSo_Ben(@ty) +N' tỷ' 
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn' 
END
else
BEGIN
if cast(@tram as int)>0
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn' 
+dbo.fuDocBaSo_Ben(@ty) +N' tỷ' 
+ dbo.fuDocBaSo_Ben(@tram)
else
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn' 
+dbo.fuDocBaSo_Ben(@ty) +N' tỷ' 
END
END
END
else
BEGIN


if cast(@Trieu as int)>0
BEGIN
if cast(@Nghin as int)>0
BEGIN
if cast(@tram as int)>0
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn tỷ' 
+dbo.fuDocBaSo_Ben(@Trieu) + N' triệu'
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn' 
+ dbo.fuDocBaSo_Ben(@tram)
else
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn tỷ' 
+dbo.fuDocBaSo_Ben(@Trieu) + N' triệu'
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn' 
END
else
BEGIN
if cast(@tram as int)>0
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn tỷ' 
+dbo.fuDocBaSo_Ben(@Trieu) + N' triệu'
+ dbo.fuDocBaSo_Ben(@tram)
else
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn tỷ' 
+dbo.fuDocBaSo_Ben(@Trieu) + N' triệu'
END
END
else
BEGIN
if cast(@Nghin as int)>0
BEGIN
if cast(@tram as int)>0
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn tỷ' 
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn' 
+ dbo.fuDocBaSo_Ben(@tram)
else
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn tỷ' 
+ dbo.fuDocBaSo_Ben(@Nghin) + N' nghìn' 
END
else
BEGIN
if cast(@tram as int)>0
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn tỷ' 
+ dbo.fuDocBaSo_Ben(@tram)
else
SET @DocThanhChu= dbo.fuDocBaSo(@nghinty) +N' nghìn tỷ' 
END
END
END
END
END 
RETURN @DocThanhChu
END

GO

/***
---------------------------------------------------------------------------------
-- 13 Nov 2016 : Nghia : viet nam currency
---------------------------------------------------------------------------------
***/
ALTER FUNCTION [dbo].[f_CurrencyToText]
(
	@sNumber nvarchar(4000),
	@ccyCode varchar(3)
)
RETURNS nvarchar(4000) AS

BEGIN
	DECLARE @result nvarchar(4000);
	select @result = dbo.f_CurrencyToTextVn(@sNumber, @ccyCode);
	--select @result = dbo.fRemoveUnicodeSign(@result);
	
	return @result 
END

GO
