using System;
using System.Collections.Generic;
using System.Text;

namespace Es.DataLayerCore.DTOs.Finacial
{
    public class FinancialShowResult
    {
        public int FinancialID { get; set; }
        public DateTime FinancialDate { get; set; }
        public int UserID { get; set; }
        public int FinancialSendTypeID { get; set; }
        public string FinancialText { get; set; }
        public string FinancialSendTypeText { get; set; }
    }
}
