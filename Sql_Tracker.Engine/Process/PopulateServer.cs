using Microsoft.Extensions.Logging;
using Sql_Tracker.Engine.Interfaces;
using Sql_Tracker.Engine.Models;
using System;
using System.Collections.Generic;
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
            
        }


        private void PopulateDatabases(List<Server> servers)
        {
            string Sql = "SELECT db.database_id, db.[name] as DatabaseName FROM sys.databases db WHERE db.database_id > 4";

        }

        private void PopulateDatabaseTables(Server server, Database database)
        {

        }


    }
}
