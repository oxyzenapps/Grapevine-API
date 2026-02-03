using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Data;

namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class atdLeavesController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public atdLeavesController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		[HttpPost("getEveryOneOnADay")]
		public async Task<IActionResult> getEveryOneOnADay(
			int CompanyFeedChannelID = 0,
			int ManagerFeedChannelID = 0,
			string PresenceDate = "",
			int PageID = 0,
			string SearchString = "",
			string PresenceStatusID = "")
		{
			string sqlQuery =
				"ode.dbo.ode_get_company_employee_attendance " +
				"@Action='Get EveryOne On A Day'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ManagerFeedChannelID='{ManagerFeedChannelID}'," +
				$"@PresenceDate='{_utilityService.FormatDate(PresenceDate)}'," +
				$"@PageID='{PageID}'," +
				$"@SearchString='{SearchString}'," +
				$"@PresenceStatusID='{PresenceStatusID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		[HttpPost("insertLeave")]
		public async Task<IActionResult> insertLeave(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int LeaveTypeID = 0,
			string LeaveReason = "",
			string FromDate = "",
			string ToDate = "")
		{
			string sqlQuery =
				"Declare @ReturnValue int=0; ode.dbo.ode_insert_company_executive_attendance " +
				"@Action='Apply Leave'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@LeaveTypeID='{LeaveTypeID}'," +
				$"@LeaveReason=N'{LeaveReason.Replace("'", "''")}'," +
				$"@ToDate='{_utilityService.FormatDate(ToDate)}'," +
				$"@FromDate='{_utilityService.FormatDate(FromDate)}'," +
				"@ReturnValue=@ReturnValue output; select @ReturnValue as ReturnValue";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		[HttpPost("updateLeave")]
		public async Task<IActionResult> updateLeave(
			int AdminFeedChannelID = 0,
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int LeaveTypeID = 0,
			int LeaveStatusID = 0,
			string LeaveReason = "",
			string ToDate = "",
			string FromDate = "")
		{
			string sqlQuery =
				"Declare @ReturnValue int=0; ode.dbo.ode_insert_company_executive_attendance " +
				"@Action='Update Leave'," +
				$"@AdminFeedChannelID='{AdminFeedChannelID}'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@LeaveStatusID='{LeaveStatusID}'," +
				$"@LeaveTypeID='{LeaveTypeID}'," +
				$"@LeaveReason=N'{LeaveReason.Replace("'", "''")}'," +
				$"@ToDate='{_utilityService.FormatDate(ToDate)}'," +
				$"@FromDate='{_utilityService.FormatDate(FromDate)}'," +
				"@ReturnValue=@ReturnValue output; select @ReturnValue as ReturnValue";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		[HttpPost("getLeave")]
		public async Task<IActionResult> getLeave(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int LeaveTypeID = 0,
			string FromDate = "")
		{
			string sqlQuery =
				"ode.dbo.ode_insert_company_executive_attendance " +
				"@Action='Get Leave'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@LeaveTypeID='{LeaveTypeID}'," +
				$"@FromDate='{_utilityService.FormatDate(FromDate)}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		[HttpPost("getAllLeave")]
		public async Task<IActionResult> getAllLeave(
			int CompanyFeedChannelID = 0,
			int ManagerFeedChannelID = 0,
			int LeaveStatus = -1,
			int PageID = 0,
			string PresenceDate = "")
		{
			string sqlQuery =
				"ode.dbo.ode_get_company_employee_attendance " +
				"@Action='Get All Leaves'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ManagerFeedChannelID='{ManagerFeedChannelID}'," +
				$"@PresenceDate='{_utilityService.FormatDate(PresenceDate)}'," +
				$"@PageID='{PageID}'," +
				$"@LeaveStatus='{LeaveStatus}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		[HttpPost("deleteLeave")]
		public async Task<IActionResult> deleteLeave(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int LeaveTypeID = 0,
			string FromDate = "")
		{
			string sqlQuery =
				"ode.dbo.ode_insert_company_executive_attendance " +
				"@Action='Delete Leave'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@LeaveTypeID='{LeaveTypeID}'," +
				$"@FromDate='{_utilityService.FormatDate(FromDate)}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		[HttpPost("getLeaveType")]
		public async Task<IActionResult> getLeaveType(int CompanyFeedChannelID = 0, int ExecutiveFeedChannelID = 0)
		{
			string sqlQuery =
				"ode.dbo.ode_insert_company_executive_attendance " +
				"@Action='Get Leave Type'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
	}
}