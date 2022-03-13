SELECT COUNT(*) as TableExists
    FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_SCHEMA = 'dbo' 
    AND TABLE_NAME IN ('tblServers', 'tblDatabases', 'tblDatabaseTables')