using Microsoft.Extensions.Logging;
using Sql_Tracker.Engine.Interfaces;
using Sql_Tracker.Engine.Utilz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Process
{
    internal class InitDB : IInitDB
    {
        private ILogger<InitDB> log;
        private IConfig cfg;

        public InitDB(ILogger<InitDB> logger, IConfig config)
        {
            log = logger;
            cfg = config;
        }

        public void Execute()
        {
            log.LogInformation("Starting Initialization of Database");

            string sqlFilesPath = cfg.GetString("sqlfiles");

            log.LogInformation("Files:");
            foreach (string queryFile in Generator.GetFiles(sqlFilesPath, ".json"))
            {
                log.LogInformation("    {0}", queryFile);
            }

        }
    }
}
