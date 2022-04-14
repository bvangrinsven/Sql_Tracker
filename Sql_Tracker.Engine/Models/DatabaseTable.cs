using Sql_Tracker.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Models
{
    public class DatabaseTable : IModel
    {
        public string GUIDDatabaseTable { get; set; }
        public string ServerGUID { get; set; }
        public string DatabaseGUID { get; set; }
        public string DatabaseName { get; set;  }
        public string SchemaName { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public string GetObjectsSql()
        {
            throw new NotImplementedException();
        }

        public string[] GetPostUpsertSql()
        {
            List<string> routines = new List<string>();

            return routines.ToArray();
        }

        public string[] GetPreUpsertSql()
        {
            List<string> routines = new List<string>();

            return routines.ToArray();
        }

        public string GetRelatedSql()
        {
            throw new NotImplementedException();
        }

        public string GetUpsertSql()
        {
            throw new NotImplementedException();
        }
    }
}
