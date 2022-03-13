using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Utilz
{
    public class Constants
    {

        public enum QueryType
        {
            Empty = 0,
            PullServerStats = 1,
            PullDataBaseStats = 2,
            PullTableStats = 3,            
            
            Structure = 98,
            PopulateSchema = 99,
        }

        public struct Queries
        {
            public const string ServerList = "SELECT [GUIDServer],[Name],[ConnectionString],[IsDeleted],[DateCreated],[DateModified] FROM [tblServers] WHERE [IsDeleted] = 0";
            public const string DatabaseList = "SELECT [GUIDDatabase],[ServerGUID],[Name],[IsDeleted],[DateCreated],[DateModified] FROM [tblDatabases] WHERE [IsDeleted] = 0";
            public const string TableList = "SELECT [GUIDDatabaseTable],[DatabaseGUID],[SchemaName],[Name],[IsDeleted],[DateCreated],[DateModified] FROM [dbo].[tblDatabaseTables] WHERE [IsDeleted] = 0";

            public const string GetDatabaseList = "SELECT [name] FROM sys.databases";
            public const string GetTableList = "SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE FROM [{0}].INFORMATION_SCHEMA.TABLES";

        }
    }
}
