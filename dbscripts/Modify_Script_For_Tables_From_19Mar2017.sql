IF COL_LENGTH('BEXPORT_LC_DOCS_PROCESSING', 'AcceptRemarks') IS NULL
BEGIN
    ALTER TABLE BEXPORT_LC_DOCS_PROCESSING
    ADD AcceptRemarks nvarchar(100) null;
		
END
ELSE
BEGIN
	ALTER TABLE BEXPORT_LC_DOCS_PROCESSING
    ALTER COLUMN AcceptRemarks nvarchar(100) null;
END

GO


IF COL_LENGTH('BEXPORT_DOCS_PROCESSING_SETTLEMENT', 'NostroAccount') IS NULL
BEGIN
    ALTER TABLE BEXPORT_DOCS_PROCESSING_SETTLEMENT
    ADD NostroAccount varchar(250) null;
		
END

GO

