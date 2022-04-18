using Sql_Tracker.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Sql_Tracker.Engine.Models;
using Sql_Tracker.Engine.Utilz;
using Sql_Tracker.Engine.Utilz.Extensions;
using Microsoft.Extensions.Logging;

namespace Sql_Tracker.Engine.DataAccess

{
    public class MssqlDBExecute : IDBExecute
    {
        private ILogger<MssqlDBExecute> log;

        public MssqlDBExecute(ISettings settings, ILogger<MssqlDBExecute> logger)
        {
            log = logger;
            ConnectionString = settings.ConnectionString;
        }

        public string ConnectionString { get; set; }

        public DataTable ExecuteDataTable(string sql, params QueryParameter[] parameters)
        {
            SqlParameter[] oSqlParameters = null;

            if (parameters.Length > 0)
            {
                oSqlParameters = new SqlParameter[parameters.Length];
                int x = 0;

                foreach (var parameter in parameters)
                {
                    oSqlParameters[x] = ConvertFromGeneric(parameter);
                    x++;
                }
            }

            return ExecuteDataTable(ConnectionString, CommandType.Text, sql, oSqlParameters);
        }

        public DataTable ExecuteDataTable(string sql, string connectionstring, params QueryParameter[] parameters)
        {
            SqlParameter[] oSqlParameters = null;

            if (parameters.Length > 0)
            {
                oSqlParameters = new SqlParameter[parameters.Length];
                int x = 0;

                foreach (var parameter in parameters)
                {
                    oSqlParameters[x] = ConvertFromGeneric(parameter);
                    x++;
                }
            }

            return ExecuteDataTable(connectionstring, CommandType.Text, sql, oSqlParameters);
        }

        public int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(ConnectionString, sql);
        }

        public int ExecuteNonQuery(string sql, params QueryParameter[] parameters)
        {
            return ExecuteNonQuery(ConnectionString, sql, parameters);
        }

        public int ExecuteNonQuery(string connectionString, string sql, params QueryParameter[] parameters)
        {
            SqlParameter[] oSqlParameters = null;

            if (parameters.Length > 0)
            {
                oSqlParameters = new SqlParameter[parameters.Length];
                int x = 0;

                foreach (var parameter in parameters)
                {
                    oSqlParameters[x] = ConvertFromGeneric(parameter);
                    x++;
                }
            }

            //pass through the call providing null for the set of SqlParameters
            //return ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[])null);
            return ExecuteNonQuery(connectionString, CommandType.Text, sql, oSqlParameters);
        }

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                SqlTransaction tran = cn.BeginTransaction();
                int retval = 0;

                try
                {
                    //create a command and prepare it for execution
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandTimeout = 3600;
                    PrepareCommand(cmd, cn, tran, commandType, commandText, commandParameters);

                    //finally, execute the command.
                    retval = cmd.ExecuteNonQuery();

                    // detach the SqlParameters from the command object, so they can be used again.
                    cmd.Parameters.Clear();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                    tran.Rollback();
                }

