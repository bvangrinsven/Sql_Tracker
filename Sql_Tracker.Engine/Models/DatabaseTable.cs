using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Models
{
    public class DatabaseTable
    {
        public string GUIDDatabaseTable { get; set; }
        public string DatabaseGUID { get; set; }
        public string SchemaName { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
