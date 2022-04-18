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

            PopulateServerInfo(servers);

            PopulateDatabases(servers);
        }

        #region Server Level

        private void PopulateServerInfo(List<Server> servers)
        {
            QueryParameter[] iParams = new QueryParameter[1];
            iParams[0] = new QueryParameter() { DbType = DbType.String, Name = "ServerGUID", Size = 36 };

            foreach (Server server in servers)
            {
                iParams[0].Value = server.GUIDServer;

                string connStr = server.ConnectionString
                    .Replace("{{database}}", "master")
                    .Replace("{{monitoruser}}", Setting.GetUsername(server.Name))
                    .Replace("{{monitorpass}}", Setting.GetPassword(server.Name));

                PopulateServerDisk(connStr, server);
                PopulateServerJob(connStr, server);
            }
        }

        private void PopulateServerJob(string connStr, Server server)
        {
            ServerSqlJob dbt = new ServerSqlJob();

            QueryParameter[] iParams = new QueryParameter[1];
            iParams[0] = new QueryParameter() { DbType = DbType.String, Name = "ServerGUID", Size = 36, Value = server.GUIDServer };

            DataTable _dt = _db.ExecuteDataTable(dbt.GetObjectsSql(), connStr, iParams);

            _db.ExecuteSqlList(dbt.GetPreUpsertSql());
            _db.ExecuteUpSert(_dt, dbt.GetUpsertSql(), "tblServerJobs", iParams);
            _db.ExecuteSqlList(dbt.GetPostUpsertSql());
        }

        private void PopulateServerDisk(string connStr, Server server)
        {
            ServerDisk dbt = new ServerDisk();

            QueryParameter[] iParams = new QueryParameter[1];
            iParams[0] = new QueryParameter() { DbType = DbType.String, Name = "ServerGUID", Size = 36, Value = server.GUIDServer };

            DataTable _dt = _db.ExecuteDataTable(dbt.GetObjectsSql(), connStr, iParams);

            _db.ExecuteSqlList(dbt.GetPreUpsertSql());
            _db.ExecuteUpSert(_dt, dbt.GetUpsertSql(), "tblServerDisk", iParams);
            _db.ExecuteSqlList(dbt.GetPostUpsertSql());
        }

        #endregion

        #region Database Level

        private void PopulateDatabases(List<Server> servers)
        {
            Database db = new Database();

            QueryParameter[] iParams = new QueryParameter[1];
            iParams[0] = new QueryParameter() { DbType = DbType.String, Name = "ServerGUID", Size = 36 };


            foreach (Server server in servers)
            {
                log.LogInformation("Server: {0}", server.Name);

                iParams[0].Value = server.GUIDServer;

                string connStr = server.ConnectionString
                    .Replace("{{database}}", "master")
                    .Replace("{{monitoruser}}", Setting.GetUsername(server.Name))
                    .Replace("{{monitorpass}}", Setting.GetPassword(server.Name));

                DataTable _dt = _db.ExecuteDataTable(db.GetObjectsSql(), connStr, iParams);

                _db.ExecuteSqlList(db.GetPreUpsertSql());
                _db.ExecuteUpSert(_dt, db.GetUpsertSql(), "tblDatabases");
                _db.ExecuteSqlList(db.GetPostUpsertSql());

                List<Database> oDatabases = Generator.ConvertDataTable<Database>(_dt);

                foreach (Database oDatabase in oDatabases)
                {
                    log.LogInformation("  Database: {0}", oDatabase.Name);
                    PopulateDatabaseTables(server, oDatabase);
                }
            }
        }

        private void PopulateDatabaseTables(Server server, Database database)
        {
            DatabaseTable dbt = new DatabaseTable();

            string connStr = server.ConnectionString
                .Replace("{{database}}", database.Name)
                .Replace("{{monitoruser}}", Setting.GetUsername(server.Name))
                .Replace("{{monitorpass}}", Setting.GetPassword(server.Name));

            QueryParameter[] iParams = new QueryParameter[2];
            iParams[0] = new QueryParameter() { DbType = DbType.String, Name = "ServerGUID", Size = 36, Value = server.GUIDServer };
            iParams[1] = new QueryParameter() { DbType = DbType.String, Name = "DatabaseName", Size = 200, Value = database.Name };

            DataTable _dt = _db.ExecuteDataTable(dbt.GetObjectsSql(), connStr, iParams);

            _db.ExecuteSqlList(dbt.GetPreUpsertSql());
            _db.ExecuteUpSert(_dt, dbt.GetUpsertSql(), "tblDatabaseTables", iParams);
            _db.ExecuteSqlList(dbt.GetPostUpsertSql());
        }

        #endregion


    }
}
