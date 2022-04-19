
BEGIN TRANSACTION;

INSERT INTO [dbo].[tblServerDiskSizes] (
[ServerDiskGUID], [TotalSizeInMB], [AvailableInMB], [FreeSpacePercent], [DateReported], [MonthReported], [YearReported], [WeekNumReported]
) 
SELECT [tD].[GUIDServerDisk], [tS].[TotalSizeInMB], [tS].[AvailableInMB], [tS].[FreeSpacePercent], [tS].[DateReported], [tS].[MonthReported], [tS].[YearReported], [tS].[WeekNumReported]
FROM @inputTable as tS
INNER JOIN [dbo].[tblServerDisk] as tD ON tS.[ServerGUID] = tD.[ServerGUID] AND tS.[Disk] = tD.[Disk]


COMMIT TRANSACTION
