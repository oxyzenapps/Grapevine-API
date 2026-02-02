using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
 
namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class commonController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public commonController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		[HttpGet("searchGrapevine")]
		public async Task<IActionResult> searchGrapevine(
			int WebsiteID = 9, string LoginID = "", int LoginFeedChannelID = 0, string SearchString = "",
			int Posts = 0, int FeedChannels = 1, int FeedChannelParticipantTypeID = 0, int ExFeedChannelParticipantTypeID = 0,
			int WhereOneCanPostOnly = 0, int PageId = 1, int PageSize = 20, string SortOption = "")
		{
			string sqlQuery =
				"exec glivebooks.dbo.crm_feed_search_grapevine " +
				$"@WebsiteID='{WebsiteID}'," +
				$"@LoginID='{LoginID}'," +
				$"@LoginFeedCHannelID='{LoginFeedChannelID}'," +
				$"@SearchString='{SearchString}'," +
				$"@Posts='{Posts}'," +
				$"@FeedChannels='{FeedChannels}'," +
				$"@FeedChannelParticipantTypeID='{FeedChannelParticipantTypeID}'," +
				$"@ExFeedChannelParticipantTypeID='{ExFeedChannelParticipantTypeID}'," +
				$"@WhereOneCanPostOnly='{WhereOneCanPostOnly}'," +
				$"@PageId='{PageId}'," +
				$"@PageSize='{PageSize}'," +
				$"@SortOption='{SortOption}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Count > 0) return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpGet("getFeedUserPreferences")]
		public async Task<IActionResult> getFeedUserPreferences(int LoginID = 0)
		{
			string sqlQuery =
				$"exec glivebooks.dbo.crm_get_Feed_UserPreferences @LoginID='{LoginID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Count > 0) return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("updateFeedUserPreferences")]
		public async Task<IActionResult> updateFeedUserPreferences(
			int LoginID = 0, int FeedByID = 0, int LastUsedMediaButtonID = 0, int LastUsedActivityButtonID = 0,
			int LastUsedListingButtonID = 0, int LastUsedListingCategoryID = 0, int LastUsedListingTopicID = 0,
			int LastUsedVisibilityID = 0, int UpdateAudio = 0, int AudioStatus = 0,
			int ListingFeedChannelID = 0, int DefaultAddressId = 0, int LastUsedFolderID = 0,
			int LastUsedEntityFeedChannelID = 0)
		{
			string sqlQuery =
				"exec glivebooks.dbo.crm_update_feed_user_preferences " +
				$"@LoginID='{LoginID}'," +
				$"@FeedByID='{FeedByID}'," +
				$"@LastUsedMediaButtonID='{LastUsedMediaButtonID}'," +
				$"@LastUsedActivityButtonID='{LastUsedActivityButtonID}'," +
				$"@LastUsedListingButtonID='{LastUsedListingButtonID}'," +
				$"@LastUsedListingCategoryID='{LastUsedListingCategoryID}'," +
				$"@LastUsedListingTopicID='{LastUsedListingTopicID}'," +
				$"@LastUsedVisibilityID='{LastUsedVisibilityID}'," +
				$"@UpdateAudio='{UpdateAudio}'," +
				$"@AudioStatus='{AudioStatus}'," +
				$"@ListingFeedChannelID='{ListingFeedChannelID}'," +
				$"@DefaultAddressId='{DefaultAddressId}'," +
				$"@LastUsedFolderID='{LastUsedFolderID}'," +
				$"@LastUsedEntityFeedChannelID='{LastUsedEntityFeedChannelID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Count > 0) return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpGet("getCompanyDetails")]
		public async Task<IActionResult> getCompanyDetails(
			int LoginID = 0, string Applicant_id = "", string FilterName = "", int CompanyID = 0,
			int FeedChannelID = 0, int PageID = 1, int PageSize = 20, int OnlyOneAddress = 1)
		{
			string sqlQuery =
				"exec ode.dbo.ode_get_company_details " +
				$"@LoginID='{LoginID}'," +
				$"@Applicant_id='{Applicant_id}'," +
				$"@FilterName='{FilterName}'," +
				$"@CompanyID='{CompanyID}'," +
				$"@FeedChannelID='{FeedChannelID}'," +
				$"@PageID='{PageID}'," +
				$"@PageSize='{PageSize}'," +
				$"@OnlyOneAddress='{OnlyOneAddress}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Count > 0) return BadRequest(result.errors);

			return Ok(result.result);
		}
	}
}
