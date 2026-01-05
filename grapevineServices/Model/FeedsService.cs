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
        public async Task<string> GetFeedsAsync()
        {
            // Simulate fetching feeds
            await Task.Delay(100); // Simulating async work
            return "Sample Feeds Data";
        }
    }
}
