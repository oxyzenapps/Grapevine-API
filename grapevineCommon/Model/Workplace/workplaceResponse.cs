using grapevineCommon.Model.Workplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace GrapevineCommon.Model.Workplace
    {
        public class WorkplaceLocationResponse
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

        public class WorkplaceSearchResponse
        {
            public string CountryID { get; set; }
            public string StateID { get; set; }
            public string CityID { get; set; }
            public string RegionID { get; set; }
            public string LocalityID { get; set; }
            public string PlaceName { get; set; }
            public string PlaceType { get; set; }
            public string LocalityName { get; set; }
            public string LocationTypeID { get; set; }
            public string CityName { get; set; }
            public string PlaceID { get; set; }
            public string Longitude { get; set; }
            public string Latitude { get; set; }
        }

        public class UpdateFilterResponse
        {
            public string SearchID { get; set; }
            public bool IsSuccess { get; set; }
        }

        public class WorkplaceListResponse
        {
            public UIActionResult ActionResult { get; set; } = new UIActionResult();
        }

        public class UIActionResult
        {
            // This list will hold the JSON Data string, SearchID, and PageNo
            public List<object> result { get; set; } = new List<object>();
        }

        // Specific item for Sale Count
        public class SaleCountResponse
        {
            public int TotalCount { get; set; }
            public string SearchID { get; set; }
    }

        


        public class WorkplaceResponse
        {
            public List<WorkplaceItem> Items { get; set; }
            public int SearchID { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }

            public WorkplaceResponse()
            {
                Items = new List<WorkplaceItem>();
                IsSuccess = true;
                Message = string.Empty;
            }
        }

        // To maintain compatibility with your existing structure
        public class UI_ACTION_RESULT
        {
            public List<string> result { get; set; }

            public UI_ACTION_RESULT()
            {
                result = new List<string>();
            }
        }

    public class ProjectListResponse
    {
        public List<ProjectListItem> Projects { get; set; }
        public int TotalCount { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public bool HasMore { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}



