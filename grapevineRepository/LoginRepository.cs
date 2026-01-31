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

        public async Task<bool> LoginByMobile(string mobileNo)
        {
            string proc_name = "ode.dbo.LoginWithOTP";
            var parameters = new DynamicParameters();
            parameters.Add("@mobile_no", mobileNo);

            storedProcedureRequest = new StoredProcedureRequest
            {
                ProcedureName = proc_name,
                Parameters = parameters
            };

            var _LoginModel = await _dapper.ExecuteAsync<LoginModel>(storedProcedureRequest);
            if(_LoginModel.ToList().Any())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
