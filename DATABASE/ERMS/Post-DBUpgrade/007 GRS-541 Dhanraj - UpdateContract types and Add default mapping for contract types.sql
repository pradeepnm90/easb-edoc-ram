BEGIN TRANSACTION;

DECLARE @GlobalReModuleExists BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT * FROM dbo.cfgModule m WHERE [m].[Name] = N'GlobalRe')),0)

IF @GlobalReModuleExists != 1
BEGIN
    INSERT INTO dbo.cfgModule (Name) VALUES (N'GlobalRe');
END;

DECLARE @globalReModuleId INT = (SELECT TOP 1 ModulePK FROM dbo.cfgModule WHERE [Name] = 'GlobalRe')

/* GlobalRe Assumed CatalogItemsVisible */

DECLARE @GlobalReAssumedCatalogItemsVisibleExists BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsVisible')),0)

IF @GlobalReAssumedCatalogItemsVisibleExists != 1
BEGIN
    INSERT INTO dbo.cfgSetting
    (
        ModuleFK,
        Name,
        Description,
        DotNetTypeName,
        RegExValidator,
        LookupTypeViewName,
        Kind
    )
    VALUES
    (   @globalReModuleId,                                -- ModuleFK - int
        N'GlobalRe Assumed CatalogItemsVisible',          -- Name - nvarchar(255)
        N'Catalog item is visible for assumed GRS deals', -- Description - nvarchar(255)
        N'System.String',                                 -- DotNetTypeName - nvarchar(255)
        N'',                                              -- RegExValidator - nvarchar(255)
        N'',                                              -- LookupTypeViewName - nvarchar(255)
        N'DealExt'                                        -- Kind - nvarchar(25)
        );
END;

/* GlobalRe Ceded CatalogItemsVisible */

DECLARE @GlobalReCededCatalogItemsVisibleExists BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Ceded CatalogItemsVisible')),0)

IF @GlobalReCededCatalogItemsVisibleExists != 1
BEGIN
    INSERT INTO dbo.cfgSetting
    (
        ModuleFK,
        Name,
        Description,
        DotNetTypeName,
        RegExValidator,
        LookupTypeViewName,
        Kind
    )
    VALUES
    (   @globalReModuleId,                                -- ModuleFK - int
        N'GlobalRe Ceded CatalogItemsVisible',          -- Name - nvarchar(255)
        N'Catalog item value override for outward GRS deals', -- Description - nvarchar(255)
        N'System.String',                                 -- DotNetTypeName - nvarchar(255)
        N'',                                              -- RegExValidator - nvarchar(255)
        N'',                                              -- LookupTypeViewName - nvarchar(255)
        N'DealExt'
        );
END;

/* GlobalRe Ceded CatalogItemsValue */

DECLARE @GlobalReCededCatalogItemsValueExists BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Ceded CatalogItemsValue')),0)

IF @GlobalReCededCatalogItemsValueExists != 1
BEGIN
    INSERT INTO dbo.cfgSetting
    (
        ModuleFK,
        Name,
        Description,
        DotNetTypeName,
        RegExValidator,
        LookupTypeViewName,
        Kind
    )
    VALUES
    (   @globalReModuleId,                                -- ModuleFK - int
        N'GlobalRe Ceded CatalogItemsValue',          -- Name - nvarchar(255)
        N'Catalog item value override for outward GRS deals', -- Description - nvarchar(255)
        N'System.String',                                 -- DotNetTypeName - nvarchar(255)
        N'',                                              -- RegExValidator - nvarchar(255)
        N'',                                              -- LookupTypeViewName - nvarchar(255)
        N'DealExt'
        );
END;

/* GlobalRe Assumed CatalogItemsValue */

DECLARE @GlobalReAssumedCatalogItemsValueExists BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsValue')),0)

IF @GlobalReAssumedCatalogItemsValueExists != 1
BEGIN
    INSERT INTO dbo.cfgSetting
    (
        ModuleFK,
        Name,
        Description,
        DotNetTypeName,
        RegExValidator,
        LookupTypeViewName,
        Kind
    )
    VALUES
    (   @globalReModuleId,                                -- ModuleFK - int
        N'GlobalRe Assumed CatalogItemsValue',          -- Name - nvarchar(255)
        N'Catalog item value override for assumed GRS deals', -- Description - nvarchar(255)
        N'System.String',                                 -- DotNetTypeName - nvarchar(255)
        N'',                                              -- RegExValidator - nvarchar(255)
        N'',                                              -- LookupTypeViewName - nvarchar(255)
        N'DealExt'
        );
END;

/* GlobalRe Assumed CatalogItemsSortOrder */

DECLARE @GlobalReAssumedCatalogItemsSortOrderExists BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsSortOrder')),0)

