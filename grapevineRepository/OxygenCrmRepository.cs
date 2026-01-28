//using Azure.Core;
//using Dapper;
//using grapevineCommon.Models.OxygenCrm;
//using grapevineData;
//using grapevineData.Interfaces;
//using grapevineRepository.Interfaces;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.SqlClient;

//namespace grapevineRepository
//{
//    public class OxygenCrmRepository : IOxygenCrmRepository
//    {
//        private readonly IDapperExecutor _dapper;
//        private StoredProcedureRequest storedProcedureRequest = null;

//        public OxygenCrmRepository(IDapperExecutor dapper)
//        {
//            this._dapper = dapper;
//        }

//        public async Task<GetProjectLeadResponse> GetProjectLeadAsync(GetProjectLeadRequest request)
//        {
//            var parameters = new DynamicParameters();
//            parameters.Add("@searchtext", request.search);
//            parameters.Add("@Leadtagvalue", request.leadtagvalue);
//            parameters.Add("@date", request.date);
//            parameters.Add("@date1", request.date1);
//            parameters.Add("@associateID", request.associate_id);
//            parameters.Add("@projecttypeID", request.project_typeid);
//            parameters.Add("@LocalityID", request.locality_id);
//            parameters.Add("@CampaignId", request.campaignId);
//            parameters.Add("@Mediaid", request.mediaid);
//            parameters.Add("@Source", request.source);
//            parameters.Add("@city_id", request.city_id);
//            parameters.Add("@PeriodType", request.PeriodType);
//            parameters.Add("@StatusID", request.StatusID);
//            parameters.Add("@StageID", request.StageID);
//            parameters.Add("@LoginFeedChannelID", request.LoginFeedChannelID);
//            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
//            parameters.Add("@AssociateFeedChannelID", request.AssociateFeedChannelID);
//            parameters.Add("@PipelineThreshold1", request.PipelineThreshold1);
//            parameters.Add("@PipelineThreshold2", request.PipelineThreshold2);
//            parameters.Add("@PageID", request.PageID);
//            parameters.Add("@PageSize", request.PageSize);
//            parameters.Add("@AgencyFeedChannelID", request.AgencyFeedChannelID);
//            parameters.Add("@AgencyContactFeedChannelID", request.AgencyContactFeedChannelID);

//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "glivebooks.dbo.crm_get_Leads",
//                Parameters = parameters
//            };

//            var leads = await _dapper.ExecuteAsync<LeadItem>(storedProcedureRequest);

//            return new GetProjectLeadResponse
//            {
//                Leads = leads.ToList(),
//                PageNumber = string.IsNullOrEmpty(request.PageID) ? 1 : int.Parse(request.PageID),
//                PageSize = string.IsNullOrEmpty(request.PageSize) ? 20 : int.Parse(request.PageSize)
//            };
//        }

//        public async Task<OHDeleteAgencyResponse> OH_Delete_AgencyAsync(OHDeleteAgencyRequest request)
//        {
//            var parameters = new DynamicParameters();
//            parameters.Add("@AgencyFeedChannelID", request.AgencyFeedChannelID);
//            parameters.Add("@acount", dbType: DbType.Int32, direction: ParameterDirection.Output);
//            parameters.Add("@ErrorMessage", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);

//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "oxyzen_homes.dbo.OH_Delete_Agency",
//                Parameters = parameters
//            };

//            await _dapper.ExecuteAsync(storedProcedureRequest);

//            var acount = parameters.Get<int>("@acount");
//            var errorMessage = parameters.Get<string>("@ErrorMessage");

//            return new OHDeleteAgencyResponse
//            {
//                ErrorMessage = errorMessage ?? string.Empty,
//                DeletedCount = acount,
//                IsSuccess = string.IsNullOrEmpty(errorMessage)
//            };
//        }

