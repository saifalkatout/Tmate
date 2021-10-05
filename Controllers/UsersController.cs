using MateAPI.Models;
using MateAPI.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMateRepository _mateRepository;

        public UsersController(IMateRepository mateRepository)
        {
            _mateRepository = mateRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<user>> GetUsers()
        {
            return await _mateRepository.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<user>> getUsers(int id)
        {
            return await _mateRepository.GetUser(id);         
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(int id, [FromBody] user user)
        {
            var newUser = await _mateRepository.RegisterUser(user);
            return CreatedAtAction(nameof(getUsers), new { id = newUser.ID }, newUser);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id,[FromBody] user user)
        {
            if(id != user.ID)
            {
                return BadRequest();
            }
            await _mateRepository.UpdateUser(user);

            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteduser = await _mateRepository.GetUser(id);
            if (deleteduser == null)
                return NotFound();
            await _mateRepository.DeleteUser(deleteduser.ID);
            return NoContent();
        }
        
    }

}
