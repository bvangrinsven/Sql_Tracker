using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Utilz
{
    internal class Generator
    {

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
