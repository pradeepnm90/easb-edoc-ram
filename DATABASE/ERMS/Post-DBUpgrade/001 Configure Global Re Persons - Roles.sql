BEGIN TRANSACTION 

DECLARE @PersonRole TABLE
(
    ID INT NOT NULL IDENTITY(1, 1),
	personId INT,
	roleName VARCHAR(200)
)
BEGIN -- data

INSERT INTO @PersonRole VALUES(10645,'Modeler Manager')
INSERT INTO @PersonRole VALUES(19340,'Modeler')
INSERT INTO @PersonRole VALUES(752428,'Modeler')
INSERT INTO @PersonRole VALUES(9525,'Modeler')
INSERT INTO @PersonRole VALUES(855394,'Modeler')
INSERT INTO @PersonRole VALUES(65125,'Modeler')
INSERT INTO @PersonRole VALUES(63184,'Actuary Manager')
INSERT INTO @PersonRole VALUES(48629,'Actuary')
INSERT INTO @PersonRole VALUES(59716,'Actuary')
INSERT INTO @PersonRole VALUES(59717,'Actuary')
INSERT INTO @PersonRole VALUES(774222,'Actuary')
INSERT INTO @PersonRole VALUES(66099,'Actuary')
INSERT INTO @PersonRole VALUES(520443,'Actuary')
INSERT INTO @PersonRole VALUES(9402,'Underwriter Manager')
INSERT INTO @PersonRole VALUES(64551,'Underwriter')
INSERT INTO @PersonRole VALUES(873638,'Underwriter')
INSERT INTO @PersonRole VALUES(32916,'Underwriter')
INSERT INTO @PersonRole VALUES(853586,'Underwriter')
INSERT INTO @PersonRole VALUES(52520,'Underwriter')
INSERT INTO @PersonRole VALUES(65564,'Underwriter')
INSERT INTO @PersonRole VALUES(52517,'Underwriter')
INSERT INTO @PersonRole VALUES(49603,'UA/TA')
INSERT INTO @PersonRole VALUES(950979,'Underwriter Manager')
INSERT INTO @PersonRole VALUES(950986,'Underwriter')
INSERT INTO @PersonRole VALUES(950993,'Underwriter')
INSERT INTO @PersonRole VALUES(950994,'Underwriter')
INSERT INTO @PersonRole VALUES(950995,'Underwriter')
INSERT INTO @PersonRole VALUES(950996,'UA/TA')
INSERT INTO @PersonRole VALUES(35402,'Underwriter Manager')
INSERT INTO @PersonRole VALUES(11123,'Underwriter')
INSERT INTO @PersonRole VALUES(44354,'Property UA/TA')
INSERT INTO @PersonRole VALUES(66495,'Underwriter')
INSERT INTO @PersonRole VALUES(66602,'Underwriter')
INSERT INTO @PersonRole VALUES(34604,'Modeler')
INSERT INTO @PersonRole VALUES(843379,'Underwriter')
INSERT INTO @PersonRole VALUES(9798,'Underwriter')
INSERT INTO @PersonRole VALUES(59632,'Property UA/TA')
INSERT INTO @PersonRole VALUES(52026,'Underwriter Manager')
INSERT INTO @PersonRole VALUES(34608,'Underwriter')
INSERT INTO @PersonRole VALUES(853777,'Underwriter')
INSERT INTO @PersonRole VALUES(52523,'Underwriter')
INSERT INTO @PersonRole VALUES(10725,'Underwriter')
INSERT INTO @PersonRole VALUES(64710,'Underwriter')
INSERT INTO @PersonRole VALUES(10275,'Underwriter')
INSERT INTO @PersonRole VALUES(848177,'Underwriter Manager')
INSERT INTO @PersonRole VALUES(848179,'Underwriter')
INSERT INTO @PersonRole VALUES(848178,'Underwriter')
INSERT INTO @PersonRole VALUES(821278,'Underwriter')
INSERT INTO @PersonRole VALUES(627938,'UA/TA')
INSERT INTO @PersonRole VALUES(10403,'Underwriter Manager')
INSERT INTO @PersonRole VALUES(34609,'Underwriter')
INSERT INTO @PersonRole VALUES(35638,'UA/TA')
INSERT INTO @PersonRole VALUES(784393,'Underwriter')
INSERT INTO @PersonRole VALUES(799488,'Underwriter')
INSERT INTO @PersonRole VALUES(34623,'Underwriter')
INSERT INTO @PersonRole VALUES(33725,'Property UA/TA')
INSERT INTO @PersonRole VALUES(9797,'Underwriter')
INSERT INTO @PersonRole VALUES(924740,'Property UA/TA')
INSERT INTO @PersonRole VALUES(48629,'Underwriter')

END 



DECLARE @RolePrefix VARCHAR(100 )= 'GlobalRe.'

DECLARE person_cursor CURSOR FOR   
SELECT p.PersonId, @RolePrefix + pr.roleName, p.PersonName
FROM @PersonRole pr
INNER JOIN [dbo].[tb_Person] P ON pr.personId = p.PersonId

SELECT p.PersonId, @RolePrefix + pr.roleName, p.PersonName
FROM @PersonRole pr
INNER JOIN [dbo].[tb_Person] P ON pr.personId = p.PersonId

OPEN person_cursor ;

DECLARE @PersonID INT,
	    @PersonName VARCHAR(100),
		@RoleName VARCHAR(200),
		@RoleFK INT,
		@ManagerID INT,
		@ManagerName VARCHAR(100)

FETCH NEXT FROM person_cursor INTO @PersonID, @RoleName, @PersonName

WHILE @@FETCH_STATUS = 0  
BEGIN  

PRINT @RoleName
PRINT @PersonName

        IF ( SELECT COUNT(*)
             FROM   cfgRole
             WHERE name = @RoleName
           ) = 0
            BEGIN  
                INSERT  INTO dbo.cfgRole
                        ( Name, SourceOfRole )
                VALUES  ( @RoleName, 'LOCAL' ) 
            END;

        SET @RoleFK = ( SELECT TOP 1 RolePK
                        FROM    dbo.cfgRole
                        WHERE   Name = @RoleName
                      ) 
        IF ( @PersonID > 0 )
            AND ( @RoleFK > 0 ) 
            BEGIN
                INSERT  INTO dbo.cfgRolePerson
                        ( RoleFK ,
                          PersonNameFK 
                        )
                        SELECT  @RoleFK ,
                                @PersonID
                        WHERE   NOT EXISTS ( SELECT RoleFK
                                             FROM   dbo.cfgRolePerson
                                             WHERE  RoleFK = @RoleFK
                                                    AND PersonNameFK = @PersonID );

				IF @RoleName LIKE '%Manager%'
				BEGIN 
				  SET @ManagerID = @PersonID;
				  SET @ManagerName = @PersonName;
				END
				ELSE BEGIN 
					UPDATE dbo.tb_Person
					SET ManagerId = @ManagerID,
						ManagerName = @ManagerName
					WHERE PersonID = @PersonID
					  --AND ManagerId IS NULL AND ManagerName IS NULL;
	            END; 
            END ;                  
    
	FETCH NEXT FROM person_cursor INTO @PersonID, @RoleName, @PersonName
END;

CLOSE person_cursor;
DEALLOCATE person_cursor;


--SELECT * FROM dbo.cfgRole
--ORDER BY _sys_CreatedDt DESC 

--SELECT * FROM dbo.cfgRolePerson
--ORDER BY _sys_CreatedDt DESC 



----ROLLBACK TRANSACTION 
COMMIT 

