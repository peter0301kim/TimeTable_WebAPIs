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
        public async Task<List<Subject>> ReadSubjects(string subjectName)
        {
            await CreateSampleSubjects();

            var value = (from s in Subjects where s.SubjectDescription.ToLower().Contains(subjectName.ToLower()) select s).ToList();

            return value;
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
                new Subject { SubjectCode = "IT-1000000001", SubjectDescription = "The Complete 2021 Web Development Bootcamp" },
                new Subject { SubjectCode = "DE-3600000002", SubjectDescription = "Complete Blender Creator: Learn 3D Modelling for Beginners"},
                new Subject { SubjectCode = "IT-1000000003", SubjectDescription = "Python for Data Science and Machine Learning"},

                new Subject { SubjectCode = "IT-1000000020", SubjectDescription = "React - Guide (incl Hooks, React Router, Redux)" },
                new Subject { SubjectCode = "DE-3600000032", SubjectDescription = "Advanced 3D Modelling"},
                new Subject { SubjectCode = "DR-5000000003", SubjectDescription = "The Art & Science of Drawing / BASIC SKILLS"},
                new Subject { SubjectCode = "IT-1000000301", SubjectDescription = "The Complete 2021 Web Development Bootcamp" },
                new Subject { SubjectCode = "DR-5100000002", SubjectDescription = "Environment Art School: Complete Perspective Drawing Course"},
                new Subject { SubjectCode = "IT-1000000303", SubjectDescription = "Machine Learning "},


            };
            Subjects = sampleSubjects;

            return true;
        }
        public async Task<List<Subject>> GetSubjects()
        {

            await CreateSampleSubjects();

            return Subjects;
        }
    }
}
