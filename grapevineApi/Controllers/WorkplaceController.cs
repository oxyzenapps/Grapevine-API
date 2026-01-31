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

        public WorkplaceController(IWorkplaceService service)
        {
            _service = service;
        }

        [HttpGet("GetCurrentLocationProgram")]
        public async Task<IActionResult> GetCurrentLocation(string contactId, int websiteId)
        {
            var result = await _service.GetCurrentLocationAsync(contactId, websiteId);
            return Ok(result);
        }

        [HttpPost("UpdateFilterparameter")]
        public async Task<IActionResult> UpdateFilter([FromBody] UpdateFilterRequest request)
        {
            var searchId = await _service.UpdateFilterAsync(request);
            return Ok(new { SearchID = searchId });
        }

        [HttpGet("ode_get_location_search")]
        public async Task<IActionResult> SearchLocation(string query, string localityRequired = "0")
        {
            var result = await _service.SearchLocationAsync(query, localityRequired);
            return Ok(result);
        }

        [HttpGet("OH_get_search_sale_List")]
        public async Task<IActionResult> GetSaleListCount(string pageNo, string searchId, string sort, string listingStatusId, string feedChannelId, string myFeedChannelId)
        {
            var data = await _service.GetSaleListCountAsync(pageNo, searchId, sort, listingStatusId, feedChannelId, myFeedChannelId);
            return Ok(new { ActionResult = new { result = new List<string> { data } } });
        }

        [HttpGet("OH_get_search_agencies_List")]
        public async Task<IActionResult> GetAgenciesList(string pageno, string searchId, string sort = "", string feedChannelId = "0", string listingStatusId = "4", string myFeedChannelId = "0")
        {
            var data = await _service.GetAgenciesListAsync(pageno, searchId, sort, feedChannelId, listingStatusId, myFeedChannelId);
            return Ok(new { ActionResult = new { result = new List<object> { data, searchId, pageno } } });
        }

        [HttpGet("OH_get_search_Developers_List")]
        public async Task<IActionResult> GetDevelopersList(string pageno, string searchId, string sort = "", string feedChannelId = "0", string listingStatusId = "4", string myFeedChannelId = "0")
        {
            var data = await _service.GetDevelopersListAsync(pageno, searchId, sort, feedChannelId, listingStatusId, myFeedChannelId);
            return Ok(new { ActionResult = new { result = new List<object> { data, searchId, pageno } } });
        }

        

        [HttpGet("OH_get_buyers_also_liked")]
        public IActionResult GetBuyersAlsoLikedV2([FromQuery] WorkplaceRequest request)
        {
            var response = _service.GetBuyersAlsoLiked(request);
            return Ok(response);
        }

        [HttpGet("OH_get_top_localities")]
        public async Task<IActionResult> OH_get_top_localities([FromQuery] string searchId)
        {
            if (string.IsNullOrEmpty(searchId))
            {
                return BadRequest(new { error = "Search ID is required" });
            }

            var result = await _service.GetTopLocalitiesAsync(searchId);

            if (!result.Success)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpPost("crm_Insert_Feed_Likes")]
        public async Task<IActionResult> InsertFeedLike(FeedLikeRequest request)
        {
            var result = await _service.AddFeedLike(request);
            return Ok(result);
        }

        [HttpPost("SaveProjectResponse")]
        public async Task<IActionResult> SaveProjectResponse(ProjectResponseRequest request)
        {
            var result = await _service.CreateProjectResponse(request);
            return Ok(result);
        }

        [HttpGet("OH_Get_Next_Lead_Associate")]
        public async Task<IActionResult> GetNextLeadAssociate(string projectId, string projectTypeId = "")
        {
            var result = await _service.GetAssociates(projectId, projectTypeId);
            return Ok(result);
        }


    }

 }