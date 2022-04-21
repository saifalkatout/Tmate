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
    public class TmateController : ControllerBase
    {
        private readonly IMateRepository _mateRepository;

        public TmateController(IMateRepository mateRepository)
        {
            _mateRepository = mateRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Tmate>> GetTmates()
        {
            return await _mateRepository.GetTmates();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tmate>> GetTmate(int id)
        {
            return await _mateRepository.GetTmate(id);
        }

        [HttpPost]
        // public async Task<ActionResult> RegisterTmate(int id, [FromBody] Tmate user)
        // {
        //     var newUser = await _mateRepository.RegisterTmate(user);
        //     return CreatedAtAction(nameof(GetTmates), new { id = newUser.ID }, newUser);
        // }
        [HttpPut]
        public async Task<ActionResult> UpdateTmate(int id, [FromBody] Tmate user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }
            await _mateRepository.UpdateTmate(user);

            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteduser = await _mateRepository.GetTmate(id);
            if (deleteduser == null)
                return NotFound();
            await _mateRepository.DeleteTmate(deleteduser.ID);
            return NoContent();
        }
        
    }
}
