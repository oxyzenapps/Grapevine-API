using grapevineCommon.Model.Homes;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace grapevineRepository.Interfaces
{
    public interface IHomesRepository
    {
        // Filter and Search Operations
        Task<string> UpdateFilterParameter(UpdateFilterRequest request, string ssn, string feedChannelId);
        Task<string> GetSaleListCount(string pageNo, string searchId, string sort, string listingStatusId, string feedChannelId, string myFeedChannelId);
        Task<IEnumerable<dynamic>> GetProjectList(SearchProjectRequest request, string searchId);
        Task<int> GetSearchDetails(SearchDetailsRequest req);
        Task<object> GetTopLocalities(string searchId);

        // Insert Operations (Project & Infrastructure)
        Task<IEnumerable<dynamic>> InsertTopDevelopers(TopDevelopersRequest req);
        Task<IEnumerable<dynamic>> OH_Insert_Projects_QuesAns(ProjectQuesAns model);
        Task<int> OH_Insert_Project_Price_Sheet(ProjectPriceSheet model);
        Task<string> OH_insert_projectconfig_combinationarea(ProjectConfigCombination model);
        Task<IEnumerable<dynamic>> OH_insert_FloorLayouts(FloorLayout model);
        Task<string> OH_insert_Buildings(Building model);
        Task<IEnumerable<dynamic>> OH_Insert_Rera_Details(ReraDetails model);
        Task<IEnumerable<dynamic>> OH_Insert_Bank_Approvals(BankApproval model);
        Task<IEnumerable<dynamic>> OH_Insert_Projects_BankAccounts(ProjectBankAccountItem m);
        Task<IEnumerable<dynamic>> OH_Insert_Agreement_Payment_CollectionSchedule(PaymentCollectionScheduleItem m);
        Task<IEnumerable<dynamic>> OH_Insert_Project_Features(ProjectFeatureItem m);
        Task<IEnumerable<dynamic>> OH_Insert_TermsAndConditions(TermsAndConditionsItem m);
        Task<IEnumerable<dynamic>> OH_Insert_Project_Credits(ProjectCreditItem m);

        // Agency and Developer Lists
        Task<IEnumerable<dynamic>> GetAgenciesList(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId);
        Task<IEnumerable<dynamic>> GetDevelopersList(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId);
        Task<List<HomesItem>> GetBuyersAlsoLiked(string loginFeedchannelID, string searchforID);

        // MHD Operations
        Task<IEnumerable<MHDFilter>> GetMHDFilterDetail(string feedChannelID, string listingCategoryID, string developerAgencyFeedChannelID);
        Task<string> InsertMHD(MHDInsertRequest request);

        // Building and Project Details
        Task<string> GetPremiumLocationCharges(string projectId);
        Task<string> InsertDeveloperMiscCharges(MiscChargesRequest req);
        Task<string> InsertProjectSubTypeAssociation(string action, string projectId, string subTypeId);
        Task<ComponentBuilding> BindBuildingComponent(string projectId, string buildingId);
        Task<string> BuildingAvailability(string projectId, string buildingId, string wingId, string floorNo, string unitCode, string configId, string reraId, string statusId, string viewText);
        Task<object> BuildingAvailabilitySimplified(string projectID, string buildingID);

        // Inventory and Naming
        Task<string> OH_Insert_Unit_Naming_Convention(dynamic parameters);
        Task<string> OH_generate_inventory(string projectId);

        // Data Retrieval
        Task<DataTable> GetRecordWithDataTable(string query);
        Task<DataSet> GetRecordWithDataset(string query);

        // Async Specialized Operations
        Task<CompanyResult> InsertUpdateCompanyAsync(InsertUpdateCompanyRequest req, string applicantId, string applicantFeedChannelId);
        Task<int> UpdateAgenciesSalesPriceAsync(UpdateAgenciesSalesPriceRequest req);
        Task<int> UpdateAgencyDealsInAsync(UpdateAgencyDealsInRequest req);
        Task<int> OH_Insert_AgencyWotkSpaceAsync(AgencyWorkspaceRequest req);
        Task<IEnumerable<dynamic>> GetChatParticipants(string search, string typeId, string postOnly, string pageNo, string pageSize, string sort, string ssn);
        Task<int> UpdateUserDetails(UpdateUserEmailRequest model);
        Task<IEnumerable<dynamic>> GetCompanyDetails(string companyId, string contactId, string feedChannelId, string pageId, string pageSize, string filter, string loginId);
    }
}