using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.OxygenCrm
{
    

    // Database entity models (if needed)
    public class LeadItem
    {
        public string LeadID { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string ProjectName { get; set; }
        public string Stage { get; set; }
        public string Status { get; set; }
        public string AssociateName { get; set; }
        public string CreatedDate { get; set; }
        public string Source { get; set; }
        public string Campaign { get; set; }
        public string Media { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }
        public string ProjectType { get; set; }
        public string LeadTag { get; set; }
    }

    public class AgencyItem
    {
        public string AgencyFeedChannelID { get; set; }
        public string AgencyName { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
    }

    public class ChannelItem
    {
        public string ChannelID { get; set; }
        public string ChannelName { get; set; }
        public string ChannelType { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
    }

    public class CPTaggingParametersItem
    {
        public string EntityFeedChannelID { get; set; }
        public string TagExpiryPeriod { get; set; }
        public string TagSiteVisitExpiryPeriod { get; set; }
        public string CustomerDetailShareProgress { get; set; }
    }

    public class MediaTypeItem
    {
        public int MediaID { get; set; }
        public string MediaName { get; set; }
        public int MediaType { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
    }
}