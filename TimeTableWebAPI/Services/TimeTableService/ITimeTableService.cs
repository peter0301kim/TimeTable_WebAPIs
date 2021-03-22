using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.TimeTableService
{
    public interface ITimeTableService
    {
        Task<List<TimeTable>> GetTimeTables(string campusId, string roomId, string dayOfWeek);
    }
}
