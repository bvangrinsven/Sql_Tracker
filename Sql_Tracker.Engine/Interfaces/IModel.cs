using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Interfaces
{
    public interface IModel
    {
        string GetAll();
        string GetObjectsSql();
        string GetUpsertSql();
        string[] GetPreUpsertSql();
        string[] GetPostUpsertSql();

        
    }
}
