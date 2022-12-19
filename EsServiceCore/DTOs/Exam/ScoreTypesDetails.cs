using System;
using System.Collections.Generic;
using System.Text;

namespace EsServiceCore.DTOs
{
    public class ScoreTypesDetails
    {
        public int ScoreTypeID { get; set; }
        public string ScoreTypeTitle { get; set; }
        public bool IsNumber { get; set; }
        public string SumScoreTypeDetailTitle { get; set; }
        public int? ScoreStart { get; set; }
        public int? ScoreFinish { get; set; }

    }
}
