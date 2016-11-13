IF COL_LENGTH('BDOCUMETARYCOLLECTION', 'RemittingType') IS NULL
BEGIN
    ALTER TABLE BDOCUMETARYCOLLECTION
    ADD RemittingType varchar(50) null Default 'A';
	
	Update BDOCUMETARYCOLLECTION set RemittingType = 'A';;		
END

GO

IF((Select count(1) From BDOCUMETARYCOLLECTION where RemittingType is null)>0)
Begin
Update BDOCUMETARYCOLLECTION set RemittingType = 'A';
End
