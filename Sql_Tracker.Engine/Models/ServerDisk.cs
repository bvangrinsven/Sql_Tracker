using Sql_Tracker.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Models
{
    public class ServerDisk : IModel
    {
        public string GUIDServerDisk { get; set; } = string.Empty;
        public string ServerGUID { get; set; } = string.Empty;
        public string Disk { get; set; } = string.Empty;
        public string FileSystem { get; set; } = string.Empty;
        public string LogicalDriveName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }


        public string GetObjectsSql()
        {
            return "SELECT DISTINCT '' as GUIDServerDisk, @ServerGUID as ServerGUID, CONVERT(CHAR(100), SERVERPROPERTY('Servername')) AS [ServerName], " +
                "volume_mount_point [Disk], " +
                "file_system_type [FileSystem], " +
                "logical_volume_name as [LogicalDriveName], 0 as IsDeleted, getdate() as DateCreated, getdate() as DateModitied " +
                "FROM sys.master_files " +
                "CROSS APPLY sys.dm_os_volume_stats(database_id, file_id) ";

        }

        public string[] GetPostUpsertSql()
        {
            List<string> routines = new List<string>();

            string Sql = "";

            // Populate Server to Server Disk
            Sql = "INSERT INTO [dbo].[tblServerToDisk] ($from_id, $to_id)   " + Environment.NewLine;
            Sql += "SELECT ts.$node_id as fromid, tsd.$node_id as toid   " + Environment.NewLine;
            Sql += "FROM tblServers as ts " + Environment.NewLine;
            Sql += "INNER JOIN tblServerDisk as tsd ON ts.GUIDServer = tsd.ServerGUID " + Environment.NewLine;
            Sql += "LEFT OUTER JOIN [tblServerToDisk] as std ON ts.$node_id = std.$from_id AND tsd.$node_id = std.$to_id   " + Environment.NewLine;
            Sql += "WHERE std.$edge_id IS NULL  " + Environment.NewLine;
            routines.Add(Sql);

            return routines.ToArray();
        }

        public string[] GetPreUpsertSql()
        {
            return new string[0];
        }

        public string GetUpsertSql()
        {
            string Sql = "BEGIN TRANSACTION;  " + Environment.NewLine;
            Sql += "UPDATE tD SET tD.[ServerGUID] = tS.[ServerGUID] " + Environment.NewLine;
            Sql += ", tD.[ServerName] = tS.[ServerName] " + Environment.NewLine;
            Sql += ", tD.[Disk] = tS.[Disk]  " + Environment.NewLine;
            Sql += ", tD.[FileSystem] = tS.[FileSystem]  " + Environment.NewLine;
            Sql += ", tD.[LogicalDriveName] = tS.[LogicalDriveName]  " + Environment.NewLine;
            Sql += ", tD.[IsDeleted] = tS.[IsDeleted]  " + Environment.NewLine;
            Sql += ", tD.[DateCreated] = tS.[DateCreated]  " + Environment.NewLine;
            Sql += ", tD.[DateModified] = tS.[DateModified]  " + Environment.NewLine;
            Sql += "FROM [dbo].[tblServerDisk] as tD  " + Environment.NewLine;
            Sql += "INNER JOIN @inputTable as tS ON tD.[ServerGUID] = tS.[ServerGUID]  " + Environment.NewLine;
            Sql += "AND tD.[Disk] = tS.[Disk]  " + Environment.NewLine;
            Sql += "AND tD.[FileSystem] = tS.[FileSystem]  " + Environment.NewLine;
            Sql += ";  " + Environment.NewLine;
            Sql += "INSERT INTO [dbo].[tblServerDisk] (  " + Environment.NewLine;
            Sql += "[ServerGUID], [ServerName], [Disk], [FileSystem], [LogicalDriveName], [IsDeleted] " + Environment.NewLine;
            Sql += ")   " + Environment.NewLine;
            Sql += "SELECT [tS].[ServerGUID], [tS].[ServerName], [tS].[Disk], [tS].[FileSystem], [tS].[LogicalDriveName], [tS].[IsDeleted] " + Environment.NewLine;
            Sql += "FROM @inputTable as tS  " + Environment.NewLine;
            Sql += "LEFT OUTER JOIN [dbo].[tblServerDisk] as tD ON tD.[ServerGUID] = tS.[ServerGUID]  " + Environment.NewLine;
            Sql += "AND tD.[Disk] = tS.[Disk]  " + Environment.NewLine;
            Sql += "AND tD.[FileSystem] = tS.[FileSystem]  " + Environment.NewLine;
            Sql += "WHERE tD.[GUIDServerDisk] IS NULL   " + Environment.NewLine;
            Sql += ";  " + Environment.NewLine;
            Sql += "COMMIT TRANSACTION; " + Environment.NewLine;


            return Sql;

        }
    }
}
