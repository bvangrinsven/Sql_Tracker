using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Utilz
{
    public class Debugging
    {
        public static void PeekDataTable<T>(ILogger<T> log, DataTable oTable)
        {
            if (log.IsEnabled(LogLevel.Debug))
            {
                int Count = oTable.Columns.Count;

                log.LogDebug("Data Table Columns", "--== Start ==--");

                for (int x = 0; x < Count; x++)
                {
                    log.LogDebug("Column Name: {0} - Data Type: {1} - Value: {2}", oTable.Columns[x].ColumnName, oTable.Columns[x].DataType, oTable.Rows[0][x]);
                }

                log.LogDebug("Data Table Columns", "--== End ==--");
            }
        }


    }
}
