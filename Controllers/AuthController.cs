using MateAPI.Configuration;
using MateAPI.Models;
using MateAPI.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IMateRepository _mateRepository;

        public AuthController(IMateRepository imaterepo){
            _mateRepository = imaterepo;
        }
    
        [HttpPost]
        public async Task<ActionResult> Register(int userType, [FromBody] AuthUser user){
            var newUser = new object();
            switch(userType){
              case 1:
                 var newShop = new Shop{
                     Username = user.Username, 
                     Password = user.Password, 
                     Shopname = user.Name, 
                     PhoneNumber= user.PhoneNumber, 
                     Email=user.Email
                 };
                 
                newUser = await _mateRepository.RegisterShop(newShop);
                 break;
              case 2:
                 var newuser = new user{
                     Username = user.Username, 
                     Password = user.Password, 
                     FirstName = user.Name, 
                     PhoneNumber= user.PhoneNumber, 
                     Email=user.Email
                 };
                 
                 newUser = await _mateRepository.RegisterUser(newuser);
                 break;
              case 3:
              var newTmate = new Tmate{
                     Username = user.Username, 
                     Password = user.Password, 
                     FirstName = user.Name, 
                     PhoneNumber= user.PhoneNumber, 
                     Email=user.Email
                 };
                 
                 newUser = await _mateRepository.RegisterTmate(newTmate);
                 break;
              default: return BadRequest();
            }
            return Ok();
        }
        
        [HttpPost("verify")]
        public async Task<AuthResult> Verify(int vc, int id, int userType)
        {
            switch(userType){
                case 1:
                 return (await _mateRepository.VerifyShop(vc, id));
                case 2:
                 return (await _mateRepository.VerifyUser(vc, id));
                case 3:
                 return (await _mateRepository.VerifyTmate(vc, id));
                 default:
                 return new AuthResult("","",false);
            }
        }

        [HttpPost("Login")]
        public async Task<AuthResult> Login(string username, string password, int userType)
        {
            switch(userType){
                case 1:
                return await _mateRepository.LoginShop(username,password);
                case 2:
                return await _mateRepository.LoginUser(username,password);
                case 3:
                return await _mateRepository.LoginTmate(username,password);
                default:
                return new AuthResult("","",false);
            }
        }

        [HttpPost("RefreshTokenTask")]
        public async Task<AuthResult> RefreshTokenTask(string token, string refreshToken)
        {
                return await _mateRepository.RefreshTokenTask(token,refreshToken); 
        }

        
    }
}