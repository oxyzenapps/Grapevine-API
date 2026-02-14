using grapevineCommon.Model.Homes;
using grapevineServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace grapevineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomesController : ControllerBase
    {
        private readonly IHomesService _service;

        public HomesController(IHomesService service)
        {
            _service = service;
        }

        [HttpPost("UpdateFilterparameter")]
        public async Task<IActionResult> UpdateFilter([FromBody] UpdateFilterRequest request)
        {
            var searchId = await _service.UpdateFilterAsync(request, "cookie_ssn_here");
            return Ok(searchId);
        }

        [HttpGet("OH_get_search_sale_List_count")]
        public async Task<IActionResult> GetSaleListCount(string pageNo, string searchId, string sort, string listingStatusId, string feedChannelId, string myFeedChannelId)
        {
            var data = await _service.GetSaleListCountAsync(pageNo, searchId, sort, listingStatusId, feedChannelId, myFeedChannelId);
            return Ok(new { ActionResult = new { result = new[] { data } } });
        }

        [HttpGet("OH_get_search_project_List_count")]
        public async Task<IActionResult> GetProjectList([FromQuery] SearchProjectRequest request)
        {
            var result = await _service.GetProjectListAsync(request);
            return Ok(result);
        }

        [HttpGet("OH_Get_Search_Details")]
        public async Task<IActionResult> OH_Get_Search_Details([FromQuery] SearchDetailsRequest req)
        {
            return Ok(await _service.OH_Get_Search_Details(req));
        }

        [HttpGet("OH_Insert_Top_Developers")]
        public async Task<IActionResult> OH_Insert_Top_Developers([FromQuery] TopDevelopersRequest req)
        {
            return Ok(await _service.OH_Insert_Top_Developers(req));
        }

        [HttpPost("OH_Insert_Projects_QuesAns")]
        public async Task<IActionResult> OH_Insert_Projects_QuesAns([FromBody] ProjectQuesAns model)
        {
            if (model == null) return BadRequest("Invalid request data.");
            return Ok(await _service.OH_Insert_Projects_QuesAns(model));
        }

        [HttpPost("OH_Insert_Project_Price_Sheet")]
        public async Task<IActionResult> OH_Insert_Project_Price_Sheet([FromBody] ProjectPriceSheet model)
        {
            if (model == null) return BadRequest("Invalid request data.");
            return Ok(await _service.OH_Insert_Project_Price_Sheet(model));
        }

        [HttpPost("OH_insert_projectconfig_combinationarea")]
        public async Task<IActionResult> OH_insert_projectconfig_combinationarea([FromBody] ProjectConfigCombination model)
        {
            if (model == null) return BadRequest("Invalid request data.");
            return Ok(await _service.OH_insert_projectconfig_combinationarea(model));
        }

        [HttpPost("OH_insert_FloorLayouts")]
        public async Task<IActionResult> OH_insert_FloorLayouts([FromBody] FloorLayout model)
        {
            var result = await _service.OH_insert_FloorLayouts(model);
            return Ok(new { ActionResult = result });
        }

        [HttpPost("OH_insert_Buildings")]
        public async Task<IActionResult> OH_insert_Buildings([FromBody] Building model)
        {
            var result = await _service.OH_insert_Buildings(model);
            return Ok(new { ActionResult = result });
        }

        [HttpPost("OH_Insert_Rera_Details")]
        public async Task<IActionResult> OH_Insert_Rera_Details([FromBody] ReraDetails model)
        {
            var result = await _service.OH_Insert_Rera_Details(model);
            return Ok(new { ActionResult = result });
        }

        [HttpPost("OH_Insert_Bank_Approvals")]
        public async Task<IActionResult> OH_Insert_Bank_Approvals([FromBody] BankApproval model)
        {
            var result = await _service.OH_Insert_Bank_Approvals(model);
            return Ok(new { ActionResult = result });
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
        public IActionResult GetBuyersAlsoLikedV2([FromQuery] HomesRequest request)
        {
            return Ok(_service.GetBuyersAlsoLiked(request));
        }

        [HttpGet("OH_get_top_localities")]
        public async Task<IActionResult> OH_get_top_localities([FromQuery] string searchId)
        {
            if (string.IsNullOrEmpty(searchId)) return BadRequest(new { error = "Search ID is required" });
            return Ok(await _service.GetTopLocalitiesAsync(searchId));
        }

        [HttpGet("OH_Get_MHD_FilterDetail")]
        public async Task<IActionResult> OH_Get_MHD_FilterDetail(string FeedChannelID, string ListingCategoryID = "0", string DeveloperAgencyFeedChannelID = "0")
        {
            return Ok(await _service.OH_Get_MHD_FilterDetail(FeedChannelID, ListingCategoryID, DeveloperAgencyFeedChannelID));
        }

        [HttpPost("OH_Insert_MHD")]
        public async Task<IActionResult> OH_Insert_MHD([FromBody] MHDInsertRequest request)
        {
            return Ok(await _service.OH_Insert_MHD(request));
        }

        [HttpPost("OH_Insert_Projects_BankAccounts")]
        public async Task<IActionResult> OH_Insert_Projects_BankAccounts([FromBody] ProjectBankAccountItem item)
            => Ok(await _service.OH_Insert_Projects_BankAccounts(item));

        [HttpPost("OH_Insert_Agreement_Payment_CollectionSchedule")]
        public async Task<IActionResult> OH_Insert_Agreement_Payment_CollectionSchedule([FromBody] PaymentCollectionScheduleItem item)
            => Ok(await _service.OH_Insert_Agreement_Payment_CollectionSchedule(item));

        [HttpPost("OH_Insert_Project_Features")]
        public async Task<IActionResult> OH_Insert_Project_Features([FromBody] ProjectFeatureItem item)
            => Ok(await _service.OH_Insert_Project_Features(item));

        [HttpPost("OH_Insert_TermsAndConditions")]
        public async Task<IActionResult> OH_Insert_TermsAndConditions([FromBody] TermsAndConditionsItem item)
            => Ok(await _service.OH_Insert_TermsAndConditions(item));

        [HttpPost("OH_Insert_Project_Credits")]
        public async Task<IActionResult> OH_Insert_Project_Credits([FromBody] ProjectCreditItem item)
            => Ok(await _service.OH_Insert_Project_Credits(item));

        [HttpGet("Get_Premium_Location_Charges")]
        public async Task<IActionResult> Get_Premium_Location_Charges(string Project_ID)
        {
            var data = await _service.GetPremiumLocationCharges(Project_ID);
            return Ok(new { ActionResult = new { result = new[] { data } } });
        }

        [HttpGet("BindBuildingComponent")]
        public async Task<IActionResult> BindBuildingComponent(string ProjectID, string BuildingID)
        {
            return Ok(await _service.BindBuildingComponent(ProjectID, BuildingID));
        }

        [HttpPost("OH_Insert_Developer_Miscellaneous_Charges")]
        public async Task<IActionResult> OH_Insert_Developer_Miscellaneous_Charges([FromBody] MiscChargesRequest model)
        {
            if (model == null) return BadRequest("Invalid request data.");
            return Ok(await _service.OH_Insert_Developer_Miscellaneous_Charges(model));
        }

        [HttpPost("OH_Insert_Project_SubType_Association")]
        public async Task<IActionResult> OH_Insert_Project_SubType_Association([FromBody] ProjectSubTypeAssociationRequest model)
        {
            if (model == null) return BadRequest("Invalid request data.");
            return Ok(await _service.OH_Insert_Project_SubType_Association(model));
        }

        [HttpGet("Building_Availability")]
        public async Task<IActionResult> Building_Availability(string ProjectID, string BuildingID)
        {
            if (string.IsNullOrEmpty(ProjectID)) return BadRequest("ProjectID is required.");
            return Ok(await _service.Building_Availability(ProjectID, BuildingID));
        }

        [HttpPost("OH_Insert_Unit_Naming_Convention")]
        public async Task<IActionResult> OH_Insert_Unit_Naming_Convention([FromBody] UnitNamingConventionItem item)
        {
            var data = await _service.OH_Insert_Unit_Naming_Convention(item);
            return Ok(new { Result = data, Success = true });
        }

        [HttpGet("OH_generate_inventory")]
        public async Task<IActionResult> OH_generate_inventory(string Project_ID)
        {
            var data = await _service.OH_generate_inventory(Project_ID);
            return Ok(new { Result = data, Success = true });
        }

        [HttpPost("OH_Insert_Brand")]
        public async Task<IActionResult> OH_Insert_Brand([FromBody] BrandInsertRequest request)
        {
            return Ok(await _service.OH_Insert_Brand(request));
        }

        [HttpPost("OH_Insert_Project_Display_List")]
        public async Task<IActionResult> OH_Insert_Project_Display_List([FromBody] ProjectDisplayListRequest request)
        {
            return Ok(await _service.OH_Insert_Project_Display_List(request));
        }

        [HttpGet("bindprojectsubtype")]
        public async Task<IActionResult> bindprojectsubtype()
        {
            return Ok(await _service.bindprojectsubtype());
        }

        [HttpGet("Bind_CompanyEntityType")]
        public async Task<IActionResult> Bind_CompanyEntityType()
        {
            return Ok(await _service.Bind_CompanyEntityType());
        }

        [HttpGet("Bind_CompanyTaxStatusID")]
        public async Task<IActionResult> Bind_CompanyTaxStatusID()
        {
            return Ok(await _service.Bind_CompanyTaxStatusID());
        }

        [HttpPost("InsertUpdateCompany")]
        public async Task<IActionResult> InsertUpdateCompany([FromBody] InsertUpdateCompanyRequest request)
        {
            if (!string.IsNullOrEmpty(request.Established_Since))
            {
                if (DateTime.TryParse(request.Established_Since, out DateTime oDate))
                    request.Established_Since = oDate.ToString("yyyy-MM-dd");
            }

            string applicantId = "0";
            string applicantFeedChannelId = "0";

            // Removed 'Async' from method name to match interface
            var result = await _service.InsertUpdateCompany(request, applicantId, applicantFeedChannelId);
            return Ok(new { ActionResult = new { result = new List<CompanyResult> { result } } });
        }

        [HttpPost("UpdateAgenciesSalesPrice")]
        public async Task<IActionResult> UpdateAgenciesSalesPrice([FromBody] UpdateAgenciesSalesPriceRequest request)
        {
            // Removed 'Async' from method name to match interface
            await _service.UpdateAgenciesSalesPrice(request);
            return Ok("1");
        }

        [HttpPost("UpdateAgencyDealsIn")]
        public async Task<IActionResult> UpdateAgencyDealsIn([FromBody] UpdateAgencyDealsInRequest request)
        {
            // Removed 'Async' from method name to match interface
            var res = await _service.UpdateAgencyDealsIn(request);
            return Ok(res.ToString());
        }

        [HttpPost("OH_Insert_AgencyWotkSpace")]
        public async Task<IActionResult> OH_Insert_AgencyWotkSpace([FromBody] AgencyWorkspaceRequest request)
        {
            // Removed 'Async' from method name to match interface
            var res = await _service.OH_Insert_AgencyWotkSpace(request);
            return Ok(res.ToString());
        }

        [HttpGet("GetAllChatParticipants")]
        public async Task<IActionResult> GetAllChatParticipants(string SearchString, string FeedChannelParticipantTypeID, string WhereOneCanPostOnly, string PageNo, string PageSize, string SortOption = "1")
        {
            // Decrypt SSN logic here or pass from header
            var result = await _service.GetAllChatParticipants(SearchString, FeedChannelParticipantTypeID, WhereOneCanPostOnly, PageNo, PageSize, SortOption, "decrypted_ssn");
            return Ok(result);
        }

        [HttpPost("UpdateUserEmail")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateUserEmailRequest request)
        {
            var result = await _service.UpdateUserEmail(request);
            return Ok(result);
        }

        [HttpGet("CompanyProfileCompany")]
        public async Task<IActionResult> CompanyProfileCompany(string Company_ID, string ContactID = "", string FeedChannelID = "0", string PageID = "1", string PageSize = "10", string FilterName = "")
        {
            var result = await _service.CompanyProfileCompany(Company_ID, ContactID, FeedChannelID, PageID, PageSize, FilterName, "login_id");
            return Ok(new { ActionResult = new { result = new[] { result } } });
        }
    }
}