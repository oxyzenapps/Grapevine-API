//using Azure.Core;
//using Dapper;
//using grapevineCommon.Model.Feed;
//using grapevineData;
//using grapevineData.Interfaces;
//using grapevineRepository.Interfaces;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace grapevineRepository
//{
//    public class FeedRepository : IFeedRepository
//    {
//        private readonly IDapperExecutor _dapper;
//        private StoredProcedureRequest storedProcedureRequest = null;

//        public FeedRepository(IDapperExecutor dapper)
//        {
//            this._dapper = dapper;
//        }

//        public async Task<FeedResponse> GetFeedAsync(FeedRequest request)
//        {
//            var parameters = new DynamicParameters();
//            parameters.Add("@WebsiteID", request.WebsiteID);
//            parameters.Add("@LoginFeedChannelID", request.LoginFeedChannelID);
//            parameters.Add("@FeedChannelID", request.FeedChannelID);
//            parameters.Add("@ExcludeFeedChannelID", request.ExcludeFeedChannelID ? 1 : 0);
//            parameters.Add("@contactFeedChannelID", request.ContactFeedChannelID);
//            parameters.Add("@ExcludeContactID", request.ExcludeContactID ? 1 : 0);
//            parameters.Add("@TaggedOnly", request.TaggedOnly);
//            parameters.Add("@FeedBy", request.FeedBy);
//            parameters.Add("@FeedID", request.FeedID);
//            parameters.Add("@ObjectID", request.ObjectID);
//            parameters.Add("@FeedTypeID", request.FeedTypeID);
//            parameters.Add("@ObjectTypeID", request.ObjectTypeID);
//            parameters.Add("@PageNo", request.PageNo);
//            parameters.Add("@SearchString", request.SearchString);
//            parameters.Add("@FeedChannelParticipantTypeID", request.FeedChannelParticipantTypeID);
//            parameters.Add("@ActivityID", request.ActivityID);
//            parameters.Add("@ListingID", request.ListingID);
//            parameters.Add("@UniversalTransactionID", request.UniversalTransactionID);
//            parameters.Add("@FeedVisibilityID", request.FeedVisibilityID);
//            parameters.Add("@MinViewCount", request.MinViewCount);
//            parameters.Add("@MaxViewCount", request.MaxViewCount);
//            parameters.Add("@Isposted", request.IsPosted);
//            parameters.Add("@DateUpto", request.DateUpto);
//            parameters.Add("@Announcement", request.Announcement);
//            parameters.Add("@LanguageID", request.LanguageID);
//            parameters.Add("@Visible", request.Visible);
//            parameters.Add("@Favourite", request.Favourite);
//            parameters.Add("@ForYou", request.ForYou);
//            parameters.Add("@SortOrder", request.SortOrder);
//            parameters.Add("@Query", request.Query);
//            parameters.Add("@SearchID", request.searchID, direction: ParameterDirection.Output);
//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "glivebooks.dbo.crm_get_Feed",
//                Parameters = parameters
//            };

//            var feeds = await _dapper.ExecuteAsync<FeedItem>(storedProcedureRequest);
//            feeds = await BindFeedDetails(feeds.ToList(),request.LoginFeedChannelID,request.WebsiteID);
//            var searchId = parameters.Get<int>("@SearchID");

//            return new FeedResponse
//            {
//                SearchID = searchId,
//                Feeds = feeds.ToList()
//            };
//        }

//        public async Task<List<FeedItem>> BindFeedDetails(List<FeedItem> feeds,int LoginFeedChannelID, int WebsiteID)
//        {
//            for(int i =0; i< feeds.Count; i++)
//            {
//                feeds[i]._Feed_Like_Reactions = await GetReactions(feeds[i].FeedID, feeds[i].ObjectID, LoginFeedChannelID, WebsiteID);
//                feeds[i]._Feed_Comments = await GetComments(feeds[i].FeedID, "0", "2", "0", LoginFeedChannelID,WebsiteID,2);
//                feeds[i]._GroupMembers = await GetTagMembers(feeds[i].FeedID, WebsiteID, feeds[i].FeedTagString,LoginFeedChannelID);
//                feeds[i]._FeedLikeTypes = await getLikeTypes();
//                //if (feeds[i].SharedFeedID != "0")
//                //{
//                //    var sharedfeed = await GetFeedAsync(new FeedRequest
//                //    {
//                //        WebsiteID = WebsiteID,
//                //        LoginFeedChannelID = LoginFeedChannelID,
//                //        FeedID = feeds[i].SharedFeedID,
//                //    });
//                //    if (sharedfeed.Feeds.Count > 0)
//                //    {
//                //        feeds[i]._SharedFeed = sharedfeed.Feeds.FirstOrDefault();
//                //    }
//                //}
//            }
//            return feeds;
//        }

