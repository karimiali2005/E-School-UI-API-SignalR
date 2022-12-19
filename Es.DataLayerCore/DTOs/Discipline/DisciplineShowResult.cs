using System;

namespace Es.DataLayerCore.DTOs.Discipline
{
    public class DisciplineShowResult
    {
        public int DisciplineID { get; set; }
        public DateTime DisciplineDate { get; set; }
        public int UserID { get; set; }
        public int DisciplineTypeID { get; set; }
        public string DisciplineText { get; set; }
        public string DisciplineTypeText { get; set; }
    }
}
