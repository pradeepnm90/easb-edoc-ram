BEGIN TRANSACTION

DECLARE @settingName varchar(100) = 'GlobalRe Deal Decline Statuses';
EXEC pr_cfgAddSetting	@ModuleName = 'GlobalRe'
						,@SettingName = @settingName
						,@Description = 'Stores Deal Decline Statuses ID from Catalogitem 7(commasperated)'
						,@DotNetType = 'System.String'
						,@Kind = 'Deal'

DECLARE @settingfk int;
SELECT @settingfk = cs.SettingPK FROM dbo.cfgSetting cs WHERE cs.Name = @settingName;

EXEC pr_cfgAddFact	@SettingName = @settingName, @FactValue = '6,65,66,67', @Sequence = 10;

--SELECT * FROM dbo.cfgSetting cs
--JOIN dbo.cfgFact cf ON cs.SettingPK = cf.SettingFK WHERE cs.Name = @settingName

COMMIT;