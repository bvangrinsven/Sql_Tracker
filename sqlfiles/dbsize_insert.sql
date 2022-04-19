BEGIN TRANSACTION;
INSERT INTO [dbo].[tblDatabaseSizes] (
[DatabaseGUID], [CurrentFileSizeMB], [TotalDBSizeMB], [DateReported], [MonthReported], [YearReported], [WeekNumReported]
) 
SELECT tD.[GUIDDatabase], [tS].[CurrentFileSizeMB], [tS].[TotalDBSizeMB], [tS].[DateReported], [tS].[MonthReported], [tS].[YearReported], [tS].[WeekNumReported]
FROM @inputTable as tS
INNER JOIN [dbo].[tblDatabases] as tD ON td.ServerGUID = tS.ServerGUID AND tD.[Name] = tS.[DatabaseName]
;
COMMIT TRANSACTION;