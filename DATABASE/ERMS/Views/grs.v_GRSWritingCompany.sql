DROP VIEW grs.v_GRSWritingCompany;
GO

/*
View returns list of applicable papers for GRS application.

*/
CREATE VIEW grs.v_GRSWritingCompany AS

SELECT
	papernum
	,companyname
	,cpnum
	,ct.name AS 'relatedcompany'
	,addr1 AS 'address1'
	,addr2 AS 'address2'
	,addr3 AS 'address3'
	,city
	,state
	,postalcode
	,cty AS 'country'
	,phone
	,fax
	,imagefilename
	,company_shortname
	,SLTrequired
	,IPTrequired
	,paper_token AS 'policynumber_token'
	,currency
	,territory
	,tb_paper.active
	,HideUnusedClaimCategory
	,JECode
	,CASE
		WHEN CAST(ct.charcode AS INT) > 0 THEN CAST(ct.charcode AS INT)
		ELSE 0  -- for handling NULL's in lower environements
	END
	AS 'IsGRSDisplay'
FROM dbo.tb_paper
LEFT OUTER JOIN (SELECT
		*
	FROM dbo.tb_catalogitems item
	WHERE item.catid = (SELECT
			def.catid
		FROM dbo.tb_catalogdef def
		WHERE def.catname = N'GRS Writing Company')) ct
	ON ct.code = papernum
--ORDER BY ct.catorder ASC

GO

GRANT SELECT ON grs.v_GRSWritingCompany TO public;

GO
 