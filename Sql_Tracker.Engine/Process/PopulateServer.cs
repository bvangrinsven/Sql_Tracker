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
        public bool ShowWizard { get; set; }

        public void Execute()
        {
            
        }


        private void PopulateDatabases(List<Server> servers, List<Database> databases, List<DatabaseTable> databaseTables)
        {

        }

        private void PopulateDatabaseTables(List<Server> servers, List<Database> databases, List<DatabaseTable> databaseTables)
        {

        }


    }
}
