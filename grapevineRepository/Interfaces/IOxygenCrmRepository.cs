//using grapevineCommon.Model.OxygenCrm;
using grapevineCommon.Model.OxygenCrm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineRepository.Interfaces
{
    public interface IOxygenCrmRepository
    {
        Task<GetProjectLeadResponse> GetProjectLeadAsync(GetProjectLeadRequest request);
        Task<OHDeleteAgencyResponse> OH_Delete_AgencyAsync(OHDeleteAgencyRequest request);
        Task<GetAllleadschildResponse> GetAllleadschildAsync(GetAllleadschildRequest request);
        Task<LeadProjectSummaryResponse> GetLeadProjectSummaryAsync(LeadProjectSummaryRequest request);
        Task<CPTaggingParametersResponse> GetCPTaggingParametersAsync(CPTaggingParametersRequest request);
        Task<InsertCPTaggingResponse> InsertLeadCPTaggingAsync(InsertCPTaggingRequest request);
        Task<ShowChannelsResponse> ShowChannelsAsync();
        Task<GetMediaTypeResponse> GetMediaTypeAsync(GetMediaTypeRequest request);
    }
}