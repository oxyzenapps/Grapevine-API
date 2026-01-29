//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace grapevineCommon.Model.Workplace
//{
//        public class UserProfileLocation
//        {
//            public string WebsiteID { get; set; }
//            public string ContactID { get; set; }
//            public string Latitude { get; set; }
//            public string Longitude { get; set; }
//            public string CityID { get; set; }
//            public string LocalityID { get; set; }
//            public string Address { get; set; }
//            public string Pincode { get; set; }
//            public string DistrictName { get; set; }
//            public string LocalityName { get; set; }
//        }

//        public class LocationSearchItem
//        {
//            public string CountryID { get; set; }
//            public string StateID { get; set; }
//            public string CityID { get; set; }
//            public string RegionID { get; set; }
//            public string LocalityID { get; set; }
//            public string PlaceName { get; set; }
//            public string PlaceType { get; set; }
//            public string LocalityName { get; set; }
//            public string LocationTypeID { get; set; }
//            public string CityName { get; set; }
//            public string PlaceID { get; set; }
//            public string Longitude { get; set; }
//            public string Latitude { get; set; }
//        }
//    }

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

    // NEW: Item for Agency/Developer details
    public class BusinessEntityItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string TotalProperties { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }

    
        public class WorkplaceItem
        {
            public int SearchID { get; set; }
            // Add other properties based on your stored procedure results
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
}
