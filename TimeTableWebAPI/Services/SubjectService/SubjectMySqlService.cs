using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.SubjectService
{
    public class SubjectMySqlService : ISubjectService
    {

        private static Logger Log = LogManager.GetCurrentClassLogger();
        private string CurrentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public MsSqlConnectionSettings ConnectionSettings { get; set; }
        private string ConnectionFile { get; set; }
        private SqlConnection SqlConn { get; set; }
        private DataConnectionSettings DataConnectionSettings { get; set; }

        private List<Subject> ListSubject { get; set; }

        public SubjectMySqlService(DataConnectionSettings dataConnectionSettings)
        {
            this.DataConnectionSettings = dataConnectionSettings;
            this.ListSubject = new List<Subject>();
        }

        public async Task<ApiReturnValue<Subjects>> CreateSubject(Subject subject)
        {
            return await Task.Run(() =>
            {

                ApiReturnValue<Subjects> apiReturnValue = new ApiReturnValue<Subjects>();

                try
                {
                    if (System.IO.File.Exists(Path.Combine(CurrentDirectory, "Data", "Subject.json")))
                    {
                        ListSubject = JsonConvert.DeserializeObject<List<Subject>>(System.IO.File.ReadAllText(Path.Combine(CurrentDirectory, "Data", "Subject.json")));
                    }

                    ListSubject.Add(subject);

                    System.IO.File.WriteAllText(Path.Combine(CurrentDirectory, "Data", "Lecturer.json"), JsonConvert.SerializeObject(ListSubject));

                    apiReturnValue.IsSuccess = true;
                }
                catch (Exception e)
                {
                    apiReturnValue.IsSuccess = false;
                    apiReturnValue.Error.displayMessage = "Error - CreateLecturer";
                    apiReturnValue.Error.errorMessage = e.Message;
                }
                return apiReturnValue;
            });
        }

        public async Task<ApiReturnValue<Subjects>> GetSubject(int pageSize = 100, int pageNumber = 1)
        {
            return await Task.Run(() =>
            {
                ApiReturnValue<Subjects> apiReturnValue = new ApiReturnValue<Subjects>();

                try
                {
                    var lstSubject = JsonConvert.DeserializeObject<List<Subject>>(System.IO.File.ReadAllText(Path.Combine(CurrentDirectory, "Data", "Subject.json")));

                    Subjects subjects = new Subjects();
                    subjects.TotalCount = lstSubject.Count();

                    subjects.PageNumber = pageNumber;
                    subjects.PageSize = pageSize;

                    subjects.Rows = lstSubject.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                    apiReturnValue.IsSuccess = true;
                    apiReturnValue.Object = subjects;

                }
                catch (Exception e)
                {
                    apiReturnValue.IsSuccess = false;
                    apiReturnValue.Object = null;
                    apiReturnValue.Error.displayMessage = "";
                    apiReturnValue.Error.errorMessage = e.Message;
                }

                return apiReturnValue;
            });

        }
        public async Task<ApiReturnValue<Subjects>> GetSubject(string subjectName, int pageSize = 100, int pageNumber = 1)
        {

            var value = await Task.Run(() =>
            {
                ApiReturnValue<Subjects> apiReturnValue = new ApiReturnValue<Subjects>();

                try
                {
                    var lstSubject = JsonConvert.DeserializeObject<List<Subject>>(System.IO.File.ReadAllText(Path.Combine(CurrentDirectory, "Data", "Lecturer.json")));
                    lstSubject = (from s in lstSubject where s.SubjectDescription.ToLower().Contains(subjectName.ToLower()) select s).ToList();

                    Subjects subjects = new Subjects();
                    subjects.TotalCount = lstSubject.Count();
                    subjects.PageNumber = pageNumber;
                    subjects.PageSize = pageSize;
                    subjects.Rows = lstSubject.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                    apiReturnValue.IsSuccess = true;
                    apiReturnValue.Object = subjects;

                }
                catch (Exception e)
                {
                    apiReturnValue.IsSuccess = false;

                }

                return apiReturnValue;

            });

            return value;
        }

        public async Task<ApiReturnValue<Subjects>> UpdateSubject(string subjectId, Subject subject)
        {
            ApiReturnValue<Subjects> apiReturnValue = new ApiReturnValue<Subjects>();

            await Task.Delay(1000);

            return apiReturnValue;
        }

        public async Task<ApiReturnValue<Subjects>> DeleteSubject(string subjectId)
        {
            ApiReturnValue<Subjects> apiReturnValue = new ApiReturnValue<Subjects>();

            await Task.Delay(1000);

            return apiReturnValue;
        }

        public async Task<ApiReturnValue<Subjects>> CreateSampleSubjects()
        {
            return await Task.Run(() =>
            {

                List<Subject> lstSubjects = new List<Subject>()
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

                System.IO.File.WriteAllText(Path.Combine(CurrentDirectory, "Data", "Subject.json"), JsonConvert.SerializeObject(lstSubjects));

                ApiReturnValue<Subjects> apiReturnValue = new ApiReturnValue<Subjects>()
                {
                    IsSuccess = true,
                    Error = new ApiReturnError() { displayMessage = "", errorMessage = "" },
                    Object = new Subjects() { TotalCount = 9, PageNumber = 1, PageSize = 100 }
                };

                return apiReturnValue;
            });
        }


    }
}
