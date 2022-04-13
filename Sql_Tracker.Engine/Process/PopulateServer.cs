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
        private ICreds _creds;

        public PopulateServer(ILogger<PopulateServer> logger, ISettings settings, IDBExecute dB, ICreds creds)
        {
            log = logger;
            Setting = settings;
            _db = dB;
            _creds = creds;
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
            string TablesSql = "SELECT db.[Name] FROM sys.databases db WHERE db.database_id > 4";

            string GetDatabaseGUID = "SELECT GUIDDatabase FROM tblDatabases WHERE [ServerGUID] = @ServerGUID AND [Name] = @Name";
            
            string InsertSql = "INSERT INTO [dbo].[tblDatabases] ([ServerGUID], [GUIDDatabase],[Name]) VALUES (@GUIDServer, @GUIDDatabase, @Name); INSERT INTO likes VALUES((SELECT $node_id FROM tblServers WHERE GUIDServer = @GUIDServer), (SELECT $node_id FROM tblDatabases WHERE GUIDDatabase = @GUIDDatabase AND ServerGUID = @GUIDServer));";

            QueryParameter[] gdg = new QueryParameter[2];
            gdg[0] = new QueryParameter() { DbType = DbType.String, Name = "ServerGUID", Size = 36 };
            gdg[1] = new QueryParameter() { DbType = DbType.String, Name = "Name", Size = 200 };

            QueryParameter[] iParams = new QueryParameter[3];
            iParams[0] = new QueryParameter() { DbType = DbType.String, Name = "GUIDServer", Size = 36 };
            iParams[1] = new QueryParameter() { DbType = DbType.String, Name = "GUIDDatabase", Size = 36 };
            iParams[2] = new QueryParameter() { DbType = DbType.String, Name = "Name", Size = 200 };

            foreach (Server server in servers)
            {
                gdg[0].Value = server.GUIDServer;
                iParams[0].Value = server.GUIDServer;

                string connStr = server.ConnectionString
                    .Replace("{{monitoruser}}", _creds.GetUsername(server.Name))
                    .Replace("{{monitorpassword}}", _creds.GetPassword(server.Name));

                DataTable _dt = _db.ExecuteDataTable(TablesSql, connStr);

                foreach (DataRow oRow in _dt.Rows)
                {
                    Database db = Generator.GetItem<Database>(oRow);

                    gdg[1].Value = db.Name;

                    db.GUIDDatabase = _db.ExecuteScalar(GetDatabaseGUID, gdg).ToString();

                    if (db.GUIDDatabase.IsEmptyValue())
                    {
                        db.GUIDDatabase = Guid.NewGuid().ToString();
                    }

                    iParams[1].Value = db.GUIDDatabase;
                    iParams[2].Value = oRow.GetString("DatabaseName");

                    _db.ExecuteNonQuery(InsertSql, iParams);

                    PopulateDatabaseTables(server, db);

                }


            }


        }

        private void PopulateDatabaseTables(Server server, Database database)
        {

        }


    }
}
