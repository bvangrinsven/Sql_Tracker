using Sql_Tracker.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Utilz
{
    internal class Settings : ISettings
    {
        IConfig _config;

        public Settings(IConfig config)
        {
            _config = config;

            ConnectionString = _config.GetString("connectionstring");

            SqlFiles = _config.GetString("sqlfiles");
        }

        public string ConnectionString { get; private set; }
        public string SqlFiles { get; set; }
    }
}
