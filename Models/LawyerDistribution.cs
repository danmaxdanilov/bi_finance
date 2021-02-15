using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Models
{
    public class LawyerDistribution
    {
        public string Fio { get; set; }
        public string Office { get; set; }  
        public Dictionary<int, double> Distribution { get; set; }
    }
}
