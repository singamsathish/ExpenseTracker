using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public class ExpenseModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public decimal amount { get; set; }
    }
}
