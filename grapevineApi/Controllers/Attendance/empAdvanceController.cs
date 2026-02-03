using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class empAdvanceController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public empAdvanceController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		[HttpPost("getExeSummary")]
		public async Task<IActionResult> getExeSummary(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int AdvancebyCompanyFeedChannelID = 0,
			int AdvanceID = 0)
		{
			string sqlQuery =
				$"ode.dbo.[ode_insert_Company_executive_advances] " +
				$"@Action='Get Executive Summary'," +
				$"@CompanyFeedChannelID={CompanyFeedChannelID}," +
				$"@ExecutiveFeedChannelID={ExecutiveFeedChannelID}," +
				$"@AdvancebyCompanyFeedChannelID={AdvancebyCompanyFeedChannelID}," +
				$"@AdvanceID={AdvanceID}";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors?.Count > 0)
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("insertEmpAdvance")]
		public async Task<IActionResult> insertEmpAdvance(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int AdvancebyCompanyFeedChannelID = 0,
			int AdvanceID = 0,
			string Amount = "",
			string AdvanceDate = "",
			string EMIs = "",
			string EMI = "")
		{
			string sqlQuery =
				$"ode.dbo.[ode_insert_Company_executive_advances] " +
				$"@Action='Insert Emp Advance'," +
				$"@CompanyFeedChannelID={CompanyFeedChannelID}," +
				$"@ExecutiveFeedChannelID={ExecutiveFeedChannelID}," +
				$"@AdvancebyCompanyFeedChannelID={AdvancebyCompanyFeedChannelID}," +
				$"@AdvanceID={AdvanceID}," +
				$"@Amount='{Amount}'," +
				$"@AdvanceDate='{AdvanceDate}'," +
				$"@EMIs='{EMIs}'," +
				$"@EMI='{EMI}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors?.Count > 0)
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("insertAdvanceProvidingCompany")]
		public async Task<IActionResult> insertAdvanceProvidingCompany(
			int CompanyFeedChannelID = 0,
			int AdvancebyCompanyFeedChannelID = 0,
			string Currency = "",
			decimal ProcessingFee = 0,
			decimal InterestRatePA = 0,
			bool Active = false)
		{
			// Convert bool to 1/0 and decimal to invariant culture
			string activeValue = Active ? "1" : "0";
			string procFee = ProcessingFee.ToString(CultureInfo.InvariantCulture);
			string interest = InterestRatePA.ToString(CultureInfo.InvariantCulture);

			string sqlQuery =
				$"ode.dbo.[ode_insert_Company_advance_providing_company] " +
				$"@Action='Insert Advance Providing Company'," +
				$"@CompanyFeedChannelID={CompanyFeedChannelID}," +
				$"@AdvancebyCompanyFeedChannelID={AdvancebyCompanyFeedChannelID}," +
				$"@Currency='{Currency}'," +
				$"@ProcessingFee={procFee}," +
				$"@InterestRatePA={interest}," +
				$"@Active={activeValue}";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors?.Count > 0)
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		// Other methods follow same pattern: getExeAdvanceDetails, deleteExeAdvance, getAdvanceProvidingCompany, deleteAdvanceProvidingCompany
	}
}