//        public async Task<GetAllleadschildResponse> GetAllleadschildAsync(GetAllleadschildRequest request)
//        {
//            var parameters = new DynamicParameters();
//            parameters.Add("@LoginFeedChannelID", request.LoginFeedChannelID);
//            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
//            parameters.Add("@AssociateFeedChannelID", request.AssociateFeedChannelID);
//            parameters.Add("@status", request.StatusID);
//            parameters.Add("@Leadtagvalue", request.Leadtagvalue);
//            parameters.Add("@search_text", request.search_text);
//            parameters.Add("@date", request.date);
//            parameters.Add("@associateID", request.associateID);
//            parameters.Add("@date1", request.date1);
//            parameters.Add("@stage", request.StageID);
//            parameters.Add("@CampaignID", request.CampaignID);
//            parameters.Add("@MediaID", request.MediaID);
//            parameters.Add("@Source", request.Source);
//            parameters.Add("@PipelineThreshold1", request.PipelineThreshold1);
//            parameters.Add("@PipelineThreshold2", request.PipelineThreshold2);
//            parameters.Add("@Index", 0);
//            parameters.Add("@PageID", request.PageID);
//            parameters.Add("@PageSize", request.PageSize);
//            parameters.Add("@LeadThreadID", request.LeadThreadID);
//            parameters.Add("@AgencyFeedChannelID", request.AgencyFeedChannelID);
//            parameters.Add("@AgencyContactFeedChannelID", request.AgencyContactFeedChannelID);

//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "glivebooks.dbo.crm_get_Lead_Project",
//                Parameters = parameters
//            };

//            var leadChildren = await _dapper.ExecuteAsync<LeadItem>(storedProcedureRequest);

//            return new GetAllleadschildResponse
//            {
//                LeadChildren = leadChildren.ToList(),
//                PageNumber = string.IsNullOrEmpty(request.PageID) ? 1 : int.Parse(request.PageID),
//                PageSize = string.IsNullOrEmpty(request.PageSize) ? 20 : int.Parse(request.PageSize)
//            };
//        }

//        public async Task<LeadProjectSummaryResponse> GetLeadProjectSummaryAsync(LeadProjectSummaryRequest request)
//        {
//            if (!string.IsNullOrEmpty(request.date) && request.date != "null" && request.date != "undefined")
//            {
//                request.date = DateTime.Parse(request.date).ToString("yyyy-MM-dd");
//            }
//            if (!string.IsNullOrEmpty(request.date1) && request.date1 != "null" && request.date1 != "undefined")
//            {
//                request.date1 = DateTime.Parse(request.date1).ToString("yyyy-MM-dd");
//            }

//            var parameters = new DynamicParameters();
//            parameters.Add("@LoginFeedChannelID", request.LoginFeedChannelID);
//            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
//            parameters.Add("@AssociateFeedChannelID", request.AssociateFeedChannelID);
//            parameters.Add("@Leadtagvalue", request.Leadtagvalue);
//            parameters.Add("@status", request.status);
//            parameters.Add("@LeadThreadID", request.LeadThreadID);
//            parameters.Add("@search_text", request.search_text);
//            parameters.Add("@date", request.date);
//            parameters.Add("@date1", request.date1);
//            parameters.Add("@stage", request.stage);
//            parameters.Add("@CampaignID", request.CampaignID);
//            parameters.Add("@MediaID", request.MediaID);
//            parameters.Add("@Source", request.Source);
//            parameters.Add("@PipelineThreshold1", request.PipelineThreshold1);
//            parameters.Add("@PipelineThreshold2", request.PipelineThreshold2);
//            parameters.Add("@Index", request.Index);
//            parameters.Add("@AgencyFeedChannelID", request.AgencyFeedChannelID);
//            parameters.Add("@AgencyContactFeedChannelID", request.AgencyContactFeedChannelID);
//            parameters.Add("@PageID", request.PageID);
//            parameters.Add("@PageSize", request.PageSize);

//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "glivebooks.dbo.crm_get_Lead_Project_summary",
//                Parameters = parameters
//            };

//            var leads = await _dapper.ExecuteAsync<LeadItem>(storedProcedureRequest);

//            return new LeadProjectSummaryResponse
//            {
//                Leads = leads.ToList(),
//                PageNumber = string.IsNullOrEmpty(request.PageID) ? 1 : int.Parse(request.PageID),
//                PageSize = string.IsNullOrEmpty(request.PageSize) ? 20 : int.Parse(request.PageSize)
//            };
//        }

//        public async Task<CPTaggingParametersResponse> GetCPTaggingParametersAsync(CPTaggingParametersRequest request)
//        {
//            var parameters = new DynamicParameters();
//            parameters.Add("@Action", "Get");
//            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
//            parameters.Add("@TagExpiryPeriod", string.Empty);
//            parameters.Add("@TagSiteVisitExpiryPeriod", string.Empty);
//            parameters.Add("@CustomerDetailShareProgress", string.Empty);

//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "glivebooks.dbo.crm_insert_lead_CP_tagging_parameters",
//                Parameters = parameters
//            };

