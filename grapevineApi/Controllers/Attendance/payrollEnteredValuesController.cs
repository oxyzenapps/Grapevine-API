using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
 

namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class paymentController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public paymentController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		[HttpGet("getPaymentLedger")]
		public async Task<IActionResult> getPaymentLedger(
			int EntityFeedChannelID = 0,
			int DocumentTypeID = 0,
			int PageID = 1,
			int PageSize = 20)
		{
			string sqlQuery =
				"exec glivebooks.dbo.Crm_Insert_payment_ledger " +
				"@Action='Get'," +
				"@EntityFeedChannelID='" + EntityFeedChannelID + "'," +
				"@DocumentTypeID='" + DocumentTypeID + "'," +
				"@PageID='" + PageID + "'," +
				"@PageSize='" + PageSize + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpGet("getOutstandingGeneralLedger")]
		public async Task<IActionResult> getOutstandingGeneralLedger(
			int EntityFeedChannelID = 0,
			int DocumentTypeID = 0)
		{
			string sqlQuery =
				"exec glivebooks.dbo.crm_insert_general_ledger " +
				"@Action='Get outstanding'," +
				"@EntityFeedChannelID='" + EntityFeedChannelID + "'," +
				"@DocumentTypeID='" + DocumentTypeID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpGet("getGeneralLedger")]
		public async Task<IActionResult> getGeneralLedger(
			int EntityFeedChannelID = 0,
			int DocumentTypeID = 0,
			int LedgerFeedChannelID = 0,
			int OnlyDebits = 0,
			int OnlyCredits = 0,
			int Pending = 0)
		{
			string sqlQuery =
				"exec glivebooks.dbo.crm_insert_general_ledger " +
				"@Action='Get Ledger'," +
				"@EntityFeedChannelID='" + EntityFeedChannelID + "'," +
				"@DocumentTypeID='" + DocumentTypeID + "'," +
				"@LedgerFeedChannelID='" + LedgerFeedChannelID + "'," +
				"@OnlyDebits='" + OnlyDebits + "'," +
				"@OnlyCredits='" + OnlyCredits + "'," +
				"@Pending='" + Pending + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("insertPaymentSettlement")]
		public async Task<IActionResult> insertPaymentSettlement(
			int EntityFeedChannelID = 0,
			string currency = "",
			string AutoSettle = "",
			int FeedChannelID = 0,
			string PayrollTeamID = "",
			int sDocumentTypeID1 = 0,
			string SDocumentID1 = "",
			string s1Amount = "",
			string s1Direction = "",
			int sDocumentTypeID2 = 0,
			string SDocumentID2 = "",
			string s2Amount = "",
			string s2Direction = "",
			string sDocumentTypeID = "",
			string SDocumentID = "")
		{
			string sqlQuery =
				"exec glivebooks.dbo.crm_Insert_payment_settlement " +
				"@Action='insert'," +
				"@EntityFeedChannelID='" + EntityFeedChannelID + "'," +
				"@currency='" + currency + "'," +
				"@AutoSettle='" + AutoSettle + "'," +
				"@FeedChannelID='" + FeedChannelID + "'," +
				"@PayrollTeamID='" + PayrollTeamID + "'," +
				"@sDocumentTypeID1='" + sDocumentTypeID1 + "'," +
				"@SDocumentID1='" + SDocumentID1 + "'," +
				"@s1Amount='" + s1Amount + "'," +
				"@s1Direction='" + s1Direction + "'," +
				"@sDocumentTypeID2='" + sDocumentTypeID2 + "'," +
				"@SDocumentID2='" + SDocumentID2 + "'," +
				"@s2Amount='" + s2Amount + "'," +
				"@s2Direction='" + s2Direction + "'," +
				"@sDocumentTypeID='" + sDocumentTypeID + "'," +
				"@SDocumentID='" + SDocumentID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpGet("getSettlementForFeedChannel")]
		public async Task<IActionResult> getSettlementForFeedChannel(
			int EntityFeedChannelID = 0,
			string currency = "",
			string AutoSettle = "",
			int FeedChannelID = 0,
			string PayrollTeamID = "",
			int sDocumentTypeID1 = 3,
			string SDocumentID1 = "",
			string s1Amount = "",
			string s1Direction = "",
			int sDocumentTypeID2 = 0,
			string SDocumentID2 = "",
			string s2Amount = "",
			string s2Direction = "",
			string sDocumentTypeID = "",
			string SDocumentID = "")
		{
			string sqlQuery =
				"exec glivebooks.dbo.crm_Insert_payment_settlement " +
				"@Action='Get settlement For FeedChannel'," +
				"@EntityFeedChannelID='" + EntityFeedChannelID + "'," +
				"@currency='" + currency + "'," +
				"@AutoSettle='" + AutoSettle + "'," +
				"@FeedChannelID='" + FeedChannelID + "'," +
				"@PayrollTeamID='" + PayrollTeamID + "'," +
				"@sDocumentTypeID1='" + sDocumentTypeID1 + "'," +
				"@SDocumentID1='" + SDocumentID1 + "'," +
				"@s1Amount='" + s1Amount + "'," +
				"@s1Direction='" + s1Direction + "'," +
				"@sDocumentTypeID2='" + sDocumentTypeID2 + "'," +
				"@SDocumentID2='" + SDocumentID2 + "'," +
				"@s2Amount='" + s2Amount + "'," +
				"@s2Direction='" + s2Direction + "'," +
				"@sDocumentTypeID='" + sDocumentTypeID + "'," +
				"@SDocumentID='" + SDocumentID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
	}
}
