using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.TimeTableService
{
    public class TimeTableMockService : ITimeTableService
    {
        private DataConnectionSettings DataConnectionSettings { get; set; }
        public TimeTableMockService(DataConnectionSettings dataConnectionSettings)
        {
            this.DataConnectionSettings = dataConnectionSettings;
        }
        public async Task<ApiReturnValue<TimeTables>> GetTimeTables(string campusId, string roomId, string dayOfWeek)
        {
            List<TimeTable> lstTable = new List<TimeTable>()
            {
                    new Models.TimeTable()
                    {
                        Crn = "CR10001", DayOfWeek = "Mon", ClassTime = "11:00-13:00", Campus = "City Campus", SubjectCode = "IT-1000000001", SubjectDesc = "The Complete 2021 Web Development Bootcamp",
                        ClassRoom = "C101", LecturerId = "LE-97898",LecturerName = "Lee",StartTerm = "1",EndTerm = "4"
                    },
                    new Models.TimeTable(){
                        Crn = "CR10002", DayOfWeek = "Tue", ClassTime = "14:00-16:00", Campus = "City East Campus", SubjectCode = "DE-3600000002", SubjectDesc = "Complete Blender Creator: Learn 3D Modelling for Beginners",
                        ClassRoom = "C305", LecturerId = "LE-20098",LecturerName = "Williams",StartTerm = "1",EndTerm = "4"
                    },
                    new Models.TimeTable() {
                        Crn = "CR10003", DayOfWeek = "Wed", ClassTime = "10:00-12:00", Campus = "City Campus", SubjectCode = "IT-1000000003", SubjectDesc = "Python for Data Science and Machine Learning",
                        ClassRoom = "A205", LecturerId = "LE-30989",LecturerName = "Ted",StartTerm = "1",EndTerm = "4"
                    },
            };


            lstTable = (from s in lstTable 
                        where s.Campus.ToLower().Contains(campusId.ToLower()) &&
                              s.ClassRoom.ToLower().Contains(roomId.ToLower()) &&
                              s.DayOfWeek.ToLower().Contains(dayOfWeek.ToLower())
                        select s).ToList();

            ApiReturnValue<TimeTables> apiReturnValue = new ApiReturnValue<TimeTables>()
            {
                IsSuccess = true,
                Error = new ApiReturnError() { displayMessage = "", errorMessage = "" },
                Object = new TimeTables() { TotalCount = lstTable.Count() , PageNumber = 1, PageSize = 100, Rows = lstTable }
            };
            return apiReturnValue;

        }


    }
}
