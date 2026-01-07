using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.Feed
{
    public class FeedRequest
    {
        public int WebsiteID { get; set; } = 9;
        public int LoginFeedChannelID { get; set; } = 0;
        public int FeedChannelID { get; set; } = 0;
        public bool ExcludeFeedChannelID { get; set; } = false;
        public int ContactFeedChannelID { get; set; } = 0;
        public bool ExcludeContactID { get; set; } = false;
        public int TaggedOnly { get; set; } = 0;
        public string? FeedBy { get; set; }
        public int FeedID { get; set; } = 0;
        public int ObjectID { get; set; } = 0;
        public int FeedTypeID { get; set; } = 0;
        public int ObjectTypeID { get; set; } = 0;
        public int PageNo { get; set; } = 1;
        public string? SearchString { get; set; }
        public int FeedChannelParticipantTypeID { get; set; } = 0;
        public int ActivityID { get; set; } = 0;
        public int ListingID { get; set; } = 0;
        public int UniversalTransactionID { get; set; } = 0;
        public string FeedVisibilityID { get; set; } = "";
        public int MinViewCount { get; set; } = 0;
        public int MaxViewCount { get; set; } = 100000000;
        public int IsPosted { get; set; } = 1;
        public DateTime? DateUpto { get; set; }
        public int Announcement { get; set; } = 0;
        public int LanguageID { get; set; } = 0;
        public int Visible { get; set; } = 1;
        public int Favourite { get; set; } = 0;
        public int ForYou { get; set; } = 0;
        public string SortOrder { get; set; } = "";
        public int Query { get; set; } = 1;
        public int searchID { get; set; } = 0;
    }

}
