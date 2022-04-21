using System;
using System.Collections.Generic;
using System.Linq;
using MateAPI.Models;
using System.Threading.Tasks;
using MateAPI.Configuration;

namespace MateAPI.repositories
{
    public interface IMateRepository
    {
        Task<AuthResult> VerifyUser(int VerCode, int id);
        Task<IEnumerable<user>> GetUsers();

        Task<user> GetUser(int id);

        Task<AuthResult> RegisterUser(user user);

        Task UpdateUser(user user);

        Task DeleteUser(int id);
        Task<AuthResult> RefreshTokenTask(string token, string refreshToken);


        Task<AuthResult> VerifyTmate(int VerCode, int id);
        Task<IEnumerable<Tmate>> GetTmates();

        Task<Tmate> GetTmate(int id);

        Task<AuthResult> RegisterTmate(Tmate user);

        Task UpdateTmate(Tmate user);

        Task DeleteTmate(int id);

        Task<AuthResult> VerifyShop(int VerCode, int id);
        Task<IEnumerable<Shop>> GetShops();

        Task<Shop> GetShop(int id);

        Task<AuthResult> RegisterShop(Shop user);

        Task UpdateShop(Shop user);

        Task DeleteShop(int id);

        Task<Service> AddService(Service service);

        Task EditService(Service service);

        Task<Service> GetService(int id);
        Task DeleteService(int id);

        Task<IEnumerable<Service>> GetServices(int shopID);
        Task<AuthResult> LoginShop(string username, string password);
        Task<AuthResult> LoginTmate(string username, string password);
        Task<AuthResult> LoginUser(string username, string password);

    }
}
