DROP PROCEDURE grs.EvaluteConfigSetting
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**
Evaluates Configiration Manager seeting
 */
CREATE PROCEDURE grs.EvaluteConfigSetting
	@dealNumber INT,
    @settingname NVARCHAR(255),
	@result NVARCHAR(255) OUTPUT
AS
BEGIN
 
	DECLARE @params XML = dbo.fn_GetDealParametersXml(@dealNumber);
	DECLARE @cfgResult NVARCHAR(255);
	DECLARE @factPK INT ;

	EXEC dbo.pr_cfgEvaluateSetting @settingname, @params, @cfgResult OUTPUT, @factPK OUTPUT, 0;			
	SET @result = @cfgResult;

	--RETURN @result;
END

GO

GRANT EXECUTE ON grs.EvaluteConfigSetting TO [ErmsUsers] AS [dbo]

GO 

