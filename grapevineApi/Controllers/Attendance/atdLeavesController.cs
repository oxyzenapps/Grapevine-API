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
		public async Task<IActionResult> getEveryOneOnADay([FromBody] System.Text.Json.JsonElement body)
		{
			int CompanyFeedChannelID = body.GetProperty("CompanyFeedChannelID").GetInt32();
			 
			int ManagerFeedChannelID = body.GetProperty("ManagerFeedChannelID").GetInt32();
			int PresenceStatusID = body.GetProperty("PresenceStatusID").GetInt32();
			int PageID = body.GetProperty("PageID").GetInt32();
			string PresenceDate = body.GetProperty("PresenceDate").GetString() ?? "";
			string SearchString = body.GetProperty("SearchString").GetString() ?? "";
		 
		 
			string sqlQuery =
				"exec ode.dbo.ode_get_company_employee_attendance " +
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

		public class InsertLeaveRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int ExecutiveFeedChannelID { get; set; } = 0;
			public int LeaveTypeID { get; set; } = 0;
			public string LeaveReason { get; set; } = "";
			public string FromDate { get; set; } = "";
			public string ToDate { get; set; } = "";
		}
		[HttpPost("insertLeave")]
		public async Task<IActionResult> insertLeave([FromBody] InsertLeaveRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int ExecutiveFeedChannelID = request.ExecutiveFeedChannelID;
			int LeaveTypeID = request.LeaveTypeID;
			string LeaveReason = request.LeaveReason;
			string FromDate = request.FromDate;
			string ToDate = request.ToDate;
			string sqlQuery =
				"Declare @ReturnValue int=0; exec ode.dbo.ode_insert_company_executive_attendance " +
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

		public class UpdateLeaveRequest
		{
			public int AdminFeedChannelID { get; set; } = 0;
			public int CompanyFeedChannelID { get; set; } = 0;
			public int ExecutiveFeedChannelID { get; set; } = 0;
			public int LeaveTypeID { get; set; } = 0;
			public int LeaveStatusID { get; set; } = 0;
			public string LeaveReason { get; set; } = "";
			public string ToDate { get; set; } = "";
			public string FromDate { get; set; } = "";
		}

		[HttpPost("updateLeave")]
		public async Task<IActionResult> updateLeave([FromBody] UpdateLeaveRequest request)
		{
			int AdminFeedChannelID = request.AdminFeedChannelID;
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int ExecutiveFeedChannelID = request.ExecutiveFeedChannelID;
			int LeaveTypeID = request.LeaveTypeID;
			int LeaveStatusID = request.LeaveStatusID;
			string LeaveReason = request.LeaveReason;
			string ToDate = request.ToDate;
			string FromDate = request.FromDate;
			string sqlQuery =
				"Declare @ReturnValue int=0; exec ode.dbo.ode_insert_company_executive_attendance " +
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

		public class GetLeaveRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int ExecutiveFeedChannelID { get; set; } = 0;
			public int LeaveTypeID { get; set; } = 0;
			public string FromDate { get; set; } = "";
		}
		[HttpPost("getLeave")]
		public async Task<IActionResult> getLeave([FromBody] GetLeaveRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int ExecutiveFeedChannelID = request.ExecutiveFeedChannelID;
			int LeaveTypeID = request.LeaveTypeID;
			string FromDate = request.FromDate;
			string sqlQuery =
				"exec ode.dbo.ode_insert_company_executive_attendance " +
				"@Action='Get Leave'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@LeaveTypeID='{LeaveTypeID}'," +
				$"@FromDate='{_utilityService.FormatDate(FromDate)}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		public class GetAllLeaveRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int ManagerFeedChannelID { get; set; } = 0;
			public int LeaveStatus { get; set; } = -1;
			public int PageID { get; set; } = 0;
			public string PresenceDate { get; set; } = "";
		}

		[HttpPost("getAllLeave")]
		public async Task<IActionResult> getAllLeave([FromBody] GetAllLeaveRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int ManagerFeedChannelID = request.ManagerFeedChannelID;
			int LeaveStatus = request.LeaveStatus;
			int PageID = request.PageID;
			string PresenceDate = request.PresenceDate;
			string sqlQuery =
				"exec ode.dbo.ode_get_company_employee_attendance " +
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
		public class DeleteLeaveRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int ExecutiveFeedChannelID { get; set; } = 0;
			public int LeaveTypeID { get; set; } = 0;
			public string FromDate { get; set; } = "";
		}

		[HttpPost("deleteLeave")]
		public async Task<IActionResult> deleteLeave([FromBody] DeleteLeaveRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int ExecutiveFeedChannelID = request.ExecutiveFeedChannelID;
			int LeaveTypeID = request.LeaveTypeID;
			string FromDate = request.FromDate;
			string sqlQuery =
				"exec ode.dbo.ode_insert_company_executive_attendance " +
				"@Action='Delete Leave'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@LeaveTypeID='{LeaveTypeID}'," +
				$"@FromDate='{_utilityService.FormatDate(FromDate)}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		public class GetLeaveTypeRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int ExecutiveFeedChannelID { get; set; } = 0;
		}
		[HttpPost("getLeaveType")]
		public async Task<IActionResult> getLeaveType([FromBody] GetLeaveTypeRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int ExecutiveFeedChannelID = request.ExecutiveFeedChannelID;

			string sqlQuery =
				"exec ode.dbo.ode_insert_company_executive_attendance " +
				"@Action='Get Leave Type'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		public class GetExePendingLeaveRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int ExecutiveFeedChannelID { get; set; } = 0;
			public int LeaveTypeID { get; set; } = 0;
			public string YearMonth { get; set; } = "";
		}

		[HttpPost("getExePendingLeave")]
		public async Task<IActionResult> getExePendingLeave([FromBody] GetExePendingLeaveRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int ExecutiveFeedChannelID = request.ExecutiveFeedChannelID;
			int LeaveTypeID = request.LeaveTypeID;
			string YearMonth = request.YearMonth;

			string formattedDate = _utilityService.FormatDate(YearMonth, true, "MM-dd-yyyy hh:mm tt");

			string sqlQuery =
				"exec ode.dbo.ode_insert_company_executive_leave_Balance " +
				"@Action='Get company executive pending leaves'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@YearMonth='{formattedDate}'," +
				$"@LeaveTypeID='{LeaveTypeID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class GetCompanyExecutiveLeaveRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int ExecutiveFeedChannelID { get; set; } = 0;
			public int LeaveTypeID { get; set; } = 0;
			public string YearMonth { get; set; } = "";
			public int Pagesize { get; set; } = 20;
			public int PageID { get; set; } = 0;
			public string SearchString { get; set; } = "";
			public bool Print { get; set; } = false;
		}

		[HttpPost("getCompanyExecutiveLeave")]
		public async Task<IActionResult> getCompanyExecutiveLeave([FromBody] GetCompanyExecutiveLeaveRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int ExecutiveFeedChannelID = request.ExecutiveFeedChannelID;
			int LeaveTypeID = request.LeaveTypeID;
			string YearMonth = request.YearMonth;
			int Pagesize = request.Pagesize;
			int PageID = request.PageID;
			string SearchString = request.SearchString;
			bool Print = request.Print;

			string formattedDate = _utilityService.FormatDate(YearMonth, true, "MM-dd-yyyy hh:mm tt");

			string sqlQuery =
				"exec ode.dbo.ode_insert_company_executive_leave_Balance " +
				"@Action='Get company executive leave'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@YearMonth='{formattedDate}'," +
				$"@SearchString='{SearchString}'," +
				$"@Pagesize='{Pagesize}'," +
				$"@PageID='{PageID}'," +
				$"@LeaveTypeID='{LeaveTypeID}'";


			if (Print == true) { }  

			

				var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
			 
		}
		public class InsertHolidayRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int AddressID { get; set; } = 0;
			public string HolidayDescription { get; set; } = "";
			public string HolidayDate { get; set; } = "";
			public string UpdateHolidateDate { get; set; } = "";
			public string GreetingImage { get; set; } = "";
			public string GreetingHtml { get; set; } = "";
		}

		[HttpPost("insertHoliday")]
		public async Task<IActionResult> insertHoliday([FromBody] InsertHolidayRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int AddressID = request.AddressID;
			string HolidayDescription = request.HolidayDescription;
			string HolidayDate = request.HolidayDate;
			string UpdateHolidateDate = request.UpdateHolidateDate;
			string GreetingImage = request.GreetingImage;
			string GreetingHtml = request.GreetingHtml;

			GreetingHtml = _utilityService.stringToHtmlString(GreetingHtml);

			string formattedHolidayDate = _utilityService.FormatDate(HolidayDate, false, "MM-dd-yyyy hh:mm tt");
			string formattedUpdateHolidayDate = _utilityService.FormatDate(UpdateHolidateDate, false, "MM-dd-yyyy hh:mm tt");

			string sqlQuery =
				"exec ode.dbo.ode_insert_Company_holidays " +
				"@Action='insert holiday'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@AddressID='{AddressID}'," +
				$"@HolidayDescription='{HolidayDescription}'," +
				$"@HolidayDate='{formattedHolidayDate}'," +
				$"@UpdateHolidateDate='{formattedUpdateHolidayDate}'," +
				$"@GreetingImage='{GreetingImage}'," +
				$"@GreetingHtml='{GreetingHtml}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class GetHolidayRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int AddressID { get; set; } = 0;
			public string HolidayDate { get; set; } = "";
		}

		[HttpPost("getHoliday")]
		public async Task<IActionResult> getHoliday([FromBody] GetHolidayRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int AddressID = request.AddressID;
			string HolidayDate = request.HolidayDate;

			string formattedHolidayDate = _utilityService.FormatDate(HolidayDate, false, "MM-dd-yyyy hh:mm tt");

			string sqlQuery =
				"exec ode.dbo.ode_insert_Company_holidays " +
				"@Action='Get holiday'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@HolidayDate='{formattedHolidayDate}'," +
				$"@AddressID='{AddressID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class DeleteHolidayRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int AddressID { get; set; } = 0;
			public string HolidayDate { get; set; } = "";
		}

		[HttpPost("deleteHoliday")]
		public async Task<IActionResult> deleteHoliday([FromBody] DeleteHolidayRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int AddressID = request.AddressID;
			string HolidayDate = request.HolidayDate;

			string formattedHolidayDate = _utilityService.FormatDate(HolidayDate, false, "MM-dd-yyyy hh:mm tt");

			string sqlQuery =
				"exec ode.dbo.ode_insert_Company_holidays " +
				"@Action='Delete holiday'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@HolidayDate='{formattedHolidayDate}'," +
				$"@AddressID='{AddressID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class InsertLeaveLimitsRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int AddressID { get; set; } = 0;
			public int LeaveTypeID { get; set; } = 0;
			public int MaxLimitDays { get; set; } = 0;
			public int LimitMonths { get; set; } = 0;
			public bool Calculated { get; set; } = false;
			public bool CarryForward { get; set; } = false;
			public int MaxCarryForwardLeaveDays { get; set; } = 0;
			public int MaxCarryForwardMonhts { get; set; } = 0;
		}

		[HttpPost("insertLeaveLimits")]
		public async Task<IActionResult> insertLeaveLimits([FromBody] InsertLeaveLimitsRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int AddressID = request.AddressID;
			int LeaveTypeID = request.LeaveTypeID;
			int MaxLimitDays = request.MaxLimitDays;
			int LimitMonths = request.LimitMonths;
			bool Calculated = request.Calculated;
			bool CarryForward = request.CarryForward;
			int MaxCarryForwardLeaveDays = request.MaxCarryForwardLeaveDays;
			int MaxCarryForwardMonhts = request.MaxCarryForwardMonhts;

			string sqlQuery =
				"exec ode.dbo.ode_insert_company_leave_limits " +
				"@Action='Insert Leave Limits'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@AddressID='{AddressID}'," +
				$"@LeaveTypeID='{LeaveTypeID}'," +
				$"@MaxLimitDays='{MaxLimitDays}'," +
				$"@LimitMonths='{LimitMonths}'," +
				$"@Calculated='{Calculated}'," +
				$"@CarryForward='{CarryForward}'," +
				$"@MaxCarryForwardLeaveDays='{MaxCarryForwardLeaveDays}'," +
				$"@MaxCarryForwardMonhts='{MaxCarryForwardMonhts}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class GetLeaveLimitsRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int AddressID { get; set; } = 0;
			public int LeaveTypeID { get; set; } = 0;
		}

		[HttpPost("getLeaveLimits")]
		public async Task<IActionResult> getLeaveLimits([FromBody] GetLeaveLimitsRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int AddressID = request.AddressID;
			int LeaveTypeID = request.LeaveTypeID;

			string sqlQuery =
				"exec ode.dbo.ode_insert_company_leave_limits " +
				"@Action='get Leave Limits'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@AddressID='{AddressID}'," +
				$"@LeaveTypeID='{LeaveTypeID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class DeleteLeaveLimitsRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int AddressID { get; set; } = 0;
			public int LeaveTypeID { get; set; } = 0;
		}

		[HttpPost("deleteLeaveLimits")]
		public async Task<IActionResult> deleteLeaveLimits([FromBody] DeleteLeaveLimitsRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int AddressID = request.AddressID;
			int LeaveTypeID = request.LeaveTypeID;

			string sqlQuery =
				"exec ode.dbo.ode_insert_company_leave_limits " +
				"@Action='Delete Leave Limits'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@AddressID='{AddressID}'," +
				$"@LeaveTypeID='{LeaveTypeID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class InsertCompanyExecutiveLeaveRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int ExecutiveFeedChannelID { get; set; } = 0;
			public int AddressID { get; set; } = 0;
			public string YearMonth { get; set; } = "";
			public int LeaveTypeID { get; set; } = 0;
			public int CarriedForward { get; set; } = 0;
		}

		[HttpPost("insertCompanyExecutiveLeave")]
		public async Task<IActionResult> insertCompanyExecutiveLeave([FromBody] InsertCompanyExecutiveLeaveRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int ExecutiveFeedChannelID = request.ExecutiveFeedChannelID;
			int AddressID = request.AddressID;
			string YearMonth = request.YearMonth;
			int LeaveTypeID = request.LeaveTypeID;
			int CarriedForward = request.CarriedForward;

			string formattedYearMonth = _utilityService.FormatDate(YearMonth, true, "MM-dd-yyyy hh:mm tt");

			string sqlQuery =
				"exec ode.dbo.ode_insert_company_executive_leave_Balance " +
				"@Action='Insert company executive leave'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@YearMonth='{formattedYearMonth}'," +
				$"@LeaveTypeID='{LeaveTypeID}'," +
				$"@CarriedForward='{CarriedForward}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class DeleteCompanyExecutiveLeaveRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int ExecutiveFeedChannelID { get; set; } = 0;
			public int LeaveTypeID { get; set; } = 0;
			public string YearMonth { get; set; } = "";
		}

		[HttpPost("deleteCompanyExecutiveLeave")]
		public async Task<IActionResult> deleteCompanyExecutiveLeave([FromBody] DeleteCompanyExecutiveLeaveRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int ExecutiveFeedChannelID = request.ExecutiveFeedChannelID;
			int LeaveTypeID = request.LeaveTypeID;
			string YearMonth = request.YearMonth;

			string formattedYearMonth = _utilityService.FormatDate(YearMonth, true, "MM-dd-yyyy hh:mm tt");

			string sqlQuery =
				"exec ode.dbo.ode_insert_company_executive_leave_Balance " +
				"@Action='Delete company executive leave'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@YearMonth='{formattedYearMonth}'," +
				$"@LeaveTypeID='{LeaveTypeID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class UpdateAttendanceRequest
		{
			public int CompanyFeedChannelID { get; set; } = 0;
			public int ExecutiveFeedChannelID { get; set; } = 0;
			public int AddressID { get; set; } = 0;
			public string LogDateTime { get; set; } = "";
			public string CompOffDate { get; set; } = "";
			public int AttendenceStatusID { get; set; } = 0;
			public decimal PresentFracton { get; set; } = 0;
		}

		[HttpPost("updateAttendance")]
		public async Task<IActionResult> updateAttendance([FromBody] UpdateAttendanceRequest request)
		{
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int ExecutiveFeedChannelID = request.ExecutiveFeedChannelID;
			int AddressID = request.AddressID;
			string LogDateTime = request.LogDateTime;
			string CompOffDate = request.CompOffDate;
			int AttendenceStatusID = request.AttendenceStatusID;
			decimal PresentFracton = request.PresentFracton;

			string formattedLogDateTime = _utilityService.FormatDate(LogDateTime, false, "MM-dd-yyyy hh:mm tt");

			string sqlQuery =
				"exec ode.dbo.ode_insert_company_executive_attendance " +
				"@Action='Update Attendence'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@LogDateTime='{formattedLogDateTime}'," +
				$"@CompOffDate='{CompOffDate}'," +
				$"@AttendenceStatusID='{AttendenceStatusID}'," +
				$"@PresentFracton='{PresentFracton}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class AllocateExecutiveLeaveRequest
		{
			public int ExecutiveFeedChannelID { get; set; } = 0;
			public int CompanyFeedChannelID { get; set; } = 0;
			public int AddressID { get; set; } = 0;
			public string YearMonth { get; set; } = "";
		}

		[HttpPost("allocateExecutiveLeave")]
		public async Task<IActionResult> allocateExecutiveLeave([FromBody] AllocateExecutiveLeaveRequest request)
		{
			int ExecutiveFeedChannelID = request.ExecutiveFeedChannelID;
			int CompanyFeedChannelID = request.CompanyFeedChannelID;
			int AddressID = request.AddressID;
			string YearMonth = request.YearMonth;

			string formattedYearMonth = _utilityService.FormatDate(YearMonth, false, "MM-dd-yyyy hh:mm tt");

			string sqlQuery =
				"exec ode.dbo.[ode_insert_Company_Allocate_leaves] " +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}'," +
				$"@AddressID='{AddressID}'," +
				$"@YearMonth='{formattedYearMonth}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
	}
}