//        public async Task<Feed_Like_Reactions> GetReactions(string FeedID, string ObjectID, int LoginFeedChannelID, int WebsiteID)
//        {
//            var parameters = new DynamicParameters();
//            parameters.Add("@WebsiteID", WebsiteID);
//            parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
//            parameters.Add("@FeedID", FeedID);
//            parameters.Add("@ObjectID", ObjectID);
//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "glivebooks.dbo.crm_get_feed_lscs",
//                Parameters = parameters
//            };

//            var (_Feed_Like_Reactions, _Feed_Like_Reactions1) = await _dapper.QueryMultipleAsync<Feed_Like_Reactions, Feed_Like_Reactions>(storedProcedureRequest);
//            if (_Feed_Like_Reactions1.Any())
//            {
//                _Feed_Like_Reactions.FirstOrDefault().ExceptFriendList = _Feed_Like_Reactions1.FirstOrDefault().ExceptFriendList;
//                _Feed_Like_Reactions.FirstOrDefault().SpecificFriendList = _Feed_Like_Reactions1.FirstOrDefault().SpecificFriendList;
//                _Feed_Like_Reactions.FirstOrDefault().CustomListID = _Feed_Like_Reactions1.FirstOrDefault().CustomListID;
//            }

//            _Feed_Like_Reactions.FirstOrDefault().FeedID = FeedID;
//            _Feed_Like_Reactions.FirstOrDefault().ObjectID = ObjectID;
//            return _Feed_Like_Reactions.FirstOrDefault();
//        }

//        public async Task<List<Feed_Comments>> GetComments(string FeedID, string CommentID, string CommentCount, string ObjectID, int LoginFeedChannelID,int WebsiteID,int OptioniD)
//        {
//            List<Feed_Comments> _comments = new List<Feed_Comments>();
//            var parameters = new DynamicParameters();
//            parameters.Add("@WebsiteID", WebsiteID);
//            parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
//            parameters.Add("@FeedID", FeedID);
//            parameters.Add("@vParentCommentID", CommentID);
//            parameters.Add("@ObjectID", ObjectID);
//            parameters.Add("@OptioniD", OptioniD);
//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "glivebooks.dbo.crm_get_Feed_Details",
//                Parameters = parameters
//            };

//            var (_Feed_Comments, _CommentsLike) = await _dapper.QueryMultipleAsync<Feed_Comments, CommentsLike>(storedProcedureRequest);
//            _comments = _Feed_Comments.ToList();
//            if (_comments.Count > 0)
//            {
//                int commentcount = 0;
//                if (CommentCount != "All")
//                {

//                    commentcount = _comments.Count - int.Parse(CommentCount);
//                    if (commentcount < 0 || commentcount == _comments.Count)
//                    {
//                        commentcount = 0;
//                    }
//                }
//                for (int i = commentcount; i < _comments.Count; i++)
//                {

//                    var id = await GetComments(FeedID, _comments[i].CommentID.ToString(), CommentCount, "0", LoginFeedChannelID,WebsiteID,OptioniD);
//                    foreach (var item in id)
//                    {
//                        item._List_Feed_Comments = await GetComments(FeedID, item.CommentID, CommentCount, "0", LoginFeedChannelID, WebsiteID, OptioniD);
//                    }
//                    _comments[i]._List_Feed_Comments = id;
//                    _comments[i]._CommentsLike = GetAllCommentLike(_comments[i].CommentID.ToString(), _CommentsLike.ToList());
//                    if (CommentCount != "All")
//                    {
//                        if (i == int.Parse(CommentCount) - 1)
//                        {
//                            break;
//                        }
//                    }

//                }
//            }
//            return _comments;
//        }
//        public List<CommentsLike> GetAllCommentLike(string CommentID, List<CommentsLike> commentsLike)
//        {
//            List<CommentsLike> _CommentsLike = new List<CommentsLike>();
//            if (commentsLike.Count > 0)
//            {
//                for (int i = 0; i < commentsLike.Count; i++)
//                {
//                    if (CommentID == commentsLike[i].CommentID.ToString())
//                    {
//                        _CommentsLike.Add(new CommentsLike()
//                        {
//                            CommentID = commentsLike[i].CommentID.ToString(),
//                            FeedLikeIcon = commentsLike[i].FeedLikeIcon.ToString(),
//                            FeedLikedbyContactID = commentsLike[i].FeedLikedbyContactID.ToString(),
//                        });

