using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StendenCafe.Services;
using StendenCafe.Models;
using StendenCafe.Authentication;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace StendenCafe.Controllers
{
    public class UserController : MyControllerBase
    {
        private readonly UserRepository UserRepository;
        
        public UserController(UserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] CafeUser newUser)
        {
            await UserRepository.Add(newUser);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] CafeUser login)
        {
            var userId = await UserRepository.Verify(login);
            if (userId == null) return BadRequest(new { message = "Username or password is incorrect" });
            var token = TokenHelper.GenerateToken(userId);
            return Ok(new { Token = token });
        }
    }
}