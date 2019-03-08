
DROP VIEW  [grs].[v_PaperExt];
GO


CREATE VIEW [grs].[v_PaperExt] AS
SELECT p.*,
       av.AssumedFlag,
       aord.AssumedSortOrder,
       cv.CededFlag,
       cord.CededSortOrder
FROM dbo.tb_paper p
    LEFT JOIN
    (
        SELECT 1 AS AssumedFlag,
               f1.PaperFK
        FROM dbo.cfgFact f1
        WHERE SettingFK =
        (
            SELECT s.SettingPK
            FROM dbo.cfgSetting s
            WHERE [s].[Name] = N'GlobalRe Assumed PaperVisible'
        )
    ) av
        ON p.papernum = av.PaperFK
    LEFT JOIN
    (
        SELECT 1 AS CededFlag,
               f2.PaperFK
        FROM dbo.cfgFact f2
        WHERE SettingFK =
        (
            SELECT s.SettingPK
            FROM dbo.cfgSetting s
            WHERE [s].[Name] = N'GlobalRe Ceded PaperVisible'
        )
    ) cv
        ON p.papernum = cv.PaperFK
    LEFT JOIN
    (
        SELECT f3.FactValue AS AssumedSortOrder,
               f3.PaperFK
        FROM dbo.cfgFact f3
        WHERE SettingFK =
        (
            SELECT s.SettingPK
            FROM dbo.cfgSetting s
            WHERE [s].[Name] = N'GlobalRe Assumed PaperSortOrder'
        )
    ) aord
        ON p.papernum = aord.PaperFK
    LEFT JOIN
    (
        SELECT f4.FactValue AS CededSortOrder,
               f4.PaperFK
        FROM dbo.cfgFact f4
        WHERE SettingFK =
        (
            SELECT s.SettingPK
            FROM dbo.cfgSetting s
            WHERE [s].[Name] = N'GlobalRe Ceded PaperSortOrder'
        )
    ) cord
        ON p.papernum = cord.PaperFK;
GO
GRANT SELECT ON  [grs].[v_PaperExt] TO public;

GO
