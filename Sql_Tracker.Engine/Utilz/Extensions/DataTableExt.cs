using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Utilz.Extensions
{
    public static class DataTableExt
    {

        public static bool IsValid(this DataTable oDataTable)
        {

            //Logging.Write(false, "oDataTable != null", oDataTable != null);
            //Logging.Write(false, "oDataTable.Rows != null", oDataTable.Rows != null);
            //Logging.Write(false, "oDataTable.Rows.Count", oDataTable.Rows.Count);

            if (oDataTable != null && oDataTable.Rows != null && oDataTable.Rows.Count > 0)
                return true;
            else
                return false;
        }

    }
}
