using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diet_center.RequestModel
{
    public class Model2
    {

        public int Id { get; set; }

        public string ClientId { get; set; }
    
        public int Type { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }

        [WeighStatus(30,ErrorMessage = "Index Degeri 30 Dan büyük olamaz")]
        public int Index => Weight / Height;

        public string Summary { get; set; }



    }
}
