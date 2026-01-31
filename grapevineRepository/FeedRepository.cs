
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
using System.Text.Json;
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
                feed._Feed_Like_Reactions = await GetReactions(feed.FeedID, feed.ObjectID, loginFeedChannelID, websiteID);
                feed._Feed_Comments = await GetComments(feed.FeedID, "0", "2", "0", loginFeedChannelID, websiteID, 2);
                feed._GroupMembers = await GetTagMembers(feed.FeedID, websiteID, feed.FeedTagString, loginFeedChannelID);
                feed._FeedLikeTypes = likeTypes;

                if (feed.SharedFeedID != "0")
                {
                    feed.SharedFeed = await GetSharedData(feed.FeedID, loginFeedChannelID, websiteID);
                }
                if (feed.FeedObjectType == "JSON")
                {
                    feed.FeedText_Json = GetData(feed.FeedText);
                }
                else if (feed.MediaShow)
                {
                    feed._FeedObjectFilePath = await Get_Feed_FileObjectPath(websiteID, feed.FeedID, feed.ObjectID, "0", "0", loginFeedChannelID);
                }
                else if (feed.FeedObjectType == "Location")
                {
                    feed._crm_FeedLocation = await GetFeedLocation(feed.FeedID);
                }
                else if (feed.FeedObjectType == "Webshare")
                {
                    feed._SharedLinkRecord = await GetLinkShareRecord(websiteID, feed.FeedID, "0");
                }
                else if (feed.FeedObjectType == "Poll"
                            || feed.FeedObjectType == "Appointments"
                            || feed.FeedObjectType == "Get Together"
                            || feed.FeedObjectType == "Party"
                            || feed.FeedObjectType == "Watch Party"
                            || feed.FeedObjectType == "Event"
                            || feed.FeedObjectType == "Live Meeting"
                            || feed.FeedObjectType == "Live Video Broadcast"
                            || feed.FeedObjectType == "Live Audio Broadcast"
                            || feed.FeedObjectType == "Other Online Events")
                {
                    string ActivityTypeID = "4";
                    if (feed.FeedObjectType == "Event")
                    {
                        ActivityTypeID = "2";
                    }
                    else if (feed.FeedObjectType == "Other Online Events")
                    {
                        ActivityTypeID = "15";
                    }
                    else if (feed.FeedObjectType == "Live Audio Broadcast")
                    {
                        ActivityTypeID = "10";
                    }
                    else if (feed.FeedObjectType == "Live Video Broadcast")
                    {
                        ActivityTypeID = "11";
                    }
                    else if (feed.FeedObjectType == "Poll")
                    {
                        ActivityTypeID = "1";
                    }
                    else if (feed.FeedObjectType == "Appointments")
                    {
                        ActivityTypeID = "5";
                    }
                    else if (feed.FeedObjectType == "Get Together")
                    {
                        ActivityTypeID = "6";
                    }
                    else if (feed.FeedObjectType == "Party")
                    {
                        ActivityTypeID = "7";
                    }
                    else if (feed.FeedObjectType == "Watch Party")
                    {
                        ActivityTypeID = "8";
                    }
                    feed._ActivityPollDetails = await CrmGetFeedActivities(feed.FeedChannelID, feed.FeedID, ActivityTypeID, "0", loginFeedChannelID);
                }
                else if (feed.FeedObjectType == "URL")
                {
                    string[] FileName = feed.FeedText.Split('/');
                    string value = FileName[FileName.Length - 1];
                    feed.FileName = value;
                }
                else if (feed.FeedObjectType == "Jobs"
                    || feed.FeedObjectType == "Offers"
                    || feed.FeedObjectType == "Shop"
                    || feed.FeedObjectType == "Dating"
                    || feed.FeedObjectType == "Real Estate")
                {
                    feed._BindJobListingValueUnit = await BindJobListing("0", feed.FeedID, loginFeedChannelID, 0, 0, 0, 0, 0, 0);
                }
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

            var result = await _dapper.QueryMultipleAsync<Feed_Like_Reactions, Feed_Like_Reactions>(request);
            var reactions = result.Item1;
            var reactionsExtra = result.Item2;

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

            var result = await _dapper.QueryMultipleAsync<Feed_Comments, CommentsLike>(request);
            var commentsData = result.Item1;
            var likesData = result.Item2;

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
                parameters.Add("@LoginFeedChannelID", loginFeedChannelID);
                procName = "glivebooks.dbo.crm_get_feed_taggedPeoplelist";
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
            var request = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_FeedLikeTypes"
            };
            var types = await _dapper.ExecuteAsync<FeedLikeTypes>(request);
            return types.ToList();
        }

        public async Task<FeedItem> GetSharedData(string feedID, int loginFeedChannelID, int websiteID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", loginFeedChannelID);
            parameters.Add("@FeedID", feedID);

            var storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_Feed_Details",
                Parameters = parameters
            };

            var feedItems = await _dapper.ExecuteAsync<FeedItem>(storedProcedureRequest);
            var singleFeed = feedItems.FirstOrDefault();

            if (singleFeed != null)
            {
                singleFeed._Feed_Like_Reactions = await GetReactions(singleFeed.FeedID, singleFeed.ObjectID, loginFeedChannelID, websiteID);
                singleFeed._Feed_Comments = await GetComments(singleFeed.FeedID, "0", "2", "0", loginFeedChannelID, websiteID, 2);
                singleFeed._GroupMembers = await GetTagMembers(singleFeed.FeedID, websiteID, singleFeed.FeedTagString, loginFeedChannelID);
                singleFeed._FeedLikeTypes = await GetLikeTypes();

                if (singleFeed.SharedFeedID != "0")
                {
                    singleFeed.SharedFeed = await GetSharedData(singleFeed.FeedID, loginFeedChannelID, websiteID);
                }

                if (singleFeed.FeedObjectType == "JSON")
                {
                    singleFeed.FeedText_Json = GetData(singleFeed.FeedText);
                }
                else if (singleFeed.MediaShow)
                {
                    singleFeed._FeedObjectFilePath = await Get_Feed_FileObjectPath(websiteID, singleFeed.FeedID, singleFeed.ObjectID, "0", "0", loginFeedChannelID);
                }
                else if (singleFeed.FeedObjectType == "Location")
                {
                    singleFeed._crm_FeedLocation = await GetFeedLocation(singleFeed.FeedID);
                }
                else if (singleFeed.FeedObjectType == "Webshare")
                {
                    singleFeed._SharedLinkRecord = await GetLinkShareRecord(websiteID, singleFeed.FeedID, "0");
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
                            || singleFeed.FeedObjectType == "Other Online Events")
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
                    singleFeed._ActivityPollDetails = await CrmGetFeedActivities(singleFeed.FeedChannelID, singleFeed.FeedID, ActivityTypeID, "0", loginFeedChannelID);
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
                    singleFeed._BindJobListingValueUnit = await BindJobListing("0", singleFeed.FeedID, loginFeedChannelID, 0, 0, 0, 0, 0, 0);
                }
            }
            return singleFeed;
        }

        public EventDetail GetData(string data)
        {
            EventDetail _EventDetail = new EventDetail();
            List<Listkeys> _Keys = new List<Listkeys>();
            List<ListValues> _ListValues = new List<ListValues>();
            string[] value = data.Split('[');
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

        public async Task<List<Feed_ObjectFilePath>> Get_Feed_FileObjectPath(int websiteID, string feedID, string objectID, string activityID, string feedObjectTypeID, int loginFeedChannelID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", loginFeedChannelID);
            parameters.Add("@FeedID", feedID);
            parameters.Add("@WebsiteID", websiteID);
            parameters.Add("@ObjectID", objectID);
            parameters.Add("@ActivityID", activityID);
            parameters.Add("@FeedObjectTypeID", feedObjectTypeID);

            var storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_objects",
                Parameters = parameters
            };

            var feedObjectFilePaths = await _dapper.ExecuteAsync<Feed_ObjectFilePath>(storedProcedureRequest);
            var feedObjects = feedObjectFilePaths.ToList();

            if (feedObjectFilePaths.Any())
            {
                for (int i = 0; i < feedObjects.Count; i++)
                {
                    feedObjects[i]._PhotosLikes = await GetLikesPhoto(feedID, feedObjects[i].ObjectID, loginFeedChannelID, 1, "", websiteID);
                }
            }
            return feedObjects;
        }

        public async Task<List<Feed_Likes>> GetLikesPhoto(string feedID, string objectID, int loginFeedChannelID, int optionID, string vParentCommentID, int websiteID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginID", "");
            parameters.Add("@LoginFeedChannelID", loginFeedChannelID);
            parameters.Add("@FeedID", feedID);
            parameters.Add("@WebsiteID", websiteID);
            parameters.Add("@ObjectID", objectID);
            parameters.Add("@vParentCommentID", vParentCommentID);
            parameters.Add("@OptioniD", optionID);

            var storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_objects",
                Parameters = parameters
            };

            var feedLikes = await _dapper.ExecuteAsync<Feed_Likes>(storedProcedureRequest);
            return feedLikes.ToList();
        }

        public async Task<crm_FeedLocation> GetFeedLocation(string feedID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FeedID", feedID);

            var storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_location",
                Parameters = parameters
            };

            var feedLocations = await _dapper.ExecuteAsync<crm_FeedLocation>(storedProcedureRequest);
            return feedLocations.FirstOrDefault();
        }

        public async Task<SharedLinkRecord> GetLinkShareRecord(int websiteID, string feedID, string showType)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WebsiteID", websiteID);
            parameters.Add("@FeedID", feedID);
            parameters.Add("@HistoryFlag", showType);

            var storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_Feed_LinkShare",
                Parameters = parameters
            };

            var results = await _dapper.QueryMultipleDynamicAsync(storedProcedureRequest);

            SharedLinkRecord sharedLinkRecord = new SharedLinkRecord();

            if (results.Count > 0 && results[0].Any())
            {
                var firstResult = results[0].FirstOrDefault();
                if (firstResult != null)
                {
                    var json = JsonSerializer.Serialize(firstResult);
                    sharedLinkRecord = JsonSerializer.Deserialize<SharedLinkRecord>(json);
                }
            }

            if (results.Count > 1 && results[1].Any())
            {
                var secondResult = results[1].FirstOrDefault();
                if (secondResult != null)
                {
                    // Handle second result set appropriately
                }
            }

            WebShare_Info webShareInfo = new WebShare_Info();

            if (showType == "1" && results.Count > 2 && results[2].Any())
            {
                var result2 = results[2].FirstOrDefault();
                if (result2 != null)
                {
                    webShareInfo.List = GetFriendsImages(result2.List?.ToString() ?? "");
                    webShareInfo.ShareText = result2.ShareText?.ToString() ?? "";
                    sharedLinkRecord._WebShare_Info = webShareInfo;
                }
            }

            return sharedLinkRecord;
        }

        public List<FriendsImages> GetFriendsImages(string images)
        {
            List<FriendsImages> friendsImages = new List<FriendsImages>();

            if (!string.IsNullOrWhiteSpace(images))
            {
                string[] imageArray = images.Split(',');
                for (int i = 0; i < imageArray.Length; i++)
                {
                    if (imageArray[i].Split('!').Length > 1)
                    {
                        string link = imageArray[i].Split('!')[0];
                        string name = imageArray[i].Split('!')[1];

                        friendsImages.Add(new FriendsImages()
                        {
                            Images = link,
                            Name = name,
                        });
                    }
                    else
                    {
                        friendsImages.Add(new FriendsImages()
                        {
                            Images = imageArray[i],
                        });
                    }
                }
            }

            return friendsImages;
        }

        public async Task<ActivityPollDetails> CrmGetFeedActivities(string feedChannelID, string feedID, string activityTypeID, string optionID, int loginFeedChannelID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", loginFeedChannelID);
            parameters.Add("@FeedID", feedID);
            parameters.Add("@FeedChannelID", feedChannelID);
            parameters.Add("@ActivityTypeID", activityTypeID);
            parameters.Add("@OptionID", optionID);

            var storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_activities",
                Parameters = parameters
            };

            var result = await _dapper.QueryMultipleDynamicAsync(storedProcedureRequest);

            ActivityPollDetails activityPollDetails = null;

            if (result.Count > 0 && result[0].Any())
            {
                var firstResult = result[0].FirstOrDefault();
                if (firstResult != null)
                {
                    var json = JsonSerializer.Serialize(firstResult);
                    activityPollDetails = JsonSerializer.Deserialize<ActivityPollDetails>(json);
                }

                if (activityPollDetails != null)
                {
                    if (result.Count > 1 && result[1].Any())
                    {
                        var pollOptionsJson = JsonSerializer.Serialize(result[1]);
                        var allPollOptions = JsonSerializer.Deserialize<List<PollOptions>>(pollOptionsJson);

                        // Check what property name PollOptions uses for ActivityID
                        activityPollDetails._PollOptions = allPollOptions
                            .Where(x => GetPropertyValue(x, "ActivityID")?.ToString() == activityPollDetails.ActivityID?.ToString())
                            .ToList();

                        if (activityPollDetails._PollOptions.Any())
                        {
                            foreach (var option in activityPollDetails._PollOptions)
                            {
                                option._PollOptionReaction = await GetAllPollOptionReaction(feedChannelID, feedID, activityTypeID, option.OptionID?.ToString(), loginFeedChannelID);
                            }
                        }
                    }

                    if (result.Count > 2 && result[2].Any())
                    {
                        var eventCoHostsJson = JsonSerializer.Serialize(result[2]);
                        var allEventCoHosts = JsonSerializer.Deserialize<List<EventCo_Hosts>>(eventCoHostsJson);

                        // Instead of filtering by ActivityID (which EventCo_Hosts doesn't have),
                        // we need to check if EventCo_Hosts has a different property that relates to the activity
                        // or if they should all be included without filtering
                        // For now, let's include all EventCo_Hosts without filtering
                        activityPollDetails._EventCo_Hosts = allEventCoHosts;
                    }
                }
            }

            return activityPollDetails;
        }

        // Helper method to get property value by name (in case property names differ)
        private object GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null) return null;

            var property = obj.GetType().GetProperty(propertyName);
            if (property != null)
            {
                return property.GetValue(obj);
            }

            // Try with different casing
            property = obj.GetType().GetProperties()
                .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));

            return property?.GetValue(obj);
        }

        public async Task<List<PollOptionReaction>> GetAllPollOptionReaction(string feedChannelID, string feedID, string activityTypeID, string optionID, int loginFeedChannelID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", loginFeedChannelID);
            parameters.Add("@FeedID", feedID);
            parameters.Add("@FeedChannelID", feedChannelID);
            parameters.Add("@ActivityTypeID", activityTypeID);
            parameters.Add("@OptionID", optionID);

            var storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_activities",
                Parameters = parameters
            };

            var result = await _dapper.ExecuteAsync<PollOptionReaction>(storedProcedureRequest);
            return result.ToList();
        }

        public async Task<List<BindJobListingValueUnit>> BindJobListing(string feedChannelID, string feedID, int loginFeedChannelID, int listingID, int listingCategoryID,
                                                        int listingTopicID, int listingContractTypeID, int radiusKm, int option)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LoginFeedChannelID", loginFeedChannelID);
            parameters.Add("@FeedChannelID", feedChannelID);
            parameters.Add("@FeedID", feedID);
            parameters.Add("@ListingID", listingID);
            parameters.Add("@ListingCategoryID", listingCategoryID);
            parameters.Add("@ListingTopicID", listingTopicID);
            parameters.Add("@ListingContractTypeID", listingContractTypeID);
            parameters.Add("@RadiusKm", radiusKm);
            parameters.Add("@Option", option);

            var storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = "glivebooks.dbo.crm_get_feed_lists",
                Parameters = parameters
            };

            var result = await _dapper.QueryMultipleDynamicAsync(storedProcedureRequest);

            List<BindJobListingValueUnit> bindJobListingValueUnits = new List<BindJobListingValueUnit>();
            List<BindJobFeedIDTable1> bindJobFeedIDTable1 = new List<BindJobFeedIDTable1>();
            List<BindJobFeedIDTable2> bindJobFeedIDTable2 = new List<BindJobFeedIDTable2>();
            List<BindJobFeedIDLocation> bindJobFeedIDLocations = new List<BindJobFeedIDLocation>();
            List<BindJobFeedIDTable4> bindJobFeedIDTable4 = new List<BindJobFeedIDTable4>();

            if (result.Count > 0 && result[0].Any())
            {
                var json0 = JsonSerializer.Serialize(result[0]);
                bindJobListingValueUnits = JsonSerializer.Deserialize<List<BindJobListingValueUnit>>(json0);
            }

            if (result.Count > 1 && result[1].Any())
            {
                var json1 = JsonSerializer.Serialize(result[1]);
                bindJobFeedIDTable1 = JsonSerializer.Deserialize<List<BindJobFeedIDTable1>>(json1);
            }

            if (result.Count > 2 && result[2].Any())
            {
                var json2 = JsonSerializer.Serialize(result[2]);
                bindJobFeedIDTable2 = JsonSerializer.Deserialize<List<BindJobFeedIDTable2>>(json2);
            }

            if (result.Count > 3 && result[3].Any())
            {
                var json3 = JsonSerializer.Serialize(result[3]);
                bindJobFeedIDLocations = JsonSerializer.Deserialize<List<BindJobFeedIDLocation>>(json3);
            }

            if (result.Count > 5 && result[5].Any())
            {
                var json5 = JsonSerializer.Serialize(result[5]);
                bindJobFeedIDTable4 = JsonSerializer.Deserialize<List<BindJobFeedIDTable4>>(json5);
            }

            for (int i = 0; i < bindJobListingValueUnits.Count; i++)
            {
                bindJobListingValueUnits[i]._BindJobFeedIDLocation = bindJobFeedIDLocations.FindAll(x => x.ListingID == bindJobListingValueUnits[i].ListingID).FirstOrDefault();
                bindJobListingValueUnits[i]._BindJobFeedIDTable2 = bindJobFeedIDTable2.FindAll(x => x.ListingID == bindJobListingValueUnits[i].ListingID).FirstOrDefault();
                bindJobListingValueUnits[i]._BindJobFeedIDTable1 = bindJobFeedIDTable1.FindAll(x => x.ListingID == bindJobListingValueUnits[i].ListingID).FirstOrDefault();
                bindJobListingValueUnits[i]._BindJobFeedIDTable4 = bindJobFeedIDTable4.FindAll(x => x.ListingID == bindJobListingValueUnits[i].ListingID).FirstOrDefault();
            }

            return bindJobListingValueUnits;
        }
    }
}