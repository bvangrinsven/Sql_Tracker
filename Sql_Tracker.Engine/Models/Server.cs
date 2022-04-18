using Sql_Tracker.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Models
{
    public class Server : IModel
    {
        public string GUIDServer { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public string GetAll()
        {
            string Sql = "SELECT " + Environment.NewLine;
            Sql += "[GUIDServer], [Name], [ConnectionString], [IsDeleted], [DateCreated], [DateModified]" + Environment.NewLine;
            Sql += "FROM [dbo].[tblServers] WITH(NOLOCK) " + Environment.NewLine;
            Sql += "WHERE " + Environment.NewLine;
            Sql += "[IsDeleted] = 0 " + Environment.NewLine;

            return Sql;
        }

        public string GetObjectsSql()
        {
            return "";
        }

        public string[] GetPreUpsertSql()
        {
            /*
            List<string> routines = new List<string>();

            string Sql = "";

            // Description of update
            Sql = "";
            routines.Add(Sql);

            return routines.ToArray();
            */
            return new string[0];
        }

        public string[] GetPostUpsertSql()
        {
            List<string> routines = new List<string>();

            string Sql = "";

            // Description of update
            Sql = "";
            routines.Add(Sql);

            return routines.ToArray();
        }

        public string GetUpsertSql()
        {
            // tD = Table Destination
            // tS = Table Source
            string Sql = "BEGIN TRANSACTION; " + Environment.NewLine;

            Sql += "UPDATE tD SET " + Environment.NewLine;
            Sql += "tD.[Name] = tS.[Name] " + Environment.NewLine;
            Sql += ", tD.[ConnectionString] = tS.[ConnectionString] " + Environment.NewLine;
            Sql += ", tD.[IsDeleted] = tS.[IsDeleted] " + Environment.NewLine;
            Sql += ", tD.[DateCreated] = tS.[DateCreated] " + Environment.NewLine;
            Sql += ", tD.[DateModified] = tS.[DateModified] " + Environment.NewLine;

            Sql += "FROM [dbo].[tblServers] as tD " + Environment.NewLine;
            Sql += "INNER JOIN @inputTable as tS ON " + Environment.NewLine;
            Sql += "tD.[GUIDServer] = tS.[GUIDServer] " + Environment.NewLine;

            Sql += "; " + Environment.NewLine;
            Sql += "INSERT INTO [dbo].[tblServers] ( " + Environment.NewLine;
            Sql += "[GUIDServer], [Name], [ConnectionString], [IsDeleted], [DateCreated], [DateModified] " + Environment.NewLine;
            Sql += ") " + Environment.NewLine;
            Sql += "SELECT [tS].[GUIDServer], [tS].[Name], [tS].[ConnectionString], [tS].[IsDeleted], [tS].[DateCreated], [tS].[DateModified] " + Environment.NewLine;
            Sql += "FROM @inputTable as tS " + Environment.NewLine;
            Sql += "LEFT OUTER JOIN [dbo].[tblServers] as tD ON " + Environment.NewLine;
            Sql += "tD.[GUIDServer] = tS.[GUIDServer] " + Environment.NewLine;

            Sql += "WHERE " + Environment.NewLine;
            Sql += "tD.[GUIDServer] IS NULL " + Environment.NewLine;

            Sql += "; " + Environment.NewLine;
            Sql += "COMMIT TRANSACTION; " + Environment.NewLine;

            return Sql;
        }
    }
}
