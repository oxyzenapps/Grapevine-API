using grapevineCommon.Model;
using grapevineServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace grapevineApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _tokenService;
        public AuthController(IJwtTokenService tokenService) => _tokenService = tokenService;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Replace with real user validation
            if (model.Username == "test" && model.Password == "password")
            {
                var token = _tokenService.GenerateToken(model.Username);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }

}
