using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Tajnned.Domain.Common
{
    public class CommonMethods
    {
        protected static string[] allFormats ={"yyyy/MM/dd","yyyy/M/d",
            "dd/MM/yyyy","d/M/yyyy",
            "dd/M/yyyy","d/MM/yyyy","yyyy-MM-dd",
            "yyyy-M-d","dd-MM-yyyy","d-M-yyyy","dd-MM-yyyy","MM-yyyy","M-yyyy",
            "dd-M-yyyy","d-MM-yyyy","yyyy MM dd",
            "yyyy M d","dd MM yyyy","d M yyyy",
            "dd M yyyy","d MM yyyy"};

        public CommonMethods()
        {

        }
       
        private static int GetAgeFromHijryDate(string hijriDate)
        {
            try
            {
                if (!string.IsNullOrEmpty(hijriDate))
                {
                    var arabicCultureInfo = new CultureInfo("ar-SA");
                    var dateOfBirth = DateTime.ParseExact(hijriDate, "dd-MM-yyyy", arabicCultureInfo.DateTimeFormat);
                    return CalculateAge(dateOfBirth);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private static int GetAgeFromgregorianDate(string gregorianDate)
        {
            if (!string.IsNullOrEmpty(gregorianDate))
            {
                DateTime dateOfBirth = DateTime.ParseExact(gregorianDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                return CalculateAge(dateOfBirth);
            }
            else
            {
                return 0;
            }
        }

       public static int GetAge(long identityNumber, string dateOfBirthH = null, string dateOfBirthG = null)
        {
            int age = 0;

            if (identityNumber.ToString().StartsWith("1"))
                age = GetAgeFromHijryDate(dateOfBirthH);

            if (identityNumber.ToString().StartsWith("2"))
                age = GetAgeFromgregorianDate(dateOfBirthG);

            return age;
        }

        private static int CalculateAge(DateTime dob)
        {
            DateTime today = DateTime.Today;

            int months = today.Month - dob.Month;
            int years = today.Year - dob.Year;

            if (today.Day < dob.Day)
            {
                months--;
            }
            if (months < 0)
            {
                years--;
            }
            return years;
        }
 

        public static string GenerateRandomString(int length)
        {
            string resultString = string.Empty;
            var random = new Random();
            if (length > 0)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                resultString = new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(length).ToArray());
            }
            return resultString;
        }


        public static object CheckNull<T>(object obj)
        {
            object resultValue = DBNull.Value;
            try
            {
                if (obj != null)
                {
                    if (typeof(T) == typeof(DateTime))
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(obj)))
                        {
                            resultValue = DBNull.Value;
                        }
                        else if (Convert.ToDateTime(obj) == DateTime.MinValue)
                        {
                            resultValue = DBNull.Value;
                        }
                        else
                        {
                            resultValue = obj;
                        }

                    }
                    else
                    {
                        resultValue = (T)Convert.ChangeType(obj, typeof(T));
                    }
                }
            }
            catch (Exception)
            {
            }
            return resultValue;
        }
    }
}
