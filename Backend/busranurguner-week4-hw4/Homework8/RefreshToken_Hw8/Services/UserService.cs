using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RefreshToken_Hw8.Context;
using RefreshToken_Hw8.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RefreshToken_Hw8.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly HotelApiDbContext _dbContext;
        private IConfiguration _configuration;

        public UserService(HotelApiDbContext dbContext,
                           IMapper mapper,
                           IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserInfo> Authenticate(TokenRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.LoginUser) ||
               string.IsNullOrWhiteSpace(req.LoginPassword))
            {
                return null;
            }

            var user = await _dbContext
                             .Users
                             .SingleOrDefaultAsync(user => user.LoginName == req.LoginUser &&
                                                           user.Password == req.LoginPassword);

            if (user == null)
                return null;
            
            var secretKey = _configuration.GetValue<string>("JwtTokenKey");
            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var tokenDesc = new SecurityTokenDescriptor
            {
                //olusturulacak token ayarları
                Subject = new ClaimsIdentity(new Claim[]
               {
                   new Claim(ClaimTypes.Name, user.Id.ToString())
               }),
                NotBefore = DateTime.Now, // ToUTC 
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256Signature)
            };

             var tokenHandler = new JwtSecurityTokenHandler();            
             var newToken = tokenHandler.CreateToken(tokenDesc);
                                 


            var userInfo = _mapper.Map<UserInfo>(user);
            userInfo.ExpireTime = tokenDesc.Expires ?? DateTime.Now.AddHours(1);  // newToken.ValidTo;
            userInfo.Token = tokenHandler.WriteToken(newToken);

            return userInfo;
        }
        //??
        public async Task<UserInfo> Refresh(TokenRequest req)
        {
           

            var user = await _dbContext
                             .Users
                             .SingleOrDefaultAsync(user => user.LoginName == req.LoginUser &&
                                                           user.Password == req.LoginPassword);
            if (string.IsNullOrWhiteSpace(req.LoginUser) ||
               string.IsNullOrWhiteSpace(req.LoginPassword))
            {
                return null;
            }

            var newRefreshToken = generateRefreshToken(req);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = req;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            _context.SaveChanges();

                      

            return newRefreshToken;
        }
        //??
        private RefreshToken generateRefreshToken(TokenRequest req)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = req,
                };
            }
        }
    }
}