//                    }
//                }
//            }
//            return _CommentsLike;
//        }

//        public async Task<List<GroupAdminMembers>> GetTagMembers(string FeedID, int WebsiteID, string FeedTagString,int LoginFeedChannelID)
//        {
//            string proc_name = "glivebooks.dbo.crm_get_FeedTargets";
//            var parameters = new DynamicParameters();
//            parameters.Add("@WebsiteID", WebsiteID);
//            parameters.Add("@FeedID", FeedID);
//            if (FeedTagString.Trim() != "")
//            {
//                parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
//                proc_name = "crm_get_feed_taggedPeoplelist";
//            }

//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = proc_name,
//                Parameters = parameters
//            };

//            var _GroupAdminMembers = await _dapper.ExecuteAsync<GroupAdminMembers>(storedProcedureRequest);


//            return _GroupAdminMembers.ToList();
//        }

//        public async Task<List<FeedLikeTypes>> getLikeTypes()
//        {
//            storedProcedureRequest = new StoredProcedureRequest
//            {
//                ProcedureName = "glivebooks.dbo.crm_FeedLikeTypes"
//            };
//            var _FeedLikeTypes = await _dapper.ExecuteAsync<FeedLikeTypes>(storedProcedureRequest);            
//            return _FeedLikeTypes.ToList();
//        }

//    }
//}

