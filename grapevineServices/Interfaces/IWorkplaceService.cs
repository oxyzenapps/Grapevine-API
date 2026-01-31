

using grapevineCommon.Model.Workplace;
//using grapevineCommon.Model.Workplace.GrapevineCommon.Models.Workplace;
using GrapevineCommon.Model.Workplace;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace grapevineService.Interfaces
{
    public interface IWorkplaceService
    {
        Task<List<WorkplaceLocationResponse>> GetCurrentLocationAsync(string contactId, int websiteId);

        Task<string> UpdateFilterAsync(UpdateFilterRequest request);

        Task<List<WorkplaceSearchResponse>> SearchLocationAsync(string query, string localityRequired = "0");

        Task<string> GetSaleListCountAsync(string pageNo, string searchId, string sort, string listingStatusId, string feedChannelId, string myFeedChannelId);

        Task<string> GetAgenciesListAsync(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId);

        Task<string> GetDevelopersListAsync(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId);

        WorkplaceResponse GetBuyersAlsoLiked(WorkplaceRequest request);
        Task<WorkplaceResponse> GetBuyersAlsoLikedAsync(WorkplaceRequest request);
        Task<TopLocalitiesResponse> GetTopLocalitiesAsync(string encryptedSearchId);

        Task<int> AddFeedLike(FeedLikeRequest request);
        Task<int> CreateProjectResponse(ProjectResponseRequest request);
        Task<IEnumerable<ProjectAssociateResponse>> GetAssociates(string projectId, string projectTypeId);

    }
}