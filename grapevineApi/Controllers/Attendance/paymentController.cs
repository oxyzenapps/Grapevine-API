using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class payrollEnteredValuesController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public payrollEnteredValuesController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		public class PayrollEnteredValueDto
		{
			public int CompanyFeedChannelID { get; set; }
			public int ExecutiveFeedChannelID { get; set; }
			public int LoginFeedChannelID { get; set; }
			public int PayComponentID { get; set; }
			public int GroupID { get; set; }
			public string YearMonth { get; set; } = "";
			public string Currency { get; set; } = "";
			public string GroupValue { get; set; } = "";
			public string Value { get; set; } = "";
		}

		[HttpPost("savePayrollEnteredValue")]
		public async Task<IActionResult> savePayrollEnteredValue([FromBody] PayrollEnteredValueDto data)
		{
			string sqlQuery = "exec ode.dbo.[ode_insert_Company_executive_Payroll_Entered_values] " +
							  "@Action='Insert'," +
							  "@CompanyFeedChannelID='" + data.CompanyFeedChannelID + "'," +
							  "@ExecutiveFeedChannelID='" + data.ExecutiveFeedChannelID + "'," +
							  "@LoginFeedChannelID='" + data.LoginFeedChannelID + "'," +
							  "@PayComponentID='" + data.PayComponentID + "'," +
							  "@GroupID='" + data.GroupID + "'," +
							  "@YearMonth='" + data.YearMonth + "'," +
							  "@Currency='" + data.Currency + "'," +
							  "@GroupValue='" + data.GroupValue + "'," +
							  "@Value='" + data.Value + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpGet("getPayrollEnteredValue")]
		public async Task<IActionResult> getPayrollEnteredValue(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int LoginFeedChannelID = 0,
			int PayComponentID = 0,
			int GroupID = 0,
			string YearMonth = "")
		{
			string sqlQuery = "exec ode.dbo.[ode_insert_Company_executive_Payroll_Entered_values] " +
							  "@Action='Get'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
							  "@LoginFeedChannelID='" + LoginFeedChannelID + "'," +
							  "@PayComponentID='" + PayComponentID + "'," +
							  "@GroupID='" + GroupID + "'," +
							  "@YearMonth='" + YearMonth + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpDelete("deletePayrollEnteredValue")]
		public async Task<IActionResult> deletePayrollEnteredValue(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int LoginFeedChannelID = 0,
			int PayComponentID = 0,
			int GroupID = 0,
			string YearMonth = "")
		{
			string sqlQuery = "exec ode.dbo.[ode_insert_Company_executive_Payroll_Entered_values] " +
							  "@Action='Delete'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
							  "@LoginFeedChannelID='" + LoginFeedChannelID + "'," +
							  "@PayComponentID='" + PayComponentID + "'," +
							  "@GroupID='" + GroupID + "'," +
							  "@YearMonth='" + YearMonth + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		// Dropdown APIs
		[HttpGet("getPayrollGroupList")]
		public async Task<IActionResult> getPayrollGroupList(int CompanyFeedChannelID = 0, int GroupID = 0)
		{
			string sqlQuery = "exec ode.dbo.[ode_insert_Company_paycomponent_group] " +
							  "@Action='Get'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@GroupID='" + GroupID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpGet("getPayrollComponentList")]
		public async Task<IActionResult> getPayrollComponentList(int CompanyFeedChannelID = 0, int GroupID = 0, int PayComponentID = 0)
		{
			string sqlQuery = "exec ode.dbo.[ode_insert_Company_paycomponent_group_details] " +
							  "@Action='Get'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@PayComponentID='" + PayComponentID + "'," +
							  "@GroupID='" + GroupID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
	}
}
