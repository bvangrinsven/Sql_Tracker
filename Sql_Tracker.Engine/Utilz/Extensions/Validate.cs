using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sql_Tracker.Engine.Utilz.Extensions
{
    public static class Validate
    {
        private static Regex AlphaPattern = new Regex(@"[^a-zA-Z]");

        public static bool IsAlpha(this string TestString)
        {            
            return !AlphaPattern.IsMatch(TestString);
        }

        private static Regex IsNumericPattern = new Regex(@"^[-+]?\d+(\.\d+)?$"); // Old, didn't account for decimals [^0-9]

        public static bool IsNumeric(this object Value)
        {
            string TestString = Convert.ToString(Value);

            if (TestString.IsNotEmpty())
                return IsNumericPattern.IsMatch(TestString);
            else
                return false;
        }
        public static bool IsNotEmptyValue(this object Value1)
        {
            return (!Value1.IsEmptyValue());
        }

        public static bool IsEmptyValue(this object Value1)
        {
            if (!Value1.IsNullValue())
                return (Convert.ToString(Value1).Trim() == "");
            else
                return true;
        }

        public static bool IsNullValue(this object Value1)
        {
            try
            {
                if (Value1 == null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return true;
            }
        }

        public static bool IsDate(this object DateValue)
        {
            try
            {
                if (DateValue == null)
                    return false;

                string strDate = Convert.ToString(DateValue);

                if (strDate.IsEmpty())
                    return false;

                //Handle Sql dates
                if (strDate.Compare("1/1/1900 12:00:00 AM") || strDate.Compare("1/1/1900"))
                    return false;

                if (strDate.Compare("1/1/1753 12:00:00 AM") || strDate.Compare("1/1/1753"))
                    return false;

                //New min date format has been introduced, it is appearing for dates that are null
                if (strDate.Compare("1/1/0001 12:00:00 AM") || strDate.Compare("1/1/0001"))
                    return false;


                // Handle Sql Dates in Canadian Locale
                if (strDate.Compare("1900-1-1 12:00:00 AM") || strDate.Compare("1900-01-01 12:00:00 AM") || strDate.Compare("1900-1-1"))
                    return false;

                if (strDate.Compare("1753-1-1 12:00:00 AM") || strDate.Compare("1753-01-01 12:00:00 AM") || strDate.Compare("1753-1-1"))
                    return false;

                //New min date format has been introduced, it is appearing for dates that are null
                if (strDate.Compare("0001-1-1 12:00:00 AM") || strDate.Compare("0001-01-01 12:00:00 AM") || strDate.Compare("0001-1-1"))
                    return false;


                DateTime dt = DateTime.Parse(strDate);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;

                return false;
            }
            catch
            {
                //Logging.Write(false, "strDate", strDate);
                return false;
            }
        }

        public static bool IsEmpty(this string Value1)
        {
            if (!Value1.IsNullValue())
                return (Convert.ToString(Value1).Trim() == "");
            else
                return true;

            // OLD way - return Convert.ToBoolean(Convert.ToString(Value1).Trim() == "");

        }

        public static bool IsNotEmpty(this string Value1)
        {
            if (!Value1.IsNullValue())
                return (Convert.ToString(Value1).Trim() != "");
            else
                return false;

            //return (!IsEmptyValue(Value1));
        }

        public static bool Compare(this object Value1, object Value2)
        {
            try
            {
                if (!Value1.IsNullValue() && !Value2.IsNullValue())
                    return Convert.ToString(Value1).ToUpper().Trim() == Convert.ToString(Value2).ToUpper().Trim();
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool ContainsValue(this string Value1, string Value2)
        {
            try
            {
                if (!Value1.IsNullValue() && !Value2.IsNullValue() && Value1.IsNotEmpty() && Value2.IsNotEmpty())
                    return Convert.ToBoolean(Value1.Trim().ToUpper().IndexOf(Value2.Trim().ToUpper()) >= 0);
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool InList(this string List, object Value)
        {
            try
            {
                if (List.IsNotEmpty())
                {
                    string[] oList = List.Split("|".ToCharArray());
                    foreach (string Item in oList)
                    {
                        if (Item.Compare(Value))
                            return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        public static bool IsGUID(this string Input)
        {
            try
            {
                if (Input.IsEmpty())
                    return false;

                Guid Temp = Guid.Parse(Input);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
