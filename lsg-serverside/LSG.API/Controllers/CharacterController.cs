using LSG.BLL.Dto.Character;
using LSG.BLL.Services.Interfaces;
using LSG.DAL.Database.Models;
using LSG.DAL.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LSG.API.Controllers
{
    //[Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var characters = await _service.GetAccountCharacters(id);

            return Ok(characters);
        }

        [HttpGet("description/{id}/{characterId}")]
        public async Task<IActionResult> GetCharacterDescription(int id, int characterId)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            IEnumerable<CharacterDescriptionForScriptDto> characterDescriptions = await _service.GetCharacterDescriptions(id);

            return Ok(characterDescriptions);
        }

        [HttpPost("description/add/{id}")]
        public async Task<IActionResult> CreateCharacterDescription(int id, CharacterDescriptionForScriptDto characterDescription)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            CharacterDescriptionForScriptDto characterDesc =  await _service.CreateDescription(characterDescription);

            return Ok(characterDesc);
        }

        [HttpDelete("description/delete/{id}/{characterDescription}")]
        public async Task<IActionResult> DeleteCharacterDescription(int id, int characterDescription)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            bool result = await _service.DeleteDescription(characterDescription);

            return Ok(result);
        }

        [HttpGet("look/{id}/{characterId}")]
        public async Task<IActionResult> GetCharacterLook(int id, int characterId)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            CharacterLookDto characterLook = await _service.GetCharacterLook(characterId);

            return Ok(characterLook);
        }

        
    }
}
