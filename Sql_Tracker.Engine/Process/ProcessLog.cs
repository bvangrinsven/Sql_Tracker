using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Process
{
    public class ProcessLog
    {
        public string GUIDProcessLog { get; set; }
        public string RunID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ServersProcessed { get; set; }
        public int DatabasesProcessed { get; set; }
        public int TablesProcessed { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
