using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI
{
    public class AppGlobalSettings
    {
        public string CurrentDirectory { get; set; }
        public string InterfaceSettingsFileName { get; set; }

        public AppGlobalSettings()
        {
            CurrentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            InterfaceSettingsFileName = "InterfaceSettings.json";
        }
    }
}
