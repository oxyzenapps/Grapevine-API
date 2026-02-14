using Dapper;
using grapevineCommon.Model.Homes;
using grapevineRepository.Interfaces;
using grapevineServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace grapevineServices.Model
{
    public class HomesService : IHomesService
    {
        private readonly IHomesRepository _repo;
        private readonly dynamic _stringEncryptor;

        public HomesService(IHomesRepository repo)
        {
            _repo = repo;
            _stringEncryptor = null;
        }

        public async Task<string> UpdateFilterAsync(UpdateFilterRequest request, string encryptedSsn)
        {
            string ssn = "Decrypted_SSN_Logic";
            string feedChannelId = "Logic_To_Get_FeedID";
            return await _repo.UpdateFilterParameter(request, ssn, feedChannelId);
        }

        public async Task<HomeServiceResponse> GetProjectListAsync(SearchProjectRequest req)
        {
            var data = await _repo.GetProjectList(req, req.OldSearchID);
            var response = new HomeServiceResponse();
            response.ActionResult.Result.Add(JsonSerializer.Serialize(data));
            response.ActionResult.Result.Add(req.OldSearchID);
            return response;
        }

        public async Task<string> GetSaleListCountAsync(string pageNo, string searchId, string sort, string listingStatusId, string feedChannelId, string myFeedChannelId)
            => await _repo.GetSaleListCount(pageNo, searchId, sort, listingStatusId, feedChannelId, myFeedChannelId);

        public async Task<HomeActionResultResponse> OH_Get_Search_Details(SearchDetailsRequest req)
        {
            var searchId = await _repo.GetSearchDetails(req);
            var response = new HomeActionResultResponse();
            response.ActionResult.Result.Add(JsonSerializer.Serialize(new { SearchID = searchId }));
            return response;
        }

        public async Task<HomeActionResultResponse> OH_Insert_Top_Developers(TopDevelopersRequest req)
        {
            var data = await _repo.InsertTopDevelopers(req);
            var response = new HomeActionResultResponse();
            response.ActionResult.Result.Add(JsonSerializer.Serialize(data));
            return response;
        }

        public async Task<object> OH_Insert_Projects_QuesAns(ProjectQuesAns model)
        {
            var data = await _repo.OH_Insert_Projects_QuesAns(model);
            return new { ActionResult = new { Result = new List<object> { data } } };
        }

        public async Task<string> OH_Insert_Project_Price_Sheet(ProjectPriceSheet m)
        {
            m.witheffectfrom = FormatDate(m.witheffectfrom);
            return (await _repo.OH_Insert_Project_Price_Sheet(m)).ToString();
        }

        public async Task<string> OH_insert_projectconfig_combinationarea(ProjectConfigCombination m)
            => await _repo.OH_insert_projectconfig_combinationarea(m);

        public async Task<UI_ACTION_RESULT> OH_insert_FloorLayouts(FloorLayout model)
        {
            var resultObj = new UI_ACTION_RESULT();
            var data = await _repo.OH_insert_FloorLayouts(model);
            resultObj.Result.Add(data);
            return resultObj;
        }

        public async Task<UI_ACTION_RESULT> OH_insert_Buildings(Building m)
        {
            m.Commencement_Date = FormatDate(m.Commencement_Date);
            m.Possession_Date = FormatDate(m.Possession_Date);
            var buildingId = await _repo.OH_insert_Buildings(m);
            var resultObj = new UI_ACTION_RESULT();
            resultObj.Result.Add(new { Status = "Success", Building_ID = buildingId });
            return resultObj;
        }

        public async Task<UI_ACTION_RESULT> OH_Insert_Rera_Details(ReraDetails m)
        {
            m.PossessionDate = FormatDate(m.PossessionDate);
            m.CommencementDate = FormatDate(m.CommencementDate);
            var data = await _repo.OH_Insert_Rera_Details(m);
            var resultObj = new UI_ACTION_RESULT();
            resultObj.Result.Add(data);
            return resultObj;
        }

        public async Task<UI_ACTION_RESULT> OH_Insert_Bank_Approvals(BankApproval m)
        {
            m.APF_date = FormatDate(m.APF_date);
            var data = await _repo.OH_Insert_Bank_Approvals(m);
            var resultObj = new UI_ACTION_RESULT();
            resultObj.Result.Add(data);
            return resultObj;
        }

        public async Task<string> GetAgenciesListAsync(string p, string s, string so, string f, string l, string m)
            => JsonSerializer.Serialize(await _repo.GetAgenciesList(p, s, so, f, l, m));

        public async Task<string> GetDevelopersListAsync(string p, string s, string so, string f, string l, string m)
            => JsonSerializer.Serialize(await _repo.GetDevelopersList(p, s, so, f, l, m));

        public HomesResponse GetBuyersAlsoLiked(HomesRequest request)
        {
            var data = GetBuyersAlsoLikedAsync(request).GetAwaiter().GetResult();
            return new HomesResponse { Success = true, IsSuccess = true, Data = data };
        }

        private async Task<List<HomesItem>> GetBuyersAlsoLikedAsync(HomesRequest request)
            => await _repo.GetBuyersAlsoLiked(request.LoginFeedchannelID, request.SearchforID);

        public Task<IEnumerable<MHDFilter>> OH_Get_MHD_FilterDetail(string feedChannelID, string listingCategoryID, string developerAgencyFeedChannelID)
            => _repo.GetMHDFilterDetail(feedChannelID, listingCategoryID, developerAgencyFeedChannelID);

        public Task<string> OH_Insert_MHD(MHDInsertRequest request)
            => _repo.InsertMHD(request);

        public async Task<HomeResponse> OH_Insert_Projects_BankAccounts(ProjectBankAccountItem item)
            => await ExecuteAndMap(item, _repo.OH_Insert_Projects_BankAccounts);

        public async Task<HomeResponse> OH_Insert_Agreement_Payment_CollectionSchedule(PaymentCollectionScheduleItem item)
            => await ExecuteAndMap(item, _repo.OH_Insert_Agreement_Payment_CollectionSchedule);

        public async Task<HomeResponse> OH_Insert_Project_Features(ProjectFeatureItem item)
            => await ExecuteAndMap(item, _repo.OH_Insert_Project_Features);

        public async Task<HomeResponse> OH_Insert_TermsAndConditions(TermsAndConditionsItem item)
            => await ExecuteAndMap(item, _repo.OH_Insert_TermsAndConditions);

        public async Task<HomeResponse> OH_Insert_Project_Credits(ProjectCreditItem item)
            => await ExecuteAndMap(item, _repo.OH_Insert_Project_Credits);

        private async Task<HomeResponse> ExecuteAndMap<T>(T item, Func<T, Task<IEnumerable<dynamic>>> repoMethod)
        {
            var data = await repoMethod(item);
            var response = new HomeResponse();
            response.ActionResult.Result.Add(JsonSerializer.Serialize(data));
            return response;
        }

        public async Task<TopLocalitiesResponse> GetTopLocalitiesAsync(string encryptedSearchId)
        {
            string searchId = _stringEncryptor != null ? _stringEncryptor.Decrypt(encryptedSearchId) : encryptedSearchId;
            var data = await _repo.GetTopLocalities(searchId);
            return new TopLocalitiesResponse { Success = true, Data = JsonSerializer.Serialize(data) };
        }

        public async Task<string> GetPremiumLocationCharges(string Project_ID)
            => await _repo.GetPremiumLocationCharges(Project_ID);

        public async Task<ComponentBuilding> BindBuildingComponent(string ProjectID, string BuildingID)
            => await _repo.BindBuildingComponent(ProjectID, BuildingID);

        public async Task<object> OH_Insert_Developer_Miscellaneous_Charges(MiscChargesRequest request)
        {
            return await _repo.InsertDeveloperMiscCharges(request);
        }

        public async Task<object> OH_Insert_Project_SubType_Association(ProjectSubTypeAssociationRequest model)
        {
            return await _repo.InsertProjectSubTypeAssociation(model.ActionType, model.ProjectID, model.Project_SubTypeID);
        }

        public async Task<object> Building_Availability(string projectID, string buildingID)
        {
            return await _repo.BuildingAvailabilitySimplified(projectID, buildingID);
        }

        private string FormatDate(string date)
        {
            if (string.IsNullOrEmpty(date) || date == "null" || date == "undefined") return null;
            return DateTime.TryParse(date, out DateTime parsedDate) ? parsedDate.ToString("yyyy-MM-dd") : date;
        }

        public async Task<string> OH_Insert_Unit_Naming_Convention(UnitNamingConventionItem item)
        {
            var p = new DynamicParameters();
            p.Add("@Action", item.ActionType);
            p.Add("@Project_ID", item.Project_ID);
            p.Add("@UnitNaming_ConventionID", item.UnitNaming_ConventionID);
            p.Add("@General_Prefix", item.General_Prefix);
            p.Add("@General_Suffix", item.General_Suffix);
            p.Add("@Building_Name", item.Building_Name);
            p.Add("@BuildingName_WingNameJoining", item.BuildingName_WingNameJoining);
            p.Add("@Wing_Name", item.Wing_Name);
            p.Add("@WingName_FloorCode_JoiningText", item.WingName_FloorCode_JoiningText);
            p.Add("@FloorCode_UnitCode_JoiningText", item.FloorCode_UnitCode_JoiningText);

            return await _repo.OH_Insert_Unit_Naming_Convention(p);
        }

        public async Task<string> OH_generate_inventory(string projectId) =>
            await _repo.OH_generate_inventory(projectId);

        public async Task<WrappedResponse> OH_Insert_Brand(BrandInsertRequest req)
        {
            string query = $"exec oxyzen_homes.dbo.OH_insert_developer @Action='{req.ActionType}', @BrandID='{req.BrandID}', @BrandName='{req.BrandName}', @Brand_Square_logo='{req.Brand_Square_logo}', @Brand_Horizontal_logo='{req.Brand_Horizontal_logo}'";
            DataTable dt = await _repo.GetRecordWithDataTable(query);

            var response = new WrappedResponse { ActionResult = new UI_ACTION_RESULT() };
            response.ActionResult.Result.Add(JsonSerializer.Serialize(dt));
            return response;
        }

        public async Task<WrappedResponse> OH_Insert_Project_Display_List(ProjectDisplayListRequest req)
        {
            string query = $@"exec oxyzen_homes.dbo.[OH_Insert_Project_Display_List] @Action='{req.ActionType}',@DisplayType_ID='{req.DisplayType_ID}', @TargetID ='{req.TargetID}',@ListingID = '{req.ListingID}',@SearchIntentID = '{req.SearchIntentID}', @CityID='{req.CityID}',@RegionID = '{req.RegionID}',@LocalityID = '{req.LocalityID}'";
            DataSet ds = await _repo.GetRecordWithDataset(query);

            var response = new WrappedResponse { ActionResult = new UI_ACTION_RESULT() };
            if (ds.Tables.Count > 0)
                response.ActionResult.Result.Add(JsonSerializer.Serialize(ds.Tables[0]));

            return response;
        }

        public async Task<List<ProjectSubTypesAgency>> bindprojectsubtype()
        {
            DataTable dt = await _repo.GetRecordWithDataTable("exec oxyzen_homes.dbo.bindprojectsubtype");
            var list = new List<ProjectSubTypesAgency>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ProjectSubTypesAgency
                {
                    Project_subtypeid = dr["Project_subtypeid"].ToString(),
                    Sub_Type_Name = dr["Sub_Type_Name"].ToString()
                });
            }
            return list;
        }

        public async Task<WrappedResponse> Bind_CompanyEntityType()
        {
            DataTable dt = await _repo.GetRecordWithDataTable("exec ode.dbo.[Bind_CompanyEntityType]");
            var response = new WrappedResponse { ActionResult = new UI_ACTION_RESULT() };
            response.ActionResult.Result.Add(JsonSerializer.Serialize(dt));
            return response;
        }

        public async Task<WrappedResponse> Bind_CompanyTaxStatusID()
        {
            DataTable dt = await _repo.GetRecordWithDataTable("exec ode.dbo.[Bind_CompanyTaxStatusID]");
            var response = new WrappedResponse { ActionResult = new UI_ACTION_RESULT() };
            response.ActionResult.Result.Add(JsonSerializer.Serialize(dt));
            return response;
        }

        

        public Task<CompanyResult> InsertUpdateCompany(InsertUpdateCompanyRequest req, string applicantId, string applicantFeedChannelId)
            => _repo.InsertUpdateCompanyAsync(req, applicantId, applicantFeedChannelId);

        public Task UpdateAgenciesSalesPrice(UpdateAgenciesSalesPriceRequest req)
            => _repo.UpdateAgenciesSalesPriceAsync(req);

        public Task<int> UpdateAgencyDealsIn(UpdateAgencyDealsInRequest req)
            => _repo.UpdateAgencyDealsInAsync(req);

        public Task<int> OH_Insert_AgencyWotkSpace(AgencyWorkspaceRequest req)
            => _repo.OH_Insert_AgencyWotkSpaceAsync(req);

        public async Task<IEnumerable<dynamic>> GetAllChatParticipants(string search, string typeId, string postOnly, string pageNo, string pageSize, string sort, string ssn)
        {
            return await _repo.GetChatParticipants(search, typeId, postOnly, pageNo, pageSize, sort, ssn);
        }

        public async Task<int> UpdateUserEmail(UpdateUserEmailRequest request)
        {
            return await _repo.UpdateUserDetails(request);
        }

        public async Task<object> CompanyProfileCompany(string companyId, string contactId, string feedChannelId, string pageId, string pageSize, string filter, string loginId)
        {
            return await _repo.GetCompanyDetails(companyId, contactId, feedChannelId, pageId, pageSize, filter, loginId);
        }
    }
}