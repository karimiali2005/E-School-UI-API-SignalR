using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESchool.Need
{
    public static class homeworkAnswerStutous
    {
        public static string gettype(int type)
        {
            string value = "";
            switch (type)
            {
                case 1:
                    //فایل متنی
                    value = "text-format.svg";
                    break;

                case 2:
                    //فایل 
                    value = "paper.svg";
                    break;
                case 3:
                    //چت صوتی 
                    value = "microphone.svg";
                    break;
            }

            return value;
        }
    }
}
