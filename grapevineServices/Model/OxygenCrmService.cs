using System;
using System.Threading.Tasks;
using grapevineCommon.Model.OxygenCrm;
using grapevineRepository.Interfaces;
using grapevineServices.Interfaces;
using Microsoft.Extensions.Logging;

namespace grapevineServices
{
    public class OxygenCrmService : IOxygenCrmService
    {
        private readonly IOxygenCrmRepository _oxygenCrmRepository;
        private readonly ILogger<OxygenCrmService> _logger;

        public OxygenCrmService(IOxygenCrmRepository oxygenCrmRepository, ILogger<OxygenCrmService> logger)
        {
            _oxygenCrmRepository = oxygenCrmRepository ?? throw new ArgumentNullException(nameof(oxygenCrmRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<GetProjectLeadResponse> GetProjectLeadsAsync(GetProjectLeadRequest request)
        {
            try
            {
                _logger.LogInformation("Fetching project leads with search: {SearchText}", request.search);
                var response = await _oxygenCrmRepository.GetProjectLeadAsync(request);
                _logger.LogInformation("Retrieved {LeadCount} project leads", response.Leads.Count);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching project leads");
                throw;
            }
        }

        public async Task<GetAllleadschildResponse> GetAllLeadsChildAsync(GetAllleadschildRequest request)
        {
            try
            {
                _logger.LogInformation("Fetching all leads child data for LoginFeedChannelID: {LoginFeedChannelID}", request.LoginFeedChannelID);
                var response = await _oxygenCrmRepository.GetAllleadschildAsync(request);
                _logger.LogInformation("Retrieved {LeadChildCount} lead children", response.LeadChildren.Count);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all leads child data");
                throw;
            }
        }

        public async Task<LeadProjectSummaryResponse> GetLeadProjectSummaryAsync(LeadProjectSummaryRequest request)
        {
            try
            {
                _logger.LogInformation("Fetching lead project summary for LoginFeedChannelID: {LoginFeedChannelID}", request.LoginFeedChannelID);
                var response = await _oxygenCrmRepository.GetLeadProjectSummaryAsync(request);
                _logger.LogInformation("Retrieved {LeadCount} lead project summaries", response.Leads.Count);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching lead project summary");
                throw;
            }
        }

        public async Task<OHDeleteAgencyResponse> DeleteAgencyAsync(OHDeleteAgencyRequest request)
        {
            try
            {
                _logger.LogInformation("Deleting agency with AgencyFeedChannelID: {AgencyFeedChannelID}", request.AgencyFeedChannelID);
                var response = await _oxygenCrmRepository.OH_Delete_AgencyAsync(request);
                _logger.LogInformation("Agency deletion result: Success={IsSuccess}, DeletedCount={DeletedCount}",
                    response.IsSuccess, response.DeletedCount);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting agency");
                throw;
            }
        }

        public async Task<ShowChannelsResponse> GetChannelsAsync(ShowChannelsRequest request)
        {
            try
            {
                _logger.LogInformation("Fetching sales channels");
                var response = await _oxygenCrmRepository.ShowChannelsAsync();
                _logger.LogInformation("Retrieved {ChannelCount} sales channels", response.TotalChannels);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching sales channels");
                throw;
            }
        }

        public async Task<CPTaggingParametersResponse> GetCPTaggingParametersAsync(CPTaggingParametersRequest request)
        {
            try
            {
                _logger.LogInformation("Fetching CP tagging parameters for EntityFeedChannelID: {EntityFeedChannelID}",
                    request.EntityFeedChannelID);
                var response = await _oxygenCrmRepository.GetCPTaggingParametersAsync(request);
                _logger.LogInformation("CP tagging parameters retrieved: HasParameters={HasParameters}",
                    response.HasParameters);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching CP tagging parameters");
                throw;
            }
        }

        public async Task<InsertCPTaggingResponse> InsertOrUpdateCPTaggingAsync(InsertCPTaggingRequest request)
        {
            try
            {
                _logger.LogInformation("Inserting/updating CP tagging parameters for EntityFeedChannelID: {EntityFeedChannelID}",
                    request.EntityFeedChannelID);
                var response = await _oxygenCrmRepository.InsertLeadCPTaggingAsync(request);
                _logger.LogInformation("CP tagging operation result: Success={IsSuccess}, AffectedRows={AffectedRows}, Message={Message}",
                    response.IsSuccess, response.AffectedRows, response.Message);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting/updating CP tagging parameters");
                throw;
            }
        }

        public async Task<GetMediaTypeResponse> GetMediaTypeAndRelatedMastersAsync(GetMediaTypeRequest request)
        {
            try
            {
                _logger.LogInformation("Fetching media types and related masters for EntityFeedChannelID: {EntityFeedChannelID}",
                    request.EntityFeedChannelID);
                var response = await _oxygenCrmRepository.GetMediaTypeAsync(request);
                _logger.LogInformation("Media types retrieved: MediaTypes={MediaCount}, Campaigns={CampaignCount}, Sources={SourceCount}",
                    response.MediaTypes.Count, response.Campaigns.Count, response.Sources.Count);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching media types and related masters");
                throw;
            }
        }
    }
}
