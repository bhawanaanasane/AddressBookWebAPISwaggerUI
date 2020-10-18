using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Services.LoginServices
{
    public static class GenerateToken
    {
        public static string Generate(string userid, string secretkey)
        {
            // authentication successful so generate jwt token using userid
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            string str = ClaimTypes.Name.ToString();
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,Convert.ToString(userid))
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);



            string Token = tokenHandler.WriteToken(token);
            return Token;
        }
    } 
}
