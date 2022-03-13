using Microsoft.Extensions.Logging;
using Sql_Tracker.Engine.Interfaces;
using Sql_Tracker.Engine.Utilz;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sql_Tracker.Engine.Models;
using Sql_Tracker.Engine.Factories;

namespace Sql_Tracker.Engine.Process
{
    internal class InitDB : IInitDB
    {
        private ILogger<InitDB> log;
        private ISettings Setting;
        private IDBExecute _db;

        public InitDB(ILogger<InitDB> logger, ISettings settings, IDBExecute dB)
        {
            log = logger;
            Setting = settings;
            _db = dB;
        }

        public void Execute()
        {
            log.LogInformation("Starting Initialization of Database");

            string sqlFilesPath = Setting.SqlFiles;

            if (sqlFilesPath.StartsWith("."))
                sqlFilesPath = Path.GetFullPath(Path.Combine(Generator.GetAssemblyPath(), sqlFilesPath));

            log.LogInformation("Scanning Path: {0}", sqlFilesPath);

            log.LogInformation("Files:");

            foreach (string queryFile in Generator.GetFiles(sqlFilesPath, "*.json"))
            {
                log.LogInformation("    {0}", queryFile);

                QueryFileInfo qFileInfo = CreateObject.PopQueryFileInfoFromFile(queryFile);

                string createTableSql = Path.Combine(sqlFilesPath, qFileInfo.CreateTableFile);
                string existsTableSql = Path.Combine(sqlFilesPath, qFileInfo.ExistsTableFile);

                log.LogInformation("        id: {0}", qFileInfo.Id);
                log.LogInformation("        Name: {0}", qFileInfo.Name);
                log.LogInformation("        Type: {0}", qFileInfo.QueryType);
                log.LogInformation("        CreateTableFile: {0}", createTableSql);
                log.LogInformation("        ExistsTableFile: {0}", existsTableSql);

                if (createTableSql.Length > 0 && existsTableSql.Length > 0)
                {
                    bool createTable = false;
                    string existsSql = File.ReadAllText(existsTableSql);
                    string createSql = File.ReadAllText(createTableSql);

                    object result = _db.ExecuteScalar(existsSql);
                    int numTables = 0;

                    if (int.TryParse(result.ToString(), out numTables))
                    {
                        if (numTables == 0)
                            createTable = true;
                    }

                    if (createTable == true)
                    {
                        string[] splitter = new string[] { "\r\nGO" };
                        string[] commandTexts = createSql.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string commandText in commandTexts)
                        {
                            try
                            {
                                _db.ExecuteNonQuery(commandText);
                            }
                            catch (Exception ex)
                            {
                                log.LogError(ex, "Error during this command: {0}", commandText);
                            }
                            
                        }                        
                    }

                }
            }

        }
    }
}
