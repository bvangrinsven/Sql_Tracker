using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Models
{
    public class Server
    {
        public string GUIDServer { get; set; }
        public string ServerGUID { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
