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

namespace MateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IMateRepository _mateRepository;
        private readonly TokenManager _tokenManager;
        public AuthController(TokenManager tm, IMateRepository imaterepo){
            _mateRepository = imaterepo;
            _tokenManager = tm;
        }
    

        [HttpPost]
        public async Task<ActionResult> Register(int id, int userType, [FromBody] AuthUser user){
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
            return Ok(newUser);
        }
        [HttpPost("verify")]
        public async Task<ActionResult> Verify(int vc, int id, int userType)
        {
            string key;
            switch(userType){
                case 1:
                 key = (await _mateRepository.VerifyShop(vc, id));
                 break;
                case 2:
                 key = (await _mateRepository.VerifyUser(vc, id));
                 break;
                case 3:
                 key = (await _mateRepository.VerifyTmate(vc, id));
                 break;
                default:
                return BadRequest();
            }
            if (key != null && key != "")
            {
                //send authtoken
                return Ok(key);
            }
            else
            {
                //resend verification code
                return BadRequest();
            }
        }

        // [HttpPost("Login")]
        // public async Task<ActionResult> Login(string username, string password, int usertype)
        // {
        //     switch(userType){
        //         case 1:
        //         //shop
        //          break;
        //         case 2:
        //         //user
        //          break;
        //         case 3:
        //         //tmate
        //          break;
        //         default:
        //         return BadRequest();
        //     }
        // }
    }
}