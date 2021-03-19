using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_center.Data
{
    public class Patient
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Summary { get; set; }
        public int Type { get; set; }
        // <18 :Zayıf  18-24:Normal  25-30:kilolu 30:obez


    }
}
