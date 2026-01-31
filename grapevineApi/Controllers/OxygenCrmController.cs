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

            [HttpPost("GetProjectLead")]
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

        [HttpPost("crm_get_Lead_Project_summary")]
        public async Task<IActionResult> GetLeadProjectSummary([FromBody] LeadProjectSummaryRequest request)
        {
            var result = await _oxygenCrmService.GetLeadProjectSummaryAsync(request);
            return Ok(result);
        }

        [HttpPost("OH_Delete_Agency")]
        public async Task<IActionResult> DeleteAgency([FromBody] OHDeleteAgencyRequest request)
        {
            var result = await _oxygenCrmService.DeleteAgencyAsync(request);
            return Ok(result);
        }

        [HttpPost("crm_get_activity_Channel")]
        public async Task<IActionResult> ShowChannels([FromBody] ShowChannelsRequest request)
        {
            // Even though request is empty, keeping consistency
            var result = await _oxygenCrmService.GetChannelsAsync(request);
            return Ok(result);
        }

        

        [HttpPost("GET_CP_tagging_parameters")]
        public async Task<IActionResult> GetCPTaggingParameters([FromBody] CPTaggingParametersRequest request)
        {
            var result = await _oxygenCrmService.GetCPTaggingParametersAsync(request);
            return Ok(result);
        }

        [HttpPost("insert_lead_CP_tagging")]
        public async Task<IActionResult> InsertCPTagging([FromBody] InsertCPTaggingRequest request)
        {
            var result = await _oxygenCrmService.InsertOrUpdateCPTaggingAsync(request);
            return Ok(result);
        }

        
        [HttpPost(" RE_getMediaType")]
        public async Task<IActionResult> GetMediaTypeAndMasters([FromBody] GetMediaTypeRequest request)
        {
            var result = await _oxygenCrmService.GetMediaTypeAndRelatedMastersAsync(request);
            return Ok(result);
        }

        
    }
}