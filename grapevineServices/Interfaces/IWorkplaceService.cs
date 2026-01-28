using grapevineCommon.Model.Workplace;
using grapevineCommon.Model.Workplace.GrapevineCommon.Models.Workplace;

namespace grapevineService.Interfaces
{
    public interface IWorkplaceService
    {
        // Changed method names to match Controller calls
        Task<List<WorkplaceLocationResponse>> GetCurrentLocationAsync(string contactId, int websiteId);

        Task<string> UpdateFilterAsync(UpdateFilterRequest request); // Match!

        Task<List<WorkplaceSearchResponse>> SearchLocationAsync(string query, string localityRequired = "0"); // Match!
    }
}