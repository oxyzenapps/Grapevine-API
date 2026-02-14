using grapevineCommon.Model.Homes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace grapevineServices.Interfaces
{
    public interface IHomesService
    {
        Task<string> UpdateFilterAsync(UpdateFilterRequest request, string encryptedSsn);
        Task<HomeServiceResponse> GetProjectListAsync(SearchProjectRequest request);
        Task<string> GetSaleListCountAsync(string pageNo, string searchId, string sort, string listingStatusId, string feedChannelId, string myFeedChannelId);
        Task<HomeActionResultResponse> OH_Get_Search_Details(SearchDetailsRequest req);
        Task<HomeActionResultResponse> OH_Insert_Top_Developers(TopDevelopersRequest req);
        Task<object> OH_Insert_Projects_QuesAns(ProjectQuesAns model);
        Task<string> OH_Insert_Project_Price_Sheet(ProjectPriceSheet model);
        Task<string> OH_insert_projectconfig_combinationarea(ProjectConfigCombination model);
        Task<UI_ACTION_RESULT> OH_insert_FloorLayouts(FloorLayout model);
        Task<UI_ACTION_RESULT> OH_insert_Buildings(Building model);
        Task<UI_ACTION_RESULT> OH_Insert_Rera_Details(ReraDetails model);
        Task<UI_ACTION_RESULT> OH_Insert_Bank_Approvals(BankApproval model);
        Task<string> GetAgenciesListAsync(string p, string s, string so, string f, string l, string m);
        Task<string> GetDevelopersListAsync(string p, string s, string so, string f, string l, string m);
        HomesResponse GetBuyersAlsoLiked(HomesRequest request);
        Task<TopLocalitiesResponse> GetTopLocalitiesAsync(string encryptedSearchId);
        Task<IEnumerable<MHDFilter>> OH_Get_MHD_FilterDetail(string feedChannelID, string listingCategoryID, string developerAgencyFeedChannelID);
        Task<string> OH_Insert_MHD(MHDInsertRequest request);
        Task<HomeResponse> OH_Insert_Projects_BankAccounts(ProjectBankAccountItem item);
        Task<HomeResponse> OH_Insert_Agreement_Payment_CollectionSchedule(PaymentCollectionScheduleItem item);
        Task<HomeResponse> OH_Insert_Project_Features(ProjectFeatureItem item);
        Task<HomeResponse> OH_Insert_TermsAndConditions(TermsAndConditionsItem item);
        Task<HomeResponse> OH_Insert_Project_Credits(ProjectCreditItem item);
        Task<string> GetPremiumLocationCharges(string Project_ID);
        Task<ComponentBuilding> BindBuildingComponent(string ProjectID, string BuildingID);

        // Fixed Signatures
        Task<object> OH_Insert_Developer_Miscellaneous_Charges(MiscChargesRequest model);
        Task<object> OH_Insert_Project_SubType_Association(ProjectSubTypeAssociationRequest model);
        Task<object> Building_Availability(string projectID, string buildingID);

        Task<string> OH_Insert_Unit_Naming_Convention(UnitNamingConventionItem item);
        Task<string> OH_generate_inventory(string projectId);

        Task<WrappedResponse> OH_Insert_Brand(BrandInsertRequest request);
        Task<WrappedResponse> OH_Insert_Project_Display_List(ProjectDisplayListRequest request);
        Task<List<ProjectSubTypesAgency>> bindprojectsubtype();
        Task<WrappedResponse> Bind_CompanyEntityType();
        Task<WrappedResponse> Bind_CompanyTaxStatusID();
        Task<CompanyResult> InsertUpdateCompany(InsertUpdateCompanyRequest request, string applicantId, string applicantFeedChannelId);

        Task UpdateAgenciesSalesPrice(UpdateAgenciesSalesPriceRequest request);

        Task<int> UpdateAgencyDealsIn(UpdateAgencyDealsInRequest request);

        Task<int> OH_Insert_AgencyWotkSpace(AgencyWorkspaceRequest request);
        Task<IEnumerable<dynamic>> GetAllChatParticipants(string search, string typeId, string postOnly, string pageNo, string pageSize, string sort, string ssn);
        Task<int> UpdateUserEmail(UpdateUserEmailRequest request);
        Task<object> CompanyProfileCompany(string companyId, string contactId, string feedChannelId, string pageId, string pageSize, string filter, string loginId);
    }
}

    
