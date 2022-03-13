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

        public PullStats(ILogger<PullStats> logger, ISettings settings, IDBExecute dB)
        {
            log = logger;
            Setting = settings;
            _db = dB;
        }

        public void Execute()
        {
            log.LogInformation("Starting Pulling of Stats");

            log.LogInformation("Starting Initialization of Database");

            string sqlFilesPath = Setting.SqlFiles;

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
            foreach (Server item in servers)
            {

                // Loop through the Databases
                List<Database> _database = databases.Where<Database>(x => x.ServerGUID == item.GUIDServer).ToList();

                foreach(Database db in _database)
                {
                    List<DatabaseTable> _dbt = databaseTables.Where<DatabaseTable>(x => x.DatabaseGUID == db.GUIDDatabase).ToList();
                    // Loop through the Tables

                    foreach (DatabaseTable databaseTable in _dbt)
                    {



                    }

                }
            }
        }
    }
}
