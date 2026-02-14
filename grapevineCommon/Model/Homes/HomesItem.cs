using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace grapevineCommon.Model.Homes
{
   

    public class SearchProjectItem
    {
        public string ProjectName { get; set; }
        public int SearchID { get; set; }
    }

    public class ProjectListItem
    {
        public string ProjectID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }

    public class SearchDetailsItem
    {
        public int SearchID { get; set; }
    }

    public class TopDeveloperItem
    {
        public string DeveloperName { get; set; }
        public string Developer_FeedChannelID { get; set; }
    }

    public class ProjectQuesAns
    {
        public string ActionType { get; set; }
        public string Project_ID { get; set; }
        public string QuestionID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string SortOrder { get; set; }
    }

    public class ProjectPriceSheet
    {
        public string ActionType { get; set; }
        public string Project_Id { get; set; }
        public string VersionNO { get; set; }
        public string witheffectfrom { get; set; }
        public string BaseSaleablerate { get; set; }
        public string Currency { get; set; }
        public string AreaUnit { get; set; }
        public string landrate { get; set; }
        public string Rateappliedon { get; set; }
        public string Projectconfigid { get; set; }
        public string ProjectpriceSheetID { get; set; }
        public string BuildingID { get; set; }
        public string HousingUnit_Config_ID { get; set; }
    }

    public class ProjectConfigCombination
    {
        public string ActionType { get; set; }
        public string Project_Config_ID { get; set; }
        public string ProjectID { get; set; }
        public string HousingUnit_ConfigID { get; set; }
        public string Land_Area { get; set; }
        public string Carpet_Area { get; set; }
        public string CARPET_AREA_RERA { get; set; }
        public string ExtenSION_Area_RERA { get; set; }
        public string Total_AREA_RERA { get; set; }
        public string Salable_Area { get; set; }
        public string Area_UNIT { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }
        public string Image_Path { get; set; }
    }

    public class FloorLayout
    {
        public string ActionType { get; set; }
        public string Project_ID { get; set; }
        public string Floor_LayoutID { get; set; }
        public string FloorLayoutTypeID { get; set; }
        public string Floor_Layout_ShortName { get; set; }
        public string FloorLayout_Image2D { get; set; }
        public string FLoorLayout_Image3D { get; set; }
    }

    public class Building
    {
        public string ActionType { get; set; }
        public string Project_Id { get; set; }
        public string Building_Id { get; set; }
        public string Building_Name { get; set; }
        public string No_of_Wings { get; set; }
        public string No_Of_Floors { get; set; }
        public string Total_Units { get; set; }
        public string Pending_Sales_Units { get; set; }
        public string Commencement_Date { get; set; }
        public string Possession_Date { get; set; }
        public string Stage_Id { get; set; }
        public string BuildingStatusID { get; set; }
        public string TaxGroupID { get; set; }
        public string Building_front_direction { get; set; }
        public string Total_Parking { get; set; }
        public string basementfloors { get; set; }
        public string podiumfloors { get; set; }
        public string totalopenparking { get; set; }
        public string PendingClosedparking { get; set; }
        public string pendingOpenparking { get; set; }
    }

    public class ReraDetails
    {
        public string ActionType { get; set; }
        public string Projectid { get; set; }
        public string BuildingID { get; set; }
        public string ReraStateID { get; set; }
        public string Rera_Registration_Number { get; set; }
        public string Rera_Url { get; set; }
        public string PossessionDate { get; set; }
        public string ProjectPhaseDescription { get; set; }
        public string QRCode { get; set; }
        public string CommencementDate { get; set; }
    }

    public class BankApproval
    {
        public string ActionType { get; set; }
        public string Project_ID { get; set; }
        public string Building_ID { get; set; }
        public string FI_FeedChannelID { get; set; }
        public string FI_AddressID { get; set; }
        public string APFNumber { get; set; }
        public string APF_date { get; set; }
        public string APF_Letter_FeedID { get; set; }
    }

    public class MHDFilter { public string FilterName { get; set; } }

    public class MHDInsertRequest
    {
        public string DeveloperAgencyFeedChannelID { get; set; }
        public string FeedChannelID { get; set; }
        public string ProjectID { get; set; }
        public string ListingID { get; set; }
        public string SearchID { get; set; }
        public string PurchaseHorizonID { get; set; }
        public string AreaType_ID { get; set; }
        public string AreaUnitID { get; set; }
        public string ShareMobileNoWithSeller { get; set; }
        public string ShareEmailWithSeller { get; set; }
        public string ListingCategoryID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Radius { get; set; }
        public string PlaceID { get; set; }
    }

    public class HomesItem
    {
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string LocalityName { get; set; }
        public string ImagePath { get; set; }
        public string Price { get; set; }
    }

    public class ProjectBankAccountItem
    {
        public string ActionType { get; set; }
        public string Project_id { get; set; }
        public string BuildingID { get; set; }
        public string BankFeedChannelID { get; set; }
        public string BankAccountNumber { get; set; }
        public string Active { get; set; }
    }

    public class PaymentCollectionScheduleItem
    {
        public string ActionType { get; set; }
        public string Project_ID { get; set; }
        public string ScheduleID { get; set; }
        public string CollectionScheduleName { get; set; }
        public string StagePaymentTypeID { get; set; }
        public string GracedDays { get; set; }
        public string NoticeDays { get; set; }
        public string PaymentDueOn { get; set; }
    }

    public class ProjectFeatureItem
    {
        public string ActionType { get; set; }
        public string Project_ID { get; set; }
        public string Feature_subtypeid { get; set; }
    }

    public class TermsAndConditionsItem
    {
        public string ActionType { get; set; }
        public string Project_ID { get; set; }
        public string ProjectTerms_ConditionID { get; set; }
        public string Project_Terms_And_Condition { get; set; }
        public string Minimum_Booking_Amount_Type { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
        public string Percentage { get; set; }
    }

    public class ProjectCreditItem
    {
        public string ActionType { get; set; }
        public string Project_ID { get; set; }
        public string Credit_TypeID { get; set; }
        public string Company_FeedChannelID { get; set; }
    }

    public class BuildingComp { public string Building_Id { get; set; } public string Building_Name { get; set; } }
    public class WingComp { public string Wing_ID { get; set; } public string Wing_Name { get; set; } public string Project_Id { get; set; } public string Floor_LayoutId { get; set; } }
    public class FloorComp { public string FloorNumber { get; set; } }
    public class UnitComp { public string Unit_Code { get; set; } }
    public class ViewTextList { public string ViewText { get; set; } }
    public class AreaComp { public string Project_Config_ID { get; set; } public string Salable_Area { get; set; } public string Land_area { get; set; } public string Area_UNIT { get; set; } public string Unit_id1 { get; set; } }
    public class StatusComp { public string Unit_StatusId { get; set; } public string UnitStatus_Description { get; set; } public string UnitStatus_ColorCode { get; set; } }

    public class ComponentBuilding
    {
        public List<BuildingComp> _BuildingComp { get; set; } = new();
        public List<WingComp> _WingComp { get; set; } = new();
        public List<FloorComp> _FloorComp { get; set; } = new();
        public List<UnitComp> _UnitComp { get; set; } = new();
        public List<AreaComp> _AreaComp { get; set; } = new();
        public List<StatusComp> _StatusComp { get; set; } = new();
        public List<ViewTextList> _ViewTextList { get; set; } = new();
    }


    public class UnitNamingConventionItem
    {
        public string ActionType { get; set; }
        public string Project_ID { get; set; }
        public string UnitNaming_ConventionID { get; set; }
        public string General_Prefix { get; set; }
        public string General_Suffix { get; set; }
        public string Building_Name { get; set; }
        public string BuildingName_WingNameJoining { get; set; }
        public string Wing_Name { get; set; }
        public string WingName_FloorCode_JoiningText { get; set; }
        public string FloorCode_UnitCode_JoiningText { get; set; }
    }

    public class ProjectSubTypesAgency
    {
        public string Project_subtypeid { get; set; }
        public string Sub_Type_Name { get; set; }
    }

    public class CompanyResult
    {
        public int CompanyID { get; set; }
        public int FeedChannelID { get; set; }
        public int ReturnValue { get; set; }
        public int CurrentOwnerFeedChannelID { get; set; }
        public string CurrentOwnerName { get; set; }
        public string CurrentOwnerPic { get; set; }
    }
    public class FeedChannelItem
    {
        public string FeedChannelID { get; set; }
        public string Name { get; set; }
        // Add other properties returned by crm_feed_search_grapevine
    }

    public class CompanyDetailItem
    {
        public string Company_ID { get; set; }
        public string CompanyName { get; set; }
        // Add other properties returned by ode_get_company_details
    }
}

