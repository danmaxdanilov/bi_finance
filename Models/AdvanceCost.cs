using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Models
{
    public class AdvanceCost
    {
        public string CostCenter { get; set; }
        public string ProfitCenter { get; set; }
        public string CostType { get; set; }
        public string Employee { get; set; }
        public decimal Amount { get; set; }
    }
}
