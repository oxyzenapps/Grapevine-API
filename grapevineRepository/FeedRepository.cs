using Dapper;
using grapevineCommon.Model.Feed;
using grapevineData;
using grapevineData.Interfaces;
using grapevineRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineRepository
{
    public class FeedRepository : IFeedRepository
    {
        private readonly IDapperExecutor _dapper;
        private StoredProcedureRequest storedProcedureRequest = null;

        public FeedRepository(IDapperExecutor dapper)
        {
            this._dapper = dapper;
        }

        public async Task<FeedResponse> GetFeedAsync(FeedRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WebsiteID", request.WebsiteID);
            parameters.Add("@LoginFeedChannelID", request.LoginFeedChannelID);
            parameters.Add("@FeedChannelID", request.FeedChannelID);
            parameters.Add("@ExcludeFeedChannelID", request.ExcludeFeedChannelID ? 1 : 0);
            parameters.Add("@contactFeedChannelID", request.ContactFeedChannelID);
            parameters.Add("@ExcludeContactID", request.ExcludeContactID ? 1 : 0);
            parameters.Add("@TaggedOnly", request.TaggedOnly);
            parameters.Add("@FeedBy", request.FeedBy);
            parameters.Add("@FeedID", request.FeedID);
            parameters.Add("@ObjectID", request.ObjectID);
            parameters.Add("@FeedTypeID", request.FeedTypeID);
            parameters.Add("@ObjectTypeID", request.ObjectTypeID);
            parameters.Add("@PageNo", request.PageNo);
            parameters.Add("@SearchString", request.SearchString);
            parameters.Add("@FeedChannelParticipantTypeID", request.FeedChannelParticipantTypeID);
            parameters.Add("@ActivityID", request.ActivityID);
            parameters.Add("@ListingID", request.ListingID);
            parameters.Add("@UniversalTransactionID", request.UniversalTransactionID);
            parameters.Add("@FeedVisibilityID", request.FeedVisibilityID);
            parameters.Add("@MinViewCount", request.MinViewCount);
            parameters.Add("@MaxViewCount", request.MaxViewCount);
            parameters.Add("@Isposted", request.IsPosted);
            parameters.Add("@DateUpto", request.DateUpto);
            parameters.Add("@Announcement", request.Announcement);
            parameters.Add("@LanguageID", request.LanguageID);
            parameters.Add("@Visible", request.Visible);
            parameters.Add("@Favourite", request.Favourite);
            parameters.Add("@ForYou", request.ForYou);
            parameters.Add("@SortOrder", request.SortOrder);
            parameters.Add("@Query", request.Query);
            parameters.Add("@SearchID", request.searchID, direction: ParameterDirection.Output);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_Feed",
                Parameters = parameters
            };
            
            var feeds = await _dapper.ExecuteAsync<FeedItem>(storedProcedureRequest);
            
            var searchId = parameters.Get<int>("@SearchID");

            return new FeedResponse
            {
                SearchID = searchId,
                Feeds = feeds.ToList()
            };
        }
    }
}
