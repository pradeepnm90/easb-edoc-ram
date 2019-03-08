DROP VIEW grs.v_GRSUserView;
GO

/*
View returns a list of deals for "GRS".
This view is intended to be a "point of entry" for GRS system into ERMS deal coverages, and all queries shoudl be based on this view
Deals output should be mapped with grs.v_GRSDeals 
*/
CREATE VIEW grs.v_GRSUserView AS

 SELECT [view_id]
	   ,[userid]
      ,[screenname]
      ,[viewname]
      ,[default]
      ,[layout]
	  ,UserViewCreationDate
	  ,[customview]
	  ,[sortorder]
FROM [grs].[tbl_userviews] 

GO

GRANT SELECT ON grs.v_GRSUserView TO public;

GO
 