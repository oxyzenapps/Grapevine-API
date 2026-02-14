using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.Homes
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
    public class HomesRequest
    {
        public string LoginFeedchannelID { get; set; }
        public string SearchforID { get; set; }
        public string CityID { get; set; } = "0";
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class SearchProjectRequest
    {
        public string Pageno { get; set; }
        public string Sort { get; set; }
        public string Mode { get; set; }
        public string OldSearchID { get; set; }
        public string MyFeedChannelID { get; set; }
    }
    public class SearchDetailsRequest
    {
        public string SearchID { get; set; }
        public string FeedChannelID { get; set; }
        public string SearchForID { get; set; }
    }

    public class TopDevelopersRequest
    {
        public string ActionType { get; set; }
        public string Developer_FeedChannelID { get; set; } = "0";
        public string CityID { get; set; } = "0";
        public string LocalityID { get; set; } = "0";
    }

    // Request.cs
    public class GenericRequest<T> { public T Data { get; set; } }


    public class HomeRequest<T> { public T Data { get; set; } }






    public class MiscChargesRequest
    {
        public string ActionType { get; set; }
        public string ProjectId { get; set; }
        public string ChargeCalculationType { get; set; }
        public string PercentageValueAppliedOn { get; set; }
        public string HousingConfigTypeID { get; set; }
        public string RateAppliedOn { get; set; }
        public string Currency { get; set; }
        public string Value { get; set; }
        public string IncludeInAgreementValue { get; set; }
        public string GST_Included { get; set; }
        public string IncludeInCommissionPayment { get; set; }
        public string StageID { get; set; }
        public string SortOrder { get; set; }
        public string Fromdate { get; set; }
        public string AreaUnit { get; set; }
        public string Project_ConfigID { get; set; }
        public string PaymentStageId { get; set; }
        public string UpwardLoadingPercentage { get; set; }
        public string ChargesId { get; set; }
        public string Develomermiscellaneouschargesid { get; set; }
        public string BuildingID { get; set; }
    }

    public class ProjectSubTypeAssociationRequest
    {
        public string ActionType { get; set; }
        public string ProjectID { get; set; }
        public string Project_SubTypeID { get; set; }
        public string BuildingID { get; set; } = "0";
    }

    public class BrandInsertRequest
    {
        public string ActionType { get; set; }
        public string BrandID { get; set; }
        public string BrandName { get; set; }
        public string Brand_Square_logo { get; set; }
        public string Brand_Horizontal_logo { get; set; }
    }

    public class ProjectDisplayListRequest
    {
        public string ActionType { get; set; }
        public string DisplayType_ID { get; set; }
        public string TargetID { get; set; }
        public string ListingID { get; set; }
        public string SearchIntentID { get; set; }
        public string CityID { get; set; }
        public string RegionID { get; set; }
        public string LocalityID { get; set; }
    }

    public class UI_ACTION_REQUEST<T>
    {
        public T Data { get; set; }
    }

    
        public class InsertUpdateCompanyRequest
        {
            public string ActionType { get; set; }
            public string Name { get; set; }
            public string Company_Typeid { get; set; } = "";
            public string TaxStatusID { get; set; } = "";
            public string Established_Since { get; set; } = "";
            public string Website_Url { get; set; } = "";
            public string Profile { get; set; } = "";
            public string Active { get; set; } = "1";
            public string CompanyLogo { get; set; } = "";
            public string CompanyID { get; set; } = "0";
            public string CompanyBanner { get; set; } = "";
            public string PageTypeID { get; set; } = "0";
            public string PageCategoryID { get; set; } = "0";
            public string EditLocked { get; set; } = "0";
            public string BrandName { get; set; } = "";
            public string LogoBannerWebsiteID { get; set; } = "7";
            public string CityID { get; set; } = "0";
            public string LocalityID { get; set; } = "0";
            public string ProjectsFinished { get; set; } = "0";
            public string ProjectsOngoing { get; set; } = "0";
            public string ReferbyEntityFeedChannelID { get; set; } = "0";
            public string ReferByContactFeedChannelID { get; set; } = "0";
            public string MakeClaim { get; set; } = "0";
            public string CurrentOwnerFeedChannelID { get; set; } = "0";
            public string BrandID { get; set; } = "0";
        }

        public class UpdateAgenciesSalesPriceRequest
        {
            public string UpdateMode { get; set; }
            public string SalesPriceRangeFrom { get; set; }
            public string SalesPriceRangeTo { get; set; }
            public string AgencyFeedChannelID { get; set; }
        }

        public class UpdateAgencyDealsInRequest
        {
            public string UpdateMode { get; set; }
            public string AgencyFeedChannelID { get; set; }
            public string ProjectSubtypeID { get; set; }
        }

        public class AgencyWorkspaceRequest
        {
            public string ActionType { get; set; }
            public string AgencyFeedChannelID { get; set; }
            public string WorkSpaceID { get; set; }
            public string SpaceID { get; set; }
        }
    public class UpdateUserEmailRequest
    {
        public string ContactID { get; set; }
        public string Gender { get; set; }
        public string GenderId { get; set; }
        public string Salutation { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; } = "+91";
        public string FeedChannelID { get; set; } = "0";
        public string AddBiodata { get; set; } = "";
        public string LanguageID { get; set; } = "2";
    }
}











