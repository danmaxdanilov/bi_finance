using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public string CostType { get; set; }
        public string IndirectDepartment { get; set; }
        public string DirectDepartment { get; set; }
        public string Comments { get; set; }
    }
}
