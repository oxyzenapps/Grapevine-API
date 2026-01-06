using grapevineCommon.Model.Feed;
using grapevineRepository.Interfaces;
using grapevineServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineServices.Model
{
    public class FeedsService : IFeedsService
    {
        private readonly IFeedRepository _feedRepository;

        public FeedsService(IFeedRepository feedRepository)
        {
            _feedRepository = feedRepository;
        }

        public async Task<FeedResponse> GetFeedAsync(FeedRequest request)
        {
            return await _feedRepository.GetFeedAsync(request);
        }
    }
}
