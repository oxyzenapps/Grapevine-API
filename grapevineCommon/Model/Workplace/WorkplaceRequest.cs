using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//namespace grapevineCommon.Model.Workplace
//{
//    public class UpdateFilterRequest
//    {
//        public int[] FilterParameter { get; set; }
//        public string[] Parameter1 { get; set; }
//        public string[] Parameter2 { get; set; }
//        public int SearchForId { get; set; }
//        public string SearchID { get; set; } = "0";
//        public string CityID { get; set; } = "0";
//        public string LocalityID { get; set; } = "0";
//        public string DistanceInKm { get; set; } = "0";
//    }
//}



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

    // NEW: Unified request for SaleCount, Agencies, and Developers
    public class WorkplaceListRequest
    {
        public string PageNo { get; set; }
        public string SearchID { get; set; }
        public string Sort { get; set; } = "";
        public string ListingStatusID { get; set; } = "4";
        public string FeedChannelID { get; set; } = "0";
        public string MyFeedChannelID { get; set; } = "0";
        public string Mode { get; set; } = ""; // To handle "SearchAgain" logic
        public string OldSearchID { get; set; } = "0";
    }

    
        public class WorkplaceRequest
        {
            public string LoginFeedchannelID { get; set; }
            public string SearchforID { get; set; }

            public WorkplaceRequest()
            {
                LoginFeedchannelID = string.Empty;
                SearchforID = string.Empty;
            }
        }

    public class ProjectListRequest
    {
        public string SearchID { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public bool SortDescending { get; set; }
    }
}
