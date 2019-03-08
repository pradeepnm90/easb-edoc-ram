BEGIN TRANSACTION;


DECLARE @catalogdefId int;

SET @catalogdefId = 382

IF (Not Exists(Select top 1 * from tb_catalogdef where catid = @catalogdefId))
BEGIN
	-- Creating the Catalog Definition - "GRS Writing Company" 
	INSERT INTO tb_catalogdef (catid, catname, usecharcode, constname)
		VALUES (@catalogdefId, 'GRS Writing Company', 0, 'GRSPaper')


	-- Creating the Catalong Items 
	DECLARE @catalogitem TABLE (catorder int, code int, charcode varchar(40), name  varchar(100), active  bit, intdata1 int);

	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (1, 1, 1, 'Markel Bermuda Limited', 1, 1)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 2, 0, 'Alterra Reinsurance Europe plc', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 3, 0, 'Markel International Ireland', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 5, 0, 'Alterra Insurance Europe Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 7, 0, 'Alterra Capital Services Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 12, 0, 'Alterra at Lloyd''s Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 10, 0, 'Alterra at Lloyd''s Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 11, 0, 'Alterra at Lloyd''s Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 13, 0, 'Alterra at Lloyd''s Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 14, 0, 'Harbor Point Re Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (2, 16, 1, 'Markel Bermuda Limited', 1, 1)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 15, 0, 'Harbor Point Re Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 17, 0, 'Alterra Reinsurance Europe plc', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 18, 0, 'Harbor Point Re Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 19, 0, 'Markel International Ireland', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 20, 0, 'Alterra Reinsurance Europe plc', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 21, 0, 'Harbor Point Re Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 23, 0, 'Markel Bermuda Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 22, 0, 'Markel Bermuda Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 27, 0, 'Harbor Point Re Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 4, 0, 'Evanston Insurance Company', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 6, 0, 'State National Insurance Company, Inc.', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 8, 0, '', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 9, 0, 'Max America Insurance Company', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 99, 0, 'BELIEVER''S INTERNATIONAL WORSHIP CENTER', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 24, 0, '', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 25, 0, '', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 26, 0, '', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 28, 0, '', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 32, 0, 'Harbor Point Re Limited', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (3, 33, 1, 'Evanston Insurance Company', 1, 1)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (4, 34, 1, '', 1, 1)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 35, 0, '', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (5, 29, 1, 'Markel International Ireland', 1, 1)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 30, 0, 'Markel International Ireland', 1, 0)
	INSERT INTO @catalogitem (catorder, code, charcode, name, active, intdata1)	VALUES (0, 31, 0, 'Alterra Insurance Europe Limited', 1, 0) 


	INSERT INTO dbo.tb_catalogitems (catid, catorder, code, charcode, name, active, intdata1)
		SELECT
			@catalogdefId
			,cb.catorder
			,cb.code
			,cb.charcode
			,cb.name
			,cb.active
			,cb.intdata1
		FROM @catalogitem cb

		PRINT 'CatID-382 setup completed'
END

COMMIT;
--ROLLBACK;