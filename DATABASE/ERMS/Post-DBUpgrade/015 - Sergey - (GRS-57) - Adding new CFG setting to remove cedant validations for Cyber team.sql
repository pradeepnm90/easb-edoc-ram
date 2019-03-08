BEGIN TRANSACTION

DECLARE @Cyberteam INT = 117; -- Cyber – Public Entity
DECLARE @SettingName varchar(100) = 'Bypass Reinsurance Cedant Group Type Validation'
DECLARE @desc varchar(100) = 'Allow teams to bypass the cedant group type validation'
DECLARE @ModuleName varchar(50) = 'Underwriting'
DECLARE @DotNetType varchar(50) = 'System.String'
DECLARE @Kind varchar(50) = 'Deal'

EXEC dbo.pr_cfgAddSetting @ModuleName = @ModuleName,
     @settingname = @SettingName, @Description = @desc,
     @DotNetType = @DotNetType, @Kind = @Kind


EXEC dbo.pr_cfgAddFact @SettingName = @SettingName, @underwritingteamfk = @Cyberteam ,@FactValue = '1',@Sequence = 10;

COMMIT