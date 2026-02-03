using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
 

namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class postSaleTeamExpController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public postSaleTeamExpController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		[HttpPost("insertTeamExp")]
		public async Task<IActionResult> insertTeamExp(
			int ExecutiveFeedChannelID = 0, int EntityFeedChannelID = 0, int WorkteamID = 0,
			string ExpenseDateTime = "", string ExpenseAccountHead = "", string Currency = "",
			string Expenses = "", int WorkTeamConfigID = 0, int CompanyPayrollTeamID = 0,
			int MediaID = 0, string Description = "", string Claims = "", int ClaimedByFeedChannelID = 0,
			int ExpenseDistributionMethodID = 0, string TeamExpenses = "")
		{
			string sqlQuery = "declare @DistributionRecID int= 0; dbo.[crm_insert_sales_team_expenses] " +
							  "@Action='insert'," +
							  $"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}',@EntityFeedChannelID='{EntityFeedChannelID}'," +
							  $"@WorkteamID='{WorkteamID}',@ExpenseDateTime='{ExpenseDateTime}',@ExpenseAccountHead='{ExpenseAccountHead}'," +
							  $"@Currency='{Currency}',@Expenses='{Expenses}',@WorkTeamConfigID='{WorkTeamConfigID}'," +
							  $"@CompanyPayrollTeamID='{CompanyPayrollTeamID}',@MediaID='{MediaID}',@Description='{Description}'," +
							  $"@Claims='{Claims}',@ClaimedByFeedChannelID='{ClaimedByFeedChannelID}'," +
							  $"@ExpenseDistributionMethodID='{ExpenseDistributionMethodID}',@TeamExpenses='{TeamExpenses}'," +
							  "@DistributionRecID=@DistributionRecID output; select @DistributionRecID as DistributionRecID";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Count > 0) return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("updateTeamExp")]
		public async Task<IActionResult> updateTeamExp(
			int ExecutiveFeedChannelID = 0, int EntityFeedChannelID = 0, int WorkteamID = 0,
			string ExpenseDateTime = "", string ExpenseAccountHead = "", string Currency = "",
			string Expenses = "", int WorkTeamConfigID = 0, int CompanyPayrollTeamID = 0,
			int MediaID = 0, string Description = "", string Claims = "", int ClaimedByFeedChannelID = 0,
			int ExpenseDistributionMethodID = 0, string TeamExpenses = "", int DistributionRecID = 0,
			int SalesTeamExpenseTransactionID = 0)
		{
			string sqlQuery = "dbo.[crm_insert_sales_team_expenses] " +
							  "@Action='Update Team Expenses'," +
							  $"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}',@EntityFeedChannelID='{EntityFeedChannelID}'," +
							  $"@WorkteamID='{WorkteamID}',@ExpenseDateTime='{ExpenseDateTime}',@ExpenseAccountHead='{ExpenseAccountHead}'," +
							  $"@Currency='{Currency}',@Expenses='{Expenses}',@WorkTeamConfigID='{WorkTeamConfigID}'," +
							  $"@CompanyPayrollTeamID='{CompanyPayrollTeamID}',@MediaID='{MediaID}',@Description='{Description}'," +
							  $"@Claims='{Claims}',@ClaimedByFeedChannelID='{ClaimedByFeedChannelID}'," +
							  $"@ExpenseDistributionMethodID='{ExpenseDistributionMethodID}',@TeamExpenses='{TeamExpenses}'," +
							  $"@DistributionRecID='{DistributionRecID}',@SalesTeamExpenseTransactionID='{SalesTeamExpenseTransactionID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Count > 0) return BadRequest(result.errors);
			return Ok(result.result);
		}

		[HttpPost("updateExeExp")]
		public async Task<IActionResult> updateExeExp(
			int ExecutiveFeedChannelID = 0, int EntityFeedChannelID = 0, int WorkteamID = 0,
			string ExpenseDateTime = "", string ExpenseAccountHead = "", string Currency = "",
			string Expenses = "", int WorkTeamConfigID = 0, int CompanyPayrollTeamID = 0,
			int MediaID = 0, string Description = "", string Claims = "", int ClaimedByFeedChannelID = 0,
			int ExpenseDistributionMethodID = 0, string TeamExpenses = "", int DistributionRecID = 0,
			int SalesTeamExpenseTransactionID = 0)
		{
			string sqlQuery = "dbo.[crm_insert_sales_team_expenses] " +
							  "@Action='Update Executive Expenses'," +
							  $"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}',@EntityFeedChannelID='{EntityFeedChannelID}'," +
							  $"@WorkteamID='{WorkteamID}',@ExpenseDateTime='{ExpenseDateTime}',@ExpenseAccountHead='{ExpenseAccountHead}'," +
							  $"@Currency='{Currency}',@Expenses='{Expenses}',@WorkTeamConfigID='{WorkTeamConfigID}'," +
							  $"@CompanyPayrollTeamID='{CompanyPayrollTeamID}',@MediaID='{MediaID}',@Description='{Description}'," +
							  $"@Claims='{Claims}',@ClaimedByFeedChannelID='{ClaimedByFeedChannelID}'," +
							  $"@ExpenseDistributionMethodID='{ExpenseDistributionMethodID}',@TeamExpenses='{TeamExpenses}'," +
							  $"@DistributionRecID='{DistributionRecID}',@SalesTeamExpenseTransactionID='{SalesTeamExpenseTransactionID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Count > 0) return BadRequest(result.errors);
			return Ok(result.result);
		}

		[HttpGet("getTeamExp")]
		public async Task<IActionResult> getTeamExp(
			int ExecutiveFeedChannelID = 0, int LoginFeedChannelID = 0, int EntityFeedChannelID = 0,
			int WorkteamID = 0, string CompanyPayrollTeamID = "", string DistributionRecID = "",
			int PageID = 1, int PageSize = 50, string FromDate = "", string Todate = "",
			string ExpenseAccountHead = "", string Approved = "", string NotApproved = "",
			string Paid = "", string NotPaid = "", bool Print = false)
		{
			string sqlQuery = "dbo.[crm_insert_sales_team_expenses] " +
							  "@Action='get Team expenses'," +
							  $"@ExecutiveFeedChannelID='{ExecutiveFeedChannelID}',@LoginFeedChannelID='{LoginFeedChannelID}'," +
							  $"@EntityFeedChannelID='{EntityFeedChannelID}',@WorkteamID='{WorkteamID}'," +
							  $"@CompanyPayrollTeamID='{CompanyPayrollTeamID}',@DistributionRecID='{DistributionRecID}'," +
							  $"@PageID='{PageID}',@PageSize='{PageSize}',@FromDate='{FromDate}',@Todate='{Todate}'," +
							  $"@ExpenseAccountHead='{ExpenseAccountHead}',@Approved='{Approved}',@NotApproved='{NotApproved}'," +
							  $"@Paid='{Paid}',@NotPaid='{NotPaid}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);
			if (result.errors.Count > 0) return BadRequest(result.errors);

			// RDLC Block Commented
			/*
            if (Print)
            {
                // TempData["RdlcDataTables"] = ...;
                // TempData["Rdlcsource"] = ...;
                // TempData["Rdlcpath"] = ...;
                // return File / link logic here
            }
            */

			return Ok(result.result);
		}

		// Other methods like getExeExp, approveTeamExp, payExp, salesTeamApportionedExpenses, crmExpensesAgainstCommissions,
		// getPayrollTeam, deletePayrollTeam etc. can follow exact same pattern:
		// 1. Build SQL string
		// 2. Call await _utilityService.GetDataResultAsync(sqlQuery)
		// 3. Return Ok(result.result) / BadRequest(result.errors)
		// 4. RDLC logic is commented out
	}
}
