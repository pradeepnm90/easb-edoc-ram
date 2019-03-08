BEGIN TRANSACTION 

DECLARE @ModuleName VARCHAR(100) = 'GlobalRe',
		@ModulBusinessRules VARCHAR(100) = 'Business Rules',
		@ModuleDefaults VARCHAR(100) = 'Defaults',
		@ModulePK INT;

  IF NOT EXISTS (SELECT * FROM dbo.cfgModule WHERE Name = @ModuleName) 
  BEGIN
    INSERT INTO dbo.cfgModule
            ( Name 
            )
    VALUES  ( @ModuleName)
  END ;

  SET @ModulePK = ( SELECT ModulePK FROM dbo.cfgModule WHERE name = @ModuleName );

  IF NOT EXISTS (SELECT * FROM dbo.cfgModule WHERE Name = @ModulBusinessRules) 
  BEGIN
    INSERT INTO dbo.cfgModule
            ( Name, ParentModuleFK 
            )
    VALUES  ( @ModulBusinessRules, @ModulePK)
  END ;

  IF NOT EXISTS (SELECT * FROM dbo.cfgModule WHERE Name = @ModuleDefaults) 
  BEGIN
    INSERT INTO dbo.cfgModule
            ( Name, ParentModuleFK 
            )
    VALUES  ( @ModuleDefaults, @ModulePK)
  END ;

COMMIT

