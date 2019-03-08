DROP VIEW grs.[v_GRSBrokers]
GO

/****** Object:  View [grs].[v_GRSBrokers]    Script Date: 2/6/2019 3:15:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
View returns list of applicable Broker details for GRS application.

*/
CREATE VIEW [grs].[v_GRSBrokers] AS

SELECT DISTINCT
	p.PersonId AS brokercontactid,
	p.PersonName AS brokercontact,
	p.FirstName AS firstname,
	p.LastName AS lastname,
	pd.[Office eMail] AS email,
	pd.[Office Phone] AS phone,
	pd.[Office Fax] AS fax,
	pd.[Office Website] AS website,
	pd.[Mobile Phone] AS mobilephone,
	pd.[Other Phone] AS otherphone,
	pd.[Other eMail] AS otheremail,
	pd.[Home Phone] AS homephone,
	pd.[Home Fax] AS homefax,
	pd.[Home eMail] AS homeemail,
	pd.[Office Extension] AS officeextension,
	pd.[Speed Dial] AS speeddial,
	CN.NameId AS brokerid,
	CN.Name AS broker,
	CPG.[ParentGroupId] AS parentgrouptypeid, -- Broker Group Type ID
	CPG.[ParentGroupName] AS parentgrouptype, -- Broker Group Type
	CNG.NameId AS brokergroupid, -- Broker Group ID
	CNG.Name AS brokergroupname, -- Broker Gruop Name 
	L.LocationId AS locationid, 
	L.AMSCode AS AMSCode,
	L.LocationName AS locationname,
	L.LocationAddress AS locationaddress,
	L.LocationCity AS locationcity,
	L.LocationState AS locationstate,
	L.LocationPostCode AS locationpostcode,
	ct.country AS country,
	CN2.Name AS parentcompanyname,
	STUFF((
		SELECT DISTINCT ' ,' + Ctc.name FROM dbo.tb_CompanyCategory Ctcc 
		JOIN dbo.tb_catalogitems Ctc ON Ctc.catid = 89 AND Ctcc.CategoryId = Ctc.code
		WHERE Ctcc.Active = 1 AND Ctcc.CompanyRSID = cmp.[CompanySID] 
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,2,''
	) AS 'brokercategories',
	STUFF((
		SELECT DISTINCT ' ,' + CAST(Ctc.code AS NVARCHAR(10)) FROM dbo.tb_CompanyCategory Ctcc 
		JOIN dbo.tb_catalogitems Ctc ON Ctc.catid = 89 AND Ctcc.CategoryId = Ctc.code
		WHERE Ctcc.Active = 1 AND Ctcc.CompanyRSID = cmp.[CompanySID]
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,2,''
	) AS 'brokercategoryid'
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
	LEFT OUTER JOIN dbo.tb_Person P ON p.LocationId = L.LocationId
	LEFT OUTER JOIN v_PersonDetail pd ON pd.PersonID = p.PersonID
WHERE 
	Ccat.CategoryId IN (91,2)					-- Global Re Broker : CatID - 89 & Code - 91 | ERMS Broker : CatID - 89 & Code - 2
	--AND CPG.ParentGroupId = 10012				-- Reinsurance Broker Group : CatID - 89, Code - 10012 & CharCode - GROUP
	--AND p.PersonId IS not null
--ORDER BY brokercontact


GO

GRANT SELECT ON grs.[v_GRSBrokers] TO public;

GO



