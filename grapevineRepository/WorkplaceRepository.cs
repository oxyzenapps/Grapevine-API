using grapevineCommon.Model.Workplace;
using grapevineData;
using grapevineData.Interfaces;
using grapevineRepository.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace grapevineRepository
{
    public class WorkplaceRepository : IWorkplaceRepository
    {
        private readonly IDapperExecutor _dapper;

        public WorkplaceRepository(IDapperExecutor dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<UserProfileLocation>> GetCurrentLocation(string contactId, int websiteId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WebsiteID", websiteId);
            parameters.Add("@ContactID", contactId);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "ode.dbo.ode_get_current_location",
                Parameters = parameters
            };

            return await _dapper.ExecuteAsync<UserProfileLocation>(request);
        }

        public async Task<IEnumerable<LocationSearchItem>> SearchLocation(string searchString, string localityIdRequired)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SearchString", searchString);
            parameters.Add("@localityIDRequired", localityIdRequired);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "ode.dbo.ode_get_location_search",
                Parameters = parameters
            };

            return await _dapper.ExecuteAsync<LocationSearchItem>(request);
        }

        public async Task<string> UpdateFilterParameter(UpdateFilterRequest request, string ssn, string feedChannelId)
        {
            string searchId = request.SearchID;

            if (searchId != "0" && !string.IsNullOrEmpty(searchId))
            {
                // FIX: Use DynamicParameters instead of anonymous type for DeleteFiltervalues
                var deleteFilterParams = new DynamicParameters();
                deleteFilterParams.Add("@SearchID", searchId);

                await _dapper.ExecuteAsync(new StoredProcedureRequest
                {
                    ProcedureName = "DeleteFiltervalues",
                    Parameters = deleteFilterParams
                });

                // FIX: Use DynamicParameters instead of anonymous type for DeleteSearchResults
                var deleteSearchParams = new DynamicParameters();
                deleteSearchParams.Add("@SearchID", searchId);

                await _dapper.ExecuteAsync(new StoredProcedureRequest
                {
                    ProcedureName = "DeleteSearchResults",
                    Parameters = deleteSearchParams
                });
            }
            else
            {
                var saveParams = new DynamicParameters();
                saveParams.Add("@SearchID", request.SearchID);
                saveParams.Add("@SSN", ssn);
                saveParams.Add("@SearchForID", request.SearchForId);
                saveParams.Add("@FeedChannelID", feedChannelId);
                saveParams.Add("@CityID", request.CityID);
                saveParams.Add("@LocalityID", request.LocalityID);
                saveParams.Add("@DistanceInKm", request.DistanceInKm);

                using (var conn = _dapper.GetConnection())
                {
                    searchId = await conn.ExecuteScalarAsync<string>(
                        "SaveFiltervalues",
                        saveParams,
                        commandType: CommandType.StoredProcedure);
                }
            }

            if (request.FilterParameter != null)
            {
                for (int i = 0; i < request.FilterParameter.Length; i++)
                {
                    if (i < request.Parameter1.Length && !string.IsNullOrEmpty(request.Parameter1[i]) && !request.Parameter1[i].Contains("undefined"))
                    {
                        var updateParams = new DynamicParameters();
                        updateParams.Add("@SearchID", searchId);
                        updateParams.Add("@FilterParameterID", request.FilterParameter[i]);
                        updateParams.Add("@Parameter1", request.Parameter1[i]);
                        updateParams.Add("@Parameter2", request.Parameter2[i]);
                        updateParams.Add("@OpType", 1);

                        await _dapper.ExecuteAsync(new StoredProcedureRequest
                        {
                            ProcedureName = "oxyzen_homes.dbo.OH_Update_Search_Details",
                            Parameters = updateParams
                        });
                    }
                }
            }
            return searchId;
        }
    }
}