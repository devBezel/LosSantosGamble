using LSG.BLL.Dto.Account;
using LSG.DAL.Database.Models;
using LSG.DAL.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LSG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountForRegisterDto accountForRegister)
        {
            if (await _repository.UserExists(accountForRegister.Username))
                return BadRequest("Użytkownik o takiej nazwie już istnieje");

            var userToCreate = new Account()
            {
                Username = accountForRegister.Username
            };

            var createdAccount = await _repository.Register(userToCreate, accountForRegister.Password);

            return StatusCode(201);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountForLoginDto accountForLogin)
        {
            Console.WriteLine($"{accountForLogin.Username}  {accountForLogin.Password}");
            var accountFromRepo = await _repository.Login(accountForLogin.Username, accountForLogin.Password);
            if (accountFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, accountFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, accountFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });

        }
    }
}
