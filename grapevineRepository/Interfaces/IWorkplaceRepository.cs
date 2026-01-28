using grapevineCommon.Model.Workplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineRepository.Interfaces
{
    public interface IWorkplaceRepository
    {
        Task<IEnumerable<UserProfileLocation>> GetCurrentLocation(string contactId, int websiteId);
        Task<string> UpdateFilterParameter(UpdateFilterRequest request, string ssn, string feedChannelId);
        Task<IEnumerable<LocationSearchItem>> SearchLocation(string searchString, string localityIdRequired);
    }
}