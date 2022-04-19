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
using Sql_Tracker.Engine.Utilz.Extensions;

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
            
            Server server = null;
            Database database = null;
            DatabaseTable databaseTable = null;

            if (sqlFilesPath.StartsWith("."))
                sqlFilesPath = Path.GetFullPath(Path.Combine(Generator.GetAssemblyPath(), sqlFilesPath));

            List<Server> servers = Generator.ConvertDataTable<Server>(_db.ExecuteDataTable(server.GetAll()));
            List<Database> databases = Generator.ConvertDataTable<Database>(_db.ExecuteDataTable(database.GetAll()));
            List<DatabaseTable> databaseTables = Generator.ConvertDataTable<DatabaseTable>(_db.ExecuteDataTable(databaseTable.GetAll()));

            List<QueryFileInfo> queryFiles = new List<QueryFileInfo>();

            log.LogInformation("Scanning Path: {0}", sqlFilesPath);

            log.LogInformation("Gather All Files:");

            foreach (string queryFile in Generator.GetFiles(sqlFilesPath, "*.json"))
            {
                QueryFileInfo qFileInfo = CreateObject.PopQueryFileInfoFromFile(queryFile);
                queryFiles.Add(qFileInfo);
            }

            DateTime ReportDate = DateTime.Now;

            QueryParameter[] iParams = new QueryParameter[6];
            iParams[0] = new QueryParameter() { DbType = DbType.String, Name = "ServerGUID", Size = 36 };
            iParams[1] = new QueryParameter() { DbType = DbType.String, Name = "DatabaseGUID", Size = 36 };
            iParams[2] = new QueryParameter() { DbType = DbType.String, Name = "DatabaseTableGUID", Size = 36 };
            iParams[3] = new QueryParameter() { DbType = DbType.DateTime, Name = "DateReported", Value = ReportDate };
            iParams[4] = new QueryParameter() { DbType = DbType.Int32, Name = "MonthReported", Value = ReportDate.Month };
            iParams[5] = new QueryParameter() { DbType = DbType.Int32, Name = "YearReported", Value = ReportDate.Year };
            iParams[6] = new QueryParameter() { DbType = DbType.Int32, Name = "WeekNumReported", Value = ReportDate.GetIso8601WeekOfYear() };

            log.LogInformation("Execute Server Level Stats");
            ExecuteSpecificLevel(servers, databases, databaseTables, queryFiles, Constants.QueryType.PullServerStats, iParams);

            log.LogInformation("Execute Database Level Stats");
            ExecuteSpecificLevel(servers, databases, databaseTables, queryFiles, Constants.QueryType.PullDataBaseStats, iParams);

            log.LogInformation("Execute Table Level Stats");
            ExecuteSpecificLevel(servers, databases, databaseTables, queryFiles, Constants.QueryType.PullTableStats, iParams);

        }


        private void ExecuteSpecificLevel(List<Server> servers, List<Database> databases, List<DatabaseTable> databaseTables, List<QueryFileInfo> queryFiles, Constants.QueryType queryType, QueryParameter[] inParams)
        {

            // Get all Queries to Execute
            List<QueryFileInfo> specificLevel = queryFiles.Where<QueryFileInfo>(x => x.QueryType == queryType).ToList();

            // Loop through the servers
            foreach (Server server in servers)
            {

                inParams[0].Value = server.GUIDServer;

                string connStr = server.ConnectionString
                    .Replace("{{database}}", "master")
                    .Replace("{{monitoruser}}", Setting.GetUsername(server.Name))
                    .Replace("{{monitorpass}}", Setting.GetPassword(server.Name));

                if (queryType == Constants.QueryType.PullServerStats)
                {
                    foreach (QueryFileInfo query in specificLevel)
                    {
                        LogQuery(connStr, query, inParams);
                    }
                }

                if (queryType == Constants.QueryType.PullDataBaseStats)
                {
                    // Loop through the Databases
                    List<Database> _database = databases.Where<Database>(x => x.ServerGUID == server.GUIDServer).ToList();

                    foreach (Database db in _database)
                    {
                        inParams[1].Value = db.GUIDDatabase;

                        connStr = server.ConnectionString
                            .Replace("{{database}}", db.Name)
                            .Replace("{{monitoruser}}", Setting.GetUsername(server.Name))
                            .Replace("{{monitorpass}}", Setting.GetPassword(server.Name));

                        foreach (QueryFileInfo query in specificLevel)
                        {
                            LogQuery(connStr, query, inParams);
                        }

                        // Loop through the Tables
                        // Pull Table Level Stats at a whole to dump into Table
                        /*
                        if (queryType == Constants.QueryType.PullTableStats)
                        {
                            List<DatabaseTable> _dbt = databaseTables.Where<DatabaseTable>(x => x.DatabaseGUID == db.GUIDDatabase).ToList();
                            foreach (DatabaseTable databaseTable in _dbt)
                            {
                                inParams[3].Value = databaseTable.GUIDDatabaseTable;
                                if (queryType == Constants.QueryType.PullTableStats)
                                {
                                    foreach (QueryFileInfo query in specificLevel)
                                    {
                                        LogQuery(connStr, query, db, databaseTable);
                                    }
                                }
                                inParams[3].Value = string.Empty;
                            }
                        }
                        */

                        inParams[1].Value = string.Empty;
                    }
                }

                inParams[0].Value = string.Empty;
            }
        }

        public void LogQuery(string sourceConnStr, QueryFileInfo queryFile, QueryParameter[] inParams)
        {
            string querySql;
            string insertSql;

            if (GetQueryInsertContent(queryFile, out querySql, out insertSql))
            {
                DataTable dt = _db.ExecuteDataTable(querySql, sourceConnStr);

                if (dt.IsValid())
                {
                    _db.ExecuteUpSert(dt, insertSql, queryFile.TableName, inParams);

                    //HandleRows(inParams, insertSql, dt);
                }
            }
        }

        //public void LogQuery(string sourceConnStr, QueryFileInfo queryFile, Database database)
        //{
        //    string querySql;
        //    string insertSql;

        //    if (GetQueryInsertContent(queryFile, out querySql, out insertSql))
        //    {
        //        DataTable dt = _db.ExecuteDataTable(querySql, sourceConnStr);

        //        if (dt.IsValid())
        //        {
        //            QueryParameter[] inParams = Generator.CreateQueryParametersFromDT(dt);
        //            inParams.Prepend(new QueryParameter() { DbType = DbType.String, Name = "DatabaseGUID", Size = 36, Value = database.GUIDDatabase });

        //            //HandleRows(inParams, insertSql, dt);
        //        }
        //    }
        //}

        //public void LogQuery(string sourceConnStr, QueryFileInfo queryFile, Database database, DatabaseTable databaseTable)
        //{
        //    string querySql;
        //    string insertSql;

        //    if (GetQueryInsertContent(queryFile, out querySql, out insertSql))
        //    {
        //        DataTable dt = _db.ExecuteDataTable(querySql, sourceConnStr);

        //        if (dt.IsValid())
        //        {
        //            QueryParameter[] inParams = Generator.CreateQueryParametersFromDT(dt);
        //            inParams.Prepend(new QueryParameter() { DbType = DbType.String, Name = "DatabaseTableGUID", Size = 36, Value = databaseTable.GUIDDatabaseTable });

        //            //HandleRows(inParams, insertSql, dt);
        //        }
        //    }
        //}

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

        //private void HandleRows(QueryParameter[] inParams, string insertSql, DataTable dt)
        //{
        //    foreach (DataRow oRow in dt.Rows)
        //    {
        //        QueryParameter[] outParams = new QueryParameter[inParams.Length];
        //        Array.Copy(inParams, outParams, inParams.Length);

        //        for (int i = 0; i < outParams.Length; i++)
        //        {
        //            outParams[i].Value = oRow[outParams[i].Name];
        //        }

        //        _db.ExecuteNonQuery(insertSql, outParams);
        //    }
        //}

    }
}
