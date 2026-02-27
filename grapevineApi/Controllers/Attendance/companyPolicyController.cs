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

		public class InsertCompanyPolicyRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public string DocumentFileName { get; set; } = "";
			public string EffectivDate { get; set; } = "";
			public int AddressID { get; set; } = 0;
			public string PolicyTitle { get; set; } = "";
			public string Expired { get; set; } = "";
		}

		[HttpPost("insertCompanyPolicy")]
		public async Task<IActionResult> insertCompanyPolicy([FromBody] InsertCompanyPolicyRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			string DocumentFileName = request.DocumentFileName;
			string EffectivDate = request.EffectivDate;
			int AddressID = request.AddressID;
			string PolicyTitle = request.PolicyTitle;
			string Expired = request.Expired;

			 
			string sqlQuery =
				"exec ode.dbo.[ode_insert_Company_Policy_documents] " +
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

		public class GetCompanyPolicyRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int AddressID { get; set; } = 0;
			public string EffectivDate { get; set; } = "";
		}

		[HttpPost("getCompanyPolicy")]
		public async Task<IActionResult> getCompanyPolicy([FromBody] GetCompanyPolicyRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int AddressID = request.AddressID;
			string EffectivDate = request.EffectivDate;

			string sqlQuery =
				"exec ode.dbo.[ode_insert_Company_Policy_documents] " +
				"@Action='get'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@AddressID='{AddressID}'," +
				$"@EffectivDate='{_utilityService.FormatDate(EffectivDate)}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class DeleteCompanyPolicyRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int AddressID { get; set; } = 0;
			public string EffectivDate { get; set; } = "";
			public string PolicyTitle { get; set; } = "";
		}

		[HttpPost("deleteCompanyPolicy")]
		public async Task<IActionResult> deleteCompanyPolicy([FromBody] DeleteCompanyPolicyRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int AddressID = request.AddressID;
			string EffectivDate = request.EffectivDate;
			string PolicyTitle = request.PolicyTitle;

			string sqlQuery =
				"exec ode.dbo.[ode_insert_Company_Policy_documents] " +
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