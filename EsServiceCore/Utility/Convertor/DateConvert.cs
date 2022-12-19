using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EsServiceCore.Utility.Convertor
{
  public static  class DateConvert
    {
        public static string getPersianDate(this DateTime _dt)
        {
            if (_dt == null)
                return "";
            DateTime dt = (DateTime)_dt;
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);
            
            string Year = Convert.ToString(year);
            string Month = month < 10 ? "0" + Convert.ToString(month) : Convert.ToString(month);
            string Day = day < 10 ? "0" + Convert.ToString(day) : Convert.ToString(day);

            return Year + "/" + Month + "/" + Day;
        }
        public static string getPersianDate(this DateTime? _dt)
        {
            if (_dt == null)
                return "";
            DateTime dt = (DateTime)_dt;
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);

            string Year = Convert.ToString(year);
            string Month = month < 10 ? "0" + Convert.ToString(month) : Convert.ToString(month);
            string Day = day < 10 ? "0" + Convert.ToString(day) : Convert.ToString(day);

            return Year + "/" + Month + "/" + Day;
        }
        public static string getPersianDateByMinutes(this DateTime? _dt)
        {
            if (_dt == null)
                return "";
            DateTime dt = (DateTime)_dt;
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);
            int hour = pc.GetHour(dt);
            int min = pc.GetMinute(dt);

            string Year = Convert.ToString(year);
            string Month = month < 10 ? "0" + Convert.ToString(month) : Convert.ToString(month);
            string Day = day < 10 ? "0" + Convert.ToString(day) : Convert.ToString(day);
            string Hour = hour < 10 ? "0" + Convert.ToString(hour) : Convert.ToString(hour);
            string Minutes = min < 10 ? "0" + Convert.ToString(min) : Convert.ToString(min);

            return Year + "/" + Month + "/" + Day + " - " + Hour + ":" + Minutes;
        }
        public static string PersianToEnglish(this string persianStr)
        {
            Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
            {
                ['۰'] = '0',
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9'
            };
            foreach (var item in persianStr)
            {
                try
                {
                    persianStr = persianStr.Replace(item, LettersDictionary[item]);
                }
                catch (Exception ex)
                {


                }

            }
            return persianStr;
        }
    }
}
