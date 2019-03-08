BEGIN TRANSACTION;

DECLARE @CovBasis TABLE (Code int,SortOrder int,NewLabel varchar(100),QS bit null,newrecord bit null);

INSERT INTO @CovBasis (Code,SortOrder,NewLabel) VALUES (6,1,'Cat Excess Loss');
INSERT INTO @CovBasis (Code,SortOrder,NewLabel) VALUES (2,2,'Per Occurrence Excess Loss');
INSERT INTO @CovBasis (Code,SortOrder,NewLabel) VALUES (7,3,'Per Risk  Excess Loss');
INSERT INTO @CovBasis (Code,SortOrder,NewLabel) VALUES (16,5,'ILW');
INSERT INTO @CovBasis (Code,SortOrder,NewLabel) VALUES (4,6,'LPT');
INSERT INTO @CovBasis (Code,SortOrder,NewLabel) VALUES (1,7,'Primary / Quota Share (Per Occurrence)');
INSERT INTO @CovBasis (Code,SortOrder,NewLabel) VALUES (18,8,'Primary / Quota Share (Per Risk)');
INSERT INTO @CovBasis (Code,SortOrder,NewLabel) VALUES (25,10,'Surplus Treaty (Per Occ)');
INSERT INTO @CovBasis (Code,SortOrder,NewLabel) VALUES (24,11,'Surplus Treaty (Per Risk)');

if NOT EXISTS (SELECT 1 FROM dbo.tb_catalogitems tc WHERE tc.catid = 87 AND tc.name = 'Aggregate Excess Loss')
BEGIN
	DECLARE @MaxCode int;
	SELECT @MaxCode = MAX(tc.code) FROM dbo.tb_catalogitems tc WHERE tc.catid = 87;
 
	INSERT INTO @CovBasis (Code,SortOrder,NewLabel,QS,newrecord) VALUES (@MaxCode+1,4,'Aggregate Excess Loss',NULL,1);
	INSERT INTO @CovBasis (Code,SortOrder,NewLabel,QS,newrecord) VALUES (@MaxCode+2,9,'Excess / Quota Share (Per Occurrence)',1,1);
	INSERT INTO @CovBasis (Code,SortOrder,NewLabel,QS,newrecord) VALUES (@MaxCode+3,12,'Variable Quota Share (Per Occurrence)',1,1);
	INSERT INTO @CovBasis (Code,SortOrder,NewLabel,QS,newrecord) VALUES (@MaxCode+4,13,'Variable Quota Share (Per Risk)',1,1);

	INSERT INTO dbo.tb_catalogitems ( catid, catorder,charcode,code, name, active, intdata1 )
	SELECT 87, cb.SortOrder,'', cb.Code,cb.NewLabel,1,cb.QS FROM @CovBasis cb WHERE cb.newrecord = 1
END
ELSE
BEGIN
	INSERT INTO @CovBasis (SortOrder,NewLabel,QS) VALUES (4,'Aggregate Excess Loss',NULL);
	INSERT INTO @CovBasis (SortOrder,NewLabel,QS) VALUES (9,'Excess / Quota Share (Per Occurrence)',1);
	INSERT INTO @CovBasis (SortOrder,NewLabel,QS) VALUES (12,'Variable Quota Share (Per Occurrence)',1);
	INSERT INTO @CovBasis (SortOrder,NewLabel,QS) VALUES (13,'Variable Quota Share (Per Risk)',1);	

	UPDATE cb SET cb.Code = tc.code FROM @CovBasis cb
	JOIN dbo.tb_catalogitems tc ON tc.catid = 87 AND tc.name = cb.NewLabel
END

UPDATE dbo.tb_catalogitems SET catorder = 7  WHERE catid = 87 AND code = 1
UPDATE dbo.tb_catalogitems SET catorder = 2  WHERE catid = 87 AND code = 2
UPDATE dbo.tb_catalogitems SET catorder = 6  WHERE catid = 87 AND code = 4
UPDATE dbo.tb_catalogitems SET catorder = 1  WHERE catid = 87 AND code = 6
UPDATE dbo.tb_catalogitems SET catorder = 3  WHERE catid = 87 AND code = 7
UPDATE dbo.tb_catalogitems SET catorder = 5  WHERE catid = 87 AND code = 16
UPDATE dbo.tb_catalogitems SET catorder = 8  WHERE catid = 87 AND code = 18
UPDATE dbo.tb_catalogitems SET catorder = 11 WHERE catid = 87 AND code = 24
UPDATE dbo.tb_catalogitems SET catorder = 10 WHERE catid = 87 AND code = 25

--SELECT * FROM dbo.tb_catalogdef tc WHERE tc.catid = 87
--SELECT * FROM dbo.tb_catalogitems tc WHERE tc.catid = 87 AND tc.AssumedFlag =1 ORDER BY tc.catorder
DECLARE @AssumedVisibleID INT = (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsVisible')

DELETE FROM dbo.cfgFact WHERE CatalogDefFK = 87; 

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedVisibleID, '', 87, cb.Code FROM @CovBasis cb 

DECLARE @AssumedValueID INT = (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsValue')

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedValueID, 'Per Occurrence Excess Loss', 87, 2 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 87 AND CatalogItemsCodeFK = 2 AND SettingFK = @AssumedValueID)
--SELECT * FROM @CovBasis cb
COMMIT;
--ROLLBACK;