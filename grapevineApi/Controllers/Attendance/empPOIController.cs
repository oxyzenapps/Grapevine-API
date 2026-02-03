using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Data;
namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class empPOIController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public empPOIController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		[HttpPost("addEmpPOI")]
		public async Task<IActionResult> addEmpPOI(
			string CompanyFeedChannelID = "0",
			string ExecutiveFeedChannelID = "",
			string FeedID = "",
			string Lat = "",
			string Lng = "",
			string Comments = "")
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_POI]" +
				"@Action='Insert EMP POI'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
				"@FeedID='" + FeedID + "'," +
				"@Lat='" + Lat + "'," +
				"@Lng='" + Lng + "'," +
				"@Comments='" + Comments + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("insertEmpPOI")]
		public async Task<IActionResult> insertEmpPOI(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			string FeedID = "",
			string Lat = "",
			string Lng = "",
			string Comments = "")
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_POI]" +
				"@Action='Insert EMP POI'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
				"@FeedID='" + FeedID + "'," +
				"@Lat='" + Lat + "'," +
				"@Lng='" + Lng + "'," +
				"@Comments='" + Comments + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("getEmpPOI")]
		public async Task<IActionResult> getEmpPOI(
			string CompanyFeedChannelID = "0",
			string ExecutiveFeedChannelID = "",
			string LoginFeedChannelID = "",
			string POICompanyFeedChannelID = "",
			string POICompanyAddressID = "",
			string EndDate = "",
			string StartDate = "")
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_POI]" +
				"@Action='Get Emp POI'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
				"@StartDate='" + StartDate + "'," +
				"@EndDate='" + EndDate + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("getEmpPoiList")]
		public async Task<IActionResult> getEmpPoiList(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			string EndDate = "",
			string StartDate = "")
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_POI]" +
				"@Action='Get Emp POI'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
				"@StartDate='" + _utilityService.FormatDate(StartDate, false, "MM-dd-yyyy hh:mm tt") + "'," +
				"@EndDate='" + _utilityService.FormatDate(EndDate, false, "MM-dd-yyyy hh:mm tt") + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);

			return Ok(result.result);
		}
	}
}
