

using grapevineCommon.Model.OxygenCrm;
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
        Task<string> GetFeedChannelDetails(string feedChannelId);
        Task<string> GetWorkteamMembers(string WorkteamId, string FeedChannelID);
        Task<string> GetWorkteamDetails(string SearchString, string FeedChannelID);
        Task<CRM_Lead> CreateLead(string project_id, string Salutation, string FirstName, string LastName,
                                    string Email, string country_code, string Mobile, string Tagtypedata, string SalesChannelID,
                                    string AssociateFeedChannelID, string Source, string MediaID, string EntityFeedChannelID,
                                    string AgencyFeedChannelID, string AgencyContactFeedChanelID, string LeadFeedChannelID,
                                    string Language, string MessageText);

        Task<string> GetCompanyDetails(string CompanyFeedChannelID, string FeedChannelID, string EmployeeOnly, string Filtername);
        Task<string> GetProjectDetails(string ProjectName, string DeveloperFeedChannelID, string ProjectID);
        Task<string> GetCompanyExecutive(string CompanyFeedChannelID, string FeedChannelID);

        Task<dynamic> execproc(string ProcedureName, string ParametersList);

    }
}