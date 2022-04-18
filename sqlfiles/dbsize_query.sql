CREATE TABLE #FileSize
(dbName NVARCHAR(128), 
    FileName NVARCHAR(128), 
    type_desc NVARCHAR(128),
    CurrentSizeMB DECIMAL(10,2), 
    FreeSpaceMB DECIMAL(10,2)
);
    
INSERT INTO #FileSize(dbName, FileName, type_desc, CurrentSizeMB, FreeSpaceMB)
exec sp_msforeachdb 
'use [?]; 
 SELECT DB_NAME() AS DbName, 
        name AS FileName, 
        type_desc,
        size/128.0 AS CurrentSizeMB,  
        size/128.0 - CAST(FILEPROPERTY(name, ''SpaceUsed'') AS INT)/128.0 AS FreeSpaceMB
FROM sys.database_files
WHERE type IN (0,1);';
    
SELECT @ServerGUID as ServerGUID, 
dbName as DatabaseName, SUM(CurrentSizeMB) - SUM(FreeSpaceMB) as [UsedSizeMB], SUM(CurrentSizeMB) as [TotalDBSizeMB]
, @DateReported as DateReported, @MonthReported as MonthReported, @YearReported as YearReported, @WeekNumReported as WeekNumReported
FROM #FileSize 
GROUP BY dbName;
    
DROP TABLE #FileSize;