using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Interfaces
{
    public interface IConfig
    {
        bool HasKey(string key);
        string GetString(string key);
        decimal GetDecimal(string key);
        int GetInt(string key);
        bool GetBool(string key);

    }
}
