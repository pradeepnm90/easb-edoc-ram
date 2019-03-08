BEGIN TRANSACTION

DECLARE @tempchklit TABLE (GRSCategory varchar(200) null,GRSTask varchar(200) null, ERMSCategory varchar(200) null,
ERMSTask varchar(200) null,changedintask bit NULL, --newGRSCategoryID int null, 
ApplicabletoNonGRS bit null, GRSCatcode int null, GRScatorder int null,GRSTaskID int null,ERMSTaskID int null,TaskOrder int null);

DECLARE @maxcatcode int;
DECLARE @maxcatorder int;
SELECT @maxcatcode = max(tc.code) + 1, @maxcatorder =max(tc.catorder) + 1  FROM dbo.tb_catalogitems tc WHERE tc.catid = 180 
--SELECT @maxcatcode,@maxcatorder

INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Pre-Bind Processing','Data Downloaded','Pipeline - Underwriting','Data Downloaded',@maxcatcode,@maxcatorder);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Pre-Bind Processing','Sent to Actuary','Pipeline - Underwriting','Sent to Actuary',@maxcatcode,@maxcatorder);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Pre-Bind Processing','Actuarial Analysis','Pipeline - Underwriting','Actuarial Analysis',@maxcatcode,@maxcatorder);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Pre-Bind Processing','Firm Order Terms Received','Pipeline – Underwriting or Underwriting','Firm Order Terms Received',@maxcatcode,@maxcatorder);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Modeling','To Be Modelled','Pipeline - Modelling','To Be Modelled',@maxcatcode+1,@maxcatorder+1);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Modeling','Picked Up By Modelers','Pipeline - Modelling','Ready for Screening',@maxcatcode+1,@maxcatorder+1);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Modeling','Submitted to CAT Model','Pipeline - Modelling','Submitted to RMS',@maxcatcode+1,@maxcatorder+1);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Modeling','Modelling Complete','Pipeline - Modelling','Modelling Complete',@maxcatcode+1,@maxcatorder+1);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Underwriting Compliance','Legal Review Complete','Pipeline - Legal or Underwriting','Legal Review Complete',@maxcatcode+2,@maxcatorder+2);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Underwriting Compliance','OFAC Checked','Pipeline - Underwriting','OFAC Checked',@maxcatcode+2,@maxcatorder+2);
--INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Underwriting Compliance','Approval / Peer Review','Pipeline - Underwriting','Peer Review',@maxcatcode+2,@maxcatorder+2);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Underwriting Compliance','Approval / Peer Review','Underwriting','Deal Approval Received / Peer Review',@maxcatcode+2,@maxcatorder+2);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Underwriting Compliance','Underwriter Review of Offers Tab','Pipeline - Underwriting','Underwriter Review of Offers Tab',@maxcatcode+2,@maxcatorder+2);
INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Underwriting Compliance','Contract Complete','Pipeline - Underwriting','Deal Pipeline Complete',@maxcatcode+2,@maxcatorder+2);

DECLARE @to int = 0;
UPDATE @tempchklit SET @to = TaskOrder = (@to+1) WHERE GRSCatcode = @maxcatcode
SET @to = 0;
UPDATE @tempchklit SET @to = TaskOrder = (@to+1) WHERE GRSCatcode = @maxcatcode+1
SET @to = 0;
UPDATE @tempchklit SET @to = TaskOrder = (@to+1) WHERE GRSCatcode = @maxcatcode+2

UPDATE t SET t.ERMSTaskID = tc.chklistnum FROM dbo.tb_chklist tc
JOIN @tempchklit t ON t.ERMSTask = tc.taskname

UPDATE t SET t.GRSTaskID = tc.chklistnum FROM dbo.tb_chklist tc
JOIN @tempchklit t ON t.GRSTask = tc.taskname


--SELECT tc2.filter,tc2.category, tc.*,t.* FROM dbo.tb_chklist tc
--JOIN dbo.tb_chklistfilter tc2 ON tc.entitynum = tc2.entitynum AND tc.chklistnum = tc2.chklistnum
--JOIN @tempchklit t ON tc.chklistnum = t.ERMSTaskID
--WHERE [filter] IN (SELECT DISTINCT tt.team FROM dbo.tb_teambuc tt WHERE tt.busdiv IN ('MGR','FED','CHUB'))

UPDATE  @tempchklit SET [@tempchklit].changedintask = 1 WHERE GRSTask <> ERMSTask

--SELECT t.* FROM @tempchklit t WHERE t.changedintask = 1
UPDATE  @tempchklit SET [@tempchklit].ApplicabletoNonGRS = 1 WHERE ERMSTask IN
(SELECT DISTINCT  tc.taskname FROM dbo.tb_chklist tc
JOIN dbo.tb_chklistfilter tc2 ON tc.entitynum = tc2.entitynum AND tc.chklistnum = tc2.chklistnum
WHERE [filter] NOT IN (SELECT DISTINCT tt.team FROM dbo.tb_teambuc tt WHERE tt.busdiv IN ('MGR','FED','CHUB'))
AND tc.taskname IN (SELECT t.ERMSTask FROM @tempchklit t WHERE t.changedintask = 1))