//            var parametersList = await _dapper.ExecuteAsync<CPTaggingParametersItem>(storedProcedureRequest);

//            return new CPTaggingParametersResponse
//            {
//                Parameters = parametersList.FirstOrDefault() ?? new CPTaggingParametersItem(),
//                HasParameters = parametersList.Any()
//            };
//        }

//        public async Task<InsertCPTaggingResponse> InsertLeadCPTaggingAsync(InsertCPTaggingRequest request)
//        {
//            var parameters = new DynamicParameters();
//            parameters.Add("@Action", request.ActionType);
//            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
//            parameters.Add("@TagExpiryPeriod", request.TagExpiryPeriod);
//            parameters.Add("@TagSiteVisitExpiryPeriod", request.TagSiteVisitExpiryPeriod);
//            parameters.Add("@CustomerDetailShareProgress", request.CustomerDetailShareProgress);
//            parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "glivebooks.dbo.crm_insert_lead_CP_tagging_parameters",
//                Parameters = parameters
//            };

//            await _dapper.ExecuteAsync(storedProcedureRequest);

//            var rowsAffected = parameters.Get<int>("@RowsAffected");

//            return new InsertCPTaggingResponse
//            {
//                IsSuccess = rowsAffected > 0,
//                AffectedRows = rowsAffected,
//                Message = rowsAffected > 0 ? "Parameters updated successfully" : "Failed to update parameters"
//            };
//        }

//        public async Task<ShowChannelsResponse> ShowChannelsAsync()
//        {
//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "glivebooks.dbo.crm_show_salesChannels"
//            };

//            var channels = await _dapper.ExecuteAsync<ChannelItem>(storedProcedureRequest);

//            return new ShowChannelsResponse
//            {
//                Channels = channels.ToList(),
//                TotalChannels = channels.Count()
//            };
//        }

//        public async Task<GetMediaTypeResponse> GetMediaTypeAsync(GetMediaTypeRequest request)
//        {
//            var parameters = new DynamicParameters();
//            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
//            parameters.Add("@applicant_id", request.applicant_id);

//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "glivebooks.dbo.crm_Get_FilterValueGlive",
//                Parameters = parameters
//            };

//            // Fixed QueryMultipleAsync implementation
//            using (var multiResult = await _dapper.QueryMultipleAsync(storedProcedureRequest))
//            {
//                var mediaTypes = (await multiResult.ReadAsync<MediaTypeItem>()).ToList();
//                var campaigns = (await multiResult.ReadAsync<CampaignItem>()).ToList();
//                var sources = (await multiResult.ReadAsync<SourceItem>()).ToList();
//                var stages = (await multiResult.ReadAsync<StageItem>()).ToList();
//                var statuses = (await multiResult.ReadAsync<StatusItem>()).ToList();

//                return new GetMediaTypeResponse
//                {
//                    MediaTypes = mediaTypes,
//                    Campaigns = campaigns,
//                    Sources = sources,
//                    Stages = stages,
//                    Statuses = statuses
//                };
//            }
//        }
//    }
//}