using Azure.Core;
using Dapper;
using grapevineCommon.Model.Feed;
using grapevineData;
using grapevineData.Interfaces;
using grapevineRepository.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace grapevineRepository
{
    public class FeedRepository : IFeedRepository
    {
        private readonly IDapperExecutor _dapper;

        public FeedRepository(IDapperExecutor dapper)
        {
            _dapper = dapper;
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
            parameters.Add("@SearchID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_Feed",
                Parameters = parameters
            };

            var feeds = await _dapper.ExecuteAsync<FeedItem>(storedProcedureRequest);
            var feedList = feeds.ToList();

            await BindFeedDetails(feedList, request.LoginFeedChannelID, request.WebsiteID);

            var searchId = parameters.Get<int>("@SearchID");

            return new FeedResponse
            {
                SearchID = searchId,
                Feeds = feedList
            };
        }

        public async Task BindFeedDetails(List<FeedItem> feeds, int loginFeedChannelID, int websiteID)
        {
            // Optimization: Get LikeTypes once rather than inside the loop
            var likeTypes = await GetLikeTypes();

            foreach (var feed in feeds)
            {
<<<<<<< HEAD
                feed._Feed_Like_Reactions = await GetReactions(feed.FeedID, feed.ObjectID, loginFeedChannelID, websiteID);
                feed._Feed_Comments = await GetComments(feed.FeedID, "0", "2", "0", loginFeedChannelID, websiteID, 2);
                feed._GroupMembers = await GetTagMembers(feed.FeedID, websiteID, feed.FeedTagString, loginFeedChannelID);
                feed._FeedLikeTypes = likeTypes;
=======
                feeds[i]._Feed_Like_Reactions = await GetReactions(feeds[i].FeedID, feeds[i].ObjectID, LoginFeedChannelID, WebsiteID);
                feeds[i]._Feed_Comments = await GetComments(feeds[i].FeedID, "0", "2", "0", LoginFeedChannelID,WebsiteID,2);
                feeds[i]._GroupMembers = await GetTagMembers(feeds[i].FeedID, WebsiteID, feeds[i].FeedTagString,LoginFeedChannelID);
                feeds[i]._FeedLikeTypes = await getLikeTypes();
                if (feeds[i].SharedFeedID != "0")
                {
                    feeds[i].SharedFeed = await GetSharedData(feeds[i].FeedID, LoginFeedChannelID, WebsiteID);
                }
                if (feeds[i].FeedObjectType == "JSON")
                {
                    feeds[i].FeedText_Json = GetData(feeds[i].FeedText);
                }
                else if (feeds[i].MediaShow)
                {
                    feeds[i]._FeedObjectFilePath = await Get_Feed_FileObjectPath(WebsiteID, feeds[i].FeedID, feeds[i].ObjectID, "0", "0", LoginFeedChannelID);
                }
                else if (feeds[i].FeedObjectType == "Location")
                {
                    feeds[i]._crm_FeedLocation = await GetFeedLocation(feeds[i].FeedID);
                }
                else if (feeds[i].FeedObjectType == "Webshare")
                {
                    feeds[i]._SharedLinkRecord = await GetLinkShareRecord(WebsiteID, feeds[i].FeedID, "0");
                }
                else if (feeds[i].FeedObjectType == "Poll"
                            || feeds[i].FeedObjectType == "Appointments"
                            || feeds[i].FeedObjectType == "Get Together"
                            || feeds[i].FeedObjectType == "Party"
                            || feeds[i].FeedObjectType == "Watch Party"
                            || feeds[i].FeedObjectType == "Event"
                            || feeds[i].FeedObjectType == "Live Meeting"
                            || feeds[i].FeedObjectType == "Live Video Broadcast"
                            || feeds[i].FeedObjectType == "Live Audio Broadcast"
                            || feeds[i].FeedObjectType == "Other Online Events"
                            )
                {
                    string ActivityTypeID = "4";
                    if (feeds[i].FeedObjectType == "Event")
                    {
                        ActivityTypeID = "2";
                    }
                    else if (feeds[i].FeedObjectType == "Other Online Events")
                    {
                        ActivityTypeID = "15";
                    }
                    else if (feeds[i].FeedObjectType == "Live Audio Broadcast")
                    {
                        ActivityTypeID = "10";
                    }
                    else if (feeds[i].FeedObjectType == "Live Video Broadcast")
                    {
                        ActivityTypeID = "11";
                    }
                    else if (feeds[i].FeedObjectType == "Poll")
                    {
                        ActivityTypeID = "1";
                    }
                    else if (feeds[i].FeedObjectType == "Appointments")
                    {
                        ActivityTypeID = "5";
                    }
                    else if (feeds[i].FeedObjectType == "Get Together")
                    {
                        ActivityTypeID = "6";
                    }
                    else if (feeds[i].FeedObjectType == "Party")
                    {
                        ActivityTypeID = "7";
                    }
                    else if (feeds[i].FeedObjectType == "Watch Party")
                    {
                        ActivityTypeID = "8";
                    }
                    feeds[i]._ActivityPollDetails = await crm_get_feed_activities(feeds[i].FeedChannelID, feeds[i].FeedID, ActivityTypeID, "0",LoginFeedChannelID);
                }
                else if (feeds[i].FeedObjectType == "URL")
                {
                    string[] FileName = feeds[i].FeedText.Split('/');
                    string value = FileName[FileName.Length - 1];
                    feeds[i].FileName = value;
                }
                else if (feeds[i].FeedObjectType == "Jobs"
                    || feeds[i].FeedObjectType == "Offers"
                    || feeds[i].FeedObjectType == "Shop"
                    || feeds[i].FeedObjectType == "Dating" 
                    || feeds[i].FeedObjectType == "Real Estate")
                {
                    feeds[i]._BindJobListingValueUnit = await BindJobListing("0", feeds[i].FeedID, LoginFeedChannelID, 0, 0, 0, 0, 0, 0);
                }
>>>>>>> e726883805c84feba0eeba7af247fb27cd7ed66e
            }
        }

        public async Task<Feed_Like_Reactions> GetReactions(string feedID, string objectID, int loginFeedChannelID, int websiteID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WebsiteID", websiteID);
            parameters.Add("@LoginFeedChannelID", loginFeedChannelID);
            parameters.Add("@FeedID", feedID);
            parameters.Add("@ObjectID", objectID);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_lscs",
                Parameters = parameters
            };

            // FIX: Added explicit casting to the deconstruction
            var result = await _dapper.QueryMultipleAsync<Feed_Like_Reactions, Feed_Like_Reactions>(request);
            var reactions = (IEnumerable<Feed_Like_Reactions>)result.Item1;
            var reactionsExtra = (IEnumerable<Feed_Like_Reactions>)result.Item2;

            var primaryReaction = reactions.FirstOrDefault();
            var extraReaction = reactionsExtra.FirstOrDefault();

            if (primaryReaction != null && extraReaction != null)
            {
                primaryReaction.ExceptFriendList = extraReaction.ExceptFriendList;
                primaryReaction.SpecificFriendList = extraReaction.SpecificFriendList;
                primaryReaction.CustomListID = extraReaction.CustomListID;
            }

            if (primaryReaction != null)
            {
                primaryReaction.FeedID = feedID;
                primaryReaction.ObjectID = objectID;
            }

            return primaryReaction;
        }

        public async Task<List<Feed_Comments>> GetComments(string feedID, string commentID, string commentCount, string objectID, int loginFeedChannelID, int websiteID, int optionID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WebsiteID", websiteID);
            parameters.Add("@LoginFeedChannelID", loginFeedChannelID);
            parameters.Add("@FeedID", feedID);
            parameters.Add("@vParentCommentID", commentID);
            parameters.Add("@ObjectID", objectID);
            parameters.Add("@OptioniD", optionID);

            var request = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_Feed_Details",
                Parameters = parameters
            };

            // FIX: Explicit casting for Comments deconstruction
            var result = await _dapper.QueryMultipleAsync<Feed_Comments, CommentsLike>(request);
            var commentsData = (IEnumerable<Feed_Comments>)result.Item1;
            var likesData = (IEnumerable<CommentsLike>)result.Item2;

            var commentsList = commentsData.ToList();
            var likesList = likesData.ToList();

            if (commentsList.Count > 0)
            {
                int limit = commentsList.Count;
                if (commentCount != "All" && int.TryParse(commentCount, out int countVal))
                {
                    if (countVal > 0 && countVal < commentsList.Count) limit = countVal;
                }

                for (int i = 0; i < limit; i++)
                {
                    // Note: Be careful with recursion depth in production
                    var subComments = await GetComments(feedID, commentsList[i].CommentID.ToString(), commentCount, "0", loginFeedChannelID, websiteID, optionID);

                    foreach (var sub in subComments)
                    {
                        sub._List_Feed_Comments = await GetComments(feedID, sub.CommentID.ToString(), commentCount, "0", loginFeedChannelID, websiteID, optionID);
                    }

                    commentsList[i]._List_Feed_Comments = subComments;
                    commentsList[i]._CommentsLike = GetAllCommentLike(commentsList[i].CommentID.ToString(), likesList);
                }
            }
            return commentsList;
        }

        public List<CommentsLike> GetAllCommentLike(string commentID, List<CommentsLike> commentsLike)
        {
            return commentsLike.Where(x => x.CommentID.ToString() == commentID).ToList();
        }

        public async Task<List<GroupAdminMembers>> GetTagMembers(string feedID, int websiteID, string feedTagString, int loginFeedChannelID)
        {
            string procName = "glivebooks.dbo.crm_get_FeedTargets";
            var parameters = new DynamicParameters();
            parameters.Add("@WebsiteID", websiteID);
            parameters.Add("@FeedID", feedID);

            if (!string.IsNullOrWhiteSpace(feedTagString))
            {
<<<<<<< HEAD
                parameters.Add("@LoginFeedChannelID", loginFeedChannelID);
                procName = "glivebooks.dbo.crm_get_feed_taggedPeoplelist"; // Added schema for consistency
=======
                parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
                proc_name = "glivebooks.dbo.crm_get_feed_taggedPeoplelist";
>>>>>>> e726883805c84feba0eeba7af247fb27cd7ed66e
            }

            var request = new StoredProcedureRequest
            {
                ProcedureName = procName,
                Parameters = parameters
            };

            var members = await _dapper.ExecuteAsync<GroupAdminMembers>(request);
            return members.ToList();
        }

        public async Task<List<FeedLikeTypes>> GetLikeTypes()
        {
<<<<<<< HEAD
            var request = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_FeedLikeTypes"
            };
            var types = await _dapper.ExecuteAsync<FeedLikeTypes>(request);
            return types.ToList();
        }
