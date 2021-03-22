using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI.Models
{
    public class MsSqlConnectionSettings
    {
        public string DatabaseServer { get; set; }
        public string DatabaseName { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string DatabaseUserName { get; set; }
        public string DatabaseUserPassword { get; set; }
    }
}
