using RefreshToken_Hw8.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefreshToken_Hw8.Models
{
    public class HotelInfo : Resource
    {
        public string Title { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public HotelAddress Location { get; set; }
    }
}
