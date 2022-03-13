using Sql_Tracker.Engine.Models;
using System;
using System.Collections.Generic;
using System.Data;
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


        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
