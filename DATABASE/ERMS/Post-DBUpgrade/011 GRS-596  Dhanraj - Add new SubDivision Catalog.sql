

BEGIN TRANSACTION

IF NOT EXISTS (SELECT 1 FROM dbo.tb_catalogdef tc WHERE tc.catname = 'SubDivision')
BEGIN
	DECLARE @MaxCatID int;
	SELECT @MaxCatID = Max(catid) + 1 FROM dbo.tb_catalogdef WHERE dbo.tb_catalogdef.catid < 500;

	INSERT INTO dbo.tb_catalogdef
	(catid,catname,usecharcode)
	VALUES
	(@MaxCatID,'SubDivision',0);

	INSERT INTO dbo.tb_catalogitems
	( catid, catorder, code,name,active)
	VALUES
	(@MaxCatID,1,1,'Casualty',1);

	INSERT INTO dbo.tb_catalogitems
	( catid, catorder, code,name,active)
	VALUES
	(@MaxCatID,2,2,'Property',1);

	INSERT INTO dbo.tb_catalogitems
	( catid, catorder, code,name,active)
	VALUES
	(@MaxCatID,3,3,'Specialty',1);
	
END
--SELECT * FROM dbo.tb_catalogitems tc WHERE tc.catid =@MaxCatID
--SELECT * FROM dbo.tb_catalogdef tc WHERE tc.catid = @MaxCatID

COMMIT;