BEGIN TRANSACTION;

DECLARE @NoteType TABLE (Code int null,SortOrder int,NewLabel varchar(100),QS bit null,newrecord bit);

INSERT INTO @NoteType (Code,SortOrder,NewLabel,newrecord) VALUES (6,1,'Peer Review',0);
INSERT INTO @NoteType (Code,SortOrder,NewLabel,newrecord) VALUES (NULL,2,'General/Misc.',1);
INSERT INTO @NoteType (Code,SortOrder,NewLabel,newrecord) VALUES (46,3,'Underwriting Info',0);
INSERT INTO @NoteType (Code,SortOrder,NewLabel,newrecord) VALUES (NULL,4,'Negotiation',1);
INSERT INTO @NoteType (Code,SortOrder,NewLabel,newrecord) VALUES (NULL,5,'Internal Communications',1);
INSERT INTO @NoteType (Code,SortOrder,NewLabel,newrecord) VALUES (NULL,6,'Modeling/Pricing',1);
INSERT INTO @NoteType (Code,SortOrder,NewLabel,newrecord) VALUES (NULL,7,'Accommodations',1);

UPDATE nt SET nt.Code = tc.code FROM  @NoteType nt
JOIN dbo.tb_catalogitems tc ON tc.catid = 100 AND nt.NewLabel = tc.name AND tc.charcode = 'D' AND nt.Code IS NULL

IF NOT EXISTS ( SELECT 1 FROM  @NoteType nt
JOIN dbo.tb_catalogitems tc ON tc.catid = 100 AND nt.NewLabel = tc.name AND tc.charcode = 'D' AND nt.Code IS NULL)
BEGIN

DECLARE @MaxCode int;
SELECT @MaxCode = MAX(tc.code) FROM dbo.tb_catalogitems tc WHERE tc.catid = 100;

UPDATE @NoteType SET [@NoteType].newrecord = 0 WHERE [@NoteType].Code IS NOT NULL
UPDATE @NoteType SET @MaxCode = Code = @MaxCode +1 WHERE [@NoteType].Code IS NULL

--SELECT * FROM @NoteType nt

INSERT INTO dbo.tb_catalogitems ( catid, catorder,charcode,code, name, active, intdata1 )
SELECT 100, cb.SortOrder,'D', cb.Code,cb.NewLabel,1,cb.QS FROM @NoteType cb WHERE cb.newrecord = 1

END
--SELECT * FROM dbo.tb_catalogdef tc WHERE tc.catid = 100
--SELECT * FROM dbo.tb_catalogitems tc WHERE tc.catid = 100 AND charcode ='D'-- tc.AssumedFlag =1 ORDER BY tc.catorder

DELETE FROM dbo.cfgFact WHERE dbo.cfgFact.CatalogDefFK = 100;

DECLARE @AssumedVisibleID INT = (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsVisible')

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedVisibleID, '', 100, cb.Code FROM @NoteType cb 

DECLARE @AssumedSortID INT = (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsSortOrder')

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedSortID, cb.SortOrder , 100, cb.Code FROM @NoteType cb 

--SELECT * FROM v_CatalogItemsExt vcie WHERE vcie.catid = 100 AND vcie.AssumedFlag = 1

COMMIT;
--ROLLBACK;