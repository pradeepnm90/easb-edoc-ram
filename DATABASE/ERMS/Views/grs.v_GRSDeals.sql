DROP VIEW grs.v_GRSDeals;
GO

/*
View returns a list of deals for "GRS".
This view is intended to be a main "point of entry" for GRS system into ERMS deals, and all queries shoudl be based on this view.
Currently creterion for selecting "GRS deals" is "Division = {MGR|FED|CHUB}"
*/
CREATE VIEW grs.v_GRSDeals AS

SELECT d.dealnum
    ,d.dealname
    ,d.contractnum
    ,d.inceptdate
    ,d.expirydate
    ,d.Expiry_EOD
    ,d.targetdt
    ,dp.ModelPriority
    ,d.submissiondate
	,d.status
	,d.outward
    ,st.name AS status_name 
	,ISNULL(dealpipelinecomplete.IsComplete,0) AS  'DealpipelineComplete'
	,IIF(((d.inceptdate <= GETDATE() AND (ISNULL(d.termdate , d.expirydate - (1 - ISNULL(CAST(d.Expiry_EOD as int), 0))) >= GETDATE()))) AND (d.status = 0), 1,0) AS InForce
	,IIF((d.inceptdate <= GETDATE()) AND (d.termdate IS NULL) AND (d.expirydate BETWEEN DATEADD(DAY, 1, EOMONTH(GETDATE())) AND EOMONTH(GETDATE(), 6) ) AND (d.status = 0), 1,0) AS Renewable
    ,d.uw1
    ,uw_n.FullName AS uw1_name
    ,d.uw2
    ,uw2_n.FullName AS uw2_name
    ,dp.TA
    ,ta_n.FullName AS TA_Name
    ,d.Modeller
    ,m_n.FullName AS modeller_name
    ,d.act1
    ,a_n.FullName AS act1_name -- actuary #1
    ,d.source AS [broker]
    ,n3.Name AS [broker_name]
	,d.sourcelocation AS [broker_location]
	,l3.LocationName AS [broker_locationname]
    ,d.bbroker AS [broker_contact]
    ,n5.Name AS [broker_contact_name]
	,BGN.NameId AS [broker_companygroupId]
	,BGN.Name AS [broker_companygroup]
    ,tb.busdiv AS division
    ,d.paper
    ,p.companyname AS paper_name
    ,d.producer AS team
    ,t.Name AS team_name
	,d.exposuretype
	,d.continuous
	,d.cpartynum AS Cedant
    ,n6.Name AS [cedant_name]
	,d.cpartylocation AS [cedent_location]
	,l6.LocationName AS [cedent_locationname]
	,CGN.NameId AS [cedent_companygroupId]
	,CGN.Name AS [cedent_companygroup]
	,d.renewal AS [renewal]
	,tcr.name AS [renewal_name]
	,d.reinsurance AS [dealtype]
	,dt.name AS [dealtype_name]
	,d.covtype AS [coveragetype]
	,cb.name AS [coveragetype_name]
	,d.policybasis AS [policybasis]
	,pb.name AS [policybasis_name]
	,d.currency AS [currency]
	,cr.currname AS [currency_name]
	,d.territory as [domicile]
	,dm.country AS [domicile_name]
	,d.Region AS [region]
	,rg.name AS [region_name]
	,PL.[ChkPreBindProc]
	,PL.[ChkModeling]
	,PL.[ChkUWCompliance]
