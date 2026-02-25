using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Data;
using System.Text.RegularExpressions;
namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AttendanceController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public AttendanceController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		// ================= COMPANY ATTENDANCE ADMIN =================
		[HttpPost("companyAttendenceAdmin")]
		public async Task<IActionResult> companyAttendenceAdmin([FromBody] System.Text.Json.JsonElement body)
		{
			int CompanyFeedChannelID = body.GetProperty("CompanyFeedChannelID").GetInt32();
			int ExecutiveFeedChannelID = body.GetProperty("ExecutiveFeedChannelID").GetInt32();
			string sqlQuery =
				$"select ode.dbo.ode_is_company_attendance_admin({CompanyFeedChannelID},{ExecutiveFeedChannelID})";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		// ================= INSERT LOG =================
		[HttpPost("insertLog")]
		public async Task<IActionResult> insertLog([FromBody] System.Text.Json.JsonElement body)
		{
			string LogDateTime = body.GetProperty("LogDateTime").GetString() ?? "";
			int CompanyFeedChannelID = body.GetProperty("CompanyFeedChannelID").GetInt32();
			int ExecutiveFeedChannelID = body.GetProperty("ExecutiveFeedChannelID").GetInt32();
			int FeedID = body.GetProperty("FeedID").GetInt32();
			int PlaceID = body.GetProperty("PlaceID").GetInt32();
			string TimeLogTypeID = body.GetProperty("TimeLogTypeID").GetString() ?? "";
			string lat = body.GetProperty("lat").GetString() ?? "";
			string lng = body.GetProperty("lng").GetString() ?? "";
			string LogText = TimeLogTypeID == "1" ? "Check-in" :
							 TimeLogTypeID == "-1" ? "Check-out" : " ";

			string sqlQuery =
				"exec ode.dbo.ode_insert_company_executive_attendance " +
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
		public async Task<IActionResult> getLog([FromBody] System.Text.Json.JsonElement body)
		{
			string LogDateTime = body.GetProperty("LogDateTime").GetString() ?? "";
			int CompanyFeedChannelID = body.GetProperty("CompanyFeedChannelID").GetInt32();
			int ExecutiveFeedChannelID = body.GetProperty("ExecutiveFeedChannelID").GetInt32();
			bool printVisitCard = body.GetProperty("printVisitCard").GetBoolean();
			string sqlQuery =
				"exec ode.dbo.ode_insert_company_executive_attendance " +
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
		public async Task<IActionResult> getMonthlyAttendance([FromBody] System.Text.Json.JsonElement body)
		{
			string PresenceDate = body.GetProperty("PresenceDate").GetString() ?? "";
			int CompanyFeedChannelID = body.GetProperty("CompanyFeedChannelID").GetInt32();
			int ManagerFeedChannelID = body.GetProperty("ManagerFeedChannelID").GetInt32();

			string sqlQuery =
				"exec ode.dbo.ode_get_company_employee_attendance " +
				"@Action='Get Monthly Attendance'," +
				$"@CompanyFeedChannelID='{CompanyFeedChannelID}'," +
				$"@ManagerFeedChannelID='{ManagerFeedChannelID}'," +
				$"@PresenceDate='{_utilityService.FormatDate(PresenceDate)}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getExecutiveWorkteam")]
		public async Task<IActionResult> getExecutiveWorkteam([FromBody] System.Text.Json.JsonElement body)
		{
			string PresenceDate = body.GetProperty("PresenceDate").GetString() ?? "";
			int CompanyFeedChannelID = body.GetProperty("CompanyFeedChannelID").GetInt32();
			int WorkteamID = body.GetProperty("WorkteamID").GetInt32();
			int ExecutiveFeedChannelID = body.GetProperty("ExecutiveFeedChannelID").GetInt32();
			int CompanyPayrollTeamID = body.GetProperty("WorkteamConfigID").GetInt32();
			int WorkteamConfigID = body.GetProperty("CompanyPayrollTeamID").GetInt32();
			string YearMonth = body.GetProperty("YearMonth").GetString() ?? "";

			string sqlQuery = "exec ode.dbo.[ode_insert_company_Executive_workteam]" +
				"@Action='get Executive Workteam'," +
				"@WorkteamID='" + WorkteamID + "'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
				"@WorkteamConfigID='" + WorkteamConfigID + "'," +
				"@YearMonth='" + _utilityService.FormatDate(YearMonth) + "'," +
				"@CompanyPayrollTeamID='" + CompanyPayrollTeamID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getTodayTimeLeft")]
		public async Task<IActionResult> getTodayTimeLeft([FromBody] System.Text.Json.JsonElement body)
		{
			//string PresenceDate = body.GetProperty("PresenceDate").GetString() ?? "";
			int CompanyFeedChannelID = body.GetProperty("CompanyFeedChannelID").GetInt32();
			int ManagerFeedChannelID = body.GetProperty("ManagerFeedChannelID").GetInt32();

			string sqlQuery = 	"select * from ode.dbo.ODE_get_Company_executive_day_present(" + CompanyFeedChannelID + "," + ManagerFeedChannelID + ")";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("checkQrScanAttendance")]
		public async Task<IActionResult> checkQrScanAttendance([FromBody] System.Text.Json.JsonElement body)
		{
			//string PresenceDate = body.GetProperty("PresenceDate").GetString() ?? "";
			int ExecutiveFeedChannelID = body.GetProperty("ExecutiveFeedChannelID").GetInt32();
			int EntityFeedChannelID = body.GetProperty("EntityFeedChannelID").GetInt32();

			string sqlQuery = "select  ode.dbo.ode_check_qrscan_attendance(" + EntityFeedChannelID + "," + ExecutiveFeedChannelID + ")";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getCompanyEmployees")]
		public async Task<IActionResult> getCompanyEmployees([FromBody] System.Text.Json.JsonElement body)
		{
			//string PresenceDate = body.GetProperty("PresenceDate").GetString() ?? "";
			int CompanyFeedChannelID = body.GetProperty("CompanyFeedChannelID").GetInt32();
			int ManagerFeedChannelID = body.GetProperty("ManagerFeedChannelID").GetInt32();
			string YearMonth = body.GetProperty("YearMonth").GetString() ?? "";
			string sqlQuery = "select * from ode.dbo.ode_get_Company_employees(" + CompanyFeedChannelID + "," + ManagerFeedChannelID + ",'" + YearMonth + "')";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getWorkteam")]
		public async Task<IActionResult> getWorkteam([FromBody] System.Text.Json.JsonElement body)
		{
			//string PresenceDate = body.GetProperty("PresenceDate").GetString() ?? "";
			int EntityFeedChannelID = body.GetProperty("EntityFeedChannelID").GetInt32();
			int loginID = body.GetProperty("loginID").GetInt32();
			int PageID = body.GetProperty("PageID").GetInt32();
			int PageSize = body.GetProperty("PageSize").GetInt32();
			//int Pagesize = body.GetProperty("Pagesize").GetInt32();
			string SearchString = body.GetProperty("SearchString").GetString() ?? "";

			string sqlQuery = "exec[glivebooks].dbo.crm_insert_Feed_Workteam  @Action ='Get',@EntityFeedChannelID='" + EntityFeedChannelID + "',@WebsiteID='9',@loginID='" + loginID + "',@pageID='" + PageID + "',@Pagesize='" + PageSize + "',@SearchString='" + SearchString + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getWorkteamSpaces")]
		public async Task<IActionResult> getWorkteamSpaces([FromBody] System.Text.Json.JsonElement body)
		{
			 
			int loginID = body.GetProperty("loginID").GetInt32();
			int WorkteamID = body.GetProperty("WorkteamID").GetInt32();
		     string SearchString = body.GetProperty("SearchString").GetString() ?? "";

			string sqlQuery = "exec[glivebooks].dbo.crm_insert_Feed_Workteam @Action = 'get spaces',@WebsiteID = 9,@loginID = '" + loginID + @"',@WorkteamID = '" + WorkteamID + "',@SearchString = '" + SearchString + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getWorkteamMembers")]
		public async Task<IActionResult> getWorkteamMembers([FromBody] System.Text.Json.JsonElement body)
		{

			int EntityFeedChannelID = body.GetProperty("EntityFeedChannelID").GetInt32();
			int CompanyPayrollTeamID = body.GetProperty("CompanyPayrollTeamID").GetInt32();
            string YearMonth = body.GetProperty("YearMonth").GetString() ?? "";

			string sqlQuery = "select * from ode.dbo.ode_get_company_employee_payroll_team('" + EntityFeedChannelID + "','" + CompanyPayrollTeamID + "','" + YearMonth + "')";
			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getAccountHead")]
		public async Task<IActionResult> getAccountHead([FromBody] System.Text.Json.JsonElement body)
		{

			int EntityFeedChannelID = body.GetProperty("EntityFeedChannelID").GetInt32();
			 
			int ParentAccountGroupNumber = body.GetProperty("ParentAccountGroupNumber").GetInt32();
			
			string sqlQuery = "exec [glivebooks].dbo.[crm_insert_feed_hk_account_head] @Action='Get',@EntityFeedChannelID ='" + EntityFeedChannelID + "',@ParentAccountGroupNumber='" + ParentAccountGroupNumber + "'";
			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getMedia")]
		public async Task<IActionResult> getMedia([FromBody] System.Text.Json.JsonElement body)
		{

			int MediaID = body.GetProperty("MediaID").GetInt32();
			 

			string sqlQuery = "exec [glivebooks].dbo.crm_get_media @MediaID = '" + MediaID + "'";
			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		// ================= GET CURRENCY =================
		[HttpPost("getCurrencyList")]
		public async Task<IActionResult> getCurrencyList()
		{
			var result = await _utilityService.GetDataResultAsync("exec glivebooks.dbo.BindCurrency");
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}

		// ================= COMPANY ATTENDANCE STATUS =================
		[HttpPost("getCompanyAttendanceStatus")]
		public async Task<IActionResult> getCompanyAttendanceStatus()
		{
			var result = await _utilityService
				.GetDataResultAsync("exec ode.dbo.ode_get_CompanyAttendanceStatus");

			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("crmFeedGetFeedChannelAddresses")]
		public async Task<IActionResult> crmFeedGetFeedChannelAddresses([FromBody] System.Text.Json.JsonElement body)
		{

			int CompanyFeedChannelID = body.GetProperty("CompanyFeedChannelID").GetInt32();
			int AddressID = body.GetProperty("AddressID").GetInt32();


			string sqlQuery = "SELECT * FROM glivebooks.dbo.crm_feed_Get_FeedChannel_Addresses ('" + CompanyFeedChannelID + "', '" + AddressID + "', 0)";
			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getCompanyDesignation")]
		public async Task<IActionResult> getCompanyDesignation([FromBody] System.Text.Json.JsonElement body)
		{

			int CompanyID = body.GetProperty("CompanyID").GetInt32();
		    string sqlQuery = "exec ode.dbo.[ode_Insert_CompanyDesignation] " +
							  "@Action='Get'," +
							  "@CompanyID='" + CompanyID + "'";
			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getRolesInCompany")]
		public async Task<IActionResult> getRolesInCompany([FromBody] System.Text.Json.JsonElement body)
		{

			int CompanyID = body.GetProperty("CompanyID").GetInt32();
			string sqlQuery = "exec ode.dbo.[ode_insert_roles] " +
							  "@flag='GET'," +
							  "@CompanyID='" + CompanyID + "'";
			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getEmployeeBirthdays")]
		public async Task<IActionResult> getEmployeeBirthdays([FromBody] System.Text.Json.JsonElement body)
		{

			int CompanyFeedChannelID = body.GetProperty("CompanyFeedChannelID").GetInt32();
			string Birthdate = body.GetProperty("Birthdate").GetString() ?? "";
			string sqlQuery = "exec ode.dbo.ode_get_company_employee_attendance " +
				"@Action='Birthdays'" +
				",@CompanyFeedChannelID='" + CompanyFeedChannelID + "'" +
				",@Birthdate='" + _utilityService.FormatDate(Birthdate, true, "MM-dd-yyyy hh:mm tt") + "'";
			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
		[HttpPost("getAllEvents")]
		public async Task<IActionResult> getAllEvents([FromBody] System.Text.Json.JsonElement body)
		{

			int CompanyFeedChannelID = body.GetProperty("CompanyFeedChannelID").GetInt32();
			int PageID = body.GetProperty("PageID").GetInt32();
			int PageSize = body.GetProperty("PageSize").GetInt32();
			string Birthdate = body.GetProperty("Birthdate").GetString() ?? "";
			string PresenceDate = body.GetProperty("Birthdate").GetString() ?? "";
			string sqlQuery = "exec ode.dbo.ode_get_company_employee_attendance " +
				"@Action='All Events'" +
				",@CompanyFeedChannelID='" + CompanyFeedChannelID + "'" +
				",@PageID='" + PageID + "'" +
				",@PageSize='" + PageSize + "'" +
				 ",@PresenceDate='" + _utilityService.FormatDate(PresenceDate, false, "MM-dd-yyyy hh:mm tt") + "'" +
				",@Birthdate='" + _utilityService.FormatDate(Birthdate, true, "MM-dd-yyyy hh:mm tt") + "'";
			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Any()) return BadRequest(result.errors);
			return Ok(result.result);
		}
	}
}