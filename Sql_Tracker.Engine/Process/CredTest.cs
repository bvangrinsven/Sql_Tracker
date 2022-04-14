using Microsoft.Extensions.Logging;
using Sql_Tracker.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Process
{
    public class CredTest : ICredTest
    {
        private ILogger<CredTest> log;
        private ISettings Setting;

        public CredTest(ILogger<CredTest> logger, ISettings settings)
        {
            log = logger;
            Setting = settings;
        }

        public void Execute()
        {

            log.LogInformation("Connection String: {0}", Setting.ConnectionString);
            Setting.ReadAllFromCurrent("MMCDNS01");

        }
    }
}
