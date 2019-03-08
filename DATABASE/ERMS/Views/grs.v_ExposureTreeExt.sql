DROP VIEW grs.v_ExposureTreeExt;
GO

CREATE VIEW grs.v_ExposureTreeExt AS
/*
View returns a list of GRS specific exposure tree. As recieved from Partha.
*/

SELECT DISTINCT 

        tc2.name AS SubdivisionName,
        tc2.code AS SubdivisionCode, 
        rdrpl.Product_Line_Code AS ProductLineCode,
        rdrpl.Product_Line_Name AS ProductLineName,
        temg.GroupId AS ExposureGroupCode,
        temg.GroupName AS ExposureGroupName,
        ename.exposuretype  AS ExposureTypeCode,
        ename.exposurename AS ExposureTypeName

--logic for getting GR exposure tree
 FROM  dbo.tb_exposetype explev1 
	OUTER APPLY 
      (
		SELECT et.exptype1 AS exposuretype
		FROM dbo.tb_exp_map et 
		WHERE et.exptype1 = explev1.exposuretype AND et.etmnum = 7
		UNION ALL

		SELECT et.exptype2 AS exposuretype
		FROM dbo.V_exposuretree et 
		WHERE et.exptype1 = explev1.exposuretype AND et.etmnum = 7

		UNION ALL

		SELECT et.exptype3 AS exposuretype
		FROM dbo.V_exposuretree et 
		WHERE et.exptype1 = explev1.exposuretype AND et.etmnum = 7
	  ) x 
	INNER JOIN dbo.tb_exposetype ename ON ename.exposuretype = x.exposuretype
	
	-- logic for getting ExposureGroup and Subdivision
	JOIN dbo.tb_ExposureMapDetail temd ON temd.exposureid = ename.exposuretype
	JOIN dbo.tb_ExposureMapGroup temg ON temg.GroupId = temd.GroupId AND temg.mapid = 13
   
   -- logic for getting product line name and code
	 JOIN rpt.ReferenceDB_XREFTAB_ERMS_Product_Line rdxepl ON cast(ename.exposuretype AS varchar(10)) = rdxepl.Legacy_Exposure_Type_Code 
	 JOIN rpt.ReferenceDB_REFTAB_Product_Line rdrpl ON rdxepl.Product_Line_Code = rdrpl.Product_Line_Code 
	
	LEFT OUTER JOIN dbo.tb_catalogdef tc ON tc.catname = 'Subdivision'
	LEFT OUTER JOIN [grs].[v_CatalogItemsExt] tc2 ON tc.catid = tc2.catid AND tc2.active = 1 AND tc2.name = 
	CASE WHEN temg.SubDivision = 'Property International' OR temg.SubDivision = 'Property North America' THEN 'Property' ELSE temg.SubDivision END

	WHERE explev1.explevel = 1 AND ename.active = 1 
	
	--additionlal logic to get only child node and exclude parent exposure as per requirement sheet
	AND ename.exposuretype NOT IN (SELECT temd2.exptype1 FROM dbo.tb_exp_map_dtl temd2 WHERE temd2.exptype2 IS NOT NULL)
	AND ename.exposuretype NOT IN (SELECT distinct temd2.exptype2 FROM dbo.tb_exp_map_dtl temd2
	JOIN dbo.tb_exposetype te ON temd2.exptype2 = te.parent WHERE temd2.exptype2 IS NOT NULL AND parent IS NOT NULL AND te.active = 1)
	AND rdxepl.Legacy_UW_Team_Code IN (SELECT cast(tt.team AS varchar(10))  FROM dbo.tb_teambuc tt WHERE busdiv IN ('MGR','FED','CHUB'))
	AND rdxepl.Mapping_Effective_End_Date >= Getdate()

   

GO 

GRANT SELECT ON grs.v_ExposureTreeExt TO public;
GO


