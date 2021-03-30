using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.LecturerService
{
    public class LecturerMySqlService : ILecturerService
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        public MsSqlConnectionSettings ConnectionSettings { get; set; }
        private string ConnectionFile { get; set; }
        private SqlConnection SqlConn { get; set; }
        private DataConnectionSettings DataConnectionSettings { get; set; }
        public LecturerMySqlService(DataConnectionSettings dataConnectionSettings)
        {
            DataConnectionSettings = dataConnectionSettings;
            Lecturers = new List<Lecturer>();
        }

        private List<Lecturer> Lecturers { get; set; }

        public async Task<ApiReturnValue<Lecturers>> CreateLecturer(Lecturer lecturer)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();
            Lecturers.Add(lecturer);
            await Task.Delay(1000);

            return apiReturnValue;
        }

        public async Task<ApiReturnValue<Lecturers>> GetLecturer(string lecturerId)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            Lecturer lecturer = new Lecturer();
            await Task.Delay(1000);

            return apiReturnValue;
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
                new Lecturer { LecturerId = "100001", GivenName = "Thomas", LastName = "Williams", EmailAddress = "Thomas@xxx.com" },
                new Lecturer { LecturerId = "100002", GivenName = "Robert", LastName = "Brown", EmailAddress = "Robert@xxx.com"},
                new Lecturer { LecturerId = "100003", GivenName = "John", LastName = "Wilson", EmailAddress = "John@xxx.com" },
            };
            await Task.Delay(1000);

            Lecturers = sampleLecturers;

            return apiReturnValue;
        }



        private async Task<List<Lecturer>> GetLecturerById(string lecturerId)
        {
            return await Task.Run(() =>
            {
                string query = $"SELECT LecturerId, GivenName, LastName, EmailAddress FROM Lecturer WHERE lecturerId = '{lecturerId}';";

                List<Lecturer> lstLecturer = new List<Lecturer>();

                using (var db = new AppDb(DataConnectionSettings.ConnectionString))
                {
                    db.Connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, db.Connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Lecturer table = new Lecturer
                                {
                                    LecturerId = (string)reader["LecturerID"],
                                    GivenName = (string)reader["GivenName"],
                                    LastName = (string)reader["LastName"],
                                    EmailAddress = (string)reader["EmailAddress"]
                                };

                                lstLecturer.Add(table);
                            }
                        }
                        reader.Close();
                    }
                    db.Connection.Close();
                }


                return lstLecturer;

            });
        }



        public async Task<ApiReturnValue<Lecturers>> GetLecturer(int pageSize = 100, int pageNumber = 1)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();
            ///



            ////
            await Task.Delay(1000);

            return apiReturnValue;
        }


    }
}
