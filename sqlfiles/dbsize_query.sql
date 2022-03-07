SELECT
    m.name AS DatabaseFileName,
    m.size * 8/1024 as CurrentFileSizeMB,
    SUM(m.size * 8/1024) OVER (PARTITION BY d.name) AS TotalDBSizeMB
FROM sys.master_files m
INNER JOIN sys.databases d ON
d.database_id = m.database_id;