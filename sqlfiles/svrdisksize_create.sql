

CREATE TYPE dbo.[tblServerDiskSizes] AS TABLE (
    [ServerGUID] [varchar](36)
    ,[Disk] [varchar](200)    
    ,[TotalSizeInMB] decimal
    ,[AvailableInMB] decimal
    ,[FreeSpacePercent] decimal
    ,[DateReported] datetime
    ,[MonthReported] int
    ,[YearReported] int
    ,[WeekNumReported] int
)

