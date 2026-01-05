using grapevineServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace grapevineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ODEController : ControllerBase
    {
        private readonly IFeedsService feedsService;
        public ODEController(IFeedsService feedsService)
        {
            this.feedsService = feedsService;
        }
    }
}
