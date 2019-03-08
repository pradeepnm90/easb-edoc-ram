CREATE TABLE [dbo].[cfgFact] 
(
[FactPK] [int] NOT NULL IDENTITY(1, 1),
[SettingFK] [int] NOT NULL,
[BusinessUnitFK] [int] NULL,
[PaperFK] [int] NULL,
[PersonFK] [int] NULL,
[RoleFK] [int] NULL,
[UnderWritingTeamFK] [int] NULL,
[DealNumFK] [int] NULL,
[Sequence] [int] NOT NULL CONSTRAINT [DF_configManagementFact_Sequence] DEFAULT (0),
[FactValue] [nvarchar] (255) NULL,
[ExposureTypeFK] [int] NULL,
[ExposureTypeMap] [int] NULL,
[AccountingReserveGroupFK] [int] NULL,
[IsOutward] [bit] NULL,
[DealTypeFk] [int] NULL,
[GLBusinessTypeFk] [int] NULL,
[ActuarialReserveTypeFK] [int] NULL,
[FromInceptDate] [datetime] NULL,
[ToInceptDate] [datetime] NULL,
[YearOfAccount] [int] NULL,
[AccountingCurrencyTLAFK] [varchar] (3) NULL,
[LloydsRiskCodeFK] [varchar] (10) NULL,
[BankFK] [int] NULL,
[BankAccount] [varchar] (50) NULL,
[InvoiceTeamFK] [int] NULL,
[BusinessTypeFK] [int] NULL,
[SourceTypeFK] [int] NULL,
[CatalogDefFK] [int] NULL,
[CatalogItemsCodeFK] [int] NULL,
[_sys_CreatedBy] [varchar] (50) NULL CONSTRAINT [DF_cfgFact_sys_CreatedBy] DEFAULT (suser_sname()),
[_sys_CreatedDt] [datetime] NULL CONSTRAINT [DF_cfgFact_sys_CreatedDt] DEFAULT (getdate()),
[_sys_ModifiedBy] [varchar] (50) NULL,
[_sys_ModifiedDt] [datetime] NULL
)
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    CREATE TRIGGER [dbo].[_sys_TD_cfgFact] ON [dbo].[cfgFact] FOR DELETE 
      NOT FOR REPLICATION
    AS 
    BEGIN
      INSERT INTO dbo._sys_Deleted (table_schema, table_name, deleted_key, DeletedBy, DeletedDt)
        SELECT 'dbo','cfgFact', ' [FactPK]'+COALESCE('=N'''+CONVERT(VARCHAR(MAX),[FactPK])+'''',' IS NULL'), SYSTEM_USER, GETDATE() FROM deleted
    END
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    CREATE TRIGGER [dbo].[_sys_TU_cfgFact] ON [dbo].[cfgFact] FOR UPDATE 
      NOT FOR REPLICATION
    AS 
    BEGIN
      UPDATE t SET
      _sys_ModifiedBy=SYSTEM_USER,
      _sys_ModifiedDt=GETDATE()
      FROM [dbo].[cfgFact] t
      JOIN inserted i ON (i.[FactPK]=t.[FactPK] or (i.[FactPK] is null and t.[FactPK] is null))
    END
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
 

CREATE TRIGGER [dbo].[td_cfgFact_SecurityAudit] ON [dbo].[cfgFact]
    AFTER DELETE
AS
    BEGIN
        SET NOCOUNT ON
        INSERT  INTO Fact_Hist
                ( Username,
                  FactValue ,
                  EventDate ,
                  [Action] ,
                  SettingKind,
                  ModuleFK ,
                  SettingFK ,
                  FactFK ,
                  RoleFK ,
                  PersonNameFK ,
                  BusinessUnitFK ,
                  PaperFK ,
                  UnderwritingTeamFK ,
                  ExposureFK ,
                  ExposureTypeMap ,
                  Dealnum ,
                  BankFK ,
                  LloydsRiskCodeFK ,
                  AccountingCurrencyTLAFK ,
                  YearOfAccount ,
                  AccountingReserveGroupFK ,
                  ActuarialReserveTypeFK ,
                  DealTypeFk ,
                  GLBusinessTypeFk ,
                  ModuleName ,   --
                  SettingName ,  --            
                  RoleName ,     --             
                  BusinessUnitName ,
                  PaperName ,
                  UnderwritingTeamName ,
                  ExposureName ,
                  PersonName ,
                  BankAccount ,
                  BankName ,
                  ToInceptDate ,
                  FromInceptDate ,
                  ActuarialReserveTypeName ,
                  GLBusinessTypeName ,
                  AccountingReserveGroupName ,
                  IsOutward,
                  InvoiceTeamFK,
                  BusinessTypeFK,
				  SourceTypeFK
                )
                SELECT DISTINCT
                suser_sname(),
                        CASE CS.Kind
                          WHEN 'Security' THEN CASE FactValue
                                                 WHEN '0' THEN 'NO'
                                                 WHEN '1' THEN 'YES'
                                                 ELSE 'NO'
                                               END
                          WHEN 'BasicWireSecurity' THEN CASE FactValue
                                                          WHEN '0' THEN 'NO'
                                                          WHEN '1' THEN 'YES'
                                                          ELSE 'NO'
                                                        END
                          WHEN 'BasicDealSecurity' THEN CASE FactValue
                                                          WHEN '0' THEN 'NO'
                                                          WHEN '1' THEN 'YES'
                                                          ELSE 'NO'
                                                        END
                          WHEN 'GrantContext' THEN CASE FactValue
                                                     WHEN '0' THEN 'NO'
                                                     WHEN '1' THEN 'YES'
                                                     ELSE 'NO'
                                                   END
						  WHEN 'InvoiceSecurity' THEN CASE FactValue
                                                     WHEN '0' THEN 'NO'
                                                     WHEN '1' THEN 'YES'
                                                     ELSE 'NO'
                                                   END                                                   
                          ELSE FactValue
                        END ,
                        GETDATE() ,
                        'DELETED' ,
                        CS.[Kind],
                        CM.ModulePK ,
                        CF.SettingFK ,
                        CF.FactPK ,
                        CF.RoleFK ,
                        CF.PersonFK ,
                        CF.BusinessUnitFK ,
                        CF.PaperFK ,
                        CF.UnderwritingTeamFK ,
                        CF.ExposureTypeFK ,
                        CF.ExposureTypeMap ,
                        CF.DealNumFK ,
                        CF.BankFK ,
                        CF.LloydsRiskCodeFK ,
                        CF.AccountingCurrencyTLAFK ,
                        CF.YearOfAccount ,
                        CF.AccountingReserveGroupFK ,
                        CF.ActuarialReserveTypeFK ,
                        CF.DealTypeFK ,
                        CF.GLBusinessTypeFK ,
                        CM.Name ,
                        CS.Name ,
                        ISNULL(CR.Name, '') ,
                        ISNULL(BU.name, '') ,
                        ISNULL(P.CompanyName, '') ,
                        ISNULL(UT.Name, '') ,
                        ISNULL(e.exposurename, '') ,
                        ISNULL(N.[FirstName], '') + ' ' + ISNULL(N.[LastName],
                                                              '') ,
                        ISNULL(CF.BankAccount, '') ,
                        ISNULL(b.BankName, '') ,
                        ToInceptDate ,
                        FromInceptDate ,
                        ISNULL(CASE ActuarialReserveTypeFK
                                 WHEN 1 THEN 'Property'
                                 WHEN 2 THEN 'Casualty'
                               END, '') AS ActuarialReserverTypeName ,
                        ISNULL(GL.Name, '') AS GLBusinessTypeName ,
                        ISNULL(ARG.Name, '') AS AccountingReserveGroupName ,
                        IsOutward,
                        CF.InvoiceTeamFK,
						CF.BusinessTypeFK,
					    CF.SourceTypeFK
                FROM    DELETED AS CF
                JOIN    cfgSetting AS CS
                ON      CF.SettingFK = CS.SettingPK
                JOIN    cfgModule AS CM
                ON      CS.ModuleFK = CM.ModulePK
                LEFT JOIN cfgRole AS CR
                ON      CF.RoleFK = CR.RolePK
                LEFT JOIN tb_Names AS N
                ON      CF.PersonFK = N.NameId
                LEFT JOIN tb_paper AS P
                ON      CF.PaperFK = P.papernum
                LEFT JOIN dmUnderwritingTeam AS UT
                ON      cf.UnderWritingTeamFK = ut.UnderwritingTeamPK
                LEFT JOIN [tb_catalogitems] AS BU
                ON      BU.[catid] = 34
                        AND BU.[code] = CF.BusinessUnitFK
                LEFT JOIN [tb_exposetype] AS E
                ON      E.ExposureType = CF.ExposureTypeFK
                LEFT JOIN [tb_banks] AS B
                ON      b.[banknum] = cf.bankFK
                LEFT JOIN [tb_catalogitems] AS GL
                ON      GL.[catid] = 93
                        AND GL.[code] = CF.GLBusinessTypeFK
                LEFT JOIN [tb_catalogitems] AS ARG
                ON      ARG.[catid] = 93
                        AND ARG.[code] = CF.AccountingReserveGroupFK
                        
    END

 

GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
	
CREATE TRIGGER [dbo].[ti_cfgFact_SecurityAudit] ON [dbo].[cfgFact]
    AFTER INSERT
AS
    BEGIN
        SET NOCOUNT ON
        INSERT  INTO Fact_Hist
                ( Username,
                  FactValue ,
                  EventDate ,
                  [Action] ,
                  SettingKind,
                  ModuleFK ,
                  SettingFK ,
                  FactFK ,
                  RoleFK ,
                  PersonNameFK ,
                  BusinessUnitFK ,
                  PaperFK ,
                  UnderwritingTeamFK ,
                  ExposureFK ,
                  ExposureTypeMap ,
                  Dealnum ,
                  BankFK ,
                  LloydsRiskCodeFK ,
                  AccountingCurrencyTLAFK ,
                  YearOfAccount ,
                  AccountingReserveGroupFK ,
                  ActuarialReserveTypeFK ,
                  DealTypeFk ,
                  GLBusinessTypeFk ,
                  ModuleName ,   --
                  SettingName ,  --            
                  RoleName ,     --             
                  BusinessUnitName ,
                  PaperName ,
                  UnderwritingTeamName ,
                  ExposureName ,
                  PersonName ,
                  BankAccount ,
                  BankName ,
                  ToInceptDate ,
                  FromInceptDate ,
                  ActuarialReserveTypeName ,
                  GLBusinessTypeName ,
                  AccountingReserveGroupName ,
                  IsOutward,
                  InvoiceTeamFK,
                  BusinessTypeFK,
				  SourceTypeFK
                )
                SELECT DISTINCT
                suser_sname(),
                        CASE CS.Kind
                          WHEN 'Security' THEN CASE FactValue
                                                 WHEN '0' THEN 'NO'
                                                 WHEN '1' THEN 'YES'
                                                 ELSE 'NO'
                                               END
                          WHEN 'BasicWireSecurity' THEN CASE FactValue
                                                          WHEN '0' THEN 'NO'
                                                          WHEN '1' THEN 'YES'
                                                          ELSE 'NO'
                                                        END
                          WHEN 'BasicDealSecurity' THEN CASE FactValue
                                                          WHEN '0' THEN 'NO'
                                                          WHEN '1' THEN 'YES'
                                                          ELSE 'NO'
                                                        END
                          WHEN 'GrantContext' THEN CASE FactValue
                                                     WHEN '0' THEN 'NO'
                                                     WHEN '1' THEN 'YES'
                                                     ELSE 'NO'
                                                   END
						  WHEN 'InvoiceSecurity' THEN CASE FactValue
                                                     WHEN '0' THEN 'NO'
                                                     WHEN '1' THEN 'YES'
                                                     ELSE 'NO'
                                                   END                                                   
                          ELSE FactValue
                        END ,
                        GETDATE() ,
                        'INSERTED' ,
                        CS.[Kind],
                        CM.ModulePK ,
                        CF.SettingFK ,
                        CF.FactPK ,
                        CF.RoleFK ,
                        CF.PersonFK ,
                        CF.BusinessUnitFK ,
                        CF.PaperFK ,
                        CF.UnderwritingTeamFK ,
                        CF.ExposureTypeFK ,
                        CF.ExposureTypeMap ,
                        CF.DealNumFK ,
                        CF.BankFK ,
                        CF.LloydsRiskCodeFK ,
                        CF.AccountingCurrencyTLAFK ,
                        CF.YearOfAccount ,
                        CF.AccountingReserveGroupFK ,
                        CF.ActuarialReserveTypeFK ,
                        CF.DealTypeFK ,
                        CF.GLBusinessTypeFK ,
                        CM.Name ,
                        CS.Name ,
                        ISNULL(CR.Name, '') ,
                        ISNULL(BU.name, '') ,
                        ISNULL(P.CompanyName, '') ,
                        ISNULL(UT.Name, '') ,
                        ISNULL(e.exposurename, '') ,
                        ISNULL(N.[FirstName], '') + ' ' + ISNULL(N.[LastName],
                                                              '') ,
                        ISNULL(CF.BankAccount, '') ,
                        ISNULL(b.BankName, '') ,
                        ToInceptDate ,
                        FromInceptDate ,
                        ISNULL(CASE ActuarialReserveTypeFK
                                 WHEN 1 THEN 'Property'
                                 WHEN 2 THEN 'Casualty'
                               END, '') AS ActuarialReserverTypeName ,
                        ISNULL(GL.Name, '') AS GLBusinessTypeName ,
                        ISNULL(ARG.Name, '') AS AccountingReserveGroupName ,
                        IsOutward,
                        CF.InvoiceTeamFK,
						CF.BusinessTypeFK,
						SourceTypeFK
                FROM    INSERTED AS CF
                JOIN    cfgSetting AS CS
                ON      CF.SettingFK = CS.SettingPK
                JOIN    cfgModule AS CM
                ON      CS.ModuleFK = CM.ModulePK
                LEFT JOIN cfgRole AS CR
                ON      CF.RoleFK = CR.RolePK
                LEFT JOIN tb_Names AS N
                ON      CF.PersonFK = N.NameId
                LEFT JOIN tb_paper AS P
                ON      CF.PaperFK = P.papernum
                LEFT JOIN dmUnderwritingTeam AS UT
                ON      cf.UnderWritingTeamFK = ut.UnderwritingTeamPK
                LEFT JOIN [tb_catalogitems] AS BU
                ON      BU.[catid] = 34
                        AND BU.[code] = CF.BusinessUnitFK
                LEFT JOIN [tb_exposetype] AS E
                ON      E.ExposureType = CF.ExposureTypeFK
                LEFT JOIN [tb_banks] AS B
                ON      b.[banknum] = cf.bankFK
                LEFT JOIN [tb_catalogitems] AS GL
                ON      GL.[catid] = 93
                        AND GL.[code] = CF.GLBusinessTypeFK
                LEFT JOIN [tb_catalogitems] AS ARG
                ON      ARG.[catid] = 93
                        AND ARG.[code] = CF.AccountingReserveGroupFK
                        
    END
 

 

GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE TRIGGER [dbo].[tr_d_AUDIT_cfgFact]
ON [dbo].[cfgFact]
FOR DELETE
NOT FOR REPLICATION
AS

-- "<TAG>SQLAUDIT GENERATED - DO NOT REMOVE</TAG>"

	/* 	------------------------------------------------------------
	   
	   	LEGAL:        	You may freely edit and modify this template and make copies of it.
	   
	   	DESCRIPTION:  	DELETE TRIGGER for Table:  [dbo].[cfgFact]
	   
	   	AUTHOR:       	ApexSQL Software
	
	   	DATE:			2/2/2010 2:56:28 PM
	   	------------------------------------------------------------ */

BEGIN
  IF DB_NAME() = dbo.fn_ProductionDbName()	-- Only audit the selected database; if moved elsewhere, skip auditing
  BEGIN
	DECLARE 
		@IDENTITY_SAVE				varchar(50),   /* used For preservation of @@IDENTITY  */
		@AUDIT_LOG_TRANSACTION_ID		Int,
		@PRIM_KEY				nvarchar(4000),
		@Table_Name	nvarchar(512),
		@Row_Count INT

	Set NOCOUNT On
	SET @Table_Name = '[dbo].[cfgFact]'
	SELECT @Row_Count = COUNT(*) FROM DELETED
	/*	Save @@IDENTITY For later restoration	*/
	Set @IDENTITY_SAVE = CAST(IsNull(@@IDENTITY,1) AS varchar(50))

	/*	INSERT audit transaction record For table [dbo].[cfgFact]	*/

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_TRANSACTIONS
	(
		TABLE_NAME,
		AUDIT_ACTION_ID,
		HOST_NAME,
		APP_NAME,
		MODIFIED_BY,
		MODIFIED_DATE,
		AFFECTED_ROWS
	)
	VALUES(
        @Table_Name,
		3,	--	ACTION ID For DELETE
		HOST_NAME(),
		APP_NAME(),
		SUSER_SNAME(),
		GETDATE(),
		@Row_Count
		)

	Set @AUDIT_LOG_TRANSACTION_ID = @@IDENTITY

	/*	INSERT audit traces For table [dbo].[cfgFact]	*/
	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'FactPK',
		CONVERT(nvarchar(4000),OLD.[FactPK])
	FROM deleted OLD
	WHERE
		OLD.[FactPK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'SettingFK',
		CONVERT(nvarchar(4000),OLD.[SettingFK])
	FROM deleted OLD
	WHERE
		OLD.[SettingFK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'BusinessUnitFK',
		CONVERT(nvarchar(4000),OLD.[BusinessUnitFK])
	FROM deleted OLD
	WHERE
		OLD.[BusinessUnitFK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'PaperFK',
		CONVERT(nvarchar(4000),OLD.[PaperFK])
	FROM deleted OLD
	WHERE
		OLD.[PaperFK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'PersonFK',
		CONVERT(nvarchar(4000),OLD.[PersonFK])
	FROM deleted OLD
	WHERE
		OLD.[PersonFK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'RoleFK',
		CONVERT(nvarchar(4000),OLD.[RoleFK])
	FROM deleted OLD
	WHERE
		OLD.[RoleFK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'UnderWritingTeamFK',
		CONVERT(nvarchar(4000),OLD.[UnderWritingTeamFK])
	FROM deleted OLD
	WHERE
		OLD.[UnderWritingTeamFK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'DealNumFK',
		CONVERT(nvarchar(4000),OLD.[DealNumFK])
	FROM deleted OLD
	WHERE
		OLD.[DealNumFK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'Sequence',
		CONVERT(nvarchar(4000),OLD.[Sequence])
	FROM deleted OLD
	WHERE
		OLD.[Sequence] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'FactValue',
		CONVERT(nvarchar(4000),OLD.[FactValue])
	FROM deleted OLD
	WHERE
		OLD.[FactValue] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'ExposureTypeFK',
		CONVERT(nvarchar(4000),OLD.[ExposureTypeFK])
	FROM deleted OLD
	WHERE
		OLD.[ExposureTypeFK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'ExposureTypeMap',
		CONVERT(nvarchar(4000),OLD.[ExposureTypeMap])
	FROM deleted OLD
	WHERE
		OLD.[ExposureTypeMap] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'AccountingReserveGroupFK',
		CONVERT(nvarchar(4000),OLD.[AccountingReserveGroupFK])
	FROM deleted OLD
	WHERE
		OLD.[AccountingReserveGroupFK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'IsOutward',
		CONVERT(nvarchar(4000),OLD.[IsOutward])
	FROM deleted OLD
	WHERE
		OLD.[IsOutward] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'DealTypeFk',
		CONVERT(nvarchar(4000),OLD.[DealTypeFk])
	FROM deleted OLD
	WHERE
		OLD.[DealTypeFk] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'GLBusinessTypeFk',
		CONVERT(nvarchar(4000),OLD.[GLBusinessTypeFk])
	FROM deleted OLD
	WHERE
		OLD.[GLBusinessTypeFk] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'ActuarialReserveTypeFK',
		CONVERT(nvarchar(4000),OLD.[ActuarialReserveTypeFK])
	FROM deleted OLD
	WHERE
		OLD.[ActuarialReserveTypeFK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'FromInceptDate',
		CONVERT(nvarchar(4000),OLD.[FromInceptDate])
	FROM deleted OLD
	WHERE
		OLD.[FromInceptDate] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'ToInceptDate',
		CONVERT(nvarchar(4000),OLD.[ToInceptDate])
	FROM deleted OLD
	WHERE
		OLD.[ToInceptDate] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'YearOfAccount',
		CONVERT(nvarchar(4000),OLD.[YearOfAccount])
	FROM deleted OLD
	WHERE
		OLD.[YearOfAccount] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'AccountingCurrencyTLAFK',
		CONVERT(nvarchar(4000),OLD.[AccountingCurrencyTLAFK])
	FROM deleted OLD
	WHERE
		OLD.[AccountingCurrencyTLAFK] Is Not Null

	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'LloydsRiskCodeFK',
		CONVERT(nvarchar(4000),OLD.[LloydsRiskCodeFK])
	FROM deleted OLD
	WHERE
		OLD.[LloydsRiskCodeFK] Is Not Null


	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'InvoiceTeamFK',
		CONVERT(nvarchar(4000),OLD.[InvoiceTeamFK])
	FROM deleted OLD
	WHERE
		OLD.[InvoiceTeamFK] Is Not Null

	
	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'BusinessTypeFK',
		CONVERT(nvarchar(4000),OLD.[BusinessTypeFK])
	FROM deleted OLD
	WHERE
		OLD.[BusinessTypeFK] Is Not Null		
	
	-- added for R37	
	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
	(
		AUDIT_LOG_TRANSACTION_ID,
		PRIMARY_KEY_DATA,
		COL_NAME,
		OLD_VALUE_LONG
	)
	SELECT
		@AUDIT_LOG_TRANSACTION_ID,
        'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
        'SourceTypeFK',
		CONVERT(nvarchar(4000),OLD.SourceTypeFK)
	FROM deleted OLD
	WHERE
		OLD.[SourceTypeFK] Is Not Null	


	/* Restore @@IDENTITY Value  */
        DECLARE @maxprec AS varchar(2)
        SET @maxprec=CAST(@@MAX_PRECISION as varchar(2))
        EXEC('SELECT IDENTITY(decimal('+@maxprec+',0),'+@IDENTITY_SAVE+',1) id INTO #tmp')
  END
END

GO
EXEC sp_settriggerorder N'[dbo].[tr_d_AUDIT_cfgFact]', 'last', 'delete', null
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE TRIGGER [dbo].[tr_u_AUDIT_cfgFact]
ON [dbo].[cfgFact]
FOR UPDATE
NOT FOR REPLICATION
As

-- "<TAG>SQLAUDIT GENERATED - DO NOT REMOVE</TAG>"

	/* 	------------------------------------------------------------
	   
	   	LEGAL:        	You may freely edit and modify this template and make copies of it.
	   
	   	DESCRIPTION:  	UPDATE TRIGGER for Table:  [dbo].[cfgFact]
	   
	   	AUTHOR:       	ApexSQL Software
	
	   	DATE:			2/2/2010 2:56:28 PM
	   	------------------------------------------------------------ */

BEGIN
  IF DB_NAME() = dbo.fn_ProductionDbName()	-- Only audit the selected database; if moved elsewhere, skip auditing
  BEGIN
	DECLARE 
		@IDENTITY_SAVE			varchar(50),   /* used For preservation of @@IDENTITY  */
		@AUDIT_LOG_TRANSACTION_ID	Int,
		@PRIM_KEY			nvarchar(4000),
		@Inserted	    		bit,
		@Table_Name	nvarchar(512),
		@Row_Count INT

	SET NOCOUNT On
	SET @Table_Name = '[dbo].[cfgFact]'
	SELECT @Row_Count = COUNT(*) FROM INSERTED
	/*	Save @@IDENTITY For later restoration	*/
	SET @IDENTITY_SAVE = CAST(IsNull(@@IDENTITY,1) AS varchar(50))

	/*	INSERT audit transaction record For table [dbo].[cfgFact]	*/
	INSERT
	INTO MaxReAUDIT.dbo.AUDIT_LOG_TRANSACTIONS
	(
		TABLE_NAME,
		AUDIT_ACTION_ID,
		HOST_NAME,
		APP_NAME,
		MODIFIED_BY,
		MODIFIED_DATE,
		AFFECTED_ROWS
	)
	VALUES(
		@Table_Name,
		1,	--	ACTION ID For UPDATE
		HOST_NAME(),
		APP_NAME(),
		SUSER_SNAME(),
		GETDATE(),
		@Row_Count
		)

	SET @AUDIT_LOG_TRANSACTION_ID = @@IDENTITY

	SET @Inserted = 0


	/*	INSERT audit traces For table [dbo].[cfgFact]	*/
	/*	INSERT audit traces For [SettingFK]	 column	*/
	If UPDATE([SettingFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'SettingFK',
			CONVERT(nvarchar(4000),OLD.[SettingFK]),
			CONVERT(nvarchar(4000),NEW.[SettingFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[SettingFK] Is Null And
					OLD.[SettingFK] Is Not Null
				) Or
				(
					NEW.[SettingFK] Is Not Null And
					OLD.[SettingFK] Is Null
				) Or
				(
					NEW.[SettingFK] !=
					OLD.[SettingFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [BusinessUnitFK]	 column	*/
	If UPDATE([BusinessUnitFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'BusinessUnitFK',
			CONVERT(nvarchar(4000),OLD.[BusinessUnitFK]),
			CONVERT(nvarchar(4000),NEW.[BusinessUnitFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[BusinessUnitFK] Is Null And
					OLD.[BusinessUnitFK] Is Not Null
				) Or
				(
					NEW.[BusinessUnitFK] Is Not Null And
					OLD.[BusinessUnitFK] Is Null
				) Or
				(
					NEW.[BusinessUnitFK] !=
					OLD.[BusinessUnitFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [PaperFK]	 column	*/
	If UPDATE([PaperFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'PaperFK',
			CONVERT(nvarchar(4000),OLD.[PaperFK]),
			CONVERT(nvarchar(4000),NEW.[PaperFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[PaperFK] Is Null And
					OLD.[PaperFK] Is Not Null
				) Or
				(
					NEW.[PaperFK] Is Not Null And
					OLD.[PaperFK] Is Null
				) Or
				(
					NEW.[PaperFK] !=
					OLD.[PaperFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [PersonFK]	 column	*/
	If UPDATE([PersonFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'PersonFK',
			CONVERT(nvarchar(4000),OLD.[PersonFK]),
			CONVERT(nvarchar(4000),NEW.[PersonFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[PersonFK] Is Null And
					OLD.[PersonFK] Is Not Null
				) Or
				(
					NEW.[PersonFK] Is Not Null And
					OLD.[PersonFK] Is Null
				) Or
				(
					NEW.[PersonFK] !=
					OLD.[PersonFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [RoleFK]	 column	*/
	If UPDATE([RoleFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'RoleFK',
			CONVERT(nvarchar(4000),OLD.[RoleFK]),
			CONVERT(nvarchar(4000),NEW.[RoleFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[RoleFK] Is Null And
					OLD.[RoleFK] Is Not Null
				) Or
				(
					NEW.[RoleFK] Is Not Null And
					OLD.[RoleFK] Is Null
				) Or
				(
					NEW.[RoleFK] !=
					OLD.[RoleFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [UnderWritingTeamFK]	 column	*/
	If UPDATE([UnderWritingTeamFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'UnderWritingTeamFK',
			CONVERT(nvarchar(4000),OLD.[UnderWritingTeamFK]),
			CONVERT(nvarchar(4000),NEW.[UnderWritingTeamFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[UnderWritingTeamFK] Is Null And
					OLD.[UnderWritingTeamFK] Is Not Null
				) Or
				(
					NEW.[UnderWritingTeamFK] Is Not Null And
					OLD.[UnderWritingTeamFK] Is Null
				) Or
				(
					NEW.[UnderWritingTeamFK] !=
					OLD.[UnderWritingTeamFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [DealNumFK]	 column	*/
	If UPDATE([DealNumFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'DealNumFK',
			CONVERT(nvarchar(4000),OLD.[DealNumFK]),
			CONVERT(nvarchar(4000),NEW.[DealNumFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[DealNumFK] Is Null And
					OLD.[DealNumFK] Is Not Null
				) Or
				(
					NEW.[DealNumFK] Is Not Null And
					OLD.[DealNumFK] Is Null
				) Or
				(
					NEW.[DealNumFK] !=
					OLD.[DealNumFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [Sequence]	 column	*/
	If UPDATE([Sequence])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'Sequence',
			CONVERT(nvarchar(4000),OLD.[Sequence]),
			CONVERT(nvarchar(4000),NEW.[Sequence])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[Sequence] Is Null And
					OLD.[Sequence] Is Not Null
				) Or
				(
					NEW.[Sequence] Is Not Null And
					OLD.[Sequence] Is Null
				) Or
				(
					NEW.[Sequence] !=
					OLD.[Sequence]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [FactValue]	 column	*/
	If UPDATE([FactValue])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'FactValue',
			CONVERT(nvarchar(4000),OLD.[FactValue]),
			CONVERT(nvarchar(4000),NEW.[FactValue])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[FactValue] Is Null And
					OLD.[FactValue] Is Not Null
				) Or
				(
					NEW.[FactValue] Is Not Null And
					OLD.[FactValue] Is Null
				) Or
				(
					NEW.[FactValue] !=
					OLD.[FactValue]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [ExposureTypeFK]	 column	*/
	If UPDATE([ExposureTypeFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'ExposureTypeFK',
			CONVERT(nvarchar(4000),OLD.[ExposureTypeFK]),
			CONVERT(nvarchar(4000),NEW.[ExposureTypeFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[ExposureTypeFK] Is Null And
					OLD.[ExposureTypeFK] Is Not Null
				) Or
				(
					NEW.[ExposureTypeFK] Is Not Null And
					OLD.[ExposureTypeFK] Is Null
				) Or
				(
					NEW.[ExposureTypeFK] !=
					OLD.[ExposureTypeFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [ExposureTypeMap]	 column	*/
	If UPDATE([ExposureTypeMap])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'ExposureTypeMap',
			CONVERT(nvarchar(4000),OLD.[ExposureTypeMap]),
			CONVERT(nvarchar(4000),NEW.[ExposureTypeMap])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[ExposureTypeMap] Is Null And
					OLD.[ExposureTypeMap] Is Not Null
				) Or
				(
					NEW.[ExposureTypeMap] Is Not Null And
					OLD.[ExposureTypeMap] Is Null
				) Or
				(
					NEW.[ExposureTypeMap] !=
					OLD.[ExposureTypeMap]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [AccountingReserveGroupFK]	 column	*/
	If UPDATE([AccountingReserveGroupFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'AccountingReserveGroupFK',
			CONVERT(nvarchar(4000),OLD.[AccountingReserveGroupFK]),
			CONVERT(nvarchar(4000),NEW.[AccountingReserveGroupFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[AccountingReserveGroupFK] Is Null And
					OLD.[AccountingReserveGroupFK] Is Not Null
				) Or
				(
					NEW.[AccountingReserveGroupFK] Is Not Null And
					OLD.[AccountingReserveGroupFK] Is Null
				) Or
				(
					NEW.[AccountingReserveGroupFK] !=
					OLD.[AccountingReserveGroupFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [IsOutward]	 column	*/
	If UPDATE([IsOutward])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'IsOutward',
			CONVERT(nvarchar(4000),OLD.[IsOutward]),
			CONVERT(nvarchar(4000),NEW.[IsOutward])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[IsOutward] Is Null And
					OLD.[IsOutward] Is Not Null
				) Or
				(
					NEW.[IsOutward] Is Not Null And
					OLD.[IsOutward] Is Null
				) Or
				(
					NEW.[IsOutward] !=
					OLD.[IsOutward]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [DealTypeFk]	 column	*/
	If UPDATE([DealTypeFk])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'DealTypeFk',
			CONVERT(nvarchar(4000),OLD.[DealTypeFk]),
			CONVERT(nvarchar(4000),NEW.[DealTypeFk])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[DealTypeFk] Is Null And
					OLD.[DealTypeFk] Is Not Null
				) Or
				(
					NEW.[DealTypeFk] Is Not Null And
					OLD.[DealTypeFk] Is Null
				) Or
				(
					NEW.[DealTypeFk] !=
					OLD.[DealTypeFk]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [GLBusinessTypeFk]	 column	*/
	If UPDATE([GLBusinessTypeFk])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'GLBusinessTypeFk',
			CONVERT(nvarchar(4000),OLD.[GLBusinessTypeFk]),
			CONVERT(nvarchar(4000),NEW.[GLBusinessTypeFk])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[GLBusinessTypeFk] Is Null And
					OLD.[GLBusinessTypeFk] Is Not Null
				) Or
				(
					NEW.[GLBusinessTypeFk] Is Not Null And
					OLD.[GLBusinessTypeFk] Is Null
				) Or
				(
					NEW.[GLBusinessTypeFk] !=
					OLD.[GLBusinessTypeFk]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [ActuarialReserveTypeFK]	 column	*/
	If UPDATE([ActuarialReserveTypeFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'ActuarialReserveTypeFK',
			CONVERT(nvarchar(4000),OLD.[ActuarialReserveTypeFK]),
			CONVERT(nvarchar(4000),NEW.[ActuarialReserveTypeFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[ActuarialReserveTypeFK] Is Null And
					OLD.[ActuarialReserveTypeFK] Is Not Null
				) Or
				(
					NEW.[ActuarialReserveTypeFK] Is Not Null And
					OLD.[ActuarialReserveTypeFK] Is Null
				) Or
				(
					NEW.[ActuarialReserveTypeFK] !=
					OLD.[ActuarialReserveTypeFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [FromInceptDate]	 column	*/
	If UPDATE([FromInceptDate])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'FromInceptDate',
			CONVERT(nvarchar(4000),OLD.[FromInceptDate]),
			CONVERT(nvarchar(4000),NEW.[FromInceptDate])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[FromInceptDate] Is Null And
					OLD.[FromInceptDate] Is Not Null
				) Or
				(
					NEW.[FromInceptDate] Is Not Null And
					OLD.[FromInceptDate] Is Null
				) Or
				(
					NEW.[FromInceptDate] !=
					OLD.[FromInceptDate]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [ToInceptDate]	 column	*/
	If UPDATE([ToInceptDate])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'ToInceptDate',
			CONVERT(nvarchar(4000),OLD.[ToInceptDate]),
			CONVERT(nvarchar(4000),NEW.[ToInceptDate])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[ToInceptDate] Is Null And
					OLD.[ToInceptDate] Is Not Null
				) Or
				(
					NEW.[ToInceptDate] Is Not Null And
					OLD.[ToInceptDate] Is Null
				) Or
				(
					NEW.[ToInceptDate] !=
					OLD.[ToInceptDate]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [YearOfAccount]	 column	*/
	If UPDATE([YearOfAccount])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'YearOfAccount',
			CONVERT(nvarchar(4000),OLD.[YearOfAccount]),
			CONVERT(nvarchar(4000),NEW.[YearOfAccount])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[YearOfAccount] Is Null And
					OLD.[YearOfAccount] Is Not Null
				) Or
				(
					NEW.[YearOfAccount] Is Not Null And
					OLD.[YearOfAccount] Is Null
				) Or
				(
					NEW.[YearOfAccount] !=
					OLD.[YearOfAccount]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [AccountingCurrencyTLAFK]	 column	*/
	If UPDATE([AccountingCurrencyTLAFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'AccountingCurrencyTLAFK',
			CONVERT(nvarchar(4000),OLD.[AccountingCurrencyTLAFK]),
			CONVERT(nvarchar(4000),NEW.[AccountingCurrencyTLAFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[AccountingCurrencyTLAFK] Is Null And
					OLD.[AccountingCurrencyTLAFK] Is Not Null
				) Or
				(
					NEW.[AccountingCurrencyTLAFK] Is Not Null And
					OLD.[AccountingCurrencyTLAFK] Is Null
				) Or
				(
					NEW.[AccountingCurrencyTLAFK] !=
					OLD.[AccountingCurrencyTLAFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END

	/*	INSERT audit traces For [LloydsRiskCodeFK]	 column	*/
	If UPDATE([LloydsRiskCodeFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'LloydsRiskCodeFK',
			CONVERT(nvarchar(4000),OLD.[LloydsRiskCodeFK]),
			CONVERT(nvarchar(4000),NEW.[LloydsRiskCodeFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[LloydsRiskCodeFK] Is Null And
					OLD.[LloydsRiskCodeFK] Is Not Null
				) Or
				(
					NEW.[LloydsRiskCodeFK] Is Not Null And
					OLD.[LloydsRiskCodeFK] Is Null
				) Or
				(
					NEW.[LloydsRiskCodeFK] !=
					OLD.[LloydsRiskCodeFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END
	
	/*	INSERT audit traces For [InvoiceTeamFK]	 column	*/
	If UPDATE([InvoiceTeamFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'InvoiceTeamFK',
			CONVERT(nvarchar(4000),OLD.[InvoiceTeamFK]),
			CONVERT(nvarchar(4000),NEW.[InvoiceTeamFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[InvoiceTeamFK] Is Null And
					OLD.[InvoiceTeamFK] Is Not Null
				) Or
				(
					NEW.[InvoiceTeamFK] Is Not Null And
					OLD.[InvoiceTeamFK] Is Null
				) Or
				(
					NEW.[InvoiceTeamFK] !=
					OLD.[InvoiceTeamFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END
	
	/*	INSERT audit traces For [BusinessTypeFK]	 column	*/
	If UPDATE([BusinessTypeFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'BusinessTypeFK',
			CONVERT(nvarchar(4000),OLD.[BusinessTypeFK]),
			CONVERT(nvarchar(4000),NEW.[BusinessTypeFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[BusinessTypeFK] Is Null And
					OLD.[BusinessTypeFK] Is Not Null
				) Or
				(
					NEW.[BusinessTypeFK] Is Not Null And
					OLD.[BusinessTypeFK] Is Null
				) Or
				(
					NEW.[BusinessTypeFK] !=
					OLD.[BusinessTypeFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END
	
	/*	INSERT audit traces For [SourceTypeFK]	 column	*/
	If UPDATE([SourceTypeFK])
	BEGIN
		INSERT
		INTO MaxReAUDIT.dbo.AUDIT_LOG_DATA
		(
			AUDIT_LOG_TRANSACTION_ID,
			PRIMARY_KEY_DATA,
			COL_NAME,
			OLD_VALUE_LONG,
			NEW_VALUE_LONG
		)
		SELECT
			@AUDIT_LOG_TRANSACTION_ID,
            'FactPK=N'''+CAST(OLD.[FactPK] as nvarchar(4000))+'''',
                'BusinessTypeFK',
			CONVERT(nvarchar(4000),OLD.[SourceTypeFK]),
			CONVERT(nvarchar(4000),NEW.[SourceTypeFK])
		FROM deleted OLD INNER Join inserted NEW On
            NEW.[FactPK] = OLD.[FactPK] AND
			(
				(
					NEW.[SourceTypeFK] Is Null And
					OLD.[SourceTypeFK] Is Not Null
				) Or
				(
					NEW.[SourceTypeFK] Is Not Null And
					OLD.[SourceTypeFK] Is Null
				) Or
				(
					NEW.[SourceTypeFK] !=
					OLD.[SourceTypeFK]
				)
			)
		SET @Inserted = CASE WHEN @@ROWCOUNT > 0 Then 1 Else @Inserted End
	END
	

	IF @Inserted = 0
	BEGIN
		DELETE FROM MaxReAUDIT.dbo.AUDIT_LOG_TRANSACTIONS WHERE AUDIT_LOG_TRANSACTION_ID = @AUDIT_LOG_TRANSACTION_ID
	END
	/* Restore @@IDENTITY Value  */
        DECLARE @maxprec AS varchar(2)
        SET @maxprec=CAST(@@MAX_PRECISION as varchar(2))
        EXEC('SELECT IDENTITY(decimal('+@maxprec+',0),'+@IDENTITY_SAVE+',1) id INTO #tmp')
  End
End

GO
EXEC sp_settriggerorder N'[dbo].[tr_u_AUDIT_cfgFact]', 'last', 'update', null
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
 

CREATE TRIGGER [dbo].[tu_cfgFact_SecurityAudit] ON [dbo].[cfgFact]
    AFTER UPDATE
AS
    BEGIN
        SET NOCOUNT ON
        INSERT  INTO Fact_Hist
                ( Username,
                  FactValue ,
                  EventDate ,
                  [Action] ,
                  SettingKind,
                  ModuleFK ,
                  SettingFK ,
                  FactFK ,
                  RoleFK ,
                  PersonNameFK ,
                  BusinessUnitFK ,
                  PaperFK ,
                  UnderwritingTeamFK ,
                  ExposureFK ,
                  ExposureTypeMap ,
                  Dealnum ,
                  BankFK ,
                  LloydsRiskCodeFK ,
                  AccountingCurrencyTLAFK ,
                  YearOfAccount ,
                  AccountingReserveGroupFK ,
                  ActuarialReserveTypeFK ,
                  DealTypeFk ,
                  GLBusinessTypeFk ,
                  ModuleName ,   --
                  SettingName ,  --            
                  RoleName ,     --             
                  BusinessUnitName ,
                  PaperName ,
                  UnderwritingTeamName ,
                  ExposureName ,
                  PersonName ,
                  BankAccount ,
                  BankName ,
                  ToInceptDate ,
                  FromInceptDate ,
                  ActuarialReserveTypeName ,
                  GLBusinessTypeName ,
                  AccountingReserveGroupName ,
                  IsOutward,
                  InvoiceTeamFK,
				  BusinessTypeFK,
				  SourceTypeFK
                )
                
                SELECT DISTINCT
                suser_sname(),
                        CASE CS.Kind
                          WHEN 'Security' THEN CASE FactValue
                                                 WHEN '0' THEN 'NO'
                                                 WHEN '1' THEN 'YES'
                                                 ELSE 'NO'
                                               END
                          WHEN 'BasicWireSecurity' THEN CASE FactValue
                                                          WHEN '0' THEN 'NO'
                                                          WHEN '1' THEN 'YES'
                                                          ELSE 'NO'
                                                        END
                          WHEN 'BasicDealSecurity' THEN CASE FactValue
                                                          WHEN '0' THEN 'NO'
                                                          WHEN '1' THEN 'YES'
                                                          ELSE 'NO'
                                                        END
                          WHEN 'GrantContext' THEN CASE FactValue
                                                     WHEN '0' THEN 'NO'
                                                     WHEN '1' THEN 'YES'
                                                     ELSE 'NO'
                                                   END
						  WHEN 'InvoiceSecurity' THEN CASE FactValue
													 WHEN '0' THEN 'NO'
													 WHEN '1' THEN 'YES'
													 ELSE 'NO'
												   END                                                   
                          ELSE FactValue
                        END ,
                        GETDATE() ,
                        'UPDATED' ,
                        CS.[Kind],
                        CM.ModulePK ,
                        CF.SettingFK ,
                        CF.FactPK ,
                        CF.RoleFK ,
                        CF.PersonFK ,
                        CF.BusinessUnitFK ,
                        CF.PaperFK ,
                        CF.UnderwritingTeamFK ,
                        CF.ExposureTypeFK ,
                        CF.ExposureTypeMap ,
                        CF.DealNumFK ,
                        CF.BankFK ,
                        CF.LloydsRiskCodeFK ,
                        CF.AccountingCurrencyTLAFK ,
                        CF.YearOfAccount ,
                        CF.AccountingReserveGroupFK ,
                        CF.ActuarialReserveTypeFK ,
                        CF.DealTypeFK ,
                        CF.GLBusinessTypeFK ,
                        CM.Name ,
                        CS.Name ,
                        ISNULL(CR.Name, '') ,
                        ISNULL(BU.name, '') ,
                        ISNULL(P.CompanyName, '') ,
                        ISNULL(UT.Name, '') ,
                        ISNULL(e.exposurename, '') ,
                        ISNULL(N.[FirstName], '') + ' ' + ISNULL(N.[LastName],
                                                              '') ,
                        ISNULL(CF.BankAccount, '') ,
                        ISNULL(b.BankName, '') ,
                        ToInceptDate ,
                        FromInceptDate ,
                        ISNULL(CASE ActuarialReserveTypeFK
                                 WHEN 1 THEN 'Property'
                                 WHEN 2 THEN 'Casualty'
                               END, '') AS ActuarialReserverTypeName ,
                        ISNULL(GL.Name, '') AS GLBusinessTypeName ,
                        ISNULL(ARG.Name, '') AS AccountingReserveGroupName ,
                        IsOutward,
                        CF.InvoiceTeamFK,
						CF.BusinessTypeFK,
					    CF.SourceTypeFK
                FROM    INSERTED AS CF
                JOIN    cfgSetting AS CS
                ON      CF.SettingFK = CS.SettingPK
                JOIN    cfgModule AS CM
                ON      CS.ModuleFK = CM.ModulePK
                LEFT JOIN cfgRole AS CR
                ON      CF.RoleFK = CR.RolePK
                LEFT JOIN tb_Names AS N
                ON      CF.PersonFK = N.NameId
                LEFT JOIN tb_paper AS P
                ON      CF.PaperFK = P.papernum
                LEFT JOIN dmUnderwritingTeam AS UT
                ON      cf.UnderWritingTeamFK = ut.UnderwritingTeamPK
                LEFT JOIN [tb_catalogitems] AS BU
                ON      BU.[catid] = 34
                        AND BU.[code] = CF.BusinessUnitFK
                LEFT JOIN [tb_exposetype] AS E
                ON      E.ExposureType = CF.ExposureTypeFK
                LEFT JOIN [tb_banks] AS B
                ON      b.[banknum] = cf.bankFK
                LEFT JOIN [tb_catalogitems] AS GL
                ON      GL.[catid] = 93
                        AND GL.[code] = CF.GLBusinessTypeFK
                LEFT JOIN [tb_catalogitems] AS ARG
                ON      ARG.[catid] = 93
                        AND ARG.[code] = CF.AccountingReserveGroupFK
                        
    END

 

GO
ALTER TABLE [dbo].[cfgFact] ADD CONSTRAINT [CK_cfgFact_Valid_BankFK_BankAccount] CHECK (([BankAccount] IS NULL OR [BankFK] IS NOT NULL))
GO
ALTER TABLE [dbo].[cfgFact] ADD CONSTRAINT [PK_configManagementFact] PRIMARY KEY NONCLUSTERED  ([FactPK])
GO
CREATE CLUSTERED INDEX [IX_cfgFact_SettingFK_RoleFK_PersonFK] ON [dbo].[cfgFact] ([SettingFK], [RoleFK], [PersonFK])
GO
CREATE NONCLUSTERED INDEX [IX_cfgFact_FK] ON [dbo].[cfgFact] 
(
	[BusinessUnitFK] ASC,
	[PaperFK] ASC,
	[UnderWritingTeamFK] ASC,
	[DealNumFK] ASC
)
INCLUDE ( [SettingFK],
[PersonFK],
[RoleFK],
[FactValue],
[ExposureTypeFK])
GO


ALTER TABLE [dbo].[cfgFact] ADD CONSTRAINT [FK_cfgFact_tb_Currency] FOREIGN KEY ([AccountingCurrencyTLAFK]) REFERENCES [dbo].[tb_currency] ([currcode])
GO
ALTER TABLE [dbo].[cfgFact] ADD CONSTRAINT [FK_cfgFact_tb_banks] FOREIGN KEY ([BankFK]) REFERENCES [dbo].[tb_banks] ([banknum])
GO
ALTER TABLE [dbo].[cfgFact] ADD CONSTRAINT [FK_cfgFact_tb_bankacct] FOREIGN KEY ([BankFK], [BankAccount]) REFERENCES [dbo].[tbl_bankacct] ([banknum], [acctnum]) ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[cfgFact] WITH NOCHECK ADD CONSTRAINT [FK_CMFact_tbl_deals] FOREIGN KEY ([DealNumFK]) REFERENCES [dbo].[tbl_deals] ([dealnum])
GO
ALTER TABLE [dbo].[cfgFact] WITH NOCHECK ADD CONSTRAINT [FK_CMFact_tb_paper] FOREIGN KEY ([PaperFK]) REFERENCES [dbo].[tb_paper] ([papernum])
GO
ALTER TABLE [dbo].[cfgFact] WITH NOCHECK ADD CONSTRAINT [FK_configManagementFact_configManagementSetting] FOREIGN KEY ([PersonFK]) REFERENCES [dbo].[tb_Names] ([NameId])
GO
ALTER TABLE [dbo].[cfgFact] ADD CONSTRAINT [FK_cfgFact_cfgRole] FOREIGN KEY ([RoleFK]) REFERENCES [dbo].[cfgRole] ([RolePK])
GO
ALTER TABLE [dbo].[cfgFact] ADD CONSTRAINT [FK_CMFact_Setting] FOREIGN KEY ([SettingFK]) REFERENCES [dbo].[cfgSetting] ([SettingPK]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[cfgFact] WITH NOCHECK ADD CONSTRAINT [FK_CMFact_dmUnderwritingTeam] FOREIGN KEY ([UnderWritingTeamFK]) REFERENCES [dbo].[dmUnderwritingTeam] ([UnderwritingTeamPK])
GO
GRANT SELECT ON  [dbo].[cfgFact] TO [ErmsUsers]
GRANT INSERT ON  [dbo].[cfgFact] TO [ErmsUsers]
GRANT DELETE ON  [dbo].[cfgFact] TO [ErmsUsers]
GRANT UPDATE ON  [dbo].[cfgFact] TO [ErmsUsers]
GO
EXEC sp_addextendedproperty N'MS_Description', N'Catalog Item: 34', 'SCHEMA', N'dbo', 'TABLE', N'cfgFact', 'COLUMN', N'BusinessUnitFK'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Deak FK to Deals table', 'SCHEMA', N'dbo', 'TABLE', N'cfgFact', 'COLUMN', N'DealNumFK'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The value of the setting. Use the Setting parent to determine type', 'SCHEMA', N'dbo', 'TABLE', N'cfgFact', 'COLUMN', N'FactValue'
GO
EXEC sp_addextendedproperty N'MS_Description', N'PapER FK to Paper table', 'SCHEMA', N'dbo', 'TABLE', N'cfgFact', 'COLUMN', N'PaperFK'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Person FK to person table', 'SCHEMA', N'dbo', 'TABLE', N'cfgFact', 'COLUMN', N'PersonFK'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Role FK to role table', 'SCHEMA', N'dbo', 'TABLE', N'cfgFact', 'COLUMN', N'RoleFK'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The order in which to look for a value when many rows found', 'SCHEMA', N'dbo', 'TABLE', N'cfgFact', 'COLUMN', N'Sequence'
GO
EXEC sp_addextendedproperty N'MS_Description', N'FK to the Setting table', 'SCHEMA', N'dbo', 'TABLE', N'cfgFact', 'COLUMN', N'SettingFK'
GO
