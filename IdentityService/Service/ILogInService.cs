using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Service
{
    public interface ILogInService
    {
        bool CheckCredentials(string id, string password);
    }
}
