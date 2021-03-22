using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI
{
    public class InterfaceSettings
    {
        public InterfaceMode InterfaceMode{ get; set; }
        public string ConnectionString { get; set; }
    }

    public enum InterfaceMode
    {
        Mock,
        MsSql,
        MySql
    }
}
