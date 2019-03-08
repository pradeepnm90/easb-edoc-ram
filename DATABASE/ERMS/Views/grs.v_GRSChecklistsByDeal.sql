DROP VIEW grs.v_GRSChecklistsByDeal;
GO

/*
View returns a list of checklist by deal for "GRS".
*/
CREATE VIEW grs.v_GRSChecklistsByDeal AS

SELECT vg.dealnum,F.entitynum, CI.[charcode] as [CategoryCharCode],
F.[Category], CI.[strdata1] as [CategoryShortName], CI.[AssumedName] as [CategoryName], CI.[AssumedSortOder] AS 'catorder'
,  F.[chklistnum], C.[TaskName] AS 'ChecklistName', F.[sortorder], cast(isnull(N.[NoteLinks],0) AS bit) AS 'Readonly'
, CASE WHEN val.chklistnum IS NULL THEN cast(0 AS bit) ELSE cast(1 AS bit) END AS 'Checked'
,val.PersonId,val.PersonName,val.FirstName,val.LastName,val.MiddleName,val.completed AS 'CheckedDateTime',val.Notes AS 'Note'
FROM [grs].[v_CatalogItemsExt] CI
join [dbo].[tb_chklistfilter] F ON F.[Category] = CI.[code] AND F.AssumedFlag = 1
JOIN grs.v_GRSDeals vg ON vg.team = F.filter
JOIN [dbo].[tb_chklist] C ON C.[entitynum] = F.[entitynum] AND C.[chklistnum] = F.[chklistnum]
LEFT JOIN (SELECT [intdata1], count([code]) as [NoteLinks] FROM [grs].[v_CatalogItemsExt] N WHERE N.[catid]=100 GROUP BY [intdata1]) N
ON N.[intdata1]=C.[chklistnum]
LEFT JOIN (SELECT tc.entitynum,tc.chklistnum,tc.key1,tc.PersonId,tc.Notes,tc.completed,tp.Name as PersonName,tp.FirstName,tp.LastName,tp.MiddleName FROM dbo.tb_chklistvals tc
LEFT JOIN dbo.tb_Names tp ON tc.PersonId = tp.NameId) val 
ON val.entitynum = F.entitynum AND F.chklistnum = val.chklistnum AND vg.dealnum = val.key1
where (CI.[catid] = 180)
--AND vg.dealnum = 106681 


GO

GRANT SELECT ON grs.v_GRSChecklistsByDeal TO public;

GO
 