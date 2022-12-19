using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISms.Need
{
    public class Codes
    {
        public static AppSettingClass GetSmsProperties()
        {
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                AppSettingClass appSettingClass = Newtonsoft.Json.JsonConvert.DeserializeObject<AppSettingClass>(json);
                return appSettingClass;
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


    }
}
