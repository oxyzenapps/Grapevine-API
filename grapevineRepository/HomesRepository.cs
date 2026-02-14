using Dapper;
using grapevineCommon.Model.Homes;
using grapevineRepository.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace grapevineRepository
{
    public class HomesRepository : IHomesRepository
    {
        private readonly IDbConnection _connection;

        public HomesRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<string> UpdateFilterParameter(UpdateFilterRequest request, string ssn, string feedChannelId)
        {
            string searchId = request.SearchID;

            if (searchId != "0" && !string.IsNullOrEmpty(searchId))
            {
                await _connection.ExecuteAsync("DeleteFiltervalues", new { SearchID = searchId }, commandType: CommandType.StoredProcedure);
                await _connection.ExecuteAsync("DeleteSearchResults", new { SearchID = searchId }, commandType: CommandType.StoredProcedure);
            }
            else
            {
                var saveParams = new DynamicParameters();
                saveParams.Add("@SearchID", request.SearchID);
                saveParams.Add("@SSN", ssn);
                saveParams.Add("@SearchForID", request.SearchForId);
                saveParams.Add("@FeedChannelID", feedChannelId);
                saveParams.Add("@CityID", request.CityID);
                saveParams.Add("@LocalityID", request.LocalityID);
                saveParams.Add("@DistanceInKm", request.DistanceInKm);

                searchId = await _connection.ExecuteScalarAsync<string>("SaveFiltervalues", saveParams, commandType: CommandType.StoredProcedure);
            }

            if (request.FilterParameter != null)
            {
                for (int i = 0; i < request.FilterParameter.Length; i++)
                {
                    if (i < request.Parameter1.Length && !string.IsNullOrEmpty(request.Parameter1[i]) && !request.Parameter1[i].Contains("undefined"))
                    {
                        var updateParams = new
                        {
                            SearchID = searchId,
                            FilterParameterID = request.FilterParameter[i],
                            Parameter1 = request.Parameter1[i],
                            Parameter2 = request.Parameter2 != null && request.Parameter2.Length > i ? request.Parameter2[i] : null,
                            OpType = 1
                        };
                        await _connection.ExecuteAsync("oxyzen_homes.dbo.OH_Update_Search_Details", updateParams, commandType: CommandType.StoredProcedure);
                    }
                }
            }
            return searchId;
        }

        public async Task<string> GetSaleListCount(string pageNo, string searchId, string sort, string listingStatusId, string feedChannelId, string myFeedChannelId)
        {
            var p = new { PageNo = pageNo, search_id = searchId, sort, ListingStatusID = listingStatusId, FeedChannelID = feedChannelId, MyFeedChannelID = myFeedChannelId };
            return await _connection.QueryFirstOrDefaultAsync<string>("oxyzen_homes.dbo.OH_get_search_sale_List_count", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<dynamic>> GetProjectList(SearchProjectRequest req, string searchId)
        {
            var p = new { MyFeedChannelID = req.MyFeedChannelID, pageno = req.Pageno, search_id = searchId, sort = req.Sort };
            return await _connection.QueryAsync<dynamic>("oxyzen_homes.dbo.OH_get_search_project_List_count", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetSearchDetails(SearchDetailsRequest req)
        {
            var p = new DynamicParameters();
            p.Add("@SearchID", req.SearchID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            p.Add("@SearchName", "");
            p.Add("@SearchForID", req.SearchForID);
            p.Add("@Copy", "0");
            p.Add("@FeedChannelID", req.FeedChannelID);
            p.Add("@DeveloperAgencyFeedChannelID", "0");

            await _connection.ExecuteAsync("oxyzen_homes.dbo.OH_Get_Search_Details", p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("@SearchID");
        }

        public async Task<IEnumerable<dynamic>> InsertTopDevelopers(TopDevelopersRequest req)
        {
            var p = new { Action = req.ActionType, Developer_FeedChannelID = req.Developer_FeedChannelID, CityID = req.CityID, LocalityID = req.LocalityID };
            return await _connection.QueryAsync<dynamic>("oxyzen_homes.dbo.OH_Insert_Top_Developers", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<dynamic>> OH_Insert_Projects_QuesAns(ProjectQuesAns m)
        {
            return await _connection.QueryAsync("oxyzen_homes.dbo.[OH_Insert_Projects_QuesAns]",
                new { Action = m.ActionType, ProjectID = m.Project_ID, m.QuestionID, m.Question, m.Answer, m.SortOrder },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> OH_Insert_Project_Price_Sheet(ProjectPriceSheet m)
        {
            return await _connection.ExecuteAsync("oxyzen_homes.dbo.[OH_Insert_Project_Price_Sheet]",
                new { Action = m.ActionType, m.Project_Id, m.VersionNO, m.witheffectfrom, m.BaseSaleablerate, BuiltUpPrcentageOnCarpet = "0.0", m.Currency, m.AreaUnit, m.landrate, m.Rateappliedon, m.Projectconfigid, m.BuildingID, m.HousingUnit_Config_ID, m.ProjectpriceSheetID },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<string> OH_insert_projectconfig_combinationarea(ProjectConfigCombination m)
        {
            var p = new DynamicParameters();
            p.Add("@Action", m.ActionType);
            p.Add("@ProjectID", m.ProjectID);
            p.Add("@HousingUnit_ConfigID", m.HousingUnit_ConfigID);
            p.Add("@Land_Area", m.Land_Area);
            p.Add("@Carpet_Area", m.Carpet_Area);
            p.Add("@CARPET_AREA_RERA", m.CARPET_AREA_RERA);
            p.Add("@ExtenSION_Area_RERA", m.ExtenSION_Area_RERA);
            p.Add("@Total_AREA_RERA", m.Total_AREA_RERA);
            p.Add("@Salable_Area", m.Salable_Area);
            p.Add("@Area_UNIT", m.Area_UNIT);
            p.Add("@Description", m.Description);
            p.Add("@Active", m.Active);
            p.Add("@Image_Path", m.Image_Path);
            p.Add("@Project_Config_ID", m.Project_Config_ID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            await _connection.ExecuteAsync("oxyzen_homes.dbo.[OH_insert_projectconfig_combinationarea]", p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("@Project_Config_ID").ToString();
        }

        public async Task<IEnumerable<dynamic>> OH_insert_FloorLayouts(FloorLayout m)
        {
            return await _connection.QueryAsync("oxyzen_homes.dbo.[OH_insert_FloorLayouts]",
                new { Action = m.ActionType, m.Project_ID, m.Floor_LayoutID, m.FloorLayoutTypeID, m.Floor_Layout_ShortName, m.FloorLayout_Image2D, m.FLoorLayout_Image3D },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<string> OH_insert_Buildings(Building m)
        {
            var p = new DynamicParameters();
            p.Add("@Action", m.ActionType);
            p.Add("@Project_ID", m.Project_Id);
            p.Add("@Building_Name", m.Building_Name);
            p.Add("@No_of_Wings", m.No_of_Wings);
            p.Add("@No_Of_Floors", m.No_Of_Floors);
            p.Add("@Total_Units", m.Total_Units);
            p.Add("@Pending_Sales_Units", m.Pending_Sales_Units);
            p.Add("@Commencement_Date", m.Commencement_Date);
            p.Add("@Proposed_Possession_Date", m.Possession_Date);
            p.Add("@Stage_Id", m.Stage_Id);
            p.Add("@TaxGroupID", m.TaxGroupID);
            p.Add("@BuildingStatusID", m.BuildingStatusID);
            p.Add("@Building_front_direction", m.Building_front_direction);
            p.Add("@Total_Parking", m.Total_Parking);
            p.Add("@basementfloors", m.basementfloors);
            p.Add("@podiumfloors", m.podiumfloors);
            p.Add("@TotalOpenParking", m.totalopenparking);
            p.Add("@PendingClosedparking", m.PendingClosedparking);
            p.Add("@pendingOpenparking", m.pendingOpenparking);
            p.Add("@Building_ID", m.Building_Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            await _connection.ExecuteAsync("oxyzen_homes.dbo.[OH_insert_Buildings]", p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("@Building_ID").ToString();
        }

        public async Task<IEnumerable<dynamic>> OH_Insert_Rera_Details(ReraDetails m)
        {
            var p = new DynamicParameters();
            p.Add("@Action", m.ActionType);
            p.Add("@Projectid", m.Projectid);
            p.Add("@BuildingID", m.BuildingID);
            p.Add("@ReraStateID", m.ReraStateID);
            p.Add("@Rera_Registration_Number", m.Rera_Registration_Number);
            p.Add("@CommencementDate", m.CommencementDate);
            p.Add("@Rera_Url", m.Rera_Url);
            p.Add("@PossessionDate", m.PossessionDate);
            p.Add("@ProjectPhaseDescription", m.ProjectPhaseDescription);
            p.Add("@QRCode", null);
            p.Add("@QRCode_Filename", m.QRCode);

            return await _connection.QueryAsync("oxyzen_homes.dbo.[OH_Insert_Rera_Details]", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<dynamic>> OH_Insert_Bank_Approvals(BankApproval m)
        {
            return await _connection.QueryAsync("oxyzen_homes.dbo.[OH_Insert_Bank_Approvals]",
                new { Action = m.ActionType, m.Project_ID, m.Building_ID, m.FI_FeedChannelID, m.FI_AddressID, m.APFNumber, m.APF_date, m.APF_Letter_FeedID },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<dynamic>> GetAgenciesList(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId)
        {
            var p = new { PageNo = pageno, search_id = searchId, sort, FeedChannelID = feedChannelId, ListingStatusID = listingStatusId, MyfeedChannelID = myFeedChannelId };
            return await _connection.QueryAsync<dynamic>("oxyzen_homes.dbo.OH_get_search_agencies_List", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<dynamic>> GetDevelopersList(string pageno, string searchId, string sort, string feedChannelId, string listingStatusId, string myFeedChannelId)
        {
            var p = new { PageNo = pageno, search_id = searchId, sort, FeedChannelID = feedChannelId, ListingStatusID = listingStatusId, MyfeedChannelID = myFeedChannelId };
            return await _connection.QueryAsync<dynamic>("oxyzen_homes.dbo.OH_get_search_Developers_List", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<HomesItem>> GetBuyersAlsoLiked(string loginFeedchannelID, string searchforID)
        {
            var p = new { LoginFeedchannelID = loginFeedchannelID, SearchforID = searchforID };
            var result = await _connection.QueryAsync<HomesItem>("OH_get_buyers_also_liked", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<IEnumerable<MHDFilter>> GetMHDFilterDetail(string feedChannelID, string listingCategoryID, string developerAgencyFeedChannelID)
        {
            var p = new { DeveloperAgencyFeedChannelID = developerAgencyFeedChannelID, FeedChannelID = feedChannelID, ListingCategoryID = listingCategoryID };
            return await _connection.QueryAsync<MHDFilter>("oxyzen_homes.dbo.OH_Get_MHD_FilterDetail", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<string> InsertMHD(MHDInsertRequest req)
        {
            var p = new DynamicParameters();
            p.Add("@DeveloperAgencyFeedChannelID", req.DeveloperAgencyFeedChannelID);
            p.Add("@FeedChannelID", req.FeedChannelID);
            p.Add("@ProjectID", req.ProjectID);
            p.Add("@ListingID", req.ListingID);
            p.Add("@SearchID", req.SearchID);
            p.Add("@PurchaseHorizonID", req.PurchaseHorizonID);
            p.Add("@AreaType_ID", req.AreaType_ID);
            p.Add("@AreaUnitID", req.AreaUnitID);
            p.Add("@Currency", "INR");
            p.Add("@ShareMobileNoWithSeller", req.ShareMobileNoWithSeller);
            p.Add("@ShareEmailWithSeller", req.ShareEmailWithSeller);
            p.Add("@ListingCategoryID", req.ListingCategoryID);
            p.Add("@Lat", req.Latitude);
            p.Add("@Lng", req.Longitude);
            p.Add("@Radius", req.Radius);
            p.Add("@PlaceID", req.PlaceID);
            p.Add("@MHDID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _connection.ExecuteAsync("oxyzen_homes.dbo.OH_Insert_MHD", p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("@MHDID").ToString();
        }

        public async Task<IEnumerable<dynamic>> OH_Insert_Projects_BankAccounts(ProjectBankAccountItem m)
        {
            return await _connection.QueryAsync("oxyzen_homes.dbo.[OH_Insert_Projects_BankAccounts]",
                new { Action = m.ActionType, m.Project_id, m.BuildingID, m.BankFeedChannelID, m.BankAccountNumber, m.Active },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<dynamic>> OH_Insert_Agreement_Payment_CollectionSchedule(PaymentCollectionScheduleItem m)
        {
            var p = new DynamicParameters();
            p.Add("@Action", m.ActionType);
            p.Add("@ProjectID", m.Project_ID);
            p.Add("@CollectionScheduleName", m.CollectionScheduleName);
            p.Add("@StagePaymentTypeID", m.StagePaymentTypeID);
            p.Add("@GracedDays", m.GracedDays);
            p.Add("@NoticeDays", m.NoticeDays);
            p.Add("@PaymentDueOn", m.PaymentDueOn);
            p.Add("@ScheduleID", m.ScheduleID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            return await _connection.QueryAsync("oxyzen_homes.dbo.[OH_Insert_Agreement_Payment_CollectionSchedule]", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<dynamic>> OH_Insert_Project_Features(ProjectFeatureItem m)
        {
            return await _connection.QueryAsync("oxyzen_homes.dbo.[OH_Insert_Project_Features]",
                new { Action = m.ActionType, m.Project_ID, m.Feature_subtypeid },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<dynamic>> OH_Insert_TermsAndConditions(TermsAndConditionsItem m)
        {
            return await _connection.QueryAsync("oxyzen_homes.dbo.[OH_Insert_TermsAndConditions]",
                new { Action = m.ActionType, m.Project_ID, m.ProjectTerms_ConditionID, m.Project_Terms_And_Condition, m.Minimum_Booking_Amount_Type, m.Currency, m.Amount, m.Percentage },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<dynamic>> OH_Insert_Project_Credits(ProjectCreditItem m)
        {
            return await _connection.QueryAsync("oxyzen_homes.dbo.[OH_Insert_Project_Credits]",
                new { Action = m.ActionType, m.Project_ID, m.Credit_TypeID, m.Company_FeedChannelID },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<string> GetPremiumLocationCharges(string projectId)
        {
            var data = await _connection.QueryAsync("oxyzen_homes.dbo.[OH_Insert_Premium_Location_Charges]",
                new { Action = "get", Project_Id = projectId }, commandType: CommandType.StoredProcedure);
            return JsonSerializer.Serialize(data);
        }

        public async Task<string> InsertDeveloperMiscCharges(MiscChargesRequest req)
        {
            string formattedDate = req.Fromdate;
            if (!string.IsNullOrEmpty(formattedDate) && formattedDate != "null" && formattedDate != "undefined")
            {
                if (DateTime.TryParse(formattedDate, out DateTime fd))
                {
                    formattedDate = fd.ToString("yyyy-MM-dd");
                }
            }

            var data = await _connection.QueryAsync("oxyzen_homes.dbo.OH_Insert_Developer_Miscellaneous_Charges",
                new
                {
                    Action = req.ActionType,
                    req.ProjectId,
                    ChargeCalculationType = req.ChargeCalculationType,
                    PercentageValueAppliedOn = req.PercentageValueAppliedOn,
                    req.HousingConfigTypeID,
                    RateAppliedOn = req.RateAppliedOn,
                    Currency = req.Currency,
                    Value = req.Value,
                    IncludeInAgreementValue = req.IncludeInAgreementValue,
                    req.GST_Included,
                    IncludeInCommissionPayment = req.IncludeInCommissionPayment,
                    StageID = req.StageID,
                    SortOrder = req.SortOrder,
                    Fromdate = formattedDate,
                    req.AreaUnit,
                    Project_configid = req.Project_ConfigID,
                    req.PaymentStageId,
                    UpwardLoadingPercentage = req.UpwardLoadingPercentage,
                    req.ChargesId,
                    Develomermiscellaneouschargesid = req.Develomermiscellaneouschargesid,
                    req.BuildingID
                }, commandType: CommandType.StoredProcedure);

            return JsonSerializer.Serialize(data);
        }

        public async Task<string> InsertProjectSubTypeAssociation(string action, string projectId, string subTypeId)
        {
            var data = await _connection.QueryAsync("oxyzen_homes.dbo.[OH_Insert_Project_SubType_Association]",
                new { Action = action, Project_ID = projectId, Project_SubTypeID = subTypeId },
                commandType: CommandType.StoredProcedure);
            return JsonSerializer.Serialize(data);
        }

        public async Task<ComponentBuilding> BindBuildingComponent(string projectId, string buildingId)
        {
            var result = new ComponentBuilding();
            using (var multi = await _connection.QueryMultipleAsync("oxyzen_homes.dbo.OH_get_project_component_details",
                new { ProjectID = projectId, BuildingID = buildingId }, commandType: CommandType.StoredProcedure))
            {
                result._BuildingComp = (await multi.ReadAsync<BuildingComp>()).ToList();
                result._WingComp = (await multi.ReadAsync<WingComp>()).ToList();

                if (!multi.IsConsumed) multi.Read();

                result._FloorComp = (await multi.ReadAsync<FloorComp>()).ToList();
                result._UnitComp = (await multi.ReadAsync<UnitComp>()).ToList();
                result._ViewTextList = (await multi.ReadAsync<ViewTextList>()).ToList();
                result._AreaComp = (await multi.ReadAsync<AreaComp>()).ToList();

                if (!multi.IsConsumed) multi.Read();

                result._StatusComp = (await multi.ReadAsync<StatusComp>()).ToList();
            }
            return result;
        }

        public async Task<string> BuildingAvailability(string projectId, string buildingId, string wingId, string floorNo, string unitCode, string configId, string reraId, string statusId, string viewText)
        {
            var data = await _connection.QueryAsync("oxyzen_homes.dbo.OH_show_availability",
                new
                {
                    project_id = projectId,
                    BuildingID = buildingId,
                    WingID = wingId,
                    FloorNo = floorNo,
                    UnitCode = unitCode,
                    Project_Config_Id = configId,
                    RERAID = reraId,
                    UnitStatusID = statusId,
                    ViewText = viewText
                }, commandType: CommandType.StoredProcedure);

            return JsonSerializer.Serialize(data);
        }

        public async Task<object> BuildingAvailabilitySimplified(string projectID, string buildingID)
        {
            var data = await _connection.QueryAsync("oxyzen_homes.dbo.OH_show_availability",
                new
                {
                    project_id = projectID,
                    BuildingID = buildingID,
                    WingID = "0",
                    FloorNo = "0",
                    UnitCode = "",
                    Project_Config_Id = "0",
                    RERAID = "0",
                    UnitStatusID = "0",
                    ViewText = ""
                }, commandType: CommandType.StoredProcedure);

            return data;
        }

        public async Task<object> GetTopLocalities(string searchId)
        {
            return await _connection.QueryAsync("oxyzen_homes.dbo.OH_get_top_localities",
                new { SearchID = searchId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<string> OH_Insert_Unit_Naming_Convention(dynamic p)
        {
            var result = await _connection.ExecuteScalarAsync<string>(
                "oxyzen_homes.dbo.[OH_Insert_Unit_Naming_Convention]",
                (object)p,
                commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<string> OH_generate_inventory(string projectId)
        {
            var result = await _connection.ExecuteScalarAsync<string>(
                "oxyzen_homes.dbo.[OH_generate_inventory]",
                new { Project_ID = projectId },
                commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<DataTable> GetRecordWithDataTable(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    await conn.OpenAsync();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public async Task<DataSet> GetRecordWithDataset(string query)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    await conn.OpenAsync();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            return ds;
        }

        public async Task<CompanyResult> InsertUpdateCompanyAsync(InsertUpdateCompanyRequest req, string applicantId, string applicantFeedChannelId)
        {
            var p = new DynamicParameters();
            p.Add("@Action", req.ActionType);
            p.Add("@Name", req.Name);
            p.Add("@BrandName", req.BrandName);
            p.Add("@Company_TypeID", req.Company_Typeid);
            p.Add("@TaxStatusID", req.TaxStatusID);
            p.Add("@Established_Since", req.Established_Since);
            p.Add("@Website_Url", req.Website_Url);
            p.Add("@Profile", req.Profile);
            p.Add("@Active", req.Active);
            p.Add("@Applicant_ID", applicantId);
            p.Add("@ApplicantFeedChannelID", applicantFeedChannelId);
            p.Add("@CompanyLogo", req.CompanyLogo);
            p.Add("@CompanyBanner", req.CompanyBanner);
            p.Add("@LogoBannerWebsiteID", req.LogoBannerWebsiteID);
            p.Add("@DeveloperID", "0");
            p.Add("@PageTypeID", req.PageTypeID);
            p.Add("@PageCategoryID", req.PageCategoryID);
            p.Add("@EditLocked", req.EditLocked);
            p.Add("@CityID", req.CityID);
            p.Add("@LocalityID", req.LocalityID);
            p.Add("@ProjectsFinished", req.ProjectsFinished);
            p.Add("@ProjectsOngoing", req.ProjectsOngoing);
            p.Add("@ReferbyEntityFeedChannelID", req.ReferbyEntityFeedChannelID);
            p.Add("@ReferByContactFeedChannelID", req.ReferByContactFeedChannelID);
            p.Add("@MakeClaim", req.MakeClaim);
            p.Add("@BrandID", req.BrandID);

            p.Add("@CompanyID", req.CompanyID, DbType.Int32, ParameterDirection.InputOutput);
            p.Add("@FeedChannelID", 0, DbType.Int32, ParameterDirection.Output);
            p.Add("@ReturnValue", 0, DbType.Int32, ParameterDirection.Output);
            p.Add("@CurrentOwnerFeedChannelID", req.CurrentOwnerFeedChannelID, DbType.Int32, ParameterDirection.InputOutput);
            p.Add("@CurrentOwnerName", "", DbType.String, ParameterDirection.Output, 100);
            p.Add("@CurrentOwnerPic", "", DbType.String, ParameterDirection.Output, 200);

            await _connection.ExecuteAsync("ode.dbo.[ode_insert_business_firms]", p, commandType: CommandType.StoredProcedure);

            return new CompanyResult
            {
                CompanyID = p.Get<int>("@CompanyID"),
                FeedChannelID = p.Get<int>("@FeedChannelID"),
                ReturnValue = p.Get<int>("@ReturnValue"),
                CurrentOwnerFeedChannelID = p.Get<int>("@CurrentOwnerFeedChannelID"),
                CurrentOwnerName = p.Get<string>("@CurrentOwnerName"),
                CurrentOwnerPic = p.Get<string>("@CurrentOwnerPic")
            };
        }

        public async Task<int> UpdateAgenciesSalesPriceAsync(UpdateAgenciesSalesPriceRequest req)
        {
            string sql = "exec oxyzen_homes.dbo.OH_Insert_Agencies @Action='Update', @UpdateMode=@UpdateMode, @AgencyFeedChannelID=@AgencyFeedChannelID, @SalesPriceRangeFrom=@SalesPriceRangeFrom, @SalesPriceRangeTo=@SalesPriceRangeTo, @Company_ID=0, @AgencyExecutiveFeedChannelID=0, @ReferByContactFeedChannelID=0, @ReferbyAgencyFeedChannelID=0, @Agency_TypeID=0, @Date_of_Induction='', @Active=0, @Approved=0, @Agencyname='', @AgencyLogo='', @CityID=0, @Agency_ID=''";
            return await _connection.ExecuteAsync(sql, req);
        }

        public async Task<int> UpdateAgencyDealsInAsync(UpdateAgencyDealsInRequest req)
        {
            string sql = "exec oxyzen_homes.dbo.[OH_Insert_AgencyProjectSubTypeAssociation] @Action=@UpdateMode, @AgencyFeedChannelID=@AgencyFeedChannelID, @ProjectSubtypeID=@ProjectSubtypeID";
            return await _connection.ExecuteAsync(sql, req);
        }

        public async Task<int> OH_Insert_AgencyWotkSpaceAsync(AgencyWorkspaceRequest req)
        {
            string sql = "exec oxyzen_homes.dbo.[OH_Insert_AgencyWotkSpace] @Action=@ActionType, @AgencyFeedChannelID=@AgencyFeedChannelID, @WorkSpaceID=@WorkSpaceID, @SpaceID=@SpaceID";
            return await _connection.ExecuteAsync(sql, req);
        }

        public async Task<IEnumerable<dynamic>> GetChatParticipants(string search, string typeId, string postOnly, string pageNo, string pageSize, string sort, string ssn)
        {
            var parameters = new { SearchString = search, FeedChannelParticipantTypeID = typeId, WhereOneCanPostOnly = postOnly, PageNo = pageNo, PageSize = pageSize, SortOption = sort, SSN = ssn };
            return await _connection.QueryAsync("crm_feed_search_grapevine", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateUserDetails(UpdateUserEmailRequest m)
        {
            var query = "exec ode.[dbo].[ode_Update_user_details] @FeedChannelID, @Email, @CountryCode, @mobile_no, @Name, @GenderId, @gender, @salutation, @LanguageID, @BioOrAbout";
            return await _connection.ExecuteAsync(query, new
            {
                m.FeedChannelID,
                m.Email,
                m.CountryCode,
                mobile_no = m.MobileNo,
                m.Name,
                m.GenderId,
                m.Gender,
                salutation = m.Salutation,
                m.LanguageID,
                BioOrAbout = m.AddBiodata
            });
        }

        public async Task<IEnumerable<dynamic>> GetCompanyDetails(string companyId, string contactId, string feedChannelId, string pageId, string pageSize, string filter, string loginId)
        {
            var query = "exec ode.dbo.ode_get_company_details @LoginID=@loginId, @Applicant_id=@contactId, @FilterName=@filter, @CompanyID=@companyId, @FeedChannelID=@feedChannelId, @PageID=@pageId, @PageSize=@pageSize, @OnlyOneAddress=1, @LoginFeedChannelID=@loginId";
            return await _connection.QueryAsync(query, new { loginId, contactId, filter, companyId, feedChannelId, pageId, pageSize });
        }
    }
}