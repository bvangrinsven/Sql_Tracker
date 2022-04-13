using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Interfaces
{
    public interface ISettings
    {
        string ConnectionString { get; }
        string SqlFiles { get; set; }

        string CredKeyFile { get; }
        string CredFile { get; }

    }
}
