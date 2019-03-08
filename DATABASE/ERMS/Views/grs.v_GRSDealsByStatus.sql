DROP VIEW grs.v_GRSDealsByStatus;
GO

CREATE VIEW grs.v_GRSDealsByStatus AS
/*
View returns a list of deals with GRS specific statuses .
*/
SELECT 
	 d.dealnum
    ,d.dealname
    ,d.contractnum
    ,d.inceptdate
    ,d.expirydate
    ,d.Expiry_EOD
    ,d.targetdt
    ,d.ModelPriority
    ,d.submissiondate
	,d.status
	,d.status_name
	,dsg.StatusCode 
    ,dsg.StatusName
	,d.DealPipelineComplete
	,d.InForce
	,d.Renewable
    ,d.uw1
    ,d.uw1_name
    ,d.uw2
    ,d.uw2_name
    ,d.TA
    ,d.TA_Name
    ,d.Modeller
    ,d.modeller_name
    ,d.act1
    ,d.act1_name 
    ,d.broker
    ,d.broker_name
	,d.broker_location
	,d.broker_locationname
    ,d.broker_contact
    ,d.broker_contact_name
	,d.broker_companygroupId
	,d.broker_companygroup
    ,division
    ,d.paper
    ,d.paper_name
    ,d.team
    ,d.team_name
	,d.exposuretype
	,dsg.GroupID AS StatusGroup
	,dsg.GroupName AS StatusGroupName
	,dsg.GroupSortOrder AS StatusGroupSortOrder
	,dsg.StatusSortOrder AS StatusSortOrder
	,d.continuous
	,d.Cedant
    ,d.cedant_name	
	,d.cedent_location
	,d.cedent_locationname
	,d.cedent_companygroupId
	,d.cedent_companygroup
	,d.renewal
	,d.renewal_name
	,d.dealtype
	,d.dealtype_name
	,d.coveragetype
	,d.coveragetype_name
	,d.policybasis
	,d.policybasis_name
	,d.currency
	,d.currency_name
	,d.domicile
	,d.domicile_name
	,d.region
	,d.region_name
	,d.ChkPreBindProc
	,d.ChkModeling
	,d.ChkUWCompliance
FROM grs.v_GRSDeals d
INNER JOIN grs.v_GRDealStatus dsg
	ON (dsg.StatusCode=d.status -- some statuses map 1:1,
        OR (dsg.StatusCode=1000 AND d.status=0 AND d.DealpipelineComplete=0) -- "Bound" is ERMS "bound" & pending items in Deal Pipeline
        OR (dsg.StatusCode=1001 AND d.Renewable = 1) -- "Renewable" is ERMS "Renewable"
        --OR (dsg.StatusCode=XXXX AND d.inforce = 1) -- "In Force" is ERMS "in force"
        )
WHERE d.outward = 0

GO

GRANT SELECT ON grs.v_GRSDealsByStatus TO public;
GO