using Azure.Core;
using Dapper;
using grapevineCommon.Model.OxygenCrm;
using grapevineData;
using grapevineData.Interfaces;
using grapevineRepository.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace grapevineRepository
{
    public class OxygenCrmRepository : IOxygenCrmRepository
    {
        private readonly IDapperExecutor _dapper;
        private StoredProcedureRequest storedProcedureRequest = null;

        public OxygenCrmRepository(IDapperExecutor dapper)
        {
            this._dapper = dapper;
        }

        public async Task<GetProjectLeadResponse> GetProjectLeadAsync(GetProjectLeadRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@searchtext", request.search);
            parameters.Add("@Leadtagvalue", request.leadtagvalue);
            parameters.Add("@date", request.date);
            parameters.Add("@date1", request.date1);
            parameters.Add("@associateID", request.associate_id);
            parameters.Add("@projecttypeID", request.project_typeid);
            parameters.Add("@LocalityID", request.locality_id);
            parameters.Add("@CampaignId", request.campaignId);
            parameters.Add("@Mediaid", request.mediaid);
            parameters.Add("@Source", request.source);
            parameters.Add("@city_id", request.city_id);
            parameters.Add("@PeriodType", request.PeriodType);
            parameters.Add("@StatusID", request.StatusID);
            parameters.Add("@StageID", request.StageID);
            parameters.Add("@LoginFeedChannelID", request.LoginFeedChannelID);
            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
            parameters.Add("@AssociateFeedChannelID", request.AssociateFeedChannelID);
            parameters.Add("@PipelineThreshold1", request.PipelineThreshold1);
            parameters.Add("@PipelineThreshold2", request.PipelineThreshold2);
            parameters.Add("@PageID", request.PageID);
            parameters.Add("@PageSize", request.PageSize);
            parameters.Add("@AgencyFeedChannelID", request.AgencyFeedChannelID);
            parameters.Add("@AgencyContactFeedChannelID", request.AgencyContactFeedChannelID);

            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_Leads",
                Parameters = parameters
            };

            var leads = await _dapper.ExecuteAsync<LeadItem>(storedProcedureRequest);

            return new GetProjectLeadResponse
            {
                Leads = leads.ToList(),
                PageNumber = int.TryParse(request.PageID.ToString(), out int pNum) ? pNum : 1,
                PageSize = int.TryParse(request.PageSize.ToString(), out int pSize) ? pSize : 20
            };
        }

        public async Task<OHDeleteAgencyResponse> OH_Delete_AgencyAsync(OHDeleteAgencyRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AgencyFeedChannelID", request.AgencyFeedChannelID);
            parameters.Add("@acount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ErrorMessage", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);

            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "oxyzen_homes.dbo.OH_Delete_Agency",
                Parameters = parameters
            };

            await _dapper.ExecuteAsync(storedProcedureRequest);

            var acount = parameters.Get<int>("@acount");
            var errorMessage = parameters.Get<string>("@ErrorMessage");

            return new OHDeleteAgencyResponse
            {
                ErrorMessage = errorMessage ?? string.Empty,
                DeletedCount = acount,
                IsSuccess = string.IsNullOrEmpty(errorMessage)
            };
        }

        public async Task<GetAllleadschildResponse> GetAllleadschildAsync(GetAllleadschildRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", request.LoginFeedChannelID);
            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
            parameters.Add("@AssociateFeedChannelID", request.AssociateFeedChannelID);
            parameters.Add("@status", request.StatusID);
            parameters.Add("@Leadtagvalue", request.Leadtagvalue);
            parameters.Add("@search_text", request.search_text);
            parameters.Add("@date", request.date);
            parameters.Add("@associateID", request.associateID);
            parameters.Add("@date1", request.date1);
            parameters.Add("@stage", request.StageID);
            parameters.Add("@CampaignID", request.CampaignID);
            parameters.Add("@MediaID", request.MediaID);
            parameters.Add("@Source", request.Source);
            parameters.Add("@PipelineThreshold1", request.PipelineThreshold1);
            parameters.Add("@PipelineThreshold2", request.PipelineThreshold2);
            parameters.Add("@Index", 0);
            parameters.Add("@PageID", request.PageID);
            parameters.Add("@PageSize", request.PageSize);
            parameters.Add("@LeadThreadID", request.LeadThreadID);
            parameters.Add("@AgencyFeedChannelID", request.AgencyFeedChannelID);
            parameters.Add("@AgencyContactFeedChannelID", request.AgencyContactFeedChannelID);

            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_Lead_Project",
                Parameters = parameters
            };

            var leadChildren = await _dapper.ExecuteAsync<LeadItem>(storedProcedureRequest);

            return new GetAllleadschildResponse
            {
                LeadChildren = leadChildren.ToList(),
                PageNumber = int.TryParse(request.PageID?.ToString(), out int pNum) ? pNum : 1,
                PageSize = int.TryParse(request.PageSize?.ToString(), out int pSize) ? pSize : 20
            };
        }

        public async Task<LeadProjectSummaryResponse> GetLeadProjectSummaryAsync(LeadProjectSummaryRequest request)
        {
            if (!string.IsNullOrEmpty(request.date) && request.date != "null" && request.date != "undefined")
            {
                request.date = DateTime.Parse(request.date).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(request.date1) && request.date1 != "null" && request.date1 != "undefined")
            {
                request.date1 = DateTime.Parse(request.date1).ToString("yyyy-MM-dd");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", request.LoginFeedChannelID);
            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
            parameters.Add("@AssociateFeedChannelID", request.AssociateFeedChannelID);
            parameters.Add("@Leadtagvalue", request.Leadtagvalue);
            parameters.Add("@status", request.status);
            parameters.Add("@LeadThreadID", request.LeadThreadID);
            parameters.Add("@search_text", request.search_text);
            parameters.Add("@date", request.date);
            parameters.Add("@date1", request.date1);
            parameters.Add("@stage", request.stage);
            parameters.Add("@CampaignID", request.CampaignID);
            parameters.Add("@MediaID", request.MediaID);
            parameters.Add("@Source", request.Source);
            parameters.Add("@PipelineThreshold1", request.PipelineThreshold1);
            parameters.Add("@PipelineThreshold2", request.PipelineThreshold2);
            parameters.Add("@Index", request.Index);
            parameters.Add("@AgencyFeedChannelID", request.AgencyFeedChannelID);
            parameters.Add("@AgencyContactFeedChannelID", request.AgencyContactFeedChannelID);
            parameters.Add("@PageID", request.PageID);
            parameters.Add("@PageSize", request.PageSize);

            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_Lead_Project_summary",
                Parameters = parameters
            };

            var leads = await _dapper.ExecuteAsync<LeadItem>(storedProcedureRequest);

            return new LeadProjectSummaryResponse
            {
                Leads = leads.ToList(),
                PageNumber = int.TryParse(request.PageID?.ToString(), out int pNum) ? pNum : 1,
                PageSize = int.TryParse(request.PageSize?.ToString(), out int pSize) ? pSize : 20
            };
        }

        public async Task<CPTaggingParametersResponse> GetCPTaggingParametersAsync(CPTaggingParametersRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "Get");
            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
            parameters.Add("@TagExpiryPeriod", string.Empty);
            parameters.Add("@TagSiteVisitExpiryPeriod", string.Empty);
            parameters.Add("@CustomerDetailShareProgress", string.Empty);

            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_insert_lead_CP_tagging_parameters",
                Parameters = parameters
            };

            var parametersList = await _dapper.ExecuteAsync<CPTaggingParametersItem>(storedProcedureRequest);

            return new CPTaggingParametersResponse
            {
                Parameters = parametersList.FirstOrDefault() ?? new CPTaggingParametersItem(),
                HasParameters = parametersList.Any()
            };
        }

        public async Task<InsertCPTaggingResponse> InsertLeadCPTaggingAsync(InsertCPTaggingRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Action", request.ActionType);
            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
            parameters.Add("@TagExpiryPeriod", request.TagExpiryPeriod);
            parameters.Add("@TagSiteVisitExpiryPeriod", request.TagSiteVisitExpiryPeriod);
            parameters.Add("@CustomerDetailShareProgress", request.CustomerDetailShareProgress);
            parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_insert_lead_CP_tagging_parameters",
                Parameters = parameters
            };

            await _dapper.ExecuteAsync(storedProcedureRequest);

            var rowsAffected = parameters.Get<int>("@RowsAffected");

            return new InsertCPTaggingResponse
            {
                IsSuccess = rowsAffected > 0,
                AffectedRows = rowsAffected,
                Message = rowsAffected > 0 ? "Parameters updated successfully" : "Failed to update parameters"
            };
        }

        public async Task<ShowChannelsResponse> ShowChannelsAsync()
        {
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_show_salesChannels"
            };

            var channels = await _dapper.ExecuteAsync<ChannelItem>(storedProcedureRequest);

            return new ShowChannelsResponse
            {
                Channels = channels.ToList(),
                TotalChannels = channels.Count()
            };
        }

        public async Task<GetMediaTypeResponse> GetMediaTypeAsync(GetMediaTypeRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EntityFeedChannelID", request.EntityFeedChannelID);
            parameters.Add("@applicant_id", request.applicant_id);

            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_Get_FilterValueGlive",
                Parameters = parameters
            };

            // Using the connection from Dapper to handle multiple result sets
            using (var connection = _dapper.GetConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(storedProcedureRequest.ProcedureName, storedProcedureRequest.Parameters, commandType: CommandType.StoredProcedure))
                {
                    var mediaTypes = (await multi.ReadAsync<MediaTypeItem>()).ToList();
                    var campaigns = (await multi.ReadAsync<CampaignItem>()).ToList();
                    var sources = (await multi.ReadAsync<SourceItem>()).ToList();
                    var stages = (await multi.ReadAsync<StageItem>()).ToList();
                    var statuses = (await multi.ReadAsync<StatusItem>()).ToList();

                    return new GetMediaTypeResponse
                    {
                        MediaTypes = mediaTypes,
                        Campaigns = campaigns,
                        Sources = sources,
                        Stages = stages,
                        Statuses = statuses
                    };
                }
            }
        }
    }
}
