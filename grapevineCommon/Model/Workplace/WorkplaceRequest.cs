using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.Workplace
{

    public class UpdateFilterRequest
    {
        public int[] FilterParameter { get; set; }
        public string[] Parameter1 { get; set; }
        public string[] Parameter2 { get; set; }
        public int SearchForId { get; set; }
        public string SearchID { get; set; } = "0";
        public string CityID { get; set; } = "0";
        public string LocalityID { get; set; } = "0";
        public string DistanceInKm { get; set; } = "0";
    }

    public class WorkplaceListRequest
    {
        public string PageNo { get; set; }
        public string SearchID { get; set; }
        public string Sort { get; set; } = "";
        public string ListingStatusID { get; set; } = "4";
        public string FeedChannelID { get; set; } = "0";
        public string MyFeedChannelID { get; set; } = "0";
        public string Mode { get; set; } = "";
        public string OldSearchID { get; set; } = "0";
    }

    public class WorkplaceRequest
    {
        public string LoginFeedchannelID { get; set; }  
        public string SearchforID { get; set; }        
        public string SearchText { get; set; }
        public bool? IsActive { get; set; }
        public string SortBy { get; set; } = "WorkplaceName";
        public string SortOrder { get; set; } = "ASC";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class ProjectListRequest
    {
        public string SearchID { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public bool SortDescending { get; set; }
    }

    public class TopLocalityRequest
    {
        public string SearchId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; } = "WorkplaceName";
        public string SortOrder { get; set; } = "ASC";
        public string SearchText { get; set; }
        public bool? IsActive { get; set; }
    }

    public class WorkplaceItem
    {
        public int WorkplaceId { get; set; }
        public string WorkplaceName { get; set; }
        public string WorkplaceCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }

        public class FeedLikeRequest
        {
            public string FeedID { get; set; }
            public string CommentID { get; set; }
            public string FeedLikeTypeID { get; set; }
            public string ObjectID { get; set; }
            public string LoginFeedChannelID { get; set; }
        }

        public class ProjectResponseRequest
        {
            public string ProjectID { get; set; }
            public string ResponseFeedChannelID { get; set; }
            public string ResponseStatusID { get; set; }
            public string ResponseText { get; set; }
            public string AssociateFeedChannelID { get; set; }
            public string ResponseChannelID { get; set; }
        }
    }
