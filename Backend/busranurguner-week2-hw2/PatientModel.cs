using diet_center.Abstract;
using System;

namespace diet_center
{
    public class PatientModel
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Index { get; set; }
        public PatientType Type { get; set; }
        public string Summary { get; set; }
    }
}
