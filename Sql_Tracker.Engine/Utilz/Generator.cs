using Sql_Tracker.Engine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Utilz
{
    public class Generator
    {

        public static string GetAssemblyPath()
        {
            //return Assembly.GetEntryAssembly().Location;
            //return AppDomain.CurrentDomain.BaseDirectory;

            return Directory.GetCurrentDirectory();
        }

        public static IEnumerable<string> GetFiles(string path, string searchPattern)
        {
            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path, searchPattern))
                {
                    yield return file;
                }
            }
            else
                yield break;
        }



    }
}
