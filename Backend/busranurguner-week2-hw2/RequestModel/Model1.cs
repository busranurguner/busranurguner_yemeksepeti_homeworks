using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diet_center.RequestModel
{
    public class Model1
    {

      //  [Required(ErrorMessage = "Id boş geçilemez")]
        public int Id { get; set; }
      //  [StringLength(10, ErrorMessage = "Client ID maksimum 10 karaketer olabilf")]
        public string ClientId { get; set; }
        public int Type { get; set; }
        public int Index => Weight / Height;
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Summary { get; set; }

        public Tuple<bool, List<string>> Validate()
        {

            List<string> validateList = new List<string>();
            if (ClientId.Trim() == string.Empty)
                validateList.Add("client Id Boş geçilemez");


            if (Index < 18)
                validateList.Add("geçersiz index değeri");

            return new Tuple<bool, List<string>>(validateList.Count <= 0, validateList);
        }


        public (bool isValid, List<string> validationErrors) Validate2()
        {
            List<string> validateList = new List<string>();
            if (ClientId.Trim() == string.Empty)
                validateList.Add("client Id Boş geçilemez");


            if (Index < 18)
                validateList.Add("geçersiz index değeri");

            return (validateList.Count <= 0, validateList);
        }


    }
}
