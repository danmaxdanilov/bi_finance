using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Models
{
    public class Payment
    {
        public int ReceiverId { get; set; }
        public string RecieverName { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal Amount { get; set; } 
    }
}
