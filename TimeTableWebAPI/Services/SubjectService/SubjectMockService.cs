using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.SubjectService
{
    public class SubjectMockService : ISubjectService
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        private DataConnectionSettings DataConnectionSettings { get; set; }
        private List<Subject> Subjects { get; set; }
        public SubjectMockService(DataConnectionSettings dataConnectionSettings)
        {
            this.DataConnectionSettings = DataConnectionSettings;
            Subjects = new List<Subject>();
        }
        public async Task<Subject> CreateSubject(Subject subject)
        {
            Subjects.Add(subject);
            await Task.Delay(1000);

            return subject;
        }
        public async Task<Subject> ReadSubject(string subjectId)
        {
            Subject lecturer = new Subject();
            await Task.Delay(1000);

            return lecturer;
        }
        public async Task<bool> UpdateSubject(string subjectId, Subject subject)
        {
            await Task.Delay(1000);

            return true;
        }
        public async Task<bool> DeleteSubject(string subjectId)
        {
            await Task.Delay(1000);

            return true;
        }

        public async Task<bool> CreateSampleSubjects()
        {
            List<Subject> sampleSubjects = new List<Subject>()
            {
                new Subject { SubjectCode = "3000000001", SubjectDescription = "Thomas" },
                new Subject { SubjectCode = "3000000002", SubjectDescription = "Robert"},
                new Subject { SubjectCode = "3000000003", SubjectDescription = "John"},
            };
            await Task.Delay(1000);

            Subjects = sampleSubjects;

            return true;
        }
        public async Task<List<Subject>> GetSubjects()
        {
            await Task.Delay(1000);

            return Subjects;
        }
    }
}
