DROP VIEW grs.v_GRSCompany
GO

/*
View returns list of applicable Cedants details for GRS application.

*/
CREATE VIEW grs.v_GRSCompany AS

SELECT 
	c.[CompanyId],
	c.[CompanySID],
	c.[CompanyName],
	c.[ParentGroupId] AS parentGroupTypeId,
	ci.[Name] AS parentGroupType,
	c.[CompanyGroupID] AS companyGroupId,
	c.[CompanyGroup] AS companyGroupName,
	isGRSCedant = (SELECT TOP 1 1 FROM dbo.tb_CompanyCategory cc 
				   WHERE cc.CompanyRSID = c.CompanySID AND cc.CategoryId = 90 -- Global Re Cedant : CatID - 89 & Code - 90
				   ),
	isGRSBroker = (SELECT TOP 1 1 FROM dbo.tb_CompanyCategory cc 
				   WHERE cc.CompanyRSID = c.CompanySID AND cc.CategoryId IN (91,2) -- Global Re Broker : CatID - 89 & Code - 91 | ERMS Broker : CatID - 89 & Code - 2
				  )
FROM dbo.v_Company c 
LEFT OUTER JOIN dbo.tb_catalogitems ci ON ci.catid = 89  AND ci.code = c.ParentGroupId AND ci.[charcode] = 'GROUP'
WHERE c.CompanySID IN (SELECT cc.CompanyRSID 
					   FROM dbo.tb_CompanyCategory cc 
					   WHERE cc.CompanyRSID = c.CompanySID AND cc.CategoryId IN 
								(90, -- Global Re Cedant : CatID - 89 & Code - 90
								 91,2) -- Global Re Broker : CatID - 89 & Code - 91 | ERMS Broker : CatID - 89 & Code - 2
					   )
--ORDER BY c.CompanyId


GO

GRANT SELECT ON grs.v_GRSCompany TO public;

GO
