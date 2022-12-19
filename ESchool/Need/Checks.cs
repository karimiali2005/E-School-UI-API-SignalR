using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ESchool.Need
{
    public class Checks
    {
        public static bool CheckCodelMeli(string codemeli)
        {
            try
            {
                char[] chArray = codemeli.ToCharArray();
                int[] numArray = new int[chArray.Length];
                for (int i = 0; i < chArray.Length; i++)
                {
                    numArray[i] = (int)char.GetNumericValue(chArray[i]);
                }
                int num2 = numArray[9];
                switch (codemeli)
                {
                    case "0000000000":
                    case "1111111111":
                    case "22222222222":
                    case "33333333333":
                    case "4444444444":
                    case "5555555555":
                    case "6666666666":
                    case "7777777777":
                    case "8888888888":
                    case "9999999999":
                        return false;                        
                }
                int num3 = ((((((((numArray[0] * 10) + (numArray[1] * 9)) + (numArray[2] * 8)) + (numArray[3] * 7)) + (numArray[4] * 6)) + (numArray[5] * 5)) + (numArray[6] * 4)) + (numArray[7] * 3)) + (numArray[8] * 2);
                int num4 = num3 - ((num3 / 11) * 11);
                if ((((num4 == 0) && (num2 == num4)) || ((num4 == 1) && (num2 == 1))) || ((num4 > 1) && (num2 == Math.Abs((int)(num4 - 11)))))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToSaveElmah();
                return false;
            }
        }

        public static bool CheckMobile(string mobile)
        {
            try
            {
                string pattern = "^09[0-9]{9}";
                return Regex.IsMatch(mobile, pattern);
            }
            catch(Exception ex)
            {
                ex.ToSaveElmah();
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return false;
            }
            
        }        

        public static bool CheckPersianDate(string _year, string _month, string _day)
        {
            try
            {
                int year, month, day;
                year = int.Parse(_year);
                month = int.Parse(_month);
                day = int.Parse(_day);
                DateTime dateTime = new DateTime(year, month, day, new System.Globalization.PersianCalendar());
                return true;
            }
            catch(Exception ex)
            {
                ex.ToSaveElmah();
                return false;
            }
        }


        public static bool CheckPersianTime(string persiantime)
        {
            string pattern = @"^(([0-1][0-9])|(2[0-3])):[0-5][0-9]$";
            return Regex.IsMatch(persiantime, pattern);
        }



    }
}
