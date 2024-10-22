/****** Object:  StoredProcedure [grs].[pr_Lck_GetLockedItem]    Script Date: 2/8/2019 2:15:19 PM ******/
DROP PROCEDURE grs.pr_Lck_GetLockedItem
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [grs].[pr_Lck_GetLockedItem](@CategoryID INT, @ItemID INT, @UserID INT)
AS
BEGIN

	DECLARE @tb_ItemsLock TABLE 
	(
		LockID INT NOT NULL PRIMARY KEY,
		CategoryID INT NOT NULL,
		ItemID INT NOT NULL,
		UserID INT NOT NULL,
		SessionID INT NOT NULL,
		DataBaseName VARCHAR(50) NOT NULL,
		IsEditLock BIT NOT NULL,
		Notes VARCHAR(250) NULL,
		EntryTime DATETIME NOT NULL,
		ExpirationTime DATETIME NOT NULL,
		LockingUser VARCHAR(64) NOT NULL
	) ;

    INSERT INTO @tb_ItemsLock 
	(
			LockID,
			CategoryID,
			ItemID,
			UserID,
			SessionID,
			DataBaseName,
			IsEditLock,
			Notes,
			EntryTime,
			ExpirationTime, 
			LockingUser     
	)
	EXEC dbo.fn_Lck_GetLockedItem @CategoryID, @ItemID, @UserID;
	
    SELECT 	l.LockID,
			l.CategoryID,
			l.ItemID,
			l.UserID,
			l.SessionID,
			l.DataBaseName,
			l.IsEditLock,
			l.Notes,
			l.EntryTime,
			l.ExpirationTime, 
			l.LockingUser,     
			LockingUserName = u.firstname + ' ' + u.lastname 
	FROM @tb_ItemsLock l
	INNER JOIN dbo.tb_user u ON u.userid = l.UserID
    ORDER BY l.IsEditLock, l.UserID;

END


GO

GRANT EXECUTE ON grs.pr_Lck_GetLockedItem TO [ErmsUsers] AS [dbo]
GO

