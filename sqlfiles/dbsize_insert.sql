INSERT INTO [dbo].[tblDatabaseSize]
    ([DatabaseGUID]
    ,[DatabaseFileName]
    ,[CurrentFileSizeMB]
    ,[TotalDBSizeMB]
    ,[DateReported]
    ,[MonthReported]
    ,[YearReported]
    ,[WeekNumReported])

INSERT INTO [dbo].[tblDatabaseSizes] (
[DatabaseGUID], [CurrentFileSizeMB], [TotalDBSizeMB], [DateReported], [MonthReported], [YearReported], [WeekNumReported]
) 
SELECT [tS].[DatabaseGUID], [tS].[CurrentFileSizeMB], [tS].[TotalDBSizeMB], [tS].[DateReported], [tS].[MonthReported], [tS].[YearReported], [tS].[WeekNumReported]
FROM @inputTable as tS
LEFT OUTER JOIN [dbo].[tblDatabases] as tD ON td.ServerGUID = tS.ServerGUID AND tD.[Name] = tS.[DatabaseName]
;