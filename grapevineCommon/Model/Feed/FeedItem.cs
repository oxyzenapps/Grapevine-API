using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.Feed
{
    public class FeedItem
    {
        public int FeedID { get; set; }
        public int FeedChannelID { get; set; }
        public string? FeedContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        // Add more properties based on your proc result columns
    }
}
