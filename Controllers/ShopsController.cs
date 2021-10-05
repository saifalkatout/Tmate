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
    public class ShopsController : ControllerBase
    {
        private readonly IMateRepository _mateRepository;
        private readonly TokenManager _tokenManager;

        public ShopsController(IMateRepository mateRepository, TokenManager tokenManager)
        {
            _mateRepository = mateRepository;
            _tokenManager = tokenManager;
        }
        [HttpPost]
        public async Task<ActionResult> RegisterShop(int id, [FromBody] Shop user)
        {
            var newUser = await _mateRepository.RegisterShop(user);
            return CreatedAtAction(nameof(GetShops), new { id = newUser.ID }, newUser);
        }
        [HttpPost("refresh")]
        /*public async Task<IActionResult> Refresh([FromBody] RefreshToken refreshRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.Token);
            if (!isValidRefreshToken)
            {
                return BadRequest();
            }

            //RefreshToken refreshTokenDTO = await _mateRepository.GetByToken(refreshRequest.Token);
            if (refreshTokenDTO == null)
            {
                return NotFound();
            }

            //await _mateRepository.Delete(refreshTokenDTO.Id);

            //Shop user = await _mateRepository.findToken(refreshTokenDTO.Id);
            if (user == null)
            {
                return NotFound();
            }

            AuthenticatedUserResponse response = await _authenticator.Authenticate(user);

            return Ok(response);
        }*/

        // [HttpPost("verifyshop/{vc}/{id}")]
        // public async Task<ActionResult> Verify(int vc, int id)
        // {
        //     var verifiedUser = await _mateRepository.VerifyShop(vc, id);
        //     if (verifiedUser != null)
        //     {
        //         string NewToken = _tokenManager.GenerateToken(verifiedUser.Username.Trim());
        //         //send authtoken
        //         verifiedUser.Token = NewToken;
        //         return Ok(NewToken);
        //     }
        //     else
        //     {
        //         //resend verification code
        //         return BadRequest();
        //     }
        // }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<Shop>> GetShops()
        {
            return await _mateRepository.GetShops();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shop>> GetShop(int id)
        {
            return await _mateRepository.GetShop(id);
        }

        
        [HttpPut]
        public async Task<ActionResult> UpdateShop(int id, [FromBody] Shop user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }
            await _mateRepository.UpdateShop(user);

            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteduser = await _mateRepository.GetShop(id); 
            if (deleteduser == null)
                return NotFound();
            await _mateRepository.DeleteShop(deleteduser.ID);
            return NoContent();
        }
        [HttpPost("/LoginShop")]
        public async Task<AuthResult> LoginShop(string username, string password)
        {
            return await _mateRepository.LoginShop(username, password);
        }
    }
}
