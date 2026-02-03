using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Data;

namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CompanyPolicyController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public CompanyPolicyController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		[HttpPost("insertCompanyPolicy")]
		public async Task<IActionResult> insertCompanyPolicy(
			int CompanyFeedChannelID,
			string DocumentFileName,
			string EffectivDate,
			int AddressID,
			string PolicyTitle,
			string Expired)
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_Policy_documents] " +
				"@Action='insert'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@DocumentFileName='{DocumentFileName}'," +
				$"@EffectivDate='{_utilityService.FormatDate(EffectivDate, false, "MM/dd/yyyy hh:mm tt")}'," +
				$"@AddressID='{AddressID}'," +
				$"@PolicyTitle='{PolicyTitle}'," +
				$"@Expired='{Expired}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("getCompanyPolicy")]
		public async Task<IActionResult> getCompanyPolicy(
			int CompanyFeedChannelID,
			int AddressID,
			string EffectivDate)
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_Policy_documents] " +
				"@Action='get'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@AddressID='{AddressID}'," +
				$"@EffectivDate='{_utilityService.FormatDate(EffectivDate)}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("deleteCompanyPolicy")]
		public async Task<IActionResult> deleteCompanyPolicy(
			int CompanyFeedChannelID,
			int AddressID,
			string EffectivDate = "",
			string PolicyTitle = "")
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_Policy_documents] " +
				"@Action='Delete'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@AddressID='{AddressID}'," +
				$"@PolicyTitle='{PolicyTitle}'," +
				$"@EffectivDate='{_utilityService.FormatDate(EffectivDate)}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);

			return Ok(result.result);
		}
	}
}