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
        private readonly ILoginService _loginService;
        public AuthController(IJwtTokenService tokenService, ILoginService loginService)
        {
            _tokenService = tokenService;
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string mobileNo)
        {
            // Replace with real user validation
            bool isExists = await _loginService.LoginByMobile(mobileNo);
            if (isExists)
            {
                var token = await _tokenService.GenerateToken(mobileNo);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized();
            }
            
        }
    }

}
