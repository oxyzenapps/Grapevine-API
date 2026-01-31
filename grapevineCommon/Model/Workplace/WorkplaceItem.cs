

using grapevineCommon.Model.Workplace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.Workplace
{
    public class UserProfileLocation
    {
        public string WebsiteID { get; set; }
        public string ContactID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CityID { get; set; }
        public string LocalityID { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string DistrictName { get; set; }
        public string LocalityName { get; set; }
    }

    public class LocationSearchItem
    {
        public string CountryID { get; set; }
        public string StateID { get; set; }
        public string CityID { get; set; }
        public string LocalityID { get; set; }
        public string PlaceName { get; set; }
        public string PlaceType { get; set; }
        public string LocalityName { get; set; }
        public string CityName { get; set; }
        public string PlaceID { get; set; }
    }

    
    public class BusinessEntityItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string TotalProperties { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }

    
        public class PropertyItem
        {
            public int SearchID { get; set; }
            
            public int? PropertyID { get; set; }
            public string? PropertyName { get; set; }
            public decimal? Price { get; set; }
            // Add more properties as needed
        }

    public class ProjectListItem
    {
        public string ProjectID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
        public string EmailType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Status { get; set; }
    }

    public class TopLocality
    {
        public int TopLocalityId { get; set; }
        public string TopLocalityName { get; set; }
        public string TopLocalityCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }

    
        // Represents the main Feed entity
        public class AllFeeds
        {
            public string FeedID { get; set; }
            public string ObjectID { get; set; }
            public Feed_Like_Reactions _Feed_Like_Reactions { get; set; }
            public AllFeeds SharedFeed { get; set; }
        }

        // Container for the feed list used in Partial Views
        public class Feed_Channel_List
        {
            public List<AllFeeds> _AllFeeds { get; set; } = new List<AllFeeds>();
        }

        // Represents reaction data (Likes/Counts)
        public class Feed_Like_Reactions
        {
            public int LikeCount { get; set; }
            public string ReactionType { get; set; }
            public bool IsLikedByMe { get; set; }
        }

        // Represents the Associate/Lead data from the OH_Get_Next_Lead_Associate procedure
        public class ProjectAssociate
        {
            public string RecID { get; set; }
            public string Associate_ID { get; set; }
            public string AssociateFeedChannelID { get; set; }
            public string ProjectName { get; set; }
            public string Logo { get; set; }
            public string AssociateName { get; set; }
            public string Pic { get; set; }
            public string CountryCode { get; set; }
            public string AssociateMobile { get; set; }
            public string Associate_email { get; set; }
            public string EntityFeedChannelID { get; set; }
            public string EntityName { get; set; }
            public string EntityLogo { get; set; }
        }
    }

