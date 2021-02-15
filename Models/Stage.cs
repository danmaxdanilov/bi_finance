using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Models
{
    public class Stage
    {
        public DateTime StageDate { get; set; }
        public int StageYear { get; set; }
        public int StageMonth { get; set; }
        public string StageQuarter { get; set; }
        public string StageMonthName { get; set; }
    }
}
