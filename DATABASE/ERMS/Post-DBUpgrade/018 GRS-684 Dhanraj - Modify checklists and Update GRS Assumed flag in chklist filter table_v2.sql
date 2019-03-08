BEGIN TRANSACTION

DECLARE @tempchklit TABLE (GRSCategory varchar(200) null,GRSTask varchar(200) null, ERMSCategory varchar(200) null,
ERMSTask varchar(200) null,changedintask bit NULL, --newGRSCategoryID int null, 
ApplicabletoNonGRS bit null, GRSCatcode int null, GRScatorder int null,GRSTaskID int null,ERMSTaskID int null,TaskOrder int null);

DECLARE @maxcatcode int;
DECLARE @maxcatorder int;
SELECT @maxcatcode = tc.code, @maxcatorder =tc.catorder FROM dbo.tb_catalogitems tc WHERE tc.catid = 180 AND tc.name = 'Underwriting Compliance';
--SELECT @maxcatcode,@maxcatorder

INSERT INTO @tempchklit (GRSCategory,GRSTask,ERMSCategory,ERMSTask,GRSCatcode,GRScatorder) VALUES ('Underwriting Compliance','Approval / Peer Review','Pipeline - Underwriting','Peer Review',@maxcatcode,@maxcatorder);


UPDATE t SET t.ERMSTaskID = tc.chklistnum FROM dbo.tb_chklist tc
JOIN @tempchklit t ON t.ERMSTask = tc.taskname

UPDATE t SET t.GRSTaskID = tc.chklistnum FROM dbo.tb_chklist tc
JOIN @tempchklit t ON t.GRSTask = tc.taskname

DELETE FROM dbo.tb_chklistfilter WHERE chklistnum = 4 AND filter = 3;

DECLARE @sortorder int;
SELECT TOP 1 @sortorder = tb.sortorder FROM dbo.tb_chklistfilter tb
JOIN @tempchklit t ON tb.chklistnum = t.GRSTaskID 

--SELECT tc2.chklistnum,t.GRSTaskID,tc2.filter,t.GRSCatcode ,@sortorder
UPDATE tc2 SET tc2.chklistnum = t.GRSTaskID,tc2.category = t.GRSCatcode ,tc2.sortorder = @sortorder, tc2.AssumedFlag = 1
FROM dbo.tb_chklist tc
JOIN dbo.tb_chklistfilter tc2 ON tc.entitynum = tc2.entitynum AND tc.chklistnum = tc2.chklistnum
JOIN @tempchklit t ON tc.chklistnum = t.ERMSTaskID 
WHERE [filter] IN (SELECT DISTINCT tt.team FROM dbo.tb_teambuc tt WHERE tt.busdiv IN ('MGR','FED','CHUB'))

DECLARE @settingname varchar(300) = 'Underwriting Add Duplicate Item For Peer Review Checklist' ;
DECLARE @fact int;

SELECT @fact = chklistnum FROM tb_chklist WHERE taskname  = 'Approval / Peer Review'; 

EXEC dbo.pr_cfgAddFact @SettingName = @SettingName, @FactValue = @fact,@Sequence = 10,@UnderWritingTeamFK = 9;
EXEC dbo.pr_cfgAddFact @SettingName = @SettingName, @FactValue = @fact,@Sequence = 10,@UnderWritingTeamFK = 33;
EXEC dbo.pr_cfgAddFact @SettingName = @SettingName, @FactValue = @fact,@Sequence = 10,@UnderWritingTeamFK = 50;
EXEC dbo.pr_cfgAddFact @SettingName = @SettingName, @FactValue = @fact,@Sequence = 10,@UnderWritingTeamFK = 112;
EXEC dbo.pr_cfgAddFact @SettingName = @SettingName, @FactValue = @fact,@Sequence = 10,@UnderWritingTeamFK = 114;
EXEC dbo.pr_cfgAddFact @SettingName = @SettingName, @FactValue = @fact,@Sequence = 10,@UnderWritingTeamFK = 115;
EXEC dbo.pr_cfgAddFact @SettingName = @SettingName, @FactValue = @fact,@Sequence = 10,@UnderWritingTeamFK = 175;
EXEC dbo.pr_cfgAddFact @SettingName = @SettingName, @FactValue = @fact,@Sequence = 10,@UnderWritingTeamFK = 177;
--Select * from dbo.tb_chklistfilter  WHERE  filter IN (9,33,50,112,114,115,175,177) AND dbo.tb_chklistfilter.chklistnum = 407
--Select * from dbo.tb_chklistfilter  ORDER BY dbo.tb_chklistfilter._sys_ModifiedDt desc
--ROLLBACK
COMMIT;