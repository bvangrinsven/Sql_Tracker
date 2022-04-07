SELECT DISTINCT 
CONVERT(CHAR(100), SERVERPROPERTY('Servername')) AS [ServerName],
volume_mount_point [Disk], 
file_system_type [FileSystem], 
logical_volume_name as [LogicalDriveName], 
CONVERT(DECIMAL(18,2),total_bytes/1024) AS [TotalSizeInMB], ---1GB = 1073741824 bytes
CONVERT(DECIMAL(18,2),available_bytes/1024) AS [AvailableSizeInMB],  
CAST(CAST(available_bytes AS FLOAT)/ CAST(total_bytes AS FLOAT) AS DECIMAL(18,2)) * 100 AS [SpaceFreePercent] 
FROM sys.master_files 
CROSS APPLY sys.dm_os_volume_stats(database_id, file_id)