IF @GlobalReAssumedCatalogItemsValueExists != 1
BEGIN
    INSERT INTO dbo.cfgSetting
    (
        ModuleFK,
        Name,
        Description,
        DotNetTypeName,
        RegExValidator,
        LookupTypeViewName,
        Kind
    )
    VALUES
    (   @globalReModuleId,                                -- ModuleFK - int
        N'GlobalRe Assumed CatalogItemsSortOrder',          -- Name - nvarchar(255)
        N'Catalog item override sort order for assumed GRS deals', -- Description - nvarchar(255)
        N'System.String',                                 -- DotNetTypeName - nvarchar(255)
        N'',                                              -- RegExValidator - nvarchar(255)
        N'',                                              -- LookupTypeViewName - nvarchar(255)
        N'DealExt'
        );
END;

/* GlobalRe Ceded CatalogItemsSortOrder */

DECLARE @GlobalReCededCatalogItemsSortOrder BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Ceded CatalogItemsSortOrder')),0)

IF @GlobalReCededCatalogItemsSortOrder != 1
BEGIN
    INSERT INTO dbo.cfgSetting
    (
        ModuleFK,
        Name,
        Description,
        DotNetTypeName,
        RegExValidator,
        LookupTypeViewName,
        Kind
    )
    VALUES
    (   @globalReModuleId,                                -- ModuleFK - int
        N'GlobalRe Ceded CatalogItemsSortOrder',          -- Name - nvarchar(255)
        N'Catalog item override sort order for ceded GRS deals', -- Description - nvarchar(255)
        N'System.String',                                 -- DotNetTypeName - nvarchar(255)
        N'',                                              -- RegExValidator - nvarchar(255)
        N'',                                              -- LookupTypeViewName - nvarchar(255)
        N'DealExt'
        );
END;


/* FACT SETTINGS FOR:  GlobalRe Assumed CatalogItemsVisible */

DECLARE @AssumedVisibleID INT = (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsVisible')

/*(Re)Insurance*/
INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedVisibleID, '', 79, 4 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 79 AND CatalogItemsCodeFK = 4 AND SettingFK = @AssumedVisibleID)

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedVisibleID, '', 79, 6 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 79 AND CatalogItemsCodeFK = 6 AND SettingFK = @AssumedVisibleID)

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedVisibleID, '', 79, 18 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 79 AND CatalogItemsCodeFK = 18 AND SettingFK = @AssumedVisibleID)

UPDATE dbo.tb_catalogitems SET catorder = 1 WHERE catid = 79 AND code = 4
UPDATE dbo.tb_catalogitems SET catorder = 3 WHERE catid = 79 AND code = 6
UPDATE dbo.tb_catalogitems SET catorder = 2 WHERE catid = 79 AND code = 18

DECLARE @AssumedValueID INT = (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsValue')

/*(Re)Insurance*/
INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedValueID, 'Treaty', 79, 4 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 79 AND CatalogItemsCodeFK = 4 AND SettingFK = @AssumedValueID)

INSERT INTO dbo.cfgFact (SettingFK, FactValue, CatalogDefFK, CatalogItemsCodeFK )
SELECT @AssumedValueID, 'Facultative', 79, 18 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE CatalogDefFK = 79 AND CatalogItemsCodeFK = 18 AND SettingFK = @AssumedValueID)


--SELECT * FROM dbo.tb_catalogitems tc WHERE tc.catid = 79 AND code IN (4,6,18)
--ROLLBACK
DECLARE @ExposureMap TABLE (ExpType int NULL, ExpName varchar(100) NULL, ConType int);

INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - A&H',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - Agri',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - Agri - Arc',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - AV',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - LPT',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - M&E',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - Mortgage',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - Other',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - SC',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - Terrorism',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - Terrorism - Arc',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - WC',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'Whole Account',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'CA - Auto',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'CA - CA',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'CA - GL',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'CA - PL',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'Property International',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'Property North America',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'CA - Cas Fac',18);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - Public Entity',4);
INSERT INTO @ExposureMap (ExpType,ExpName,ConType) VALUES (0,'SP - Public Entity',6);

UPDATE em SET em.ExpType = te.exposuretype FROM dbo.tb_exposetype te
JOIN @ExposureMap em ON em.ExpName = te.exposurename
WHERE te.active = 1


-- Define Cfg setting for defaults in GRS defaults module
DECLARE @settinfk int;
DECLARE @settinName Varchar(200) =  'GRS UW Contract Type Defaults';
EXEC pr_cfgAddSetting 
    @ModuleName =         'Defaults',  
    @SettingName =       @settinName,  
    @Description =        'Default contract type based on exposure type',  
    @DotNetType =         'System.String',  
    @RegExValidator =     '',  
    @Kind =               'User';  

SELECT  @settinfk = cs.SettingPK FROM dbo.cfgSetting cs WHERE cs.Name = @settinName;
DELETE FROM dbo.cfgFact WHERE SettingFK = @settinfk;
INSERT INTO dbo.cfgFact (SettingFK,FactValue,ExposureTypeFK,[Sequence])
SELECT @settinfk,[@ExposureMap].ConType,[@ExposureMap].ExpType,10 FROM @ExposureMap

--SELECT * FROM dbo.cfgSetting cs WHERE cs.SettingPK = @settinfk
--SELECT * FROM dbo.cfgFact cf WHERE cf.SettingFK = @settinfk
COMMIT;
----ROLLBACK;