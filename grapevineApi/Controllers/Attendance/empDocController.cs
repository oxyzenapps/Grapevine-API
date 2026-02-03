using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
 

namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class empDocController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public empDocController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		[HttpPost("insertEmployeeDocument")]
		public async Task<IActionResult> InsertEmployeeDocument(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int TemplateID = 0,
			string Document = "",
			string DocumentDate = ""
		)
		{
			string sqlQuery =
				"declare @ReturnValue int=0; " +
				"ode.dbo.[ode_insert_Company_executive_document_template] " +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
				"@TemplateID='" + TemplateID + "'," +
				"@Document='" + Document + "'," +
				"@DocumentDate='" + _utilityService.FormatDate(DocumentDate, true, "MM-dd-yyyy hh:mm tt") + "'," +
				"@ReturnValue=@ReturnValue output; select @ReturnValue as ReturnValue";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("getEmpDoc")]
		public async Task<IActionResult> GetEmpDoc(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int DocumentID = 0,
			string DocumentDate = ""
		)
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_document_template] " +
				"@Action='Get Employee Document'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
				"@DocumentID='" + DocumentID + "'," +
				"@DocumentDate='" + _utilityService.FormatDate(DocumentDate) + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("releaseEmpDoc")]
		public async Task<IActionResult> ReleaseEmpDoc(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int DocumentID = 0
		)
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_document_template] " +
				"@Action='Release Employee Document'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
				"@DocumentID='" + DocumentID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("deleteEmpDoc")]
		public async Task<IActionResult> DeleteEmpDoc(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int DocumentID = 0
		)
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_document_template] " +
				"@Action='Delete Employee Document'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
				"@DocumentID='" + DocumentID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		// ================== TEMPLATE ==================

		[HttpPost("insertExecutiveDocumentTemplate")]
		public async Task<IActionResult> InsertExecutiveDocumentTemplate(
			int CompanyFeedChannelID = 0,
			int TemplateID = 0,
			int AddressID = 0,
			int CompanyEmployeeDocumentTypeID = 0,
			int RoleID = 0,
			int DesignationID = 0,
			string FromDate = "",
			string DocumentTemplatePath = "",
			string DocumentTemplateName = ""
		)
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_document_template] " +
				"@Action='Insert'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@TemplateID='" + TemplateID + "'," +
				"@AddressID='" + AddressID + "'," +
				"@CompanyEmployeeDocumentTypeID='" + CompanyEmployeeDocumentTypeID + "'," +
				"@RoleID='" + RoleID + "'," +
				"@DesignationID='" + DesignationID + "'," +
				"@FromDate='" + _utilityService.FormatDate(FromDate) + "'," +
				"@DocumentTemplatePath='" + DocumentTemplatePath + "'," +
				"@DocumentTemplateName='" + DocumentTemplateName + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("getExecutiveDocumentTemplate")]
		public async Task<IActionResult> GetExecutiveDocumentTemplate(int CompanyFeedChannelID = 0, int TemplateID = 0)
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_document_template] " +
				"@Action='Get'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@TemplateID='" + TemplateID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("deleteExecutiveDocumentTemplate")]
		public async Task<IActionResult> DeleteExecutiveDocumentTemplate(int CompanyFeedChannelID = 0, int TemplateID = 0)
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_document_template] " +
				"@Action='Delete'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@TemplateID='" + TemplateID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("getEmployeeDocumentTypes")]
		public async Task<IActionResult> GetEmployeeDocumentTypes(int CompanyFeedChannelID = 0)
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_document_template] " +
				"@Action='Get Employee Document Types'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

		[HttpPost("getTemplateForEmployee")]
		public async Task<IActionResult> GetTemplateForEmployee(
			int CompanyFeedChannelID = 0,
			int ExecutiveFeedChannelID = 0,
			int CompanyEmployeeDocumentTypeID = 0,
			string DocumentDate = ""
		)
		{
			string sqlQuery =
				"ode.dbo.[ode_insert_Company_executive_document_template] " +
				"@Action='Get Template for Employee'," +
				"@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
				"@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
				"@CompanyEmployeeDocumentTypeID='" + CompanyEmployeeDocumentTypeID + "'," +
				"@DocumentDate='" + _utilityService.FormatDate(DocumentDate) + "'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
	}
}
