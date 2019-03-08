DROP PROCEDURE grs.pr_GetGRSDealCountByStatus
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**
Returns a list of Global Re specific statuses with deals count additionally filtered by exposures and or key members
 @Exposurelist and @ERMSPersonID is comma separated list of subdivisions
 */
CREATE PROCEDURE grs.pr_GetGRSDealCountByStatus
	@Exposurelist VARCHAR(1000) = NULL,
  @ERMSPersonID  VARCHAR(1000) = NULL
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	    DECLARE @Exposures TABLE 
    (
        exposuretype INT 
    ) ;

	DECLARE @Persons TABLE 
    (
        personid INT 
    ) ;

    IF @Exposurelist IS NOT NULL
    BEGIN 
        INSERT INTO @Exposures
        SELECT ids.Token 
        FROM dbo.fn_SplitTokens(@Exposurelist,',') ids;
    END 
    
	IF @ERMSPersonID IS NOT NULL
    BEGIN 
        INSERT INTO @Persons
        SELECT ids.Token 
        FROM dbo.fn_SplitTokens(@ERMSPersonID,',') ids;
    END 
    

    SELECT s.GroupID AS StatusGroup, 
            s.GroupName AS StatusGroupName, 
            s.StatusCode, 
            s.StatusName, 
            s.GroupSortOrder AS StatusGroupSortOrder, 
            s.StatusSortOrder, 
            s.WorkflowID, 
            s.WorkflowName,
             COUNT(dealnum) AS 'Count'
    FROM  grs.v_GRDealStatus s
    LEFT OUTER JOIN (SELECT d.dealnum, d.StatusGroup, d.StatusCode
                      FROM  grs.v_GRSDealsByStatus d
                     WHERE (@Exposurelist IS NULL OR @Exposurelist = '' OR 
                            EXISTS (SELECT *
                                    FROM @Exposures s
                                    WHERE s.exposuretype = d.exposuretype))
                        AND (@ERMSPersonID IS NULL OR @ERMSPersonID = '' OR
                            (EXISTS (SELECT *
                                    FROM @Persons p 
                                    WHERE p.personid = d.uw1 OR p.personid = d.uw2 
									OR d.Modeller = p.personid OR d.act1 = p.personid OR d.TA = p.personid)
							)
                          
                        )
        ) deals ON s.GroupID = deals.StatusGroup AND s.StatusCode = deals.StatusCode
    GROUP BY s.GroupID, s.GroupName, s.StatusCode, s.StatusName, s.GroupSortOrder, s.StatusSortOrder,  s.WorkflowID, s.WorkflowName
    ORDER BY StatusGroupSortOrder, StatusSortOrder
 
END

GO


GRANT EXECUTE ON grs.pr_GetGRSDealCountByStatus TO [ErmsUsers] AS [dbo]

GO 

