
DROP VIEW  [grs].[v_GRSNotes];
GO


CREATE VIEW [grs].[v_GRSNotes] AS
/*
View returns a list of deal notes.
*/

SELECT [notenum]
      ,dn.[dealnum]
      ,dn.[notedate]
      ,dn.[notetype]
      ,dn.[notes] --description
      ,dn.[whoentered]
	  ,nn.Name
	  ,nn.FirstName
	  ,nn.MiddleName
	  ,nn.LastName
	  ,dn.CreatedBy
	    FROM [dbo].[tb_dealnotes] dn
		LEFT OUTER JOIN [dbo].[tb_Names] nn
		ON dn.whoentered=nn.NameId

GO

GRANT SELECT ON  [grs].[v_GRSNotes] TO public;

GO
