SELECT svr.GUIDServer, db.*, tbl.*
FROM dbo.tblServers as svr, dbo.tblServerToDatabase  as svr2db, dbo.tblDatabases as db, dbo.tblDatabaseToTable as db2tbl, dbo.tblDatabaseTables as tbl
WHERE MATCH(svr-(svr2db)->db-(db2tbl)->tbl)



SELECT svr.[$node_id_A81E05C24D31401A959EF1BE902AD2E3]
, svr.[GUIDServer] as ServerGUID
, svr.[Name] as SvrName
, 'InstalledOn' as InstalledOn
, db.[$node_id_4873AFA648FF413795B6350E256F1F52]
, db.[GUIDDatabase] as DatabaseGUID
, db.[Name] as DBName
FROM dbo.tblServers as svr, dbo.tblServerToDatabase as svr2db, dbo.tblDatabases as db
WHERE MATCH(svr-(svr2db)->db)
AND svr.IsDeleted = 0 AND db.IsDeleted = 0



