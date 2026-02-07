using grapevineCommon.Model.OxygenCrm;
using grapevineCommon.Model.Workplace;
using GrapevineCommon.Model.Workplace;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace grapevineRepository.Interfaces
{
    public interface IWorkplaceRepository
    {
        Task<IEnumerable<UserProfileLocation>> GetCurrentLocation(string contactId, int websiteId);
        Task<string> UpdateFilterParameter(UpdateFilterRequest request, string ssn, string feedChannelId);
        Task<IEnumerable<LocationSearchItem>> SearchLocation(string searchString, string localityIdRequired);
        Task<string> GetSaleListCount(string pageNo, string searchId, string sort, string listingStatusId, string feedChannelId, string myFeedChannelId);
        Task<IEnumerable<dynamic>> GetAgenciesList(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId);
        Task<IEnumerable<dynamic>> GetDevelopersList(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId);
        Task<List<WorkplaceItem>> GetBuyersAlsoLiked(string loginFeedchannelID, string searchforID);
        Task<int> GetSearchID(string loginFeedchannelID, string searchforID);

        
        Task<IEnumerable<dynamic>> GetProjectList(string searchId, int pageNo, int pageSize);
        Task<bool> SendProjectEmail(string email, string name, string projectId, string responseByFeedChannelId,
                                   string message, string from, string emailType, string applicantId);
        Task<dynamic> GetProjectById(string projectId);
        Task<bool> UpdateProjectStatus(string projectId, string status);

        Task<DataSet> GetTopLocalitiesAsync(string searchId);

        Task<int> InsertFeedLike(FeedLikeRequest request);
        Task<int> SaveProjectResponse(ProjectResponseRequest request);
        Task<IEnumerable<ProjectAssociateResponse>> GetNextLeadAssociate(string projectId, string projectTypeId);
        Task<string> GetFeedChannelDetails(string FeedChannelID);
        Task<string> GetWorkteamMembers(string WorkteamId, string CompanyFeedChannelID);
        Task<string> GetWorkteamDetails(string SearchString, string CompanyFeedChannelID);
        Task<CRM_Lead> CreateLead(string WebsiteID, string ContactID, string Interaction_TypeID, string URL, string PageName, string Form_ButtonID, string Placement
            , string Interaction_Message, string Tagtypedata, string CallerPhoneNumber, string ThreadTitle, string ContactText, string RatingParameter, string Name
            , string Email, string ReceiverContactID, string CallStatus, string CallDuration, string Calltoken, string CallRecordingURL, string NextThreadID
            , string project_id, string country_code, string WishlistName, string PrivacySetting, string ThreadType, string RootNode, string AssociateFeedChannelID
            , string LeadSalesChannelID, string fbid, string Source, string MediaID, string EntityFeedChannelID, string ActivitySubTypeID, string ActivityChannelID
            , string ActivityDescription, string ActivityStatusID, string schedule_date, string StartTime, string EndTime, string ActivityThreadScheduling_StatusID
            , string Enddate, string ActivityTagtypedata, string AgencyFeedChannelID, string AgencyContactFeedChanelID, string Salutation = ""
            , string LeadFeedChannelID = "", string Language = "", string PropertyID = null, string ListingID = null
            , string Seperator = null, string Triplet_Separator = null);

    }
}