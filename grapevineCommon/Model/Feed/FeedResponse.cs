using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.Feed
{
    public class FeedResponse
    {
        public int SearchID { get; set; }
        public List<FeedItem> Feeds { get; set; } = new();
    }
}
