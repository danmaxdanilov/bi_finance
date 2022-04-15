using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Models
{
    public class SalaryCost
    {
        public string Employee { get; set; }
        public List<decimal> SalaryFee { get; set; } = new();
        public decimal SalaryTotal => this.PensionaryFeeType1.Sum();
    }
}
