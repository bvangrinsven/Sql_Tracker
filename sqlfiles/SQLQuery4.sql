/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [$node_id_A81E05C24D31401A959EF1BE902AD2E3]
      ,[GUIDServer]
      ,[Name]
      ,[ConnectionString]
      ,[IsDeleted]
      ,[DateCreated]
      ,[DateModified]
  FROM [CentralMonitoring].[dbo].[tblServers]


SELECT db.database_id, db.[name] as DatabaseName
FROM sys.databases db 
WHERE db.database_id > 4 