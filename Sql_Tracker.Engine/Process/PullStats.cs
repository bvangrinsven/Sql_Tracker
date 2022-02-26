using Microsoft.Extensions.Logging;
using Sql_Tracker.Engine.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Process
{
    public class PullStats : IPullStats
    {
        private ILogger<PullStats> log;
        private IConfig cfg;

        public PullStats(ILogger<PullStats> logger, IConfig config)
        {
            log = logger;
            cfg = config;
        }

        public void Execute()
        {
            log.LogInformation("Starting Pulling of Stats");

            string sqlFilesPath = cfg.GetString("sqlfiles");

            

        }
    }
}
