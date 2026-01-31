using grapevineCommon.Model.Workplace;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string LocationName { get; set; }
        public string City { get; set; }
        public string State { get; set; }

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
        public string LocationName { get; set; }
        public string LocalityId { get; set; }
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
       
        public List<object> result { get; set; } = new List<object>();
    }

   
    public class SaleCountResponse
    {
        public int TotalCount { get; set; }
        public string SearchID { get; set; }
    }



    public class WorkplaceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<WorkplaceItem> Data { get; set; } = new List<WorkplaceItem>();
        public List<WorkplaceItem> Items { get; set; } = new List<WorkplaceItem>();
        public bool IsSuccess { get; set; } 
        public int SearchID { get; set; } 
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class TopLocalitiesResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }

    
        public class ProjectAssociateResponse
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


    



       
    

    




