using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SL.Models
{
    public class RevenueModel
    {
        public string InvoiceName { get; set; }
        public string StageName { get; set; }
        public string ProfitCenter { get; set; }
        public string Counterparty { get; set; }
        public string DocumentInfo { get; set; }
        public decimal Amount { get; set; }
        public string Line { get; set; }

        public int InvoiceNumber
        {
            get
            {
                var match = Regex.Match(this.InvoiceName, @"\d+(\d+)?");
                var value = match.Groups.Count > 1 ? match.Groups[0].Value : "0";
                int.TryParse(value, out int invoiceNumber);
                return invoiceNumber;
            }
        }
        public DateTime? InvoiceDate
        {
            get
            {
                var match = Regex.Match(this.InvoiceName, @"(\d{2}).(\d{2}).(\d{4}) ((\d{2})|(\d{1})):(\d{2}):(\d{2})");
                var value = match.Groups.Count > 1 ? match.Groups[0].Value : "0";
                DateTime.TryParseExact(value, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime invoiceDate);
                if (invoiceDate == DateTime.MinValue)
                {
                    DateTime.TryParseExact(value, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime invoiceDate1);
                    if (invoiceDate1 == DateTime.MinValue)
                        return null;
                    else
                        return invoiceDate1;
                }    
                else
                    return invoiceDate;
            }
        }
    }
}
