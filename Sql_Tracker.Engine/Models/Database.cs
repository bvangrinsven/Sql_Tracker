using Sql_Tracker.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Models
{
    public class Database : IModel
    {
        public string GUIDDatabase { get; set; }
        public string ServerGUID { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public string GetObjectsSql()
        {
            return "SELECT db.[Name], db.create_date as DateCreated, db.create_date as DateModified, @ServerGUID as ServerGUID FROM sys.databases db WHERE db.database_id > 4";
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
            string Sql = "INSERT INTO [dbo].[tblServerToDatabase] ($from_id, $to_id) " + Environment.NewLine;
            Sql += "SELECT svr.$node_id as fromid, dbs.$node_id as toid " + Environment.NewLine;
            Sql += "FROM tblServers as svr " + Environment.NewLine;
            Sql += "INNER JOIN tblDatabases as dbs ON svr.GUIDServer = dbs.ServerGUID " + Environment.NewLine;
            Sql += "LEFT OUTER JOIN tblServerToDatabase as std ON svr.$node_id = std.$from_id AND dbs.$node_id = std.$to_id " + Environment.NewLine;
            Sql += "WHERE std.$edge_id IS NULL " + Environment.NewLine;

            return Sql;
        }

        public string GetUpsertSql()
        {
            string Sql = "BEGIN TRANSACTION; " + Environment.NewLine;
            Sql += "UPDATE tD SET tD.[ServerGUID] = tS.[ServerGUID] " + Environment.NewLine;
            Sql += ", tD.[Name] = tS.[Name] " + Environment.NewLine;
            Sql += ", tD.[IsDeleted] = tS.[IsDeleted] " + Environment.NewLine;
            Sql += ", tD.[DateCreated] = tS.[DateCreated] " + Environment.NewLine;
            Sql += ", tD.[DateModified] = tS.[DateModified] " + Environment.NewLine;
            Sql += "FROM [dbo].[tblDatabases] as tD " + Environment.NewLine;
            Sql += "INNER JOIN @inputTable as tS ON ttD.[ServerGUID] = tS.[ServerGUID] AND tD.[Name] = tS.[Name] " + Environment.NewLine;
            Sql += "; " + Environment.NewLine;
            Sql += "INSERT INTO [dbo].[tblDatabases] ( " + Environment.NewLine;
            Sql += "[ServerGUID], [GUIDDatabase], [Name], [IsDeleted], [DateCreated], [DateModified] " + Environment.NewLine;
            Sql += ")  " + Environment.NewLine;
            Sql += "SELECT [tS].[ServerGUID], [tS].[GUIDDatabase], [tS].[Name], [tS].[IsDeleted], [tS].[DateCreated], [tS].[DateModified] " + Environment.NewLine;
            Sql += "FROM @inputTable as tS " + Environment.NewLine;
            Sql += "LEFT OUTER JOIN [dbo].[tblDatabases] as tD ON tD.[ServerGUID] = tS.[ServerGUID] AND tD.[Name] = tS.[Name] " + Environment.NewLine;
            Sql += "WHERE tD.[GUIDDatabase] IS NULL  " + Environment.NewLine;
            Sql += "; " + Environment.NewLine;
            Sql += "COMMIT TRANSACTION; " + Environment.NewLine;

            return Sql;
        }


    }
}
