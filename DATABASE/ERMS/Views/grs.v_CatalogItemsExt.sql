DROP VIEW [grs].[v_CatalogItemsExt];
GO
GO

/****** Object:  View [grs].[v_CatalogItemsExt]    Script Date: 12/13/2018 12:16:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [grs].[v_CatalogItemsExt]
AS
SELECT c.catid, 
	   c.catorder, 
	   c.code, 
	   c.charcode, 
	   c.[name],
	   c.active,
	   c.intdata1,
	   c.strdata1,
	   c.FloatData1,
       av.AssumedFlag,
       COALESCE(aord.AssumedSortOder, c.catorder) AS "AssumedSortOder",
       COALESCE(aval.AssumedName, c.[name]) AS "AssumedName",
       cv.CededFlag,
       COALESCE(cord.CededSortOrder, c.catorder) as "CededSortOrder",
       COALESCE(cval.CededName, c.[name]) AS "CededName",
	   c._sys_CreatedBy,
	   c._sys_CreatedDt,
	   c._sys_ModifiedBy,
	   c._sys_ModifiedDt
FROM dbo.tb_catalogitems c
    LEFT JOIN
    (
        SELECT 1 AS AssumedFlag,
               f1.CatalogDefFK,
               f1.CatalogItemsCodeFK
        FROM dbo.cfgFact f1
        WHERE SettingFK =
        (
            SELECT s.SettingPK
            FROM dbo.cfgSetting s
            WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsVisible'
        )
    ) av
        ON c.catid = av.CatalogDefFK
           AND c.code = av.CatalogItemsCodeFK
    LEFT JOIN
    (
        SELECT 1 AS CededFlag,
               f2.CatalogDefFK,
               f2.CatalogItemsCodeFK
        FROM dbo.cfgFact f2
        WHERE SettingFK =
        (
            SELECT s.SettingPK
            FROM dbo.cfgSetting s
            WHERE [s].[Name] = N'GlobalRe Ceded CatalogItemsVisible'
        )
    ) cv
        ON c.catid = cv.CatalogDefFK
           AND c.code = cv.CatalogItemsCodeFK
    LEFT JOIN
    (
        SELECT FactValue AS AssumedName,
               f3.CatalogDefFK,
               f3.CatalogItemsCodeFK
        FROM dbo.cfgFact f3
        WHERE SettingFK =
        (
            SELECT s.SettingPK
            FROM dbo.cfgSetting s
            WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsValue'
        )
    ) aval
        ON c.catid = aval.CatalogDefFK
           AND c.code = aval.CatalogItemsCodeFK
    LEFT JOIN
    (
        SELECT FactValue AS CededName,
               f4.CatalogDefFK,
               f4.CatalogItemsCodeFK
        FROM dbo.cfgFact f4
        WHERE SettingFK =
        (
            SELECT s.SettingPK
            FROM dbo.cfgSetting s
            WHERE [s].[Name] = N'GlobalRe Ceded CatalogItemsValue'
        )
    ) cval
        ON c.catid = cval.CatalogDefFK
           AND c.code = cval.CatalogItemsCodeFK
    LEFT JOIN
    (
        SELECT FactValue AS AssumedSortOder,
               f5.CatalogDefFK,
               f5.CatalogItemsCodeFK
        FROM dbo.cfgFact f5
        WHERE SettingFK =
        (
            SELECT s.SettingPK
            FROM dbo.cfgSetting s
            WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsSortOrder'
        )
    ) aord
        ON c.catid = aord.CatalogDefFK
           AND c.code = aord.CatalogItemsCodeFK
    LEFT JOIN
    (
        SELECT FactValue AS CededSortOrder,
               f6.CatalogDefFK,
               f6.CatalogItemsCodeFK
        FROM dbo.cfgFact f6
        WHERE SettingFK =
        (
            SELECT s.SettingPK
            FROM dbo.cfgSetting s
            WHERE [s].[Name] = N'GlobalRe Ceded CatalogItemsSortOrder'
        )
    ) cord
        ON c.catid = cord.CatalogDefFK
           AND c.code = cord.CatalogItemsCodeFK;
GO

GRANT SELECT ON grs.[v_CatalogItemsExt] TO public;

GO


