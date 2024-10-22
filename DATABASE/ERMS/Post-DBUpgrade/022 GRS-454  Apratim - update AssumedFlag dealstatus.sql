/****** Script for SelectTopNRows command from SSMS  ******/
BEGIN TRANSACTION

DECLARE @catid INT, @notetypecode INT, @authori INT, @boundpending INT, @notused INT,@onhold INT,@outstandingquote INT, @tobedeclined INT,@underreview INT

SELECT @catid=catid FROM [dbo].[tb_catalogdef]
WHERE catname='Deal Status'

--SELECT * FROM [dbo].[tb_catalogdef] WHERE catname='Deal Status'

--SELECT * FROM [dbo].tb_catalogitems WHERE name='Under Review' and catid=7
--code=80

SELECT @authori = code FROM [dbo].tb_catalogitems
WHERE name='Authorize' and catid=@catid

SELECT @boundpending = code FROM [dbo].tb_catalogitems
WHERE name='Bound Pending Data Entry' and catid=@catid

SELECT @notused = code FROM [dbo].tb_catalogitems
WHERE name='Not Used' and catid=@catid

SELECT @onhold = code FROM [dbo].tb_catalogitems
WHERE name='On Hold' and catid=@catid

SELECT @outstandingquote = code FROM [dbo].tb_catalogitems
WHERE name='Outstanding Quote' and catid=@catid

SELECT @tobedeclined = code FROM [dbo].tb_catalogitems
WHERE name='To Be Declined' and catid=@catid 

SELECT @underreview = code FROM [dbo].tb_catalogitems
WHERE name='Under Review' and catid=@catid

--SELECT @catid,@notetypecode,@general,@Underwriting,@PeerReview,@Decision

-- select * from [tb_catalogitems] where catid=7
    --Master table for assume name entry

DECLARE @catalogitemsvisible INT
SELECT @catalogitemsvisible=s.SettingPK
            FROM dbo.cfgSetting s
            WHERE [s].[Name] = N'GlobalRe Assumed CatalogItemsVisible'

DELETE FROM [dbo].[cfgFact] WHERE dbo.cfgFact.SettingFK = @catalogitemsvisible AND dbo.cfgFact.CatalogDefFK = @catid;

INSERT INTO [dbo].[cfgFact] ([CatalogITemsCodeFK], [Sequence], [CatalogDefFK], [FACTvalue], [SettingFK]) VALUES (@authori, 0, @catid, '',@catalogitemsvisible)
                INSERT INTO [dbo].[cfgFact] ([CatalogITemsCodeFK], [Sequence], [CatalogDefFK], [FACTvalue], [SettingFK]) VALUES (@boundpending, 0, @catid, '',@catalogitemsvisible)
                INSERT INTO [dbo].[cfgFact] ([CatalogITemsCodeFK], [Sequence], [CatalogDefFK], [FACTvalue], [SettingFK]) VALUES (@notused, 0, @catid, '',@catalogitemsvisible)
    INSERT INTO [dbo].[cfgFact] ([CatalogITemsCodeFK], [Sequence], [CatalogDefFK], [FACTvalue], [SettingFK]) VALUES (@onhold, 0, @catid, '',@catalogitemsvisible)
                INSERT INTO [dbo].[cfgFact] ([CatalogITemsCodeFK], [Sequence], [CatalogDefFK], [FACTvalue], [SettingFK]) VALUES (@outstandingquote, 0, @catid, '',@catalogitemsvisible)
                INSERT INTO [dbo].[cfgFact] ([CatalogITemsCodeFK], [Sequence], [CatalogDefFK], [FACTvalue], [SettingFK]) VALUES (@tobedeclined, 0, @catid, '',@catalogitemsvisible)
                INSERT INTO [dbo].[cfgFact] ([CatalogITemsCodeFK], [Sequence], [CatalogDefFK], [FACTvalue], [SettingFK]) VALUES (@underreview, 0, @catid, '',@catalogitemsvisible)

--SELECT * FROM [v_CatalogItemsExt] WHERE catid= 100 AND dbo.v_CatalogItemsExt.AssumedFlag =1 
--ORDER BY dbo.v_CatalogItemsExt.AssumedSortOder
COMMIT;
--ROLLBACK


