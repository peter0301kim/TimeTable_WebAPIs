using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.SubjectService
{
    public interface ISubjectService
    {
        Task<Subject> CreateSubject(Subject subject);
        Task<List<Subject>> ReadSubjects(string subjectName);
        Task<bool> UpdateSubject(string subjectId, Subject subject);
        Task<bool> DeleteSubject(string subjectId);

        Task<bool> CreateSampleSubjects();
        Task<List<Subject>> GetSubjects();
    }
}
