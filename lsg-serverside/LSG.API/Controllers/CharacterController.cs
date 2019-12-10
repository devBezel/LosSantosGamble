using LSG.BLL.Services.Interfaces;
using LSG.DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _service;

        public CharacterController(ICharacterService service)
        {
            _service = service;
        }
        [HttpGet("list/{id}")]
        public async Task<IActionResult> GetAccountCharacters(int id)
        {
            var characters = await _service.GetAccountCharacters(id);

            return Ok(characters);
        }
        
    }
}
