

CREATE TYPE dbo.[tblDatabaseTableSizes] AS TABLE (
    [ServerGUID] [varchar](36)
    , [DatabaseGUID] [varchar](36)
    , [TableName] [varchar](200)
    , [SchemaName] [varchar](200)
    , [RowsCount] bigint
    , [TotalSpaceMB] bigint
    , [UsedSpaceMB] bigint
    , [UnusedSpaceMB] bigint
    , [DateReported] datetime
    , [MonthReported] int
    , [YearReported] int
    , [WeekNumReported] int
)

