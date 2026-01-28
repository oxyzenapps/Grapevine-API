using grapevineCommon.Model.Workplace;
using grapevineCommon.Model.Workplace.GrapevineCommon.Models.Workplace;
using grapevineRepository.Interfaces;
using grapevineService.Interfaces;

namespace grapevineServices.Model
{
    public class WorkplaceService : IWorkplaceService
    {
        private readonly IWorkplaceRepository _repo;
        public WorkplaceService(IWorkplaceRepository repo) => _repo = repo;

        public async Task<List<WorkplaceLocationResponse>> GetCurrentLocationAsync(string contactId, int websiteId)
        {
            var data = await _repo.GetCurrentLocation(contactId, websiteId);
            return data.Cast<WorkplaceLocationResponse>().ToList();
        }

        // Use the name UpdateFilterAsync here
        public async Task<string> UpdateFilterAsync(UpdateFilterRequest request)
        {
            string ssn = "example_ssn";
            string feedChannelId = "example_channel";
            return await _repo.UpdateFilterParameter(request, ssn, feedChannelId);
        }

        // Use the name SearchLocationAsync here
        public async Task<List<WorkplaceSearchResponse>> SearchLocationAsync(string query, string localityRequired)
        {
            var data = await _repo.SearchLocation(query, localityRequired);
            return data.Cast<WorkplaceSearchResponse>().ToList();
        }
    }
}