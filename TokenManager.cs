using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MateAPI.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace MateAPI
{
    public class TokenManager
    {
        private readonly JwtConfig _jwtconfig;

        public TokenManager(JwtConfig jwtconfig)
        {
            _jwtconfig = jwtconfig;
        }
        
        public string GenerateToken(string UserName)
        {
            byte[] key = System.Text.Encoding.UTF8.GetBytes(_jwtconfig.Secret);
            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, UserName) }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)

            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
