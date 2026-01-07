using Azure.Core;
using Dapper;
using grapevineCommon.Model.Feed;
using grapevineData;
using grapevineData.Interfaces;
using grapevineRepository.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            feeds = await BindFeedDetails(feeds.ToList(),request.LoginFeedChannelID,request.WebsiteID);
            var searchId = parameters.Get<int>("@SearchID");

            return new FeedResponse
            {
                SearchID = searchId,
                Feeds = feeds.ToList()
            };
        }

        public async Task<List<FeedItem>> BindFeedDetails(List<FeedItem> feeds,int LoginFeedChannelID, int WebsiteID)
        {
            for(int i =0; i< feeds.Count; i++)
            {
                feeds[i]._Feed_Like_Reactions = await GetReactions(feeds[i].FeedID, feeds[i].ObjectID, LoginFeedChannelID, WebsiteID);
                feeds[i]._Feed_Comments = await GetComments(feeds[i].FeedID, "0", "2", "0", LoginFeedChannelID,WebsiteID,2);
                feeds[i]._GroupMembers = await GetTagMembers(feeds[i].FeedID, WebsiteID, feeds[i].FeedTagString,LoginFeedChannelID);
                feeds[i]._FeedLikeTypes = await getLikeTypes();
                //if (feeds[i].SharedFeedID != "0")
                //{
                //    var sharedfeed = await GetFeedAsync(new FeedRequest
                //    {
                //        WebsiteID = WebsiteID,
                //        LoginFeedChannelID = LoginFeedChannelID,
                //        FeedID = feeds[i].SharedFeedID,
                //    });
                //    if (sharedfeed.Feeds.Count > 0)
                //    {
                //        feeds[i]._SharedFeed = sharedfeed.Feeds.FirstOrDefault();
                //    }
                //}
            }
            return feeds;
        }

        public async Task<Feed_Like_Reactions> GetReactions(string FeedID, string ObjectID, int LoginFeedChannelID, int WebsiteID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WebsiteID", WebsiteID);
            parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
            parameters.Add("@FeedID", FeedID);
            parameters.Add("@ObjectID", ObjectID);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_lscs",
                Parameters = parameters
            };

            var (_Feed_Like_Reactions, _Feed_Like_Reactions1) = await _dapper.QueryMultipleAsync<Feed_Like_Reactions, Feed_Like_Reactions>(storedProcedureRequest);
            if (_Feed_Like_Reactions1.Any())
            {
                _Feed_Like_Reactions.FirstOrDefault().ExceptFriendList = _Feed_Like_Reactions1.FirstOrDefault().ExceptFriendList;
                _Feed_Like_Reactions.FirstOrDefault().SpecificFriendList = _Feed_Like_Reactions1.FirstOrDefault().SpecificFriendList;
                _Feed_Like_Reactions.FirstOrDefault().CustomListID = _Feed_Like_Reactions1.FirstOrDefault().CustomListID;
            }
            
            _Feed_Like_Reactions.FirstOrDefault().FeedID = FeedID;
            _Feed_Like_Reactions.FirstOrDefault().ObjectID = ObjectID;
            return _Feed_Like_Reactions.FirstOrDefault();
        }

        public async Task<List<Feed_Comments>> GetComments(string FeedID, string CommentID, string CommentCount, string ObjectID, int LoginFeedChannelID,int WebsiteID,int OptioniD)
        {
            List<Feed_Comments> _comments = new List<Feed_Comments>();
            var parameters = new DynamicParameters();
            parameters.Add("@WebsiteID", WebsiteID);
            parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
            parameters.Add("@FeedID", FeedID);
            parameters.Add("@vParentCommentID", CommentID);
            parameters.Add("@ObjectID", ObjectID);
            parameters.Add("@OptioniD", OptioniD);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_Feed_Details",
                Parameters = parameters
            };

            var (_Feed_Comments, _CommentsLike) = await _dapper.QueryMultipleAsync<Feed_Comments, CommentsLike>(storedProcedureRequest);
            _comments = _Feed_Comments.ToList();
            if (_comments.Count > 0)
            {
                int commentcount = 0;
                if (CommentCount != "All")
                {

                    commentcount = _comments.Count - int.Parse(CommentCount);
                    if (commentcount < 0 || commentcount == _comments.Count)
                    {
                        commentcount = 0;
                    }
                }
                for (int i = commentcount; i < _comments.Count; i++)
                {

                    var id = await GetComments(FeedID, _comments[i].CommentID.ToString(), CommentCount, "0", LoginFeedChannelID,WebsiteID,OptioniD);
                    foreach (var item in id)
                    {
                        item._List_Feed_Comments = await GetComments(FeedID, item.CommentID, CommentCount, "0", LoginFeedChannelID, WebsiteID, OptioniD);
                    }
                    _comments[i]._List_Feed_Comments = id;
                    _comments[i]._CommentsLike = GetAllCommentLike(_comments[i].CommentID.ToString(), _CommentsLike.ToList());
                    if (CommentCount != "All")
                    {
                        if (i == int.Parse(CommentCount) - 1)
                        {
                            break;
                        }
                    }

                }
            }
            return _comments;
        }
        public List<CommentsLike> GetAllCommentLike(string CommentID, List<CommentsLike> commentsLike)
        {
            List<CommentsLike> _CommentsLike = new List<CommentsLike>();
            if (commentsLike.Count > 0)
            {
                for (int i = 0; i < commentsLike.Count; i++)
                {
                    if (CommentID == commentsLike[i].CommentID.ToString())
                    {
                        _CommentsLike.Add(new CommentsLike()
                        {
                            CommentID = commentsLike[i].CommentID.ToString(),
                            FeedLikeIcon = commentsLike[i].FeedLikeIcon.ToString(),
                            FeedLikedbyContactID = commentsLike[i].FeedLikedbyContactID.ToString(),
                        });

                    }
                }
            }
            return _CommentsLike;
        }

        public async Task<List<GroupAdminMembers>> GetTagMembers(string FeedID, int WebsiteID, string FeedTagString,int LoginFeedChannelID)
        {
            string proc_name = "glivebooks.dbo.crm_get_FeedTargets";
            var parameters = new DynamicParameters();
            parameters.Add("@WebsiteID", WebsiteID);
            parameters.Add("@FeedID", FeedID);
            if (FeedTagString.Trim() != "")
            {
                parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
                proc_name = "crm_get_feed_taggedPeoplelist";
            }
                
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = proc_name,
                Parameters = parameters
            };

            var _GroupAdminMembers = await _dapper.ExecuteAsync<GroupAdminMembers>(storedProcedureRequest);


            return _GroupAdminMembers.ToList();
        }

        public async Task<List<FeedLikeTypes>> getLikeTypes()
        {
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_FeedLikeTypes"
            };
            var _FeedLikeTypes = await _dapper.ExecuteAsync<FeedLikeTypes>(storedProcedureRequest);            
            return _FeedLikeTypes.ToList();
        }

    }
}
