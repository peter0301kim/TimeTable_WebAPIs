using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.SubjectService
{
    public interface ISubjectService
    {
        Task<ApiReturnValue<Subjects>> CreateSubject(Subject subject);
        Task<ApiReturnValue<Subjects>> GetSubject(int pageSize = 100, int pageNumber = 1);
        Task<ApiReturnValue<Subjects>> GetSubject(string subjectName, int pageSize = 100, int pageNumber = 1);
        Task<ApiReturnValue<Subjects>> UpdateSubject(string subjectId, Subject subject);
        Task<ApiReturnValue<Subjects>> DeleteSubject(string subjectId);
        Task<ApiReturnValue<Subjects>> CreateSampleSubjects();

    }
}
