using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MateAPI.Configuration;
using Microsoft.IdentityModel.Tokens;
using MateAPI.Models;

namespace MateAPI
{
    public class TokenManager
    {
        private readonly JwtConfig _jwtconfig;
        private readonly Context _context;

        public TokenManager(JwtConfig jwtconfig, Context C)
        {
            _jwtconfig = jwtconfig;
            _context = C;
        }
        
        public AuthResult GenerateToken(string UserName, string type)
        {
            byte[] key = System.Text.Encoding.UTF8.GetBytes(_jwtconfig.Secret);
            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] { new Claim(JwtRegisteredClaimNames.NameId, UserName),new Claim(JwtRegisteredClaimNames.Typ, type), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(_jwtconfig.AccessTokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature),

            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            var refreshToken = new RefreshToken {
                JwtId = token.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString(),
            };
            _context.RefreshTokens.Add(refreshToken);
             _context.SaveChanges();
            return new AuthResult(handler.WriteToken(token),refreshToken.Token,true);
        }
    }
}
