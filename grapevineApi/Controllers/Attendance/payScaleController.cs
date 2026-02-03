using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
 

namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class payScaleController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public payScaleController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		// ===================== EXECUTIVE PAYROLL =====================
		[HttpGet("getExePayroll")]
		public async Task<IActionResult> getExePayroll(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			string YearMonth = "",
			string Approved = "",
			string NotApproved = "",
			string Paid = "",
			string NotPaid = "",
			bool Print = false
		)
		{
			string sqlQuery = "ode.dbo.[ode_insert_company_Pay_Scales] " +
							  "@Action='Get Executive Pay roll'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
							  "@YearMonth='" + YearMonth + "'," +
							  "@Approved='" + Approved + "'," +
							  "@NotApproved='" + NotApproved + "'," +
							  "@Paid='" + Paid + "'," +
							  "@NotPaid='" + NotPaid + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Count > 0)
				return BadRequest(result.errors);

			// For Print functionality, you can handle RDL report generation here
			if (Print)
			{
				// Placeholder for RDL generation logic
				string reportLink = "Link_To_Generated_Report";
				return Ok(reportLink);
			}

			return Ok(result.result);
		}

		// ===================== PAYROLL ENTERED VALUES =====================
		[HttpPost("savePayrollEnteredValue")]
		public async Task<IActionResult> savePayrollEnteredValue([FromBody] dynamic data)
		{
			string sqlQuery = "ode.dbo.[ode_insert_Company_executive_Payroll_Entered_values] " +
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

			if (result.errors.Count > 0)
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
			string YearMonth = ""
		)
		{
			string sqlQuery = "ode.dbo.[ode_insert_Company_executive_Payroll_Entered_values] " +
							  "@Action='Get'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
							  "@LoginFeedChannelID='" + LoginFeedChannelID + "'," +
							  "@PayComponentID='" + PayComponentID + "'," +
							  "@GroupID='" + GroupID + "'," +
							  "@YearMonth='" + YearMonth + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Count > 0)
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
			string YearMonth = ""
		)
		{
			string sqlQuery = "ode.dbo.[ode_insert_Company_executive_Payroll_Entered_values] " +
							  "@Action='Delete'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
							  "@LoginFeedChannelID='" + LoginFeedChannelID + "'," +
							  "@PayComponentID='" + PayComponentID + "'," +
							  "@GroupID='" + GroupID + "'," +
							  "@YearMonth='" + YearMonth + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Count > 0)
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		// ===================== PAY SCALE CRUD =====================
		[HttpPost("insertCompanyPayScale")]
		public async Task<IActionResult> insertCompanyPayScale(
			int CompanyFeedChannelID,
			int ExecutiveFeedChannelID,
			string PayScale,
			string Currency,
			decimal BasicPay
		)
		{
			string sqlQuery = "ode.dbo.[ode_insert_company_Pay_Scales] " +
							  "@Action='Insert company Pay Scale'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
							  "@PayScale='" + PayScale + "'," +
							  "@Currency='" + Currency + "'," +
							  "@BasicPay='" + BasicPay + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Count > 0)
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpGet("getCompanyPayScale")]
		public async Task<IActionResult> getCompanyPayScale(int CompanyFeedChannelID, int ExecutiveFeedChannelID, int PayScaleID)
		{
			string sqlQuery = "ode.dbo.[ode_insert_company_Pay_Scales] " +
							  "@Action='Get company Pay Scale'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
							  "@PayScaleID='" + PayScaleID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Count > 0)
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpDelete("deleteCompanyPayScale")]
		public async Task<IActionResult> deleteCompanyPayScale(int CompanyFeedChannelID, int ExecutiveFeedChannelID, int PayScaleID)
		{
			string sqlQuery = "ode.dbo.[ode_insert_company_Pay_Scales] " +
							  "@Action='Delete company Pay Scale'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
							  "@PayScaleID='" + PayScaleID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Count > 0)
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		// ===================== PAY COMPONENTS =====================
		[HttpGet("getPayComponents")]
		public async Task<IActionResult> getPayComponents(int CompanyFeedChannelID = 0, int ExecutiveFeedChannelID = 0)
		{
			string sqlQuery = "ode.dbo.[ode_insert_company_Pay_Scales] " +
							  "@Action='Get Pay Components'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Count > 0) return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("insertCompanyPayScaleComponent")]
		public async Task<IActionResult> insertCompanyPayScaleComponent(
			int CompanyFeedChannelID,
			int ExecutiveFeedChannelID,
			int PayScaleID,
			int PayComponentID,
			int CalculationTypeID,
			decimal Percentage,
			decimal Amount,
			string Currency,
			decimal MinValue,
			decimal MaxValue
		)
		{
			string sqlQuery = "ode.dbo.[ode_insert_company_Pay_Scales] " +
							  "@Action='Insert company Pay Scale Component'," +
							  "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
							  "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
							  "@PayScaleID='" + PayScaleID + "'," +
							  "@PayComponentID='" + PayComponentID + "'," +
							  "@CalculationTypeID='" + CalculationTypeID + "'," +
							  "@Percentage='" + Percentage + "'," +
							  "@Amount='" + Amount + "'," +
							  "@Currency='" + Currency + "'," +
							  "@MinValue='" + MinValue + "'," +
							  "@MaxValue='" + MaxValue + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Count > 0) return BadRequest(result.errors);

			return Ok(result.result);
		}

		// You can continue converting other methods like getCompanyPayScaleComponent, deleteCompanyPayScaleComponent, payrollPosting, statutoryPayroll similarly
	}
}
