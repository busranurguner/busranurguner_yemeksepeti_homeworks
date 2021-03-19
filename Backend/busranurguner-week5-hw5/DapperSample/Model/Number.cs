using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSample.Model
{
    public class Number
    {
        public int NumberId { get; set; }
        public string  PersonNumber { get; set; }
        public NumberType NumberType { get;set; }
        public int Items { get; set; }
    }
}