FROM dbo.tbl_deals d
JOIN dbo.dmUnderwritingTeam t ON t.UnderwritingTeamPK=d.producer
JOIN dbo.tb_paper p ON p.papernum=d.paper
JOIN dbo.tb_teambuc tb ON tb.paper=d.paper AND tb.team=t.UnderwritingTeamPK
LEFT OUTER JOIN dbo.tb_deal_pipeline dp ON dp.DealNum = d.dealnum
LEFT OUTER JOIN dbo.fnDWgetCatalogItems(7) st ON st.code=d.status
LEFT OUTER JOIN tb_Names uw_n ON uw_n.NameId=d.uw1
LEFT OUTER JOIN tb_Names uw2_n ON uw2_n.NameId=d.uw2
LEFT OUTER JOIN tb_Names ta_n ON ta_n.NameId=dp.TA
LEFT OUTER JOIN tb_Names m_n ON m_n.NameId=d.Modeller
LEFT OUTER JOIN tb_Names a_n ON a_n.NameId=d.act1
LEFT OUTER JOIN tb_Names n3 ON (d.source = n3.NameId)									-- Broker Details
LEFT OUTER JOIN tb_Location l3 ON (d.sourcelocation = l3.LocationId)					-- Broker Location
LEFT OUTER JOIN tb_Names n5 ON (d.bbroker = n5.NameId)
LEFT OUTER JOIN tb_Names n6 ON (d.cpartynum = n6.NameId)								-- Cedent Details
LEFT OUTER JOIN tb_Location l6 ON (d.cpartylocation = l6.LocationId)							-- Cedent Location
LEFT OUTER JOIN tb_catalogitems cb ON cb.catid = 87 AND (d.covtype = cb.code)			-- Coverage Type 
LEFT OUTER JOIN tb_catalogitems pb ON pb.catid = 81 AND (d.policybasis = pb.code)		-- Policy Basis 
LEFT OUTER JOIN tb_catalogitems dt ON dt.catid = 79 AND (d.reinsurance = dt.code)		-- Deal Type 
LEFT OUTER JOIN tb_catalogitems rg ON rg.catid = 145 AND (d.Region = rg.code)			-- Region 
LEFT OUTER JOIN tb_currency cr ON (d.currency = cr.currency)							-- Currency 
LEFT OUTER JOIN tb_country dm ON (d.territory = dm.cnum)								-- Domicile 
LEFT OUTER JOIN dbo.tb_catalogitems tcr ON tcr.catid = 90 AND (d.renewal = tcr.code)			-- Renewal 
LEFT OUTER JOIN [dbo].[tbl_Company] CG ON CG.[CompanySID] = n6.CompanyId
LEFT OUTER JOIN [dbo].[tb_Names] CGN ON CG.[CompanyGroupID] = CGN.NameId
LEFT OUTER JOIN [dbo].[tbl_Company] BG ON BG.[CompanySID] = n3.CompanyId
LEFT OUTER JOIN [dbo].[tb_Names] BGN ON BG.[CompanyGroupID] = BGN.NameId
LEFT OUTER JOIN (SELECT X.key1
  , MAX(CASE WHEN X.[category] = 17 THEN X.TaskName END) AS [ChkPreBindProc] --Pre-Bind Processing
  , MAX(CASE WHEN X.[category] = 18 THEN X.TaskName END) AS [ChkModeling] --Modeling
  , MAX(CASE WHEN X.[category] = 19 THEN X.TaskName END) AS [ChkUWCompliance] --Underwriting Compliance
  
  FROM (SELECT V.key1
    , F.[category]
	--, tc.[name] AS CategoryName
    , V.Notes
    , L.TaskName AS [TaskName]
    , ROW_NUMBER() OVER (PARTITION BY V.key1, F.Category ORDER BY V.completed DESC, F.sortorder DESC) AS [Rank]
    FROM dbo.tb_chklistvals AS V WITH (NOLOCK)
    JOIN dbo.tb_chklistfilter AS F ON F.entitynum = V.entitynum AND F.chklistnum = V.chklistnum
    JOIN dbo.tb_chklist AS L ON F.entitynum = L.entitynum AND F.chklistnum = L.chklistnum
				AND V.chklistnum = L.chklistnum AND L.entitynum = V.entitynum
	JOIN dbo.tbl_deals td ON v.key1=td.dealnum AND F.filter = td.producer
	--JOIN dbo.tb_catalogitems tc ON tc.catid = 180 AND tc.code = f.category
    WHERE V.entitynum = 1
    ) AS X
  WHERE [Rank] = 1
  GROUP BY X.key1
  ) AS PL ON PL.key1 = D.dealnum

OUTER APPLY 
	(SELECT TOP 1 CASE WHEN ISNULL(key1, 0) = 0 THEN 0 ELSE 1 END AS  IsComplete
	 FROM dbo.tb_chklist c
	 INNER JOIN dbo.tb_chklistvals v ON v.entitynum = c.entitynum AND v.chklistnum = c.chklistnum
	 WHERE c.entitynum = 1 -- deals
	   AND c.chklistnum = 100 -- 'Deal Pipeline Complete'
	   AND d.dealnum = v.key1
	 ) dealpipelinecomplete
WHERE d.dealnum > 0 
  AND tb.busdiv IN ('MGR','FED','CHUB')



GO

GRANT SELECT ON grs.v_GRSDeals TO public;

GO
 