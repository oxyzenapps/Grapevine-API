using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.OxygenCrm
{
    

    public class GetProjectLeadResponse 
    {
        public List<LeadItem> Leads { get; set; }
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    public class OHDeleteAgencyResponse 
    {
        public string ErrorMessage { get; set; }
        public int DeletedCount { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class GetAllleadschildResponse 
    {
        public List<LeadItem> LeadChildren { get; set; }
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    public class LeadProjectSummaryResponse 
    {
        public SummaryData Summary { get; set; }
        public List<LeadItem> Leads { get; set; }
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    public class SummaryData
    {
        public int TotalLeads { get; set; }
        public int HotLeads { get; set; }
        public int WarmLeads { get; set; }
        public int ColdLeads { get; set; }
        public int ConvertedLeads { get; set; }
        public int LostLeads { get; set; }
        public decimal TotalPipelineValue { get; set; }
        public decimal AverageDealSize { get; set; }
    }

    public class CPTaggingParametersResponse 
    {
        public CPTaggingParametersItem Parameters { get; set; }
        public bool HasParameters { get; set; }
    }

    public class InsertCPTaggingResponse 
    {
        public bool IsSuccess { get; set; }
        public int AffectedRows { get; set; }
        public string Message { get; set; }
    }

    public class ShowChannelsResponse 
    {
        public List<ChannelItem> Channels { get; set; }
        public int TotalChannels { get; set; }
    }

    public class GetMediaTypeResponse 
    {
        public List<MediaTypeItem> MediaTypes { get; set; }
        public List<CampaignItem> Campaigns { get; set; }
        public List<SourceItem> Sources { get; set; }
        public List<StageItem> Stages { get; set; }
        public List<StatusItem> Statuses { get; set; }
    }

    // Supporting models for GetMediaTypeResponse
    public class CampaignItem
    {
        public string CampaignID { get; set; }
        public string CampaignName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
    }

    public class SourceItem
    {
        public string SourceID { get; set; }
        public string SourceName { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
    }

    public class StageItem
    {
        public string StageID { get; set; }
        public string StageName { get; set; }
        public string StageOrder { get; set; }
        public string Probability { get; set; }
        public string IsActive { get; set; }
    }

    public class StatusItem
    {
        public string StatusID { get; set; }
        public string StatusName { get; set; }
        public string StatusType { get; set; }
        public string IsActive { get; set; }
    }

    // Generic Response wrapper
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public ApiResponse()
        {
            Errors = new List<string>();
        }

        public static ApiResponse<T> CreateSuccess(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = 200
            };
        }

        public static ApiResponse<T> CreateError(string message, int statusCode = 400, List<string> errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                StatusCode = statusCode,
                Errors = errors ?? new List<string>()
            };
        }
    }

    
    }
