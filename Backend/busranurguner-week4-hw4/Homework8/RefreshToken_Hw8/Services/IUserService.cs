using RefreshToken_Hw8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefreshToken_Hw8.Services
{
    public interface IUserService
    {
        Task<UserInfo> Authenticate(TokenRequest req);

    }
}
