using grapevineCommon.Model.Feed;
using grapevineServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace grapevineApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedsController : ControllerBase
    {

        private readonly IFeedsService _feedService;

        public FeedsController(IFeedsService feedService)
        {
            _feedService = feedService;
        }

        [HttpPost("GetFeed")]
        public async Task<IActionResult> GetFeed([FromBody] FeedRequest request)
        {
            var result = await _feedService.GetFeedAsync(request);
            return Ok(result);
        }
    }
}
