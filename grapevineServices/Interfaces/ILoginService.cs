using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineServices.Interfaces
{
    public interface ILoginService
    {
        Task<(string message, string FeedChannelID)> LoginByMobile(string mobileNo);
    }
}
