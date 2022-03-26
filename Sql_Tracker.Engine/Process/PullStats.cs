using Microsoft.Extensions.Logging;
using Sql_Tracker.Engine.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sql_Tracker.Engine.Utilz;
using Sql_Tracker.Engine.Factories;
using Sql_Tracker.Engine.Models;
using System.Data;

namespace Sql_Tracker.Engine.Process
{
    public class PullStats : IPullStats
    {
        private ILogger<PullStats> log;
        private ISettings Setting;
        private IDBExecute _db;
        private ProcessLog processLog = new ProcessLog();
        private string sqlFilesPath = string.Empty;

        public PullStats(ILogger<PullStats> logger, ISettings settings, IDBExecute dB)
        {
            log = logger;
            Setting = settings;
            _db = dB;
            sqlFilesPath = Setting.SqlFiles;
        }

        public void Execute()
        {
            log.LogInformation("Starting Pulling of Stats");

            log.LogInformation("Starting Initialization of Database");

            if (sqlFilesPath.StartsWith("."))
                sqlFilesPath = Path.GetFullPath(Path.Combine(Generator.GetAssemblyPath(), sqlFilesPath));

            List<Server> servers = Generator.ConvertDataTable<Server>(_db.ExecuteDataTable(Constants.Queries.ServerList));
            List<Database> databases = Generator.ConvertDataTable<Database>(_db.ExecuteDataTable(Constants.Queries.DatabaseList));
            List<DatabaseTable> databaseTables = Generator.ConvertDataTable<DatabaseTable>(_db.ExecuteDataTable(Constants.Queries.TableList));

            List<QueryFileInfo> queryFiles = new List<QueryFileInfo>();

            log.LogInformation("Scanning Path: {0}", sqlFilesPath);

            log.LogInformation("Gather All Files:");

            foreach (string queryFile in Generator.GetFiles(sqlFilesPath, "*.json"))
            {
                QueryFileInfo qFileInfo = CreateObject.PopQueryFileInfoFromFile(queryFile);
                queryFiles.Add(qFileInfo);
            }

            log.LogInformation("Execute Server Level Stats");
            ExecuteSpecificLevel(servers, databases, databaseTables, queryFiles, Constants.QueryType.PullServerStats);

            log.LogInformation("Execute Database Level Stats");
            ExecuteSpecificLevel(servers, databases, databaseTables, queryFiles, Constants.QueryType.PullDataBaseStats);

            log.LogInformation("Execute Table Level Stats");
            ExecuteSpecificLevel(servers, databases, databaseTables, queryFiles, Constants.QueryType.PullTableStats);

        }


        private void ExecuteSpecificLevel(List<Server> servers, List<Database> databases, List<DatabaseTable> databaseTables, List<QueryFileInfo> queryFiles, Constants.QueryType queryType)
        {

            // Get all Queries to Execute
            List<QueryFileInfo> specificLevel = queryFiles.Where<QueryFileInfo>(x => x.QueryType == queryType).ToList();

            // Loop through the servers
            foreach (Server server in servers)
            {

                if (queryType == Constants.QueryType.PullServerStats)
                {
                    foreach (QueryFileInfo query in specificLevel)
                    {
                        LogQuery(query, server);
                    }
                }

                // Loop through the Databases
                List<Database> _database = databases.Where<Database>(x => x.ServerGUID == server.GUIDServer).ToList();

                foreach (Database db in _database)
                {
                    if (queryType == Constants.QueryType.PullDataBaseStats)
                    {
                        foreach (QueryFileInfo query in specificLevel)
                        {
                            LogQuery(query, db);
                        }
                    }

                    List<DatabaseTable> _dbt = databaseTables.Where<DatabaseTable>(x => x.DatabaseGUID == db.GUIDDatabase).ToList();
                    // Loop through the Tables

                    foreach (DatabaseTable databaseTable in _dbt)
                    {
                        if (queryType == Constants.QueryType.PullTableStats)
                        {
                            foreach (QueryFileInfo query in specificLevel)
                            {
                                LogQuery(query, db, databaseTable);
                            }
                        }
                    }

                }
            }
        }

        public void LogQuery(QueryFileInfo queryFile, Server server)
        {
            string querySql;
            string insertSql;

            if (GetQueryInsertContent(queryFile, out querySql, out insertSql))
            {
                DataTable dt = _db.ExecuteDataTable(querySql, server.ConnectionString);

                if (dt != null && dt.Rows.Count > 0)
                {
                    QueryParameter[] inParams = Generator.CreateQueryParametersFromDT(dt);
                    inParams.Prepend(new QueryParameter() { DbType = DbType.String, Name = "ServerGUID", Size = 36, Value = server.GUIDServer });

                    HandleRows(inParams, insertSql, dt);
                }
            }
        }

        public void LogQuery(QueryFileInfo queryFile, Database database)
        {
            string querySql;
            string insertSql;

            if (GetQueryInsertContent(queryFile, out querySql, out insertSql))
            {
                DataTable dt = _db.ExecuteDataTable(querySql, database.ConnectionString);

                if (dt != null && dt.Rows.Count > 0)
                {
                    QueryParameter[] inParams = Generator.CreateQueryParametersFromDT(dt);
                    inParams.Prepend(new QueryParameter() { DbType = DbType.String, Name = "DatabaseGUID", Size = 36, Value = database.GUIDDatabase });

                    HandleRows(inParams, insertSql, dt);
                }
            }
        }

        public void LogQuery(QueryFileInfo queryFile, Database database, DatabaseTable databaseTable)
        {
            string querySql;
            string insertSql;

            if (GetQueryInsertContent(queryFile, out querySql, out insertSql))
            {
                DataTable dt = _db.ExecuteDataTable(querySql, database.ConnectionString);

                if (dt != null && dt.Rows.Count > 0)
                {
                    QueryParameter[] inParams = Generator.CreateQueryParametersFromDT(dt);
                    inParams.Prepend(new QueryParameter() { DbType = DbType.String, Name = "DatabaseTableGUID", Size = 36, Value = databaseTable.GUIDDatabaseTable });

                    HandleRows(inParams, insertSql, dt);
                }
            }
        }

        private bool GetQueryInsertContent(QueryFileInfo queryFile, out string querySql, out string insertSql)
        {
            string queryFileSql = Path.Combine(sqlFilesPath, queryFile.QueryFile);
            string insertFileSql = Path.Combine(sqlFilesPath, queryFile.InsertRecordFile);

            if (queryFileSql.Length > 0 && insertFileSql.Length > 0)
            {
                querySql = File.ReadAllText(queryFileSql);
                insertSql = File.ReadAllText(insertFileSql);
                return true;
            }
            else
            {
                querySql = string.Empty;
                insertSql = string.Empty;
                return false;
            }
        }

        private void HandleRows(QueryParameter[] inParams, string insertSql, DataTable dt)
        {
            foreach (DataRow oRow in dt.Rows)
            {
                QueryParameter[] outParams = new QueryParameter[inParams.Length];
                Array.Copy(inParams, outParams, inParams.Length);

                for (int i = 0; i < outParams.Length; i++)
                {
                    outParams[i].Value = oRow[outParams[i].Name];
                }

                _db.ExecuteNonQuery(insertSql, outParams);
            }
        }

    }
}
