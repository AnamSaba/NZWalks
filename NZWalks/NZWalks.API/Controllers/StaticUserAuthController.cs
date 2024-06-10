using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaticUserAuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandlerStaticUser tokenHandler;

        public StaticUserAuthController(IUserRepository userRepository,
            ITokenHandlerStaticUser tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            // Check if user is authenticated.
            // Check username and password.
            var user = await userRepository.AuthenticateAsync(userLoginDto.Username, userLoginDto.Password);

            if(user != null)
            {
                // Generate a Jwt token
                var token = tokenHandler.CreateTokenAsync(user);

                return Ok(token);
            }

            return BadRequest("Username or password is incorrect.");
        }
    }
}
