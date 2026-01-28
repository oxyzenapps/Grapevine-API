using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using grapevineCommon.Model.OxygenCrm;
using grapevineServices.Interfaces;    
using System.Threading.Tasks;

namespace grapevineApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OxygenCrmController : ControllerBase
    {
        private readonly IOxygenCrmService _oxygenCrmService;

        public OxygenCrmController(IOxygenCrmService oxygenCrmService)
        {
            _oxygenCrmService = oxygenCrmService;
        }

        // ───────────────────────────────────────────────
        // Lead Related Endpoints
        // ───────────────────────────────────────────────

        [HttpPost("GetProjectLeads")]
        public async Task<IActionResult> GetProjectLeads([FromBody] GetProjectLeadRequest request)
        {
            var result = await _oxygenCrmService.GetProjectLeadsAsync(request);
            return Ok(result);
        }

        [HttpPost("GetAllLeadsChild")]
        public async Task<IActionResult> GetAllLeadsChild([FromBody] GetAllleadschildRequest request)
        {
            var result = await _oxygenCrmService.GetAllLeadsChildAsync(request);
            return Ok(result);
        }

        [HttpPost("GetLeadProjectSummary")]
        public async Task<IActionResult> GetLeadProjectSummary([FromBody] LeadProjectSummaryRequest request)
        {
            var result = await _oxygenCrmService.GetLeadProjectSummaryAsync(request);
            return Ok(result);
        }

        // ───────────────────────────────────────────────
        // Agency / Channel Related Endpoints
        // ───────────────────────────────────────────────

        [HttpPost("DeleteAgency")]
        public async Task<IActionResult> DeleteAgency([FromBody] OHDeleteAgencyRequest request)
        {
            var result = await _oxygenCrmService.DeleteAgencyAsync(request);
            return Ok(result);
        }

        [HttpPost("ShowChannels")]
        public async Task<IActionResult> ShowChannels([FromBody] ShowChannelsRequest request)
        {
            // Even though request is empty, keeping consistency
            var result = await _oxygenCrmService.GetChannelsAsync(request);
            return Ok(result);
        }

        // ───────────────────────────────────────────────
        // Tagging / Configuration Endpoints
        // ───────────────────────────────────────────────

        [HttpPost("GetCPTaggingParameters")]
        public async Task<IActionResult> GetCPTaggingParameters([FromBody] CPTaggingParametersRequest request)
        {
            var result = await _oxygenCrmService.GetCPTaggingParametersAsync(request);
            return Ok(result);
        }

        [HttpPost("InsertCPTagging")]
        public async Task<IActionResult> InsertCPTagging([FromBody] InsertCPTaggingRequest request)
        {
            var result = await _oxygenCrmService.InsertOrUpdateCPTaggingAsync(request);
            return Ok(result);
        }

        // ───────────────────────────────────────────────
        // Master Data / Dropdowns
        // ───────────────────────────────────────────────

        [HttpPost("GetMediaTypeAndMasters")]
        public async Task<IActionResult> GetMediaTypeAndMasters([FromBody] GetMediaTypeRequest request)
        {
            var result = await _oxygenCrmService.GetMediaTypeAndRelatedMastersAsync(request);
            return Ok(result);
        }

        
    }
}