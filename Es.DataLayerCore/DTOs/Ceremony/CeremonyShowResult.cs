using System;
using System.Collections.Generic;
using System.Text;

namespace Es.DataLayerCore.DTOs.Ceremony
{
    public class CeremonyShowResult
    {
        public int CeremonyID { get; set; }
        public DateTime CeremonyDate { get; set; }
        public int CeremonyTypeID { get; set; }
        public string CeremonyText { get; set; }
        public string CeremonyTypeText { get; set; }
    }
}
