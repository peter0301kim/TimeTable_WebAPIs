using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Service
{
    public class LogInMockService : ILogInService
    {
        public bool CheckCredentials(string id, string password)
        {
            return true;
        }
    }
}
