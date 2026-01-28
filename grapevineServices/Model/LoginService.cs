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
    public class LoginService:ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<bool> LoginByMobile(string mobileNo)
        {
            return await _loginRepository.LoginByMobile(mobileNo);
        }
    }
}
