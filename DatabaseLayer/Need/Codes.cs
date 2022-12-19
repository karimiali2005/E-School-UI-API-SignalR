using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Need
{
    public class Codes
    {
        public static string GetConnectionString()
        {
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                AppSettingClass appSettingClass = Newtonsoft.Json.JsonConvert.DeserializeObject<AppSettingClass>(json);
                return appSettingClass.connectionStrings.DefaultConnectionString;
            }
        }

        public static string getPersianDate(DateTime dt)
        {
            if (dt == null)
                return "";

            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);

            string Year = Convert.ToString(year);
            string Month = month < 10 ? "0" + Convert.ToString(month) : Convert.ToString(month);
            string Day = day < 10 ? "0" + Convert.ToString(day) : Convert.ToString(day);

            return Year + "/" + Month + "/" + Day;

        }

        public static DateTime GetLocalDateTime()
        {
            DateTime serverTime = DateTime.Now;
            DateTime utcTime = serverTime.ToUniversalTime();

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

            return localTime;
        }

        public static string getPersianDay(DateTime dt)
        {
            if (dt == null)
                return "";

            PersianCalendar pc = new PersianCalendar();
            DayOfWeek dayOfWeek = pc.GetDayOfWeek(dt);
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    return getDay(6);
                case DayOfWeek.Monday:
                    return getDay(2);
                case DayOfWeek.Sunday:
                    return getDay(1);
                case DayOfWeek.Saturday:
                    return getDay(0);
                case DayOfWeek.Wednesday:
                    return getDay(4);
                case DayOfWeek.Thursday:
                    return getDay(5);
                case DayOfWeek.Tuesday:
                    return getDay(3);
            }

            return "";
        }

        public static Dictionary<int, string> getDays()
        {
            Dictionary<int, string> days = new Dictionary<int, string>();
            days.Add(0, "شنبه");
            days.Add(1, "یک شنبه");
            days.Add(2, "دوشنبه");
            days.Add(3, "سه شنبه");
            days.Add(4, "چهارشنبه");
            days.Add(5, "پنج شنبه");
            days.Add(6, "جمعه");

            return days;
        }

        public static string getDay(int index)
        {
            Dictionary<int, string> days = getDays();
            return days[index];
        }

        public static DayOfWeek getDayofWeek(string week_day)
        {
            week_day = string.IsNullOrEmpty(week_day) ? getDay(0) : week_day;

            if (week_day == getDay(0))
                return DayOfWeek.Saturday;
            if (week_day == getDay(1))
                return DayOfWeek.Sunday;
            if (week_day == getDay(2))
                return DayOfWeek.Monday;
            if (week_day == getDay(3))
                return DayOfWeek.Tuesday;
            if (week_day == getDay(4))
                return DayOfWeek.Wednesday;
            if (week_day == getDay(5))
                return DayOfWeek.Thursday;
            if (week_day == getDay(6))
                return DayOfWeek.Friday;

            return DayOfWeek.Saturday;
        }

        public static string Hash(string text)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }


        public static string ReplaceForSearch(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace(" ", "");
                text = text.Replace("ی", "ي");
                text = text.Replace("ک", "ک");
                text = text.ToLower();
                return text;
            }
            else
            {
                return "";
            }            
        }       

    }
}
