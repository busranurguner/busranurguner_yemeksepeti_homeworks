using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefreshToken_Hw8.Models
{
    public class UserInfo
    {
       
            public int Id { get; set; }
            public string FullName { get; set; }
            public DateTime ExpireTime { get; set; }
            public string Token { get; set; }
        
    }
}
