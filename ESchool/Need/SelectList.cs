using DatabaseLayer.Access;
using DatabaseLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESchool.Need
{
    public class SelectList
    {

        public static List<SelectListItem> getIdcardSeriesLetters()
        {
            List<SelectListItem> IdcardSeriesLetters = new List<SelectListItem>();
            IdcardSeriesLetters.Add(new SelectListItem { Text = "الف", Value = "الف" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "ب", Value = "ب" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "ل", Value = "ل" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "د", Value = "د" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "ر", Value = "ر" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "ج", Value = "ج" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "چ", Value = "چ" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "ح", Value = "ح" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "خ", Value = "خ" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "و", Value = "و" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "یک", Value = "یک" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "دو", Value = "دو" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "سه", Value = "سه" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "چهار", Value = "چهار" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "نه", Value = "نه" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "ده", Value = "ده" });
            IdcardSeriesLetters.Add(new SelectListItem { Text = "یازده", Value = "یازده" });

            return IdcardSeriesLetters;
        }

        public static List<SelectListItem> getDays(string selected = "01")
        {
            List<SelectListItem> Days = new List<SelectListItem>();
            Days.Add(new SelectListItem { Text = "01", Value = "01" });            
            Days.Add(new SelectListItem { Text = "02", Value = "02" });            
            Days.Add(new SelectListItem { Text = "03", Value = "03" });            
            Days.Add(new SelectListItem { Text = "04", Value = "04" });            
            Days.Add(new SelectListItem { Text = "05", Value = "05" });            
            Days.Add(new SelectListItem { Text = "06", Value = "06" });            
            Days.Add(new SelectListItem { Text = "07", Value = "07" });            
            Days.Add(new SelectListItem { Text = "08", Value = "08" });            
            Days.Add(new SelectListItem { Text = "09", Value = "09" });
            Days.Add(new SelectListItem { Text = "10", Value = "10" });
            Days.Add(new SelectListItem { Text = "11", Value = "11" });
            Days.Add(new SelectListItem { Text = "12", Value = "12" });
            Days.Add(new SelectListItem { Text = "13", Value = "13" });
            Days.Add(new SelectListItem { Text = "14", Value = "14" });
            Days.Add(new SelectListItem { Text = "15", Value = "15" });
            Days.Add(new SelectListItem { Text = "16", Value = "16" });
            Days.Add(new SelectListItem { Text = "17", Value = "17" });
            Days.Add(new SelectListItem { Text = "18", Value = "18" });
            Days.Add(new SelectListItem { Text = "19", Value = "19" }); 
            Days.Add(new SelectListItem { Text = "20", Value = "20" }); 
            Days.Add(new SelectListItem { Text = "21", Value = "21" });
            Days.Add(new SelectListItem { Text = "22", Value = "22" });
            Days.Add(new SelectListItem { Text = "23", Value = "23" });
            Days.Add(new SelectListItem { Text = "24", Value = "24" });
            Days.Add(new SelectListItem { Text = "25", Value = "25" });
            Days.Add(new SelectListItem { Text = "26", Value = "26" });
            Days.Add(new SelectListItem { Text = "27", Value = "27" });
            Days.Add(new SelectListItem { Text = "28", Value = "28" });
            Days.Add(new SelectListItem { Text = "29", Value = "29" });
            Days.Add(new SelectListItem { Text = "30", Value = "30" });
            Days.Add(new SelectListItem { Text = "31", Value = "31" });

            var day = Days.Where(p => p.Value == selected).First();
            if(day != null)
            {
                day.Selected = true;
            }

            return Days;
        }


        public static List<SelectListItem> getHours(string selected = "05")
        {
            List<SelectListItem> Hours = new List<SelectListItem>();            
            Hours.Add(new SelectListItem { Text = "00", Value = "00" });
            Hours.Add(new SelectListItem { Text = "01", Value = "01" });
            Hours.Add(new SelectListItem { Text = "02", Value = "02" });
            Hours.Add(new SelectListItem { Text = "03", Value = "03" });
            Hours.Add(new SelectListItem { Text = "04", Value = "04" });
            Hours.Add(new SelectListItem { Text = "05", Value = "05" });
            Hours.Add(new SelectListItem { Text = "06", Value = "06" });
            Hours.Add(new SelectListItem { Text = "07", Value = "07" });
            Hours.Add(new SelectListItem { Text = "08", Value = "08" });
            Hours.Add(new SelectListItem { Text = "09", Value = "09" });
            Hours.Add(new SelectListItem { Text = "10", Value = "10" });
            Hours.Add(new SelectListItem { Text = "11", Value = "11" });
            Hours.Add(new SelectListItem { Text = "12", Value = "12" });
            Hours.Add(new SelectListItem { Text = "13", Value = "13" });
            Hours.Add(new SelectListItem { Text = "14", Value = "14" });
            Hours.Add(new SelectListItem { Text = "15", Value = "15" });
            Hours.Add(new SelectListItem { Text = "16", Value = "16" });
            Hours.Add(new SelectListItem { Text = "17", Value = "17" });
            Hours.Add(new SelectListItem { Text = "18", Value = "18" });
            Hours.Add(new SelectListItem { Text = "19", Value = "19" });
            Hours.Add(new SelectListItem { Text = "20", Value = "20" });
            Hours.Add(new SelectListItem { Text = "21", Value = "21" });
            Hours.Add(new SelectListItem { Text = "22", Value = "22" });
            Hours.Add(new SelectListItem { Text = "23", Value = "23" });

            var hour = Hours.Where(p => p.Value == selected);
            if (hour.Count() > 0)
            {
                hour.First().Selected = true;
            }

            return Hours;
        }

        public static List<SelectListItem> getMinutes(string selected = "01")
        {
            List<SelectListItem> Minutes = new List<SelectListItem>();
            Minutes.Add(new SelectListItem { Text = "00", Value = "00" });
            Minutes.Add(new SelectListItem { Text = "05", Value = "05" });
            Minutes.Add(new SelectListItem { Text = "10", Value = "10" });
            Minutes.Add(new SelectListItem { Text = "15", Value = "15" });
            Minutes.Add(new SelectListItem { Text = "20", Value = "20" });
            Minutes.Add(new SelectListItem { Text = "25", Value = "25" });
            Minutes.Add(new SelectListItem { Text = "30", Value = "30" });
            Minutes.Add(new SelectListItem { Text = "35", Value = "35" });
            Minutes.Add(new SelectListItem { Text = "40", Value = "40" });
            Minutes.Add(new SelectListItem { Text = "45", Value = "45" });
            Minutes.Add(new SelectListItem { Text = "50", Value = "50" });
            Minutes.Add(new SelectListItem { Text = "55", Value = "55" });

            var minute = Minutes.Where(p => p.Value == selected);
            if (minute.Count() > 0)
            {
                minute.First().Selected = true;
            }

            return Minutes;
        }

        public static List<SelectListItem> getMonths(string selected = "01")
        {
            List<SelectListItem> Months = new List<SelectListItem>();
            Months.Add(new SelectListItem { Text = "فروردین", Value = "01" });
            Months.Add(new SelectListItem { Text = "اردیبهشت", Value = "02" });
            Months.Add(new SelectListItem { Text = "خرداد", Value = "03" });
            Months.Add(new SelectListItem { Text = "تیر", Value = "04" });
            Months.Add(new SelectListItem { Text = "مرداد", Value = "05" });
            Months.Add(new SelectListItem { Text = "شهریور", Value = "06" });
            Months.Add(new SelectListItem { Text = "مهر", Value = "07" });
            Months.Add(new SelectListItem { Text = "آبان", Value = "08" });
            Months.Add(new SelectListItem { Text = "آذر", Value = "09" });
            Months.Add(new SelectListItem { Text = "دی", Value = "10" });
            Months.Add(new SelectListItem { Text = "بهمن", Value = "11" });
            Months.Add(new SelectListItem { Text = "اسفند", Value = "12" });

            var month = Months.Where(p => p.Value == selected).First();
            if (month != null)
            {
                month.Selected = true;
            }

            return Months;
        }

        public static List<SelectListItem> getLivePlaces()
        {
            List<SelectListItem> LivePlaces = new List<SelectListItem>();
            LivePlaces.Add(new SelectListItem { Text = "پدر و مادر", Value = "پدر و مادر" });
            LivePlaces.Add(new SelectListItem { Text = "فقط پدر", Value = "فقط پدر" });
            LivePlaces.Add(new SelectListItem { Text = "فقط مادر", Value = "فقط مادر" });
            LivePlaces.Add(new SelectListItem { Text = "سایر", Value = "سایر" });            

            return LivePlaces;
        }


        public static List<SelectListItem> getEducations()
        {
            List<SelectListItem> Educations = new List<SelectListItem>();
            Educations.Add(new SelectListItem { Text = "بی سواد", Value = "بی سواد" });
            Educations.Add(new SelectListItem { Text = "ابتدایی", Value = "ابتدایی" });
            Educations.Add(new SelectListItem { Text = "سیکل", Value = "سیکل" });
            Educations.Add(new SelectListItem { Text = "دیپلم", Value = "دیپلم" });
            Educations.Add(new SelectListItem { Text = "فوق دیپلم", Value = "فوق دیپلم" });
            Educations.Add(new SelectListItem { Text = "لیسانس", Value = "لیسانس" });
            Educations.Add(new SelectListItem { Text = "فوق لیسانس", Value = "فوق لیسانس" });
            Educations.Add(new SelectListItem { Text = "دکتری و بالاتر", Value = "دکتری و بالاتر" });


            return Educations;
        }

        public static List<SelectListItem> getBoolean(string selected = "False")
        {
            List<SelectListItem> booleans = new List<SelectListItem>();
            booleans.Add(new SelectListItem { Text = "خير", Value = "False" });
            booleans.Add(new SelectListItem { Text = "بلي", Value = "True" });

            var boolean = booleans.Where(p => p.Value == selected);
            if (boolean.Count() > 0)
            {
                boolean.First().Selected = true;
            }

            return booleans;
        }


        public static List<SelectListItem> getWeeks(bool AddEmpty = false, string selected = "")
        {
            selected = string.IsNullOrEmpty(selected) && AddEmpty == true ? "" : selected;
            selected = string.IsNullOrEmpty(selected) && AddEmpty == false ? Codes.getDay(0) : selected;

            List<SelectListItem> weeks = new List<SelectListItem>();
            if(AddEmpty)
            {
                weeks.Add(new SelectListItem { Text = "", Value = "" });
            }
            weeks.Add(new SelectListItem { Text = Codes.getDay(0), Value = Codes.getDay(0) });           
            weeks.Add(new SelectListItem { Text = Codes.getDay(1), Value = Codes.getDay(1) });           
            weeks.Add(new SelectListItem { Text = Codes.getDay(2), Value = Codes.getDay(2) });           
            weeks.Add(new SelectListItem { Text = Codes.getDay(3), Value = Codes.getDay(3) });           
            weeks.Add(new SelectListItem { Text = Codes.getDay(4), Value = Codes.getDay(4) });           
            weeks.Add(new SelectListItem { Text = Codes.getDay(5), Value = Codes.getDay(5) });           
            weeks.Add(new SelectListItem { Text = Codes.getDay(6), Value = Codes.getDay(6) });

            var week = weeks.Where(p => p.Value == selected);
            if (week.Count() > 0)
            {
                week.First().Selected = true;
            }

            return weeks;
        }


        public static List<SelectListItem> getParentStatuses()
        {
            ESchoolContext db = new ESchoolContext();
            var query = db.ParentStatus.Select(p => new SelectListItem { Text = p.ParentStatusTitle, Value = p.ParentStatusId.ToString() });
            return query.ToList();
        }


        public static List<SelectListItem> getUserTypes(bool onlyParents = true)
        {
            ESchoolContext db = new ESchoolContext();
            if(onlyParents)
            {
                var query = db.UserType.Where(p => p.UserTypeId == 2 || p.UserTypeId == 3).Select(p => new SelectListItem { Text = p.UserTypeTitle, Value = p.UserTypeId.ToString() });
                return query.ToList();
            }
            else
            {
                var query = db.UserType.Select(p => new SelectListItem { Text = p.UserTypeTitle, Value = p.UserTypeId.ToString() });
                return query.ToList();
                
            }            
        }


        public static List<SelectListItem> getTeacherCourses(int? DegreeId, int? GradeId, int? StudyFieldId, int? CourseId, int? TeacherId)
        {
            TeacherCourseOp teacherCourseOp = new TeacherCourseOp();
            List<TeacherCourse> teacherCourses = teacherCourseOp.GetTeacherCourses(DegreeId, GradeId, StudyFieldId);
            var query = teacherCourses.Select(p => new SelectListItem { Text = p.User.FirstName + " " + p.User.LastName + " : " + p.Course.CourseTitle, Value = p.User.UserId.ToString() + "-" + p.Course.CourseId.ToString() });

            if(CourseId != null && TeacherId != null)
            {
                string TeacherCourseId = TeacherId + "-" + CourseId;
                var q = query.Where(p => p.Value == TeacherCourseId);
                if (q.Count() > 0)
                {
                    q.First().Selected = true;
                }
            }

            return query.ToList();
        }
    }
}
