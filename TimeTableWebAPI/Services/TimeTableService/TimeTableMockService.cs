using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.TimeTableService
{
    public class TimeTableMockService : ITimeTableService
    {
        private InterfaceSettings InterfaceSettings { get; set; }
        public TimeTableMockService(InterfaceSettings interfaceSettings)
        {
            InterfaceSettings = interfaceSettings;
        }
        public async Task<List<TimeTable>> GetTimeTables(string campusId, string roomId, string dayOfWeek)
        {
            List<TimeTable> tTables = new List<TimeTable>()
            {
                new TimeTable { Crn = "100001", DayOfWeek = "Thomas", ClassTime = "Williams", Campus = "Thomas@xxx.com", SubjectCode = "", SubjectDesc = "", ClassRoom = "", LecturerId = "", LecturerName = "" },
                new TimeTable { Crn = "100002", DayOfWeek = "Robert", ClassTime = "Brown", Campus = "Robert@xxx.com", SubjectCode = "", SubjectDesc = "", ClassRoom = "", LecturerId = "", LecturerName = "" },
                new TimeTable { Crn = "100003", DayOfWeek = "John", ClassTime = "Wilson", Campus = "John@xxx.com", SubjectCode = "", SubjectDesc = "", ClassRoom = "", LecturerId = "", LecturerName = "" },
            };

            await Task.Delay(1000);

            return tTables;

        }


    }
}
