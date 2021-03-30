using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI
{
    public class AppGlobalSettings
    {
        public string CurrentDirectory { get; set; }
        public string DataConnectionSettingsPathFile { get; set; }

        public AppGlobalSettings()
        {
            CurrentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            DataConnectionSettingsPathFile = Path.Combine(CurrentDirectory, "DataConnectionSettings.json");
        }
    }
}
