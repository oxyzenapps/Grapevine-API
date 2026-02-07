using grapevineCommon.Model.OxygenCrm;
using grapevineCommon.Model.Workplace;
using GrapevineCommon.Model.Workplace;
using grapevineRepository.Interfaces;
using grapevineService.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace grapevineServices.Model
{
    public class WorkplaceService : IWorkplaceService
    {
        private readonly IWorkplaceRepository _repo;
        private readonly IStringEncryptor _stringEncryptor;

        public WorkplaceService(
            IWorkplaceRepository repo,
            IStringEncryptor stringEncryptor)
        {
            _repo = repo;
            _stringEncryptor = stringEncryptor;
        }

        // --- Implementation of IWorkplaceService Members ---

        public Task<int> AddFeedLike(FeedLikeRequest request) => _repo.InsertFeedLike(request);

        public Task<int> CreateProjectResponse(ProjectResponseRequest request) => _repo.SaveProjectResponse(request);

        public Task<IEnumerable<ProjectAssociateResponse>> GetAssociates(string projectId, string projectTypeId) => _repo.GetNextLeadAssociate(projectId, projectTypeId);

        public async Task<List<WorkplaceLocationResponse>> GetCurrentLocationAsync(string contactId, int websiteId)
        {
            var data = await _repo.GetCurrentLocation(contactId, websiteId);
            return data?.Select(CreateWorkplaceLocationResponse).ToList()
                   ?? new List<WorkplaceLocationResponse>();
        }

        public async Task<string> UpdateFilterAsync(UpdateFilterRequest request)
        {
            return await _repo.UpdateFilterParameter(request, "example_ssn", "example_channel");
        }

        public async Task<List<WorkplaceSearchResponse>> SearchLocationAsync(string query, string localityRequired = "0")
        {
            var data = await _repo.SearchLocation(query, localityRequired);
            return data?.Select(CreateSearchResponse).ToList()
                   ?? new List<WorkplaceSearchResponse>();
        }

        public async Task<string> GetSaleListCountAsync(string pageNo, string searchId, string sort, string listingStatusId, string feedChannelId, string myFeedChannelId)
        {
            return await _repo.GetSaleListCount(pageNo, searchId, sort, listingStatusId, feedChannelId, myFeedChannelId);
        }

        public async Task<string> GetAgenciesListAsync(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId)
        {
            var data = await _repo.GetAgenciesList(pageno, searchId, sort, feedChannelId, listingStatusId, myFeedChannelId);
            return JsonSerializer.Serialize(data);
        }

        public async Task<string> GetDevelopersListAsync(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId)
        {
            var data = await _repo.GetDevelopersList(pageno, searchId, sort, feedChannelId, listingStatusId, myFeedChannelId);
            return JsonSerializer.Serialize(data);
        }

        public WorkplaceResponse GetBuyersAlsoLiked(WorkplaceRequest request)
        {
            return GetBuyersAlsoLikedAsync(request).GetAwaiter().GetResult();
        }

        public async Task<WorkplaceResponse> GetBuyersAlsoLikedAsync(WorkplaceRequest request)
        {
            var items = await _repo.GetBuyersAlsoLiked(request.LoginFeedchannelID, request.SearchforID);
            return new WorkplaceResponse
            {
                Success = true,
                IsSuccess = true,
                Data = items?.ToList() ?? new List<WorkplaceItem>(),
                Items = items?.ToList() ?? new List<WorkplaceItem>()
            };
        }

        public async Task<TopLocalitiesResponse> GetTopLocalitiesAsync(string encryptedSearchId)
        {
            var searchId = _stringEncryptor.Decrypt(encryptedSearchId);
            var ds = await _repo.GetTopLocalitiesAsync(searchId);

            return new TopLocalitiesResponse
            {
                Success = true,
                Data = ConvertDataSetToJson(ds)
            };
        }

        // --- Helper Methods ---

        private WorkplaceLocationResponse CreateWorkplaceLocationResponse(object item)
        {
            return new WorkplaceLocationResponse
            {
                LocationName = GetProp(item, "LocationName") ?? "",
                City = GetProp(item, "City") ?? "",
                State = GetProp(item, "State") ?? ""
            };
        }

        private WorkplaceSearchResponse CreateSearchResponse(object item)
        {
            return new WorkplaceSearchResponse
            {
                LocationName = GetProp(item, "LocationName") ?? "",
                LocalityId = GetProp(item, "LocalityId") ?? ""
            };
        }

        private string GetProp(object obj, string name)
        {
            return obj?.GetType().GetProperty(name)?.GetValue(obj)?.ToString();
        }

        private string ConvertDataSetToJson(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0) return "{}";

            var dict = new Dictionary<string, object>();
            foreach (DataTable t in ds.Tables)
            {
                dict[t.TableName] = t.Rows.Cast<DataRow>()
                    .Select(r => t.Columns.Cast<DataColumn>()
                        .ToDictionary(c => c.ColumnName, c => r[c]?.ToString()))
                    .ToList();
            }
            return JsonSerializer.Serialize(dict);
        }

        public async Task<string> GetFeedChannelDetails(string feedChannelId)
        {
            var data = await _repo.GetFeedChannelDetails(feedChannelId);
            return data;
        }

        public async Task<string> GetWorkteamMembers(string WorkteamId,string FeedChannelID)
        {
            var data = await _repo.GetWorkteamMembers(WorkteamId, FeedChannelID);
            return data;
        }
        public async Task<string> GetWorkteamDetails(string SearchString,string FeedChannelID)
        {
            var data = await _repo.GetWorkteamDetails(SearchString, FeedChannelID);
            return data;
        }

        public async Task<CRM_Lead> CreateLead(string project_id, string Salutation, string FirstName, string LastName,
                                    string Email, string country_code, string Mobile, string Tagtypedata, string SalesChannelID,
                                    string AssociateFeedChannelID, string Source, string MediaID, string EntityFeedChannelID,
                                    string AgencyFeedChannelID, string AgencyContactFeedChanelID, string LeadFeedChannelID,
                                    string Language, string MessageText)
        {
            var data = await _repo.CreateLead("9", "", "10", "", "CRM", "12", "Center", MessageText, Tagtypedata, Mobile, "", "", "", FirstName + " " + LastName, Email,
                "", "", "", "", "", "", project_id
               , country_code, "", "", "", "", AssociateFeedChannelID, SalesChannelID, "", Source, MediaID, EntityFeedChannelID
               , "", "", "", "", "", "", "", "", "", "", AgencyFeedChannelID, AgencyContactFeedChanelID, Salutation, LeadFeedChannelID, Language);
            return data;
        }
    }

    // --- Auxiliary Classes and Interfaces ---

    public interface IStringEncryptor
    {
        string Encrypt(string plainText);
        string Decrypt(string encryptedText);
    }

    public class StringEncryptor : IStringEncryptor
    {
        public string Encrypt(string plainText) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

        public string Decrypt(string encryptedText) =>
            Encoding.UTF8.GetString(Convert.FromBase64String(encryptedText));
    }
}