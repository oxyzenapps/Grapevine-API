using grapevineCommon.Model.Workplace;
using grapevineService.Interfaces; // Ensure this points to where IWorkplaceService is
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grapevineApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkplaceController : ControllerBase
    {
        private readonly IWorkplaceService _service;
        public WorkplaceController(IWorkplaceService service) => _service = service;

        [HttpGet("location")]
        public async Task<IActionResult> GetCurrentLocation(string contactId, int websiteId)
        {
            var result = await _service.GetCurrentLocationAsync(contactId, websiteId);
            return Ok(result);
        }

        [HttpPost("update-filter")]
        public async Task<IActionResult> UpdateFilter([FromBody] UpdateFilterRequest request)
        {
            // This will now find the method because we renamed it in the Interface
            var searchId = await _service.UpdateFilterAsync(request);
            return Ok(new { SearchID = searchId });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchLocation(string query, string localityRequired = "0")
        {
            // This will now find the method because we renamed it in the Interface
            var result = await _service.SearchLocationAsync(query, localityRequired);
            return Ok(result);
        }
    }
}