=======
            var _FeedLikeTypes = await _dapper.ExecuteTableFunctionAsync<FeedLikeTypes>("glivebooks.dbo.crm_FeedLikeTypes");            
            return _FeedLikeTypes.ToList();
        }

        public async Task<FeedItem> GetSharedData(string FeedID,int LoginFeedChannelID,int WebsiteID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
            parameters.Add("@FeedID", FeedID);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_Feed_Details",
                Parameters = parameters
            };
            var _FeedItem = await _dapper.ExecuteAsync<FeedItem>(storedProcedureRequest);
            var singleFeed = _FeedItem.FirstOrDefault();
            if (_FeedItem.Any())
            {
                singleFeed._Feed_Like_Reactions = await GetReactions(singleFeed.FeedID, singleFeed.ObjectID, LoginFeedChannelID, WebsiteID);
                singleFeed._Feed_Comments = await GetComments(singleFeed.FeedID, "0", "2", "0", LoginFeedChannelID, WebsiteID, 2);
                singleFeed._GroupMembers = await GetTagMembers(singleFeed.FeedID, WebsiteID, singleFeed.FeedTagString, LoginFeedChannelID);
                singleFeed._FeedLikeTypes = await getLikeTypes();
                if (singleFeed.SharedFeedID != "0")
                {
                    singleFeed.SharedFeed = await GetSharedData(singleFeed.FeedID, LoginFeedChannelID, WebsiteID);
                }
                if (singleFeed.FeedObjectType == "JSON")
                {
                    singleFeed.FeedText_Json = GetData(singleFeed.FeedText);
                }
                else if (singleFeed.MediaShow)
                {
                    singleFeed._FeedObjectFilePath = await Get_Feed_FileObjectPath(WebsiteID, singleFeed.FeedID, singleFeed.ObjectID, "0", "0", LoginFeedChannelID);
                }
                else if (singleFeed.FeedObjectType == "Location")
                {
                    singleFeed._crm_FeedLocation = await GetFeedLocation(singleFeed.FeedID);
                }
                else if (singleFeed.FeedObjectType == "Webshare")
                {
                    singleFeed._SharedLinkRecord = await GetLinkShareRecord(WebsiteID, singleFeed.FeedID, "0");
                }
                else if (singleFeed.FeedObjectType == "Poll"
                            || singleFeed.FeedObjectType == "Appointments"
                            || singleFeed.FeedObjectType == "Get Together"
                            || singleFeed.FeedObjectType == "Party"
                            || singleFeed.FeedObjectType == "Watch Party"
                            || singleFeed.FeedObjectType == "Event"
                            || singleFeed.FeedObjectType == "Live Meeting"
                            || singleFeed.FeedObjectType == "Live Video Broadcast"
                            || singleFeed.FeedObjectType == "Live Audio Broadcast"
                            || singleFeed.FeedObjectType == "Other Online Events"
                            )
                {
                    string ActivityTypeID = "4";
                    if (singleFeed.FeedObjectType == "Event")
                    {
                        ActivityTypeID = "2";
                    }
                    else if (singleFeed.FeedObjectType == "Other Online Events")
                    {
                        ActivityTypeID = "15";
                    }
                    else if (singleFeed.FeedObjectType == "Live Audio Broadcast")
                    {
                        ActivityTypeID = "10";
                    }
                    else if (singleFeed.FeedObjectType == "Live Video Broadcast")
                    {
                        ActivityTypeID = "11";
                    }
                    else if (singleFeed.FeedObjectType == "Poll")
                    {
                        ActivityTypeID = "1";
                    }
                    else if (singleFeed.FeedObjectType == "Appointments")
                    {
                        ActivityTypeID = "5";
                    }
                    else if (singleFeed.FeedObjectType == "Get Together")
                    {
                        ActivityTypeID = "6";
                    }
                    else if (singleFeed.FeedObjectType == "Party")
                    {
                        ActivityTypeID = "7";
                    }
                    else if (singleFeed.FeedObjectType == "Watch Party")
                    {
                        ActivityTypeID = "8";
                    }
                    singleFeed._ActivityPollDetails = await crm_get_feed_activities(singleFeed.FeedChannelID, singleFeed.FeedID, ActivityTypeID, "0", LoginFeedChannelID);
                }
                else if (singleFeed.FeedObjectType == "URL")
                {
                    string[] FileName = singleFeed.FeedText.Split('/');
                    string value = FileName[FileName.Length - 1];
                    singleFeed.FileName = value;
                }
                else if (singleFeed.FeedObjectType == "Jobs"
                    || singleFeed.FeedObjectType == "Offers"
                    || singleFeed.FeedObjectType == "Shop"
                    || singleFeed.FeedObjectType == "Dating"
                    || singleFeed.FeedObjectType == "Real Estate")
                {
                    singleFeed._BindJobListingValueUnit = await BindJobListing("0", singleFeed.FeedID, LoginFeedChannelID, 0, 0, 0, 0, 0, 0);
                }
            }
            return _FeedItem.FirstOrDefault();
        }

        public EventDetail GetData(string Data)
        {
            EventDetail _EventDetail = new EventDetail();
            List<Listkeys> _Keys = new List<Listkeys>();
            List<ListValues> _ListValues = new List<ListValues>();
            string[] value = Data.Split('[');
            string[] value1;
            string[] value2;

            if (value.Length > 0)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] != "")
                    {
                        value1 = value[i].Replace("{", "").Replace("}", "").Replace("]", "").Replace("\",\"", "\"!\"").Replace("\"", "").Replace("\\/", " ").Replace("?", "₹ ").Split('!');
                        if (value1.Length > 0)
                        {
                            for (int j = 0; j < value1.Length; j++)
                            {
                                value2 = value1[j].Split(':');
                                if (value2.Length > 0)
                                {
                                    for (int k = 0; k < value2.Length; k++)
                                    {
                                        if (k % 2 == 0)
                                        {
                                            _Keys.Add(new Listkeys()
                                            {
                                                Keys = value2[k],
                                            });
                                        }
                                        else
                                        {
                                            _ListValues.Add(new ListValues()
                                            {
                                                values = value2[k],
                                            });
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }


            _EventDetail._ListKeys = _Keys;
            _EventDetail._ListValues = _ListValues;
            return _EventDetail;

        }

        public async Task<List<Feed_ObjectFilePath>> Get_Feed_FileObjectPath(int WebsiteID, string FeedID, string ObjectID, string ActivityID , string FeedObjectTypeID ,int LoginFeedChannelID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
            parameters.Add("@FeedID", FeedID);
            parameters.Add("@WebsiteID", WebsiteID);
            parameters.Add("@ObjectID", ObjectID);
            parameters.Add("@ActivityID", ActivityID);
            parameters.Add("@FeedObjectTypeID", FeedObjectTypeID);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_objects",
                Parameters = parameters
            };
            var _Feed_ObjectFilePath = await _dapper.ExecuteAsync<Feed_ObjectFilePath>(storedProcedureRequest);
            var _FeedObject = _Feed_ObjectFilePath.ToList();
            if (_Feed_ObjectFilePath.Any())
            {
                for(int i=0;i<_FeedObject.Count;i++)
                {
                    _FeedObject[i]._PhotosLikes = await GetLikesPhoto(FeedID, _FeedObject[i].ObjectID,LoginFeedChannelID,1,"",WebsiteID);
                }
            }
            return _FeedObject;
        }

        public async Task<List<Feed_Likes>> GetLikesPhoto(string FeedID, string ObjectID,int LoginFeedChannelID,int OptioniD,string vParentCommentID,int WebsiteID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginID", "");
            parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
            parameters.Add("@FeedID", FeedID);
            parameters.Add("@WebsiteID", WebsiteID);
            parameters.Add("@ObjectID", ObjectID);
            parameters.Add("@vParentCommentID", vParentCommentID);
            parameters.Add("@OptioniD", OptioniD);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_objects",
                Parameters = parameters
            };
            var _Feed_Likes = await _dapper.ExecuteAsync<Feed_Likes>(storedProcedureRequest);
            
            return _Feed_Likes.ToList();
        }

        public async Task<crm_FeedLocation> GetFeedLocation(string FeedID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FeedID", FeedID);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_location",
                Parameters = parameters
            };

            var _crm_FeedLocation = await _dapper.ExecuteAsync<crm_FeedLocation>(storedProcedureRequest);
            
            return _crm_FeedLocation.FirstOrDefault();
        }

        public async Task<SharedLinkRecord> GetLinkShareRecord(int WebsiteID,string FeedID, string ShowType)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WebsiteID", WebsiteID);
            parameters.Add("@FeedID", FeedID);
            parameters.Add("@HistoryFlag", ShowType);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_Feed_LinkShare",
                Parameters = parameters
            };
            var results = await _dapper.QueryMultipleDynamicAsync(storedProcedureRequest);

            SharedLinkRecord _SharedLinkRecord = new SharedLinkRecord();
            _SharedLinkRecord = results[0].FirstOrDefault();
            _SharedLinkRecord._List_SharedLinkRecord = results[1].FirstOrDefault();
            WebShare_Info _WebShare_Info = new WebShare_Info();
            if (ShowType == "1")
            {
                _WebShare_Info.List = GetFriendsImages(results[2].FirstOrDefault().List);
                _WebShare_Info.ShareText = results[2].FirstOrDefault().ShareText;
                _SharedLinkRecord._WebShare_Info = _WebShare_Info;
            }
            return _SharedLinkRecord;
        }
        public List<FriendsImages> GetFriendsImages(string Images)
        {

            List<FriendsImages> _FriendsImages = new List<FriendsImages>();
            if (Images != "")
            {
                string[] images = Images.Split(',');
                for (int i = 0; i < images.Length; i++)
                {
                    if (images[i].Split('!').Length > 1)
                    {
                        string link = images[i].Split('!')[0];
                        string name = images[i].Split('!')[1];

                        _FriendsImages.Add(new FriendsImages()
                        {
                            Images = link,
                            Name = name,
                        });

                    }
                    else
                    {


                        _FriendsImages.Add(new FriendsImages()
                        {
                            Images = images[i],
                            //Name = name,
                        });
                    }

                }
            }

            return _FriendsImages;
        }

        public async Task<ActivityPollDetails> crm_get_feed_activities(string FeedChannelID, string FeedID, string ActivityTypeID, string OptionID,int LoginFeedChannelID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
            parameters.Add("@FeedID", FeedID);
            parameters.Add("@FeedChannelID", FeedChannelID);
            parameters.Add("@ActivityTypeID", ActivityTypeID);
            parameters.Add("@OptionID", OptionID);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_activities",
                Parameters = parameters
            };
            var result = await _dapper.QueryMultipleDynamicAsync(storedProcedureRequest);
            var _activityPollDetails = result[0];
            var _options = result[1].ToList();
            var _EventCo_Hosts = result[2];
            var _activityObj = _activityPollDetails.FirstOrDefault();
            if (_activityPollDetails.Any())
            {
                _activityObj._EventCo_Hosts = _EventCo_Hosts.ToList().FindAll(x=> x.ActivityID == _activityObj.ActivityID);
                _activityObj._PollOptions = _options.ToList().FindAll(x => x.ActivityID == _activityObj.ActivityID);
                if(_activityObj._PollOptions.Any())
                {
                    foreach (var option in _activityObj._PollOptions)
                    {
                        option._PollOptionReaction = await GetAllPollOptionReaction(option.FeedChannelID, FeedID, ActivityTypeID, option.OptionID,LoginFeedChannelID);
                    }
                }
                
            }
            return _activityObj;
        }

        public async Task<List<PollOptionReaction>> GetAllPollOptionReaction(string FeedChannelID, string FeedID, string ActivityTypeID, string OptionID,int LoginFeedChannelID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
            parameters.Add("@FeedID", FeedID);
            parameters.Add("@FeedChannelID", FeedChannelID);
            parameters.Add("@ActivityTypeID", ActivityTypeID);
            parameters.Add("@OptionID", OptionID);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_activities",
                Parameters = parameters
            };
            var result =await _dapper.ExecuteAsync<PollOptionReaction>(storedProcedureRequest);

            return result.ToList();

        }

        public async Task<List<BindJobListingValueUnit>> BindJobListing(string FeedChannelID, string FeedID,int LoginFeedChannelID,int ListingID,int ListingCategoryID,
                                                        int ListingTopicID,int ListingContractTypeID,int RadiusKm,int Option)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", LoginFeedChannelID);
            parameters.Add("@FeedChannelID", FeedChannelID);
            parameters.Add("@FeedID", FeedID);
            parameters.Add("@ListingID", ListingID);
            parameters.Add("@ListingCategoryID", ListingCategoryID);
            parameters.Add("@ListingTopicID", ListingTopicID);
            parameters.Add("@ListingContractTypeID", ListingContractTypeID);
            parameters.Add("@RadiusKm", RadiusKm);
            parameters.Add("@Option", Option);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_lists",
                Parameters = parameters
            };
            var result = await _dapper.QueryMultipleDynamicAsync(storedProcedureRequest);
            var _BindJobListingValueUnit = result[0].Select(x => (BindJobListingValueUnit)x).ToList();
            var _BindJobFeedIDTable1 = result[1].Select(x => (BindJobFeedIDTable1)x).ToList();
            var _BindJobFeedIDTable2 = result[2].Select(x => (BindJobFeedIDTable2)x).ToList();
            var _BindJobFeedIDLocation = result[3].Select(x => (BindJobFeedIDLocation)x).ToList();
            var _BindJobFeedIDTable4 = result[5].Select(x => (BindJobFeedIDTable4)x).ToList();

            for(int i=0;i< _BindJobListingValueUnit.Count; i++)
            {
                _BindJobListingValueUnit[i]._BindJobFeedIDLocation = _BindJobFeedIDLocation.FindAll(x => x.ListingID == _BindJobListingValueUnit[i].ListingID).FirstOrDefault();
                _BindJobListingValueUnit[i]._BindJobFeedIDTable2 = _BindJobFeedIDTable2.FindAll(x => x.ListingID == _BindJobListingValueUnit[i].ListingID).FirstOrDefault();
                _BindJobListingValueUnit[i]._BindJobFeedIDTable1 = _BindJobFeedIDTable1.FindAll(x => x.ListingID == _BindJobListingValueUnit[i].ListingID).FirstOrDefault();
                _BindJobListingValueUnit[i]._BindJobFeedIDTable4 = _BindJobFeedIDTable4.FindAll(x => x.ListingID == _BindJobListingValueUnit[i].ListingID).FirstOrDefault();
            }

            return _BindJobListingValueUnit;
        }
>>>>>>> e726883805c84feba0eeba7af247fb27cd7ed66e
    }
}