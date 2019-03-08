DROP VIEW grs.v_GRSCedants
GO

/*
View returns list of applicable Cedants details for GRS application.

*/
CREATE VIEW grs.v_GRSCedants AS

SELECT
	CN.NameId as cedantid,
	CN.Name AS cedant,
	CPG.[ParentGroupId] AS parentgrouptypeid,
	CPG.[ParentGroupName] AS parentgrouptype,
	CNG.NameId AS cedantgroupid,
	CNG.Name AS cedantgroupname,
	L.LocationId AS locationid, 
	L.LocationName AS locationname,
	L.LocationAddress AS locationaddress,
	L.LocationCity as locationcity,
	L.LocationState AS locationstate,
	L.LocationPostCode AS locationpostcode,
	ct.country AS country,
	CN2.Name AS parentcompanyname,
	STUFF((
		SELECT distinct ' ,' + Ctc.name FROM dbo.tb_CompanyCategory Ctcc 
		JOIN dbo.tb_catalogitems Ctc ON Ctc.catid = 89 AND Ctcc.CategoryId = Ctc.code
		WHERE Ctcc.Active = 1 AND Ctcc.CompanyRSID = cmp.[CompanySID] 
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,2,''
	) as 'cedantcategories',
	STUFF((
		SELECT distinct ' ,' + cast(Ctc.code as NVARCHAR(10)) FROM dbo.tb_CompanyCategory Ctcc 
		JOIN dbo.tb_catalogitems Ctc ON Ctc.catid = 89 AND Ctcc.CategoryId = Ctc.code
		WHERE Ctcc.Active = 1 AND Ctcc.CompanyRSID = cmp.[CompanySID]
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,2,''
	) as 'cedantcategoryid'
FROM dbo.tb_Company cmp
	JOIN [dbo].[tb_Location] AS L ON (L.[CompanyRSID] = cmp.[CompanySID])
	LEFT JOIN [dbo].[tb_Names] CN ON CN.NameId = cmp.CompanyId
	LEFT OUTER JOIN [dbo].[tb_Names] CN2 ON cmp.ParentCompany = CN2.NameId
	LEFT OUTER JOIN [dbo].[tb_Names] CNG ON cmp.[CompanyGroupID] = CNG.NameId
	OUTER APPLY (SELECT TOP 1 Cct.[code] AS [ParentGroupId], Cct.name AS [ParentGroupName]
					FROM [dbo].[tbl_Company] Ccg
						INNER JOIN [dbo].[tb_CompanyCategory] Ccc ON Ccc.CompanyRSID = Ccg.CompanySID AND Ccc.Active = 1
						INNER JOIN [dbo].[tb_catalogitems] Cct ON Cct.[catid] = 89 AND Cct.[code] = Ccc.[CategoryId] AND Cct.[charcode] = 'GROUP'
						WHERE Ccg.[CompanySID] = CNG.[CompanyId]
						ORDER BY Cct.code DESC
	) CPG
	LEFT JOIN dbo.tb_CompanyCategory Ccat ON Ccat.CompanyRSID = cmp.CompanySID
	LEFT OUTER JOIN dbo.tb_country ct ON ct.cnum = L.LocationCountryId
WHERE 
	Ccat.CategoryId IN (90)						-- Global Re Cedant : CatID - 89 & Code - 90
--	AND CPG.ParentGroupId = 10011				-- Reinsurance Cedant Group : CatID - 89, Code - 10011 & CharCode - GROUP
--	AND cmp.CompanyName like '%Starr Ind%'

--ORDER BY CN.Name ASC
--OPTION (FAST 100)


GO

GRANT SELECT ON grs.v_GRSCedants TO public;

GO

