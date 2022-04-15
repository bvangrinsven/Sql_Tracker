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
            return "SELECT @ServerGUID as ServerGUID, " +
                "'' as DatabaseGUID, " +
                "@DatabaseName as [DatabaseName], " +
                "'' as GUIDDatabaseTable, " +
                "SCHEMA_NAME(t.schema_id) as SchemaName, " +
                "t.NAME AS [Name], " +
                "0 as [IsDeleted], " +
                "t.create_date as DateCreated, " +
                "t.modify_date as DateModified " +
                "FROM sys.tables AS t " +
                "WHERE ([name] NOT LIKE 'dt%') AND (is_ms_shipped = 0)";
        }

        public string[] GetPostUpsertSql()
        {
            List<string> routines = new List<string>();

            string Sql = "";

            // Update the Database GUID
            Sql = "UPDATE dbt SET dbt.DatabaseGUID = dbs.GUIDDatabase FROM tblDatabaseTables as dbt INNER JOIN tblDatabases as dbs ON dbt.ServerGUID = dbs.ServerGUID AND dbt.DatabaseName = dbs.[Name]";
            routines.Add(Sql);

            // Populate Database to Table Relation
            Sql = "INSERT INTO [dbo].[tblDatabaseToTable] ($from_id, $to_id) " + Environment.NewLine;
            Sql += "SELECT dbs.$node_id as fromid, dbt.$node_id as toid " + Environment.NewLine;
            Sql += "FROM tblDatabases as dbs " + Environment.NewLine;
            Sql += "INNER JOIN tblDatabaseTables as dbt ON dbs.GUIDDatabase = dbt.DatabaseGUID " + Environment.NewLine;
            Sql += "LEFT OUTER JOIN [tblDatabaseToTable] as std ON dbs.$node_id = std.$from_id AND dbt.$node_id = std.$to_id " + Environment.NewLine;
            Sql += "WHERE std.$edge_id IS NULL " + Environment.NewLine;
            routines.Add(Sql);

            return routines.ToArray();
        }

        public string[] GetPreUpsertSql()
        {
            List<string> routines = new List<string>();

            return routines.ToArray();
        }

        public string GetUpsertSql()
        {
            string Sql = "BEGIN TRANSACTION; " + Environment.NewLine;
            Sql += " " + Environment.NewLine;
            Sql += "UPDATE tD SET tD.[ServerGUID] = tS.[ServerGUID] " + Environment.NewLine;
            Sql += ", tD.[DatabaseGUID] = tS.[DatabaseGUID] " + Environment.NewLine;
            Sql += ", tD.[DatabaseName] = tS.[DatabaseName] " + Environment.NewLine;
            Sql += ", tD.[SchemaName] = tS.[SchemaName] " + Environment.NewLine;
            Sql += ", tD.[Name] = tS.[Name] " + Environment.NewLine;
            Sql += ", tD.[IsDeleted] = tS.[IsDeleted] " + Environment.NewLine;
            Sql += ", tD.[DateCreated] = tS.[DateCreated] " + Environment.NewLine;
            Sql += ", tD.[DateModified] = tS.[DateModified] " + Environment.NewLine;
            Sql += " " + Environment.NewLine;
            Sql += "FROM [dbo].[tblDatabaseTables] as tD " + Environment.NewLine;
            Sql += "INNER JOIN @inputTable as tS ON tD.[ServerGUID] = tS.[ServerGUID] " + Environment.NewLine;
            Sql += "AND tD.[DatabaseName] = tS.[DatabaseName] " + Environment.NewLine;
            Sql += "AND tD.[SchemaName] = tS.[SchemaName] " + Environment.NewLine;
            Sql += "AND tD.[Name] = tS.[Name]; " + Environment.NewLine;
            Sql += " " + Environment.NewLine;
            Sql += "INSERT INTO [dbo].[tblDatabaseTables] ( " + Environment.NewLine;
            Sql += "[ServerGUID], [DatabaseGUID], [DatabaseName], [SchemaName], [Name], [IsDeleted], [DateCreated], [DateModified] " + Environment.NewLine;
            Sql += ")  " + Environment.NewLine;
            Sql += "SELECT [tS].[ServerGUID], [tS].[DatabaseGUID], [tS].[DatabaseName], [tS].[SchemaName], [tS].[Name], [tS].[IsDeleted], [tS].[DateCreated], [tS].[DateModified] " + Environment.NewLine;
            Sql += "FROM @inputTable as tS " + Environment.NewLine;
            Sql += "LEFT OUTER JOIN [dbo].[tblDatabaseTables] as tD ON tD.[ServerGUID] = tS.[ServerGUID] " + Environment.NewLine;
            Sql += "AND tD.[DatabaseName] = tS.[DatabaseName] " + Environment.NewLine;
            Sql += "AND tD.[SchemaName] = tS.[SchemaName] " + Environment.NewLine;
            Sql += "AND tD.[Name] = tS.[Name] " + Environment.NewLine;
            Sql += "WHERE tD.[GUIDDatabaseTable] IS NULL; " + Environment.NewLine;
            Sql += " " + Environment.NewLine;
            Sql += "COMMIT TRANSACTION; " + Environment.NewLine;

            return Sql;
        }
    }
}
