using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Utilz
{
    public class Constants
    {

        public enum QueryType
        {
            Empty = 0,
            PullServerStats = 1,
            PullDataBaseStats = 2,
            PullTableStats = 3,            
            
            Structure = 98,
            PopulateSchema = 99,
        }

    }
}
