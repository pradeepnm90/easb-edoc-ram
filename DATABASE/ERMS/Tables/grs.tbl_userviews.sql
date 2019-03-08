CREATE TABLE [grs].[tbl_userviews]
(
[view_id] [int] NOT NULL IDENTITY(1, 1),
[userid] [int] NOT NULL,
[screenname] [varchar] (100) NOT NULL,
[viewname] [varchar] (50) NOT NULL,
[default] [bit] NOT NULL CONSTRAINT [DF_tbl_userviews_default] DEFAULT (0),
[layout] [text] NULL,
[customview] [bit] NOT NULL CONSTRAINT [DF_tbl_userviews_customview]  DEFAULT (1),
[sortorder] [int] NOT NULL CONSTRAINT [DF_tbl_userviews_sortorder]  DEFAULT (0),
[_sys_CreatedBy] [varchar] (50) NULL CONSTRAINT [DF_tbl_userviews_sys_CreatedBy] DEFAULT (suser_sname()),
[_sys_CreatedDt] [datetime] NULL CONSTRAINT [DF_tbl_userviews_sys_CreatedDt] DEFAULT (getdate()),
[_sys_ModifiedBy] [varchar] (50) NULL,
[_sys_ModifiedDt] [datetime] NULL,
[UserViewCreationDate] [datetime] NULL CONSTRAINT [DF_tbl_userviews_UserViewCreationDate] DEFAULT (GETDATE())
)
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    CREATE TRIGGER [grs].[_sys_TU_tbl_userviews] ON [grs].[tbl_userviews] FOR UPDATE 
      NOT FOR REPLICATION
    AS 
    BEGIN
      UPDATE t SET
      _sys_ModifiedBy=SYSTEM_USER,
      _sys_ModifiedDt=GETDATE()
      FROM [grs].[tbl_userviews] t
      JOIN inserted i ON (i.[view_id]=t.[view_id] or (i.[view_id] is null and t.[view_id] is null))
    END
GO
ALTER TABLE [grs].[tbl_userviews] ADD CONSTRAINT [PK_tbl_userviews] PRIMARY KEY CLUSTERED  ([view_id])

ALTER TABLE [grs].[tbl_userviews] ADD CONSTRAINT [FK_tb_user_tbl_userviews] FOREIGN KEY ([userid]) REFERENCES [dbo].[tb_user] (userid)

ALTER TABLE [grs].[tbl_userviews] ADD CONSTRAINT [PK_tbl_userviews_userid_viewname] UNIQUE ([userid], [screenname] , [viewname])

--ALTER TABLE [grs].[tbl_userviews] ADD UserViewCreationDate DATETIME NOT NULL DEFAULT (GETDATE());

GO
GRANT SELECT ON  [grs].[tbl_userviews] TO [ErmsUsers]
GRANT INSERT ON  [grs].[tbl_userviews] TO [ErmsUsers]
GRANT DELETE ON  [grs].[tbl_userviews] TO [ErmsUsers]
GRANT UPDATE ON  [grs].[tbl_userviews] TO [ErmsUsers]
GO
