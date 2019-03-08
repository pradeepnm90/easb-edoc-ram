BEGIN TRANSACTION

UPDATE dbo.tb_catalogitems SET strdata1 = 'GRS Modeling'  WHERE catid = 180 AND name = 'Modeling' AND strdata1 = 'Modeling';

COMMIT;