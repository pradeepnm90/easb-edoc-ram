DROP VIEW grs.v_KeyDocuments
GO

/*
View returns list of key douments (Documents Types) mapped in GRS/ERMS system

*/
CREATE VIEW grs.v_KeyDocuments AS


		SELECT
		dcv.Value_Id AS keydocid
		,dcv.Key1 AS filenumber
		,D.producer AS producer
		,dcv.DMS_DocId AS docid
		,dcv.DMS_DocName AS docname
		,cfg.Sequence AS sortorder
		,uw.DMSLocation AS location
		,uw.DMSDrawer AS drawer
		,dci.Item_DMSFolder AS folderid
		,dci.Item_DMSFolder2 AS foldername
		,dci.Item_DMSDocumentType AS doctype
		,dcv.DMS_DocType AS ermsclasstype
		,dcv.DMS_FileType AS filetype
		,dci.Item_DMSLocation AS dmspath
		,dci.Item_Id AS itemcategoryid
		,dci.Item_Name AS ermscategory
		,dcv.Last_User AS lastupdateduser
		,dcv.Last_Timestamp AS lasttimestamp
		,dcv.DMS_Created AS dmscreated
		,dcv.DMS_Updated AS dmsupdated
	FROM cfgFact cfg
	INNER JOIN dbo.cfgSetting cfgs
		ON cfgs.SettingPK = cfg.SettingFK
	INNER JOIN dbo.tb_DocChkItem dci
		ON dci.Item_DMSDocumentType = cfg.FactValue
	INNER JOIN tb_DocChkValue dcv
		ON dcv.Item_Id = dci.Item_Id
	INNER JOIN v_deals D
		ON D.dealnum = dcv.Key1
	INNER JOIN dbo.dmUnderwritingTeam uw
		ON uw.UnderwritingTeamPK = D.Producer
	WHERE cfgs.Name = 'GlobalRe DocumentTypes'
	AND dcv.DMS_DocId IS NOT NULL
	UNION ALL (SELECT
		0 AS keydocid
		,0 AS filenumber
		,(CASE 
			WHEN cfg.UnderWritingTeamFK IS NULL THEN 0
			ELSE cfg.UnderWritingTeamFK
		END) AS 'producer'
		,'' AS docid
		,'' AS docname
		,cfg.Sequence AS sortorder
		,'' AS location
		,'' AS drawer
		,chk.Item_DMSFolder AS folderid
		,chk.Item_DMSFolder2 AS foldername
		,cfg.FactValue AS doctype
		,'' AS ermsclasstype
		,'' AS filetype
		,chk.Item_DMSLocation AS dmspath
		,chk.Item_Id AS itemcategoryid
		,chk.Item_Name AS ermscategory
		,'' AS lastupdateduser
		,GETDATE() AS lasttimestamp
		,GETDATE() AS dmscreated
		,GETDATE() AS dmsupdated
	FROM dbo.cfgFact cfg
	INNER JOIN dbo.cfgSetting cfgs
		ON cfgs.SettingPK = cfg.SettingFK
	LEFT OUTER JOIN dbo.tb_DocChkItem chk
		ON chk.Item_DMSDocumentType = cfg.FactValue
	WHERE cfgs.Name = 'GlobalRe DocumentTypes'
	)
	--ORDER BY filenumber, sortorder


GO

GRANT SELECT ON grs.v_KeyDocuments TO public;

GO
