using CoreLayer.Entities.Identity;
using CoreLayer.ServiceContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IdentityServices
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration config;

        public TokenServices(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<string> CreateToken(UserApplication user, UserManager<UserApplication> _userManager)
        {
            //privateClaims
           var AuthClaims = new List<Claim>
           {
               new Claim(ClaimTypes.Email,user.Email),
               new Claim(ClaimTypes.GivenName,user.UserName),
               new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber)

           };
            //Key
            var Authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            var Token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                expires: DateTime.Now.AddDays(int.Parse(config["JWT:ExpireTimeByDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(Authkey, SecurityAlgorithms.HmacSha256Signature)
                
                );
            return  new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
