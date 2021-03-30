using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI
{
    public class DataConnectionSettings
    {
        public DataConnectionMode DataConnectionMode { get; set; }
        public string ConnectionString { get; set; }
    }

    public enum DataConnectionMode
    {
        Mock,
        MsSql,
        MySql
    }
}
