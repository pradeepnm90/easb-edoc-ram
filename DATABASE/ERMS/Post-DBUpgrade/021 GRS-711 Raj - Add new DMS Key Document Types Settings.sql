BEGIN TRANSACTION

DECLARE @settingpk INT = 0
DECLARE @doctypessettingname varchar(100) = 'GlobalRe DocumentTypes'

IF NOT EXISTS (SELECT * FROM dbo.cfgSetting WHERE Name = @doctypessettingname)
BEGIN
EXEC pr_cfgAddSetting	@ModuleName = 'GlobalRe'
						,@SettingName = @doctypessettingname
						,@Description = 'GlobalRe DocumentTypes for GRS Deals'
						,@DotNetType = 'System.String'
						,@Kind = 'DealExt'
END

-- Getting newly added setting key 
SELECT @settingpk = SettingPK FROM dbo.cfgSetting WHERE Name = @doctypessettingname

-- Adding fact settings 
IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Contract - Final RI' AND SettingFK = @settingpk)
BEGIN
	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Contract - Final RI', @Sequence = 1
END


IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Contract - Add/End RI' AND SettingFK = @settingpk)
BEGIN
	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Contract - Add/End RI', @Sequence = 2
END


IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Submission RI' AND SettingFK = @settingpk)
BEGIN
	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Submission RI', @Sequence = 3
END


IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Legal Review RI' AND SettingFK = @settingpk)
BEGIN
	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Legal Review RI', @Sequence = 4
END


IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Quotes/Indications RI' AND SettingFK = @settingpk)
BEGIN
	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Quotes/Indications RI', @Sequence = 5
END


IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Authorizations RI' AND SettingFK = @settingpk)
BEGIN
	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Authorizations RI', @Sequence = 6
END


IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Signed Lines Confirmation RI' AND SettingFK = @settingpk)
BEGIN
	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Signed Lines Confirmation RI', @Sequence = 7
END


IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Underwriting Memo RI' AND SettingFK = @settingpk)
BEGIN
	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Underwriting Memo RI', @Sequence = 8
END


IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Pricing RI' AND SettingFK = @settingpk)
BEGIN
	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Pricing RI', @Sequence = 9
END


--IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Submission RI' AND SettingFK = @settingpk)
--BEGIN
--	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Submission RI', @UnderWritingTeamFK = 175, @Sequence = 10
--END


IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Broker Certificate RI' AND SettingFK = @settingpk)
BEGIN
	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Broker Certificate RI', @UnderWritingTeamFK = 175, @Sequence = 11
END


IF NOT EXISTS (SELECT TOP 1	* FROM cfgFact WHERE FactValue = 'Markel Certificate RI' AND SettingFK = @settingpk)
BEGIN
	EXEC pr_cfgAddFact	@SettingName = @doctypessettingname, @FactValue = 'Markel Certificate RI', @UnderWritingTeamFK = 175, @Sequence = 12
END


COMMIT
--ROLLBACK TRANSACTION
