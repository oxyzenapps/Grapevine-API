using grapevineCommon.Model.OxygenCrm;

namespace grapevineServices.Interfaces
{
    public interface IOxygenCrmService
    {
        Task<GetProjectLeadResponse> GetProjectLeadsAsync(GetProjectLeadRequest request);
        Task<GetAllleadschildResponse> GetAllLeadsChildAsync(GetAllleadschildRequest request);
        Task<LeadProjectSummaryResponse> GetLeadProjectSummaryAsync(LeadProjectSummaryRequest request);
        Task<OHDeleteAgencyResponse> DeleteAgencyAsync(OHDeleteAgencyRequest request);
        Task<ShowChannelsResponse> GetChannelsAsync(ShowChannelsRequest request);
        Task<CPTaggingParametersResponse> GetCPTaggingParametersAsync(CPTaggingParametersRequest request);
        Task<InsertCPTaggingResponse> InsertOrUpdateCPTaggingAsync(InsertCPTaggingRequest request);
        Task<GetMediaTypeResponse> GetMediaTypeAndRelatedMastersAsync(GetMediaTypeRequest request);
    }
}