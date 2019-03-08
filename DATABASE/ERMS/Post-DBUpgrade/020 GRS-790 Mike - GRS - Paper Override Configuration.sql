--BEGIN TRANSACTION
/* Setup up the new paper configuration settings */

DECLARE @GlobalReModuleExists BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT * FROM dbo.cfgModule m WHERE [m].[Name] = N'GlobalRe')),0)

IF @GlobalReModuleExists != 1
BEGIN
    INSERT INTO dbo.cfgModule (Name) VALUES (N'GlobalRe');
END;

DECLARE @globalReModuleId INT = (SELECT TOP 1 ModulePK FROM dbo.cfgModule WHERE [Name] = 'GlobalRe')

/* GlobalRe Assumed PaperVisible */

DECLARE @GlobalReAssumedPaperVisibleExists BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed PaperVisible')),0)

IF @GlobalReAssumedPaperVisibleExists != 1
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
        N'GlobalRe Assumed PaperVisible',          -- Name - nvarchar(255)
        N'Paper is visible for assumed GRS deals', -- Description - nvarchar(255)
        N'System.String',                                 -- DotNetTypeName - nvarchar(255)
        N'',                                              -- RegExValidator - nvarchar(255)
        N'',                                              -- LookupTypeViewName - nvarchar(255)
        N'DealExt'
        );
END;

/* GlobalRe Ceded PaperVisible */

DECLARE @GlobalReCededPaperVisibleExists BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Ceded PaperVisible')),0)

IF @GlobalReCededPaperVisibleExists != 1
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
        N'GlobalRe Ceded PaperVisible',          -- Name - nvarchar(255)
        N'Paper is visible for ceded GRS deals', -- Description - nvarchar(255)
        N'System.String',                                 -- DotNetTypeName - nvarchar(255)
        N'',                                              -- RegExValidator - nvarchar(255)
        N'',                                              -- LookupTypeViewName - nvarchar(255)
        N'DealExt'
        );
END;

/* GlobalRe Assumed PaperSortOrder */

DECLARE @GlobalReAssumedPaperSortOrderExists BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed PaperSortOrder')),0)

IF @GlobalReAssumedPaperSortOrderExists != 1
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
        N'GlobalRe Assumed PaperSortOrder',          -- Name - nvarchar(255)
        N'Paper override sort order for assumed GRS deals', -- Description - nvarchar(255)
        N'System.String',                                 -- DotNetTypeName - nvarchar(255)
        N'',                                              -- RegExValidator - nvarchar(255)
        N'',                                              -- LookupTypeViewName - nvarchar(255)
        N'DealExt'
        );
END;

/* GlobalRe Ceded PaperSortOrder */

DECLARE @GlobalReCededPaperSortOrderExists BIT = ISNULL((SELECT 1 WHERE EXISTS (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Ceded PaperSortOrder')),0) 

IF @GlobalReCededPaperSortOrderExists != 1
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
        N'GlobalRe Ceded PaperSortOrder',          -- Name - nvarchar(255)
        N'Paper override sort order for ceded GRS deals', -- Description - nvarchar(255)
        N'System.String',                                 -- DotNetTypeName - nvarchar(255)
        N'',                                              -- RegExValidator - nvarchar(255)
        N'',                                              -- LookupTypeViewName - nvarchar(255)
        N'DealExt'
        );
END;

/* FACT SETTINGS FOR:  GlobalRe Assumed PaperVisible */

DECLARE @AssumedPaperVisibleID INT = (SELECT s.SettingPK FROM dbo.cfgSetting s WHERE [s].[Name] = N'GlobalRe Assumed PaperVisible')

/*Markel Bermuda Limited*/
INSERT INTO dbo.cfgFact (SettingFK, FactValue, PaperFK )
SELECT @AssumedPaperVisibleID, '', 1 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE PaperFK = 1 AND SettingFK = @AssumedPaperVisibleID)

INSERT INTO dbo.cfgFact (SettingFK, FactValue, PaperFK )
SELECT @AssumedPaperVisibleID, '', 16 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE PaperFK = 16 AND SettingFK = @AssumedPaperVisibleID)

INSERT INTO dbo.cfgFact (SettingFK, FactValue, PaperFK )
SELECT @AssumedPaperVisibleID, '', 33 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE PaperFK = 33 AND SettingFK = @AssumedPaperVisibleID)

INSERT INTO dbo.cfgFact (SettingFK, FactValue, PaperFK )
SELECT @AssumedPaperVisibleID, '', 34 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE PaperFK = 34 AND SettingFK = @AssumedPaperVisibleID)

INSERT INTO dbo.cfgFact (SettingFK, FactValue, PaperFK )
SELECT @AssumedPaperVisibleID, '', 29 WHERE NOT EXISTS (SELECT * FROM dbo.cfgFact WHERE PaperFK = 29 AND SettingFK = @AssumedPaperVisibleID)

SELECT * FROM dbo.cfgFact WHERE SettingFK = @AssumedPaperVisibleID

--ROLLBACK


