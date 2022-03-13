using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Sql_Tracker.Engine.Models;

namespace Sql_Tracker.Engine.Interfaces
{
    public interface IDBExecute
    {
        string ConnectionString { get; set; }

        int ExecuteNonQuery(string sql);
        int ExecuteNonQuery(string sql, params QueryParameter[] parameters);
        DataTable ExecuteDataTable(string sql, params QueryParameter[] parameters);
        object ExecuteScalar(string sql);
        object ExecuteScalar(string sql, params QueryParameter[] parameters);
    }
}
