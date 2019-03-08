DROP VIEW grs.v_GRSContractTypes;
GO

/*
View returns a list of contract types along with exposure type for default contract type logic.

*/
CREATE VIEW grs.v_GRSContractTypes AS

SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY tc.code ASC) AS RowID, 
tc.code,tc.catorder,tc.name as InsuranceName,tc.active,
tc.AssumedName,tc.AssumedFlag,tc.CededName,tc.CededFlag
,te.exposuretype,te.exposurename
FROM [grs].[v_CatalogItemsExt] tc
LEFT JOIN dbo.cfgSetting cs ON cs.Name = 'GRS UW Contract Type Defaults'
LEFT JOIN dbo.cfgFact cf ON cs.SettingPK = cf.SettingFK AND tc.code = cf.FactValue
LEFT JOIN dbo.tb_exposetype te ON cf.ExposureTypeFK = te.exposuretype
WHERE tc.catid = 79 AND tc.active = 1

GO

GRANT SELECT ON grs.v_GRSContractTypes TO public;

GO
 