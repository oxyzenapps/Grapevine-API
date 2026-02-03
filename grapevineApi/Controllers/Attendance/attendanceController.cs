using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Data;
namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class attendanceController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public attendanceController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		// ================= COMPANY ATTENDANCE ADMIN =================
		[HttpPost("companyAttendenceAdmin")]
		public async Task<IActionResult> companyAttendenceAdmin(int CompanyFeedChannelID = 0, int ExecutiveFeedChannelID = 0)
		{
			string sqlQuery =
				$"select ode.dbo.ode_is_company_attendance_admin({CompanyFeedChannelID},{ExecutiveFeedChannelID})";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		// ================= INSERT LOG =================
		[HttpPost("insertLog")]
		public async Task<IActionResult> insertLog(
			string LogDateTime = "",
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int FeedID = 0,
			int PlaceID = 0,
			string TimeLogTypeID = "",
			string lat = "",
			string lng = "")
		{
			string LogText = TimeLogTypeID == "1" ? "Check-in" :
							 TimeLogTypeID == "-1" ? "Check-out" : " ";

			string sqlQuery =
				"ode.dbo.ode_insert_company_executive_attendance " +
				"@Action='log'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@FeedID='{FeedID}'," +
				$"@PlaceID='{PlaceID}'," +
				$"@TimeLogTypeID='{TimeLogTypeID}'," +
				$"@LogText='{LogText}'," +
				$"@lat='{lat}'," +
				$"@lng='{lng}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		// ================= GET LOG =================
		[HttpPost("getLog")]
		public async Task<IActionResult> getLog(
			string LogDateTime = "",
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			bool printVisitCard = false)
		{
			string sqlQuery =
				"ode.dbo.ode_insert_company_executive_attendance " +
				"@Action='get log'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@LogDateTime='{LogDateTime}'";

			if (printVisitCard)
			{
				return Ok("pending");
				//var datasetsResult = await _utilityService.GetDataResultAsync(sqlQuery);

				//if (datasetsResult.result == null || datasetsResult.result.Count < 3)
				//	return BadRequest("Required data not found for visiting card.");

				//var thirdDataset = datasetsResult.result["dataset3"] as IEnumerable<dynamic>;

				//if (thirdDataset == null || !thirdDataset.Any())
				//	return BadRequest("Required data not found for visiting card.");

				//var firstRow = thirdDataset.First();
				//string fName = firstRow.ExecutiveName;
				//string filename = Regex.Replace(fName + "Visting Card.png", @"\s+", "");

				//string rdlcSource = ",,Companyds";
				//string rdlcPath = "~/Reports/VisitingCard/VisitingCard.rdlc";

				//string link = _utilityService.GenerateRdlcReport(filename, datasetsResult.result, rdlcSource, rdlcPath);

				//return Ok(link);
			}



			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		// ================= MONTHLY ATTENDANCE =================
		[HttpPost("getMonthlyAttendance")]
		public async Task<IActionResult> getMonthlyAttendance(
			string PresenceDate = "",
			int CompanyFeedChannelID = 0,
			int ManagerFeedChannelID = 0)
		{
			string sqlQuery =
				"ode.dbo.ode_get_company_employee_attendance " +
				"@Action='Get Monthly Attendance'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ManagerFeedChannelID='{ManagerFeedChannelID}'," +
				$"@PresenceDate='{_utilityService.FormatDate(PresenceDate)}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		// ================= GET CURRENCY =================
		[HttpPost("getCurrencyList")]
		public async Task<IActionResult> getCurrencyList()
		{ 
			var result = await _utilityService.GetDataResultAsync("glivebooks.dbo.BindCurrency");
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		// ================= COMPANY ATTENDANCE STATUS =================
		[HttpPost("getCompanyAttendanceStatus")]
		public async Task<IActionResult> getCompanyAttendanceStatus()
		{
			var result = await _utilityService
				.GetDataResultAsync("ode.dbo.ode_get_CompanyAttendanceStatus");

			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
	}
}