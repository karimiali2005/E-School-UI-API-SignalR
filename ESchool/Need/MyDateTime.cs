using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace System
{
    public static class MyDateTime
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
        public static string getPersianDate(this DateTime? _dt)
        {
            if (_dt == null)
                return "";
            DateTime dt =(DateTime) _dt;
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

            return Year + "/" + Month + "/" + Day+" - "+Hour+":"+Minutes;
        }
        public static DateTime ToGeorgianDateTime(this string persianDate)
        {
            persianDate = persianDate.PersianToEnglish();
            int year = Convert.ToInt32(persianDate.Substring(0, 4));
            int month = Convert.ToInt32(persianDate.Substring(5, 2));
            int day = Convert.ToInt32(persianDate.Substring(8, 2));
            DateTime georgianDateTime = new DateTime(year, month, day, new System.Globalization.PersianCalendar());
            return georgianDateTime;
        }
        public static DateTime ToGeorgianDateTimeByMinutes(this string persianDate)
        {
            persianDate = persianDate.PersianToEnglish();
            int year = Convert.ToInt32(persianDate.Substring(0, 4));
            int month = Convert.ToInt32(persianDate.Substring(5, 2));
            int day = Convert.ToInt32(persianDate.Substring(8, 2));
            int hour = Convert.ToInt32(persianDate.Substring(13, 2));
            int minutes = Convert.ToInt32(persianDate.Substring(16, 2));

            DateTime georgianDateTime = new DateTime(year, month, day,hour,minutes,0, new System.Globalization.PersianCalendar());
            return georgianDateTime;
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
        public static string ToTimeChatString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return "";
            DateTime _dateTime = (DateTime)dateTime;
            if (_dateTime.Date == DateTime.Now.Date)
            {
                return _dateTime.ToString("HH:mm");
            }

            if (_dateTime.Date == DateTime.Now.AddDays(-1).Date)
            {
                return "دیروز";
            }

            return ToShamsiShort(_dateTime);
        }
        public static string ToShamsi(this DateTime? value)
        {
            if (value == null)
                return "";
            var _date = (DateTime) value;
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(_date) + "/" + pc.GetMonth(_date).ToString("00") + "/" +
                   pc.GetDayOfMonth(_date).ToString("00");
        }
        public static string ToShamsiShort(this DateTime? value)
        {
            if (value == null)
                return "";
            var _date = (DateTime)value;
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(_date).ToString().Substring(2,2) + "/" + pc.GetMonth(_date).ToString("00") + "/" +
                   pc.GetDayOfMonth(_date).ToString("00");
        }
    }
}
