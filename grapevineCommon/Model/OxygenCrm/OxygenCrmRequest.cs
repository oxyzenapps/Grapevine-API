
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.OxygenCrm
{
    public class GetProjectLeadRequest
    {
        public string search { get; set; } = "";
        public int leadtagvalue { get; set; } = 0;
        public DateTime? date { get; set; }
        public DateTime? date1 { get; set; }
        public int associate_id { get; set; } = 0;
        public string campaignId { get; set; } = "";
        public string mediaid { get; set; } = "";
        public string source { get; set; } = "";
        public int EntityFeedChannelID { get; set; } = 0;
        public int AssociateFeedChannelID { get; set; } = 0;
        public int PageID { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public int LoginFeedChannelID { get; set; } = 0;
        public string PipelineThreshold1 { get; set; }
        public string PipelineThreshold2 { get; set; }
        public int StageID { get; set; } = 0;
        public int StatusID { get; set; } = 0;
        public int project_typeid { get; set; } = 0;
        public int locality_id { get; set; } = 0;
        public int city_id { get; set; } = 0;
        public int PeriodType { get; set; } = 0;
        public int AgencyFeedChannelID { get; set; } = 0;
        public int AgencyContactFeedChannelID { get; set; } = 0;
    }

    public class OHDeleteAgencyRequest
    {
        public int AgencyFeedChannelID { get; set; } = 0;
    }

    public class GetAllleadschildRequest
    {
        public string Leadtagvalue { get; set; }
        public string search_text { get; set; }
        public string date { get; set; }
        public string associateID { get; set; }
        public string date1 { get; set; }
        public string stage { get; set; }
        public string CampaignID { get; set; } = "";
        public string MediaID { get; set; } = "";
        public string Source { get; set; } = "";
        public string city_id { get; set; }
        public string StatusID { get; set; }
        public string StageID { get; set; }
        public string LoginFeedChannelID { get; set; }
        public string EntityFeedChannelID { get; set; }
        public string AssociateFeedChannelID { get; set; }
        public string PipelineThreshold1 { get; set; }
        public string PipelineThreshold2 { get; set; }
        public string LeadThreadID { get; set; }
        public string AgencyFeedChannelID { get; set; } = "0";
        public string AgencyContactFeedChannelID { get; set; } = "0";
        public string PageID { get; set; } = "1";
        public string PageSize { get; set; } = "20";
    }

    public class LeadProjectSummaryRequest
    {
        public string LoginFeedChannelID { get; set; }
        public string EntityFeedChannelID { get; set; }
        public string AssociateFeedChannelID { get; set; }
        public string Leadtagvalue { get; set; }
        public string status { get; set; }
        public string LeadThreadID { get; set; }
        public string search_text { get; set; }
        public string date { get; set; }
        public string date1 { get; set; }
        public string stage { get; set; }
        public string CampaignID { get; set; }
        public string MediaID { get; set; }
        public string Source { get; set; }
        public string PipelineThreshold1 { get; set; }
        public string PipelineThreshold2 { get; set; }
        public string Index { get; set; }
        public string AgencyFeedChannelID { get; set; }
        public string AgencyContactFeedChannelID { get; set; }
        public string PageID { get; set; }
        public string PageSize { get; set; }
    }

    public class CPTaggingParametersRequest
    {
        public int EntityFeedChannelID { get; set; } = 0;
    }

    public class InsertCPTaggingRequest
    {
        public int ActionType { get; set; } = 0;
        public int EntityFeedChannelID { get; set; } = 0;
        public int TagExpiryPeriod { get; set; } = 0;
        public int TagSiteVisitExpiryPeriod { get; set; } = 0;
        public string CustomerDetailShareProgress { get; set; }
    }

    public class ShowChannelsRequest
    {
        // Empty request - no parameters needed
    }

    public class GetMediaTypeRequest
    {
        public int EntityFeedChannelID { get; set; } = 0;
        public int applicant_id { get; set; } = 0;
    }
}