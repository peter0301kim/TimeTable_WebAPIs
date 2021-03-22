using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.LecturerService
{
    public class LecturerMockService : ILecturerService
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        
        private string CurrentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private InterfaceSettings InterfaceSettings { get; set; }


        private List<Lecturer> Lecturers { get; set; }

        public LecturerMockService(InterfaceSettings interfaceSettings)
        {
            InterfaceSettings = interfaceSettings;
            Lecturers = new List<Lecturer>();
        }
        public async Task<ApiReturnValue<Lecturers>> CreateLecturer(Lecturer lecturer)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            try
            {
                if(System.IO.File.Exists(Path.Combine(CurrentDirectory, "Data", "Lecturer.json")))
                {
                    Lecturers = JsonConvert.DeserializeObject<List<Lecturer>>(System.IO.File.ReadAllText(Path.Combine(CurrentDirectory, "Data", "Lecturer.json")));
                }                

                Lecturers.Add(lecturer);

                System.IO.File.WriteAllText(Path.Combine(CurrentDirectory, "Data", "Lecturer.json"), JsonConvert.SerializeObject(Lecturers));

                apiReturnValue.IsSuccess = true;
            }
            catch (Exception e)
            {
                apiReturnValue.IsSuccess = false;
                apiReturnValue.Error.displayMessage = "Error - CreateLecturer";
                apiReturnValue.Error.errorMessage = e.Message;
            }
            return apiReturnValue;
        }

        /*
        private ApiReturnValue<Lecturers> MakeReturnValue(bool isSuccess, List<Lecturer> lstLecturer, ApiReturnError error)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            apiReturnValue.IsSuccess = isSuccess;

            if (isSuccess)
            {
                Lecturers lecturers = new Lecturers() { TotalCount = lstLecturer.Count, PageSize = 0, PageNumber = 1, Rows = lstLecturer };
                apiReturnValue.Object = lecturers;
            }
            else
            {
                apiReturnValue.Object = null;
                apiReturnValue.Error = error;
            }

            return apiReturnValue;
        }
        */
        public async Task<ApiReturnValue<Lecturers>> GetLecturer(int pageSize = 0, int pageNumber = 1)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            try
            {
                var lstLecturer = JsonConvert.DeserializeObject<List<Lecturer>>(System.IO.File.ReadAllText(Path.Combine(CurrentDirectory, "Data", "Lecturer.json")));

                Lecturers lecturers = new Lecturers();
                lecturers.TotalCount = lstLecturer.Count();
                
                lecturers.PageNumber = pageNumber;
                lecturers.PageSize = pageSize;

                if(pageSize == 0)
                {
                    lecturers.Rows = lstLecturer;
                }
                else
                {
                    lecturers.Rows = lstLecturer.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }

                apiReturnValue.IsSuccess = true;
                apiReturnValue.Object = lecturers;

            }
            catch (Exception e)
            {
                apiReturnValue.IsSuccess = false;
                apiReturnValue.Object = null;
                apiReturnValue.Error.displayMessage = "";
                apiReturnValue.Error.errorMessage = e.Message;
            }

            return apiReturnValue;
        }
        public async Task<ApiReturnValue<Lecturers>> GetLecturer(string lecturerId)
        {

            var value = await Task.Run(() =>
            {
                ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

                try
                {
                    var lstLecturers = JsonConvert.DeserializeObject<List<Lecturer>>(System.IO.File.ReadAllText(Path.Combine(CurrentDirectory, "Data", "Lecturer.json")));


                    List<Lecturer> retLecturer = new List<Lecturer>();

                    Lecturers lecturers = new Lecturers();
                    lecturers.TotalCount = 70;
                    lecturers.PageNumber = 1;
                    lecturers.PageSize = 20;
                    lecturers.Rows = retLecturer;

                    apiReturnValue.IsSuccess = true;
                    apiReturnValue.Object = lecturers;
                }
                catch (Exception e)
                {
                    apiReturnValue.IsSuccess = false;

                }

                return apiReturnValue;

            });

            return value;
        }

        public async Task<ApiReturnValue<Lecturers>> UpdateLecturer(string lecturerId, Lecturer lecturer)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            await Task.Delay(1000);

            return apiReturnValue;
        }

        public async Task<ApiReturnValue<Lecturers>> DeleteLecturer(string lecturerId)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            await Task.Delay(1000);

            return apiReturnValue;
        }

        public async Task<ApiReturnValue<Lecturers>> CreateSampleLecturer()
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            List<Lecturer> sampleLecturers = new List<Lecturer>()
            {
                new Lecturer { LecturerId = "100001", GivenName = "James", LastName = "Oliver", EmailAddress = "James@xxx.com" },
                new Lecturer { LecturerId = "100002", GivenName = "Mary", LastName = "Amelia", EmailAddress = "Mary@xxx.com"},
                new Lecturer { LecturerId = "100003", GivenName = "John", LastName = "Jack", EmailAddress = "John@xxx.com" },
                new Lecturer { LecturerId = "100004", GivenName = "Patricia", LastName = "Olivia", EmailAddress = "Patricia@xxx.com" },
                new Lecturer { LecturerId = "100005", GivenName = "Robert", LastName = "Harry", EmailAddress = "Robert@xxx.com"},
                new Lecturer { LecturerId = "100006", GivenName = "Jennifer", LastName = "Isla", EmailAddress = "Jennifer@xxx.com" },
                new Lecturer { LecturerId = "100007", GivenName = "Michael", LastName = "Jacob", EmailAddress = "Michael@xxx.com" },
                new Lecturer { LecturerId = "100008", GivenName = "Linda", LastName = "Emily", EmailAddress = "Linda@xxx.com"},
                new Lecturer { LecturerId = "100009", GivenName = "William", LastName = "Charlie", EmailAddress = "William@xxx.com" },
                new Lecturer { LecturerId = "100010", GivenName = "Elizabeth", LastName = "Poppy", EmailAddress = "Elizabeth@xxx.com" },
                new Lecturer { LecturerId = "100011", GivenName = "David", LastName = "Thomas", EmailAddress = "David@xxx.com"},
                new Lecturer { LecturerId = "100012", GivenName = "Barbara", LastName = "Ava", EmailAddress = "Barbara@xxx.com" },
                new Lecturer { LecturerId = "100013", GivenName = "Richard", LastName = "George", EmailAddress = "Richard@xxx.com" },
                new Lecturer { LecturerId = "100013", GivenName = "Susan", LastName = "Isabella", EmailAddress = "Susan@xxx.com" },
                new Lecturer { LecturerId = "100014", GivenName = "Joseph", LastName = "Oscar", EmailAddress = "Joseph@xxx.com"},
                new Lecturer { LecturerId = "100015", GivenName = "Jessica", LastName = "Jessica", EmailAddress = "Jessica@xxx.com" },
                new Lecturer { LecturerId = "100016", GivenName = "Thomas", LastName = "James", EmailAddress = "Thomas@xxx.com" },
                new Lecturer { LecturerId = "100017", GivenName = "Sarah", LastName = "Liny", EmailAddress = "Sarah@xxx.com"},
                new Lecturer { LecturerId = "100018", GivenName = "Charles", LastName = "William", EmailAddress = "Charles@xxx.com" },
                new Lecturer { LecturerId = "100015", GivenName = "Karen", LastName = "Smith", EmailAddress = "Karen@xxx.com" },
                new Lecturer { LecturerId = "100016", GivenName = "Christopher", LastName = "Jones", EmailAddress = "Christopher@xxx.com" },
                new Lecturer { LecturerId = "100017", GivenName = "Nancy", LastName = "Davis", EmailAddress = "Nancy@xxx.com"},
                new Lecturer { LecturerId = "100018", GivenName = "Daniel", LastName = "Taylor", EmailAddress = "Daniel@xxx.com" },
            };
            await Task.Delay(1000);

            Lecturers = sampleLecturers;


            System.IO.File.WriteAllText(Path.Combine(CurrentDirectory,"Data", "Lecturer.json"), JsonConvert.SerializeObject(sampleLecturers));

            return apiReturnValue;
        }


    }
}
