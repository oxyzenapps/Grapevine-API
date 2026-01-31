using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineRepository.Interfaces
{
    public interface ILoginRepository
    {
        Task<bool> LoginByMobile(string mobileNo);
    }
}
