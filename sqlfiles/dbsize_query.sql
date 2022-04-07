SELECT
    m.name AS DatabaseFileName,
    m.size * 8/1024 as CurrentFileSizeMB,
    SUM(m.size * 8/1024) OVER (PARTITION BY d.name) AS TotalDBSizeMB
FROM sys.master_files m
INNER JOIN sys.databases d ON
d.database_id = m.database_id;


/*
SELECT f.name AS [File Name] , f.physical_name AS [Physical Name], 
CAST((f.size/128.0) AS DECIMAL(15,2)) AS [Total Size in MB],
CAST(f.size/128.0 - CAST(FILEPROPERTY(f.name, 'SpaceUsed') AS int)/128.0 AS DECIMAL(15,2)) 
AS [Available Space In MB], [file_id], fg.name AS [Filegroup Name]
FROM sys.database_files AS f WITH (NOLOCK) 
LEFT OUTER JOIN sys.data_spaces AS fg WITH (NOLOCK) 
ON f.data_space_id = fg.data_space_id OPTION (RECOMPILE);
*/