using Dapper;
using grapevineCommon.Model.OxygenCrm;
using grapevineCommon.Model.Workplace;
using GrapevineCommon.Model.Workplace;

using grapevineData.Interfaces;
using grapevineRepository.Interfaces;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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

            var request = new grapevineData.Interfaces.StoredProcedureRequest
            {
                ProcedureName = "ode.dbo.ode_get_current_location",
                Parameters = parameters
            };

            return await _dapper.ExecuteAsync<UserProfileLocation>(request);
        }

        public async Task<string> UpdateFilterParameter(UpdateFilterRequest request, string ssn, string feedChannelId)
        {
            string searchId = request.SearchID;

            if (searchId != "0" && !string.IsNullOrEmpty(searchId))
            {
                var deleteFilterParams = new DynamicParameters();
                deleteFilterParams.Add("@SearchID", searchId);

                await _dapper.ExecuteAsync(new StoredProcedureRequest
                {
                    ProcedureName = "DeleteFiltervalues",
                    Parameters = deleteFilterParams
                });

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

        public async Task<string> GetSaleListCount(string pageNo, string searchId, string sort, string listingStatusId, string feedChannelId, string myFeedChannelId)
        {
            var p = new DynamicParameters();
            p.Add("@PageNo", pageNo);
            p.Add("@search_id", searchId);
            p.Add("@sort", sort);
            p.Add("@ListingStatusID", listingStatusId);
            p.Add("@FeedChannelID", feedChannelId);
            p.Add("@MyFeedChannelID", myFeedChannelId);

            using (var conn = _dapper.GetConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<string>("oxyzen_homes.dbo.OH_get_search_sale_List_count", p, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> GetAgenciesList(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId)
        {
            var p = new DynamicParameters();
            p.Add("@PageNo", pageno);
            p.Add("@search_id", searchId);
            p.Add("@sort", sort);
            p.Add("@FeedChannelID", feedChannelId);
            p.Add("@ListingStatusID", listingStatusId);
            p.Add("@MyfeedChannelID", myFeedChannelId);

            return await _dapper.ExecuteAsync<dynamic>(new StoredProcedureRequest
            {
                ProcedureName = "oxyzen_homes.dbo.OH_get_search_agencies_List",
                Parameters = p
            });
        }

        public async Task<IEnumerable<dynamic>> GetDevelopersList(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId)
        {
            var p = new DynamicParameters();
            p.Add("@PageNo", pageno);
            p.Add("@search_id", searchId);
            p.Add("@sort", sort);
            p.Add("@FeedChannelID", feedChannelId);
            p.Add("@ListingStatusID", listingStatusId);
            p.Add("@MyfeedChannelID", myFeedChannelId);

            return await _dapper.ExecuteAsync<dynamic>(new StoredProcedureRequest
            {
                ProcedureName = "oxyzen_homes.dbo.OH_get_search_Developers_List",
                Parameters = p
            });
        }

        public async Task<List<WorkplaceItem>> GetBuyersAlsoLiked(string loginFeedchannelID, string searchforID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedchannelID", loginFeedchannelID);
            parameters.Add("@SearchforID", searchforID);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "OH_get_buyers_also_liked",
                Parameters = parameters
            };

            var result = await _dapper.ExecuteAsync<WorkplaceItem>(request);
            return result.ToList();
        }

        public async Task<int> GetSearchID(string loginFeedchannelID, string searchforID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedchannelID", loginFeedchannelID);
            parameters.Add("@SearchforID", searchforID);
            parameters.Add("@SearchID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var conn = _dapper.GetConnection())
            {
                await conn.ExecuteAsync(
                    "oxyzen_homes.dbo.OH_get_buyers_also_liked",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@SearchID");
            }
        }

        public async Task<IEnumerable<dynamic>> GetProjectList(string searchId, int pageNo = 1, int pageSize = 10)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pageno", pageNo);
            parameters.Add("@search_id", searchId ?? string.Empty);
            parameters.Add("@pageSize", pageSize);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "oxyzen_homes.dbo.OH_get_search_project_List",
                Parameters = parameters
            };

            return await _dapper.ExecuteAsync<dynamic>(request);
        }

        public async Task<bool> SendProjectEmail(string email, string name, string projectId, string responseByFeedChannelId,
                                               string message, string from, string emailType, string applicantId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email ?? string.Empty);
                parameters.Add("@Name", name ?? string.Empty);
                parameters.Add("@ProjectID", projectId ?? string.Empty);
                parameters.Add("@ResponseByFeedChannelID", responseByFeedChannelId ?? string.Empty);
                parameters.Add("@Message", message ?? string.Empty);
                parameters.Add("@From", from ?? "UserToAdvertiser");
                parameters.Add("@EmailType", emailType ?? "User To Advertiser");
                parameters.Add("@ApplicantID", applicantId ?? string.Empty);
                parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var request = new StoredProcedureRequest
                {
                    ProcedureName = "OH_Send_Project_Email",
                    Parameters = parameters
                };

                await _dapper.ExecuteAsync(request);
                return parameters.Get<int?>("@Result") > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending project email: {ex.Message}");
                return false;
            }
        }

        public async Task<dynamic> GetProjectById(string projectId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProjectID", projectId);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "OH_Get_Project_By_ID",
                Parameters = parameters
            };

            var result = await _dapper.ExecuteAsync<dynamic>(request);
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateProjectStatus(string projectId, string status)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProjectID", projectId);
            parameters.Add("@Status", status);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "OH_Update_Project_Status",
                Parameters = parameters
            };

            await _dapper.ExecuteAsync(request);
            return parameters.Get<int?>("@Result") > 0;
        }

        public async Task<DataSet> GetTopLocalitiesAsync(string searchId)
        {
            var dataSet = new DataSet();
            using (var conn = _dapper.GetConnection())
            {
                using (var command = new SqlCommand("OH_get_top_localities", (SqlConnection)conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SearchID", searchId);
                    if (conn.State != ConnectionState.Open) conn.Open();
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                    }
                }
            }
            return dataSet;
        }

        public async Task<List<WorkplaceItem>> GetWorkplacesAsync(WorkplaceRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SearchText", request.SearchText ?? string.Empty);
            parameters.Add("@IsActive", request.IsActive);
            parameters.Add("@SortBy", request.SortBy ?? "WorkplaceName");
            parameters.Add("@SortOrder", request.SortOrder ?? "ASC");
            parameters.Add("@Offset", (request.PageNumber - 1) * request.PageSize);
            parameters.Add("@PageSize", request.PageSize);

            var storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "OH_Get_Workplaces",
                Parameters = parameters
            };

            var result = await _dapper.ExecuteAsync<WorkplaceItem>(storedProcedureRequest);
            return result.ToList();
        }

        public async Task<WorkplaceItem> GetWorkplaceByIdAsync(int workplaceId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WorkplaceId", workplaceId);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "OH_Get_Workplace_By_ID",
                Parameters = parameters
            };

            var result = await _dapper.ExecuteAsync<WorkplaceItem>(request);
            return result.FirstOrDefault();
        }

        public async Task<int> CreateWorkplaceAsync(WorkplaceItem workplace)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WorkplaceName", workplace.WorkplaceName);
            parameters.Add("@WorkplaceCode", workplace.WorkplaceCode);
            parameters.Add("@Address", workplace.Address);
            parameters.Add("@City", workplace.City);
            parameters.Add("@State", workplace.State);
            parameters.Add("@Country", workplace.Country);
            parameters.Add("@PostalCode", workplace.PostalCode);
            parameters.Add("@IsActive", workplace.IsActive);
            parameters.Add("@CreatedDate", workplace.CreatedDate ?? DateTime.Now);
            parameters.Add("@CreatedBy", workplace.CreatedBy);
            parameters.Add("@NewWorkplaceId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "OH_Create_Workplace",
                Parameters = parameters
            };

            await _dapper.ExecuteAsync(request);
            return parameters.Get<int>("@NewWorkplaceId");
        }

        public async Task<bool> UpdateWorkplaceAsync(WorkplaceItem workplace)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WorkplaceId", workplace.WorkplaceId);
            parameters.Add("@WorkplaceName", workplace.WorkplaceName);
            parameters.Add("@WorkplaceCode", workplace.WorkplaceCode);
            parameters.Add("@Address", workplace.Address);
            parameters.Add("@City", workplace.City);
            parameters.Add("@State", workplace.State);
            parameters.Add("@Country", workplace.Country);
            parameters.Add("@PostalCode", workplace.PostalCode);
            parameters.Add("@IsActive", workplace.IsActive);
            parameters.Add("@ModifiedDate", workplace.ModifiedDate ?? DateTime.Now);
            parameters.Add("@ModifiedBy", workplace.ModifiedBy);
            parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "OH_Update_Workplace",
                Parameters = parameters
            };

            await _dapper.ExecuteAsync(request);
            return parameters.Get<int>("@RowsAffected") > 0;
        }

        public async Task<bool> DeleteWorkplaceAsync(int workplaceId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WorkplaceId", workplaceId);
            parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "OH_Delete_Workplace",
                Parameters = parameters
            };

            await _dapper.ExecuteAsync(request);
            return parameters.Get<int>("@RowsAffected") > 0;
        }

        public async Task<int> GetTotalWorkplacesCountAsync(WorkplaceRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SearchText", request.SearchText ?? string.Empty);
            parameters.Add("@IsActive", request.IsActive);
            parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "OH_Get_Workplaces_Count",
                Parameters = parameters
            };

            await _dapper.ExecuteAsync(storedProcedureRequest);
            return parameters.Get<int>("@TotalCount");
        }

        public async Task<int> InsertFeedLike(FeedLikeRequest req)
        {
            var p = new DynamicParameters();
            p.Add("@Website_ID", 9);
            p.Add("@FeedID", req.FeedID);
            p.Add("@ObjectID", req.ObjectID);
            p.Add("@CommentID", req.CommentID);
            p.Add("@FeedLikeTypeID", req.FeedLikeTypeID);
            p.Add("@FeedChannelID", req.LoginFeedChannelID);

            using (var conn = _dapper.GetConnection())
            {
                return await conn.ExecuteAsync("glivebooks.dbo.crm_Insert_Feed_Likes", p, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> SaveProjectResponse(ProjectResponseRequest req)
        {
            var p = new DynamicParameters();
            p.Add("@ListingID", "0");
            p.Add("@ResponseID", "0");
            p.Add("@ProjectID", req.ProjectID);
            p.Add("@ResponseFeedChannelID", req.ResponseFeedChannelID);
            p.Add("@AssociateFeedChannelID", req.AssociateFeedChannelID);
            p.Add("@ResponseStatusID", req.ResponseStatusID);
            p.Add("@ResponseText", req.ResponseText);
            p.Add("@ResponseChannelID", req.ResponseChannelID);

            using (var conn = _dapper.GetConnection())
            {
                return await conn.ExecuteAsync("glivebooks.dbo.crm_insert_feed_listing_homes_leads", p, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ProjectAssociateResponse>> GetNextLeadAssociate(string projectId, string projectTypeId)
        {
            var p = new DynamicParameters();
            p.Add("@EntityFeedChannelID", 0);
            p.Add("@ApplicationID", 1);
            p.Add("@ProjectID", projectId);
            p.Add("@WorkSpaceID", projectTypeId);
            p.Add("@SpaceID", 1);
            p.Add("@ContactID", "");
            p.Add("@ContactFeedChannelID", 0);
            p.Add("@RoleID", 0);

            using (var conn = _dapper.GetConnection())
            {
                return await conn.QueryAsync<ProjectAssociateResponse>("oxyzen_homes.dbo.OH_Get_Next_Lead_Associate", p, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<string> GetCompanyDetails(string CompanyFeedChannelID, string FeedChannelID, string EmployeeOnly, string Filtername)
        {
            var p = new DynamicParameters();
            p.Add("@LoginFeedChannelID", FeedChannelID);
            p.Add("@FeedChannelID", CompanyFeedChannelID);
            p.Add("@OnlyOneAddress", "1");
            p.Add("@EmployeeOnly", EmployeeOnly);
            p.Add("@Filtername", Filtername);

            using (var conn = _dapper.GetConnection())
            {
                var data = await conn.QueryAsync<ProjectAssociateResponse>("ode.dbo.ode_get_company_details", p, commandType: CommandType.StoredProcedure);
                string json = JsonConvert.SerializeObject(data);
                return json;
            }

           
        }

        public async Task<string> GetProjectDetails(string ProjectName, string DeveloperFeedChannelID, string ProjectID)
        {
            var p = new DynamicParameters();
            p.Add("@ProjectName", ProjectName);
            p.Add("@DeveloperFeedChannelID", DeveloperFeedChannelID);
            p.Add("@ProjectID", ProjectID);
            using (var conn = _dapper.GetConnection())
            {
                var data = await conn.QueryAsync<ProjectAssociateResponse>("oxyzen_homes.dbo.OH_get_projects", p, commandType: CommandType.StoredProcedure);
                string json = JsonConvert.SerializeObject(data);
                return json;
            }


        }

        public async Task<string> GetCompanyExecutive(string CompanyFeedChannelID, string FeedChannelID)
        {
            var p = new DynamicParameters();
            p.Add("@EntityFeedChannelID", CompanyFeedChannelID);
            p.Add("@FeedChannelID", FeedChannelID);

            using (var conn = _dapper.GetConnection())
            {
                var data = await conn.QueryAsync<ProjectAssociateResponse>("ode.dbo.ode_Get_CompanyExecutives", p, commandType: CommandType.StoredProcedure);
                string json = JsonConvert.SerializeObject(data);
                return json;
            }


        }

        public async Task<string> GetFeedChannelDetails(string FeedChannelID)
        {
            var data = await _dapper.ExecuteTableFunctionAsync<dynamic>("glivebooks.dbo.[crm_feed_Get_FeedChannel_Details]", new { FeedChannelID });
            string json = JsonConvert.SerializeObject(data);
            return json;

        }

        public async Task<string> GetWorkteamMembers(string WorkteamId, string CompanyFeedChannelID)
        {
            var p = new DynamicParameters();
            p.Add("@EntityFeedChannelID", CompanyFeedChannelID);
            p.Add("@Workteamid", WorkteamId);
            p.Add("@RoleLevel", "0");
            p.Add("@Onroll", "1");
            using (var conn = _dapper.GetConnection())
            {
                var data = await conn.QueryAsync<dynamic>("glivebooks.dbo.crm_get_feed_workteam_members", p, commandType: CommandType.StoredProcedure);
                string json = JsonConvert.SerializeObject(data);
                return json;
            }
        }

        public async Task<string> GetWorkteamDetails(string SearchString, string CompanyFeedChannelID)
        {
            var p = new DynamicParameters();
            p.Add("@EntityFeedChannelID", CompanyFeedChannelID);
            p.Add("@SearchString", SearchString);
            p.Add("@Action", "Get");
            using (var conn = _dapper.GetConnection())
            {
                var data = await conn.QueryAsync<dynamic>("glivebooks.dbo.crm_insert_Feed_Workteam", p, commandType: CommandType.StoredProcedure);
                string json = JsonConvert.SerializeObject(data);
                return json;
            }
        }

        public async Task<CRM_Lead> CreateLead(string WebsiteID, string ContactID, string Interaction_TypeID, string URL, string PageName, string Form_ButtonID, string Placement
            , string Interaction_Message, string Tagtypedata, string CallerPhoneNumber, string ThreadTitle, string ContactText, string RatingParameter, string Name
            , string Email, string ReceiverContactID, string CallStatus, string CallDuration, string Calltoken, string CallRecordingURL, string NextThreadID
            , string project_id, string country_code, string WishlistName, string PrivacySetting, string ThreadType, string RootNode, string AssociateFeedChannelID
            , string LeadSalesChannelID, string fbid, string Source, string MediaID, string EntityFeedChannelID, string ActivitySubTypeID, string ActivityChannelID
            , string ActivityDescription, string ActivityStatusID, string schedule_date, string StartTime, string EndTime, string ActivityThreadScheduling_StatusID
            , string Enddate, string ActivityTagtypedata, string AgencyFeedChannelID, string AgencyContactFeedChanelID, string Salutation = ""
            , string LeadFeedChannelID = "", string Language = "", string PropertyID = null, string ListingID = null
            , string Seperator = null, string Triplet_Separator = null)
        {
            var Crm = new CRM_Lead();
            var p = new DynamicParameters();

            // INPUT PARAMETERS
            p.Add("@triplet_separator", Triplet_Separator);
            p.Add("@separator", Seperator);
            p.Add("@Property_ID", PropertyID);
            p.Add("@listing_ID", ListingID);
            p.Add("@WebsiteID", WebsiteID);
            p.Add("@ContactID", ContactID);
            p.Add("@Interaction_TypeID", Interaction_TypeID);
            p.Add("@URL", URL);
            p.Add("@PageName", PageName);
            p.Add("@Form_ButtonID", Form_ButtonID);
            p.Add("@Placement", Placement);
            p.Add("@Interaction_Message", Interaction_Message);
            p.Add("@Tagtypedata", Tagtypedata);
            p.Add("@CallerPhoneNumber", CallerPhoneNumber);
            p.Add("@ThreadTitle", ThreadTitle);
            p.Add("@ContactText", ContactText);
            p.Add("@RatingParameter", RatingParameter);
            p.Add("@Name", Name);
            p.Add("@Email", Email);
            p.Add("@ReceiverContactID", ReceiverContactID);
            p.Add("@CallStatus", CallStatus);
            p.Add("@CallDuration", CallDuration);
            p.Add("@Calltoken", Calltoken);
            p.Add("@CallRecordingURL", CallRecordingURL);
            p.Add("@NextThreadID", NextThreadID);
            p.Add("@project_id", project_id);
            p.Add("@country_code", country_code);
            p.Add("@WishlistName", WishlistName);
            p.Add("@PrivacySetting", PrivacySetting);
            p.Add("@ThreadType", ThreadType);
            p.Add("@RootNode", RootNode);
            p.Add("@AssociateFeedChannelID", AssociateFeedChannelID);
            p.Add("@LeadSalesChannelID", LeadSalesChannelID);
            p.Add("@fbid", fbid);
            p.Add("@Source", Source);
            p.Add("@MediaID", MediaID);
            p.Add("@EntityFeedChannelID", EntityFeedChannelID);
            p.Add("@AgencyFeedChannelID", AgencyFeedChannelID);
            p.Add("@AgencyContactFeedChanelID", AgencyContactFeedChanelID);
            p.Add("@ActivitySubTypeID", ActivitySubTypeID);
            p.Add("@ActivityChannelID", ActivityChannelID);
            p.Add("@ActivityDescription", ActivityDescription);
            p.Add("@ActivityStatusID", ActivityStatusID);
            p.Add("@schedule_date", schedule_date);
            p.Add("@StartTime", StartTime);
            p.Add("@EndTime", EndTime);
            p.Add("@ActivityThreadScheduling_StatusID", ActivityThreadScheduling_StatusID);
            p.Add("@Enddate", Enddate);
            p.Add("@Salutation", Salutation);
            p.Add("@LeadFeedChannelID", LeadFeedChannelID);
            p.Add("@language", Language);
            p.Add("@ActivityTagtypedata", ActivityTagtypedata);

            // OUTPUT PARAMETERS
            p.Add("@LeadThreadID", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
            p.Add("@ApplicantId", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
            p.Add("@LeadthreadMsg", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
            p.Add("@activitythreadID", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            using (var conn = _dapper.GetConnection())
            {
                var data = await conn.QueryAsync<dynamic>("oxyzen_homes.dbo.CreateLead ", p, commandType: CommandType.StoredProcedure);
                Crm.LeadThreadID = p.Get<string>("@LeadThreadID");
                Crm.ApplicantId = p.Get<string>("@ApplicantId");
                Crm.LeadThreadMsg = p.Get<string>("@LeadthreadMsg");
                Crm.ActivityThreadID = p.Get<string>("@activitythreadID");
                return Crm;
            }   
        }
    }
}