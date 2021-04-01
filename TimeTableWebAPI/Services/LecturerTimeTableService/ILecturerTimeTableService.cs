using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.LecturerTimeTable
{
    public interface ILecturerTimeTableService
    {
        Task<ApiReturnValue<TimeTables>> GetLecturerTimeTables(string lecturerName, string dayOfWeeks);
    }
}
