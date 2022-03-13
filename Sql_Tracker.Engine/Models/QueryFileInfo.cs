using Sql_Tracker.Engine.Utilz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Models
{

    public class QueryFileInfo
    {
        public string Id { get; set; }
        public Constants.QueryType QueryType {  get; set; }
        public string Name { get; set; }
        public string QueryFile { get; set; }
        public string ExistsTableFile { get; set; }
        public string CreateTableFile { get; set; }
        public string InsertRecordFile { get; set; }
        public string TableName {get; set; }

    }
}
