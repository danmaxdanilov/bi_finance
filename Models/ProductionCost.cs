using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Models
{
    public class ProductionCost
    {
        public string CostCenter { get; set; }
        public string ProfitCenter { get; set; }
        public string CostType { get; set; }
        public string Reciever { get; set; }
        public string ExpensiveItem { get; set; }
        public decimal Amount { get; set; }  
    }
}
