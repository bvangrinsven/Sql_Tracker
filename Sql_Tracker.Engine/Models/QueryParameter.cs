using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Models
{
    [Serializable]
    public class QueryParameter
    {
        public string Name { get; set; } = string.Empty;
        public object Value { get; set; } = string.Empty;
        public DbType DbType { get; set; } = DbType.String;
        public int Size { get; set; } = 0;

        public string ParamName { get { return "@" + Name; } }
    }
}
