using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;

namespace Tajnned.Domain.Common
{
    public static class Utility
    {
        public const int RowsNumber = 10;

        public const int MaxRowsNumber = 1000;

        public static List<int> RowsList = new List<int> { 10, 20, 30, 40, 50 };
        public static int maxRowList = 999;

        #region TotalPages
        public static int TotalPages(float totalRecords, float recordsPerPage)
        {
            return recordsPerPage > 0 ? (int)Math.Ceiling(totalRecords / recordsPerPage) : 0;
        }

        #endregion TotalPages
        public static DateTime GetFirstDateOfMonth(int month = 0)
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(month);
        }
        public static DateTime GetLastDateOfMonth(int month = 0)
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(month).AddDays(-1);
        }

        public static string ConfigKey(string key)
        {
            return string.Empty;
            // TODO: @shalash refactor
            //return ConfigurationManager.AppSettings[key].ToString();
        }

        public static short ToSafeInt16(this object objValue)
        {
            short result = 0;
            return (objValue == null || objValue == DBNull.Value) ? result : ToInt16(objValue.ToString());
        }
        public static short ToInt16(object str)
        {
            short result = 0;
            if (str != null)
            {
                try
                {
                    short.TryParse(str.ToString(), out result);
                }
                catch { }
            }

            return result;
        }
        public static string ToSafeString(this object objString)
        {
            return (objString == null || objString == DBNull.Value) ? string.Empty : objString.ToString().Trim();
        }
        public static DateTime ConvertDDMMYYHHMMToDate(this string dateString, string timeString)
        {
            DateTime dt;
            if (DateTime.TryParseExact(string.Format("{0} {1}", dateString, timeString),
                                        "d/M/yyyy H:mm",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                out dt))
            {
                //valid date
            }
            else
            {
                //invalid date
            }

            return dt;
        }

        public static long ToLong(string str)
        {
            long result = 0;
            if (str != null && Information.IsNumeric(str))
            {
                try
                {
                    result = Convert.ToInt64(str);
                }
                catch { }
            }
            return result;
        }
        public static long ToSafeLong(this object objString)
        {
            return (objString == null || objString == DBNull.Value) ? 0 : ToLong(objString.ToString());
        }
        public static int ToInt(string str)
        {
            int result = 0;
            if (str != null && str.Length > 0)
            {
                try
                {
                    int.TryParse(str, out result);
                }
                catch { }
            }

            return result;
        }
        public static int ToSafeInt(this object objString)
        {
            return (objString == null || objString == DBNull.Value) ? 0 : ToInt(objString.ToString());
        }
        public static bool ToSafeBool(this object objString, bool defValue)
        {
            return (objString == null || objString == DBNull.Value) ? defValue : ToBool(objString.ToString());
        }
        public static decimal ToSafeDecimal(this object objString)
        {
            return (objString == null || objString == DBNull.Value) ? 0 : ToDecimal(objString.ToString());
        }
        public static decimal ToDecimal(string str)
        {
            decimal result = 0;
            if (str != null && Information.IsNumeric(str))
            {
                try
                {
                    result = Convert.ToDecimal(str);
                }
                catch { }
            }
            return result;
        }
        public static bool ToBool(string data)
        {
            bool result = false;
            if (data != null)
            {
                string tdata = data.Trim().ToLower();
                if (tdata == "true" || tdata == "yes" || tdata == "1")
                    result = true;
                else
                    result = false;
            }
            return result;
        }
        public static byte ToSafeByte(this object objString)
        {
            byte defaultValue = 0;
            return (objString == null || objString == DBNull.Value) ? defaultValue : ToByte(objString.ToString());
        }

        public static byte ToByte(string str)
        {
            byte result = 0;
            if (str != null && str.Length > 0)
            {
                try
                {
                    byte.TryParse(str, out result);
                }
                catch { }
            }

            return result;
        }
        private static string[] allFormats = { "yyyy/MM/dd", "yyyy/M/d", "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy", "yyyy-MM-dd", "yyyy-M-d", "dd-MM-yyyy", "d-M-yyyy", "dd-M-yyyy", "d-MM-yyyy", "yyyy MM dd", "yyyy M d", "dd MM yyyy", "d M yyyy", "dd M yyyy", "d MM yyyy" };
        public static string GetGregorianFromHijri(string hijri)
        {
            CultureInfo arCul = new CultureInfo("ar-SA");
            CultureInfo enCul = new CultureInfo("en-US");
            DateTime tempDate = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
            return tempDate.ToString("dd/MM/yyyy", enCul.DateTimeFormat);
        }
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(
                                    prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static DataTable ToDataTableWithNull<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(
                                    prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
