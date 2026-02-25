using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineServices.Interfaces
{
    public interface IStringEncryptor
    {
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
    }
}
