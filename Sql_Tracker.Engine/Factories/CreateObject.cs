using Sql_Tracker.Engine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Factories
{
    public class CreateObject
    {
        private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public static QueryFileInfo PopQueryFileInfoFromFile(string path)
        {
            string jsonString = File.ReadAllText(path);
            QueryFileInfo result = JsonSerializer.Deserialize<QueryFileInfo>(jsonString, options: jsonSerializerOptions);

            return result;

        }
    }
}
