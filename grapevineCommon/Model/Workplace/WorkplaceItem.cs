using System;
using System.Collections.Generic;
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
    }