--SELECT t.* FROM @tempchklit t -- WHERE t.ApplicabletoNonGRS = 1
--Create new categories
IF NOT EXISTS (SELECT 1 FROM dbo.tb_catalogitems tc WHERE tc.catid = 180 AND tc.name IN (SELECT DISTINCT t.GRSCategory FROM @tempchklit t))
BEGIN
	INSERT INTO dbo.tb_catalogitems (catid,catorder,code,charcode,[name],active,intdata1,strdata1,FloatData1)
	SELECT distinct 180,t.GRScatorder,t.GRSCatcode ,'P',t.GRSCategory,1,NULL,t.GRSCategory,NULL FROM @tempchklit t
END;

--update categoryId for exisiting task those are not changing
UPDATE tc2 SET tc2.category = t.GRSCatcode FROM dbo.tb_chklist tc
JOIN dbo.tb_chklistfilter tc2 ON tc.entitynum = tc2.entitynum AND tc.chklistnum = tc2.chklistnum
JOIN @tempchklit t ON taskname = ERMSTask AND changedintask IS NULL
JOIN dbo.tb_catalogitems tc3 ON catid =180 AND code = category
WHERE [filter] IN (SELECT DISTINCT tt.team FROM dbo.tb_teambuc tt WHERE tt.busdiv IN ('MGR','FED','CHUB'))


--update exisiting task those are non GRS
UPDATE t SET  t.GRSTaskID = t.ERMSTaskID FROM  @tempchklit t 
JOIN dbo.tb_chklist tc ON tc.taskname = t.ERMSTask
WHERE t.changedintask = 1 AND t.ApplicabletoNonGRS IS NULL

UPDATE tc SET tc.taskname = t.GRSTask FROM  @tempchklit t 
JOIN dbo.tb_chklist tc ON tc.taskname = t.ERMSTask
WHERE t.changedintask = 1 AND t.ApplicabletoNonGRS IS NULL
--Update category for non GRS task
--select tc2.category , t.GRSCatcode FROM dbo.tb_chklist tc
UPDATE tc2 SET tc2.category = t.GRSCatcode FROM dbo.tb_chklist tc
JOIN dbo.tb_chklistfilter tc2 ON tc.entitynum = tc2.entitynum AND tc.chklistnum = tc2.chklistnum
JOIN @tempchklit t ON taskname = t.GRSTask AND changedintask = 1 AND t.ApplicabletoNonGRS IS NULL
JOIN dbo.tb_catalogitems tc3 ON catid =180 AND code = category
WHERE [filter] IN (SELECT DISTINCT tt.team FROM dbo.tb_teambuc tt WHERE tt.busdiv IN ('MGR','FED','CHUB'))

-- Create new task for task those are mapped to non GRS teams
DECLARE @MaxID int;
SELECT @MaxID = MAX(chklistnum) FROM tb_chklist;

UPDATE @tempchklit SET @MaxID = GRSTaskID = (@MaxID + 1)
WHERE  ApplicabletoNonGRS = 1

INSERT INTO tb_chklist (entitynum,chklistnum,taskname,dftsortorder)
SELECT 1, GRSTaskID ,t.GRSTask, GRSTaskID FROM @tempchklit t WHERE t.ApplicabletoNonGRS = 1

--SELECT t.GRSTaskID,t.GRSTask,tc.taskname,t.GRSCategory, tc2.*
--update checklist and category on for newly created task.
UPDATE tc2 SET tc2.chklistnum = t.GRSTaskID, tc2.category = t.GRSCatcode
 FROM  @tempchklit t
JOIN dbo.tb_chklist tc ON t.ERMSTask = tc.taskname AND  t.ApplicabletoNonGRS = 1
JOIN dbo.tb_chklistfilter tc2 ON tc.entitynum = tc2.entitynum AND tc.chklistnum = tc2.chklistnum
WHERE [filter] IN (SELECT DISTINCT tt.team FROM dbo.tb_teambuc tt WHERE tt.busdiv IN ('MGR','FED','CHUB'))

UPDATE dbo.tb_chklistfilter SET AssumedFlag = NULL WHERE AssumedFlag = 1 
UPDATE dbo.tb_chklistfilter SET AssumedFlag = 1 
WHERE 
chklistnum IN (SELECT t.GRSTaskID FROM @tempchklit t)
AND [filter] IN (SELECT DISTINCT tt.team FROM dbo.tb_teambuc tt WHERE tt.busdiv IN ('MGR','FED','CHUB'))

---Change check list order.

UPDATE tc SET  tc.sortorder = t.TaskOrder
FROM @tempchklit t
JOIN dbo.tb_chklistfilter tc ON t.GRSTaskID = tc.chklistnum
WHERE [filter] IN (SELECT DISTINCT tt.team FROM dbo.tb_teambuc tt WHERE tt.busdiv IN ('MGR','FED','CHUB'))


--ROLLBACK
COMMIT;