using diet_center.Abstract;
using System;

namespace diet_center
{
    public abstract class PatientStatus
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Index => Weight / Height;
        public string Summary { get; set; }
        public PatientType Type { get; set; }
    }
}
