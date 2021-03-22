using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.LecturerService
{
    public interface ILecturerService
    {
        Task<ApiReturnValue<Lecturers>> CreateLecturer(Lecturer lecturer);
        Task<ApiReturnValue<Lecturers>> GetLecturer(int pageSize = 0, int pageNumber = 1);
        Task<ApiReturnValue<Lecturers>> GetLecturer(string lecturerId);
        Task<ApiReturnValue<Lecturers>> UpdateLecturer(string lecturerId, Lecturer lecturer);
        Task<ApiReturnValue<Lecturers>> DeleteLecturer(string lecturerId);
        Task<ApiReturnValue<Lecturers>> CreateSampleLecturer();
        
    }
}
