using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Data;
 namespace grapevineApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class empWorkteamController : ControllerBase
    {
        private readonly UtilityService _utilityService;

        public empWorkteamController(UtilityService utilityService)
        {
            _utilityService = utilityService;
        }

        [HttpPost("insertExecutiveWorkteam")]
        public async Task<IActionResult> insertExecutiveWorkteam(
            int CompanyFeedChannelID = 0,
            int ExecutiveFeedChannelID = 0,
            int WorkteamID = 0,
            int WorkteamConfigID = 0,
            decimal SharePercentage = 0,
            string YearMonth = "",
            int CompanyPayrollTeamID = 0)
        {
            string sqlQuery =
                "exec ode.dbo.[ode_insert_company_Executive_workteam] " +
                "@Action='Insert Executive Workteam'," +
                "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
                "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
                "@WorkteamID='" + WorkteamID + "'," +
                "@WorkteamConfigID='" + WorkteamConfigID + "'," +
                "@SharePercentage='" + SharePercentage + "'," +
                "@YearMonth='" + YearMonth + "'," +
                "@CompanyPayrollTeamID='" + CompanyPayrollTeamID + "'";

            var result = await _utilityService.GetDataResultAsync(sqlQuery);

            if (result.errors.Any())
                return BadRequest(result.errors);

            return Ok(result.result);
        }

        [HttpPost("getExecutiveWorkteam")]
        public async Task<IActionResult> getExecutiveWorkteam(
            int CompanyFeedChannelID = 0,
            int ExecutiveFeedChannelID = 0,
            int WorkteamID = 0,
            int WorkteamConfigID = 0,
            string YearMonth = "",
            int CompanyPayrollTeamID = 0)
        {
            string sqlQuery =
                "exec ode.dbo.[ode_insert_company_Executive_workteam] " +
                "@Action='get Executive Workteam'," +
                "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
                "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
                "@WorkteamID='" + WorkteamID + "'," +
                "@WorkteamConfigID='" + WorkteamConfigID + "'," +
                "@YearMonth='" + YearMonth + "'," +
                "@CompanyPayrollTeamID='" + CompanyPayrollTeamID + "'";

            var result = await _utilityService.GetDataResultAsync(sqlQuery);

            if (result.errors.Any())
                return BadRequest(result.errors);

            return Ok(result.result);
        }

        [HttpPost("deleteExecutiveWorkteam")]
        public async Task<IActionResult> deleteExecutiveWorkteam(
            int CompanyFeedChannelID = 0,
            int ExecutiveFeedChannelID = 0,
            int WorkteamID = 0,
            int WorkteamConfigID = 0,
            string YearMonth = "",
            int CompanyPayrollTeamID = 0)
        {
            string sqlQuery =
                "exec ode.dbo.[ode_insert_company_Executive_workteam] " +
                "@Action='delete Executive Workteam'," +
                "@ExecutiveFeedChannelID='" + ExecutiveFeedChannelID + "'," +
                "@CompanyFeedChannelID='" + CompanyFeedChannelID + "'," +
                "@WorkteamID='" + WorkteamID + "'," +
                "@WorkteamConfigID='" + WorkteamConfigID + "'," +
                "@YearMonth='" + YearMonth + "'," +
                "@CompanyPayrollTeamID='" + CompanyPayrollTeamID + "'";

            var result = await _utilityService.GetDataResultAsync(sqlQuery);

            if (result.errors.Any())
                return BadRequest(result.errors);

            return Ok(result.result);
        }
    }
}
