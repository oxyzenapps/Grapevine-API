using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineRepository.Interfaces
{
    public interface ILoginRepository
    {
        Task<(string message, string FeedChannelID)> LoginByMobile(string mobileNo);
        Task<(string? msg, string FeedChannelID)> CheckRegistration(string name, string password, string email, string mobile, string FeedChannelID, string ProfilePic = "", string fbid = "",
            string gender = "", string first_name = "", string last_name = "", string dob = "", string salutation = "");
        Task<string> GetFeedChannelTitle(string FeedChannelID);
        Task<(string message, string FeedChannelID)> crm_Get_feed_Profile_PageData(string FeedChannelID);
    }
}
