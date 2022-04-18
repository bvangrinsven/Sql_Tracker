using Sql_Tracker.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Models
{
    public class ServerSqlJob : IModel
    {
        public string ServerJobGUID { get; set; } = string.Empty;
        public string ServerGUID { get; set; } = string.Empty;
        public Guid JobGuid { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Enabled { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public string GetAll()
        {
            string Sql = "SELECT " + Environment.NewLine;
            Sql += "[GUIDServerJob], [ServerGUID], [JobGuid], [Name], [Enabled], [IsDeleted], [DateCreated], [DateModified]" + Environment.NewLine;
            Sql += "FROM [dbo].[tblServerJobs] WITH(NOLOCK) " + Environment.NewLine;
            Sql += "WHERE " + Environment.NewLine;
            Sql += "[IsDeleted] = 0 " + Environment.NewLine;

            return Sql;
        }

        public string GetObjectsSql()
        {
            return "SELECT @ServerGUID as [ServerGUID],	sj.job_id as JobGuid, sj.[name] as [Name], sj.[enabled] as [Enabled], 0 as [IsDeleted], sj.date_created as [DateCreated], sj.date_modified as [DateModified] " +
                "FROM msdb.dbo.sysjobs sj";
        }

        public string[] GetPostUpsertSql()
        {
            List<string> routines = new List<string>();

            string Sql = "";

            // Description of update
            Sql = "INSERT INTO [dbo].[tblServerToJob] ($from_id, $to_id) " + Environment.NewLine;
            Sql += "SELECT ts.$node_id as fromid, tsj.$node_id as toid " + Environment.NewLine;
            Sql += "FROM tblServers as ts " + Environment.NewLine;
            Sql += "INNER JOIN tblServerJobs as tsj ON ts.GUIDServer = tsj.ServerGUID " + Environment.NewLine;
            Sql += "LEFT OUTER JOIN [tblServerToJob] as std ON ts.$node_id = std.$from_id AND tsj.$node_id = std.$to_id " + Environment.NewLine;
            Sql += "WHERE std.$edge_id IS NULL  " + Environment.NewLine;
            routines.Add(Sql);

            return routines.ToArray();
        }

        public string[] GetPreUpsertSql()
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
            Sql += "tD.[ServerGUID] = tS.[ServerGUID] " + Environment.NewLine;
            Sql += ", tD.[JobGuid] = tS.[JobGuid] " + Environment.NewLine;
            Sql += ", tD.[Name] = tS.[Name] " + Environment.NewLine;
            Sql += ", tD.[Enabled] = tS.[Enabled] " + Environment.NewLine;
            Sql += ", tD.[IsDeleted] = tS.[IsDeleted] " + Environment.NewLine;
            Sql += ", tD.[DateCreated] = tS.[DateCreated] " + Environment.NewLine;
            Sql += ", tD.[DateModified] = tS.[DateModified] " + Environment.NewLine;
            Sql += "FROM [dbo].[tblServerJobs] as tD " + Environment.NewLine;
            Sql += "INNER JOIN @inputTable as tS ON " + Environment.NewLine;
            Sql += "tD.[JobGuid] = tS.[JobGuid] " + Environment.NewLine;
            Sql += "; " + Environment.NewLine;
            Sql += "INSERT INTO [dbo].[tblServerJobs] ( " + Environment.NewLine;
            Sql += "[ServerGUID], [JobGuid], [Name], [Enabled], [IsDeleted], [DateCreated], [DateModified] " + Environment.NewLine;
            Sql += ") " + Environment.NewLine;
            Sql += "SELECT [tS].[ServerGUID], [tS].[JobGuid], [tS].[Name], [tS].[Enabled], [tS].[IsDeleted], [tS].[DateCreated], [tS].[DateModified] " + Environment.NewLine;
            Sql += "FROM @inputTable as tS " + Environment.NewLine;
            Sql += "LEFT OUTER JOIN [dbo].[tblServerJobs] as tD ON " + Environment.NewLine;
            Sql += "tD.[JobGuid] = tS.[JobGuid] " + Environment.NewLine;
            Sql += "WHERE " + Environment.NewLine;
            Sql += "tD.[GUIDServerJob] IS NULL " + Environment.NewLine;
            Sql += "; " + Environment.NewLine;
            Sql += "COMMIT TRANSACTION; " + Environment.NewLine;

            return Sql;
        }
    }
}
