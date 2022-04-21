using MateAPI.Configuration;
using MateAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MateAPI.repositories
{
    public class MateRepostiroy : IMateRepository
    {
        private readonly Context _context;
        private readonly JwtConfig _jwtconfig;

        public MateRepostiroy(Context mateContext, JwtConfig jwtconfig)
        {
            _context = mateContext;
            _jwtconfig = jwtconfig;
        }
        public async Task<AuthResult> VerifyUser(int VerCode, int id)
        {
            if (VerCode == 1236)
            {
                var user = await _context.Users.FindAsync(id);
                var TM = new TokenManager(_jwtconfig, _context);
                return TM.GenerateToken(user.Username.Trim(),"2"); 
            }
            return null;
        }
        public async Task<AuthResult> LoginUser(string username, string password)
        {
            var SignedIn = await _context.Users.SingleOrDefaultAsync(user => user.Username == username);
            if (BCrypt.Net.BCrypt.Verify(password,SignedIn.Password))
                return new AuthResult(SignedIn.Token,SignedIn.RefreshToken,true);
            else
                return new AuthResult("","",false);
        }
         
        public async Task<AuthResult> RegisterUser(user user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new AuthResult(user.Token,user.RefreshToken,true);
        }

        public async Task DeleteUser(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            _context.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<user>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<user> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateUser(user user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tmate>> GetTmates()
        {
            return await _context.Tmates.ToListAsync();
        }
        public async Task<AuthResult> LoginTmate(string username, string password)
        {
            var SignedIn = await _context.Tmates.SingleOrDefaultAsync(user => user.Username == username);
            if (BCrypt.Net.BCrypt.Verify(password,SignedIn.Password)){
                
                return new AuthResult(SignedIn.Token,SignedIn.RefreshToken,true);
            }
            else
                return new AuthResult("","",false);
        }
        public async Task<AuthResult> VerifyTmate(int VerCode, int id)
        {
            if (VerCode == 1234)
            {
               var Tmate = await _context.Tmates.FindAsync(id); 
               var TM = new TokenManager(_jwtconfig, _context);
               return TM.GenerateToken(Tmate.Username.Trim(),"3"); 
            }
            return null;
        }
        public async Task<Tmate> GetTmate(int id)
        {
            return await _context.Tmates.FindAsync(id);
        }

        public async Task<AuthResult> RegisterTmate(Tmate user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Tmates.Add(user);
            await _context.SaveChangesAsync();
            return new AuthResult(user.Token,user.RefreshToken,true);
        }

        public async Task UpdateTmate(Tmate user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTmate(int id)
        {
            var userToDelete = await _context.Tmates.FindAsync(id);
            _context.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<AuthResult> VerifyShop(int VerCode, int id)
        {
            if (VerCode == 1234)
            {
                /*var userToDelete = await _context.Shops.FindAsync(id);
                _context.Remove(userToDelete);
                await _context.SaveChangesAsync();*/
                var shop = await _context.Shops.FindAsync(id);
                var TM = new TokenManager(_jwtconfig, _context);
               return TM.GenerateToken(shop.Username.Trim(),"3"); 
            }
            return null;
        }
        public async Task<IEnumerable<Shop>> GetShops()
        {
            return await _context.Shops.ToListAsync();
        }

        public async Task<Shop> GetShop(int id)
        {
            return await _context.Shops.FindAsync(id);
        }

        public async Task<AuthResult> RegisterShop(Shop user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Shops.Add(user);
            await _context.SaveChangesAsync();
            return new AuthResult(user.Token,user.RefreshToken,true);
        }
        public async Task<AuthResult> LoginShop(string username, string password)
        {
            var SignedIn = await _context.Shops.SingleOrDefaultAsync(user => user.Username == username);
            if (BCrypt.Net.BCrypt.Verify(password,SignedIn.Password))
                return new AuthResult(SignedIn.Token,SignedIn.RefreshToken,true);
             else
                return new AuthResult("","",false);
        }
         
        public async Task UpdateShop(Shop user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShop(int id)
        {
            var userToDelete = await _context.Shops.FindAsync(id);
            _context.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Service> AddService(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task EditService(Service service)
        {
            _context.Entry(service).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Service> GetService(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task DeleteService(int id)
        {
            var ToDelete = await _context.Services.FindAsync(id);
            _context.Remove(ToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> GetServices(int shopID)
        {
            var Services =  await _context.Services.ToListAsync();
            //might need improvement
            return Services.Where((x) => x.ShopID == shopID);
        }

        public async Task<AuthResult> RefreshTokenTask(string token, string refreshToken)
        {
                var validatedToken = GetPrincipalFromToken(token);
                if(validatedToken == null){
                  return new AuthResult("","",false);
                }
                var ExpiryDate = long.Parse(validatedToken.Claims.Single(x=>x.Type == JwtRegisteredClaimNames.Exp).Value);
                var proccessedDate = new DateTime().AddSeconds(ExpiryDate);
                if(proccessedDate > DateTime.UtcNow){
                    return new AuthResult("","",false);
                }
                var jti = (validatedToken.Claims.Single(x=>x.Type == JwtRegisteredClaimNames.Jti).Value); 
                var StoredRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x=>x.Token == (refreshToken));
                if(StoredRefreshToken == null){
                    return new AuthResult("","",false);
                }
                if(DateTime.UtcNow > StoredRefreshToken.ExpiryDate){
                   return new AuthResult("","",false);
                }
                if(StoredRefreshToken.IsRevorked){
                    return new AuthResult("","",false);
                }
                if(StoredRefreshToken.IsUsed)
                    return new AuthResult("","",false);
                if(StoredRefreshToken.JwtId != jti)
                   return new AuthResult("","",false);
                StoredRefreshToken.IsUsed = true;
                _context.RefreshTokens.Update(StoredRefreshToken);
                await _context.SaveChangesAsync();
                var TM = new TokenManager(_jwtconfig, _context);
                var name = validatedToken.Claims.Single(x=>x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                var type = validatedToken.Claims.Single(x=>x.Type == JwtRegisteredClaimNames.Typ).Value;
                return  TM.GenerateToken(name,type);
        }
        private ClaimsPrincipal GetPrincipalFromToken(string token){
            var tokenHandler = new JwtSecurityTokenHandler();
            var TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtconfig.Secret)),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = false, 
                    ValidateIssuer = false,

                };
            try{
                var principle = tokenHandler.ValidateToken(token,TokenValidationParameters ,out var validatedToken);
                if(!isJWTWithSecurity(validatedToken)){
                    return null;
                }
                return principle;
            }
            catch (Exception e){
                Console.WriteLine(e);
                return null;
            }
        }
        private bool isJWTWithSecurity(SecurityToken validatedToken){
            return (validatedToken is JwtSecurityToken jwtsecuritytoken) && 
            jwtsecuritytoken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
