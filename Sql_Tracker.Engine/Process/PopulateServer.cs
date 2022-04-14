using Microsoft.Extensions.Logging;
using Sql_Tracker.Engine.Interfaces;
using Sql_Tracker.Engine.Models;
using Sql_Tracker.Engine.Utilz;
using Sql_Tracker.Engine.Utilz.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sql_Tracker.Engine.Process
{
    public class PopulateServer : IPopulateServer
    {
        private ILogger<PopulateServer> log;
        private ISettings Setting;
        private IDBExecute _db;

        public PopulateServer(ILogger<PopulateServer> logger, ISettings settings, IDBExecute dB)
        {
            log = logger;
            Setting = settings;
            _db = dB;
        }

        public void Execute()
        {
            string Sql = "SELECT $node_id as NodeId, [GUIDServer], [Name], [ConnectionString] FROM [tblServers]";
            DataTable dbt = _db.ExecuteDataTable(Sql);

            List<Server> servers = Generator.ConvertDataTable<Server>(dbt);

            PopulateDatabases(servers);
        }


        private void PopulateDatabases(List<Server> servers)
        {
            Database db = new Database();

            string GetDatabaseGUID = "SELECT GUIDDatabase FROM tblDatabases WHERE [ServerGUID] = @GUIDServer AND [Name] = @DBName";

            string InsertSql = "INSERT INTO [dbo].[tblDatabases] ([ServerGUID], [GUIDDatabase],[Name]) VALUES (@GUIDServer, @GUIDDatabase, @DBName);";
            string InsertSvrSql = "IF(SELECT COUNT(*) FROM[dbo].[tblServerToDatabase] WHERE $from_id = (SELECT $node_id FROM tblServers WHERE GUIDServer = @GUIDServer) AND $to_id = (SELECT $node_id FROM tblDatabases WHERE GUIDDatabase = @GUIDDatabase AND ServerGUID = @GUIDServer)) = 0 BEGIN " +
                "INSERT INTO[dbo].[tblServerToDatabase] VALUES((SELECT $node_id FROM tblServers WHERE GUIDServer = @GUIDServer), (SELECT $node_id FROM tblDatabases WHERE GUIDDatabase = @GUIDDatabase AND ServerGUID = @GUIDServer)); " +
                "END";

            QueryParameter[] iParams = new QueryParameter[3];
            iParams[0] = new QueryParameter() { DbType = DbType.String, Name = "GUIDServer", Size = 36 };
            iParams[1] = new QueryParameter() { DbType = DbType.String, Name = "GUIDDatabase", Size = 36 };
            iParams[2] = new QueryParameter() { DbType = DbType.String, Name = "DBName", Size = 200 };

            foreach (Server server in servers)
            {
                iParams[0].Value = server.GUIDServer;

                string connStr = server.ConnectionString
                    .Replace("{{database}}", "master")
                    .Replace("{{monitoruser}}", Setting.GetUsername(server.Name))
                    .Replace("{{monitorpass}}", Setting.GetPassword(server.Name));

                DataTable _dt = _db.ExecuteDataTable(db.GetObjectsSql(), connStr);

                foreach (DataRow oRow in _dt.Rows)
                {
                    bool CreateDBEntry = false;
                    db = Generator.GetItem<Database>(oRow);

                    iParams[2].Value = db.Name;

                    object result = _db.ExecuteScalar(GetDatabaseGUID, iParams);

                    if (result.IsEmptyValue())
                    {
                        db.GUIDDatabase = Guid.NewGuid().ToString().ToUpper();
                        CreateDBEntry = true;
                    }
                    else
                    {
                        db.GUIDDatabase = result.ToString();
                    }

                    iParams[1].Value = db.GUIDDatabase;

                    if (CreateDBEntry) 
                        _db.ExecuteNonQuery(InsertSql, iParams);

                    _db.ExecuteNonQuery(InsertSvrSql, iParams);

                    PopulateDatabaseTables(server, db);

                }
            }

        }

        private void PopulateDatabaseTables(Server server, Database database)
        {

            string SqlPullTables = "SELECT SCHEMA_NAME(t.schema_id) as SchemaName, t.NAME AS [Name], t.create_date as DateCreated, t.modify_date as DateModified " +
                "FROM sys.tables t WHERE t.NAME NOT LIKE 'dt%' AND t.is_ms_shipped = 0";

            string InsertDBTableSql = "INSERT INTO [dbo].[tblDatabaseTables] ([DatabaseGUID],[GUIDDatabaseTable],[SchemaName],[Name]) " +
                "VALUES (@DatabaseGUID,@GUIDDatabaseTable,@SchemaName,@TableName)";

            string InsertDb2TableSql = "IF (SELECT COUNT(*) FROM [dbo].[tblDatabaseToTable] WHERE $from_id = (SELECT $node_id FROM tblDatabases WHERE GUIDDatabase = @DatabaseGUID AND ServerGUID = @GUIDServer) AND $to_id = (SELECT $node_id FROM tblDatabaseTables WHERE DatabaseGUID = @DatabaseGUID AND GUIDDatabaseTable = @GUIDDatabaseTable)) = 0 BEGIN " +
                "INSERT INTO [dbo].[tblDatabaseToTable] VALUES((SELECT $node_id FROM tblDatabases WHERE GUIDDatabase = @DatabaseGUID AND ServerGUID = @GUIDServer), (SELECT $node_id FROM tblDatabaseTables WHERE DatabaseGUID = @DatabaseGUID AND GUIDDatabaseTable = @GUIDDatabaseTable)); " +
                "END";

            string connStr = server.ConnectionString
                .Replace("{{database}}", database.Name)
                .Replace("{{monitoruser}}", Setting.GetUsername(server.Name))
                .Replace("{{monitorpass}}", Setting.GetPassword(server.Name));


            string GetTableGUID = "SELECT GUIDDatabaseTable FROM tblDatabaseTables WHERE DatabaseGUID = @DatabaseGUID AND [Name] = @TableName AND [SchemaName] = @SchemaName";

            QueryParameter[] iParams = new QueryParameter[5];
            iParams[0] = new QueryParameter() { DbType = DbType.String, Name = "DatabaseGUID", Size = 36, Value = database.GUIDDatabase };
            iParams[1] = new QueryParameter() { DbType = DbType.String, Name = "GUIDDatabaseTable", Size = 36 };
            iParams[2] = new QueryParameter() { DbType = DbType.String, Name = "SchemaName", Size = 75 };
            iParams[3] = new QueryParameter() { DbType = DbType.String, Name = "TableName", Size = 200 };
            iParams[4] = new QueryParameter() { DbType = DbType.String, Name = "GUIDServer", Size = 36, Value = server.GUIDServer };

            DataTable _dt = _db.ExecuteDataTable(SqlPullTables, connStr);

            foreach (DataRow oRow in _dt.Rows)
            {
                bool CreateTblEntry = false;

                DatabaseTable db = Generator.GetItem<DatabaseTable>(oRow);

                iParams[2].Value = db.SchemaName;
                iParams[3].Value = db.Name;

                object result = _db.ExecuteScalar(GetTableGUID, iParams);

                if (result.IsEmptyValue())
                {
                    db.GUIDDatabaseTable = Guid.NewGuid().ToString().ToUpper();
                    CreateTblEntry = true;
                }
                else
                {
                    db.GUIDDatabaseTable = result.ToString();
                }
                iParams[1].Value = db.GUIDDatabaseTable;

                if (CreateTblEntry)
                    _db.ExecuteNonQuery(InsertDBTableSql, iParams);

                _db.ExecuteNonQuery(InsertDb2TableSql, iParams);

            }
        }


    }
}
