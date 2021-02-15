using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Models
{
    public class SalaryCost
    {
        public string Employee { get; set; }
        public decimal SalaryFee { get; set; }
        public decimal PensionaryFeeType1 { get; set; }
        public decimal PensionaryFeeType2 { get; set; }
        public decimal PensionaryFeeType3 { get; set; }
        public decimal PensionaryFeeType4 { get; set; }
        public decimal PensionaryFeeType5 { get; set; }
        public decimal SalaryTotal => this.SalaryFee +
                    this.PensionaryFeeType1 +
                    this.PensionaryFeeType2 +
                    this.PensionaryFeeType3 +
                    this.PensionaryFeeType4 +
                    this.PensionaryFeeType5;
    }
}
