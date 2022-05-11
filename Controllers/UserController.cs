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
        private readonly UserRepository userRepository;
        private readonly TokenHelper tokenHelper;
        
        public UserController(UserRepository userRepository, TokenHelper tokenHelper)
        {
            this.userRepository = userRepository;
            this.tokenHelper = tokenHelper;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromForm] CafeUser newUser)
        {
            await userRepository.Add(newUser);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromForm] CafeUser login)
        {
            var userId = await userRepository.Verify(login);

            if (userId == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var token = tokenHelper.GenerateToken(userId);

            return Ok(new { Token = token });
            
        }
    }
}