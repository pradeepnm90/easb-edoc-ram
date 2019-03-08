CREATE TABLE [dbo].[tb_chklistfilter]
(
[entitynum] [int] NOT NULL,
[filter] [int] NOT NULL,
[chklistnum] [int] NOT NULL,
[category] [int] NULL CONSTRAINT [DF_tb_producerchklist_category] DEFAULT (0),
[sortorder] [int] NULL,
[AssumedFlag] [bit] NULL,
[_sys_CreatedBy] [varchar] (50) NULL CONSTRAINT [DF_tb_chklistfilter_sys_CreatedBy] DEFAULT (suser_sname()),
[_sys_CreatedDt] [datetime] NULL CONSTRAINT [DF_tb_chklistfilter_sys_CreatedDt] DEFAULT (getdate()),
[_sys_ModifiedBy] [varchar] (50) NULL,
[_sys_ModifiedDt] [datetime] NULL
)
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    CREATE TRIGGER [dbo].[_sys_TU_tb_chklistfilter] ON [dbo].[tb_chklistfilter] FOR UPDATE 
      NOT FOR REPLICATION
    AS 
    BEGIN
      UPDATE t SET
      _sys_ModifiedBy=SYSTEM_USER,
      _sys_ModifiedDt=GETDATE()
      FROM [dbo].[tb_chklistfilter] t
      JOIN inserted i ON (i.[filter]=t.[filter] or (i.[filter] is null and t.[filter] is null)) and (i.[entitynum]=t.[entitynum] or (i.[entitynum] is null and t.[entitynum] is null)) and (i.[chklistnum]=t.[chklistnum] or (i.[chklistnum] is null and t.[chklistnum] is null))
    END
GO
ALTER TABLE [dbo].[tb_chklistfilter] ADD CONSTRAINT [PK_tb_producerchklist] PRIMARY KEY CLUSTERED  ([filter], [entitynum], [chklistnum])
GO
ALTER TABLE [dbo].[tb_chklistfilter] ADD CONSTRAINT [FK_tb_chklistfilter_tb_chklist] FOREIGN KEY ([entitynum], [chklistnum]) REFERENCES [dbo].[tb_chklist] ([entitynum], [chklistnum]) ON DELETE CASCADE ON UPDATE CASCADE
GO
GRANT SELECT ON  [dbo].[tb_chklistfilter] TO [ErmsUsers]
GRANT INSERT ON  [dbo].[tb_chklistfilter] TO [ErmsUsers]
GRANT DELETE ON  [dbo].[tb_chklistfilter] TO [ErmsUsers]
GRANT UPDATE ON  [dbo].[tb_chklistfilter] TO [ErmsUsers]
GO
