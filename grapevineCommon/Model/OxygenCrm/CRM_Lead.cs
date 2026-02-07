using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.OxygenCrm
{
    public class CRM_Lead
    {
        
        public CRM_Lead()
        {
            _token = new List<token>();
        }
        public string InteractionID { get; set; } = string.Empty;
        public string LeadThreadID { get; set; } = string.Empty;
        public string LeadThreadMsg { get; set; } = string.Empty;
        public string ApplicantId { get; set; } = string.Empty;
        public string ActivityThreadID { get; set; } = string.Empty;
        public string ChannelID { get; set; } = string.Empty;

        public List<token> _token { get; set; }

    }
    public class token
    {
        public string tokendata { get; set; } = string.Empty;
    }
}
