
BEGIN TRANSACTION;

INSERT INTO [dbo].[tblDatabaseTableSizes] (
[DatabaseTableGUID], [RowsCount], [TotalSpaceMB], [UsedSpaceMB], [UnusedSpaceMB], [DateReported], [MonthReported], [YearReported], [WeekNumReported]
) 
SELECT [tD].[GUIDDatabaseTable], [tS].[RowsCount], [tS].[TotalSpaceMB], [tS].[UsedSpaceMB], [tS].[UnusedSpaceMB], [tS].[DateReported], [tS].[MonthReported], [tS].[YearReported], [tS].[WeekNumReported]
FROM @inputTable as tS
INNER JOIN [dbo].[tblDatabaseTables] as tD ON tS.[ServerGUID] = tD.[ServerGUID] AND tS.[DatabaseGUID] = tD.[DatabaseGUID] AND tS.[TableName] = tD.[Name] AND tS.[SchemaName] = tD.[SchemaName]

COMMIT TRANSACTION