                return retval;
            }
        }

        public DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    DataSet ds = null;
                    ds = ExecuteDataset(cn, commandType, commandText, commandParameters);
                    if ((ds != null))
                        if (ds.Tables.Count > 0)
                            return ds.Tables[0];

                    return null;
                }
            }
            catch (Exception ex)
            {
                if (ex.Data.Contains("SqlStatement"))
                {
                    ex.Data["SqlStatement"] += "\r\n\r\n\r\n\r\n" + commandText;
                }
                else
                {
                    ex.Data.Add("SqlStatement", commandText);
                }
                throw ex;
            }
        }

        public DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 6000;

            //cmd.Parameters.AddWithValue("@RecordID", CustomerID);

            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

            //create the DataAdapter & DataSet
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            //fill the DataSet using default values for DataTable names, etc.
            da.Fill(ds);

            // detach the SqlParameters from the command object, so they can be used again.			
            cmd.Parameters.Clear();

            //return the dataset
            return ds;
        }

        private void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //associate the connection with the command
            command.Connection = connection;

            //set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            //set the command type
            command.CommandType = commandType;

            //attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }

            return;
        }

        private void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }

                command.Parameters.Add(p);
            }
        }

        private SqlParameter ConvertFromGeneric(QueryParameter queryParameter)
        {
            SqlParameter sqlParameter = new SqlParameter();

            if (queryParameter.Value != null)
                sqlParameter = new SqlParameter(queryParameter.ParamName, queryParameter.Value);
            else
                sqlParameter = new SqlParameter(queryParameter.ParamName, DBNull.Value);

            sqlParameter.SqlDbType = TypeConvertor.ToSqlDbType(queryParameter.DbType);

            if (queryParameter.Size != 0)
                sqlParameter.Size = queryParameter.Size;

            return sqlParameter;
        }

        public object ExecuteScalar(string sql)
        {
            return ExecuteScalar(ConnectionString, sql);
        }

        public object ExecuteScalar(string sql, params QueryParameter[] parameters)
        {
            return ExecuteScalar(ConnectionString, sql, parameters);
        }

        public object ExecuteScalar(string connectionString, string sql)
        {
            return ExecuteScalar(connectionString, sql, null);
        }

        public object ExecuteScalar(string connectionString, string sql, params QueryParameter[] parameters)
        {

            SqlParameter[] oSqlParameters = null;

            if (parameters != null && parameters.Length > 0)
            {
                oSqlParameters = new SqlParameter[parameters.Length];
                int x = 0;

                foreach (var parameter in parameters)
                {
                    oSqlParameters[x] = ConvertFromGeneric(parameter);
                    x++;
                }
            }

            //pass through the call providing null for the set of SqlParameters
            //return ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[])null);
            return ExecuteScalar(connectionString, CommandType.Text, sql, oSqlParameters);
        }

        public object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                object retval = new object();

                try
                {
                    //create a command and prepare it for execution
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandTimeout = 3600;
                    PrepareCommand(cmd, cn, null, commandType, commandText, commandParameters);

                    //finally, execute the command.
                    retval = cmd.ExecuteScalar();

                    // detach the SqlParameters from the command object, so they can be used again.
                    cmd.Parameters.Clear();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                }

                return retval;
            }
        }

        public void ExecuteUpSert(DataTable sourceData, string UpSertSql, string DataTableTypeName, params QueryParameter[] parameters)
        {
            ExecuteUpSert(ConnectionString, sourceData, UpSertSql, DataTableTypeName, parameters);
        }

        public void ExecuteUpSert(string connectionString, DataTable sourceData, string UpSertSql, string DataTableTypeName, params QueryParameter[] parameters)
        {
            log.LogInformation("     ExecuteUpsert - Start");
            if (UpSertSql.IsNotEmpty())
            {
                log.LogInformation("    - sourceData.IsValid() = {0}", sourceData.IsValid());

                if (sourceData.IsValid())
                {

                    Console.WriteLine("    >> Pushing {0} - {1} to the Destination Database.", sourceData.Rows.Count, DataTableTypeName);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Configure the command and parameter.  
                        SqlCommand insertCommand = new SqlCommand(UpSertSql, connection);
                        insertCommand.CommandTimeout = 9000;
                        SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@inputTable", sourceData);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo." + DataTableTypeName;

                        SqlParameter[] oSqlParameters = null;

                        if (parameters != null && parameters.Length > 0)
                        {
                            oSqlParameters = new SqlParameter[parameters.Length];
                            int x = 0;

                            foreach (var parameter in parameters)
                            {
                                oSqlParameters[x] = ConvertFromGeneric(parameter);
                                x++;
                            }
                        }

                        // Execute the command.  
                        insertCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    log.LogInformation("  - Nothing to Process");
                }


                log.LogInformation("---- Finished - UpSert ---");

            }

            log.LogInformation("    ExecuteUpSert - End ==-- ");

        }

        public void ExecuteSqlList(string[] oSqlList)
        {
            ExecuteSqlList(ConnectionString, oSqlList);
        }

        public void ExecuteSqlList(string connectionstring, string[] oSqlList)
        {
            if (oSqlList != null && oSqlList.Length > 0)
            {
                log.LogDebug("--- Start - Sql List ---");
                int i = 1;
                foreach (string oSql in oSqlList)
                {
                    log.LogInformation("  >  Update {0} / {1}", i, oSqlList.Length);
                    if (oSql.IsNotEmpty())
                    {
                        ExecuteNonQuery(connectionstring, oSql);
                    }
                    i++;
                }
                log.LogDebug("--- Finish - Sql List ---");
            }
        }

    }
}
