using grapevineCommon.Model;
using grapevineService.Interfaces;
using grapevineServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace grapevineApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _tokenService;
        private readonly ILoginService _loginService;
        private readonly IWorkplaceService _service;
        public AuthController(IJwtTokenService tokenService, ILoginService loginService, IWorkplaceService service)
        {
            _tokenService = tokenService;
            _loginService = loginService;
            _service=service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string LoginFeedChannelID)
        {
            string mobileNo = "";
            string ApplicantID = "";
            try
            {
                var result = await _service.GetFeedChannelDetails(LoginFeedChannelID);
                var data = JsonConvert.DeserializeObject<dynamic>(result);
                if(data != null)
                {
                    mobileNo = data[0].mobile;
                    ApplicantID = data[0].FeedChannelParticipantGroupID;
                   
                    // Replace with real user validation
                    var (message, FeedChannelID) = await _loginService.LoginByMobile(mobileNo);
                    if (message.Contains("Old User") || message.Contains("New User"))
                    {
                        var token = await _tokenService.GenerateToken(mobileNo);
                        return Ok(new { token = token, FeedChannelID = FeedChannelID, ApplicantID = ApplicantID });
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized();
                }
               
            }
            catch(Exception ex)
            {
                return Unauthorized();
            }
            

        }
        [HttpGet("get_LoginUser_detail")]
        public async Task<IActionResult> crm_feed_Get_FeedChannel_Details(string FeedChannelID)
        {
            try
            {
                var result = await _service.GetFeedChannelDetails(FeedChannelID);
                var ok = ApiResponse<string>.Success(result, "Feed channel details retrieved successfully", 200, "OK", true);
                return StatusCode(ok.StatusCode, ok);
            }
            catch (Exception ex)
            {
                var error = ApiResponse<string>.Error($"An error occurred while retrieving feed channel details: {ex.Message}", 500, "Internal Server Error", null, false);
                return StatusCode(error.StatusCode, error);
            }

        }
    }

}
