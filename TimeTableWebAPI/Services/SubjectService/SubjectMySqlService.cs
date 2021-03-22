using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.SubjectService
{
    public class SubjectMySqlService : ISubjectService
    {

        private static Logger Log = LogManager.GetCurrentClassLogger();
        public MsSqlConnectionSettings ConnectionSettings { get; set; }
        private string ConnectionFile { get; set; }
        private SqlConnection SqlConn { get; set; }
        private InterfaceSettings InterfaceSettings { get; set; }

        public SubjectMySqlService(InterfaceSettings interfaceSettings)
        {
            InterfaceSettings = interfaceSettings;
            Subjects = new List<Subject>();
        }

        private List<Subject> Subjects { get; set; }

        public async Task<Subject> CreateSubject(Subject Subject)
        {
            Subjects.Add(Subject);
            await Task.Delay(1000);

            return Subject;
        }

        public async Task<Subject> ReadSubject(string SubjectId)
        {

            Subject Subject = new Subject();
            await Task.Delay(1000);

            return Subject;
        }

        public async Task<bool> UpdateSubject(string SubjectId, Subject Subject)
        {

            await Task.Delay(1000);

            return true;
        }

        public async Task<bool> DeleteSubject(string SubjectId)
        {

            await Task.Delay(1000);

            return true;
        }

        public async Task<bool> CreateSampleSubjects()
        {
            List<Subject> sampleSubjects = new List<Subject>()
            {
                new Subject { SubjectCode = "100001", SubjectDescription = "Thomas" },
                new Subject { SubjectCode = "100002", SubjectDescription = "Robert"},
                new Subject { SubjectCode = "100003", SubjectDescription = "John" },
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
