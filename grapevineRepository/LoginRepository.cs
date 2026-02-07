using grapevineData.Interfaces;
using grapevineData;
using grapevineRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using grapevineCommon.Model.Feed;
using grapevineCommon.Model;
using System.Data;
using System.Reflection;

namespace grapevineRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDapperExecutor _dapper;
        private StoredProcedureRequest storedProcedureRequest = null;

        public LoginRepository(IDapperExecutor dapper)
        {
            this._dapper = dapper;
        }

        public async Task<(string message, string FeedChannelID)> LoginByMobile(string mobileNo)
        {
            string message = "";
            string FeedChannelID = "";
            string proc_name = "ode.dbo.LoginWithOTP";
            var parameters = new DynamicParameters();
            parameters.Add("@mobile_no", mobileNo);

            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = proc_name,
                Parameters = parameters
            };

            var _LoginModel = await _dapper.ExecuteAsync<LoginModel>(storedProcedureRequest);
            if (_LoginModel.ToList().Any())
            {
                var obj = _LoginModel.ToList();
                FeedChannelID = obj[0].aplid;

                if (obj[0].status.Equals("Your Account is Not registered with us."))
                {
                    var (msg, LoginFeedChannelID) = await CheckRegistration("", "", "", mobileNo, FeedChannelID);
                    if (msg != "Mobile No. already exists")
                    {
                        message = "Old User";
                        FeedChannelID = LoginFeedChannelID;
                    }
                    else
                    {
                        message = msg;
                        FeedChannelID = LoginFeedChannelID;
                    }
                }
                else
                {
                    (message, FeedChannelID) = await crm_Get_feed_Profile_PageData(FeedChannelID);
                }

                return (message, FeedChannelID);
            }
            else
            {
                return (message, FeedChannelID);
            }

        }

        public async Task<(string? msg, string FeedChannelID)> CheckRegistration(string name, string password, string email, string mobile, string FeedChannelID, string ProfilePic = "", string fbid = "",
            string gender = "", string first_name = "", string last_name = "", string dob = "", string salutation = "")
        {
            string applicantid = "";
            string message = "";
            string proc_name = "ode.dbo.Appregistration_final";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", name);
            parameters.Add("@mobile_no", mobile);
            parameters.Add("@Email", email);
            parameters.Add("@password", password);
            parameters.Add("@ProfilePic", ProfilePic);
            parameters.Add("@medium", "sys");
            parameters.Add("@fbid", fbid);
            parameters.Add("@Gender", gender);
            parameters.Add("@fname", first_name);
            parameters.Add("@lname", last_name);
            parameters.Add("@dob", dob);
            parameters.Add("@Salutation", salutation);
            parameters.Add("@applicantid", applicantid, dbType: DbType.String, direction: ParameterDirection.Output);
            parameters.Add("@FeedChannelID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = proc_name,
                Parameters = parameters
            };

            var obj = await _dapper.ExecuteAsync<string>(storedProcedureRequest);
            FeedChannelID = parameters.Get<int>("@FeedChannelID").ToString();
            if (obj != null)
            {
                message = obj.FirstOrDefault();
            }
            return (message, FeedChannelID);
        }

        public async Task<string> GetFeedChannelTitle(string FeedChannelID)
        {
            var result = await _dapper.QueryAsync<string>("select isnull(fname,'')as fname from ode.dbo.Contact_Information where FeedChannelID=@FeedChannelID", FeedChannelID);
            return result.FirstOrDefault() ?? string.Empty;
        }

        public class ProfileData
        {
            public string Dob { get; set; }
            public string Logo { get; set; }
        }
        public async Task<(string message, string FeedChannelID)> crm_Get_feed_Profile_PageData(string FeedChannelID)
        {
            string message = "";
            string proc_name = "glivebooks.dbo.crm_Get_feed_Profile_PageData";
            var parameters = new DynamicParameters();
            parameters.Add("@loginfeedChannelID", FeedChannelID);
            parameters.Add("@FeedChannelID", FeedChannelID);
            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = proc_name,
                Parameters = parameters
            };

            var obj = await _dapper.ExecuteAsync<ProfileData>(storedProcedureRequest);
            if (obj.ToList().Any())
            {
                if (String.IsNullOrEmpty(obj.ToList().FirstOrDefault().Logo) || String.IsNullOrEmpty(obj.ToList().FirstOrDefault().Dob))
                {
                    message = "New User";
                }
                else
                {
                    message = "Old User";
                }
            }
            return (message, FeedChannelID);
        }
    }
}
