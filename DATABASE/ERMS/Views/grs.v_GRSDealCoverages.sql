DROP VIEW grs.v_GRSDealCoverages;
GO

/*
View returns a list of deals for "GRS".
This view is intended to be a "point of entry" for GRS system into ERMS deal coverages, and all queries shoudl be based on this view
Deals output should be mapped with grs.v_GRSDeals 
*/
CREATE VIEW grs.v_GRSDealCoverages AS

SELECT d.dealnum
	,d.cover_id
	,t.cover_name
FROM tb_dealcover d
JOIN tb_cover t ON t.cover_id = d.cover_id
WHERE d.dealnum > 0 

GO

GRANT SELECT ON grs.v_GRSDealCoverages TO public;

GO
 