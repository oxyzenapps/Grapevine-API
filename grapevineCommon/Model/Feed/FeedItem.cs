using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.Feed
{    
    public class FeedItem
    {
        public string SearchID { get; set; }
        public string FeedID { get; set; }
        public string WebsiteID { get; set; }
        public string FeedChannelID { get; set; }
        public EventDetail FeedText_Json { get; set; }
        public string testdata { get; set; }
        public string NotificationsAllowed { get; set; }
        public string FeedObjectTypeID { get; set; }
        public string ObjectType { get; set; }
        public string ObjectID { get; set; }
        public string FeedByID { get; set; }
        public string BOTID { get; set; }
        public string FeedByContactid { get; set; }
        public string SentDateTime { get; set; }
        public string DateOfPost { get; set; }
        public string FeedVisibilityID { get; set; }
        public string FeedVisibilityIcon { get; set; }
        public string Visible { get; set; }
        public string FeedObjectType { get; set; }
        public string FeedText { get; set; }
        public string FileName { get; set; }
        public string FeedByContact_Img { get; set; }
        public string FeedByContact_Banner { get; set; }
        public string FeedByContact_Name { get; set; }
        public string MyFeedByContact_Name { get; set; }
        public string FeedByParticipantTypeID { get; set; }
        public string FeedByContactMobile { get; set; }
        public string FeedChannelTitle { get; set; }
        public string FeedChannel_Img { get; set; }
        public string TimeOfPost { get; set; }
        public string TimeOfPost_Full { get; set; }
        public string MDay { get; set; }
        public string vFeed_Like_Count { get; set; }
        public string vFeed_Comment_Count { get; set; }
        public string vFeed_Seen_Count { get; set; }
        public string vFeed_Share_Count { get; set; }
        public int Feed_Like_Count { get; set; }
        public int Feed_Share_Count { get; set; }
        public int Feed_Comment_Count { get; set; }
        public int Feed_Seen_Count { get; set; }
        public string FeedbyFeedChannelID { get; set; }
        public int ShareCount { get; set; }
        public int IsSeen { get; set; }
        public string ApplicationDescription { get; set; }
        public string ApplicationLogo { get; set; }
        public string MyLikeTypeID { get; set; }
        public string MyLiketypeDescription { get; set; }
        public string MyLiketypeIcon { get; set; }
        public string FeedTagString { get; set; }
        public string SharedFeedID { get; set; }
        public string FeelingIcon { get; set; }
        public string CommentsAllowed { get; set; }
        public string longitude { get; set; }
        public string Latitude { get; set; }
        public string LocalityID { get; set; }
        public string Place { get; set; }
        public string IsMessageFeed { get; set; }
        public string MyFeeds { get; set; }
        public int isDelete { get; set; }
        public int AdFeedID { get; set; }
        public int PhotoCount { get; set; }
        public int VideoCount { get; set; }
        public bool MediaShow { get; set; }
        public string ViewCount { get; set; }
        public string Starred { get; set; }
        public string LikeIcons { get; set; }
        public string FeedChannelParticiapntTypeID { get; set; }
        public string LanguageID { get; set; }
        public string LabelID { get; set; }
        public string ParticipantTypeID { get; set; }
        public string SavedCount { get; set; }
        public string FeedChannelTitleGoTo { get; set; }
        public string Announcement { get; set; }
        public string Pinned { get; set; }
        public string FeedDetailTitle { get; set; }
        public string FeedDetailDescription { get; set; }
        public string IsFollow { get; set; }
        public string ParentFileObjectType { get; set; }
        public int SeenCount { get; set; }
        public int IsPosted { get; set; }
        public int FeedTypeID { get; set; }
        public int FeedByApplicationID { get; set; }
        public string ReactionDateTime { get; set; }

        public string AddtoStory { get; set; }
        public string FeedChannelParticipantGroupID { get; set; }
        public List<Feed_ObjectFilePath> _FeedObjectFilePath { get; set; }
        public Feed_Likes _Feed_Likes { get; set; }
        public List<Feed_Likes> _List_Feed_Likes { get; set; }
        public List<Feed_Comments> _Feed_Comments { get; set; }
        public List<GroupAdminMembers> _GroupMembers { get; set; }
        public List<FeedLikeTypes> _FeedLikeTypes { get; set; }
        public FeedItem SharedFeed { get; set; }
        public crm_FeedLocation _crm_FeedLocation { get; set; }
        public SharedLinkRecord _SharedLinkRecord { get; set; }
        public ActivityPollDetails _ActivityPollDetails { get; set; }
        public List<FeedContacts> _FeedContacts { get; set; }
        public List<FeedLabels> _FeedLabels { get; set; }
        public Feed_Like_Reactions _Feed_Like_Reactions { get; set; }
        public List<BindJobListingValueUnit> _BindJobListingValueUnit { get; set; }
        public List<CampaignMessageModel> _MessageTemplates { get; set; }
    }
    public class FeedLabels
    {
        public string FeedID { get; set; }
        public string FeedLabelID { get; set; }
        public string FeedLabelGroupID { get; set; }
        public string FeedLabelText { get; set; }
        public string FeedLabelColorCode { get; set; }
        public string FeedLabelType { get; set; }
        public string AddedbyFeedChannelID { get; set; }

    }

    public class crm_FeedLocation
    {
        public string FeedID { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string LocalityID { get; set; }
        public string Place { get; set; }
        public string PlaceID { get; set; }
        public string PlaceName { get; set; }
        public string CItyName { get; set; }
        public string DisplayString { get; set; }
        public string CheckInString { get; set; }
        public int CheckedInCount { get; set; }
        public string CheckedIn { get; set; }
    }

    public class Listkeys
    {
        public string Keys { get; set; }
    }
    public class ListValues
    {
        public string values { get; set; }
    }

    public class EventDetail
    {
        public List<Listkeys> _ListKeys { get; set; }
        public List<ListValues> _ListValues { get; set; }
    }
    public class Feed_ObjectFilePath
    {

        public List<Feed_ObjectFilePath> _Feed_ObjectFilePath { get; set; }
        public string WebsiteID { get; set; }
        public string FeedObjectFilePath { get; set; }
        public string OriginalFeedObjectFilePath { get; set; }
        public string VideoImagePath { get; set; }
        public string MPDObjectFilePathFull { get; set; }
        public string HLSObjectFilePathFull { get; set; }
        public string FeedObjectAccessTypeiD { get; set; }
        public string AudioReference { get; set; }
        public string Extension { get; set; }
        public string FileSize { get; set; }
        public string FileName { get; set; }
        public string TimeOfPost { get; set; }
        public string FeedID { get; set; }
        public string ObjectID { get; set; }
        public string CommentID { get; set; }
        public string MyLikeTypeID { get; set; }
        public string MyLiketypeDescription { get; set; }
        public string MyLiketypeIcon { get; set; }
        public string LikeIcons { get; set; }
        public string PDuration { get; set; }
        public string vLikeCount { get; set; }
        public string vCommentsCount { get; set; }
        public string vSeenCount { get; set; }
        public string FeedObjectType { get; set; }
        public int FeedObjectTypeID { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public string ShortLikeCount { get; set; }
        public string ShortCommentsCount { get; set; }
        public string ShortShareCount { get; set; }
        public string ShortSeenCount { get; set; }

        public string FeedText { get; set; }
        public string ObjectText { get; set; }

        public string AdsButtonName { get; set; }
        public string AdFeedTitle { get; set; }
        public string AdFeedLogoPic { get; set; }
        public string AdFeedLink { get; set; }
        public List<Feed_Likes> _PhotosLikes { get; set; }
    }

    public class Feed_Likes
    {
        public List<Feed_Likes> _Feed_Like_Icons { get; set; }
        public List<Feed_Likes> _Like { get; set; }
        public string WebsiteID { get; set; }
        public string FeedID { get; set; }
        public string CommentID { get; set; }
        public string ObjectID { get; set; }
        public string FeedLikeTypeID { get; set; }
        public string FeedLikedbyContactID { get; set; }
        public string FeedLikedbyEnitytTyoeID { get; set; }
        public string FeedLikedbyEnitytID { get; set; }
        public string Likedatetime { get; set; }
        public string Contact_ProfilePic { get; set; }
        public string Contact_Name { get; set; }
        public string FeedLikeIcon { get; set; }
        public string FeedLikeDescription { get; set; }
        public string likecount { get; set; }

    }

    public class Feed_Comments
    {
        public List<Feed_Comments> _List_Feed_Comments { get; set; }
        public string WebsiteID { get; set; }
        public string FeedID { get; set; }
        public string CommentID { get; set; }
        public string ReplytoCommentID { get; set; }
        public string CommentByContactID { get; set; }
        public string CommentbyFeedChannelID { get; set; }
        public string CommentByEntityTypeID { get; set; }
        public string CommentByEntityID { get; set; }
        public string Comment { get; set; }
        public string FeedObjectTypeID { get; set; }
        public string FeedObjectType { get; set; }
        public string ObjectFilePath { get; set; }
        public string Duration { get; set; }
        public string Contact_ProfilePic { get; set; }
        public string Contact_Name { get; set; }
        public string LikeCount { get; set; }
        public string MyLikeTypeID { get; set; }
        public string MyLiketypeDescription { get; set; }
        public string MyLiketypeIcon { get; set; }
        public int IsMyComment { get; set; }
        public List<CommentsLike> _CommentsLike { get; set; }
        public List<FeedLikeTypes> _FeedLikeTypes { get; set; }

    }

    public class GroupAdminMembers
    {
        public string WebsiteID { get; set; }
        public string WorkTeamID { get; set; }
        public string FeedTargetContactid { get; set; }
        public string MemberRole { get; set; }
        public string ReportingToMemberID { get; set; }
        public string ReportingToMemberRole { get; set; }
        public string FeedChannelParticipantStatusID { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string ProfilePic { get; set; }
        public string JoiningDateTime { get; set; }
        public string EducationDetails { get; set; }
        public string IsFriend { get; set; }
        public string applicant_id { get; set; }
        public string admincontactfeedchannelid { get; set; }
        public string AdminRole { get; set; }
        public string AdminroleDescription { get; set; }
        public string MutualFriends { get; set; }
        public string IsSelf { get; set; }
        public string Invited { get; set; }
        public string Blocked { get; set; }
        public string Resigned { get; set; }
        public string Removed { get; set; }
        public string Active { get; set; }
        public string Muted { get; set; }
        public string Leftt { get; set; }
        public string FeedChannel_ID { get; set; }
        public string TagXCoordinate { get; set; }
        public string TagYCoordinate { get; set; }


    }

    public class FeedLikeTypes
    {
        public string FeedLikeTypeID { get; set; }
        public string FeedLikeDescription { get; set; }
        public string FeedLikeIcon { get; set; }
    }
    public class SharedLinkRecord
    {
        public string LinkID { get; set; }
        public string WebsiteURL { get; set; }
        public string LinkURL { get; set; }
        public string LinkTitle { get; set; }
        public string LinkDescription { get; set; }
        public string LinkImagePath { get; set; }
        public string Blocked { get; set; }
        public string WebsiteName { get; set; }
        public string SharedFirst { get; set; }
        public string FeedID { get; set; }
        public string WebsiteID { get; set; }
        public string FeedChannelPageID { get; set; }
        public string FeedChannelPageTypeID { get; set; }
        public string FeedChanelLegalEntityTypeID { get; set; }
        public string FeedChannelLegalEntityID { get; set; }
        public string PageName { get; set; }
        public string PageDescription { get; set; }
        public string PageFolder { get; set; }
        public string PageWebsite { get; set; }
        public string CreationDateTime { get; set; }
        public string PageVisible { get; set; }
        public string ParentPageID { get; set; }
        public string FeedChannelID { get; set; }
        public string PageTemplateID { get; set; }
        public string PhysicalLocation { get; set; }
        public string AppointmentInvitation { get; set; }
        public string TravelToCustomers { get; set; }
        public string OnlineGoodsServices { get; set; }
        public string PhysicalAddress { get; set; }
        public string WebsiteAddress { get; set; }
        public string LocationFeedID { get; set; }
        public string PageLogo { get; set; }
        public List<SharedLinkRecord> _List_SharedLinkRecord { get; set; }
        public WebShare_Info _WebShare_Info { get; set; }

    }

    public class ActivityPollDetails
    {
        public string CanPost { get; set; }
        public string Option1Text { get; set; }
        public string Option2Text { get; set; }
        public string Option3Text { get; set; }
        public string Option4Text { get; set; }
        public string vShareCount { get; set; }
        public string ActivityMethod { get; set; }
        public string NewAttendees { get; set; }
        public string ActivityDisplayLine { get; set; }
        public string PageNo { get; set; }
        public string FeedChannelID { get; set; }
        public string FeedbyFeedChannelID { get; set; }
        public string ActivityTypeID { get; set; }
        public string ActivityID { get; set; }
        public string ActivityImagePath { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityDetails { get; set; }
        public string ActivityQuestion { get; set; }
        public string PaidActivity { get; set; }
        public string FeedByContactID { get; set; }
        public string EventByLoginID { get; set; }
        public string Currency { get; set; }
        public string ParticipationAmount { get; set; }
        public string ScheduleDateTimeFormat1 { get; set; }
        public string ExpiryDateTimeFormat1 { get; set; }
        public string ScheduleDateTime { get; set; }
        public string ExpiryDateTime { get; set; }
        public string ScheduleDateTimeFormat { get; set; }
        public string ExpiryDateTimeFormat { get; set; }
        public string AllowMultipleAnswers { get; set; }
        public string AddOptionbyOthers { get; set; }
        public int GuestCanInviteFriends { get; set; }
        public int IsCohostInEvent { get; set; }
        public string Attendees { get; set; }
        public int PostToBeApprovedByAdmin { get; set; }
        public int OnlyAdminCanPostToWall { get; set; }
        public string ActiveStatus { get; set; }
        public string FeedID { get; set; }
        public string EventWeekName { get; set; }
        public string EventDateTime { get; set; }
        public string Went { get; set; }
        public string EventEndWeekName { get; set; }
        public string EventStartDate { get; set; }
        public string FeedChannelTitle { get; set; }
        public string FeedByContactName { get; set; }
        public string FeedByContactImage { get; set; }
        public string ScheduleDateTimeFormatforCalendar { get; set; }
        public string ExpiryDateTimeFormatforCalendar { get; set; }
        public string CoHosts { get; set; }
        public string ActivityTypeName { get; set; }
        public string IsPastEvent { get; set; }
        public string TicketLink { get; set; }
        public int IsPasswordOk { get; set; }
        public int IsPassword { get; set; }
        public int IsSaved { get; set; }
        public int ResponseCount { get; set; }
        public List<PollOptions> _PollOptions { get; set; }
        public crm_FeedLocation _crm_FeedLocation { get; set; }
        public List<EventCo_Hosts> _EventCo_Hosts { get; set; }
        public List<Feed_ObjectFilePath> _ActivityObjects { get; set; }
        public int IsResponded { get; set; }
        public int IsActivityByLoginID { get; set; }
        public int IsCohost { get; set; }
        public string OnlineDeliveryMethod { get; set; }
        public string OnlineActivityJoiningLink { get; set; }

    }

    public class FeedContacts
    {
        public string FeedID { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public string ContactEmail { get; set; }
        public string VcardString { get; set; }
        public string FeedChannelID { get; set; }
        public int ContactCount { get; set; }
        public string[] ContactImages { get; set; }
    }

    public class Feed_Like_Reactions
    {
        public string vLikeCount { get; set; }
        public string vCommentsCount { get; set; }
        public string vShareCount { get; set; }
        public string vSeenCount { get; set; }
        public int LikeCount { get; set; }
        public int Viewcount { get; set; }
        public int CommentsCount { get; set; }
        public int ShareCount { get; set; }
        public int SeenCount { get; set; }
        public string ShortLikeCount { get; set; }
        public string ShortCommentsCount { get; set; }
        public string ShortShareCount { get; set; }
        public string ShortSeenCount { get; set; }
        public string MyLikeTypeID { get; set; }
        public string MyLiketypeDescription { get; set; }
        public string MyLiketypeIcon { get; set; }
        public string FeedID { get; set; }
        public string ObjectID { get; set; }
        public string ExceptFriendList { get; set; }
        public string SpecificFriendList { get; set; }
        public string CustomListID { get; set; }
        public List<FeedLikeTypes> _FeedLikeTypes { get; set; }
    }

    public class BindJobListingValueUnit
    {
        public string CustomInt2 { get; set; }
        public string CustomInt3 { get; set; }
        public string ResponseID { get; set; }
        public string ListingStatus { get; set; }
        public string FeedChannelParticipantGroupID { get; set; }
        public string ListingCommercialPercentage { get; set; }
        public string JobBrandbyFeedChannelID { get; set; }
        public string JobBrandByFeedChannelTitle { get; set; }
        public string JobBrandByFeedChannelLogo { get; set; }
        public string ListingContractTypeId { get; set; }
        public string FeedChannelProfileImage { get; set; }
        public string Newviews { get; set; }
        public int Isposted { get; set; }
        public string NewApplicationCount { get; set; }
        public string ListingID { get; set; }
        public string ListingByFeedChannelID { get; set; }
        public string FeedID { get; set; }
        public string ListingCategoryID { get; set; }
        public string ListingTitle { get; set; }
        public string ListingHeadLine { get; set; }
        public string ListingDescription { get; set; }
        public string CreationUpdationDate { get; set; }
        public string ScheduleDate { get; set; }
        public string ScheduleDateNormal { get; set; }
        public string ExpiryDateNormal { get; set; }
        public int Expires { get; set; }
        public string ExpiryDate { get; set; }
        public string Active { get; set; }
        public string Currency { get; set; }
        public string ListingValueFrom { get; set; }
        public string ListingValueTo { get; set; }
        public string ListingValueUnitID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FeedChannelID { get; set; }
        public string FeedByContactID { get; set; }
        public string ListingTopicsID { get; set; }
        public string FeedChannelTitle { get; set; }
        public string ListingCategory { get; set; }
        public string IsPastListing { get; set; }
        public string ListingContractType { get; set; }
        public string ListingValueUnit { get; set; }
        public string Duration { get; set; }
        public string DurationforSinglePage { get; set; }
        public string ButtonColor { get; set; }
        public string FullWide { get; set; }
        public string ButtonName { get; set; }
        public string ButtonIcon { get; set; }
        public string PageCategory { get; set; }
        public string FeedChannelParticiapntTypeID { get; set; }
        public string AllowResponseDocument { get; set; }
        public string Members { get; set; }
        public string ChannelCreationDateTime { get; set; }
        public string FeedChannellogo { get; set; }
        public string StockUnitID { get; set; }
        public string StockUnitVariantID { get; set; }
        public string AvailableUnit { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string NetPrice { get; set; }
        public string Taxes { get; set; }
        public string TotalPrice { get; set; }
        public string CountryID { get; set; }
        public string SellerNote { get; set; }
        public string DeliveryPeriod { get; set; }
        public string PeriodTypeID { get; set; }
        public string CancellationPolicy { get; set; }
        public string RefundPolicy { get; set; }
        public string BrandbyFeedChannelID { get; set; }
        public string BrandbyFeedChannelTitle { get; set; }
        public string StockUnitName { get; set; }
        public string StockUnitModel { get; set; }
        public string PackedQuantity { get; set; }
        public string StockUnitServiceDescription { get; set; }
        public string StockUnitWarrantyDescription { get; set; }
        public string BrandbyFeedChannelProfileImage { get; set; }
        public string StockUnitDescription { get; set; }
        public string FeedListTopic { get; set; }
        public string FeedChannelParticiapntType { get; set; }
        public string Place { get; set; }
        public string ListingCommercial { get; set; }
        public string BrandByFeedChannelLogo { get; set; }
        public string CustomInt1 { get; set; }
        public string CustomNvarchar1 { get; set; }
        public string CustomNvarchar2 { get; set; }
        public string PromoCode { get; set; }
        public string Websitelink { get; set; }
        public string Age { get; set; }
        public string DurationforManagePage { get; set; }
        public int DistanceInKM { get; set; }
        public string PageNo { get; set; }

        //public List<BindJobFeedIDTable2> _BindImages { get; set; }
        public List<Feed_ObjectFilePath> _BindImages { get; set; }
        public BindJobFeedIDTable4 _Place { get; set; }
        public BindJobFeedIDTable4 _BindJobFeedIDTable4 { get; set; }
        public BindJobFeedIDLocation _BindJobFeedIDLocation { get; set; }
        public BindJobFeedIDTable2 _BindJobFeedIDTable2 { get; set; }
        public BindJobFeedIDTable1 _BindJobFeedIDTable1 { get; set; }
        public BindJobFeedIDTable3 _BindJobFeedIDTable3 { get; set; }
        public Contact_Detail _Contact_Detail { get; set; }
        public Feed_Like_Reactions _Feed_Like_Reactions { get; set; }
        //public UserEditPagedetails _UserEditPagedetails { get; set; }
        public DatingList _Dating { get; set; }
        public Vehicle _Vehicle { get; set; }
    }



    public class BindJobFeedIDTable4
    {

        public string FeedChannelID { get; set; }
        public string FeedChannelDigitalContactID { get; set; }
        public string ListingCategoryID { get; set; }
        public string ListingTopicsID { get; set; }
        public string ListingID { get; set; }
        public string name { get; set; }
        public string FeedID { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }

    }
    //                    
    public class BindJobFeedIDTable1
    {
        public string ListingID { get; set; }
        public string QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string QuestionHint { get; set; }
        public string QuestionReplyTypeID { get; set; }
        public string ReplyChoiseList { get; set; }
        public string FeedChannelID { get; set; }
        public string ListingCategoryID { get; set; }
        public string ListingTopicsID { get; set; }
        public string FeedID { get; set; }

    }

    public class CommentsLike
    {
        public string CommentID { get; set; }
        public string ObjectID { get; set; }
        public string FeedLikeTypeID { get; set; }
        public string likecount { get; set; }
        public string FeedLikeIcon { get; set; }
        public string FeedLikedbyContactID { get; set; }
    }

    public class WebShare_Info
    {
        public List<FriendsImages> List { get; set; }
        public string ShareText { get; set; }
    }
    public class FriendsImages
    {
        public string Images { get; set; }
        public string Name { get; set; }
    }

    public class PollOptions
    {
        public string FeedChannelID { get; set; }
        public string ActivityID { get; set; }
        public string OptionID { get; set; }
        public string OptionText { get; set; }
        public string AddedbyFeedChannelID { get; set; }
        public string Winner { get; set; }
        public string ResponseCount { get; set; }
        public string ResponsePercentage { get; set; }
        public string MyResponse { get; set; }
        public string ActivityTYpeID { get; set; }
        public List<PollOptionReaction> _PollOptionReaction { get; set; }

    }

    public class PollOptionReaction
    {
        public string ActivityID { get; set; }
        public string OptionID { get; set; }
        public string ProfilePic { get; set; }
        public string FeedChannelTitle { get; set; }
        public string ResponseDateTime { get; set; }
    }
    public class EventCo_Hosts
    {
        public string ProfilePic { get; set; }
        public string FeedChannelTitle { get; set; }
        public string FeedChannelID { get; set; }
        public string Email { get; set; }
        public int status { get; set; }
        public int IsFriend { get; set; }
    }
    public class BindJobFeedIDLocation
    {
        public string FeedID { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string LocalityID { get; set; }
        public string Place { get; set; }
        public string PlaceID { get; set; }
        public string CheckedIn { get; set; }
        public string PlaceName { get; set; }
        public string FeedChannelID { get; set; }
        public string ListingCategoryID { get; set; }
        public string ListingTopicsID { get; set; }
        public string ListingID { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }

    }
    public class BindJobFeedIDTable2
    {
        public string WebsiteID { get; set; }
        public string FeedID { get; set; }
        public string FileSize { get; set; }
        public string ObjectID { get; set; }
        public string ObjectSourceID { get; set; }
        public string FeedObjectTypeID { get; set; }
        public string FeedObjectFilePath { get; set; }
        public string FeedChannelID { get; set; }
        public string ListingCategoryID { get; set; }
        public string ListingTopicsID { get; set; }
        public string ListingID { get; set; }


        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string LocalityID { get; set; }
        public string Place { get; set; }


        public string PlaceID { get; set; }
        public string CheckedIn { get; set; }
        public string PlaceName { get; set; }

        public List<Feed_ObjectFilePath> _Feed_ObjectFilePath { get; set; }


    }
    public class BindJobFeedIDTable3
    {
        public string ListingID { get; set; }
        public string ListingTopicID { get; set; }
        public string FeedChannelID { get; set; }
        public string ListingCategoryID { get; set; }
        public string ListingTopicsID { get; set; }
        public string FeedID { get; set; }
        public string FeedListTopic { get; set; }


    }
    public class Contact_Detail
    {
        public string AutoId { get; set; }
        public string ContactID { get; set; }
        public string ReligionID { get; set; }
        public string CasteID { get; set; }
        public string SubCasteID { get; set; }
        public string DietID { get; set; }
        public string ReligionDescription { get; set; }
        public string CastDescription { get; set; }
        public string SubCasteDescription { get; set; }
        public string DietType { get; set; }

    }
    public class UserEditPagedetails
    {
        public string Graduate { get; set; }
        public string CurrentCityID { get; set; }
        public string CurrentDesignation { get; set; }
        public string CompanyName { get; set; }
        public string maritial_statusText { get; set; }
        public string maritial_status { get; set; }
        public string EmploymentClassID { get; set; }
        public string EmploymentClass { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string AnnualIncomeCurrency { get; set; }
        public string AnnualIncome { get; set; }
        public List<Hobbies> _Hobbies { get; set; }
        public List<Applyjob> _Education { get; set; }
        public List<Applyjob> _WorkData { get; set; }
        public Contact_Detail _Contact_Detail { get; set; }
    }
    public class Hobbies
    {
        public string HobbyID { get; set; }
        public string ContactID { get; set; }
        public string Hobby { get; set; }
        public string HobbyIcon { get; set; }
    }
    public class DatingList
    {
        public string ListingID { get; set; }
        public string MyGenderID { get; set; }
        public string GenderDescription { get; set; }
        public string ShowGenderOnProfile { get; set; }
        public string MyOrientationIDList { get; set; }
        public string OrientationDescription { get; set; }
        public string ShowOrientationOnProfile { get; set; }
        public string MyRelationshipPreferenceIDList { get; set; }
        public string MyRelationship_PreferencesList { get; set; }
        public string RelationshipPreferenceDescription { get; set; }
        public string ShowRelationshipPreferenceOnProfile { get; set; }
        public string OtherCanSeeMyAge { get; set; }
        public string OtherCanSeeMyDistance { get; set; }
        public string HideMyProfile { get; set; }
        public string MyAnthemFeedID { get; set; }
        public string LookingForGenderID { get; set; }
        public string LooingForDistanceInKm { get; set; }
        public string LookingforAgeFrom { get; set; }
        public string LookingForAgeTo { get; set; }
        public string LookingForRelationshipPreferenceIDList { get; set; }
        public string FeedChannelID { get; set; }
        public string ListingCategoryID { get; set; }
        public string ListingTopicsID { get; set; }
        public string FeedID { get; set; }
        public string MyGender { get; set; }
        public string MyOrientationList { get; set; }
        public string LookingForGender { get; set; }
        public string Languages { get; set; }
        public string PreferredLanguageIDlist { get; set; }
        public string LookingforGlobalSearch { get; set; }
        public string DiscoveryGlobalSearch { get; set; }
        public string DiscoveryDistanceInKm { get; set; }
        public string DiscoveryAgeFrom { get; set; }
        public string DiscoveryAgeTo { get; set; }
        public string HaveHoroscope { get; set; }
        public string MotherTongue { get; set; }
        public string HoroscopePath { get; set; }
        public string Manglik { get; set; }
        public string FamilyBasedatLoclity { get; set; }
        public string FamilyBasedatCity { get; set; }
        public string FamilyBasedatState { get; set; }
        public string FamilyBasedatCountry { get; set; }
        public string FamilyBasedatLocalityID { get; set; }
        public string LookingforReligionStrictMatch { get; set; }
        public string LookingforCasteStrictMatch { get; set; }
        public string LookingforHoroscopeMatchRequired { get; set; }
        public string LookingforManglikStrictMatch { get; set; }
        public string LookingforDiet { get; set; }
        public string LookingforMaritalStatus { get; set; }
        public string LookingforEmploymentType { get; set; }
        public string LookingforHeightRangeFrom { get; set; }
        public string LookingforHeightRangeTo { get; set; }
        public string LookingforWeightRangeFrom { get; set; }
        public string LookingforWeightRangeTo { get; set; }
        public string LookingforAnnualIncomeRangeFrom { get; set; }
        public string LookingforAnnualIncomeRangeTo { get; set; }
        public string LookingforPreferredCountry { get; set; }
        public string LookingforPreferredCities { get; set; }
        public string FamilyDetails { get; set; }
        public string IncludeInTopPick { get; set; }
        public string IncludeinRecommendedSort { get; set; }
        public string LookingForProfileType { get; set; }
        public string LookingForProfileTypeList { get; set; }
        public string MotherIs { get; set; }
        public string FatherIs { get; set; }
        public string Sister { get; set; }
        public string SisterMarried { get; set; }
        public string BrotherMarried { get; set; }
        public string Brother { get; set; }
        public string FamilyStatus { get; set; }
        public string FamilyType { get; set; }
        public string FamilyValues { get; set; }
        public string LivingWithParents { get; set; }
        public string FamilyIncomeFrom { get; set; }
        public string FamilyIncomeTo { get; set; }
        public string DrinkingHabits { get; set; }
        public string Smoking { get; set; }
        public string ResidentialStatus { get; set; }
        public string OpenToPets { get; set; }
        public string Ownahouse { get; set; }
        public string OwnaCar { get; set; }
        public string Hiv { get; set; }
        public string Thalassemia { get; set; }
        public string Challenged { get; set; }
        public string NatureOfHandicap { get; set; }
        public string Gothra { get; set; }
        public string Gothra_maternal { get; set; }
        public string BloodGroup { get; set; }
        public string MotherIsText { get; set; }
        public string FatherIsText { get; set; }
        public List<Feed_ObjectFilePath> _FeedObjectFilePath { get; set; }

    }
    public class Vehicle
    {
        public string ListingID { get; set; }
        public string Vehicle_Colour { get; set; }
        public string Vehicle_Condition { get; set; }
        public string Vehicle_Registration { get; set; }
        public string Month { get; set; }
        public string Year_of_Purchase { get; set; }
        public string TravelledDistance { get; set; }
        public string LastInsurancePaidon { get; set; }
        public string Negotiable { get; set; }
        public string BrandByFeedChannelID { get; set; }
    }
    public class Applyjob
    {
        public LoginUser_detail _LoginUser_detail { get; set; }
        public Applyjob _Applyjob { get; set; }
        public Applyjob _Resume { get; set; }
        public string views { get; set; }
        public string StatusUpdateDateTime { get; set; }

        public string ListingByProfilePic { get; set; }
        public string JobBrandByFeedChannelLogo { get; set; }
        public string JobBrandByFeedChannelTitle { get; set; }

        public string ListingByName { get; set; }
        public string BioOrSlogan { get; set; }
        public string LegalEntityID { get; set; }
        public string AnnualIncomeCurrency { get; set; }
        public string AnnualIncome { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string applicant_id { get; set; }
        public string Salutation { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }
        public string Followers { get; set; }
        public string Following { get; set; }
        public string ProfileStatusName { get; set; }
        public string IncomeCurrency { get; set; }
        public string Income { get; set; }
        public string IncomePeriod { get; set; }
        public string locality { get; set; }
        public string City { get; set; }
        public string WorkExpInYears { get; set; }
        public string Locality { get; set; }
        public string Currency { get; set; }
        public string ListingValueFrom { get; set; }
        public string ListingValueTo { get; set; }
        public string FormattedListingValueFrom { get; set; }
        public string FormattedListingValueTo { get; set; }
        public string FormattedResponseText { get; set; }
        public string ListingTitle { get; set; }
        public string ListingImage { get; set; }
        public string ListingDescription { get; set; }
        public string DurationforSinglePage { get; set; }
        public string ListingCommercial { get; set; }
        public string ListingCommercialPercentage { get; set; }
        public string ExpiryDate { get; set; }
        public string Expires { get; set; }
        public string IsPastListing { get; set; }
        public Feed_Like_Reactions _Feed_Like_Reactions { get; set; }
        public string ListingCategoryID { get; set; }
        public string PrevResponseStatusID { get; set; }
        public string ResponseID { get; set; }
        public string ListingID { get; set; }
        public string ListingOnlyOnTimeLine { get; set; }
        public string ResponseByFeedChannelID { get; set; }
        public string ResponseByListingID { get; set; }
        public string ResponseByContactId { get; set; }
        public string LeadThreadID { get; set; }
        public string ResponseButtonID { get; set; }
        public string ResponseStatusID { get; set; }
        public string Starred { get; set; }
        public string ResponseLabelID { get; set; }
        public string ResponseDateTime { get; set; }
        public string ResponseText { get; set; }
        public string ProfilePic { get; set; }
        public string RespponseName { get; set; }
        public string FeedChnnelID { get; set; }
        public string FeedChannelID { get; set; }
        public string full_name { get; set; }
        public string FeedChannelTitle { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string User_Language { get; set; }
        public string ResponseMobile { get; set; }
        public string ResponseEmail { get; set; }
        public string Profile_Pic { get; set; }
        public string Cover_Photo { get; set; }
        public string HomeTown { get; set; }
        public string CurrentCity { get; set; }
        public string Education { get; set; }
        public string workdetails { get; set; }
        public string ResponseStatus { get; set; }
        public string CompletionYearMonth { get; set; }
        public string StockUnitVariantID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }

        public string Path { get; set; }
        public string FeedListTopicID { get; set; }
        public string FeedListParentTopicID { get; set; }
        public string FeedListTopic { get; set; }
        public string ListingContractTypeID { get; set; }
        public string ListingContractType { get; set; }
        public string FeedListCategoryID { get; set; }
        public string FeedListCategory { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string FeedObjectTypeID { get; set; }
        public string UnitType { get; set; }
        public string IconPath { get; set; }
        public string rowid { get; set; }
        public string Unit_id1 { get; set; }
        public string Unit_name { get; set; }
        public string Unit_type { get; set; }
        public string FeedListTagTypeID { get; set; }
        public string FeedListTagTypeName { get; set; }
        public string FeedListTagValueID { get; set; }
        public string FeedListTagValue { get; set; }
        public string StockUnitID { get; set; }
        public string FeedVisibilityID { get; set; }
        public string CreatedbyFeedChannelID { get; set; }
        public string BrandbyFeedChannelID { get; set; }
        public string StockUnitModel { get; set; }
        public string StockUnitTopicID { get; set; }
        public string StockUnitName { get; set; }
        public string PackedQuantity { get; set; }
        public string QuantityUnitID { get; set; }
        public string StockUnitDescription { get; set; }
        public string StockUnitServiceDescription { get; set; }
        public string StockUnitWarrantyDescription { get; set; }
        public string Address { get; set; }
        public string ListingCategory { get; set; }
        public string ListingCount { get; set; }
        public List<Applyjob> _WorkDetailForJobApply { get; set; }
        public List<Applyjob> _Education { get; set; }
        public List<Applyjob> _Profiledata { get; set; }
        public List<Applyjob> _WorkData { get; set; }
        public List<Applyjob> _ResponseStatus { get; set; }
        public List<Applyjob> _ProfileDocuments { get; set; }
        public List<Applyjob> _GetCommonJobPages { get; set; }
        public List<Applyjob> _TopicsList { get; set; }
    }
    public class LoginUser_detail
    {
        public string full_name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string mobile_Country_Code { get; set; }
        public string DateOfBirth { get; set; }
        public string dob_formatted { get; set; }
        public string Profile_Pic { get; set; }
        public string User_Language { get; set; }
        public string Cover_Photo { get; set; }
        public string ContactID { get; set; }
        public string FeedChannelID { get; set; }
        public string ParticipantTypeID { get; set; }
        public string FeedChannelParticipantGroupID { get; set; }
        public string Followers { get; set; }
        public string Following { get; set; }
        public string salutation { get; set; }
        public string WhatsappWebInstanceID { get; set; }
        public string WhatsappWebInstanceCreatedDateTime { get; set; }
        public string WhatsappWebInstanceLastMessageReceivedDateTime { get; set; }
    }
}
