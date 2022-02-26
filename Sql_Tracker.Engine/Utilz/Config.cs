using Sql_Tracker.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotenv.net;
using dotenv.net.Utilities;

namespace Sql_Tracker.Engine.Utilz
{
    public class Config : IConfig
    {
        public bool GetBool(string key)
        {
            return EnvReader.GetBooleanValue(key);
        }

        public decimal GetDecimal(string key)
        {
            return EnvReader.GetDecimalValue(key);
        }

        public int GetInt(string key)
        {
            return EnvReader.GetIntValue(key);
        }

        public string GetString(string key)
        {
            return EnvReader.GetStringValue(key);
        }

        public bool HasKey(string key)
        {
            return EnvReader.HasValue(key);
        }
    }
}
