DROP VIEW grs.v_GRDealStatus;
GO

CREATE VIEW grs.v_GRDealStatus AS
/*
View returns a list of GRS specific statuses .
*/

SELECT * FROM (
VALUES
    (1, 'Default', 1, 'In Progress', 1, 3, 'Under Review', 1),
    (1, 'Default', 1, 'In Progress', 1, 80, 'Authorize', 2),
    (1, 'Default', 1, 'In Progress', 1, 2, 'Outstanding Quote', 3),
    (1, 'Default', 1, 'In Progress', 1, 14, 'To Be Declined', 4),
    (1, 'Default', 1, 'In Progress', 1, 29, 'Bound Pending Data Entry', 5),
    (1, 'Default', 2, 'On Hold', 2, 16, 'On Hold', 1),
    (1, 'Default', 3, 'Bound - Pending Actions', 3, 1000, 'Bound - Pending Actions', 1),
    (2, 'Renewals', 4, 'Renewable - 6 Months', 4, 1001, 'Renewable', 1)
    --(3, 'DUMMY', 5, 'DUMMY - QA USE Only', 5, 23, 'Bound (With Errors)', 1), -- remove after QA is done
    --(3, 'DUMMY', 6, 'DUMMY - DEV USE Only', 6, 41, 'Inquired', 1), -- remove after QA is done
    --(3, 'DUMMY', 6, 'DUMMY - DEV USE Only', 6, 1, 'In Due Diligence', 2), -- remove after QA is done
    --(3, 'DUMMY', 6, 'DUMMY - DEV USE Only', 6, 22, 'Informational Agreement', 3), -- remove after QA is done
    --(1, 'Default', 7, 'DUMMY - Multiple Statuses', 7, 0, 'Bound', 1), -- remove after QA is done
    --(1, 'Default', 7, 'DUMMY - Multiple Statuses', 7, 15, 'Indicated', 2) -- remove after QA is done
    ) AS v([WorkflowID], [WorkflowName], [GroupID], [GroupName], [GroupSortOrder], [StatusCode], [StatusName], [StatusSortOrder])

GO 

GRANT SELECT ON grs.v_GRDealStatus TO public;
GO


