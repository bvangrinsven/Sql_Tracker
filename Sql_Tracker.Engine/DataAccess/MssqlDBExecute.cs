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
using PetaPoco;

namespace Sql_Tracker.Engine.DataAccess

{
    public class MssqlDBExecute : IDBExecute
    {
        public MssqlDBExecute(ISettings settings)
        {
            ConnectionString = settings.ConnectionString;


            pdb = new PetaPoco.Database(ConnectionString);
        }

        public string ConnectionString { get; set; }

        public PetaPoco.Database pdb { get; set; }

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
                sqlParameter = new SqlParameter(queryParameter.Name, queryParameter.Value);
            else
                sqlParameter = new SqlParameter(queryParameter.Name, DBNull.Value);

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


    }
}
