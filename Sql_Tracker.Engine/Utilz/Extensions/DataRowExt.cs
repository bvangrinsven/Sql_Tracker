using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Utilz.Extensions
{
    public static class DataRowExt
    {
        public static bool ContainsColumn(this DataRow oRow, string ColumnName)
        {
            return (oRow != null && oRow.Table.Columns.Contains(ColumnName));
        }

        public static string GetString(this DataRow oDataRow, string FieldName, string DefaultValue = "", bool PreserveValue = false)
        {
            string DBValue = "";

            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                {
                    if (!PreserveValue)
                        DBValue = Convert.ToString(oDataRow[FieldName]).Trim();
                    else
                        DBValue = Convert.ToString(oDataRow[FieldName]);

                    if (DBValue.IsNotEmpty())
                        return DBValue;
                    else
                        return DefaultValue;
                }
                else
                    return DefaultValue;
            }
            else
                return DefaultValue;
        }

        public static char GetChar(this DataRow oDataRow, string FieldName, char DefaultValue = '\0')
        {
            char DBValue = '\0';

            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                {
                    DBValue = Convert.ToChar(oDataRow[FieldName].ToString().Trim());

                    if (DBValue.ToString().IsNotEmpty())
                        return DBValue;
                    else
                        return DefaultValue;
                }
                else
                    return DefaultValue;
            }
            else
                return DefaultValue;
        }

        public static float GetFloat(this DataRow oDataRow, string FieldName)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                    return Convert.ToSingle(oDataRow[FieldName]);
                else
                    return 0F;
            }
            else
                return 0F;
        }

        public static double GetDouble(this DataRow oDataRow, string FieldName)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                    return Convert.ToDouble(oDataRow[FieldName]);
                else
                    return 0F;
            }
            else
                return 0F;
        }

        public static decimal GetDecimal(this DataRow oDataRow, string FieldName)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                    return Convert.ToDecimal(oDataRow[FieldName]);
                else
                    return 0;
            }
            else
                return 0;
        }

        public static short GetInt16(this DataRow oDataRow, string FieldName)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                    return Convert.ToInt16(oDataRow[FieldName]);
                else
                    return 0;
            }
            else
                return 0;
        }

        public static int GetInt32(this DataRow oDataRow, string FieldName)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue() && oDataRow[FieldName].IsNumeric())
                    return Convert.ToInt32(oDataRow[FieldName]);
                else
                    return 0;
            }
            else
                return 0;
        }

        public static long GetInt64(this DataRow oDataRow, string FieldName)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                    return Convert.ToInt64(oDataRow[FieldName]);
                else
                    return 0;
            }
            else
                return 0;
        }

        public static bool GetBoolean(this DataRow oDataRow, string FieldName)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                    return Convert.ToBoolean(oDataRow[FieldName]);
                else
                    return false;
            }
            else
                return false;
        }

        public static byte GetByte(this DataRow oDataRow, string FieldName)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                    return Convert.ToByte(oDataRow[FieldName]);
                else
                    return 0;
            }
            else
                return 0;
        }

        public static DateTime GetDateTime(this DataRow oDataRow, string FieldName)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                {
                    object DateHopeful = oDataRow[FieldName];
                    if (DateHopeful.IsDate())
                        return Convert.ToDateTime(DateHopeful);
                    else
                        return DateTime.MinValue;
                }
                else
                    return DateTime.MinValue;
            }
            else
                return DateTime.MinValue;
        }

        public static DateTime GetTime(this DataRow oDataRow, string FieldName)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                {
                    object DateHopeful = oDataRow[FieldName];
                    if (!DateHopeful.IsNullValue())
                        return Convert.ToDateTime(DateHopeful);
                    else
                        return DateTime.MinValue;
                }
                else
                    return DateTime.MinValue;
            }
            else
                return DateTime.MinValue;
        }


        public static byte[] GetBytes(this DataRow oDataRow, string FieldName)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                if (!oDataRow[FieldName].IsNullValue())
                    return (byte[])(oDataRow[FieldName]);
                else
                    return default(byte[]);
            }
            else
                return default(byte[]);
        }

        public static void SetValue(this DataRow oDataRow, string FieldName, object NewValue)
        {
            if (oDataRow.ContainsColumn(FieldName))
            {
                oDataRow.BeginEdit();
                oDataRow[FieldName] = NewValue;
                oDataRow.EndEdit();
            }
        }

    }
}
