using grapevineCommon.Model.Workplace;
using grapevineService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var searchId = await _service.UpdateFilterAsync(request);
            return Ok(new { SearchID = searchId });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchLocation(string query, string localityRequired = "0")
        {
            var result = await _service.SearchLocationAsync(query, localityRequired);
            return Ok(result);
        }

        [HttpGet("sale-list-count")]
        public async Task<IActionResult> GetSaleListCount(string pageNo, string searchId, string sort, string listingStatusId, string feedChannelId, string myFeedChannelId)
        {
            var data = await _service.GetSaleListCountAsync(pageNo, searchId, sort, listingStatusId, feedChannelId, myFeedChannelId);
            return Ok(new { ActionResult = new { result = new List<string> { data } } });
        }

        [HttpGet("agencies-list")]
        public async Task<IActionResult> GetAgenciesList(string pageno, string searchId, string sort = "", string feedChannelId = "0", string listingStatusId = "4", string myFeedChannelId = "0")
        {
            var data = await _service.GetAgenciesListAsync(pageno, searchId, sort, feedChannelId, listingStatusId, myFeedChannelId);
            return Ok(new { ActionResult = new { result = new List<object> { data, searchId, pageno } } });
        }

        [HttpGet("developers-list")]
        public async Task<IActionResult> GetDevelopersList(string pageno, string searchId, string sort = "", string feedChannelId = "0", string listingStatusId = "4", string myFeedChannelId = "0")
        {
            var data = await _service.GetDevelopersListAsync(pageno, searchId, sort, feedChannelId, listingStatusId, myFeedChannelId);
            return Ok(new { ActionResult = new { result = new List<object> { data, searchId, pageno } } });
        }

        [HttpGet("get-buyers-also-liked")]
        public IActionResult GetBuyersAlsoLiked([FromQuery] string loginFeedchannelID, [FromQuery] string searchforID)
        {
            try
            {
                var request = new WorkplaceRequest
                {
                    LoginFeedchannelID = loginFeedchannelID,
                    SearchforID = searchforID
                };

                var response = _service.GetBuyersAlsoLiked(request);

                // For compatibility with your existing frontend
                if (response.IsSuccess)
                {
                    var actionResult = new UI_ACTION_RESULT();

                    // Convert items to JSON string
                    var jsonData = System.Text.Json.JsonSerializer.Serialize(response.Items);
                    actionResult.result.Add(jsonData);

                    return Ok(new { ActionResult = actionResult });
                }
                else
                {
                    return BadRequest(new { error = response.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("buyers-also-liked")]
        public IActionResult GetBuyersAlsoLikedV2([FromQuery] WorkplaceRequest request)
        {
            var response = _service.GetBuyersAlsoLiked(request);
            return Ok(response);
        }
    }

    // Added this class since it's referenced in the controller
    public class UI_ACTION_RESULT
    {
        public List<string> result { get; set; } = new List<string>();
    }
}