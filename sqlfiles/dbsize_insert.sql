INSERT INTO [dbo].[tblDatabaseSize]
    ([DatabaseGUID]
    ,[DatabaseFileName]
    ,[CurrentFileSizeMB]
    ,[TotalDBSizeMB]
    ,[DateReported]
    ,[MonthReported]
    ,[YearReported]
    ,[WeekNumReported])
VALUES
    (@DatabaseGUID
    ,@DatabaseFileName
    ,@CurrentFileSizeMB
    ,@TotalDBSizeMB
    ,@DateReported
    ,@MonthReported
    ,@YearReported
    ,@WeekNumReported)