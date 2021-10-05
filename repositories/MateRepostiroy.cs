using MateAPI.Configuration;
using MateAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MateAPI.repositories
{
    public class MateRepostiroy : IMateRepository
    {
        private readonly Context _context;
        private readonly TokenManager _tokenManager;
        public MateRepostiroy(Context mateContext, TokenManager TokenManager)
        {
            _context = mateContext;
            _tokenManager = TokenManager;
        }
        public async Task<string> VerifyUser(int VerCode, int id)
        {
            if (VerCode == 1236)
            {
                var user = await _context.Users.FindAsync(id);
                user.Token = _tokenManager.GenerateToken(user.Username.Trim());
                return  user.Token; 
            }
            return null;
        }
        public async Task<user> RegisterUser(user user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
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

        public async Task<string> VerifyTmate(int VerCode, int id)
        {
            if (VerCode == 1234)
            {
               var Tmate = await _context.Tmates.FindAsync(id); 
               Tmate.Token =  _tokenManager.GenerateToken(Tmate.Username.Trim());
               return Tmate.Token; 
            }
            return null;
        }
        public async Task<Tmate> GetTmate(int id)
        {
            return await _context.Tmates.FindAsync(id);
        }

        public async Task<Tmate> RegisterTmate(Tmate user)
        {
            _context.Tmates.Add(user);
            await _context.SaveChangesAsync();
            return user;
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

        public async Task<string> VerifyShop(int VerCode, int id)
        {
            if (VerCode == 1234)
            {
                /*var userToDelete = await _context.Shops.FindAsync(id);
                _context.Remove(userToDelete);
                await _context.SaveChangesAsync();*/
                var shop = await _context.Shops.FindAsync(id);
                shop.Token = _tokenManager.GenerateToken(shop.Username.Trim());
                return shop.Token;
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

        public async Task<Shop> RegisterShop(Shop user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt()).Substring(0,10);
            _context.Shops.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<AuthResult> LoginShop(string username, string password)
        {
            Shop SignedInShop = await _context.Shops.SingleOrDefaultAsync(user => user.Username == username);
            if (BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt()).Substring(0, 10) == SignedInShop.Password)
                return new AuthResult(SignedInShop.Token,SignedInShop.Token,true);
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
    }
}
