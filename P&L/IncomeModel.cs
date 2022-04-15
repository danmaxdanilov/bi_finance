using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SL.Models
{
    public class IncomeModel
    {
        public string InvoiceName { get; set; }
        public string ProfitCenter { get; set; }
        public string StageName { get; set; }
        public string Line { get; set; }
        public decimal Amount { get; set; }

        public int InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
    }
}
