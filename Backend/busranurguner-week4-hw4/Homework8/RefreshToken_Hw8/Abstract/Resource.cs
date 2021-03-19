using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefreshToken_Hw8.Abstract
{
    public class Resource
    {
        // :TODO : HBK - Property Order neden çalışmıyor...
        [JsonProperty(Order = -1)]
        public string Href { get; set; }
    }
}
