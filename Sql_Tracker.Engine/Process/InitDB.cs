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

                log.LogInformation("        id: {0}", qFileInfo.Id);
                log.LogInformation("        Name: {0}", qFileInfo.Name);
                log.LogInformation("        CreateTableFile: {0}", createTableSql);

                if (createTableSql.Length > 0)
                {

                    string createSql = File.ReadAllText(createTableSql);

                    _db.ExecuteNonQuery(createSql);

                }
            }

        }
    }
}
