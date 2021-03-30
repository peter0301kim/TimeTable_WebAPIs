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
        private DataConnectionSettings DataConnectionSettings { get; set; }

        public SubjectMySqlService(DataConnectionSettings dataConnectionSettings)
        {
            this.DataConnectionSettings = dataConnectionSettings;
            this.Subjects = new List<Subject>();
        }

        private List<Subject> Subjects { get; set; }

        public async Task<Subject> CreateSubject(Subject subject)
        {
            Subjects.Add(subject);
            await Task.Delay(1000);

            return subject;
        }

        public async Task<List<Subject>> ReadSubjects(string subjectName)
        {


            await Task.Delay(1000);

            return new List<Subject>();
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
