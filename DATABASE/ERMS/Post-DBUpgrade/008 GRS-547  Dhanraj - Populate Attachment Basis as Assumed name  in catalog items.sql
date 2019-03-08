BEGIN TRANSACTION;

DECLARE @AssumedVisibleID INT = (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsVisible')

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedVisibleID, '', 81, 1 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 81 AND CatalogItemsCodeFK = 1 AND SettingFK = @AssumedVisibleID)

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedVisibleID, '', 81, 2 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 81 AND CatalogItemsCodeFK = 2 AND SettingFK = @AssumedVisibleID)

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedVisibleID, '', 81, 3 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 81 AND CatalogItemsCodeFK = 3 AND SettingFK = @AssumedVisibleID)

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedVisibleID, '', 81, 7 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 81 AND CatalogItemsCodeFK = 7 AND SettingFK = @AssumedVisibleID)

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedVisibleID, '', 81, 27 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 81 AND CatalogItemsCodeFK = 27 AND SettingFK = @AssumedVisibleID)

DECLARE @AssumedValueID INT = (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsValue')
INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedValueID, 'Risks Attaching During', 81, 1 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 81 AND CatalogItemsCodeFK = 1 AND SettingFK = @AssumedValueID)

UPDATE dbo.tb_catalogitems SET catorder = 2 WHERE catid = 81 AND code = 1
UPDATE dbo.tb_catalogitems SET catorder = 1 WHERE catid = 81 AND code = 2
UPDATE dbo.tb_catalogitems SET catorder = 4 WHERE catid = 81 AND code = 3
UPDATE dbo.tb_catalogitems SET catorder = 5 WHERE catid = 81 AND code = 7
UPDATE dbo.tb_catalogitems SET catorder = 3 WHERE catid = 81 AND code = 27


--SELECT * FROM dbo.tb_catalogdef tc WHERE tc.catid = 81
--SELECT * FROM dbo.tb_catalogitems tc WHERE tc.catid = 81 

COMMIT;
--ROLLBACK;