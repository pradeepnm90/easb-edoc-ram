DROP VIEW [grs].[v_DMSFileType]
GO

/*

VIEW NAME : [grs].[v_DMSFileType]
DESCRIPTION :Get the File type of deals
CREATED BY :Namita Barnwal
CREATED ON :14-FEB-2019
MODIFIED BY
MODIFIED ON
MODIFICATION DESCRIPTION:

*/
CREATE VIEW [grs].[v_DMSFileType] AS

SELECT td.DmsSystem,td.UnderwritingTeamFK,td.DocType,td.IsBound,td.FileType,td.Folder,td.DocumentLog,td.IsOutward 
		,vg.dealnum,vg.producer
FROM dbo.tb_DMSConfig td 
JOIN dbo.tbl_deals vg ON td.UnderwritingTeamFK = vg.producer 
AND td.IsBound = case when vg.status = 0 THEN 1 ELSE 0 END AND td.IsOutward = vg.outward




GO


GRANT SELECT ON [grs].[v_DMSFileType] TO public;

GO


